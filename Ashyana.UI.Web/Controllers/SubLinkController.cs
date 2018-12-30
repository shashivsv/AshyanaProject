using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using System.Data.Entity;

namespace Ashyana.UI.Web.Controllers
{
    public class SubLinkController : Controller
    {
        // GET: SubLink
        public ActionResult Index()
        {

            return RedirectToAction("List");
        }

        //show list of links

        public ActionResult List()
        {
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var list = objEntity.SubLinks.ToList();
                return View(list);
            }


        }

        // GET: SubLink/Details/5
        public ActionResult Details(int? id)
        {

            SubLink sb = new SubLink();
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var lstSublink = (from i in objEntity.SubLinks.ToList()
                                  where i.subLinkID == id
                                  select i).ToList();

                foreach (var item in lstSublink)
                {

                    sb.subLinkID = item.subLinkID;
                    sb.subLinkName = item.subLinkName;
                    sb.subLinkPath = item.subLinkPath;
                    sb.subLinkRank = item.subLinkRank;
                    sb.subLinkView = item.subLinkView;

                }
            }

            return View(sb);
        }

        // GET: SubLink/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: SubLink/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SubLink/Edit/5
        public ActionResult Edit(int? id)
        {
            SubLink sb = new SubLink();
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                var lstSublink = (from i in objEntity.SubLinks.ToList()
                                  where i.subLinkID == id
                                  select i).FirstOrDefault();

                sb.subLinkID = lstSublink.subLinkID;
                sb.subLinkName = lstSublink.subLinkName;
                sb.subLinkPath = lstSublink.subLinkPath;
                sb.subLinkRank = lstSublink.subLinkRank;
                sb.subLinkView = lstSublink.subLinkView;


            }
            return View(sb);
        }

        // POST: SubLink/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SubLink sb)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                    {
                        SubLink getdetailsById = (from i in objEntity.SubLinks
                                                  where i.subLinkID == id
                                                  select i).FirstOrDefault();
                        getdetailsById.subLinkID = id;
                        getdetailsById.subLinkName = sb.subLinkName;
                        getdetailsById.subLinkPath = sb.subLinkPath;
                        getdetailsById.subLinkRank = sb.subLinkRank;

                        objEntity.SubLinks.Add(getdetailsById);
                        objEntity.Entry(getdetailsById).State = EntityState.Modified;
                        ModelState.Remove(sb.subLinkPath);
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

        // GET: SubLink/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubLink/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
