namespace Presentacion.Presentacion
{
    partial class FrmPrincipal
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
            this.btnPacientes = new System.Windows.Forms.Button();
            this.btnDoctores = new System.Windows.Forms.Button();
            this.btnCitas = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnHorarios = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txttitulo
            // 
            this.txttitulo.AutoSize = true;
            this.txttitulo.Location = new System.Drawing.Point(9, 39);
            this.txttitulo.Name = "txttitulo";
            this.txttitulo.Size = new System.Drawing.Size(164, 13);
            this.txttitulo.TabIndex = 0;
            this.txttitulo.Text = "SISTEMA DE AGENDA MÉDICA";
            // 
            // btnPacientes
            // 
            this.btnPacientes.Location = new System.Drawing.Point(12, 112);
            this.btnPacientes.Name = "btnPacientes";
            this.btnPacientes.Size = new System.Drawing.Size(155, 29);
            this.btnPacientes.TabIndex = 1;
            this.btnPacientes.Text = "GESTIONAR PACIENTES";
            this.btnPacientes.UseVisualStyleBackColor = true;
            this.btnPacientes.Click += new System.EventHandler(this.btnPacientes_Click);
            // 
            // btnDoctores
            // 
            this.btnDoctores.Location = new System.Drawing.Point(12, 160);
            this.btnDoctores.Name = "btnDoctores";
            this.btnDoctores.Size = new System.Drawing.Size(155, 31);
            this.btnDoctores.TabIndex = 2;
            this.btnDoctores.Text = "GESTIONAR DOCTORES";
            this.btnDoctores.UseVisualStyleBackColor = true;
            this.btnDoctores.Click += new System.EventHandler(this.btnDoctores_Click);
            // 
            // btnCitas
            // 
            this.btnCitas.Location = new System.Drawing.Point(15, 210);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(155, 30);
            this.btnCitas.TabIndex = 3;
            this.btnCitas.Text = "GESTIONAR CITAS";
            this.btnCitas.UseVisualStyleBackColor = true;
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "AGENDA MÉDICA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bienvenido al sistema";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Seleccione el módulo que desea utilizar.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 419);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sistema de Agenda Médica · 2026";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Administrar pacientes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Administrar doctores";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Programar y administrar citas";
            // 
            // btnHorarios
            // 
            this.btnHorarios.Location = new System.Drawing.Point(18, 259);
            this.btnHorarios.Name = "btnHorarios";
            this.btnHorarios.Size = new System.Drawing.Size(152, 28);
            this.btnHorarios.TabIndex = 11;
            this.btnHorarios.Text = "GESTIONAR HORARIOS";
            this.btnHorarios.UseVisualStyleBackColor = true;
            this.btnHorarios.Click += new System.EventHandler(this.btnHorarios_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Gestionar disponibilidad de los doctores.";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 441);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnHorarios);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCitas);
            this.Controls.Add(this.btnDoctores);
            this.Controls.Add(this.btnPacientes);
            this.Controls.Add(this.txttitulo);
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txttitulo;
        private System.Windows.Forms.Button btnPacientes;
        private System.Windows.Forms.Button btnDoctores;
        private System.Windows.Forms.Button btnCitas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnHorarios;
        private System.Windows.Forms.Label label8;
    }
}