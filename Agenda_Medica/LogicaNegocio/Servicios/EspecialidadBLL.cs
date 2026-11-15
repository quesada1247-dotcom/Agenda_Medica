using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class EspecialidadBLL
    {
        // Objeto para utilizar los métodos de acceso a datos.
        private readonly EspecialidadDAL especialidadDAL =
            new EspecialidadDAL();

        // VALIDAR LOS DATOS DE UNA ESPECIALIDAD.
        private void ValidarEspecialidad(Especialidad especialidad)
        {
            if (especialidad == null)
                throw new ArgumentNullException(nameof(especialidad));

            if (string.IsNullOrWhiteSpace(especialidad.Nombre))
                throw new ArgumentException(
                    "El nombre de la especialidad es obligatorio.");

            if (especialidad.Nombre.Trim().Length > 100)
                throw new ArgumentException(
                    "El nombre no puede superar los 100 caracteres.");

            if (especialidad.Descripcion != null &&
                especialidad.Descripcion.Length > 250)
            {
                throw new ArgumentException(
                    "La descripción no puede superar los 250 caracteres.");
            }

            especialidad.Nombre = especialidad.Nombre.Trim();

            if (especialidad.Descripcion != null)
                especialidad.Descripcion =
                    especialidad.Descripcion.Trim();
        }

        // CONSULTAR SOLAMENTE LAS ESPECIALIDADES ACTIVAS.
        // Se utiliza para cargar las especialidades de los doctores.
        public List<Especialidad> ListarEspecialidades()
        {
            return especialidadDAL.ObtenerTodas();
        }

        // CONSULTAR TODAS LAS ESPECIALIDADES.
        // Incluye las activas y las desactivadas.
        public List<Especialidad> ListarTodas()
        {
            return especialidadDAL.ListarTodas();
        }

        // REGISTRAR UNA ESPECIALIDAD.
        public bool RegistrarEspecialidad(Especialidad especialidad)
        {
            ValidarEspecialidad(especialidad);

            especialidad.Estado = true;

            return especialidadDAL.Insertar(especialidad);
        }

        // MODIFICAR UNA ESPECIALIDAD.
        public bool ModificarEspecialidad(Especialidad especialidad)
        {
            ValidarEspecialidad(especialidad);

            if (especialidad.IdEspecialidad <= 0)
                throw new ArgumentException(
                    "Selecciona una especialidad válida.");

            return especialidadDAL.Actualizar(especialidad);
        }

        // ACTIVAR O DESACTIVAR UNA ESPECIALIDAD.
        public bool CambiarEstadoEspecialidad(
            int idEspecialidad, bool estado)
        {
            if (idEspecialidad <= 0)
                throw new ArgumentException(
                    "Selecciona una especialidad válida.");

            return especialidadDAL.CambiarEstado(
                idEspecialidad, estado);
        }
    }
}