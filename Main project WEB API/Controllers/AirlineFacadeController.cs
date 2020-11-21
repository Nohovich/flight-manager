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
    [AirlineBasicAuthentication]
    public class AirlineFacadeController : ApiController
    {
        private void GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token)
        {
            Request.Properties.TryGetValue("airlineLoginToken", out object airlineLoginToken);
            token = airlineLoginToken as LoginToken<AirlineCompany>;
            Request.Properties.TryGetValue("airlineFacade", out object airlineFacade);
            facade = airlineFacade as LoggedInAirlineFacade;
        }

        #region Create flight API
        /// <summary>
        /// Add a flight of airline company
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpPost]
        [Route("api/airlinecompany/createflight")]
        public IHttpActionResult CreateFlight([FromBody] JObject jsonDataForFlight)
        {
            long airlineCompanyID = (long)jsonDataForFlight["airlineCompanyID"];
            long originCountryCode = (long)jsonDataForFlight["originCountryCode"];
            long destinationCountryCode = (long)jsonDataForFlight["destinationCountryCode"];
            DateTime departureTime = (DateTime)jsonDataForFlight["departureTime"];
            DateTime landingTime = (DateTime)jsonDataForFlight["landingTime"];
            long remainingTickets = (long)jsonDataForFlight["remainingTickets"];

            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);
            Flight flight = new Flight(departureTime,landingTime,remainingTickets,airlineCompanyID,originCountryCode,destinationCountryCode);
           
            try
            {
                facade.CreateFlight(token, flight);
            }
            catch (TokenIsNullException)
            {
                return Unauthorized();
            }
            catch (UserNameIDAndSecondTableIDNotMatchException notMatch)
            {
                return BadRequest($"Error: {notMatch}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
            return CreatedAtRoute(" ", new { id = flight.ID }, flight);
        }
        #endregion

        #region Get a flight by ID API
        /// <summary>
        /// get a flight by id from the data base
        /// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/airlinecompany/getaflightbyid/{flightID}")]
        public IHttpActionResult GetAFlightByid([FromUri]string flightID)
        {
            Flight flight;
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                 flight = facade.GetAFlightByid(token, flightID);
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
            return Ok(flight);
        }
        #endregion

        #region Get All flights of all the airline companies API
        /// <summary>
        /// Get All flights of all the airline companies
        ///// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/airlinecompany/getallflights")]
        public IHttpActionResult GetAllFlights()
        {
            IList<Flight> flights;
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);
            try
            {
               flights = facade.GetAllFlights(token);
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

        #region Get all flights of an airline company API
        ///// <summary>
        ///// get all flights from the data base
        ///// </summary>
        [ResponseType(typeof(Flight))]
        [HttpGet]
        [Route("api/airlinecompany/getalfFlightsofanairlinecompany")]
        public IHttpActionResult GetAllFlightsOfAnAirlineCompany()
        {
            IList<Flight> flights;
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                 flights = facade.GetAllFlights(token);
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
            return Ok(flights);
        }
        #endregion

        #region Get all tickets of an airline company API
        /// <summary>
        /// get all tickets sold to the airline company
        /// </summary>
        [ResponseType(typeof(Ticket))]
        [HttpGet]
        [Route("api/airlinecompany/getalltickets")]
        public IHttpActionResult GetAllTickets()
        {
            IList<Ticket> tickets;
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                 tickets = facade.GetAllTickets(token);
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
            return Ok(tickets);
        }
        #endregion

        #region Update flight API
        ///// <summary> 
        ///// Update the fields of a flight of the airline company
        ///// </summary>
        [ResponseType(typeof(Flight))]
        [HttpPut]
        [Route("api/airlinecompany/updateflight")]
        public IHttpActionResult UpdateFlight([FromBody] Flight flight)
        {
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                facade.UpdateFlight(token, flight);
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

        #region Update airline details API
        ///// <summary>
        ///// Update fields of an airline company
        ///// </summary>
        [ResponseType(typeof(AirlineCompany))]
        [HttpPut]
        [Route("api/airlinecompany/modifyairlinedetails")]
        public IHttpActionResult ModifyAirlineDetails([FromBody] AirlineCompany airlineCompany)
        {
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                facade.ModifyAirlineDetails(token, airlineCompany);
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

        #region Change my password API
        ///// <summary>
        ///// Change the password of airline company
        ///// </summary>
        [ResponseType(typeof(AirlineCompany))]
        [HttpPut]
        [Route("api/airlinecompany/chngemypassword")]
        public IHttpActionResult ChangeMyPassword([FromBody]string newPassword)
        {
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                facade.ChangeMyPassword(token, newPassword);
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

        #region Cancel flight API
        ///// <summary>
        /////  Cancel a flight of a airline company .
        ///// </summary>
        [ResponseType(typeof(Flight))]
        [HttpDelete]
        [Route("api/airlinecompany/cancelflight")]
        public IHttpActionResult CancelFlight([FromBody] Flight flight)
        {
            GetLoginData(out LoggedInAirlineFacade facade, out LoginToken<AirlineCompany> token);

            try
            {
                facade.CancelFlight(token, flight);
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
