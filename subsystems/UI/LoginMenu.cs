using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using Entity;
namespace Boundary
{
    public partial class LoginMenu : Form
    {
        
        public LoginMenu()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submit(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string password = this.password.Text;
            LoginController controller = new LoginController(this);
            controller.userLogin(username, password);
        }

        private void passwordTextChanged(object sender, EventArgs e)
        {
            string maskedPass = "";
            foreach(char c in this.password.Text)
            {
                maskedPass = maskedPass + "*";
            }
            this.password.Text = maskedPass;
        }
        public static void display(string message)
        {
            MessageBox.Show(message);
        }
        
    }
}
