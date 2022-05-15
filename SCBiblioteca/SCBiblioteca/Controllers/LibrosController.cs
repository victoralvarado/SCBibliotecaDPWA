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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
