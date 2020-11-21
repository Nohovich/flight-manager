using Main_Project.Exceptions;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        #region Get all airline companies
        /// <summary>
        /// get all airline companies in the data base
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }
        #endregion

        #region Get all flights
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }
        #endregion

        #region Get all razor flights
        /// <summary>
        /// Get all the razor flights
        /// </summary>
        /// <returns></returns>
        public IList<FlightRazor> RazorAllFlights()
        {

            return _flightDAO.RazorAllFlights();
        }
        #endregion

        #region Get all flights that will land in the upcoming 12 hours or landed in the last 4 hours
        /// <summary>
        /// Get all flights that will land in the upcoming 12 hours or landed in the last 4 hours
        /// </summary>
        /// <returns></returns>
        public IList<FlightRazor> LandingFlights()
        {
            return _flightDAO.LandingFlights();
        }
        #endregion

        #region Get all flights that will departure in the upcoming 12
        /// <summary>
        /// Get all flights that will land in the upcoming 12 hours or landed in the last 4 hours
        /// </summary>
        /// <returns></returns>
        public IList<FlightRazor> DeparturesFlights()
        {
            return _flightDAO.DeparturesFlights();
        }
        #endregion

        #region Get all flights that isn't full booked
        /// <summary>
        /// Get all flights that isn't full booked from the data base
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }
        #endregion

        #region get flight
        /// <summary>
        /// get flight by flight id in the data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight GetFlightById(long id)
        {
            return _flightDAO.Get(id);
        }
        #endregion

        #region get flights by departure date
        /// <summary>
        /// get flights by departure date from the data base
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepatrureDate(departureDate);
        }
        #endregion

        #region get flights by destination country
        /// <summary>
        /// get flights by destination country from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }
        #endregion

        #region Get flights by landing time
        /// <summary>
        /// Get flights by landing time from the data base
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }
        #endregion

        #region Get flights by origin country
        /// <summary>
        /// Get flights by origin country from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(countryCode);
        }
        #endregion

        #region Get flights by origin country and destination
        /// <summary>
        /// Get flights by origin country and destination from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<FlightRazor> GetFlightsByOriginCountryAndDestination(long origin, long destination)
        {
            return _flightDAO.GetFlightsByOriginCountryAndDestination(origin, destination);
        }
        #endregion

        #region Get all countries in the database
        /// <summary>
        /// Get all countries in the database
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAllCountries()
        {
            return _countryDAO.GetAll();
        }
        #endregion

        #region Create a customer and a userRepository for that customer
        /// <summary>
        ///  Create a customer and a userRepository for that customer and add them to the data base
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="customer"></param>
        public void CreateCustomerAndUserRepository(UserRepository userRepository, Customer customer)
        {
                UserRepository userRepository1 = _userRepositoryDAO.GetUserByUserName(userRepository.UserName);
            if (userRepository1 == null)
            {
                _userRepositoryDAO.CreateCustomerInUserRepository(userRepository);
                customer.User_Repository_ID = userRepository.ID;
                _customerDAO.Add(customer);
            }
            else
            {
                throw new UserNameAlreadyExistsException($"UserName: {userRepository.UserName} Already exists");
            }
        }
        #endregion

        public IList<FlightRazor> FilterFlights(string filter = "", string flightType = "", string value = "")
        {
            IList<FlightRazor> flights = RazorAllFlights().ToList();
            switch (filter)
            {
                case "CompanyName":
                    flights = flights.Where(f => f.AirlineName == value).ToList();
                    break;
                case "DestinationCountry":
                    flights = flights.Where(f => f.DestinationCountry == value).ToList();
                    break;
                case "OriginCountry":
                    flights = flights.Where(f => f.OriginCountry == value).ToList();
                    break;
                case "FlightNumber":
                    flights = flights.Where(f => f.ID == Convert.ToInt32(value)).ToList();
                    break;
                default:
                    break;
            }
            if (flightType == "Landings")
                flights = flights.Where(f => f.LandingTime.Subtract(DateTime.Now).Hours > 12).ToList();
            else if (flightType == "Departures")
                flights = flights.Where(f => f.DepartureTime < DateTime.Now).ToList();

            return flights;
        }
    }
}
