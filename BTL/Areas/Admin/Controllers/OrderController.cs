using BTL.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BTL.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int ? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var items = db.Orders.OrderByDescending(x => x.CreateDate).ToPagedList(pageIndex, pageSize);
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.index = pageIndex;
            ViewBag.pageSize = pageSize;
            return View(items);
        }
        
        public ActionResult Detail(int id)
        {
            var item = db.Orders.FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        public ActionResult Partial_OrderDetail(int id)
        {
            var item = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(item);   
        }

        public ActionResult UpdateOrder(int id, int status)
        {
            var item = db.Orders.Find(id);
            if(item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = status;  
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "False", Success = false });
        }
    }
}