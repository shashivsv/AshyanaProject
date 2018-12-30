using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashyana.Common
{
   public static class Encode
    {
        public static string Encrypt(object str)
        {
            byte[] stringArray = UTF8Encoding.UTF8.GetBytes(Convert.ToString(str));
            return Convert.ToBase64String(stringArray, 0, stringArray.Length);
        }

        public static string Decrypt(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                byte[] stringArray = Convert.FromBase64String(str);
                return UTF8Encoding.UTF8.GetString(stringArray, 0, stringArray.Length);

            }
        }
    }
}
