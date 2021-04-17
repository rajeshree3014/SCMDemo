using System;
using System.Configuration;

namespace DemoProject.Common
{
    [Serializable()]
    public static class ConnectionString
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        }
    }
}
