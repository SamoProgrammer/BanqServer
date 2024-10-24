using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banq.Database;
using Banq.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Banq.Authentication;
using Microsoft.AspNetCore.Authorization;
using Banq.ViewModels;
using Banq.DTOs;
using Microsoft.Extensions.Caching.Distributed;

namespace Banq.Controllers
{
    // [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDistributedCache _cache;

        public CommentsController(DatabaseContext context, UserManager<ApplicationUser> userManager, IDistributedCache cache)
        {
            _context = context;
            _userManager = userManager;
            _cache = cache;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetComments()
        {
            return await _context.Comments.Include(x => x.User).Select(x => x.ToCommentViewModel()).ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentViewModel>> GetComment(ulong id)
        {
            var comment = await _cache.GetAsync($"comment-{id}", async token =>
            {
                var product = await _context.Comments.Include(x => x.User).Where(x => x.Id == id).FirstAsync();

                return product;
            });

            if (comment == null)
            {
                return NotFound();
            }

            return comment.ToCommentViewModel();
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Manager + "," + UserRoles.Teacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(ulong id, CommentDTO comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            if (!CommentExists(id))
            {
                return NotFound();
            }

            _context.Entry(comment).State = EntityState.Modified;

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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Manager + "," + UserRoles.Teacher)]
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentDTO commentDTO)
        {
            var comment = await commentDTO.ToComment(null, _userManager);
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(ulong id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(ulong id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }

    }
}
