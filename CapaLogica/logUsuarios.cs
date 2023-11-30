using CapaeEntidad;
using CapaDatos; 
using System;
using System.Collections.Generic;

namespace CapaLogica
{
    public class logUsuarios
    {
        #region Singleton
        // Patrón de Diseño Singleton
        private static readonly logUsuarios _instancia = new logUsuarios();
        public static logUsuarios Instancia
        {
            get { return logUsuarios._instancia; }
        }
        #endregion
        public bool AutenticarUsuario(string nombreUsuario, string contraseña)
        {
            entUsuarios usuario = datUsuarios.Instancia.ObtenerUsuarioPorNombre(nombreUsuario);

            if (usuario != null && usuario.Contraseña == contraseña)
            {
                // Autenticación exitosa

                // Establecer el estado logeado
                datUsuarios.Instancia.EstablecerEstadoLogeado(usuario.ID, true);

                return true;
            }
            else
            {
                // Autenticación fallida
                return false;
            }
        }

        public int ObtenerIdUsuarioLogeado(string nombreUsuario)
        {
            return datUsuarios.Instancia.ObtenerIdUsuarioLogeado(nombreUsuario);
        }

        public void DesloguearUsuario(string nombreUsuario, bool Logeado)
        {
            // Establecer el estado logeado a 0 (falso)
            datUsuarios.Instancia.EstablecerEstadoDeslogeado(nombreUsuario, Logeado);
        }
        public void establecerEstado(int ID, bool Logeado)
        {
            // Establecer el estado logeado a 0 (falso)
            datUsuarios.Instancia.EstablecerEstadoLogeado(ID, Logeado);
        }

    }
}
