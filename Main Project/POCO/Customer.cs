using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.POCO
{
    public class Customer : IPoco, IUser
    {

        public Int64 ID { get; set; }
        public Int64 User_Repository_ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String phoneNumber { get; set; }
        public String CreditCard { get; set; }

        public Customer(string firstName, string lastName, string address, string phoneNumber, string creditCard, Int64 user_Repository_ID)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            this.phoneNumber = phoneNumber;
            this.CreditCard = creditCard;
            User_Repository_ID = user_Repository_ID;
        }
        public Customer()
        {

        }
        public override string ToString()
        {
            return $" Customer ID {ID}, first name {FirstName}, last name {LastName}, user_Repository_ID {User_Repository_ID}, address ID {Address}, phone number {phoneNumber}, credit card {CreditCard}";
        }
        public static bool operator ==(Customer customer1, Customer customer2)
        {
            if (ReferenceEquals(customer1, null) && ReferenceEquals(customer2, null))
            {
                return true;
            }
            if (ReferenceEquals(customer1, null) || ReferenceEquals(customer2, null))
            {
                return false;
            }
            if (customer1.ID == customer2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }
        public static bool operator >(Customer customer1, Customer customer2)
        {
            if (customer1.ID == customer2.ID)
            {
                return false;
            }
            if (customer1.ID > customer2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Customer customer1, Customer customer2)
        {
            if (customer1.ID == customer2.ID)
            {
                return false;
            }
            if (customer1.ID > customer2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Customer customer1, Customer customer2)
        {

            if (customer1.ID >= customer2.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Customer customer1, Customer customer2)
        {
            if (customer1.ID >= customer2.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            Customer customer = obj as Customer;
            return this == customer;

        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}
