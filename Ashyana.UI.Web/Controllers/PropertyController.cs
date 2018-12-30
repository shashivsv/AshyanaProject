using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using Ashyana.UI.Web.Common;
using System.IO;
using System.Data.Entity;
using System.Configuration;
using Ashyana.UI.Web.Repository;

namespace Ashyana.UI.Web.Controllers
{

    //[MyAuthorizeFilter]
    public class PropertyController : Controller
    {
        //using repository pattern
        private IBaseRepository<Property> repositoryProperty;
        private IBaseRepository<Purchase> repositoryPurchase;
        public PropertyController()
        {
            this.repositoryProperty = new BaseRepository<Property>();
            this.repositoryPurchase = new BaseRepository<Purchase>();
        }

        // GET: Property
        public ActionResult Index()
        {
            var list = (from i in repositoryProperty.GetModel() select i).ToList();
            foreach (var item in list)
            {
                TempData["image"] = ConfigurationManager.AppSettings["image"] + item.propertyImage;
            }
            return View(list);
        }

  
        public ActionResult Search()
        {
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var list = (from i in objEntity.U_sp_SelectProperty(1)

                            select new Property
                            {
                                propertyID = i.PropertyID,
                                propertyTypeID = i.propertyTypeID,
                                propertyDesc = i.propertyDesc,
                                purchaseType = i.purchaseType,
                                propertyTypeName = i.propertyType,
                                propertyPrice = i.propertyPrice,
                                bedroom = i.bedroom,
                                propertyArea = i.propertyArea,
                                countryID = i.countryID,
                                CountryName = i.CountryName,
                                stateID = i.stateID,
                                StateName = i.StateName,
                                cityID = i.cityID,
                                CityName = i.CityName,
                                metro = i.metro,
                                propertyAddress = i.propertyAddress,
                                propertyLocality = i.propertyLocality,
                                pin = i.pin,
                                propertyAge = i.propertyAge,
                                propertyTenure = i.propertyTenure,
                                propertyImage = i.propertyImage

                            }).ToList();
                return View("Search", list);
            }

        }
        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            var list = (from i in repositoryProperty.GetModel() where i.propertyID == id select i).FirstOrDefault();
            return View(list);
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            PurchaseType();

            return View();
        }
        public ActionResult News()
        {
            return View();

        }
        public void PurchaseType()
        {
            var purchaselist = (from i in repositoryPurchase.GetModel()
                                select new SelectListItem
                                {
                                    Text = i.purchaseType,
                                    Value = i.purchaseTypeID.ToString()

                                });
            ViewBag.purchaseDetails = new SelectList(purchaselist, "Value", "Text");

        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(Property property, HttpPostedFileBase file)
        {
            try
            {
                Random rnd = new Random();
                int generatedNo = 0;
                // TODO: Add insert logic here
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    //image upload
                    string myfile = string.Empty;
                    if (file != null)
                    {


                        generatedNo = rnd.Next(100, int.MaxValue);
                        var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                        var filewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                        myfile = filewithoutext + "_" + generatedNo + ext;
                        string path = this.Server.MapPath(ConfigurationManager.AppSettings["image"]);
                        var Targetpath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["imagethumb"]), myfile);
                        string fullpath = Path.Combine(path, myfile);
                        TempData["Image"] = fullpath;
                        Request.Files[0].SaveAs(fullpath);
                    }
                    property.propertyImage = myfile;
                    property.propertyCreatedby = Convert.ToInt32(Session["userID"]);
                    property.propertyCreatedon = Convert.ToDateTime(DateTime.Now.ToString());
                    objEntity.Properties.Add(property);
                    objEntity.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            PurchaseType();
            Property property = repositoryProperty.GetModelByID(id);
            TempData["Image"] = ConfigurationManager.AppSettings["image"] + property.propertyImage;
            return View(property);
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Property property, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                    {
                        string pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(
                                       Server.MapPath(ConfigurationManager.AppSettings["image"]), pic);
                        // file is uploaded
                        file.SaveAs(path);
                        property.propertyImage = pic;
                        objEntity.Entry(property).State = EntityState.Modified;
                        objEntity.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(property);
            }
            catch
            {
                return View();
            }
        }
     
        // GET: Property/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repositoryProperty.DeleteModel(id);
            repositoryProperty.Save();
            return RedirectToAction("Index");

        }

    }
}
