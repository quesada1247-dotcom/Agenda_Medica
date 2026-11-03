namespace Presentacion.Presentacion
{
    partial class FrmPacientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.Label();
            this.txtnombre1 = new System.Windows.Forms.TextBox();
            this.txtapellido = new System.Windows.Forms.Label();
            this.txtapellido1 = new System.Windows.Forms.TextBox();
            this.txtindentificacion = new System.Windows.Forms.Label();
            this.txtidentificacion1 = new System.Windows.Forms.TextBox();
            this.txttelefono = new System.Windows.Forms.Label();
            this.txttelefono1 = new System.Windows.Forms.TextBox();
            this.txtcorreo = new System.Windows.Forms.Label();
            this.txtcorreo1 = new System.Windows.Forms.TextBox();
            this.txtfechanacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btnlimpiar = new System.Windows.Forms.Button();
            this.btnconsultar = new System.Windows.Forms.Button();
            this.dgvPacientes = new System.Windows.Forms.DataGridView();
            this.btnmodificar = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "REGISTRO DE PACIENTES";
            // 
            // txtnombre
            // 
            this.txtnombre.AutoSize = true;
            this.txtnombre.Location = new System.Drawing.Point(24, 99);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(44, 13);
            this.txtnombre.TabIndex = 1;
            this.txtnombre.Text = "Nombre";
            this.txtnombre.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtnombre1
            // 
            this.txtnombre1.Location = new System.Drawing.Point(3, 125);
            this.txtnombre1.Name = "txtnombre1";
            this.txtnombre1.Size = new System.Drawing.Size(100, 20);
            this.txtnombre1.TabIndex = 2;
            // 
            // txtapellido
            // 
            this.txtapellido.AutoSize = true;
            this.txtapellido.Location = new System.Drawing.Point(216, 99);
            this.txtapellido.Name = "txtapellido";
            this.txtapellido.Size = new System.Drawing.Size(49, 13);
            this.txtapellido.TabIndex = 3;
            this.txtapellido.Text = "Apellidos";
            // 
            // txtapellido1
            // 
            this.txtapellido1.Location = new System.Drawing.Point(190, 125);
            this.txtapellido1.Name = "txtapellido1";
            this.txtapellido1.Size = new System.Drawing.Size(100, 20);
            this.txtapellido1.TabIndex = 4;
            // 
            // txtindentificacion
            // 
            this.txtindentificacion.AutoSize = true;
            this.txtindentificacion.Location = new System.Drawing.Point(12, 176);
            this.txtindentificacion.Name = "txtindentificacion";
            this.txtindentificacion.Size = new System.Drawing.Size(70, 13);
            this.txtindentificacion.TabIndex = 5;
            this.txtindentificacion.Text = "Identificación";
            // 
            // txtidentificacion1
            // 
            this.txtidentificacion1.Location = new System.Drawing.Point(3, 204);
            this.txtidentificacion1.Name = "txtidentificacion1";
            this.txtidentificacion1.Size = new System.Drawing.Size(100, 20);
            this.txtidentificacion1.TabIndex = 6;
            // 
            // txttelefono
            // 
            this.txttelefono.AutoSize = true;
            this.txttelefono.Location = new System.Drawing.Point(216, 176);
            this.txttelefono.Name = "txttelefono";
            this.txttelefono.Size = new System.Drawing.Size(49, 13);
            this.txttelefono.TabIndex = 7;
            this.txttelefono.Text = "Teléfono";
            // 
            // txttelefono1
            // 
            this.txttelefono1.Location = new System.Drawing.Point(190, 204);
            this.txttelefono1.Name = "txttelefono1";
            this.txttelefono1.Size = new System.Drawing.Size(100, 20);
            this.txttelefono1.TabIndex = 8;
            // 
            // txtcorreo
            // 
            this.txtcorreo.AutoSize = true;
            this.txtcorreo.Location = new System.Drawing.Point(24, 261);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(38, 13);
            this.txtcorreo.TabIndex = 9;
            this.txtcorreo.Text = "Correo";
            // 
            // txtcorreo1
            // 
            this.txtcorreo1.Location = new System.Drawing.Point(3, 288);
            this.txtcorreo1.Name = "txtcorreo1";
            this.txtcorreo1.Size = new System.Drawing.Size(100, 20);
            this.txtcorreo1.TabIndex = 10;
            // 
            // txtfechanacimiento
            // 
            this.txtfechanacimiento.AutoSize = true;
            this.txtfechanacimiento.Location = new System.Drawing.Point(187, 261);
            this.txtfechanacimiento.Name = "txtfechanacimiento";
            this.txtfechanacimiento.Size = new System.Drawing.Size(106, 13);
            this.txtfechanacimiento.TabIndex = 11;
            this.txtfechanacimiento.Text = "Fecha de nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(180, 285);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(123, 20);
            this.dtpFechaNacimiento.TabIndex = 12;
            // 
            // btnguardar
            // 
            this.btnguardar.Location = new System.Drawing.Point(27, 353);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(62, 23);
            this.btnguardar.TabIndex = 13;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.UseVisualStyleBackColor = true;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnlimpiar
            // 
            this.btnlimpiar.Location = new System.Drawing.Point(118, 353);
            this.btnlimpiar.Name = "btnlimpiar";
            this.btnlimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnlimpiar.TabIndex = 14;
            this.btnlimpiar.Text = "Limpiar";
            this.btnlimpiar.UseVisualStyleBackColor = true;
            this.btnlimpiar.Click += new System.EventHandler(this.btnlimpiar_Click);
            // 
            // btnconsultar
            // 
            this.btnconsultar.Location = new System.Drawing.Point(219, 353);
            this.btnconsultar.Name = "btnconsultar";
            this.btnconsultar.Size = new System.Drawing.Size(75, 23);
            this.btnconsultar.TabIndex = 15;
            this.btnconsultar.Text = "Consultar";
            this.btnconsultar.UseVisualStyleBackColor = true;
            this.btnconsultar.Click += new System.EventHandler(this.btnconsultar_Click);
            // 
            // dgvPacientes
            // 
            this.dgvPacientes.AllowUserToAddRows = false;
            this.dgvPacientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacientes.Location = new System.Drawing.Point(5, 442);
            this.dgvPacientes.Name = "dgvPacientes";
            this.dgvPacientes.ReadOnly = true;
            this.dgvPacientes.Size = new System.Drawing.Size(310, 120);
            this.dgvPacientes.TabIndex = 16;
            this.dgvPacientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPacientes_CellClick);
            // 
            // btnmodificar
            // 
            this.btnmodificar.Location = new System.Drawing.Point(77, 392);
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.Size = new System.Drawing.Size(61, 23);
            this.btnmodificar.TabIndex = 17;
            this.btnmodificar.Text = "Modificar";
            this.btnmodificar.UseVisualStyleBackColor = true;
            this.btnmodificar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(180, 392);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(75, 23);
            this.btneliminar.TabIndex = 18;
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // FrmPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 574);
            this.Controls.Add(this.btneliminar);
            this.Controls.Add(this.btnmodificar);
            this.Controls.Add(this.dgvPacientes);
            this.Controls.Add(this.btnconsultar);
            this.Controls.Add(this.btnlimpiar);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.txtfechanacimiento);
            this.Controls.Add(this.txtcorreo1);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.txttelefono1);
            this.Controls.Add(this.txttelefono);
            this.Controls.Add(this.txtidentificacion1);
            this.Controls.Add(this.txtindentificacion);
            this.Controls.Add(this.txtapellido1);
            this.Controls.Add(this.txtapellido);
            this.Controls.Add(this.txtnombre1);
            this.Controls.Add(this.txtnombre);
            this.Controls.Add(this.label1);
            this.Name = "FrmPacientes";
            this.Text = "FrmPacientes";
            this.Load += new System.EventHandler(this.FrmPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtnombre;
        private System.Windows.Forms.TextBox txtnombre1;
        private System.Windows.Forms.Label txtapellido;
        private System.Windows.Forms.TextBox txtapellido1;
        private System.Windows.Forms.Label txtindentificacion;
        private System.Windows.Forms.TextBox txtidentificacion1;
        private System.Windows.Forms.Label txttelefono;
        private System.Windows.Forms.TextBox txttelefono1;
        private System.Windows.Forms.Label txtcorreo;
        private System.Windows.Forms.TextBox txtcorreo1;
        private System.Windows.Forms.Label txtfechanacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Button btnlimpiar;
        private System.Windows.Forms.Button btnconsultar;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.Button btnmodificar;
        private System.Windows.Forms.Button btneliminar;
    }
}