using Ashyana.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ashyana.UI.Web.Controllers
{
    public class LinkController : Controller
    {
        // GET: Link
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var list =( from i in objEntity.Links select i ).ToList();
                return View(list);
            }

        }
        // GET: Link/Details/5
        public ActionResult Details(int? id)
        {
            Link newlink = new Link();
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                Link lnk = (from i in objEntity.Links where i.linkID == id select i).FirstOrDefault();
              
                newlink.linkName = lnk.linkName;
                newlink.linkPath = lnk.linkPath;
                newlink.linkDesc = lnk.linkDesc;
            }
            return View(newlink);
        }

        // GET: Link/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Link/Create
        [HttpPost]
        public ActionResult Create(Link lnk)
        {
            try
            {
             using(AshyanaDBEntities objEntity=new AshyanaDBEntities())
             {
                 
                 objEntity.Entry(lnk).State = EntityState.Added;
                 objEntity.SaveChanges();

             }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Link/Edit/5
        public ActionResult Edit(int id)
        {
            Link lnk = new Link();
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var lst = (from i in objEntity.Links
                           where i.linkID == id
                           select i).FirstOrDefault();

                lnk.linkName = lst.linkName;
                lnk.linkPath = lst.linkPath;
                lnk.linkDesc = lst.linkDesc;

            }

            return View(lnk);
        }

        // POST: Link/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Link lnk)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                    {
                        var lnkst = (from i in objEntity.Links
                                     where i.linkID == id
                                     select i
                                       ).FirstOrDefault();

                        lnkst.linkName = lnk.linkName;
                        lnkst.linkPath = lnk.linkPath;
                        objEntity.Entry(lnkst).State = EntityState.Modified;
                        objEntity.SaveChanges();

                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Link/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {

                    var getrow = (from i in objEntity.Links where i.linkID == id select i).FirstOrDefault();
                    objEntity.Entry(getrow).State = EntityState.Deleted;
                    objEntity.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Link/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                using (AshyanaDBEntities objEntity=new AshyanaDBEntities())
                {
                
                    var getrow = (from i in objEntity.Links where i.linkID == id select i).FirstOrDefault();
                    objEntity.Entry(getrow).State = EntityState.Deleted;
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
