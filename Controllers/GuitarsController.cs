using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuitarBazar.Models;

namespace GuitarBazar.Controllers
{
    public class GuitarsController : Controller
    {
        Models.GuitarBazarDBEntities DB = new Models.GuitarBazarDBEntities();
        // GET: Guitars
        public ActionResult Index()
        {
            return View(DB.Guitars.OrderByDescending(g => g.AddDate));
        }

        public PartialViewResult GuitarForm(Guitar guitar)
        {
            return PartialView(guitar);
        }

        public ActionResult Details(int id)
        {
            Guitar guitar = DB.Guitars.Find(id);
            if (guitar != null)
            {
                return View(guitar);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            ViewBag.Conditions = SelectListItemConverter<Condition>.Convert(DB.Conditions.ToList());
            ViewBag.GuitarTypes = SelectListItemConverter<GuitarType>.Convert(DB.GuitarTypes.ToList());
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList(), false);
            return View(new Guitar());
        }
        [HttpPost]
        public ActionResult Create(Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                DB.Guitars.Add(guitar);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Conditions = SelectListItemConverter<Condition>.Convert(DB.Conditions.ToList());
            ViewBag.GuitarTypes = SelectListItemConverter<GuitarType>.Convert(DB.GuitarTypes.ToList());
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList(), false);
            return View(new Guitar());
        }

        public ActionResult Edit(int id)
        {
            Guitar guitar = DB.Guitars.Find(id);
            if (guitar != null)
            {
                ViewBag.Conditions = SelectListItemConverter<Condition>.Convert(DB.Conditions.ToList());
                ViewBag.GuitarTypes = SelectListItemConverter<GuitarType>.Convert(DB.GuitarTypes.ToList());
                ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList());
                return View(guitar);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(guitar).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Conditions = SelectListItemConverter<Condition>.Convert(DB.Conditions.ToList());
            ViewBag.GuitarTypes = SelectListItemConverter<GuitarType>.Convert(DB.GuitarTypes.ToList());
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList());
            return View(guitar);
        }
        public ActionResult Delete(int id)
        {
            Guitar guitar = DB.Guitars.Find(id);
            DB.Guitars.Remove(guitar);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}