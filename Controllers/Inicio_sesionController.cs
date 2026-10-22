using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1_Ericka_Morales_Q.Models;
using Proyecto_1_Ericka_Morales_Q.Conexion_DB;

namespace Proyecto_1_Ericka_Morales_Q.Controllers
{
    public class Inicio_sesionController : Controller
    {
        // GET: Inicio_sesion
        private registro_usuario info_u = new registro_usuario();
        private gestion_contrasenas encript = new gestion_contrasenas();

        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inicio( string Email, string Contrasena) 
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Contrasena))
            {
                ViewBag.Error = "Debe ingresar el su correo y contraseña";
                return View();
                
            }

            Usuarios usuario = info_u.buscar_u(Email.Trim());
            if (usuario == null || !encript.V_contrasena(usuario.ContrasenaH, Contrasena)) 
            {
                ViewBag.Error = "Su correo o contraseña son incorrectos";
                return View();

            }

            Session["IdUsuario"] = usuario.Id;
            Session["NombreUsuario"] = usuario.Nombre_usuario;
            Session["Rol"] = usuario.Rol;

            if (usuario.Rol == "Administrador") 
            {

                return RedirectToAction("Index", "Logica_Tickets");
            
            }

            return RedirectToAction("Index", "Logica_Tickets");

        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(string Nombre_usuario, string Email, string Contrasena, string C_contrasena) 
        {
            if (string.IsNullOrWhiteSpace(Nombre_usuario) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Contrasena) || string.IsNullOrWhiteSpace(C_contrasena))
            {
                ViewBag.Error = "Por favor complete toda la información";
                return View();

            }

            if (!string.Equals(Contrasena, C_contrasena, StringComparison.Ordinal))
            {
                ViewBag.Error = "La contraseña es incorrecta";
                return View();

            }

            if (Contrasena.Length < 8)
            {
                ViewBag.Error = "La contraseña debe de tener un minimo de 8 caracteres";
                return View();

            }

            if (info_u.buscar_u(Email.Trim()) != null)
            {
                ViewBag.Error = "Esta cuenta ya se encuentra registrada";
                return View();

            }

            Usuarios usuarios = new Usuarios
            {
                Nombre_usuario = Nombre_usuario.Trim(),
                Email = Email.Trim(),
                ContrasenaH = Contrasena

            };

            try
            {
                bool registrado = info_u.registro_U(usuarios);
                if (registrado)
                {
                    TempData["Mensaje"] = "Su cuenta se ha creado correctamente";
                    return RedirectToAction("Inicio");

                }

                ViewBag.Error = "Se ha presentado un error al intentar crear la cuenta";
                return View();
            }
            catch (System.Data.SqlClient.SqlException) 
            {
                ViewBag.Error = "No fue posible registrar la cuenta";
                return View();


            }

        }

        public ActionResult Cerrar()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Inicio");
        }



        public ActionResult Index()
        {
            return View();
        }
    }
}