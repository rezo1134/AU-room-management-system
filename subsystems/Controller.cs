using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
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
    }

    public static class DBConnector
    {
        public static void initializeDB()
        {
            //DB Initialization. This should read data in from a flat file and create all the tables
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db")))
                File.Create(Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db"));

            //DB Initialization. This should read data in from a flat file and create all the tables

            //File path is in the project folder bin
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    string strSql = @"BEGIN TRANSACTION;
                    DROP TABLE IF EXISTS ACCOUNT;
                    DROP TABLE IF EXISTS LOGIN;
                    DROP TABLE IF EXISTS LOGOUT;
                    DROP TABLE IF EXISTS ROOM;
                    DROP TABLE IF EXISTS RESERVATION;
                    COMMIT;";
                    cmd.CommandText = strSql;
                    cmd.ExecuteNonQuery();
                    string table = @"CREATE TABLE [ACCOUNT] (
                                  [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [username] TEXT NOT NULL
                                , [type] TEXT NOT NULL
                                , [password] TEXT NOT NULL
                                , [NAME] TEXT NOT NULL
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    table = @"CREATE TABLE [LOGIN] (
                                  [timestamp] TEXT NOT NULL
                                , [accid] INTEGER NOT NULL
                                , FOREIGN KEY([accid]) REFERENCES [ACCOUNT]([id])
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    table = @"CREATE TABLE [LOGOUT] (
                                  [timestamp] TEXT NOT NULL
                                , [accid] INTEGER NOT NULL
                                , FOREIGN KEY([accid]) REFERENCES [ACCOUNT]([id])
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    table = @"CREATE TABLE [ROOM] (
                                  [roomid] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [building] TEXT NOT NULL
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    table = @"CREATE TABLE [RESERVATION] (
                                  [reservationid] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                , [date] TEXT NOT NULL
                                , [time] TEXT NOT NULL
                                , [employeeid] TEXT NOT NULL
                                , [roomid] TEXT NOT NULL
                                , FOREIGN KEY([employeeid]) REFERENCES [ACCOUNT]([id])
                                , FOREIGN KEY([roomid]) REFERENCES [ROOM]([roomid])
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    strSql = @"BEGIN TRANSACTION; 
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('adm', 'admin', $hashpwd1, 'John');
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('emp', 'employee', $hashpwd2, 'Joe');
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('jawilt-adm', 'admin', $hashpwd3, 'James');
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('jawilt', 'employee', $hashpwd4, 'James');
                                INSERT INTO ROOM (building) VALUES ('Mcknight');
                                INSERT INTO ROOM (building) VALUES ('Barret');
                                INSERT INTO ROOM (building) VALUES ('Gracie');
                                INSERT INTO ROOM (building) VALUES ('Lejuene');
                                INSERT INTO ROOM (building) VALUES ('Grant');
                                INSERT INTO ROOM (building) VALUES ('Hall');
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/22/2023', '8:00', 1, 1);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/22/2023', '9:00', 2, 2);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/23/2023', '12:00', 3, 4);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/23/2023', '13:00', 3, 4);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/25/2023', '14:00', 2, 1);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/25/2023', '10:00', 4, 5);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/26/2023', '8:00', 1, 4);
                                INSERT INTO RESERVATION (date, time, employeeid, roomid) VALUES ('4/26/2023', '9:00', 1, 1);

                                COMMIT;";
                    cmd.CommandText = strSql;
                    cmd.Parameters.AddWithValue("$hashpwd1", "1qaz".GetHashCode());
                    cmd.Parameters.AddWithValue("$hashpwd2", "2wsx".GetHashCode());
                    cmd.Parameters.AddWithValue("$hashpwd3", "admin123".GetHashCode());
                    cmd.Parameters.AddWithValue("$hashpwd4", "qwer".GetHashCode());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        Debug.WriteLine("Initialized");
        }

        public static Account getUserAccount(string username, string password)
        {
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                conn.Open();
                int x = username.GetHashCode();
                int y = password.GetHashCode();
                string stm = @"SELECT[id]
                    ,[username]
                    ,[password]
                    ,[type]
                    ,[name]
                    FROM[ACCOUNT]
                    WHERE[username] == ($name)
                    AND[password] == ($pd);";
                using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
                {
                    cmd.Parameters.AddWithValue("$name", username);
                    cmd.Parameters.AddWithValue("$pd", y);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //Account acct = new Account(rdr.GetInt32(0), user, rdr.GetString(3));
                            Debug.WriteLine($"{rdr.GetInt32(0).ToString()} {username}  {rdr.GetString(1).ToString()}  {rdr.GetString(2).ToString()}  {rdr.GetString(3).ToString()}");
                            Account userAcc = new Account(username, rdr.GetString(3).ToString(), password, rdr.GetString(4).ToString());
                            return userAcc;
                        }
                        //Account act = new Account(0, null, null);
                        return null;
                    }
                }
            }
        }

        public static Entity.List getList(Account userAccount)
        {
            if (userAccount.role == "admin")
            {
                List<Reservation> resourceList = new List<Reservation>();
                string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
                using (SQLiteConnection conn = new SQLiteConnection(root))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM RESERVATION JOIN ACCOUNT ON RESERVATION.EMPLOYEEID = ACCOUNT.ID JOIN ROOM ON RESERVATION.ROOMID = ROOM.ROOMID;";
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                int id = rdr.GetInt32(0);
                                string date = (string)rdr["date"];
                                string time = (string)rdr["time"];
                                int roomid = int.Parse(rdr["roomid"].ToString());
                                string name = (string)rdr["name"];
                                string username = (string)rdr["username"];
                                string password = (string)rdr["password"];
                                string role = (string)rdr["type"];
                                string building = (string)rdr["building"];
                                Debug.WriteLine(DateTime.Parse($"{date} {time}").ToString());
                                resourceList.Add(new Reservation(id, new Account(username, role, password, name), new Room(roomid, building), DateTime.Parse($"{date} {time}")));
                            }
                        }
                    }
                }
                return new Entity.List(resourceList);
            }
            else
            {
                List<Room> resourceList = new List<Room>();
                string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
                using (SQLiteConnection conn = new SQLiteConnection(root))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM ROOM;";
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                int roomid = int.Parse(rdr["roomid"].ToString());
                                string building = (string)rdr["building"];
                                resourceList.Add(new Room(roomid, building));
                            }
                        }
                    }
                }
                return new Entity.List(resourceList);
            }
            
        }

        public static void saveLogin(Account userAccount)
        {
            //This method saves the login user and time to the database
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                conn.Open();
                int id = 0;
                // int hash = userAccount.username.GetHashCode();
                string stm = "SELECT [id] FROM ACCOUNT WHERE username = ($name);";
                using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
                {
                    cmnd.Parameters.AddWithValue("$name", userAccount.username);
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            id = rdr.GetInt32(0);
                        }
                    }
                }
                stm = @"INSERT INTO LOGIN VALUES($time,$id);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$id", id);
                    cmnd.Parameters.AddWithValue("$time", DateTime.Now.ToString());
                    cmnd.ExecuteNonQuery();
                    Debug.WriteLine($"Saved Login for {userAccount.username} at {DateTime.Now.ToString()}");
                }
            }
           
        }

        public static void saveLogout(Account userAccount)
        {
            //This method saves the login user and time to the database
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                conn.Open();
                int id = 0;
                // int hash = userAccount.username.GetHashCode();
                string stm = "SELECT [id] FROM ACCOUNT WHERE username = ($name);";
                using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
                {
                    cmnd.Parameters.AddWithValue("$name", userAccount.username);
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            id = rdr.GetInt32(0);
                        }
                    }
                }
                stm = @"INSERT INTO LOGOUT VALUES($time, $id);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$id", id);
                    cmnd.Parameters.AddWithValue("$time", DateTime.Now.ToString());
                    cmnd.ExecuteNonQuery();
                    Debug.WriteLine("Saved Logout");

                }
            }
        }

        public static Reservation getReservation(int resID)
        {
            //This method Deletes a Reservation from the database via the resID primary key
            Reservation res = null;
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                conn.Open();
                string stm = "SELECT * FROM RESERVATION JOIN ACCOUNT ON RESERVATION.EMPLOYEEID = ACCOUNT.ID JOIN ROOM ON RESERVATION.ROOMID=ROOM.ROOMID WHERE reservationid = ($resID);";
                using (SQLiteCommand cmd = new SQLiteCommand(stm, conn))
                {
                    cmd.Parameters.AddWithValue("$resID", resID);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int id = rdr.GetInt32(0);
                            string date = (string)rdr["date"];
                            string time = (string)rdr["time"];
                            int empid = int.Parse(rdr["employeeid"].ToString());
                            int roomid = int.Parse(rdr["roomid"].ToString());
                            string name = (string)rdr["name"];
                            string username = (string)rdr["username"];
                            string password = (string)rdr["password"];
                            string role = (string)rdr["type"];
                            string building = (string)rdr["building"];
                            Debug.WriteLine($"Reservation: {id}");
                            res = new Reservation(id, new Account(username, role, password, name), new Room(roomid, building), DateTime.Parse($"{date} {time}"));
                        }
                    }
                }
            }
            return res;
        }


         public static void Save(Reservation reservation)
        {
            int employeeid = 0;

            //This method saves a new reservation to the Database
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                conn.Open();
                string stm = "SELECT [id] FROM ACCOUNT WHERE username = ($name);";
                using (SQLiteCommand cmnd = new SQLiteCommand(stm, conn))
                {
                    cmnd.Parameters.AddWithValue("$name", reservation.user.username);
                    using (SQLiteDataReader rdr = cmnd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            employeeid = rdr.GetInt32(0);
                        }
                    }
                }

                stm = @"INSERT INTO RESERVATION VALUES($reservationid, $date, $time, $employeeid, $roomid);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$reservationid", reservation.resID);
                    cmnd.Parameters.AddWithValue("$date", $"{reservation.dtg.ToString().Split(' ')[0]}");
                    cmnd.Parameters.AddWithValue("$time", $"{reservation.dtg.ToString().Split(' ')[1]}");
                    cmnd.Parameters.AddWithValue("$employeeid", employeeid);
                    cmnd.Parameters.AddWithValue("$roomid", reservation.room.roomID);
                    cmnd.ExecuteNonQuery();
                }
            }
            //Debug.WriteLine("Saved Logout");
            Debug.WriteLine("Saved Reservation");
        }

        public static void cancelReservation(Reservation reservation)
        {
            //This method Deletes a Reservation from the database via the resID primary key
            string root = "Data Source=" + Path.Combine(Environment.CurrentDirectory, @"Data\", "RoomsDB.db");
            using (SQLiteConnection conn = new SQLiteConnection(root))
            {
                conn.Open();
                string stm = "DELETE FROM RESERVATION WHERE reservationid = ($resID);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$resID", reservation.resID);
                    cmnd.ExecuteNonQuery();
                }
            }
            Debug.WriteLine($"Cancelled Reservation: {reservation.resID}");
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
            if (username == null || password == null)
            {
                return false;
            }
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
                else
                {
                    DBConnector.saveLogin(userAccount);
                    Entity.List resourceList = DBConnector.getList(userAccount);

                    if (userAccount.role == "admin")
                    {
                        AdminDashboard.Launch(userAccount, resourceList);
                        this.form.Close();
                    }
                    else if (userAccount.role == "employee")
                    {
                        EmployeeDashboard.Launch(userAccount, resourceList);
                        this.form.Close();
                    }
                }
            }
            else
            {
                LoginMenu.display("Please enter a valid Username or Password.");
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
            this.form = new ReserveForm();
            this.form.Show(); // Create the ReserveForm
            //this.form.display(room);

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

        public static void cancel(Account userAccount, Reservation reservation)
        {
            //This method cancels a reservation based on the Reservation Primary Key in the DB
            DBConnector.cancelReservation(reservation);
            Entity.List resourceList = DBConnector.getList(userAccount);

            if (userAccount.role == "admin")
            {
                AdminDashboard.Launch(userAccount, resourceList);
            }
        }

        public void submit(Account userAccount, int resID)
        {
            //This method sumbits a getReservation request to the dbconnector and gets a reservation in return
            Reservation reservation = DBConnector.getReservation(resID);
            new CancelForm(userAccount, reservation).Show(); // Create the ReserveForm
            //this.form.Show();
            //this.form.display(reservation);

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
            DBConnector.saveLogout(userAccount);
            new LogoutMenu().Show();
        }
    }
}
