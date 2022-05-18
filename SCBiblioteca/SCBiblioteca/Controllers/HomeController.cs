using SCBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCBiblioteca.Controllers
{
    public class HomeController : Controller
    {
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


        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(string usuario, string password)
        {
            UsuarioDB bd = new UsuarioDB();
            if (bd.Login(usuario, password))
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
        public ActionResult CrearCuenta(Usuario u, string Telefono)
        {
            UsuarioDB bd = new UsuarioDB();
            if (bd.CrearCuenta(u, Telefono))
            {
                ViewBag.estadoRegistro = "";
                return View();
            }
            else
            {
                ViewBag.estadoRegistro = "Ocurrio un error";
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
