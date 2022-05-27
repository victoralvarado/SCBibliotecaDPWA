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
    public class AutoresController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Autores
        public ActionResult Index()
        {
            return View(db.Autor.ToList());
        }

        // GET: Autores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // GET: Autores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAutor,Autor1")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Autor.Add(autor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autor);
        }

        // GET: Autores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAutor,Autor1")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Autor autor = db.Autor.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autor autor = db.Autor.Find(id);
            db.Autor.Remove(autor);
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
