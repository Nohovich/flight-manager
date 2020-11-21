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
    class AdminDAO : IAdminDAO
    {
        #region Add admin to data base
        /// <summary>
        /// add admin to data base
        /// </summary>
        /// <param name="t"></param>
        public void Add(Admin t)
        {
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_ADMIN_IN_ADMINS", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", t.UserRepositoryID));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", t.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", t.LastName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                t.ID = newId;
            }
        }
        #endregion

        #region Get admin by ID
        /// <summary>
        /// get admin by id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Admin Get(long ID)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ADMIN_IN_ADMINS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Admin admin = new Admin()
                    {
                        ID = (Int64)reader["ID"],
                        UserRepositoryID = (Int64)reader["USER_REPOSITORY_ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString()
                    };
                    return admin;
                }


                return null;
            }
        }
        #endregion

        #region Get all admin
        /// <summary>
        /// get all admin in data base
        /// </summary>
        /// <returns></returns>
        public IList<Admin> GetAll()
        {
            List<Admin> admins = new List<Admin>();
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ALL_ADMINS_IN_ADMINS", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                while (reader.Read() == true)
                {

                    Admin admin = new Admin()
                    {
                        ID = (Int64)reader["ID"],
                        UserRepositoryID = (Int64)reader["USER_REPOSITORY_ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString()

                    };
                    admins.Add(admin);
                }


                return admins;
            }
        }
        #endregion

        #region Remove admin by his ID
        /// <summary>
        /// remove admin by his ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Admin t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_ADMIN_IN_ADMINS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Update admin by his ID
        /// <summary>
        /// update admin by his ID in the data base
        /// </summary>
        /// <param name="t"></param>
        public void Update(Admin t)
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE_ADMIN_IN_ADMINS_BY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@ID", t.ID));
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", t.UserRepositoryID));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", t.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", t.LastName));


                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Get admin By userRepository ID
        /// <summary>
        /// get admin by his user repository ID in the data base
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Admin GetAdminByUserRepositoryID(long ID)
        {
            // help
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("GET_ADMIN_IN_ADMINS_BY_USER_REPOSITORY_ID", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", ID));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                if (reader.Read() == true)
                {
                    Admin admin = new Admin()
                    {
                        ID = (Int64)reader["ID"],
                        UserRepositoryID = (Int64)reader["USER_REPOSITORY_ID"],
                        FirstName = reader["FIRST_NAME"].ToString(),
                        LastName = reader["LAST_NAME"].ToString()


                    };
                    return admin;


                }


                return null;
            }
        }
        #endregion


    }
}
