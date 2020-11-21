using Main_Project;
using Main_Project.Exceptions;
using Main_Project.Facade;
using Main_Project.Login;
using Main_Project.POCO;
using Main_project_WEB_API.BasicAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Main_project_WEB_API.Controllers
{
    [CustomerBasicAuthentication]
    public class CustomerFacadeController : ApiController
    {
        private void GetLoginData(out LoggedInCustomerFacade facade, out LoginToken<Customer> token)
        {
            Request.Properties.TryGetValue("customerLoginToken", out object customerLoginToken);
             token = customerLoginToken as LoginToken<Customer>;
            Request.Properties.TryGetValue("CustomerFacade", out object CustomerFacade);
            facade = CustomerFacade as LoggedInCustomerFacade;
        }

        #region Get all my flights API
        /// <summary>
        /// Get all the customer flights
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/customer/getallmyflights")]
        public IHttpActionResult GetAllMyFlights()
        {
            IList<Flight> flights;
            GetLoginData(out LoggedInCustomerFacade facade, out LoginToken<Customer> token);
            try
            {
                flights = facade.GetAllMyFlights(token);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
        #endregion

        #region Purchase ticket API
        /// <summary>
        /// Customer buy a ticket
        /// </summary>
        [ResponseType(typeof(Ticket))]
        [HttpPost]
        [Route("api/customer/purchaseticket")]
        public IHttpActionResult PurchaseTicket([FromBody] Flight flight)
        {
            GetLoginData(out LoggedInCustomerFacade facade, out LoginToken<Customer> token);

            try
            {
                facade.PurchaseTicket(token, flight);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (DataNotFoundException dataNotFoun)
            {
                return BadRequest($"Error: {dataNotFoun}");
            }
            return Ok();
        }
        #endregion;

        #region Cancel ticket API
        /// <summary>
        /// Remove a ticket of the customer
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpDelete]
        [Route("api/customer/cancelticket")]
        public IHttpActionResult CancelFlight([FromBody] Ticket ticket)
        {
            GetLoginData(out LoggedInCustomerFacade facade, out LoginToken<Customer> token);

            try
            {
                facade.CancelTicket(token, ticket);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (UserNameIDAndSecondTableIDNotMatchException NotMatc)
            {
                return BadRequest($"Error: {NotMatc}");
            }
            return Ok();
        }

        #endregion

    }
}
