using MySql.Data.MySqlClient;
using Modelos.Modelos;
using AccesoDatos.Conexion;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Datos
{
    public class EspecialidadDAL
    {
        // Crear un objeto para utilizar la conexión a MySQL.
        private readonly ConexionBD conexionBD = new ConexionBD();

        // Consultar las especialidades que se encuentran activas.
        public List<Especialidad> ObtenerTodas()
        {
            // Lista donde se almacenarán las especialidades.
            List<Especialidad> lista = new List<Especialidad>();

            // Consulta SQL para obtener las especialidades activas.
            string consulta = @"SELECT id_especialidad, nombre, descripcion, estado
                                FROM Especialidades
                                WHERE estado = 1
                                ORDER BY nombre";

            // Obtener la conexión con la base de datos.
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                // Ejecutar la consulta.
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        // Recorrer los resultados.
                        while (lector.Read())
                        {
                            Especialidad especialidad = new Especialidad();

                            especialidad.IdEspecialidad =
                                Convert.ToInt32(lector["id_especialidad"]);

                            especialidad.Nombre =
                                lector["nombre"].ToString();

                            especialidad.Descripcion =
                                lector["descripcion"].ToString();

                            especialidad.Estado =
                                Convert.ToBoolean(lector["estado"]);

                            // Agregar la especialidad a la lista.
                            lista.Add(especialidad);
                        }
                    }
                }
            }

            // Devolver las especialidades encontradas.
            return lista;
        }
    }
}