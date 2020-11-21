using Main_Project;
using Main_Project.Exceptions;
using Main_Project.Facade;
using Main_Project.Login;
using Main_Project.POCO;
using Main_project_WEB_API.BasicAuthentication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Main_project_WEB_API.Controllers
{
    [AdminBasicAuthentication]
    public class AdministratorFacdeController : ApiController
    {
    
        private void GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token)
        {
            Request.Properties.TryGetValue("adminLoginToken", out object adminLoginToken);
            token = adminLoginToken as LoginToken<Admin>;
            Request.Properties.TryGetValue("adminFacade", out object adminFacade);
            facade = adminFacade as LoggedInAdministratorFacade;
        }

        #region Create new admin API
        /// <summary>
        ///  Creat new admin
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Admin))]
        [HttpPost]
        [Route("api/administrator/createnewadmin")]
        public IHttpActionResult CreateNewAdmin([FromBody] JObject jsonDataForAdmin)
        {
            string userName = jsonDataForAdmin["userName"].ToString();
            string password = jsonDataForAdmin["password"].ToString();
            string firstName = jsonDataForAdmin["firstName"].ToString();
            string lastName = jsonDataForAdmin["lastName"].ToString();

            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);
            UserRepository userRepository = new UserRepository(userName, password, RolesEnum.admin);
            Admin admin = new Admin(firstName, lastName, 1);
           
            try
            {
                facade.CreateNewAdmin(token, userRepository, admin);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (UserNameAlreadyExistsException nameAlreadyExists)
            {
                return BadRequest($"Error: {nameAlreadyExists}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }

            return CreatedAtRoute("getadminbyid", new { adminID = admin.ID }, admin);
        }
        #endregion

        #region Create new country API
        /// <summary>
        /// add a new country to the data base
        /// </summary>
        [ResponseType(typeof(Country))]
        [HttpPost]
        [Route("api/administrator/createnewcountry")]
        public IHttpActionResult CreateNewCountry([FromBody] JObject jsonDataForCountry)
        {
            string countryName = jsonDataForCountry["countryName"].ToString();

            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            Country country = new Country(countryName);

            try
            {
                facade.CreateNewCountry(token, country);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (UserNameAlreadyExistsException nameAlreadyExists)
            {
                return BadRequest($"Error: {nameAlreadyExists}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
                return CreatedAtRoute("getcountrybyid", new { countryID = country.ID }, country);
        }
        #endregion

        #region Create new airline API
        /// <summary>
        /// Create a new airline company
        /// </summary>
        [ResponseType(typeof(AirlineCompany))]
        [HttpPost]
        [Route("api/administrator/createnewairline")]
        public IHttpActionResult CreateNewAirline([FromBody] JObject jsonDataForAirline)
        {
            string userName = jsonDataForAirline["userName"].ToString();
            string password = jsonDataForAirline["password"].ToString();
            string airlineName = jsonDataForAirline["airlineName"].ToString();
            long countrCode = (long)jsonDataForAirline["countrCode"];
            string countryName = jsonDataForAirline["countryName"].ToString();

            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            AirlineCompany airlineCompany = new AirlineCompany(airlineName, 1, countrCode);
            UserRepository userRepository = new UserRepository(userName, password, RolesEnum.airline);
            Country country = new Country(countryName);
            
            try
            {
                facade.CreateNewAirline(token, userRepository, airlineCompany, country);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (UserNameAlreadyExistsException nameAlreadyExists)
            {
                return BadRequest($"Error: {nameAlreadyExists}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return CreatedAtRoute("getairlinecompanybyid", new { airlineCompanyID = airlineCompany.ID }, airlineCompany);
        }
        #endregion

        #region Get customer by ID API
        /// <summary>
        ///  Get customer by ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        [HttpGet]
        [Route("api/administrator/getcustomerbyid/{customerID}", Name = "getcustomerbyid")]
        public IHttpActionResult GetCustomerByid([FromUri]int customerID)
        {
            Customer customer;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                 customer = facade.GetCustomerByid(token, customerID.ToString());
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFound)
            {
                return BadRequest($"Error: {dataNotFound}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(customer);
        }
        #endregion

        #region Get country by ID API
        /// <summary>
        ///  Get country by ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Country))]
        [HttpGet]
        [Route("api/administrator/getcountrybyid/{countryID}", Name = "getcountrybyid")]
        public IHttpActionResult GetCountryByID([FromUriAttribute] int countryID)
        {
            Country country;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                 country = facade.GetCountryByID(token, countryID.ToString());
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFound)
            {
                return BadRequest($"Error: {dataNotFound}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(country);
        }
        #endregion

        #region Get admin by ID API
        /// <summary>
        ///  Get admin by ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Admin))]
        [HttpGet]
        [Route("api/administrator/getadminbyid/{adminID}", Name = "getadminbyid")]
        public IHttpActionResult GetAdminById([FromUri] int adminID)
        {
            Admin admin;

            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            //UserRepository userRepository = new UserRepository("ticklishwolf360", "yeah", RolesEnum.admin);
            //Admin admin1 = new Admin("","", 11386);
            //LoginToken<Admin> token = userRepository, admin1;

            //FlyingCenterSystem.GetInstance().TryLogin("ticklishwolf360", "yeah", out ILogin token, out FacadeBase facade);

            //LoginToken<Admin> adminLoginToken = token as LoginToken<Admin>;
            //LoggedInAdministratorFacade adminFacade = facade as LoggedInAdministratorFacade;

            try
            {
                 admin = facade.GetAdminByid(token, adminID.ToString());
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFound)
            {
                return BadRequest($"Error: {dataNotFound}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(admin);
        }
        #endregion

        #region Get airlineCompany by ID API
        /// <summary>
        ///  Get airlineCompany by ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [HttpGet]
        [Route("api/administrator/getairlinecompanybyid/{airlineCompanyID}", Name = "getairlinecompanybyid")]
        public IHttpActionResult GetAirlineCompanyByid([FromUri] string airlineCompanyID)
        {
            AirlineCompany airlineCompany;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                 airlineCompany = facade.GetAirlineCompanyByid(token, airlineCompanyID);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFound)
            {
                return BadRequest($"Error: {dataNotFound}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(airlineCompany);
        }
        #endregion

        #region Get all countries API
        /// <summary>
        /// get all countries
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Country))]
        [HttpGet]
        [Route("api/administrator/getallcountries")]
        public IHttpActionResult GetAllCountries()
        {
            IList<Country> countries;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                countries = facade.GetAllCountries(token);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(countries);
        }
        #endregion

        #region Get all administrator in the date base API
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>v
        /// <returns></returns>
        [ResponseType(typeof(Country))]
        [HttpGet]
        [Route("api/administrator/getadmins")]
        public IHttpActionResult GetAdmins()
        {
            IList<Admin> admins;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                admins = facade.GetAdmins(token);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(admins);
        }
        #endregion

        #region Get allAirline companies of a country API
        /// <summary>
        /// Get allAirline companies of a country
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [HttpGet]
        [Route("api/administrator/getallcirlinecompaniesofacountry/{countryID}")]
        public IHttpActionResult GetAllAirlineCompaniesOfACountry([FromUriAttribute] long countryID)
        {
            IList<AirlineCompany> airlineCompaniesOfACountry;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                 airlineCompaniesOfACountry = facade.GetAllAirlineCompaniesOfACountry(token, countryID);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(airlineCompaniesOfACountry);
        }
        #endregion

        #region Get all allAirline companies API
        /// <summary>
        ///  Get all allAirline companies
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [HttpGet]
        [Route("api/administrator/getallairlinecompanies")]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlineCompanies;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                airlineCompanies = facade.GetAllAirlineCompanies(token);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(airlineCompanies);
        }
        #endregion

        #region  Get userRepository API
        /// <summary>
        ///  Get userRepository
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(UserRepository))]
        [HttpGet]
        [Route("api/administrator/getuserrepositorydetails")]
        public IHttpActionResult GetUserRepositoryDetails([FromBody] UserRepository userRepository)
        {
            UserRepository userRepository1;
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            userRepository1 = facade.GetUserRepositoryDetails(token, userRepository);
            try
            {
                userRepository1 = facade.GetUserRepositoryDetails(token, userRepository);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok(userRepository1);
        }
        #endregion

        #region   Update airlineCompany API
        /// <summary>
        ///  Update airlineCompany
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [HttpPut]
        [Route("api/administrator/updateairlinedetails")]
        public IHttpActionResult UpdateAirlineDetails([FromBody] AirlineCompany airlineCompany)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.UpdateAirlineDetails(token, airlineCompany);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion

        #region   Update admin API
        /// <summary>
        ///  Update admin
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Admin))]
        [HttpPut]
        [Route("api/administrator/up")]
        public IHttpActionResult UpdateAdminDetails([FromBody] Admin admin)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.UpdateAdminDetails(token, admin);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion

        #region   Update customer API
        /// <summary>
        ///  Update customer
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        [HttpPut]
        [Route("api/administrator/updatecustomerdetails")]
        public IHttpActionResult UpdateCustomerDetails([FromBody] Customer customer)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.UpdateCustomerDetails(token, customer);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion

        #region  Update userRepository API
        /// <summary>
        ///  Update userRepository
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(UserRepository))]
        [HttpPut]
        [Route("api/administrator/updateuserrepositorydetails")]
        public IHttpActionResult UpdateUserRepositoryDetails([FromBody] UserRepository userRepository)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.UpdateUserRepositoryDetails(token, userRepository);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion

        #region Remove admin by its userRepository ID API
        /// <summary>
        ///  Remove admin by its userRepository ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Admin))]
        [HttpDelete]
        [Route("api/administrator/removeadmin/{userRepositoryID}")]
        public IHttpActionResult RemoveAdmin([FromUri] int userRepositoryID)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.RemoveAdmin(token, userRepositoryID.ToString());
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion

        #region Remove airlineCompany by its userRepository ID API
        /// <summary>
        ///  Remove airlineCompany by its userRepository ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(AirlineCompany))]
        [HttpDelete]
        [Route("api/administrator/removeairlinecompany/{userRepositoryID}")]
        public IHttpActionResult RemoveAirline([FromUri] string userRepositoryID)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.RemoveAirline(token, userRepositoryID);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion

        #region Remove customer by its userRepository ID API
        /// <summary>
        ///  Remove customer by its userRepository ID
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        [HttpDelete]
        [Route("api/administrator/removecustomer/{userRepositoryID}")]
        public IHttpActionResult RemoveCustomer([FromUri] string userRepositoryID)
        {
            GetLoginData(out LoggedInAdministratorFacade facade, out LoginToken<Admin> token);

            try
            {
                facade.RemoveCustomer(token, userRepositoryID);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return Ok();
        }
        #endregion
    }
}
 