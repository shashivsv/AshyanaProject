using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using System.IO;
using System.Configuration;

namespace Ashyana.UI.Web.Controllers
{
    public class CountriesController : Controller
    {
        private AshyanaDBEntities db = new AshyanaDBEntities();

        // GET: Countries
        public ActionResult Index()
        {
          //  return View(db.Countries.ToList());
            return RedirectToAction("List");
        }

        public ActionResult List()
        {

            return View(db.Countries.ToList());
        }
        //public ActionResult Details()
        //{
        //    return View();
        //}



        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            Country country;
            if (id == null)
            {
                var countryList = (from i in db.Countries
                                   select i).ToList();

                return View(countryList);
            }
            else
            {
                country = db.Countries.Find(id);
                return View(country);
            }

            //if (country == null)
            //{
            //    return HttpNotFound();
            //}

        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country, HttpPostedFileBase file)
        {
            Random rnd = new Random();
            int generatedNo = 0;
            if (ModelState.IsValid)
            {
                generatedNo = rnd.Next(100, int.MaxValue);
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                var filewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                string myfile = filewithoutext + "_" + generatedNo + ext;
                string path = this.Server.MapPath(ConfigurationManager.AppSettings["image"]);
                var Targetpath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["imagethumb"]), myfile);
                string fullpath = Path.Combine(path, myfile);
                TempData["Image"] = fullpath;
                Request.Files[0].SaveAs(fullpath);
                db.Countries.Add(country);
                country.CountryImage = myfile;
                country.CountryCreatedby = Convert.ToInt32(Session["UserID"]);
                country.CountryCreatedon = Convert.ToDateTime(DateTime.Now.ToString());

                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            Country country = db.Countries.Find(id);
            TempData["Image"] = ConfigurationManager.AppSettings["image"] + country.CountryImage;
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country, HttpPostedFileBase file)
        {
                if (ModelState.IsValid)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                               Server.MapPath(ConfigurationManager.AppSettings["image"]), pic);
                // file is uploaded
                file.SaveAs(path);
                country.CountryImage = pic;
                //db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();   
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
