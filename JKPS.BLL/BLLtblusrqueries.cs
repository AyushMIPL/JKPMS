using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
namespace JKPS.BLL
{
    public class BLLtblusrqueries
    {
        public static int InsertQuery(ref object TransactionObject, ref DVOtblusrqueries objDVOtblusrqueries, out int newQueryID)
        {
            newQueryID = 0;
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            bool statusObjTransaction = true;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            try
            {
                object[] InsParameter = new object[6];
                InsParameter[0] = objDVOtblusrqueries.userid;
                InsParameter[1] = objDVOtblusrqueries.query;
                InsParameter[2] = objDVOtblusrqueries.qname;
                InsParameter[3] = objDVOtblusrqueries.category;
                InsParameter[4] = objDVOtblusrqueries.insertby;
                InsParameter[5] = objDVOtblusrqueries.insertmachineinfo;

                using (DataSet ds = objDalBaseClass.InsertAndGetData_ByTransaction(ref TransactionObject, ref InsParameter, objDVOtblusrqueries.GetType()))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value && dr[1]!=DBNull.Value && Convert.ToInt32( dr[0])==1 && Convert.ToInt32(dr[1])>0)
                        {
                            newQueryID = Convert.ToInt32(dr[1]);
                        }
                        else
                            throw new Exception();
                    }
                }

                //object obj = objDalBaseClass.InsertData_ByTransaction(ref TransactionObject, ref InsParameter, typeof(DVOtblusrqueries), true);
                //if (obj == null)
                //    throw new Exception();
                //else if (Convert.ToInt32(obj) < 1)
                //    throw new Exception();

                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                if (TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            //return success;
        }

        public static int UpdateQuery(ref object TransactionObject, ref DVOtblusrqueries objDVOtblusrqueries)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            int success = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[7];
                parameters[0] = objDVOtblusrqueries.qid;
                parameters[1] = objDVOtblusrqueries.userid;
                parameters[2] = objDVOtblusrqueries.query;
                parameters[3] = objDVOtblusrqueries.qname;
                parameters[4] = objDVOtblusrqueries.category;
                parameters[5] = objDVOtblusrqueries.updateby;
                parameters[6] = objDVOtblusrqueries.updatemachineinfo;

                object obj = objDalBaseClass.UpdateData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOtblusrqueries), true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();

                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            //return success;
        }
        public static int DeleteQuery(ref object TransactionObject, ref DVOtblusrqueries objDVOtblusrqueries)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            int success = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOtblusrqueries.qid;
                object obj = objDalBaseClass.DeleteData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOtblusrqueries), true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            //return success;
        }

        public static List<DVOtblusrqueries> GetQuery(ref DVOtblusrqueries objDVOtblusrqueries)
        {
            object[] parameters = new object[5];
            parameters[0] = objDVOtblusrqueries.qid;
            parameters[1] = objDVOtblusrqueries.userid;
            parameters[2] = objDVOtblusrqueries.query;
            parameters[3] = objDVOtblusrqueries.qname;
            parameters[4] = objDVOtblusrqueries.category;

            List<DVOtblusrqueries> ListDVOtblusrqueries = new List<DVOtblusrqueries>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOtblusrqueries)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOtblusrqueries tempDVOtblusrqueries = new DVOtblusrqueries();
                        tempDVOtblusrqueries.qid = dr["qid"] != DBNull.Value ? Convert.ToInt32(dr["qid"]) : 0;
                        tempDVOtblusrqueries.userid = dr["userid"] != DBNull.Value ? Convert.ToInt32(dr["userid"]) : 0;
                        tempDVOtblusrqueries.query = dr["query"] != DBNull.Value ? dr["query"].ToString().Trim() : string.Empty;
                        tempDVOtblusrqueries.qname = dr["qname"] != DBNull.Value ? dr["qname"].ToString().Trim() : string.Empty;
                        tempDVOtblusrqueries.category = dr["category"] != DBNull.Value ? dr["category"].ToString().Trim() : string.Empty;
                        tempDVOtblusrqueries.rowid = dr["rowid"] != DBNull.Value ? Convert.ToInt32(dr["rowid"]) : 0;
                        ListDVOtblusrqueries.Add(tempDVOtblusrqueries);
                    }
                }
                parameters = null;
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return ListDVOtblusrqueries;
        }
    }
}
