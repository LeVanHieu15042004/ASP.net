using levanhieu_2122110522.Data;
using levanhieu_2122110522.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace levanhieu_2122110522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Category - Lấy tất cả Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Category/5 - Lấy Category theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound("Không tìm thấy Category với ID này");

            return category;
        }

        // POST: api/Category - Thêm mới Category
        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] Category newCategory)
        {
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/Category/5 - Cập nhật Category
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updatedCategory)
        {
            if (id != updatedCategory.Id)
                return BadRequest("ID không khớp");

            _context.Entry(updatedCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Category/5 - Xoá Category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound("Không tìm thấy Category để xoá");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
