using AccesoDatos.Conexion;
using Modelos.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Datos
{
    public class DoctorDAL
    {
        private ConexionBD conexionBD = new ConexionBD();

        // REGISTRAR DOCTOR
        public bool Insertar(Doctor doctor)
        {
            string consulta = @"INSERT INTO Doctores
                (nombre, apellidos, especialidad, telefono, correo)
                VALUES
                (@nombre, @apellidos, @especialidad, @telefono, @correo)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", doctor.Nombre);
                    comando.Parameters.AddWithValue("@apellidos", doctor.Apellidos);
                    comando.Parameters.AddWithValue("@especialidad", doctor.Especialidad);
                    comando.Parameters.AddWithValue("@telefono",
                        string.IsNullOrWhiteSpace(doctor.Telefono)
                        ? (object)DBNull.Value : doctor.Telefono);
                    comando.Parameters.AddWithValue("@correo",
                        string.IsNullOrWhiteSpace(doctor.Correo)
                        ? (object)DBNull.Value : doctor.Correo);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // CONSULTAR DOCTORES
        public List<Doctor> ObtenerTodos()
        {
            List<Doctor> lista = new List<Doctor>();

            string consulta = "SELECT * FROM Doctores";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Doctor doctor = new Doctor();

                            doctor.IdDoctor = Convert.ToInt32(lector["id_doctor"]);
                            doctor.Nombre = lector["nombre"].ToString();
                            doctor.Apellidos = lector["apellidos"].ToString();
                            doctor.Especialidad = lector["especialidad"].ToString();

                            doctor.Telefono = lector["telefono"] == DBNull.Value
                                ? "" : lector["telefono"].ToString();

                            doctor.Correo = lector["correo"] == DBNull.Value
                                ? "" : lector["correo"].ToString();

                            lista.Add(doctor);
                        }
                    }
                }
            }

            return lista;
        }

        public bool Actualizar(Doctor doctor)
        {
            string consulta = @"UPDATE Doctores SET
                        nombre = @nombre,
                        apellidos = @apellidos,
                        especialidad = @especialidad,
                        telefono = @telefono,
                        correo = @correo
                        WHERE id_doctor = @idDoctor";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", doctor.Nombre);
                    comando.Parameters.AddWithValue("@apellidos", doctor.Apellidos);
                    comando.Parameters.AddWithValue("@especialidad", doctor.Especialidad);
                    comando.Parameters.AddWithValue("@telefono",
                        string.IsNullOrWhiteSpace(doctor.Telefono)
                        ? (object)DBNull.Value : doctor.Telefono);
                    comando.Parameters.AddWithValue("@correo",
                        string.IsNullOrWhiteSpace(doctor.Correo)
                        ? (object)DBNull.Value : doctor.Correo);
                    comando.Parameters.AddWithValue("@idDoctor", doctor.IdDoctor);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int idDoctor)
        {
            string consulta = "DELETE FROM Doctores WHERE id_doctor = @idDoctor";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idDoctor", idDoctor);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }


}
