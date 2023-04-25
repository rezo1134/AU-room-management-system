using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            //Set the starting locations of each of the card assets
            int panelPosX = 40;
            int panelPosY = 120;
            int counter = 0;
            foreach (Reservation reserve in resourceList.reservations)
            {


                Panel card = new Panel();
                Panel innercard = new Panel();
                Label rlabel = new Label();
                GroupBox rgroupBox = new GroupBox();
                Button rbutton = new Button();
                Label room = new Label();
                Label room2 = new Label();
                Label building = new Label();
                Label building2 = new Label();
                Label employee = new Label();
                Label employee2 = new Label();
                innercard.SuspendLayout();
                card.SuspendLayout();
                rgroupBox.SuspendLayout();
                this.SuspendLayout();

                //Configure the Card
                card.Controls.Add(innercard);
                card.Name = $"Card-{reserve.room.roomID}-Background";
                card.Size = new Size(210, 238);
                card.Location = new Point(panelPosX, panelPosY);
                card.BackColor = Color.Black;
                card.TabIndex = 0;
                if (panelPosX < 930)
                    panelPosX = panelPosX + 228; //Tile Card along the row
                else
                {
                    panelPosX = 40;//Reset to next row
                    panelPosY = panelPosY + 260;
                }

                //Configure innercard
                innercard.Controls.Add(rlabel);
                innercard.Controls.Add(rgroupBox);
                innercard.Controls.Add(rbutton);
                innercard.Size = new Size(208, 236);
                innercard.Location = new Point(1, 1);
                innercard.TabIndex = 0;
                innercard.BackColor = Color.White;

                //Configure the Label
                rlabel.AutoSize = true;
                rlabel.Size = new Size(78, 32);
                rlabel.Font = new Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                rlabel.Name = $"Label-{reserve.room.roomID}-{counter}";
                rlabel.Text = $"Room #{reserve.room.roomID}";
                rlabel.TabIndex = 0;
                rlabel.Location = new Point(56, 9); //Location is relative to the card

                //Configure the groupbox
                rgroupBox.Location = new System.Drawing.Point(17, 44);
                rgroupBox.Name = $"GroupBox-{reserve.room.roomID}-{counter}";
                rgroupBox.Size = new System.Drawing.Size(176, 126);
                rgroupBox.TabIndex = 1;
                rgroupBox.TabStop = false;
                rgroupBox.Controls.Add(room);
                rgroupBox.Controls.Add(room2);
                rgroupBox.Controls.Add(building);
                rgroupBox.Controls.Add(building2);
                rgroupBox.Controls.Add(employee);
                rgroupBox.Controls.Add(employee2);

                //Configure inner groupBox
                room.AutoSize = true;
                room.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
                room.Location = new Point(10, 33);
                room.Name = "Room Number";
                room.Size = new Size(127, 21);
                room.TabIndex = 0;
                room.Text = $"Room Number:";

                room2.AutoSize = true;
                room2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
                room2.Location = new Point(135, 33);
                room2.Name = $"{reserve.room.roomID}-{counter}";
                room2.Size = new Size(127, 21);
                room2.TabIndex = 0;
                room2.Text = $"{reserve.room.roomID}";

                building.AutoSize = true;
                building.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
                building.Location = new Point(10, 53);
                building.Name = "Building";
                building.Size = new Size(127, 21);
                building.TabIndex = 0;
                building.Text = $"Building:";

                building2.AutoSize = true;
                building2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
                building2.Location = new Point(85, 53);
                building2.Name = $"{reserve.room.building}-{counter}";
                building2.Size = new Size(127, 21);
                building2.TabIndex = 0;
                building2.Text = $"{reserve.room.building}";

                employee.AutoSize = true;
                employee.Font = new Font("Segoe UI", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point);
                employee.Location = new Point(10, 73);
                employee.Name = "Employee";
                employee.Size = new Size(127, 21);
                employee.TabIndex = 0;
                employee.Text = $"Employee:";

                employee2.AutoSize = true;
                employee2.Font = new Font("Segoe UI", 12F, GraphicsUnit.Point);
                employee2.Location = new Point(100, 73);
                employee2.Name = $"{reserve.user.name}-{counter}";
                employee2.Size = new Size(127, 21);
                employee2.TabIndex = 0;
                employee2.Text = $"{reserve.user.name}";

                //Configure the Button
                rbutton.Location = new Point(66, 176);
                rbutton.Name = $"Button-{reserve.resID}";
                rbutton.Size = new Size(78, 46);
                rbutton.TabIndex = 2;
                rbutton.Text = "Cancel";
                rbutton.UseVisualStyleBackColor = true;

                rbutton.Click += new EventHandler(this.Submit);

                //Add the card to the window
                this.Controls.Add(card);
                rgroupBox.ResumeLayout(false);
                card.ResumeLayout(false);
                card.PerformLayout();
                this.ResumeLayout(false);
                counter++;
            }

        }

        private void Logout(object sender, EventArgs e)
        {
            LogoutController.Logout(this.userAccount);
            this.Close();
        }

        public void Submit(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int resID = int.Parse(btn.Name.Split('-')[1]);
            //We need to know Which component we've referenced for this. for now i'm using "this.card"
            CancelController controller = new CancelController(this);
            Debug.WriteLine($"Cancelling Reservation {resID}");
            controller.Submit(userAccount, resID);
            this.Close(); //Close immediately after sending deets to the Controller
        }

        public static void Launch(Account userAccount, Entity.List list)
        {
            new AdminDashboard(userAccount, list).Show();
        }
    }
}
