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
using Ashyana.UI.Web.Mapper;
using PagedList;
using PagedList.Mvc;
using Ashyana.Common;
using Ashyana.UI.Web.Repository;

using Microsoft.Practices.Unity;
using Ashyana.Business;
using System.Data;
using Ashyana.Data;

namespace Ashyana.UI.Web.Controllers
{
    [HandleError]
    public class UsersController : Controller
    {

        private IBaseRepository<User> repositoryUser;
        private IBaseRepository<Country> repositoryCountry;
        private IBaseRepository<State> repositoryState;
        private IBaseRepository<City> repositoryCity;
        private IBaseRepository<Role> repositoryRole;

        //using DI

        private IGetDetails _Igetdetails;
        public UsersController(IGetDetails objGetDetails)
        {
            _Igetdetails = objGetDetails;
        }
        public UsersController()
        {
            this.repositoryUser = new BaseRepository<User>();
            this.repositoryCountry = new BaseRepository<Country>();
            this.repositoryState = new BaseRepository<State>();
            this.repositoryCity = new BaseRepository<City>();
            this.repositoryRole = new BaseRepository<Role>();
        }


        // GET: Users
        public ActionResult Index()
        {
            //Implementation of Dependency Injection
            UnityContainer UI = new UnityContainer();
            UI.RegisterType<BAL>();
            UI.RegisterType<DAL>();

            UI.RegisterType<IGetDetails,DAL>();
            BAL objBal = UI.Resolve<BAL>();
            objBal.GetDetails();


            //int a = 0; int b = 1;
            //int ck = b / a;
            var uslist = (from u in repositoryUser.GetModel()
                          join c in repositoryCountry.GetModel() on u.countryID equals c.CountryID
                          join s in repositoryState.GetModel() on u.StateID equals s.StateID
                          join cty in repositoryCity.GetModel() on u.cityID equals cty.CityID
                          join r in repositoryRole.GetModel() on u.RoleID equals r.roleID
                          select new
                                     {
                                         u.userName,
                                         u.userFirstname,
                                         u.userLastname,
                                         u.userPassword,
                                         u.userEmailID,
                                         u.userContactno,
                                         u.userID,
                                         u.RoleID,
                                         r.roleName,
                                         c.CountryName,
                                         s.StateName,
                                         cty.CityName
                                     });

            MyUserModel mus = new MyUserModel();

            List<MyUserModel> flist = new List<MyUserModel>();
            foreach (var item in uslist)
            {
                MyUserModel mymodel = new MyUserModel();
                mymodel.userFirstname = item.userFirstname;
                mymodel.userLastname = item.userLastname;
                mymodel.userEmailID = item.userEmailID;
                mymodel.userContactno = item.userContactno;
                mymodel.userName = item.userName;
                mymodel.RoleID = item.RoleID;
                mymodel.Roles = item.roleName;
                mymodel.userPassword = item.userPassword;
                mymodel.userID = item.userID;
                mymodel.StateName = item.StateName;
                mymodel.CityName = item.CityName;
                mymodel.CountryName = item.CountryName;
                flist.Add(mymodel);
            }
            return View(flist);

            #region using storedprocedure


            //date fetched using stored procedure
            //AshyanaDBEntities objEntity = new AshyanaDBEntities();
            //var userlist = (from user in objEntity.U_sp_SelectAllUser()
            //                select new MyUserModel
            //                {
            //                    userFirstname = user.userFirstname,
            //                    userLastname = user.userLastname,
            //                    userEmailID = user.userEmailID,
            //                    userContactno = user.userContactno,
            //                    userName = user.userName,
            //                    Roles = user.roleName,
            //                    CountryName = user.CountryName,
            //                    StateName = user.StateName,
            //                    CityName = user.CityName,
            //                    userPassword = user.userPassword,
            //                    //ids
            //                    userID = user.userID,
            //                    countryID = user.countryID,
            //                    StateID = user.stateID,
            //                    cityID = user.cityid,
            //                    //}).ToList().ToPagedList(page ?? 1, 15);
            //                }).ToList();
            ////var userlist = (from user in objEntity.Users
            ////                join country in objEntity.Countries on user.countryID equals country.CountryID
            ////                join state in objEntity.States on user.StateID equals state.StateID
            ////                join city in objEntity.Cities on user.cityID equals city.CityID
            ////                join role in objEntity.Roles on user.RoleID equals role.roleID

            ////                select new MyUserModel
            ////                {
            ////                    userFirstname = user.userLastname,
            ////                    userLastname = user.userLastname,
            ////                    userEmailID = user.userEmailID,
            ////                    userContactno = user.userContactno,
            ////                    userName = user.userName,
            ////                    Roles = role.roleName,
            ////                    CountryName = country.CountryName,
            ////                    StateName = state.StateName,
            ////                    CityName = city.CityName,
            ////                    userPassword = user.userPassword,
            ////                    //ids
            ////                    userID = user.userID,
            ////                    countryID = country.CountryID,
            ////                    StateID = state.StateID,
            ////                    cityID = city.CityID
            ////                }).ToList().ToPagedList(page ?? 1,4);
            #endregion using storedprocedure


            //return View(userlist);
        }


        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            var userdetails = repositoryUser.GetModelByID(id);
            return View(userdetails);
        }


        // POST: Users/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.userPassword = Encode.Encrypt(user.userPassword);
                repositoryUser.InsertModel(user);
                repositoryUser.Save();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            MyUserModel msu = new MyUserModel();

            User user = (from i in repositoryUser.GetModel() where i.userID == id select i).FirstOrDefault();
            msu.userID = user.userID;
            msu.RoleID = user.RoleID;
            msu.userFirstname = user.userFirstname;
            msu.userLastname = user.userLastname;
            msu.userName = user.userName;
            msu.userEmailID = user.userEmailID;
            msu.cityID = user.cityID;
            msu.countryID = user.countryID;
            msu.StateID = user.StateID;
            msu.userContactno = user.userContactno;
            msu.userPassword = Encode.Decrypt(user.userPassword);
            return View(msu);

        }
        // POST: Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var list = repositoryUser.GetModelByID(id);
                list.userName = user.userName;
                list.userFirstname = user.userFirstname;
                list.userLastname = user.userLastname;
                list.userEmailID = user.userEmailID;
                list.userContactno = user.userContactno;
                list.RoleID = user.RoleID;
                repositoryUser.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }



        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            User user = repositoryUser.GetModelByID(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = repositoryUser.GetModelByID(id);
            repositoryUser.DeleteModel(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Index(string prefix)
        {
            if (!string.IsNullOrEmpty(prefix))
            {

                var Users = (from N in repositoryUser.GetModel()
                             where N.userFirstname.StartsWith(prefix)
                             select new { N.userFirstname, N.userID });

                return Json(Users, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(JsonRequestBehavior.AllowGet);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        repositoryUser.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
