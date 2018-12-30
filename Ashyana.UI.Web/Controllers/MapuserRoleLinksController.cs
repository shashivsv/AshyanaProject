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
using System.Text;

namespace Ashyana.UI.Web.Controllers
{
    public class MapuserRoleLinksController : Controller
    {
        private AshyanaDBEntities db = new AshyanaDBEntities();

        // GET: MapuserRoleLinks


        public ActionResult Index()
        {

            var userlist = (from i in db.U_sp_SelectMapRoleUser() select i).ToList();
            List<MapLinks> lstrolelink = new List<MapLinks>();
            foreach (var item in userlist)
            {
                MapLinks lnk = new MapLinks();
                lnk.linkName = item.linkName;
                lnk.subLinkName = item.sublinkName;
                lnk.userName = item.userName;
                lnk.roleName = item.roleName;
                lnk.sublinkAdd = item.sublinkAdd;
                lnk.subLinkcheck = item.linkCheck;
                lnk.sublinkDelete = item.sublinkDelete;
                lnk.sublinkPrint = item.sublinkPrint;
                lnk.sublinkUpdate = item.sublinkUpdate;
                lnk.userLinkMapID = item.userLinkMapID;
                lstrolelink.Add(lnk);
            }

            return View(lstrolelink);
        }

        // GET: MapuserRoleLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapuserRoleLink mapuserRoleLink = db.MapuserRoleLinks.Find(id);
            if (mapuserRoleLink == null)
            {
                return HttpNotFound();
            }
            return View(mapuserRoleLink);
        }

        // GET: MapuserRoleLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapuserRoleLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MapuserRoleLink mapuserRoleLink)
        {
            if (ModelState.IsValid)
            {
                db.MapuserRoleLinks.Add(mapuserRoleLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mapuserRoleLink);
        }

        // GET: MapuserRoleLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapuserRoleLink mapuserRoleLink = db.MapuserRoleLinks.Find(id);
            if (mapuserRoleLink == null)
            {
                return HttpNotFound();
            }
            return View(mapuserRoleLink);
        }

        // POST: MapuserRoleLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MapuserRoleLink mapuserRoleLink)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(mapuserRoleLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mapuserRoleLink);
        }

        // GET: MapuserRoleLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MapuserRoleLink mapuserRoleLink = db.MapuserRoleLinks.Find(id);
            if (mapuserRoleLink == null)
            {
                return HttpNotFound();
            }
            return View(mapuserRoleLink);
        }

        // POST: MapuserRoleLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MapuserRoleLink mapuserRoleLink = db.MapuserRoleLinks.Find(id);
            db.MapuserRoleLinks.Remove(mapuserRoleLink);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult updateAll(string[] selectedViews, string[] selectedAdd, string[] selectedUpdate,
            string[] selectedDelete, string[] selectedPrint, string[] selectedLinkCheck, string[] selectedSubLinkCheck)
        {
            //update Views permission
            StringBuilder sbView = new StringBuilder();
            if (selectedViews != null)
                for (int i = 0; i < selectedViews.Length; i++)
                {

                    sbView.Append(selectedViews[i].Replace("View_", "") + ",");
                }
            string result = sbView.ToString().TrimEnd(',');
            AshyanaDBEntities objEntity = new AshyanaDBEntities();

            //var listupd = objEntity.MapuserRoleLinks
            //             .Where(x => result.Contains(x.userLinkMapID.ToString())).ToList();
            //listupd.ForEach(a => a.sublinkView = 1);

           // var listupd= from i in objEntity.U_sp_UpdateUserRoleLinkMap()
            //                      where

         //   db.Entry(listupd).State = EntityState.Modified;

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
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
