using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace JKPS.BLL
{
  public  class BllTest
    {

        public static Int32 InsertData(ref DVOTest testdvo)
        {
            object[] Parameter = new object[3];
            Parameter[0] = testdvo.text1;
            Parameter[1] = testdvo.text2;
            Parameter[2] = testdvo.text3;
             DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
              DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
             object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            Int32 I;
             //try
             //{
             //    String Connectionstring = System.Configuration.ConfigurationSettings.AppSettings["SQLConn"].ToString();
             //    SqlConnection sconn = new SqlConnection(Connectionstring);
             //    sconn.Open();
             //    SqlTransaction stran = sconn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
             //  I=  SqlHelper.ExecuteScalar(stran,"uspInsertTestTable",Parameter);
             //  if (Convert.ToInt32( I) < 1)
             //  {
             //      stran.Rollback();
             //  }
             //  else
             //  {
             //      stran.Commit();
             //  }
             //}
             //catch(Exception ex)
             //{
             //    throw ex;
             //}
          

             I = objDalBaseClass.InsertData_ByTransaction(ref objTransaction, ref Parameter,typeof(DVOTest));
             
             if (objTransaction != null)
             {
                 objDALBaseClassHelper.CommitTransaction(ref objTransaction);
             }
             return I;
        }
    }
}
