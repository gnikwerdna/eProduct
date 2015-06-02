using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eP.Models;

namespace eP.Controllers
{
    public class CompliancesController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        // GET: Compliances
        public ActionResult Index()
        {
            var compliance = db.compliance.Include(c => c.ComplianceForm);
            return View(compliance.ToList());
        }

        // GET: Compliances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compliance compliance = db.compliance.Find(id);
            if (compliance == null)
            {
                return HttpNotFound();
            }
            return View(compliance);
        }

        // GET: Compliances/Create
        public ActionResult Create()
        {
            ViewBag.ComplianceFormId = new SelectList(db.ComplianceForms, "ComplianceFormId", "FormName");
            return View();
        }

        // POST: Compliances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplianceID,Title,Description,subID,level,grp,order,ComplianceFormId")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                db.compliance.Add(compliance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComplianceFormId = new SelectList(db.ComplianceForms, "ComplianceFormId", "FormName", compliance.ComplianceFormId);
            return View(compliance);
        }

        // GET: Compliances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compliance compliance = db.compliance.Find(id);
            if (compliance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComplianceFormId = new SelectList(db.ComplianceForms, "ComplianceFormId", "FormName", compliance.ComplianceFormId);
            return View(compliance);
        }

        // POST: Compliances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplianceID,Title,Description,subID,level,grp,order,ComplianceFormId")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compliance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComplianceFormId = new SelectList(db.ComplianceForms, "ComplianceFormId", "FormName", compliance.ComplianceFormId);
            return View(compliance);
        }

        // GET: Compliances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compliance compliance = db.compliance.Find(id);
            if (compliance == null)
            {
                return HttpNotFound();
            }
            return View(compliance);
        }

        // POST: Compliances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compliance compliance = db.compliance.Find(id);
            db.compliance.Remove(compliance);
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
