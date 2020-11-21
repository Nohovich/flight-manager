using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class AirlineCompany : IPoco, IUser
    {
        public Int64 ID { get;  set; }
        public String AirlineName { get;  set; }
        public Int64 User_Repository_ID { get;  set; }
        public Int64 CountryCode { get;  set; }

        public AirlineCompany(string airlineName, Int64 user_Repository_ID, Int64 countryCode)
        {
            AirlineName = airlineName;
            User_Repository_ID = user_Repository_ID;
            CountryCode = countryCode;
        }
        public AirlineCompany()
        {

        }

        public override string ToString()
        {
            return $" Airline company ID {ID}, airlineName {AirlineName}, User_Repository_ID {User_Repository_ID}, Country code{CountryCode}";
        }
        public static bool operator ==(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            if (ReferenceEquals(airlineCompany1, null) && ReferenceEquals(airlineCompany2, null))
            {
                return true;
            }
            if (ReferenceEquals(airlineCompany1, null) || ReferenceEquals(airlineCompany2, null))
            {
                return false;
            }
            if (airlineCompany1.ID == airlineCompany2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            return !(airlineCompany1 == airlineCompany2);
        }
        public static bool operator >(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            if (airlineCompany1.ID == airlineCompany2.ID)
            {
                return false;
            }
            if (airlineCompany1.ID > airlineCompany2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            if (airlineCompany1.ID == airlineCompany2.ID)
            {
                return false;
            }
            if (airlineCompany1.ID > airlineCompany2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {

            if (airlineCompany1.ID >= airlineCompany2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            if (airlineCompany1.ID >= airlineCompany2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            AirlineCompany airlineCompany = obj as AirlineCompany;
            return this == airlineCompany;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
