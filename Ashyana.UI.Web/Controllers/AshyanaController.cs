using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;

namespace Ashyana.UI.Web.Controllers
{
    public class AshyanaController : Controller
    {
        private AshyanaDBEntities db = new AshyanaDBEntities();

        // GET: Ashyana
        public ActionResult Index()
        {
          //  GetPropertyTypeList();
            //return View(db.Properties.ToList());
            return View();
        }

        // GET: Ashyana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Ashyana/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ashyana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "propertyID,propertyTypeID,propertyDesc,purchaseType,propertyPrice,bedroom,propertyArea,countryID,stateID,cityID,metro,propertyAddress,propertyLocality,pin,propertyAge,propertyTenure,propertyImage,propertyDelete,propertyDeletedon,propertyCreatedby,propertyCreatedon,propertyDeletedby,propertyUpdatedby,propertyUpdatedon")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(property);
        }

        // GET: Ashyana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Ashyana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "propertyID,propertyTypeID,propertyDesc,purchaseType,propertyPrice,bedroom,propertyArea,countryID,stateID,cityID,metro,propertyAddress,propertyLocality,pin,propertyAge,propertyTenure,propertyImage,propertyDelete,propertyDeletedon,propertyCreatedby,propertyCreatedon,propertyDeletedby,propertyUpdatedby,propertyUpdatedon")] Property property)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Ashyana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Ashyana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetPropertyTypeList()
        {
            using (AshyanaDBEntities db = new AshyanaDBEntities())
            {
                var propertTypeDetails = (from items in db.PropertyTypes.ToList()
                                          select new SelectListItem
                                          {
                                              Text = items.propertyType1,
                                              Value = items.propertyTypeID.ToString()
                                          }).ToList();
                ViewBag.propertTypeDetails = new SelectList(propertTypeDetails, "Text", "Value");

            }
            return View("_AshyanaSearchResult");
        }
        public ActionResult GetBuildingTypeList()
        {
            using (AshyanaDBEntities db = new AshyanaDBEntities())
            {
                var buildingTypeDetails = (from items in db.Buildings.ToList()
                                           select new SelectListItem
                                           {
                                               Text = items.buildingType,
                                               Value = items.buildingTypeID.ToString()
                                           }).ToList();
                ViewBag.buildingTypeDetails = new SelectList(buildingTypeDetails, "Text", "Value");

            }
            return View("_AshyanaSearchResult");
        }
        public ActionResult GetFloorTypeList()
        {
            using (AshyanaDBEntities db = new AshyanaDBEntities())
            {
                var floorTypeDetails = (from items in db.Floors.ToList()
                                        select new SelectListItem
                                        {
                                            Text = items.floorType,
                                            Value = items.floorTypeID.ToString()
                                        }).ToList();
                ViewBag.floorTypeDetails = new SelectList(floorTypeDetails, "Text", "Value");

            }
            return View("_AshyanaSearchResult");
        }
        public ActionResult GetPropertyList()
        {
            using (AshyanaDBEntities db = new AshyanaDBEntities())
            {
                IEnumerable<Property> propertyDetails = db.Properties.ToList();
                ViewBag.propertyDetails = propertyDetails;
            }
            GetCountryList();
            GetPropertyTypeList();
            GetBuildingTypeList();
            GetFloorTypeList();
            return View();
        }

        //Get country
        public ActionResult GetCountryList()
        {
            
            return View("_AshyanaSearchResult");
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
