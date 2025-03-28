using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using web_banhang.connect;

namespace web_banhang.Controllers
{
    public class ProductController : Controller
    {
        private web_banhangEntities _dbContext = new web_banhangEntities();

        // GET: Product
        public ActionResult Index()
        {
            var products = _dbContext.products.ToList();

            if (!products.Any())
            {
                ViewBag.Message = "Không có sản phẩm nào!";
            }

            return View(products);
        }
    }
}
