using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ashyana.UI.Web.ViewModel
{
    public class MyCityModel
    {
        public int CityID { get; set; }

        [Display(Name="City")]
        public string CityName { get; set; }
        [Display(Name="Desc")]
        public string CityDesc { get; set; }
        public Nullable<int> CityForHome { get; set; }
         [Display(Name = "Imag")]
        public string CityImage { get; set; }
         [Display(Name = "Delete")]
        public Nullable<int> CityDelete { get; set; }
        public Nullable<int> CityCreatedby { get; set; }
        public Nullable<System.DateTime> CityCreatedon { get; set; }
        public Nullable<int> CityDeletedby { get; set; }
        public Nullable<System.DateTime> CityDeletedon { get; set; }
        public Nullable<int> CityUpdatedby { get; set; }
        public Nullable<System.DateTime> CityUpdatedon { get; set; }
         [Display(Name = "Country")]
        public Nullable<int> countryID { get; set; }
         [Display(Name = "State")]
        public Nullable<int> StateID { get; set; }

        //added columns
         [Display(Name = "Country")]
        public string CountryName { get; set; }
         [Display(Name = "State")]
        public string StateName { get; set; }
    }
}