using Main_Project.Login;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    interface ILoggedInAdministratorFacade
    {

        #region Get all countries in the date base
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<Country> GetAllCountries(LoginToken<Admin> token);

        #endregion

        #region Get all administrator in the date base
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<Admin> GetAdmins(LoginToken<Admin> token);

        #endregion

        #region Get all airline companies of a country
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<AirlineCompany> GetAllAirlineCompaniesOfACountry(LoginToken<Admin> token, long countryID);

        #endregion

        #region Get all airline companies in database
        /// <summary>
        /// Get All flights of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<AirlineCompany> GetAllAirlineCompanies(LoginToken<Admin> token);

        #endregion

        #region Transfer tickets that have landed
        void UpdateHistoryInfo();
        #endregion

        #region Create new country
        /// <summary>
        /// add a new country to the data base
        /// </summary>
        /// <param name="token"></param>
        /// <param name="country"></param>
        void CreateNewCountry(LoginToken<Admin> token, Country country);
        #endregion

        #region Create new admin
        /// <summary>
        /// Creating a new admin
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        /// <param name="admin"></param>
        void CreateNewAdmin(LoginToken<Admin> token, UserRepository userRepository, Admin admin);

        #endregion

        #region Create new airline
        /// <summary>
        /// Create a new airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        /// <param name="airlineCompany"></param>
        /// <param name="country"></param>
        void CreateNewAirline(LoginToken<Admin> token, UserRepository userRepository, AirlineCompany airlineCompany, Country country);
        #endregion

        #region Get admin by id
        /// <summary>
        /// get an admin by his id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        Admin GetAdminByid(LoginToken<Admin> token, string ID);
        #endregion

        #region Get country by id
        /// <summary>
        /// get an admin by his id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        Country GetCountryByID(LoginToken<Admin> token, string ID);
        #endregion

        #region Get airline company by id
        /// <summary>
        /// get an airline company by its id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        AirlineCompany GetAirlineCompanyByid(LoginToken<Admin> token, string ID);
        #endregion

        #region Get customer by its id
        /// <summary>
        /// get a customer by its id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        Customer GetCustomerByid(LoginToken<Admin> token, string ID);
        #endregion

        #region Remove admin
        /// <summary>
        /// Remove an admin by his UserRepositoryID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository1"></param>
        void RemoveAdmin(LoginToken<Admin> token, string UserRepositoryID);

        #endregion

        #region Remove airline
        /// <summary>
        /// Remove an airline company by its UserRepositoryID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="UserRepositoryID"></param>
        void RemoveAirline(LoginToken<Admin> token, string airlineUserRepositoryID);
        #endregion

        #region Remove customer
        /// <summary>
        /// Remove a customer by his UserRepositoryID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="UserRepositoryID"></param>
        void RemoveCustomer(LoginToken<Admin> token, string UserRepositoryID);

        #endregion

        #region Update admin details
        /// <summary>
        /// Update an admin
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        void UpdateAdminDetails(LoginToken<Admin> token, Admin admin);

        #endregion

        #region Update airline details
        /// <summary>
        /// Update an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany airline);

        #endregion

        #region Update customer details
        /// <summary>
        /// Update an customer with his ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer);

        #endregion

        #region Get userRepository details
        /// <summary>
        /// Get an userRepository by its ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        /// <returns></returns>
        UserRepository GetUserRepositoryDetails(LoginToken<Admin> token, UserRepository userRepository);

        #endregion

        #region Update userRepository details
        /// <summary>
        /// Update an userRepository by its ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userRepository"></param>
        void UpdateUserRepositoryDetails(LoginToken<Admin> token, UserRepository userRepository);

        #endregion

        #region Check if token is valid
        /// <summary>
        /// Check that the token isn't null
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool CheckIfTokenIsValid(LoginToken<Admin> token);
        #endregion

        #region Is token id matchs T id
        /// <summary>
        /// Check if the token id matches the other poko
        /// </summary>
        /// <param name = "token" ></ param >
        /// < param name="Id"></param>
        /// <returns></returns>
        bool IsTokenidMatchsTid(LoginToken<Admin> token, long Id);
        
        #endregion
    }
}
