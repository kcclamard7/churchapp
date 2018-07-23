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
    public class TotalController : Controller
    {
        private ChurchMgtEntities db = new ChurchMgtEntities();
      
        // GET: Total
        public ActionResult Index()
        {
            ViewBag.TotalTithes = db.Tithes.ToList().Sum(m => m.Amount);
            ViewBag.TotalOffering = db.Offerings.ToList().Sum(m => m.Amount);
            ViewBag.TotalExpenses = db.Expenses.ToList().Sum(m => m.Amount);
            ViewBag.TotalSpecialContrs = db.SpecialContrs.ToList().Sum(m => m.Amount);
            return View("Total");
        }
    }
}