using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_MONTHAYTHE.Models.Entity
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }       
        public string Password { get; set; }    
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string phone { get; set; }   
    }
}