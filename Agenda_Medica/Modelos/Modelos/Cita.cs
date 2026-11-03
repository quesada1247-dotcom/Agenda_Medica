using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos
{
    public class Cita
    {
        public int IdCita { get; set; }

        public int IdPaciente { get; set; }

        public int IdDoctor { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Motivo { get; set; }

        public string Estado { get; set; }
        public string NombrePaciente { get; set; }

        public string NombreDoctor { get; set; }
    }
}
