using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boundary;
using Entity;
namespace Controller
{
    public abstract class Controller
    {
        public Form form { get; set; }
        protected Controller(Form form) 
        {
            this.form = form;
        }
        //protected Controller() { }
    }
    
    public static class DBConnector
    {

        public static void initializeDB()
        {
            //DB Initialization. This should read data in from a flat file and create all the tables
        }

        public static Account getUserAccount(string username, string password)
        {
            //This methoud should Query the DB for an account, and then return the info in an Account Object
            Account userAccount = new Account("jawilt", "admin", "test123", "James");
            return userAccount;

        }

        public static void saveLogin(Account userAccount)
        {
            //This method saves the login user and time to the database
        }

        public static Entity.List getList(Account userAccount)
        {
            //This method needs to evaluate the accountType and then return a list of either
            //Reservations for the Admin, or Rooms for the User
            Entity.List list = new Entity.List(new List<Room>());
            return list;


        }

        public static void Save(Reservation reservation)
        {
            //This method saves a new reservation to the Database
        }

        public static Reservation getReservation(int resID)
        {
            //This method needs to query the database for the reservation based on the resID
            DateTime dt = DateTime.Now;
            Reservation reservation = new Reservation("jawilt", resID, dt.ToString());
            return reservation;
        }

        public static void cancelReservation(int resID)
        {
            //This method Deletes a Reservation from the database via the resID primary key
        }

        public static void saveLogout(Account userAccount, string time)
        {
            //This method saves the logout of username and time
        }
    }

    public class LoginController : Controller
    {
        public LoginController(Form form) : base(form)
        {
            this.form = form;
            //This is the Login Controller Constructor
        }

        public bool validateInput(string username, string password)
        //Define what our username and password specification should be
        {
            return true;
        }
        public void userLogin(string username, string password)
        {
            //This method validates the input of the username and password then calls the GetUserAccount
            if (this.validateInput(username, password) == true)
            {
                //After validation, we create a dbconnector and get the user account
                Account userAccount = DBConnector.getUserAccount(username, password);
                if (userAccount == null)
                {
                    LoginMenu.display("Incorrect Username or Password; Please Try Again.");
                }
                this.form.Close();
                DBConnector.saveLogin(userAccount);
                Entity.List resourceList = DBConnector.getList(userAccount);
                if (userAccount.role == "admin")
                {
                    AdminDashboard.Launch(userAccount, resourceList);
                }
            }
        }
    }
    
    public class ReserveController : Controller
    {
        public ReserveController(Form form) : base(form)
        {
            //This is the Constructor for the ReserveController
            this.form = form;

        }
        public void reserve(int roomID, string building)
        {
            //This method creates a reservation object
            Room room = new Room(roomID, building); //placeholder time for creation
            this.form = new ReserveForm(); // Create the ReserveForm
            this.form.display(room);

            //Then it creates and calls display from the ReserveForm
        }

        public static void submit(Reservation reservation)
        {
            //This method submits a Reservation
            DBConnector.Save(reservation);
            Entity.List resourceList = DBConnector.getList(reservation.user);

            if (reservation.user.role == "employee")
            {
                EmployeeDashboard.Launch(reservation.user, resourceList);
            }
        }
    }

    public class CancelController : Controller
    {
        public CancelController(Form form) : base(form) 
        {
            this.form = form;
        }

        public static void cancel(Reservation reservation)
        {
            //This method cancels a reservation based on the Reservation Primary Key in the DB
            DBConnector.cancelReservation(reservation.resID);
            Entity.List resourceList = DBConnector.getList(reservation.user);

            if (reservation.user.role == "admin")
            {
                AdminDashboard.Launch(reservation.user, resourceList);
            }
        }

        public void submit(int resID)
        {
            //This method sumbits a getReservation request to the dbconnector and gets a reservation in return
            Reservation reservation = DBConnector.getReservation(resID);
            this.form = new CancelForm(); // Create the ReserveForm
            this.form.display(reservation);

        }
    }

    public class LogoutController: Controller
    {
        public LogoutController(Form form): base(form)
        {
            this.form = form;
        }
        public static void logout(Account userAccount)
        {
            DBConnector.saveLogout(userAccount, DateTime.Now.ToString());
            new LoginMenu().Show();
        }
    }
}
