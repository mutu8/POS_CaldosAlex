using CapaDatos;
using CapaeEntidad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CapaLogica
{
    public class logImpresion
    {
        public void GenerarDocumentosImpresion(int idVenta, string nombreArchivoHTML, string fechaVenta, int idPedido, int idMetodoPago, string RUC, string nombreCliente, string apellidoCliente, string dniCliente, string nombreCatVen, string nombreMetodoPago, List<EntPedidoComponente> componentesPedido, decimal importeVenta)
        {
            try
            {
                // Llama al método de la capa de datos para generar documentos
                string nombreArchivoPDF = $"{DateTime.Now:yyyyMMddHHmmssfff}.pdf";
                datImpresion.GenerarDocumentosVenta(nombreArchivoPDF, nombreArchivoHTML, fechaVenta, idVenta, idPedido, idMetodoPago, RUC, nombreCliente, apellidoCliente, dniCliente, nombreCatVen, nombreMetodoPago, componentesPedido, importeVenta);

                //datImpresion.ImprimirDocumentoConSeleccionDeImpresora(rutaPDF);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la generación de documentos: {ex.Message}");
            }
        }

        public void GenerarDocumentosImpresionCaja(int idCaja, string nombreArchivoHTML, DateTime fechaApertura, DateTime? fechaCierre, bool estadoCaja, decimal montoInicial, decimal? montoFinal,
         decimal? totalVentasBoleta, decimal? totalVentasFactura, decimal? totalVentasNotaVenta, decimal? totalVentasYape, decimal? totalVentasPlin, decimal? totalVentasEfectivo, decimal? totalVentasTarjeta, decimal? gastos)
            {
                try
                {
                    // Llama al método de la capa de datos para generar documentos
                    string nombreArchivoPDF = $"{DateTime.Now:yyyyMMddHHmmssfff}.pdf";
                    datImpresion.GenerarDocumentosCaja(nombreArchivoPDF, nombreArchivoHTML, fechaApertura, fechaCierre, estadoCaja, montoInicial, montoFinal,
                        totalVentasBoleta, totalVentasFactura, totalVentasNotaVenta, totalVentasYape, totalVentasPlin, totalVentasEfectivo, totalVentasTarjeta, gastos);

                    //datImpresion.ImprimirDocumentoConSeleccionDeImpresora(rutaPDF);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la generación de documentos de la caja: {ex.Message}");
                }
            }


    }
}
