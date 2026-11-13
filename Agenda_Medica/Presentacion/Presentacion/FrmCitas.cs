using LogicaNegocio.Servicios;
using Modelos.Modelos;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmCitas : Form
    {
        private PacienteBLL pacienteBLL = new PacienteBLL();
        private DoctorBLL doctorBLL = new DoctorBLL();
        private CitaBLL citaBLL = new CitaBLL();
        private int idCitaSeleccionada = 0;

        public FrmCitas()
        {
            InitializeComponent();
            CargarListas();
            CargarTiposYDuraciones();
        }

        private void CargarTiposYDuraciones()
        {
            // Cargar los tipos de cita.
            cmbTipoCita.Items.Clear();
            cmbTipoCita.Items.Add("Presencial");
            cmbTipoCita.Items.Add("Virtual");
            cmbTipoCita.Items.Add("Urgencia");

            cmbTipoCita.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoCita.SelectedIndex = 0;

            // Cargar las duraciones.
            cmbDuracion.Items.Clear();
            cmbDuracion.Items.Add(30);
            cmbDuracion.Items.Add(45);
            cmbDuracion.Items.Add(60);

            cmbDuracion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDuracion.SelectedIndex = 0;
        }
        // AQUÍ VAN LOS DOS ATRIBUTOS Y EL MÉTODO
        private void CargarListas()
        {
            cmbPaciente.DataSource = pacienteBLL.ListarPacientes();
            cmbPaciente.DisplayMember = "Nombre";
            cmbPaciente.ValueMember = "IdPaciente";

            cmbDoctor.DataSource = doctorBLL.ListarDoctores();
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "IdDoctor";
        }

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
        cita.DuracionMinutos = Convert.ToInt32(cmbDuracion.SelectedItem);
        cita.Estado = "Programada";

        bool registrado = citaBLL.RegistrarCita(cita);

        if (registrado)
        {
            MessageBox.Show("Cita registrada correctamente.",
                "Registro exitoso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtmotivo1.Clear();
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

        private void btnConsultarCitas_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCitas.DataSource = null;
                dgvCitas.DataSource = citaBLL.ListarCitas();

                // Ocultar los identificadores
                dgvCitas.Columns["IdPaciente"].Visible = false;
                dgvCitas.Columns["IdDoctor"].Visible = false;

                // Cambiar los encabezados
                dgvCitas.Columns["IdCita"].HeaderText = "N.º Cita";
                dgvCitas.Columns["NombrePaciente"].HeaderText = "Paciente";
                dgvCitas.Columns["NombreDoctor"].HeaderText = "Doctor";
                dgvCitas.Columns["Fecha"].HeaderText = "Fecha";
                dgvCitas.Columns["Hora"].HeaderText = "Hora";
                dgvCitas.Columns["Motivo"].HeaderText = "Motivo";
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
        // EVENTO PARA SELECCIONAR UNA CITA Y CARGAR SUS DATOS
        private void dgvCitas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que se haya seleccionado una fila válida.
            if (e.RowIndex >= 0)
            {
                // Obtiene el ID de la cita seleccionada.
                DataGridViewRow fila = dgvCitas.Rows[e.RowIndex];

                idCitaSeleccionada = Convert.ToInt32(fila.Cells["IdCita"].Value);

                // Carga el paciente y el doctor seleccionados.
                cmbPaciente.SelectedValue = Convert.ToInt32(fila.Cells["IdPaciente"].Value);
                cmbDoctor.SelectedValue = Convert.ToInt32(fila.Cells["IdDoctor"].Value);

                // Carga la fecha y la hora de la cita.
                dtpFecha.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
                dtpHora.Value = DateTime.Today.Add((TimeSpan)fila.Cells["Hora"].Value);

                // Carga el motivo de la cita.
                txtmotivo1.Text = fila.Cells["Motivo"].Value.ToString();

                // Cargar el tipo de cita.
                cmbTipoCita.SelectedItem = fila.Cells["Tipo"].Value.ToString();

                // Cargar la duración.
                cmbDuracion.SelectedItem = Convert.ToInt32(
                    fila.Cells["DuracionMinutos"].Value);
            }
        }

        // EVENTO PARA MODIFICAR UNA CITA REGISTRADA
        private void btnModificarCita_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que se haya seleccionado una cita.
                if (idCitaSeleccionada <= 0)
                {
                    MessageBox.Show("Primero debes seleccionar una cita.");
                    return;
                }

                // Crea un objeto con los datos actualizados.
                Cita cita = new Cita();

                cita.IdCita = idCitaSeleccionada;
                cita.IdPaciente = Convert.ToInt32(cmbPaciente.SelectedValue);
                cita.IdDoctor = Convert.ToInt32(cmbDoctor.SelectedValue);
                cita.Fecha = dtpFecha.Value.Date;
                cita.Hora = dtpHora.Value.TimeOfDay;
                cita.Motivo = txtmotivo1.Text;
                cita.Tipo = cmbTipoCita.SelectedItem.ToString();
                cita.DuracionMinutos = Convert.ToInt32(cmbDuracion.SelectedItem);
                cita.Estado = "Programada";

                // Envía los cambios a la base de datos.
                bool modificado = citaBLL.ModificarCita(cita);

                if (modificado)
                {
                    MessageBox.Show("Cita modificada correctamente.");

                    // Actualiza la tabla con los datos nuevos.
                    btnConsultarCitas.PerformClick();

                    // Limpia el identificador seleccionado.
                    idCitaSeleccionada = 0;
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

        // EVENTO PARA ELIMINAR UNA CITA
        private void btnEliminarCita_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que se haya seleccionado una cita.
                if (idCitaSeleccionada <= 0)
                {
                    MessageBox.Show("Primero debes seleccionar una cita.");
                    return;
                }

                // Solicita confirmación antes de eliminar.
                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar esta cita?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Elimina la cita seleccionada.
                    bool eliminado = citaBLL.EliminarCita(idCitaSeleccionada);

                    if (eliminado)
                    {
                        MessageBox.Show("Cita eliminada correctamente.");

                        // Actualiza la tabla.
                        btnConsultarCitas.PerformClick();

                        // Reinicia el identificador seleccionado.
                        idCitaSeleccionada = 0;

                        // Limpia el motivo.
                        txtmotivo1.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la cita.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la cita: " + ex.Message);
            }
        }
    }
}
