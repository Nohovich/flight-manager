using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class Flight : IPoco
    {
        public Int64 ID { get;  set; }
        public Int64 AirlineCompanyID { get;  set; }
        public Int64 OriginCountryCode { get;  set; }
        public Int64 DestinationCountryCode { get;  set; }
        public DateTime DepartureTime { get;  set; }
        public DateTime LandingTime { get;  set; }
        public Int64 RemainingTickets { get;  set; }
        public Flight(DateTime departureTime, DateTime landingTime, long remaninigTickets, Int64 airlineCompanyID, Int64 originCountryCode, Int64 destinationCountryCode)
        {
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTickets = remaninigTickets;
            AirlineCompanyID = airlineCompanyID;
            OriginCountryCode = originCountryCode;
            DestinationCountryCode = destinationCountryCode;

        }
        public Flight()
        {

        }
        public override string ToString()
        {
            return $" Flight ID {ID}, AirlineCompanyID {AirlineCompanyID}, origin country code {OriginCountryCode}, destination country code {DestinationCountryCode}, departure time {DepartureTime}, landing time {LandingTime}, remaining tickets {RemainingTickets}";
        }
        public static bool operator ==(Flight flight1, Flight flight2)
        {
            if (ReferenceEquals(flight1, null) && ReferenceEquals(flight2, null))
            {
                return true;
            }
            if (ReferenceEquals(flight1, null) || ReferenceEquals(flight2, null))
            {
                return false;
            }
            if (flight1.ID == flight2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Flight flight1, Flight flight2)
        {
            return !(flight1 == flight2);
        }
        public static bool operator >(Flight flight1, Flight flight2)
        {
            if (flight1.ID == flight2.ID)
            {
                return false;
            }
            if (flight1.ID > flight2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Flight flight1, Flight flight2)
        {
            if (flight1.ID == flight2.ID)
            {
                return false;
            }
            if (flight1.ID > flight2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Flight flight1, Flight flight2)
        {

            if (flight1.ID >= flight2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Flight flight1, Flight flight2)
        {
            if (flight1.ID >= flight2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            Flight flight = obj as Flight;
            return this == flight;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }

        public string GetStatus()
        {
            int minutes = LandingTime.Subtract(DateTime.Now).Minutes;
            int hours = LandingTime.Subtract(DateTime.Now).Hours;
            if (hours <= 2)
            {
                if (hours <= 0 && minutes <= 0)
                    return "Landed";
                else if (hours == 0 && minutes > 0 && minutes < 16)
                    return "Landing";
                else if (hours > -1 && minutes > 15)
                    return "Final";
            }
            return "Not Final";
        }
    }
}
