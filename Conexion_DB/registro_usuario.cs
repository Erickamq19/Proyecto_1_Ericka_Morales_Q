using Proyecto_1_Ericka_Morales_Q.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Ericka_Morales_Q.Conexion_DB
{
    public class registro_usuario
    {
        private gestion_contrasenas encript = new gestion_contrasenas();

        /// Método para registrar nuevos usuarios, donde se le asigna el rol de usuario de una ves.
        public bool registro_U (Usuarios usuarios) 
        {
            using (SqlConnection conection = conexion.DBconexion()) 
            {
                string consulta_sql = @"INSERT INTO Usuarios
                (
                    Nombre,
                    Email,
                    ContrasenaHash,
                    Rol,
                    FechaRegistro,
                    Estado
                )
                VALUES
                (
                    @Nombre,
                    @Email,
                    @ContrasenaHash,
                    @Rol,
                    GETDATE(), 
                    1
                )";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {

                    ejecucion.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = usuarios.Nombre_usuario;
                    ejecucion.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = usuarios.Email;
                    ejecucion.Parameters.Add("@ContrasenaHash", SqlDbType.NVarChar, -1).Value = encript.Password(usuarios.ContrasenaH);
                    ejecucion.Parameters.Add("@Rol", SqlDbType.NVarChar, 30).Value = "Usuario";
                    
                    conection.Open();
                    return ejecucion.ExecuteNonQuery() > 0;

                }


            }
        
        
        }

        /// Método para iniciar sesión mediante la búsqueda por correo.
        public Usuarios buscar_u(string email) 
        {
            using (SqlConnection conection = conexion.DBconexion())
            {
                string consulta_sql = @"SELECT 
               
                    Id,
                    Nombre,
                    Email,
                    ContrasenaHash,
                    Rol,
                    FechaRegistro,
                    Estado
                    FROM Usuarios WHERE Email = @Email";

                using (SqlCommand ejecucion = new SqlCommand(consulta_sql, conection))
                {
                    ejecucion.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = email;
                    conection.Open();

                    using (SqlDataReader lectura = ejecucion.ExecuteReader())
                    {
                        while (lectura.Read())
                        {

                            Usuarios usuarios = new Usuarios();

                            usuarios.Id = Convert.ToInt32(lectura["Id"]);
                            usuarios.Nombre_usuario = lectura["Nombre"].ToString();
                            usuarios.Email = lectura["Email"].ToString();
                            usuarios.ContrasenaH = lectura["ContrasenaHash"].ToString();
                            usuarios.Rol = lectura["Rol"].ToString();
                            usuarios.F_registro = Convert.ToDateTime(lectura["FechaRegistro"]);
                            usuarios.Estado = Convert.ToBoolean(lectura["Estado"]);

                            return usuarios;


                        }


                    }

                    
                }


            }

            return null;


        }



    }
}