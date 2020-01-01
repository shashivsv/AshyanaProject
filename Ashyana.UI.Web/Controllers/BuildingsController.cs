using System;

using System.Linq;

using System.Web.Mvc;
using Ashyana.UI.Web.Models;
using Ashyana.UI.Web.Repository;

namespace Ashyana.UI.Web.Controllers
{
    public class BuildingsController : Controller
    {
        private IBaseRepository<Building> repository;
        private AshyanaDBEntities db = new AshyanaDBEntities();

        public BuildingsController()
        {
            this.repository = new BaseRepository<Building>();
        }

        // GET: Buildings
        public ActionResult Index()
        {
            var lstBuilding = (from i in repository.GetModel() select i).ToList();
            return View(lstBuilding);
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Building building)
        {
            if (ModelState.IsValid)
            {
                repository.InsertModel(building);
                repository.Save();
                return RedirectToAction("Index");
            }

            return View(building);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(int id)
        {
            Building building = repository.GetModelByID(id);
            if (building == null)
                return HttpNotFound();
            return View(building);
        }

        // POST: Buildings/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Building building)
        {
            if (ModelState.IsValid)
            {
                repository.InsertModel(building);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int id)
        {
            Building building = repository.GetModelByID(id);
            if (building == null)
                return HttpNotFound();
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = repository.GetModelByID(id);
            repository.DeleteModel(id);
            repository.Save();
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
