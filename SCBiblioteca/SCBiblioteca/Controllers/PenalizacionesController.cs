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
    public class PenalizacionesController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Penalizaciones
        public ActionResult Index()
        {
            var penalizacion = db.Penalizacion.Include(p => p.Devolucion);
            return View(penalizacion.ToList());
        }

        // GET: Penalizaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Penalizacion penalizacion = db.Penalizacion.Find(id);
            if (penalizacion == null)
            {
                return HttpNotFound();
            }
            return View(penalizacion);
        }

        // GET: Penalizaciones/Create
        public ActionResult Create()
        {
            ViewBag.IdDevolucion = new SelectList(db.Devolucion, "IdDevolucion", "Observaciones");
            return View();
        }

        // POST: Penalizaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPenalizacion,MontoPenalizacion,MotivoPenalizacion,IdDevolucion")] Penalizacion penalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Penalizacion.Add(penalizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDevolucion = new SelectList(db.Devolucion, "IdDevolucion", "Observaciones", penalizacion.IdDevolucion);
            return View(penalizacion);
        }

        // GET: Penalizaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Penalizacion penalizacion = db.Penalizacion.Find(id);
            if (penalizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDevolucion = new SelectList(db.Devolucion, "IdDevolucion", "Observaciones", penalizacion.IdDevolucion);
            return View(penalizacion);
        }

        // POST: Penalizaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPenalizacion,MontoPenalizacion,MotivoPenalizacion,IdDevolucion")] Penalizacion penalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penalizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDevolucion = new SelectList(db.Devolucion, "IdDevolucion", "Observaciones", penalizacion.IdDevolucion);
            return View(penalizacion);
        }

        // GET: Penalizaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Penalizacion penalizacion = db.Penalizacion.Find(id);
            if (penalizacion == null)
            {
                return HttpNotFound();
            }
            return View(penalizacion);
        }

        // POST: Penalizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Penalizacion penalizacion = db.Penalizacion.Find(id);
            db.Penalizacion.Remove(penalizacion);
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
