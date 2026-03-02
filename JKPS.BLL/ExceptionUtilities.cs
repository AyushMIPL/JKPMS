using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Web;
using System.Configuration;

namespace JKPS.BLL
{
    public static class ExceptionUtilities
    {
        static ExceptionUtilities()
        {
        }
        public static void HandleException(Exception ex)
        {
            AppDomain currAppDomain = AppDomain.CurrentDomain;
            string strData = String.Empty;
            int evtId = 0;
            bool logIt = Convert.ToBoolean(ConfigurationSettings.AppSettings["logErrors"]);
            if (logIt)
            {
                strData = "\nSOURCE: " + ex.Source +
                      "\nMESSAGE: " + ex.Message +
                    //"\nFORM: " + sForm +
                    // "\nQUERYSTRING: " + sQuery +
               "\nTARGETSITE: " + ex.TargetSite +
               "\nSTACKTRACE: " + ex.StackTrace;
                //"\nREFERER: " + referer;
                //"usp_WebAppLogsInsert";
                try
                {

                    string x = string.Empty;
                    //cmd.Parameters.Add(new SqlParameter("@Source", ex.Source));
                    //               cmd.Parameters.Add(new SqlParameter("@Message", ex.Message));
                    //               cmd.Parameters.Add(new SqlParameter("@Form", sForm));
                    //               cmd.Parameters.Add(new SqlParameter("@QueryString", sQuery));
                    //               cmd.Parameters.Add(new SqlParameter("@TargetSite", ex.TargetSite.ToString()));
                    //               cmd.Parameters.Add(new SqlParameter("@StackTrace", ex.StackTrace.ToString()));
                    //               cmd.Parameters.Add(new SqlParameter("@Referer", referer));
                    //               SqlParameter outParm = new SqlParameter("@EventId", SqlDbType.Int);
                    //               outParm.Direction = ParameterDirection.Output;
                    //               cmd.Parameters.Add(outParm);
                    //               cmd.ExecuteNonQuery();
                    //               evtId = Convert.ToInt32(cmd.Parameters[7].Value);
                    //               cmd.Dispose();
                    //               cn.Close();

                }
                catch (Exception exc)
                {
                    EventLog.WriteEntry(ex.Source, "Database Error From Exception Log!",
                                          EventLogEntryType.Error, 65535);
                }
                try
                {
                    EventLog.WriteEntry(ex.Source, strData, EventLogEntryType.Error, evtId);
                }
                catch (Exception exl)
                {
                    throw exl;
                }

            }
        }
    }
}
