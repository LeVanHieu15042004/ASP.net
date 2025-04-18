using levanhieu_2122110522.Data;
using levanhieu_2122110522.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace levanhieu_2122110522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Product - Lấy tất cả Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        // GET: api/Product/5 - Lấy Product theo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound("Không tìm thấy Product với ID này");

            return product;
        }

        // POST: api/Product - Thêm mới Product
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product newProduct)
        {
            var category = await _context.Categories.FindAsync(newProduct.CategoryId);
            if (category == null)
                return BadRequest("CategoryId không tồn tại");

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/Product/5 - Cập nhật Product
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest("ID không khớp");

            var category = await _context.Categories.FindAsync(updatedProduct.CategoryId);
            if (category == null)
                return BadRequest("CategoryId không tồn tại");

            _context.Entry(updatedProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Product/5 - Xoá Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound("Không tìm thấy Product để xoá");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //-------------------------------------------
        // GET: api/Product/by-category/3
        [HttpGet("by-category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryId(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Product/detail/3
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<Product>> GetDetailById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm" });

            return Ok(product);
        }



    }
}
