using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SCBiblioteca.Models;

namespace SCBiblioteca.Controllers
{
    public class EditorialesController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Editoriales
        public ActionResult Index()
        {
            return View(db.Editorial.ToList());
        }

        // GET: Editoriales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Editorial editorial = db.Editorial.Find(id);
            if (editorial == null)
            {
                return HttpNotFound();
            }
            return View(editorial);
        }

        public ActionResult VerReporte()
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/ReporteProveedor.rpt");

            //conexion para el reporte
            var coninfo = new ConnectionInfo { ServerName = "DESKTOP-GN9NFD8", DatabaseName = "SCBiblioteca", IntegratedSecurity = true };
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream =
            reporte.ExportToStream(ExportFormatType.PortableDocFormat);
            return new FileStreamResult(stream, "application/pdf");
        }

        // GET: Editoriales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEditorial,Editorial1,Direccion,Telefono")] Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                db.Editorial.Add(editorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editorial);
        }

        // GET: Editoriales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Editorial editorial = db.Editorial.Find(id);
            if (editorial == null)
            {
                return HttpNotFound();
            }
            return View(editorial);
        }

        // POST: Editoriales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEditorial,Editorial1,Direccion,Telefono")] Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editorial);
        }

        // GET: Editoriales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Editorial editorial = db.Editorial.Find(id);
            if (editorial == null)
            {
                return HttpNotFound();
            }
            return View(editorial);
        }

        // POST: Editoriales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Editorial editorial = db.Editorial.Find(id);
            db.Editorial.Remove(editorial);
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
