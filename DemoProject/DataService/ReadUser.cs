using DemoProject.BusinessService;
using DemoProject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.DataService
{
    /// <summary>
    /// This class provide read methods of users
    /// </summary>
    public class ReadUser : StandardDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">DataController object</param>
        public ReadUser(DataController data)
            : base(data)
        {
        }

        /// <summary>This class return user object based on userID</summary>
        /// <param name="userID">int</param>
        /// <returns>User object</returns>
        public static UserData ReadSpecificUser(int userID)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConnectionString.GetConnectionString());
            SqlCommand myCommand = new SqlCommand("spGetSpecificUser", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter param = new SqlParameter("@intUserID", SqlDbType.Int, 4);
            param.Value = userID;
            myCommand.Parameters.Add(param);

            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            UserData data = null;

            try
            {
                if (myReader.Read())
                {
                    data = new UserData();
                    data.UserID = (int)myReader["intUserID"];
                    data.UserName = (string)myReader["strUserName"];
                    data.Password = (string)myReader["strPassword"];
                    data.AuthKey = (Guid)myReader["strAuthKey"];
                }
            }
            finally
            {
                if (myReader != null) myReader.Close();
            }
            return data;
        }

        /// <summary>This class return user object based on userID</summary>
        /// <param name="userID">int</param>
        /// <returns>User object</returns>
        public static UserData ReadSpecificUserByUserName(string userName, string password)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = new SqlConnection(ConnectionString.GetConnectionString());
            SqlCommand myCommand = new SqlCommand("spGetSpecificUserByUserName", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter paramUserName = new SqlParameter("@strUserName", SqlDbType.NVarChar, 20);
            paramUserName.Value = userName;
            myCommand.Parameters.Add(paramUserName);

            SqlParameter paramPassword = new SqlParameter("@strPassword", SqlDbType.NVarChar, 20);
            paramPassword.Value = password;
            myCommand.Parameters.Add(paramPassword);

            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            UserData data = null;

            try
            {
                if (myReader.Read())
                {
                    data = new UserData();
                    data.UserID = (int)myReader["intUserID"];
                    data.UserName = (string)myReader["strUserName"];
                    data.Password = (string)myReader["strPassword"];
                    data.AuthKey = (Guid)myReader["strAuthKey"];
                }
            }
            finally
            {
                if (myReader != null) myReader.Close();
            }
            return data;
        }
    }
}
