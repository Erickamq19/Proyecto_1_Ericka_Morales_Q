using Proyecto_1_Ericka_Morales_Q.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Conexion_DB
{
    public class registro_coms
    {

        public List<Comentarios> r_comentarios(int IdTicket)
        {
            List<Comentarios> coms = new List<Comentarios>();   

            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"SELECT Id, IdTicket, IdUsuario, Texto, Fecha
                FROM Comentarios
                WHERE IdTicket = @IdTicket
                ORDER BY Fecha ASC";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {
                    
                    ejecucion.Parameters.Add("@IdTicket", SqlDbType.Int).Value = IdTicket;
                    conection.Open();
                   

                    using (SqlDataReader lectura = ejecucion.ExecuteReader())
                    {
                        while (lectura.Read())
                        {

                            Comentarios comentarios = new Comentarios();    

                            comentarios.Id = Convert.ToInt32(lectura["Id"]);
                            comentarios.Id_Ticket = Convert.ToInt32(lectura["IdTicket"]);
                            comentarios.Id_Usuario = Convert.ToInt32(lectura["IdUsuario"]);
                            comentarios.Texto = lectura["Texto"].ToString();
                            comentarios.Fecha = Convert.ToDateTime(lectura["Fecha"]);
                            
                            coms.Add(comentarios);
                        

                        }


                    }
                }
            }

            return coms;


        }

        public bool registro_comentarios(Comentarios comentarios)
        {
            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"INSERT INTO Comentarios 
                (
                    IdTicket,
                    IdUsuario,
                    Texto,
                    Fecha
                    
                )
                VALUES
                (
                    @IdTicket,
                    @IdUsuario,
                    @Texto,
                    GETDATE()
                   
                )";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {

                    ejecucion.Parameters.Add("@IdTicket", SqlDbType.Int).Value = comentarios.Id_Ticket;
                    ejecucion.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = comentarios.Id_Usuario;
                    ejecucion.Parameters.Add("@Texto", SqlDbType.NVarChar, -1).Value = comentarios.Texto;
                    

                    conection.Open();
                    return ejecucion.ExecuteNonQuery() > 0;

                }

            }
        }


    }
}