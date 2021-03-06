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
    public class ComprasController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Compras
        public ActionResult Index()
        {
            var compra = db.Compra.Include(c => c.Editorial).Include(c => c.Libro);
            return View(compra.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.IdEditorial = new SelectList(db.Editorial, "IdEditorial", "Editorial1");
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo");
            return View();
        }

        public ActionResult VerReporteCompra(string fecha)
        {
            string[] f = fecha.Split('-');
            string month = f[1];
            string year = f[0];
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/ReporteCompraFecha.rpt");
            //ESTABLECIENDO UN PARAMETRO AL REPORTE
            reporte.SetParameterValue("month", month);
            reporte.SetParameterValue("year", year);
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
            Stream stream = reporte.ExportToStream(ExportFormatType.PortableDocFormat);
            return new FileStreamResult(stream, "application/pdf");
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCompra,Correlativo,Cantidad,Subtotal,TotalCompra,FechaCompra,IdLibro,IdEditorial")] Compra compra)
        {
            ModelDB m = new ModelDB();
            if (ModelState.IsValid)
            {

                if (m.SumarStock(compra.Cantidad, compra.IdLibro))
                {
                    db.Compra.Add(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(compra);
                }

            }

            ViewBag.IdEditorial = new SelectList(db.Editorial, "IdEditorial", "Editorial1", compra.IdEditorial);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", compra.IdLibro);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEditorial = new SelectList(db.Editorial, "IdEditorial", "Editorial1", compra.IdEditorial);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", compra.IdLibro);
            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCompra,Correlativo,Cantidad,Subtotal,TotalCompra,FechaCompra,IdLibro,IdEditorial")] Compra compra)
        {
            ModelDB m = new ModelDB();
            if (ModelState.IsValid)
            {
                if (compra.Cantidad <= m.StockLibros(compra.IdLibro))
                {
                    if (m.RestarStock(m.CompraCantidad(compra.IdCompra), compra.IdLibro))
                    {
                        if (m.SumarStock(compra.Cantidad, compra.IdLibro))
                        {
                            db.Entry(compra).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ViewBag.CantidadLibro = "La cantidad de libros debe ser menor";
                }

            }
            ViewBag.IdEditorial = new SelectList(db.Editorial, "IdEditorial", "Editorial1", compra.IdEditorial);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", compra.IdLibro);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compra.Find(id);
            db.Compra.Remove(compra);
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
