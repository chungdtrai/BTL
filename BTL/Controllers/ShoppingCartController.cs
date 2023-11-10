using BTL.Models;
using BTL.Models.Entity;
using BTL_MONTHAYTHE.Models.Entity;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BTL.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                return View(cart.CartItems);
            }
            return View();
        }
   

        public ActionResult ShowCount()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart!= null)
            {
                return Json(new {count = cart.CartItems.Count}, JsonRequestBehavior.AllowGet);
            }
            return Json(new {count = 0}, JsonRequestBehavior.AllowGet);   
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity, string sizeName)
        {
            var code = new { Success = false, msg = "", code = -1, count=0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                Cart cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    cart = new Cart();
                }

                CartItem item = new CartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    ProductImange = checkProduct.Image,
                    CategoryName = checkProduct.ProductCategory.Title,
                    ProductSize = sizeName,
                    Quantity = quantity,
                    Price = checkProduct.Price,
                    TotalPrice = checkProduct.Price * quantity
                };
               
                cart.AddToCart(item, quantity, sizeName);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm thành công", code = 1, count = cart.CartItems.Count};
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                var checkProduct = cart.CartItems.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = code = new { Success = true, msg = "", code = 1, count = cart.CartItems.Count };
                }
            }
            return Json(code);  
        }
        public ActionResult CheckOut()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                ViewBag.checkCart = cart;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(Order model)
        {
            if(ModelState.IsValid)
            {
                Cart cart = (Cart)Session["Cart"];
                if(cart != null)
                {
                    Order order = new Order();
                    order.CustomerName = model.CustomerName;
                    order.Phone = model.Phone;
                    order.Address = model.Address;
                    cart.CartItems.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = model.Id,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                    }));
                    order.TatablAmount = cart.CartItems.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = model.TypePayment;
                    order.CreateDate = DateTime.Now;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next();
                    db.Orders.Add(order);
                    //send mail
                    var strSanPham = "";
                    var tongTien = decimal.Zero;
                    foreach (var sp in cart.CartItems)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + sp.Price + "$</td>";
                        strSanPham += "</tr>";
                        tongTien += sp.Price * sp.Quantity;
                    }          
                    string contentCustomer =System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentCustomer=contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer=contentCustomer.Replace("{{NgayDat}}", order.CreateDate.Value.ToString("dd/MM/yyyy"));
                    contentCustomer=contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer=contentCustomer.Replace("{{TenKhachHang}}", model.CustomerName);
                    contentCustomer=contentCustomer.Replace("{{DiaChi}}", model.Address);
                    contentCustomer=contentCustomer.Replace("{{SoDienThoai}}", model.Phone);
                    contentCustomer=contentCustomer.Replace("{{Email}}", model.Email);
                    contentCustomer=contentCustomer.Replace("{{TongTien}}", tongTien+"$");
                    BTL.Common.Common.SendMail("ShopTuyetChinh", "Đơn hàng #" + order.Code, contentCustomer.ToString(), model.Email);
                    cart.ClearCart();
                    db.SaveChanges();
                }
            }
            ViewBag.Email = model.Email;
            return View("OrderSuccess");
        }
        public ActionResult Partial_Cart_Item()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.CartItems);
            }
            return PartialView();
        }
    }
}