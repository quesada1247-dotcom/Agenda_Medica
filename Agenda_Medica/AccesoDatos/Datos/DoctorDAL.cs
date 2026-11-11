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
            // Consulta SQL para registrar un doctor.
            string consulta = @"INSERT INTO Doctores
        (nombre, apellidos, identificacion, id_especialidad, telefono, correo)
        VALUES
        (@nombre, @apellidos, @identificacion, @idEspecialidad, @telefono, @correo)";

            // Crear la conexión con la base de datos.
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", doctor.Nombre);
                    comando.Parameters.AddWithValue("@apellidos", doctor.Apellidos);
                    comando.Parameters.AddWithValue("@identificacion", doctor.Identificacion);
                    comando.Parameters.AddWithValue("@idEspecialidad",
                        doctor.IdEspecialidad.HasValue
                        ? (object)doctor.IdEspecialidad.Value
                        : DBNull.Value);

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
            // Lista donde se almacenarán los doctores.
            List<Doctor> lista = new List<Doctor>();

            // Consulta los doctores junto con el nombre de su especialidad.
            string consulta = @"SELECT
                        d.id_doctor,
                        d.nombre,
                        d.apellidos,
                        d.identificacion,
                        d.id_especialidad,
                        e.nombre AS especialidad,
                        d.telefono,
                        d.correo
                        FROM Doctores d
                        LEFT JOIN Especialidades e
                        ON d.id_especialidad = e.id_especialidad";

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
                            doctor.Identificacion = lector["identificacion"].ToString();

                            doctor.IdEspecialidad = lector["id_especialidad"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(lector["id_especialidad"]);

                            doctor.Especialidad = lector["especialidad"] == DBNull.Value
                                ? ""
                                : lector["especialidad"].ToString();

                            doctor.Telefono = lector["telefono"] == DBNull.Value
                                ? ""
                                : lector["telefono"].ToString();

                            doctor.Correo = lector["correo"] == DBNull.Value
                                ? ""
                                : lector["correo"].ToString();

                            lista.Add(doctor);
                        }
                    }
                }
            }

            return lista;
        }

       // MODIFICAR DOCTOR
public bool Actualizar(Doctor doctor)
{
    // Consulta SQL para modificar los datos del doctor.
    string consulta = @"UPDATE Doctores SET
                nombre = @nombre,
                apellidos = @apellidos,
                identificacion = @identificacion,
                id_especialidad = @idEspecialidad,
                telefono = @telefono,
                correo = @correo
                WHERE id_doctor = @idDoctor";

    // Crear la conexión con la base de datos.
    using (MySqlConnection conexion = conexionBD.ObtenerConexion())
    {
        conexion.Open();

        using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
        {
            comando.Parameters.AddWithValue("@nombre", doctor.Nombre);
            comando.Parameters.AddWithValue("@apellidos", doctor.Apellidos);
            comando.Parameters.AddWithValue("@identificacion", doctor.Identificacion);

            comando.Parameters.AddWithValue("@idEspecialidad",
                doctor.IdEspecialidad.HasValue
                ? (object)doctor.IdEspecialidad.Value
                : DBNull.Value);

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
        // MÉTODO PARA ELIMINAR UN DOCTOR
        public bool Eliminar(int idDoctor)
        {
            // Consulta SQL para eliminar un doctor según su identificador
            string consulta = "DELETE FROM Doctores WHERE id_doctor = @idDoctor";

            // Obtener la conexión con la base de datos.
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                // Abrir la conexión.
                conexion.Open();

                // Crear el comando para ejecutar la consulta SQL.
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    // Asignar el identificador del doctor al parámetro.
                    comando.Parameters.AddWithValue("@idDoctor", idDoctor);

                    // Ejecutar la consulta y comprobar si se eliminó algún registro.
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }


}
