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
    public class AlertController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();


        [HttpPost]
        public ActionResult Create(FormCollection col)
        {

            Alert_T alert_T = new Alert_T();
            alert_T.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            alert_T.VehicleID = Convert.ToInt32(col["VehicleID"]);
            alert_T.DueDate = Convert.ToDateTime(col["duedate"]);
            alert_T.Alert = Convert.ToString(col["alert"]);
            db.Alert_T.Add(alert_T);
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
