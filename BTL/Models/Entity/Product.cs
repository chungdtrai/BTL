using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL.Models.Entity;

namespace BTL_MONTHAYTHE.Models.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        [StringLength(150)]
        [Remote(action:"IsProductCodeAvailable",controller: "Product",areaName:"Admin",HttpMethod = "POST", ErrorMessage ="Mã sản phẩm đã tồn tại")]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Giá sản phâm không được để trống")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Số lượng sản phẩm không được để trống")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Chưa lựa chọn danh mục sản phẩm")]
        public int ProductCategoryId { get; set; }
   
        public bool IsActive { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
    }
}