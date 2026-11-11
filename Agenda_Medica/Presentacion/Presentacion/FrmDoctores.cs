using LogicaNegocio.Servicios;
using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmDoctores : Form
    {
        // Objetos para utilizar las reglas del negocio.
        private DoctorBLL doctorBLL = new DoctorBLL();
        private EspecialidadBLL especialidadBLL = new EspecialidadBLL();

        public FrmDoctores()
        {
            InitializeComponent();

            // Cargar las especialidades al abrir el formulario.
            CargarEspecialidades();
        }

        // CARGAR ESPECIALIDADES EN EL COMBOBOX
        private void CargarEspecialidades()
        {
            try
            {
                List<Especialidad> lista =
                    especialidadBLL.ListarEspecialidades();

                cmbEspecialidad.DataSource = lista;
                cmbEspecialidad.DisplayMember = "Nombre";
                cmbEspecialidad.ValueMember = "IdEspecialidad";
                cmbEspecialidad.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las especialidades: " + ex.Message);
            }
        }

        // LIMPIAR LOS CAMPOS DEL FORMULARIO
        private void LimpiarCampos()
        {
            txtnombre1.Clear();
            txtapellidos2.Clear();
            txtidentificacion6.Clear();
            txttelefono4.Clear();
            txtcorreo5.Clear();

            cmbEspecialidad.SelectedIndex = -1;
        }

        // GUARDAR DOCTOR
        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEspecialidad.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar una especialidad.");
                    return;
                }

                Doctor doctor = new Doctor();

                doctor.Nombre = txtnombre1.Text;
                doctor.Apellidos = txtapellidos2.Text;
                doctor.Identificacion = txtidentificacion6.Text;
                doctor.IdEspecialidad =
                    Convert.ToInt32(cmbEspecialidad.SelectedValue);
                doctor.Telefono = txttelefono4.Text;
                doctor.Correo = txtcorreo5.Text;

                bool registrado = doctorBLL.RegistrarDoctor(doctor);

                if (registrado)
                {
                    MessageBox.Show("Doctor registrado correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    dgvDoctores.DataSource = doctorBLL.ListarDoctores();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el doctor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }

        // CONSULTAR DOCTORES
        private void btnconsultar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDoctores.DataSource = null;
                dgvDoctores.DataSource = doctorBLL.ListarDoctores();

                if (dgvDoctores.Rows.Count == 0)
                {
                    MessageBox.Show("No hay doctores registrados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar doctores: " + ex.Message);
            }
        }

        // SELECCIONAR UN DOCTOR DE LA TABLA
        private void dgvDoctores_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvDoctores.Rows[e.RowIndex];

                if (fila.DataBoundItem is Doctor doctor)
                {
                    txtnombre1.Text = doctor.Nombre;
                    txtapellidos2.Text = doctor.Apellidos;
                    txtidentificacion6.Text = doctor.Identificacion;
                    txttelefono4.Text = doctor.Telefono;
                    txtcorreo5.Text = doctor.Correo;

                    // Seleccionar la especialidad correspondiente.
                    if (doctor.IdEspecialidad.HasValue)
                    {
                        cmbEspecialidad.SelectedValue =
                            doctor.IdEspecialidad.Value;
                    }
                    else
                    {
                        cmbEspecialidad.SelectedIndex = -1;
                    }
                }
            }
        }

        // MODIFICAR DOCTOR
        private void btnmodificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDoctores.CurrentRow == null)
                {
                    MessageBox.Show("Primero debes seleccionar un doctor.");
                    return;
                }

                if (cmbEspecialidad.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar una especialidad.");
                    return;
                }

                Doctor doctorSeleccionado =
                    dgvDoctores.CurrentRow.DataBoundItem as Doctor;

                if (doctorSeleccionado == null)
                {
                    MessageBox.Show("No se pudo obtener el doctor seleccionado.");
                    return;
                }

                Doctor doctor = new Doctor();

                doctor.IdDoctor = doctorSeleccionado.IdDoctor;
                doctor.Nombre = txtnombre1.Text;
                doctor.Apellidos = txtapellidos2.Text;
                doctor.Identificacion = txtidentificacion6.Text;
                doctor.IdEspecialidad =
                    Convert.ToInt32(cmbEspecialidad.SelectedValue);
                doctor.Telefono = txttelefono4.Text;
                doctor.Correo = txtcorreo5.Text;

                bool actualizado = doctorBLL.ModificarDoctor(doctor);

                if (actualizado)
                {
                    MessageBox.Show("Doctor modificado correctamente.");

                    dgvDoctores.DataSource = null;
                    dgvDoctores.DataSource = doctorBLL.ListarDoctores();

                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el doctor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        // ELIMINAR DOCTOR
        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDoctores.CurrentRow == null)
                {
                    MessageBox.Show("Primero debes seleccionar un doctor.");
                    return;
                }

                Doctor doctorSeleccionado =
                    dgvDoctores.CurrentRow.DataBoundItem as Doctor;

                if (doctorSeleccionado == null)
                {
                    MessageBox.Show("No se pudo obtener el doctor seleccionado.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este doctor?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    bool eliminado = doctorBLL.EliminarDoctor(
                        doctorSeleccionado.IdDoctor);

                    if (eliminado)
                    {
                        MessageBox.Show("Doctor eliminado correctamente.");

                        dgvDoctores.DataSource = null;
                        dgvDoctores.DataSource = doctorBLL.ListarDoctores();

                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el doctor.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }
    }
}