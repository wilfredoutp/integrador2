using System.Configuration;

namespace WILF.DA
{
    public static class DatabaseHelper
    {
        public const string ConexionData = "WilfredoDatabase";
        public static string GetDbConnectionString(string ConnectionString)
        {
            return ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
        }

        public static string GetDbProvider(string ConnectionString)
        {
            //string ss = ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
            return ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
        }
    }
}
