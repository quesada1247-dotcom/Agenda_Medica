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
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor));
            }

            if (string.IsNullOrWhiteSpace(doctor.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(doctor.Apellidos))
            {
                throw new ArgumentException("Los apellidos son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(doctor.Especialidad))
            {
                throw new ArgumentException("La especialidad es obligatoria.");
            }

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
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor));
            }

            if (string.IsNullOrWhiteSpace(doctor.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(doctor.Apellidos))
            {
                throw new ArgumentException("Los apellidos son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(doctor.Especialidad))
            {
                throw new ArgumentException("La especialidad es obligatoria.");
            }

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