using SCBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace SCBiblioteca.Controllers
{
    public class HomeController : Controller
    {
        private SCBibliotecaEntities db = new SCBibliotecaEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult NotFound()
        {
            Response.Status = "404 Not Found";
            Response.StatusCode = 404;
            return View();
        }
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(string usuario, string password)
        {
            ModelDB m = new ModelDB();
            if (m.Login(usuario, password))
            {
                ViewBag.estado = "";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.estado = "Usuario o Constraseña incorrectos";
            }
            return View();
        }

        [HttpPost]
        public ActionResult CrearCuenta(Usuario u, Cliente c)
        {
            ModelDB m = new ModelDB();
            if (ModelState.IsValid)
            {
                if (m.CrearCuenta(u, c))
                {
                    ViewBag.estadoRegistro = "";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.estadoRegistro = "Ocurrio un error";
                }
            }
            else
            {
                ViewBag.estadoRegistro = "Los campos son requeridos";
            }
            return View();
        }

        public ActionResult ActualizarCuenta(string correo)
        {
            ModelDB m = new ModelDB();
            if (m.MostrarDatosUsuario(correo))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ActualizarCuenta(Usuario u, Cliente c)
        {
            ModelDB m = new ModelDB();
            if (ModelState.IsValid)
            {
                if (m.ActualizarUsuarioCliente(u, c, c.IdCliente))
                {
                    ViewBag.estadoRegistro = "";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.estadoRegistro = "Ocurrio un error";
                }
            }
            else
            {
                ViewBag.estadoRegistro = "Los campos son requeridos";
            }
            return View();
        }

        public ActionResult CrearCuenta()
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
