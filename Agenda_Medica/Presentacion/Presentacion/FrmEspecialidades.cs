using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Modelos.Modelos;
using LogicaNegocio.Servicios;

namespace Presentacion.Presentacion
{
    public partial class FrmEspecialidades : Form
    {
        private readonly EspecialidadBLL especialidadBLL =
            new EspecialidadBLL();

        private int idEspecialidadSeleccionada = 0;

        public FrmEspecialidades()
        {
            InitializeComponent();

            btnGuardar.Click += btnGuardar_Click;
            btnConsultar.Click += btnConsultar_Click;
            btnModificar.Click += btnModificar_Click;
            btnDesactivar.Click += btnDesactivar_Click;
            dgvEspecialidades.CellClick += dgvEspecialidades_CellClick;

            dgvEspecialidades.AutoGenerateColumns = true;
            dgvEspecialidades.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvEspecialidades.MultiSelect = false;
            dgvEspecialidades.ReadOnly = true;
            dgvEspecialidades.AllowUserToAddRows = false;

            chkEstado.Checked = true;

            CargarEspecialidades();
        }

        // CONSULTAR Y MOSTRAR LAS ESPECIALIDADES.
        private void CargarEspecialidades()
        {
            try
            {
                List<Especialidad> lista =
                    especialidadBLL.ListarTodas();

                dgvEspecialidades.DataSource = null;
                dgvEspecialidades.DataSource = lista;

                if (dgvEspecialidades.Columns["IdEspecialidad"] != null)
                    dgvEspecialidades.Columns["IdEspecialidad"].HeaderText =
                        "Código";

                if (dgvEspecialidades.Columns["Nombre"] != null)
                    dgvEspecialidades.Columns["Nombre"].HeaderText =
                        "Especialidad";

                if (dgvEspecialidades.Columns["Descripcion"] != null)
                    dgvEspecialidades.Columns["Descripcion"].HeaderText =
                        "Descripción";

                if (dgvEspecialidades.Columns["Estado"] != null)
                    dgvEspecialidades.Columns["Estado"].HeaderText =
                        "Activa";

                dgvEspecialidades.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar las especialidades: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // GUARDAR UNA ESPECIALIDAD.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Especialidad especialidad = new Especialidad();

                especialidad.Nombre = txtNombre.Text;
                especialidad.Descripcion = txtDescripcion.Text;
                especialidad.Estado = true;

                bool registrado =
                    especialidadBLL.RegistrarEspecialidad(especialidad);

                if (registrado)
                {
                    MessageBox.Show(
                        "Especialidad registrada correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarEspecialidades();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo registrar la especialidad.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar la especialidad: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // CONSULTAR LAS ESPECIALIDADES.
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarEspecialidades();
        }

        // CARGAR LOS DATOS AL SELECCIONAR UNA FILA.
        private void dgvEspecialidades_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Especialidad especialidad =
                dgvEspecialidades.Rows[e.RowIndex].DataBoundItem
                as Especialidad;

            if (especialidad == null)
                return;

            idEspecialidadSeleccionada =
                especialidad.IdEspecialidad;

            txtNombre.Text = especialidad.Nombre;
            txtDescripcion.Text = especialidad.Descripcion;
            chkEstado.Checked = especialidad.Estado;
        }

        // MODIFICAR UNA ESPECIALIDAD.
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEspecialidadSeleccionada <= 0)
                {
                    MessageBox.Show(
                        "Selecciona una especialidad de la tabla.");
                    return;
                }

                Especialidad especialidad = new Especialidad();

                especialidad.IdEspecialidad =
                    idEspecialidadSeleccionada;

                especialidad.Nombre = txtNombre.Text;
                especialidad.Descripcion = txtDescripcion.Text;

                bool modificado =
                    especialidadBLL.ModificarEspecialidad(especialidad);

                if (modificado)
                {
                    // Actualizar también el estado seleccionado.
                    especialidadBLL.CambiarEstadoEspecialidad(
                        idEspecialidadSeleccionada,
                        chkEstado.Checked);

                    MessageBox.Show(
                        "Especialidad modificada correctamente.",
                        "Modificación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarEspecialidades();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo modificar la especialidad.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al modificar la especialidad: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // DESACTIVAR UNA ESPECIALIDAD.
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEspecialidadSeleccionada <= 0)
                {
                    MessageBox.Show(
                        "Selecciona una especialidad de la tabla.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que deseas desactivar esta especialidad?",
                    "Confirmar desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    bool desactivado =
                        especialidadBLL.CambiarEstadoEspecialidad(
                            idEspecialidadSeleccionada, false);

                    if (desactivado)
                    {
                        MessageBox.Show(
                            "Especialidad desactivada correctamente.",
                            "Proceso exitoso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CargarEspecialidades();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo desactivar la especialidad.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al desactivar la especialidad: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // LIMPIAR LOS CAMPOS DEL FORMULARIO.
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();

            chkEstado.Checked = true;

            idEspecialidadSeleccionada = 0;

            dgvEspecialidades.ClearSelection();
            txtNombre.Focus();
        }
    }
}