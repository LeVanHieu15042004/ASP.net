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
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Order - Lấy tất cả Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await _context.Orders
                .Include(o => o.User) // nếu muốn kèm thông tin User
                .ToListAsync();
        }

        // GET: api/Order/5 - Lấy Order theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound("Không tìm thấy Order với ID này");

            return order;
        }

        // POST: api/Order - Thêm mới Order
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order newOrder)
        {
            newOrder.Id = 0;         // tránh lỗi IDENTITY_INSERT
            newOrder.User = null;    // không thêm lại User

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
        }

        // PUT: api/Order/5 - Cập nhật Order
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order updatedOrder)
        {
            if (id != updatedOrder.Id)
                return BadRequest("ID không khớp");

            // Đảm bảo không update user
            updatedOrder.User = null;

            _context.Entry(updatedOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Order/5 - Xoá Order
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound("Không tìm thấy Order để xoá");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Order/by-user/5 - Lấy đơn hàng theo User
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetByUserId(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .ToListAsync();

            return Ok(orders);
        }
    }
}
