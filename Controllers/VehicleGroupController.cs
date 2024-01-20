using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogFleet.Models;

namespace GoogFleet.Controllers
{
    public class VehicleGroupController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: VehicleGroup
        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();
            return View(db.VehicleGroup_T.OrderBy(x=>x.VehicleGroup).ToList());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FleetCompanyID,VehicleGroup,VehicleGroupID")] VehicleGroup_T vehicleGroup_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.VehicleGroup_T.Add(vehicleGroup_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleGroup_T);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FleetCompanyID,VehicleGroup,VehicleGroupID")] VehicleGroup_T vehicleGroup_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.Entry(vehicleGroup_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleGroup_T);
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
