using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banq.Database;
using Banq.Database.Entities;

namespace Banq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SchoolsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/School
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools()
        {
            return await _context.Schools.ToListAsync();
        }

        // GET: api/School/5
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(string id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }

        // PUT: api/School/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(string id, School school)
        {
            if (id != school.Code)
            {
                return BadRequest();
            }

            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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

        // POST: api/School
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            _context.Schools.Add(school);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchoolExists(school.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSchool", new { id = school.Code }, school);
        }

        // DELETE: api/School/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(string id)
        {
            return _context.Schools.Any(e => e.Code == id);
        }
    }
}
