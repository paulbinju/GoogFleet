using System;
using System.Web.Mvc;
using GoogFleet.Models;

namespace GoogFleet.Controllers
{
    public class VehicleRegistrationController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        [HttpPost]
        public ActionResult Create(FormCollection col)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            VehicleRegistration_T vehicleRegistration_T = new VehicleRegistration_T();
            vehicleRegistration_T.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            vehicleRegistration_T.VehicleID = Convert.ToInt32(col["VehicleID"]);
            vehicleRegistration_T.StartDate = Convert.ToDateTime(col["startdate"]);
            vehicleRegistration_T.EndDate= Convert.ToDateTime(col["enddate"]);
            db.VehicleRegistration_T.Add(vehicleRegistration_T);
            db.SaveChanges();

            // add alert

            Alert_T alertt = new Alert_T();
            alertt.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            alertt.VehicleID = Convert.ToInt32(col["VehicleID"]);
            alertt.DueDate = Convert.ToDateTime(col["enddate"]).AddDays(-10);
            alertt.Alert = "Vehicle Registration is expiring on " + Convert.ToDateTime(col["enddate"]).ToString("dd-MMM-yyyy");
            db.Alert_T.Add(alertt);
            db.SaveChanges();

            return RedirectToAction("../VehicleDetails/" + Convert.ToInt32(col["VehicleID"]));

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
