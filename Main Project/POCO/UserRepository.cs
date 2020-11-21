using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class UserRepository : IPoco, IUser
    {
        public Int64 ID { get;  set; }
        public String UserName { get;  set; }
        public String Password { get;  set; }
        public RolesEnum UserRoleID { get;  set; }
        public UserRepository(string userName, string password, RolesEnum userRoleID)
        {
            UserName = userName;
            Password = password;
            UserRoleID = userRoleID;
        }
        public UserRepository()
        {

        }
        public override string ToString()
        {
            return $"UserRepository ID {ID}, user Name {UserName}, password {Password}, UserRole {UserRoleID}";
        }
        public static bool operator ==(UserRepository user1, UserRepository user2)
        {
            if (ReferenceEquals(user1, null) && ReferenceEquals(user2, null))
            {
                return true;
            }
            if (ReferenceEquals(user1, null) || ReferenceEquals(user2, null))
            {
                return false;
            }
            if (user1.ID == user2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(UserRepository user1, UserRepository user2)
        {
            return !(user1 == user2);
        }
        public static bool operator >(UserRepository user1, UserRepository user2)
        {
            if (user1.ID == user2.ID)
            {
                return false;
            }
            if (user1.ID > user2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(UserRepository user1, UserRepository user2)
        {
            if (user1.ID == user2.ID)
            {
                return false;
            }
            if (user1.ID > user2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(UserRepository user1, UserRepository user2)
        {

            if (user1.ID >= user2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(UserRepository user1, UserRepository user2)
        {
            if (user1.ID >= user2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            UserRepository user = obj as UserRepository;
            return this == user;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
