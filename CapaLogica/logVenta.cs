using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logVenta
    {
        private static readonly logVenta _instancia = new logVenta();
        public static logVenta Instancia
        {
            get { return logVenta._instancia; }
        }

        // Nuevo método para obtener todas las categorías de venta
        public DataTable ObtenerCategoriasVenta()
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datVenta.Instancia.ObtenerCategoriasVenta();
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logVenta.ObtenerCategoriasVenta(): " + ex.Message);
                return null;
            }
        }

        // Nuevo método para obtener el nombre de una categoría de venta por su id
        public string ObtenerNombreCategoriaVenta(int idCategoriaVenta)
        {
            try
            {
                // Llamamos al método correspondiente en la capa de datos
                return datVenta.Instancia.ObtenerNombreCategoriaVenta(idCategoriaVenta);
            }
            catch (Exception ex)
            {
                // Puedes manejar la excepción según tus necesidades
                Console.WriteLine("Error en logVenta.ObtenerNombreCategoriaVenta(): " + ex.Message);
                return string.Empty;
            }
        }
    }

}
