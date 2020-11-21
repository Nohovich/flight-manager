using System;
using Main_Project.POCO;
using Main_Project.Exceptions;
using MainProjectTester.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Main_Project.Login;
using Main_Project;
using Main_Project.Facade;
using System.Collections.Generic;

namespace MainProjectTester
{
    [TestClass]
    public class AdministratorFacadeTest : BaseTest
    {
        #region Add admin and get it
        /// <summary>
        /// test adding a userRepository of a admin the data base, after that add the admin to the data base, then compare the two admins and see if there are same.
        /// </summary>
        [TestMethod]
        public void AddAdminAndGetIt()
        {
            UserRepository testUr = new UserRepository("dad", "aes", RolesEnum.admin);
            Admin testAdmin = new Admin("dav,", "id", 3);
            FlyingCenterSystem.GetInstance().TryLogin(ur.UserName, ur.Password, out ILogin token,
                out FacadeBase facade);
            LoginToken<Admin> myToken = token as LoginToken<Admin>;
            LoggedInAdministratorFacade myFacade = facade as LoggedInAdministratorFacade;
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
            Admin admin2 = myFacade.GetAdminByid(myToken, testAdmin.ID.ToString());
            Assert.AreEqual(admin2.ID, testAdmin.ID);



        }
        /// <summary>
        /// test if null token throw exception;
        /// </summary>
        [ExpectedException(typeof(TokenIsNullException))]
        [TestMethod]
        public void NullToken()
        {
            LoginToken<Admin> myToken = null;
            LoggedInAdministratorFacade myFacade = new LoggedInAdministratorFacade();
            myFacade.GetAdminByid(myToken, "3");
        }
        /// <summary>
        /// test if userName already exists throw exception;
        /// </summary>
        [ExpectedException(typeof(UserNameAlreadyExistsException))]
        [TestMethod]
        public void UserNameAlreadyExists()
        {
            UserRepository testUr = new UserRepository("dad", "aes", RolesEnum.admin);
            Admin testAdmin = new Admin("dav,", "id", 3);
            FlyingCenterSystem.GetInstance().TryLogin(ur.UserName, ur.Password, out ILogin token,
                out FacadeBase facade);
            LoginToken<Admin> myToken = token as LoginToken<Admin>;
            LoggedInAdministratorFacade myFacade = facade as LoggedInAdministratorFacade;
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
        }
        #endregion

        #region Add country and get it
        /// <summary>
        /// test adding a country the data base, then compare the two countries and see if there are same.
        /// </summary>
        [TestMethod]
        public void AddAnewCountryAndGetIt()
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
            Country testCounty = myFacade.GetCountryByID(myToken, country.ID.ToString());
            Assert.AreEqual(country.ID, testCounty.ID);

        }
        #endregion

        #region Add airline company and get it
        /// <summary>
        /// test adding a userRepository of a airline company to the data base, after that add the airline company to the data base, then compare the two airlines and see if there are same.
        /// </summary>
        [TestMethod]
        public void AddAirlineCompanyAndGetIt()
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
            AirlineCompany airlineCompany1 = myFacade.GetAirlineCompanyByid(myToken, airlineCompany.ID.ToString());
            Assert.AreEqual(airlineCompany.ID, airlineCompany1.ID);
        }
        #endregion

        #region Get all airline companies in database
        /// <summary>
        /// Get all airline companies
        /// </summary>
        /// <param name="token"></param>
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
            List<AirlineCompany> airlineCompanies = (List<AirlineCompany>)myFacade.GetAllAirlineCompanies();
            Assert.IsNotNull(airlineCompanies);
        }
        #endregion

        #region Get all airline companies in database of a country
        /// <summary>
        /// Get all airline companies of a country
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [TestMethod]
        public void GetAllAirlineCompaniesOfACountry()
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
            List<AirlineCompany> airlineCompanies = (List<AirlineCompany>)myFacade.GetAllAirlineCompaniesOfACountry(myToken,country.ID);
            Assert.IsNotNull(airlineCompanies);
        }
        #endregion

        #region Remove admin from the data base
        /// <summary>
        /// test remove admin and his userRepository from the data base
        /// </summary>
        [ExpectedException(typeof(DataNotFoundException))]
        [TestMethod]
        public void RemoveAdminAndHisUserRepository()
        {
            UserRepository testUr = new UserRepository("dad", "aes", RolesEnum.admin);
            Admin testAdmin = new Admin("dav,", "id", 3);
            FlyingCenterSystem.GetInstance().TryLogin(ur.UserName, ur.Password, out ILogin token,
                out FacadeBase facade);
            LoginToken<Admin> myToken = token as LoginToken<Admin>;
            LoggedInAdministratorFacade myFacade = facade as LoggedInAdministratorFacade;
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
            myFacade.RemoveAdmin(myToken, testAdmin.UserRepositoryID.ToString());
            Admin admin2 = myFacade.GetAdminByid(myToken, testAdmin.ID.ToString());
        }

        #endregion

        #region Remove airline company from the data base
        /// <summary>
        /// test remove airline company and its userRepository from the data base
        /// </summary>
        [ExpectedException(typeof(DataNotFoundException))]
        [TestMethod]
        public void RemoveAirlineCompanyAndItsUserRepository()
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
            myFacade.RemoveAirline(myToken, airlineCompany.User_Repository_ID.ToString());
            AirlineCompany airlineCompany1 = myFacade.GetAirlineCompanyByid(myToken, airlineCompany.ID.ToString());


        }

        #endregion

        #region Remove customer from the data base
        /// <summary>
        /// test remove customer and his userRepository from the data base
        /// </summary>
        [ExpectedException(typeof(DataNotFoundException))]
        [TestMethod]
        public void RemoveCustomerAndHisUserRepository()
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
            UserRepository customerTestUr = new UserRepository("David", "Noho", RolesEnum.customer);
            Customer customer = new Customer("David", "Noho", "Tel", "0506794632", "78458956852174587", 2);
            myFacade.CreateCustomerAndUserRepository(customerTestUr, customer);
            myFacade.RemoveCustomer(myToken, customer.User_Repository_ID.ToString());
            myFacade.GetCustomerByid(myToken, customer.ID.ToString());

        }

        #endregion

        #region Update admin
        /// <summary>
        /// test update admin values in the data base
        /// </summary>
        [TestMethod]
        public void UpdateAdmin()
        {
            UserRepository testUr = new UserRepository("dad", "aes", RolesEnum.admin);
            Admin testAdmin = new Admin("dav,", "id", 3);
            FlyingCenterSystem.GetInstance().TryLogin(ur.UserName, ur.Password, out ILogin token,
                out FacadeBase facade);
            LoginToken<Admin> myToken = token as LoginToken<Admin>;
            LoggedInAdministratorFacade myFacade = facade as LoggedInAdministratorFacade;
            myFacade.CreateNewAdmin(myToken, testUr, testAdmin);
            Admin admin2 = myFacade.GetAdminByid(myToken, testAdmin.ID.ToString());
            testAdmin.FirstName = "tts";
            myFacade.UpdateAdminDetails(myToken, testAdmin);
            Assert.AreNotEqual(admin2.FirstName, testAdmin.FirstName);
        }
        #endregion
    }
}
