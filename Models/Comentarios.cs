using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Models
{
    public class Comentarios
    {

        public int Id { get; set; }
        public int Id_Ticket { get; set; }
        public int Id_Usuario { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
       
    }
}