using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetItemById(int id)
        {
            var items = db.Products.Where(x => x.ProductCategoryId == id).ToList();
            return PartialView("_GetItemById",items);
        }

        public ActionResult Detail(string title, int id)
        {
            var productSize = db.ProductSizes.Where(x => x.ProductId == id).ToList();
            var productSizeExit = productSize.Where(x => x.Quantity > 0).ToList();
            ViewBag.Productize = productSizeExit;
            var items = db.Products.Find(id);
            return View(items);
        }

        public ActionResult GetAllItem()
        {
            var items = db.Products.ToList();
            return PartialView("_GetAllItem",items);
        }

    }
}