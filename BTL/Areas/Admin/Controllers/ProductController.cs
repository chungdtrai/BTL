using BTL.Models;
using BTL.Models.Entity;
using BTL_MONTHAYTHE.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BTL.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var items = db.Products.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.index = pageIndex;
            ViewBag.pageSize = pageSize;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.Size = db.Sizes.ToList();
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, HttpPostedFileBase image, int? S_1, int? M_2, int? L_3, int? XL_4)
        {
            if (ModelState.IsValid)
            {
                if (S_1 == null) S_1 = 0;
                if (M_2 == null) M_2 = 0;
                if (L_3 == null) L_3 = 0;
                if (XL_4 == null) XL_4 = 0;
                model.Quantity = (int)(S_1 + M_2 + L_3 + XL_4);
                model.IsActive = true;
                List<ProductSize> ps = new List<ProductSize>
                {
                    new ProductSize{ProductId = model.Id, SizeId = 1, Quantity = (int)S_1 },
                    new ProductSize{ProductId = model.Id, SizeId = 2, Quantity = (int)M_2 },
                    new ProductSize{ProductId = model.Id, SizeId = 3, Quantity = (int)L_3 },
                    new ProductSize{ProductId = model.Id, SizeId = 4, Quantity = (int)XL_4 }
                };
                db.ProductSizes.AddRange(ps);
                db.Products.Add(model);
                db.SaveChanges();
                if (image != null && image.ContentLength > 0)
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
            ViewBag.Size = db.Sizes.ToList();
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }



        public ActionResult Edit(int id)
        {
            var item = db.Products.Find(id);
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            var productSize = db.ProductSizes.Where(x => x.ProductId == id).OrderBy(x => x.SizeId).ToList();
            ViewBag.ProductSize = productSize;
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, HttpPostedFileBase image, int? S_1, int? M_2, int? L_3, int? XL_4)
        {
            if (ModelState.IsValid)
            {
                if (S_1 == null) S_1 = 0;
                if (M_2 == null) M_2 = 0;
                if (L_3 == null) L_3 = 0;
                if (XL_4 == null) XL_4 = 0;
                model.Quantity = (int)(S_1 + M_2 + L_3 + XL_4);
                var psOlder = db.ProductSizes.Where(x => x.ProductId == model.Id);
                db.ProductSizes.RemoveRange(psOlder);
                List<ProductSize> psNew = new List<ProductSize>
                {
                    new ProductSize{ProductId = model.Id, SizeId = 1, Quantity = (int)S_1 },
                    new ProductSize{ProductId = model.Id, SizeId = 2, Quantity = (int)M_2 },
                    new ProductSize{ProductId = model.Id, SizeId = 3, Quantity = (int)L_3 },
                    new ProductSize{ProductId = model.Id, SizeId = 4, Quantity = (int)XL_4 }
                };
                db.Products.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.ProductSizes.AddRange(psNew);
                db.SaveChanges();
                if (image != null && image.ContentLength > 0)
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

        public ActionResult FilterItem(int? id, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var items = db.Products.Where(x => x.ProductCategoryId == id).OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.index = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.ProductCategoryIdPresent = id;
            return View(items);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var item = db.Products.Find(id);
            var productSize = db.ProductSizes.Where(x => x.ProductId == id).ToList();


            if (item != null)
            {
                foreach (var i in productSize)
                {
                    db.ProductSizes.Remove(i);
                }
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int? id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsProductCodeAvailable(string productCode)
        {
            var item = db.Products.Where(x => x.ProductCode == productCode).FirstOrDefault();
            if (item != null)
            {
                return Json(false);
            }
            return Json(true);
        }
        [HttpPost]
        public ActionResult FindItemByName(string stringSearch, int? categoryId, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var item_result = db.Products.OrderByDescending(x => x.Id).ToPagedList(pageIndex, 1000);
            if (categoryId == 0)
            {
                item_result = item_result.Where(x => x.Title.IndexOf(stringSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .OrderByDescending(x => x.Id)
                                    .ToPagedList(pageIndex, pageSize);
            }
            else
            {
                var items = db.Products.Where(x => x.ProductCategoryId == categoryId).ToList();
                item_result = items.Where(x => x.Title.IndexOf(stringSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .OrderByDescending(x => x.Id)
                                    .ToPagedList(pageIndex, pageSize);

            }
            ViewBag.ProductCategoryIdPresent = categoryId;
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.index = pageIndex;
            ViewBag.pageSize = pageSize;
            return View(item_result);
        }
    }
}