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
using Banq.DTOs;
using Banq.ViewModels;

namespace Banq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSetsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionSetsController(DatabaseContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: api/QuestionSets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionSetViewModel>>> GetQuestionSets()
        {
            return await _context.QuestionSets.Include(x => x.Lesson).Include(x => x.Field).Include(x => x.Author).Select(x => x.ToQuestionSetViewModel()).ToListAsync();
        }

        // GET: api/QuestionSets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionSetViewModel>> GetQuestionSet(ulong id)
        {
            var QuestionSet = await _context.QuestionSets.Include(x => x.Lesson).Include(x => x.Field).Include(x => x.Author).Where(x => x.Id == id).FirstAsync();

            if (QuestionSet == null)
            {
                return NotFound();
            }

            return QuestionSet.ToQuestionSetViewModel();
        }
        [HttpGet("SeacrhQuestionSet")]
        public async Task<IActionResult> SearchQuestionSets(int grade, string fieldName = "", string lessonName = "")
        {
            if (!string.IsNullOrEmpty(fieldName) && !await _context.Fields.AnyAsync(x => x.Name == fieldName))
            {
                return BadRequest("Field not found!");
            }

            if (!string.IsNullOrEmpty(lessonName) && !await _context.Lessons.AnyAsync(x => x.Name == lessonName))
            {
                return BadRequest("Lesson not found!");
            }

            if (grade != 0 && (grade < 7 || grade > 12))
            {
                return BadRequest("Unavailable grade");
            }

            var query = _context.QuestionSets.Include(x => x.Lesson).Include(x => x.Field).Include(x => x.Author).AsQueryable();

            if (!string.IsNullOrEmpty(fieldName))
            {
                query = query.Where(x => x.Field.Name == fieldName);
            }

            if (!string.IsNullOrEmpty(lessonName))
            {
                query = query.Where(x => x.Lesson.Name == lessonName);
            }

            if (grade != 0)
            {
                query = query.Where(x => x.Grade == (Grade)grade);
            }

            var filterResult = await query.Select(x => x.ToQuestionSetViewModel()).ToListAsync();
            return Ok(filterResult);
        }

        // PUT: api/QuestionSets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher},{UserRoles.Supervisor}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestionSet(ulong id, QuestionSetDTO QuestionSetDTO)
        {
            if (!await _context.QuestionSets.AnyAsync(x => x.Id == id))
            {
                return BadRequest("QuestionSet not found!");
            }

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            var QuestionSet = await QuestionSetDTO.ToQuestionSet(_context, user, id);
            if (id != QuestionSet.Id)
            {
                return BadRequest();
            }

            _context.Entry(QuestionSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionSetExists(id))
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

        // POST: api/QuestionSets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpPost]
        public async Task<ActionResult<QuestionSet>> PostQuestionSet(QuestionSetDTO QuestionSetDTO)
        {
            if (!await _context.Fields.AnyAsync(x => x.Name == QuestionSetDTO.FieldName))
            {
                return BadRequest("Field not found!");
            }

            if (!await _context.Lessons.AnyAsync(x => x.Name == QuestionSetDTO.LessonName))
            {
                return BadRequest("Lesson not found!");
            }

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            var QuestionSet = await QuestionSetDTO.ToQuestionSet(_context, user, 0);
            await _context.QuestionSets.AddAsync(QuestionSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionSet", new { id = QuestionSet.Id }, QuestionSet);
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Supervisor},{UserRoles.Teacher}")]
        [HttpGet("ChangeQuestionSetStatus/{id}")]
        public async Task<IActionResult> ChangeQuestionSetStatus(ulong id, Status status)
        {
            if (!await _context.QuestionSets.AnyAsync(x => x.Id == id))
            {
                return NotFound("Question not found");
            }

            var questionSet = await _context.QuestionSets.FindAsync(id);
            questionSet.Status = status;
            await _context.SaveChangesAsync();
            return Ok("Status Changed");
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher}")]
        [HttpPost("UploadQuestionSetFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, ulong id)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }

            if (!await _context.QuestionSets.AnyAsync(x => x.Id == id))
            {
                return BadRequest("QuestionSet not found");
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
            var QuestionSet = await _context.QuestionSets.FindAsync(id);
            QuestionSet.FileLink = filePath;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher},{UserRoles.Supervisor}")]
        [HttpGet("DownloadQuestionSetFile")]
        public async Task<IActionResult> DownloadFile(ulong id)
        {
            if (!await _context.QuestionSets.AnyAsync(x => x.Id == id))
            {
                return BadRequest("QuestionSet not found");
            }
            var QuestionSet = await _context.QuestionSets.FindAsync(id);
            var filePath = Path.Combine(QuestionSet.FileLink);
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


        // DELETE: api/QuestionSets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionSet(ulong id)
        {
            var QuestionSet = await _context.QuestionSets.FindAsync(id);
            if (QuestionSet == null)
            {
                return NotFound();
            }

            _context.QuestionSets.Remove(QuestionSet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionSetExists(ulong id)
        {
            return _context.QuestionSets.Any(e => e.Id == id);
        }
    }
}
