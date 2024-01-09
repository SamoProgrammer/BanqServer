// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Banq.Database;
// using Banq.Database.Entities;

// namespace Banq.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class TeachersController : ControllerBase
//     {
//         private readonly DatabaseContext _context;

//         public TeachersController(DatabaseContext context)
//         {
//             _context = context;
//         }

//         // GET: api/Teachers
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
//         {
//             return await _context.Teachers.ToListAsync();
//         }

//         // GET: api/Teachers/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Teacher>> GetTeacher(string id)
//         {
//             var teacher = await _context.Teachers.FindAsync(id);

//             if (teacher == null)
//             {
//                 return NotFound();
//             }

//             return teacher;
//         }

//         // PUT: api/Teachers/5
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPut("{id}")]
//         public async Task<IActionResult> Edit(string id, Teacher teacher)
//         {
//             if (id != teacher.PersonnelCode)
//             {
//                 return BadRequest();
//             }

//             _context.Entry(teacher).State = EntityState.Modified;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!TeacherExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }

//             return NoContent();
//         }
//         // private bool TeacherExists(string id)
//         // {
//         //     return _context.Teachers.Any(e => e.PersonnelCode == id);
//         // }
//     }
// }
