using Main_Project.IDAO;
using Main_Project.POCO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.DAO
{
    class TicketDAO : ITicketDAO
    {
        #region Get tickets that have landed
        /// <summary>
        ///  remove tickets that have landed to another Table called ticketsHistory
        /// </summary>
        public void UpdateTicketsHistoryThatHaveLanded()
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_TICKETS_HISTORY_OF_FLIGHTS_THAT_HAVE_LANDED", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Add a ticket
        /// <summary>
        /// buy a ticket of a flight and add it to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(Ticket t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_TICKET_TO_TICKETS", conn);
                cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightID));
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", t.CustomerID));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }

        #endregion

        #region Get a ticket
        /// <summary>
        /// get a ticket by id from the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Ticket Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_TICKET_IN_TICKETS_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Ticket ticket = new Ticket
                    {
                        ID = (Int64)reader["ID"],
                        FlightID = (Int64)reader["FLIGHT_ID"],
                        CustomerID = (Int64)reader["CUSTOMER_ID"]


                    };
                    return ticket;
                }


                return null;
            }
        }
        #endregion

        #region Get all tickets that a customer bought
        /// <summary>
        /// get all tickets that a customer bought from the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTicketsOfACusomer(long ID)

        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_TICKETS_IN_TICKETS_BY_CUSTOMER_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", ID));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Ticket ticket = new Ticket
                    {
                        ID = (Int64)reader["ID"],
                        FlightID = (Int64)reader["FLIGHT_ID"],
                        CustomerID = (Int64)reader["CUSTOMER_ID"]


                    };
                    tickets.Add(ticket);
                }


                return tickets;
            }
        }
        #endregion

        #region Get all tickets of an airline company sold
        /// <summary>
        /// get all tickets of an airline company sold from the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTicketsOfAirlineCompany(long ID)

        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_TICKETS_IN_TICKETS_BY_FLIGHT_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", ID));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Ticket ticket = new Ticket
                    {
                        ID = (Int64)reader["ID"],
                        FlightID = (Int64)reader["FLIGHT_ID"],
                        CustomerID = (Int64)reader["CUSTOMER_ID"]


                    };
                    tickets.Add(ticket);
                }


                return tickets;
            }
        }
        #endregion

        #region Get all ticket that have been sold
        /// <summary>
        /// get all ticket that have been sold from the data base
        /// </summary>
        /// <returns></returns>
        public IList<Ticket> GetAll()

        {
            List<Ticket> tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_TICKETS_IN_TICKETS_INFO", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Ticket ticket = new Ticket
                    {
                        ID = (Int64)reader["ID"],
                        FlightID = (Int64)reader["FLIGHT_ID"],
                        CustomerID = (Int64)reader["CUSTOMER_ID"]


                    };
                    tickets.Add(ticket);
                }


                return tickets;
            }
        }
        #endregion

        #region Remove a ticket
        /// <summary>
        /// remove an existing ticket from the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_TICKET_IN_TICKETS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Remove all tickets of an airline company
        /// <summary>
        /// Remove all existing tickets of an airline company from the data base
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveTicketsByAirlineCompanyID(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_TICKETS_IN_TICKETS_BY_AIRLINECOMPANY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Remove all tickets of a customer
        /// <summary>
        /// Remove all existing tickets of a customer from the data base
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveTicketsByCustomerID(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_TICKETS_IN_TICKETS_BY_CUSTOMER_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Remove all tickets of a flight
        /// <summary>
        /// remove all tickets of a flight from the data base
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveTicketsByFlightID(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_TICKETS_IN_TICKETS_BY_FLIGHT_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update a ticket
        /// <summary>
        /// update an existing ticket from the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_TICKET_IN_TICKETS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightID));
                cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", t.CustomerID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }

        public void AddTicket(Ticket ticket, Flight f)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
