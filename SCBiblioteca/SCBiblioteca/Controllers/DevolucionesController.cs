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
    public class DevolucionesController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var devolucion = db.Devolucion.Include(d => d.Prestamo);
            return View(devolucion.ToList());
        }

        // GET: Devoluciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // GET: Devoluciones/Create
        public ActionResult Create()
        {
            ViewBag.IdPrestamo = new SelectList(db.Prestamo, "IdPrestamo", "IdPrestamo");
            return View();
        }

        // POST: Devoluciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDevolucion,FechaDevolucion,Observaciones,IdPrestamo")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Devolucion.Add(devolucion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPrestamo = new SelectList(db.Prestamo, "IdPrestamo", "IdPrestamo", devolucion.IdPrestamo);
            return View(devolucion);
        }

        // GET: Devoluciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrestamo = new SelectList(db.Prestamo, "IdPrestamo", "IdPrestamo", devolucion.IdPrestamo);
            return View(devolucion);
        }

        // POST: Devoluciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDevolucion,FechaDevolucion,Observaciones,IdPrestamo")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devolucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPrestamo = new SelectList(db.Prestamo, "IdPrestamo", "IdPrestamo", devolucion.IdPrestamo);
            return View(devolucion);
        }

        // GET: Devoluciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devolucion devolucion = db.Devolucion.Find(id);
            if (devolucion == null)
            {
                return HttpNotFound();
            }
            return View(devolucion);
        }

        // POST: Devoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Devolucion devolucion = db.Devolucion.Find(id);
            db.Devolucion.Remove(devolucion);
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
