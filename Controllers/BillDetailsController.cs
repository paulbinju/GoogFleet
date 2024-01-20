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
    public class BillDetailsController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: BillDetails

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

        public ActionResult Index(int billid)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }
            domainfinder();

            int fleetcompanyid = Convert.ToInt32(Session["FleetCompanyID"]);
            ViewBag.BillID = billid;
            ViewBag.ServiceParts = db.ServiceParts_T.Where(x => x.FleetCompanyID == fleetcompanyid).OrderBy(x => x.ServicePartName).ToList();


            ViewBag.BillSummary = (from b in db.Bill_T
                                   where b.BillID == billid
                                   select b).ToList();


            ViewBag.BillDetails = (from bd in db.BillDetails_T
                                   join sp in db.ServiceParts_T on bd.ServicePartsID equals sp.ServicePartsID
                                   where bd.BillID == billid && bd.FleetCompanyID == fleetcompanyid
                                   select new BillDetailed { ServiceParts = sp.ServicePartName, BillID = bd.BillID, Quantity = bd.Quantity, Description = bd.Description, Amount = bd.Amount }
                                   ).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection col)
        {
            if (Session["FleetCompanyID"] == null) { return RedirectToAction("Login", "Home"); }

            BillDetails_T billDetails_T = new BillDetails_T();
            billDetails_T.BillID = Convert.ToInt32(col["BillID"]);
            billDetails_T.FleetCompanyID = Convert.ToInt32(Convert.ToInt32(Session["FleetCompanyID"]));
            billDetails_T.ServicePartsID = Convert.ToInt32(col["ServicePartID"]);
            billDetails_T.Description = Convert.ToString(col["Description"]);
            billDetails_T.Quantity = Convert.ToInt32(col["Quantity"]);
            billDetails_T.Amount = Convert.ToDecimal(col["Amount"]);
            db.BillDetails_T.Add(billDetails_T);
            db.SaveChanges();
            return RedirectToAction("../BillDetail/" + billDetails_T.BillID);
        }

        // GET: BillDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillDetails_T billDetails_T = db.BillDetails_T.Find(id);
            if (billDetails_T == null)
            {
                return HttpNotFound();
            }
            return View(billDetails_T);
        }

        // POST: BillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillDetailsID,FleetCompanyID,BillID,ServicePartsID,Description,Quantity,Amount")] BillDetails_T billDetails_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billDetails_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billDetails_T);
        }

        // GET: BillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillDetails_T billDetails_T = db.BillDetails_T.Find(id);
            if (billDetails_T == null)
            {
                return HttpNotFound();
            }
            return View(billDetails_T);
        }

        // POST: BillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillDetails_T billDetails_T = db.BillDetails_T.Find(id);
            db.BillDetails_T.Remove(billDetails_T);
            db.SaveChanges();
            return RedirectToAction("Index");
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
