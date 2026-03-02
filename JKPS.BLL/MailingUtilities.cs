#region OlderCode
//Older Code
  using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.ComponentModel;
using System.Windows.Forms;
using JKPS.DL;
using JKPS.COMMON;
namespace JKPS.CommonUtilities
{
    public class MailingUtilities
    {
        public static void SendMails(string strData)
        {
            string strEmails = ConfigurationSettings.AppSettings["emailAddresses"];
            if (strEmails.Length > 0)
            {
                string[] emails = strEmails.Split(Convert.ToChar("|"));
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = false;
                msg.Sender = new MailAddress(emails[0]);
                //for (int i = 1; i < emails.Length; i++)
                //    msg..CC =new MailAddress(emails[i]);
                string fromEmail = ConfigurationSettings.AppSettings["fromEmail"].ToString();
                msg.From = new MailAddress(fromEmail);
                msg.Subject = "Application error!";
                string detailURL =
                msg.Body = strData;// + detailURL + "?EvtId=" + evtId.ToString();
                SmtpClient smtp = new SmtpClient("mail.lelogix.com", 21);
                //System.Configuration.ConfigurationSettings.AppSettings["smtpServer"].ToString();
                try
                {
                    smtp.Send(msg);
                }
                catch (Exception excm)
                {
                    throw;
                }
            }

        }

        public static void SendMailsAttachment(string strData, string attachmentFilename)
        {
            string strEmails = ConfigurationSettings.AppSettings["emailAddresses"];
            if (strEmails.Length > 0)
            {
                string[] emails = strEmails.Split(Convert.ToChar("|"));
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = false;
                msg.Sender = new MailAddress(emails[0]);
                //for (int i = 1; i < emails.Length; i++)
                //    msg..CC =new MailAddress(emails[i]);
                string fromEmail = ConfigurationSettings.AppSettings["fromEmail"].ToString();
                msg.From = new MailAddress(fromEmail);
                msg.Subject = "Application error!";
                string detailURL =
                msg.Body = strData;// + detailURL + "?EvtId=" + evtId.ToString();
                SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["smtpServer"].ToString(), 587);
                smtp.Credentials = new System.Net.NetworkCredential(fromEmail, System.Configuration.ConfigurationSettings.AppSettings["emailPWD"].ToString());
                smtp.EnableSsl = true;
                smtp.Timeout = 3000;
                //System.Configuration.ConfigurationSettings.AppSettings["smtpServer"].ToString();

                if (attachmentFilename != null)
                {
                    //System.Net.Mail.Attachment attachment;
                    //attachment = new System.Net.Mail.Attachment(attachmentFilename);
                    msg.Attachments.Add(new Attachment(attachmentFilename));
                }
                try
                {
                    smtp.Send(msg);
                }
                catch (Exception excm)
                {
                    throw;
                }
            }

        }
    }
}
#endregion OlderCode
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Net.Mail;
//using System.Net;
//using System.Net.Mime;
//using System.Threading;
//using System.Windows.Forms;
//using System.ComponentModel;

//namespace JKPS.BLL.Mail
//{
//    /// <summary>
//    /// Working Mail Utility by Shrishanshu
//    /// </summary>
//    public class SendAutomatedMail
//    {
//        /// <summary>
//        /// Use this Function to PostError Logs at Application Startup.
//        /// </summary>
//        public static void SendErrorLogs()
//        {


//            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
//            msg.To.Add(System.Configuration.ConfigurationSettings.AppSettings["ToMail"]);
//            msg.To.Add(System.Configuration.ConfigurationSettings.AppSettings["ToCCMail"]);
//            msg.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["FromMail"],
//                                       System.Configuration.ConfigurationSettings.AppSettings["FromAlias"],
//                                       System.Text.Encoding.UTF8);
//            msg.Subject = "Error Logs for ";// We can get the user name.
//            msg.SubjectEncoding = System.Text.Encoding.UTF8;
//            msg.Body = "Error Log for JKPS.";
//            msg.BodyEncoding = System.Text.Encoding.UTF8;
//            msg.IsBodyHtml = false;
//            msg.Priority = MailPriority.High;


//            // Add here a function for Selecting Log files .
//           Attachment attch = new Attachment(@"D:\123654.txt");
//            msg.Attachments.Add(attch);

//            SmtpClient client = new SmtpClient();
//            client.Credentials = new System.Net.NetworkCredential
//                                        (System.Configuration.ConfigurationSettings.AppSettings["FromMail"],
//                                         System.Configuration.ConfigurationSettings.AppSettings["FromPass"]);

//            client.Port = 587;
//            client.Host = "smtp.gmail.com";
//            client.EnableSsl = true;
//            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
//            object userState = msg;
//            try
//            {
//                client.SendAsync(msg, userState);
//            }
//            catch (System.Net.Mail.SmtpException ex)
//            {
//                MessageBox.Show(ex.Message, "Send Mail Error");
//            }

//        }
//        public static void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
//        {
//            MailMessage mail = (MailMessage)e.UserState;
//            string subject = mail.Subject;

//            if (e.Cancelled)
//            {
//                string cancelled = string.Format("[{0}] Send canceled.", subject);
//                MessageBox.Show(cancelled);
//            }
//            if (e.Error != null)
//            {
//                string error = String.Format("[{0}] {1}", subject, e.Error.ToString());
//                MessageBox.Show(error);
//            }
//            else
//            {
//                MessageBox.Show("Message sent.");
//            }

//        }
//    }
//}

namespace JKPS.BLL.Mail
{
    //Written by Satvjeet On 16/12/2009
    public class SendMail
    {   
        //Use to send Accounts With Differences txt file.
        public static void SendAccountsList(string filePath,string msgBody,string subjectLine)
           
        {

             //string msgBody,string subjectLine)
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(System.Configuration.ConfigurationSettings.AppSettings["ToMail"]);
            msg.To.Add(System.Configuration.ConfigurationSettings.AppSettings["ToCCMail"]);
            msg.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["FromMail"],
                                       System.Configuration.ConfigurationSettings.AppSettings["FromAlias"],
                                       System.Text.Encoding.UTF8);
            msg.Subject = subjectLine;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = msgBody;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;


            // Add here a function for Selecting Log files .
            Attachment attch = new Attachment(@filePath);
            msg.Attachments.Add(attch);

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential
                                        (System.Configuration.ConfigurationSettings.AppSettings["FromMail"],
                                         System.Configuration.ConfigurationSettings.AppSettings["FromPass"]);

            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            object userState = msg;
            try
            {
                client.SendAsync(msg, userState);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //MessageBox.Show(ex.Message, "Send Mail Error");
            }

        }
        public static void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mail = (MailMessage)e.UserState;
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                string cancelled = string.Format("[{0}] Send canceled.", subject);
                //MessageBox.Show(cancelled);
            }
            if (e.Error != null)
            {
                string error = String.Format("[{0}] {1}", subject, e.Error.ToString());
                //MessageBox.Show(error);
            }
            else
            {
                //MessageBox.Show("Message sent.");
            }

        }
    }
}


