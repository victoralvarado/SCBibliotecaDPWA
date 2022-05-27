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
    public class LibrosController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Libros
        public ActionResult Index()
        {
            var libro = db.Libro.Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Especialidad);
            return View(libro.ToList());
        }

        // GET: Libros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        public ActionResult VerReporte()
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/ReporteLibros.rpt");

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

        // GET: Libros/Create
        public ActionResult Create()
        {
            ViewBag.IdAutor = new SelectList(db.Autor, "IdAutor", "Autor1");
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Categoria1");
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Especialidad1");
            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLibro,Titulo,Stock,Activo,IdAutor,IdCategoria,IdEspecialidad")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Libro.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAutor = new SelectList(db.Autor, "IdAutor", "Autor1", libro.IdAutor);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Categoria1", libro.IdCategoria);
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Especialidad1", libro.IdEspecialidad);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAutor = new SelectList(db.Autor, "IdAutor", "Autor1", libro.IdAutor);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Categoria1", libro.IdCategoria);
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Especialidad1", libro.IdEspecialidad);
            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLibro,Titulo,Stock,Activo,IdAutor,IdCategoria,IdEspecialidad")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAutor = new SelectList(db.Autor, "IdAutor", "Autor1", libro.IdAutor);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Categoria1", libro.IdCategoria);
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "Especialidad1", libro.IdEspecialidad);
            return View(libro);
        }

        // GET: Libros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libro libro = db.Libro.Find(id);
            db.Libro.Remove(libro);
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
