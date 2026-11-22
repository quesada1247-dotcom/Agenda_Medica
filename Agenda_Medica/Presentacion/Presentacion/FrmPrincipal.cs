
using Modelos.Modelos;
using System;
using System.Windows.Forms;

namespace Presentacion.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        // Usuario que inició sesión.
        private Usuario usuarioActual;

        // Constructor para el diseñador de Visual Studio.
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        // Constructor que recibe el usuario autenticado.
        public FrmPrincipal(Usuario usuario) : this()
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");

            usuarioActual = usuario;

            // Aplicar los permisos según el rol.
            AplicarPermisos();
        }

        // =====================================================
        // 1. CONFIGURAR LOS PERMISOS DEL USUARIO
        // =====================================================
        private void AplicarPermisos()
        {
            // El ID 1 corresponde al rol Administrador.
            bool esAdministrador = usuarioActual.IdRol == 1;

            // Solo el Administrador puede administrar usuarios.
            btnUsuarios.Visible = esAdministrador;
            btnUsuarios.Enabled = esAdministrador;
        }

        // =====================================================
        // 2. ABRIR PACIENTES
        // =====================================================
        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FrmPacientes formulario = new FrmPacientes();
            formulario.ShowDialog();
        }

        // =====================================================
        // 3. ABRIR DOCTORES
        // =====================================================
        private void btnDoctores_Click(object sender, EventArgs e)
        {
            FrmDoctores formulario = new FrmDoctores();
            formulario.ShowDialog();
        }

        // =====================================================
        // 4. ABRIR CITAS
        // =====================================================
        private void btnCitas_Click(object sender, EventArgs e)
        {
            FrmCitas formulario = new FrmCitas();
            formulario.ShowDialog();
        }

        // =====================================================
        // 5. ABRIR HORARIOS
        // =====================================================
        private void btnHorarios_Click(object sender, EventArgs e)
        {
            FrmHorarios formulario = new FrmHorarios();
            formulario.ShowDialog();
        }

        // =====================================================
        // 6. ABRIR ESPECIALIDADES
        // =====================================================
        private void btnEspecialidades_Click(object sender, EventArgs e)
        {
            FrmEspecialidades formulario = new FrmEspecialidades();
            formulario.ShowDialog();
        }

        // =====================================================
        // 7. ABRIR HISTORIAL MEDICO
        // =====================================================
        private void btnHistorialMedico_Click(object sender, EventArgs e)
        {
            FrmHistorialMedico formulario = new FrmHistorialMedico();
            formulario.ShowDialog();
        }

        // =====================================================
        // 8. ABRIR AGENDA MEDICA
        // =====================================================
        private void btnAgenda_Click(object sender, EventArgs e)
        {
            FrmAgenda formulario = new FrmAgenda();
            formulario.ShowDialog();
        }

        // =====================================================
        // 9. ABRIR ADMINISTRACION DE USUARIOS
        // =====================================================
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // Verificar nuevamente los permisos.
            if (usuarioActual == null || usuarioActual.IdRol != 1)
            {
                MessageBox.Show(
                    "No tiene permisos para administrar usuarios.",
                    "Acceso restringido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            FrmUsuarios formulario = new FrmUsuarios();
            formulario.ShowDialog();
        }
        // =====================================================
        // 11. Reportes
        // =====================================================
        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes formulario = new FrmReportes();
            formulario.ShowDialog();
        }
    }
}
