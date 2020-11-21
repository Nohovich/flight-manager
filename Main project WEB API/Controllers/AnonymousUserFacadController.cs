using Main_Project.POCO;
using Main_Project.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using Main_Project;
using Main_Project.Exceptions;
using Main_project_WEB_API.Helper;
using System.Web.Http.Cors;

namespace Main_project_WEB_API.Controllers
{
    [EnableCors("*","*","*")]
    public class AnonymousUserFacadController : ApiController
    {
        AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();

        #region Get all countries API
        /// <summary>
        /// get all countries from the data base
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getallcountries")]
        public IHttpActionResult GetAllcountries()
        {
            IList<Country> countries = anonymousUserFacade.GetAllCountries();
            if (countries.Count == 0)
                return NotFound();
            return Ok(countries);
        }
        #endregion

        #region Get all airline companies API
        /// <summary>
        /// get all airline companies in the data base
        /// </summary>
        [ResponseType(typeof(AirlineCompany))]
        [HttpGet]
        [Route("api/anonymoususer/getallairlinecompanies")]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlineCompanies = anonymousUserFacade.GetAllAirlineCompanies();
            if (airlineCompanies.Count == 0)
                return NotFound();
            return Ok(airlineCompanies);
        }
        #endregion

        #region Get all flights API
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getallflights")]
        public IHttpActionResult GetAllFlights()
        {
            IList<FlightRazor> flights = anonymousUserFacade.RazorAllFlights();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion
        
        #region Get all flights that isn't full booked API
        /// <summary>
        /// Get all flights that isn't full booked from the data base
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getallflightsvacancy")]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = anonymousUserFacade.GetAllFlightsVacancy();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion

        #region get flight API 
        /// <summary>
        /// get flight by flight id in the data base
        /// </summary>
        /// <param name="flightID"></param>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getflightbyid/{flightID}")]
        public IHttpActionResult GetFlightById([FromUri]long flightID)
        {

            Flight flight = anonymousUserFacade.GetFlightById(flightID);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }
        #endregion

        #region get flights by departure date API
        /// <summary>
        /// get flights by departure date from the data base
        /// </summary>
        /// <param name="departureDate"></param>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getflightsbydepatruredate/{departureDate}")]
        public IHttpActionResult GetFlightsByDepatrureDate([FromUri]DateTime departureDate)
        {
            IList<Flight> flights = anonymousUserFacade.GetFlightsByDepatrureDate(departureDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion

        #region get flights by destination country API
        /// <summary>
        /// get flights by destination country from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getflightsbydestinationcountry/{countryCode}")]
        public IHttpActionResult GetFlightsByDestinationCountry([FromUri]long countryCode)
        {
            IList<Flight> flights = anonymousUserFacade.GetFlightsByDestinationCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion

        #region Get flights by landing time API
        /// <summary>
        /// Get flights by landing time from the data base
        /// </summary>
        /// <param name="landingDate"></param>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getflightsbylandingdate/{landingDate}")]
        public IHttpActionResult GetFlightsByLandingDate([FromUri]DateTime landingDate)
        {
            IList<Flight> flights = anonymousUserFacade.GetFlightsByLandingDate(landingDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion

        #region Get flights by origin country API
        /// <summary>
        /// Get flights by origin country from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/anonymoususer/getflightsbyorigincountry/{countryCode}")]
        public IHttpActionResult GetFlightsByOriginCountry([FromUri]long countryCode)
        {
            IList<Flight> flights = anonymousUserFacade.GetFlightsByOriginCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion

        #region Create a customer and a userRepository for that customer API
        /// <summary>
        ///  Create a customer and a userRepository for that customer and add them to the data base
        /// </summary>
        [ResponseType(typeof(Customer))]
        [HttpPost]
        [Route("api/anonymoususer/createcustomer")]
        public IHttpActionResult CreateCustomerAndUserRepository([FromBody] JObject jsonDataForCustomer)
        {
            string userName = jsonDataForCustomer["userName"].ToString();
            string password = jsonDataForCustomer["password"].ToString();
            string firstName = jsonDataForCustomer["firstName"].ToString();
            string lastName = jsonDataForCustomer["lastName"].ToString();
            string address = jsonDataForCustomer["address"].ToString();
            string phoneNumber = jsonDataForCustomer["phoneNumber"].ToString();
            string creditCard = jsonDataForCustomer["creditCard"].ToString();
            UserRepository userRepository = new UserRepository(userName, password, RolesEnum.customer);
            Customer customer = new Customer(firstName, lastName, address, phoneNumber, creditCard, 1);

            try
            {
                anonymousUserFacade.CreateCustomerAndUserRepository(userRepository, customer);
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
            return CreatedAtRoute("getcustomerbyid", new { customerID = customer.ID }, customer);
        }
        #endregion

        #region Query search flights by origin country and its destination country APi
        /// <summary>
        /// help
        ///   query string
        /// </summary>
        [HttpGet]
        [Route("api/anonymoususer/vanillasearchflights")]
        public IHttpActionResult searchFlights([FromUri] long from = 0, [FromUri] long to = 0)
        {

            if (from != 0 && to != 0)
            {
                IList<FlightRazor> flights = anonymousUserFacade.GetFlightsByOriginCountryAndDestination(from,to);
                    return Ok(flights);
            }
            return Content(HttpStatusCode.BadRequest,"You Have To Send Origin AND Destination Country");
        }
        #endregion 

    }
}
