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
        private int _roomID;
        public int roomID 
        {
            get
            {
                return this._roomID; 
            }
            set
            {
                if (value < int.MaxValue)
                    this._roomID = value;
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
        private int _resID;
        public int resID
        {
            get
            {
                return this._resID;
            }
            set
            {
                if (value < int.MaxValue)
                    this._resID = value;
            }
        }
        public Account user { get; set; }
        public Room room { get; set; }
        public DateTime dtg { get; set; } 

        //For grabbing old reservations from DB
        public Reservation(int resID, Account userAccount, Room room, DateTime dtg)
        {
            this.resID = resID;
            this.user = userAccount;
            this.room = room;
            this.dtg = dtg;
        }
    }
}
