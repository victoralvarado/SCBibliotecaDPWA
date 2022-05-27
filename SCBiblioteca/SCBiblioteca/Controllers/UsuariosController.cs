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
    public class UsuariosController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Rol);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        public ActionResult VerReporte()
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/ReporteEmpleados.rpt");

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
            Stream stream =
            reporte.ExportToStream(ExportFormatType.PortableDocFormat);
            return new FileStreamResult(stream, "application/pdf");
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Rol1");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Password = Security.Encrypt(usuario.Password);
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Rol1", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Rol1", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            ModelDB m = new ModelDB();
            if (ModelState.IsValid)
            {
                if (m.CompararPassword(usuario.IdUsuario, usuario.Password) == usuario.Password)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    usuario.Password = Security.Encrypt(usuario.Password);
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "Rol1", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
