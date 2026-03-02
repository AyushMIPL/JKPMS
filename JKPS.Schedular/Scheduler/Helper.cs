using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKPS.Schedular.Scheduler
{
  public class Helper
  {
    public static Dictionary<string, string> GetFTPSetting()
    {
      string ftpServerUrl = System.Configuration.ConfigurationSettings.AppSettings.Get("ftpServerUrl");
      string ftpUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("ftpUsername");
      string ftpPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("ftpPassword");
      string localFilePath = System.Configuration.ConfigurationSettings.AppSettings.Get("localFilePath");
      string IsFTP = System.Configuration.ConfigurationSettings.AppSettings.Get("IsFTP");
      string sftpServerUrl = System.Configuration.ConfigurationSettings.AppSettings.Get("sftpServerUrl");
      string sftpUsername = System.Configuration.ConfigurationSettings.AppSettings.Get("sftpUsername");
      string sftpPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("sftpPassword");
      string sftpFilePath = System.Configuration.ConfigurationSettings.AppSettings.Get("sftpFilePath");
      string sftpPort = System.Configuration.ConfigurationSettings.AppSettings.Get("sftpPort");

      Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
      {
          { "ftpServerUrl", ftpServerUrl },
          { "ftpUsername", ftpUsername },
          { "ftpPassword", ftpPassword },
          { "localFilePath", localFilePath },

          { "IsFTP", IsFTP },
          { "sftpServerUrl", sftpServerUrl },
          { "sftpUsername", sftpUsername },
          { "sftpPassword", sftpPassword },
          { "sftpFilePath", sftpFilePath },
          { "sftpPort", sftpPort }

      };

      return keyValuePairs;
    }
  }
}
