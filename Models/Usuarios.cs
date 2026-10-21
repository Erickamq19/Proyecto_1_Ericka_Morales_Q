using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre_usuario { get; set; }
        public string Email { get; set; }
        public string ContrasenaH { get; set; }
        public string Rol { get; set; }
        public DateTime F_registro { get; set; }
        public bool Estado { get; set; }

    }
}