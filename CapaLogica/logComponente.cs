using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logComponente
    {
        private static readonly logComponente _instancia = new logComponente();
        public static logComponente Instancia
        {
            get { return logComponente._instancia; }
        }

        public decimal ObtenerPrecioComponenteDesdeBD(int idComponente)
        {
            // Accede al método en la capa lógica
            return datComponente.Instancia.ObtenerPrecioComponenteDesdeBD(idComponente);
        }
        public bool ActualizarComponenteEnBD(int idComponente, string nuevaDescripcion, decimal nuevoPrecio)
        {
            // Accede al método en la capa de datos
            return datComponente.Instancia.ActualizarComponente(idComponente, nuevaDescripcion, nuevoPrecio);
        }
        public int ObtenerIdDelComponente(string descripcionComponente)
        {
            return datComponente.Instancia.ObtenerIdDesdeBaseDeDatos(descripcionComponente);
        }

    }
}
