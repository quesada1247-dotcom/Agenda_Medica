using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicaNegocio.Servicios;
using Modelos.Modelos;


namespace Presentacion.Presentacion
{
    public partial class FrmHistorialMedico : Form
    {
        private PacienteBLL pacienteBLL = new PacienteBLL();
        private DoctorBLL doctorBLL = new DoctorBLL();
        private CitaBLL citaBLL = new CitaBLL();
        private HistorialMedicoBLL historialBLL = new HistorialMedicoBLL();

        public FrmHistorialMedico()
        {
            InitializeComponent();

            CargarPacientes();
            CargarDoctores();
            CargarCitas();
        }

        private void CargarPacientes()
        {
            List<Paciente> pacientes = pacienteBLL.ListarPacientes();

            cmbPaciente.DataSource = pacientes;
            cmbPaciente.DisplayMember = "Nombre";
            cmbPaciente.ValueMember = "IdPaciente";
            cmbPaciente.SelectedIndex = -1;
        }

        private void CargarDoctores()
        {
            List<Doctor> doctores = doctorBLL.ListarDoctores();

            cmbDoctor.DataSource = doctores;
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "IdDoctor";
            cmbDoctor.SelectedIndex = -1;
        }

        private void CargarCitas()
        {
            List<Cita> citas = citaBLL.ListarCitas();

            cmbCita.DataSource = citas;
            cmbCita.DisplayMember = "IdCita";
            cmbCita.ValueMember = "IdCita";
            cmbCita.SelectedIndex = -1;
        }
    }
}
