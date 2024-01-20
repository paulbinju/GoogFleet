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
    public class AutoBrandController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: AutoBrand
        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();
            return View(db.AutoBrand_T.OrderBy(x => x.Make).ThenBy(x => x.Model).ToList());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutoBrandID,FleetCompanyID,Make,Model")] AutoBrand_T autoBrand_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.AutoBrand_T.Add(autoBrand_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoBrand_T);
        }

  
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AutoBrandID,FleetCompanyID,Make,Model")] AutoBrand_T autoBrand_T)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            if (ModelState.IsValid)
            {
                db.Entry(autoBrand_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autoBrand_T);
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
