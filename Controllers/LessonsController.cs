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
using Banq.DTOs;
using Banq.ViewModels;

namespace Banq.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Supervisor},{UserRoles.Teacher}")]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LessonsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetLessons()
        {
            return await _context.Lessons.Select(x => x.ToLessonViewModel()).ToListAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LessonViewModel>> GetLesson(string id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson.ToLessonViewModel();
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(string id, LessonDTO lessonDTO)
        {
            var lesson = lessonDTO.ToLesson();
            if (id != lesson.Code)
            {
                return BadRequest();
            }
            if (!LessonExists(id))
            {
                return NotFound();
            }
            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(LessonDTO lessonDTO)
        {
            var lesson = lessonDTO.ToLesson();
            if (LessonExists(lesson.Code))
            {
                return Conflict();
            }
            _context.Lessons.Add(lesson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetLesson", new { id = lesson.Code }, lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(string id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(string id)
        {
            return _context.Lessons.Any(e => e.Code == id);
        }
    }
}
