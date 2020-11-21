using Main_Project.IDAO;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.DAO
{
    class FlightDAO : IFlightDAO
    {
        #region Get flights that have landed
        /// <summary>
        ///  remove flights that have landed to another Table called FlightsHistory
        /// </summary>
        public void UpdateFlightsHistoryThatHaveLanded()
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_FLIGHTS__HISTORY_THAT_HAVE_LANDED", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }
    
        #endregion

        #region Add a flight
        /// <summary>
        /// add a flight to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(Flight t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_FLIGHT_TO_FLIGHTS", conn);
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", t.AirlineCompanyID));
                cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", t.OriginCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", t.DestinationCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME_DATE_TIME", t.DepartureTime));
                cmd.Parameters.Add(new SqlParameter("@LANDING_TIME_DATE_TIME", t.LandingTime));
                cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", t.RemainingTickets));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Get a flight by ID
        /// <summary>
        /// get a flight by id from the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Flight Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHT_IN_FLIGHTS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]


                    };
                    return flight;
                }


                return null;
            }
        }
        #endregion

        #region Get all flights ID of an airline company
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlightsIdOfAnAirlineCompany(long airlineCompanyID)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("Get_FLIGHTS_IN_FLIGHTS_OF_AIRLINECOMPANY_ID", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", airlineCompanyID));

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Get all flights of an airline company
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlightsOfAnAirlineCompany (AirlineCompany airlineCompany)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("Get_FLIGHTS_IN_FLIGHTS_OF_AIRLINECOMPANY_ID", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", airlineCompany.ID));

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Get all flights
        /// <summary>
        /// get all flights from the data base
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS_IN_FLIGHTS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Get all razor flights
        /// <summary>
        /// Get all the razor flights
        /// </summary>
        /// <returns></returns>
        public IList<FlightRazor> RazorAllFlights()
        {
            List<FlightRazor> flights = new List<FlightRazor>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("RAZOR_GET_ALL_FLIGHTS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    FlightRazor flight = new FlightRazor
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = (string)reader["AIRLINE_NAME"],
                        AirlineNameAndFlightID = $"{(string)reader["AIRLINE_NAME"]} {(Int64)reader["ID"]}",
                        OriginCountry = (string)reader["ORIGIN_COUNTRY"],
                        DestinationCountry = (string)reader["DEST_COUNTRY"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"]

                    };
                    flights.Add(flight);
                }
                return flights;
            }
        }
        #endregion

        #region Get all flights that will departure in the upcoming 12 hours
        /// <summary>
        /// Get all flights that will departure in the upcoming 12 hours
        /// </summary>
        /// <returns></returns>
        public IList<FlightRazor> DeparturesFlights()
        {
            List<FlightRazor> flights = new List<FlightRazor>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("RAZOR_DEPARTING_FLIGHTS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    FlightRazor flight = new FlightRazor
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = (string)reader["AIRLINE_NAME"],
                        AirlineNameAndFlightID = $"{(string)reader["AIRLINE_NAME"]} {(Int64)reader["ID"]}",
                        OriginCountry = (string)reader["ORIGIN_COUNTRY"],
                        DestinationCountry = (string)reader["DEST_COUNTRY"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"]

                    };
                    flights.Add(flight);
                }
                return flights;
            }
        }
        #endregion

        #region Get all flights that will land in the upcoming 12 hours or landed in the last 4 hours
        /// <summary>
        /// Get all flights that will land in the upcoming 12 hours or landed in the last 4 hours
        /// </summary>
        /// <returns></returns>
        public IList<FlightRazor> LandingFlights()
        {
            List<FlightRazor> flightsResults = new List<FlightRazor>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("RAZOR_LANDING_FLIGHTS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    FlightRazor flight = new FlightRazor
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = (string)reader["AIRLINE_NAME"],
                        AirlineNameAndFlightID = $"{(string)reader["AIRLINE_NAME"]} {(Int64)reader["ID"]}",
                        OriginCountry = (string)reader["ORIGIN_COUNTRY"],
                        DestinationCountry = (string)reader["DEST_COUNTRY"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"]

                    };
                    //flight.Airline_Pic = $"Src/Images/Logos/{flight.AirlineName}.png";

                    flightsResults.Add(flight);



                }


                return flightsResults;
            }
        }
        #endregion

        #region Get all flights that isn't fully booked
        /// <summary>
        /// Get all flights that isn't fully booked from the data base
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = new Dictionary<Flight, int>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS_IN_FLIGHTS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight, (int)flight.RemainingTickets);
                }


                return flights;
            }
        }
        #endregion

        #region Get flights of a customer
        /// <summary>
        /// get all flights of a customer from the data base
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns> 
        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS_OF_CUSTOMER_IN_FLIGHTS_BY_CUSTOMER_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", customer.ID));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Get flights by departure date
        /// <summary>
        /// get flights by departure date from the data base
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_IN_FLIGHTS_BY_DEPARTURE_TIME", conn);
                cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME_DATE_TIME", departureDate));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Get flights by destination country
        /// <summary>
        /// Get flights by destination country from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_IN_FLIGHTS_BY_DESTINATION_COUNTRY_CODE", conn);
                cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", countryCode));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
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
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_IN_FLIGHTS_BY_LANDING_TIME", conn);
                cmd.Parameters.Add(new SqlParameter("@LANDING_TIME_DATE_TIME", landingDate));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
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
            List<Flight> flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_IN_FLIGHTS_BY_ORIGIN_COUNTRY_CODE", conn);
                cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", countryCode));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Flight flight = new Flight
                    {
                        ID = (Int64)reader["ID"],
                        AirlineCompanyID = (Int64)reader["AIRLINECOMPANY_ID"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"],
                        LandingTime = (DateTime)reader["LANDING_TIME_DATE_TIME"],
                        DestinationCountryCode = (Int64)reader["DESTINATION_COUNTRY_CODE"],
                        OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Get flights by origin country and its destination country
        /// <summary>
        /// Get flights by origin country from the data base
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<FlightRazor> GetFlightsByOriginCountryAndDestination(long origin, long destination)
        {
            List<FlightRazor> flights = new List<FlightRazor>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_ORIGIN_COUNTRY_AND_DESTINATION_COUNTRY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", origin));
                cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", destination));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    FlightRazor flight = new FlightRazor
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = (string)reader["AIRLINE_NAME"],
                        AirlineNameAndFlightID = $"{(string)reader["AIRLINE_NAME"]} {(Int64)reader["ID"]}",
                        OriginCountry = (string)reader["ORIGIN_COUNTRY"],
                        DestinationCountry = (string)reader["DEST_COUNTRY"],
                        RemainingTickets = (int)reader["REMAINING_TICKETS"],
                        DepartureTime = (DateTime)reader["DEPARTURE_TIME_DATE_TIME"]

                    };
                    flights.Add(flight);
                }


                return flights;
            }
        }
        #endregion

        #region Remove a flight
        /// <summary>
        /// Remove a flight from the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_FLIGHT_IN_FLIGHTS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Remove flights by airline company
        /// <summary>
        /// remove all flights of an airline company
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveFlightsByAirlineCompanyID(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_FLIGHTS_IN_FLIGHTS_BY_AIRLINECOMPANY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update a flight
        /// <summary>
        /// update a flight by ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_FLIGHT_IN_FLIGHTS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", t.AirlineCompanyID));
                cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", t.OriginCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", t.DestinationCountryCode));
                cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME_DATE_TIME", t.DepartureTime));
                cmd.Parameters.Add(new SqlParameter("@LANDING_TIME_DATE_TIME", t.LandingTime));
                cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", t.RemainingTickets));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Add one ticket to a flight tickets info
        /// <summary>
        /// Add one ticket to a flight tickets info in the data base
        /// </summary>
        /// <param name="ID"></param>
        public void AddAndUpdateFlightTicket(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_AND_UPDATE_FLIGHT_TICKET_IN_FLIGHTS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Reduct a ticket to a flight tickets info
        /// <summary>
        /// Reduct a ticket to a flight tickets info in the data base
        /// </summary>
        /// <param name="ID"></param>
        public void ReductionFlightTicket(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REDUCTION_AND_UPDATE_FLIGHT_TICKET_IN_FLIGHTS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

    }
}
