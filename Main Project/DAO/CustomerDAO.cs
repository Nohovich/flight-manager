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
    class CustomerDAO : ICustomerDAO
    {
        #region Add customer to database
        /// <summary>
        /// add a customer to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(Customer t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_CUSTOMER_TO_CUSTOMERS", conn);
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", t.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", t.LastName));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", t.Address));
                cmd.Parameters.Add(new SqlParameter("@PHONE_NO", t.phoneNumber));
                cmd.Parameters.Add(new SqlParameter("@CREDIT_CARD_NUMBER", t.CreditCard));
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", t.User_Repository_ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;

            }
        }
        #endregion

        #region Get customer by ID
        /// <summary>
        /// get a customer by his ID in the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_CUSTOMER_IN_CUSTOMERS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Customer customer = new Customer
                    {
                        ID = (Int64)reader["ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString(),
                        Address = reader["ADDRESS"].ToString(),
                        CreditCard = reader["CREDIT_CARD_NUMBER"].ToString(),
                        phoneNumber = reader["PHONE_NO"].ToString(),
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    return customer;
                }


                return null;
            }
        }
        #endregion

        #region Get all customers
        /// <summary>
        /// get all customers in the data base
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_CUSTOMERS_IN_CUSTOMERS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Customer customer = new Customer
                    {
                        ID = (Int64)reader["ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString(),
                        Address = reader["ADDRESS"].ToString(),
                        CreditCard = reader["CREDIT_CARD_NUMBER"].ToString(),
                        phoneNumber = reader["PHONE_NO"].ToString(),
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    customers.Add(customer);
                }


                return customers;
            }
        }
        #endregion

        #region Get customer by his userName
        /// <summary>
        /// get a customer by his userName in the data base
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Customer GetCustomerByUserame(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_CUSTOMER_IN_CUSTOMERS_BY_USER_NAME", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", userName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Customer customer = new Customer
                    {
                        ID = (Int64)reader["ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString(),
                        Address = reader["ADDRESS"].ToString(),
                        CreditCard = reader["CREDIT_CARD_NUMBER"].ToString(),
                        phoneNumber = reader["PHONE_NO"].ToString(),
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    return customer;
                }


                return null;
            }
        }
        #endregion

        #region Remove a customer
        /// <summary>
        /// remove a customer by his ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Customer t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_CUSTOMER_IN_CUSTOMERS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update a customer
        /// <summary>
        /// update a customer by his ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(Customer t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_CUSTOMER_IN_CUSTOMERS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", t.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", t.LastName));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", t.Address));
                cmd.Parameters.Add(new SqlParameter("@PHONE_NO", t.phoneNumber));
                cmd.Parameters.Add(new SqlParameter("@CREDIT_CARD_NUMBER", t.CreditCard));
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", t.User_Repository_ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Get a customer by his userRepository ID
        /// <summary>
        ///  get a customer by his userRepository ID in the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer GetCustomerByUserRepositoryID(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_CUSTOMER_IN_CUSTOMERS_BY_USER_REPOSITORY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Customer customer = new Customer
                    {
                        ID = (Int64)reader["ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString(),
                        Address = reader["ADDRESS"].ToString(),
                        CreditCard = reader["CREDIT_CARD_NUMBER"].ToString(),
                        phoneNumber = reader["PHONE_NO"].ToString(),
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    return customer;
                }


                return null;
            }
        }
        #endregion

    }
}
