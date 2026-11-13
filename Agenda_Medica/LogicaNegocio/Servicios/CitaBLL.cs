using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class CitaBLL
    {
        private string ObtenerDiaSemana(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Lunes";
                case DayOfWeek.Tuesday:
                    return "Martes";
                case DayOfWeek.Wednesday:
                    return "Miercoles";
                case DayOfWeek.Thursday:
                    return "Jueves";
                case DayOfWeek.Friday:
                    return "Viernes";
                case DayOfWeek.Saturday:
                    return "Sabado";
                case DayOfWeek.Sunday:
                    return "Domingo";
                default:
                    throw new ArgumentException("Día de la semana no válido.");
            }
        }

        private readonly CitaDAL citaDAL = new CitaDAL();

        // VALIDAR LOS DATOS DE LA CITA
        private void ValidarCita(Cita cita)
        {
            if (cita == null)
                throw new ArgumentNullException(nameof(cita));

            if (cita.IdPaciente <= 0)
                throw new ArgumentException("Debes seleccionar un paciente.");

            if (cita.IdDoctor <= 0)
                throw new ArgumentException("Debes seleccionar un doctor.");

            if (cita.Fecha.Date < DateTime.Today)
                throw new ArgumentException("La fecha no puede ser pasada.");

            if (string.IsNullOrWhiteSpace(cita.Motivo))
                throw new ArgumentException("El motivo es obligatorio.");

            if (cita.DuracionMinutos != 30 &&
                cita.DuracionMinutos != 45 &&
                cita.DuracionMinutos != 60)
            {
                throw new ArgumentException(
                    "La duración debe ser de 30, 45 o 60 minutos.");
            }

            if (cita.Tipo != "Presencial" &&
                cita.Tipo != "Virtual" &&
                cita.Tipo != "Urgencia")
            {
                throw new ArgumentException("El tipo de cita no es válido.");
            }

            if (cita.Hora < TimeSpan.Zero ||
                cita.Hora >= TimeSpan.FromHours(24))
            {
                throw new ArgumentException("La hora de la cita no es válida.");
            }
        }

        // REGISTRAR CITA
        public bool RegistrarCita(Cita cita)
        {
            ValidarCita(cita);

            string diaSemana = ObtenerDiaSemana(cita.Fecha);

            // Verificar que la cita esté dentro del horario del doctor.
            if (!citaDAL.EstaDentroDelHorario(
                cita.IdDoctor,
                diaSemana,
                cita.Hora,
                cita.DuracionMinutos))
            {
                throw new ArgumentException(
                    "La cita no está dentro del horario disponible del doctor.");
            }

            // Verificar que el doctor no tenga otra cita en ese horario.
            if (citaDAL.TieneCruceHorario(
                cita.IdDoctor,
                cita.Fecha,
                cita.Hora,
                cita.DuracionMinutos))
            {
                throw new ArgumentException(
                    "El doctor ya tiene otra cita en ese horario.");
            }

            // Verificar que el paciente no tenga otra cita simultánea.
            if (citaDAL.PacienteTieneCruceHorario(
                cita.IdPaciente,
                cita.Fecha,
                cita.Hora,
                cita.DuracionMinutos))
            {
                throw new ArgumentException(
                    "El paciente ya tiene otra cita en ese horario.");
            }

            cita.Estado = "Programada";

            return citaDAL.Insertar(cita);
        }

        // CONSULTAR CITAS
        public List<Cita> ListarCitas()
        {
            return citaDAL.ObtenerTodos();
        }

        // MODIFICAR CITA
        public bool ModificarCita(Cita cita)
        {
            ValidarCita(cita);

            if (cita.IdCita <= 0)
                throw new ArgumentException("Selecciona una cita válida.");

            string[] estadosPermitidos =
            {
        "Programada",
        "Confirmada",
        "Atendida",
        "Cancelada",
        "No asistio",
        "Reprogramada"
    };

            if (Array.IndexOf(estadosPermitidos, cita.Estado) < 0)
                throw new ArgumentException("El estado de la cita no es válido.");

            // Verificar que el doctor no tenga otra cita en ese horario.
            // Se excluye la cita que estamos modificando.
            if (citaDAL.TieneCruceHorario(
                cita.IdDoctor,
                cita.Fecha,
                cita.Hora,
                cita.DuracionMinutos,
                cita.IdCita))
            {
                throw new ArgumentException(
                    "El doctor ya tiene otra cita en ese horario.");
            }

            return citaDAL.Actualizar(cita);
        }

        // ELIMINAR CITA
        public bool EliminarCita(int idCita)
        {
            if (idCita <= 0)
                throw new ArgumentException("Selecciona una cita válida.");

            return citaDAL.Eliminar(idCita);
        }
    }
}