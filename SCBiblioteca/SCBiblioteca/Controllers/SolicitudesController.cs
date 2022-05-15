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
    public class SolicitudesController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Solicitudes
        public ActionResult Index()
        {
            var solicitud = db.Solicitud.Include(s => s.Cliente).Include(s => s.Libro).Include(s => s.Usuario);
            return View(solicitud.ToList());
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "NombreCompleto");
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreCompleto");
            return View();
        }

        // POST: Solicitudes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSolicitud,FechaSolicitud,CantidadLibros,Activo,IdCliente,IdLibro,IdUsuario")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Solicitud.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "NombreCompleto", solicitud.IdCliente);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", solicitud.IdLibro);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreCompleto", solicitud.IdUsuario);
            return View(solicitud);
        }

        // GET: Solicitudes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "NombreCompleto", solicitud.IdCliente);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", solicitud.IdLibro);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreCompleto", solicitud.IdUsuario);
            return View(solicitud);
        }

        // POST: Solicitudes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolicitud,FechaSolicitud,CantidadLibros,Activo,IdCliente,IdLibro,IdUsuario")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Cliente, "IdCliente", "NombreCompleto", solicitud.IdCliente);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", solicitud.IdLibro);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreCompleto", solicitud.IdUsuario);
            return View(solicitud);
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            db.Solicitud.Remove(solicitud);
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
