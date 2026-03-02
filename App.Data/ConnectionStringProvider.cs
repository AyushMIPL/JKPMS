using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Data
{

    public class ConnectionStringProvider
    {
        public static string ConnectionName = "AppConnection";
        public string GetConnectionString()
        {
            string connection = "AppConnection";
            if (HttpContext.Current !=null && HttpContext.Current.Session != null)
            {
                var Region = HttpContext.Current.Session["RegionName"];
                if (Region != null)
                {
                    string RegionName = HttpContext.Current.Session["RegionName"] as string;
                    if (!string.IsNullOrEmpty(RegionName))
                    {
                        if (RegionName == "KASHMIR REGION")
                            connection = "AppConnection1";
                        else
                            connection = "AppConnection";
                    }
                }
            }
            return ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }

        public string GetConnectionStringApi(string Region)
        {
            string connection = "AppConnection";
            if (!string.IsNullOrEmpty(Region))
            {
                if (Region.Trim().ToUpper() == "KASHMIR REGION" || Region.Trim().ToUpper() == "KASHMIR")
                    connection = "AppConnection1";
                else
                    connection = "AppConnection";
            }
            return ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }
    }
}
