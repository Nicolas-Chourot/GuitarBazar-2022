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
                List<SelectListItem> soldFilterChoicesItems = new List<SelectListItem>();
                soldFilterChoicesItems.Add(new SelectListItem { Value = "0", Text = "Toutes" });
                soldFilterChoicesItems.Add(new SelectListItem { Value = "1", Text = "Invendues" });
                soldFilterChoicesItems.Add(new SelectListItem { Value = "2", Text = "Vendues" });
                Session["SoldFilterList"] = new SelectList(soldFilterChoicesItems, 0);
            }
            if (Session["SellerFilterChoice"] == null)
            {
                Session["SellerFilterChoice"] = 0;
                List<SelectListItem> sellerFilterChoicesItems = new List<SelectListItem>();
                sellerFilterChoicesItems.Add(new SelectListItem { Value = "0", Text = "Tous" });
                foreach (Seller seller in DB.Sellers.OrderBy(s => s.Name))
                {
                    sellerFilterChoicesItems.Add(new SelectListItem { Value = seller.Id.ToString(), Text = seller.Name });
                }
                Session["SellerFilterList"] = new SelectList(sellerFilterChoicesItems, 0);
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
            ViewBag.Sellers = SelectListItemConverter<Seller>.Convert(DB.Sellers.ToList(), false);
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