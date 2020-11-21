using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class Ticket : IPoco
    {
        public Int64 ID { get;  set; }
        public Int64 FlightID { get;  set; }
        public Int64 CustomerID { get;  set; }
        public Ticket(long flightID, long customerID)
        {
            FlightID = flightID;
            CustomerID = customerID;
        }
        public Ticket()
        {

        }

        public override string ToString()
        {
            return $"Ticket ID {ID}, flightID {FlightID}, customerID {CustomerID}";
        }
        public static bool operator ==(Ticket ticket1, Ticket ticket2)
        {
            if (ReferenceEquals(ticket1, null) && ReferenceEquals(ticket2, null))
            {
                return true;
            }
            if (ReferenceEquals(ticket2, null) || ReferenceEquals(ticket2, null))
            {
                return false;
            }
            if (ticket1.ID == ticket2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Ticket ticket1, Ticket ticket2)
        {
            return !(ticket1 == ticket2);
        }
        public static bool operator >(Ticket ticket1, Ticket ticket2)
        {
            if (ticket1.ID == ticket2.ID)
            {
                return false;
            }
            if (ticket1.ID > ticket2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Ticket ticket1, Ticket ticket2)
        {
            if (ticket1.ID == ticket2.ID)
            {
                return false;
            }
            if (ticket1.ID > ticket2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Ticket ticket1, Ticket ticket2)
        {

            if (ticket1.ID >= ticket2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Ticket ticket1, Ticket ticket2)
        {
            if (ticket1.ID >= ticket2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            Ticket ticket = obj as Ticket;
            return this == ticket;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
