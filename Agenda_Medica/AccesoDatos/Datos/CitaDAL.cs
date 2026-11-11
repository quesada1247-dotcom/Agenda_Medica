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
    (id_paciente, id_doctor, id_usuario, fecha, hora,
     duracion_minutos, tipo, motivo, estado, observaciones)
    VALUES
    (@idPaciente, @idDoctor, @idUsuario, @fecha, @hora,
     @duracion, @tipo, @motivo, @estado, @observaciones)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idPaciente", cita.IdPaciente);
                    comando.Parameters.AddWithValue("@idDoctor", cita.IdDoctor);

                    comando.Parameters.AddWithValue("@idUsuario",
                        cita.IdUsuario.HasValue ? (object)cita.IdUsuario.Value : DBNull.Value);

                    comando.Parameters.AddWithValue("@fecha", cita.Fecha.Date);
                    comando.Parameters.AddWithValue("@hora", cita.Hora);
                    comando.Parameters.AddWithValue("@duracion", cita.DuracionMinutos);
                    comando.Parameters.AddWithValue("@tipo", cita.Tipo);
                    comando.Parameters.AddWithValue("@motivo", cita.Motivo);
                    comando.Parameters.AddWithValue("@estado", cita.Estado);
                    comando.Parameters.AddWithValue("@observaciones",
                        string.IsNullOrWhiteSpace(cita.Observaciones)
                        ? (object)DBNull.Value
                        : cita.Observaciones);

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
        c.id_usuario,
        p.nombre AS NombrePaciente,
        d.nombre AS NombreDoctor,
        c.fecha,
        c.hora,
        c.hora_fin,
        c.duracion_minutos,
        c.tipo,
        c.motivo,
        c.estado,
        c.observaciones
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

                            cita.IdUsuario = lector["id_usuario"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(lector["id_usuario"]);

                            cita.NombrePaciente = lector["NombrePaciente"].ToString();
                            cita.NombreDoctor = lector["NombreDoctor"].ToString();

                            cita.Fecha = Convert.ToDateTime(lector["fecha"]);
                            cita.Hora = (TimeSpan)lector["hora"];
                            cita.HoraFin = (TimeSpan)lector["hora_fin"];

                            cita.DuracionMinutos = Convert.ToInt32(lector["duracion_minutos"]);
                            cita.Tipo = lector["tipo"].ToString();
                            cita.Motivo = lector["motivo"].ToString();
                            cita.Estado = lector["estado"].ToString();
                            cita.Observaciones = lector["observaciones"] == DBNull.Value
                                ? ""
                                : lector["observaciones"].ToString();

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
        id_usuario = @idUsuario,
        fecha = @fecha,
        hora = @hora,
        duracion_minutos = @duracion,
        tipo = @tipo,
        motivo = @motivo,
        estado = @estado,
        observaciones = @observaciones
        WHERE id_cita = @idCita";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idPaciente", cita.IdPaciente);
                    comando.Parameters.AddWithValue("@idDoctor", cita.IdDoctor);

                    comando.Parameters.AddWithValue("@idUsuario",
                        cita.IdUsuario.HasValue
                        ? (object)cita.IdUsuario.Value
                        : DBNull.Value);

                    comando.Parameters.AddWithValue("@fecha", cita.Fecha.Date);
                    comando.Parameters.AddWithValue("@hora", cita.Hora);
                    comando.Parameters.AddWithValue("@duracion", cita.DuracionMinutos);
                    comando.Parameters.AddWithValue("@tipo", cita.Tipo);
                    comando.Parameters.AddWithValue("@motivo", cita.Motivo);
                    comando.Parameters.AddWithValue("@estado", cita.Estado);

                    comando.Parameters.AddWithValue("@observaciones",
                        string.IsNullOrWhiteSpace(cita.Observaciones)
                        ? (object)DBNull.Value
                        : cita.Observaciones);

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

        // VERIFICAR SI EL DOCTOR TIENE OTRA CITA EN ESE HORARIO
        public bool TieneCruceHorario(
            int idDoctor,
            DateTime fecha,
            TimeSpan horaInicio,
            int duracionMinutos,
            int? idCitaExcluir = null)
        {
            string consulta = @"
        SELECT COUNT(*)
        FROM Citas
        WHERE id_doctor = @idDoctor
        AND fecha = @fecha
        AND estado NOT IN ('Cancelada', 'No asistio')
        AND hora < ADDTIME(@hora, SEC_TO_TIME(@duracion * 60))
        AND hora_fin > @hora";

            if (idCitaExcluir.HasValue)
            {
                consulta += " AND id_cita <> @idCita";
            }

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idDoctor", idDoctor);
                    comando.Parameters.AddWithValue("@fecha", fecha.Date);
                    comando.Parameters.AddWithValue("@hora", horaInicio);
                    comando.Parameters.AddWithValue("@duracion", duracionMinutos);

                    if (idCitaExcluir.HasValue)
                    {
                        comando.Parameters.AddWithValue("@idCita", idCitaExcluir.Value);
                    }

                    int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }
    }
}
