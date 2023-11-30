using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logMesa
    {
        private static readonly logMesa _instancia = new logMesa();
        public static logMesa Instancia
        {
            get { return logMesa._instancia; }
        }

        public bool VerificarMesaOcupada(int numeroMesa)
        {
            // Accede al método en la capa lógica
            return datMesa.Instancia.VerificarMesaOcupada(numeroMesa);
        }
    }
}
