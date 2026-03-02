using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace App.Web.Repository
{
  public class PensionAndRefundEmailRepo
  {
    public static void sendMailProcedure(string to, int id, string name, string status)
    {
      string applicant = System.Web.Configuration.WebConfigurationManager.AppSettings["ApplicantMailId"];
      string client = System.Web.Configuration.WebConfigurationManager.AppSettings["ApprovalMailId"];
      string staticMail = System.Web.Configuration.WebConfigurationManager.AppSettings["staticmail"];
      if (staticMail == "Yes")
      {
        if (status.Contains("..."))
        {
          to = client;
        }
        else
        {
          to = applicant;
        }
      }
      if (!string.IsNullOrEmpty(to))
      {
        try
        {
          bool isEmail = Regex.IsMatch(to, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
          if (isEmail)
          {
            dynamic emails = new Email("PensionApproval");
            emails.To = to;
            //emails.To = to;
            emails.Name = name;
            emails.id = id;
            emails.Status = status;
            emails.Send();
          }
        }
        catch (Exception ex)
        {

        }
      }
    }

    public static void sendMailProcedureRefund(string to, int id, string name, string status)
    {
      string applicant = System.Web.Configuration.WebConfigurationManager.AppSettings["ApplicantMailId"];
      string client = System.Web.Configuration.WebConfigurationManager.AppSettings["ApprovalMailId"];
      string staticMail = System.Web.Configuration.WebConfigurationManager.AppSettings["staticmail"];
      if (staticMail == "Yes")
      {
        if (status.Contains("..."))
        {
          to = client;
        }
        else
        {
          to = applicant;
        }
      }
      if (!string.IsNullOrEmpty(to))
      {
        try
        {
          bool isEmail = Regex.IsMatch(to, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
          if (isEmail)
          {
            dynamic emails = new Email("RefundApproval");
            emails.To = to;
            //emails.To = to;
            emails.Name = name;
            emails.id = id;
            emails.Status = status;
            emails.Send();
          }
        }
        catch (Exception ex)
        {

        }
      }
    }
  }
}