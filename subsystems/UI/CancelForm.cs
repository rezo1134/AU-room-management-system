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
        internal Reservation reservation;
        public CancelForm(Reservation reserve):base()
        {
            InitializeComponent();
            this.reservation = reserve;
            Panel backgroundPanel = new Panel();
            Panel cPanel = new Panel();
            Label RoomNumber = new Label();
            Label Building = new Label();
            Label Employee = new Label();
            Label Date = new Label();
            Label Time = new Label();
            Label RoomNumber2 = new Label();
            Label Building2 = new Label();
            Label Employee2 = new Label();
            Label Date2 = new Label();
            Label Time2 = new Label();
            backgroundPanel.SuspendLayout();
            cPanel.SuspendLayout();
            this.SuspendLayout();

            // Background Panel
            backgroundPanel.BackColor = Color.Black;
            backgroundPanel.Controls.Add(cPanel);
            backgroundPanel.Location = new Point(42, 88);
            backgroundPanel.Name = "cpanel";
            backgroundPanel.Size = new Size(361, 298);
            backgroundPanel.TabIndex = 11;

            // Main Card
            cPanel.BackColor = Color.White;
            cPanel.Controls.Add(Time);
            cPanel.Controls.Add(Date);
            cPanel.Controls.Add(Employee);
            cPanel.Controls.Add(Building);
            cPanel.Controls.Add(RoomNumber);
            cPanel.Controls.Add(Time2);
            cPanel.Controls.Add(Date2);
            cPanel.Controls.Add(Employee2);
            cPanel.Controls.Add(Building2);
            cPanel.Controls.Add(RoomNumber2);
            cPanel.Location = new Point(1, 1);
            cPanel.Name = "Card";
            cPanel.Size = new Size(359, 296);
            cPanel.TabIndex = 0;

            // RoomNumber
            RoomNumber.AutoSize = true;
            RoomNumber.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
            RoomNumber.Location = new Point(37, 33);
            RoomNumber.Name = "Room Number";
            RoomNumber.Size = new Size(127, 21);
            RoomNumber.TabIndex = 0;
            RoomNumber.Text = $"Room Number:";

            RoomNumber2.AutoSize = true;
            RoomNumber2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
            RoomNumber2.Location = new Point(160, 33);
            RoomNumber2.Name = "Room Number2";
            RoomNumber2.Size = new Size(127, 21);
            RoomNumber2.TabIndex = 1;
            RoomNumber2.Text = $"{reserve.room.roomID}";


            // Building
            Building.AutoSize = true;
            Building.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
            Building.Location = new Point(37, 82);
            Building.Name = "Building";
            Building.Size = new Size(79, 21);
            Building.TabIndex = 2;
            Building.Text = $"Building:";

            Building2.AutoSize = true;
            Building2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
            Building2.Location = new Point(120, 82);
            Building2.Name = "Building2";
            Building2.Size = new Size(79, 21);
            Building2.TabIndex = 3;
            Building2.Text = $"{reserve.user.username}";

            // Employee
            Employee.AutoSize = true;
            Employee.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
            Employee.Location = new Point(37, 133);
            Employee.Name = "Employee Name";
            Employee.Size = new Size(140, 21);
            Employee.TabIndex = 4;
            Employee.Text = $"Employee Name:";

            Employee2.AutoSize = true;
            Employee2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
            Employee2.Location = new Point(175, 133);
            Employee2.Name = "Employee Name2";
            Employee2.Size = new Size(140, 21);
            Employee2.TabIndex = 5;
            Employee2.Text = $"{reserve.user.name}";

            // Date
            Date.AutoSize = true;
            Date.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
            Date.Location = new Point(37, 183);
            Date.Name = "Date";
            Date.Size = new Size(50, 21);
            Date.TabIndex = 3;
            Date.Text = "Date:";

            Date2.AutoSize = true;
            Date2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
            Date2.Location = new Point(85, 183);
            Date2.Name = "Date2";
            Date2.Size = new Size(50, 21);
            Date2.TabIndex = 3;
            Date2.Text = $"{reserve.dtg.Month}-{reserve.dtg.Day}-{reserve.dtg.Year}";

            // Time
            Time.AutoSize = true;
            Time.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
            Time.Location = new Point(37, 238);
            Time.Name = "Time";
            Time.Size = new Size(52, 21);
            Time.TabIndex = 4;
            Time.Text = $"Time:";

            Time2.AutoSize = true;
            Time2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
            Time2.Location = new Point(85, 238);
            Time2.Name = "Time2";
            Time2.Size = new Size(52, 21);
            Time2.TabIndex = 4;
            Time2.Text = $"{reserve.dtg.Hour}:{reserve.dtg.Minute}:{reserve.dtg.Second}";

            this.Controls.Add(backgroundPanel);
            backgroundPanel.ResumeLayout(false);
            cPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();


        }

        private void cancel(object sender, EventArgs e)
        {
            //Gotta somehow get the resID from the reservation in the display event
            CancelController.cancel(this.reservation);
            this.Close();
        }

        public void display(Reservation reservation)
        {
            //This needs to transform the reservation into the 
        }

    }
}
