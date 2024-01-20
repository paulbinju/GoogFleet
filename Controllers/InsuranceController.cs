using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GoogFleet.Models;

namespace GoogFleet.Controllers
{
    public class InsuranceController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: Insurance
        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();
            return View(db.Insurance_T.OrderBy(x=>x.Insurance).ToList());
        }

         
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InsuranceID,FleetCompanyID,Insurance")] Insurance_T insurance_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.Insurance_T.Add(insurance_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insurance_T);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsuranceID,FleetCompanyID,Insurance")] Insurance_T insurance_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.Entry(insurance_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insurance_T);
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
