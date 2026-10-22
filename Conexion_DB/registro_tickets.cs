using Proyecto_1_Ericka_Morales_Q.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;


namespace Proyecto_1_Ericka_Morales_Q.Conexion_DB
{
    public class registro_tickets
    {
        public List<Tickets> R_tickets()
        {
            List<Tickets> tickets = new List<Tickets>();
            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"SELECT Id, Titulo, Descripcion, IdUsuarioSolicitante, IdUsuarioAsignado, IdEstado, FechaCreacion, FechaActualizacion, Categoria
                FROM Tickets
                ORDER BY FechaCreacion DESC";
                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {
                    conection.Open();

                    using (SqlDataReader lectura = ejecucion.ExecuteReader())
                    {
                        while (lectura.Read())
                        {

                            Tickets tickets1 = new Tickets();

                            tickets1.Id = Convert.ToInt32(lectura["Id"]);
                            tickets1.Titulo = lectura["Titulo"].ToString();
                            tickets1.Descripcion = lectura["Descripcion"].ToString();
                            tickets1.Id_Usuario_solicitante = Convert.ToInt32(lectura["IdUsuarioSolicitante"]);
                            tickets1.Id_Usuario_asigando = lectura["IdUsuarioAsignado"] == DBNull.Value ? (int?)null : Convert.ToInt32(lectura["IdUsuarioAsignado"]);
                            tickets1.Id_Estado = Convert.ToInt32(lectura["IdEstado"]);
                            tickets1.F_creacion = Convert.ToDateTime(lectura["FechaCreacion"]);
                            tickets1.F_actu = lectura["FechaActualizacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lectura["FechaActualizacion"]);
                            tickets1.Categoria = lectura["Categoria"].ToString();

                            tickets.Add(tickets1);


                        }


                    }
                }
            }

            return tickets;

        }



        /// Método para registrar los tickets del lado del cliente
        public bool registroTicket(Tickets tickets)
        {
            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"INSERT INTO Tickets 
                (
                    Titulo,
                    Descripcion,
                    IdUsuarioSolicitante,
                    IdUsuarioAsignado,
                    IdEstado,
                    FechaCreacion,
                    Categoria
                )
                VALUES
                (
                    @Titulo,
                    @Descripcion,
                    @IdUsuarioSolicitante,
                    NULL,
                    (SELECT Id FROM EstadosTicket WHERE Nombre = @Estado),
                    GETDATE(),
                    @Categoria
                
                )";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {

                    ejecucion.Parameters.Add("@Titulo", SqlDbType.NVarChar, 150).Value = tickets.Titulo;
                    ejecucion.Parameters.Add("@Descripcion", SqlDbType.NVarChar, -1).Value = tickets.Descripcion;
                    ejecucion.Parameters.Add("@IdUsuarioSolicitante", SqlDbType.Int).Value = tickets.Id_Usuario_solicitante;
                    ejecucion.Parameters.Add("@Estado", SqlDbType.NVarChar, 30).Value = "Abierto";
                    ejecucion.Parameters.Add("@Categoria", SqlDbType.NVarChar, 80).Value = tickets.Categoria;

                    conection.Open();
                    return ejecucion.ExecuteNonQuery() > 0;

                }

            }
        }

        /// Método visualizar los tickets realizados por cada usuario
        public List<Tickets> cons_tikets(int Usuario)
        {
            List<Tickets> cons_lista = new List<Tickets>();

            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"SELECT  
                    Id,
                    Titulo,
                    Descripcion,
                    IdUsuarioSolicitante,
                    IdUsuarioAsignado,
                    IdEstado,
                    FechaCreacion,
                    FechaActualizacion,
                    Categoria
                    FROM Tickets
                    WHERE IdUsuarioSolicitante = @IdUsuario
                    ORDER BY FechaCreacion DESC";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {
                    ejecucion.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Usuario;
                    conection.Open();

                    using (SqlDataReader lectura = ejecucion.ExecuteReader())
                    {
                        while (lectura.Read())
                        {

                            Tickets consulta = new Tickets();

                            consulta.Id = Convert.ToInt32(lectura["Id"]);
                            consulta.Titulo = lectura["Titulo"].ToString();
                            consulta.Descripcion = lectura["Descripcion"].ToString();
                            consulta.Id_Usuario_solicitante = Convert.ToInt32(lectura["IdUsuarioSolicitante"]);
                            consulta.Id_Usuario_asigando = lectura["IdUsuarioAsignado"] == DBNull.Value ? (int?)null : Convert.ToInt32(lectura["IdUsuarioAsignado"]);
                            consulta.Id_Estado = Convert.ToInt32(lectura["IdEstado"]);
                            consulta.F_creacion = Convert.ToDateTime(lectura["FechaCreacion"]);
                            consulta.F_actu = lectura["FechaActualizacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lectura["FechaActualizacion"]);
                            consulta.Categoria = lectura["Categoria"].ToString();


                            cons_lista.Add(consulta);

                        }


                    }


                }



            }

            return cons_lista;

        }


        /// Método visualizar los estados de cada ticket realizados por cada usuario
        public List<Tickets> tickets_estado(int idUsuario) 
        { 
            List<Tickets> listaT_estado = new List<Tickets>();
            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"SELECT  
                    T.Id,
                    T.Titulo,
                    T.Descripcion,
                    T.IdUsuarioSolicitante,
                    T.IdUsuarioAsignado,
                    T.IdEstado,
                    E.Nombre AS NombreEstado,
                    T.FechaCreacion,
                    T.FechaActualizacion,
                    T.Categoria
                    FROM Tickets T
                    INNER JOIN EstadosTicket E
                    ON T.IdEstado = E.Id
                    WHERE T.IdUsuarioSolicitante = @IdUsuario
                    ORDER BY T.FechaCreacion DESC";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {
                    ejecucion.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                    conection.Open();

                    using (SqlDataReader lectura = ejecucion.ExecuteReader())
                    {
                        while (lectura.Read())
                        {

                            Tickets consulta = new Tickets();

                            consulta.Id = Convert.ToInt32(lectura["Id"]);
                            consulta.Titulo = lectura["Titulo"].ToString();
                            consulta.Descripcion = lectura["Descripcion"].ToString();
                            consulta.Id_Usuario_solicitante = Convert.ToInt32(lectura["IdUsuarioSolicitante"]);
                            consulta.Id_Usuario_asigando = lectura["IdUsuarioAsignado"] == DBNull.Value ? (int?)null : Convert.ToInt32(lectura["IdUsuarioAsignado"]);
                            consulta.N_Estado = lectura["NombreEstado"].ToString();
                            consulta.F_creacion = Convert.ToDateTime(lectura["FechaCreacion"]);
                            consulta.F_actu = lectura["FechaActualizacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(lectura["FechaActualizacion"]);
                            consulta.Categoria = lectura["Categoria"].ToString();


                            listaT_estado.Add(consulta);

                        }


                    }


                }
            }

            return listaT_estado;
        }

    }

}