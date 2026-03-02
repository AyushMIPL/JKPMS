using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.Configuration
{
    public class Settings
    {
        public static string IFXConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["Databases"].ToString();
                //ConfigurationSettings.JKPSConfiguration objConfiguration = new ConfigurationSettings.JKPSConfiguration();
                //objConfiguration.Credentials = new System.Net.NetworkCredential("Administrator", "12345678");
                //return objConfiguration.IFXConnectionString();
            }
        }

        public static string SQLConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["Databases"].ToString();
                //ConfigurationSettings.JKPSConfiguration objConfiguration = new ConfigurationSettings.JKPSConfiguration();
                //objConfiguration.Credentials = new System.Net.NetworkCredential("Administrator", "12345678");
                //return objConfiguration.SQLConnectionString();
            }
        }
    }
}
