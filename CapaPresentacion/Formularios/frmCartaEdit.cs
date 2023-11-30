using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace POS.Formularios
{
    public partial class frmCartaEdit : Form
    {
        private string precio;
        public frmCartaEdit()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            txtPrecio.ReadOnly = true;
            txtPrecio.Enabled = false;
            PersonalizarDataGridView(dataGridView1);
        }
        private string connectionString = logConexion.Instancia.ObtenerConexion();
        //private string connectionString = "Server=34.176.49.57;Database=bd_caldosAlex;User Id=sqlserver;Password=@hV\"1%`(o63_/7V:;";
        private DataGridViewRow filaSeleccionada = null;

        private void ConfigurarDataGridView()
        {
            // Configuración para ajuste automático de tamaño de columnas y filas
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.CellPainting += dataGridView1_CellPainting;

            // Llamamos a la función para cargar datos en el DataGridView
            carta();
        }
        public Panel ObtenerPanelCartaEdit()
        {
            return panelCartaEdit;
        }

        private void carta() 
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Consulta para obtener todos los datos de la tabla Componentes con nombres en lugar de IDs
                    string consulta = @"
                        SELECT C.idComponente, C.descripcionComponente, C.precioComponente, 
                        TC.descripcionTipoComponente, COALESCE(CP.nombreCategoria, '------------------------') AS nombreCategoria
                        FROM Componentes C
                        INNER JOIN TipoComponentes TC ON C.idTipoComponente = TC.idTipoComponente
                        LEFT JOIN CategoriaPlato CP ON C.idCategoriaPlato = CP.idCategoriaPlato";

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion))
                    {
                        DataTable tablaComponentes = new DataTable();
                        adaptador.Fill(tablaComponentes);

                        // Asignar la tabla al DataGridView
                        dataGridView1.DataSource = tablaComponentes;
                        dataGridView1.ReadOnly = true;

                        dataGridView1.Columns["idComponente"].Visible = false;

                        // Asignar nombres específicos a las columnas
                        //dataGridView1.Columns["idComponente"].HeaderText = "ID";
                        dataGridView1.Columns["descripcionComponente"].HeaderText = "NOMBRE";
                        dataGridView1.Columns["precioComponente"].HeaderText = "PRECIO";
                        //dataGridView1.Columns["descripcionTipoComponente"].HeaderText = "TIPO DE COMPONENTE";
                        dataGridView1.Columns["nombreCategoria"].HeaderText = "CATEGORÍA DE PLATO";

                        dataGridView1.Columns["descripcionTipoComponente"].Visible = false;

                        CambiarOrdenColumnas("descripcionComponente", "nombreCategoria", "precioComponente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CambiarOrdenColumnas(params string[] columnas)
        {
            foreach (var columna in columnas)
            {
                dataGridView1.Columns[columna].DisplayIndex = Array.IndexOf(columnas, columna);
            }
        }

        private void PersonalizarDataGridView(DataGridView dgv)
        {
            // Ajustar el tamaño de la fuente
            dgv.Font = new Font("Segoe UI", 12);

            // Personalizar el estilo
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 128, 0);
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White; 
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray; 
            dgv.DefaultCellStyle.ForeColor = System.Drawing.Color.Black; 
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.RowHeadersVisible = false;
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Establecer el estilo de fuente en negrita para el encabezado
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);

            // Ajustar el tamaño automáticamente y centrar en el panel
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Desactivar la posibilidad de cambiar el ancho de las columnas
            foreach (DataGridViewColumn columna in dgv.Columns)
            {
                columna.Resizable = DataGridViewTriState.False;
            }
        }


        private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic fue en una celda y no en el encabezado de columna
            if (e.RowIndex >= 0)
            {
           
                // Obtener la fila clickeada
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                // Seleccionar la nueva fila
                fila.Selected = true;
                fila.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue; // Cambia el color según tus preferencias

                // Guardar la fila seleccionada
                filaSeleccionada = fila;

                // Obtener el valor de la celda clickeada
                object valorID = fila.Cells["idComponente"].Value;
                object valorDescripcion = fila.Cells["descripcionComponente"].Value;
                object valorPrecio = fila.Cells["precioComponente"].Value;

                // Asignar los valores a los controles
                lblID.Text = valorID.ToString();
                lblDescripcion.Text = valorDescripcion.ToString();
                txtPrecio.Text = valorPrecio.ToString();
            }
        }
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Verificar si el valor del TextBox es igual a cero
                if (txtPrecio.Text == "0")
                {
                    MessageBox.Show("El valor no puede ser cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si el valor es cero
                }

                // Obtener los valores modificados del formulario
                string descripcion = lblDescripcion.Text;
                decimal nuevoPrecio = decimal.Parse(txtPrecio.Text);

                // Obtener el ID del componente que estás modificando
                int idComponente = int.Parse(lblID.Text);

                // Realizar la actualización en la base de datos usando la capa lógica
                bool actualizacionExitosa = logComponente.Instancia.ActualizarComponenteEnBD(idComponente, descripcion, nuevoPrecio);

                if (actualizacionExitosa)
                {
                    MessageBox.Show("Modificación guardada correctamente");
                    carta();
                    txtPrecio.ReadOnly = false;
                    // Para deshabilitar completamente el DataGridView
                    dataGridView1.Enabled = true;
                    btnCancel.Enabled = false;
                    btnEditar.Enabled = true;
                    txtPrecio.Enabled = false;
                    btnGuardar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Error al guardar la modificación. Consulta los registros de errores para más detalles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la imagen: {ex.Message}");
            }
        }




        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ingrese el nuevo valor a editar");
            precio=txtPrecio.Text;
            txtPrecio.Enabled = true;
            txtPrecio.ReadOnly = false;

            btnEditar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancel.Enabled = true;
            // Para deshabilitar completamente el DataGridView
            dataGridView1.Enabled = false;
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada no es un número, la tecla de retroceso o el punto decimal
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true; // Desactivar la tecla presionada
                return;
            }

            // Obtener el texto actual del TextBox antes de realizar cualquier cambio
            string currentText = txtPrecio.Text;

            // Verificar si se está ingresando un cero y el último dígito también es cero en los dos primeros números
            if (e.KeyChar == '0' && currentText.Length < 2 && currentText.StartsWith("0"))
            {
                e.Handled = true; // Desactivar la tecla presionada
                return;
            }
        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                // Verifica si es una celda de encabezado y la columna deseada
                e.PaintBackground(e.CellBounds, true);

                // Cambia el color de fondo y el color de texto
                e.Graphics.FillRectangle(System.Drawing.Brushes.LightBlue, e.CellBounds);
                e.Graphics.DrawString(e.Value?.ToString(), e.CellStyle.Font, System.Drawing.Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);

                // Evita que se dibuje el contenido estándar
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPrecio.Text = precio;
            txtPrecio.ReadOnly = true;
            txtPrecio.Enabled = false;
            // Para deshabilitar completamente el DataGridView
            dataGridView1.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancel.Enabled = false;
            btnEditar.Enabled = true;

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            // Obtener el texto actual del TextBox
            string currentText = txtPrecio.Text;

            // Verificar si ya hay dos dígitos y no hay un punto decimal
            if (currentText.Length == 2 && currentText.IndexOf('.') == -1)
            {
                // Si no hay un punto decimal, agregar automáticamente uno
                txtPrecio.Text = currentText + ".";
                txtPrecio.SelectionStart = txtPrecio.Text.Length; // Mover el cursor al final
            }

            // Verificar si ya hay 4 dígitos después del punto decimal
            int dotIndex = currentText.IndexOf('.');
            if (dotIndex != -1 && currentText.Substring(dotIndex + 1).Length >= 2)
            {
                // Si ya hay 4 dígitos después del punto decimal, actualizar el texto sin cambiar el cursor
                txtPrecio.Text = currentText.Substring(0, dotIndex + 3);
                txtPrecio.SelectionStart = txtPrecio.Text.Length; // Mover el cursor al final
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica que estás formateando la columna "precioComponente"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "precioComponente" && e.Value != null)
            {
                // Añade "S/." al valor y establece el resultado formateado
                e.Value = string.Format("S/. {0}", e.Value);
                e.FormattingApplied = true; // Indica que has aplicado el formato
            }
        }
    }
}
