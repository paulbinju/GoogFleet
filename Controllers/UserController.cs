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
    public class UserController : Controller
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
            else if (Domain.Contains("www.fleetmanager.us"))
            {
                ViewBag.PageTitle = "Fleet Manager";
                ViewBag.Logo = "logo2.png";
            }
        }
        // GET: User
        public ActionResult Index(string user)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();
            ViewBag.user = user;
            return View(db.User_T.Where(x => x.UserType == user).OrderByDescending(x => x.UserID).ToList());
        }

        public ActionResult UserVehicleAsso1(int userid) {

            Session["selecteddriverid"] = userid;
            return RedirectToAction("Available", "Vehicle");
        }


       

        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            return View();
        }

        [HttpPost]
        public ActionResult Createx(FormCollection col)
        {
            User_T user_T = new User_T();
            user_T.FleetCompanyID = Convert.ToInt32(col["FleetCompanyID"]);
            user_T.UserName = Convert.ToString(col["UserName"]);
            user_T.UserType = Convert.ToString(col["UserType"]);
            user_T.EmailID = Convert.ToString(col["EmailID"]);
            user_T.MobileNo = Convert.ToString(col["MobileNo"]);
            user_T.NationalID = Convert.ToString(col["NationalID"]);
            user_T.Address = Convert.ToString(col["Address"]);
            user_T.DOC = Convert.ToDateTime(DateTime.Now);
            db.User_T.Add(user_T);
            db.SaveChanges();
            return RedirectToAction("../User/" + Convert.ToString(col["UserType"]));
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
