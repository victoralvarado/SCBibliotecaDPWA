using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCBiblioteca.Models;

namespace SCBiblioteca.Controllers
{
    public class PrestamosController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamo = db.Prestamo.Include(p => p.Solicitud);
            return View(prestamo.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamos/Create
        public ActionResult Create(int? idSolicitud)
        {
            ModelDB m = new ModelDB();
            ViewBag.IdSolicitud = idSolicitud;
            if (m.DatosPrestamo(idSolicitud))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Comprobante()
        {
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prestamo prestamo, Comprobante c)
        {
            ModelDB m = new ModelDB();
            c.Activo = prestamo.Activo;
            c.FechaCreacion = prestamo.FechaPrestamo;
            if (ModelState.IsValid)
            {
                if (m.CrearComprobante(c) && m.SolicitudDes(prestamo.IdSolicitud))
                {
                    db.Prestamo.Add(prestamo);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Solicitudes");
                }

            }

            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud", prestamo.IdSolicitud);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud", prestamo.IdSolicitud);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrestamo,FechaPrestamo,Activo,Cantidad,IdSolicitud")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud", prestamo.IdSolicitud);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
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
