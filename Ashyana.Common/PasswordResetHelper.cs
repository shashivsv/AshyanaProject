using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ashyana.Common;

namespace Ashyana.Common
{
   public class PasswordResetHelper : MyEmailSender
    {
       private IEmailSender emailSender;

       public void ResetPassword()
       {
           // ...call interface methods to configure e-mail details...
           emailSender.sendEmail();
       }
       public PasswordResetHelper( IEmailSender emailSenderParam)
       {
           emailSender = emailSenderParam;
            
       }
     
    }
}
