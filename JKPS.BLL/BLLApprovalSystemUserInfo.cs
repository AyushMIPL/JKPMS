using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
using System.Reflection;
using System.Diagnostics;
namespace JKPS.BLL
{
    /// <summary>
    /// Implemented by :chandra
    /// Date : 18/08/2008
    /// Description : This  class basically used for interacting with Data Access Layer and gets the data requested from Form frmApprovalSystemUserInfo.  
    /// Modified by:chandra
    /// Modified Date :
    /// Description :
    /// </summary>
    public class BLLApprovalSystemUserInfo
    {

        static BLLApprovalSystemUserInfo()
        {
            objTransaction = null;
            AllSuccessFullyLocked = true;
            AllRecordLocksReleased = true;
        }
        public static bool isAllLocksReleased(ref List<DVOLocks> lstDVOLocks)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            foreach (DVOLocks obj in lstDVOLocks)
            {
                if (!isLockReleasedIndividual(obj.TableName, obj.KeyFieldName, obj.KeyFieldValue, obj.GetType(), ref objDalBaseClass))
                {
                    AllRecordLocksReleased = false;
                    break;
                }
            }
            return AllRecordLocksReleased;
        }
        private static bool isLockReleasedIndividual(string pTableName, string pKeyFieldName, object pKeyFieldValue, Type pType, ref DALBaseClass objDalBaseClass)
        {
            bool isStillLocked = false;
            object[] parameters = new object[6];
            parameters[0] = DBNull.Value;
            parameters[1] = DBNull.Value;
            parameters[2] = Environment.MachineName;
            parameters[3] = pTableName;
            parameters[4] = pKeyFieldName;
            parameters[5] = pKeyFieldValue;
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOLocks)))
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        isStillLocked = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return isStillLocked;
        }
        /// <summary>
        /// Obtain Transaction to lock the record
        /// </summary>
        private static object objTransaction;
        /// <summary>
        /// Flag to indicate whether all the records specified for update at the form level are locked or not
        /// </summary>
        private static bool AllSuccessFullyLocked;
        /// <summary>
        /// Flag to indicate whether all the records specified for update are released
        /// </summary>
        private static bool AllRecordLocksReleased;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTableName"></param>
        /// <param name="pKeyFieldName"></param>
        /// <param name="pKeyFieldValue"></param>
        /// <param name="pType"></param>
        /// <param name="objDalBaseClass"></param>
        /// <param name="pobjTransaction"></param>
        /// <returns></returns>
        private static bool MakeLockIndiVidual(string pTableName, string pKeyFieldName, object pKeyFieldValue, string pDummyfield, string pDummyvalue, Type pType, ref DALBaseClass objDalBaseClass, ref object pobjTransaction)
        {
            bool CouldLock = false;
            object[] parameters = new object[5];
            parameters[0] = pTableName;
            parameters[1] = pKeyFieldName;
            parameters[2] = pKeyFieldValue;
            parameters[3] = pDummyfield;
            parameters[4] = pDummyvalue;
            try
            {
                if (Convert.ToInt32(objDalBaseClass.ForceLock(ref objTransaction, ref parameters, pType)) == 0)
                {
                    CouldLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return CouldLock;
        }
        /// <summary>
        /// Forcing locks on certain rowsets
        /// </summary>
        /// <param name="KeyFieldValue"></param>
        /// <returns></returns>
        public static bool ForceLock(ref List<DVOLocks> lstDVOLocks)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
            }
            foreach (DVOLocks obj in lstDVOLocks)
            {
                if (!MakeLockIndiVidual(obj.TableName, obj.KeyFieldName, obj.KeyFieldValue, obj.Dummyfield, obj.DummyValue, obj.GetType(), ref objDalBaseClass, ref objTransaction))
                {
                    AllSuccessFullyLocked = false;
                }
            }
            if (!AllSuccessFullyLocked) { objTransaction = null; }
            return AllSuccessFullyLocked;
        }
        private static void addAccountPermissions(ref List<DVOUserApprovalInfoOverAll> lstDVOUserApprovalInfoOverAll, ref DVOAccountPermission objDVOAccountPermission, int index)
        {
            lstDVOUserApprovalInfoOverAll[index].DVOAccountPermissions = objDVOAccountPermission;
        }
        private static void addtoList(ref List<DVOUserApprovalInfoOverAll> lstDVOUserApprovalInfoOverAll, ref DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll, int index)
        {
            lstDVOUserApprovalInfoOverAll.Insert(index, objDVOUserApprovalInfoOverAll);
        }
        /// <summary>
        /// Calling data layer's get Data
        /// Passing object array build from the creteria captured in the winForm
        /// Also passing type of ApprovalSystemUserInfo so that stored procedure hard Coded as property picked up from Common Layer
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOApprovalSystemUserInfo Defined in the common Layer</returns>
        public static List<DVOUserApprovalInfoOverAll> GetApprovals(int puser_id)
        {
            object[] parameters = new object[1];
            parameters[0] = puser_id;
            List<DVOUserApprovalInfoOverAll> lstDVOUserApprovalInfoOverAll = new List<DVOUserApprovalInfoOverAll>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUserApprovalInfoOverAll)))
                {
                    int user_id = 0;
                    int index = -1;
                    if (ds.Tables[0].Rows.Count != 0)
                        user_id = ds.Tables[0].Rows[0]["user_id"] == DBNull.Value ? user_id = 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"]);
                    DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll = null;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int temp = dr["user_id"] == DBNull.Value ? temp = 0 : temp = Convert.ToInt32(dr["user_id"]);
                        if (user_id == temp)
                        {
                            if (objDVOUserApprovalInfoOverAll == null)
                            {
                                index++;
                                objDVOUserApprovalInfoOverAll = new DVOUserApprovalInfoOverAll(temp);
                                if (!Convert.IsDBNull(dr["position"])) objDVOUserApprovalInfoOverAll.position = Convert.ToString(dr["position"]);
                                addtoList(ref lstDVOUserApprovalInfoOverAll, ref objDVOUserApprovalInfoOverAll, index);
                            }
                            DVOAccountPermission objDVOAccountPermission = new DVOAccountPermission();
                            FillAccountPermissions(ref objDVOAccountPermission, dr);
                            addAccountPermissions(ref lstDVOUserApprovalInfoOverAll, ref objDVOAccountPermission, index);
                            user_id = temp;
                        }
                        else
                        {
                            int temp1 = dr["user_id"] == DBNull.Value ? temp = 0 : temp = Convert.ToInt32(dr["user_id"]);
                            objDVOUserApprovalInfoOverAll = null;
                            index++;
                            objDVOUserApprovalInfoOverAll = new DVOUserApprovalInfoOverAll(temp1);
                            if (!Convert.IsDBNull(dr["position"])) objDVOUserApprovalInfoOverAll.position = Convert.ToString(dr["position"]);
                            addtoList(ref lstDVOUserApprovalInfoOverAll, ref objDVOUserApprovalInfoOverAll, index);

                            DVOAccountPermission objDVOAccountPermission = new DVOAccountPermission();
                            FillAccountPermissions(ref objDVOAccountPermission, dr);
                            addAccountPermissions(ref lstDVOUserApprovalInfoOverAll, ref objDVOAccountPermission, index);
                            user_id = temp1;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOUserApprovalInfoOverAll;
        }
        private static void FillAccountPermissions(ref DVOAccountPermission objDVOAccountPermission, DataRow dr)
        {
            if (!Convert.IsDBNull(dr["acd_id"])) objDVOAccountPermission.acd_id = Convert.ToInt32(dr["acd_id"]);
            if (!Convert.IsDBNull(dr["acct_type_id"])) objDVOAccountPermission.acct_type_id = Convert.ToInt32(dr["acct_type_id"]);
            if (!Convert.IsDBNull(dr["accounttype"])) objDVOAccountPermission.accounttype = Convert.ToString(dr["accounttype"]);
            if (!Convert.IsDBNull(dr["approval_level"])) objDVOAccountPermission.approval_level = Convert.ToInt32(dr["approval_level"]);
            if (!Convert.IsDBNull(dr["acc_mask"])) objDVOAccountPermission.acc_mask = Convert.ToString(dr["acc_mask"]);
            if (!Convert.IsDBNull(dr["dept"])) objDVOAccountPermission.dept = Convert.ToString(dr["dept"]);
            if (!Convert.IsDBNull(dr["rowid"])) objDVOAccountPermission.RowID = Convert.ToInt32(dr["rowid"]);
        }
        /// <summary>
        /// Calling data layer's get All Data
        /// Passing no Parameters since we are accessing all the data
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOApprovalSystemUserInfo Defined in the common Layer</returns>
        public static List<DVOUserApprovalInfoOverAll> GetAllApprovals()
        {
            List<DVOUserApprovalInfoOverAll> lstDVOUserApprovalInfoOverAll = new List<DVOUserApprovalInfoOverAll>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOGeneralLedger)))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll = new DVOUserApprovalInfoOverAll();
                        //objDVOApprovalSystemUserInfo.acct_no = Convert.ToInt32(dr["acct_no"]);
                        //objDVOApprovalSystemUserInfo.acct_type = Convert.ToString(dr["acct_type"]);
                        //objDVOApprovalSystemUserInfo.acct_desc = Convert.ToString(dr["acct_desc"]);
                        //objDVOApprovalSystemUserInfo.subtotal_group = Convert.ToString(dr["subtotal_group"]);
                        //objDVOApprovalSystemUserInfo.incr_with_crdt = Convert.ToString(dr["incr_with_crdt"]);
                        //objDVOApprovalSystemUserInfo.keyvalue = Convert.ToString(dr["keyvalue"]);
                        lstDVOUserApprovalInfoOverAll.Add(objDVOUserApprovalInfoOverAll);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOUserApprovalInfoOverAll;
        }
        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOApprovalSystemUserInfo Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static object InsertApprovals(ref DVOSystemUserInfo objDVOSystemUserInfo,
            ref DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll)
        {
            int Success = 0;
            object[] userparameters = new object[5];
            userparameters[0] = objDVOSystemUserInfo.user_id;
            userparameters[1] = objDVOSystemUserInfo.login_id;
            userparameters[2] = objDVOSystemUserInfo.first_name;
            userparameters[3] = objDVOSystemUserInfo.last_name;
            userparameters[4] = objDVOSystemUserInfo.position;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                obj = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref userparameters, typeof(DVOSystemUserInfo), true);
                if (obj.ToString() == "1")
                {
                    List<DVOAccountPermission> lstDVOAccountPermission = new List<DVOAccountPermission>();
                    lstDVOAccountPermission = (List<DVOAccountPermission>)objDVOUserApprovalInfoOverAll.DVOAccountPermissions;
                    object objIns = null;
                    foreach (DVOAccountPermission objAccountPermission in lstDVOAccountPermission)
                    {
                        object[] Approvalparameters = new object[5];
                        Approvalparameters[0] = objDVOUserApprovalInfoOverAll.user_id;
                        Approvalparameters[1] = objAccountPermission.acct_type_id;
                        Approvalparameters[2] = objAccountPermission.acc_mask;
                        Approvalparameters[3] = objAccountPermission.acd_id;
                        Approvalparameters[4] = objAccountPermission.approval_level;
                        objIns = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref Approvalparameters, typeof(DVOUserApprovalInfoOverAll), true);
                    }
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    return objIns;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return obj;
            }

            return obj;
        }
        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOApprovalSystemUserInfo Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static int DeleteApprovals(ref DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll)
        {
            int Success = 0;
            object[] parameters = new object[1];
            parameters[0] = objDVOUserApprovalInfoOverAll.user_id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                Success = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUserApprovalInfoOverAll));
                Success = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOSystemUserInfo));
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return Success;
        }
        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOApprovalSystemUserInfo Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static object UpdateApprovals(ref DVOSystemUserInfo objDVOSystemUserInfo,
            ref DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll, ref DVOUserApprovalInfoOverAll DeleteDVOUserApprovalInfoOverAll)
        {
            int Success = 0;
            object[] userparameters = new object[2];
            userparameters[0] = objDVOSystemUserInfo.user_id;
            userparameters[1] = objDVOSystemUserInfo.position;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object obj = null;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
            }
            try
            {
                obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref userparameters, typeof(DVOSystemUserInfo), true);
                if (obj.ToString() == "1")
                {
                    List<DVOAccountPermission> lstDVOAccountPermission = new List<DVOAccountPermission>();
                    lstDVOAccountPermission = (List<DVOAccountPermission>)objDVOUserApprovalInfoOverAll.DVOAccountPermissions;
                    foreach (DVOAccountPermission objAccountPermission in lstDVOAccountPermission)
                    {
                        object[] Approvalparameters = new object[6];
                        Approvalparameters[0] = objDVOUserApprovalInfoOverAll.user_id;
                        Approvalparameters[1] = objAccountPermission.acct_type_id;
                        Approvalparameters[2] = objAccountPermission.acc_mask;
                        Approvalparameters[3] = objAccountPermission.acd_id;
                        Approvalparameters[4] = objAccountPermission.approval_level;
                        Approvalparameters[5] = objAccountPermission.RowID;
                        obj = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref Approvalparameters, typeof(DVOUserApprovalInfoOverAll), true);
                        if (obj == null)
                            throw new Exception();
                        else if (Convert.ToInt32(obj) < 1)
                            throw new Exception();
                       Approvalparameters = null;
                    }
                    if (Convert.ToInt32(obj) == 1)
                    {
                        List<DVOAccountPermission> lstDVOAccountPermision = new List<DVOAccountPermission>();
                        lstDVOAccountPermision = (List<DVOAccountPermission>)DeleteDVOUserApprovalInfoOverAll.DVOAccountPermissions;
                        foreach (DVOAccountPermission objAccountPermission in lstDVOAccountPermision)
                        {
                            if(objAccountPermission.RowID >0)
                            //if (DeleteDVOUserApprovalInfoOverAll.user_id > 0)
                                BLLApprovalSystemUserInfo.DeleteDetailByRowId(ref objTransaction, ref DeleteDVOUserApprovalInfoOverAll);
                        }
                     
                    }
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    return obj;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return obj;
            }
            finally
            {
                objTransaction = null;
                AllSuccessFullyLocked = true;
            }
            return obj;
        }

        private static object  DeleteDetailByRowId(ref object objTransaction, ref DVOUserApprovalInfoOverAll DeleteDVOUserApprovalInfoOverAll)
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
                List<DVOAccountPermission> lstDVOAccountPermission = new List<DVOAccountPermission>();
                lstDVOAccountPermission = (List<DVOAccountPermission>)DeleteDVOUserApprovalInfoOverAll.DVOAccountPermissions;
                    foreach (DVOAccountPermission objAccountPermission in lstDVOAccountPermission)
                    {
                        //foreach (DVOUserApprovalInfoOverAll objDVOUserApprovalInfoOverAll in DeleteDVOUserApprovalInfoOverAll)
                        //{
                        object[] parameters = new object[1];
                        parameters[0] = objAccountPermission.RowID;

                        //Set order number on the basis of pay_period

                        obj = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOUserApprovalInfoOverAll), DeleteDVOUserApprovalInfoOverAll.DELETE_APPROVAL_DETAILS);
                        parameters = null;
                    }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return obj;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                else
                    throw ex;

                ExceptionManagement.ExceptionManager.Publish(ex);
                return obj;
            }
            return obj;





        }

        public static DataSet GetAcctMask(ref DVOSystemUserInfo objSystemUserInfo)
        {
            List<DVOSystemUserInfo> lstDVOSource = new List<DVOSystemUserInfo>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] Parameters = new object[1];
            Parameters[0] = objSystemUserInfo.accttype;
            DataSet ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOSystemUserInfo), objSystemUserInfo.Get_ACCT_MASK);
            return ds;
        }


        public static DataTable GetData(int puser_id)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            DataTable dt = new DataTable();

            try
            {
                object[] parameters = new object[1];
                parameters[0] = puser_id;

                using (DataSet ds = objDALBaseClass.GetData((new DVOUserApprovalInfoOverAll()).GET_USER_APPROVAL(ref parameters) ))
                {
                    if (ds != null && ds.Tables.Count > 0)
                        return ds.Tables[0];
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return dt;
        }
    }
}
