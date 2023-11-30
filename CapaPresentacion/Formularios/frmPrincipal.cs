using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Windows.Forms;
using CapaeEntidad;
using CapaLogica;
using POS.Formularios;

namespace POS
{
    public partial class frmPrincipal : Form
    {
        public int numMesaSeleccionada = 0;
        public int numCajaSeleccionada = logCaja.Instancia.ObtenerIdCajaAbierta();

        private string connectionString = logConexion.Instancia.ObtenerConexion();

        //Impresión
        private PrintDocument PD = new PrintDocument();
        private PrintPreviewDialog PPD = new PrintPreviewDialog();
        private int longpaper;

        public frmPrincipal()
        {
            InitializeComponent();
            InitilizeUI("UIMode");
            AsignarEventosBotonesMesas();
            CargarMetodosPago(cboMetodoPago);
            CargarCategoriasVenta(cboCategoriaVenta);
            ConfigurarTextBoxes();
            InactividadValidacion();
            enableDisableCaja();

            PD.BeginPrint += new PrintEventHandler(PD_BeginPrint);
            PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
        }

        private void InitilizeUI(string key)
        {
            try 
            {
                var uiMode = ConfigurationManager.AppSettings[key];
                if (uiMode == "light") 
                {
                    this.ForeColor = Color.FromArgb(47, 54, 54);
                    this.BackColor = Color.FromArgb(245, 246, 250);
                    ConfigurationManager.AppSettings[key]= "dark";
                }
                else 
                {
                    this.ForeColor = Color.FromArgb(245, 246, 250);
                    this.BackColor = Color.FromArgb(47, 54, 54);
                    ConfigurationManager.AppSettings[key] = "light";
                }
            }catch(Exception ex) 
            {
                throw new NotImplementedException();
            }
            
        }

        private void InactividadValidacion()
        {
            TemporizadorInactividad a = new TemporizadorInactividad();
            a.IniciarTemporizador();
            a.ManejarEventosActividadUsuario(this);

        }
        //Método para asignar automáticamente el evento Clic a los botones de mesa
        private void AsignarEventosBotonesMesas()
        {
            foreach (System.Windows.Forms.Control control in panelMesas.Controls)
            {
                if (control is FontAwesome.Sharp.IconButton iconButton && control.Name.StartsWith("icoM"))
                {
                    iconButton.Click += BtnMesa_Click;
                }
            }
        }
        //Método personalizar un textBox
        private void ConfigurarTextBox(TextBox textBox, string textoPlaceholder)
        {
            textBox.Text = textoPlaceholder;
            textBox.ForeColor = Color.Silver;

            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == textoPlaceholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (textBox.Text == "")
                {
                    textBox.Text = textoPlaceholder;
                    textBox.ForeColor = Color.Silver;
                }
            };
        }

