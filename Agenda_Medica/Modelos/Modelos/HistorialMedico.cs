using System;

namespace Modelos.Modelos
{
    public class HistorialMedico
    {
        public int IdHistorial { get; set; }

        public int IdPaciente { get; set; }

        public int IdDoctor { get; set; }

        public int? IdCita { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Diagnostico { get; set; }

        public string Tratamiento { get; set; }

        public string Observaciones { get; set; }

        // Datos adicionales para mostrar en las consultas.
        public string NombrePaciente { get; set; }

        public string NombreDoctor { get; set; }
    }
}