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
    public class групаController : Controller
    {
        private DekanatDBEntities1 db = new DekanatDBEntities1();

        // GET: група
        public ActionResult Index()
        {
            List<GroupViewModel> groupList = db.група.Select(x => new GroupViewModel
            {
                GroupId = x.GroupId,
                назва = x.назва,
                SpecialtyId = x.SpecialtyId,
                спеціальність = x.спеціальність
            }).ToList();

            return View(groupList);
        }

        // GET: група/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            група група = db.група.Find(id);
            if (група == null)
            {
                return HttpNotFound();
            }
            return View(група);
        }

        // GET: група/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва");
            return View();
        }

        // POST: група/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,назва,SpecialtyId")] група група)
        {
            if (ModelState.IsValid)
            {
                db.група.Add(група);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва", група.SpecialtyId);
            return View(група);
        }

        // GET: група/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            група група = db.група.Find(id);
            if (група == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва", група.SpecialtyId);
            return View(група);
        }

        // POST: група/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,назва,SpecialtyId")] група група)
        {
            if (ModelState.IsValid)
            {
                db.Entry(група).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialtyId = new SelectList(db.спеціальність, "SpecialtyId", "назва", група.SpecialtyId);
            return View(група);
        }

        // GET: група/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            група група = db.група.Find(id);
            if (група == null)
            {
                return HttpNotFound();
            }
            return View(група);
        }

        // POST: група/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            група група = db.група.Find(id);
            db.група.Remove(група);
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

            List<GroupViewModel> groupList = db.група.Select(x => new GroupViewModel
            {
                GroupId = x.GroupId,
                назва = x.назва,
                SpecialtyId = x.SpecialtyId,
                спеціальність = x.спеціальність
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1:A3"].Style.Font.Bold = true;
            ws.Row(6).Style.Font.Bold = true;
            ws.Cells[1, 1, 6 + groupList.Count, 3].Style.Font.Size = 14;

            ws.Cells[7, 1, 7 + groupList.Count, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            ws.Cells["A1:B2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Double);


            ws.Cells[6, 1, 6 + groupList.Count, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + groupList.Count, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[6, 1, 6 + groupList.Count, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);

            ws.Cells["A1"].Value = "Список";
            ws.Cells["B1"].Value = "Групи";


            ws.Cells["A2"].Value = "Дата";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} у {0:H:mm tt}", DateTimeOffset.Now);


            ws.Cells["A6"].Value = "Номер групи";
            ws.Cells["B6"].Value = "Назва групи";
            ws.Cells["C6"].Value = "Назва спеціальності";

            int rowStart = 7;
            foreach (var item in groupList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.GroupId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.назва;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.спеціальність.назва;
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
