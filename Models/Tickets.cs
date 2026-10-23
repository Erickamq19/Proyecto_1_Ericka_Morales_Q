using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Id_Usuario_solicitante { get; set; }
        public string Nombre_Solicitante {  get; set; }
        public int? Id_Usuario_asigando { get; set; }
        public int Id_Estado { get; set; }
        public DateTime F_creacion { get; set; }
        public DateTime? F_actu { get; set; }
        public string Categoria { get; set; }
        public string N_Estado { get; set; }
    }
}