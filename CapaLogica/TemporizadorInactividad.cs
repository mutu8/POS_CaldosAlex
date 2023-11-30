using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    public class TemporizadorInactividad
    {
        private static readonly TemporizadorInactividad _instancia = new TemporizadorInactividad();

        private const int TiempoInactividadMaximo = 10000;
        private DateTime ultimoTiempoActividad;
        private Timer temporizadorInactividad;

        public static TemporizadorInactividad Instancia
        {
            get { return TemporizadorInactividad._instancia; }
        }

        public TemporizadorInactividad()
        {
            InicializarTemporizadorInactividad();
        }

        public void InicializarTemporizadorInactividad()
        {
            temporizadorInactividad = new Timer();
            temporizadorInactividad.Interval = 300000;
            temporizadorInactividad.Tick += VerificarInactividad;
            ultimoTiempoActividad = DateTime.Now;
        }

        public void ManejarEventosActividadUsuario(Form formulario)
        {
            formulario.MouseMove += ActividadUsuario;
            formulario.KeyDown += ActividadUsuario;
        }

        public void IniciarTemporizador()
        {
            temporizadorInactividad.Start();
        }

        public void ActividadUsuario(object sender, EventArgs e)
        {
            ultimoTiempoActividad = DateTime.Now;
        }

        public void VerificarInactividad(object sender, EventArgs e)
        {
            int tiempoTranscurrido = (int)(DateTime.Now - ultimoTiempoActividad).TotalMilliseconds;

            if (tiempoTranscurrido >= TiempoInactividadMaximo)
            {
                DialogResult result = MessageBox.Show("Se ha detectado inactividad, por lo que se cerrará sesión", "Inactividad", MessageBoxButtons.OK);
                temporizadorInactividad.Stop();
                // Verificar si el usuario hizo clic en OK
                if (result == DialogResult.OK)
                {
                    // Cerrar la aplicación
                    Application.Exit();
                }
            }
        }
    
        

    }
}
