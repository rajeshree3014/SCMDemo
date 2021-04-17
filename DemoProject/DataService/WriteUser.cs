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
    /// This class provide write methods for user
    /// </summary>
	public class WriteUser : StandardDataService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">DataController object</param>
		public WriteUser(DataController data) : base(data)
        {
        }

        /// <summary>This method save User details</summary>
        /// <param name="data">User object</param>
        public void WriteUserDetails(UserData data)
        {
            SqlCommand myCommand = new SqlCommand("spUpdateUserAuthKey");

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC		

            SqlParameter paramAuthKey = new SqlParameter("@strAuthKey", SqlDbType.UniqueIdentifier, 500);
            paramAuthKey.Value = data.AuthKey;
            myCommand.Parameters.Add(paramAuthKey);

            SqlParameter paramUserId = new SqlParameter("@intUserID", SqlDbType.Int, 4);
            paramUserId.Value = data.UserID;
            myCommand.Parameters.Add(paramUserId);

            try
            {
                this.Controller.AddCommand(myCommand);
                myCommand.ExecuteNonQuery();
            }
            finally
            {
                myCommand.Dispose();
            }
        }
    }
}
