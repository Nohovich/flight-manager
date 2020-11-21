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
    public class AnonymousUserFacadeTest : BaseTest
    {
        #region Get all airline companies
        /// <summary>
        /// get all airline companies in the data base
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void GetAllAirlineCompanies()
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
            AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
            List<AirlineCompany> airlineCompanies = (List<AirlineCompany>)anonymousUserFacade.GetAllAirlineCompanies();
            Assert.IsNotNull(airlineCompanies);
        }
        #endregion

        #region Get all flights
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
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
            AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
            List<Flight> flights = (List<Flight>)anonymousUserFacade.GetAllFlights();
            Assert.IsNotNull(flights);
        }
        #endregion

        #region Get all flights that isn't fully booked
        /// <summary>
        /// Get all flights that isn't full booked from the data base
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void GetAllFlightsThatIsentFullyBooked()
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
            Dictionary<Flight, int> flights = (Dictionary<Flight, int>)anonymousUserFacade.GetAllFlightsVacancy();
            Assert.IsNotNull(flights);
        }
        #endregion

        #region get flight
        /// <summary>
        /// get flight by flight id in the data base
        /// </summary>
        [TestMethod]
        public void GetFlight()
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
            Flight flight1 = anonymousUserFacade.GetFlightById(flight.ID);
            Assert.IsNotNull(flight1);
        }
        #endregion

        #region get flights by departure date
        /// <summary>
        /// get flights by departure date from the data base
        /// </summary>
        [TestMethod]
        public void GetAllFlightsByDepartureDate()
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
            List<Flight> flights = (List<Flight>)anonymousUserFacade.GetFlightsByDepatrureDate(DateTime.Now);
            Assert.IsNotNull(flights);
        }
        #endregion

        #region get flights by destination country
        /// <summary>
        /// get flights by destination country from the data base
        /// </summary>
        [TestMethod]
        public void GetAllFlightsByDestinationcountry()
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
            List<Flight> flights = (List<Flight>)anonymousUserFacade.GetFlightsByDestinationCountry(country.ID);
            Assert.IsNotNull(flights);
        }
        #endregion

        #region Get flights by landing time
        /// <summary>
        /// Get flights by landing time from the data base
        /// </summary>
        [TestMethod]
        public void GetAllFlightsByLandingTime()
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
            List<Flight> flights = (List<Flight>)anonymousUserFacade.GetFlightsByLandingDate(DateTime.Now);
            Assert.IsNotNull(flights);
        }
            #endregion

        #region Get flights by origin country
            /// <summary>
            /// Get flights by origin country from the data base
            /// </summary>
        [TestMethod]
        public void GetAllFlightsByOriginCountry()
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
            List<Flight> flights = (List<Flight>)anonymousUserFacade.GetFlightsByOriginCountry(country.ID);
            Assert.IsNotNull(flights);
        }
            #endregion

        #region Create a customer and a userRepository for that customer
        /// <summary>
        ///  Create a customer and a userRepository for that customer and add them to the data base
        /// </summary>
        [TestMethod]
        public void CreateACustomerAndAUserRepositoryForThatCustomer()
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
            AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();
            Customer customer = new Customer("asd","fgh","adthv","0506794532","123479520589243",2);
            UserRepository customerUserRepository = new UserRepository("rad", "ass", RolesEnum.customer);
            anonymousUserFacade.CreateCustomerAndUserRepository(customerUserRepository, customer);
            Customer customer1 = myFacade.GetCustomerByid(myToken, customer.ID.ToString());
            Assert.AreEqual(customer.ID, customer1.ID);
        }
            #endregion

        
    }
}