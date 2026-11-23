
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using LogicaNegocio.Servicios;
using Modelos.Modelos;

namespace Presentacion.Presentacion
{
    public partial class FrmAgenda : Form
    {
        // Servicios de las capas de negocio
        private DoctorBLL doctorBLL = new DoctorBLL();
        private PacienteBLL pacienteBLL = new PacienteBLL();
        private CitaBLL citaBLL = new CitaBLL();

        public FrmAgenda()
        {
            InitializeComponent();

            // Configurar los controles
            ConfigurarFormulario();

            // Cargar información desde MySQL
            CargarDoctores();
            CargarPacientes();
            CargarEstados();
            CargarEstadosCambio();

            // Conectar los botones
            btnBuscar.Click += btnBuscar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnCambiarEstado.Click += btnCambiarEstado_Click;

            // Configurar la tabla
            ConfigurarTabla();
        }

        // ==========================================
        // CONFIGURAR TABLA DE RESULTADOS
        // ==========================================
        private void ConfigurarTabla()
        {
            dgvAgenda.AutoGenerateColumns = true;
            dgvAgenda.ReadOnly = true;
            dgvAgenda.AllowUserToAddRows = false;
            dgvAgenda.AllowUserToDeleteRows = false;

            dgvAgenda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAgenda.MultiSelect = false;

            // Ajustar las columnas al espacio disponible
            dgvAgenda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ajustar automáticamente la altura de las filas
            dgvAgenda.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Encabezados
            dgvAgenda.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Evitar que el usuario cambie el tamaño de las columnas
            dgvAgenda.AllowUserToResizeColumns = false;

            // Apariencia
            dgvAgenda.RowHeadersVisible = false;
            dgvAgenda.BackgroundColor = System.Drawing.Color.White;
            dgvAgenda.BorderStyle = BorderStyle.FixedSingle;

            // Selección de filas
            dgvAgenda.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.LightSteelBlue;

            dgvAgenda.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;
        }


        // ==========================================
        // CONFIGURAR FORMULARIO
        // ==========================================
        private void ConfigurarFormulario()
        {
            cmbDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaciente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpFecha.Format = DateTimePickerFormat.Short;

            // Ningún médico ni paciente seleccionado significa
            // que no se aplicará ese filtro.
            cmbDoctor.SelectedIndex = -1;
            cmbPaciente.SelectedIndex = -1;
        }

        // ==========================================
        // CARGAR DOCTORES
        // ==========================================
        private void CargarDoctores()
        {
            List<Doctor> doctores = doctorBLL.ListarDoctores();

            cmbDoctor.DataSource = null;
            cmbDoctor.DataSource = doctores;
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "IdDoctor";
            cmbDoctor.SelectedIndex = -1;
        }

        // ==========================================
        // CARGAR PACIENTES
        // ==========================================
        private void CargarPacientes()
        {
            List<Paciente> pacientes = pacienteBLL.ListarPacientes();

            cmbPaciente.DataSource = null;
            cmbPaciente.DataSource = pacientes;
            cmbPaciente.DisplayMember = "Nombre";
            cmbPaciente.ValueMember = "IdPaciente";
            cmbPaciente.SelectedIndex = -1;
        }

        // ==========================================
        // CARGAR ESTADOS DE LAS CITAS
        // ==========================================
        private void CargarEstados()
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Todos");
            cmbEstado.Items.Add("Programada");
            cmbEstado.Items.Add("Confirmada");
            cmbEstado.Items.Add("Atendida");
            cmbEstado.Items.Add("Cancelada");
            cmbEstado.Items.Add("No asistio");
            cmbEstado.Items.Add("Reprogramada");

            cmbEstado.SelectedIndex = 0;
        }

        // ==========================================
        // BUSCAR CITAS DE LA AGENDA
        // ==========================================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el médico seleccionado
                int? idDoctor = null;

                if (cmbDoctor.SelectedIndex >= 0)
                {
                    idDoctor = Convert.ToInt32(cmbDoctor.SelectedValue);
                }

                // Obtener el paciente seleccionado
                int? idPaciente = null;

                if (cmbPaciente.SelectedIndex >= 0)
                {
                    idPaciente = Convert.ToInt32(cmbPaciente.SelectedValue);
                }

                // Obtener la fecha solamente si se activa el CheckBox
                DateTime? fecha = null;

                if (chkFiltrarFecha.Checked)
                {
                    fecha = dtpFecha.Value.Date;
                }

