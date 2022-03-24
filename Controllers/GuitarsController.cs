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

        private void InitSessionKeys()
        {
            if (Session["SoldFilterChoice"] == null)
            {
                Session["SoldFilterChoice"] = 0;
                Session["SoldFilterList"] = SelectListItemConverter<string>.Convert(new List<string> { "Toutes", "invendues", "Vendues" });
                Session["SellerFilterChoice"] = 0;
            }

            if (HttpRuntime.Cache["SellerFilterList"] == null)
            {
                HttpRuntime.Cache["SellerFilterList"] = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList(), "0", "Tous");
                HttpRuntime.Cache["SellersListUpdated"] = true;
            }
            else
            {
                if (!(bool)HttpRuntime.Cache["SellersListUpdated"])
                {
                    Session["SellerFilterChoice"] = 0;
                    HttpRuntime.Cache["SellerFilterList"] = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList(), "0", "Tous");
                    HttpRuntime.Cache["SellersListUpdated"] = true;
                }
            }
        }

        // GET: Guitars
        public ActionResult Index()
        {
            InitSessionKeys();
            return View(DB.FilteredGuitarList((int)Session["SoldFilterChoice"], (int)Session["SellerFilterChoice"]));
        }

        public ActionResult SetSoldFilterChoice(int id)
        {
            Session["SoldFilterChoice"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult SetSellerFilterChoice(int id)
        {
            Session["SellerFilterChoice"] = id;
            return RedirectToAction("Index");
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
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList());
            return View(new Guitar());
        }

        [HttpPost]
        public ActionResult Create(Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                DB.Add_Guitar(guitar);
                return RedirectToAction("Index");
            }
            ViewBag.Conditions = SelectListItemConverter<Condition>.Convert(DB.Conditions.ToList());
            ViewBag.GuitarTypes = SelectListItemConverter<GuitarType>.Convert(DB.GuitarTypes.ToList());
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList());
            return View(guitar);
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
                DB.Update_Guitar(guitar);
                return RedirectToAction("Index");
            }
            ViewBag.Conditions = SelectListItemConverter<Condition>.Convert(DB.Conditions.ToList());
            ViewBag.GuitarTypes = SelectListItemConverter<GuitarType>.Convert(DB.GuitarTypes.ToList());
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList());
            return View(guitar);
        }
        public ActionResult Delete(int id)
        {
            DB.Remove_Guitar(id);
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