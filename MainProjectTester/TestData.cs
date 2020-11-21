using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Main_Project;
using Main_Project.Facade;
using Main_Project.Login;

namespace MainProjectTester
{
    public static class TestData
    {
        #region InstToken
        public static LoginToken<Admin> adminTokenNull;
        public static LoginToken<AirlineCompany> airlineCompanyTokenNull;
        public static LoginToken<Customer> customerTokenNull;
        public static LoginToken<Admin> adminToken = FlyingCenterSystem.mainAdmin;
        #endregion

        #region InstFacade
        public static AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
        public static LoggedInAdministratorFacade adminFacade = new LoggedInAdministratorFacade();
        public static LoggedInAirlineFacade loggedInAirlineFacade = new LoggedInAirlineFacade();
        public static LoggedInCustomerFacade lLoggedInCustomerFacade = new LoggedInCustomerFacade();
        #endregion

        #region ComperPoco
        public static void CompareAssertUserRole(UserRole userRole1, UserRole userRole2)
        {
            Assert.AreEqual(userRole1.UserRoleOf, userRole2.UserRoleOf);
        }
        public static void CompareAssertUserRepository(UserRepository userRepository1, UserRepository userRepository2)
        {
            Assert.AreEqual(userRepository1.UserName, userRepository2.UserName);
            Assert.AreEqual(userRepository1.Password, userRepository2.Password);
        }
        public static void CompareAssertCountry(Country country1, Country country2)
        {
            Assert.AreEqual(country1.CountryName, country2.CountryName);
        }
        public static void CompareAssertAdmins(Admin admin1, Admin admin2)
        {
            Assert.AreEqual(admin1.FirstName, admin2.FirstName);
            Assert.AreEqual(admin1.LastName, admin2.LastName);
        }
        public static void CompareAssertAirlineCompany(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            Assert.AreEqual(airlineCompany1.AirlineName, airlineCompany2.AirlineName);
        }
        public static void CompareAssertCustomer(Customer Customer1, Customer Customer2)
        {
            Assert.AreEqual(Customer1.FirstName, Customer2.FirstName);
            Assert.AreEqual(Customer1.LastName, Customer1.LastName);
            Assert.AreEqual(Customer1.Address, Customer2.Address);
            Assert.AreEqual(Customer1.phoneNumber, Customer2.phoneNumber);
            Assert.AreEqual(Customer1.CreditCard, Customer1.CreditCard);
        }
        public static void CompareAssertFlight(Flight flight1, Flight flight2)
        {
            Assert.AreEqual(flight1.RemainingTickets, flight2.RemainingTickets);
            Assert.AreEqual(flight1.LandingTime, flight2.LandingTime);
            Assert.AreEqual(flight1.DepartureTime, flight2.DepartureTime);
        }
        public static void CompareAssertTicket(Ticket ticket1, Ticket ticket2)
        {

        }
        #endregion
    }
}
