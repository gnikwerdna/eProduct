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
    public class CompliancesController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        // GET: Compliances
        public ActionResult Index()
        {


            /*
            IQueryable<Course> courses = db.Courses
                .Where(c => !SelectedDepartment.HasValue || c.DepartmentID == departmentID)
                .OrderBy(d => d.CourseID)
                .Include(d => d.Department);
            var sql = courses.ToString();
            return View(courses.ToList());
            
            */
            IQueryable<Compliance> compliance = db.compliance
                .Include(d => d.ComplianceForm)
                .OrderBy(d=>d.grp)
                .OrderByDescending(d=>d.order)
                ;
                
            var sql = compliance.ToString();
            return View(compliance.ToList());
            //return View(db.compliance.ToList());
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
            createPopulateComplianceForms();
            CreatePopulateComplianceSubItems();
            return View();
        }

        // POST: Compliances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplianceID,Title,Description,subID,ComplianceFormId,level,grp,order")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                db.compliance.Add(compliance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            PopulateComplianceForms(compliance.ComplianceFormId);
            PopulateComplianceSubItems(compliance.ComplianceFormId);
            return View(compliance);
        }
        private void createPopulateComplianceForms()
        {
            var ComplianceFormsQuery = from d in db.ComplianceForms
                                       orderby d.FormName
                                       select d;
            ViewBag.ComplianceFormId = new SelectList(ComplianceFormsQuery, "ComplianceFormId", "FormName", "Select");
        }

        private void CreatePopulateComplianceSubItems()
        {
            var compsubitems = from d in db.compliance
                               orderby d.Title
                               select d;
            ViewBag.subID = new SelectList(compsubitems, "ComplianceID", "Title", "Select");

        }

        private void PopulateComplianceForms(object selectedForm = null)
        {
            var ComplianceFormsQuery = from d in db.ComplianceForms
                                       orderby d.FormName
                                   select d;
            ViewBag.ComplianceFormId = new SelectList(ComplianceFormsQuery, "ComplianceFormId", "FormName", selectedForm);
        }
        private void PopulateComplianceSubItems(object selectedForm = null)
        {
            var compsubitems = from d in db.compliance
                               orderby d.Title
                               select d;
            ViewBag.subID = new SelectList(compsubitems, "ComplianceID", "Title", selectedForm);

        }
        // POST: Compliances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplianceID,Title,Description,subID,ComplianceFormId,level,grp,order")] Compliance compliance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compliance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
