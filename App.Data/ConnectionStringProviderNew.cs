using System.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace App.Data
{

    public class ConnectionStringProviderNew
    {
        public static string GetConnectionString(HttpContextBase context)
        {
            string ConnectionName = string.Empty;
            var connection = context.Session["ConnectionName"];
            if (connection != null)
            {
                ConnectionName = context.Session["ConnectionName"] as string ?? "AppConnection";
            }

            if (string.IsNullOrEmpty(ConnectionName))
            {
                ConnectionName = "AppConnection";
            }

            return ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
        }
    }


}
