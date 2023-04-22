using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using Controller;
namespace Boundary
{
    public partial class ReserveForm : Form
    {
        public ReserveForm()
        {
            InitializeComponent();
        }

        public void display(Room room)
        {
            //This again assumes that we have a way to map a reservation object to a Form
            this.Controls.Add(room);
        }

        private void submit(object sender, EventArgs e)
        {
            //Have to again map the Form data back to a reservation
            ReserveController.submit(room);
            this.Close();
        }
    }
}
