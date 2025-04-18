using levanhieu_2122110522.Data;
using levanhieu_2122110522.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace levanhieu_2122110522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            return await _context.Posts
                .Where(p => p.DeletedAt == null)
                .ToListAsync();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetById(long id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null || post.DeletedAt != null)
                return NotFound("Không tìm thấy bài viết");

            return post;
        }

        // POST: api/Post
        [HttpPost]
        public async Task<ActionResult<Post>> Create(Post post)
        {
            post.Id = 0; // tránh lỗi IDENTITY
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Post updatedPost)
        {
            if (id != updatedPost.Id)
                return BadRequest("ID không khớp");

            var post = await _context.Posts.FindAsync(id);
            if (post == null || post.DeletedAt != null)
                return NotFound("Không tìm thấy bài viết để cập nhật");

            // Cập nhật dữ liệu
            post.Title = updatedPost.Title;
            post.Slug = updatedPost.Slug;
            post.Content = updatedPost.Content;
            post.Description = updatedPost.Description;
            post.Thumbnail = updatedPost.Thumbnail;
            post.Status = updatedPost.Status;
            post.UpdatedBy = updatedPost.UpdatedBy;
            post.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Post/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null || post.DeletedAt != null)
                return NotFound("Không tìm thấy bài viết để xoá");

            post.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
