using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class Admin : IPoco, IUser
    {
        public Int64 ID { get; set; }
        public Int64 UserRepositoryID { get; set; }
        public String FirstName { get;  set; }
        public String LastName { get; set; }

        public Admin(string firstName, string lastName,Int64 userRepositoryID)
        {
            FirstName = firstName;
            LastName = lastName;
            UserRepositoryID = userRepositoryID;
        }
        public Admin()
        {

        }

        public override string ToString()
        {
            return $"Admin ID {ID}, UserRepositoryID {UserRepositoryID}, first name {FirstName}, last name{LastName}";
        }
        public static bool operator ==(Admin admin1, Admin admin2)
        {
            if (ReferenceEquals(admin1, null) && ReferenceEquals(admin2, null))
            {
                return true;
            }
            if (ReferenceEquals(admin1, null) || ReferenceEquals(admin2, null))
            {
                return false;
            }
            if (admin1.ID == admin2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Admin admin1, Admin admin2)
        {
            return !(admin1 == admin2);
        }
        public static bool operator >(Admin admin1, Admin admin2)
        {
            if (admin1.ID == admin2.ID)
            {
                return false;
            }
            if (admin1.ID > admin2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Admin admin1, Admin admin2)
        {
            if (admin1.ID == admin2.ID)
            {
                return false;
            }
            if (admin1.ID > admin2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Admin admin1, Admin admin2)
        {

            if (admin1.ID >= admin1.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Admin admin1, Admin admin2)
        {
            if (admin1.ID >= admin1.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            Admin admin = obj as Admin;
            return this == admin;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
