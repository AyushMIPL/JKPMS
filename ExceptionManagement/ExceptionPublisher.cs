using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Mail;
using System.IO;
using ExceptionManagement;

namespace ExceptionManagement
{
	public class ExceptionPublisher : IExceptionPublisher
	{
		private string m_LogName = @"C:\ErrorLog.txt";
		private string m_OpMail = "";

		public ExceptionPublisher()
		{
		}
		// Provide implementation of the IPublishException interface
		// This contains the single Publish method.
		void IExceptionPublisher.Publish(Exception exception, NameValueCollection AdditionalInfo, NameValueCollection ConfigSettings)
		{
			// Load Config values if they are provided.
			if (ConfigSettings != null)
			{
				if (ConfigSettings["fileName"] != null &&  
					ConfigSettings["fileName"].Length > 0)
				{  
					m_LogName = ConfigSettings["fileName"];
				}
				if (ConfigSettings["operatorMail"] !=null && 
					ConfigSettings["operatorMail"].Length > 0)
				{
					m_OpMail = ConfigSettings["operatorMail"];
				}
			}
			// Create StringBuilder to maintain publishing information.
			StringBuilder strInfo = new StringBuilder();

			// Record the contents of the AdditionalInfo collection.
			if (AdditionalInfo != null)
			{
				// Record General information.
				strInfo.AppendFormat("{0}General Information{0}", Environment.NewLine);
				strInfo.AppendFormat("{0}Additonal Info:", Environment.NewLine);
				foreach (string i in AdditionalInfo)
				{
					strInfo.AppendFormat("{0}{1}: {2}", Environment.NewLine, i, AdditionalInfo.Get(i));
				}
			}
			// Append the exception text
			strInfo.AppendFormat("{0}{0}Exception Information{0}{1}", Environment.NewLine, exception.ToString());

			// Write the entry to the log file.   
			using ( FileStream fs = File.Open(m_LogName,
						FileMode.Create,FileAccess.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(strInfo.ToString());
				}
			}
			// send notification email if operatorMail attribute was provided
			if (m_OpMail.Length > 0)
			{
				string subject = "Exception Notification";
				string body = strInfo.ToString();
                SmtpClient SmtpMail = new SmtpClient();
				SmtpMail.Send("CustomPublisher@mycompany.com", m_OpMail, subject, body);
			}
		}
	}
}
