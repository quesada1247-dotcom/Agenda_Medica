using System;
using System.Data;
using System.Windows.Forms;
using LogicaNegocio.Servicios;

namespace Presentacion.Presentacion
{
    public partial class FrmReportes : Form
    {
        private readonly CitaBLL citaBLL = new CitaBLL();
        private readonly DoctorBLL doctorBLL = new DoctorBLL();

        private string tipoReporte = "";

        public FrmReportes()
        {
            InitializeComponent();

            // CONECTAR EVENTOS DE LOS BOTONES
            btnCitasPorDoctor.Click += btnCitasPorDoctor_Click;
            btnCitasCanceladas.Click += btnCitasCanceladas_Click;
            btnPacientesAtendidos.Click += btnPacientesAtendidos_Click;
            btnGenerar.Click += btnGenerar_Click;
            btnLimpiar.Click += btnLimpiar_Click;

            // CONFIGURAR TABLA
            dgvReportes.ReadOnly = true;
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvReportes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.MultiSelect = false;

            // CARGAR INFORMACIÓN
            CargarDoctores();

            dtpFecha.Value = DateTime.Today;
        }

        // CARGAR DOCTORES
        private void CargarDoctores()
        {
            try
            {
                cmbDoctor.DataSource = doctorBLL.ListarDoctores();
                cmbDoctor.DisplayMember = "NombreCompleto";
                cmbDoctor.ValueMember = "IdDoctor";
                cmbDoctor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar doctores: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // SELECCIONAR REPORTE: CITAS POR DOCTOR
        private void btnCitasPorDoctor_Click(object sender, EventArgs e)
        {
            tipoReporte = "CITAS_DOCTOR";

            dtpFecha.Enabled = true;
            cmbDoctor.Enabled = true;

            dgvReportes.DataSource = null;
        }

        // SELECCIONAR REPORTE: CITAS CANCELADAS
        private void btnCitasCanceladas_Click(object sender, EventArgs e)
        {
            tipoReporte = "CITAS_CANCELADAS";

            dtpFecha.Enabled = false;
            cmbDoctor.Enabled = false;

            GenerarReporte();
        }

        // SELECCIONAR REPORTE: PACIENTES ATENDIDOS
        private void btnPacientesAtendidos_Click(object sender, EventArgs e)
        {
            tipoReporte = "PACIENTES_ATENDIDOS";

            dtpFecha.Enabled = false;
            cmbDoctor.Enabled = false;

            GenerarReporte();
        }

        // BOTON GENERAR REPORTE
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        // GENERAR LOS REPORTES
        private void GenerarReporte()
        {
            try
            {
                DataTable resultado = null;

                if (tipoReporte == "CITAS_DOCTOR")
                {
                    if (cmbDoctor.SelectedValue == null)
                    {
                        MessageBox.Show(
                            "Debes seleccionar un doctor.",
                            "Atención",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    int idDoctor =
                        Convert.ToInt32(cmbDoctor.SelectedValue);

                    resultado = citaBLL.ReporteCitasPorDiaDoctor(
                        dtpFecha.Value.Date,
                        idDoctor);
                }
                else if (tipoReporte == "CITAS_CANCELADAS")
                {
                    resultado = citaBLL.ReporteCitasCanceladas();
                }
                else if (tipoReporte == "PACIENTES_ATENDIDOS")
                {
                    resultado = citaBLL.ReportePacientesAtendidos();
                }
                else
                {
                    MessageBox.Show(
                        "Primero debes seleccionar un tipo de reporte.",
                        "Atención",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                dgvReportes.DataSource = resultado;

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron registros para este reporte.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Reporte generado correctamente. Registros encontrados: "
                        + resultado.Rows.Count,
                        "Reporte",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el reporte: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // LIMPIAR
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            tipoReporte = "";

            cmbDoctor.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Today;

            cmbDoctor.Enabled = true;
            dtpFecha.Enabled = true;

            dgvReportes.DataSource = null;
        }
    }
}
