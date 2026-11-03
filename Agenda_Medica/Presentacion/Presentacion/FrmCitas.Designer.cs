namespace Presentacion.Presentacion
{
    partial class FrmCitas
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
            this.txtpaciente = new System.Windows.Forms.Label();
            this.cmbPaciente = new System.Windows.Forms.ComboBox();
            this.txtdoctor = new System.Windows.Forms.Label();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.txtfecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txthora = new System.Windows.Forms.Label();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.txtmotivo = new System.Windows.Forms.Label();
            this.txtmotivo1 = new System.Windows.Forms.TextBox();
            this.btnGuardarCita = new System.Windows.Forms.Button();
            this.btnConsultarCitas = new System.Windows.Forms.Button();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.btnModificarCita = new System.Windows.Forms.Button();
            this.btnEliminarCita = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "REGISTRO DE CITAS MÉDICAS.";
            // 
            // txtpaciente
            // 
            this.txtpaciente.AutoSize = true;
            this.txtpaciente.Location = new System.Drawing.Point(50, 63);
            this.txtpaciente.Name = "txtpaciente";
            this.txtpaciente.Size = new System.Drawing.Size(49, 13);
            this.txtpaciente.TabIndex = 1;
            this.txtpaciente.Text = "Paciente";
            // 
            // cmbPaciente
            // 
            this.cmbPaciente.FormattingEnabled = true;
            this.cmbPaciente.Location = new System.Drawing.Point(53, 79);
            this.cmbPaciente.Name = "cmbPaciente";
            this.cmbPaciente.Size = new System.Drawing.Size(121, 21);
            this.cmbPaciente.TabIndex = 2;
            // 
            // txtdoctor
            // 
            this.txtdoctor.AutoSize = true;
            this.txtdoctor.Location = new System.Drawing.Point(50, 114);
            this.txtdoctor.Name = "txtdoctor";
            this.txtdoctor.Size = new System.Drawing.Size(39, 13);
            this.txtdoctor.TabIndex = 3;
            this.txtdoctor.Text = "Doctor";
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(53, 130);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(121, 21);
            this.cmbDoctor.TabIndex = 4;
            // 
            // txtfecha
            // 
            this.txtfecha.AutoSize = true;
            this.txtfecha.Location = new System.Drawing.Point(50, 164);
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.Size = new System.Drawing.Size(37, 13);
            this.txtfecha.TabIndex = 5;
            this.txtfecha.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(53, 180);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(118, 20);
            this.dtpFecha.TabIndex = 6;
            // 
            // txthora
            // 
            this.txthora.AutoSize = true;
            this.txthora.Location = new System.Drawing.Point(50, 212);
            this.txthora.Name = "txthora";
            this.txthora.Size = new System.Drawing.Size(30, 13);
            this.txthora.TabIndex = 7;
            this.txthora.Text = "Hora";
            // 
            // dtpHora
            // 
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHora.Location = new System.Drawing.Point(53, 228);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.ShowUpDown = true;
            this.dtpHora.Size = new System.Drawing.Size(118, 20);
            this.dtpHora.TabIndex = 8;
            // 
            // txtmotivo
            // 
            this.txtmotivo.AutoSize = true;
            this.txtmotivo.Location = new System.Drawing.Point(50, 264);
            this.txtmotivo.Name = "txtmotivo";
            this.txtmotivo.Size = new System.Drawing.Size(39, 13);
            this.txtmotivo.TabIndex = 9;
            this.txtmotivo.Text = "Motivo";
            // 
            // txtmotivo1
            // 
            this.txtmotivo1.Location = new System.Drawing.Point(53, 280);
            this.txtmotivo1.Name = "txtmotivo1";
            this.txtmotivo1.Size = new System.Drawing.Size(118, 20);
            this.txtmotivo1.TabIndex = 10;
            // 
            // btnGuardarCita
            // 
            this.btnGuardarCita.Location = new System.Drawing.Point(14, 329);
            this.btnGuardarCita.Name = "btnGuardarCita";
            this.btnGuardarCita.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarCita.TabIndex = 11;
            this.btnGuardarCita.Text = "Guardar";
            this.btnGuardarCita.UseVisualStyleBackColor = true;
            this.btnGuardarCita.Click += new System.EventHandler(this.btnGuardarCita_Click);
            // 
            // btnConsultarCitas
            // 
            this.btnConsultarCitas.Location = new System.Drawing.Point(115, 329);
            this.btnConsultarCitas.Name = "btnConsultarCitas";
            this.btnConsultarCitas.Size = new System.Drawing.Size(75, 23);
            this.btnConsultarCitas.TabIndex = 12;
            this.btnConsultarCitas.Text = "Consultar";
            this.btnConsultarCitas.UseVisualStyleBackColor = true;
            this.btnConsultarCitas.Click += new System.EventHandler(this.btnConsultarCitas_Click);
            // 
            // dgvCitas
            // 
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Location = new System.Drawing.Point(3, 385);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.Size = new System.Drawing.Size(391, 173);
            this.dgvCitas.TabIndex = 13;
            this.dgvCitas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCitas_CellContentClick);
            // 
            // btnModificarCita
            // 
            this.btnModificarCita.Location = new System.Drawing.Point(214, 329);
            this.btnModificarCita.Name = "btnModificarCita";
            this.btnModificarCita.Size = new System.Drawing.Size(75, 23);
            this.btnModificarCita.TabIndex = 14;
            this.btnModificarCita.Text = "Modificar";
            this.btnModificarCita.UseVisualStyleBackColor = true;
            this.btnModificarCita.Click += new System.EventHandler(this.btnModificarCita_Click);
            // 
            // btnEliminarCita
            // 
            this.btnEliminarCita.Location = new System.Drawing.Point(306, 329);
            this.btnEliminarCita.Name = "btnEliminarCita";
            this.btnEliminarCita.Size = new System.Drawing.Size(75, 23);
            this.btnEliminarCita.TabIndex = 15;
            this.btnEliminarCita.Text = "Eliminar";
            this.btnEliminarCita.UseVisualStyleBackColor = true;
            this.btnEliminarCita.Click += new System.EventHandler(this.btnEliminarCita_Click);
            // 
            // FrmCitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 564);
            this.Controls.Add(this.btnEliminarCita);
            this.Controls.Add(this.btnModificarCita);
            this.Controls.Add(this.dgvCitas);
            this.Controls.Add(this.btnConsultarCitas);
            this.Controls.Add(this.btnGuardarCita);
            this.Controls.Add(this.txtmotivo1);
            this.Controls.Add(this.txtmotivo);
            this.Controls.Add(this.dtpHora);
            this.Controls.Add(this.txthora);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.txtfecha);
            this.Controls.Add(this.cmbDoctor);
            this.Controls.Add(this.txtdoctor);
            this.Controls.Add(this.cmbPaciente);
            this.Controls.Add(this.txtpaciente);
            this.Controls.Add(this.label1);
            this.Name = "FrmCitas";
            this.Text = "FrmCitas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtpaciente;
        private System.Windows.Forms.ComboBox cmbPaciente;
        private System.Windows.Forms.Label txtdoctor;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.Label txtfecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label txthora;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.Label txtmotivo;
        private System.Windows.Forms.TextBox txtmotivo1;
        private System.Windows.Forms.Button btnGuardarCita;
        private System.Windows.Forms.Button btnConsultarCitas;
        private System.Windows.Forms.DataGridView dgvCitas;
        private System.Windows.Forms.Button btnModificarCita;
        private System.Windows.Forms.Button btnEliminarCita;
    }
}