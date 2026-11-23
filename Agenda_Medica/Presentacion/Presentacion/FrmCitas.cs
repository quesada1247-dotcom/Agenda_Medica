
using LogicaNegocio.Servicios;
using Modelos.Modelos;
using System;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmCitas : Form
    {
        private PacienteBLL pacienteBLL;
        private DoctorBLL doctorBLL;
        private CitaBLL citaBLL;

        private int idCitaSeleccionada = 0;
        private DateTime fechaOriginal;
        private TimeSpan horaOriginal;
        private string estadoOriginal;

        public FrmCitas()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            pacienteBLL = new PacienteBLL();
            doctorBLL = new DoctorBLL();
            citaBLL = new CitaBLL();

            CargarListas();
            CargarTiposYDuraciones();
        }

        // CARGAR TIPOS Y DURACIONES
        private void CargarTiposYDuraciones()
        {
            cmbTipoCita.Items.Clear();
            cmbTipoCita.Items.Add("Presencial");
            cmbTipoCita.Items.Add("Virtual");
            cmbTipoCita.Items.Add("Urgencia");

            cmbTipoCita.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoCita.SelectedIndex = 0;

            cmbDuracion.Items.Clear();
            cmbDuracion.Items.Add(30);
            cmbDuracion.Items.Add(45);
            cmbDuracion.Items.Add(60);

            cmbDuracion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDuracion.SelectedIndex = 0;
        }

        // CARGAR PACIENTES Y DOCTORES
        private void CargarListas()
        {
            cmbPaciente.DataSource = pacienteBLL.ListarPacientes();
            cmbPaciente.DisplayMember = "Nombre";
            cmbPaciente.ValueMember = "IdPaciente";

            cmbDoctor.DataSource = doctorBLL.ListarDoctores();
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "IdDoctor";
        }

        // REGISTRAR CITA
        private void btnGuardarCita_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPaciente.SelectedValue == null ||
                    cmbDoctor.SelectedValue == null)
                {
                    MessageBox.Show("Debes seleccionar un paciente y un doctor.");
                    return;
                }

                Cita cita = new Cita();

                cita.IdPaciente = Convert.ToInt32(cmbPaciente.SelectedValue);
                cita.IdDoctor = Convert.ToInt32(cmbDoctor.SelectedValue);
                cita.Fecha = dtpFecha.Value.Date;
                cita.Hora = dtpHora.Value.TimeOfDay;
                cita.Motivo = txtmotivo1.Text;
                cita.Tipo = cmbTipoCita.SelectedItem.ToString();
                cita.DuracionMinutos =
                    Convert.ToInt32(cmbDuracion.SelectedItem);
                cita.Estado = "Programada";

                bool registrado = citaBLL.RegistrarCita(cita);

                if (registrado)
                {
                    MessageBox.Show("Cita registrada correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimpiarCampos();
                    ConsultarCitas();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la cita.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la cita: " + ex.Message);
            }
        }

        // CONSULTAR CITAS
        private void btnConsultarCitas_Click(object sender, EventArgs e)
        {
            ConsultarCitas();
        }

        private void ConsultarCitas()
        {
            try
            {
                dgvCitas.DataSource = null;
                dgvCitas.DataSource = citaBLL.ListarCitas();

                if (dgvCitas.Columns.Contains("IdPaciente"))
                    dgvCitas.Columns["IdPaciente"].Visible = false;

                if (dgvCitas.Columns.Contains("IdDoctor"))
                    dgvCitas.Columns["IdDoctor"].Visible = false;

                dgvCitas.Columns["IdCita"].HeaderText = "N.º Cita";
                dgvCitas.Columns["NombrePaciente"].HeaderText = "Paciente";
                dgvCitas.Columns["NombreDoctor"].HeaderText = "Doctor";
                dgvCitas.Columns["Fecha"].HeaderText = "Fecha";
                dgvCitas.Columns["Hora"].HeaderText = "Hora";
                dgvCitas.Columns["Motivo"].HeaderText = "Motivo";
                dgvCitas.Columns["Tipo"].HeaderText = "Tipo de cita";
                dgvCitas.Columns["DuracionMinutos"].HeaderText =
                    "Duración (minutos)";
                dgvCitas.Columns["Estado"].HeaderText = "Estado";

                if (dgvCitas.Rows.Count == 0)
                {
                    MessageBox.Show("No hay citas registradas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar las citas: " + ex.Message);
            }
        }

        // SELECCIONAR UNA CITA
        private void dgvCitas_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = dgvCitas.Rows[e.RowIndex];

                idCitaSeleccionada =
                    Convert.ToInt32(fila.Cells["IdCita"].Value);

                fechaOriginal =
                    Convert.ToDateTime(fila.Cells["Fecha"].Value);

                horaOriginal =
                    (TimeSpan)fila.Cells["Hora"].Value;

                estadoOriginal =
                    fila.Cells["Estado"].Value.ToString();

                cmbPaciente.SelectedValue =
                    Convert.ToInt32(fila.Cells["IdPaciente"].Value);

                cmbDoctor.SelectedValue =
                    Convert.ToInt32(fila.Cells["IdDoctor"].Value);

                dtpFecha.Value = fechaOriginal;

                dtpHora.Value =
                    DateTime.Today.Add(horaOriginal);

                txtmotivo1.Text =
                    fila.Cells["Motivo"].Value.ToString();

                cmbTipoCita.SelectedItem =
                    fila.Cells["Tipo"].Value.ToString();

                cmbDuracion.SelectedItem =
                    Convert.ToInt32(fila.Cells["DuracionMinutos"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar la cita: " + ex.Message);
            }
        }

        // MODIFICAR Y REPROGRAMAR
        private void btnModificarCita_Click(object sender, EventArgs e)
        {
            try
            {
                if (idCitaSeleccionada <= 0)
                {
                    MessageBox.Show("Primero debes seleccionar una cita.");
                    return;
                }

                if (estadoOriginal == "Cancelada" ||
                    estadoOriginal == "Atendida" ||
                    estadoOriginal == "No asistio")
                {
                    MessageBox.Show(
                        "Esta cita ya está finalizada o cancelada y no se puede modificar.");
                    return;
                }

                Cita cita = new Cita();

                cita.IdCita = idCitaSeleccionada;
                cita.IdPaciente = Convert.ToInt32(cmbPaciente.SelectedValue);
                cita.IdDoctor = Convert.ToInt32(cmbDoctor.SelectedValue);
                cita.Fecha = dtpFecha.Value.Date;
                cita.Hora = dtpHora.Value.TimeOfDay;
                cita.Motivo = txtmotivo1.Text;
                cita.Tipo = cmbTipoCita.SelectedItem.ToString();
                cita.DuracionMinutos =
                    Convert.ToInt32(cmbDuracion.SelectedItem);

                bool cambioHorario =
                    cita.Fecha.Date != fechaOriginal.Date ||
                    cita.Hora != horaOriginal;

                if (cambioHorario)
                {
                    cita.Estado = "Reprogramada";
                }
                else
                {
                    cita.Estado = estadoOriginal;
                }

                bool modificado = citaBLL.ModificarCita(cita);

                if (modificado)
                {
                    MessageBox.Show(
                        cambioHorario
                        ? "La cita fue reprogramada correctamente."
                        : "La cita fue modificada correctamente.");

                    ConsultarCitas();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la cita.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la cita: " + ex.Message);
            }
        }

        // CANCELAR CITA SIN ELIMINAR EL REGISTRO
        private void btnEliminarCita_Click(object sender, EventArgs e)
        {
            try
            {
                if (idCitaSeleccionada <= 0)
                {
                    MessageBox.Show("Primero debes seleccionar una cita.");
                    return;
                }

                if (estadoOriginal == "Cancelada")
                {
                    MessageBox.Show("Esta cita ya está cancelada.");
                    return;
                }

                if (estadoOriginal == "Atendida" ||
                    estadoOriginal == "No asistio")
                {
                    MessageBox.Show("Esta cita ya está finalizada.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Deseas cancelar esta cita? El registro se conservará en el historial.",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                bool cancelada = citaBLL.CambiarEstadoCita(
                    idCitaSeleccionada,
                    "Cancelada",
                    "Cancelada desde el módulo de citas");

                if (cancelada)
                {
                    MessageBox.Show("La cita fue cancelada correctamente.");

                    ConsultarCitas();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo cancelar la cita.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar la cita: " + ex.Message);
            }
        }

        // LIMPIAR CAMPOS
        private void LimpiarCampos()
        {
            idCitaSeleccionada = 0;
            fechaOriginal = DateTime.MinValue;
            horaOriginal = TimeSpan.Zero;
            estadoOriginal = null;

            if (cmbPaciente.Items.Count > 0)
                cmbPaciente.SelectedIndex = 0;

            if (cmbDoctor.Items.Count > 0)
                cmbDoctor.SelectedIndex = 0;

            if (cmbTipoCita.Items.Count > 0)
                cmbTipoCita.SelectedIndex = 0;

            if (cmbDuracion.Items.Count > 0)
                cmbDuracion.SelectedIndex = 0;

            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Today;
            txtmotivo1.Clear();
        }
    }
}
