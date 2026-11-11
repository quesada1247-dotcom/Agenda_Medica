using System;

namespace Modelos.Modelos
{
    public class HorarioMedico
    {
        // Identificador único del horario.
        public int IdHorario { get; set; }

        // Identificador del doctor al que pertenece.
        public int IdDoctor { get; set; }

        // Nombre del doctor para mostrarlo en consultas.
        public string NombreDoctor { get; set; }

        // Día de la semana en que trabaja el doctor.
        public string DiaSemana { get; set; }

        // Hora en que comienza la atención.
        public TimeSpan HoraInicio { get; set; }

        // Hora en que termina la atención.
        public TimeSpan HoraFin { get; set; }

        // Indica si el horario está activo.
        public bool Estado { get; set; }
    }
}