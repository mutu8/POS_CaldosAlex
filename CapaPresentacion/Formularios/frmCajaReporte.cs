using CapaeEntidad;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Formularios
{
    public partial class frmCajaReporte : Form
    {
        private string connectionString = logConexion.Instancia.ObtenerConexion();
        private DataGridViewRow filaSeleccionada = null;

        private DataTable tablaVentasCompleta; // Almacena todos los datos originales
        private int pageSize = 10; // Tamaño de cada página
        private int currentPage = 1; // Página actual
        private int totalPages; // Número total de páginas

        private PrintDocument PD = new PrintDocument();
        private PrintPreviewDialog PPD = new PrintPreviewDialog();
        private int longpaper;
        public frmCajaReporte()
        {
            InitializeComponent();
            PersonalizarDataGridView(dataGridViewCaja);
            ConfigurarDataGridView();

            dataGridViewCaja.CellFormatting += dataGridViewCaja_CellFormatting;

        }
        public Panel ObtenerPanelVenta()
        {
            return panelCaja;
        }
        private void CargarDatosCaja()
        {
            DataTable dataTable = logCaja.Instancia.ObtenerDatosCaja();

            dataGridViewCaja.DataSource = dataTable;

            // Almacena los datos originales antes de paginar
            tablaVentasCompleta = dataTable.Copy();

            // Calcula el número total de páginas
            totalPages = (int)Math.Ceiling((double)tablaVentasCompleta.Rows.Count / pageSize);

            ocultarColumnas();

            dataGridViewCaja.Columns["FechaApertura"].HeaderText = "FECHA DE APERTURA";
            dataGridViewCaja.Columns["FechaCierre"].HeaderText = "FECHA DE CIERRE";
            //dataGridViewCaja.Columns["MontoFinal"].HeaderText = "MONTO FINAL";
            //dataGridViewCaja.Columns["MontoInicial"].HeaderText = "MONTO INICIAL";
            dataGridViewCaja.Columns["HoraCierre"].HeaderText = "HORA DE CIERRE";
            dataGridViewCaja.Columns["HoraApertura"].HeaderText = "HORA DE APERTURA";
            dataGridViewCaja.Columns["EstadoCaja"].HeaderText = "ESTADO DE CAJA";

            // Cambiar el orden de las columnas
            CambiarOrdenColumnas("HoraApertura", "HoraCierre", "FechaApertura", "FechaCierre", "EstadoCaja");

            dataGridViewCaja.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridViewCaja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // o DataGridViewAutoSizeColumnsMode.AllCells



            ActualizarDgvCaja();
        }
        private void ocultarColumnas() 
        {
            string[] columnasOcultas = {
                "TotalVentasBoleta",
                "TotalVentasFactura",
                "TotalVentasNotaVenta",
                "TotalVentasYape",
                "TotalVentasPlin",
                "TotalVentasEfectivo",
                "TotalVentasTarjeta",
                "idUsuario",
                "idCaja",
                "MontoInicial",
                "MontoFinal",
                "Gastos"
            };

            foreach (var columna in columnasOcultas)
            {
                dataGridViewCaja.Columns[columna].Visible = false;
            }
        }
        private void CambiarOrdenColumnas(params string[] columnas)
        {
            foreach (var columna in columnas)
            {
                dataGridViewCaja.Columns[columna].DisplayIndex = Array.IndexOf(columnas, columna);
            }
        }

        public void ConfigurarDataGridView()
        {
            // Configuración para ajuste automático de tamaño de columnas y filas
            dataGridViewCaja.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCaja.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCaja.CellPainting += dataGridViewCaja_CellPainting;
            // Establecer el estilo de fuente en negrita para el encabezado
            dataGridViewCaja.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridViewCaja.Font, FontStyle.Bold);

            CargarDatosCaja();

        }
        private void dataGridViewCaja_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewCaja.Columns[e.ColumnIndex].Name == "FechaApertura" ||dataGridViewCaja.Columns[e.ColumnIndex].Name == "FechaCierre")
               {
                if (e.Value != null && e.Value != DBNull.Value)
                   {
                    DateTime fecha = Convert.ToDateTime(e.Value);
                    e.Value = fecha.ToString("dd/MM/yyyy"); // Cambiar el formato según tus preferencias
                    e.FormattingApplied = true;
                    }
                }
        }
        private void ActualizarDgvCaja()
        {
            // Calcula el índice inicial y final para la página actual
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, tablaVentasCompleta.Rows.Count - 1);

            // Filtra las filas para la página actual
            DataRow[] rowsToShow = tablaVentasCompleta.AsEnumerable()
                                                .Skip(startIndex)
                                                .Take(endIndex - startIndex + 1)  // Utiliza endIndex en lugar de pageSize
                                                .ToArray();

            // Crea un DataTable con las filas filtradas
            DataTable paginaActual = tablaVentasCompleta.Clone();
            foreach (DataRow row in rowsToShow)
            {
                paginaActual.ImportRow(row);
            }

            // Asigna la vista ordenada al DataGridView
            dataGridViewCaja.DataSource = paginaActual;

            // Actualiza la etiqueta de la página actual y total de páginas
            lblPagina.Text = $"{currentPage} / {totalPages}";

            // Habilita o deshabilita los botones de navegación según la página actual
            btnAnterior.Enabled = currentPage > 1;
            btnSiguiente.Enabled = currentPage < totalPages;
        }

        private void PersonalizarDataGridView(DataGridView dgv)
        {
            // Ajustar el tamaño de la fuente
            dgv.Font = new Font("Segoe UI", 12);
            dgv.CellPainting += dataGridViewCaja_CellPainting;
            dgv.RowPrePaint += dataGridViewCaja_RowPrePaint;

            // Personalizar el estilo
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 0);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = Color.LightGray;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;

            // Manejar el evento ColumnAdded para deshabilitar el ordenamiento
            dgv.ColumnAdded += (s, args) =>
            {
                args.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            };

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

        private void dataGridViewCaja_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic fue en una celda y no en el encabezado de columna
            if (e.RowIndex >= 0)
            {

                // Obtener la fila clickeada
                DataGridViewRow fila = dataGridViewCaja.Rows[e.RowIndex];

                // Seleccionar la nueva fila
                fila.Selected = true;
                fila.DefaultCellStyle.BackColor = Color.LightBlue; // Cambia el color según tus preferencias

                // Guardar la fila seleccionada
                filaSeleccionada = fila;

            }
        }

        private void dataGridViewCaja_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            // Verifica que estás pintando la columna "EstadoCaja" y que la celda tiene un valor booleano
            if (e.ColumnIndex >= 0 && dataGridViewCaja.Columns[e.ColumnIndex].Name == "EstadoCaja" && e.Value is bool)
            {
                // Convierte el valor booleano a un formato visual deseado
                bool estado = (bool)e.Value;

                // Define las imágenes para abierto y cerrado
                Image imagenAbierto = Properties.Resources.Open;  // Reemplaza con tu propia imagen
                Image imagenCerrado = Properties.Resources.Close;  // Reemplaza con tu propia imagen

                // Selecciona la imagen adecuada según el estado
                Image imagen = estado ? imagenAbierto : imagenCerrado;

                // Calcula el rectángulo para mantener la proporción original de la imagen sin distorsión
                Rectangle rect = new Rectangle();

                if (imagen.Width > imagen.Height)
                {
                    // Imagen más ancha que alta
                    rect.Width = e.CellBounds.Width - 4;
                    rect.Height = (int)(imagen.Height * ((float)rect.Width / imagen.Width));
                }
                else
                {
                    // Imagen más alta que ancha o cuadrada
                    rect.Height = e.CellBounds.Height - 4;
                    rect.Width = (int)(imagen.Width * ((float)rect.Height / imagen.Height));
                }

                rect.X = e.CellBounds.X + (e.CellBounds.Width - rect.Width) / 2;
                rect.Y = e.CellBounds.Y + (e.CellBounds.Height - rect.Height) / 2;

                // Dibuja la celda con la imagen sin distorsión
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.DrawImage(imagen, rect);

                e.Handled = true;
            }
        }

        private void dataGridViewCaja_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridViewCaja.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void dataGridViewCaja_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
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

        private void dataGridViewCaja_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic ocurrió en una celda válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Obtener el valor de la celda en la columna "idCaja" (reemplaza con el nombre de tu columna)
                object idCajaObject = dataGridViewCaja.Rows[e.RowIndex].Cells["idCaja"].Value;

                // Verificar si el valor no es null
                if (idCajaObject != null && int.TryParse(idCajaObject.ToString(), out int idCaja))
                {
                    lblIdCaja.Text = idCaja.ToString();
                }
            }
        }


        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ActualizarDgvCaja();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ActualizarDgvCaja();
            }
        }
        public DataTable ObtenerDatosCajaPorId(int idCaja)
        {
            DataTable datosCaja = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    string consulta = @"
            SELECT idCaja, FechaApertura, FechaCierre, EstadoCaja, MontoInicial, MontoFinal,
                   TotalVentasBoleta, TotalVentasFactura, TotalVentasNotaVenta,
                   TotalVentasYape, TotalVentasPlin, TotalVentasEfectivo, TotalVentasTarjeta, idUsuario
            FROM Caja
            WHERE idCaja = @IdCaja";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdCaja", idCaja);

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(datosCaja);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                throw new Exception($"Error al obtener datos de caja por ID: {ex.Message}");
            }

            return datosCaja;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Obtener el ID de la venta
            if (string.IsNullOrEmpty(lblIdCaja.Text) || !int.TryParse(lblIdCaja.Text, out int idVenta) || idVenta <= 0)
            {
                MessageBox.Show("VERIFIQUE SI SELECCIONÓ CORRECTAMENTE!!");
                return;
            }

            // Si llegamos aquí, lblIdCaja.Text no es null ni una cadena vacía
            int IdCaja = int.Parse(lblIdCaja.Text);

            // Obtener los datos de la caja
            DataTable datosCaja = ObtenerDatosCajaPorId(int.Parse(lblIdCaja.Text));

            if (datosCaja.Rows.Count > 0)
            {

                // Obtener los datos específicos de la caja
                DateTime fechaApertura = Convert.ToDateTime(datosCaja.Rows[0]["FechaApertura"]);
                DateTime? fechaCierre = datosCaja.Rows[0]["FechaCierre"] as DateTime?;
                bool estadoCaja = Convert.ToBoolean(datosCaja.Rows[0]["EstadoCaja"]);
                decimal montoInicial = Convert.ToDecimal(datosCaja.Rows[0]["MontoInicial"]);
                decimal? montoFinal = datosCaja.Rows[0]["MontoFinal"] as decimal?;
                decimal? totalVentasBoleta = datosCaja.Rows[0]["TotalVentasBoleta"] as decimal?;
                decimal? totalVentasFactura = datosCaja.Rows[0]["TotalVentasFactura"] as decimal?;
                decimal? totalVentasNotaVenta = datosCaja.Rows[0]["TotalVentasNotaVenta"] as decimal?;
                decimal? totalVentasYape = datosCaja.Rows[0]["TotalVentasYape"] as decimal?;
                decimal? totalVentasPlin = datosCaja.Rows[0]["TotalVentasPlin"] as decimal?;
                decimal? totalVentasEfectivo = datosCaja.Rows[0]["TotalVentasEfectivo"] as decimal?;
                decimal? totalVentasTarjeta = datosCaja.Rows[0]["TotalVentasTarjeta"] as decimal?;

                decimal gastos = 0; // Variable para almacenar los gastos ingresados

                // Verificar si la caja está abierta
                if (logCaja.Instancia.CajaEstaAbierta(IdCaja))
                {
                    MessageBox.Show("Aún no cierra caja", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Verificar si la caja ya tiene gastos registrados
                else if (logCaja.Instancia.CajaTieneGastosRegistrados(IdCaja))
                {
                    MessageBox.Show("La caja ya tiene gastos registrados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gastos = logCaja.Instancia.ObtenerGastoCajaPorId(IdCaja);
                }
                else
                {
                    // Preguntar si se desean registrar gastos
                    DialogResult result = MessageBox.Show("¿Desea registrar gastos en la caja?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Crear un cuadro de diálogo para ingresar los gastos
                        string gastosInput = Microsoft.VisualBasic.Interaction.InputBox("Ingrese los gastos:", "GASTOS", "");

                        // Verificar si se ingresó algún valor
                        if (!string.IsNullOrEmpty(gastosInput))
                        {
                            // Convertir el valor ingresado a decimal
                            if (decimal.TryParse(gastosInput, out gastos))
                            {
                                // Llamar al método para insertar gastos en la caja seleccionada
                                logCaja.Instancia.InsertarGasto(IdCaja, gastos);

                                MessageBox.Show("GASTOS: " + gastos);
                            }
                            else
                            {
                                MessageBox.Show("Ingrese un valor válido para los gastos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Salir del método si los gastos no son válidos
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se ingresaron gastos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return; // Salir del método si no se ingresaron gastos
                        }
                    }
                }



                // Llamar a la función para generar documentos de impresión de la caja
                logImpresion generadorImpresion = new logImpresion();
                generadorImpresion.GenerarDocumentosImpresionCaja(
                    IdCaja,
                    Path.Combine(Directory.GetCurrentDirectory(), $"Caja_{IdCaja}.html"),
                    fechaApertura,
                    fechaCierre,
                    estadoCaja,
                    montoInicial,
                    montoFinal,
                    totalVentasBoleta,
                    totalVentasFactura,
                    totalVentasNotaVenta,
                    totalVentasYape,
                    totalVentasPlin,
                    totalVentasEfectivo,
                    totalVentasTarjeta,
                    gastos
                ); ;


                // MessageBox.Show("Documentos generados exitosamente.");

            }
            else
            {
                MessageBox.Show("No se encontraron datos para la caja seleccionada.");
            }

        }
    }
}
