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
    public class предметController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: предмет
        public ActionResult Index()
        {
            List<SubjectViewModel> subjectList = db.предмет.Select(x => new SubjectViewModel
            {
                SubjectId = x.SubjectId,
                назва = x.назва
            }).ToList();

            return View(subjectList);
        }

        // GET: предмет/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            предмет предмет = db.предмет.Find(id);
            if (предмет == null)
            {
                return HttpNotFound();
            }
            return View(предмет);
        }

        // GET: предмет/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: предмет/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectId,назва")] предмет предмет)
        {
            if (ModelState.IsValid)
            {
                db.предмет.Add(предмет);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(предмет);
        }

        // GET: предмет/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            предмет предмет = db.предмет.Find(id);
            if (предмет == null)
            {
                return HttpNotFound();
            }
            return View(предмет);
        }

        // POST: предмет/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectId,назва")] предмет предмет)
        {
            if (ModelState.IsValid)
            {
                db.Entry(предмет).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(предмет);
        }

        // GET: предмет/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            предмет предмет = db.предмет.Find(id);
            if (предмет == null)
            {
                return HttpNotFound();
            }
            return View(предмет);
        }

        // POST: предмет/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            предмет предмет = db.предмет.Find(id);
            db.предмет.Remove(предмет);
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

            List<SubjectViewModel> subjectList = db.предмет.Select(x => new SubjectViewModel
            {
                SubjectId = x.SubjectId,
                назва = x.назва
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:A3"].Style.Font.Bold = true;
            ws.Row(6).Style.Font.Bold = true;
            ws.Column(1).Style.Font.Size = 14;
            ws.Column(2).Style.Font.Size = 14;

            ws.Cells[7, 1, 7 + subjectList.Count, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            ws.Cells["A1:B2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);


            ws.Cells[6, 1, 6 + subjectList.Count, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + subjectList.Count, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + subjectList.Count, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            ws.Cells["A1"].Value = "Список";
            ws.Cells["B1"].Value = "Предмети";


            ws.Cells["A2"].Value = "Дата";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} у {0:H:mm tt}", DateTimeOffset.Now);


            ws.Cells["A6"].Value = "Номер предмету";
            ws.Cells["B6"].Value = "Назва предмету";

            int rowStart = 7;
            foreach (var item in subjectList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.SubjectId;
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
