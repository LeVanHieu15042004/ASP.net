using levanhieu_2122110522.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace levanhieu_2122110522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // Khởi tạo danh sách rỗng
        private static List<Category> _categories = new List<Category>();

        // GET: api/Category - Lấy tất cả category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return Ok(_categories);
        }

        // GET api/Category/5 - Lấy category theo ID
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound("Không tìm thấy category với ID này");
            }
            return Ok(category);
        }

        // POST api/Category - Thêm category mới
        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category newCategory)
        {
            // Xử lý trường hợp danh sách rỗng
            newCategory.Id = _categories.Any() ? _categories.Max(c => c.Id) + 1 : 1;
            _categories.Add(newCategory);

            // Trả về kết quả với status 201 Created
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        // PUT api/Category/5 - Cập nhật category
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category updatedCategory)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                return NotFound("Không tìm thấy category để cập nhật");
            }

            // Cập nhật thông tin
            existingCategory.Name = updatedCategory.Name;
            existingCategory.Image = updatedCategory.Image;

            return NoContent(); // Status 204 No Content
        }

        // DELETE api/Category/5 - Xóa category
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound("Không tìm thấy category để xóa");
            }

            _categories.Remove(category);
            return NoContent(); // Status 204 No Content
        }
    }
}