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
            //this.Controls.Add(room);
            Console.WriteLine(room);
        }

        private void submit(object sender, EventArgs e)
        {
            //Have to again map the Form data back to a reservation
            Reservation room = new Reservation(new Account("jawilt", "admin", "test123", "James"), new Room(1, "Mcknight")); //Fake Data
            ReserveController.submit(room);
            this.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void From_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void To_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
