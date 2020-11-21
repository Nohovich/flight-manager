using Main_Project.Exceptions;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    public class WpfFacade : FacadeBase, IWpfFacade
    {
        // tip: consider adapter pattern (or proxy + adapter)
        #region Create new admin
        /// <summary>
        /// Creating a new admin
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="admin"></param>
        public void CreateNewAdmin(UserRepository userRepository, Admin admin)
        {
            var userRepository_exist = _userRepositoryDAO.GetUserByUserName(userRepository.UserName);
            if (userRepository_exist == null)
            {
                _userRepositoryDAO.CreateAdminIndUserRepository(userRepository);
                admin.UserRepositoryID = userRepository.ID;
                _adminDAO.Add(admin);
            }
            else
            {
                throw new UserNameAlreadyExistsException($"UserName: {userRepository_exist.UserName} Already exists");
            }
        }
        #endregion

        #region Create new country
        /// <summary>
        /// add a new country to the data base
        /// </summary>
        /// <param name="country"></param>
        public void CreateNewCountry(Country country)
        {
            Country country1 = _countryDAO.GetCountryByName(country.CountryName);
            if (country1 == null)
            {
                _countryDAO.Add(country);
            }
            else
            {
                throw new UserNameAlreadyExistsException($"Country: {country.CountryName} Already exists");
            }
        }
        #endregion

        #region Get all customers in the date base
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAllCustomers()
        {
            return _customerDAO.GetAll();
        }
        #endregion

        #region Get all countries in the date base
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Country> GetAllCountries()
        {
                return _countryDAO.GetAll();
        }
        #endregion

        #region Create new airline
        /// <summary>
        /// Create a new airline company
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="airlineCompany"></param>
        /// <param name="country"></param>
        public void CreateNewAirline(UserRepository userRepository, AirlineCompany airlineCompany, Country country)
        {

            country = _countryDAO.Get(country.ID);
            if (country != null)
            {
                UserRepository userRepository1 = _userRepositoryDAO.GetUserByUserName(userRepository.UserName);
                if (userRepository1 == null)
                {
                    _userRepositoryDAO.CreateAirlineIndUserRepository(userRepository);
                    airlineCompany.User_Repository_ID = userRepository.ID;
                    airlineCompany.CountryCode = country.ID;
                    _airlineDAO.Add(airlineCompany);
                }
                else
                {
                    throw new UserNameAlreadyExistsException($"UserName: {userRepository.UserName} Already exists");
                }
            }

        }
        #endregion

        #region Get all airline companies in database
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }
        #endregion

        #region Create flight
        /// <summary>
        /// Add a flight of airline company
        /// </summary>
        /// <param name="flight"></param>
        public void CreateFlight(AirlineCompany airlineCompany, Flight flight)
        {
            flight.AirlineCompanyID = airlineCompany.ID;
            _flightDAO.Add(flight);

        }
        #endregion

        #region Get all flights of an airline company
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        public IList<Flight> GetAllFlightsOfAnAirlineCompany(AirlineCompany airlineCompany)
        {
            {
                return _flightDAO.GetAllFlightsOfAnAirlineCompany(airlineCompany);
            }
        }
        #endregion

        #region Get all flights
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
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

        #region Purchase ticket
        /// <summary>
        /// Customer buy a ticket
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public void PurchaseTicket(Customer customer, Flight flight)
        {

            Flight flight1 = _flightDAO.Get(flight.ID);
            if (flight1 != null)
            {
                Ticket ticket = new Ticket(flight.ID, customer.ID);
                _ticketDAO.Add(ticket);
                _flightDAO.ReductionFlightTicket(flight.ID);
                return;

            }
            throw new DataNotFoundException($"there isn't an existing flight with this ID: {flight.ID} in the data base");
        }



    }
    #endregion
}

