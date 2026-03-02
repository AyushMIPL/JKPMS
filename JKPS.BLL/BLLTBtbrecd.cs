using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
using JKPS.CommonUtilities;

namespace JKPS.BLL
{
    public class BLLTBtbrecd
    {
        public static decimal GetReceivedAmount(ref DVOTBtbrecd objDVOTBtbrecd)
        {
            decimal TotalreceivedAmt = 0;
            List<DVOTBtbrecd> Listobjtbrecd = new List<DVOTBtbrecd>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objDVOTBtbrecd.tbschid;
                parameters[1] = objDVOTBtbrecd.issue_num;
                parameters[2] = objDVOTBtbrecd.tend_code;

                object obj = objDalBaseClass.ExecuteScalar(ref parameters, objDVOTBtbrecd.GET_RECEIVED_AMT);
                if (obj != null && obj != DBNull.Value)
                    TotalreceivedAmt = Convert.ToDecimal(obj);

                return TotalreceivedAmt;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return TotalreceivedAmt;
        }
        public static List<DVOTBtbrecd> GetDataFromtbrecd(ref DVOTBtbrecd objTBtbrecd)
        {
            List<DVOTBtbrecd> Listobjtbrecd = new List<DVOTBtbrecd>();
            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            try
            {
                object[] parameters = new object[5];
                parameters[0] = objTBtbrecd.Rowid;
                parameters[1] = objTBtbrecd.tbschid;
                parameters[2] = objTBtbrecd.issue_num;
                parameters[3] = objTBtbrecd.tend_code;
                parameters[4] = objTBtbrecd.batch_id;

                IDataReader dr = objDalBaseClass.GetDataByReader(ref parameters, objTBtbrecd.GetType());
                //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOTBtbrecd)))
                //{
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                while(dr.Read())
                    {
                        using (DVOTBtbrecd objTBMaintanance = new DVOTBtbrecd())
                        {
                            //objTBMaintanance.Rowid = (dr["rowid"] != DBNull.Value ? Convert.ToInt32(dr["rowid"]) : 0);
                            //objTBMaintanance.tbr_doc_no = (dr["tbr_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["tbr_doc_no"]) : 0);
                            //objTBMaintanance.tbschid = (dr["tbschid"] != DBNull.Value ? Convert.ToInt32(dr["tbschid"]) : 0);
                            //objTBMaintanance.issue_num = (dr["issue_num"] != DBNull.Value ? Convert.ToInt32(dr["issue_num"]) : 0);
                            //objTBMaintanance.tend_code = (dr["tend_code"] != DBNull.Value ? dr["tend_code"].ToString().Trim() : string.Empty);
                            //objTBMaintanance.amt_rec = (dr["amt_rec"] != DBNull.Value ? Convert.ToDecimal(dr["amt_rec"]) : 0);
                            //objTBMaintanance.amt_bal = (dr["amt_bal"] != DBNull.Value ? Convert.ToInt32(dr["amt_bal"]) : 0);
                            //objTBMaintanance.ar_cr_doc_no = (dr["ar_cr_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["ar_cr_doc_no"]) : 0);
                            //objTBMaintanance.ok_to_post = (dr["ok_to_post"] != DBNull.Value ? dr["ok_to_post"].ToString().Trim() : string.Empty);

                            objTBMaintanance.Rowid = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);
                            objTBMaintanance.tbr_doc_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                            objTBMaintanance.tbschid = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);
                            objTBMaintanance.issue_num = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objTBMaintanance.tend_code = (dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty);
                            objTBMaintanance.amt_rec = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
                            objTBMaintanance.amt_bal = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                            objTBMaintanance.ar_cr_doc_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
                            objTBMaintanance.ok_to_post = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
                            Listobjtbrecd.Add(objTBMaintanance);
                        }
                    }
                //}
                    dr.Close();
                return Listobjtbrecd;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return Listobjtbrecd;
        }

        public static bool InsertIntoTbrecd(ref object objTransaction,ref DVOTBtbrecd objTbrecd , out int NewDocNo)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            NewDocNo = 0;
            try
            {
                object[] parameters = new object[12];
                parameters[0] = objTbrecd.tbr_doc_no;
                parameters[1] = objTbrecd.issue_num;
                parameters[2] = objTbrecd.tend_code;
                parameters[3] = objTbrecd.ar_cr_doc_no;
                parameters[4] = objTbrecd.tbschid;
                parameters[5] = objTbrecd.ok_to_post;
                parameters[6] = objTbrecd.amt_rec;
                parameters[7] = objTbrecd.amt_bal;
                parameters[8] = DVOApplicationUserInfo.UserId;
                parameters[9] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[10] = DVOApplicationUserInfo.MachineInfo;
                parameters[11] = objTbrecd.batch_id;

                //Object o = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOTBtbrecd), true);
                //if (o == null)
                //    throw new Exception();
                //else if (Convert.ToInt32(o) != 1)
                //    throw new Exception();

                DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOTBtbrecd));
                if (ds == null)
                    throw new Exception("Error occured during Insert row in tbrecd.");
                else if (ds.Tables.Count <= 0)
                    throw new Exception("Error occured during Insert row in tbrecd.");
                else if (ds.Tables[0].Rows.Count <= 0)
                    throw new Exception("Error occured during Insert row in tbrecd.");
                else
                {
                    int procStatus = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                    if (procStatus < 1)
                        throw new Exception("Error occured during Insert row in tbrecd.");
                    NewDocNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;
                }
                parameters = null;
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return true;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                //return false;
            }
            return false;
        }

        public static int UpdateReceivedFromTender(ref object TransactionObject, ref DVOTBtbrecd objtbrecd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[5];
                parameters[0] = objtbrecd.Rowid;
                parameters[1] = objtbrecd.amt_rec;
                parameters[2] = objtbrecd.amt_bal;

                parameters[3] = objtbrecd.updateby;
                parameters[4] = objtbrecd.updatemachineinfo;


                object obj = objDalBaseClass.UpdateData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOTBtbrecd), true);
                if (obj == null)
                    throw new Exception("Error occured during Updation row in tbrecd.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured during Updation row in tbrecd.");

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
            return 0;
        }
        
        public static int UpdateARDocNoTbrecd(ref object TransactionObject, ref DVOTBtbrecd objtbrecd)
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
                parameters[0] = objtbrecd.tbr_doc_no;
                parameters[1] = objtbrecd.tbschid;
                parameters[2] = objtbrecd.issue_num;
                parameters[3] = objtbrecd.tend_code;
                parameters[4] = objtbrecd.ar_cr_doc_no;
                parameters[5] = objtbrecd.updateby;
                parameters[6] = objtbrecd.updatemachineinfo;

                object obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameters, objtbrecd.SET_ARDOCNO_TBRECD);
                if (obj == null || obj == DBNull.Value)
                    throw new Exception("Error occured during set ardocno in tbrecd.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured during set ardocno in tbrecd.");

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
            return 0;
        }

        public static int DeleteReceivedFromTenders(ref object TransactionObject, ref DVOTBtbrecd objDVOTBtbrecd)
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
                parameters[0] = objDVOTBtbrecd.Rowid;
                object obj = objDalBaseClass.DeleteData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOTBtbrecd), true);
                if (obj == null)
                    throw new Exception("Error occured during deleting row in tbrecd.");
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception("Error occured during deleting row in tbrecd.");
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
            return 0;
        }
    }
}
