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
    public class факультетController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: факультет
        public ActionResult Index()
        {
            List<FacultyViewModel> facultyList = db.факультет.Select(x => new FacultyViewModel
            {
                FacultyID = x.FacultyID,
                назва = x.назва
            }).ToList();

            return View(facultyList);

            //db.факультет.ToList()
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

        public void ExportToExcel()
        {
            List<FacultyViewModel> facultyList = db.факультет.Select(x => new FacultyViewModel
            {
                FacultyID = x.FacultyID,
                назва = x.назва
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:A3"].Style.Font.Bold = true;
            ws.Cells["A6:B6"].Style.Font.Bold = true;
            ws.Row(6).Style.Font.Bold = true;
            ws.Column(1).Style.Font.Size = 14;
            ws.Column(2).Style.Font.Size = 14;

            ws.Cells[7,1,7+facultyList.Count, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            ws.Cells["A1:B3"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);

            ws.Cells[6, 1, 6 + facultyList.Count, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
            ws.Cells[6, 1, 6 + facultyList.Count, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + facultyList.Count, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            
            ws.Cells["A1"].Value = "Список";
            ws.Cells["B1"].Value = "Факультети";

            ws.Cells["A2"].Value = "Звіт";
            ws.Cells["B2"].Value = "Звіт №1";

            ws.Cells["A3"].Value = "Дата";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H:mm tt}", DateTimeOffset.Now);

     
            ws.Cells["A6"].Value = "Номер факультету";
            ws.Cells["B6"].Value = "Назва факультету";

            int rowStart = 7;
            foreach (var item in facultyList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.FacultyID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.назва;
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
