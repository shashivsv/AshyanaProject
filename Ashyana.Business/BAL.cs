using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ashyana.Common;

namespace Ashyana.Business
{
   public  class BAL
    {
       private IGetDetails _IGetDetails;
       public BAL(IGetDetails objGetdetails)
       {
           _IGetDetails = objGetdetails;
       }

       public void GetDetails()
       {

       }

    }
}
