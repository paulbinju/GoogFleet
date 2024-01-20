using GoogFleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoogFleet.Controllers
{
    public class HomeController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        public ActionResult Index()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }

            domainfinder();
            return View();
        }

        public ActionResult Login()
        {
            domainfinder();
            return View();
        }

        

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            int fleetcompanyid = Convert.ToInt32(Session["FleetCompanyID"]);
            ViewBag.fleetcompany = (from u in db.FleetCompany_T
                                where u.FleetCompanyID == fleetcompanyid
                                select u).ToList();


            ViewBag.Subscriptions = (from s in db.Subscription_T
                                     where s.FleetCompanyID == fleetcompanyid
                                     orderby s.SubscriptionID descending
                                     select s
                                   ).ToList();
            return View();
        }
        public ActionResult Logout()
        {
            Session["FleetCompanyID"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection col)
        {
            string userid = Convert.ToString(col["username"]);
            string pass = Convert.ToString(col["password"]);


            var fleetcompany = (from u in db.FleetCompany_T
                       where u.EmailID == userid && u.Password == pass
                       select u).ToList();

            if (fleetcompany.Count>0)
            {
                Session["FleetCompanyID"] = Convert.ToInt32(fleetcompany[0].FleetCompanyID);
                Session["ContactPerson"] = Convert.ToString(fleetcompany[0].ContactPerson);

                return RedirectToAction("../");
            }
            else
            {
                ViewBag.Results = "Invalid User ID / Password";
            }

            return View();
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
    }
}