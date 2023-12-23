    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datMetodoPago
    {
        // Actualiza los detalles de conexión según tu servidor de Azure SQL
        private string connectionString = Conexion.Instancia.obtenerConexion();
        #region Singleton
        private static readonly datMetodoPago _instancia = new datMetodoPago();
        public static datMetodoPago Instancia
        {
            get { return datMetodoPago._instancia; }
        }
        #endregion

        public DataTable ObtenerMetodosPago()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Definir la consulta SQL para obtener los datos de la tabla MetodoPago
                    string query = "SELECT idMetodoPago, nombreMetPag FROM MetodoPago";

                    // Crear un SqlDataAdapter y llenar el DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones según tus necesidades
                Console.WriteLine("Error: " + ex.Message);
            }

            return dataTable;
        }
        public string ObtenerNombreMetodoPago(int idMetodoPago)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nombreMetPag FROM MetodoPago WHERE idMetodoPago = @IdMetodoPago";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdMetodoPago", idMetodoPago);

                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }

    }
}
