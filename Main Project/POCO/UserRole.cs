using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class UserRole : IPoco, IUser
    {
        public Int64 ID { get;  set; }
        public RolesEnum UserRoleOf { get;  set; }
        public UserRole()
        {

        }

        public UserRole(RolesEnum userRoleOf)
        {
            UserRoleOf = userRoleOf;
        }
        public override string ToString()
        {
            return $"UserRole ID {ID}, user role {UserRoleOf}";
        }
        public static bool operator ==(UserRole userRole1, UserRole userRole2)
        {
            if (ReferenceEquals(userRole1, null) && ReferenceEquals(userRole2, null))
            {
                return true;
            }
            if (ReferenceEquals(userRole1, null) || ReferenceEquals(userRole2, null))
            {
                return false;
            }
            if (userRole1.ID == userRole2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(UserRole userRole1, UserRole userRole2)
        {
            return !(userRole1 == userRole2);
        }
        public static bool operator >(UserRole userRole1, UserRole userRole2)
        {
            if (userRole1.ID == userRole2.ID)
            {
                return false;
            }
            if (userRole1.ID > userRole2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(UserRole userRole1, UserRole userRole2)
        {
            if (userRole1.ID == userRole2.ID)
            {
                return false;
            }
            if (userRole1.ID > userRole2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(UserRole userRole1, UserRole userRole2)
        {

            if (userRole1.ID >= userRole2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(UserRole userRole1, UserRole userRole2)
        {
            if (userRole1.ID >= userRole2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            UserRole user = obj as UserRole;
            return this == user;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
