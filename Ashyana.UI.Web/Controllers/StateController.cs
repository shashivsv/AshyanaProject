using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ashyana.UI.Web.Models;
using Ashyana.UI.Web.ViewModel;
using System.Data.Entity;
using System.Configuration;
using Ashyana.UI.Web.Common;

namespace Ashyana.UI.Web.Controllers
{
        [MyAuthorizeFilter]
    public class StateController : Controller
    {
        // GET: State
            [Authorize]
        public ActionResult Index()
        {

            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                // var statelst = objEntity.States.ToList();
                var statelsttt = (from st in objEntity.States
                                  join ct in objEntity.Countries
                                  on st.countryID equals ct.CountryID
                                  select new MyStateModel
                                  {
                                      StateID = st.StateID,
                                      StateName = st.StateName,
                                      StateDesc = st.StateDesc,
                                      CountryName = ct.CountryName
                                  }
                                             ).ToList();
                MyStateModel ssss = new MyStateModel();

                List<MyStateModel> lst = new List<MyStateModel>();
                foreach (var item in statelsttt)
                {
                    MyStateModel abd = new MyStateModel();
                    abd.StateID = item.StateID;
                    abd.CountryName = item.CountryName;
                    abd.StateName = item.StateName;
                    abd.StateDesc = item.StateDesc;
                    lst.Add(abd);
                }
                return View(lst);
            }
        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: State/Create
        public ActionResult Create()
        {
         
            return View();
        }
       
        // POST: State/Create
        [HttpPost]
        public ActionResult Create(State state, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here

                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                   Server.MapPath(ConfigurationManager.AppSettings["image"]), pic);

                    file.SaveAs(path);
                    State st = new State();
                    st.StateName = state.StateName;
                    st.countryID = state.countryID;
                    st.StateImage = pic;
                    st.StateDesc = state.StateDesc;
                    st.StateCreatedby = Convert.ToInt32(Session["userid"]);
                    st.StateCreatedon = DateTime.Now;
                    objEntity.Entry(st).State = EntityState.Added;
                    objEntity.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: State/Edit/5
        public ActionResult Edit(int id)
        {
           
            using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
            {
                State st = (from item in objEntity.States
                            where item.StateID == id
                            select item).FirstOrDefault();

                State state = new State();
                state.StateID = st.StateID;
                state.StateName = st.StateName;
                state.StateDesc = st.StateDesc;
                state.StateImage = st.StateImage;
                state.countryID = st.countryID;
                TempData["stateImage"] = ConfigurationManager.AppSettings["image"] + st.StateImage;



                var ct = (from c in objEntity.Countries
                          select new SelectListItem
                          {
                              Text = c.CountryName,
                              Value = c.CountryID.ToString()

                          }).ToList();
                ViewBag.countryDetails = new SelectList(ct, "Value", "Text");

                return View(state);

            }

        }

        // POST: State/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, State st, HttpPostedFileBase file)
        {
            try
            {
           
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                    {
                        State getState = (from i in objEntity.States
                                          where i.StateID == id
                                          select i).FirstOrDefault();

                        //var originalFilename = System.IO.Path.GetFileName(file.FileName);
                        //string fileId = Guid.NewGuid().ToString().Replace("-", "");
                       
                        string pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(
                                       Server.MapPath(ConfigurationManager.AppSettings["image"]), pic);

                        file.SaveAs(path);
                        st.StateImage = pic;
                        getState.StateName = st.StateName;
                        getState.StateDesc = st.StateDesc;
                        getState.countryID = st.countryID;
                        getState.StateID = st.StateID;
                        getState.StateImage = st.StateImage;
                        //objEntity.Entry(getState).State = EntityState.Modified;
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

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: State/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, State state)
        {
            try
            {
                using (AshyanaDBEntities objEntity = new AshyanaDBEntities())
                {
                    State st = (from i in objEntity.States where i.StateID == id select i).FirstOrDefault();
                    objEntity.States.Remove(st);
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
