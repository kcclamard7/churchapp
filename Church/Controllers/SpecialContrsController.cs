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
    public class SpecialContrsController : Controller
    {
        private ChurchMgtEntities db = new ChurchMgtEntities();
        List<SpecialContr> allSpecContr;
        // GET: SpecialContrs
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
                allSpecContr = db.SpecialContrs.Include(t => t.Member).OrderByDescending(m => m.DateOfSpecContr).ToList();
            }*/
            if (MemberID == null && Date1 == null && Date2 == null)
            {
                allSpecContr = db.SpecialContrs.Include(t => t.Member).OrderByDescending(m => m.DateOfSpecContr).ToList();
            }
            else if (MemberID != null && Date1 == null && Date2 == null)
            {
                allSpecContr = db.SpecialContrs.Include(t => t.Member).Where(m => m.Member.MemberID == MemberID).OrderByDescending(m => m.DateOfSpecContr).ToList();
            }
            else if (MemberID != null && Date1 == null || Date2 == null)
            {
                ViewBag.RequestError = "No filter was applied.Please make sure you have selected date correctly";
                allSpecContr = db.SpecialContrs.Include(t => t.Member).OrderByDescending(m => m.DateOfSpecContr).ToList();

            }

            else if (MemberID != null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    allSpecContr = db.SpecialContrs.Include(t => t.Member).Where(m => m.Member.MemberID == MemberID && (m.DateOfSpecContr >= Date1 && m.DateOfSpecContr <= Date2)).OrderByDescending(m => m.DateOfSpecContr).ToList();

                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date. Please Correct";
                    allSpecContr = db.SpecialContrs.Include(t => t.Member).OrderByDescending(m => m.DateOfSpecContr).ToList();
                }

            }

            else if (MemberID == null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    allSpecContr = db.SpecialContrs.Include(t => t.Member).Where(m => m.DateOfSpecContr >= Date1 && m.DateOfSpecContr <= Date2).OrderByDescending(m => m.DateOfSpecContr).ToList();
                    if (allSpecContr == null)
                    {
                        ViewBag.RequestError = "There is no result for your inquiery. Please try different combination";
                        allSpecContr = db.SpecialContrs.Include(t => t.Member).OrderByDescending(m => m.DateOfSpecContr).ToList();
                    }
                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date.Please Correct";
                    allSpecContr = db.SpecialContrs.Include(t => t.Member).OrderByDescending(m => m.DateOfSpecContr).ToList();
                }

            }

            ViewBag.Total = allSpecContr.Sum(m => m.Amount);
            return View(allSpecContr.ToPagedList(page ?? 1, 6));
            // ENDS


            /*var specialContrs = db.SpecialContrs.Include(s => s.Member);
            return View(specialContrs.ToList());*/
        }

        // GET: SpecialContrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialContr specialContr = db.SpecialContrs.Find(id);
            if (specialContr == null)
            {
                return HttpNotFound();
            }
            return View(specialContr);
        }

        // GET: SpecialContrs/Create
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

        // POST: SpecialContrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecContrID,DateOfSpecContr,Amount,MemberID")] SpecialContr specialContr)
        {
            if (ModelState.IsValid)
            {
                db.SpecialContrs.Add(specialContr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", specialContr.MemberID);
            return View(specialContr);
        }

        // GET: SpecialContrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialContr specialContr = db.SpecialContrs.Find(id);
            if (specialContr == null)
            {
                return HttpNotFound();
            }
            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", specialContr.MemberID);
            return View(specialContr);
        }

        // POST: SpecialContrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecContrID,DateOfSpecContr,Amount,MemberID")] SpecialContr specialContr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialContr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", specialContr.MemberID);
            return View(specialContr);
        }

        // GET: SpecialContrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialContr specialContr = db.SpecialContrs.Find(id);
            if (specialContr == null)
            {
                return HttpNotFound();
            }
            return View(specialContr);
        }

        // POST: SpecialContrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecialContr specialContr = db.SpecialContrs.Find(id);
            db.SpecialContrs.Remove(specialContr);
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
