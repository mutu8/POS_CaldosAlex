using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datVenta
    {
        // Actualiza los detalles de conexión según tu servidor de Azure SQL
        private string connectionString = Conexion.Instancia.obtenerConexion();

        #region Singleton
        // Patrón de Diseño Singleton
        private static readonly datVenta _instancia = new datVenta();
        public static datVenta Instancia
        {
            get { return datVenta._instancia; }
        }
        #endregion

        // Nuevo método para obtener todas las categorías de venta
        public DataTable ObtenerCategoriasVenta()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Definir la consulta SQL para obtener los datos de la tabla CategoriaVenta
                    string query = "SELECT idCategoriaVenta, nombreCategoria FROM CategoriaVenta";

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

        // Nuevo método para obtener el nombre de una categoría de venta por su id
        public string ObtenerNombreCategoriaVenta(int idCategoriaVenta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nombreCategoria FROM CategoriaVenta WHERE idCategoriaVenta = @IdCategoriaVenta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategoriaVenta", idCategoriaVenta);

                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }
    }

}
