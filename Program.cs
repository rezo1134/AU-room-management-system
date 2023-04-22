using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Boundary;
using Entity;
using Controller;
namespace AU_room_management_system
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //Initialize the application Configuration
            ApplicationConfiguration.Initialize();

            //Create the DBConnector object and initialize the DB
            DBConnector.initializeDB();

            //Launch the Login page
            Application.Run(new LoginMenu());
        }
    }
}