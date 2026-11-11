using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LogicaNegocio.Servicios;
using Modelos.Modelos;

namespace Presentacion.Presentacion
{
    public partial class FrmHorarios : Form
    {
        // Crear los objetos para utilizar las capas de negocio.
        private readonly DoctorBLL doctorBLL = new DoctorBLL();
        private readonly HorarioMedicoBLL horarioBLL = new HorarioMedicoBLL();

        public FrmHorarios()
        {
            InitializeComponent();

            // Configurar y cargar los controles.
            cmbDoctores.FormattingEnabled = true;
            cmbDoctores.Format += cmbDoctores_Format;

            CargarDoctores();
            CargarDias();

            // Conectar el botón Consultar.
            btnConsultar.Click += btnConsultar_Click;
            // Conectar el botón Guardar.
            btnGuardar.Click += btnGuardar_Click;
            // Conectar el botón Modificar.
            btnModificar.Click += btnModificar_Click;
            // Conectar el botón Eliminar.
            btnEliminar.Click += btnEliminar_Click;

            // Configurar la tabla.
            dgvHorarios.AutoGenerateColumns = true;
        }

        // CARGAR LOS DOCTORES EN EL COMBOBOX.
        private void CargarDoctores()
        {
            try
            {
                List<Doctor> lista = doctorBLL.ListarDoctores();

                cmbDoctores.DataSource = lista;
                cmbDoctores.ValueMember = "IdDoctor";
                cmbDoctores.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los doctores: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // MOSTRAR EL NOMBRE Y LOS APELLIDOS DEL DOCTOR.
        private void cmbDoctores_Format(object sender, ListControlConvertEventArgs e)
        {
            Doctor doctor = e.ListItem as Doctor;

            if (doctor != null)
            {
                e.Value = doctor.Nombre + " " + doctor.Apellidos;
            }
        }

        // CARGAR LOS DÍAS DE LA SEMANA.
        private void CargarDias()
        {
            cmbDias.Items.Clear();

            cmbDias.Items.AddRange(new object[]
            {
                "Lunes",
                "Martes",
                "Miercoles",
                "Jueves",
                "Viernes",
                "Sabado",
                "Domingo"
            });

            cmbDias.SelectedIndex = -1;
        }

        // CONSULTAR Y MOSTRAR LOS HORARIOS MÉDICOS.
        private void CargarHorarios()
        {
            try
            {
                List<HorarioMedico> lista = horarioBLL.ListarHorarios();

                dgvHorarios.DataSource = null;
                dgvHorarios.DataSource = lista;

                dgvHorarios.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvHorarios.ReadOnly = true;
                dgvHorarios.AllowUserToAddRows = false;
                dgvHorarios.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar los horarios: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // EVENTO DEL BOTÓN CONSULTAR.
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarHorarios();
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear el objeto con los datos del formulario.
                HorarioMedico horario = new HorarioMedico();

                // Obtener el ID del doctor seleccionado.
                if (cmbDoctores.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar un doctor.");
                    return;
                }

                horario.IdDoctor = Convert.ToInt32(cmbDoctores.SelectedValue);

                // Obtener el día seleccionado.
                if (cmbDias.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar un día de la semana.");
                    return;
                }

                horario.DiaSemana = cmbDias.SelectedItem.ToString();

                // Obtener las horas del formulario.
                horario.HoraInicio = dtpHoraInicio.Value.TimeOfDay;
                horario.HoraFin = dtpHoraFin.Value.TimeOfDay;

                // Obtener el estado del horario.
                horario.Estado = chkEstado.Checked;

                // Enviar los datos a la capa de negocio.
                bool registrado = horarioBLL.RegistrarHorario(horario);

                if (registrado)
                {
                    MessageBox.Show(
                        "El horario médico se registró correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Actualizar la tabla.
                    CargarHorarios();

                    // Limpiar los campos del formulario.
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No fue posible registrar el horario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar el horario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado un horario.
                if (dgvHorarios.CurrentRow == null ||
                    dgvHorarios.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Debes seleccionar un horario de la tabla.");
                    return;
                }

                // Obtener el horario seleccionado.
                HorarioMedico horario =
                    (HorarioMedico)dgvHorarios.CurrentRow.DataBoundItem;

                // Verificar que se haya seleccionado un doctor.
                if (cmbDoctores.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar un doctor.");
                    return;
                }

                horario.IdDoctor =
                    Convert.ToInt32(cmbDoctores.SelectedValue);

                // Verificar que se haya seleccionado un día.
                if (cmbDias.SelectedIndex == -1)
                {
                    MessageBox.Show("Debes seleccionar un día.");
                    return;
                }

                horario.DiaSemana = cmbDias.SelectedItem.ToString();

                // Obtener las horas.
                horario.HoraInicio = dtpHoraInicio.Value.TimeOfDay;
                horario.HoraFin = dtpHoraFin.Value.TimeOfDay;

                // Obtener el estado.
                horario.Estado = chkEstado.Checked;

                // Enviar los cambios a la capa de negocio.
                bool modificado = horarioBLL.ModificarHorario(horario);

                if (modificado)
                {
                    MessageBox.Show(
                        "El horario se modificó correctamente.",
                        "Modificación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarHorarios();

                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No fue posible modificar el horario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al modificar el horario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado un horario.
                if (dgvHorarios.CurrentRow == null ||
                    dgvHorarios.CurrentRow.DataBoundItem == null)
                {
                    MessageBox.Show("Debes seleccionar un horario de la tabla.");
                    return;
                }

                // Obtener el horario seleccionado.
                HorarioMedico horario =
                    (HorarioMedico)dgvHorarios.CurrentRow.DataBoundItem;

                // Confirmar antes de eliminar.
                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este horario?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Eliminar el horario de la base de datos.
                    bool eliminado =
                        horarioBLL.EliminarHorario(horario.IdHorario);

                    if (eliminado)
                    {
                        MessageBox.Show(
                            "El horario se eliminó correctamente.",
                            "Eliminación exitosa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Actualizar la tabla.
                        CargarHorarios();

                        // Limpiar los campos del formulario.
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No fue posible eliminar el horario.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el horario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvHorarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado una fila válida.
                if (e.RowIndex < 0)
                    return;

                // Obtener el horario seleccionado.
                HorarioMedico horario =
                    (HorarioMedico)dgvHorarios.Rows[e.RowIndex].DataBoundItem;

                if (horario == null)
                    return;

                // Mostrar el doctor seleccionado.
                cmbDoctores.SelectedValue = horario.IdDoctor;

                // Mostrar el día de la semana.
                cmbDias.SelectedItem = horario.DiaSemana;

                // Mostrar la hora de inicio.
                dtpHoraInicio.Value = DateTime.Today.Add(horario.HoraInicio);

                // Mostrar la hora de finalización.
                dtpHoraFin.Value = DateTime.Today.Add(horario.HoraFin);

                // Mostrar el estado del horario.
                chkEstado.Checked = horario.Estado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar el horario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // LIMPIAR LOS CAMPOS DEL FORMULARIO.
        private void LimpiarCampos()
        {
            cmbDoctores.SelectedIndex = -1;
            cmbDias.SelectedIndex = -1;

            dtpHoraInicio.Value = DateTime.Today;
            dtpHoraFin.Value = DateTime.Today;

            chkEstado.Checked = true;

            dgvHorarios.ClearSelection();
        }




    }
}