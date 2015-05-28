using eProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProduct.Controllers
{
    public class UsersoldController : Controller
    {

        private ProductDBContext db = new ProductDBContext();
        // GET: Users
        public ActionResult Index()
        {
            //IQueryable viewModel = null;
            //viewModel = from u in db.users select new { u.UserID, u.Fname, u.Lname };
            var viewModel = db.users.ToList();
            return View(viewModel);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            //fill supplier dropdown via view bag
            var utype = from u in db.usertypes orderby u.TypeDesc select new { u.ID, u.TypeDesc };
            var utypelst = utype.ToList().Select(u => new SelectListItem
            {
                Text = u.TypeDesc.ToString(),
                Value = u.ID.ToString()

            }).ToList();

            ViewBag.usertypeList = utypelst;
            return View();
        }
       
        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
