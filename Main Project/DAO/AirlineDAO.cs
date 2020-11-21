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
    class AirlineDAO : IAirlineDAO
    {
        #region Add airline company to database
        /// <summary>
        /// add airline company to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(AirlineCompany t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_AIRLINECOMPANY_IN_AIRLINECOMPANIES", conn);
                cmd.Parameters.Add(new SqlParameter("@AIRLINE_NAME", t.AirlineName));
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", t.CountryCode));
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", t.User_Repository_ID));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Get airline company by ID
        /// <summary>
        /// get airline company by its ID in the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AirlineCompany Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_AIRLINECOMPANY_IN_AIRLINECOMPANIES_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    AirlineCompany airlineCompany = new AirlineCompany
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = reader["AIRLINE_NAME"].ToString(),
                        CountryCode = (Int64)reader["COUNTRY_CODE"],
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    return airlineCompany;
                }


                return null;
            }
        }
        #endregion

        #region Get airline company by its userName
        /// <summary>
        /// get airline company by its userName in the data base
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineByUserame(string userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_AIRLINECOMPANY_IN_AIRLINECOMPANIES_BY_USER_NAME", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", userName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    AirlineCompany airlineCompany = new AirlineCompany
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = reader["AIRLINE_NAME"].ToString(),
                        CountryCode = (Int64)reader["COUNTRY_CODE"],
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    return airlineCompany;
                }


                return null;
            }
        }
        #endregion

        #region Get all airline companies in database
        /// <summary>
        /// get all the airline companies in the data base
        /// </summary>
        /// <returns></returns>
        public IList<AirlineCompany> GetAll()
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_AIRLINECOMPANIES_IN_AIRLINECOMPANIES", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    AirlineCompany airlineCompany = new AirlineCompany
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = reader["AIRLINE_NAME"].ToString(),
                        CountryCode = (Int64)reader["COUNTRY_CODE"],
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]

                    };
                    airlineCompanies.Add(airlineCompany);
                }


                return airlineCompanies;
            }
        }
        #endregion

        #region Get all airline companies of a country
        /// <summary>
        /// get all the airline companies of a country from the data base
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
         public IList<AirlineCompany> GetAllAirlinesByCountry(long countryId)
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_AIRLINECOMPANIES_IN_AIRLINECOMPANIES_BY_COUNTRY_CODE", conn);
                cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", countryId));
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    AirlineCompany airlineCompany = new AirlineCompany
                    {
                        ID = (Int64)reader["ID"],
                        AirlineName = reader["AIRLINE_NAME"].ToString(),
                        CountryCode = (Int64)reader["COUNTRY_CODE"],
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]

                    };
                    airlineCompanies.Add(airlineCompany);
                }


                return airlineCompanies;
            }
        }
        #endregion

        #region Remove airline company
        /// <summary>
        /// remove airline company by ID
        /// </summary>
        /// <param name="t"></param>
        public void Remove(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_AIRLINECOMPANY_IN_AIRLINECOMPANIES_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update airline company
        /// <summary>
        /// update airline company by its ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_AIRLINECOMPANY_IN_AIRLINECOMPANIES_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@AIRLINE_NAME", t.AirlineName));
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", t.CountryCode));
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", t.User_Repository_ID));


                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Get airline company by userRepository ID
        /// <summary>
        /// get airline company by its userRepository ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AirlineCompany GetAirlineCompanyByUserRepositoryID(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_AIRLINECOMPANY_IN_AIRLINECOMPANIES_BY_USER_REPOSITORY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    AirlineCompany airlineCompany = new AirlineCompany()
                    {

                        ID = (Int64)reader["ID"],
                        AirlineName = reader["AIRLINE_NAME"].ToString(),
                        CountryCode = (Int64)reader["COUNTRY_CODE"],
                        User_Repository_ID = (Int64)reader["USER_REPOSITORY_ID"]


                    };
                    return airlineCompany;
                }


                return null;
            }
        }
        #endregion

    }
}
