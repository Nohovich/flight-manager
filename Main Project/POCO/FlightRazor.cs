﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class FlightRazor
    {
        public Int64 ID { get; set; }
        public string AirlineName { get; set; }
        // public string Airline_Pic { get; set; }
        public string OriginCountry { get; set; }
        public String DestinationCountry { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public Int64 RemainingTickets { get; set; }
        public string AirlineNameAndFlightID { get; set; }
        public override string ToString()
        {
            return $" Flight ID {ID}, airline Company {AirlineName}, origin country {OriginCountry}, destination country {DestinationCountry}, departure time {DepartureTime}, landing time {LandingTime}, remaining tickets {RemainingTickets}";
        }
        public static bool operator ==(FlightRazor flight1, FlightRazor flight2)
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
        public static bool operator !=(FlightRazor flight1, FlightRazor flight2)
        {
            return !(flight1 == flight2);
        }
        public static bool operator >(FlightRazor flight1, FlightRazor flight2)
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
        public static bool operator <(FlightRazor flight1, FlightRazor flight2)
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
        public static bool operator >=(FlightRazor flight1, FlightRazor flight2)
        {

            if (flight1.ID >= flight2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(FlightRazor flight1, FlightRazor flight2)
        {
            if (flight1.ID >= flight2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            FlightRazor flight = obj as FlightRazor;
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
