
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
    public partial class FrmLogin : Form
    {
        // Crear el servicio encargado de validar los usuarios.
        private UsuarioBLL usuarioBLL = new UsuarioBLL(); 

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el usuario y la contraseña ingresados.
                string nombreUsuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text;

                // Validar las credenciales mediante la lógica de negocio.
                Usuario usuario = usuarioBLL.ValidarInicioSesion(
                    nombreUsuario,
                    contrasena
                );

                // Mostrar un mensaje de bienvenida.
                MessageBox.Show(
                    "Bienvenido(a), " + usuario.Nombre,
                    "Inicio de sesión correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Abrir el formulario principal del sistema.
                FrmPrincipal principal = new FrmPrincipal();

                // Cerrar el formulario de inicio cuando se cierre el principal.
                principal.FormClosed += (s, args) => this.Close();

                // Mostrar el menú principal y ocultar el inicio de sesión.
                this.Hide();
                principal.Show();
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje si las credenciales no son válidas.
                MessageBox.Show(
                    ex.Message,
                    "Error de inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Limpiar el campo de contraseña.
                txtContrasena.Clear();
                txtContrasena.Focus();
            }
        }
    }
}
