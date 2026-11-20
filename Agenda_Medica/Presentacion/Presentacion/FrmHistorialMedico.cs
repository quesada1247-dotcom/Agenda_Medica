
using LogicaNegocio.Servicios;
using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmHistorialMedico : Form
    {
        // Objetos de las capas de lógica de negocio
        private PacienteBLL pacienteBLL = new PacienteBLL();
        private DoctorBLL doctorBLL = new DoctorBLL();
        private CitaBLL citaBLL = new CitaBLL();
        private HistorialMedicoBLL historialBLL = new HistorialMedicoBLL();

        // Identificador del historial seleccionado
        private int idHistorialSeleccionado = 0;

        // Constructor del formulario
        public FrmHistorialMedico()
        {
            InitializeComponent();

            // Cargar información en los ComboBox
            CargarPacientes();
            CargarDoctores();
            CargarCitas();

            // Conectar los eventos de los botones
            btnGuardar.Click += btnGuardar_Click;
            btnConsultar.Click += btnConsultar_Click;
            dgvHistorial.CellClick += dgvHistorial_CellClick;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;

        }

        // ==========================================
        // CARGAR PACIENTES
        // ==========================================
        private void CargarPacientes()
        {
            List<Paciente> pacientes = pacienteBLL.ListarPacientes();

            cmbPaciente.DataSource = pacientes;
            cmbPaciente.DisplayMember = "Nombre";
            cmbPaciente.ValueMember = "IdPaciente";
            cmbPaciente.SelectedIndex = -1;
        }

        // ==========================================
        // CARGAR DOCTORES
        // ==========================================
        private void CargarDoctores()
        {
            List<Doctor> doctores = doctorBLL.ListarDoctores();

            cmbDoctor.DataSource = doctores;
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "IdDoctor";
            cmbDoctor.SelectedIndex = -1;
        }

        // ==========================================
        // CARGAR CITAS
        // ==========================================
        private void CargarCitas()
        {
            List<Cita> citas = citaBLL.ListarCitas();

            cmbCita.DataSource = citas;
            cmbCita.DisplayMember = "IdCita";
            cmbCita.ValueMember = "IdCita";
            cmbCita.SelectedIndex = -1;
        }

        // ==========================================
        // BOTÓN GUARDAR HISTORIAL MÉDICO
        // ==========================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se seleccione un paciente
                if (cmbPaciente.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un paciente.");
                    return;
                }

                // Validar que se seleccione un doctor
                if (cmbDoctor.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un doctor.");
                    return;
                }

                // Validar que se escriba un diagnóstico
                if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
                {
                    MessageBox.Show("Debe ingresar el diagnóstico.");
                    txtDiagnostico.Focus();
                    return;
                }

                // Crear el objeto del historial médico
                HistorialMedico historial = new HistorialMedico();

                historial.IdPaciente =
                    Convert.ToInt32(cmbPaciente.SelectedValue);

                historial.IdDoctor =
                    Convert.ToInt32(cmbDoctor.SelectedValue);

                // La cita es opcional
                if (cmbCita.SelectedValue != null)
                {
                    historial.IdCita =
                        Convert.ToInt32(cmbCita.SelectedValue);
                }
                else
                {
                    historial.IdCita = null;
                }

                // Obtener los datos de los campos
                historial.Diagnostico = txtDiagnostico.Text.Trim();
                historial.Tratamiento = txtTratamiento.Text.Trim();
                historial.Observaciones = txtObservaciones.Text.Trim();

                // Registrar el historial en la base de datos
                historialBLL.RegistrarHistorial(historial);

                // Actualizar la tabla de historiales
                CargarHistoriales();

                MessageBox.Show(
                    "Historial médico registrado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Limpiar los campos después de guardar
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar el historial médico: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ==========================================
        // CARGAR HISTORIALES EN EL DATAGRIDVIEW
        // ==========================================
        private void CargarHistoriales()
        {
            dgvHistorial.DataSource = null;

            // Obtener los historiales desde la capa de negocio
            dgvHistorial.DataSource = historialBLL.ListarHistoriales();

            // Configurar la presentación de la tabla
            dgvHistorial.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvHistorial.ReadOnly = true;

            dgvHistorial.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvHistorial.AllowUserToAddRows = false;

            dgvHistorial.MultiSelect = false;
        }

        // ==========================================
        // BOTÓN CONSULTAR HISTORIALES
        // ==========================================
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar todos los historiales registrados
                CargarHistoriales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar los historiales médicos: " + ex.Message,
                    "Error de consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ==========================================
        // LIMPIAR LOS CAMPOS DEL FORMULARIO
        // ==========================================
        private void LimpiarCampos()
        {
            cmbPaciente.SelectedIndex = -1;
            cmbDoctor.SelectedIndex = -1;
            cmbCita.SelectedIndex = -1;

            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            txtObservaciones.Clear();
        }

        // ==========================================
        // SELECCIONAR UN HISTORIAL DE LA TABLA
        // ==========================================
        private void dgvHistorial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Evitar seleccionar el encabezado de la tabla
                if (e.RowIndex < 0)
                {
                    return;
                }

                // Obtener el historial seleccionado
                HistorialMedico historial =
                    dgvHistorial.Rows[e.RowIndex].DataBoundItem as HistorialMedico;

                if (historial == null)
                {
                    return;
                }

                // Guardar el ID del historial seleccionado
                idHistorialSeleccionado = historial.IdHistorial;

                // Cargar el paciente
                cmbPaciente.SelectedValue = historial.IdPaciente;

                // Cargar el doctor
                cmbDoctor.SelectedValue = historial.IdDoctor;

                // Cargar la cita, si existe
                if (historial.IdCita.HasValue)
                {
                    cmbCita.SelectedValue = historial.IdCita.Value;
                }
                else
                {
                    cmbCita.SelectedIndex = -1;
                }

                // Cargar los datos del historial
                txtDiagnostico.Text = historial.Diagnostico;
                txtTratamiento.Text = historial.Tratamiento;
                txtObservaciones.Text = historial.Observaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar el historial: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ==========================================
        // BOTÓN MODIFICAR HISTORIAL MÉDICO
        // ==========================================
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado un historial
                if (idHistorialSeleccionado <= 0)
                {
                    MessageBox.Show("Debe seleccionar un historial médico de la tabla.");
                    return;
                }

                // Validar paciente
                if (cmbPaciente.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un paciente.");
                    return;
                }

                // Validar doctor
                if (cmbDoctor.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un doctor.");
                    return;
                }

                // Validar diagnóstico
                if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
                {
                    MessageBox.Show("Debe ingresar el diagnóstico.");
                    return;
                }

                // Crear objeto con los datos modificados
                HistorialMedico historial = new HistorialMedico();

                historial.IdHistorial = idHistorialSeleccionado;
                historial.IdPaciente = Convert.ToInt32(cmbPaciente.SelectedValue);
                historial.IdDoctor = Convert.ToInt32(cmbDoctor.SelectedValue);

                // La cita es opcional
                if (cmbCita.SelectedValue != null)
                {
                    historial.IdCita = Convert.ToInt32(cmbCita.SelectedValue);
                }
                else
                {
                    historial.IdCita = null;
                }

                historial.Diagnostico = txtDiagnostico.Text.Trim();
                historial.Tratamiento = txtTratamiento.Text.Trim();
                historial.Observaciones = txtObservaciones.Text.Trim();

                // Actualizar el historial en la base de datos
                historialBLL.ModificarHistorial(historial);

                MessageBox.Show(
                    "Historial médico modificado correctamente.",
                    "Modificación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Actualizar la tabla
                CargarHistoriales();

                // Reiniciar el formulario
                LimpiarCampos();

                idHistorialSeleccionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al modificar el historial médico: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ==========================================
        // BOTÓN ELIMINAR HISTORIAL MÉDICO
        // ==========================================
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado un historial
                if (idHistorialSeleccionado <= 0)
                {
                    MessageBox.Show("Debe seleccionar un historial médico de la tabla.");
                    return;
                }

                // Solicitar confirmación antes de eliminar
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este historial médico?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                // Si el usuario selecciona No, cancelar la operación
                if (resultado == DialogResult.No)
                {
                    return;
                }

                // Eliminar el historial mediante la capa de negocio
                historialBLL.EliminarHistorial(idHistorialSeleccionado);

                MessageBox.Show(
                    "Historial médico eliminado correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Actualizar la tabla
                CargarHistoriales();

                // Limpiar los campos
                LimpiarCampos();

                // Reiniciar el identificador seleccionado
                idHistorialSeleccionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el historial médico: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
    }
}
