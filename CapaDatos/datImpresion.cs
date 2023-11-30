using CapaeEntidad;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDatos
{
    public class datImpresion
    {
        public string rutaCompletaParaImprimir;
        public static async void GenerarDocumentosVenta(string nombreArchivoPDF, string nombreArchivoHTML, string fechaVenta, int idVenta, int idPedido, int idMetodoPago, string numRUC, string nombreCliente, string apellidoCliente, string dniCliente, string nombreCatVen, string nombreMetodoPago, List<EntPedidoComponente> componentesPedido, decimal importeTotal)
        {
            try
            {
                // Genera archivo HTML de manera asíncrona
                await GenerarHTMLDesdeDatos(nombreArchivoHTML, fechaVenta, idVenta, idPedido, idMetodoPago, numRUC, nombreCliente, apellidoCliente, dniCliente, nombreCatVen, nombreMetodoPago, componentesPedido, importeTotal);
                // El resto de tu código aquí
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar los documentos: {ex.Message}");
            }
        }
        public static async void GenerarDocumentosCaja(string nombreArchivoPDF, string nombreArchivoHTML, DateTime fechaApertura, DateTime? fechaCierre, bool estadoCaja, decimal montoInicial, decimal? montoFinal,
        decimal? totalVentasBoleta, decimal? totalVentasFactura, decimal? totalVentasNotaVenta, decimal? totalVentasYape, decimal? totalVentasPlin, decimal? totalVentasEfectivo, decimal? totalVentasTarjeta, decimal? gastos)
        {
            try
            {
                // Genera archivo HTML de manera asíncrona
                await GenerarHTMLCajaDesdeDatos(nombreArchivoHTML, fechaApertura, fechaCierre, estadoCaja, montoInicial, montoFinal, totalVentasBoleta, totalVentasFactura, totalVentasNotaVenta, totalVentasYape, totalVentasPlin, totalVentasEfectivo, totalVentasTarjeta, gastos);
                // El resto de tu código aquí
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar los documentos de la caja: {ex.Message}");
            }
        }

        public static async Task GenerarHTMLDesdeDatos(string nombreArchivoHTML, string fechaVenta, int idVenta, int idPedido, int idMetodoPago, string numRUC, string nombreCliente, string apellidoCliente, string dniCliente, string nombreCatVen, string nombreMetodoPago, List<EntPedidoComponente> componentesPedido, decimal importeTotal)
        {
            try
            {
                nombreMetodoPago= datMetodoPago.Instancia.ObtenerNombreMetodoPago(idMetodoPago);
                // Ruta completa del archivo HTML
                string rutaCompletaHTML = Path.Combine(Directory.GetCurrentDirectory(), nombreArchivoHTML);
                DateTime fv = DateTime.Parse(fechaVenta);
                string fechaCompleta = fv.ToString("yyyy-MM-dd"); // Formato personalizado para la fecha
                string horaCompleta = fv.ToString("HH:mm:ss"); // Formato personalizado para la hora

                // Dentro del método GenerarDocumentosVenta
                string n = $"{DateTime.Now:yyyyMMddHHmmssfff}";
                string nombreArchivoPDF = $"{n}";
                
                // Crea un archivo HTML
                using (StreamWriter sw = new StreamWriter(rutaCompletaHTML))
                {
                    // Escribe el contenido HTML en el archivo
                    sw.WriteLine("<!DOCTYPE html>");
                    sw.WriteLine("<html lang=\"es\">");
                    sw.WriteLine("<head>");
                    sw.WriteLine("    <meta charset=\"UTF-8\">");
                    sw.WriteLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                    sw.WriteLine("    <title>Nota de Venta</title>");
                    sw.WriteLine("    <style>");
                    sw.WriteLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
                    sw.WriteLine("        table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }");
                    sw.WriteLine("        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
                    sw.WriteLine("        th { background-color: #f2f2f2; }");
                    sw.WriteLine("        h2 { color: #333; position: relative; }");
                    sw.WriteLine("        p { margin: 0; }");
                    sw.WriteLine("        .logo { position: absolute; top: 0; right: 0; width: 100px; height: auto; }");
                    sw.WriteLine("    </style>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("");
                    sw.WriteLine($"    <h2>{nombreCatVen}<img src=\"data:image/png;base64,{ObtenerBase64Imagen("Logo_venta.png")}\" alt=\"Logo\" class=\"logo\"></h2>");

                    sw.WriteLine("");
                    sw.WriteLine("    <table>");
                    sw.WriteLine($"        <tr><th>Fecha</th><td>{fechaCompleta}</td></tr>");
                    sw.WriteLine($"        <tr><th>Hora</th><td>{horaCompleta}</td></tr>");
                    sw.WriteLine($"        <tr><th>N° Pedido</th><td>{idPedido}</td></tr>");
                    sw.WriteLine($"        <tr><th>Método Pago</th><td>{nombreMetodoPago}</td></tr>");
                    sw.WriteLine($"        <tr><th>Número RUC</th><td>{numRUC}</td></tr>");

                    if(nombreCatVen!= "Nota de Venta")
                    {
                        sw.WriteLine($"        <tr><th>Nombre Cliente</th><td>{nombreCliente}</td></tr>");
                        sw.WriteLine($"        <tr><th>Apellido Cliente</th><td>{apellidoCliente}</td></tr>");
                        sw.WriteLine($"        <tr><th>DNI Cliente</th><td>{dniCliente}</td></tr>");
                        sw.WriteLine("    </table>");
                    }
                    else
                    {
                        sw.WriteLine("    </table>");
                    }
                    
                    sw.WriteLine("    </table>");

                    // Agrega la tabla de detalles de componentes
                    sw.WriteLine("    <h2>Detalle del Pedido</h2>");
                    sw.WriteLine("    <table>");
                    sw.WriteLine("        <tr><th>ID</th><th>Descripción</th><th>Cantidad</th><th>Importe</th></tr>");

                    foreach (var componente in componentesPedido)
                    {
                        sw.WriteLine($"        <tr><td>{componente.IdComponente}</td><td>{componente.NombreComponente}</td><td>{componente.Cantidad}</td><td>{componente.ImporteComponente}</td></tr>");
                    }
                    // Agrega la fila con el importe total proporcionado como parámetro
                    sw.WriteLine($"        <tr><th colspan=\"3\">Importe Total</th><td>{importeTotal}</td></tr>");

                    sw.WriteLine("    </table");

                    // Fin del archivo HTML
                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                }

                // Llama a GenerarPDFDesdeHTML para convertir HTML a PDF
                await GenerarPDFDesdeHTMLAsync(nombreArchivoPDF, nombreArchivoHTML);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el archivo HTML: {ex.Message}");
            }
        }

        public static async Task GenerarHTMLCajaDesdeDatos(string nombreArchivoHTML, DateTime fechaApertura, DateTime? fechaCierre, bool estadoCaja, decimal montoInicial, decimal? montoFinal,
    decimal? totalVentasBoleta, decimal? totalVentasFactura, decimal? totalVentasNotaVenta, decimal? totalVentasYape, decimal? totalVentasPlin, decimal? totalVentasEfectivo, decimal? totalVentasTarjeta, decimal? gastos)
        {
            try
            {
                // Ruta completa del archivo HTML
                string rutaCompletaHTML = Path.Combine(Directory.GetCurrentDirectory(), nombreArchivoHTML);
                string estadoCajaTexto = estadoCaja ? "Abierta" : "Cerrada";

                // Calcular el TOTAL DE VENTAS (Monto Final - Gastos)
                decimal totalVentas = montoFinal.GetValueOrDefault() - (gastos ?? 0);

                // Dentro del método GenerarDocumentosVenta
                string n = $"{DateTime.Now:yyyyMMddHHmmssfff}";
                string nombreArchivoPDF = $"{n}";

                using (StreamWriter sw = new StreamWriter(rutaCompletaHTML))
                {
                    sw.WriteLine("<!DOCTYPE html>");
                    sw.WriteLine("<html lang=\"es\">");
                    sw.WriteLine("<head>");
                    sw.WriteLine("    <meta charset=\"UTF-8\">");
                    sw.WriteLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                    sw.WriteLine("    <title>Detalle de Caja</title>");
                    sw.WriteLine("    <style>");
                    sw.WriteLine("        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 20px; background-color: #f5f5f5; }");
                    sw.WriteLine("        table { width: 100%; margin: 20px auto; border-collapse: collapse; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); background-color: white; }");
                    sw.WriteLine("        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; font-size: 12px; }");
                    sw.WriteLine("        th { background-color: #4CAF50; color: white; }");
                    sw.WriteLine("        h2 { color: #333; text-align: center; padding: 10px 0; position: relative; font-size: 18px; }");
                    sw.WriteLine("        .logo { position: absolute; top: 0; right: 20px; width: 80px; height: auto; }");
                    sw.WriteLine("        p { margin: 0; font-size: 12px; }");
                    sw.WriteLine("        .footer { margin-top: 20px; text-align: center; font-size: 12px; }");
                    sw.WriteLine("        .currency { font-weight: bold; color: #4CAF50; }");
                    sw.WriteLine("    </style>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");

                    sw.WriteLine($"<h2>{"REPORTE"}<img src=\"data:image/png;base64,{ObtenerBase64Imagen("Logo_venta.png")}\" alt=\"Logo\" class=\"logo\"></h2>");

                    sw.WriteLine("    <table>");
                    sw.WriteLine($"        <tr><th>FECHA APERTURA</th><td>{fechaApertura:dd/MM/yyyy}</td></tr>");
                    sw.WriteLine($"        <tr><th>FECHA CIERRE</th><td>{fechaCierre?.ToString("dd/MM/yyyy")}</td></tr>");
                    sw.WriteLine($"        <tr><th>HORA APERTURA</th><td>{fechaApertura:hh:mm:ss tt}</td></tr>");
                    sw.WriteLine($"        <tr><th>HORA CIERRE</th><td>{fechaCierre?.ToString("hh:mm:ss tt")}</td></tr>");
                    sw.WriteLine($"        <tr><th>ESTADO CAJA</th><td>{estadoCajaTexto.ToUpper()}</td></tr>");
                    sw.WriteLine($"        <tr><th>MONTO INICIAL</th><td><span class=\"currency\">S/. </span>{montoInicial}</td></tr>");
                    sw.WriteLine($"        <tr><th>MONTO FINAL</th><td><span class=\"currency\">S/. </span>{montoFinal}</td></tr>");
                    sw.WriteLine($"        <tr><th>GASTOS</th><td><span class=\"currency\">S/. </span>{gastos ?? 0}</td></tr>");
                    sw.WriteLine($"        <tr><th>TOTAL VENTAS</th><td><span class=\"currency\">S/. </span>{totalVentas}</td></tr>");
                    sw.WriteLine("    </table>");

                    sw.WriteLine("<h2>POR CATEGORÍA DE VENTA</h2>");
                    sw.WriteLine("    <table>");
                    sw.WriteLine($"        <tr><th>BOLETAS</th><td><span class=\"currency\">S/. </span>{totalVentasBoleta}</td></tr>");
                    sw.WriteLine($"        <tr><th>FACTURAS</th><td><span class=\"currency\">S/. </span>{totalVentasFactura}</td></tr>");
                    sw.WriteLine($"        <tr><th>NOTAS DE VENTA</th><td><span class=\"currency\">S/. </span>{totalVentasNotaVenta}</td></tr>");
                    sw.WriteLine("    </table>");

                    sw.WriteLine("<h2>POR MÉTODO DE PAGO</h2>");
                    sw.WriteLine("    <table>");
                    sw.WriteLine($"        <tr><th>YAPE</th><td><span class=\"currency\">S/. </span>{totalVentasYape}</td></tr>");
                    sw.WriteLine($"        <tr><th>PLIN</th><td><span class=\"currency\">S/. </span>{totalVentasPlin}</td></tr>");
                    sw.WriteLine($"        <tr><th>TARJETA</th><td><span class=\"currency\">S/. </span>{totalVentasTarjeta}</td></tr>");
                    sw.WriteLine($"        <tr><th>EFECTIVO</th><td><span class=\"currency\">S/. </span>{totalVentasEfectivo}</td></tr>");
                    sw.WriteLine("    </table>");

                    sw.WriteLine("    <div class=\"footer\">");
                    sw.WriteLine("        <p>~\r\nREPORTE GENERADO POR SISTEMA POS~\r\n</p>");
                    sw.WriteLine("    </div>");

                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                }



                // Llama a GenerarPDFDesdeHTML para convertir HTML a PDF
                await GenerarPDFDesdeHTMLAsync($"{nombreArchivoPDF}.pdf", nombreArchivoHTML);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el archivo HTML de la caja: {ex.Message}");
            }
        }

        // Función para obtener la representación en base64 de una imagen
        private static string ObtenerBase64Imagen(string nombreArchivo)
        {
            string rutaImagen = Path.Combine(Directory.GetCurrentDirectory(), nombreArchivo);
            byte[] bytesImagen = File.ReadAllBytes(rutaImagen);
            return Convert.ToBase64String(bytesImagen);
        }
        public static async Task GenerarPDFDesdeHTMLAsync(string nombreArchivoPDF, string nombreArchivoHTML)
        {
            try
            {
                // Ruta completa del archivo HTML
                string rutaCompletaHTML = Path.Combine(Directory.GetCurrentDirectory(), nombreArchivoHTML);

                // Configurar el cuadro de diálogo para guardar el PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = nombreArchivoPDF;

                // Mostrar el cuadro de diálogo y verificar si el usuario hizo clic en "Guardar"
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Ruta completa del archivo PDF
                    string rutaCompletaPDF = saveFileDialog.FileName;
                    
                    //Nuevo método
                    try
                    {
                        await GeneratePdfFromHtml(rutaCompletaHTML, rutaCompletaPDF);

                        Console.WriteLine("Conversión exitosa. PDF guardado en resultado.pdf");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error en la conversión: {ex.Message}");
                    }

                    Console.WriteLine($"Archivo PDF creado exitosamente en: {rutaCompletaPDF}");

                    // Elimina el archivo HTML después de generar el PDF
                    File.Delete(rutaCompletaHTML);
                    //Console.WriteLine($"Archivo HTML eliminado exitosamente: {rutaCompletaHTML}");

                    // Imprimir el PDF generado
                    ImprimirPDFConSeleccionDeImpresora(rutaCompletaPDF);
                }
                else
                {
                    Console.WriteLine("Operación de generación de PDF cancelada por el usuario.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el archivo PDF: {ex.Message}");
            }
        }

        static async Task GeneratePdfFromHtml(string htmlFilePath, string pdfFilePath)
        {
            try
            {
                object value = await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

                using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    Args = new[] { "--disable-web-security" } // Deshabilitar la seguridad web para evitar problemas con la caché
                }))
                using (var page = await browser.NewPageAsync())
                {
                    // Cargar HTML desde el archivo
                    var htmlContent = File.ReadAllText(htmlFilePath);

                    // Establecer el contenido HTML de la página
                    await page.SetContentAsync(htmlContent);

                    // Configuración de PdfOptions
                    var pdfOptions = new PuppeteerSharp.PdfOptions
                    {
                        PrintBackground = true,

                        DisplayHeaderFooter = true,
                        Scale = 1,
                    };

                    // Generar PDF
                    await page.PdfAsync(pdfFilePath, pdfOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el archivo PDF: {ex.Message}");
            }
        }

        public static void ImprimirPDFConSeleccionDeImpresora(string rutaPDF)
        {
            try
            {
                // Abre el archivo PDF con el visor predeterminado del sistema
                Process.Start(rutaPDF);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir el documento PDF: {ex.Message}");
            }
        }



    }
}
