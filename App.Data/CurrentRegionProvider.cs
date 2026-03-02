using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Data
{

    public class CurrentRegionProvider
    {
        public string GetCurrentRegion() 
        {
            string RegionName = "KASHMIR REGION";
            try
            {
                var Region = HttpContext.Current.Session["RegionName"];
                if (Region != null)
                {
                    RegionName = HttpContext.Current.Session["RegionName"] as string;
                }
            }
            catch (Exception ex)
            {
            }
            return RegionName;
        }
    }
}
