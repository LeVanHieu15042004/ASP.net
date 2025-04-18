using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using levanhieu_2122110522.Data;
using levanhieu_2122110522.Model;

namespace levanhieu_2122110522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
        {
            return await _context.Brands.ToListAsync();
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetById(long id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                return NotFound("Không tìm thấy thương hiệu");

            return brand;
        }

        // POST: api/Brand
        [HttpPost]
        public async Task<ActionResult<Brand>> Create([FromBody] Brand brand)
        {
            brand.Id = 0; // tránh lỗi IDENTITY
            brand.CreatedAt = DateTime.Now;

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
        }

        // PUT: api/Brand/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Brand brand)
        {
            if (id != brand.Id)
                return BadRequest("ID không khớp");

            brand.UpdatedAt = DateTime.Now;

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Brands.Any(e => e.Id == id))
                    return NotFound("Không tìm thấy thương hiệu để cập nhật");

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Brand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                return NotFound("Không tìm thấy thương hiệu để xoá");

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
