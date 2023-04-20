using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Boundary
{
    //Abstract Classes for Form and Dashboard which inherits from Form
    public abstract class Form
    {
        protected Form() { }
        public void close() { }

        public void open() { }
        public void display(string message) { }
    }
    public abstract class Dashboard : Form
    {
        public Dashboard(): base() { }

        public void launch(string username, List<Type> entityList) { }

        public void logout() { }

    }

    //Classes that directly inherit from Form
    public class LoginMenu:Form
    {
        public LoginMenu(): base() { }
        
        public void submit() { }

        public void display( string message) { }
    }

    public class ReserveForm:Form
    {
        public ReserveForm(): base() { }

        public void submit() { }

        public void display(Reservation reservation) { }
    }

    public class CancelForm : Form
    {
        public CancelForm() { }

        public void cancel() { }

        public void display(Reservation reservation) { }
    }

    //Classes that inherit from Dashboard
    public class EmployeeDashboard:Dashboard
    {
        public EmployeeDashboard(): base() { }
        public void submit() { }

        public void launch(string username, List<Room> roomList) { }
    }

    public class AdminDashboard:Dashboard
    {
        public AdminDashboard(): base() { }
    
        public void submit() { }

        public void launch(string username, List<Reservation> reservation) { }

    }
}
