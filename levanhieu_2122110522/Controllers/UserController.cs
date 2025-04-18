using levanhieu_2122110522.Data;
using levanhieu_2122110522.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace levanhieu_2122110522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả User
        // GET: api/User
        [Authorize] // Bắt buộc phải có token mới call API này đượ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // Lấy User theo id
        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "Không tìm thấy user" });

            return Ok(user);
        }

        // Thêm mới User
        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        // Cập nhật User
        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest(new { message = "Id không khớp" });

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "Không tìm thấy user" });

            // ✅ Cập nhật cả Username
            user.Username = updatedUser.Username;
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Xoá User
        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "Không tìm thấy user để xoá" });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
