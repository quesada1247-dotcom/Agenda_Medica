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
using Presentacion.Presentacion;

namespace Presentacion.Presentacion
{
    public partial class FrmPacientes : Form
        
    {
        private PacienteBLL pacienteBLL = new PacienteBLL();

        public FrmPacientes()
        {
            InitializeComponent();
        }

        private void FrmPacientes_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un objeto paciente
                Paciente paciente = new Paciente();

                // Obtener los datos del formulario
                paciente.Nombre = txtnombre1.Text;
                paciente.Apellidos = txtapellido1.Text;
                paciente.Identificacion = txtidentificacion1.Text;
                paciente.Telefono = txttelefono1.Text;
                paciente.Correo = txtcorreo1.Text;
                paciente.FechaNacimiento = dtpFechaNacimiento.Value;

                // Enviar el paciente a la logica de negocio
                bool registrado = pacienteBLL.RegistrarPaciente(paciente);

                if (registrado)
                {
                    MessageBox.Show(
                        "Paciente registrado correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el paciente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al registrar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de texto
            txtnombre1.Clear();
            txtapellido1.Clear();
            txtidentificacion1.Clear();
            txttelefono1.Clear();
            txtcorreo1.Clear();

            // Restablecer la fecha de nacimiento
            dtpFechaNacimiento.Value = DateTime.Today;

            // Colocar el cursor en el campo Nombre
            txtnombre1.Focus();
        }

        private void btnconsultar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvPacientes.DataSource = null;
                dgvPacientes.DataSource = pacienteBLL.ListarPacientes();

                if (dgvPacientes.Rows.Count == 0)
                {
                    MessageBox.Show("No hay pacientes registrados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar pacientes: " + ex.Message);
            }
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPacientes.CurrentRow == null)
                {
                    MessageBox.Show("Primero debes seleccionar un paciente.");
                    return;
                }

                Paciente pacienteSeleccionado =
                    dgvPacientes.CurrentRow.DataBoundItem as Paciente;

                if (pacienteSeleccionado == null)
                {
                    MessageBox.Show("No se pudo obtener el paciente seleccionado.");
                    return;
                }

                Paciente paciente = new Paciente();

                paciente.IdPaciente = pacienteSeleccionado.IdPaciente;
                paciente.Nombre = txtnombre1.Text;
                paciente.Apellidos = txtapellido1.Text;
                paciente.Identificacion = txtidentificacion1.Text;
                paciente.Telefono = txttelefono1.Text;
                paciente.Correo = txtcorreo1.Text;
                paciente.FechaNacimiento = dtpFechaNacimiento.Value;

                bool actualizado = pacienteBLL.ModificarPaciente(paciente);

                if (actualizado)
                {
                    MessageBox.Show("Paciente modificado correctamente.");

                    dgvPacientes.DataSource = null;
                    dgvPacientes.DataSource = pacienteBLL.ListarPacientes();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el paciente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void dgvPacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvPacientes.Rows[e.RowIndex];

                if (fila.DataBoundItem is Paciente paciente)
                {
                    txtnombre1.Text = paciente.Nombre;
                    txtapellido1.Text = paciente.Apellidos;
                    txtidentificacion1.Text = paciente.Identificacion;
                    txttelefono1.Text = paciente.Telefono;
                    txtcorreo1.Text = paciente.Correo;
                    dtpFechaNacimiento.Value = paciente.FechaNacimiento;
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPacientes.CurrentRow == null)
                {
                    MessageBox.Show("Primero debes seleccionar un paciente.");
                    return;
                }

                Paciente pacienteSeleccionado =
                    dgvPacientes.CurrentRow.DataBoundItem as Paciente;

                if (pacienteSeleccionado == null)
                {
                    MessageBox.Show("No se pudo obtener el paciente seleccionado.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este paciente?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    bool eliminado = pacienteBLL.EliminarPaciente(
                        pacienteSeleccionado.IdPaciente);

                    if (eliminado)
                    {
                        MessageBox.Show("Paciente eliminado correctamente.");

                        dgvPacientes.DataSource = null;
                        dgvPacientes.DataSource = pacienteBLL.ListarPacientes();

                        txtnombre1.Clear();
                        txtapellido1.Clear();
                        txtidentificacion1.Clear();
                        txttelefono1.Clear();
                        txtcorreo1.Clear();
                        dtpFechaNacimiento.Value = DateTime.Today;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el paciente.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Verifica si el paciente tiene citas relacionadas.
                if (ex.Message.IndexOf(
                    "foreign key constraint fails",
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show(
                        "No se puede eliminar este paciente porque tiene citas médicas registradas.",
                        "Paciente con citas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "Ocurrió un error al eliminar el paciente: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
