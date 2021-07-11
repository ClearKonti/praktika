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
    public class студентController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: студент
        public ActionResult Index()
        {
            List<StudentViewModel> studentList = db.студент.Select(x => new StudentViewModel
            {
                StudentId = x.StudentId,
                імя = x.імя,
                прізвище = x.прізвище,
                дата_народження = x.дата_народження,
                телефон = x.телефон,
                GroupId = x.GroupId,
            }).ToList();

            return View(studentList);
        }

        // GET: студент/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            студент студент = db.студент.Find(id);
            if (студент == null)
            {
                return HttpNotFound();
            }
            return View(студент);
        }

        // GET: студент/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.група, "GroupId", "назва");
            return View();
        }

        // POST: студент/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,імя,прізвище,дата_народження,телефон,GroupId")] студент студент)
        {
            if (ModelState.IsValid)
            {
                db.студент.Add(студент);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.група, "GroupId", "назва", студент.GroupId);
            return View(студент);
        }

        // GET: студент/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            студент студент = db.студент.Find(id);
            if (студент == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.група, "GroupId", "назва", студент.GroupId);
            return View(студент);
        }

        // POST: студент/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,імя,прізвище,дата_народження,телефон,GroupId")] студент студент)
        {
            if (ModelState.IsValid)
            {
                db.Entry(студент).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.група, "GroupId", "назва", студент.GroupId);
            return View(студент);
        }

        // GET: студент/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            студент студент = db.студент.Find(id);
            if (студент == null)
            {
                return HttpNotFound();
            }
            return View(студент);
        }

        // POST: студент/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            студент студент = db.студент.Find(id);
            db.студент.Remove(студент);
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

            List<StudentViewModel> studentList = db.студент.Select(x => new StudentViewModel
            {
                StudentId = x.StudentId,
                імя = x.імя,
                прізвище = x.прізвище,
                дата_народження = x.дата_народження,
                телефон = x.телефон,
                GroupId = x.GroupId,
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:A3"].Style.Font.Bold = true;
            ws.Row(6).Style.Font.Bold = true;
            ws.Cells[1, 1, 6 + studentList.Count, 6].Style.Font.Size = 14;

            ws.Cells[7, 1, 7 + studentList.Count, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            ws.Cells["A1:B2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);


            ws.Cells[6, 1, 6 + studentList.Count, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + studentList.Count, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + studentList.Count, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            ws.Cells["A1"].Value = "Список";
            ws.Cells["B1"].Value = "Студенти";


            ws.Cells["A2"].Value = "Дата";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} у {0:H:mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Номер студента";
            ws.Cells["B6"].Value = "Ім'я";
            ws.Cells["C6"].Value = "Прізвище";
            ws.Cells["D6"].Value = "Дата народження";
            ws.Cells["E6"].Value = "Телефон";
            ws.Cells["F6"].Value = "Номер групи";


            int rowStart = 7;
            foreach (var item in studentList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.StudentId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.імя;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.прізвище;
                ws.Cells[string.Format("D{0}", rowStart)].Value = string.Format("{0:yyyy-MM-dd}", item.дата_народження);
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.телефон;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.GroupId;
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
