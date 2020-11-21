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
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        
        #region Cancel flight
        /// <summary>
        ///  Cancel a flight of a airline company .
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (CheckIfTokenIsValid(token))
            {
                if (IsTokenidMatchsTid(token, flight.AirlineCompanyID))
                {
                    _ticketDAO.RemoveTicketsByFlightID(flight.ID);
                    _flightDAO.Remove(flight);
                }
                else throw new UserNameIDAndSecondTableIDNotMatchException($"UserName ID: { token.User.ID} and airline company: {flight.AirlineCompanyID} do not Match");

            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");

        }
        #endregion

        #region Change my password
        /// <summary>
        /// Change the password of airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string newPassword)
        {
            if(CheckIfTokenIsValid(token))
            {
                UserRepository userRepository = _userRepositoryDAO.Get(token.User.User_Repository_ID);
                userRepository.Password = newPassword;
                _userRepositoryDAO.Update(userRepository);
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion

        #region Create flight
        /// <summary>
        /// Add a flight of airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (CheckIfTokenIsValid(token))
            {
             if (IsTokenidMatchsTid(token,flight.AirlineCompanyID))
             {
                    flight.AirlineCompanyID = token.User.ID;

                _flightDAO.Add(flight);

             }
            else throw new UserNameIDAndSecondTableIDNotMatchException($"UserName ID: {token.User.ID} and airline company: {flight.AirlineCompanyID} do not Match");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion

        #region Get a flight by ID
        /// <summary>
        /// get a flight by id from the data base
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Flight GetAFlightByid(LoginToken<AirlineCompany> token, string ID)
        {
            if (CheckIfTokenIsValid(token))
            {
                long tryID;
                bool trynum = long.TryParse(ID, out tryID);
                if (trynum)
                {
                    Flight flight = _flightDAO.Get(tryID);
                    if (flight != null)
                    {
                        return flight;
                    }
                    throw new DataNotFoundException($"there isn't an existing flight with this {tryID} in the data base");
                }
                if (!trynum)
                {
                    throw new FormatException($"cant convert {ID} to a number");
                }
            }
            throw new TokenIsNullException($"Token ID: {token} is null");
        }
        #endregion

        #region Get all flights of an airline company
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlightsOfAnAirlineCompany(LoginToken<AirlineCompany> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _flightDAO.GetAllFlightsOfAnAirlineCompany(token.User);
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion

        #region Get All flights of all the airline companies
        /// <summary>
        /// Get All flights of all the airline companies
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _flightDAO.GetAll();
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");

        }
        #endregion

        #region Get all tickets of an airline company
        /// <summary>
        /// get all tickets sold to the airline company
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                List<Ticket> tickets = new List<Ticket>();
                List<Ticket> ticketFlight = new List<Ticket>();
                List<Flight> flights = (List<Flight>)_flightDAO.GetAllFlightsIdOfAnAirlineCompany(token.User.ID);
                if (flights.Count > 0)
                {
                     new List<Ticket>();
                    foreach (var flight in flights)
                    {
                        ticketFlight =(List<Ticket>)_ticketDAO.GetAllTicketsOfAirlineCompany(flight.ID); // help
                        foreach (var ticket in ticketFlight)
                        {
                            tickets.Add(ticket);
                        }
                    }
                    return tickets;
                }
                else throw new DataNotFoundException($"there aren't active flights for this airline {token.User}");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion

        #region Modify airline details
        /// <summary>
        /// Update fields of an airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airlineCompany"></param>
        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airlineCompany)
        {
            if (CheckIfTokenIsValid(token))
            {
                if (IsTokenidMatchsTid(token, airlineCompany.ID))
                {
                    _airlineDAO.Update(airlineCompany);
                }
                else throw new UserNameIDAndSecondTableIDNotMatchException($"UserName ID: { token.User.ID} and airline company ID: {airlineCompany.ID} do not Match");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID} is null");
        }
        #endregion

        #region Update flight
        /// <summary>
        /// Update the fields of a flight of the airline company
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (CheckIfTokenIsValid(token))
            {
                if (IsTokenidMatchsTid(token, flight.AirlineCompanyID))
                {
                    _flightDAO.Update(flight);
                }
            else throw new UserNameIDAndSecondTableIDNotMatchException($"UserName ID: { token.User.ID} and flight airline company ID: {flight.AirlineCompanyID} do not Match");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID} is null");
        }
        #endregion

        #region Check if token is valid
        /// <summary>
        /// Check that the token isn't null
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool CheckIfTokenIsValid(LoginToken<AirlineCompany> token)
        {
            return (token != null && token.User != null);
        }
        #endregion

        #region Is token id matches T id
        /// <summary>
        /// Check if the token id matches the other poko
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool IsTokenidMatchsTid(LoginToken<AirlineCompany> token, long Id)
        {
            return(token.User.ID == Id);
        }
        #endregion
    }
}
