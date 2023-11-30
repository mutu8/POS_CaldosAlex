using CapaDatos;
using CapaeEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logPedidoComponente
    {
        private static readonly logPedidoComponente _instancia = new logPedidoComponente();
        public static logPedidoComponente Instancia
        {
            get { return logPedidoComponente._instancia; }
        }
        public decimal ObtenerImporteTotal(int numeroMesa)
        {
            // Llama al método en la capa de datos
            return datPedidoComponente.Instancia.MostrarImporteTotal(numeroMesa);
        }
        public int ObtenerCantidadComponente(int idPedido, int idTipoComponente, int idComponente)
        {
            // Llama al nuevo método en la capa de datos
            return datPedidoComponente.Instancia.ObtenerCantidadComponenteDesdeBD(idPedido, idTipoComponente, idComponente);
        }
        public List<EntPedidoComponente> ObtenerComponentesPedido(int idPedido)
        {
            // Llama al método en la capa de datos
            return datPedidoComponente.Instancia.ObtenerComponentesPedido(idPedido);
        }
    }
}