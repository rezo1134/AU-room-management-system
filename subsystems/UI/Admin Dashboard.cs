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

            foreach (Reservation reserve in resourceList.reservations)
            {


                Panel card = new Panel();
                Panel innercard = new Panel();
                Label rlabel = new Label();
                GroupBox rgroupBox = new GroupBox();
                Button rbutton = new Button();
                innercard.SuspendLayout();
                card.SuspendLayout();
                this.SuspendLayout();

                //Configure the Card
                card.Controls.Add(innercard);
                card.Name = $"Card-{reserve.roomID}-Background";
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
                rlabel.Name = $"Label-{reserve.resID}";
                rlabel.Text = $"Room #{reserve.resID}";
                rlabel.TabIndex = 0;
                rlabel.Location = new Point(56, 9); //Location is relative to the card

                //Configure the groupbox
                rgroupBox.Location = new System.Drawing.Point(17, 44);
                rgroupBox.Name = $"GroupBox-{reserve.roomID}";
                rgroupBox.Size = new System.Drawing.Size(176, 126);
                rgroupBox.TabIndex = 1;
                rgroupBox.TabStop = false;

                //Configure the Button
                rbutton.Location = new System.Drawing.Point(66, 176);
                rbutton.Name = $"Button-{reserve.resID}";
                rbutton.Size = new System.Drawing.Size(78, 46);
                rbutton.TabIndex = 2;
                rbutton.Text = "Cancel";
                rbutton.UseVisualStyleBackColor = true;

                rbutton.Click += new EventHandler(this.submitClick);

                //Add the card to the window
                this.Controls.Add(card);
                card.ResumeLayout(false);
                card.PerformLayout();
                this.ResumeLayout(false);
            }

        }

        private void logoutClick(object sender, EventArgs e)
        {
            LogoutController.logout(new Account("jawilt", "admin", "test123", "James"));
            this.Close();
        }

        public void submitClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int resID = int.Parse(btn.Name.Split('-')[1]);
            //We need to know Which component we've referenced for this. for now i'm using "this.card"
            CancelController controller = new CancelController(this);
            //controller.submit(resid);
            Debug.WriteLine($"Cancelling Reservation {resID}");
            controller.submit(resID);
            this.Close(); //Close immediately after sending deets to the Controller
        }

        public static void Launch(Account userAccount, Entity.List list)
        {
            new AdminDashboard(userAccount, list).Show();
        }
    }
}
