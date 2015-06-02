using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eP.Models;
using eP.ViewModels;
using System.Data.Entity.Infrastructure;

namespace eP.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult Compliance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            popassignedcompliance(product);
            popSubassignedcompliance(product);
            if (product == null)
            {
                return HttpNotFound();
            }
            PopulateProdSpecDropDownList(product.ProductManager);
            PopulateProdManDropDownList(product.ProductManager);
            PopulateSupplierDropDownList(product.SupplierID);
            PopulateCategoryDropDownList(product.CategoryID);
            return View(product);

        }
       //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compliance(int? id, string[] SelectedComp, string[] SelectedCompSubItem_1, string[] SelectedCompSubItem_2)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instructorToUpdate = db.Products

               .Include(i => i.Compliance)
               .Where(i => i.ProductID == id)
               .Single();

            if (TryUpdateModel(instructorToUpdate, "",
               new string[] { "ProductID", "ProductName", "Price", "SupplierID", "CategoryID", "ProductGroup", "ProntoPartNumber", "ProductManager", "StandardReferenceNumber", "TestHouseName", "AccreditationAgencyName", "Licenses", "Regulation" }))
            {
                try
                {


                    ResetProducts(SelectedComp, instructorToUpdate);
                    UpdateInstructorCourses(SelectedComp, instructorToUpdate);
                    // UpdateInstructorCourses(SelectedCompSubItem_1, instructorToUpdate);
                    //UpdateInstructorCourses(SelectedCompSubItem_2, instructorToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            popassignedcompliance(instructorToUpdate);
            return View(instructorToUpdate);

        }
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void ResetProducts(string[] selectedCourses, Product instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Compliance = new List<Compliance>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Compliance.Select(c => c.ComplianceID));
            foreach (var course in db.compliance)
            {
                instructorToUpdate.Compliance.Remove(course);
            }
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Product instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Compliance = new List<Compliance>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Compliance.Select(c => c.ComplianceID));
            foreach (var course in db.compliance)
            {
                if (selectedCoursesHS.Contains(course.ComplianceID.ToString()))
                {
                    if (!instructorCourses.Contains(course.ComplianceID))
                    {
                        instructorToUpdate.Compliance.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.ComplianceID))
                    {
                        // instructorToUpdate.Compliance.Remove(course);
                    }
                }
            }
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void popassignedcompliance(Product product)
        {
            var allCompliance = from a in db.compliance orderby a.grp, a.order select a;
            //var allCompliance = db.compliance;
            var productcompliance = new HashSet<int>(product.Compliance.Select(c => c.ComplianceID));
            var viewModel = new List<AssignedComplianceData>();

            foreach (var compliance in allCompliance)
            {
                viewModel.Add(new AssignedComplianceData
                {
                    ComplianceID = compliance.ComplianceID,
                    Title = compliance.Title,
                    Assigned = productcompliance.Contains(compliance.ComplianceID),
                    level = compliance.level,
                    subId = compliance.subID,
                    grp = compliance.grp,
                    order = compliance.order
                });


            }
            ViewBag.compliance = viewModel;


        }
        private void popSubassignedcompliance(Product product)
        {

            var allCompliance = from a in db.compliance where a.subID == a.ComplianceID select a;
            var productcompliance = new HashSet<int>(product.Compliance.Select(c => c.ComplianceID));
            var viewModel = new List<AssignedComplianceData>();

            foreach (var compliance in allCompliance)
            {
                viewModel.Add(new AssignedComplianceData
                {
                    ComplianceID = compliance.ComplianceID,
                    Title = compliance.Title,
                    Assigned = productcompliance.Contains(compliance.ComplianceID),
                    level = compliance.level,
                    subId = compliance.subID
                });


            }
            ViewBag.subcompliance = viewModel;


        }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void PopulateCategoryDropDownList(object selectedCategory)
        {
            var CatQuery = from c in db.Categories orderby c.CategoryName select c;
            ViewBag.CategoryID = new SelectList(CatQuery, "CategoryID", "CategoryName", selectedCategory);


        }
        private void PopulateSupplierDropDownList(object selectedSupplier)
        {
            var SupQuery = from s in db.Suppliers orderby s.CompanyName select s;
            ViewBag.SupplierID = new SelectList(SupQuery, "SupplierID", "CompanyName", selectedSupplier);

        }
        private void PopulateProdManDropDownList(object selectedProdMan)
        {
            var ProdManQuery = from u in db.users orderby u.Lname select new { u.UserID, fullname = u.Fname + " " + u.Lname };
            ViewBag.UserID = new SelectList(ProdManQuery, "UserID", "fullname", selectedProdMan);

        }
        private void PopulateProdSpecDropDownList(object selectedProdSpec)
        {
            var ProdSpectQuery = from u in db.users orderby u.Lname select new { u.UserID, fullname = u.Fname + " " + u.Lname };
            ViewBag.UserProdSpectID = new SelectList(ProdSpectQuery, "UserID", "fullname", selectedProdSpec);

        }
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductGroup,ProntoPartNumber,ProductManager,ProductComplianceSpecialist,StandardReferenceNumber,Regulation,TestHouseName,AccreditationAgencyName,Licenses,SupplierID,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductGroup,ProntoPartNumber,ProductManager,ProductComplianceSpecialist,StandardReferenceNumber,Regulation,TestHouseName,AccreditationAgencyName,Licenses,SupplierID,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
