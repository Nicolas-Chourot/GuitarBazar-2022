using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuitarBazar.Models;

namespace GuitarBazar.Controllers
{
    public class SellersController : Controller
    {
        private GuitarBazarDBEntities db = new GuitarBazarDBEntities();

         // GET: Sellers
        public ActionResult Index()
        {
            return View(db.Sellers.ToList());
        }

        // GET: Sellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        public PartialViewResult SellerForm(Seller seller)
        {
            return PartialView(seller);
        }

        // GET: Sellers/Create
        public ActionResult Create()
        {
            return View(new Seller());
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Add_Seller(seller);
                HttpRuntime.Cache["SellersListUpdated"] = false;
                return RedirectToAction("Index");
            }

            return View(seller);
        }

        // GET: Sellers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Update_Seller(seller);
                HttpRuntime.Cache["SellersListUpdated"] = false;
                return RedirectToAction("Index");
            }
            return View(seller);
        }

        public ActionResult Delete(int id)
        {
            db.Remove_Seller(id);
            HttpRuntime.Cache["SellersListUpdated"] = false;
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
