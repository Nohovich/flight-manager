using Main_Project.Login;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    interface ILoggedInAirlineFacade
    {
        #region Cancel flight
        /// <summary>
        ///  Cancel a flight of a airline company .
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
         void CancelFlight(LoginToken<AirlineCompany> token, Flight flight);

        #endregion

        #region Change my password
        /// <summary>
        /// Change the password of airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string newPassword);

        #endregion

        #region Create flight
        /// <summary>
        /// Add a flight of airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        void CreateFlight(LoginToken<AirlineCompany> token, Flight flight);

        #endregion

        #region Get a flight by ID
        /// <summary>
        /// get a flight by id from the data base
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        Flight GetAFlightByid(LoginToken<AirlineCompany> token, string ID);

        #endregion

        #region Get all flights of an airline company
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
        IList<Flight> GetAllFlightsOfAnAirlineCompany(LoginToken<AirlineCompany> token);
        #endregion

        #region Get All flights of all the airline companies
        /// <summary>
        /// Get All flights of all the airline companies
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token);

        #endregion

        #region Get all tickets of an airline company
        /// <summary>
        /// get all tickets sold to the airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token);

        #endregion

        #region Modify airline details
        /// <summary>
        /// Update fields of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompany"></param>
        void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airlineCompany);

        #endregion

        #region Update flight
        /// <summary>
        /// Update the fields of a flight of the airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight);
       
        #endregion

    }
}
