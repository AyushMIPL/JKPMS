using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
namespace JKPS.BLL
{
    /// <summary>
    /// Implemented by :chandra
    /// Date : 07/08/2008
    /// Description : This class basically used for interacting with Data Access Layer and gets the data requested from Form frmLedgerAccounts.  
    /// Modified by:chandra
    /// Modified Date :07/11/2008
    /// Description :
    /// </summary>
    public class BLLGeneralLedger 
    {
        /// <summary>
        /// Calling data layer's get Data
        /// Passing object array build from the creteria captured in the winForm
        /// Also passing type of LedgerAccount so that stored procedure hard Coded as property picked up from Common Layer
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOGeneralLedger Defined in the common Layer</returns>
        public static List<DVOGeneralLedger> GetLedgerAccounts(DVOGeneralLedger pDVOGeneralLedger)
        {
            object[] parameters = new object[7];
            parameters[0] = pDVOGeneralLedger.acct_type;
            parameters[1] = pDVOGeneralLedger.acct_desc;
            parameters[2] = pDVOGeneralLedger.incr_with_crdt;
            parameters[3] = pDVOGeneralLedger.subtotal_group;
            parameters[4] = pDVOGeneralLedger.keyvalue;
            parameters[5] = pDVOGeneralLedger.acct_no;
            parameters[6] = pDVOGeneralLedger.acct_cat;

            //parameters[7] = pDVOGeneralLedger.gobzero;
            //parameters[8] = pDVOGeneralLedger.active;

            List<DVOGeneralLedger> lstDVOGeneralLedger = new List<DVOGeneralLedger>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOGeneralLedger)))
              {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
                        objDVOGeneralLedger.acct_no = Convert.ToInt32(dr[0]);//"acct_no"
                        objDVOGeneralLedger.acct_type = Convert.ToString(dr[1]);//"acct_type"
                        objDVOGeneralLedger.acct_desc = Convert.ToString(dr[2]);//"acct_desc"
                        objDVOGeneralLedger.acct_cat = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//"acct_cat"
                        objDVOGeneralLedger.subtotal_group = Convert.ToString(dr[6]);//"subtotal_group"
                        objDVOGeneralLedger.incr_with_crdt = Convert.ToString(dr[5]);//"incr_with_crdt"
                        objDVOGeneralLedger.keyvalue = Convert.ToString(dr[7]);//"keyvalue"
                        objDVOGeneralLedger.acct_type_id = (dr[9] != DBNull.Value) ? Convert.ToInt32(dr[9]) : 0;//"v_acct_type_id"
                        objDVOGeneralLedger.gobzero = (dr[10] != DBNull.Value) ? Convert.ToInt32(dr[10]) : 0;
                        objDVOGeneralLedger.active  = (dr[11] != DBNull.Value) ? Convert.ToInt32(dr[11]) : 0;
                        lstDVOGeneralLedger.Add(objDVOGeneralLedger);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Exception to be handled
            }
            return lstDVOGeneralLedger;
        }
        /// <summary>
        /// Calling data layer's get All Data
        /// Passing no Parameters since we are accessing all the data
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOGeneralLedger Defined in the common Layer</returns>
        public static List<DVOGeneralLedger> GetAllLedgerAccounts()
        {
                List<DVOGeneralLedger> lstDVOGeneralLedger = new List<DVOGeneralLedger>();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                try
                {
                    using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOGeneralLedger)))
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
                            objDVOGeneralLedger.acct_no = Convert.ToInt32(dr[0]);//"acct_no"
                            objDVOGeneralLedger.acct_type = Convert.ToString(dr[1]);//"acct_type"
                            objDVOGeneralLedger.acct_desc = Convert.ToString(dr[2]);//"acct_desc"
                            objDVOGeneralLedger.acct_cat = (dr[3] != DBNull.Value) ? dr[3].ToString().Trim() : string.Empty;//"acct_cat"
                            objDVOGeneralLedger.subtotal_group = Convert.ToString(dr[6]);//"subtotal_group"
                            objDVOGeneralLedger.incr_with_crdt = Convert.ToString(dr[5]);//"incr_with_crdt"
                            objDVOGeneralLedger.keyvalue = Convert.ToString(dr[7]);//"keyvalue"
                            objDVOGeneralLedger.acct_type_id = (dr[9] != DBNull.Value) ? Convert.ToInt32(dr[9]) : 0;//"v_acct_type_id"
                            lstDVOGeneralLedger.Add(objDVOGeneralLedger);
                        }
                    }
                }
                catch (Exception ex)
                {
                //Exception to be handled
                }
            return lstDVOGeneralLedger;
        }
        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOGeneral Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static int InsertLedgerAccounts(ref DVOGeneralLedger objDVOGeneralLedger, out int NewAccountNo, out string AccountDescription)
        {
            int Success = 0;
            NewAccountNo = 0;
            AccountDescription = string.Empty;
            object[] parameters = new object[11];
            parameters[0] = objDVOGeneralLedger.acct_type;
            parameters[1] = objDVOGeneralLedger.acct_desc;
            parameters[2] = objDVOGeneralLedger.acct_cat;
            parameters[3] = objDVOGeneralLedger.processing_seq;
            parameters[4] = objDVOGeneralLedger.subtotal_group;
            parameters[5] = objDVOGeneralLedger.incr_with_crdt;
            parameters[6] = objDVOGeneralLedger.keyvalue;
            parameters[7] = objDVOGeneralLedger.gobzero;
            parameters[8] = objDVOGeneralLedger.active;
            parameters[9] = DVOApplicationUserInfo.UserId;
            parameters[10] = DVOApplicationUserInfo.MachineInfo;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                using (DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, objDVOGeneralLedger.GetType()))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Success = ds.Tables[0].Rows.Count;
                            NewAccountNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"V_NEWACCOUNT"
                            AccountDescription = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString().Trim() : string.Empty;//p_acct_desc
                        }
                }
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                objTransaction = null;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);               
                objTransaction = null;
                throw ex;
                Success = 0;
                //Exception to be handled
            }
            return Success;
        }


        public static int CreateLedgerAccounts(ref DVOGeneralLedger objDVOGeneralLedger, out int NewAccountNo, out string AccountDescription)
        {
            int Success = 0;
            NewAccountNo = 0;
            AccountDescription = string.Empty;
            object[] parameters = new object[2];
            parameters[0] = objDVOGeneralLedger.acct_type;            
            parameters[1] = objDVOGeneralLedger.keyvalue;
           
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                using (DataSet ds = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, objDVOGeneralLedger.Create_GLAccount))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Success = ds.Tables[0].Rows.Count;
                            NewAccountNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"V_NEWACCOUNT"
                            AccountDescription = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString().Trim() : string.Empty;//p_acct_desc
                        }
                }
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                objTransaction = null;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                objTransaction = null;
                throw ex;
                Success = 0;
                //Exception to be handled
            }
            return Success;
        }
        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOGeneral Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static object DeleteLedgerAccounts(ref DVOGeneralLedger objDVOGeneralLedger)
        {
            object obj = null;
            object[] parameters = new object[1];
            parameters[0]=objDVOGeneralLedger.acct_no;
            //parameters[1]=objDVOGeneralLedger.UpdateBy;
            //parameters[2]=objDVOGeneralLedger.UpdateMachineInfo;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                obj = objDALBaseClass.DeleteData(ref parameters,typeof(DVOGeneralLedger),true);
            }
            catch (Exception ex)
            {
                throw ex;
                //Exception to be handled
            }
            return obj;
        }
        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOGeneral Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static object UpdateLedgerAccounts(ref object objTransaction, ref DVOGeneralLedger objDVOGeneralLedger)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                object obj = null;
            try
            {
                object[] parameters = new object[8];
             
                parameters[0] = objDVOGeneralLedger.acct_type;
                parameters[1] = objDVOGeneralLedger.acct_desc;
                parameters[2] = objDVOGeneralLedger.subtotal_group;
                parameters[3] = objDVOGeneralLedger.incr_with_crdt;
                parameters[4] = objDVOGeneralLedger.keyvalue;
                parameters[5] = objDVOGeneralLedger.acct_no;
                parameters[6] = objDVOGeneralLedger.gobzero;
                parameters[7] = objDVOGeneralLedger.active;


                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction,ref parameters, objDVOGeneralLedger.GetType(),true);
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return obj;
        }

        public static int CountAccounts_ofAccountTypeId(int AccountTypeId)
        {
            int _count = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = AccountTypeId;
                object o = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOGeneralLedger()).COUNT_ACCOUNTS_OF_ACCOUNTTYPEID);
                if (o != null)
                    _count = Convert.ToInt32(o);
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            return _count;
        }

        public static void GetAccountInfoForAccountTextBox(DVOGeneralLedger pDVOGeneralLedger, out int AccountNo, out string AccountDescription, out int CountMatch, out int BudgetCountMatch, out int GoBelowZero,out int Active)//
        {
            AccountNo = 0;
            AccountDescription = string.Empty;
            CountMatch = 0;
            BudgetCountMatch = 0;
            GoBelowZero = 0;
            Active = 0;

            object[] parameters = new object[4];
            parameters[0] = pDVOGeneralLedger.acct_type;
            //parameters[1] = pDVOGeneralLedger.acct_desc;
            //parameters[2] = pDVOGeneralLedger.incr_with_crdt;
            //parameters[3] = pDVOGeneralLedger.subtotal_group;
            parameters[1] = pDVOGeneralLedger.keyvalue;
            //parameters[5] = pDVOGeneralLedger.acct_no;
            parameters[2] = pDVOGeneralLedger.acct_cat;
            parameters[3] = pDVOGeneralLedger.keyvalue.Replace("#", "?");
            //List<DVOGeneralLedger> lstDVOGeneralLedger = new List<DVOGeneralLedger>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, pDVOGeneralLedger.GetType(), pDVOGeneralLedger.ACCOUNT_INFO_FOR_ACCOUNT_TEXT_BOX))
                {
                    if(ds!=null)
                        if(ds.Tables.Count>0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                CountMatch = ds.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;
                                BudgetCountMatch = ds.Tables[0].Rows[0][1] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                                AccountNo = ds.Tables[0].Rows[0][2] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][2]) : 0;
                                AccountDescription = ds.Tables[0].Rows[0][3] != DBNull.Value ? ds.Tables[0].Rows[0][3].ToString().Trim() : string.Empty;
                                GoBelowZero = ds.Tables[0].Rows[0][4] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][4]) : 0;
                                Active = ds.Tables[0].Rows[0][5] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0][5]) : 0;
                            }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            //return lstDVOGeneralLedger;
        }

        public static int CHK_KEYVALUE(string keyvalue)
        {
            int _count = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = keyvalue;
                object o = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOGeneralLedger()).COUNT_KEYVALUE);
                if (o != null)
                    _count = Convert.ToInt32 (o);
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            return _count;
        }
    }
}
