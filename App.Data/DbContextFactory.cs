using System.Configuration;
using System.Web;

namespace App.Data
{
    public class DbContextFactory
    {
        public static AppDbContext CreateDbContext()
        {
            var connectionString = GetConnectionStringForRegion();
            return new AppDbContext(connectionString);
        }

        private static string GetConnectionStringForRegion() 
        {
            // Get the selected region from session
            var region = HttpContext.Current.Session["RegionName"] as string;

            // Default to a fallback connection string if region is not set
            if (string.IsNullOrEmpty(region))
            {
                return ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
            }

            // Construct the connection string based on the region
            switch (region)
            {
                case "KASHMIR REGION":
                    return ConfigurationManager.ConnectionStrings["AppConnection1"].ConnectionString;
                case "JAMMU REGION":
                    return ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
                default:
                    return ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
            }
        }
    }
}