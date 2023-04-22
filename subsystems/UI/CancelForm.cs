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
    public partial class CancelForm : Form
    {
        public CancelForm()
        {
            InitializeComponent();
        }

        private void cancel(object sender, EventArgs e)
        {
            //Gotta somehow get the resID from the reservation in the display event
            CancelController.cancel(reservation.resID);
        }

        public void display(Reservation reservation)
        {
            //This needs to transform the reservation into the 
        }
    }
}
