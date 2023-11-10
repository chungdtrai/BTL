using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BTL_MONTHAYTHE.Models.Entity;

namespace BTL.Models.Entity
{
    public class ProductSize
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
    }
}