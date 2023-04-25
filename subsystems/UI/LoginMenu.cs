using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Submit(object sender, EventArgs e)
        {
            string username = this.username.Text;
            string password = this.password.Text;
            LoginController controller = new LoginController(this);
            controller.UserLogin(username, password);
        }

        public static void Display(string ErrorMessage)
        {
            MessageBox.Show(ErrorMessage);
        }
        
    }
}
