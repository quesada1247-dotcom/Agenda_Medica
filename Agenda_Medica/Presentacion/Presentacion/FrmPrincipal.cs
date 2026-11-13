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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            FrmPacientes formulario = new FrmPacientes();
            formulario.ShowDialog();
        }

        private void btnDoctores_Click(object sender, EventArgs e)
        {
            FrmDoctores formulario = new FrmDoctores();
            formulario.ShowDialog();
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            FrmCitas formulario = new FrmCitas();
            formulario.ShowDialog();
        }

        private void btnHorarios_Click(object sender, EventArgs e)
        {
            FrmHorarios formulario = new FrmHorarios();
            formulario.ShowDialog();
        }
    }
}