        //Método para personalizar textBox necesarios en el form
        private void ConfigurarTextBoxes()
        {
            ConfigurarTextBox(txtDNI, " DNI ");
        }
        //Ajuste de tamaño del form acorde a la pantalla de ejecución
        private void Form1_Load(object sender, EventArgs e)
        {
            gunaLabel_date.Text = DateTime.Now.ToLongDateString();
            txtDNI.MaxLength = 8;
            // Obtener el tamaño de la pantalla actual
            Screen pantalla = Screen.PrimaryScreen;
            int anchoPantalla = pantalla.Bounds.Width;
            int altoPantalla = pantalla.Bounds.Height;

            // Calcular el nuevo tamaño del formulario
            int nuevoAnchoFormulario = anchoPantalla;
            int nuevoAltoFormulario = altoPantalla;

            // Establecer el nuevo tamaño del formulario
            this.Size = new System.Drawing.Size(nuevoAnchoFormulario, nuevoAltoFormulario);

            // Obtener la hora actual
            DateTime horaActual = DateTime.Now;

            // Obtener el segundo actual
            int segundoActual = horaActual.Second;

            //CentrarPanelEnPanelGeneral();
            hidePanelesNecesariosMenú();

            //Console.WriteLine(numCajaSeleccionada);
            
        }
        //Método de carga de métodos de pago en el comboBox
        private void CargarMetodosPago(ComboBox cboMetodoPago)
        {
            try
            {
                // Llamamos al método en la capa de datos para obtener los métodos de pago
                DataTable dtMetodosPago = logMetodoPago.Instancia.ObtenerMetodosPago();

                // Verificamos si se obtuvieron datos
                if (dtMetodosPago != null && dtMetodosPago.Rows.Count > 0)
                {
                    // Limpiamos el ComboBox antes de agregar nuevos elementos
                    cboMetodoPago.Items.Clear();

                    // Recorremos los datos y los agregamos al ComboBox
                    foreach (DataRow row in dtMetodosPago.Rows)
                    {
                        int idMetodoPago = Convert.ToInt32(row["idMetodoPago"]);
                        string nombreMetodoPago = row["nombreMetPag"].ToString();

                        // Agregamos el nombre del método de pago al ComboBox
                        cboMetodoPago.Items.Add(nombreMetodoPago);
                    }

                    // Opcional: Seleccionar el primer elemento en el ComboBox
                    if (cboMetodoPago.Items.Count > 0)
                    {
                        cboMetodoPago.SelectedIndex = 0;
                    }
                    cboMetodoPago.SelectedIndex = -1;
                }
                else
                {
                    // Manejo si no se obtuvieron datos
                    MessageBox.Show("No se encontraron métodos de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al cargar métodos de pago: " + ex.Message);
            }
        }
        private void CargarCategoriasVenta(ComboBox cbo)
        {
            try
            {
                // Llamamos al método en la capa de lógica para obtener las categorías de venta
                DataTable dtCategoriasVenta = logVenta.Instancia.ObtenerCategoriasVenta();

                // Verificamos si se obtuvieron datos
                if (dtCategoriasVenta != null && dtCategoriasVenta.Rows.Count > 0)
                {
                    // Limpiamos el ComboBox antes de agregar nuevos elementos
                    cbo.Items.Clear();

                    // Recorremos los datos y los agregamos al ComboBox
                    foreach (DataRow row in dtCategoriasVenta.Rows)
                    {
                        int idCategoriaVenta = Convert.ToInt32(row["idCategoriaVenta"]);
                        string nombreCategoriaVenta = row["nombreCategoria"].ToString();

                        // Agregamos el nombre de la categoría de venta al ComboBox
                        cbo.Items.Add(nombreCategoriaVenta);
                    }

                    // Opcional: Seleccionar el primer elemento en el ComboBox
                    if (cbo.Items.Count > 0)
                    {
                        cbo.SelectedIndex = 0;
                    }
                    cbo.SelectedIndex = -1;
                }
                else
                {
                    // Manejo si no se obtuvieron datos
                    MessageBox.Show("No se encontraron categorías de venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al cargar categorías de venta: " + ex.Message);
            }
        }

        private void enableDisableCaja() 
        {
            if (HayCajaAbierta())
            {
                btnAbrirCaja.Enabled = false;
            }
            else
            {
                btnCerrarCaja.Enabled = false;
            }
        }

        //Limpieza de panel de pedido
        private void LimpiarPanelPedido()
        {
            var controlesParaBorrar = panelPedido.Controls.OfType<UserControl2>().ToList();

            foreach (var control in controlesParaBorrar)
            {
                if (control.Name != panel1.Name)
                {
                    panelPedido.Controls.Remove(control);
                    control.Dispose(); // Liberar recursos
                }
            }
        }
        //Mostrar y actualizar
        private void MostrarImporteTotalEnLabel(int numeroMesa)
        {
            // Llama al método de la capa lógica y obtén el importe total
            decimal importeTotal = logPedidoComponente.Instancia.ObtenerImporteTotal(numeroMesa);

            // Asigna el importe total al texto del Label
            lblTotal.Text = importeTotal == 0 ? "____" : $"{importeTotal}";
        }
        public void MostrarUsuarioLogeado(string nombreUsuario)
        {
            //gunaLabel15.Text = nombreUsuario;
            lblNombreLogin.Text = nombreUsuario;
        }
        //Llenado de carta en el panel
        private void cargarElementosPanelCarta(string query)
        {
            // Limpiar el panel antes de cargar nuevos datos
            panelGeneral.Controls.Clear();

            string carpetaImagenes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Imagenes");

            try
            {
                int topMargin = 10;
                int leftMargin = 10;
                int spacing = 10;
                int maxWidth = panelGeneral.Width - 20;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nombreComponente = reader.GetString(0);
                                decimal precioComponente = reader.GetDecimal(1);
                                //Console.WriteLine($"Nombre: {nombreComponente}, Precio: {precioComponente}");

                                // Crea una instancia de tu UserControl1
                                UserControl1 userControl1 = new UserControl1();

                                // Llama al método ActualizarDatos con los valores de la base de datos
                                userControl1.ActualizarDatos(nombreComponente, precioComponente);

                                // Cargar la imagen para el componente
                                CargarImagenParaComponente(nombreComponente, userControl1, carpetaImagenes);

                                // Establece la ubicación del UserControl1 y agrégalo al panel
                                userControl1.Location = new Point(leftMargin, topMargin);
                                panelGeneral.Controls.Add(userControl1);

                                // Manejar el evento de selección
                                userControl1.ElementoSeleccionado += UserControl1_ElementoSeleccionado;

                                // Actualiza la posición para el próximo UserControl1
                                leftMargin += userControl1.Width + spacing;

                                // Si se supera el ancho máximo, reinicia en la siguiente fila
                                if (leftMargin + userControl1.Width > maxWidth)
                                {
                                    leftMargin = 10;
                                    topMargin += userControl1.Height + spacing;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Método para añadir img necesarias en la carta
        private void CargarComponentesPedidoBD(int numeroMesa)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verificar si la mesa tiene algún pedido
                string tienePedidoQuery = $"SELECT COUNT(*) FROM Pedido WHERE idMesa = {numeroMesa}";
                using (SqlCommand tienePedidoCommand = new SqlCommand(tienePedidoQuery, connection))
                {
                    int cantidadPedidos = Convert.ToInt32(tienePedidoCommand.ExecuteScalar());

                    if (cantidadPedidos == 0)
                    {
                        // La mesa no tiene pedidos, limpiar el panel y mostrar mensaje
                        LimpiarPanelPedido();
                        //MessageBox.Show("La mesa no tiene pedidos activos. Puede insertar nuevos elementos al pedido de la mesa");
                        return;
                    }
                }

                // Resto del código para cargar los componentes del pedido
                string query = $"SELECT c.descripcionComponente, pc.Cantidad, c.precioComponente FROM PedidoComponente pc " +
                                $"JOIN Componentes c ON pc.idComponente = c.idComponente " +
                                $"JOIN Pedido p ON pc.idPedido = p.idPedido " +
                                $"WHERE p.idMesa = {numeroMesa}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Establecer la posición de panel1 al principio del panelPedido
                        //panel1.Top = 0;
                        //panelPedido.Controls.Add(panel1);

                        LimpiarPanelPedido();

                        int verticalPosition = 0;

                        foreach (DataRow row in dataTable.Rows)
                        {
                            UserControl2 existingUserControl = BuscarUserControlEnPanel(row["descripcionComponente"].ToString());

                            if (existingUserControl != null)
                            {
                                // El componente ya existe en el panel, incrementar la cantidad
                                existingUserControl.Cantidad += Convert.ToInt32(row["Cantidad"]);

                                // Calcular e actualizar el importe total
                                decimal importeComponente = existingUserControl.Precio * existingUserControl.Cantidad;
                                // Asignar el importe al UserControl2
                                existingUserControl.Importe = importeComponente;
                            }
                            else
                            {
                                // El componente no existe en el panel, agregar uno nuevo
                                UserControl2 userControl2 = new UserControl2
                                {
                                    Nombre = row["descripcionComponente"].ToString(),
                                    Precio = Convert.ToDecimal(row["precioComponente"]),
                                    Cantidad = Convert.ToInt32(row["Cantidad"])
                                };

                                // Calcular e actualizar el importe total
                                decimal importeComponente = userControl2.Precio * userControl2.Cantidad;
                                // Asignar el importe al UserControl2
                                userControl2.Importe = importeComponente;

                                userControl2.CantidadChanged += (sender, e) =>
                                {
                                    OnCantidadChangedEnUserControl2(userControl2, numMesaSeleccionada);
                                };

                                userControl2.EliminarClick += (sender, e) => OnEliminarClick(userControl2, numeroMesa);

                                userControl2.Top = verticalPosition;

                                verticalPosition += userControl2.Height + 10;

                                // Ajustar el ancho del UserControl2 al ancho del panel
                                userControl2.Width = panelPedido.Width;

                                panelPedido.Controls.Add(userControl2);
                            }
                        }

                        string actualizarEstadoQuery = $"UPDATE Mesa SET estadoOcupado = 1 WHERE idMesa = {numeroMesa}";

                        using (SqlCommand actualizarEstadoCommand = new SqlCommand(actualizarEstadoQuery, connection))
                        {
                            actualizarEstadoCommand.ExecuteNonQuery();
                            //Console.WriteLine($"EstadoOcupado de Mesa {numeroMesa} actualizado a verdadero.");
                        }
                    }
                }

                // Mostrar el importe total en la etiqueta
                MostrarImporteTotalEnLabel(numMesaSeleccionada);
            }
        }
        //Método de selección de elemento de la carta
        private void UserControl1_ElementoSeleccionado(object sender, EventArgs e)
        {
            try {
                //Valifación en caso no se seleccione una mesa
                if (numMesaSeleccionada == 0)
                {
                    MessageBox.Show("SELECCIONE UNA MESA!!");
                }
                else if (sender is UserControl1 userControl1)
                {
                    // Crear una instancia de UserControl2 basada en la selección de UserControl1
                    UserControl2 userControl2 = CrearUserControl2DesdeUserControl1(userControl1);

                    // Agregar UserControl2 al panelPedido
                    panelPedido.Controls.Add(userControl2);

                    // Añadir el componente al PedidoComponente
                    AgregarComponenteAPedido(userControl1, userControl2);

                    // Llamada a CargarComponentesPedidoBD
                    CargarComponentesPedidoBD(numMesaSeleccionada);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Consulta de datos necesaria para obtencion de datos
        private int EjecutarScalarQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        //Método necesario para la obtención de datos
        private int ObtenerIdPedidoActual(int numeroMesa)
        {
            string query = $"SELECT TOP 1 p.idPedido FROM Pedido p " +
                           $"JOIN Mesa m ON p.idMesa = m.idMesa " +
                           $"WHERE m.numeroMesa = {numeroMesa} ORDER BY p.FechaPedido DESC";
            return EjecutarScalarQuery(query);
        }
        private int ObtenerIdTipoComponente(string nombreComponente)
        {
            string query = $"SELECT idTipoComponente FROM Componentes WHERE descripcionComponente = '{nombreComponente}'";
            return EjecutarScalarQuery(query);
        }
        private int ObtenerIdComponente(string nombreComponente)
        {
            string query = $"SELECT idComponente FROM Componentes WHERE descripcionComponente = '{nombreComponente}'";
            return EjecutarScalarQuery(query);
        }
        private int ObtenerCantidadComponenteDesdeBD(int idPedido, int idTipoComponente, int idComponente)
        {
            return logPedidoComponente.Instancia.ObtenerCantidadComponente(idPedido, idTipoComponente, idComponente);
        }
        private decimal ObtenerPrecioComponenteDesdeBD(int idComponente)
        {
            return logComponente.Instancia.ObtenerPrecioComponenteDesdeBD(idComponente);
        }



        //Métodos necesarios para la carga de resources
        private bool VerificarMesaOcupada(int numeroMesa)
        {
            numeroMesa = numMesaSeleccionada;
            return logMesa.Instancia.VerificarMesaOcupada(numeroMesa);
        }
        // Función para buscar un UserControl en el panel
        private UserControl2 BuscarUserControlEnPanel(string nombreComponente)
        {
            foreach (Control control in panelPedido.Controls)
            {
                if (control is UserControl2 userControl2 && userControl2.Nombre == nombreComponente)
                {
                    return userControl2;
                }
            }

            return null;
        }
        //Validación de existencia de imagenes en la BD
        private void CargarImagenParaComponente(string nombreComponente, UserControl1 userControl, string carpetaImagenes)
        {
            try
            {
                string nombreArchivo = $"{nombreComponente}.png";
                string rutaCompleta = Path.Combine(Application.StartupPath, carpetaImagenes, nombreArchivo);

                if (File.Exists(rutaCompleta))
                {
                    System.Drawing.Image imagen = System.Drawing.Image.FromFile(rutaCompleta);
                    userControl.BackgroundImage = imagen;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar imagen: {ex.Message}");
            }
        }
        private void CargarElementosPorQuery(string query)
        {
            panelGeneral.Controls.Clear();

            if (existePanelesNecesariosMenú())
            {
                cargarElementosPanelCarta(query);
            }
            else
            {
                showPanelesNecesariosMenú();
                cargarElementosPanelCarta(query);
            }
        }


        //Agreagado de componentes al formulario
        private void AgregarComponenteAPedido(UserControl1 userControl1, UserControl2 userControl2)
        {
            string nombreComponente = userControl1.Nombre;

            int idPedido = ObtenerIdPedidoActual(numMesaSeleccionada);
            int idTipoComponente = ObtenerIdTipoComponente(nombreComponente);
            int idComponente = ObtenerIdComponente(nombreComponente);

            // Obtener el importe del UserControl2
            decimal importeComponente = userControl2.Importe;

            int cantidadComponente = 0; // Declarar la variable fuera del bloque condicional

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verificar si la mesa está ocupada
                bool mesaOcupada = VerificarMesaOcupada(numMesaSeleccionada);

                if (!mesaOcupada)
                {
                    // La mesa no está ocupada, marcarla como ocupada
                    string marcarMesaOcupadaQuery = $"UPDATE Mesa SET estadoOcupado = 1 WHERE idMesa = {numMesaSeleccionada}";
                    using (SqlCommand marcarMesaOcupadaCommand = new SqlCommand(marcarMesaOcupadaQuery, connection))
                    {
                        marcarMesaOcupadaCommand.ExecuteNonQuery();
                        //Console.WriteLine($"EstadoOcupado de Mesa {numMesaSeleccionada} actualizado a verdadero.");
                    }
                }

                // Verificar si hay un pedido actual
                if (idPedido == -1)
                {
                    // No hay pedido, crear uno nuevo
                    string crearPedidoQuery = $"INSERT INTO Pedido (FechaPedido, idMesa, pagado) " +
                                              $"VALUES (GETDATE(), {numMesaSeleccionada}, 0);" +
                                              "SELECT SCOPE_IDENTITY() AS NewId";

                    using (SqlCommand crearPedidoCommand = new SqlCommand(crearPedidoQuery, connection))
                    {
                        object result = crearPedidoCommand.ExecuteScalar();

                        if (result != null)
                        {
                            idPedido = Convert.ToInt32(result);
                            //Console.WriteLine($"Nuevo idPedido: {idPedido}");
                        }
                    }
                }

                // Verificar si el componente ya existe en PedidoComponente
                string verificarExistenciaQuery = $"SELECT idPedidoComponentes, Cantidad FROM PedidoComponente " +
                                                  $"WHERE idPedido = {idPedido} " +
                                                  $"AND idTipoComponente = {idTipoComponente} " +
                                                  $"AND idComponente = {idComponente}";

                using (SqlCommand verificarExistenciaCommand = new SqlCommand(verificarExistenciaQuery, connection))
                {
                    using (SqlDataReader reader = verificarExistenciaCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // El componente ya existe, actualizar la cantidad y el importe
                            int idPedidoComponentes = reader.GetInt32(0);
                            cantidadComponente = reader.GetInt32(1); // Asignar el valor a la variable

                            int nuevaCantidad = cantidadComponente + 1;

                            // Utilizar el setter de UserControl2 para actualizar la cantidad
                            userControl2.Cantidad = nuevaCantidad;

                            // Calcular el nuevo importe total
                            decimal precioComponente = ObtenerPrecioComponenteDesdeBD(idComponente); // Obtener precio desde la tabla Componentes
                            decimal importeNuevoComponente = precioComponente * nuevaCantidad;

                            // Cerrar el primer DataReader antes de ejecutar otro comando
                            reader.Close();

                            // Actualizar la cantidad y el importe en PedidoComponente
                            string actualizarCantidadQuery = $"UPDATE PedidoComponente " +
                                                             $"SET Cantidad = {nuevaCantidad}, " +
                                                             $"importeComponente = {importeNuevoComponente} " +
                                                             $"WHERE idPedidoComponentes = {idPedidoComponentes}";

                            using (SqlCommand actualizarCantidadCommand = new SqlCommand(actualizarCantidadQuery, connection))
                            {
                                actualizarCantidadCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Cerrar el primer DataReader antes de ejecutar otro comando
                            reader.Close();

                            // Utilizar el setter de UserControl2 para actualizar la cantidad y calcular el importe
                            userControl2.Cantidad = 1;

                            // Calcular el importe total basado en la tabla Componentes
                            decimal precioComponente = ObtenerPrecioComponenteDesdeBD(idComponente); // Obtener precio desde la tabla Componentes
                            decimal importeNuevoComponente = (importeComponente != 0) ? importeComponente : precioComponente;

                            // Insertar el nuevo componente con el importe calculado
                            string agregarComponenteQuery = $"INSERT INTO PedidoComponente (idPedido, idTipoComponente, idComponente, Cantidad, importeComponente) " +
                                                            $"VALUES ({idPedido}, {idTipoComponente}, {idComponente}, 1, {importeNuevoComponente})";

                            using (SqlCommand agregarComponenteCommand = new SqlCommand(agregarComponenteQuery, connection))
                            {
                                agregarComponenteCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        private UserControl2 CrearUserControl2DesdeUserControl1(UserControl1 userControl1)
        {
            UserControl2 userControl2 = new UserControl2();

            // Copiar o configurar propiedades de UserControl1 a UserControl2 según sea necesario
            userControl2.Nombre = userControl1.Nombre;  // Ejemplo, ajusta según las propiedades que tengas en UserControl1
            userControl2.Precio = userControl1.Precio;

            return userControl2;
        }



        //Métodos para selección o uso de elementos del form
        private void OnEliminarClick(UserControl2 userControl2, int numeroMesa)
        {
            numeroMesa = numMesaSeleccionada;

            // Obtener información del UserControl2
            string nombreComponente = userControl2.Nombre;
            decimal precioComponente = userControl2.Precio;
            int cantidadComponente = userControl2.Cantidad;

            // Aquí debes implementar la lógica para eliminar el componente del pedido
            // Utiliza la información obtenida para identificar el componente específico
            // y ejecuta la eliminación en tu base de datos.

            string eliminarComponenteQuery = $"DELETE FROM PedidoComponente WHERE idPedido IN " +
                                    $"(SELECT p.idPedido FROM Pedido p " +
                                    $"JOIN Mesa m ON p.idMesa = m.idMesa " +
                                    $"WHERE m.idMesa = {numeroMesa} " +
                                    $"AND idComponente IN (SELECT idComponente FROM Componentes " +
                                    $"WHERE descripcionComponente = '{nombreComponente}' " +
                                    $"AND precioComponente = {precioComponente} " +
                                    $"AND Cantidad = {cantidadComponente}))";

            // Luego, ejecutas la consulta en tu base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(eliminarComponenteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Verificar si el pedido está sin componentes
                string verificarPedidoSinComponentesQuery = $"SELECT COUNT(*) FROM PedidoComponente WHERE idPedido IN " +
                                                            $"(SELECT idPedido FROM Pedido WHERE idMesa = {numeroMesa})";

                using (SqlCommand verificarPedidoSinComponentesCommand = new SqlCommand(verificarPedidoSinComponentesQuery, connection))
                {
                    int cantidadComponentes = Convert.ToInt32(verificarPedidoSinComponentesCommand.ExecuteScalar());

                    if (cantidadComponentes == 0)
                    {
                        // Si no hay más componentes en el pedido, eliminar el pedido
                        string eliminarPedidoQuery = $"DELETE FROM Pedido WHERE idMesa = {numeroMesa}";

                        using (SqlCommand eliminarPedidoCommand = new SqlCommand(eliminarPedidoQuery, connection))
                        {
                            eliminarPedidoCommand.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Volver a cargar los componentes del pedido después de la eliminación
            CargarComponentesPedidoBD(numeroMesa);
            MostrarImporteTotalEnLabel(numMesaSeleccionada);
        }
        private void OnCantidadChangedEnUserControl2(UserControl2 userControl2, int numMesaSeleccionada)
        {
            try
            {
                int nuevaCantidad = userControl2.Cantidad;
                string nombreComponente = userControl2.Nombre;
                decimal precioComponente = userControl2.Precio;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Obtener el idPedido desde la tabla Pedido
                    int idPedido;
                    string obtenerIdPedidoQuery = $"SELECT idPedido FROM Pedido WHERE idMesa = {numMesaSeleccionada}";

                    using (SqlCommand obtenerIdPedidoCommand = new SqlCommand(obtenerIdPedidoQuery, connection))
                    {
                        idPedido = Convert.ToInt32(obtenerIdPedidoCommand.ExecuteScalar());
                    }

                    if (nuevaCantidad == 0)
                    {
                        // Si la nueva cantidad es cero, eliminar el componente del pedido
                        string eliminarComponenteQuery =
                            "DELETE FROM PedidoComponente " +
                            "WHERE idPedido = @idPedido " +
                            "AND idComponente = (SELECT idComponente FROM Componentes " +
                            "WHERE descripcionComponente = @nombreComponente " +
                            "AND precioComponente = @precioComponente)";

                        // Ejecutar la consulta para eliminar el componente
                        using (SqlCommand eliminarCommand = new SqlCommand(eliminarComponenteQuery, connection))
                        {
                            eliminarCommand.Parameters.AddWithValue("@idPedido", idPedido);
                            eliminarCommand.Parameters.AddWithValue("@nombreComponente", nombreComponente);
                            eliminarCommand.Parameters.AddWithValue("@precioComponente", precioComponente);

                            eliminarCommand.ExecuteNonQuery();
                        }

                        // Verificar si el pedido está sin componentes
                        string verificarPedidoSinComponentesQuery =
                            "SELECT COUNT(*) FROM PedidoComponente WHERE idPedido = @idPedido";

                        using (SqlCommand verificarPedidoSinComponentesCommand = new SqlCommand(verificarPedidoSinComponentesQuery, connection))
                        {
                            verificarPedidoSinComponentesCommand.Parameters.AddWithValue("@idPedido", idPedido);

                            int cantidadComponentes = Convert.ToInt32(verificarPedidoSinComponentesCommand.ExecuteScalar());

                            if (cantidadComponentes == 0)
                            {
                                // Si no hay más componentes en el pedido, eliminar el pedido
                                string eliminarPedidoQuery = "DELETE FROM Pedido WHERE idPedido = @idPedido";

                                using (SqlCommand eliminarPedidoCommand = new SqlCommand(eliminarPedidoQuery, connection))
                                {
                                    eliminarPedidoCommand.Parameters.AddWithValue("@idPedido", idPedido);
                                    eliminarPedidoCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        lblTotal.Text = "___";
                        // Eliminar el UserControl2 del panel
                        panelPedido.Controls.Remove(userControl2);
                    }
                    else
                    {
                        // Actualizar la cantidad en la base de datos
                        string actualizarCantidadQuery =
                            "UPDATE PedidoComponente SET Cantidad = @nuevaCantidad, " +
                            "importeComponente = @nuevaCantidad * @precioComponente " +
                            "WHERE idPedido = @idPedido " +
                            "AND idComponente = (SELECT idComponente FROM Componentes " +
                            "WHERE descripcionComponente = @nombreComponente " +
                            "AND precioComponente = @precioComponente)";

                        // Ejecutar la consulta para actualizar la cantidad y el importeComponente
                        using (SqlCommand actualizarCommand = new SqlCommand(actualizarCantidadQuery, connection))
                        {
                            actualizarCommand.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad);
                            actualizarCommand.Parameters.AddWithValue("@idPedido", idPedido);
                            actualizarCommand.Parameters.AddWithValue("@nombreComponente", nombreComponente);
                            actualizarCommand.Parameters.AddWithValue("@precioComponente", precioComponente);

                            actualizarCommand.ExecuteNonQuery();
                        }

                        MostrarImporteTotalEnLabel(numMesaSeleccionada);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            CargarComponentesPedidoBD(numMesaSeleccionada);

        }
        private void BtnMesa_Click(object sender, EventArgs e)
        {
            // Obtener el número de mesa desde el nombre del botón
            if (sender is FontAwesome.Sharp.IconButton iconButton && int.TryParse(iconButton.Name.Substring(4), out int numeroMesa))
            {
                if (numeroMesa==21) 
                {
                    CargarComponentesPedidoBD(numeroMesa);
                    lblMesaSeleccion.Text = "Para llevar";
                    numMesaSeleccionada = numeroMesa;
                    MostrarImporteTotalEnLabel(numMesaSeleccionada);
                }
                else
                {
                    CargarComponentesPedidoBD(numeroMesa);
                    lblMesaSeleccion.Text = "Se encuentra en la mesa: " + " " + numeroMesa.ToString();
                    numMesaSeleccionada = numeroMesa;
                    MostrarImporteTotalEnLabel(numMesaSeleccionada);
                }
               
            }
        }



        //Métodos para la selección de carta
        private void btnCombos_Click(object sender, EventArgs e)
        {
            CargarElementosPorQuery("SELECT descripcionComponente, precioComponente FROM Componentes WHERE idCategoriaPlato IS NOT NULL AND descripcionComponente LIKE '%COMBO%';");
        }
        private void gunaGradientTileButton1_Click(object sender, EventArgs e)
        {
            CargarElementosPorQuery("SELECT descripcionComponente, precioComponente FROM Componentes WHERE idCategoriaPlato IS NOT NULL AND descripcionComponente NOT LIKE '%COMBO%';");
        }
        private void btnBebidas_Click(object sender, EventArgs e)
        {
            CargarElementosPorQuery("SELECT C.descripcionComponente, C.precioComponente\r\nFROM Componentes C\r\nJOIN TipoComponentes TC ON C.idTipoComponente = TC.idTipoComponente\r\nWHERE TC.descripcionTipoComponente = 'Bebida';");
        }
        private void btnAdicionales_Click(object sender, EventArgs e)
        {
            CargarElementosPorQuery("SELECT C.descripcionComponente, C.precioComponente\r\nFROM Componentes C\r\nJOIN TipoComponentes TC ON C.idTipoComponente = TC.idTipoComponente\r\nWHERE TC.descripcionTipoComponente = 'Adicional';");
        }



        //Busqueda de clientes por medio del API
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";

                // Construir la URL según el tipo de documento
                if (txtDNI.Text.Length == 11)
                {
                    url = "https://dniruc.apisperu.com/api/v1/ruc/" + txtDNI.Text + "?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6InlvcmR5NjVAaG90bWFpbC5jb20ifQ.Rjx0z1TkmXho4t-vD1Tehaxch1Ko_p90MaG72v2NHFg";
                }
                else if (txtDNI.Text.Length == 8)
                {
                    url = "https://dniruc.apisperu.com/api/v1/dni/" + txtDNI.Text + "?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6InlvcmR5NjVAaG90bWFpbC5jb20ifQ.Rjx0z1TkmXho4t-vD1Tehaxch1Ko_p90MaG72v2NHFg";
                }
                else
                {
                    MessageBox.Show("Ingrese un número de documento válido.", "Documento inválido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Llamada a la capa lógica para obtener información desde la API
                dynamic respuesta = logApi.Get(url); // Cambié ObtenerDataDesdeApi a Get

                if (respuesta != null)
                {
                    if (txtDNI.Text.Length == 11)
                    {
                        lblNombre.Text = respuesta.razonSocial.ToString();
                        lblApellido.Text = respuesta.direccion.ToString();
                    }
                    else if (txtDNI.Text.Length == 8)
                    {
                        lblNombre.Text = respuesta.nombres.ToString();
                        lblApellido.Text = respuesta.apellidoPaterno.ToString() + " " + respuesta.apellidoMaterno.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudieron obtener los resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InsertarVenta(int idPedido, int idMetodoPago, string nombreCliente, string apellidoCliente, string dniCliente, string nombreCatVen, decimal total)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Inicia una transacción
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            DateTime fechaVenta = DateTime.Now;

                            // Inserta en la tabla Venta
                            string queryVenta = @"
                            INSERT INTO Venta (FechaVenta, idPedido, idMetodoPago, NombreCliente, ApellidoCliente, DNICliente, idCategoríaVenta, ImporteVenta)
                            VALUES (@FechaVenta, @IdPedido, @IdMetodoPago, @NombreCliente, @ApellidoCliente, @DNICliente, @IdCategoriaVenta, @ImporteVenta)";

                            using (SqlCommand commandVenta = new SqlCommand(queryVenta, connection, transaction))
                            {
                                commandVenta.Parameters.AddWithValue("@FechaVenta", fechaVenta);
                                commandVenta.Parameters.AddWithValue("@IdPedido", idPedido);
                                commandVenta.Parameters.AddWithValue("@IdMetodoPago", idMetodoPago);
                                commandVenta.Parameters.AddWithValue("@NombreCliente", string.IsNullOrEmpty(nombreCliente) ? (object)DBNull.Value : nombreCliente);
                                commandVenta.Parameters.AddWithValue("@ApellidoCliente", string.IsNullOrEmpty(apellidoCliente) ? (object)DBNull.Value : apellidoCliente);
                                commandVenta.Parameters.AddWithValue("@DNICliente", string.IsNullOrEmpty(dniCliente) ? (object)DBNull.Value : dniCliente);
                                commandVenta.Parameters.AddWithValue("@IdCategoriaVenta", cboCategoriaVenta.SelectedIndex + 1);
                                commandVenta.Parameters.AddWithValue("@ImporteVenta", total);
                                commandVenta.ExecuteNonQuery();
                            }

                            // Confirma la transacción
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // En caso de error, realiza un rollback
                            transaction.Rollback();
                            throw; // Re-lanza la excepción para que pueda ser manejada por el bloque catch externo
                        }
                    }
                }

                MessageBox.Show("La venta se ha registrado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la venta: {ex.Message}");
            }
        }
        private void ActualizarCajaDespuesDeVenta(int idCaja, decimal importeVenta, int idCategoriaVenta, int idMetodoPago)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Inicia una transacción
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Actualiza la tabla Caja
                            string queryActualizarCaja = @"
                            UPDATE Caja
                            SET 
                                MontoFinal = COALESCE(MontoFinal, 0) + @ImporteVenta,
                                TotalVentasBoleta = ISNULL(TotalVentasBoleta, 0) + CASE WHEN @idCategoríaVenta = 1 THEN @ImporteVenta ELSE 0 END,
                                TotalVentasFactura = ISNULL(TotalVentasFactura, 0) + CASE WHEN @idCategoríaVenta = 2 THEN @ImporteVenta ELSE 0 END,
                                TotalVentasNotaVenta = ISNULL(TotalVentasNotaVenta, 0) + CASE WHEN @idCategoríaVenta = 3 THEN @ImporteVenta ELSE 0 END,
                                TotalVentasYape = ISNULL(TotalVentasYape, 0) + CASE WHEN @idMetodoPago = 1 THEN @ImporteVenta ELSE 0 END,
                                TotalVentasPlin = ISNULL(TotalVentasPlin, 0) + CASE WHEN @idMetodoPago = 2 THEN @ImporteVenta ELSE 0 END,
                                TotalVentasEfectivo = ISNULL(TotalVentasEfectivo, 0) + CASE WHEN @idMetodoPago = 3 THEN @ImporteVenta ELSE 0 END,
                                TotalVentasTarjeta = ISNULL(TotalVentasTarjeta, 0) + CASE WHEN @idMetodoPago = 4 THEN @ImporteVenta ELSE 0 END
                            WHERE idCaja = @IdCaja;";

                            using (SqlCommand commandActualizarCaja = new SqlCommand(queryActualizarCaja, connection, transaction))
                            {
                                commandActualizarCaja.Parameters.AddWithValue("@IdCaja", idCaja);
                                commandActualizarCaja.Parameters.AddWithValue("@ImporteVenta", importeVenta);
                                commandActualizarCaja.Parameters.AddWithValue("@idCategoríaVenta", idCategoriaVenta);
                                commandActualizarCaja.Parameters.AddWithValue("@idMetodoPago", idMetodoPago);
                                commandActualizarCaja.ExecuteNonQuery();
                            }

                            // Confirma la transacción
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // En caso de error, realiza un rollback
                            transaction.Rollback();
                            throw; // Re-lanza la excepción para que pueda ser manejada por el bloque catch externo
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la caja: {ex.Message}");

            }
        }




        private int ObtenerIdVentaInsertada()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta para obtener el último ID de venta insertado
                    string query = "SELECT IDENT_CURRENT('Venta') AS LastInsertedID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Convertir el resultado a entero
                            return Convert.ToInt32(result)+1;
                        }
                    }
                }

                return -1; // Retorna -1 si no se pudo obtener el ID
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener el ID de la venta insertada: {ex.Message}");
                return -1; // Otra forma de indicar un error
            }
        }

        private bool elementosVacios() 
        {
            if(cboCategoriaVenta.SelectedIndex==-1 ||cboMetodoPago.SelectedIndex==-1) 
            { 
                return false; 
            }
            else 
            {
                return true;
            }
        }
        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            bool tieneComponentes = logMesa.Instancia.VerificarMesaOcupada(numMesaSeleccionada);
            //int idVentaInsertada = ObtenerIdVentaInsertada();

            if (!ContieneNumero(lblMesaSeleccion.Text))
            {
                MessageBox.Show("Por favor, seleccione una mesa.");
            }
            else if (!tieneComponentes)
            {
                MessageBox.Show("La mesa no cuenta con pedidos");
                //Console.WriteLine("no tiene pedidos");
            }
            else if(!elementosVacios()) 
            {
                MessageBox.Show("VERIFIQUE LOS CAMPOS PORFAVOR");
            }
            else
            {
                try
                {   
                    int idPedido = ObtenerIdPedidoActual(numMesaSeleccionada);
                    int idMetodoPago = cboMetodoPago.SelectedIndex + 1;
                    string nombreCliente = lblNombre.Text;
                    string apellidoCliente = lblApellido.Text;
                    string dniCliente = txtDNI.Text;
                    string nombreCatVen = cboCategoriaVenta.Text;
                    decimal total = decimal.Parse(lblTotal.Text);

                    bool esNotaVenta = cboCategoriaVenta.Text == "Nota de Venta";
                    bool contieneDNI = string.IsNullOrEmpty(txtDNI.Text);


                    // Validar que se haya seleccionado un elemento en los ComboBox
                    if (cboMetodoPago.SelectedIndex == -1 || cboCategoriaVenta.SelectedIndex == -1)
                    {
                        MessageBox.Show("Por favor, seleccione un método de pago y una categoría de venta.");
                    }
                    else if (!esNotaVenta && !contieneDNI && txtDNI.Text.Length != 8)
                    {
                        MessageBox.Show("Verifique el DNI");
                    }
                    else
                    {
                        // Llama al método para insertar en la tabla Venta
                        InsertarVenta(idPedido, idMetodoPago, nombreCliente, apellidoCliente, dniCliente, nombreCatVen, total);
                        ActualizarCajaDespuesDeVenta(numCajaSeleccionada, total, cboCategoriaVenta.SelectedIndex+1, cboMetodoPago.SelectedIndex+1);

                        // Limpieza de elementos usados en el formulario
                        lblTotal.Text = "";
                        txtDNI.Text = "";
                        lblNombre.Text = "";
                        lblApellido.Text = "";
                        cboCategoriaVenta.SelectedIndex = -1;
                        cboMetodoPago.SelectedIndex = -1;

                        numMesaSeleccionada = 0;
                        lblMesaSeleccion.Text = "No se encuentra posicionado en ninguna mesa";

                        MostrarImporteTotalEnLabel(numMesaSeleccionada);
                        CargarComponentesPedidoBD(numMesaSeleccionada);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar la venta: {ex.Message}");
                }
            }
        }

        // Función para verificar si una cadena contiene algún dígito numérico
        bool ContieneNumero(string input)
        {
            if (input=="Para llevar") 
            {
                return true;
            }
            else 
            {
                return input.Any(char.IsDigit);
            }
        }

        private void DeslogeoUp()
        {
            logUsuarios lg = new logUsuarios();
            int n = lg.ObtenerIdUsuarioLogeado(lblNombreLogin.Text);
            lg.establecerEstado(n, false);
        }


        //Salida del sistema
        private void gunaControlBox1_Click_1(object sender, EventArgs e)
        {
            DeslogeoUp();
            Application.Exit();
        }
        

        private bool existePanelesNecesariosMenú()
        {
            // Verifica si al menos uno de los paneles está visible
            if (panelVenta.Visible || panelMesas.Visible || gunaGradient2Panel1.Visible)
            {
                return true;
            }

            return false;
        }
        private void hidePanelesNecesariosMenú()
        {
            panelVenta.Hide();
            //panelVenta.Dispose();

            panelMesas.Hide();
            //panelMesas.Dispose();

            gunaGradient2Panel1.Hide();
            //gunaGradient2Panel1.Dispose();

            btnAbrirCaja.Hide();
            btnCerrarCaja.Hide();
        }
        
        private void showPanelesNecesariosMenú()
        {
            panelVenta.Show();
            panelMesas.Show();
            gunaGradient2Panel1.Show();

            btnAbrirCaja.Show();
            btnCerrarCaja.Show();
        }   

        private void btnCartaEditar_Click(object sender, EventArgs e)
        {
            panelGeneral.Controls.Clear();
            
            frmCartaEdit frm = new frmCartaEdit();
            if (existePanelesNecesariosMenú()) 
            {
                hidePanelesNecesariosMenú();
                // Acceder al panel del formulario secundario
                Panel panelSecundario = frm.ObtenerPanelCartaEdit();
                // Asignar el panel del formulario secundario al panel del formulario principal
                panelGeneral.Controls.Add(panelSecundario);
            }
            else 
            {
                // Acceder al panel del formulario secundario
                Panel panelSecundario = frm.ObtenerPanelCartaEdit();

                // Asignar el panel del formulario secundario al panel del formulario principal
                panelGeneral.Controls.Add(panelSecundario);
            }
        }

        private void btnRegistroFacturas_Click(object sender, EventArgs e)
        {
            panelGeneral.Controls.Clear();
            
            frmVentas frm = new frmVentas();
            // Acceder al panel del formulario secundario
            Panel panelSecundario = frm.ObtenerPanelVenta();

            if (existePanelesNecesariosMenú())
            {
                hidePanelesNecesariosMenú();
                CargarCategoriasVenta(frm.ObtenerComboBoxVenta());
                CargarMetodosPago(frm.ObtenerComboBoxPago());
                // Asignar el panel del formulario secundario al panel del formulario principal
                panelGeneral.Controls.Add(panelSecundario);

            }
            else
            {
                CargarCategoriasVenta(frm.ObtenerComboBoxVenta());
                CargarMetodosPago(frm.ObtenerComboBoxPago());
                // Asignar el panel del formulario secundario al panel del formulario principal
                panelGeneral.Controls.Add(panelSecundario);
            }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada no es un número o la tecla de retroceso
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Desactivar la tecla presionada
            }
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            // Verificar si hay tres números consecutivos iguales en el TextBox
            if (HayTresNumerosConsecutivos(txtDNI.Text))
            {
                //MessageBox.Show("No se permiten tres números consecutivos iguales en el DNI.");
                // Puedes borrar el contenido del TextBox o tomar otra acción según tus necesidades
                txtDNI.Text = "";
            }
        }

        // Método para verificar si hay tres números consecutivos iguales
        private bool HayTresNumerosConsecutivos(string texto)
        {
            for (int i = 0; i < texto.Length - 3; i++)
            {
                if (texto[i] == texto[i + 1] && texto[i] == texto[i + 2] && texto[i] == texto[i + 3])
                {
                    // Si hay cuatro números consecutivos iguales, retorna true
                    return true;
                }
            }
            return false;
        }
        //Salida del sistema
        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DeslogeoUp();
                Application.Exit();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            panelGeneral.Controls.Clear();
            if (existePanelesNecesariosMenú()) 
            {
                lblMesaSeleccion.Text = "No se encuentra posicionado en ninguna mesa";
                LimpiarPanelPedido();
                lblTotal.Text = "00.00";
            }
            else
            {
                hidePanelesNecesariosMenú();
            }
        }

   

        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            InitilizeUI("UIMode");
        }
        
        
        private bool MesaTieneComponentesEnPedido(int numeroMesa)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verificar si la mesa tiene algún pedido
                string tienePedidoQuery = $"SELECT COUNT(*) FROM Pedido WHERE idMesa = {numeroMesa}";

                using (SqlCommand tienePedidoCommand = new SqlCommand(tienePedidoQuery, connection))
                {
                    int cantidadPedidos = Convert.ToInt32(tienePedidoCommand.ExecuteScalar());

                    if (cantidadPedidos == 0)
                    {
                        // La mesa no tiene pedidos
                        return false;
                    }
                }

                // Verificar si la mesa tiene componentes en el pedido
                string tieneComponentesQuery = $"SELECT COUNT(*) FROM PedidoComponente pc " +
                                               $"JOIN Pedido p ON pc.idPedido = p.idPedido " +
                                               $"WHERE p.idMesa = {numeroMesa}";

                using (SqlCommand tieneComponentesCommand = new SqlCommand(tieneComponentesQuery, connection))
                {
                    int cantidadComponentes = Convert.ToInt32(tieneComponentesCommand.ExecuteScalar());

                    return cantidadComponentes > 0;
                }
            }
        }

        private void btnPrePedido_Click(object sender, EventArgs e)
        {
            try
            {
                if(numMesaSeleccionada == 0 || numMesaSeleccionada == null)
                {
                    MessageBox.Show("NO SE ENCUENTRA UBICADO EN NINGUNA MESA");
                }
                else if(!MesaTieneComponentesEnPedido(numMesaSeleccionada)) 
                {
                    MessageBox.Show("NO HAY PEDIDOS INGRESADOS");
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
                int idPedido = ObtenerIdPedidoActual(numMesaSeleccionada);

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

                // Cuadro de información de la factura
                RectangleF facturaRectangle = new RectangleF((e.PageBounds.Width - logoWidth) / 2, 80, logoWidth + 10, 30);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), Rectangle.Round(facturaRectangle));
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString("DETALLES DE PEDIDO", f10b, Brushes.Black, facturaRectangle, centerFormat);

                e.Graphics.DrawString(line, f8, Brushes.Black, 0, facturaRectangle.Bottom + 8); // Ajustar el eje Y

                // Detalles de los componentes
                int height = 0;
                float fontSizeSmall = 7;
                Font f8Small = new Font("Arial", fontSizeSmall, FontStyle.Regular);

                e.Graphics.DrawString("Cant", f8Small, Brushes.Black, 0, facturaRectangle.Bottom + 15); // Ajustar el eje Y
                e.Graphics.DrawString("Descripción", f8Small, Brushes.Black, 25, facturaRectangle.Bottom + 15); // Ajustar el eje Y
                e.Graphics.DrawString("Unitario", f8Small, Brushes.Black, 180, facturaRectangle.Bottom + 15, right);
                e.Graphics.DrawString("Total", f8Small, Brushes.Black, rightmargin, facturaRectangle.Bottom + 15, right);
                e.Graphics.DrawString(line, f8, Brushes.Black, 0, facturaRectangle.Bottom + 25); // Ajustar el eje Y

                int lastPosition = 0;

                // Obtener los componentes del pedido
                List<EntPedidoComponente> componentesPedido = logPedidoComponente.Instancia.ObtenerComponentesPedido(idPedido);

                foreach (EntPedidoComponente componente in componentesPedido)
                {
                    height += 15;
                    e.Graphics.DrawString(componente.Cantidad.ToString(), f8, Brushes.Black, 0, facturaRectangle.Bottom + 20 + height); // Ajustar el eje Y
                    e.Graphics.DrawString(" " + componente.NombreComponente, f8, Brushes.Black, 25, facturaRectangle.Bottom + 20 + height); // Ajustar el eje Y

                    decimal precioUnitario = Convert.ToDecimal(componente.ImporteComponente);
                    e.Graphics.DrawString(precioUnitario.ToString("##,##0"), f8, Brushes.Black, 180, facturaRectangle.Bottom + 20 + height, right);

                    decimal totalprice = componente.Cantidad * precioUnitario;
                    e.Graphics.DrawString(totalprice.ToString("##,##0"), f8, Brushes.Black, rightmargin, facturaRectangle.Bottom + 20 + height, right);

                    lastPosition = (int)facturaRectangle.Bottom + 20 + height; // Ajustar el eje Y
                }
                // Línea separadora
                e.Graphics.DrawString(lineS, f8, Brushes.Black, 0, lastPosition + 10); // Ajustar el eje Y

                // Total, Items, Agradecimientos, etc.
                e.Graphics.DrawString($"Total: S/. {lblTotal.Text}", f10b, Brushes.Black, rightmargin, lastPosition + 25, right);
                e.Graphics.DrawString($"Items: {componentesPedido.Count}", f10, Brushes.Black, 0, lastPosition + 25);

                // Alinea la imagen del código QR
                int qrCodeWidth = 100;  // Ancho del código QR
                int qrCodeHeight = 100;  // Altura del código QR
                e.Graphics.DrawImage(qrTest, centermargin - qrCodeWidth / 2, lastPosition + 50, qrCodeWidth, qrCodeHeight);

                e.Graphics.DrawString("~ GRACIAS POR PREFERIRNOS ~", f10, Brushes.Black, centermargin, lastPosition + 155, center);
                e.Graphics.DrawString("~ Documento generado por: SISTEMA POS ~", f8, Brushes.Black, centermargin, lastPosition + 170, center);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir el detalle del pedido: {ex.Message}");
            }
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            panelGeneral.Controls.Clear();

            frmCajaReporte frm = new frmCajaReporte();
            if (existePanelesNecesariosMenú())
            {
                hidePanelesNecesariosMenú();
                // Acceder al panel del formulario secundario
                Panel panelSecundario = frm.ObtenerPanelVenta();
                // Asignar el panel del formulario secundario al panel del formulario principal
                panelGeneral.Controls.Add(panelSecundario);
            }
            else
            {
                // Acceder al panel del formulario secundario
                Panel panelSecundario = frm.ObtenerPanelVenta();

                // Asignar el panel del formulario secundario al panel del formulario principal
                panelGeneral.Controls.Add(panelSecundario);
            }
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar confirmación antes de abrir la caja
                DialogResult result = MessageBox.Show("¿Seguro que quieres abrir la caja?", "Confirmar apertura de caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Obtener la fecha de apertura (puedes ajustar esto según tu lógica)
                    DateTime fechaApertura = DateTime.Now;

                    // Llamar al método del log para insertar una nueva caja
                    logCaja.Instancia.InsertarNuevaCaja(fechaApertura);

                    // Obtener el ID de la caja abierta más reciente
                    numCajaSeleccionada = logCaja.Instancia.ObtenerIdCajaAbierta();

                    btnAbrirCaja.Enabled = false;
                    btnCerrarCaja.Enabled = true;
                    MessageBox.Show("Caja abierta exitosamente");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la apertura de la caja
                MessageBox.Show("Error al abrir la caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (numCajaSeleccionada != -1)
                {
                    // Mostrar confirmación antes de cerrar la caja
                    DialogResult result = MessageBox.Show($"¿Seguro que quieres cerrar la caja con ID {numCajaSeleccionada}?", "Confirmar cierre de caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Llamar al método del log para cerrar la caja
                        logCaja.Instancia.CerrarCaja(numCajaSeleccionada);

                        btnCerrarCaja.Enabled = false;
                        btnAbrirCaja.Enabled = true;
                        MessageBox.Show($"Caja cerrada exitosamente. ID de la caja cerrada: {numCajaSeleccionada}");
                    }
                }
                else
                {
                    MessageBox.Show("No hay caja abierta para cerrar.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el cierre de la caja
                MessageBox.Show("Error al cerrar la caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool HayCajaAbierta() 
        {
            if (logCaja.Instancia.ExistenCajasAbiertas())
            {
                return true;
            }
            else return false;   
        }

        

       
    }
}