                // Obtener el estado seleccionado
                string estado = null;

                if (cmbEstado.SelectedIndex > 0)
                {
                    estado = cmbEstado.SelectedItem.ToString();
                }

                // Consultar las citas utilizando los filtros
                List<Cita> citas = citaBLL.FiltrarCitas(
                    idDoctor,
                    fecha,
                    idPaciente,
                    estado
                );

                // Mostrar los resultados
                dgvAgenda.DataSource = null;
                dgvAgenda.DataSource = citas;

                // Personalizar encabezados
                if (dgvAgenda.Columns["IdCita"] != null)
                    dgvAgenda.Columns["IdCita"].HeaderText = "N.º Cita";

                if (dgvAgenda.Columns["Fecha"] != null)
                    dgvAgenda.Columns["Fecha"].HeaderText = "Fecha";

                if (dgvAgenda.Columns["Hora"] != null)
                    dgvAgenda.Columns["Hora"].HeaderText = "Hora";

                if (dgvAgenda.Columns["NombrePaciente"] != null)
                    dgvAgenda.Columns["NombrePaciente"].HeaderText = "Paciente";

                if (dgvAgenda.Columns["NombreDoctor"] != null)
                    dgvAgenda.Columns["NombreDoctor"].HeaderText = "Médico";

                if (dgvAgenda.Columns["Tipo"] != null)
                    dgvAgenda.Columns["Tipo"].HeaderText = "Tipo";

                if (dgvAgenda.Columns["DuracionMinutos"] != null)
                    dgvAgenda.Columns["DuracionMinutos"].HeaderText = "Duración";

                if (dgvAgenda.Columns["Estado"] != null)
                    dgvAgenda.Columns["Estado"].HeaderText = "Estado";

                // Ocultar columnas que no necesitamos mostrar
                string[] columnasOcultas =
                {
            "IdPaciente",
            "IdDoctor",
            "IdUsuario",
            "HoraFin",
            "Motivo",
            "Observaciones"
        };

                foreach (string columna in columnasOcultas)
                {
                    if (dgvAgenda.Columns[columna] != null)
                        dgvAgenda.Columns[columna].Visible = false;
                }

                dgvAgenda.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                MessageBox.Show(
                    "Se encontraron " + citas.Count + " citas.",
                    "Consulta de agenda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar la agenda: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ==========================================
        // LIMPIAR FILTROS
        // ==========================================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbDoctor.SelectedIndex = -1;
            cmbPaciente.SelectedIndex = -1;
            cmbEstado.SelectedIndex = 0;

            chkFiltrarFecha.Checked = false;
            dtpFecha.Value = DateTime.Today;

            dgvAgenda.DataSource = null;

        }

        // CARGAR ESTADOS PARA CAMBIAR UNA CITA
        private void CargarEstadosCambio()
        {
            cmbNuevoEstado.Items.Clear();

            cmbNuevoEstado.Items.Add("Programada");
            cmbNuevoEstado.Items.Add("Confirmada");
            cmbNuevoEstado.Items.Add("Atendida");
            cmbNuevoEstado.Items.Add("Cancelada");
            cmbNuevoEstado.Items.Add("No asistio");
            cmbNuevoEstado.Items.Add("Reprogramada");

            cmbNuevoEstado.SelectedIndex = -1;
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAgenda.CurrentRow == null)
                {
                    MessageBox.Show("Primero debes seleccionar una cita.",
                        "Atención",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (cmbNuevoEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar el nuevo estado.",
                        "Atención",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                int idCita = Convert.ToInt32(
                    dgvAgenda.CurrentRow.Cells["IdCita"].Value);

                string nuevoEstado = cmbNuevoEstado.SelectedItem.ToString();

                string observaciones = txtObservacionesEstado.Text.Trim();

                DialogResult respuesta = MessageBox.Show(
                    "¿Deseas cambiar el estado de esta cita a " + nuevoEstado + "?",
                    "Confirmar cambio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                bool actualizado = citaBLL.CambiarEstadoCita(
                    idCita,
                    nuevoEstado,
                    observaciones);

                if (actualizado)
                {
                    MessageBox.Show(
                        "El estado de la cita se actualizó correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    cmbNuevoEstado.SelectedIndex = -1;
                    txtObservacionesEstado.Clear();

                    // Actualizar la tabla conservando los filtros
                    btnBuscar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el estado de la cita.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cambiar el estado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
