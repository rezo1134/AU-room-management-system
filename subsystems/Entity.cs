using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Account
    {
        private string username { get; set; }
        private string password { get; set; }
        private string role { get; set; }
        private string name { get; set; }
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
        private int roomID 
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
        private string building { get; set; }
        public Room(int roomID, string building)
        {
            this.roomID = roomID;
            this.building = building;
        }
    }

    public class Reservation
    {
        private int rid
        {
            get
            {
                return this.rid;
            }
            set
            {
                if (value < int.MaxValue)
                    this.rid = value;
            }
        }
        private string usn { get; set; }
        private int roomID {
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
        private DateTime dtg { get; set; }

        //For creating new reservations
        public Reservation(string usn, int roomID, string datetime)
        {
            this.usn = usn;
            this.roomID = roomID;
            DateTime dt;
            if (DateTime.TryParse(datetime, out dt) == true)
            {
                this.dtg = dt;
            }
        }

        //For grabbing old reservations from DB
        public Reservation(int rid, string usn, int roomID, DateTime dtg)
        {
            this.rid = rid;
            this.usn = usn;
            this.roomID=roomID;
            this.dtg = dtg;
        }
    }
}
