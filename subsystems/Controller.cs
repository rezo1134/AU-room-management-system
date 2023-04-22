using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boundary;
using Entity;
using Boundary;
namespace Controller
{
    public abstract class Controller
    {
        public Boundary.Form form { get; set; }
        public DBConnector dbconn { get; set; }

        //protected Controller(Boundary.Form form, DBConnector dbconn) 
        //{
        //    this.form = form;
        //    this.dbconn = dbconn;
        //}
        protected Controller() { }
    }
    
    public class DBConnector
    {
        public DBConnector()
        {
            //This is the constructor to the DataBase
        }

        public void initializeDB()
        {
            //DB Initialization. This should read data in from a flat file and create all the tables
        }

        public Account getUserAccount(string username, string password)
        {
            //This methoud should Query the DB for an account, and then return the info in an Account Object
            Account userAccount = new Account("jawilt", "admin", "test123", "James");
            return userAccount;

        }

        public void saveLogin(string username, string datetime)
        {
            //This method saves the login user and time to the database
        }

        public Entity.List<object> getList(string accountType)
        {
            //This method needs to evaluate the accountType and then return a list of either
            //Reservations for the Admin, or Rooms for the User

            return null;


        }

        public void Save(Reservation reservation)
        {
            //This method saves a new reservation to the Database
        }

        public Reservation getReservation(int rid)
        {
            //This method needs to query the database for the reservation based on the rid
            DateTime dt = DateTime.Now;
            Reservation reservation = new Reservation("jawilt", rid, dt.ToString());
            return reservation;
        }

        public void cancelReservation(int rid)
        {
            //This method Deletes a Reservation from the database via the rid primary key
        }

        public void SaveLogout(string username, string datetime)
        {
            //This method saves the logout of username and time
        }
    }

    public class LoginController : Controller
    {
        public LoginController() : base()
        {
            //This is the Login Controller Constructor
        }

        public bool validateInput(string username, string password)
        //Define what our username and password specification should be
        {
            return true;
        }
        public Account userLogin(string username, string password)
        {
            //This method validates the input of the username and password then calls the GetUserAccount
            if (this.validateInput(username, password) == true)
            {
                //After validation, we create a dbconnector and get the user account
                Account userAccount = this.dbconn.getUserAccount(username, password);
                return userAccount;
            }
            Account emptyAccount = new Account("empty", "empty", "empty", "empty");
            Boundary.LoginMenu.display("Incorrect Username or Password; Please Try Again.");
            return emptyAccount;

        }
    }
    
    public class ReserveController : Controller
    {
        public ReserveController() : base()
        {
            //This is the Constructor for the ReserveController

        }
        public void reserve(int roomID, string username)
        {
            //This method creates a reservation object
            Reservation reservation = new Reservation(username, roomID, DateTime.Now.ToString());

            //Then it creates and calls display from the ReserveForm
        }

        public void submit(Reservation reservation)
        {
            //This method submits a Reservation
        }
    }

    public class CancelController : Controller
    {
        public CancelController() : base() { }

        public void cancel(int rid)
        {
            //This method cancels a reservation based on the Reservation Primary Key in the DB
            this.dbconn.cancelReservation(rid);

            //Then we need to call the this.dbconn.getReservation

            //Then we need to create and launch the AdminDashboard with the new list.
        }

        public void submit(int rid)
        {
            //This method sumbits a getReservation request to the dbconnector and gets a reservation in return
            Reservation reservation = this.dbconn.getReservation(rid);
        }
    }
}
