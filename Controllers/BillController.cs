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
    public class BillController : Controller
    {
        private FleetManagerEntities db = new FleetManagerEntities();

        // GET: Bill
        public ActionResult Index()
        {
            return View(db.Bill_T.ToList());
        }

        // GET: Bill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_T bill_T = db.Bill_T.Find(id);
            if (bill_T == null)
            {
                return HttpNotFound();
            }
            return View(bill_T);
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(FormCollection col)
        {
            Bill_T bill_T = new Bill_T();
            bill_T.FleetCompanyID = Convert.ToInt32(Session["FleetCompanyID"]);
            bill_T.VehicleID = Convert.ToInt32(col["VehicleID"]);
            bill_T.BillNo = Convert.ToString(col["BillNo"]);
            bill_T.BillDate = Convert.ToDateTime(col["BillDate"]);
            bill_T.DOC = DateTime.Now;
            bill_T.ShopID = Convert.ToInt32(col["ShopID"]);
            bill_T.TotalAmount = Convert.ToDecimal(col["TotalAmount"]);
            bill_T.Odometer = Convert.ToString(col["Odometer"]);

            db.Bill_T.Add(bill_T);
            db.SaveChanges();
            return RedirectToAction("../BillDetail/" + bill_T.BillID);
        }

        // GET: Bill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_T bill_T = db.Bill_T.Find(id);
            if (bill_T == null)
            {
                return HttpNotFound();
            }
            return View(bill_T);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillID,FleetCompanyID,BillNo,BillDate,ShopID,VehicleID,Odometer,TotalAmount,DOC")] Bill_T bill_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bill_T);
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_T bill_T = db.Bill_T.Find(id);
            if (bill_T == null)
            {
                return HttpNotFound();
            }
            return View(bill_T);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill_T bill_T = db.Bill_T.Find(id);
            db.Bill_T.Remove(bill_T);
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
