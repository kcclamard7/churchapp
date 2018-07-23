using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Church.Models;

namespace Church.Controllers
{
    [Authorize]
    public class CategoryExpensController : Controller
    {
        private ChurchMgtEntities db = new ChurchMgtEntities();

        // GET: CategoryExpens
        public ActionResult Index()
        {
            return View(db.CategoryExpenses.ToList());
        }

        // GET: CategoryExpens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryExpens categoryExpens = db.CategoryExpenses.Find(id);
            if (categoryExpens == null)
            {
                return HttpNotFound();
            }
            return View(categoryExpens);
        }

        // GET: CategoryExpens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryExpens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Name")] CategoryExpens categoryExpens)
        {
            if (ModelState.IsValid)
            {
                db.CategoryExpenses.Add(categoryExpens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryExpens);
        }

        // GET: CategoryExpens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryExpens categoryExpens = db.CategoryExpenses.Find(id);
            if (categoryExpens == null)
            {
                return HttpNotFound();
            }
            return View(categoryExpens);
        }

        // POST: CategoryExpens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name")] CategoryExpens categoryExpens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryExpens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryExpens);
        }

        // GET: CategoryExpens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryExpens categoryExpens = db.CategoryExpenses.Find(id);
            if (categoryExpens == null)
            {
                return HttpNotFound();
            }
            return View(categoryExpens);
        }

        // POST: CategoryExpens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryExpens categoryExpens = db.CategoryExpenses.Find(id);
            db.CategoryExpenses.Remove(categoryExpens);
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
