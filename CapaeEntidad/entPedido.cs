using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaeEntidad
{
    public class EntPedido
    {
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public int IdMesa { get; set; }
        public bool Pagado { get; set; }
    }

}
