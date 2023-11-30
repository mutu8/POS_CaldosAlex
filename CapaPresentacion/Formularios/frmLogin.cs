
using CapaLogica;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '*';
 
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            // Llamar a la función de autenticación
            logUsuarios logicaUsuarios = new logUsuarios();
            bool autenticado = logicaUsuarios.AutenticarUsuario(nombreUsuario, contraseña);

            // Verificar si la autenticación fue exitosa
            if (autenticado)
            {

                // Aquí puedes abrir la ventana principal o realizar cualquier otra acción requerida.
                frmPrincipal dashboard = new frmPrincipal();
                this.Hide();
                dashboard.Show();

                // Llama al método para mostrar el usuario
                dashboard.MostrarUsuarioLogeado(nombreUsuario.ToUpper());
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos. Por favor, inténtelo de nuevo.");
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra la aplicación completa
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            btnIngresar.BackColor = Color.FromArgb(33, 67, 104);

        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            btnIngresar.BackColor = Color.Transparent;

        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Evita que se procese la tecla "Enter" en el TextBox actual

                // Cambia el foco al siguiente TextBox (txtContraseña) si txtUsuario no está vacío
                if (!string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    txtContraseña.Focus();
                }
                // Si txtUsuario está vacío, realiza alguna acción o simplemente espera que se complete
                // Puedes agregar aquí la lógica necesaria según tus requerimientos.
            }
            else if (e.KeyCode == Keys.Tab && string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                e.Handled = true; // Evita que se procese la tecla "Tab" en el TextBox actual

                // Cambia el foco al siguiente TextBox (txtContraseña) si txtUsuario está vacío
                txtContraseña.Focus();
            }
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Evita que se procese la tecla "Enter" en el TextBox actual

                // Realiza la acción de ingreso si ambos TextBox tienen valores
                if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    btnIngresar.PerformClick();
                }
                // Si txtContraseña está vacío, realiza alguna acción o simplemente espera que se complete
                // Puedes agregar aquí la lógica necesaria según tus requerimientos.
            }
            else if (e.KeyCode == Keys.Tab && string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                e.Handled = true; // Evita que se procese la tecla "Tab" en el TextBox actual

                // Cambia el foco al siguiente TextBox (btnIngresar) si txtContraseña está vacío
                btnIngresar.Focus();
            }
        }

        private void btnIngresar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Evita que se procese la tecla "Enter" en el TextBox actual

                // Realiza la acción de ingreso si ambos TextBox tienen valores
                if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && !string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    btnIngresar.PerformClick();
                }
                // Si btnIngresar está vacío, cambia el foco al primer TextBox (txtUsuario)
                else if (string.IsNullOrWhiteSpace(btnIngresar.Text))
                {
                    txtUsuario.Focus();
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
    }
}
