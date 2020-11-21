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
    class UserRoleDAO : IUserRoleDAO
    {
        #region Add a userRole
        /// <summary>
        /// add a userRole to the data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(UserRole t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_USER_ROLE_TO_USER_ROLE", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", t.UserRoleOf));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Get a userRole by its ID
        /// <summary>
        /// get a userRole by its ID from the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public UserRole Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_USER_ROLE_IN_USER_ROLE_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    UserRole user = new UserRole
                    {
                        ID = (Int64)reader["ID"],

                    };
                    return user;
                }


                return null;
            }
        }
        #endregion

        #region Get all userRoles
        /// <summary>
        /// get all userRoles from the data base
        /// </summary>
        /// <returns></returns>
        public IList<UserRole> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                List<UserRole> users = new List<UserRole>();
                SqlCommand cmd = new SqlCommand("GET_ALL_USER_ROLE_IN_USER_ROLE", conn);

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {
                    UserRole user = new UserRole
                    {
                        ID = (Int64)reader["ID"],

                    };
                    users.Add(user);
                }


                return users;
            }
        }
        #endregion

        #region Remove a userRole by its ID
        /// <summary>
        /// remove a userRole by its ID from the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(UserRole t)
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

        #region Update a userRole
        /// <summary>
        /// update a userRole by its ID from the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(UserRole t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_USER_ROLE_IN_USER_ROLE_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", t.UserRoleOf));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

    }
}
