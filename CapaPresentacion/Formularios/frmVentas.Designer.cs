namespace POS.Formularios
{
    partial class frmVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVentas));
            this.cboTipoVenta = new System.Windows.Forms.ComboBox();
            this.lblTipo = new Guna.UI.WinForms.GunaLabel();
            this.dateTimePickerDesde = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerHasta = new System.Windows.Forms.DateTimePicker();
            this.gunaLabel4 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel5 = new Guna.UI.WinForms.GunaLabel();
            this.panelVentaReporte = new System.Windows.Forms.Panel();
            this.panelInferior = new System.Windows.Forms.Panel();
            this.dataGridViewVentas = new System.Windows.Forms.DataGridView();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelFiltroI = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gunaGradient2Panel1 = new Guna.UI.WinForms.GunaGradient2Panel();
            this.lblPagina = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel6 = new Guna.UI.WinForms.GunaLabel();
            this.gunaGradient2Panel3 = new Guna.UI.WinForms.GunaGradient2Panel();
            this.lblIdVenta = new Guna.UI.WinForms.GunaLabel();
            this.btnTicket = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnSiguiente = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnAnterior = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnA4 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.panelFiltroS = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRestablecer = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btnDiaActual = new Guna.UI.WinForms.GunaAdvenceButton();
            this.cboMetodoPago = new System.Windows.Forms.ComboBox();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.btnMesActual = new Guna.UI.WinForms.GunaAdvenceButton();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.btnUltimaHora = new Guna.UI.WinForms.GunaAdvenceButton();
            this.lblTotal = new Guna.UI.WinForms.GunaLabel();
            this.btnFiltrar = new Guna.UI.WinForms.GunaAdvenceButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panelVentaReporte.SuspendLayout();
            this.panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).BeginInit();
            this.panelSuperior.SuspendLayout();
            this.panelFiltroI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gunaGradient2Panel1.SuspendLayout();
            this.gunaGradient2Panel3.SuspendLayout();
            this.panelFiltroS.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTipoVenta
            // 
            this.cboTipoVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.cboTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoVenta.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoVenta.ForeColor = System.Drawing.SystemColors.Window;
            this.cboTipoVenta.FormattingEnabled = true;
            this.cboTipoVenta.ItemHeight = 17;
            this.cboTipoVenta.Location = new System.Drawing.Point(338, 19);
            this.cboTipoVenta.Name = "cboTipoVenta";
            this.cboTipoVenta.Size = new System.Drawing.Size(122, 25);
            this.cboTipoVenta.TabIndex = 59;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.ForeColor = System.Drawing.Color.Gray;
            this.lblTipo.Location = new System.Drawing.Point(228, 24);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(82, 15);
            this.lblTipo.TabIndex = 60;
            this.lblTipo.Text = "Tipo de venta:";
            // 
            // dateTimePickerDesde
            // 
            this.dateTimePickerDesde.Location = new System.Drawing.Point(535, 19);
            this.dateTimePickerDesde.Name = "dateTimePickerDesde";
            this.dateTimePickerDesde.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerDesde.TabIndex = 69;
            // 
            // dateTimePickerHasta
            // 
            this.dateTimePickerHasta.Location = new System.Drawing.Point(535, 59);
            this.dateTimePickerHasta.Name = "dateTimePickerHasta";
            this.dateTimePickerHasta.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerHasta.TabIndex = 70;
            // 
            // gunaLabel4
            // 
            this.gunaLabel4.AutoSize = true;
            this.gunaLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel4.ForeColor = System.Drawing.Color.Gray;
            this.gunaLabel4.Location = new System.Drawing.Point(481, 23);
            this.gunaLabel4.Name = "gunaLabel4";
            this.gunaLabel4.Size = new System.Drawing.Size(43, 15);
            this.gunaLabel4.TabIndex = 71;
            this.gunaLabel4.Text = "Desde:";
            // 
            // gunaLabel5
            // 
            this.gunaLabel5.AutoSize = true;
            this.gunaLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel5.ForeColor = System.Drawing.Color.Gray;
            this.gunaLabel5.Location = new System.Drawing.Point(484, 64);
            this.gunaLabel5.Name = "gunaLabel5";
            this.gunaLabel5.Size = new System.Drawing.Size(40, 15);
            this.gunaLabel5.TabIndex = 72;
            this.gunaLabel5.Text = "Hasta:";
            // 
            // panelVentaReporte
            // 
            this.panelVentaReporte.Controls.Add(this.panelInferior);
            this.panelVentaReporte.Controls.Add(this.panelSuperior);
            this.panelVentaReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVentaReporte.Location = new System.Drawing.Point(0, 0);
            this.panelVentaReporte.Name = "panelVentaReporte";
            this.panelVentaReporte.Size = new System.Drawing.Size(1152, 800);
            this.panelVentaReporte.TabIndex = 0;
            // 
            // panelInferior
            // 
            this.panelInferior.BackColor = System.Drawing.Color.Transparent;
            this.panelInferior.Controls.Add(this.dataGridViewVentas);
            this.panelInferior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInferior.Location = new System.Drawing.Point(0, 245);
            this.panelInferior.Name = "panelInferior";
            this.panelInferior.Size = new System.Drawing.Size(1152, 555);
            this.panelInferior.TabIndex = 80;
            // 
            // dataGridViewVentas
            // 
            this.dataGridViewVentas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVentas.Location = new System.Drawing.Point(90, 90);
            this.dataGridViewVentas.MultiSelect = false;
            this.dataGridViewVentas.Name = "dataGridViewVentas";
            this.dataGridViewVentas.Size = new System.Drawing.Size(923, 184);
            this.dataGridViewVentas.TabIndex = 58;
            this.dataGridViewVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVentas_CellClick);
            this.dataGridViewVentas.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVentas_CellEnter);
            this.dataGridViewVentas.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewVentas_CellPainting);
            this.dataGridViewVentas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewVentas_DataBindingComplete);
            this.dataGridViewVentas.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewVentas_RowPrePaint);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.Transparent;
            this.panelSuperior.Controls.Add(this.panelFiltroI);
            this.panelSuperior.Controls.Add(this.panelFiltroS);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1152, 245);
            this.panelSuperior.TabIndex = 79;
            // 
            // panelFiltroI
            // 
            this.panelFiltroI.Controls.Add(this.groupBox1);
            this.panelFiltroI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltroI.Location = new System.Drawing.Point(0, 136);
            this.panelFiltroI.Name = "panelFiltroI";
            this.panelFiltroI.Size = new System.Drawing.Size(1152, 109);
            this.panelFiltroI.TabIndex = 86;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gunaGradient2Panel1);
            this.groupBox1.Controls.Add(this.gunaLabel6);
            this.groupBox1.Controls.Add(this.gunaGradient2Panel3);
            this.groupBox1.Controls.Add(this.btnTicket);
            this.groupBox1.Controls.Add(this.btnSiguiente);
            this.groupBox1.Controls.Add(this.btnAnterior);
            this.groupBox1.Controls.Add(this.btnA4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Gray;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1152, 109);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Venta";
            // 
            // gunaGradient2Panel1
            // 
            this.gunaGradient2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradient2Panel1.Controls.Add(this.lblPagina);
            this.gunaGradient2Panel1.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gunaGradient2Panel1.GradientColor2 = System.Drawing.Color.Silver;
            this.gunaGradient2Panel1.Location = new System.Drawing.Point(735, 24);
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
            this.gunaLabel6.Location = new System.Drawing.Point(260, 45);
            this.gunaLabel6.Name = "gunaLabel6";
            this.gunaLabel6.Size = new System.Drawing.Size(128, 15);
            this.gunaLabel6.TabIndex = 60;
            this.gunaLabel6.Text = "N° Venta seleccionada:";
            // 
            // gunaGradient2Panel3
            // 
            this.gunaGradient2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradient2Panel3.Controls.Add(this.lblIdVenta);
            this.gunaGradient2Panel3.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.gunaGradient2Panel3.GradientColor2 = System.Drawing.Color.Silver;
            this.gunaGradient2Panel3.Location = new System.Drawing.Point(420, 36);
            this.gunaGradient2Panel3.Name = "gunaGradient2Panel3";
            this.gunaGradient2Panel3.Radius = 8;
            this.gunaGradient2Panel3.Size = new System.Drawing.Size(73, 31);
            this.gunaGradient2Panel3.TabIndex = 85;
            // 
            // lblIdVenta
            // 
            this.lblIdVenta.AutoSize = true;
            this.lblIdVenta.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblIdVenta.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblIdVenta.Location = new System.Drawing.Point(12, 3);
            this.lblIdVenta.Name = "lblIdVenta";
            this.lblIdVenta.Size = new System.Drawing.Size(52, 21);
            this.lblIdVenta.TabIndex = 80;
            this.lblIdVenta.Text = "______";
            // 
            // btnTicket
            // 
            this.btnTicket.AnimationHoverSpeed = 0.07F;
            this.btnTicket.AnimationSpeed = 0.03F;
            this.btnTicket.BackColor = System.Drawing.Color.Transparent;
            this.btnTicket.BaseColor = System.Drawing.Color.Transparent;
            this.btnTicket.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnTicket.BorderSize = 1;
            this.btnTicket.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnTicket.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnTicket.CheckedForeColor = System.Drawing.Color.White;
            this.btnTicket.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnTicket.CheckedImage")));
            this.btnTicket.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnTicket.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTicket.FocusedColor = System.Drawing.Color.Empty;
            this.btnTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTicket.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnTicket.Image = null;
            this.btnTicket.ImageSize = new System.Drawing.Size(20, 20);
            this.btnTicket.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnTicket.Location = new System.Drawing.Point(579, 58);
            this.btnTicket.Name = "btnTicket";
            this.btnTicket.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnTicket.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnTicket.OnHoverForeColor = System.Drawing.Color.White;
            this.btnTicket.OnHoverImage = null;
            this.btnTicket.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnTicket.OnPressedColor = System.Drawing.Color.Black;
            this.btnTicket.Radius = 8;
            this.btnTicket.Size = new System.Drawing.Size(91, 29);
            this.btnTicket.TabIndex = 78;
            this.btnTicket.Text = "Ticket";
            this.btnTicket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnTicket.Click += new System.EventHandler(this.btnTicket_Click);
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
            this.btnSiguiente.Location = new System.Drawing.Point(786, 59);
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
            this.btnAnterior.Location = new System.Drawing.Point(689, 59);
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
            // btnA4
            // 
            this.btnA4.AnimationHoverSpeed = 0.07F;
            this.btnA4.AnimationSpeed = 0.03F;
            this.btnA4.BackColor = System.Drawing.Color.Transparent;
            this.btnA4.BaseColor = System.Drawing.Color.Transparent;
            this.btnA4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnA4.BorderSize = 1;
            this.btnA4.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnA4.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnA4.CheckedForeColor = System.Drawing.Color.White;
            this.btnA4.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnA4.CheckedImage")));
            this.btnA4.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnA4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnA4.FocusedColor = System.Drawing.Color.Empty;
            this.btnA4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnA4.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnA4.Image = null;
            this.btnA4.ImageSize = new System.Drawing.Size(20, 20);
            this.btnA4.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnA4.Location = new System.Drawing.Point(579, 19);
            this.btnA4.Name = "btnA4";
            this.btnA4.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnA4.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnA4.OnHoverForeColor = System.Drawing.Color.White;
            this.btnA4.OnHoverImage = null;
            this.btnA4.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnA4.OnPressedColor = System.Drawing.Color.Black;
            this.btnA4.Radius = 8;
            this.btnA4.Size = new System.Drawing.Size(91, 29);
            this.btnA4.TabIndex = 74;
            this.btnA4.Text = "A4";
            this.btnA4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnA4.Click += new System.EventHandler(this.btnA4_Click);
            // 
            // panelFiltroS
            // 
            this.panelFiltroS.BackColor = System.Drawing.Color.Transparent;
            this.panelFiltroS.Controls.Add(this.groupBox2);
            this.panelFiltroS.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltroS.Location = new System.Drawing.Point(0, 0);
            this.panelFiltroS.Name = "panelFiltroS";
            this.panelFiltroS.Size = new System.Drawing.Size(1152, 136);
            this.panelFiltroS.TabIndex = 85;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRestablecer);
            this.groupBox2.Controls.Add(this.lblTipo);
            this.groupBox2.Controls.Add(this.btnDiaActual);
            this.groupBox2.Controls.Add(this.gunaLabel4);
            this.groupBox2.Controls.Add(this.dateTimePickerDesde);
            this.groupBox2.Controls.Add(this.cboTipoVenta);
            this.groupBox2.Controls.Add(this.cboMetodoPago);
            this.groupBox2.Controls.Add(this.dateTimePickerHasta);
            this.groupBox2.Controls.Add(this.gunaLabel2);
            this.groupBox2.Controls.Add(this.btnMesActual);
            this.groupBox2.Controls.Add(this.gunaLabel1);
            this.groupBox2.Controls.Add(this.btnUltimaHora);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.btnFiltrar);
            this.groupBox2.Controls.Add(this.gunaLabel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1152, 136);
            this.groupBox2.TabIndex = 82;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtro de busqueda";
            // 
            // btnRestablecer
            // 
            this.btnRestablecer.AnimationHoverSpeed = 0.07F;
            this.btnRestablecer.AnimationSpeed = 0.03F;
            this.btnRestablecer.BackColor = System.Drawing.Color.Transparent;
            this.btnRestablecer.BaseColor = System.Drawing.Color.Transparent;
            this.btnRestablecer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRestablecer.BorderSize = 1;
            this.btnRestablecer.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnRestablecer.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnRestablecer.CheckedForeColor = System.Drawing.Color.White;
            this.btnRestablecer.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnRestablecer.CheckedImage")));
            this.btnRestablecer.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnRestablecer.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRestablecer.FocusedColor = System.Drawing.Color.Empty;
            this.btnRestablecer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestablecer.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnRestablecer.Image = null;
            this.btnRestablecer.ImageSize = new System.Drawing.Size(20, 20);
            this.btnRestablecer.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnRestablecer.Location = new System.Drawing.Point(865, 69);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnRestablecer.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnRestablecer.OnHoverForeColor = System.Drawing.Color.White;
            this.btnRestablecer.OnHoverImage = null;
            this.btnRestablecer.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnRestablecer.OnPressedColor = System.Drawing.Color.Black;
            this.btnRestablecer.Radius = 8;
            this.btnRestablecer.Size = new System.Drawing.Size(91, 29);
            this.btnRestablecer.TabIndex = 82;
            this.btnRestablecer.Text = "Restablecer";
            this.btnRestablecer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            // 
            // btnDiaActual
            // 
            this.btnDiaActual.AnimationHoverSpeed = 0.07F;
            this.btnDiaActual.AnimationSpeed = 0.03F;
            this.btnDiaActual.BackColor = System.Drawing.Color.Transparent;
            this.btnDiaActual.BaseColor = System.Drawing.Color.Transparent;
            this.btnDiaActual.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDiaActual.BorderSize = 1;
            this.btnDiaActual.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnDiaActual.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnDiaActual.CheckedForeColor = System.Drawing.Color.White;
            this.btnDiaActual.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnDiaActual.CheckedImage")));
            this.btnDiaActual.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnDiaActual.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDiaActual.FocusedColor = System.Drawing.Color.Empty;
            this.btnDiaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiaActual.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnDiaActual.Image = null;
            this.btnDiaActual.ImageSize = new System.Drawing.Size(20, 20);
            this.btnDiaActual.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnDiaActual.Location = new System.Drawing.Point(768, 50);
            this.btnDiaActual.Name = "btnDiaActual";
            this.btnDiaActual.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnDiaActual.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnDiaActual.OnHoverForeColor = System.Drawing.Color.White;
            this.btnDiaActual.OnHoverImage = null;
            this.btnDiaActual.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnDiaActual.OnPressedColor = System.Drawing.Color.Black;
            this.btnDiaActual.Radius = 8;
            this.btnDiaActual.Size = new System.Drawing.Size(91, 29);
            this.btnDiaActual.TabIndex = 79;
            this.btnDiaActual.Text = "Día actual";
            this.btnDiaActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnDiaActual.Click += new System.EventHandler(this.btnDiaActual_Click);
            // 
            // cboMetodoPago
            // 
            this.cboMetodoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.cboMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodoPago.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMetodoPago.ForeColor = System.Drawing.SystemColors.Window;
            this.cboMetodoPago.FormattingEnabled = true;
            this.cboMetodoPago.ItemHeight = 17;
            this.cboMetodoPago.Location = new System.Drawing.Point(338, 59);
            this.cboMetodoPago.Name = "cboMetodoPago";
            this.cboMetodoPago.Size = new System.Drawing.Size(122, 25);
            this.cboMetodoPago.TabIndex = 75;
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.ForeColor = System.Drawing.Color.Gray;
            this.gunaLabel2.Location = new System.Drawing.Point(228, 64);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(98, 15);
            this.gunaLabel2.TabIndex = 76;
            this.gunaLabel2.Text = "Método de pago:";
            // 
            // btnMesActual
            // 
            this.btnMesActual.AnimationHoverSpeed = 0.07F;
            this.btnMesActual.AnimationSpeed = 0.03F;
            this.btnMesActual.BackColor = System.Drawing.Color.Transparent;
            this.btnMesActual.BaseColor = System.Drawing.Color.Transparent;
            this.btnMesActual.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnMesActual.BorderSize = 1;
            this.btnMesActual.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnMesActual.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnMesActual.CheckedForeColor = System.Drawing.Color.White;
            this.btnMesActual.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnMesActual.CheckedImage")));
            this.btnMesActual.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnMesActual.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMesActual.FocusedColor = System.Drawing.Color.Empty;
            this.btnMesActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesActual.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnMesActual.Image = null;
            this.btnMesActual.ImageSize = new System.Drawing.Size(20, 20);
            this.btnMesActual.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnMesActual.Location = new System.Drawing.Point(768, 15);
            this.btnMesActual.Name = "btnMesActual";
            this.btnMesActual.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnMesActual.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnMesActual.OnHoverForeColor = System.Drawing.Color.White;
            this.btnMesActual.OnHoverImage = null;
            this.btnMesActual.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnMesActual.OnPressedColor = System.Drawing.Color.Black;
            this.btnMesActual.Radius = 8;
            this.btnMesActual.Size = new System.Drawing.Size(91, 29);
            this.btnMesActual.TabIndex = 78;
            this.btnMesActual.Text = "Mes actual";
            this.btnMesActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnMesActual.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel1.ForeColor = System.Drawing.Color.Gray;
            this.gunaLabel1.Location = new System.Drawing.Point(226, 99);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(153, 15);
            this.gunaLabel1.TabIndex = 80;
            this.gunaLabel1.Text = "Importe total de las ventas: ";
            // 
            // btnUltimaHora
            // 
            this.btnUltimaHora.AnimationHoverSpeed = 0.07F;
            this.btnUltimaHora.AnimationSpeed = 0.03F;
            this.btnUltimaHora.BackColor = System.Drawing.Color.Transparent;
            this.btnUltimaHora.BaseColor = System.Drawing.Color.Transparent;
            this.btnUltimaHora.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnUltimaHora.BorderSize = 1;
            this.btnUltimaHora.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnUltimaHora.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnUltimaHora.CheckedForeColor = System.Drawing.Color.White;
            this.btnUltimaHora.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnUltimaHora.CheckedImage")));
            this.btnUltimaHora.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnUltimaHora.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnUltimaHora.FocusedColor = System.Drawing.Color.Empty;
            this.btnUltimaHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUltimaHora.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnUltimaHora.Image = null;
            this.btnUltimaHora.ImageSize = new System.Drawing.Size(20, 20);
            this.btnUltimaHora.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnUltimaHora.Location = new System.Drawing.Point(768, 85);
            this.btnUltimaHora.Name = "btnUltimaHora";
            this.btnUltimaHora.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnUltimaHora.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnUltimaHora.OnHoverForeColor = System.Drawing.Color.White;
            this.btnUltimaHora.OnHoverImage = null;
            this.btnUltimaHora.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnUltimaHora.OnPressedColor = System.Drawing.Color.Black;
            this.btnUltimaHora.Radius = 8;
            this.btnUltimaHora.Size = new System.Drawing.Size(91, 29);
            this.btnUltimaHora.TabIndex = 81;
            this.btnUltimaHora.Text = "Última hora";
            this.btnUltimaHora.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnUltimaHora.Click += new System.EventHandler(this.btnUltimaHora_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTotal.Location = new System.Drawing.Point(410, 94);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(50, 21);
            this.lblTotal.TabIndex = 59;
            this.lblTotal.Text = "00.00";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.AnimationHoverSpeed = 0.07F;
            this.btnFiltrar.AnimationSpeed = 0.03F;
            this.btnFiltrar.BackColor = System.Drawing.Color.Transparent;
            this.btnFiltrar.BaseColor = System.Drawing.Color.Transparent;
            this.btnFiltrar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnFiltrar.BorderSize = 1;
            this.btnFiltrar.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnFiltrar.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnFiltrar.CheckedForeColor = System.Drawing.Color.White;
            this.btnFiltrar.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnFiltrar.CheckedImage")));
            this.btnFiltrar.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnFiltrar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFiltrar.FocusedColor = System.Drawing.Color.Empty;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnFiltrar.Image = null;
            this.btnFiltrar.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFiltrar.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnFiltrar.Location = new System.Drawing.Point(865, 34);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnFiltrar.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFiltrar.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFiltrar.OnHoverImage = null;
            this.btnFiltrar.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnFiltrar.OnPressedColor = System.Drawing.Color.Black;
            this.btnFiltrar.Radius = 8;
            this.btnFiltrar.Size = new System.Drawing.Size(91, 29);
            this.btnFiltrar.TabIndex = 74;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 800);
            this.Controls.Add(this.panelVentaReporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVentas";
            this.Text = "frmVentas";
            this.panelVentaReporte.ResumeLayout(false);
            this.panelInferior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVentas)).EndInit();
            this.panelSuperior.ResumeLayout(false);
            this.panelFiltroI.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gunaGradient2Panel1.ResumeLayout(false);
            this.gunaGradient2Panel1.PerformLayout();
            this.gunaGradient2Panel3.ResumeLayout(false);
            this.gunaGradient2Panel3.PerformLayout();
            this.panelFiltroS.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cboTipoVenta;
        private Guna.UI.WinForms.GunaLabel lblTipo;
        private System.Windows.Forms.DateTimePicker dateTimePickerDesde;
        private System.Windows.Forms.DateTimePicker dateTimePickerHasta;
        private Guna.UI.WinForms.GunaLabel gunaLabel4;
        private Guna.UI.WinForms.GunaLabel gunaLabel5;
        private System.Windows.Forms.Panel panelVentaReporte;
        private Guna.UI.WinForms.GunaAdvenceButton btnFiltrar;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private System.Windows.Forms.ComboBox cboMetodoPago;
        private Guna.UI.WinForms.GunaAdvenceButton btnMesActual;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.DataGridView dataGridViewVentas;
        private Guna.UI.WinForms.GunaAdvenceButton btnTicket;
        private Guna.UI.WinForms.GunaAdvenceButton btnA4;
        private Guna.UI.WinForms.GunaLabel gunaLabel6;
        private System.Windows.Forms.Panel panelInferior;
        private Guna.UI.WinForms.GunaAdvenceButton btnDiaActual;
        public Guna.UI.WinForms.GunaLabel lblTotal;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaAdvenceButton btnUltimaHora;
        public Guna.UI.WinForms.GunaLabel lblIdVenta;
        private Guna.UI.WinForms.GunaAdvenceButton btnAnterior;
        private Guna.UI.WinForms.GunaAdvenceButton btnSiguiente;
        private Guna.UI.WinForms.GunaLabel lblPagina;
        private System.Windows.Forms.Panel panelFiltroI;
        private System.Windows.Forms.Panel panelFiltroS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Guna.UI.WinForms.GunaGradient2Panel gunaGradient2Panel3;
        private Guna.UI.WinForms.GunaGradient2Panel gunaGradient2Panel1;
        private Guna.UI.WinForms.GunaAdvenceButton btnRestablecer;
    }
}