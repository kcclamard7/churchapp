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
    public class OfferingsController : Controller
    {
        private ChurchMgtEntities db = new ChurchMgtEntities();
        List<Offering> allOfferings;
        // GET: Offerings
        public ActionResult Index(DateTime? Date1, DateTime? Date2, int? MemberID, int? page)
        {
            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text");


            // We create a request base of the data recieved from the browser
            // START

           /* if (MemberID == null && (Date1 != null || Date2 != null))
            {
                ViewBag.RequestError = "No filter was applied.Please make sure you have selected date correctly";
                allOfferings = db.Offerings.Include(t => t.Member).OrderByDescending(m => m.DateOfOffering).ToList();
            }*/
            if (MemberID == null && Date1 == null && Date2 == null)
            {
                allOfferings = db.Offerings.Include(t => t.Member).OrderByDescending(m => m.DateOfOffering).ToList();
            }
            else if (MemberID != null && Date1 == null && Date2 == null)
            {
                allOfferings = db.Offerings.Include(t => t.Member).Where(m => m.Member.MemberID == MemberID).OrderByDescending(m => m.DateOfOffering).ToList();
            }
            else if (MemberID != null && Date1 == null || Date2 == null)
            {
                ViewBag.RequestError = "No filter was applied.Please make sure you have selected date correctly";
                allOfferings = db.Offerings.Include(t => t.Member).OrderByDescending(m => m.DateOfOffering).ToList();

            }

            else if (MemberID != null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    // code here
                    allOfferings = db.Offerings.Include(t => t.Member).Where(m => m.Member.MemberID == MemberID && (m.DateOfOffering >= Date1 && m.DateOfOffering <= Date2)).OrderByDescending(m => m.DateOfOffering).ToList();

                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date. Please Correct";
                    allOfferings = db.Offerings.Include(t => t.Member).OrderByDescending(m => m.DateOfOffering).ToList();
                }

            }

            else if (MemberID == null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    allOfferings = db.Offerings.Include(t => t.Member).Where(m => m.DateOfOffering >= Date1 && m.DateOfOffering <= Date2).OrderByDescending(m => m.DateOfOffering).ToList();
                    if (allOfferings == null)
                    {
                        ViewBag.RequestError = "There is no result for your inquiery. Please try different combination";
                        allOfferings = db.Offerings.Include(t => t.Member).OrderByDescending(m => m.DateOfOffering).ToList();
                    }
                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date.Please Correct";
                    allOfferings = db.Offerings.Include(t => t.Member).OrderByDescending(m => m.DateOfOffering).ToList();
                }

            }

            ViewBag.Total = allOfferings.Sum(m => m.Amount);
            return View(allOfferings.ToPagedList(page ?? 1, 6));
            // ENDS

            /*var offerings = db.Offerings.Include(o => o.Member);
            return View(offerings.ToList());
            */
        }

        // GET: Offerings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offering offering = db.Offerings.Find(id);
            if (offering == null)
            {
                return HttpNotFound();
            }
            return View(offering);
        }

        // GET: Offerings/Create
        public ActionResult Create()
        {
            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text");
            return View();
        }

        // POST: Offerings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OfferingID,DateOfOffering,Amount,MemberID")] Offering offering)
        {
            if (ModelState.IsValid)
            {
                db.Offerings.Add(offering);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", offering.MemberID);
            return View(offering);
        }

        // GET: Offerings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offering offering = db.Offerings.Find(id);
            if (offering == null)
            {
                return HttpNotFound();
            }
            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", offering.MemberID);
            return View(offering);
        }

        // POST: Offerings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OfferingID,DateOfOffering,Amount,MemberID")] Offering offering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offering).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", offering.MemberID);
            return View(offering);
        }

        // GET: Offerings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offering offering = db.Offerings.Find(id);
            if (offering == null)
            {
                return HttpNotFound();
            }
            return View(offering);
        }

        // POST: Offerings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offering offering = db.Offerings.Find(id);
            db.Offerings.Remove(offering);
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
