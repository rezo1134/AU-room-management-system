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

namespace Boundary
{
    public partial class EmployeeDashboard : Form
    {
        public EmployeeDashboard(Account userAccount, Entity.List resourceList): base(userAccount, resourceList)
        {
            InitializeComponent();

            //
            // All the rooooooms
            //
            foreach (Room room in resourceList.rooms)
            {
                this.Controls.Add(room); //Not quite sure how we want to do this. But a light wrapper code that accepts a Reservation/Room object and creates the card would be nice.
            }

        }

        private void logoutClick(object sender, EventArgs e)
        {

        }

        private void reservationClick(object sender, EventArgs e)
        {
            //We need to know Which component we've referenced for this. for now i'm using "this.card"
            ReserveController controller = new ReserveController(this);
            controller.reserve(this.userAccount, this.card.roomid, this.card.time);
            this.Close(); //Close immediately after sending deets to the Controller
        }
        public static void Launch(Account userAccount, Entity.List list)
        {
            new EmployeeDashboard(userAccount, list);
        }

    }
}
