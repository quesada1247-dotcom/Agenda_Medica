using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos
{
    public class Doctor
    {
        // Identificador único del doctor.
        public int IdDoctor { get; set; }

        // Número de identificación del doctor.
        public string Identificacion { get; set; }

        // Identificador de la especialidad a la que pertenece
        public int? IdEspecialidad { get; set; }

        // Nombre del doctor.
        public string Nombre { get; set; }

        // Apellidos del doctor.
        public string Apellidos { get; set; }

        // Nombre de la especialidad del doctor.
        // Se utiliza principalmente para mostrar la información.
        public string Especialidad { get; set; }

        // Número de teléfono del doctor.
        public string Telefono { get; set; }

        // Correo electrónico del doctor.
        public string Correo { get; set; }
    }
}