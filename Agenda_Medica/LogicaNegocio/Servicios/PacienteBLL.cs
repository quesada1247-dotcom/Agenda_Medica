using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Servicios
{
    public class PacienteBLL
    {
        private readonly PacienteDAL pacienteDAL = new PacienteDAL();

        public bool RegistrarPaciente(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente));
            }

            if (string.IsNullOrWhiteSpace(paciente.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(paciente.Apellidos))
            {
                throw new ArgumentException("Los apellidos son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(paciente.Identificacion))
            {
                throw new ArgumentException("La identificación es obligatoria.");
            }

            if (paciente.FechaNacimiento.Date > DateTime.Today)
            {
                throw new ArgumentException(
                    "La fecha de nacimiento no puede ser futura.");
            }

            return pacienteDAL.Insertar(paciente);
        }

        // ESTE ES EL PRIMER CÓDIGO NUEVO
        public List<Paciente> ListarPacientes()
        {
            return pacienteDAL.ObtenerTodos();
        }

        public bool ModificarPaciente(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente));
            }

            if (string.IsNullOrWhiteSpace(paciente.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(paciente.Apellidos))
            {
                throw new ArgumentException("Los apellidos son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(paciente.Identificacion))
            {
                throw new ArgumentException("La identificación es obligatoria.");
            }

            if (paciente.FechaNacimiento.Date > DateTime.Today)
            {
                throw new ArgumentException("La fecha de nacimiento no puede ser futura.");
            }

            return pacienteDAL.Actualizar(paciente);
        }

        public bool EliminarPaciente(int idPaciente)
        {
            if (idPaciente <= 0)
            {
                throw new ArgumentException("El ID del paciente no es válido.");
            }

            return pacienteDAL.Eliminar(idPaciente);
        }

    }


}
