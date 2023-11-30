using System;
using CapaDatos;

namespace CapaLogica
{
    public class logApi
    {
        public static readonly DatApi _datApi = new DatApi();

        public static dynamic Get(string url)
        {
            try
            {
                return _datApi.Get(url);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error en logApi.Get(): {ex.Message}");
                return null;
            }
        }
    }
}
