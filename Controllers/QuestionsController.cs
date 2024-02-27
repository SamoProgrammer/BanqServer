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

namespace Banq.Controllers
{
    // [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Manager + "," + UserRoles.Teacher)]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public QuestionsController(DatabaseContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionViewModel>>> GetQuestions()
        {
            return await _context.Questions.Include(x => x.Lesson).Include(x => x.Field).Select(x => x.ToQuestionViewModel()).ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionViewModel>> GetQuestion(ulong id)
        {
            var question = await _context.Questions.Include(x => x.Lesson).Include(x => x.Field).Where(x => x.Id == id).FirstAsync();

            if (question == null)
            {
                return NotFound();
            }

            return question.ToQuestionViewModel();
        }

        // public async Task<IActionResult<QuestionViewModel>> SearchQuestions(){

        // }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(ulong id, QuestionDTO questionDTO)
        {
            if (!await _context.Questions.AnyAsync(x => x.Id == id))
            {
                return BadRequest("Question not found!");
            }

            var question = await questionDTO.ToQuestion(_context, id);
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

            var question = await questionDTO.ToQuestion(_context, 0);
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }


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
