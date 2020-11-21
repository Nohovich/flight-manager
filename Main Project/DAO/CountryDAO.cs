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
    class CountryDAO : ICountryDAO
    {
        #region Add country to a database
        /// <summary>
        /// add country to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(Country t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_COUNTRY_IN_COUNTRIES", conn);
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_NAME", t.CountryName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Get country by ID
        /// <summary>
        /// get country by its ID in the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Country Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_COUNTRY_IN_COUNTRIES_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Country country = new Country
                    {
                        ID = (Int64)reader["ID"],
                        CountryName = reader["COUNTRY_NAME"].ToString()


                    };
                    return country;
                }


                return null;
            }
        }
        #endregion

        #region get country by name
        /// <summary>
        /// get a country by its name in the data base
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public Country GetCountryByName(string countryName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_COUNTRY_IN_COUNTRIES_BY_COUNTRY_NAME", conn);
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_NAME", countryName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Country country = new Country
                    {
                        ID = (Int64)reader["ID"],
                        CountryName = reader["COUNTRY_NAME"].ToString()


                    };
                    return country;
                }


                return null;
            }
        }
        #endregion

        #region Get all countries in the database
        /// <summary>
        /// get all countries in the data base
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_COUNTRIES_IN_COUNTRIES", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Country country = new Country
                    {
                        ID = (Int64)reader["ID"],
                        CountryName = reader["COUNTRY_NAME"].ToString()

                    };
                    countries.Add(country);
                }


                return countries;
            }
        }
        #endregion

        #region Remove country
        /// <summary>
        /// remove a country by its ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_COUNTRY_IN_COUNTRIES_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update country
        /// <summary>
        /// update a country by its ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_COUNTRY_IN_COUNTRIES_BY_ID @ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@COUNTRY_NAME", t.CountryName));


                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

    }
}
