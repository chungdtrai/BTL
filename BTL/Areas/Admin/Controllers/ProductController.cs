using BTL.Models;
using BTL_MONTHAYTHE.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(int ? page)
        {
            var pageSize = 5;
            if(page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var items = db.Products.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.index = pageIndex;
            ViewBag.pageSize = pageSize;    
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, HttpPostedFileBase image)
        {
            if(ModelState.IsValid)
            {
                model.IsActive = true;
                db.Products.Add(model);
                db.SaveChanges();
                if(image != null && image.ContentLength > 0)
                {
                    int id = int.Parse(db.Products.ToList().Last().Id.ToString());
                    string fileName = "";
                    int index = image.FileName.IndexOf(".");
                    fileName = "product" + id.ToString() + "." + image.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/Upload"), fileName);
                    image.SaveAs(path);
                    var product = db.Products.FirstOrDefault(x => x.Id == id);
                    product.Image = fileName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }

        

        public ActionResult Edit(int id)
        {
            var item = db.Products.Find(id);
            return View(item);
        }

    }
}