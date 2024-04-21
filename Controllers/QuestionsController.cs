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
            return await _context.Questions.Include(x=>x.QuestionSet).Select(x => x.ToQuestionViewModel()).ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionViewModel>> GetQuestion(ulong id)
        {
            var Question = await _context.Questions.Include(x => x.QuestionSet).Where(x => x.Id == id).FirstAsync();

            if (Question == null)
            {
                return NotFound();
            }

            return Question.ToQuestionViewModel();
        }

        [HttpGet("GetQuestionByQuestionSetId/{id}")]
        public async Task<ActionResult<List<QuestionViewModel>>> GetQuestionByQuestionSetId(ulong id)
        {
            var Questions = await _context.Questions.Include(x => x.QuestionSet).Where(x => x.QuestionSet.Id == id).ToListAsync();

            if (Questions == null)
            {
                return NotFound();
            }

            return Questions.Select(x => x.ToQuestionViewModel()).ToList();
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Supervisor}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(ulong id, QuestionDTO QuestionDTO)
        {
            if (!await _context.Questions.AnyAsync(x => x.Id == id))
            {
                return BadRequest("Question not found!");
            }

            var Question = await QuestionDTO.ToQuestion(_context);
            if (id != Question.Id)
            {
                return BadRequest();
            }

            _context.Entry(Question).State = EntityState.Modified;

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
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Supervisor}")]
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO QuestionDTO)
        {
            if (!await _context.QuestionSets.AnyAsync(x => x.Id == QuestionDTO.QuestionSetId))
            {
                return BadRequest("Question Set not found!");
            }

            var Question = await QuestionDTO.ToQuestion(_context);
            await _context.Questions.AddAsync(Question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = Question.Id }, Question);
        }

        // DELETE: api/Questions/5
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Supervisor}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(ulong id)
        {
            var Question = await _context.Questions.FindAsync(id);
            if (Question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(Question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(ulong id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
