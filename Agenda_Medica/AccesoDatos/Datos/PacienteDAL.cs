using AccesoDatos.Conexion;
using Modelos.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Datos
{
    public class PacienteDAL
    {
        private ConexionBD conexionBD = new ConexionBD();

        public bool Insertar(Paciente paciente)
        {
            string consulta = @"INSERT INTO Pacientes
                (nombre, apellidos, identificacion, telefono, correo, fecha_nacimiento)
                VALUES
                (@nombre, @apellidos, @identificacion, @telefono, @correo, @fechaNacimiento)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", paciente.Nombre);
                    comando.Parameters.AddWithValue("@apellidos", paciente.Apellidos);
                    comando.Parameters.AddWithValue("@identificacion", paciente.Identificacion);
                    comando.Parameters.AddWithValue("@telefono",
                        string.IsNullOrWhiteSpace(paciente.Telefono)
                        ? (object)DBNull.Value : paciente.Telefono);
                    comando.Parameters.AddWithValue("@correo",
                        string.IsNullOrWhiteSpace(paciente.Correo)
                        ? (object)DBNull.Value : paciente.Correo);
                    comando.Parameters.AddWithValue("@fechaNacimiento",
                        paciente.FechaNacimiento.Date);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Paciente> ObtenerTodos()
        {
            List<Paciente> lista = new List<Paciente>();

            string consulta = @"SELECT id_paciente, nombre, apellidos,
                        identificacion, telefono, correo, fecha_nacimiento
                        FROM Pacientes";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Paciente paciente = new Paciente();

                            paciente.IdPaciente = Convert.ToInt32(lector["id_paciente"]);
                            paciente.Nombre = lector["nombre"].ToString();
                            paciente.Apellidos = lector["apellidos"].ToString();
                            paciente.Identificacion = lector["identificacion"].ToString();

                            paciente.Telefono = lector["telefono"] == DBNull.Value
                                ? "" : lector["telefono"].ToString();

                            paciente.Correo = lector["correo"] == DBNull.Value
                                ? "" : lector["correo"].ToString();

                            paciente.FechaNacimiento = Convert.ToDateTime(lector["fecha_nacimiento"]);

                            lista.Add(paciente);
                        }
                    }
                }
            }

            return lista; 

        }

        public bool Actualizar(Paciente paciente)
        {
            string consulta = @"UPDATE Pacientes SET
                        nombre = @nombre,
                        apellidos = @apellidos,
                        identificacion = @identificacion,
                        telefono = @telefono,
                        correo = @correo,
                        fecha_nacimiento = @fechaNacimiento
                        WHERE id_paciente = @idPaciente";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", paciente.Nombre);
                    comando.Parameters.AddWithValue("@apellidos", paciente.Apellidos);
                    comando.Parameters.AddWithValue("@identificacion", paciente.Identificacion);
                    comando.Parameters.AddWithValue("@telefono",
                        string.IsNullOrWhiteSpace(paciente.Telefono)
                        ? (object)DBNull.Value : paciente.Telefono);
                    comando.Parameters.AddWithValue("@correo",
                        string.IsNullOrWhiteSpace(paciente.Correo)
                        ? (object)DBNull.Value : paciente.Correo);
                    comando.Parameters.AddWithValue("@fechaNacimiento",
                        paciente.FechaNacimiento.Date);
                    comando.Parameters.AddWithValue("@idPaciente", paciente.IdPaciente);

                    return comando.ExecuteNonQuery() > 0;
                }


            }
        }

        public bool Eliminar(int idPaciente)
        {
            string consulta = "DELETE FROM Pacientes WHERE id_paciente = @idPaciente";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idPaciente", idPaciente);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }

}
