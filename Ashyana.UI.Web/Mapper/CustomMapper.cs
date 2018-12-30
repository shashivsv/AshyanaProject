using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ashyana.UI.Web.ViewModel;
using Ashyana.UI.Web.Models;

namespace Ashyana.UI.Web.Mapper
{
    public class pageController
    {
        public class MyWrapper
        {
            public User us { get; set; }
            public Property property { get; set; }
            public MyWrapper(User u, Property prop)
            {
                this.us = u;
                this.property = prop;

            }
        }
    }
    public class CustomMapper
    {
        MyUserModel myuser = new MyUserModel();
       
         
        public CustomMapper()
        {

        }
        public class UserMapper
        {
            User u = new User();
            MyUserModel myuser = new MyUserModel();
            

            public int userID { get; set; }
        }   
    }

}