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
    public class FloorsController : Controller
    {
        private AshyanaDBEntities db = new AshyanaDBEntities();

        // GET: Floors

        public ActionResult  Index()
        {
            return View();
        }
        public JsonResult GetFloors()
        {
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var result = (from i in objEntity.Floors select i).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //return View(db.Floors.ToList());
            //return View(list);
        }

        // GET: Floors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return HttpNotFound();
            }
            return View(floor);
        }

        // GET: Floors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Floors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "floorTypeID,floorType,floorDesc,floorImage,floorDelete,floorDeletedon,floorCreatedby,floorCreatedon,floorDeletedby,floorUpdatedby,floorUpdatedon")] Floor floor)
        {
            if (ModelState.IsValid)
            {
                db.Floors.Add(floor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(floor);
        }

        // GET: Floors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return HttpNotFound();
            }
            return View(floor);
        }

        // POST: Floors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "floorTypeID,floorType,floorDesc,floorImage,floorDelete,floorDeletedon,floorCreatedby,floorCreatedon,floorDeletedby,floorUpdatedby,floorUpdatedon")] Floor floor)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(floor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(floor);
        }

        // GET: Floors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Floor floor = db.Floors.Find(id);
            if (floor == null)
            {
                return HttpNotFound();
            }
            return View(floor);
        }

        // POST: Floors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Floor floor = db.Floors.Find(id);
            db.Floors.Remove(floor);
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
