using MySql.Data.MySqlClient;
using Modelos.Modelos;
using AccesoDatos.Conexion;
using System;
using System.Collections.Generic;

namespace AccesoDatos.Datos
{
    public class HorarioMedicoDAL
    {
        // Crear un objeto para utilizar la conexión a MySQL.
        private readonly ConexionBD conexionBD = new ConexionBD();

        // CONSULTAR LOS HORARIOS REGISTRADOS.
        public List<HorarioMedico> ObtenerTodos()
        {
            List<HorarioMedico> lista = new List<HorarioMedico>();

            string consulta = @"
                SELECT h.id_horario, h.id_doctor,
                       d.nombre AS nombre_doctor,
                       h.dia_semana, h.hora_inicio,
                       h.hora_fin, h.estado
                FROM HorariosMedicos h
                INNER JOIN Doctores d
                    ON h.id_doctor = d.id_doctor
                ORDER BY d.nombre, h.dia_semana";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            HorarioMedico horario = new HorarioMedico();

                            horario.IdHorario = Convert.ToInt32(
                                lector["id_horario"]);

                            horario.IdDoctor = Convert.ToInt32(
                                lector["id_doctor"]);

                            horario.NombreDoctor =
                                lector["nombre_doctor"].ToString();

                            horario.DiaSemana =
                                lector["dia_semana"].ToString();

                            horario.HoraInicio =
                                (TimeSpan)lector["hora_inicio"];

                            horario.HoraFin =
                                (TimeSpan)lector["hora_fin"];

                            horario.Estado =
                                Convert.ToBoolean(lector["estado"]);

                            lista.Add(horario);
                        }
                    }
                }
            }

            return lista;
        }

        // REGISTRAR UN NUEVO HORARIO MÉDICO.
        public bool Insertar(HorarioMedico horario)
        {
            string consulta = @"
                INSERT INTO HorariosMedicos
                (id_doctor, dia_semana, hora_inicio, hora_fin, estado)
                VALUES
                (@idDoctor, @diaSemana, @horaInicio, @horaFin, @estado)";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idDoctor", horario.IdDoctor);

                    comando.Parameters.AddWithValue(
                        "@diaSemana", horario.DiaSemana);

                    comando.Parameters.AddWithValue(
                        "@horaInicio", horario.HoraInicio);

                    comando.Parameters.AddWithValue(
                        "@horaFin", horario.HoraFin);

                    comando.Parameters.AddWithValue(
                        "@estado", horario.Estado ? 1 : 0);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // MODIFICAR UN HORARIO EXISTENTE.
        public bool Actualizar(HorarioMedico horario)
        {
            string consulta = @"
                UPDATE HorariosMedicos
                SET id_doctor = @idDoctor,
                    dia_semana = @diaSemana,
                    hora_inicio = @horaInicio,
                    hora_fin = @horaFin,
                    estado = @estado
                WHERE id_horario = @idHorario";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idHorario", horario.IdHorario);

                    comando.Parameters.AddWithValue(
                        "@idDoctor", horario.IdDoctor);

                    comando.Parameters.AddWithValue(
                        "@diaSemana", horario.DiaSemana);

                    comando.Parameters.AddWithValue(
                        "@horaInicio", horario.HoraInicio);

                    comando.Parameters.AddWithValue(
                        "@horaFin", horario.HoraFin);

                    comando.Parameters.AddWithValue(
                        "@estado", horario.Estado ? 1 : 0);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // ELIMINAR UN HORARIO MÉDICO.
        public bool Eliminar(int idHorario)
        {
            string consulta = @"
                DELETE FROM HorariosMedicos
                WHERE id_horario = @idHorario";

            using (MySqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlCommand comando =
                    new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@idHorario", idHorario);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}