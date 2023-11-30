namespace POS
{
    partial class UserControl2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl2));
            this.gunaLinePanel1 = new Guna.UI.WinForms.GunaLinePanel();
            this.lblPrecio = new Guna.UI.WinForms.GunaLabel();
            this.NumericUpDownC = new Guna.UI.WinForms.GunaNumeric();
            this.lblNombre = new Guna.UI.WinForms.GunaLabel();
            this.btnEliminar = new Guna.UI.WinForms.GunaAdvenceButton();
            this.lblHoraIngresada = new Guna.UI.WinForms.GunaLabel();
            this.gunaLinePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gunaLinePanel1
            // 
            this.gunaLinePanel1.BackColor = System.Drawing.Color.Transparent;
            this.gunaLinePanel1.Controls.Add(this.lblPrecio);
            this.gunaLinePanel1.Controls.Add(this.NumericUpDownC);
            this.gunaLinePanel1.Controls.Add(this.lblHoraIngresada);
            this.gunaLinePanel1.Controls.Add(this.lblNombre);
            this.gunaLinePanel1.Controls.Add(this.btnEliminar);
            this.gunaLinePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaLinePanel1.LineBottom = 1;
            this.gunaLinePanel1.LineColor = System.Drawing.Color.Silver;
            this.gunaLinePanel1.LineStyle = System.Windows.Forms.BorderStyle.None;
            this.gunaLinePanel1.Location = new System.Drawing.Point(0, 0);
            this.gunaLinePanel1.Name = "gunaLinePanel1";
            this.gunaLinePanel1.Size = new System.Drawing.Size(385, 73);
            this.gunaLinePanel1.TabIndex = 0;
            // 
            // lblPrecio
            // 
            this.lblPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.749999F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.Location = new System.Drawing.Point(327, 28);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(43, 15);
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "25.00";
            // 
            // NumericUpDownC
            // 
            this.NumericUpDownC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDownC.BackColor = System.Drawing.Color.Transparent;
            this.NumericUpDownC.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.NumericUpDownC.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.NumericUpDownC.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.NumericUpDownC.ButtonForeColor = System.Drawing.Color.White;
            this.NumericUpDownC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.NumericUpDownC.ForeColor = System.Drawing.Color.Black;
            this.NumericUpDownC.Location = new System.Drawing.Point(255, 19);
            this.NumericUpDownC.Maximum = ((long)(9999999));
            this.NumericUpDownC.Minimum = ((long)(0));
            this.NumericUpDownC.Name = "NumericUpDownC";
            this.NumericUpDownC.Radius = 4;
            this.NumericUpDownC.Size = new System.Drawing.Size(61, 30);
            this.NumericUpDownC.TabIndex = 3;
            this.NumericUpDownC.Text = "gunaNumeric1";
            this.NumericUpDownC.Value = ((long)(0));
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(54, 14);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(168, 16);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Pasta in Tomato Sauce";
            // 
            // btnEliminar
            // 
            this.btnEliminar.AnimationHoverSpeed = 0.07F;
            this.btnEliminar.AnimationSpeed = 0.03F;
            this.btnEliminar.BaseColor = System.Drawing.Color.Transparent;
            this.btnEliminar.BorderColor = System.Drawing.Color.Black;
            this.btnEliminar.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnEliminar.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnEliminar.CheckedForeColor = System.Drawing.Color.White;
            this.btnEliminar.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.CheckedImage")));
            this.btnEliminar.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEliminar.FocusedColor = System.Drawing.Color.Empty;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageSize = new System.Drawing.Size(20, 20);
            this.btnEliminar.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnEliminar.Location = new System.Drawing.Point(12, 23);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.OnHoverBaseColor = System.Drawing.Color.Transparent;
            this.btnEliminar.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnEliminar.OnHoverForeColor = System.Drawing.Color.White;
            this.btnEliminar.OnHoverImage = null;
            this.btnEliminar.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnEliminar.OnPressedColor = System.Drawing.Color.Black;
            this.btnEliminar.Size = new System.Drawing.Size(33, 26);
            this.btnEliminar.TabIndex = 0;
            this.btnEliminar.Text = "gunaAdvenceButton1";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblHoraIngresada
            // 
            this.lblHoraIngresada.AutoSize = true;
            this.lblHoraIngresada.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblHoraIngresada.ForeColor = System.Drawing.Color.Gray;
            this.lblHoraIngresada.Location = new System.Drawing.Point(53, 38);
            this.lblHoraIngresada.Name = "lblHoraIngresada";
            this.lblHoraIngresada.Size = new System.Drawing.Size(45, 13);
            this.lblHoraIngresada.TabIndex = 2;
            this.lblHoraIngresada.Text = "@22:00";
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gunaLinePanel1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(385, 73);
            this.gunaLinePanel1.ResumeLayout(false);
            this.gunaLinePanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI.WinForms.GunaLinePanel gunaLinePanel1;
        private Guna.UI.WinForms.GunaNumeric NumericUpDownC;
        private Guna.UI.WinForms.GunaLabel lblNombre;
        private Guna.UI.WinForms.GunaAdvenceButton btnEliminar;
        private Guna.UI.WinForms.GunaLabel lblPrecio;
        private Guna.UI.WinForms.GunaLabel lblHoraIngresada;
    }
}
