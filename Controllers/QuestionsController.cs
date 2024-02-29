using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banq.Database;
using Banq.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Banq.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Banq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(DatabaseContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionViewModel>>> GetQuestions()
        {
            return await _context.Questions.Include(x => x.Lesson).Include(x => x.Field).Include(x => x.Author).Select(x => x.ToQuestionViewModel()).ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionViewModel>> GetQuestion(ulong id)
        {
            var question = await _context.Questions.Include(x => x.Lesson).Include(x => x.Field).Include(x => x.Author).Where(x => x.Id == id).FirstAsync();

            if (question == null)
            {
                return NotFound();
            }

            return question.ToQuestionViewModel();
        }
        [HttpGet("SeacrhQuestion")]
        public async Task<IActionResult> SearchQuestions(string fieldName, string lessonName, Grade grade)
        {
            if (!await _context.Fields.AnyAsync(x => x.Name == fieldName))
            {
                return BadRequest("Field not found!");
            }

            if (!await _context.Lessons.AnyAsync(x => x.Name == lessonName))
            {
                return BadRequest("Lesson not found!");
            }

            return Ok(await _context.Questions.Include(x => x.Lesson).Include(x => x.Field).Include(x => x.Author).Where(x => x.Lesson.Name == lessonName && x.Field.Name == fieldName && x.Grade == grade).Select(x => x.ToQuestionViewModel()).ToListAsync());
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(ulong id, QuestionDTO questionDTO)
        {
            if (!await _context.Questions.AnyAsync(x => x.Id == id))
            {
                return BadRequest("Question not found!");
            }

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            var question = await questionDTO.ToQuestion(_context, user, id);
            if (id != question.Id)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questionDTO)
        {
            if (!await _context.Fields.AnyAsync(x => x.Name == questionDTO.FieldName))
            {
                return BadRequest("Field not found!");
            }

            if (!await _context.Lessons.AnyAsync(x => x.Name == questionDTO.LessonName))
            {
                return BadRequest("Lesson not found!");
            }

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            var question = await questionDTO.ToQuestion(_context, user, 0);
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpPost("UploadQuestionFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, ulong id)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }

            if (!await _context.Questions.AnyAsync(x => x.Id == id))
            {
                return BadRequest("Question not found");
            }



            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            switch (fileExtension)
            {
                case ".docx":
                    break;
                case ".doc":
                    break;
                case ".pdf":
                    break;
                default:
                    return BadRequest("Only document files are allowed = " + fileExtension);
            }
            System.Console.WriteLine(_hostEnvironment.WebRootPath);
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Uploads");

            // if (Path.Exists(Path.Combine(uploadsFolder, id + fileExtension)))
            // {
            //     return BadRequest("File duplicated");
            // }

            // if (!Directory.Exists(uploadsFolder))
            // {
            //     Directory.CreateDirectory(uploadsFolder);
            // }

            string filePath = Path.Combine(uploadsFolder, id + fileExtension);
            System.Console.WriteLine(filePath);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            var question = await _context.Questions.FindAsync(id);
            question.FileLink = filePath;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpGet("DownloadQuestionFile")]
        public async Task<IActionResult> DownloadFile(ulong id)
        {
            if (!await _context.Questions.AnyAsync(x => x.Id == id))
            {
                return BadRequest("Question not found");
            }
            var question = await _context.Questions.FindAsync(id);
            var filePath = Path.Combine(question.FileLink);
            if (System.IO.File.Exists(filePath))
            {
                var fileStream = System.IO.File.OpenRead(filePath);
                return File(fileStream, "application/octet-stream", fileStream.Name, enableRangeProcessing: true);
            }
            else
            {
                return NotFound();
            }
        }


        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(ulong id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(ulong id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
