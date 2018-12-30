using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashyana.Repository
{
  
    public class Users
    {
        public int userID { get; set; }
    
        public string userFirstname { get; set; }
    
        public string userLastname { get; set; }
        public string userEmailID { get; set; }
       
        public string userContactno { get; set; }
      
        public string userName { get; set; }
    
        public Nullable<int> RoleID { get; set; }
       
        public Nullable<int> countryID { get; set; }
       
        public Nullable<int> StateID { get; set; }
      
        public Nullable<int> cityID { get; set; }

        public int userDelete { get; set; }
        //[Required]
     
        public string userPassword { get; set; }
      
        public int userCreatedBy { get; set; }
     
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
     
        public string SearchButton { get; set; }
        public bool RememberMe { get; set; }
    }
}
   
