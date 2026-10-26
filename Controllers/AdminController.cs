using Proyecto_1_Ericka_Morales_Q.Conexion_DB;
using Proyecto_1_Ericka_Morales_Q.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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

            ViewBag.Administradores = info_T.Administradores();
            ViewBag.Estados = info_T.Estados();

            var ticket = info_T.R_tickets();
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Asignacion(int Idticket, int Admin)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.StatusCode = 401;
                return Json(new
                {
                    exito = false,
                    mensaje = "Tu sesión a expirado, por favor iniciar sesión nuevamente."
                });
            }

            if (Session["Rol"] == null || Session["Rol"].ToString() != "Administrador") {
                
                Response.StatusCode = 403;
                
                return Json(new
                {
                    exito = false,
                    mensaje = "No tiene permisos."
                });
            }

            try
            {
                var administradores = info_T.Administradores();
                bool admin_asigando = info_T.Asignacion(Idticket, Admin);
                

              
                if (!admin_asigando)
                {
                    return Json(new
                    {
                        exito = false,
                        mensaje = "No se logró completar la asiganción."
                    });

                }

                
                return Json(new
                {
                    exito = true,
                    mensaje = "La asginación se realizó correctamente",
                    
                });

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    exito = false,
                    mensaje = "Estimando usuario, se presentó un error al asingar el ticket"
                });

            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Estado(int Idticket, int IdEstado)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.StatusCode = 401;
                return Json(new
                {
                    exito = false,
                    mensaje = "Tu sesión a expirado, por favor iniciar sesión nuevamente."
                });
            }

            if (Session["Rol"] == null || Session["Rol"].ToString() != "Administrador")
            {

                Response.StatusCode = 403;

                return Json(new
                {
                    exito = false,
                    mensaje = "No tiene permisos."
                });
            }

            try
            {
                var estados = info_T.Estados();
                bool estado_asigando = estados.Any(e => e.Value == IdEstado.ToString());
              
                if (!estado_asigando)
                {
                    return Json(new
                    {
                        exito = false,
                        mensaje = "No se logró completar la marcación."
                    });

                }

                bool actualizacion = info_T.Estado(Idticket, IdEstado);
                if (!actualizacion)
                {
                    return Json(new
                    {
                        exito = false,
                        mensaje = "No se logró completar la marcación."
                    });

                }


                return Json(new
                {
                    exito = true,
                    mensaje = "La marcación se realizó correctamente",

                });


               

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    exito = false,
                    mensaje = "Estimando usuario, se presentó un error al asingar el estado"
                });

            }


        }

    }
}