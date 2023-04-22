﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using Entity;
namespace Boundary
{
    public partial class Logout : Form
    {
        public Logout()
        {
            InitializeComponent();
        }

        private void resetClick(object sender, EventArgs e)
        {
            //We again need the userAccount object to save
            LogoutController.logout(userAccount);
            this.Close();

        }
    }
}
