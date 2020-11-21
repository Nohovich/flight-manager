using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Main_Project;
using Main_Project.Exceptions;
using Main_Project.Facade;
using Main_Project.Login;
using Main_Project.POCO;
using MainProjectTester.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainProjectTester
{
   
    [TestClass]
    public class AirlineFacadeTest : BaseTest
    {
        #region Create flight
        [TestMethod]
        public void CreateFlight()
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
            Flight flight1 = myFacadeAir.GetFlightById(flight.ID);
            Assert.AreEqual(flight.ID, flight1.ID);

        }
        #endregion

        #region Cancel flight
        [TestMethod]
        public void CancelFlight() 
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
            myFacadeAir.CancelFlight(myTokenair, flight);
           Flight flight1 = myFacadeAir.GetFlightById(flight.ID);
            Assert.IsNull(flight1);

        }
        #endregion

        #region  Change my password
        [TestMethod]
        public void ChangeMyPassword()
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
            string pass = airlineTestUr.Password;
            myFacadeAir.ChangeMyPassword(myTokenair, "asd");
            airlineTestUr = myFacade.GetUserRepositoryDetails(myToken, airlineTestUr);
            Assert.AreNotEqual(airlineTestUr.Password, pass);
        }
        #endregion

        #region Get all flights
        [TestMethod]
        public void GetAllFlights()
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
            List<Flight> flights = (List<Flight>)myFacadeAir.GetAllFlightsOfAnAirlineCompany(myTokenair);
            Assert.IsTrue(flights.Count> 0);
        }
        #endregion

        #region Get all tickets
        [TestMethod]
        public void GetAllTickets()
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
            List<Ticket> tickets = (List<Ticket>)myFacadeAir.GetAllTickets(myTokenair);
            Assert.IsTrue(tickets.Count > 0);
        }
        #endregion

        #region Modify airline details
        [TestMethod]
        public void ModifyAirlineDetails()
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
            airlineCompany.AirlineName = "Yes";
            myFacadeAir.ModifyAirlineDetails(myTokenair, airlineCompany);
            AirlineCompany airlineCompany1 = myFacade.GetAirlineCompanyByid(myToken, airlineCompany.ID.ToString());
            Assert.AreEqual(airlineCompany.AirlineName, airlineCompany1.AirlineName);
        }
        #endregion

        #region  Update flight
        [TestMethod]
        public void UpdateFlight()
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
            Flight flight1 = myFacadeAir.GetFlightById(flight.ID);
            flight.DepartureTime = DateTime.Now.AddDays(11);
            myFacadeAir.UpdateFlight(myTokenair, flight);
            Assert.AreNotEqual(flight.DepartureTime, flight1.DepartureTime);
        }
        #endregion
    }

}
