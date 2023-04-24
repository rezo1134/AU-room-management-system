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
    public partial class EmployeeDashboard : Form
    {
        public Account userAccount;
        public Entity.List resourceList;
        public EmployeeDashboard(Account userAccount, Entity.List resourceList) : base()
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

            foreach (Room res in resourceList.rooms)
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
                innercard.SuspendLayout();
                card.SuspendLayout();
                rgroupBox.SuspendLayout();
                this.SuspendLayout();

                //Configure the Card
                card.Controls.Add(innercard);
                card.Name = $"Card-{res.roomID}-Background";
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
                rlabel.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
                rlabel.Name = $"Label-{res.roomID}";
                rlabel.Text = $"Room #{res.roomID}";
                rlabel.TabIndex = 0;
                rlabel.Location = new Point(56, 9); //Location is relative to the card

                //Configure the groupbox
                rgroupBox.Location = new Point(17, 44);
                rgroupBox.Name = $"GroupBox-{res.roomID}";
                rgroupBox.Size = new Size(176, 126);
                rgroupBox.TabIndex = 1;
                rgroupBox.TabStop = false;
                rgroupBox.Controls.Add(room);
                rgroupBox.Controls.Add(room2);
                rgroupBox.Controls.Add(building);
                rgroupBox.Controls.Add(building2);

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
                room2.Location = new Point(130, 33);
                room2.Name = $"{res.roomID}";
                room2.Size = new Size(127, 21);
                room2.TabIndex = 0;
                room2.Text = $"{res.roomID}";

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
                building2.Name = $"{res.building}";
                building2.Size = new Size(127, 21);
                building2.TabIndex = 0;
                building2.Text = $"{res.building}";

                //Configure the Button
                rbutton.Location = new Point(66, 176);
                rbutton.Name = $"Button-{res.roomID}-{res.building}";
                rbutton.Size = new Size(78, 46);
                rbutton.TabIndex = 2;
                rbutton.Text = "Reserve";
                rbutton.UseVisualStyleBackColor = true;
                rbutton.Click += new EventHandler(this.reservationClick);

                //Add the card to the window
                this.Controls.Add(card);
                rgroupBox.ResumeLayout(false);
                card.ResumeLayout(false);
                card.PerformLayout();
                this.ResumeLayout(false);
            }

        }

        private void reservationClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int roomID = int.Parse(btn.Name.Split('-')[1]);
            string building = btn.Name.Split('-')[2];

            ReserveController controller = new ReserveController(this);
            controller.reserve(roomID, building);
            this.Close(); //Close immediately after sending deets to the Controller
        }
        public static void Launch(Account userAccount, Entity.List list)
        {
            new EmployeeDashboard(userAccount, list).Show();
        }

        private void logoutClick(object sender, EventArgs e)
        {
            LogoutController.logout(new Account("jawilt", "employee", "test123", "James"));
            this.Close();
        }
    }
}
