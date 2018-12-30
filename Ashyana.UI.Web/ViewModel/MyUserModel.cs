using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace Ashyana.UI.Web.ViewModel
{
    public class MyUserModel
    {
        public int userID { get; set; }
        //[Required]
        [Display(Name = "Firstname")]
        public string userFirstname { get; set; }
        //[Required]
        [Display(Name = "LastName")]
        public string userLastname { get; set; }
        //[Required]
        [Display(Name = "Email")]
        public string userEmailID { get; set; }
        //[Required]
        [Display(Name = "Contact")]
       
        public string userContactno { get; set; }
        [Required]
        public string userName { get; set; }
        [Display(Name = "Role")]
        public Nullable<int> RoleID { get; set; }
        [Display(Name = "Country")]
        public Nullable<int> countryID { get; set; }
        [Display(Name = "State")]
        public Nullable<int> StateID { get; set; }
        [Display(Name = "City")]
        public Nullable<int> cityID { get; set; }

        public int userDelete { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string userPassword { get; set; }
        [Display(Name = "CreatedBy")]
        public int userCreatedBy { get; set; }
        [Display(Name = "Created on")]
        public System.DateTime userCreatedOn { get; set; }
        public Nullable<int> userDeletedBy { get; set; }
        public Nullable<System.DateTime> userDeletedOn { get; set; }
        public Nullable<int> userUpdatedBy { get; set; }
        public Nullable<System.DateTime> userUpdatedOn { get; set; }
        //add columns
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Roles { get; set; }

        //for paging
        public IPagedList<MyUserModel> SearchResults { get; set; }
        public string SearchButton { get; set; }
        public bool RememberMe { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}