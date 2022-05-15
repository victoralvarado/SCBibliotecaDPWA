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
    public class DetalleSolicitudesController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: DetalleSolicitudes
        public ActionResult Index()
        {
            var detalleSolicitud = db.DetalleSolicitud.Include(d => d.Libro).Include(d => d.Solicitud);
            return View(detalleSolicitud.ToList());
        }

        // GET: DetalleSolicitudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleSolicitud detalleSolicitud = db.DetalleSolicitud.Find(id);
            if (detalleSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(detalleSolicitud);
        }

        // GET: DetalleSolicitudes/Create
        public ActionResult Create()
        {
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo");
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud");
            return View();
        }

        // POST: DetalleSolicitudes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalleSolicitud,Cantidad,IdLibro,IdSolicitud")] DetalleSolicitud detalleSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.DetalleSolicitud.Add(detalleSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", detalleSolicitud.IdLibro);
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud", detalleSolicitud.IdSolicitud);
            return View(detalleSolicitud);
        }

        // GET: DetalleSolicitudes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleSolicitud detalleSolicitud = db.DetalleSolicitud.Find(id);
            if (detalleSolicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", detalleSolicitud.IdLibro);
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud", detalleSolicitud.IdSolicitud);
            return View(detalleSolicitud);
        }

        // POST: DetalleSolicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalleSolicitud,Cantidad,IdLibro,IdSolicitud")] DetalleSolicitud detalleSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", detalleSolicitud.IdLibro);
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "IdSolicitud", detalleSolicitud.IdSolicitud);
            return View(detalleSolicitud);
        }

        // GET: DetalleSolicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleSolicitud detalleSolicitud = db.DetalleSolicitud.Find(id);
            if (detalleSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(detalleSolicitud);
        }

        // POST: DetalleSolicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleSolicitud detalleSolicitud = db.DetalleSolicitud.Find(id);
            db.DetalleSolicitud.Remove(detalleSolicitud);
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
