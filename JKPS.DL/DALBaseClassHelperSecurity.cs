using System;
using System.Collections.Generic;
using System.Text;

//using IBM.Data.Informix;
using System.Data;
using System.Data.SqlClient;
using JKPS.COMMON;

namespace JKPS.DL
{
    public class DALBaseClassHelperSecurity
    {
        private static bool SQL = false, IFX = false;
        private SqlConnection sconn = null;
        //private IfxConnection iconn = null;

        public enum ActiveDatabaseTypes
        {
            SQL = 1,
            INFORMIX = 2
        }

        public static ActiveDatabaseTypes ActiveDatabaseType
        {
           
            set
            {
                if (value == ActiveDatabaseTypes.SQL)
                {
                    SQL = true; IFX = false;
                    try
                    {
                        _ActiveConnectionString = JKPS.Configuration.Settings.SQLConnectionString;
                    }
                    catch { _ActiveConnectionString = System.Configuration.ConfigurationSettings.AppSettings["Databases"].ToString(); }
                }
                else if (value == ActiveDatabaseTypes.INFORMIX)
                {
                    IFX = true; SQL = false;
                    try
                    {
                        _ActiveConnectionString = JKPS.Configuration.Settings.IFXConnectionString;
                    }
                    catch { _ActiveConnectionString = System.Configuration.ConfigurationSettings.AppSettings["Databases"].ToString(); }
                }
            }
        }

        private static string _ActiveConnectionString = string.Empty;
        public static string ActiveConnectionString
        {
           
            set { _ActiveConnectionString = value; }
            
            get { return _ActiveConnectionString; }
        }

        public DALBaseClassHelperSecurity()
        {
            SQL = true;
            if (SQL == false && IFX == false)
            {
                try
                {
                    if (System.Configuration.ConfigurationSettings.AppSettings["DataProvider"].Trim().ToUpper() == "SQL")
                    {
                        SQL = true;
                    }
                    else if (System.Configuration.ConfigurationSettings.AppSettings["DataProvider"].Trim().ToUpper() == "IFX")
                    {
                        IFX = true;
                    }
                }
                catch { }
            }
           
        }

        public DALBaseClass GetDAL()
        {
            Type type = null;
            SQL = true;
            if (SQL)
            {
                type = Type.GetType("JKPS.DL.SqlHelper", true);
            }
            else if (IFX)
            {
                type = Type.GetType("JKPS.DL.IfxHelper", true);
            }
            DALBaseClass returnObject = (DALBaseClass)Activator.CreateInstance(type);
            if (_ActiveConnectionString == string.Empty)
              _ActiveConnectionString = System.Configuration.ConfigurationSettings.AppSettings["SQLConn"].ToString();
                
                returnObject.ConnectionString = _ActiveConnectionString;
            

            return returnObject;
        }

        public object GetTransactionObject()
        {
            object objTrans = new object();
            if (_ActiveConnectionString == string.Empty)
              _ActiveConnectionString = System.Configuration.ConfigurationSettings.AppSettings["SQLConn"].ToString();
          
            if (SQL)
            {
                sconn = new SqlConnection(_ActiveConnectionString);
                sconn.Open();
                SqlTransaction strans = sconn.BeginTransaction();
                objTrans = strans;

            }
            else if (IFX)
            {
              sconn = new SqlConnection(_ActiveConnectionString);
              sconn.Open();
              SqlTransaction strans = sconn.BeginTransaction(IsolationLevel.ReadUncommitted);
              objTrans = strans;
                //iconn = new IfxConnection(_ActiveConnectionString);
                //if(iconn.State==ConnectionState.Closed)
                //iconn.Open();
                //IfxTransaction itrans = iconn.BeginTransaction();
                //objTrans = itrans;
            }
            return objTrans;
        }

        public object GetUncommittedTransactionObject()
        {
            object objTrans = null;
            if (_ActiveConnectionString == string.Empty)
                _ActiveConnectionString = System.Configuration.ConfigurationSettings.AppSettings["SQLConn"].ToString();
          
            if (SQL)
            {
                sconn = new SqlConnection(_ActiveConnectionString);
                sconn.Open();
                SqlTransaction strans = sconn.BeginTransaction(IsolationLevel.ReadUncommitted);
                
                objTrans = strans;

            }
            else if (IFX)
            {
              sconn = new SqlConnection(_ActiveConnectionString);
              sconn.Open();
              SqlTransaction strans = sconn.BeginTransaction(IsolationLevel.ReadUncommitted);
              objTrans = strans;
                //iconn = new IfxConnection(_ActiveConnectionString);
                //iconn.Open();
                //IfxTransaction itrans = iconn.BeginTransaction(IsolationLevel.ReadUncommitted);
                //objTrans = itrans;
            }
            return objTrans;
        }

        public void CommitTransaction(ref Object TransactionObject)
        {
            if (SQL)
            {
                ((System.Data.SqlClient.SqlTransaction)TransactionObject).Commit();
            }
            //else if (IFX)
            //{
            //    ((IBM.Data.Informix.IfxTransaction)TransactionObject).Commit();
            //}
            TransactionObject = null;

            if (sconn != null)
            {
                if (sconn.State != ConnectionState.Closed)
                {
                    sconn.Close();
                    sconn.Dispose();
                }
            }
            //if (iconn != null)
            //{
            //    if (iconn.State != ConnectionState.Closed)
            //    {
            //        iconn.Close();
            //        iconn.Dispose();
            //    }
            //}
        }

        public void RollbackTransaction(ref Object TransactionObject)
        {
            if (SQL)
            {
                ((System.Data.SqlClient.SqlTransaction)TransactionObject).Rollback();
            }
            //else if (IFX)
            //{
            //    ((IBM.Data.Informix.IfxTransaction)TransactionObject).Rollback();
            //}
            TransactionObject = null;
           
            //if (iconn != null)
            //{
            //    if (iconn.State != ConnectionState.Closed)
            //    {
            //        iconn.Close();
            //        iconn.Dispose();
            //    }
            //}
            if (sconn != null)
            {
                if (sconn.State != ConnectionState.Closed)
                {
                    sconn.Close();
                    sconn.Dispose();
                }
            }
        }
    }
}
