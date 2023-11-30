using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaeEntidad
{
    public class EntPedidoComponente
    {
        public int IdPedidoComponentes { get; set; }
        public int IdPedido { get; set; }
        public int IdTipoComponente { get; set; }
        public int IdComponente { get; set; }
        public int Cantidad { get; set; }
        public decimal? ImporteComponente { get; set; }
        public string NombreComponente { get; set; }
    }

        
}
