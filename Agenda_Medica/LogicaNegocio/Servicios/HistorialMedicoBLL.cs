using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class HistorialMedicoBLL
    {
        private readonly HistorialMedicoDAL historialDAL =
            new HistorialMedicoDAL();

        // VALIDAR LOS DATOS DEL HISTORIAL.
        private void ValidarHistorial(HistorialMedico historial)
        {
            if (historial == null)
                throw new ArgumentNullException(nameof(historial));

            if (historial.IdPaciente <= 0)
                throw new ArgumentException(
                    "Debes seleccionar un paciente.");

            if (historial.IdDoctor <= 0)
                throw new ArgumentException(
                    "Debes seleccionar un doctor.");

            if (historial.IdCita.HasValue &&
                historial.IdCita.Value <= 0)
                throw new ArgumentException(
                    "La cita seleccionada no es válida.");

            if (string.IsNullOrWhiteSpace(historial.Diagnostico))
                throw new ArgumentException(
                    "El diagnóstico es obligatorio.");

            historial.Diagnostico =
                historial.Diagnostico.Trim();

            if (historial.Tratamiento != null)
                historial.Tratamiento =
                    historial.Tratamiento.Trim();

            if (historial.Observaciones != null)
                historial.Observaciones =
                    historial.Observaciones.Trim();
        }

        // CONSULTAR TODOS LOS HISTORIALES.
        public List<HistorialMedico> ListarHistoriales()
        {
            return historialDAL.ObtenerTodos();
        }

        // REGISTRAR UN HISTORIAL.
        public bool RegistrarHistorial(HistorialMedico historial)
        {
            ValidarHistorial(historial);

            return historialDAL.Insertar(historial);
        }

        // MODIFICAR UN HISTORIAL.
        public bool ModificarHistorial(HistorialMedico historial)
        {
            ValidarHistorial(historial);

            if (historial.IdHistorial <= 0)
                throw new ArgumentException(
                    "Debes seleccionar un historial válido.");

            return historialDAL.Actualizar(historial);
        }

        // ELIMINAR UN HISTORIAL.
        public bool EliminarHistorial(int idHistorial)
        {
            if (idHistorial <= 0)
                throw new ArgumentException(
                    "Debes seleccionar un historial válido.");

            return historialDAL.Eliminar(idHistorial);
        }
    }
}