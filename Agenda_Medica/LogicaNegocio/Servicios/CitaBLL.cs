using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class CitaBLL
    {
        private readonly CitaDAL citaDAL = new CitaDAL();

        // REGISTRAR CITA
        public bool RegistrarCita(Cita cita)
        {
            if (cita == null)
            {
                throw new ArgumentNullException(nameof(cita));
            }

            if (cita.IdPaciente <= 0)
            {
                throw new ArgumentException("Debes seleccionar un paciente.");
            }

            if (cita.IdDoctor <= 0)
            {
                throw new ArgumentException("Debes seleccionar un doctor.");
            }

            if (cita.Fecha.Date < DateTime.Today)
            {
                throw new ArgumentException("La fecha de la cita no puede ser pasada.");
            }

            if (string.IsNullOrWhiteSpace(cita.Motivo))
            {
                throw new ArgumentException("El motivo de la cita es obligatorio.");
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
            if (cita == null)
            {
                throw new ArgumentNullException(nameof(cita));
            }

            if (cita.IdCita <= 0)
            {
                throw new ArgumentException("Selecciona una cita válida.");
            }

            if (cita.IdPaciente <= 0 || cita.IdDoctor <= 0)
            {
                throw new ArgumentException("Debes seleccionar un paciente y un doctor.");
            }

            if (cita.Fecha.Date < DateTime.Today)
            {
                throw new ArgumentException("La fecha no puede ser pasada.");
            }

            if (string.IsNullOrWhiteSpace(cita.Motivo))
            {
                throw new ArgumentException("El motivo es obligatorio.");
            }

            return citaDAL.Actualizar(cita);
        }

        // ELIMINAR CITA
        public bool EliminarCita(int idCita)
        {
            // Validar que el ID sea correcto.
            if (idCita <= 0)
            {
                throw new ArgumentException("Selecciona una cita válida.");
            }

            // Enviar la solicitud a la capa de acceso a datos.
            return citaDAL.Eliminar(idCita);
        }
    }
}