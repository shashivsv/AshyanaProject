using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using Ashyana.UI.Web.ViewModel;

namespace Ashyana.UI.Web.Controllers
{
    public class MapRoleLinksController : Controller
    {
        private AshyanaDBEntities db = new AshyanaDBEntities();

        // GET: MapRoleLinks
        public ActionResult Index()
        {
            var listrole = (from i in db.U_sp_SelectRoleLinkMap()
                            select i).ToList();

            List<MapLinks> lnk = new List<MapLinks>();

            foreach (var item in listrole)
            {
                MapLinks ml = new MapLinks();
                ml.linkName = item.linkName;
                ml.roleLinkMapID = item.roleLinkMapID;
                ml.roleName = item.roleName;
                ml.linkCheck = item.linkCheck;
                ml.subLinkAdd = item.subLinkAdd;
                ml.subLinkcheck = item.subLinkcheck;
                ml.sublinkDelete = item.sublinkDelete;
                ml.sublinkPrint = item.sublinkPrint;
                ml.sublinkUpdate = item.sublinkUpdate;
                ml.sublinkView = item.sublinkView;
                ml.subLinkName = item.subLinkName;


                lnk.Add(ml);




            }
            return View(lnk);
            //return View(db.MapRoleLinks.ToList());
        }

      
        public ActionResult Update(MapLinks fc, int? id)
        {

            var item = (from i in db.MapRoleLinks where i.roleLinkMapID == id select i).FirstOrDefault();

            MapRoleLink mlnk = new MapRoleLink();

            mlnk.linkCheck = item.linkCheck;
            mlnk.sublinkAdd = item.sublinkAdd;
            mlnk.sublinkPrint = item.sublinkPrint;
            mlnk.sublinkUpdate = item.sublinkUpdate;

            db.MapRoleLinks.Add(mlnk);
            //db.Entry(mlnk).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index");


        }
        // GET: MapRoleLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapRoleLink mapRoleLink = db.MapRoleLinks.Find(id);
            if (mapRoleLink == null)
            {
                return HttpNotFound();
            }
            return View(mapRoleLink);
        }

        // GET: MapRoleLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapRoleLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roleLinkMapID,roleID,linkID,subLinkID,sublinkView,sublinkAdd,sublinkUpdate,sublinkDelete,sublinkPrint,linkCheck,subLinkcheck,linkCreatedBy,linkCreatedOn,linkDeletedBy,linkDeletedOn,linkUpdatedBy,linkUpdatedOn")] MapRoleLink mapRoleLink)
        {
            if (ModelState.IsValid)
            {
                db.MapRoleLinks.Add(mapRoleLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mapRoleLink);
        }

        // GET: MapRoleLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapRoleLink mapRoleLink = db.MapRoleLinks.Find(id);
            if (mapRoleLink == null)
            {
                return HttpNotFound();
            }
            return View(mapRoleLink);
        }

        // POST: MapRoleLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roleLinkMapID,roleID,linkID,subLinkID,sublinkView,sublinkAdd,sublinkUpdate,sublinkDelete,sublinkPrint,linkCheck,subLinkcheck,linkCreatedBy,linkCreatedOn,linkDeletedBy,linkDeletedOn,linkUpdatedBy,linkUpdatedOn")] MapRoleLink mapRoleLink)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(mapRoleLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mapRoleLink);
        }

        // GET: MapRoleLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapRoleLink mapRoleLink = db.MapRoleLinks.Find(id);
            if (mapRoleLink == null)
            {
                return HttpNotFound();
            }
            return View(mapRoleLink);
        }

        // POST: MapRoleLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MapRoleLink mapRoleLink = db.MapRoleLinks.Find(id);
            db.MapRoleLinks.Remove(mapRoleLink);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateAll()
        {
            return View();
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
