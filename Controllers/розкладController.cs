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
    public class розкладController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: розклад
        public ActionResult Index()
        {
            List<ScheduleViewModel> curriculumList = db.розклад.Select(x => new ScheduleViewModel
            {
                CurriculumId = x.CurriculumId,
                курс = x.курс,
                семестр = x.семестр,
                лекційні_часи = x.лекційні_часи,
                практичні_часи = x.практичні_часи,
                лабораторні_часи = x.лабораторні_часи,
                форма_захисту = x.форма_захисту,
                курсова_робота = x.курсова_робота,
                курсові_часи = x.курсові_часи,
                SpecialtyId = x.SpecialtyId,
                SubjectId = x.SubjectId,
                предмет = x.предмет, 
                спеціальність = x.спеціальність
            }).ToList();

            return View(curriculumList);
        }

        // GET: розклад/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            розклад розклад = db.розклад.Find(id);
            if (розклад == null)
            {
                return HttpNotFound();
            }
            return View(розклад);
        }

        // GET: розклад/Create
        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.предмет, "SubjectId", "назва");
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва");
            return View();
        }

        // POST: розклад/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurriculumId,курс,семестр,лекційні_часи,практичні_часи,лабораторні_часи,форма_захисту,курсова_робота,курсові_часи,SpecialtyId,SubjectId")] розклад розклад)
        {
            if (ModelState.IsValid)
            {
                db.розклад.Add(розклад);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.предмет, "SubjectId", "назва", розклад.SubjectId);
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва", розклад.SpecialtyId);
            return View(розклад);
        }

        // GET: розклад/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            розклад розклад = db.розклад.Find(id);
            if (розклад == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.предмет, "SubjectId", "назва", розклад.SubjectId);
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва", розклад.SpecialtyId);
            return View(розклад);
        }

        // POST: розклад/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurriculumId,курс,семестр,лекційні_часи,практичні_часи,лабораторні_часи,форма_захисту,курсова_робота,курсові_часи,SpecialtyId,SubjectId")] розклад розклад)
        {
            if (ModelState.IsValid)
            {
                db.Entry(розклад).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.предмет, "SubjectId", "назва", розклад.SubjectId);
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва", розклад.SpecialtyId);
            return View(розклад);
        }

        // GET: розклад/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            розклад розклад = db.розклад.Find(id);
            if (розклад == null)
            {
                return HttpNotFound();
            }
            return View(розклад);
        }

        // POST: розклад/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            розклад розклад = db.розклад.Find(id);
            db.розклад.Remove(розклад);
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

            List<ScheduleViewModel> curriculumList = db.розклад.Select(x => new ScheduleViewModel
            {
                CurriculumId = x.CurriculumId,
                курс = x.курс,
                семестр = x.семестр,
                лекційні_часи = x.лекційні_часи,
                практичні_часи = x.практичні_часи,
                лабораторні_часи = x.лабораторні_часи,
                форма_захисту = x.форма_захисту,
                курсова_робота = x.курсова_робота,
                курсові_часи = x.курсові_часи,
                SpecialtyId = x.SpecialtyId,
                SubjectId = x.SubjectId,
                предмет = x.предмет,
                спеціальність = x.спеціальність
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:A3"].Style.Font.Bold = true;
            ws.Row(6).Style.Font.Bold = true;
            ws.Cells[1, 1, 6 + curriculumList.Count, 11].Style.Font.Size = 14;

            ws.Cells[1, 1, 6 + curriculumList.Count, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            ws.Cells["A1:B2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);


            ws.Cells[6, 1, 6 + curriculumList.Count, 11].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + curriculumList.Count, 10].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + curriculumList.Count, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            ws.Cells["A1"].Value = "Список";
            ws.Cells["B1"].Value = "Розклади";


            ws.Cells["A2"].Value = "Дата";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} у {0:H:mm tt}", DateTimeOffset.Now);


            ws.Cells["A6"].Value = "Номер розкладу";
            ws.Cells["B6"].Value = "Назва предмету";
            ws.Cells["C6"].Value = "Назва спеціальності";
            ws.Cells["D6"].Value = "Курс";
            ws.Cells["E6"].Value = "Семестр";
            ws.Cells["F6"].Value = "Лекційні години";
            ws.Cells["G6"].Value = "Практичні години";
            ws.Cells["H6"].Value = "Лабораторні години";
            ws.Cells["I6"].Value = "Форма захисту";
            ws.Cells["J6"].Value = "Курсова робота";
            ws.Cells["K6"].Value = "Курсові години";

            int rowStart = 7;
            foreach (var item in curriculumList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.CurriculumId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.предмет.назва;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.спеціальність.назва;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.курс;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.семестр;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.лекційні_часи;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.практичні_часи;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.лабораторні_часи;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.форма_захисту;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.курсова_робота;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.курсові_часи;
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
