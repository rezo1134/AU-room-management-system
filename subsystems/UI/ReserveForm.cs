using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        internal Account userAccount;
        internal Room room;
        internal DateTime startTime;
        internal DateTime stopTime;
        public ReserveForm(Account userAccount, Room room) : base()
        {
            this.userAccount = userAccount;
            this.room = room;
            InitializeComponent();

            //Modify the reservePanel
            Label roomlabel = new Label();
            Label buildinglabel= new Label();
            this.reservePanel.SuspendLayout();
            this.SuspendLayout();

            roomlabel.AutoSize = true;
            roomlabel.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
            roomlabel.Location = new Point(145, 44);
            roomlabel.Name = "Room Number";
            roomlabel.Size = new Size(127, 21);
            roomlabel.TabIndex = 0;
            roomlabel.Text = $"{this.room.roomID}";

            buildinglabel.AutoSize = true;
            buildinglabel.Font = new Font("Segoe UI", 12F,GraphicsUnit.Point);
            buildinglabel.Location = new Point(99, 85);
            buildinglabel.Name = "Room Number";
            buildinglabel.Size = new Size(127, 21);
            buildinglabel.TabIndex = 0;
            buildinglabel.Text = $"{this.room.building}";

            this.reservePanel.Controls.Add(roomlabel);
            this.reservePanel.Controls.Add(buildinglabel);
            this.reservePanel.ResumeLayout(false);
            this.reservePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private void submit(object sender, EventArgs e)
        {
            //Need to parse the DateRange picker module + TO FROM boxes
            startTime = DateTime.Parse($"{this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd")} {this.From.Text}");
            stopTime = DateTime.Parse($"{this.monthCalendar1.SelectionEnd.ToString("yyyy-MM-dd")} {this.To.Text}");
            Debug.WriteLine($"{startTime} - {stopTime}");
            Reservation res = new Reservation(1, new Account("jawilt", "admin", "test123", "James"), new Room(1, "Mcknight"), this.startTime, this.stopTime);
            ReserveController.submit(this.userAccount, res);
            this.Close();
        }
    }
}
