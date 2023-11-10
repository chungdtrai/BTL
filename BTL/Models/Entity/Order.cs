﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_MONTHAYTHE.Models.Entity
{
    public class Order
    {
        public Order() { 
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }
        public string Email { get; set; }   
        public decimal TatablAmount { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Quantity { get; set; }

        public int TypePayment { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}