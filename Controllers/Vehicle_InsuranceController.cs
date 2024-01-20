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
    public class Vehicle_InsuranceController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        
        [HttpPost]
        public ActionResult Create(FormCollection col)
        {
            Vehicle_Insurance_T vehicle_Insurance_T = new Vehicle_Insurance_T();
            vehicle_Insurance_T.VehicleID = Convert.ToInt32(col["VehicleID"]);
            vehicle_Insurance_T.FleetCompanyID = Convert.ToInt32(col["FleetCompanyID"]);
            vehicle_Insurance_T.InsuranceID = Convert.ToInt32(col["InsuranceID"]);
            vehicle_Insurance_T.StartDate = Convert.ToDateTime(col["startdate"]);
            vehicle_Insurance_T.EndDate = Convert.ToDateTime(col["enddate"]);
            db.Vehicle_Insurance_T.Add(vehicle_Insurance_T);
            db.SaveChanges();

            string insurancecompany = db.Database.SqlQuery<string>("select Insurance from Insurance_T where InsuranceID=" + Convert.ToInt32(col["InsuranceID"]) + " and FleetCompanyID=" + Convert.ToInt32(col["FleetCompanyID"])).Single();

            // add alert

            Alert_T alertt = new Alert_T();
            alertt.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            alertt.VehicleID = Convert.ToInt32(col["VehicleID"]);
            alertt.DueDate = Convert.ToDateTime(col["enddate"]).AddDays(-10);
            alertt.Alert = "Insurance with " + insurancecompany + " is expiring on " + Convert.ToDateTime(col["enddate"]).ToString("dd-MMM-yyyy");
            db.Alert_T.Add(alertt);
            db.SaveChanges();

            return RedirectToAction("../VehicleDetails/" + Convert.ToInt32(col["VehicleID"]));
        }
         
 
    
 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Vehicle_Insurance_T vehicle_Insurance_T = db.Vehicle_Insurance_T.Find(id);
            db.Vehicle_Insurance_T.Remove(vehicle_Insurance_T);
            db.SaveChanges();
            return RedirectToAction("../VehicleDetails/" + id);
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
