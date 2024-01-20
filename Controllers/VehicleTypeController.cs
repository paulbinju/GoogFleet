using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GoogFleet.Models;

namespace GoogFleet.Controllers
{
    public class VehicleTypeController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: VehicleType
        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();
            return View(db.VehicleType_T.OrderBy(x=>x.VehicleType).ToList());
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleTypeID,FleetCompanyID,VehicleType")] VehicleType_T vehicleType_T)
        {
            if (ModelState.IsValid)
            {
                db.VehicleType_T.Add(vehicleType_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleType_T);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleTypeID,FleetCompanyID,VehicleType")] VehicleType_T vehicleType_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.Entry(vehicleType_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleType_T);
        }

        public void domainfinder()
        {
            string Domain = Request.Url.ToString();
            ViewBag.Domain = Domain;

            if (Domain.Contains("app.googfleet.com"))
            {
                ViewBag.PageTitle = "GoogFleet";
                ViewBag.Logo = "logo.png";
            }
            else if (Domain.Contains("www.fleetmanager.us"))
            {
                ViewBag.PageTitle = "Fleet Manager";
                ViewBag.Logo = "logo2.png";
            }
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
