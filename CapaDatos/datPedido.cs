using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datPedido
    {
        // Actualiza los detalles de conexión según tu servidor de Azure SQL
        private string connectionString = Conexion.Instancia.obtenerConexion();

        #region Singleton
        // Patrón de Diseño Singleton
        private static readonly datPedido _instancia = new datPedido();
        public static datPedido Instancia
        {
            get { return datPedido._instancia; }
        }
        #endregion
    }
}
