using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class Country : IPoco
    {
        public Int64 ID { get;  set; }
        public String CountryName { get;  set; }

        public Country(string countryName)
        {
            CountryName = countryName;
        }
        public Country()
        {

        }
        public override string ToString()
        {
            return $" Country ID {ID}, country name {CountryName}";
        }
        public static bool operator ==(Country country1, Country country2)
        {
            if (ReferenceEquals(country1, null) && ReferenceEquals(country2, null))
            {
                return true;
            }
            if (ReferenceEquals(country1, null) || ReferenceEquals(country2, null))
            {
                return false;
            }
            if (country1.ID == country2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Country country1, Country country2)
        {
            return !(country1 == country2);
        }
        public static bool operator >(Country country1, Country country2)
        {
            if (country1.ID == country2.ID)
            {
                return false;
            }
            if (country1.ID > country2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Country country1, Country country2)
        {
            if (country1.ID == country2.ID)
            {
                return false;
            }
            if (country1.ID > country2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Country country1, Country country2)
        {

            if (country1.ID >= country2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Country country1, Country country2)
        {
            if (country1.ID >= country2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            Country country = obj as Country;
            return this == country;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
