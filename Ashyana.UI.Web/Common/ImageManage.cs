using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;


namespace Ashyana.UI.Web.Common
{
    public static class ImageManage
    {
        //public static string GetSaveImage(HttpPostedFileBase file)
        //{
        //    Random rnd = new Random();
        //    int generatedNo = 0;
        //    string myfile = string.Empty;
        //    if (file != null)
        //    {
        //        generatedNo = rnd.Next(100, int.MaxValue);
        //        var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
        //        var filewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
        //        myfile = filewithoutext + "_" + generatedNo + ext;
        //        //string path = this.Server.MapPath(ConfigurationManager.AppSettings["image"]);
        //        //var Targetpath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["imagethumb"]), myfile);
        //        //string fullpath = Path.Combine(path, myfile);
        //        return myfile;
        //    }
        //}
    }
}