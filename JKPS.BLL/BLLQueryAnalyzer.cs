using System;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using IBM.Data.Informix;
using System.Data;
using System.Diagnostics;
namespace JKPS.BLL
{
    public class BLLQueryAnalyzer
    {
        private  static IfxConnection conn;                     //Connection String for QA
        private static IfxCommand ifxcmd;                         //Command for QA-IFX
        private static string strMessage = string.Empty;         //Message or Error to Show on QA
        private static string strHostConnection = string.Empty;  //Returns the Host Name,connected
        private static Stopwatch stopWatch;
        private static TimeSpan ts;
        private static List<string> lstColName;
        private static List<string> FeildValue;
        
        /// <summary>
        /// Function To Test Connectivity with the DB
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection(out string strStatus,out string strHost)
        {
            strStatus = "Not Connected";
            strHost="0.0.0.0";
            bool res = false;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            if (objDALBaseClass.ConnectionString.Length > 0)
            {
                conn = new IfxConnection(objDALBaseClass.ConnectionString);
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        res = true;
                        strMessage = "Connected";
                        strStatus = strMessage;
                        int ssindex = conn.ConnectionString.Replace(" ", "").IndexOf("Host=");
                        int eindex = conn.ConnectionString.Replace(" ", "").IndexOf(";", ssindex);
                        strHostConnection = conn.ConnectionString.Replace(" ", "").Substring(ssindex + 5, eindex - 5);
                        strHost = strHostConnection;
                    }
                    else
                    {
                        strMessage = "Not Connected";
                        strStatus = strMessage;
                    }
                }
                catch (IfxException ex)
                {
                    strMessage = "Error : " + ex.Message;
                    strStatus = strMessage;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            else
            {
                strMessage = "No Connection Available.";
                strStatus = strMessage;
            }
            return res; 
        }

        public static DataTable GetRecords(string strQuery,out string strMessageOut,out string strTime)
        {
            strMessageOut = string.Empty;
            strTime = string.Empty;

            stopWatch = new Stopwatch();
            stopWatch.Start();
            DataTable dt=new DataTable ();
            if (conn != null)
            {
                if (strQuery.Trim() != string.Empty && strQuery.Length > 0)
                {
                    ifxcmd = new IfxCommand(strQuery.Trim(), conn);
                    try
                    {
                        IfxDataAdapter ifxadp = new IfxDataAdapter();
                        ifxadp.SelectCommand = ifxcmd;
                        DataTable table = new DataTable();
                        table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                        ifxadp.Fill(table);
                        dt = table;
                    }
                    catch (IfxException ex)
                    {
                        strMessage = "Error : " + ex.Message;
                        strMessageOut = strMessage;
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        ts = stopWatch.Elapsed;
                        stopWatch.Stop();
                    }
                   
                }
            }
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            strTime = elapsedTime;
            return dt;
        }

        public static IfxDataReader GetRecordsDR(string strQuery,out List<string> lstcolnameOut,out string strMessageOut )
        {
            lstcolnameOut = null;
            strMessageOut = string.Empty;

            stopWatch = new Stopwatch();
            stopWatch.Start();
            IfxDataReader idr=null ;

            if (conn != null)
            {
                if (strQuery != string.Empty && strQuery.Length > 0)
                {
                    ifxcmd = new IfxCommand(strQuery.Trim(), conn);
                    try
                    {
                        conn.Open();
                        DataTable dtableSchema = new DataTable();
                        
                        idr = ifxcmd.ExecuteReader();
                        dtableSchema = idr.GetSchemaTable();
                        lstColName = new List<string>(dtableSchema.Rows.Count);
                        for (int i = 0; i < dtableSchema.Rows.Count; i++)
                        {
                            lstColName.Add((dtableSchema.Rows[i][0].ToString().Trim()));
                        }

                        lstcolnameOut = lstColName;

                        ts = stopWatch.Elapsed;
                        stopWatch.Stop();
                    }

                    catch (IfxException ex)
                    {
                        strMessage = "Error : " + ex.Message;
                        strMessageOut = strMessage;
                    }

                }
            }
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            return idr;
        }

        public static bool CloseDataReader(ref IfxDataReader idr)
        {
            try
            {
                if (idr != null && (!(idr.IsClosed)))
                {
                    idr.Close();
                    idr = null;
                }
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }
    }
}
