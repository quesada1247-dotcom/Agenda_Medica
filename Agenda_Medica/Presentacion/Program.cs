using LogicaNegocio.Servicios;
using Modelos.Modelos;
using Presentacion.Presentacion;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class Program
    {
        // Punto de entrada principal de la aplicación.
        [STAThread]
        static void Main()
        {
            // Habilitar los estilos visuales de Windows.
            Application.EnableVisualStyles();

            // Configurar la representación de texto.
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Iniciar la aplicación mostrando el formulario de Login.
            Application.Run(new FrmLogin());
        }
    }
}