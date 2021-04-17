using DemoProject.Common;
using DemoProject.DataService;
using DemoProject.Interface;
using System;

namespace DemoProject.BusinessService
{
    /// <summary>This class provide User properties and methods
    /// </summary>
    [Serializable()]
    public class UserData : DataItem, IPersistableV2, IPersistable
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public Guid AuthKey { get; set; }

        /// <summary>This method return User object based on userID
        /// </summary>
        /// <param name="userID">int</param>
        /// <returns>User object</returns>
        public static UserData Specific(int userID)
        {
            return ReadUser.ReadSpecificUser(userID);
        }

        /// <summary>
        /// Check if User exists or not for given username and password
        /// </summary>
        /// <returns>bool</returns>
        public static UserData GetUser(string userName, string password)
        {
            return ReadUser.ReadSpecificUserByUserName(userName, password);
        }

        void IPersistable.Save()
        {
            DataController conn = new DataController();
            try
            {
                conn.StartDatabase(ConnectionString.GetConnectionString());
                ((IPersistableV2)this).Save(conn);
            }
            finally
            {
                if (conn.IsDBStarted) conn.EndDatabase();
            }
        }

        /// <summary>This method save Account details with connection
        /// </summary>
        /// <param name="conn">DataController object</param>
        void IPersistableV2.Save(DataController conn)
        {
            try
            {
                WriteUser wr = new WriteUser(conn);
                wr.WriteUserDetails(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving user Auth Key " + this.UserID.ToString(), ex);
            }
        }
    }
}
