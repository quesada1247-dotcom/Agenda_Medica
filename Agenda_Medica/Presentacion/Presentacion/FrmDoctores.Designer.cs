namespace Presentacion.Presentacion
{
    partial class FrmDoctores
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
            this.txtapellidos = new System.Windows.Forms.Label();
            this.txtapellidos2 = new System.Windows.Forms.TextBox();
            this.txtespecialidad = new System.Windows.Forms.Label();
            this.txttelefono = new System.Windows.Forms.Label();
            this.txttelefono4 = new System.Windows.Forms.TextBox();
            this.txtcorreo = new System.Windows.Forms.Label();
            this.txtcorreo5 = new System.Windows.Forms.TextBox();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btnconsultar = new System.Windows.Forms.Button();
            this.btnmodificar = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();
            this.dgvDoctores = new System.Windows.Forms.DataGridView();
            this.cmbEspecialidad = new System.Windows.Forms.ComboBox();
            this.txtidentificacion = new System.Windows.Forms.Label();
            this.txtidentificacion6 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctores)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "REGISTRO DE DOCTORES";
            // 
            // txtnombre
            // 
            this.txtnombre.AutoSize = true;
            this.txtnombre.Location = new System.Drawing.Point(30, 92);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(47, 13);
            this.txtnombre.TabIndex = 1;
            this.txtnombre.Text = "Nombre:";
            // 
            // txtnombre1
            // 
            this.txtnombre1.Location = new System.Drawing.Point(33, 108);
            this.txtnombre1.Name = "txtnombre1";
            this.txtnombre1.Size = new System.Drawing.Size(100, 20);
            this.txtnombre1.TabIndex = 2;
            // 
            // txtapellidos
            // 
            this.txtapellidos.AutoSize = true;
            this.txtapellidos.Location = new System.Drawing.Point(199, 92);
            this.txtapellidos.Name = "txtapellidos";
            this.txtapellidos.Size = new System.Drawing.Size(52, 13);
            this.txtapellidos.TabIndex = 3;
            this.txtapellidos.Text = "Apellidos:";
            // 
            // txtapellidos2
            // 
            this.txtapellidos2.Location = new System.Drawing.Point(202, 108);
            this.txtapellidos2.Name = "txtapellidos2";
            this.txtapellidos2.Size = new System.Drawing.Size(100, 20);
            this.txtapellidos2.TabIndex = 4;
            // 
            // txtespecialidad
            // 
            this.txtespecialidad.AutoSize = true;
            this.txtespecialidad.Location = new System.Drawing.Point(30, 157);
            this.txtespecialidad.Name = "txtespecialidad";
            this.txtespecialidad.Size = new System.Drawing.Size(70, 13);
            this.txtespecialidad.TabIndex = 5;
            this.txtespecialidad.Text = "Especialidad:";
            // 
            // txttelefono
            // 
            this.txttelefono.AutoSize = true;
            this.txttelefono.Location = new System.Drawing.Point(199, 157);
            this.txttelefono.Name = "txttelefono";
            this.txttelefono.Size = new System.Drawing.Size(52, 13);
            this.txttelefono.TabIndex = 7;
            this.txttelefono.Text = "Teléfono:";
            // 
            // txttelefono4
            // 
            this.txttelefono4.Location = new System.Drawing.Point(202, 173);
            this.txttelefono4.Name = "txttelefono4";
            this.txttelefono4.Size = new System.Drawing.Size(100, 20);
            this.txttelefono4.TabIndex = 8;
            // 
            // txtcorreo
            // 
            this.txtcorreo.AutoSize = true;
            this.txtcorreo.Location = new System.Drawing.Point(30, 209);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(41, 13);
            this.txtcorreo.TabIndex = 9;
            this.txtcorreo.Text = "Correo:";
            // 
            // txtcorreo5
            // 
            this.txtcorreo5.Location = new System.Drawing.Point(33, 225);
            this.txtcorreo5.Name = "txtcorreo5";
            this.txtcorreo5.Size = new System.Drawing.Size(100, 20);
            this.txtcorreo5.TabIndex = 10;
            // 
            // btnguardar
            // 
            this.btnguardar.Location = new System.Drawing.Point(13, 275);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(64, 27);
            this.btnguardar.TabIndex = 11;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.UseVisualStyleBackColor = true;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnconsultar
            // 
            this.btnconsultar.Location = new System.Drawing.Point(202, 275);
            this.btnconsultar.Name = "btnconsultar";
            this.btnconsultar.Size = new System.Drawing.Size(65, 27);
            this.btnconsultar.TabIndex = 13;
            this.btnconsultar.Text = "Consultar";
            this.btnconsultar.UseVisualStyleBackColor = true;
            this.btnconsultar.Click += new System.EventHandler(this.btnconsultar_Click);
            // 
            // btnmodificar
            // 
            this.btnmodificar.Location = new System.Drawing.Point(108, 275);
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.Size = new System.Drawing.Size(75, 27);
            this.btnmodificar.TabIndex = 14;
            this.btnmodificar.Text = "Modificar";
            this.btnmodificar.UseVisualStyleBackColor = true;
            this.btnmodificar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(108, 320);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(75, 27);
            this.btneliminar.TabIndex = 15;
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // dgvDoctores
            // 
            this.dgvDoctores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoctores.Location = new System.Drawing.Point(11, 370);
            this.dgvDoctores.Name = "dgvDoctores";
            this.dgvDoctores.Size = new System.Drawing.Size(304, 142);
            this.dgvDoctores.TabIndex = 16;
            this.dgvDoctores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoctores_CellClick);
            // 
            // cmbEspecialidad
            // 
            this.cmbEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEspecialidad.FormattingEnabled = true;
            this.cmbEspecialidad.Location = new System.Drawing.Point(33, 173);
            this.cmbEspecialidad.Name = "cmbEspecialidad";
            this.cmbEspecialidad.Size = new System.Drawing.Size(110, 21);
            this.cmbEspecialidad.TabIndex = 17;
            // 
            // txtidentificacion
            // 
            this.txtidentificacion.AutoSize = true;
            this.txtidentificacion.Location = new System.Drawing.Point(199, 209);
            this.txtidentificacion.Name = "txtidentificacion";
            this.txtidentificacion.Size = new System.Drawing.Size(73, 13);
            this.txtidentificacion.TabIndex = 18;
            this.txtidentificacion.Text = "Identificación.";
            // 
            // txtidentificacion6
            // 
            this.txtidentificacion6.Location = new System.Drawing.Point(202, 225);
            this.txtidentificacion6.Name = "txtidentificacion6";
            this.txtidentificacion6.Size = new System.Drawing.Size(100, 20);
            this.txtidentificacion6.TabIndex = 19;
            // 
            // FrmDoctores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 524);
            this.Controls.Add(this.txtidentificacion6);
            this.Controls.Add(this.txtidentificacion);
            this.Controls.Add(this.cmbEspecialidad);
            this.Controls.Add(this.dgvDoctores);
            this.Controls.Add(this.btneliminar);
            this.Controls.Add(this.btnmodificar);
            this.Controls.Add(this.btnconsultar);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.txtcorreo5);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.txttelefono4);
            this.Controls.Add(this.txttelefono);
            this.Controls.Add(this.txtespecialidad);
            this.Controls.Add(this.txtapellidos2);
            this.Controls.Add(this.txtapellidos);
            this.Controls.Add(this.txtnombre1);
            this.Controls.Add(this.txtnombre);
            this.Controls.Add(this.label1);
            this.Name = "FrmDoctores";
            this.Text = "FrmDoctores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtnombre;
        private System.Windows.Forms.TextBox txtnombre1;
        private System.Windows.Forms.Label txtapellidos;
        private System.Windows.Forms.TextBox txtapellidos2;
        private System.Windows.Forms.Label txtespecialidad;
        private System.Windows.Forms.Label txttelefono;
        private System.Windows.Forms.TextBox txttelefono4;
        private System.Windows.Forms.Label txtcorreo;
        private System.Windows.Forms.TextBox txtcorreo5;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Button btnconsultar;
        private System.Windows.Forms.Button btnmodificar;
        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.DataGridView dgvDoctores;
        private System.Windows.Forms.ComboBox cmbEspecialidad;
        private System.Windows.Forms.Label txtidentificacion;
        private System.Windows.Forms.TextBox txtidentificacion6;
    }
}