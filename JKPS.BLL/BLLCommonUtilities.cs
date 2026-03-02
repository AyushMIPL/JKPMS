using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLCommonUtilities
    {
        /// <summary>
        /// Lock Current Record of Form
        /// </summary>
        /// <param name="objTransaction">Transaction Object to lock the record, it should be null initially.</param>
        /// <param name="objDVO">iDVO implemented DVO type class object of Current Record on Form</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// <returns></returns>
        public static int LockCurrentRecord(ref object objTransaction, iDVO objDVO, int UserId, string MachineInfo)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //try
            //{
            //    object[] parameters = new object[4];
            //    parameters[0] = objDVO.TABLE_NAME;
            //    parameters[1] = UserId;
            //    parameters[2] = MachineInfo;
            //    parameters[3] = objDVO.UNIQUE_ID;

            //    int lockstatus = -1;
            //    string lockmessage = string.Empty;

            //    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).RECORD_LOCK))
            //    {
            //        if (ds.Tables.Count > 0)
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                lockstatus = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : -1;//lockstatus
            //                lockmessage = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString().Trim() : string.Empty;//msg
            //            }
            //    }
            //    if (lockstatus == 0)
            //        System.Windows.Forms.MessageBox.Show(lockmessage);

           
            //    if (lockstatus != 1)
            //    {
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //        objTransaction = null;
            //    }

            //    parameters = null;
            //    return lockstatus;
            //}
            //catch (Exception ex)
            //{
            //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// Lock Current Record of Form
        /// </summary>
        /// <param name="objTransaction">Transaction Object to lock the record, it should be null initially.</param>
        /// <param name="objDVO">iDVO implemented DVO type class object of Current Record on Form</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// <returns></returns>
        public static int LockCurrentRecord(ref object objTransaction, iDVO objDVO, int UserId, string MachineInfo, bool IsNewTransaction, bool EndTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (IsNewTransaction)
                objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            else
            {
                if (objTransaction == null)
                    objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //try
            //{
            //    object[] parameters = new object[4];
            //    parameters[0] = objDVO.TABLE_NAME;
            //    parameters[1] = UserId;
            //    parameters[2] = MachineInfo;
            //    parameters[3] = objDVO.UNIQUE_ID;

            //    int lockstatus = -1;
            //    string lockmessage = string.Empty;

            //    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).RECORD_LOCK))
            //    {
            //        if (ds.Tables.Count > 0)
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                lockstatus = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : -1;//lockstatus
            //                lockmessage = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString() : string.Empty;//msg
            //            }
            //    }
            //    if (lockstatus == 0)
            //        System.Windows.Forms.MessageBox.Show(lockmessage);

            //    if (lockstatus != 1)
            //    {
            //        if (EndTransaction)
            //        {
            //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //            objTransaction = null;
            //        }
            //    }

            //    parameters = null;
            //    return lockstatus;
            //}
            //catch (Exception ex)
            //{
            //    if (EndTransaction)
            //    {
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    }
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// Lock Current Record of Form
        /// </summary>
        /// <param name="objTransaction">Transaction Object to lock the record, it should be null initially.</param>
        /// <param name="objDVO">iDVO implemented DVO type class object of Current Record on Form</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// 
        /// <returns></returns>
        public static int LockCurrentRecord(ref object objTransaction, iDVO objDVO, int UserId, string MachineInfo, bool IsNewTransaction, bool EndTransaction, bool ShowMessage)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (IsNewTransaction)
                objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            else
            {
                if (objTransaction == null)
                    objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //try
            //{
            //    object[] parameters = new object[4];
            //    parameters[0] = objDVO.TABLE_NAME;
            //    parameters[1] = UserId;
            //    parameters[2] = MachineInfo;
            //    parameters[3] = objDVO.UNIQUE_ID;

            //    int lockstatus = -1;
            //    string lockmessage = string.Empty;

            //    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).RECORD_LOCK))
            //    {
            //        if (ds.Tables.Count > 0)
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                lockstatus = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : -1;//lockstatus
            //                lockmessage = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString() : string.Empty;//msg
            //            }
            //    }
            //    if (lockstatus == 0 && ShowMessage)
            //        System.Windows.Forms.MessageBox.Show(lockmessage);

            //    if (lockstatus != 1)
            //    {
            //        if (EndTransaction)
            //        {
            //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //            objTransaction = null;
            //        }
            //    }

            //    parameters = null;
            //    return lockstatus;
            //}
            //catch (Exception ex)
            //{
            //    if (EndTransaction)
            //    {
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    }
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// Lock Current Record of Form
        /// </summary>
        /// <param name="objTransaction">Transaction Object to lock the record, it should be null initially.</param>
        /// <param name="TableName">name of the table for which you want to lock the record</param>
        /// <param name="UniqueId">unique id of record in table, you want to lock</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// <returns></returns>
        public static int LockCurrentRecord(ref object objTransaction, string TableName, int UniqueId, int UserId, string MachineInfo, bool IsNewTransaction, bool EndTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (IsNewTransaction)
                objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            else
            {
                if (objTransaction == null)
                    objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //try
            //{
            //    object[] parameters = new object[4];
            //    parameters[0] = TableName;
            //    parameters[1] = UserId;
            //    parameters[2] = MachineInfo;
            //    parameters[3] = UniqueId;

            //    int lockstatus = -1;
            //    string lockmessage = string.Empty;

            //    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).RECORD_LOCK))
            //    {
            //        if (ds.Tables.Count > 0)
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                lockstatus = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : -1;//lockstatus
            //                lockmessage = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString() : string.Empty;//msg
            //            }
            //    }
            //    if (lockstatus == 0)
            //        System.Windows.Forms.MessageBox.Show(lockmessage);

            //    if (lockstatus != 1)
            //    {
            //        if (EndTransaction)
            //        {
            //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //            objTransaction = null;
            //        }
            //    }

            //    parameters = null;
            //    return lockstatus;
            //}
            //catch (Exception ex)
            //{
            //    if (EndTransaction)
            //    {
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    }
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// Lock Current Record of Form
        /// </summary>
        /// <param name="objTransaction">Transaction Object to lock the record, it should be null initially.</param>
        /// <param name="TableName">name of the table for which you want to lock the record</param>
        /// <param name="UniqueId">unique id of record in table, you want to lock</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// <param name="ShowMessage">false, if you don't want to show any message box.</param>
        /// <returns></returns>
        public static int LockCurrentRecord(ref object objTransaction, string TableName, int UniqueId, int UserId, string MachineInfo, bool IsNewTransaction, bool EndTransaction, bool ShowMessage)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (IsNewTransaction)
                objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            else
            {
                if (objTransaction == null)
                    objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //try
            //{
            //    object[] parameters = new object[4];
            //    parameters[0] = TableName;
            //    parameters[1] = UserId;
            //    parameters[2] = MachineInfo;
            //    parameters[3] = UniqueId;

            //    int lockstatus = -1;
            //    string lockmessage = string.Empty;

            //    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).RECORD_LOCK))
            //    {
            //        if (ds.Tables.Count > 0)
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                lockstatus = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : -1;//lockstatus
            //                lockmessage = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? ds.Tables[0].Rows[0][1].ToString() : string.Empty;//msg
            //            }
            //    }
            //    if (lockstatus == 0 && ShowMessage)
            //        System.Windows.Forms.MessageBox.Show(lockmessage);

            //    if (lockstatus != 1)
            //    {
            //        if (EndTransaction)
            //        {
            //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //            objTransaction = null;
            //        }
            //    }

            //    parameters = null;
            //    return lockstatus;
            //}
            //catch (Exception ex)
            //{
            //    if (EndTransaction)
            //    {
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    }
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// Release Current Locked Record of Form
        /// </summary>
        /// <param name="objTransaction">Same object of Transaction, used to lock the record, it should not be null.</param>
        /// <param name="objDVO">iDVO implemented DVO type class object of Current Record on Form</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// <returns></returns>
        public static int ReleaseLockCurrentRecord(ref object objTransaction, iDVO objDVO, int UserId, string MachineInfo, bool EndTransaction)
        {
            bool statusObjectTransactionNotNull = true;
            if (objTransaction == null)
            {
                statusObjectTransactionNotNull = false;
            }
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                if (EndTransaction && objTransaction != null)// && (!statusObjectTransactionNotNull))
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (EndTransaction && objTransaction != null)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManagement.ExceptionManager.Publish(ex);
                    return 0;
                }
            }
            return 0;
            //try
            //{
            //    object[] parameters = new object[4];
            //    parameters[0] = objDVO.TABLE_NAME;
            //    parameters[1] = UserId;
            //    parameters[2] = MachineInfo;
            //    parameters[3] = objDVO.UNIQUE_ID;

            //    objDALBaseClass.ExecuteProcedure(ref parameters, (new DVOCommonEntities()).RECORD_RELEASE);

            //    if (EndTransaction)// && (!statusObjectTransactionNotNull))
            //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            //    parameters = null;
            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    if (EndTransaction)// && (!statusObjectTransactionNotNull))
            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return 0;
            //}
            
        }

        /// <summary>
        /// Release Current Locked Record of Form
        /// </summary>
        /// <param name="objTransaction">Same object of Transaction, used to lock the record, it should not be null.</param>
        /// <param name="TableName">name of the table for which you want to release the record</param>
        /// <param name="UniqueId">unique id of record in table, you want to release</param>
        /// <param name="UserId">Current Login UserId [Program.UserId]</param>
        /// <param name="MachineInfo">User's Machine Info [Program.MachineInfo]</param>
        /// <returns></returns>
        public static int ReleaseLockCurrentRecord(ref object objTransaction, string TableName, int UniqueId, int UserId, string MachineInfo, bool EndTransaction)
        {
            bool statusObjectTransactionNotNull = true;
            if (objTransaction == null)
            {
                statusObjectTransactionNotNull = false;
            }
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                if (EndTransaction && objTransaction != null)// && (!statusObjectTransactionNotNull))
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch(Exception ex)
            {
                if (EndTransaction && objTransaction != null)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    ExceptionManagement.ExceptionManager.Publish(ex);
                    return 0;
                }
            }
           return 0;
        }

        /// <summary>
        /// Rollback Transaction used to lock current record.
        /// </summary>
        /// <param name="objTransaction"></param>
        public static void LockTransaction_Rollback(ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        }

        /// <summary>
        /// Commit Transaction used to lock current record.
        /// </summary>
        /// <param name="objTransaction"></param>
        public static void LockTransaction_Commit(ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (objTransaction != null)
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        }

        /// <summary>
        /// Rollback Transaction 
        /// </summary>
        /// <param name="objTransaction"></param>
        public static void Transaction_Rollback(ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (objTransaction != null)
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        }

        /// <summary>
        /// Commit Transaction
        /// </summary>
        /// <param name="objTransaction"></param>
        public static void Transaction_Commit(ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            if (objTransaction != null)
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        }

        public static List<DVOLockTableStatus> GetLockedRecords()
        {
            //object objTransaction = DALBaseClassHelper.GetUncommittedTransactionObject();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOLockTableStatus> listDVOLockTableStatus = new List<DVOLockTableStatus>();

            try
            {
                object[] parameters = null;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).MONITOR_LOCKS))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOLockTableStatus objDVOLockTableStatus = new DVOLockTableStatus();
                        objDVOLockTableStatus.RowId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"v_rowid"
                        objDVOLockTableStatus.LockedRowId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"v_lockedrowid"
                        objDVOLockTableStatus.TableName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_tablename"
                        objDVOLockTableStatus.LockByUserId = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;//"v_lockbyuserid"
                        objDVOLockTableStatus.LockDatetime = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"v_lockdatetime"
                        objDVOLockTableStatus.LockByMachInfo = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"v_lockbymachinfo"
                        objDVOLockTableStatus.LockByUserLoginId = (dr[6] != DBNull.Value) ? dr[6].ToString().Trim() : string.Empty;//"v_loginid"
                        objDVOLockTableStatus.LockByUserFirstName = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"v_firstname"

                        listDVOLockTableStatus.Add(objDVOLockTableStatus);
                    }
                }

                //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return listDVOLockTableStatus;
            }
            catch (Exception ex)
            {
                //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                listDVOLockTableStatus = new List<DVOLockTableStatus>();
                return listDVOLockTableStatus;
            }
            return listDVOLockTableStatus;
        }

        //public static DataSet GetLockedRecords()//List<DVOLockTableStatus>
        //{
        //    object objTransaction = DALBaseClassHelper.GetUncommittedTransactionObject();
        //    DALBaseClass objDALBaseClass = DALBaseClassHelper.GetDAL();
        //    //List<DVOLockTableStatus> listDVOLockTableStatus = new List<DVOLockTableStatus>();
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        object[] parameters = null;
        //        //using (
        //        ds = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, (new DVOCommonEntities()).MONITOR_LOCKS);
        //        // )
        //        //{
        //        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //        //    {
        //        //        DVOLockTableStatus objDVOLockTableStatus = new DVOLockTableStatus();
        //        //        objDVOLockTableStatus.RowId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;//"v_rowid"
        //        //        objDVOLockTableStatus.LockedRowId = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;//"v_lockedrowid"
        //        //        objDVOLockTableStatus.TableName = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;//"v_tablename"
        //        //        objDVOLockTableStatus.LockByUserId = (dr[3] != DBNull.Value) ? Convert.ToInt32(dr[3]) : 0;//"v_lockbyuserid"
        //        //        objDVOLockTableStatus.LockDatetime = (dr[4] != DBNull.Value) ? dr[4].ToString().Trim() : string.Empty;//"v_lockdatetime"
        //        //        objDVOLockTableStatus.LockByMachInfo = (dr[5] != DBNull.Value) ? dr[5].ToString().Trim() : string.Empty;//"v_lockbymachinfo"
        //        //        objDVOLockTableStatus.LockByUserLoginId = (dr[6] != DBNull.Value) ? dr[6].ToString().Trim() : string.Empty;//"v_loginid"
        //        //        objDVOLockTableStatus.LockByUserFirstName = (dr[7] != DBNull.Value) ? dr[7].ToString().Trim() : string.Empty;//"v_firstname"

        //        //        listDVOLockTableStatus.Add(objDVOLockTableStatus);
        //        //    }
        //        //}

        //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        //return listDVOLockTableStatus;
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        //listDVOLockTableStatus = new List<DVOLockTableStatus>();
        //        //return listDVOLockTableStatus;
        //        return ds;
        //    }
        //    //return listDVOLockTableStatus;
        //    return ds;
        //}

        
        public static string GetServerDate()
        {
            string objstr = string.Empty;
            DVOCommonEntities objcommon = new DVOCommonEntities();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[0];
            object obj = objDalBaseClass.ExecuteScalar(ref parameters, objcommon.GETSERVERDATE);
            if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
                throw new Exception("Error occured to get server date.");
            else
                objstr = Convert.ToDateTime(obj).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//"ServerDate"
            parameters = null;
            objcommon = null;
            return objstr;
        }
        public static DateTime GetServerDateTime()
        {
            DateTime serverdatetime = Convert.ToDateTime(null);
            DVOCommonEntities objcommon = new DVOCommonEntities();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[0];
            object obj = objDalBaseClass.ExecuteScalar(ref parameters, objcommon.GETSERVERDATE);
            if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
                throw new Exception("Error occured to get server date.");
            else
                serverdatetime = Convert.ToDateTime(obj);//"ServerDate"
            parameters = null;
            objcommon = null;
            return serverdatetime;
        }


        
        public static string[] GetCurPeriod()
        {
            string[] str=new string[2];
            str[0] = string.Empty;
            str[1] = string.Empty;
            try
            {
                DVOCommonEntities objcommon = new DVOCommonEntities();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object[] parameters = new object[0];
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), objcommon.GETCURPRD);
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    throw new Exception("Error occured while geting current period.");
                else
                {
                    str[0] = ds.Tables[0].Rows[0][0].ToString().Trim();
                    str[1] = ds.Tables[0].Rows[0][1].ToString().Trim();
                }
                parameters = null;
                objcommon = null;
            }
            catch (Exception ex) { throw ex; }
            return str;
        }


        //**********************************************************************

        /// <summary>
        /// To Validate keyvalue for AccountType OR/AND create new account
        /// </summary>
        /// <param name="pobjDVOGeneralLedger">DVOGeneralLedger type object with acct_type, keyvalue properties to validate.</param>
        /// <param name="AccountCategory">AccountCategory of newly created account.</param>
        /// <param name="IsBudgeted">if Validation for budgeted accounts</param>
        /// <param name="CreateNewAccount">true if you want to create new account when keyvalue invalid for selected accounttype.</param>
        /// <param name="CountMatch">return No. of accounts matched.</param>
        /// <param name="BudgetedCountMatch">return No. of budgeted accounts matched</param>
        /// <param name="AccountNumber">return Account No. of first account matched.</param>
        /// <param name="AccountDescription">return Account Description of first account matched.</param>
        /// <param name="IsAccountTypeExist">return True, if entered account type exist</param>
        /// <param name="IsValidKeyvalueStructure">return True, if entered keyvalue structure is valid for entered account type.</param>
        /// <param name="IsAccountCreated">return True, if new account created for invalid keyvalue.</param>
        /// <returns>True, if kevalue valid for entered AccountType</returns>
        public static bool ValidateKeyValue(ref DVOGeneralLedger pobjDVOGeneralLedger,
            AccountCategories AccountCategory, bool IsBudgeted, bool CreateNewAccount, bool ShouldValidate,
            bool ShouldValidateKeyvalueStructure,
            out int CountMatch, out int BudgetedCountMatch, out int AccountNumber,
            out string AccountDescription, out bool IsAccountTypeExist, out bool IsValidKeyvalueStructure,
            out bool IsAccountCreated, bool AllAccounts, out int GoBelowZero, out int Active,bool IsSegmentValueReq)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //variable as an indicator that Inserted Account Number is Validate or not. default is false.
            bool status = false;
            //default values for out parameters
            CountMatch = 0;
            AccountNumber = 0;
            AccountDescription = string.Empty;
            BudgetedCountMatch = 0;
            IsAccountTypeExist = false;
            IsValidKeyvalueStructure = false;
            IsAccountCreated = false;
            GoBelowZero = 0;
            Active = 0;

           

            #region Updated Code

            try
            {
                object[] parameters = new object[10];
                parameters[0] = pobjDVOGeneralLedger.acct_type.Trim();
                parameters[1] = pobjDVOGeneralLedger.keyvalue.Trim();
                parameters[2] = "";
                if (!AllAccounts)
                    if (IsBudgeted)
                        parameters[2] = "U";
                    else
                        parameters[2] = "-";

                parameters[3] = pobjDVOGeneralLedger.keyvalue.Trim().Replace("#", "?");
                parameters[4] = IsBudgeted ? 1 : 0;
                parameters[5] = CreateNewAccount ? 1 : 0;
                parameters[6] = ShouldValidate ? 1 : 0;
                parameters[7] = ShouldValidateKeyvalueStructure ? 1 : 0;
                parameters[8] = AllAccounts ? 1 : 0;
                parameters[9] = IsSegmentValueReq ? 1 : 0;

                using (DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref parameters, (new DVOGLAccountTypeMaintenance()).VALIDATE_KEYVALUE))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                      

                        DataRow dr = ds.Tables[0].Rows[0];
                        status = dr[0] != DBNull.Value ? Convert.ToBoolean(dr[0]) : false;
                        CountMatch = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                        AccountNumber = dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0;
                        AccountDescription = dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty;
                        BudgetedCountMatch = dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0;
                        IsAccountTypeExist = dr[5] != DBNull.Value ? Convert.ToBoolean(dr[5]) : false;
                        IsValidKeyvalueStructure = dr[6] != DBNull.Value ? Convert.ToBoolean(dr[6]) : false;
                        IsAccountCreated = dr[7] != DBNull.Value ? Convert.ToBoolean(dr[7]) : false;
                        GoBelowZero = dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0;
                        Active = dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0;
                    }
                }
                if (status && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                else
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                status = false;
                CountMatch = 0;
                AccountNumber = 0;
                AccountDescription = string.Empty;
                BudgetedCountMatch = 0;
                IsAccountTypeExist = false;
                IsValidKeyvalueStructure = false;
                IsAccountCreated = false;
                GoBelowZero = 0;
                Active = 0;

                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return status;

            #endregion Updated Code

            //********************************************************


            #region Old Code

            //DVOGLAccountTypeMaintenance objDVOGLAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
            //if (!AllAccounts)
            //{
            //    if (IsBudgeted)
            //        objDVOGLAccountTypeMaintenance.AccountCategory = "U";
            //    else
            //        objDVOGLAccountTypeMaintenance.AccountCategory = "-U";
            //}
            //objDVOGLAccountTypeMaintenance.accounttype = pobjDVOGeneralLedger.acct_type;
            //List<DVOGLAccountTypeMaintenance> listDVOGLAccountTypeMaintenance = BLLGLAccountTypeMaintenance.GetAccountTypeMaintenance(ref objDVOGLAccountTypeMaintenance);
            //if (listDVOGLAccountTypeMaintenance.Count > 0)
            //{
            //    IsAccountTypeExist = true;
            //    if (listDVOGLAccountTypeMaintenance[0].keylength != pobjDVOGeneralLedger.keyvalue.Trim().Length && ShouldValidateKeyvalueStructure)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        IsValidKeyvalueStructure = true;
            //        //if (ShouldValidate)
            //        //{
            //        //initialize DVOGeneralLedger type object to use as parameter to get account-number of entered keyValue
            //        DVOGeneralLedger objGeneralLedger = new DVOGeneralLedger();
            //        //set account type for which you want to get AccountNumber
            //        objGeneralLedger.acct_type = pobjDVOGeneralLedger.acct_type.Trim();
            //        //set keyvalue for which you want to get AccountNumber to this object
            //        objGeneralLedger.keyvalue = pobjDVOGeneralLedger.keyvalue.Trim();
            //        if (!AllAccounts)
            //        {
            //            if (IsBudgeted)
            //                objGeneralLedger.acct_cat = "U";
            //            else
            //                objGeneralLedger.acct_cat = "-";
            //        }

            //        BLLGeneralLedger.GetAccountInfoForAccountTextBox(objGeneralLedger, out AccountNumber, out AccountDescription, out CountMatch, out BudgetedCountMatch, out GoBelowZero, out Active);
            //        if (CountMatch > 0)
            //            status = true;

            //        ////user GetLedgerAccounts function of BLLGeneralLedger class to get list of AccountNumber related to keyValue entered
            //        //List<DVOGeneralLedger> listGeneralLedger = BLLGeneralLedger.GetLedgerAccounts(objGeneralLedger);

            //        ////check list has some items or not, if there are some records then entered KeyValue is valid and
            //        ////      we can get AccountNumber of this KeyValue
            //        //if (listGeneralLedger.Count > 0)
            //        //{
            //        //    CountMatch = listGeneralLedger.Count;
            //        //    AccountNumber = listGeneralLedger[0].acct_no;
            //        //    AccountDescription = listGeneralLedger[0].acct_desc.Trim();

            //        //    listGeneralLedger = listGeneralLedger.FindAll(delegate(DVOGeneralLedger objDVOGeneralLedger1)
            //        //    {
            //        //        return objDVOGeneralLedger1.acct_cat == "U";
            //        //    });
            //        //    if (listGeneralLedger.Count > 0)
            //        //        BudgetedCountMatch = listGeneralLedger.Count;

            //        //    //and make status true
            //        //    status = true;
            //        //}

            //        objGeneralLedger = null;
            //        //listGeneralLedger = null;
            //        //}
            //    }
            //}
            //else
            //{
            //    return false;
            //}
            //listDVOGLAccountTypeMaintenance = null;
            //objDVOGLAccountTypeMaintenance = null;
            //bool _segStatus = true;
            //if (IsSegmentValueReq)
            //{
            //    if (ValidateSegments(pobjDVOGeneralLedger.acct_type, pobjDVOGeneralLedger.keyvalue))
            //        _segStatus = true;

            //    else
            //        _segStatus = false;
            //}


            ////if status is false then show a message that entered account-number is not valid.
            //if (!status)
            //{
            //    //check CreateNewAccount property is set or not
            //    if (CreateNewAccount && _segStatus)
            //    {

            //        //if CreateNewAccount property is set then create new account for entered keyvalue.
            //        DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
            //        objDVOGeneralLedger.acct_type = pobjDVOGeneralLedger.acct_type.Trim();
            //        objDVOGeneralLedger.acct_cat = StringEnum.GetStringValue(AccountCategory);
            //        objDVOGeneralLedger.keyvalue = pobjDVOGeneralLedger.keyvalue.Trim();
            //        objDVOGeneralLedger.gobzero = 0;
            //        objDVOGeneralLedger.active = 1;

            //        BLLGeneralLedger.InsertLedgerAccounts(ref objDVOGeneralLedger, out AccountNumber, out AccountDescription);
            //        CountMatch = 1;
            //        if (AccountCategory == AccountCategories.Budget)
            //            BudgetedCountMatch = 1;

            //        IsAccountCreated = true;
            //        objDVOGeneralLedger = null;
            //    }
            //}
            //if (IsSegmentValueReq)
            //{
            //    status = _segStatus;
            //}
            //return status;

            #endregion Old Code
        }


        public static bool ValidateSegments(string acctType,string keyvalue)
        {
            try
            {
                object[] parameters = new object[1];
                parameters[0] = acctType;
                DVOCommonEntities objcommon = new DVOCommonEntities();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), objcommon.GET_SEGMENTS);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (keyvalue.Trim().Length > 0)
                    {
                        int _totLength = 0;
                        int _position = 0;
                        int _length = 0;
                        string _required = string.Empty;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            _position = dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0;
                            _length = dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0;
                            _required = dr[4] != DBNull.Value ? Convert.ToString(dr[4]) : string.Empty;
                            _totLength += _length;
                            if (keyvalue.Trim().Length >= _totLength)
                            {
                                string _key = keyvalue.Substring(_position - 1, _length);
                                if (_key.Contains("#"))
                                {
                                    if (_required != "N")
                                        return false;
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return true;
        }


        //*************************** Added By Bharat Dhall [13 December, 2009] **********************
        //********************************************************************************************
        /// <summary>
        /// to Get Account's information based on assigned AccountNumber
        /// </summary>
        /// <param name="AccountNumber">Account Number for which you want to get information</param>
        /// <param name="KeyValue">out parameter to get KeyValue of account</param>
        /// <param name="AccountType">out parameter to get AccountType of account</param>
        /// <param name="AccountTypeId">out parameter to get AccountTypeId of account</param>
        /// <param name="AccountDescription">out parameter to get Description of account</param>
        public static void GetAccountInformation(int AccountNumber, out string KeyValue, out string AccountType, out int AccountTypeId, out string AccountDescription)
       {
        ////    //set default values for out parameters
          KeyValue = string.Empty;
          AccountType = string.Empty;
          AccountTypeId = 0;
          AccountDescription = string.Empty;

        ////    if (AccountNumber > 0)
        ////    {
        ////        //make object to send as parameter 
        ////        DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
        ////        //set account number of object for which you want to get information
        ////        objDVOGeneralLedger.acct_no = AccountNumber;
        ////        //call BLL class function to get information
        ////        objDVOGeneralLedger = ((List<DVOGeneralLedger>)BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger)).Count > 0 ? ((List<DVOGeneralLedger>)BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger))[0] : null;

        ////        if (objDVOGeneralLedger != null)
        ////        {
        ////            //set variables with information 
        ////            KeyValue = objDVOGeneralLedger.keyvalue;
        ////            AccountType = objDVOGeneralLedger.acct_type;
        ////            AccountTypeId = objDVOGeneralLedger.acct_type_id;
        ////            AccountDescription = objDVOGeneralLedger.acct_desc;

        ////            objDVOGeneralLedger = null;
        ////        }
        ////    }
       }

        ///// <summary>
        /// to Get Account's information based on assigned Keyvalue
        /// </summary>
        /// <param name="KeyValue">Keyvalue for which you want to get information</param>
        /// <param name="AccountNumber">out parameter to get Account Number</param>
        /// <param name="AccountType">out parameter to get AccountType of account</param>
        /// <param name="AccountTypeId">out parameter to get AccountTypeId of account</param>
        /// <param name="AccountDescription">out parameter to get Description of account</param>
    public static void GetAccountInformation(string Keyvalue, out int AccountNumber, out string AccountType, out int AccountTypeId, out string AccountDescription)
       {
    ////        //set default values for out parameters
           AccountNumber = 0;
          AccountType = string.Empty;
          AccountTypeId = 0;
          AccountDescription = string.Empty;

          if (Keyvalue.Trim() != string.Empty)
          {
              //make object to send as parameter 
              DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
              //set keyvalue of object for which you want to get information
              objDVOGeneralLedger.keyvalue = Keyvalue;
              //call BLL class function to get information
              objDVOGeneralLedger = ((List<DVOGeneralLedger>)BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger)).Count > 0 ? ((List<DVOGeneralLedger>)BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger))[0] : null;

              if (objDVOGeneralLedger != null)
              {
                  //set variables with information 
                  AccountNumber = objDVOGeneralLedger.acct_no;
                  AccountType = objDVOGeneralLedger.acct_type;
                  AccountTypeId = objDVOGeneralLedger.acct_type_id;
                  AccountDescription = objDVOGeneralLedger.acct_desc;

                  objDVOGeneralLedger = null;
              }
          }
      }
    ////    //********************************************************************************************
        //***********************Added By Rahul Jain on 20/01/2009************************************
        /// <summary>
        /// This method is use to get Databaseinformation from database 
        /// </summary>
        /// <returns>return a list having approval classes information</returns>
      public static List<DVOActiveDatabase> GetDBInformation(ref DVOActiveDatabase objDVOActiveDatabase)
        {
            object[] parameters = new object[1];
            parameters[0] = objDVOActiveDatabase.DBType;
            List<DVOActiveDatabase> objDBInformationlist = new List<DVOActiveDatabase>();
            //DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
            SqlHelper objIfxHelper = new SqlHelper();
            //DataSet ds = objIfxHelper.GetAlldatabases(null, typeof(DVOCommonEntities));
            //using (DataSet ds = objIfxHelper.GetAlldatabases(parameters, typeof(DVOActiveDatabase)))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DVOActiveDatabase objDBInformation = new DVOActiveDatabase();
            //        objDBInformation.DBName = dr[0].ToString().Trim();//dbname
            //        objDBInformation.DBDescription = dr[1].ToString().Trim(); //DBDescription
            //        objDBInformation.DBType = dr[2].ToString().Trim();//dbtype
            //        objDBInformation.ConString = dr[3].ToString().Trim();//constring
            //        objDBInformation.UserId = dr[4].ToString().Trim();//userid
            //        objDBInformation.Password = dr[5].ToString().Trim();//password
            //        objDBInformation.Active = Convert.ToInt16(dr[6]);//active

            //        objDBInformationlist.Add(objDBInformation);
            //    }
            //}
            return objDBInformationlist;
        }
        //public static string GetConnectionString(ref DVOActiveDatabase objDVOActiveDatabase)
        //{
        //    string objstr = string.Empty;
        //    object[] parameters = new object[1];
        //    parameters[0] = objDVOActiveDatabase.DBDescription;
        //    DVOActiveDatabase objcommon = new DVOActiveDatabase();
        //    IfxHelper objIfxHelper = new IfxHelper();
        //    using (DataSet ds = objIfxHelper.GetDataconnection(parameters, typeof(DVOActiveDatabase)))
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {



        //            objcommon.ConString = dr[3].ToString().Trim();
        //        objstr = objcommon.ConString;//"Constring"
        //        objstr = objstr + "User ID=" + dr[4].ToString().Trim()+";";
        //        objstr = objstr + "Password=" + dr[5].ToString().Trim()+";";
        //        }

        //    }
        //    return objstr;
        //}
        //********************************************************************************************


        // **********************************Added By Rajeev ********************************
        //Aim:- To Create Several Function to validate Data 
        //Date :- 23/02/09 

        /// <summary> 
        /// The function will validate the monetory data and it accept uptill 3 decimal place 
        /// </summary> 
        /// <param name="InputVal"></param> 
        /// <returns></returns> 
        public static bool ValidateMoneyValue(string InputVal)
        {
            Regex regMoneyVal = new Regex(@"(^[+-]?\d{1,10}\.$)|(^[+-]$)|(^[+-]?\d{1,10}$)|(^[+-]?\d{0,10}\.\d{1,3}$)");
            Boolean blnInputValid = true;
            if (!regMoneyVal.IsMatch(InputVal))
            {
                blnInputValid = false;
                return blnInputValid;
            }
            else
                return blnInputValid;
        }

        /// <summary> 
        /// The Function validate only char string 
        /// </summary> 
        /// <param name="InputVal"></param> 
        /// <returns></returns> 
        public static bool ValidateStringValue(string InputVal)
        {
            Regex regCharValue = new Regex(@"^[a-zA-Z'.\s]{1,50}$");
            Boolean blnInputValid = true;
            if (!regCharValue.IsMatch(InputVal))
            {
                blnInputValid = false;
                return blnInputValid;
            }
            else
                return blnInputValid;
        }

        /// <summary> 
        /// validate positive, negative integer with zero 
        /// </summary> 
        /// <param name="InputVal"></param> 
        /// <returns></returns> 
        public static bool ValidateIntegerValue(string InputVal)
        {
            Regex regIntegerValue = new Regex(@"^[-+]?(\d?\d?\d?,?)?(\d{3}\,?)*$");
            Boolean blnInputValid = true;
            if (!regIntegerValue.IsMatch(InputVal))
            {
                blnInputValid = false;
                return blnInputValid;
            }
            else
                return blnInputValid;
        }
        public static int InsertLogOutTime(ref DVOLoginLog objLoginLog)
        {
            object[] parameters = new object[1];
            //parameters[0] = objLoginLog.LogOutTime;
            parameters[0] = objLoginLog.loginlogid;

            DALBaseClassHelperSecurity objDALBaseClassHelperSecurity = new DALBaseClassHelperSecurity();
            DALBaseClass objDalBaseClass = objDALBaseClassHelperSecurity.GetDAL();
            object c = objDalBaseClass.InsertData(ref parameters, typeof(DVOSecUsers),objLoginLog.INSERT_LOGOUT_TIME);

            return Convert.ToInt32(c);
        }

        public static DataTable GetDataSourceForSmartTextbox(string ColumnNames, string TableName)
        {
            DataTable dt = new DataTable();
            try
            {
                DVOCommonEntities objcommon = new DVOCommonEntities();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                using (DataSet ds = objDalBaseClass.GetData(objcommon.GET_DATA_FOR_SMARTTEXTBOX(ColumnNames, TableName)))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return dt;
        }

        public static DataTable GetDataSourceForSmartTextbox(string ColumnNames, string TableName, string WhereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                DVOCommonEntities objcommon = new DVOCommonEntities();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                using (DataSet ds = objDalBaseClass.GetData(objcommon.GET_DATA_FOR_SMARTTEXTBOX(ColumnNames, TableName, WhereCondition)))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return dt;
        }

        //*****************  Added by Bharat Dhall[05/15/2009]  *****************************
        //******** function will be used for some special security check ********************
        //************ and will be defined later. *******************************************
        public static bool SecurityCheck(bool Status)
        {
            return Status;
        }
        //************************************************************************************


        //*****************  Added by Bharat Dhall[06/03/2009]  *****************************
        /// <summary>
        /// to check date is existing in current accounting period or not
        /// </summary>
        /// <param name="DateToCheck">Date to check for current period</param>
        /// <returns>true, if date is valid for current period</returns>
        public static bool ValidateCurrentPeriod(DateTime DateToCheck)
        {
            try
            {
                //function of BLL layer will return DataSet which contains date range of current period.            
                DataSet ds = BLLGeneralLibrary.GetCurrentPeriod();
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][1] != DBNull.Value && ds.Tables[0].Rows[0][2] != DBNull.Value)// 1-Period, 2-Year
                                if (ds.Tables[0].Rows[0][1].ToString().Trim() != string.Empty && ds.Tables[0].Rows[0][2].ToString().Trim() != string.Empty)
                                {
                                    //Get start_date and end_date for current period
                                    DateTime startDate, endDate;
                                    startDate = (ds.Tables[0].Rows[0][3] != DBNull.Value && ds.Tables[0].Rows[0][3].ToString().Trim() != string.Empty) ? Convert.ToDateTime(ds.Tables[0].Rows[0][3]) : DateTime.MinValue;// 3-StartDate
                                    endDate = (ds.Tables[0].Rows[0][4] != DBNull.Value && ds.Tables[0].Rows[0][4].ToString().Trim() != string.Empty) ? Convert.ToDateTime(ds.Tables[0].Rows[0][4]) : DateTime.MinValue;// 4-EndDate

                                    if ((DateToCheck <= endDate && DateToCheck >= startDate))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                        }
                        else
                        {
                            return false;
                        }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return false;
        }
        //************************************************************************************

        public static DataTable MakeSegments(string acctType,string oldAcctType, string keyvalue, ref List<DVOFlexSegCommon> objListSegDesc,ref DataSet dsSegvd)
        {
            //This datatable object used to hold segments.
            #region dtlSegments
            DataTable dtlSegments = new DataTable();
            dtlSegments.Columns.Add("Level1");
            dtlSegments.Columns.Add("Level2");
            dtlSegments.Columns.Add("Level3");
            dtlSegments.Columns.Add("Level4");
            dtlSegments.Columns.Add("Level5");
            dtlSegments.Columns.Add("Level6");
            dtlSegments.Columns.Add("Level7");
            dtlSegments.Columns.Add("Level8");
            dtlSegments.Columns.Add("Level9");
            dtlSegments.Columns.Add("Level10");
            dtlSegments.Columns.Add("LevelDesc1");
            dtlSegments.Columns.Add("LevelDesc2");
            dtlSegments.Columns.Add("LevelDesc3");
            dtlSegments.Columns.Add("LevelDesc4");
            dtlSegments.Columns.Add("LevelDesc5");
            dtlSegments.Columns.Add("LevelDesc6");
            dtlSegments.Columns.Add("LevelDesc7");
            dtlSegments.Columns.Add("LevelDesc8");
            dtlSegments.Columns.Add("LevelDesc9");
            dtlSegments.Columns.Add("LevelDesc10");
            DataRow drSegments = dtlSegments.NewRow();
            #endregion

            try
            {
                //get information on the account type structure
                if (objListSegDesc == null)
                {
                    objListSegDesc = GetSegmentDescription(acctType);
                }
                else if (acctType != oldAcctType)
                {
                    objListSegDesc = GetSegmentDescription(acctType);
                }
                //Get Information from Master_Segment
                if (dsSegvd == null)
                {
                    dsSegvd = BLLGLTrialBalance.GetMonthEndSegmentDetails();
                }
                int locParentId = 0;
                int locCurLevel = 1;
                string locSegDesc = string.Empty;
                foreach (DVOFlexSegCommon obj in objListSegDesc)
                {
                    string locSegment = keyvalue.Substring(obj.position - 1,  obj.length);

                    // we do not care about segments that we are not printing subtotals for
                    if (obj.subtotal != "Y" || locSegment.Contains ("#"))
                    {
                        continue;
                    }
                    //If this is a new hierarchy in the
                    //keyvalue structure so there is no parent segment
                    if (obj.subdivides == 0)
                    {
                        locParentId = 0;
                    }
                    DataRow[] dra = dsSegvd.Tables[0].Select("segmentid = " + obj.flexsegid + " AND issubto = " + locParentId + " AND keyvalue= '" + locSegment+"'");
                    if (dra.Length > 0)
                    {
                        locSegDesc = Convert.ToString(dra[0]["desc"]).Trim();
                        locParentId = dra[0]["id"] != DBNull.Value ? Convert.ToInt32(dra[0]["id"]) : 0;
                    }
                    else
                    {
                        locParentId = 0;
                    }
                    switch (locCurLevel)
                    {  
                        case 1:
                            drSegments["Level1"] = locSegment;
                            drSegments["LevelDesc1"] = locSegDesc;
                            break;
                        case 2:
                            drSegments["Level2"] = locSegment;
                            drSegments["LevelDesc2"] = locSegDesc;
                            break;
                        case 3:
                            drSegments["Level3"] = locSegment;
                            drSegments["LevelDesc3"] = locSegDesc;
                            break;
                        case 4:
                            drSegments["Level4"] = locSegment;
                            drSegments["LevelDesc4"] = locSegDesc;
                            break;
                        case 5:
                            drSegments["Level5"] = locSegment;
                            drSegments["LevelDesc5"] = locSegDesc;
                            break;
                        case 6:
                            drSegments["Level6"] = locSegment;
                            drSegments["LevelDesc6"] = locSegDesc;
                            break;
                        case 7:
                            drSegments["Level7"] = locSegment;
                            drSegments["LevelDesc7"] = locSegDesc;
                            break;
                        case 8:
                            drSegments["Level8"] = locSegment;
                            drSegments["LevelDesc8"] = locSegDesc;
                            break;
                        case 9:
                            drSegments["Level9"] = locSegment;
                            drSegments["LevelDesc9"] = locSegDesc;
                            break;
                        case 10:
                            drSegments["Level10"] = locSegment;
                            drSegments["LevelDesc10"] = locSegDesc;
                            break;

                    }
                    locCurLevel++;
                }
                dtlSegments.Rows.Add(drSegments);
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return dtlSegments;
        
        }
        public static List<DVOFlexSegCommon> GetSegmentDescription(string acctType)
        { 
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOFlexSegCommon> objListSegDesc = new List<DVOFlexSegCommon>();
            try
            {
                Object[] parameters = new object[1];
                parameters[0] = acctType;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), (new DVOFlexSegCommon()).FIND_SEGDESC))
                { 
                  if(ds!=null)
                      if(ds.Tables.Count>0)
                          if (ds.Tables[0].Rows.Count > 0)
                          {
                              foreach (DataRow dr in ds.Tables[0].Rows)
                              {
                                  DVOFlexSegCommon obj = new DVOFlexSegCommon();
                                  obj.flexsegid = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                                  obj.position = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                                  obj.length = dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0;
                                  obj.subtotal = dr[3] != DBNull.Value ? Convert.ToString(dr[3]) :"Y";
                                  obj.subdivides = dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0;
                                  objListSegDesc.Add(obj);
                              }
                          }

                }

            }
            catch(Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return objListSegDesc;
        
        }
    }
}
