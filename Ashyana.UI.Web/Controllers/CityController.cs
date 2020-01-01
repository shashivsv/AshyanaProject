using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using Ashyana.UI.Web.ViewModel;
using System.IO;
using System.Configuration;

namespace Ashyana.UI.Web.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {

            AshyanaDBEntities objEntity = new AshyanaDBEntities();

            // var citylist = objEntity.Cities.ToList();
            var citylist = (from i in objEntity.Cities
                            join s in objEntity.States on i.StateID equals s.StateID
                            join c in objEntity.Countries on i.countryID equals c.CountryID
                            select new MyCityModel
                            {
                                CityID = i.CityID,
                                CityName = i.CityName,
                                CityDesc = i.CityDesc,
                                CityImage = i.CityImage,
                                StateName = s.StateName,
                                CountryName = c.CountryName

                            });

            return View(citylist);

        }

        // GET: City/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: City/Create
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(MyCityModel city, HttpPostedFileBase file)
        {
            try
            {
                City newcity = new City();
                // TODO: Add insert logic here
                AshyanaController objCtrl = new AshyanaController();
                objCtrl.GetCountryList();
                var rnd = new Random();

                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    int generatedNo = rnd.Next(100, int.MaxValue);
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    var filewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                    string myfile = filewithoutext + "_" + generatedNo + ext;
                    string path = this.Server.MapPath(ConfigurationManager.AppSettings["image"]);
                    var Targetpath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["imageThumb"]), myfile);
                    string fullpath = Path.Combine(path, myfile);
                    TempData["Image"] = fullpath;
                    Request.Files[0].SaveAs(fullpath);
                    newcity.CityImage = myfile;

                    newcity.CityName = city.CityName;
                    newcity.CityDesc = city.CityDesc;
                    newcity.StateID = Convert.ToInt32(city.StateName);
                    newcity.countryID = Convert.ToInt32(city.CountryName);

                    newcity.CityCreatedby = Convert.ToInt32(Session["userid"]);
                    newcity.CityCreatedon = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    objEntity.Cities.Add(newcity);
                    objEntity.SaveChanges();
                }



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: City/Edit/5
        public ActionResult Edit(int id)
        {

            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var city = (from i in objEntity.Cities
                            where i.CityID == id
                            select i).ToList();
                City ct = new City();
                foreach (var item in city)
                {
                    ct.CityName = item.CityName;
                    ct.CityDesc = item.CityDesc;
                    TempData["cityimage"] = ConfigurationManager.AppSettings["image"] + item.CityImage;
                    ct.CityUpdatedby = Convert.ToInt32(Session["userid"]);
                }
                return View(ct);
            }

        }

        // POST: City/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, City cty, HttpPostedFileBase file)
        {
            try
            {
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    City getCity = (from i in objEntity.Cities
                                    where i.CityID == id
                                    select i).FirstOrDefault();
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                   Server.MapPath(ConfigurationManager.AppSettings["image"]), pic);

                    file.SaveAs(path);
                    cty.CityImage = pic;
                    getCity.CityName = cty.CityName;
                    getCity.CityDesc = cty.CityDesc;
                    getCity.countryID = cty.countryID;
                    getCity.StateID = cty.StateID;
                    getCity.CityImage = cty.CityImage;
                    objEntity.SaveChanges();
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: City/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: City/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, City cty)
        {
            try
            {
                // TODO: Add delete logic here
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    City c = (from i in objEntity.Cities where i.CityID == id select i).FirstOrDefault();
                    objEntity.Cities.Remove(c);
                    objEntity.SaveChanges();
                }



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
