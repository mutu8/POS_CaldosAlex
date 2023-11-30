using CapaeEntidad;
using CapaLogica;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Formularios
{
    public partial class frmVentas : Form
    {
        //private string connectionString = "Server=34.176.49.57;Database=bd_caldosAlex;User Id=sqlserver;Password=@hV\"1%`(o63_/7V:;";
        private string connectionString = logConexion.Instancia.ObtenerConexion();

        private DataGridViewRow filaSeleccionada = null;

        private DataTable tablaVentasCompleta; // Almacena todos los datos originales
        private int pageSize = 10; // Tamaño de cada página
        private int currentPage = 1; // Página actual
        private int totalPages; // Número total de páginas

        private PrintDocument PD = new PrintDocument();
        private PrintPreviewDialog PPD = new PrintPreviewDialog();
        private int longpaper;

        public frmVentas()
        {
            InitializeComponent();
            PersonalizarDataGridView(dataGridViewVentas);
            PersonalizarDateTimePicker(dateTimePickerDesde);
            PersonalizarDateTimePicker(dateTimePickerHasta);
            ConfigurarDataGridView();

            dataGridViewVentas.CellFormatting += dataGridViewVentas_CellFormatting;

            PD.BeginPrint += new PrintEventHandler(PD_BeginPrint);
            PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
        }
        public Panel ObtenerPanelVenta()
        {
            return panelVentaReporte;
        }
        public ComboBox ObtenerComboBoxVenta()
        {
            return cboTipoVenta;
        }
        public ComboBox ObtenerComboBoxPago()
        {
            return cboMetodoPago;
        }
        private void ObtenerFechasMesActual(out DateTime fechaInicio, out DateTime fechaFinal)
        {
            DateTime hoy = DateTime.Today;
            fechaInicio = new DateTime(hoy.Year, hoy.Month, 1);
            fechaFinal = fechaInicio.AddMonths(1).AddDays(-1);
        }
        private void ObtenerFechasDiaActual(out DateTime fechaInicio, out DateTime fechaFinal)
        {
            DateTime hoy = DateTime.Today;
            fechaInicio = hoy.Date; // Inicio del día actual
            fechaFinal = hoy.Date.AddDays(1).AddTicks(-1); // Fin del día actual
        }
        private void ObtenerFechasUltimaHora(out DateTime fechaInicio, out DateTime fechaFinal)
        {
            DateTime ahora = DateTime.Now;

            // Obtener la hora de inicio 1 hora antes de la hora actual
            fechaInicio = ahora.AddHours(-1);

            // Obtener la hora final como la hora actual
            fechaFinal = ahora;
        }

        private void ConfigurarDataGridView()
        { // Establecer el modo de ajuste automático para las filas y columnas

            ObtenerFechasUltimaHora(out DateTime fechaInicio, out DateTime fechaFinal);

            //Llamamos a la función para cargar datos en el DataGridView
            CargarVentas(null, null, fechaInicio, fechaFinal);
            ActualizarDGV();
        }

    

        private void PersonalizarDataGridView(DataGridView dgv)
        {
            // Ajustar el tamaño de la fuente
            dgv.Font = new Font("Segoe UI", 12);
            dgv.CellPainting += dataGridViewVentas_CellPainting;
            dgv.RowPrePaint += dataGridViewVentas_RowPrePaint;

            // Establecer el estilo de fuente en negrita para el encabezado
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);

            // Personalizar el estilo
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = Color.LightGray;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
            foreach (DataGridViewColumn columna in dgv.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desactivar la posibilidad de cambiar el tamaño de filas y columnas
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;

            // Ajustar el tamaño automáticamente y centrar en el panel
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Desactivar la posibilidad de cambiar el ancho de las columnas
            foreach (DataGridViewColumn columna in dgv.Columns)
            {
                columna.Resizable = DataGridViewTriState.False;
            }

            // Deshabilitar la selección de celdas individuales y habilitar la selección de filas completas
            dgv.MultiSelect = false;
        }


        private void PersonalizarDateTimePicker(DateTimePicker dateTimePicker)
        {
            // Personalizar la apariencia del DateTimePicker
            dateTimePicker.Font = new System.Drawing.Font("Segoe UI", 12);
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd/MM/yyyy";
            dateTimePicker.ShowUpDown = true;
            dateTimePicker.BackColor = System.Drawing.Color.LightGray;
            dateTimePicker.ForeColor = System.Drawing.Color.Black;

            // Configurar el rango mínimo y máximo (si es necesario)
            dateTimePicker.MinDate = DateTime.Today.AddYears(-10);
            dateTimePicker.MaxDate = DateTime.Today.AddYears(10);
            dateTimePicker.MinDate = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePicker.MaxDate = DateTime.Today.AddYears(10);
        }


        /*
        private void CargarVentas(string tipoVenta, string metodoPago, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta para obtener los datos filtrados de la tabla Venta
                    string consulta = @"
                    SELECT V.idVenta,
                           CV.nombreCategoria AS TipoVenta,
                           MP.nombreMetPag AS MetodoPago,
                           CONVERT(VARCHAR(10), V.FechaVenta, 103) AS FechaVenta,
                           CONVERT(VARCHAR(5), V.FechaVenta, 108) AS HoraVenta, 
                           V.NombreCliente,
                           V.ApellidoCliente,
                           V.ImporteVenta
                    FROM Venta V
                        INNER JOIN CategoriaVenta CV ON V.idCategoríaVenta = CV.idCategoriaVenta
                        INNER JOIN Pedido P ON V.idPedido = P.idPedido
                        INNER JOIN MetodoPago MP ON V.idMetodoPago = MP.idMetodoPago
                        AND V.FechaVenta >= @FechaInicio
                        AND V.FechaVenta < DATEADD(MONTH, 1, @FechaInicio)
                        AND V.FechaVenta < @FechaFinal
                        AND V.FechaVenta >= @FechaInicio AND V.FechaVenta < DATEADD(DAY, 1, @FechaFinal)
                        AND (@TipoVenta IS NULL OR CV.nombreCategoria = @TipoVenta OR @TipoVenta = '')
                        AND (@MetodoPago IS NULL OR MP.nombreMetPag = @MetodoPago)
                        ORDER BY V.FechaVenta DESC";


                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        adaptador.SelectCommand.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                        adaptador.SelectCommand.Parameters.AddWithValue("@TipoVenta", tipoVenta ?? (object)DBNull.Value);
                        adaptador.SelectCommand.Parameters.AddWithValue("@MetodoPago", metodoPago ?? (object)DBNull.Value);

                        DataTable tablaVentas = new DataTable();
                        adaptador.Fill(tablaVentas);

                        // Añadir la columna ImporteVenta si no existe
                        if (!tablaVentas.Columns.Contains("ImporteVenta"))
                            tablaVentas.Columns.Add("ImporteVenta", typeof(decimal));

                        // Añadir la columna Fecha si no existe
                        if (!tablaVentas.Columns.Contains("Fecha"))
                            tablaVentas.Columns.Add("Fecha", typeof(string));

                        // Iterar a través de las filas y dividir la FechaVenta en Fecha y Hora
                        foreach (DataRow fila in tablaVentas.Rows)
                        {
                            DateTime fechaVenta = Convert.ToDateTime(fila["FechaVenta"]);
                            fila["Fecha"] = fechaVenta.ToString("dd/MM/yyyy");
                        }

                        // Remover la columna original de FechaVenta
                        tablaVentas.Columns.Remove("FechaVenta");

                        // Almacena los datos originales antes de paginar
                        tablaVentasCompleta = tablaVentas.Copy();

                        // Calcula el número total de páginas
                        totalPages = (int)Math.Ceiling((double)tablaVentasCompleta.Rows.Count / pageSize);

                        // Asignar la vista ordenada al DataGridView
                        dataGridViewVentas.DataSource = tablaVentas.DefaultView.ToTable();
                        dataGridViewVentas.ReadOnly = true;

                        // Configurar la visibilidad de las columnas
                        dataGridViewVentas.Columns["NombreCliente"].Visible = false;
                        dataGridViewVentas.Columns["ApellidoCliente"].Visible = false;
                        dataGridViewVentas.Columns["idVenta"].Visible = false;

                        // Asignar nombres específicos a las columnas
                        //dataGridViewVentas.Columns["idVenta"].HeaderText = "ID";
                        dataGridViewVentas.Columns["TipoVenta"].HeaderText = "TIPO DE COMPROBANTE";
                        dataGridViewVentas.Columns["metodoPago"].HeaderText = "FORMA DE PAGO";
                        dataGridViewVentas.Columns["HoraVenta"].HeaderText = "HORA DE VENTA";
                        dataGridViewVentas.Columns["ImporteVenta"].HeaderText = "IMPORTE DE VENTA";

                        // Configurar el orden de las columnas
                        dataGridViewVentas.Columns["Fecha"].DisplayIndex = 3;
                        
                        
                        // Ordenar las filas por la columna "Fecha" en orden descendente
                        tablaVentas.DefaultView.Sort = "Fecha DESC";
                        DataTable tablaVentasOrdenada = tablaVentas.DefaultView.ToTable();
                        decimal totalImporteVentas = CalcularTotalImporteVentas(tablaVentas);
                        lblTotal.Text=totalImporteVentas.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        */

        private void CargarVentas(string tipoVenta, string metodoPago, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta para obtener los datos filtrados de la tabla Venta
                    string consulta = @"
                    SELECT V.idVenta,
                           CV.nombreCategoria AS TipoVenta,
                           MP.nombreMetPag AS MetodoPago,
                           CONVERT(VARCHAR(10), V.FechaVenta, 103) AS FechaVenta,
                           CONVERT(VARCHAR(5), V.FechaVenta, 108) AS HoraVenta, 
                           V.NombreCliente,
                           V.ApellidoCliente,
                           V.ImporteVenta
                    FROM Venta V
                        INNER JOIN CategoriaVenta CV ON V.idCategoríaVenta = CV.idCategoriaVenta
                        INNER JOIN Pedido P ON V.idPedido = P.idPedido
                        INNER JOIN MetodoPago MP ON V.idMetodoPago = MP.idMetodoPago
                        AND V.FechaVenta >= @FechaInicio
                        AND V.FechaVenta < DATEADD(MONTH, 1, @FechaInicio)
                        AND V.FechaVenta < @FechaFinal
                        AND V.FechaVenta >= @FechaInicio AND V.FechaVenta < DATEADD(DAY, 1, @FechaFinal)
                        AND (@TipoVenta IS NULL OR CV.nombreCategoria = @TipoVenta OR @TipoVenta = '')
                        AND (@MetodoPago IS NULL OR MP.nombreMetPag = @MetodoPago)
                        ORDER BY V.FechaVenta DESC";


                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                    {
                        adaptador.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        adaptador.SelectCommand.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                        adaptador.SelectCommand.Parameters.AddWithValue("@TipoVenta", tipoVenta ?? (object)DBNull.Value);
                        adaptador.SelectCommand.Parameters.AddWithValue("@MetodoPago", metodoPago ?? (object)DBNull.Value);

                        DataTable tablaVentas = new DataTable();
                        adaptador.Fill(tablaVentas);

                        ProcesarTablaVentas(tablaVentas);

                        // Almacena los datos originales antes de paginar
                        tablaVentasCompleta = tablaVentas.Copy();

                        // Calcula el número total de páginas
                        totalPages = (int)Math.Ceiling((double)tablaVentasCompleta.Rows.Count / pageSize);

                        ConfigurarDataGridViewVentas(tablaVentas);

                        // Configurar el DataGridView con la fuente de datos
                        dataGridViewVentas.DataSource = tablaVentas;

                        // Actualizar la visualización
                        dataGridViewVentas.Refresh();

                        decimal totalImporteVentas = CalcularTotalImporteVentas(tablaVentas);
                        lblTotal.Text = totalImporteVentas.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcesarTablaVentas(DataTable tablaVentas)
        {
            // Añadir la columna ImporteVenta si no existe
            if (!tablaVentas.Columns.Contains("ImporteVenta"))
                tablaVentas.Columns.Add("ImporteVenta", typeof(decimal));

            // Añadir la columna Fecha si no existe
            if (!tablaVentas.Columns.Contains("Fecha"))
                tablaVentas.Columns.Add("Fecha", typeof(string));

            // Iterar a través de las filas y dividir la FechaVenta en Fecha y Hora
            foreach (DataRow fila in tablaVentas.Rows)
            {
                DateTime fechaVenta = Convert.ToDateTime(fila["FechaVenta"]);
                fila["Fecha"] = fechaVenta.ToString("dd/MM/yyyy");
            }

            // Remover la columna original de FechaVenta
            tablaVentas.Columns.Remove("FechaVenta");
        }

        private void ConfigurarDataGridViewVentas(DataTable tablaVentas)
        {
            // Asignar la vista ordenada al DataGridView
            dataGridViewVentas.DataSource = tablaVentas.DefaultView.ToTable();
            dataGridViewVentas.ReadOnly = true;

            // Configurar la visibilidad de las columnas
            dataGridViewVentas.Columns["NombreCliente"].Visible = false;
            dataGridViewVentas.Columns["ApellidoCliente"].Visible = false;
            dataGridViewVentas.Columns["idVenta"].Visible = false;

            // Asignar nombres específicos a las columnas
            //dataGridViewVentas.Columns["idVenta"].HeaderText = "ID";
            dataGridViewVentas.Columns["TipoVenta"].HeaderText = "TIPO DE COMPROBANTE";
            dataGridViewVentas.Columns["metodoPago"].HeaderText = "FORMA DE PAGO";
            dataGridViewVentas.Columns["HoraVenta"].HeaderText = "HORA DE VENTA";
            dataGridViewVentas.Columns["ImporteVenta"].HeaderText = "IMPORTE DE VENTA";

            // Configurar el orden de las columnas
            dataGridViewVentas.Columns["Fecha"].DisplayIndex = 3;

            // Ordenar las filas por la columna "Fecha" en orden descendente
            tablaVentas.DefaultView.Sort = "Fecha DESC";
            DataTable tablaVentasOrdenada = tablaVentas.DefaultView.ToTable();
        }

        private void dataGridViewVentas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si es la columna "ImporteVenta" y no es la fila de encabezado
            if (e.ColumnIndex == dataGridViewVentas.Columns["ImporteVenta"].Index && e.RowIndex >= 0)
            {
                // Obtener el valor actual de la celda "ImporteVenta"
                if (e.Value != null)
                {
                    // Formatear el valor con "S/. " y mostrarlo en la celda
                    e.Value = string.Format("S/. {0}", e.Value);
                    e.FormattingApplied = true;
                }
            }

            // Verificar si es la columna "HoraVenta" y no es la fila de encabezado
            if (e.ColumnIndex == dataGridViewVentas.Columns["HoraVenta"].Index && e.RowIndex >= 0)
            {
                // Obtener el valor actual de la celda "HoraVenta"
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    // Convertir el valor a DateTime
                    DateTime horaVenta = DateTime.ParseExact(e.Value.ToString(), "HH:mm", CultureInfo.InvariantCulture);

                    // Formatear la hora con "AM" o "PM" en mayúsculas y mostrarlo en la celda
                    e.Value = horaVenta.ToString("hh:mm tt", CultureInfo.InvariantCulture).ToUpper();
                    e.FormattingApplied = true;
                }
            }
        }



        private void ActualizarDGV()
        {
            // Verifica si hay registros en la tabla
            if (tablaVentasCompleta.Rows.Count == 0)
            {
                // Si no hay registros, limpia el DataGridView y deshabilita los botones de navegación
                dataGridViewVentas.DataSource = null;
                lblPagina.Text = "0 / 0"; // O cualquier otra lógica que desees para indicar que no hay páginas
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            // Calcula el índice inicial y final para la página actual
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, tablaVentasCompleta.Rows.Count - 1);

            // Filtra las filas para la página actual
            DataRow[] rowsToShow = tablaVentasCompleta.AsEnumerable()
                                                .Skip(startIndex)
                                                .Take(endIndex - startIndex + 1)
                                                .ToArray();

            // Crea un DataTable con las filas filtradas
            DataTable paginaActual = tablaVentasCompleta.Clone();
            foreach (DataRow row in rowsToShow)
            {
                paginaActual.ImportRow(row);
            }

            // Asigna la vista ordenada al DataGridView
            dataGridViewVentas.DataSource = paginaActual;

            // Actualiza la etiqueta de la página actual y total de páginas
            lblPagina.Text = $"{currentPage} / {totalPages}";

            // Habilita o deshabilita los botones de navegación según la página actual
            btnAnterior.Enabled = currentPage > 1;
            btnSiguiente.Enabled = currentPage < totalPages;
        }




        private void dataGridViewVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Desactivar la capacidad de seleccionar múltiples filas solo la primera vez
            if (dataGridViewVentas.MultiSelect)
            {
                dataGridViewVentas.MultiSelect = false;
            }

            // Verificar si el clic fue en la primera fila (encabezado de columna)
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                // Tu lógica actual para la manipulación de columnas
                dataGridViewVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridViewVentas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dataGridViewVentas.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewVentas.AllowUserToResizeColumns = false;
                dataGridViewVentas.AllowUserToResizeRows = false;
            }
            else if (e.RowIndex >= 0) // Asegurarse de que el clic no fue en el encabezado de fila
            {
                // Obtener el valor de la celda que contiene el ID
                DataGridViewCell cell = dataGridViewVentas.Rows[e.RowIndex].Cells["idVenta"]; 

                // Asegurarse de que la celda no sea nula y su valor no sea nulo ni una cadena de espacios en blanco
                if (cell != null && cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    int id;
                    // Intentar convertir el valor de la celda a un entero
                    if (int.TryParse(cell.Value.ToString(), out id))
                    {
                        // Si la conversión es exitosa, asignar el ID a txtIDventa.Text
                        //txtIDventa.Text = id.ToString();
                        lblIdVenta.Text = id.ToString();    
                    }
                    else
                    {
                        // Si la conversión no es exitosa, puedes manejarlo de acuerdo a tus necesidades
                        Console.WriteLine("No se pudo convertir el valor de la celda a un entero.");
                    }
                }
            }
        }
        private decimal CalcularTotalImporteVentas(DataTable tablaVentas)
        {
            // Asegúrate de que la columna "ImporteVenta" exista
            if (tablaVentas.Columns.Contains("ImporteVenta"))
            {
                // Sumar los valores de la columna "ImporteVenta"
                decimal totalImporteVentas = tablaVentas.AsEnumerable().Sum(row => row.Field<decimal>("ImporteVenta"));
                return totalImporteVentas;
            }
            else
            {
                // Si la columna no existe, retorna 0
                return 0;
            }
        }


        private void dataGridViewVentas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic fue en una celda y no en el encabezado de columna
            if (e.RowIndex >= 0)
            {

                // Obtener la fila clickeada
                DataGridViewRow fila = dataGridViewVentas.Rows[e.RowIndex];

                // Seleccionar la nueva fila
                fila.Selected = true;
                fila.DefaultCellStyle.BackColor = Color.LightBlue; // Cambia el color según tus preferencias

                // Guardar la fila seleccionada
                filaSeleccionada = fila;
                
            }
        }

        private void dataGridViewVentas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            // Verificar si no hay datos en el DataGridView
            if (dataGridView.Rows.Count == 0)
            {
                // Llenar las filas del DataGridView con un color específico
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGray; // Puedes ajustar el color según tus preferencias
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string Tventa = cboTipoVenta.SelectedItem != null ? cboTipoVenta.SelectedItem.ToString() : null;
            string Tmetodopago = cboMetodoPago.SelectedItem != null ? cboMetodoPago.SelectedItem.ToString() : null;
            DateTime fechaInicio = dateTimePickerDesde.Value;
            DateTime fechaFinal = dateTimePickerHasta.Value;

            //fechaInicio = fechaInicio.AddDays(-1);
            //fechaFinal = fechaInicio.AddDays(+1);

            CargarVentas(Tventa,Tmetodopago,fechaInicio,fechaFinal);
            ActualizarDGV();
        }

      
        public DataTable ObtenerDatosVentaPorId(int idVenta)
        {
            DataTable datosVenta = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    string consulta = @"
                    SELECT V.idVenta, V.FechaVenta, V.idPedido, V.idMetodoPago,
                           V.NombreCliente, V.ApellidoCliente, V.DNICliente,
                           CV.nombreCategoria, V.ImporteVenta
                    FROM Venta V
                    INNER JOIN CategoriaVenta CV ON V.idCategoríaVenta = CV.idCategoriaVenta
                    WHERE V.idVenta = @IdVenta";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdVenta", idVenta);

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(datosVenta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                throw new Exception($"Error al obtener datos de venta por ID: {ex.Message}");
            }

            return datosVenta;
        }
        public List<EntPedidoComponente> ObtenerComponentesPedidoPorIdPedido(int idPedido)
        {
            List<EntPedidoComponente> componentesPedido = new List<EntPedidoComponente>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    string consulta = @"
                    SELECT PC.idPedidoComponentes, PC.idPedido, PC.idTipoComponente,
                           PC.idComponente, PC.Cantidad, PC.importeComponente,
                           TC.descripcionTipoComponente, C.descripcionComponente,
                           C.precioComponente, CP.nombreCategoria
                    FROM PedidoComponente PC
                    INNER JOIN TipoComponentes TC ON PC.idTipoComponente = TC.idTipoComponente
                    INNER JOIN Componentes C ON PC.idComponente = C.idComponente
                    LEFT JOIN CategoriaPlato CP ON C.idCategoriaPlato = CP.idCategoriaPlato
                    WHERE PC.idPedido = @IdPedido";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPedido", idPedido);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EntPedidoComponente componente = new EntPedidoComponente
                                {
                                    // Asigna los valores a las propiedades de la entidad
                                    IdPedidoComponentes = reader.GetInt32(0),
                                    IdPedido = reader.GetInt32(1),
                                    IdTipoComponente = reader.GetInt32(2),
                                    IdComponente = reader.GetInt32(3),
                                    Cantidad = reader.GetInt32(4),
                                    ImporteComponente = reader.GetDecimal(5),
                                    NombreComponente = reader.GetString(7),  // Cambiado a NombreComponente
                                };

                                componentesPedido.Add(componente);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener componentes de pedido por ID de pedido: {ex.Message}");
            }

            return componentesPedido;
        }

        private void btnA4_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID de la venta
                if (!int.TryParse(lblIdVenta.Text, out int idVenta) || idVenta <= 0)
                {
                    MessageBox.Show("SELECCIONE UNA VENTA");
                    return;
                }

                // Obtener los datos de la venta
                DataTable datosVenta = ObtenerDatosVentaPorId(idVenta);

                if (datosVenta.Rows.Count > 0)
                {
                    // Obtener los datos específicos de la venta
                    DateTime fechaVenta = Convert.ToDateTime(datosVenta.Rows[0]["FechaVenta"]);
                    int idPedido = Convert.ToInt32(datosVenta.Rows[0]["idPedido"]);
                    int idMetodoPago = Convert.ToInt32(datosVenta.Rows[0]["idMetodoPago"]);
                    string nombreCliente = datosVenta.Rows[0]["NombreCliente"].ToString();
                    string apellidoCliente = datosVenta.Rows[0]["ApellidoCliente"].ToString();
                    string dniCliente = datosVenta.Rows[0]["DNICliente"].ToString();
                    string nombreCatVen = datosVenta.Rows[0]["nombreCategoria"].ToString();
                    decimal total = Convert.ToDecimal(datosVenta.Rows[0]["ImporteVenta"]);
                    string metodoPagoSeleccionado = cboMetodoPago.Text;
                    // Obtener los componentes del pedido
                    List<EntPedidoComponente> componentesPedido = ObtenerComponentesPedidoPorIdPedido(idPedido);

                    // Llamar a la función para generar documentos de impresión
                    logImpresion generadorImpresion = new logImpresion();
                    generadorImpresion.GenerarDocumentosImpresion(
                        idVenta,
                        Path.Combine(Directory.GetCurrentDirectory(), $"Venta_{idVenta}.html"),
                        fechaVenta.ToString(),
                        idPedido,
                        idMetodoPago,
                        "20608387570",
                        nombreCliente,
                        apellidoCliente,
                        dniCliente,
                        nombreCatVen,
                        metodoPagoSeleccionado,
                        componentesPedido,
                        total
                    );

                    //MessageBox.Show("Documento generados exitosamente.");

                }
                else
                {
                    MessageBox.Show("No se encontraron datos para la venta seleccionada.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar documentos de impresión: {ex.Message}");
            }
        }

        private string nombreMetodoPago(int idPedido) 
        {
            if (idPedido == 1) 
            {
                return "YAPE";
            }
            else if(idPedido ==2) 
            {
                return "PLIN";
            }
            else if(idPedido == 3)
            {   
                return "EFECTIVO";
            }
            else 
            {
                return "TARJETA";
            }
        }
       
        private void btnTicket_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID de la venta
                if (!int.TryParse(lblIdVenta.Text, out int idVenta) || idVenta <= 0)
                {
                    MessageBox.Show("SELECCIONE UNA VENTA");
                    return;
                }
                else
                {
                    // Configura el diálogo de impresión sin mostrarlo
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = PD;
                    printDialog.PrinterSettings.PrintToFile = true;

                    // Muestra el cuadro de diálogo para seleccionar la impresora
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        PD.Print();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void PD_BeginPrint(object sender, PrintEventArgs e)
        {
            PageSettings pagesetup = new PageSettings();
            pagesetup.PaperSize = new PaperSize("Custom", 250, longpaper);

            PD.DefaultPageSettings = pagesetup;
        }

        private void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Font f8 = new Font("Calibri", 8, FontStyle.Regular);
                Font f10 = new Font("Calibri", 10, FontStyle.Regular);
                Font f10b = new Font("Calibri", 10, FontStyle.Bold);

                int leftmargin = PD.DefaultPageSettings.Margins.Left;
                int centermargin = PD.DefaultPageSettings.PaperSize.Width / 2;
                int rightmargin = PD.DefaultPageSettings.PaperSize.Width;

                StringFormat right = new StringFormat();
                StringFormat center = new StringFormat();
                right.Alignment = StringAlignment.Far;
                center.Alignment = StringAlignment.Center;

                string line = "*********************************************";                           
                string lineS = "-------------------------------------------------------------------------";
                Image logoImage = Properties.Resources.Logo_venta;
                Image qrTest = Properties.Resources.QR_Test;

                // Porcentaje del ancho de la página para la imagen (ajusta según sea necesario)
                float imageWidthPercentage = 0.8f;

                int logoWidth = (int)(e.PageBounds.Width * imageWidthPercentage);
                int logoHeight = (int)((float)logoImage.Height / logoImage.Width * logoWidth);
                e.Graphics.ScaleTransform(0.8f, 0.8f); // -20%
                e.Graphics.DrawImage(logoImage, (e.PageBounds.Width - logoWidth) / 2, 5, logoWidth + 10, logoHeight + 10);

                DataTable datosVenta = ObtenerDatosVentaPorId(Convert.ToInt32(lblIdVenta.Text));

                if (datosVenta.Rows.Count > 0)
                {
                    // Obtener datos de la venta
                    DateTime fechaVenta = Convert.ToDateTime(datosVenta.Rows[0]["FechaVenta"]);
                    int idPedido = Convert.ToInt32(datosVenta.Rows[0]["idPedido"]);
                    int idMetodoPago = Convert.ToInt32(datosVenta.Rows[0]["idMetodoPago"]);
                    string nombreCliente = datosVenta.Rows[0]["NombreCliente"].ToString();
                    string apellidoCliente = datosVenta.Rows[0]["ApellidoCliente"].ToString();
                    string dniCliente = datosVenta.Rows[0]["DNICliente"].ToString();
                    string nombreCatVen = datosVenta.Rows[0]["nombreCategoria"].ToString();
                    decimal total = Convert.ToDecimal(datosVenta.Rows[0]["ImporteVenta"]);

                    string nombreMP = nombreMetodoPago(idPedido);

                    // Obtener los componentes del pedido
                    List<EntPedidoComponente> componentesPedido = ObtenerComponentesPedidoPorIdPedido(idPedido);

                    // Información de la empresa
                    e.Graphics.DrawString("RUC 20608387570", f10, Brushes.Black, centermargin, 85, center);
                    e.Graphics.DrawString("AV. AMERICA NORTE NRO. 140", f10, Brushes.Black, centermargin, 100, center);
                    e.Graphics.DrawString("URB. LA INTENDENCIA ET.1,", f10, Brushes.Black, centermargin, 115, center);
                    e.Graphics.DrawString("TRUJILLO, TRUJILLO , LA", f10, Brushes.Black, centermargin, 130, center);
                    e.Graphics.DrawString("        LIBERTAD       ", f10, Brushes.Black, centermargin, 145, center);

                    // Cuadro de información de la factura
                    RectangleF facturaRectangle = new RectangleF(10, 170, rightmargin - 20, 30);
                    e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), Rectangle.Round(facturaRectangle));
                    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    e.Graphics.DrawString(nombreCatVen.ToUpper(), f10b, Brushes.Black, facturaRectangle, centerFormat);

                    // Espacio entre el cuadro de información de la factura y la información adicional
                    float espacioEntreCuadroYInfo = 15; // Ajusta según sea necesario

                    // Información adicional sobre la venta
                    string fechaLabel = "Fecha                          :";
                    string fechaValue = fechaVenta.ToString("dd/MM/yyyy");

                    // Determinar el ancho del texto de la etiqueta y el valor de la fecha
                    float fechaLabelWidth = e.Graphics.MeasureString(fechaLabel, f8).Width;
                    float fechaValueWidth = e.Graphics.MeasureString(fechaValue, f8).Width;

                    // Dibujar la etiqueta "Fecha"
                    e.Graphics.DrawString(fechaLabel, f8, Brushes.Black, 0, facturaRectangle.Bottom + espacioEntreCuadroYInfo);

                    // Dibujar el valor de la fecha alineado a la derecha
                    e.Graphics.DrawString(fechaValue, f8, Brushes.Black, rightmargin - fechaValueWidth, facturaRectangle.Bottom + espacioEntreCuadroYInfo);

                    float lineHeight = 15; // Ajusta la altura de cada línea según sea necesario

                    float clienteLabelWidth = 0;
                    float dniLabelWidth = 0;
                    float clienteValueWidth = 0;
                    float dniValueWidth = 0;

                    if (nombreCatVen.ToUpper() != "NOTA DE VENTA")
                    {
                        // Información del Cliente y DNI
                        string clienteLabel = "Cliente                        :";
                        string dniLabel = "DNI                              :";
                        string clienteValue = nombreCliente + " " + apellidoCliente;
                        string dniValue = dniCliente;

                        // Determinar anchos de los textos
                        clienteLabelWidth = e.Graphics.MeasureString(clienteLabel, f8).Width;
                        dniLabelWidth = e.Graphics.MeasureString(dniLabel, f8).Width;
                        clienteValueWidth = e.Graphics.MeasureString(clienteValue, f8).Width;
                        dniValueWidth = e.Graphics.MeasureString(dniValue, f8).Width;

                        // Ajustar el eje Y para la siguiente línea
                        float clienteDniOffset = lineHeight + espacioEntreCuadroYInfo;
                        facturaRectangle.Offset(0, clienteDniOffset);

                        // Dibujar etiqueta y valor del Cliente
                        e.Graphics.DrawString(clienteLabel, f8, Brushes.Black, 0, facturaRectangle.Bottom);
                        e.Graphics.DrawString(clienteValue, f8, Brushes.Black, rightmargin - clienteValueWidth, facturaRectangle.Bottom);

                        // Ajustar el eje Y para la siguiente línea
                        facturaRectangle.Offset(0, lineHeight);

                        // Dibujar etiqueta y valor del DNI
                        e.Graphics.DrawString(dniLabel, f8, Brushes.Black, 0, facturaRectangle.Bottom);
                        e.Graphics.DrawString(dniValue, f8, Brushes.Black, rightmargin - dniValueWidth, facturaRectangle.Bottom);

                        // Ajustar el eje Y para la siguiente línea
                        facturaRectangle.Offset(0, clienteDniOffset);
                    }
                    else
                    {
                        // Ajustar el eje Y para compensar la omisión de las líneas de cliente y DNI
                        facturaRectangle = new RectangleF(10, facturaRectangle.Bottom + espacioEntreCuadroYInfo, rightmargin - 20, 25);
                    }

                    // Método de Pago 
                    string metodoPagoLabel = "Método de pago       :";
                    string metodoPagoValue = nombreMP;

                    // Determinar anchos del texto del Método de Pago
                    float metodoPagoLabelWidth = e.Graphics.MeasureString(metodoPagoLabel, f8).Width;
                    float metodoPagoValueWidth = e.Graphics.MeasureString(metodoPagoValue, f8).Width;

                    // Dibujar etiqueta y valor del Método de Pago
                    e.Graphics.DrawString(metodoPagoLabel, f8, Brushes.Black, 0, facturaRectangle.Bottom);
                    e.Graphics.DrawString(metodoPagoValue, f8, Brushes.Black, rightmargin - metodoPagoValueWidth, facturaRectangle.Bottom);

                    e.Graphics.DrawString(line, f8, Brushes.Black, 0, facturaRectangle.Bottom + 30); // Ajustar el eje Y

                    // Detalles de los componentes
                    int height = 0;
                    float fontSizeSmall = 7;
                    Font f8Small = new Font("Arial", fontSizeSmall, FontStyle.Regular);

                    e.Graphics.DrawString("Cant", f8Small, Brushes.Black, 0, facturaRectangle.Bottom + 45); // Ajustar el eje Y
                    e.Graphics.DrawString("Nombre", f8Small, Brushes.Black, 25, facturaRectangle.Bottom + 45); // Ajustar el eje Y
                    e.Graphics.DrawString("Unitario", f8Small, Brushes.Black, 180, facturaRectangle.Bottom + 45, right);
                    e.Graphics.DrawString("Total", f8Small, Brushes.Black, rightmargin, facturaRectangle.Bottom + 45, right);
                    e.Graphics.DrawString(line, f8, Brushes.Black, 0, facturaRectangle.Bottom + 60); // Ajustar el eje Y

                    int lastPosition = 0;

                    foreach (EntPedidoComponente componente in componentesPedido)
                    {
                        height += 15;
                        e.Graphics.DrawString(componente.Cantidad.ToString(), f8, Brushes.Black, 0, facturaRectangle.Bottom + 60 + height); // Ajustar el eje Y
                        e.Graphics.DrawString(" " + componente.NombreComponente, f8, Brushes.Black, 25, facturaRectangle.Bottom + 60 + height); // Ajustar el eje Y

                        decimal precioUnitario = Convert.ToDecimal(componente.ImporteComponente);
                        e.Graphics.DrawString(precioUnitario.ToString("##,##0"), f8, Brushes.Black, 180, facturaRectangle.Bottom + 60 + height, right);

                        decimal totalprice = componente.Cantidad * precioUnitario;
                        e.Graphics.DrawString(totalprice.ToString("##,##0"), f8, Brushes.Black, rightmargin, facturaRectangle.Bottom + 60 + height, right);

                        lastPosition = (int)facturaRectangle.Bottom + 60 + height; // Ajustar el eje Y
                    }

                    // Línea separadora
                    e.Graphics.DrawString(lineS, f8, Brushes.Black, 0, lastPosition + 15); // Ajustar el eje Y

                    // Total, Items, Agradecimientos, etc.
                    e.Graphics.DrawString($"Total: S/. {total.ToString("##,##0")}", f10b, Brushes.Black, rightmargin, lastPosition + 30, right);
                    e.Graphics.DrawString($"Items: {componentesPedido.Count}", f10, Brushes.Black, 0, lastPosition + 30);
                    e.Graphics.DrawString("~ GRACIAS POR PREFERIRNOS ~", f10, Brushes.Black, centermargin, lastPosition + 60, center);

                    e.Graphics.DrawString("~ Documento generado por: SISTEMA POS ~", f8, Brushes.Black, centermargin, lastPosition + 75, center);
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para la venta seleccionada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir la factura: {ex.Message}");
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            ObtenerFechasMesActual(out DateTime fechaInicio, out DateTime fechaFinal);
            //Console.WriteLine($"Fecha Inicio: {fechaInicio}, Fecha Fin: {fechaFinal}");
           
            // Asignar las fechas al DateTimePicker
            dateTimePickerDesde.Value = fechaInicio;
            dateTimePickerHasta.Value = fechaFinal;

            // Llamamos a la función para cargar datos en el DataGridView
            CargarVentas(null, null, fechaInicio, fechaFinal);
            ActualizarDGV();
        }
        private void btnDiaActual_Click(object sender, EventArgs e)
        {
            ObtenerFechasDiaActual(out DateTime fechaInicio, out DateTime fechaFinal);
            //Console.WriteLine($"Fecha Inicio: {fechaInicio}, Fecha Fin: {fechaFinal}");

            dateTimePickerDesde.Value = fechaInicio;
            dateTimePickerHasta.Value = fechaFinal;

            // Llamamos a la función para cargar datos en el DataGridView
            CargarVentas(null, null, fechaInicio, fechaFinal);
            ActualizarDGV();
        }

        private void btnUltimaHora_Click(object sender, EventArgs e)
        {
            ObtenerFechasUltimaHora(out DateTime fechaInicio, out DateTime fechaFinal);

            // Asignar las fechas al DateTimePicker
            dateTimePickerDesde.Value = fechaInicio;
            dateTimePickerHasta.Value = fechaFinal;

            // Llamamos a la función para cargar datos en el DataGridView
            CargarVentas(null, null, fechaInicio, fechaFinal);
            ActualizarDGV();
        }

        private void dataGridViewVentas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                // Verifica si es una celda de encabezado y la columna deseada
                e.PaintBackground(e.CellBounds, true);

                // Cambia el color de fondo y el color de texto
                e.Graphics.FillRectangle(Brushes.LightBlue, e.CellBounds);
                e.Graphics.DrawString(e.Value?.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);

                // Evita que se dibuje el contenido estándar
                e.Handled = true;
            }
        }

        private void dataGridViewVentas_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridViewVentas.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ActualizarDGV();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ActualizarDGV();
            }
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            // Reset DateTimePicker controls
            dateTimePickerDesde.Value = DateTime.Now;  // Set to your default start date
            dateTimePickerHasta.Value = DateTime.Now;  // Set to your default end date

            // Reset ComboBox selections
            cboTipoVenta.SelectedIndex = -1;  // Set to no selection or default index
            cboMetodoPago.SelectedIndex = -1;  // Set to no selection or default index

            ObtenerFechasUltimaHora(out DateTime fechaInicio, out DateTime fechaFinal);

            //Llamamos a la función para cargar datos en el DataGridView
            CargarVentas(null, null, fechaInicio, fechaFinal);
            ActualizarDGV();
        }
    }
}
