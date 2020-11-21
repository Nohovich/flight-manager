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
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        #region Cancel ticket
        /// <summary>
        /// Remove a ticket of the customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket"></param>
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (CheckIfTokenIsValid(token))
            {
                if (IsTokenidMatchsTid(token, ticket.CustomerID))
                {
                    _ticketDAO.RemoveTicketsByCustomerID(ticket.ID);
                    _flightDAO.AddAndUpdateFlightTicket(ticket.FlightID);
                }
                else throw new UserNameIDAndSecondTableIDNotMatchException($"customer ID: { token.User.ID} and customer ID: {ticket.CustomerID} do not Match");

            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion

        #region Get all my flights
        /// <summary>
        /// Get all the customer flights
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (CheckIfTokenIsValid(token))
            {
                return _flightDAO.GetFlightsByCustomer(token.User);
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion

        #region Purchase ticket
        /// <summary>
        /// Customer buy a ticket
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public void PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            if (CheckIfTokenIsValid(token))
            {
                Flight flight1 = _flightDAO.Get(flight.ID);
                if (flight1 != null)
                {
                    if (flight1.DepartureTime <= DateTime.Now.AddMinutes(15))
                    {
                        Ticket ticket = new Ticket(flight.ID, token.User.ID);
                        _ticketDAO.Add(ticket);
                        _flightDAO.ReductionFlightTicket(flight.ID);
                        return;
                    }
                    throw new FlightAlreadyDeparturException($"this flight with this ID: {flight.ID}  has already departed");

                }
                throw new DataNotFoundException($"there isn't an existing flight with this ID: {flight.ID} in the data base");
            }
            else throw new TokenIsNullException($"Token ID: {token.User.User_Repository_ID}");
        }
        #endregion;

        #region Check if token is valid
        /// <summary>
        /// Check that the token isn't null
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool CheckIfTokenIsValid(LoginToken<Customer> token)
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
        private bool IsTokenidMatchsTid(LoginToken<Customer> token, long Id)
        {
            return (token.User.ID == Id);
        }
        #endregion
    }
}
