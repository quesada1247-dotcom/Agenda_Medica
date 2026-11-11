using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos
{
    public class Cita
    {
        // Identificador único de cada cita médica.
        public int IdCita { get; set; }

        // Identificador del paciente que tiene la cita.
        public int IdPaciente { get; set; }

        // Identificador del doctor encargado de atender la cita.
        public int IdDoctor { get; set; }

        // Identificador del usuario que registra la cita
        public int? IdUsuario { get; set; }

        // Fecha en la que se realizará la cita médica.
        public DateTime Fecha { get; set; }

        // Hora de inicio de la cita médica.
        public TimeSpan Hora { get; set; }

        // Hora en la que finaliza la cita médica.
        public TimeSpan HoraFin { get; set; }

        // Duración de la cita expresada en minutos.
        // Por defecto, cada cita tiene una duración de 30 minutos.
        public int DuracionMinutos { get; set; } = 30;

        // Tipo de cita médica, por ejemplo:
        // General, control o emergencia.
        public string Tipo { get; set; }

        // Motivo por el cual el paciente solicita la cita.
        public string Motivo { get; set; }

        // Estado actual de la cita:
        public string Estado { get; set; }

        // Información adicional relacionada con la cita médica.
        public string Observaciones { get; set; }

        // Nombre del paciente.
        public string NombrePaciente { get; set; }

        // Nombre del doctor encargado de atender la cita.
        public string NombreDoctor { get; set; }
    }
}
