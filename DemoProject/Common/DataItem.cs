using DemoProject.Interface;
using System;

namespace DemoProject.Common
{
    /// <summary>
    /// Inherit from this if you need to provide database functionality
    /// </summary>
    [Serializable()]
    public abstract class DataItem : IPersistable
    {
        protected int _loginID = 0; 
        protected int _systemID = 0; 
      
        /// <summary>
        /// Save 
        /// </summary>
        public virtual void Save()
        {
            if (this is IPersistableV2)
            {
                DataController con = new DataController();
                con.StartDatabase(ConnectionString.GetConnectionString());
                try
                {
                    ((IPersistableV2)this).Save(con);
                }
                finally
                {
                    if (con.IsDBStarted) con.EndDatabase(); 
                }
            }
        }
       
    }
}
