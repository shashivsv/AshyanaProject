using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ashyana.UI.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Ashyana.UI.Web.ViewModel
{
    public class MapLinks
    {
        [Display(Name="userLink")]
        public int userLinkMapID { get; set; }

        public Nullable<int> roleID { get; set; }
        public string roleName { get; set; }
        public string userName { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> linkID { get; set; }
        public Nullable<int> subLinkID { get; set; }
         [Display(Name = "View")]
        public Nullable<int> sublinkView { get; set; }
         [Display(Name = "Add")]
        public Nullable<int> sublinkAdd { get; set; }
         [Display(Name = "Update")]
        public Nullable<int> sublinkUpdate { get; set; }
         [Display(Name = "Delete")]
        public Nullable<int> sublinkDelete { get; set; }
         [Display(Name = "Print")]
        public Nullable<int> sublinkPrint { get; set; }
         [Display(Name = "LinkCheck")]
        public int linkCheck { get; set; }
         [Display(Name = "SubLinkCheck")]
        public int subLinkcheck { get; set; }
        public Nullable<int> linkCreatedBy { get; set; }
        public Nullable<System.DateTime> linkCreatedOn { get; set; }
        public Nullable<int> linkDeletedBy { get; set; }
        public Nullable<System.DateTime> linkDeletedOn { get; set; }
        public Nullable<int> linkUpdatedBy { get; set; }
        public Nullable<System.DateTime> linkUpdatedOn { get; set; }
        //////////////////////////////////////

        public int roleLinkMapID { get; set; }
       
       

        ////////////////////
        public string linkName { get; set; }
   
        public string subLinkName { get; set; }

        public Nullable<int> subLinkAdd { get; set; }
    }
}