using MySql.Data.MySqlClient;
using Modelos.Modelos;
using AccesoDatos.Conexion;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Datos
{
    public class EspecialidadDAL
    {
        // Objeto para utilizar la conexión a MySQL.
        private readonly ConexionBD conexionBD = new ConexionBD();

        // CONSULTAR SOLAMENTE LAS ESPECIALIDADES ACTIVAS.
        // Este método se utiliza al registrar doctores.
        public List<Especialidad> ObtenerTodas()
        {
            List<Especialidad> lista = new List<Especialidad>();

            string consulta = @"
                SELECT id_especialidad, nombre, descripcion, estado
                FROM Especialidades
                WHERE estado = 1
                ORDER BY nombre";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Especialidad especialidad = new Especialidad();

                            especialidad.IdEspecialidad =
                                Convert.ToInt32(lector["id_especialidad"]);

                            especialidad.Nombre =
                                lector["nombre"].ToString();

                            especialidad.Descripcion =
                                lector["descripcion"] == DBNull.Value
                                ? ""
                                : lector["descripcion"].ToString();

                            especialidad.Estado =
                                Convert.ToBoolean(lector["estado"]);

                            lista.Add(especialidad);
                        }
                    }
                }
            }

            return lista;
        }

        // CONSULTAR TODAS LAS ESPECIALIDADES,
        // INCLUYENDO LAS ACTIVAS Y DESACTIVADAS.
        public List<Especialidad> ListarTodas()
        {
            List<Especialidad> lista = new List<Especialidad>();

            string consulta = @"
                SELECT id_especialidad, nombre, descripcion, estado
                FROM Especialidades
                ORDER BY nombre";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Especialidad especialidad = new Especialidad();

                            especialidad.IdEspecialidad =
                                Convert.ToInt32(lector["id_especialidad"]);

                            especialidad.Nombre =
                                lector["nombre"].ToString();

                            especialidad.Descripcion =
                                lector["descripcion"] == DBNull.Value
                                ? ""
                                : lector["descripcion"].ToString();

                            especialidad.Estado =
                                Convert.ToBoolean(lector["estado"]);

                            lista.Add(especialidad);
                        }
                    }
                }
            }

            return lista;
        }

        // REGISTRAR UNA NUEVA ESPECIALIDAD.
        public bool Insertar(Especialidad especialidad)
        {
            string consulta = @"
                INSERT INTO Especialidades
                (nombre, descripcion, estado)
                VALUES
                (@nombre, @descripcion, @estado)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@nombre", especialidad.Nombre);

                    comando.Parameters.AddWithValue(
                        "@descripcion",
                        string.IsNullOrWhiteSpace(especialidad.Descripcion)
                        ? (object)DBNull.Value
                        : especialidad.Descripcion);

                    comando.Parameters.AddWithValue(
                        "@estado", especialidad.Estado ? 1 : 0);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // MODIFICAR UNA ESPECIALIDAD.
        public bool Actualizar(Especialidad especialidad)
        {
            string consulta = @"
                UPDATE Especialidades
                SET nombre = @nombre,
                    descripcion = @descripcion
                WHERE id_especialidad = @idEspecialidad";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idEspecialidad", especialidad.IdEspecialidad);

                    comando.Parameters.AddWithValue(
                        "@nombre", especialidad.Nombre);

                    comando.Parameters.AddWithValue(
                        "@descripcion",
                        string.IsNullOrWhiteSpace(especialidad.Descripcion)
                        ? (object)DBNull.Value
                        : especialidad.Descripcion);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // ACTIVAR O DESACTIVAR UNA ESPECIALIDAD.
        public bool CambiarEstado(int idEspecialidad, bool estado)
        {
            string consulta = @"
                UPDATE Especialidades
                SET estado = @estado
                WHERE id_especialidad = @idEspecialidad";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idEspecialidad", idEspecialidad);

                    comando.Parameters.AddWithValue(
                        "@estado", estado ? 1 : 0);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}