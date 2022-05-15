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
    public class DetalleComprasController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: DetalleCompras
        public ActionResult Index()
        {
            var detalleCompra = db.DetalleCompra.Include(d => d.Compra).Include(d => d.Libro);
            return View(detalleCompra.ToList());
        }

        // GET: DetalleCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Create
        public ActionResult Create()
        {
            ViewBag.IdCompra = new SelectList(db.Compra, "IdCompra", "Correlativo");
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo");
            return View();
        }

        // POST: DetalleCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalleCompra,Cantidad,Subtotal,IdCompra,IdLibro")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.DetalleCompra.Add(detalleCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCompra = new SelectList(db.Compra, "IdCompra", "Correlativo", detalleCompra.IdCompra);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", detalleCompra.IdLibro);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCompra = new SelectList(db.Compra, "IdCompra", "Correlativo", detalleCompra.IdCompra);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", detalleCompra.IdLibro);
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalleCompra,Cantidad,Subtotal,IdCompra,IdLibro")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCompra = new SelectList(db.Compra, "IdCompra", "Correlativo", detalleCompra.IdCompra);
            ViewBag.IdLibro = new SelectList(db.Libro, "IdLibro", "Titulo", detalleCompra.IdLibro);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            if (detalleCompra == null)
            {
                return HttpNotFound();
            }
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleCompra detalleCompra = db.DetalleCompra.Find(id);
            db.DetalleCompra.Remove(detalleCompra);
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
