using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Models
{
    public class Reportes
    {
        public int Id { get; set; }
        public int IdAdministrador { get; set; }
        public DateTime F_generacion { get; set; }
        public int T_Tickets { get; set; }
        public int T_abiertos { get; set; }
        public int T_enproceso { get; set; }
        public int T_resueltos { get; set; }
        public int T_cerrados { get; set; }
        public int T_Usuarios { get; set; }

    }
}