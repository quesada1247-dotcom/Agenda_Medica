using MySql.Data.MySqlClient;
using Modelos.Modelos;
using AccesoDatos.Conexion;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Datos
{
    public class HistorialMedicoDAL
    {
        private readonly ConexionBD conexionBD =
            new ConexionBD();

        // CONSULTAR TODOS LOS REGISTROS.
        public List<HistorialMedico> ObtenerTodos()
        {
            List<HistorialMedico> lista =
                new List<HistorialMedico>();

            string consulta = @"
                SELECT
                    h.id_historial,
                    h.id_paciente,
                    h.id_doctor,
                    h.id_cita,
                    h.fecha_registro,
                    h.diagnostico,
                    h.tratamiento,
                    h.observaciones,
                    CONCAT(p.nombre, ' ', p.apellidos) AS nombre_paciente,
                    CONCAT(d.nombre, ' ', d.apellidos) AS nombre_doctor
                FROM HistorialMedico h
                INNER JOIN Pacientes p
                    ON h.id_paciente = p.id_paciente
                INNER JOIN Doctores d
                    ON h.id_doctor = d.id_doctor
                ORDER BY h.fecha_registro DESC";

            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector =
                        comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            HistorialMedico historial =
                                new HistorialMedico();

                            historial.IdHistorial =
                                Convert.ToInt32(
                                    lector["id_historial"]);

                            historial.IdPaciente =
                                Convert.ToInt32(
                                    lector["id_paciente"]);

                            historial.IdDoctor =
                                Convert.ToInt32(
                                    lector["id_doctor"]);

                            historial.IdCita =
                                lector["id_cita"] == DBNull.Value
                                ? (int?)null
                                : Convert.ToInt32(
                                    lector["id_cita"]);

                            historial.FechaRegistro =
                                Convert.ToDateTime(
                                    lector["fecha_registro"]);

                            historial.Diagnostico =
                                lector["diagnostico"].ToString();

                            historial.Tratamiento =
                                lector["tratamiento"] == DBNull.Value
                                ? ""
                                : lector["tratamiento"].ToString();

                            historial.Observaciones =
                                lector["observaciones"] == DBNull.Value
                                ? ""
                                : lector["observaciones"].ToString();

                            historial.NombrePaciente =
                                lector["nombre_paciente"].ToString();

                            historial.NombreDoctor =
                                lector["nombre_doctor"].ToString();

                            lista.Add(historial);
                        }
                    }
                }
            }

            return lista;
        }

        // REGISTRAR UN HISTORIAL MÉDICO.
        public bool Insertar(HistorialMedico historial)
        {
            string consulta = @"
                INSERT INTO HistorialMedico
                (
                    id_paciente,
                    id_doctor,
                    id_cita,
                    diagnostico,
                    tratamiento,
                    observaciones
                )
                VALUES
                (
                    @idPaciente,
                    @idDoctor,
                    @idCita,
                    @diagnostico,
                    @tratamiento,
                    @observaciones
                )";

            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idPaciente", historial.IdPaciente);

                    comando.Parameters.AddWithValue(
                        "@idDoctor", historial.IdDoctor);

                    comando.Parameters.AddWithValue(
                        "@idCita",
                        historial.IdCita.HasValue
                        ? (object)historial.IdCita.Value
                        : DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@diagnostico", historial.Diagnostico);

                    comando.Parameters.AddWithValue(
                        "@tratamiento",
                        string.IsNullOrWhiteSpace(
                            historial.Tratamiento)
                        ? (object)DBNull.Value
                        : historial.Tratamiento);

                    comando.Parameters.AddWithValue(
                        "@observaciones",
                        string.IsNullOrWhiteSpace(
                            historial.Observaciones)
                        ? (object)DBNull.Value
                        : historial.Observaciones);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // MODIFICAR UN HISTORIAL MÉDICO.
        public bool Actualizar(HistorialMedico historial)
        {
            string consulta = @"
                UPDATE HistorialMedico
                SET
                    id_paciente = @idPaciente,
                    id_doctor = @idDoctor,
                    id_cita = @idCita,
                    diagnostico = @diagnostico,
                    tratamiento = @tratamiento,
                    observaciones = @observaciones
                WHERE id_historial = @idHistorial";

            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idHistorial", historial.IdHistorial);

                    comando.Parameters.AddWithValue(
                        "@idPaciente", historial.IdPaciente);

                    comando.Parameters.AddWithValue(
                        "@idDoctor", historial.IdDoctor);

                    comando.Parameters.AddWithValue(
                        "@idCita",
                        historial.IdCita.HasValue
                        ? (object)historial.IdCita.Value
                        : DBNull.Value);

                    comando.Parameters.AddWithValue(
                        "@diagnostico", historial.Diagnostico);

                    comando.Parameters.AddWithValue(
                        "@tratamiento",
                        string.IsNullOrWhiteSpace(
                            historial.Tratamiento)
                        ? (object)DBNull.Value
                        : historial.Tratamiento);

                    comando.Parameters.AddWithValue(
                        "@observaciones",
                        string.IsNullOrWhiteSpace(
                            historial.Observaciones)
                        ? (object)DBNull.Value
                        : historial.Observaciones);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // ELIMINAR UN HISTORIAL MÉDICO.
        public bool Eliminar(int idHistorial)
        {
            string consulta = @"
                DELETE FROM HistorialMedico
                WHERE id_historial = @idHistorial";

            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idHistorial", idHistorial);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}