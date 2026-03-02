using System;
using System.Collections.Generic;
using System.Text;

//using IBM.Data.Informix;
using System.Data;
using System.Data.SqlClient;
using JKPS.COMMON;
using App.Data;

namespace JKPS.DL
{
    public class DALBaseClassHelper
    {
        ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();

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
                }
                else if (value == ActiveDatabaseTypes.INFORMIX)
                {
                    IFX = true; SQL = false;
                }
            }
        }

        private static string _ActiveConnectionString = string.Empty;
        public string ActiveConnectionString
        {
            set { _ActiveConnectionString = value; }
            get { return ConnectionStringProvider.GetConnectionString(); }
        }

        public DALBaseClassHelper()
        {
            //if (SQL == false && IFX == false)
            //{
            //    try
            //    {
            //        if (System.Configuration.ConfigurationSettings.AppSettings["DataProvider"].Trim().ToUpper() == "SQL")
            //        {
            //            SQL = true;
            //        }
            //        else if (System.Configuration.ConfigurationSettings.AppSettings["DataProvider"].Trim().ToUpper() == "IFX")
            //        {
            //            IFX = true;
            //        }
            //    }
            //    catch { }
            //}
        }

        public DALBaseClass GetDAL()
        {
            SQL = true;
            Type type = null;
            if (SQL)
            {
                type = Type.GetType("JKPS.DL.SqlHelper", true);
            }
            else if (IFX)
            {
                type = Type.GetType("JKPS.DL.IfxHelper", true);
            }
            DALBaseClass returnObject = (DALBaseClass)Activator.CreateInstance(type);
            if (ConnectionStringProvider.GetConnectionString() != string.Empty)
                returnObject.ConnectionString = ConnectionStringProvider.GetConnectionString();

            return returnObject;
        }

        public DALBaseClass GetDALByRegion(string region)
        {
            SQL = true;
            Type type = null;
            if (SQL)
            {
                type = Type.GetType("JKPS.DL.SqlHelper", true);
            }
            else if (IFX)
            {
                type = Type.GetType("JKPS.DL.IfxHelper", true);
            }
            DALBaseClass returnObject = (DALBaseClass)Activator.CreateInstance(type);

            returnObject.ConnectionString = ConnectionStringProvider.GetConnectionStringApi(region);

            return returnObject;
        }

        public object GetTransactionObject()
        {
            if (_ActiveConnectionString == string.Empty)
                _ActiveConnectionString = ConnectionStringProvider.GetConnectionString();

            object objTrans = new object();
            if (SQL)
            {
                sconn = new SqlConnection(ConnectionStringProvider.GetConnectionString());
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
                //if(iconn.State==ConnectionState.Closed)
                //iconn.Open();
                //IfxTransaction itrans = iconn.BeginTransaction(IsolationLevel.ReadUncommitted);
                //objTrans = itrans;
            }
            return objTrans;
        }

        public object GetUncommittedTransactionObject()
        {

            //_ActiveConnectionString = "Host=192.0.0.151;Service=sqlexec_fitrix;Database=bk2005;Server=se_fitrix;User ID=admin;Pwd=cr8z4i;";
            object objTrans = null;
            if (_ActiveConnectionString == string.Empty)
                _ActiveConnectionString = ConnectionStringProvider.GetConnectionString();      //System.Configuration.ConfigurationSettings.AppSettings["SQLConn"].ToString();

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
            // iconn = null;
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
