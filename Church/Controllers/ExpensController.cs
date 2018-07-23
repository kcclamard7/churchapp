using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Church.Models;
using PagedList.Mvc;
using PagedList;


namespace Church.Controllers
{
    [Authorize]
    public class ExpensController : Controller
    {
        private ChurchMgtEntities db = new ChurchMgtEntities();
        
        List<Expens> allExpenses;
        // GET: Expens
        public ActionResult Index(DateTime? Date1, DateTime? Date2, int? ChurchID, int? page)
        {
            ViewBag.ChurchID = new SelectList(db.Expenses.ToList().Select(m => new SelectListItem
            {
                Text = m.Church.ChurchName,
                Value = (m.ChurchID).ToString()
            }), "Value", "Text");
            ViewBag.MemberID = new SelectList(db.Expenses.ToList().Select(m => new SelectListItem
            {
                Text = m.Church.ChurchName,
                Value = (m.ChurchID).ToString()
            }), "Value", "Text");
            int? MemberID = ChurchID;
            // We create a request base of the data recieved from the browser
            // START
            /*if (MemberID == null && (Date1 != null || Date2 != null))
            {
                ViewBag.RequestError = "No filter was applied.Please make sure you have selected date correctly";
                allExpenses = db.Expenses.Include(t => t.Church).OrderByDescending(m => m.Date).ToList();
            }*/

            if (MemberID == null && Date1 == null && Date2 == null)
            {
                allExpenses = db.Expenses.Include(t => t.Church).OrderByDescending(m => m.Date).ToList();
            }
            else if (MemberID != null && Date1 == null && Date2 == null)
            {
                allExpenses = db.Expenses.Include(t => t.Church).Where(m => m.Church.ChurchID == MemberID).OrderByDescending(m => m.Date).ToList();
            }
            else if (MemberID != null && Date1 == null || Date2 == null)
            {
                ViewBag.RequestError = "No filter was applied.Please make sure you have selected date correctly";
                allExpenses = db.Expenses.Include(t => t.Church).OrderByDescending(m => m.Date).ToList();

            }

            else if (MemberID != null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    allExpenses = db.Expenses.Include(t => t.Church).Where(m => m.Church.ChurchID == MemberID && (m.Date >= Date1 && m.Date <= Date2)).OrderByDescending(m => m.Date).ToList();

                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date. Please Correct";
                    allExpenses = db.Expenses.Include(t => t.Church).OrderByDescending(m => m.Date).ToList();
                }

            }

            else if (MemberID == null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    allExpenses = db.Expenses.Include(t => t.Church).Where(m => m.Date >= Date1 && m.Date <= Date2).OrderByDescending(m => m.Date).ToList();
                    if (allExpenses == null)
                    {
                        ViewBag.RequestError = "There is no result for your inquiery. Please try different combination";
                        allExpenses = db.Expenses.Include(t => t.Church).OrderByDescending(m => m.Date).ToList();
                    }
                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date.Please Correct";
                    allExpenses = db.Expenses.Include(t => t.Church).OrderByDescending(m => m.Date).ToList();
                }

            }

            ViewBag.Total = allExpenses.Sum(m => m.Amount);
            return View(allExpenses.ToPagedList(page ?? 1, 6));
            // ENDS

            /*var expenses = db.Expenses.Include(e => e.CategoryExpens).Include(e => e.Church);
            return View(expenses.ToList());*/
        }

        // GET: Expens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // GET: Expens/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.CategoryExpenses, "CategoryID", "Name");
            ViewBag.ChurchID = new SelectList(db.Expenses.ToList().Select(m => new SelectListItem
            {
                Text = m.Church.ChurchName,
                Value = (m.ChurchID).ToString()
            }), "Value", "Text");
            return View();
        }

        // POST: Expens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseID,ExpenseName,ChurchID,CategoryID,Date,Amount,Description")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.CategoryExpenses, "CategoryID", "Name", expens.CategoryID);
            //ViewBag.ChurchID = new SelectList(db.Churches, "ChurchID", "ChurchName", expens.ChurchID);

            ViewBag.ChurchID = new SelectList(db.Expenses.ToList().Select(m => new SelectListItem
            {
                Text = m.Church.ChurchName,
                Value = (m.ChurchID).ToString()
            }), "Value", "Text", expens.ChurchID);
            return View(expens);
        }

        // GET: Expens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.CategoryExpenses, "CategoryID", "Name", expens.CategoryID);
            ViewBag.ChurchID = new SelectList(db.Expenses.ToList().Select(m => new SelectListItem
            {
                Text = m.Church.ChurchName,
                Value = (m.ChurchID).ToString()
            }), "Value", "Text", expens.ChurchID);
            return View(expens);
            return View(expens);
        }

        // POST: Expens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseID,ExpenseName,ChurchID,CategoryID,Date,Amount,Description")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.CategoryExpenses, "CategoryID", "Name", expens.CategoryID);
            ViewBag.ChurchID = new SelectList(db.Expenses.ToList().Select(m => new SelectListItem
            {
                Text = m.Church.ChurchName,
                Value = (m.ChurchID).ToString()
            }), "Value", "Text", expens.ChurchID);
   
            return View(expens);
        }

        // GET: Expens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expens expens = db.Expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // POST: Expens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expens expens = db.Expenses.Find(id);
            db.Expenses.Remove(expens);
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
