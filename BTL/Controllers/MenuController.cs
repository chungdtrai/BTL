using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {        
            return PartialView("_MenuTop");
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuArrival()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuArrival", items);
        }
    }
}