using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banq.Database;
using Banq.Database.Entities;
using Banq.ViewModels;
using Banq.DTOs;
using Microsoft.AspNetCore.Authorization;
using Banq.Authentication;

namespace Banq.Controllers
{
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Supervisor},{UserRoles.Teacher}")]
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FieldsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Fields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldViewModel>>> GetFields()
        {
            return await _context.Fields.Select(x => x.ToFieldViewModel()).ToListAsync();
        }

        // GET: api/Fields/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldViewModel>> GetField(string id)
        {
            var field = await _context.Fields.FindAsync(id);

            if (field == null)
            {
                return NotFound();
            }

            return field.ToFieldViewModel();
        }

        // PUT: api/Fields/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutField(string id, FieldDTO fieldDTO)
        {
            var field = fieldDTO.ToField();
            if (id != field.Code)
            {
                return BadRequest();
            }

            _context.Entry(field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(id))
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

        // POST: api/Fields
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FieldViewModel>> PostField(FieldDTO field)
        {
            _context.Fields.Add(field.ToField());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FieldExists(field.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetField", new { id = field.Code }, field);
        }

        // DELETE: api/Fields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(string id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FieldExists(string id)
        {
            return _context.Fields.Any(e => e.Code == id);
        }
    }
}
