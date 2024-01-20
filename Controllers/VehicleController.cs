using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogFleet.Models;
using System.Data.SqlClient;

namespace GoogFleet.Controllers
{
    public class VehicleController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        public void domainfinder()
        {
            string Domain = Request.Url.ToString();
            ViewBag.Domain = Domain;

            if (Domain.Contains("app.googfleet.com"))
            {
                ViewBag.PageTitle = "GoogFleet";
                ViewBag.Logo = "logo.png";
            }
            else if (Domain.Contains("fleetmanager.us"))
            {
                ViewBag.PageTitle = "Fleet Manager";
                ViewBag.Logo = "logo2.png";
            }
        }
        // GET: Vehicle
        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }

            domainfinder();

            ViewBag.Vehicles = db.Database.SqlQuery<Vehicles>(@"select VehicleID,VehicleNoName,VehicleGroup,VehicleType,RegistrationNo,Make+' '+model as Brand from [dbo].[Vehicle_T] v 
             join  [dbo].[VehicleGroup_T] vg on v.VehicleGroupID=vg.VehicleGroupID
             join VehicleType_T vt on v.VehicleTypeID = vt.VehicleTypeID
             join AutoBrand_T ab on v.AutoBrandID=ab.AutoBrandID order by v.vehicleid desc
            ");
            


            return View();
        }
        public ActionResult VehicleUserAllocation(int vehicleid, string message)
        {
            if (Session["selecteddriverid"] == null) { return RedirectToAction("Customer", "User"); }
            domainfinder();

            ViewBag.UserID = Convert.ToString(Session["selecteddriverid"]);
            ViewBag.VehicleID = Convert.ToString(vehicleid);
            if (message == "Allocated")
            {
                ViewBag.Allocated = "TRUE";
            }

            ViewBag.Vehicle = db.Database.SqlQuery<Vehicles>(@"select VehicleID,VehicleNoName,VehicleGroup,VehicleType,RegistrationNo,Make+' '+model as Brand,Note,YearofManufacture from [dbo].[Vehicle_T] v 
             join  [dbo].[VehicleGroup_T] vg on v.VehicleGroupID=vg.VehicleGroupID
             join VehicleType_T vt on v.VehicleTypeID = vt.VehicleTypeID
             join AutoBrand_T ab on v.AutoBrandID=ab.AutoBrandID 
            where  v.fleetcompanyid=" + Session["FleetCompanyID"] + " and vehicleid= " + vehicleid + " order by v.vehicleid desc");

            ViewBag.User = db.Database.SqlQuery<User_T>("select * from user_t where fleetcompanyid=" + Session["FleetCompanyID"] + " and userid=" + Convert.ToString(Session["selecteddriverid"]));

            return View();
        }

        [HttpPost]
        public ActionResult VehicleUserAllocation(FormCollection col)
        {
            Vehicle_User_T vehicleuser = new Vehicle_User_T();
            vehicleuser.DOC = DateTime.Now;
            vehicleuser.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            vehicleuser.StartDate = Convert.ToDateTime(DateTime.ParseExact(col["startdate"], "MM/dd/yyyy", null).ToString("yyyy/MM/dd"));
            vehicleuser.UserID = Convert.ToInt32(col["userid"]);
            vehicleuser.VehicleID = Convert.ToInt32(col["vehicleid"]);
            vehicleuser.Note = Convert.ToString(col["notes"]);
            db.Vehicle_User_T.Add(vehicleuser);
            db.SaveChanges();

            return RedirectToAction("../VehicleUserAllocation/" + col["vehicleid"] + "/Allocated");
        }






        public ActionResult VehicleDetails(int vehicleid, string message)
        {
            domainfinder();
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }

            int FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);

            ViewBag.VehicleID = Convert.ToString(vehicleid);
            if (message == "Allocated")
            {
                ViewBag.Allocated = "TRUE";
            }

            ViewBag.Vehicle = db.Database.SqlQuery<Vehicles>(@"select VehicleID,VehicleNoName,VehicleNo,VehicleGroup,v.VehicleGroupID,VehicleType,v.VehicleTypeID,v.AutoBrandID,RegistrationNo,Make+' '+model as Brand,Note,YearofManufacture from [dbo].[Vehicle_T] v 
             join  [dbo].[VehicleGroup_T] vg on v.VehicleGroupID=vg.VehicleGroupID
             join VehicleType_T vt on v.VehicleTypeID = vt.VehicleTypeID
             join AutoBrand_T ab on v.AutoBrandID=ab.AutoBrandID 
            where  v.fleetcompanyid=" + Session["FleetCompanyID"] + " and vehicleid= " + vehicleid + " order by v.vehicleid desc");

            ViewBag.InsuranceCompanies = (from i in db.Insurance_T
                                          where i.FleetCompanyID == FleetCompanyID
                                          orderby i.Insurance
                                          select i
                                         ).ToList();

            ViewBag.VehicleInsurance = (from vi in db.Vehicle_Insurance_T
                                        join i in db.Insurance_T on vi.InsuranceID equals i.InsuranceID
                                        where vi.FleetCompanyID == FleetCompanyID && vi.VehicleID == vehicleid
                                        orderby vi.VehicleInsuranceID descending
                                        select new VehicleInsurance { VehicleInsuranceID = vi.VehicleInsuranceID, VehicleID = vi.VehicleID, InsuranceID = vi.InsuranceID, StartDate = vi.StartDate, EndDate = vi.EndDate, Insurance = i.Insurance }
                                        ).ToList();


            ViewBag.VehicleAlerts = (from va in db.Alert_T
                                     where va.FleetCompanyID == FleetCompanyID && va.VehicleID == vehicleid
                                     orderby va.AlertID descending
                                     select va).ToList();


            ViewBag.VehicleRegistrations = (from vr in db.VehicleRegistration_T
                                            where vr.FleetCompanyID == FleetCompanyID && vr.VehicleID == vehicleid
                                            orderby vr.VehicleRegistrationID descending
                                            select vr).ToList();

            ViewBag.VehicleUsers = (from vu in db.Vehicle_User_T
                                    join u in db.User_T on vu.UserID equals u.UserID
                                    where vu.VehicleID == vehicleid
                                    orderby vu.StartDate descending
                                    select new VehicleUser { UserName = u.UserName, EmailID = u.EmailID, MobileNo = u.MobileNo, NationalID = u.NationalID, StartDate = vu.StartDate, EndDate = vu.EndDate, UserType = u.UserType, VehicleUserID = vu.VehicleDriverID, Note = vu.Note, UserID = vu.UserID }
                                  ).ToList();


            ViewBag.TrafficContraventions = (from tc in db.TrafficContraventions_T
                                             join u in db.User_T on tc.UserID equals u.UserID
                                             where tc.VehicleID == vehicleid
                                             where tc.FleetCompanyID == FleetCompanyID && tc.VehicleID == vehicleid
                                             orderby tc.TrafficContraventionID descending
                                             select new VehicleTraffic { UserName = u.UserName, Description = tc.Description, TrafficContraventionsDate = tc.TrafficContraventionDate }).ToList();
            ViewBag.Shop = (from sh in db.Shop_T
                            orderby sh.ShopName
                            select sh
                            ).ToList();

            ViewBag.VehicleBills = (from vb in db.Bill_T
                                    where vb.FleetCompanyID == FleetCompanyID && vb.VehicleID == vehicleid
                                    select vb).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult VehicleRemoveAllocation(FormCollection col) {

            Vehicle_User_T vehicleuser = new Vehicle_User_T();
            vehicleuser.VehicleDriverID = Convert.ToInt32(col["VehicleUserID"]);
            vehicleuser.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            vehicleuser.VehicleID = Convert.ToInt32(col["VehicleID"]);
            vehicleuser.UserID = Convert.ToInt32(col["UserID"]);
            vehicleuser.StartDate = Convert.ToDateTime(col["startdate"]);
            vehicleuser.EndDate = Convert.ToDateTime(col["enddate"]);

            vehicleuser.Note = Convert.ToString(col["note"]);

            db.Entry(vehicleuser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("../VehicleDetails/" + Convert.ToInt32(col["VehicleID"]));


             
        }

        [HttpPost]
        public ActionResult VehicleBill(FormCollection col)
        {
            Bill_T bill = new Bill_T();
            bill.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            bill.VehicleID = Convert.ToInt32(col["VehicleID"]);
            bill.BillNo = Convert.ToString(col["BillNo"]);
            bill.BillDate = Convert.ToDateTime(col["BillDate"]);
            bill.ShopID = Convert.ToInt32(col["ShopID"]);
            bill.Odometer = Convert.ToString(col["Odometer"]);
            bill.TotalAmount = Convert.ToInt32(col["TotalAmount"]);
            db.Bill_T.Add(bill);
            db.SaveChanges();
            return RedirectToAction("../VehicleDetail/" + Convert.ToInt32(col["VehicleID"]));



        }

        [HttpPost]
        public ActionResult VehicleTrafficContraventions(FormCollection col)
        {
            TrafficContraventions_T trafficCon = new TrafficContraventions_T();

            trafficCon.UserID = Convert.ToInt32(col["userid"]);
            trafficCon.VehicleID = Convert.ToInt32(col["vehicleid"]);
            trafficCon.Description = Convert.ToString(col["Description"]);
            trafficCon.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            trafficCon.TrafficContraventionDate = Convert.ToDateTime(col["TrafficContraventionDate"]);
            db.TrafficContraventions_T.Add(trafficCon);
            db.SaveChanges();

            return RedirectToAction("../VehicleDetails/" + Convert.ToInt32(col["VehicleID"]));
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection col)
        {

            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }

            //stored procedure
            string vehiclegroupnoname = db.Database.SqlQuery<string>("vehiclegroupname @FleetCompanyID, @VehicleGroupID", new SqlParameter("FleetCompanyID", Convert.ToInt32(Session["FleetCompanyID"])), new SqlParameter("@VehicleGroupID", Convert.ToInt32(col["VehicleGroupID"]))).Single();
            int vehiclegroupno = db.Database.SqlQuery<int>("select ISNULL(MAX(vehicleno),0)+1 as vehicleno from Vehicle_T where FleetCompanyID=" + Convert.ToInt32(Session["FleetCompanyID"]) + " and VehicleGroupID=" + Convert.ToInt32(col["VehicleGroupID"])).Single();

            Vehicle_T veh = new Vehicle_T();
            veh.VehicleNo = Convert.ToInt32(vehiclegroupno);
            veh.VehicleNoName = vehiclegroupnoname;
            veh.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            veh.VehicleGroupID = Convert.ToInt32(col["VehicleGroupID"]);
            veh.VehicleTypeID = Convert.ToInt32(col["VehicleTypeID"]);
            veh.AutoBrandID = Convert.ToInt32(col["AutoBrandID"]);
            veh.GPSURL = Convert.ToString(col["GPSURL"]);
            veh.RegistrationNo = Convert.ToString(col["RegistrationNo"]);
            veh.YearofManufacture = Convert.ToDateTime(col["YearofManufacture"]);
            veh.Note = Convert.ToString(col["Note"]);
            veh.DOC = Convert.ToDateTime(DateTime.Now);
            db.Vehicle_T.Add(veh);
            db.SaveChanges();

            return RedirectToAction("/");
        }

         

 
        [HttpPost]
        public ActionResult Edit(FormCollection col)
        {
            string vehiclegroupnoname = Convert.ToString(col["VehicleNoName"]);
            int vehiclegroupno = Convert.ToInt32(col["VehicleNo"]);

            if (Convert.ToString(col["LastVehicleGroupID"])!= Convert.ToString(col["VehicleGroupID"]))
            {
                vehiclegroupnoname = db.Database.SqlQuery<string>("vehiclegroupname @FleetCompanyID, @VehicleGroupID", new SqlParameter("FleetCompanyID", Convert.ToInt32(Session["FleetCompanyID"])), new SqlParameter("@VehicleGroupID", Convert.ToInt32(col["VehicleGroupID"]))).Single();
                vehiclegroupno = db.Database.SqlQuery<int>("select ISNULL(MAX(vehicleno),0)+1 as vehicleno from Vehicle_T where FleetCompanyID=" + Convert.ToInt32(Session["FleetCompanyID"]) + " and VehicleGroupID=" + Convert.ToInt32(col["VehicleGroupID"])).Single();
            }
            Vehicle_T veh = new Vehicle_T();
            veh.VehicleID = Convert.ToInt32(col["VehicleID"]);
            veh.VehicleNo = Convert.ToInt32(vehiclegroupno);
            veh.VehicleNoName = vehiclegroupnoname;
            veh.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            veh.VehicleGroupID = Convert.ToInt32(col["VehicleGroupID"]);
            veh.VehicleTypeID = Convert.ToInt32(col["VehicleTypeID"]);
            veh.AutoBrandID = Convert.ToInt32(col["AutoBrandID"]);
            veh.RegistrationNo = Convert.ToString(col["RegistrationNo"]);
            veh.YearofManufacture = Convert.ToDateTime(col["YearofManufacture"]);
            veh.Note = Convert.ToString(col["Note"]);
            veh.DOC = Convert.ToDateTime(DateTime.Now);
            
            db.Entry(veh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("../VehicleDetails/" + veh.VehicleID);

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
