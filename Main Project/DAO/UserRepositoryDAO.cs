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
    class UserRepositoryDAO : IUserRepositoryDAO
    {
        #region Creating an admin userRepository user
        /// <summary>
        /// creating an admin userRepository user in the data base
        /// </summary>
        /// <param name="t"></param>
        public void CreateAdminIndUserRepository(UserRepository t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_USER_TO_USER_REPOSITORY", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", RolesEnum.admin));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion 

        #region Creating an airline company userRepository user
        /// <summary>
        /// creating an airline company userRepository user in the data base
        /// </summary>
        /// <param name="t"></param>
        public void CreateAirlineIndUserRepository(UserRepository t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_USER_TO_USER_REPOSITORY", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", RolesEnum.airline));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Creating an customer userRepository user
        /// <summary>
        /// creating an customer userRepository user in the data base
        /// </summary>
        /// <param name="t"></param>
        public void CreateCustomerInUserRepository(UserRepository t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_USER_TO_USER_REPOSITORY", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", RolesEnum.customer));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Adding a userRepository user
        /// <summary>
        /// Adding a userRepository user to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(UserRepository t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_USER_TO_USER_REPOSITORY", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", t.UserRoleID));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Get an UserRepository by its ID
        /// <summary>
        /// get an UserRepository by its ID from the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public UserRepository Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_USER_IN_USER_REPOSITORY_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    /*
                    var obj = new
                    {
                        ID = reader["ID"],
                        UserName = reader["USER_NAME"],
                        Password = reader["PASSWORD"],
                        UserRoleID =  reader["USER_ROLE"]
                    };
                    */
                    
                    UserRepository user = new UserRepository
                    {
                        ID = (Int64)reader["ID"],
                        UserName = reader["USER_NAME"].ToString(),
                        Password = reader["PASSWORD"].ToString(),
                        UserRoleID = (RolesEnum)Convert.ToInt32(reader["USER_ROLE"])
                    };
                    
                    return user;
                }


                return null;
            }
        }
        #endregion

        #region Get all userRepository users
        /// <summary>
        /// get all UserRepository users from the data base
        /// </summary>
        /// <returns></returns>
        public IList<UserRepository> GetAll()
        {
            List<UserRepository> users = new List<UserRepository>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_USERES_IN_USER_REPOSITORY", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    UserRepository user = new UserRepository
                    {
                        ID = (Int64)reader["ID"],
                        UserName = reader["USER_NAME"].ToString(),
                        Password = reader["PASSWORD"].ToString(),
                        UserRoleID = (RolesEnum)Convert.ToInt32(reader["USER_ROLE"])

                    };
                    users.Add(user);
                }


                return users;
            }
        }
        #endregion

        #region Remove a userRepository by its ID
        /// <summary>
        /// remove a userRepository by its ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(UserRepository t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_USER_ROLE_IN_USER_ROLE_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update an userRepository by its ID
        /// <summary>
        ///  update an userRepository by its ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(UserRepository t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_USER_IN_USER_REPOSITORY_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", t.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", t.Password));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", t.UserRoleID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Get a userRepository user by its userName
        /// <summary>
        /// get a userRepository user by its userName from the data base
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserRepository GetUserByUserName(String userName)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_USER_IN_USER_REPOSITORY_BY_USER_NAME", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", userName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    UserRepository user = new UserRepository
                    {
                        ID = (Int64)reader["ID"],
                        UserName = reader["USER_NAME"].ToString(),
                        Password = reader["PASSWORD"].ToString(),
                        UserRoleID = (RolesEnum)((Int64)reader["USER_ROLE"])


                    };
                    return user;
                }


                return null;
            }
        }
        #endregion

    }
}
