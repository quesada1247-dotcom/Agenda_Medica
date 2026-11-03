using LogicaNegocio.Servicios;
using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmDoctores : Form
    {
        private DoctorBLL doctorBLL = new DoctorBLL();

        public FrmDoctores()
        {
            InitializeComponent();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                Doctor doctor = new Doctor();

                doctor.Nombre = txtnombre1.Text;
                doctor.Apellidos = txtapellidos2.Text;
                doctor.Especialidad = txtespecialidad3.Text;
                doctor.Telefono = txttelefono4.Text;
                doctor.Correo = txtcorreo5.Text;

                bool registrado = doctorBLL.RegistrarDoctor(doctor);

                if (registrado)
                {
                    MessageBox.Show("Doctor registrado correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtnombre1.Clear();
                    txtapellidos2.Clear();
                    txtespecialidad3.Clear();
                    txttelefono4.Clear();
                    txtcorreo5.Clear();
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

        private void dgvDoctores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvDoctores.Rows[e.RowIndex];

                if (fila.DataBoundItem is Doctor doctor)
                {
                    txtnombre1.Text = doctor.Nombre;
                    txtapellidos2.Text = doctor.Apellidos;
                    txtespecialidad3.Text = doctor.Especialidad;
                    txttelefono4.Text = doctor.Telefono;
                    txtcorreo5.Text = doctor.Correo;
                }
            }
        }

        private void btnmodificar_Click(object sender, EventArgs e)
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

                Doctor doctor = new Doctor();

                doctor.IdDoctor = doctorSeleccionado.IdDoctor;
                doctor.Nombre = txtnombre1.Text;
                doctor.Apellidos = txtapellidos2.Text;
                doctor.Especialidad = txtespecialidad3.Text;
                doctor.Telefono = txttelefono4.Text;
                doctor.Correo = txtcorreo5.Text;

                bool actualizado = doctorBLL.ModificarDoctor(doctor);

                if (actualizado)
                {
                    MessageBox.Show("Doctor modificado correctamente.");

                    dgvDoctores.DataSource = null;
                    dgvDoctores.DataSource = doctorBLL.ListarDoctores();
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

                        txtnombre1.Clear();
                        txtapellidos2.Clear();
                        txtespecialidad3.Clear();
                        txttelefono4.Clear();
                        txtcorreo5.Clear();
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
