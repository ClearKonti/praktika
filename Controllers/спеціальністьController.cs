using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DekanatWeb.Models;

namespace DekanatWeb.Controllers
{
    public class спеціальністьController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: спеціальність
        public ActionResult Index()
        {
            var спеціальність = db.спеціальність.Include(с => с.факультет);
            return View(спеціальність.ToList());
        }

        // GET: спеціальність/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            спеціальність спеціальність = db.спеціальність.Find(id);
            if (спеціальність == null)
            {
                return HttpNotFound();
            }
            return View(спеціальність);
        }

        // GET: спеціальність/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(db.факультет, "FacultyID", "назва");
            return View();
        }

        // POST: спеціальність/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecialtyId,назва,FacultyId")] спеціальність спеціальність)
        {
            if (ModelState.IsValid)
            {
                db.спеціальність.Add(спеціальність);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyId = new SelectList(db.факультет, "FacultyID", "назва", спеціальність.FacultyId);
            return View(спеціальність);
        }

        // GET: спеціальність/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            спеціальність спеціальність = db.спеціальність.Find(id);
            if (спеціальність == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyId = new SelectList(db.факультет, "FacultyID", "назва", спеціальність.FacultyId);
            return View(спеціальність);
        }

        // POST: спеціальність/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecialtyId,назва,FacultyId")] спеціальність спеціальність)
        {
            if (ModelState.IsValid)
            {
                db.Entry(спеціальність).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.факультет, "FacultyID", "назва", спеціальність.FacultyId);
            return View(спеціальність);
        }

        // GET: спеціальність/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            спеціальність спеціальність = db.спеціальність.Find(id);
            if (спеціальність == null)
            {
                return HttpNotFound();
            }
            return View(спеціальність);
        }

        // POST: спеціальність/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            спеціальність спеціальність = db.спеціальність.Find(id);
            db.спеціальність.Remove(спеціальність);
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
