
using LogicaNegocio.Servicios;
using Modelos.Modelos;
using System;
using System.Data;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmUsuarios : Form
    {
        // Objeto para utilizar la lógica de negocio.
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        // Identificador del usuario seleccionado.
        private int idUsuarioSeleccionado = 0;

        public FrmUsuarios()
        {
            InitializeComponent();

            // Conectar los eventos de los controles.
            this.Load += FrmUsuarios_Load;

            btnGuardar.Click += btnGuardar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnBuscar.Click += btnBuscar_Click;

            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        // =====================================================
        // 1. CARGAR EL FORMULARIO
        // =====================================================
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarTabla();
                CargarRoles();
                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el formulario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // 2. CONFIGURAR LA TABLA DE USUARIOS
        // =====================================================
        private void ConfigurarTabla()
        {
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvUsuarios.RowHeadersVisible = false;
        }

        // =====================================================
        // 3. CARGAR LOS ROLES DESDE MYSQL
        // =====================================================
        private void CargarRoles()
        {
            DataTable roles = usuarioBLL.ListarRoles();

            cmbRol.DataSource = roles;
            cmbRol.DisplayMember = "nombre";
            cmbRol.ValueMember = "id_rol";
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbRol.SelectedIndex = -1;
        }

        // =====================================================
        // 4. CARGAR TODOS LOS USUARIOS
        // =====================================================
        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = usuarioBLL.ListarUsuarios();

            if (dgvUsuarios.Columns.Contains("ID"))
            {
                dgvUsuarios.Columns["ID"].Visible = false;
            }

            if (dgvUsuarios.Columns.Contains("Estado"))
            {
                dgvUsuarios.Columns["Estado"].HeaderText = "Estado";
            }

            dgvUsuarios.ClearSelection();
        }

        // =====================================================
        // 5. REGISTRAR UN NUEVO USUARIO
        // =====================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("Ingrese el nombre completo.");

                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                    throw new Exception("Ingrese el nombre de usuario.");

                if (string.IsNullOrWhiteSpace(txtContrasena.Text))
                    throw new Exception("Ingrese una contraseña.");

                if (txtContrasena.Text != txtConfirmarContrasena.Text)
                    throw new Exception("Las contraseñas no coinciden.");

                if (cmbRol.SelectedIndex < 0)
                    throw new Exception("Seleccione un rol.");

                Usuario usuario = new Usuario();

                usuario.Nombre = txtNombre.Text.Trim();
                usuario.NombreUsuario = txtUsuario.Text.Trim();
                usuario.Correo = txtCorreo.Text.Trim();
                usuario.IdRol = Convert.ToInt32(cmbRol.SelectedValue);

                // Registrar mediante la lógica de negocio.
                usuarioBLL.Registrar(usuario, txtContrasena.Text);

                MessageBox.Show(
                    "Usuario registrado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // 6. MODIFICAR UN USUARIO
        // =====================================================
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idUsuarioSeleccionado <= 0)
                    throw new Exception("Seleccione un usuario de la tabla.");

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new Exception("Ingrese el nombre completo.");

                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                    throw new Exception("Ingrese el nombre de usuario.");

                if (cmbRol.SelectedIndex < 0)
                    throw new Exception("Seleccione un rol.");

                Usuario usuario = new Usuario();

                usuario.IdUsuario = idUsuarioSeleccionado;
                usuario.Nombre = txtNombre.Text.Trim();
                usuario.NombreUsuario = txtUsuario.Text.Trim();
                usuario.Correo = txtCorreo.Text.Trim();
                usuario.IdRol = Convert.ToInt32(cmbRol.SelectedValue);
                usuario.Estado = chkEstado.Checked;

                // Actualizar los datos mediante la lógica de negocio.
                usuarioBLL.Modificar(usuario);

                MessageBox.Show(
                    "Usuario modificado correctamente.",
                    "Modificación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarUsuarios();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // 7. DESACTIVAR UN USUARIO
        // =====================================================
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idUsuarioSeleccionado <= 0)
                    throw new Exception("Seleccione un usuario de la tabla.");

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea desactivar este usuario?",
                    "Confirmar desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    usuarioBLL.Desactivar(idUsuarioSeleccionado);

                    MessageBox.Show(
                        "Usuario desactivado correctamente.",
                        "Operación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarUsuarios();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // 8. SELECCIONAR UN USUARIO DE LA TABLA
        // =====================================================
        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.CurrentRow == null)
                    return;

                DataRowView fila =
                    dgvUsuarios.CurrentRow.DataBoundItem as DataRowView;

                if (fila == null)
                    return;

                idUsuarioSeleccionado = Convert.ToInt32(fila["ID"]);

                txtNombre.Text = fila["Nombre"].ToString();
                txtUsuario.Text = fila["Usuario"].ToString();
                txtCorreo.Text = fila["Correo"].ToString();

                txtContrasena.Clear();
                txtConfirmarContrasena.Clear();

                // Buscar el ID del rol en la lista cargada.
                DataTable roles = cmbRol.DataSource as DataTable;

                if (roles != null)
                {
                    foreach (DataRow rol in roles.Rows)
                    {
                        if (rol["nombre"].ToString() == fila["Rol"].ToString())
                        {
                            cmbRol.SelectedValue = Convert.ToInt32(rol["id_rol"]);
                            break;
                        }
                    }
                }

                chkEstado.Checked = Convert.ToBoolean(fila["Estado"]);
            }
            catch
            {
                // Evitar errores durante la carga inicial.
            }
        }

        // =====================================================
        // 9. BUSCAR USUARIOS
        // =====================================================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvUsuarios.DataSource =
                    usuarioBLL.BuscarUsuarios(txtBuscar.Text.Trim());

                if (dgvUsuarios.Columns.Contains("ID"))
                    dgvUsuarios.Columns["ID"].Visible = false;

                dgvUsuarios.ClearSelection();
                idUsuarioSeleccionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error de búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // 10. BUSCAR AUTOMÁTICAMENTE AL ESCRIBIR
        // =====================================================
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvUsuarios.DataSource =
                    usuarioBLL.BuscarUsuarios(txtBuscar.Text.Trim());

                if (dgvUsuarios.Columns.Contains("ID"))
                    dgvUsuarios.Columns["ID"].Visible = false;

                idUsuarioSeleccionado = 0;
            }
            catch
            {
                // La búsqueda se puede volver a intentar.
            }
        }

        // =====================================================
        // 11. ACTUALIZAR LA TABLA
        // =====================================================
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                txtBuscar.Clear();
                CargarUsuarios();
                LimpiarCampos();

                MessageBox.Show(
                    "Lista de usuarios actualizada.",
                    "Actualización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // 12. LIMPIAR LOS CAMPOS
        // =====================================================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            idUsuarioSeleccionado = 0;

            txtNombre.Clear();
            txtUsuario.Clear();
            txtCorreo.Clear();
            txtContrasena.Clear();
            txtConfirmarContrasena.Clear();

            cmbRol.SelectedIndex = -1;

            chkEstado.Checked = true;

            dgvUsuarios.ClearSelection();

            txtNombre.Focus();
        }
    }
}
