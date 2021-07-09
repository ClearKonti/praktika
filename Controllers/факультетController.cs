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
    public class факультетController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: факультет
        public ActionResult Index()
        {
            return View(db.факультет.ToList());
        }

        // GET: факультет/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            факультет факультет = db.факультет.Find(id);
            if (факультет == null)
            {
                return HttpNotFound();
            }
            return View(факультет);
        }

        // GET: факультет/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: факультет/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyID,назва")] факультет факультет)
        {
            if (ModelState.IsValid)
            {
                db.факультет.Add(факультет);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(факультет);
        }

        // GET: факультет/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            факультет факультет = db.факультет.Find(id);
            if (факультет == null)
            {
                return HttpNotFound();
            }
            return View(факультет);
        }

        // POST: факультет/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyID,назва")] факультет факультет)
        {
            if (ModelState.IsValid)
            {
                db.Entry(факультет).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(факультет);
        }

        // GET: факультет/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            факультет факультет = db.факультет.Find(id);
            if (факультет == null)
            {
                return HttpNotFound();
            }
            return View(факультет);
        }

        // POST: факультет/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            факультет факультет = db.факультет.Find(id);
            db.факультет.Remove(факультет);
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
