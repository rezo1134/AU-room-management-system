﻿using System;
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
        protected Controller() { }
    }

    public class StartupController: Controller
    {
        public StartupController(): base()
        {
            
        }
        public void Initiate()
        {
            DBConnector.InitializeDB();
            new LoginMenu().Show();
        }
    }

    public static class DBConnector
    {
        public static void InitializeDB()
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
                                  [timestamp] DATETIME NOT NULL
                                , [accid] INTEGER NOT NULL
                                , FOREIGN KEY([accid]) REFERENCES [ACCOUNT]([id])
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    table = @"CREATE TABLE [LOGOUT] (
                                  [timestamp] DATETIME NOT NULL
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
                                , [startTime] DATETIME NOT NULL
                                , [stopTime] DATETIME NOT NULL
                                , [employeeid] TEXT NOT NULL
                                , [roomid] TEXT NOT NULL
                                , FOREIGN KEY([employeeid]) REFERENCES [ACCOUNT]([id])
                                , FOREIGN KEY([roomid]) REFERENCES [ROOM]([roomid])
                                );";
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    strSql = @"BEGIN TRANSACTION; 
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('adminuser', 'admin', $hashpwd1, 'John');
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('employeeuser', 'employee', $hashpwd2, 'Joe');
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('jawilt-adm', 'admin', $hashpwd3, 'James');
                                INSERT INTO ACCOUNT (username, type, password, name) VALUES ('jawilt-emp', 'employee', $hashpwd4, 'James');
                                INSERT INTO ROOM (building) VALUES ('Mcknight');
                                INSERT INTO ROOM (building) VALUES ('Barret');
                                INSERT INTO ROOM (building) VALUES ('Gracie');
                                INSERT INTO ROOM (building) VALUES ('Lejuene');
                                INSERT INTO ROOM (building) VALUES ('Grant');
                                INSERT INTO ROOM (building) VALUES ('Hall');
                                INSERT INTO RESERVATION (startTime, stopTime, employeeid, roomid) VALUES ('2023-04-22 08:00:00','2023-04-22 09:00:00', 1, 1);
                                INSERT INTO RESERVATION (startTime, stopTime, employeeid, roomid) VALUES ('2023-04-22 09:00:00','2023-04-22 10:00:00', 2, 2);
                                INSERT INTO RESERVATION (startTime, stopTime, employeeid, roomid) VALUES ('2023-04-23 12:00:00','2023-04-23 13:00:00', 3, 4);
                                INSERT INTO RESERVATION (startTime, stopTime, employeeid, roomid) VALUES ('2023-04-23 13:00:00','2023-04-23 15:00:00', 3, 4);
                                COMMIT;";
                    cmd.CommandText = strSql;
                    cmd.Parameters.AddWithValue("$hashpwd1", "admin123".GetHashCode());
                    cmd.Parameters.AddWithValue("$hashpwd2", "employee123".GetHashCode());
                    cmd.Parameters.AddWithValue("$hashpwd3", "jawiltadm123".GetHashCode());
                    cmd.Parameters.AddWithValue("$hashpwd4", "jawilt123".GetHashCode());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        Debug.WriteLine("Initialized");
        
        }

        public static Account GetUserAccount(string username, string password)
        {
            Debug.WriteLine($"{password}");
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

        public static Entity.List GetList(Account userAccount)
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
                                DateTime startTime = (DateTime)rdr["startTime"];
                                DateTime stopTime = (DateTime)rdr["stopTime"];
                                int roomid = int.Parse(rdr["roomid"].ToString());
                                string name = (string)rdr["name"];
                                string username = (string)rdr["username"];
                                string password = (string)rdr["password"];
                                string role = (string)rdr["type"];
                                string building = (string)rdr["building"];
                                Debug.WriteLine(DateTime.Parse($"{startTime}").ToString());
                                resourceList.Add(new Reservation(id, new Account(username, role, password, name), new Room(roomid, building), startTime, stopTime));
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

        public static void SaveLogin(Account userAccount)
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
                    cmnd.Parameters.AddWithValue("$time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmnd.ExecuteNonQuery();
                    Debug.WriteLine($"Saved Login for {userAccount.username} at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                }
            }
           
        }

        public static void SaveLogout(Account userAccount)
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
                    cmnd.Parameters.AddWithValue("$time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmnd.ExecuteNonQuery();
                    Debug.WriteLine("Saved Logout");

                }
            }
        }

        public static Reservation GetReservation(int resID)
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
                            DateTime startTime = (DateTime)rdr["startTime"];
                            DateTime stopTime = (DateTime)rdr["stopTime"];
                            int empid = int.Parse(rdr["employeeid"].ToString());
                            int roomid = int.Parse(rdr["roomid"].ToString());
                            string name = (string)rdr["name"];
                            string username = (string)rdr["username"];
                            string password = (string)rdr["password"];
                            string role = (string)rdr["type"];
                            string building = (string)rdr["building"];
                            Debug.WriteLine($"Reservation: {id}");
                            res = new Reservation(id, new Account(username, role, password, name), new Room(roomid, building), startTime, stopTime);
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

                stm = @"INSERT INTO RESERVATION (startTime, stopTime, employeeid, roomid) VALUES($startTime, $stopTime, $employeeid, $roomid);";
                using (SQLiteCommand cmnd = new SQLiteCommand())
                {
                    cmnd.Connection = conn;
                    cmnd.CommandText = stm;
                    cmnd.Parameters.AddWithValue("$startTime", $"{reservation.startTime.ToString("yyyy-MM-dd HH:mm:ss")}");
                    cmnd.Parameters.AddWithValue("$stopTime", $"{reservation.stopTime.ToString("yyyy-MM-dd HH:mm:ss")}");
                    cmnd.Parameters.AddWithValue("$employeeid", employeeid);
                    cmnd.Parameters.AddWithValue("$roomid", reservation.room.roomID);
                    cmnd.ExecuteNonQuery();
                }
            }
            //Debug.WriteLine("Saved Logout");
            Debug.WriteLine("Saved Reservation");
        }

        public static void CancelReservation(Reservation reservation)
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

        public bool ValidateInput(string username, string password)
        //Define what our username and password specification should be
        {
            if (username == null || password == null)
            {
                return false;
            }
            //If password or user is under 8 or over 12, not valid
            if (password.Length < 8 || password.Length > 12 || username.Length < 8 || username.Length > 12)
                return false;
            //If pass or user contains no-no character, not valid
            if (username.Contains(" ") || username.Contains("/") || username.Contains("\\") || username.Contains(".") || username.Contains(";") || username.Contains(":"))
                return false;
            if (password.Contains(" ") || password.Contains("/") || password.Contains("\\") || password.Contains(".") || password.Contains(";") || password.Contains(":"))
                return false;
            //Password validation
            int digits = 0;
            int capitals = 0;
            foreach (char chr in password)
            {
                if (char.IsNumber(chr))
                    digits++;
                if (char.IsUpper(chr))
                    capitals++;
            }
            if (digits == 0 && capitals == 0)
                return false;

            return true;
        }
        
        public void UserLogin(string username, string password)
        {
            //This method validates the input of the username and password then calls the GetUserAccount
            if (this.ValidateInput(username, password) == true)
            {
                //After validation, we create a dbconnector and get the user account
                Account userAccount = DBConnector.GetUserAccount(username, password);
                if (userAccount == null)
                {
                    LoginMenu.Display("Incorrect Username or Password; Please Try Again.");
                }
                else
                {
                    DBConnector.SaveLogin(userAccount);
                    Entity.List resourceList = DBConnector.GetList(userAccount);

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
                LoginMenu.Display("Please enter a valid Username or Password.");
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
        public void ReserveRoom(Account userAccount, int roomID, string building)
        {
            //This method creates a reservation object
            Room room = new Room(roomID, building); //placeholder time for creation
            new ReserveForm(userAccount, room).Show(); // Create and display the ReserveForm
        }

        public static void Submit(Account userAccount, Reservation reservation)
        {
            //This method submits a Reservation
            DBConnector.Save(reservation);
            Entity.List resourceList = DBConnector.GetList(userAccount);
            EmployeeDashboard.Launch(userAccount, resourceList);
        }
    }

    public class CancelController : Controller
    {
        public CancelController(Form form) : base(form) 
        {
            this.form = form;
        }

        public static void Cancel(Account userAccount, Reservation reservation)
        {
            //This method cancels a reservation based on the Reservation Primary Key in the DB
            DBConnector.CancelReservation(reservation);
            Entity.List resourceList = DBConnector.GetList(userAccount);

            if (userAccount.role == "admin")
            {
                AdminDashboard.Launch(userAccount, resourceList);
            }
        }

        public void Submit(Account userAccount, int resID)
        {
            //This method sumbits a GetReservation request to the dbconnector and gets a reservation in return
            Reservation reservation = DBConnector.GetReservation(resID);
            new CancelForm(userAccount, reservation).Show(); // Create the ReserveForm
        }
    }

    public class LogoutController: Controller
    {
        public LogoutController(Form form): base(form)
        {
            this.form = form;
        }
        public static void Logout(Account userAccount)
        {
            DBConnector.SaveLogout(userAccount);
            new LogoutMenu().Show();
        }
    }
}
