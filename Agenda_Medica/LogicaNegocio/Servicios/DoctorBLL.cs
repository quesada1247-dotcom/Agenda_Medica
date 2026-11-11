using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class DoctorBLL
    {
        private readonly DoctorDAL doctorDAL = new DoctorDAL();

        // REGISTRAR DOCTOR
        public bool RegistrarDoctor(Doctor doctor)
        {
            // Verificar que el objeto doctor no sea nulo.
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor));
            }

            // Validar que el nombre sea obligatorio.
            if (string.IsNullOrWhiteSpace(doctor.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            // Validar que los apellidos sean obligatorios.
            if (string.IsNullOrWhiteSpace(doctor.Apellidos))
            {
                throw new ArgumentException("Los apellidos son obligatorios.");
            }

            // Validar el número de identificación.
            if (string.IsNullOrWhiteSpace(doctor.Identificacion))
            {
                throw new ArgumentException("La identificación es obligatoria.");
            }

            // Validar que se seleccione una especialidad.
            if (!doctor.IdEspecialidad.HasValue || doctor.IdEspecialidad.Value <= 0)
            {
                throw new ArgumentException("Debes seleccionar una especialidad.");
            }

            // Enviar el doctor a la capa de acceso a datos.
            return doctorDAL.Insertar(doctor);
        }

        // CONSULTAR DOCTORES
        public List<Doctor> ListarDoctores()
        {
            return doctorDAL.ObtenerTodos();
        }

        // MODIFICAR DOCTOR
        public bool ModificarDoctor(Doctor doctor)
        {
            // Verificar que el objeto doctor no sea nulo.
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor));
            }

            // Validar que el ID del doctor sea válido.
            if (doctor.IdDoctor <= 0)
            {
                throw new ArgumentException("El ID del doctor no es válido.");
            }

            // Validar que el nombre sea obligatorio.
            if (string.IsNullOrWhiteSpace(doctor.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            // Validar que los apellidos sean obligatorios.
            if (string.IsNullOrWhiteSpace(doctor.Apellidos))
            {
                throw new ArgumentException("Los apellidos son obligatorios.");
            }

            // Validar el número de identificación.
            if (string.IsNullOrWhiteSpace(doctor.Identificacion))
            {
                throw new ArgumentException("La identificación es obligatoria.");
            }

            // Validar que se haya seleccionado una especialidad.
            if (!doctor.IdEspecialidad.HasValue || doctor.IdEspecialidad.Value <= 0)
            {
                throw new ArgumentException("Debes seleccionar una especialidad.");
            }

            // Enviar los datos a la capa de acceso a datos.
            return doctorDAL.Actualizar(doctor);
        }

        public bool EliminarDoctor(int idDoctor)
        {
            if (idDoctor <= 0)
            {
                throw new ArgumentException("El ID del doctor no es válido.");
            }

            return doctorDAL.Eliminar(idDoctor);
        }
    }
}