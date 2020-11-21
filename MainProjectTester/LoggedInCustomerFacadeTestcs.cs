using System;
using System.Collections.Generic;
using Main_Project;
using Main_Project.Facade;
using Main_Project.Login;
using Main_Project.POCO;
using MainProjectTester.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainProjectTester
{
    [TestClass]
    public class LoggedInCustomerFacadeTestcs : BaseTest
    {
        #region Purchase ticket
        /// <summary>
        /// Customer buy a ticket
        /// </summary>
        [TestMethod]
        public void PurchaseTicket()
        {
            UserRepository testUr = new UserRepository("dad", "aes", RolesEnum.admin);
            Admin testAdmin = new Admin("dav,", "id", 3);
            FlyingCenterSystem.GetInstance().TryLogin(ur.UserName, ur.Password, out ILogin token,
                out FacadeBase facade);
            LoginToken<Admin> myToken = token as LoginToken<Admin>;
            LoggedInAdministratorFacade myFacade = facade as LoggedInAdministratorFacade;
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
            Country country = new Country("Israel");
            myFacade.CreateNewCountry(myToken, country);
            AirlineCompany airlineCompany = new AirlineCompany("ElALL", 1, country.ID);
            UserRepository airlineTestUr = new UserRepository("radt", "ass", RolesEnum.airline);
            myFacade.CreateNewAirline(myToken, airlineTestUr, airlineCompany, country);
            Flight flight = new Flight(DateTime.Now, DateTime.Now, 50, airlineCompany.ID, country.ID, country.ID);
            FlyingCenterSystem.GetInstance().TryLogin(airlineTestUr.UserName, airlineTestUr.Password, out ILogin tokenAir,
                out FacadeBase facadeAir);
            LoginToken<AirlineCompany> myTokenair = tokenAir as LoginToken<AirlineCompany>;
            LoggedInAirlineFacade myFacadeAir = facadeAir as LoggedInAirlineFacade;
            myFacadeAir.CreateFlight(myTokenair, flight);
            AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
            Customer customer = new Customer("asd", "fgh", "adthv", "0506794532", "123479520589243", 2);
            UserRepository customerUserRepository = new UserRepository("rad", "ass", RolesEnum.customer);
            anonymousUserFacade.CreateCustomerAndUserRepository(customerUserRepository, customer);
            FlyingCenterSystem.GetInstance().TryLogin(customerUserRepository.UserName, customerUserRepository.Password, out ILogin tokenCustomer,
              out FacadeBase facadeCustomer);
            LoginToken<Customer> myTokencustomer = tokenCustomer as LoginToken<Customer>;
            LoggedInCustomerFacade loggedInCustomerFacade = facadeCustomer as LoggedInCustomerFacade;
            loggedInCustomerFacade.PurchaseTicket(myTokencustomer, flight);
            List<Flight> flights = (List<Flight>) loggedInCustomerFacade.GetAllMyFlights(myTokencustomer);
            Assert.IsNotNull(flights);

        }
        #endregion;

        #region Cancel ticket help
        /// <summary>
        /// Remove a ticket of the customer
        /// </summary>
 
        #endregion

        #region Get all my flights
        /// <summary>
        /// Get all the customer flights
        /// </summary>
        [TestMethod]
        public void GetAllMyFlights()
        {
            UserRepository testUr = new UserRepository("dad", "aes", RolesEnum.admin);
            Admin testAdmin = new Admin("dav,", "id", 3);
            FlyingCenterSystem.GetInstance().TryLogin(ur.UserName, ur.Password, out ILogin token,
                out FacadeBase facade);
            LoginToken<Admin> myToken = token as LoginToken<Admin>;
            LoggedInAdministratorFacade myFacade = facade as LoggedInAdministratorFacade;
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
            Country country = new Country("Israel");
            myFacade.CreateNewCountry(myToken, country);
            AirlineCompany airlineCompany = new AirlineCompany("ElALL", 1, country.ID);
            UserRepository airlineTestUr = new UserRepository("rad", "ass", RolesEnum.airline);
            myFacade.CreateNewAirline(myToken, airlineTestUr, airlineCompany, country);
            Flight flight = new Flight(DateTime.Now, DateTime.Now, 50, airlineCompany.ID, country.ID, country.ID);
            FlyingCenterSystem.GetInstance().TryLogin(airlineTestUr.UserName, airlineTestUr.Password, out ILogin tokenAir,
                out FacadeBase facadeAir);
            LoginToken<AirlineCompany> myTokenair = tokenAir as LoginToken<AirlineCompany>;
            LoggedInAirlineFacade myFacadeAir = facadeAir as LoggedInAirlineFacade;
            myFacadeAir.CreateFlight(myTokenair, flight);
            AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
            Customer customer = new Customer("asdjkb", "fgh", "adthv", "0506794532", "123479520589243", 2);
            UserRepository customerUserRepository = new UserRepository("radp", "ass", RolesEnum.customer);
            anonymousUserFacade.CreateCustomerAndUserRepository(customerUserRepository, customer);
            FlyingCenterSystem.GetInstance().TryLogin(customerUserRepository.UserName, customerUserRepository.Password, out ILogin tokenCustomer,
              out FacadeBase facadeCustomer);
            LoginToken<Customer> myTokencustomer = tokenCustomer as LoginToken<Customer>;
            LoggedInCustomerFacade loggedInCustomerFacade = facadeCustomer as LoggedInCustomerFacade;
            loggedInCustomerFacade.PurchaseTicket(myTokencustomer, flight);
            List<Flight> flights = (List<Flight>)loggedInCustomerFacade.GetAllMyFlights(myTokencustomer);
            Assert.IsNotNull(flights);
        }
        #endregion

    }
}
