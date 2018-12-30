using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ashyana.UI.Web.ViewModel
{
    public class MenuList
    {
        public int linkID { get; set; }
         [Display(Name = "Link")]
        public string linkName { get; set; }
         [Display(Name = "Path")]
        public string linkPath { get; set; }
         [Display(Name = "Rank")]
        public Nullable<int> linkRank { get; set; }
        public Nullable<int> linkforHome { get; set; }
         [Display(Name = "Desc")]
        public string linkDesc { get; set; }
        public Nullable<int> linkDelete { get; set; }
        public Nullable<int> linkCreatedBy { get; set; }
        public Nullable<System.DateTime> linkCreatedOn { get; set; }
        public Nullable<int> linkDeletedBy { get; set; }
        public Nullable<System.DateTime> linkDeletedOn { get; set; }
        public Nullable<int> linkUpdatedBy { get; set; }
        public Nullable<System.DateTime> linkUpdatedOn { get; set; }

        //sublink
        public int subLinkID { get; set; }
      [Display(Name = "SubLink")]
        public string subLinkName { get; set; }
        public string subLinkPath { get; set; }
        public Nullable<int> subLinkRank { get; set; }
         [Display(Name = "Delete")]
        public Nullable<int> subLinkDelete { get; set; }
         [Display(Name = "View")]
        public Nullable<int> subLinkView { get; set; }
         [Display(Name = "Add")]
        public Nullable<int> subLinkAdd { get; set; }
         [Display(Name = "Update")]
        public Nullable<int> sublinkUpdate { get; set; }
         [Display(Name = "Print")]
        public Nullable<int> sublinkPrint { get; set; }
        public Nullable<int> subLinkCreatedBy { get; set; }
        public Nullable<System.DateTime> subLinkCreatedOn { get; set; }
        public Nullable<int> subLinkDeletedBy { get; set; }
        public Nullable<System.DateTime> subLinkDeletedOn { get; set; }
        public Nullable<int> subLinkUpdatedBy { get; set; }
        public Nullable<System.DateTime> subLinkUpdatedOn { get; set; }

        //maproletolink
        public int roleLinkMapID { get; set; }
        public Nullable<int> roleID { get; set; }
       
     
   
     
        [Display(Name="LnkCheck")]
        public int linkCheck { get; set; }
            [Display(Name = "subCheck")]
        public int subLinkcheck { get; set; }
    
  //mapusertolink
        public int userLinkMapID { get; set; }
      
       
   
    }
}