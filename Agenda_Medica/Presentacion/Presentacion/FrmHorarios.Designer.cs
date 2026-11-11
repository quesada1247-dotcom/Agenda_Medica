namespace Presentacion.Presentacion
{
    partial class FrmHorarios
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
            this.txttitulo = new System.Windows.Forms.Label();
            this.txtDoctor = new System.Windows.Forms.Label();
            this.cmbDoctores = new System.Windows.Forms.ComboBox();
            this.txtDia = new System.Windows.Forms.Label();
            this.cmbDias = new System.Windows.Forms.ComboBox();
            this.txtHoraInicio = new System.Windows.Forms.Label();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.txtHoraFin = new System.Windows.Forms.Label();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dgvHorarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).BeginInit();
            this.SuspendLayout();
            // 
            // txttitulo
            // 
            this.txttitulo.AutoSize = true;
            this.txttitulo.Location = new System.Drawing.Point(36, 36);
            this.txttitulo.Name = "txttitulo";
            this.txttitulo.Size = new System.Drawing.Size(193, 13);
            this.txttitulo.TabIndex = 0;
            this.txttitulo.Text = "REGISTRO DE HORARIOS MÉDICOS";
            // 
            // txtDoctor
            // 
            this.txtDoctor.AutoSize = true;
            this.txtDoctor.Location = new System.Drawing.Point(27, 95);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.Size = new System.Drawing.Size(42, 13);
            this.txtDoctor.TabIndex = 1;
            this.txtDoctor.Text = "Doctor:";
            // 
            // cmbDoctores
            // 
            this.cmbDoctores.FormattingEnabled = true;
            this.cmbDoctores.Location = new System.Drawing.Point(30, 111);
            this.cmbDoctores.Name = "cmbDoctores";
            this.cmbDoctores.Size = new System.Drawing.Size(121, 21);
            this.cmbDoctores.TabIndex = 2;
            // 
            // txtDia
            // 
            this.txtDia.AutoSize = true;
            this.txtDia.Location = new System.Drawing.Point(27, 148);
            this.txtDia.Name = "txtDia";
            this.txtDia.Size = new System.Drawing.Size(94, 13);
            this.txtDia.TabIndex = 3;
            this.txtDia.Text = "Día de la semana:";
            // 
            // cmbDias
            // 
            this.cmbDias.FormattingEnabled = true;
            this.cmbDias.Location = new System.Drawing.Point(30, 164);
            this.cmbDias.Name = "cmbDias";
            this.cmbDias.Size = new System.Drawing.Size(121, 21);
            this.cmbDias.TabIndex = 4;
            // 
            // txtHoraInicio
            // 
            this.txtHoraInicio.AutoSize = true;
            this.txtHoraInicio.Location = new System.Drawing.Point(27, 198);
            this.txtHoraInicio.Name = "txtHoraInicio";
            this.txtHoraInicio.Size = new System.Drawing.Size(75, 13);
            this.txtHoraInicio.TabIndex = 5;
            this.txtHoraInicio.Text = "Hora de inicio:";
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicio.Location = new System.Drawing.Point(30, 214);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.ShowUpDown = true;
            this.dtpHoraInicio.Size = new System.Drawing.Size(121, 20);
            this.dtpHoraInicio.TabIndex = 6;
            // 
            // txtHoraFin
            // 
            this.txtHoraFin.AutoSize = true;
            this.txtHoraFin.Location = new System.Drawing.Point(27, 250);
            this.txtHoraFin.Name = "txtHoraFin";
            this.txtHoraFin.Size = new System.Drawing.Size(103, 13);
            this.txtHoraFin.TabIndex = 7;
            this.txtHoraFin.Text = "Hora de finalización:";
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.Location = new System.Drawing.Point(30, 266);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.ShowUpDown = true;
            this.dtpHoraFin.Size = new System.Drawing.Size(121, 20);
            this.dtpHoraFin.TabIndex = 8;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.Location = new System.Drawing.Point(30, 305);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(92, 17);
            this.chkEstado.TabIndex = 9;
            this.chkEstado.Text = "Horario activo";
            this.chkEstado.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(163, 299);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(244, 299);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 11;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(163, 328);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 24);
            this.btnConsultar.TabIndex = 12;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click_1);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(243, 328);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(76, 24);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dgvHorarios
            // 
            this.dgvHorarios.AllowUserToAddRows = false;
            this.dgvHorarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHorarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHorarios.Location = new System.Drawing.Point(2, 372);
            this.dgvHorarios.Name = "dgvHorarios";
            this.dgvHorarios.ReadOnly = true;
            this.dgvHorarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHorarios.Size = new System.Drawing.Size(317, 156);
            this.dgvHorarios.TabIndex = 14;
            this.dgvHorarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHorarios_CellClick);
            // 
            // FrmHorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 540);
            this.Controls.Add(this.dgvHorarios);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.chkEstado);
            this.Controls.Add(this.dtpHoraFin);
            this.Controls.Add(this.txtHoraFin);
            this.Controls.Add(this.dtpHoraInicio);
            this.Controls.Add(this.txtHoraInicio);
            this.Controls.Add(this.cmbDias);
            this.Controls.Add(this.txtDia);
            this.Controls.Add(this.cmbDoctores);
            this.Controls.Add(this.txtDoctor);
            this.Controls.Add(this.txttitulo);
            this.Name = "FrmHorarios";
            this.Text = "FrmHorarios";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txttitulo;
        private System.Windows.Forms.Label txtDoctor;
        private System.Windows.Forms.ComboBox cmbDoctores;
        private System.Windows.Forms.Label txtDia;
        private System.Windows.Forms.ComboBox cmbDias;
        private System.Windows.Forms.Label txtHoraInicio;
        private System.Windows.Forms.DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.Label txtHoraFin;
        private System.Windows.Forms.DateTimePicker dtpHoraFin;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvHorarios;
    }
}