using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ashyana.UI.Web.CustomValidation
{
    public class DueDate :ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return ((DateTime)value).Date > DateTime.Now.Date;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


    }
}