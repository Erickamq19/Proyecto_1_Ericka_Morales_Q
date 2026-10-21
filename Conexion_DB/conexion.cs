using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Conexion_DB
{
    public class conexion
    {
        public static SqlConnection DBconexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["DB_Proyecto"].ConnectionString;
            return new SqlConnection(cadena);
        }
    }
}