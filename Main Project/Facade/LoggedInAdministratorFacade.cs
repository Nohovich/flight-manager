using Main_Project.Exceptions;
using Main_Project.Login;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        #region Get all countries in the date base
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Country> GetAllCountries(LoginToken<Admin> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _countryDAO.GetAll();
            }
            else throw new TokenIsNullException($"Token ID: {token.TokenUserRepository.ID}");

        }
        #endregion

        #region Get all administrator in the date base
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Admin> GetAdmins(LoginToken<Admin> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _adminDAO.GetAll();
            }
            else throw new TokenIsNullException($"Token ID: {token.TokenUserRepository.ID}");

        }
        #endregion

        #region Get all airline companies of a country
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompaniesOfACountry(LoginToken<Admin> token,long countryID)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _airlineDAO.GetAllAirlinesByCountry(countryID);
            }
            else throw new TokenIsNullException($"Token ID: {token.TokenUserRepository.ID}");

        }
        #endregion

        #region Get all airline companies in database
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<AirlineCompany> GetAllAirlineCompanies(LoginToken<Admin> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _airlineDAO.GetAll();
            }
            else throw new TokenIsNullException($"Token ID: {token.TokenUserRepository.ID}");

        }
        #endregion

        #region Transfer tickets that have landed
        public void UpdateHistoryInfo()
        {
             _ticketDAO.UpdateTicketsHistoryThatHaveLanded();
            _flightDAO.UpdateFlightsHistoryThatHaveLanded();
        }

        #endregion

        #region Create new country
        /// <summary>
        /// add a new country to the data base
        /// </summary>
        /// <param name="token"></param>
        /// <param name="country"></param>
        public void CreateNewCountry(LoginToken<Admin> token, Country country)
        {
            if (CheckIfTokenIsValid(token))
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
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Create new admin
        /// <summary>
        /// Creating a new admin
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        /// <param name="admin"></param>
        public void CreateNewAdmin(LoginToken<Admin> token, UserRepository userRepository, Admin admin)
        {
            if (CheckIfTokenIsValid(token))
            {
                var  userRepository_exist = _userRepositoryDAO.GetUserByUserName(userRepository.UserName);
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
              else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Create new airline
        /// <summary>
        /// Create a new airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        /// <param name="airlineCompany"></param>
        /// <param name="country"></param>
        public void CreateNewAirline(LoginToken<Admin> token, UserRepository userRepository, AirlineCompany airlineCompany, Country country)
        {
            if (CheckIfTokenIsValid(token))
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
                    else throw new Exception($"The system does not know the CountryCod: {country.ID} you have entered");
            }
             else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Get admin by id
        /// <summary>
        /// get an admin by his id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Admin GetAdminByid(LoginToken<Admin> token, string ID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryID;
                bool trynum = long.TryParse(ID, out tryID);
                if (trynum)
                {
                    Admin admin = _adminDAO.Get(tryID);
                    if(admin!= null)
                    {
                        return admin;
                    }
                    throw new DataNotFoundException($"there isn't an existing admin with this {tryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {ID} to a number");
                }
            }
               throw new TokenIsNullException($"Token ID: {token} is null");
        }
        #endregion

        #region Get country by id
        /// <summary>
        /// get an admin by his id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Country GetCountryByID(LoginToken<Admin> token, string ID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryID;
                bool trynum = long.TryParse(ID, out tryID);
                if (trynum)
                {
                    Country country = _countryDAO.Get(tryID);
                    if (country != null)
                    {
                        return country;
                    }
                    throw new DataNotFoundException($"there isn't an existing country with this {tryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {ID} to a number");
                }
            }
            throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Get airline company by id
        /// <summary>
        /// get an airline company by its id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineCompanyByid(LoginToken<Admin> token, string ID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryID;
                bool trynum = long.TryParse(ID, out tryID);
                if (trynum)
                {
                    AirlineCompany airlineCompany = _airlineDAO.Get(tryID);
                    if (airlineCompany != null)
                    {
                        return airlineCompany;
                    }
                    throw new DataNotFoundException($"there isn't an existing airlineCompany with this {tryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {ID} to a number");
                }
            }
            throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Get customer by its id
        /// <summary>
        /// get a customer by its id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer GetCustomerByid(LoginToken<Admin> token, string ID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryID;
                bool trynum = long.TryParse(ID, out tryID);
                if (trynum)
                {
                    Customer customer = _customerDAO.Get(tryID);
                    if (customer != null)
                    {
                        return customer;
                    }
                    throw new DataNotFoundException($"there isn't an existing customer with this {tryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {ID} to a number");
                }
            }
            throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Remove admin
        /// <summary>
        /// Remove an admin by his UserRepositoryID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository1"></param>
        public void RemoveAdmin(LoginToken<Admin> token, string UserRepositoryID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryUserRepositoryID;
                bool trynum = long.TryParse(UserRepositoryID, out tryUserRepositoryID);
                if (trynum)
                {
                    UserRepository userRepository = _userRepositoryDAO.Get(tryUserRepositoryID);
                    if (userRepository != null)
                    {
                        Admin admin = _adminDAO.GetAdminByUserRepositoryID(tryUserRepositoryID);
                        if (admin != null)
                        {
                            _adminDAO.Remove(admin);
                        }
                        else throw new DataNotFoundException($"there isn't an existing admin with this UserRepositoryID: {tryUserRepositoryID} in the data base");
                        _userRepositoryDAO.Remove(userRepository);
                    }
                    else throw new DataNotFoundException($"there isn't an existing UserRepository with this ID: {tryUserRepositoryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {UserRepositoryID} to a number");
                }
               
            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Remove airline
        /// <summary>
        /// Remove an airline company by its UserRepositoryID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="UserRepositoryID"></param>
        public void RemoveAirline(LoginToken<Admin> token, string airlineUserRepositoryID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryUserRepositoryID;
                bool trynum = long.TryParse(airlineUserRepositoryID, out tryUserRepositoryID);
                if (trynum)
                {
                    UserRepository userRepository = _userRepositoryDAO.Get(tryUserRepositoryID);
                    if (userRepository != null)
                    {
                        AirlineCompany airlineCompany = _airlineDAO.GetAirlineCompanyByUserRepositoryID(tryUserRepositoryID);
                        if (airlineCompany != null)
                        {
                            List<Flight> flights = (List<Flight>)_flightDAO.GetAllFlightsIdOfAnAirlineCompany(airlineCompany.ID);
                            if(flights.Count> 0)
                            {
                                foreach (var flight in flights)
                                {
                                    _ticketDAO.RemoveTicketsByFlightID(flight.ID);
                                }
                                _flightDAO.RemoveFlightsByAirlineCompanyID(tryUserRepositoryID);
                            }
                            _airlineDAO.Remove(airlineCompany);
                        }
                        else throw new DataNotFoundException($"there isn't an existing airlineCompany with this UserRepositoryID: {tryUserRepositoryID} in the data base");
                        _userRepositoryDAO.Remove(userRepository);
                    }
                    else throw new DataNotFoundException($"there isn't an existing UserRepository with this ID: {tryUserRepositoryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {airlineUserRepositoryID} to a number");
                }

            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Remove customer
        /// <summary>
        /// Remove a customer by his UserRepositoryID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="UserRepositoryID"></param>
        public void RemoveCustomer(LoginToken<Admin> token, string UserRepositoryID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryUserRepositoryID;
                bool trynum = long.TryParse(UserRepositoryID, out tryUserRepositoryID);
                if (trynum)
                {
                    UserRepository userRepository = _userRepositoryDAO.Get(tryUserRepositoryID);
                    if (userRepository != null)
                    {
                        Customer customer = _customerDAO.GetCustomerByUserRepositoryID(tryUserRepositoryID);
                        if (customer != null)
                        {
                            _customerDAO.Remove(customer);
                        }
                        else throw new DataNotFoundException($"there isn't an existing customer with this UserRepositoryID: {tryUserRepositoryID} in the data base");
                        _userRepositoryDAO.Remove(userRepository);
                    }
                    else throw new DataNotFoundException($"there isn't an existing UserRepository with this ID: {tryUserRepositoryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {UserRepositoryID} to a number");
                }
            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Update admin details
        /// <summary>
        /// Update an admin
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void UpdateAdminDetails(LoginToken<Admin> token, Admin admin)
        {
            if (CheckIfTokenIsValid(token))
            {
                Admin admin1 = _adminDAO.Get(admin.ID);
                if (admin1 != null)
                {
                    _adminDAO.Update(admin);
                }
                else throw new DataNotFoundException($"there isn't an existing admin with this ID: {admin.ID} in the data base");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Update airline details
        /// <summary>
        /// Update an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany airline)
        {
            if (CheckIfTokenIsValid(token))
            {
                AirlineCompany airlineCompany = _airlineDAO.Get(airline.ID);
                if (airlineCompany != null)
                {
                    _airlineDAO.Update(airline);
                }
                else throw new DataNotFoundException($"there isn't an existing airlineCompany with this ID: {airline.ID} in the data base");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Update customer details
        /// <summary>
        /// Update an customer with his ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer)
        {
            if (CheckIfTokenIsValid(token))
            {
                Customer customer1 = _customerDAO.Get(customer.ID);
                if (customer1 != null)
                {
                    _customerDAO.Update(customer);
                }
                else throw new DataNotFoundException($"there isn't an existing customer with this ID: {customer.ID} in the data base");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Get userRepository details
        /// <summary>
        /// Get an userRepository by its ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        /// <returns></returns>
        public UserRepository GetUserRepositoryDetails(LoginToken<Admin> token, UserRepository userRepository)
        {
            if (CheckIfTokenIsValid(token))
            {
               
                    UserRepository userRepository1 = _userRepositoryDAO.Get(userRepository.ID);
                    if (userRepository1 != null)
                    {
                        return userRepository1;
                    }
                    throw new DataNotFoundException($"there isn't an existing userRepository with this ID: {userRepository.ID} in the data base");
            }
            throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Update userRepository details
        /// <summary>
        /// Update an userRepository by its ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        public void UpdateUserRepositoryDetails(LoginToken<Admin> token, UserRepository userRepository)
        {
            if (CheckIfTokenIsValid(token))
            {
                UserRepository userRepository1 = _userRepositoryDAO.Get(userRepository.ID);
                if (userRepository1 != null)
                {
                    _userRepositoryDAO.Update(userRepository);
                }
                else throw new DataNotFoundException($"there isn't an existing customer with this ID: {userRepository.ID} in the data base");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.ID} is null");
        }
        #endregion

        #region Check if token is valid
        /// <summary>
        /// Check that the token isn't null
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool CheckIfTokenIsValid(LoginToken<Admin> token)
        {
            return (token != null && token.User != null);
        }
        #endregion

        #region Is token id matchs T id
        /// <summary>
        /// Check if the token id matches the other poko
        /// </summary>
        /// <param name = "token" ></ param >
        /// < param name="Id"></param>
        /// <returns></returns>
        private bool IsTokenidMatchsTid(LoginToken<Admin> token, long Id)
        {
            return (token.User.ID == Id);
        }

        bool ILoggedInAdministratorFacade.CheckIfTokenIsValid(LoginToken<Admin> token)
        {
            throw new NotImplementedException();
        }

        bool ILoggedInAdministratorFacade.IsTokenidMatchsTid(LoginToken<Admin> token, long Id)
        {
            throw new NotImplementedException();
        }
        #endregion


        
    }
}
