using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GoogFleet.Models;

namespace GoogFleet.Controllers
{
    public class ServicePartsController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: ServiceParts
        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();
            return View(db.ServiceParts_T.OrderBy(x=>x.ServicePartName).ToList());
        }

        
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicePartsID,FleetCompanyID,ServicePartName")] ServiceParts_T serviceParts_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.ServiceParts_T.Add(serviceParts_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceParts_T);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicePartsID,FleetCompanyID,ServicePartName")] ServiceParts_T serviceParts_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.Entry(serviceParts_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceParts_T);
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
