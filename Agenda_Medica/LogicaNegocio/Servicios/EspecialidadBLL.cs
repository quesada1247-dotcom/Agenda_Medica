using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class EspecialidadBLL
    {
        // Crear un objeto para utilizar los métodos de EspecialidadDAL.
        private readonly EspecialidadDAL especialidadDAL = new EspecialidadDAL();

        // CONSULTAR ESPECIALIDADES
        public List<Especialidad> ListarEspecialidades()
        {
            // Obtener la lista de especialidades activas desde la base de datos.
            return especialidadDAL.ObtenerTodas();
        }
    }
}
