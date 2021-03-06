using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DekanatWeb.Models;
using OfficeOpenXml;

namespace DekanatWeb.Controllers
{
    public class спеціальністьController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: спеціальність
        public ActionResult Index()
        {
            List<SpecialtyViewModel> specialtyList = db.спеціальність.Select(x => new SpecialtyViewModel
            {
                SpecialtyId = x.SpecialtyId,
                назва = x.назва,
                факультет = x.факультет
                
            }).ToList();

            return View(specialtyList);
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


        public void ExportToExcel()
        {

            List<SpecialtyViewModel> specialtyList = db.спеціальність.Select(x => new SpecialtyViewModel
            {
                SpecialtyId = x.SpecialtyId,
                назва = x.назва,
                факультет = x.факультет
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:A3"].Style.Font.Bold = true;
            ws.Row(6).Style.Font.Bold = true;
            ws.Cells[1, 1, 6 + specialtyList.Count, 3].Style.Font.Size = 14;

            ws.Cells[7, 1, 7 + specialtyList.Count, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            ws.Cells["A1:B2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);

 
            ws.Cells[6, 1, 6 + specialtyList.Count, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + specialtyList.Count, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + specialtyList.Count, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            ws.Cells["A1"].Value = "Список";
            ws.Cells["B1"].Value = "Спеціальності";


            ws.Cells["A2"].Value = "Дата";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} у {0:H:mm tt}", DateTimeOffset.Now);


            ws.Cells["A6"].Value = "Номер спеціальності";
            ws.Cells["B6"].Value = "Назва спеціальності";
            ws.Cells["C6"].Value = "Факультет";

            int rowStart = 7;
            foreach (var item in specialtyList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.SpecialtyId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.назва;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.факультет.назва;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

    }
}
