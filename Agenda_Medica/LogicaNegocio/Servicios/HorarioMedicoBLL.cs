using AccesoDatos.Datos;
using Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.Servicios
{
    public class HorarioMedicoBLL
    {
        // Crear un objeto para utilizar los métodos de HorarioMedicoDAL.
        private readonly HorarioMedicoDAL horarioDAL =
            new HorarioMedicoDAL();

        // CONSULTAR LOS HORARIOS MÉDICOS.
        public List<HorarioMedico> ListarHorarios()
        {
            return horarioDAL.ObtenerTodos();
        }

        // VALIDAR LOS DATOS DEL HORARIO.
        private void ValidarHorario(HorarioMedico horario)
        {
            if (horario == null)
                throw new ArgumentNullException(nameof(horario));

            if (horario.IdDoctor <= 0)
                throw new ArgumentException("Debes seleccionar un doctor.");

            string[] diasPermitidos =
            {
                "Lunes", "Martes", "Miercoles", "Jueves",
                "Viernes", "Sabado", "Domingo"
            };

            if (Array.IndexOf(diasPermitidos, horario.DiaSemana) < 0)
                throw new ArgumentException("Debes seleccionar un día válido.");

            if (horario.HoraInicio >= horario.HoraFin)
                throw new ArgumentException(
                    "La hora de inicio debe ser anterior a la hora de finalización.");
        }

        // VERIFICAR SI EL HORARIO SE CRUZA CON OTRO.
        private void ValidarCruceHorario(
            HorarioMedico horario,
            int idHorarioExcluir = 0)
        {
            // Solo se verifican los horarios activos.
            if (!horario.Estado)
                return;

            bool existeCruce = horarioDAL.ExisteCruceHorario(
                horario.IdDoctor,
                horario.DiaSemana,
                horario.HoraInicio,
                horario.HoraFin,
                idHorarioExcluir);

            if (existeCruce)
            {
                throw new ArgumentException(
                    "El doctor ya tiene un horario registrado que se cruza con este horario.");
            }
        }

        // REGISTRAR UN HORARIO MÉDICO.
        public bool RegistrarHorario(HorarioMedico horario)
        {
            ValidarHorario(horario);

            // Verificar que no existan horarios cruzados.
            ValidarCruceHorario(horario);

            return horarioDAL.Insertar(horario);
        }

        // MODIFICAR UN HORARIO MÉDICO.
        public bool ModificarHorario(HorarioMedico horario)
        {
            if (horario == null)
                throw new ArgumentNullException(nameof(horario));

            if (horario.IdHorario <= 0)
                throw new ArgumentException(
                    "El horario seleccionado no es válido.");

            ValidarHorario(horario);

            // Excluir el horario actual para evitar compararlo consigo mismo.
            ValidarCruceHorario(horario, horario.IdHorario);

            return horarioDAL.Actualizar(horario);
        }

        // ELIMINAR UN HORARIO MÉDICO.
        public bool EliminarHorario(int idHorario)
        {
            if (idHorario <= 0)
                throw new ArgumentException(
                    "El identificador del horario no es válido.");

            return horarioDAL.Eliminar(idHorario);
        }
    }
}