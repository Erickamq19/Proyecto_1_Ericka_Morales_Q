using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_1_Ericka_Morales_Q.Conexion_DB;
using Proyecto_1_Ericka_Morales_Q.Models;

namespace Proyecto_1_Ericka_Morales_Q.Controllers
{
    public class Logica_TicketsController : Controller
    {

        private registro_tickets info_T = new registro_tickets();
        private registro_coms info_C = new registro_coms(); 
        // GET: Logica_Tickets
        public ActionResult Index()
        {
            if (Session["IdUsuario"] == null)
            {
                return RedirectToAction("Inicio", "Inicio_sesion");
            }

            int idUsuario = (int)Session["IdUsuario"];
            var ticket = info_T.tickets_estado(idUsuario);
            return View(ticket);
        }

        public ActionResult comentarios_mostrar(int IdTicket) 
        {
            if (Session["IdUsuario"] == null)
            {
                return new HttpStatusCodeResult(401);
            }
            int idUsuario = (int)Session["IdUsuario"];
            var coms_1 = info_T.cons_tikets(idUsuario);

            bool asig_tickets = coms_1.Exists(t => t.Id == IdTicket);
            if (!asig_tickets)
            {
                return new HttpStatusCodeResult(403);


            }


            var coms_2 = info_C.r_comentarios(IdTicket);
            return Json(coms_2, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registrar_ticket(Tickets tickets) 
        {
            if (Session["IdUsuario"] == null)
            {
                return Json(new
                {
                    exito = false,
                    mensaje = "Tu sesión a expirado, por favor iniciar sesión nuevamente."
                });
            }

            if (tickets == null || string.IsNullOrWhiteSpace(tickets.Titulo) || string.IsNullOrWhiteSpace(tickets.Descripcion) || string.IsNullOrWhiteSpace(tickets.Categoria))
            {
                return Json(new
                {
                    exito = false,
                    mensaje = "Por favor, completar todos los campos del formulario."
                });

            }

            if (tickets.Titulo.Trim().Length > 150)
            {
                return Json(new
                {
                    exito = false,
                    mensaje = "El título no puede superar los 150 caracteres."
                });

            }

            string[] categorias = { 
                "Software", "Redes", "Hardware", "Acceso", "Licencias", "Otros"
            
            
            };

            if (!categorias.Contains(tickets.Categoria))
            {
                return Json(new
                {
                    exito = false,
                    mensaje = "Por favor, seleccione una categoría valida."
                });

            }

            try
            {
                int idUsuario = (int)Session["IdUsuario"];
                tickets.Id_Usuario_solicitante = idUsuario;
                tickets.Titulo = tickets.Titulo.Trim();
                tickets.Descripcion = tickets.Descripcion.Trim();

                bool registrado = info_T.registroTicket(tickets);
                if (!registrado)
                {
                    return Json(new
                    {
                        exito = false,
                        mensaje = "No se logró completar el registro."
                    });

                }

                var tickets2 = info_T.tickets_estado(idUsuario);
                var lista_T1 = tickets2.Select(t => new
                {
                    t.Id,
                    t.Titulo,
                    t.Categoria,
                    Fecha = t.F_creacion.ToString("dd/MM/yyyy HH:mm"),
                    Estado = t.N_Estado
                }).ToList();

                return Json(new
                {
                    exito = true,
                    mensaje = "El ticket se ha registrado exitosamente",
                    tickets = lista_T1
                });

            }
            catch (Exception) 
            { 
                Response.StatusCode = 500;
                return Json(new
                {
                    exito = false,
                    mensaje = "Estimando usuario, se presentó un error al registrar el ticket"
                });

            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult registrar_comentario(Comentarios comentarios)
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

            int idUsuario = (int)Session["IdUsuario"];
            if (comentarios == null || comentarios.Id_Ticket <= 0 || string.IsNullOrWhiteSpace(comentarios.Texto))
            {
                return Json(new
                {
                    exito = false,
                    mensaje = "Debe seleccionar un ticket para escribir un comentario."
                });

            }

            if (comentarios.Texto.Trim().Length > 2000)
            {
                return Json(new
                {
                    exito = false,
                    mensaje = "El comentario no puede superar los 2000 caracteres."
                });

            }

            var tickes3 = info_T.cons_tikets(idUsuario);
            bool ticket_usuario = tickes3.Exists(t => t.Id == comentarios.Id_Ticket);

            if (!ticket_usuario)
            {
                Response.StatusCode = 403;
                return Json(new
                {
                    exito = false,
                    mensaje = "No tiene acceso a esta función."
                });


            }
                
            try
            {
                
                comentarios.Id_Usuario = idUsuario;
                comentarios.Texto = comentarios.Texto.Trim();
               

                bool registrado = info_C.registro_comentarios(comentarios);
                if (!registrado)
                {
                    return Json(new
                    {
                        exito = false,
                        mensaje = "No se logró completar el registro."
                    });

                }

                return Json(new
                {
                    exito = true,
                    mensaje = "El comentario se ha registrado exitosamente",
                    
                });

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    exito = false,
                    mensaje = "Estimando usuario, se presentó un error al registrar el comentario"
                });

            }


        }

    }
}