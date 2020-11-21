using Main_Project;
using Main_Project.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProjectTester.Properties
{
     public abstract class BaseTest
    {
        protected UserRepository ur = new UserRepository("mainAdmin", "9999", 0);
        protected Admin admin = new Admin("David", "Nohovich", 0);

        #region General test functions
        /// <summary>
        /// remove all existing data from the test data base
        /// </summary>
        [TestInitialize]
        public void RemoveAllDataBaseInfo()
        {
            FlyingCenterConfig.testMode = true;
            using (SqlConnection conn = new SqlConnection(TestConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("REMOVE_ALL_INFO_FROM_DATA_BASE", conn);

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
            
            long newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_USER_TO_USER_REPOSITORY", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", ur.UserName));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", ur.Password));
                cmd.Parameters.Add(new SqlParameter("@USER_ROLE", RolesEnum.admin));
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                newId = (long)cmd.ExecuteScalar();
                ur.ID = newId;
            }
            admin.UserRepositoryID = ur.ID;
            long _newId;
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.ConString))
            {
                SqlCommand cmd = new SqlCommand("ADD_ADMIN_IN_ADMINS", conn);
                cmd.Parameters.Add(new SqlParameter("@USER_REPOSITORY_ID", admin.UserRepositoryID));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", admin.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", admin.LastName));

                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                _newId = (long)cmd.ExecuteScalar();
                admin.ID = _newId;
            }
        }
        #endregion
    }
}
