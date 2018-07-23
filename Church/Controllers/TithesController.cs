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
    public class TithesController : Controller
    {
        private ChurchMgtEntities db = new ChurchMgtEntities();
        List<Tithe> allTithes;

        // GET: Tithes
        public ActionResult Index( DateTime? Date1, DateTime? Date2, int? MemberID, int? page)
        {
             // here a viewbag to get the list of member         
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text");

            // We create a request base of the data recieved from the browser
            // START
            if( MemberID == null && Date1 == null && Date2 == null)
            {
                allTithes = db.Tithes.Include(t => t.Member).OrderByDescending(m => m.DateOfTithe).ToList();
            }
           /* else if (MemberID == null && (Date1 != null && Date2 != null))
            {
                ViewBag.RequestError = " 2222 No filter was applied.Please make sure you have selected date correctly";
                allTithes = db.Tithes.Include(t => t.Member).OrderByDescending(m => m.DateOfTithe).ToList();
            }*/
            else if(MemberID != null && Date1 == null && Date2 == null)
            {
                allTithes = db.Tithes.Include(t => t.Member).Where(m => m.Member.MemberID == MemberID).OrderByDescending( m => m.DateOfTithe).ToList();
            }
            else if (MemberID != null && Date1 == null || Date2 == null)
            {
                ViewBag.RequestError = "No filter was applied.Please make sure you have selected date correctly";
                allTithes = db.Tithes.Include(t => t.Member).OrderByDescending(m => m.DateOfTithe).ToList();
                
            }

            else if (MemberID != null && Date1 != null && Date2 != null)
            {
                if(Date1 <= Date2)
                {
                    allTithes = db.Tithes.Include(t => t.Member).Where(m => m.Member.MemberID == MemberID && (m.DateOfTithe >= Date1 && m.DateOfTithe <= Date2)).OrderByDescending(m => m.DateOfTithe).ToList();

                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date. Please Correct";
                    allTithes = db.Tithes.Include(t => t.Member).OrderByDescending(m => m.DateOfTithe).ToList();
                }

            }

            else if (MemberID == null && Date1 != null && Date2 != null)
            {
                if (Date1 <= Date2)
                {
                    allTithes = db.Tithes.Include(t => t.Member).Where(m => m.DateOfTithe >= Date1 && m.DateOfTithe <= Date2).OrderByDescending(m => m.DateOfTithe).ToList();
                    if (allTithes == null)
                    {
                        ViewBag.RequestError = "There is no result for your inquiery. Please try different combination";
                        allTithes = db.Tithes.Include(t => t.Member).OrderByDescending(m => m.DateOfTithe).ToList();
                    }
                }
                else
                {
                    ViewBag.RequestError = "Fist Date must be superior to Second Date.Please Correct";
                    allTithes = db.Tithes.Include(t => t.Member).OrderByDescending(m => m.DateOfTithe).ToList();
                }

            }
            
            ViewBag.Total = allTithes.Sum(m => m.Amount);
            return View(allTithes.ToPagedList(page ?? 1, 6));
            // ENDS
        }

        // GET: Tithes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tithe tithe = db.Tithes.Find(id);
            if (tithe == null)
            {
                return HttpNotFound();
            }
            return View(tithe);
        }

        // GET: Tithes/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            { Text = m.FirstName + " " + m.LastName,
              Value = (m.MemberID).ToString()
            }), "Value", "Text");
            return View();
        }

        // POST: Tithes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TitheID,DateOfTithe,Amount,MemberID")] Tithe tithe)
        {
            if (ModelState.IsValid)
            {
                db.Tithes.Add(tithe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //  ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FirstName", tithe.MemberID);

            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", tithe.MemberID);


            return View(tithe);
        }

        // GET: Tithes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tithe tithe = db.Tithes.Find(id);
            ViewBag.TheDate = String.Format("{0:d}", tithe.DateOfTithe);
            if (tithe == null)
            {
                return HttpNotFound();
            }
            // ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FirstName", tithe.MemberID);

            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", tithe.MemberID);
            return View(tithe);
        }

        // POST: Tithes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TitheID,DateOfTithe,Amount,MemberID")] Tithe tithe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tithe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.MemberID = new SelectList(db.Members, "MemberID", "FirstName", tithe.MemberID);

            ViewBag.MemberID = new SelectList(db.Members.ToList().Select(m => new SelectListItem
            {
                Text = m.FirstName + " " + m.LastName,
                Value = (m.MemberID).ToString()
            }), "Value", "Text", tithe.MemberID);
            return View(tithe);
        }

        // GET: Tithes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tithe tithe = db.Tithes.Find(id);
            if (tithe == null)
            {
                return HttpNotFound();
            }
            return View(tithe);
        }

        // POST: Tithes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tithe tithe = db.Tithes.Find(id);
            db.Tithes.Remove(tithe);
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
