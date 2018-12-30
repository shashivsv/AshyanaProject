using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Ashyana.UI.Web.ViewModel
{
    public class MyStateModel
    {
        [Display(Name = "ID")]
        public int StateID { get; set; }
        [Display(Name="State")]
        public string StateName { get; set; }
        [Display(Name="Desc")]
        public string StateDesc { get; set; }
        [Display(Name="Country")]
        public Nullable<int> countryID { get; set; }
        public string CountryName { get; set; }

    }
}