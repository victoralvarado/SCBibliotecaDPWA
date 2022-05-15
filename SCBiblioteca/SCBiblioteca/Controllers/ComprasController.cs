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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCompra,Correlativo,Cantidad,Subtotal,TotalCompra,FechaCompra,IdLibro,IdEditorial")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Compra.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index");
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
