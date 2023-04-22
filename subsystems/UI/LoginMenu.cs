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
namespace Boundary
{
    public partial class LoginMenu : Form
    {
        LoginController controller = new LoginController();
        public LoginMenu()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enter_Click(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string password = this.password.Text;
            controller.userLogin(username, password);
        }

        private void usernameTextChanged(object sender, EventArgs e)
        {

        }
        private void passwordTextChanged(object sender, EventArgs e)
        {

        }
        public void display(string message)
        {
            this.errorLabel.Text = message;
            this.errorLabel.Visible = true;
        }
    }
}
