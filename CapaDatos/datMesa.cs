using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datMesa
    {
        // Actualiza los detalles de conexión según tu servidor
        private string connectionString = Conexion.Instancia.obtenerConexion();
        #region Singleton
        // Patrón de Diseño Singleton
        private static readonly datMesa _instancia = new datMesa();
        public static datMesa Instancia
        {
            get { return datMesa._instancia; }
        }
        #endregion

        public bool VerificarMesaOcupada(int numeroMesa)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verificar si la mesa está ocupada
                string verificarOcupacionQuery = $"SELECT estadoOcupado FROM Mesa WHERE idMesa = {numeroMesa}";

                using (SqlCommand verificarOcupacionCommand = new SqlCommand(verificarOcupacionQuery, connection))
                {
                    bool estadoOcupado = Convert.ToBoolean(verificarOcupacionCommand.ExecuteScalar());

                    return estadoOcupado;
                }
            }
        }
    }
}
