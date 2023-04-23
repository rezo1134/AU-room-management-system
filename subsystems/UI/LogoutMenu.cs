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
    public partial class LogoutMenu : Form
    {
        public LogoutMenu()
        {
            InitializeComponent();
        }

        private void resetClick(object sender, EventArgs e)
        {
            //We again need the userAccount object to 
            this.Close();
            new LoginMenu().Show();
            

        }
    }
}
