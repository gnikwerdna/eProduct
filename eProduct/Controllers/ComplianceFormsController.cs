using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eProduct.Models;

namespace eProduct.Controllers
{
    public class ComplianceFormsController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        // GET: ComplianceForms
        public ActionResult Index()
        {
            return View(db.ComplianceForms.ToList());
        }

        // GET: ComplianceForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplianceForm complianceForm = db.ComplianceForms.Find(id);
            if (complianceForm == null)
            {
                return HttpNotFound();
            }
            return View(complianceForm);
        }

        // GET: ComplianceForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComplianceForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplianceFormId,FormName,Description")] ComplianceForm complianceForm)
        {
            if (ModelState.IsValid)
            {
                db.ComplianceForms.Add(complianceForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complianceForm);
        }

        // GET: ComplianceForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplianceForm complianceForm = db.ComplianceForms.Find(id);
            if (complianceForm == null)
            {
                return HttpNotFound();
            }
            return View(complianceForm);
        }

        // POST: ComplianceForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplianceFormId,FormName,Description")] ComplianceForm complianceForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complianceForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(complianceForm);
        }

        // GET: ComplianceForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplianceForm complianceForm = db.ComplianceForms.Find(id);
            if (complianceForm == null)
            {
                return HttpNotFound();
            }
            return View(complianceForm);
        }

        // POST: ComplianceForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComplianceForm complianceForm = db.ComplianceForms.Find(id);
            db.ComplianceForms.Remove(complianceForm);
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
