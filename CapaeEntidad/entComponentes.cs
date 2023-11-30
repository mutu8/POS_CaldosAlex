using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaeEntidad
{
    public class entComponentes
    {
        public int idComponente { get; set; }
        public string descripcionComponente { get; set; }
        public decimal precioComponente { get; set; }
        public int idTipoComponente { get; set; }
        public int? idCategoriaPlato { get; set; }
    }

}
