using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1_Ericka_Morales_Q.Conexion_DB;


namespace Proyecto_1_Ericka_Morales_Q.Controllers
{
    public class AdminController : Controller
    {
        private registro_tickets info_T = new registro_tickets();
        public ActionResult Index()
        {
            if (Session["IdUsuario"] == null)
            {
                return RedirectToAction("Inicio", "Inicio_sesion");
            }

            if (Session["Rol"] == null || Session["Rol"].ToString() != "Administrador")
            {
                return RedirectToAction("Index", "Logica_Tickets");
                
            }

            var ticket = info_T.R_tickets();
            return View(ticket);
        }
       
    }
}