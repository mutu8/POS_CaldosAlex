using CapaeEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaDatos
{
    public class datPedidoComponente
    {
        // Actualiza los detalles de conexión según tu servidor de Azure SQL
        //private string connectionString = "Server=34.176.49.57;Database=bd_caldosAlex;User Id=sqlserver;Password=@hV\"1%`(o63_/7V:;";
        private string connectionString = Conexion.Instancia.obtenerConexion();

        #region Singleton
        // Patrón de Diseño Singleton
        private static readonly datPedidoComponente _instancia = new datPedidoComponente();
        public static datPedidoComponente Instancia
        {
            get { return datPedidoComponente._instancia; }
        }
        #endregion
        public decimal MostrarImporteTotal(int numeroMesa)
        {
            decimal importeTotalPedido = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT c.precioComponente, pc.Cantidad FROM PedidoComponente pc " +
                                $"JOIN Componentes c ON pc.idComponente = c.idComponente " +
                                $"JOIN Pedido p ON pc.idPedido = p.idPedido " +
                                $"WHERE p.idMesa = {numeroMesa}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // Verificar si hay filas en el resultado
                        {
                            while (reader.Read())
                            {
                                decimal precioComponente = reader.GetDecimal(0);
                                int cantidadComponente = reader.GetInt32(1);

                                // Crear una instancia de EntPedidoComponente y asignar valores
                                EntPedidoComponente pedidoComponente = new EntPedidoComponente
                                {
                                    ImporteComponente = precioComponente * cantidadComponente
                                    // Puedes asignar otros valores según tu modelo de datos
                                };

                                importeTotalPedido += pedidoComponente.ImporteComponente ?? 0;
                            }
                        }
                    }
                }

                // Devolver el importe total
                return importeTotalPedido;
            }
        }

        public int ObtenerCantidadComponenteDesdeBD(int idPedido, int idTipoComponente, int idComponente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string obtenerCantidadQuery = $"SELECT Cantidad FROM PedidoComponente " +
                                              $"WHERE idPedido = {idPedido} " +
                                              $"AND idTipoComponente = {idTipoComponente} " +
                                              $"AND idComponente = {idComponente}";

                using (SqlCommand obtenerCantidadCommand = new SqlCommand(obtenerCantidadQuery, connection))
                {
                    object result = obtenerCantidadCommand.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
       
            public List<EntPedidoComponente> ObtenerComponentesPedido(int idPedido)
            {
                List<EntPedidoComponente> componentesPedido = new List<EntPedidoComponente>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT
                        pc.idPedidoComponentes,
                        pc.idPedido,
                        pc.idTipoComponente,
                        pc.idComponente,
                        c.descripcionComponente as NombreComponente,
                        pc.Cantidad,
                        pc.importeComponente
                    FROM
                        PedidoComponente pc
                    JOIN
                        Componentes c ON pc.idComponente = c.idComponente
                    WHERE
                        pc.idPedido = @IdPedido";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdPedido", idPedido);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EntPedidoComponente componenteInfo = new EntPedidoComponente
                                {
                                    IdPedidoComponentes = Convert.ToInt32(reader["idPedidoComponentes"]),
                                    IdPedido = Convert.ToInt32(reader["idPedido"]),
                                    IdTipoComponente = Convert.ToInt32(reader["idTipoComponente"]),
                                    IdComponente = Convert.ToInt32(reader["idComponente"]),
                                    NombreComponente = reader["NombreComponente"].ToString(),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    ImporteComponente = reader["importeComponente"] != DBNull.Value ? Convert.ToDecimal(reader["importeComponente"]) : (decimal?)null
                                };

                                componentesPedido.Add(componenteInfo);
                            }
                        }
                    }
                }

                return componentesPedido;
            }
    }

}


