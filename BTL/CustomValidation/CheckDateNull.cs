using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL.CustomValidation
{
    public class CheckDateNull:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return true;
        }
    }
}