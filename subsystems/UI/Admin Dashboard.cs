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
    public partial class AdminDashboard : Form
    {
        private Account userAccount;
        Entity.List resourceList;
       
        public AdminDashboard(Account userAccount, Entity.List resourceList)
        {
            this.userAccount = userAccount;
            this.resourceList = resourceList;
            InitializeComponent();

            //
            // All the reservations
            //
            foreach (Reservation reserve in resourceList.reservations)
            {
                //this.Controls.Add(reserve); //Not quite sure how we want to do this. But a light wrapper code that accepts a Reservation/Room object and creates the card would be nice.
                Console.WriteLine(reserve.ToString());
            }
            
        }

        private void logoutClick(object sender, EventArgs e)
        {

        }

        private void submitClick(object sender, EventArgs e)
        { 
            //We need to know Which component we've referenced for this. for now i'm using "this.card"
            CancelController controller = new CancelController(this);
            //controller.submit(resid);
            controller.submit(1);
            this.Close(); //Close immediately after sending deets to the Controller
        }

        public static void Launch(Account userAccount, Entity.List list)
        {
            new AdminDashboard(userAccount, list).Show();
        }
    }
}
