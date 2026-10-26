using Proyecto_1_Ericka_Morales_Q.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Proyecto_1_Ericka_Morales_Q.Conexion_DB
{
    public class registro_reporte
    {

        public Reportes G_Datos(int idAdmin) 
        { 
            Reportes reportes = new Reportes();

            using (SqlConnection conection = conexion.DBconexion()) 
            {
                string consulta_sql = @"SELECT
                COUNT (T.Id) AS T_Tickets,
                SUM(CASE WHEN E.Nombre = 'Abierto' THEN 1 ELSE 0 END) AS T_abiertos,
                SUM(CASE WHEN E.Nombre = 'En Proceso' THEN 1 ELSE 0 END) AS T_enproceso,
                SUM(CASE WHEN E.Nombre = 'Resuelto' THEN 1 ELSE 0 END) AS T_resueltos,
                SUM(CASE WHEN E.Nombre = 'Cerrado' THEN 1 ELSE 0 END) AS T_cerrados
                FROM Tickets T
                INNER JOIN EstadosTicket E ON T.IdEstado = E.Id;
                SELECT COUNT(*) AS T_Usuarios
                FROM Usuarios;";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {
                    
                    conection.Open();

                    using (SqlDataReader lectura = ejecucion.ExecuteReader())
                    {
                        if (lectura.Read())
                        {
                            reportes.T_Tickets = Convert.ToInt32(lectura["T_Tickets"]);
                            reportes.T_abiertos = Convert.ToInt32(lectura["T_abiertos"]);
                            reportes.T_enproceso = Convert.ToInt32(lectura["T_enproceso"]);
                            reportes.T_resueltos = Convert.ToInt32(lectura["T_resueltos"]);
                            reportes.T_cerrados = Convert.ToInt32(lectura["T_cerrados"]);
                        }
                        lectura.NextResult();
                        if (lectura.Read())
                        {
                            reportes.T_Usuarios = Convert.ToInt32(lectura["T_Usuarios"]);

                        }


                    }


                }

            }

            reportes.IdAdministrador = idAdmin;
            reportes.F_generacion = DateTime.Now;
            return reportes;
        
        }

        public bool registro_R(Reportes reportes)
        {
            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"INSERT INTO Reportes
                (
                    IdUsuarioAdministrador,
                    FechaGeneracion,
                    TotalTickets,
                    TicketsAbiertos,
                    TicketsEnProceso,
                    TicketsResueltos,
                    TicketsCerrados,
                    TotalUsuarios
                )
                VALUES
                (
                    @IdAdministrador,
                    @FechaGeneracion,
                    @T_Tickets,
                    @T_abiertos, 
                    @T_enproceso, 
                    @T_resueltos, 
                    @T_cerrados, 
                    @T_Usuarios 
                )";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {

                    ejecucion.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = reportes.IdAdministrador;
                    ejecucion.Parameters.Add("@FechaGeneracion", SqlDbType.DateTime).Value = reportes.F_generacion;
                    ejecucion.Parameters.Add("@T_Tickets", SqlDbType.Int).Value = reportes.T_Tickets;
                    ejecucion.Parameters.Add("@T_abiertos", SqlDbType.Int).Value = reportes.T_abiertos;
                    ejecucion.Parameters.Add("@T_enproceso", SqlDbType.Int).Value = reportes.T_enproceso;
                    ejecucion.Parameters.Add("@T_resueltos", SqlDbType.Int).Value = reportes.T_resueltos;
                    ejecucion.Parameters.Add("@T_cerrados", SqlDbType.Int).Value = reportes.T_cerrados;
                    ejecucion.Parameters.Add("@T_Usuarios", SqlDbType.Int).Value = reportes.T_Usuarios;


                    conection.Open();
                    return ejecucion.ExecuteNonQuery() > 0;

                }


            }


        }



    }
}