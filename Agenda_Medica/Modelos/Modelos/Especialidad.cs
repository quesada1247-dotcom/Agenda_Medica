using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos
{
    public class Especialidad
    {
        // Identificador único de la especialidad.
        public int IdEspecialidad { get; set; }

        // Nombre de la especialidad médica.
        public string Nombre { get; set; }

        // Descripción de la especialidad.
        public string Descripcion { get; set; }

        // Estado de la especialidad (activa o inactiva).
        public bool Estado { get; set; }
    }
}
