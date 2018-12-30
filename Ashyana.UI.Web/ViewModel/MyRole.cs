using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ashyana.UI.Web.ViewModel
{
    public class MyRole
    {

        public int roleID { get; set; }
        [Display(Name = "Name")]
        public string roleName { get; set; }
        [Display(Name = "Desc")]
        public string roleDescription { get; set; }
        [Display(Name = "Delete")]
        public int roleDelete { get; set; }
        [Display(Name = "CreatedBy ")]
        public Nullable<int> roleCreatedBy { get; set; }
        public Nullable<System.DateTime> roleCreatedOn { get; set; }
        public Nullable<int> roleDeletedBy { get; set; }

        public Nullable<System.DateTime> roleDeletedOn { get; set; }
        public Nullable<int> roleUpdatedBy { get; set; }
        public Nullable<System.DateTime> roleUpdatedOn { get; set; }
    }
}
