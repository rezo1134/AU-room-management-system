using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{

    public class List
    {
        public List<Reservation> reservations;
        public List<Room> rooms;

        public List(List<Room> rooms) 
        {
            this.rooms = rooms;
        }

        public List(List<Reservation> reservations)
        {
            this.reservations = reservations;
        }
    }
    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string name { get; set; }
        public Account(string username, string role, string password, string name)
        {
            //Because this is all received from the db, validation is assumed to have happened.
            this.username = username;
            this.role = role;
            this.password = password;
            this.name = name;
        }
    }


    public class Room
    {
        public int roomID 
        {
            get
            {
                return this.roomID; 
            }
            set
            {
                if (value < int.MaxValue)
                    this.roomID = value;
            }
        }
        public string building { get; set; }
        public Room(int roomID, string building)
        {
            this.roomID = roomID;
            this.building = building;
        }
    }

    public class Reservation
    {
        public int resID
        {
            get
            {
                return this.resID;
            }
            set
            {
                if (value < int.MaxValue)
                    this.resID = value;
            }
        }
        public Account user { get; set; }
        public int roomID {
            get
            {
                return this.roomID;
            }
            set
            {
                if (value < int.MaxValue)
                    this.roomID = value;
            }
        }
        public DateTime dtg { get; set; }

        //For creating new reservations
        public Reservation(Account userAccount, int roomID)
        {
            this.user = userAccount;
            this.roomID = roomID;
        }
        public Reservation(Account userAccount, int roomID, string datetime)
        {
            this.user = userAccount;
            this.roomID = roomID;
            DateTime dt;
            if (DateTime.TryParse(datetime, out dt) == true)
            {
                this.dtg = dt;
            }
        }

        //For grabbing old reservations from DB
        public Reservation(int resID, Account userAccount, int roomID, DateTime dtg)
        {
            this.resID = resID;
            this.user = userAccount;
            this.roomID=roomID;
            this.dtg = dtg;
        }
    }
}
