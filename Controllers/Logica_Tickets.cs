using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1_Ericka_Morales_Q.Conexion_DB;

namespace Proyecto_1_Ericka_Morales_Q.Controllers
{
    public class Logica_Tickets : Controller
    {

        private registro_tickets info_T = new registro_tickets();
        private registro_coms info_C = new registro_coms(); 
        // GET: Logica_Tickets
        public ActionResult Index()
        {
            var ticket = info_T.R_tickets();
            return View(ticket);
        }

        public ActionResult comentarios_mostrar(int IdTicket) {
            var coms_1 = info_C.r_comentarios(IdTicket);
            return Json(coms_1, JsonRequestBehavior.AllowGet);


        }
    }
}