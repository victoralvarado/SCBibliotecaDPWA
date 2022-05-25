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
    public class SolicitudesController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();

        // GET: Solicitudes
        public ActionResult Index(string parametro)

        {
            var solicitud = db.Solicitud.Include(s => s.Cliente).Include(s => s.Libro).Include(s => s.Usuario);
            System.Web.HttpContext.Current.Session["ParametroReporteC"] = parametro;
            return View(solicitud.ToList());
        }

        public ActionResult MisSolicitudes()
        {
            var solicitud = db.Solicitud.Include(s => s.Cliente).Include(s => s.Libro).Include(s => s.Usuario);
            return View(solicitud.ToList());
        }


        public ActionResult VerComprobanteUsuario(string parametro)
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/ComprobanteUsuario.rpt");
            //ESTABLECIENDO UN PARAMETRO AL REPORTE
            reporte.SetParameterValue("ComprobanteCod", parametro);
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
            Stream stream = reporte.ExportToStream(ExportFormatType.PortableDocFormat);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ConsultarLibros(string es)
        {
            var libro = db.Libro.Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Especialidad);
            ViewBag.ES = es;
            return View(libro.ToList());
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

        public ActionResult Solicitud(int? id)
        {
            ModelDB m = new ModelDB();
            ViewBag.TituloLibro = m.TituloLibro(id);
            System.Web.HttpContext.Current.Session["IdLibro"] = id;
            ViewBag.display = "none";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Solicitud(Solicitud solicitud)
        {
            ViewBag.display = "none";
            int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdLibro"]);
            DateTime thisDay = DateTime.Now;
            ModelDB m = new ModelDB();
            if (ModelState.IsValid)
            {
                if (m.CantidadLibrosU((int)solicitud.IdUsuario) != 3)
                {
                    if (m.StockLibros(solicitud.IdLibro) >= solicitud.CantidadLibros)
                    {
                        if (m.RestarStock(solicitud.CantidadLibros, solicitud.IdLibro))
                        {
                            solicitud.IdLibro = id;
                            solicitud.Activo = 1;
                            solicitud.FechaSolicitud = thisDay;
                            db.Solicitud.Add(solicitud);
                            db.SaveChanges();
                            ViewBag.EstadoSolicitud = "exito";
                            return RedirectToAction("ConsultarLibros", new { es = ViewBag.EstadoSolicitud });

                        }
                        else
                        {
                            return RedirectToAction("ConsultarLibros");
                        }


                    }
                    else
                    {
                        ViewBag.Solicitud = "No hay Suficientes Libros";
                        ViewBag.TituloLibro = m.TituloLibro(id);
                        ViewBag.display = "normal";
                        return View(solicitud);

                    }
                }
                else
                {
                    ViewBag.Solicitud = "Ya exedio el Maximo de Libros prestados";
                    ViewBag.TituloLibro = m.TituloLibro(id);
                    ViewBag.display = "normal";
                    return View(solicitud);
                }

            }
            else
            {
                ViewBag.Solicitud = "Solicitud no procesada, comuniquese con atencion al cliente";
                ViewBag.TituloLibro = m.TituloLibro(id);
                ViewBag.display = "normal";
                return View(solicitud);
            }
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
            string sesion = (string)(System.Web.HttpContext.Current.Session["UserSesion"]);
            ModelDB m = new ModelDB();
            Solicitud solicitud = db.Solicitud.Find(id);
            if (m.SumarStock(solicitud.CantidadLibros, solicitud.IdLibro))
            {
                db.Solicitud.Remove(solicitud);
                db.SaveChanges();

                if (sesion.Equals("Usuario"))
                {
                    return RedirectToAction("MisSolicitudes");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                if (sesion.Equals("Usuario"))
                {
                    return RedirectToAction("MisSolicitudes");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

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
