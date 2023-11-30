namespace POS.Formularios
{
    partial class frmCajaReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCajaReporte));
            this.panelCaja = new System.Windows.Forms.Panel();
            this.panelInferiorCaja = new System.Windows.Forms.Panel();
            this.dataGridViewCaja = new System.Windows.Forms.DataGridView();
            this.groupBoxCaja = new System.Windows.Forms.GroupBox();
            this.gunaGradient2Panel1 = new Guna.UI.WinForms.GunaGradient2Panel();
            this.lblPagina = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel6 = new Guna.UI.WinForms.GunaLabel();
            this.gunaGradient2Panel3 = new Guna.UI.WinForms.GunaGradient2Panel();
            this.lblIdCaja = new Guna.UI.WinForms.GunaLabel();
            this.btnSiguiente = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnAnterior = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnImprimir = new Guna.UI.WinForms.GunaAdvenceButton();
            this.panelCaja.SuspendLayout();
            this.panelInferiorCaja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCaja)).BeginInit();
            this.groupBoxCaja.SuspendLayout();
            this.gunaGradient2Panel1.SuspendLayout();
            this.gunaGradient2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCaja
            // 
            this.panelCaja.Controls.Add(this.panelInferiorCaja);
            this.panelCaja.Controls.Add(this.groupBoxCaja);
            this.panelCaja.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCaja.Location = new System.Drawing.Point(0, 0);
            this.panelCaja.Name = "panelCaja";
            this.panelCaja.Size = new System.Drawing.Size(1152, 800);
            this.panelCaja.TabIndex = 0;
            // 
            // panelInferiorCaja
            // 
            this.panelInferiorCaja.BackColor = System.Drawing.Color.Transparent;
            this.panelInferiorCaja.Controls.Add(this.dataGridViewCaja);
            this.panelInferiorCaja.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInferiorCaja.Location = new System.Drawing.Point(0, 103);
            this.panelInferiorCaja.Name = "panelInferiorCaja";
            this.panelInferiorCaja.Size = new System.Drawing.Size(1152, 697);
            this.panelInferiorCaja.TabIndex = 84;
            // 
            // dataGridViewCaja
            // 
            this.dataGridViewCaja.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCaja.Location = new System.Drawing.Point(90, 180);
            this.dataGridViewCaja.MultiSelect = false;
            this.dataGridViewCaja.Name = "dataGridViewCaja";
            this.dataGridViewCaja.ReadOnly = true;
            this.dataGridViewCaja.Size = new System.Drawing.Size(923, 118);
            this.dataGridViewCaja.TabIndex = 58;
            this.dataGridViewCaja.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCaja_CellClick);
            this.dataGridViewCaja.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCaja_CellEnter);
            this.dataGridViewCaja.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewCaja_CellPainting);
            this.dataGridViewCaja.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewCaja_DataBindingComplete);
            this.dataGridViewCaja.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewCaja_RowPrePaint);
            // 
            // groupBoxCaja
            // 
            this.groupBoxCaja.Controls.Add(this.gunaGradient2Panel1);
            this.groupBoxCaja.Controls.Add(this.gunaLabel6);
            this.groupBoxCaja.Controls.Add(this.gunaGradient2Panel3);
            this.groupBoxCaja.Controls.Add(this.btnSiguiente);
            this.groupBoxCaja.Controls.Add(this.btnAnterior);
            this.groupBoxCaja.Controls.Add(this.btnImprimir);
            this.groupBoxCaja.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCaja.ForeColor = System.Drawing.Color.Gray;
            this.groupBoxCaja.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCaja.Name = "groupBoxCaja";
            this.groupBoxCaja.Size = new System.Drawing.Size(1152, 103);
            this.groupBoxCaja.TabIndex = 86;
            this.groupBoxCaja.TabStop = false;
            this.groupBoxCaja.Text = "Caja";
            // 
            // gunaGradient2Panel1
            // 
            this.gunaGradient2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradient2Panel1.Controls.Add(this.lblPagina);
            this.gunaGradient2Panel1.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gunaGradient2Panel1.GradientColor2 = System.Drawing.Color.Silver;
            this.gunaGradient2Panel1.Location = new System.Drawing.Point(900, 22);
            this.gunaGradient2Panel1.Name = "gunaGradient2Panel1";
            this.gunaGradient2Panel1.Radius = 8;
            this.gunaGradient2Panel1.Size = new System.Drawing.Size(83, 29);
            this.gunaGradient2Panel1.TabIndex = 86;
            // 
            // lblPagina
            // 
            this.lblPagina.AutoSize = true;
            this.lblPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagina.ForeColor = System.Drawing.Color.Gray;
            this.lblPagina.Location = new System.Drawing.Point(18, 5);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(52, 15);
            this.lblPagina.TabIndex = 84;
            this.lblPagina.Text = "_________";
            // 
            // gunaLabel6
            // 
            this.gunaLabel6.AutoSize = true;
            this.gunaLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel6.ForeColor = System.Drawing.Color.Gray;
            this.gunaLabel6.Location = new System.Drawing.Point(425, 43);
            this.gunaLabel6.Name = "gunaLabel6";
            this.gunaLabel6.Size = new System.Drawing.Size(120, 15);
            this.gunaLabel6.TabIndex = 60;
            this.gunaLabel6.Text = "N° Caja seleccionada:";
            // 
            // gunaGradient2Panel3
            // 
            this.gunaGradient2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradient2Panel3.Controls.Add(this.lblIdCaja);
            this.gunaGradient2Panel3.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gunaGradient2Panel3.GradientColor2 = System.Drawing.Color.Silver;
            this.gunaGradient2Panel3.Location = new System.Drawing.Point(585, 34);
            this.gunaGradient2Panel3.Name = "gunaGradient2Panel3";
            this.gunaGradient2Panel3.Radius = 8;
            this.gunaGradient2Panel3.Size = new System.Drawing.Size(73, 31);
            this.gunaGradient2Panel3.TabIndex = 85;
            // 
            // lblIdCaja
            // 
            this.lblIdCaja.AutoSize = true;
            this.lblIdCaja.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblIdCaja.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblIdCaja.Location = new System.Drawing.Point(12, 3);
            this.lblIdCaja.Name = "lblIdCaja";
            this.lblIdCaja.Size = new System.Drawing.Size(52, 21);
            this.lblIdCaja.TabIndex = 80;
            this.lblIdCaja.Text = "______";
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.AnimationHoverSpeed = 0.07F;
            this.btnSiguiente.AnimationSpeed = 0.03F;
            this.btnSiguiente.BackColor = System.Drawing.Color.Transparent;
            this.btnSiguiente.BaseColor = System.Drawing.Color.Transparent;
            this.btnSiguiente.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSiguiente.BorderSize = 1;
            this.btnSiguiente.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnSiguiente.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnSiguiente.CheckedForeColor = System.Drawing.Color.White;
            this.btnSiguiente.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnSiguiente.CheckedImage")));
            this.btnSiguiente.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnSiguiente.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSiguiente.FocusedColor = System.Drawing.Color.Empty;
            this.btnSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnSiguiente.Image = null;
            this.btnSiguiente.ImageSize = new System.Drawing.Size(20, 20);
            this.btnSiguiente.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnSiguiente.Location = new System.Drawing.Point(951, 57);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnSiguiente.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnSiguiente.OnHoverForeColor = System.Drawing.Color.White;
            this.btnSiguiente.OnHoverImage = null;
            this.btnSiguiente.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnSiguiente.OnPressedColor = System.Drawing.Color.Black;
            this.btnSiguiente.Radius = 8;
            this.btnSiguiente.Size = new System.Drawing.Size(91, 29);
            this.btnSiguiente.TabIndex = 82;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.AnimationHoverSpeed = 0.07F;
            this.btnAnterior.AnimationSpeed = 0.03F;
            this.btnAnterior.BackColor = System.Drawing.Color.Transparent;
            this.btnAnterior.BaseColor = System.Drawing.Color.Transparent;
            this.btnAnterior.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnAnterior.BorderSize = 1;
            this.btnAnterior.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnAnterior.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnAnterior.CheckedForeColor = System.Drawing.Color.White;
            this.btnAnterior.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnAnterior.CheckedImage")));
            this.btnAnterior.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnAnterior.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAnterior.FocusedColor = System.Drawing.Color.Empty;
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnAnterior.Image = null;
            this.btnAnterior.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAnterior.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnAnterior.Location = new System.Drawing.Point(854, 57);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnAnterior.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAnterior.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAnterior.OnHoverImage = null;
            this.btnAnterior.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnAnterior.OnPressedColor = System.Drawing.Color.Black;
            this.btnAnterior.Radius = 8;
            this.btnAnterior.Size = new System.Drawing.Size(91, 29);
            this.btnAnterior.TabIndex = 83;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AnimationHoverSpeed = 0.07F;
            this.btnImprimir.AnimationSpeed = 0.03F;
            this.btnImprimir.BackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.BaseColor = System.Drawing.Color.Transparent;
            this.btnImprimir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnImprimir.BorderSize = 1;
            this.btnImprimir.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnImprimir.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnImprimir.CheckedForeColor = System.Drawing.Color.White;
            this.btnImprimir.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnImprimir.CheckedImage")));
            this.btnImprimir.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnImprimir.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImprimir.FocusedColor = System.Drawing.Color.Empty;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnImprimir.Image = null;
            this.btnImprimir.ImageSize = new System.Drawing.Size(20, 20);
            this.btnImprimir.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnImprimir.Location = new System.Drawing.Point(738, 34);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnImprimir.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnImprimir.OnHoverForeColor = System.Drawing.Color.White;
            this.btnImprimir.OnHoverImage = null;
            this.btnImprimir.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnImprimir.OnPressedColor = System.Drawing.Color.Black;
            this.btnImprimir.Radius = 8;
            this.btnImprimir.Size = new System.Drawing.Size(91, 29);
            this.btnImprimir.TabIndex = 74;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // frmCajaReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 800);
            this.ControlBox = false;
            this.Controls.Add(this.panelCaja);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaReporte";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = "frmCajaReporte";
            this.panelCaja.ResumeLayout(false);
            this.panelInferiorCaja.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCaja)).EndInit();
            this.groupBoxCaja.ResumeLayout(false);
            this.groupBoxCaja.PerformLayout();
            this.gunaGradient2Panel1.ResumeLayout(false);
            this.gunaGradient2Panel1.PerformLayout();
            this.gunaGradient2Panel3.ResumeLayout(false);
            this.gunaGradient2Panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCaja;
        private System.Windows.Forms.Panel panelInferiorCaja;
        private System.Windows.Forms.DataGridView dataGridViewCaja;
        private System.Windows.Forms.GroupBox groupBoxCaja;
        private Guna.UI.WinForms.GunaGradient2Panel gunaGradient2Panel1;
        private Guna.UI.WinForms.GunaLabel lblPagina;
        private Guna.UI.WinForms.GunaLabel gunaLabel6;
        private Guna.UI.WinForms.GunaGradient2Panel gunaGradient2Panel3;
        public Guna.UI.WinForms.GunaLabel lblIdCaja;
        private Guna.UI.WinForms.GunaAdvenceButton btnSiguiente;
        private Guna.UI.WinForms.GunaAdvenceButton btnAnterior;
        private Guna.UI.WinForms.GunaAdvenceButton btnImprimir;
    }
}