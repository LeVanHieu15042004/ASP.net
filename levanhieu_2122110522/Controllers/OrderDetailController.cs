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
    public class OrderDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderDetailController(AppDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả OrderDetail
        // GET: api/OrderDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAll()
        {
            var orderDetails = await _context.OrderDetails.ToListAsync();
            return Ok(orderDetails);
        }

        // Lấy OrderDetail theo id
        // GET: api/OrderDetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetById(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
                return NotFound(new { message = "Không tìm thấy OrderDetail" });

            return Ok(orderDetail);
        }

        // Thêm mới OrderDetail
        // POST: api/OrderDetail
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> Create([FromBody] OrderDetail newOrderDetail)
        {
            _context.OrderDetails.Add(newOrderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newOrderDetail.Id }, newOrderDetail);
        }

        // Cập nhật OrderDetail
        // PUT: api/OrderDetail/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetail updatedOrderDetail)
        {
            if (id != updatedOrderDetail.Id)
                return BadRequest(new { message = "Id không khớp" });

            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
                return NotFound(new { message = "Không tìm thấy OrderDetail" });

            // Cập nhật từng field
            orderDetail.OrderId = updatedOrderDetail.OrderId;
            orderDetail.ProductId = updatedOrderDetail.ProductId;
            orderDetail.Quantity = updatedOrderDetail.Quantity;
            orderDetail.Trash = updatedOrderDetail.Trash;
            orderDetail.Status = updatedOrderDetail.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Xoá OrderDetail
        // DELETE: api/OrderDetail/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
                return NotFound(new { message = "Không tìm thấy OrderDetail để xoá" });

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/OrderDetail/by-order/2
        [HttpGet("by-order/{orderId}")]
        public async Task<ActionResult> GetOrderDetail(int orderId)
        {
            var orderDetails = await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Product) // load thêm thông tin Product
                .ToListAsync();

            return Ok(orderDetails);
        }

    }
}
