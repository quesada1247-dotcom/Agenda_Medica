using AccesoDatos.Conexion;
using Modelos.Modelos;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Datos
{
    public class CitaDAL
    {
        private ConexionBD conexionBD = new ConexionBD();

        // REGISTRAR CITA
        public bool Insertar(Cita cita)
        {
            string consulta = @"INSERT INTO Citas
    (id_paciente, id_doctor, fecha, hora, motivo, estado)
    VALUES
    (@idPaciente, @idDoctor, @fecha, @hora, @motivo, @estado)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idPaciente", cita.IdPaciente);
                    comando.Parameters.AddWithValue("@idDoctor", cita.IdDoctor);
                    comando.Parameters.AddWithValue("@fecha", cita.Fecha.Date);
                    comando.Parameters.AddWithValue("@hora", cita.Hora);
                    comando.Parameters.AddWithValue("@motivo", cita.Motivo);
                    comando.Parameters.AddWithValue("@estado", cita.Estado);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // CONSULTAR CITAS
        public List<Cita> ObtenerTodos()
        {
            List<Cita> lista = new List<Cita>();

            string consulta = @"SELECT
    c.id_cita,
    c.id_paciente,
    c.id_doctor,
    p.nombre AS NombrePaciente,
    d.nombre AS NombreDoctor,
    c.fecha,
    c.hora,
    c.motivo,
    c.estado
    FROM Citas c
    INNER JOIN Pacientes p ON c.id_paciente = p.id_paciente
    INNER JOIN Doctores d ON c.id_doctor = d.id_doctor";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Cita cita = new Cita();

                            cita.IdCita = Convert.ToInt32(lector["id_cita"]);
                            cita.IdPaciente = Convert.ToInt32(lector["id_paciente"]);
                            cita.IdDoctor = Convert.ToInt32(lector["id_doctor"]);
                            cita.NombrePaciente = lector["NombrePaciente"].ToString();
                            cita.NombreDoctor = lector["NombreDoctor"].ToString();
                            cita.Fecha = Convert.ToDateTime(lector["fecha"]);
                            cita.Hora = (TimeSpan)lector["hora"];
                            cita.Motivo = lector["motivo"].ToString();
                            cita.Estado = lector["estado"].ToString(); 


                            lista.Add(cita);
                        }
                    }
                }
            }

            return lista;
        }

        // MODIFICAR CITA
        public bool Actualizar(Cita cita)
        {
            string consulta = @"UPDATE Citas SET
        id_paciente = @idPaciente,
        id_doctor = @idDoctor,
        fecha = @fecha,
        hora = @hora,
        motivo = @motivo,
        estado = @estado
        WHERE id_cita = @idCita";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idPaciente", cita.IdPaciente);
                    comando.Parameters.AddWithValue("@idDoctor", cita.IdDoctor);
                    comando.Parameters.AddWithValue("@fecha", cita.Fecha.Date);
                    comando.Parameters.AddWithValue("@hora", cita.Hora);
                    comando.Parameters.AddWithValue("@motivo", cita.Motivo);
                    comando.Parameters.AddWithValue("@estado", cita.Estado);
                    comando.Parameters.AddWithValue("@idCita", cita.IdCita);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }


        // MÉTODO PARA ELIMINAR UNA CITA
        public bool Eliminar(int idCita)
        {
            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                string consulta = "DELETE FROM Citas WHERE id_cita = @idCita";

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idCita", idCita);

                    conexion.Open();

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}