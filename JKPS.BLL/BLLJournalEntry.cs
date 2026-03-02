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
    /// Description : This class basically used for interacting with Data Access Layer and gets the data requested from Form frmApprovalSystemUserInfo.  
    /// Modified by:chandra
    /// Modified Date :
    /// Description :
    /// </summary>
    public class BLLJournalEntry
    {

        static BLLJournalEntry()
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
        private static bool MakeLockIndiVidual(string pTableName, string pKeyFieldName, object pKeyFieldValue, Type pType, ref DALBaseClass objDalBaseClass, ref object pobjTransaction)
        {
            bool CouldLock = false;
            object[] parameters = new object[3];
            parameters[0] = pTableName;
            parameters[1] = pKeyFieldName;
            parameters[2] = pKeyFieldValue;
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
                if (!MakeLockIndiVidual(obj.TableName, obj.KeyFieldName, obj.KeyFieldValue, obj.GetType(), ref objDalBaseClass, ref objTransaction))
                {
                    AllSuccessFullyLocked = false;
                }
            }
            if (!AllSuccessFullyLocked) { objTransaction = null; }
            return AllSuccessFullyLocked;
        }
        private static void addDetailsToHeader(ref List<DVOJournalHeaderEntry> lstDVOJournalHeaderEntry, ref DVOJournalDetailsEntry objDVOJournalDetailsEntry, int index)
        {
            lstDVOJournalHeaderEntry[index].DVOJournalDetailsEntries = objDVOJournalDetailsEntry;
        }
        private static void addtoList(ref List<DVOJournalHeaderEntry> lstDVOJournalHeaderEntry, ref DVOJournalHeaderEntry objDVOJournalHeaderEntry, int index)
        {
            lstDVOJournalHeaderEntry.Insert(index, objDVOJournalHeaderEntry);
        }
        /// <summary>
        /// Calling data layer's get Data
        /// Passing object array build from the creteria captured in the winForm
        /// Also passing type of ApprovalSystemUserInfo so that stored procedure hard Coded as property picked up from Common Layer
        /// </summary>
        /// <returns>List of records in form of generic list of Type DVOApprovalSystemUserInfo Defined in the common Layer</returns>
        public static List<DVOJournalHeaderEntry> GetJournalEntries(ref DVOJournalHeaderEntry SearchDVOJournalHeaderEntry)
        {
            object[] headerparameters = new object[8];
            headerparameters[0] = SearchDVOJournalHeaderEntry.doc_no;
            headerparameters[1] = SearchDVOJournalHeaderEntry.doc_desc;
            headerparameters[2] = SearchDVOJournalHeaderEntry.doc_date;
            headerparameters[3] = SearchDVOJournalHeaderEntry.auto_rev;
            headerparameters[4] = SearchDVOJournalHeaderEntry.batch_id;
            headerparameters[5] = SearchDVOJournalHeaderEntry.user_id;
            headerparameters[6] = SearchDVOJournalHeaderEntry.posted;
            //************Added by Sunil Pahwa for locking purpose***********
            headerparameters[7] = SearchDVOJournalHeaderEntry.rowid;
            //***************************************************************

            List<DVOJournalHeaderEntry> lstDVOJournalHeaderEntry = new List<DVOJournalHeaderEntry>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref headerparameters, typeof(DVOJournalHeaderEntry)))
                {
                    //**************************Updated by sanjay*********************//
                    //SET COLUMN NAMES
                    ds.Tables[0].Columns[0].ColumnName = "doc_no";
                    ds.Tables[0].Columns[1].ColumnName = "doc_desc";
                    ds.Tables[0].Columns[2].ColumnName = "doc_date";
                    ds.Tables[0].Columns[3].ColumnName = "doc_src";
                    ds.Tables[0].Columns[4].ColumnName = "auto_rev";
                    ds.Tables[0].Columns[5].ColumnName = "file_type";
                    ds.Tables[0].Columns[6].ColumnName = "posted";
                    ds.Tables[0].Columns[7].ColumnName = "ok_to_post";
                    ds.Tables[0].Columns[8].ColumnName = "batch_id";
                    ds.Tables[0].Columns[9].ColumnName = "user_id";
                    ds.Tables[0].Columns[10].ColumnName = "orig_journal";
                    ds.Tables[0].Columns[11].ColumnName = "line_no";
                    ds.Tables[0].Columns[12].ColumnName = "acct_no";
                    ds.Tables[0].Columns[13].ColumnName = "department";
                    ds.Tables[0].Columns[14].ColumnName = "amount";
                    ds.Tables[0].Columns[15].ColumnName = "debit_credit";
                    ds.Tables[0].Columns[16].ColumnName = "src_desc";
                    ds.Tables[0].Columns[17].ColumnName = "acct_desc";
                    ds.Tables[0].Columns[18].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[19].ColumnName = "acct_type";
                    ds.Tables[0].Columns[20].ColumnName = "acct_type_id";
                    ds.Tables[0].Columns[21].ColumnName = "rowid";//stgjoure
                    ds.Tables[0].Columns[22].ColumnName = "drowid";//stgjourd

                    //*****************************************************************
                    int doc_no = 0;
                    int index = -1;
                    if (ds.Tables[0].Rows.Count != 0)
                        doc_no = ds.Tables[0].Rows[0]["doc_no"] == DBNull.Value ? doc_no = 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["doc_no"]);
                    DVOJournalHeaderEntry objDVOJournalHeaderEntry = null;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int temp = dr["doc_no"] == DBNull.Value ? temp = 0 : temp = Convert.ToInt32(dr["doc_no"]);
                        if (doc_no == temp)
                        {
                            if (objDVOJournalHeaderEntry == null)
                            {
                                index++;
                                //                            dbo.stgjoure.doc_no, dbo.stgjoure.doc_desc, dbo.stgjoure.doc_date, dbo.stgjoure.doc_src,
                                //dbo.stgjoure.auto_rev, dbo.stgjoure.file_type,dbo.stgjoure.posted, dbo.stgjoure.ok_to_post, 
                                //dbo.stgjoure.batch_id, dbo.stgjoure.user_id, 

                                objDVOJournalHeaderEntry = new DVOJournalHeaderEntry();
                                FillHeader(ref objDVOJournalHeaderEntry, dr);

                                addtoList(ref lstDVOJournalHeaderEntry, ref objDVOJournalHeaderEntry, index);
                            }
                            DVOJournalDetailsEntry objDVOJournalDetailsEntry = new DVOJournalDetailsEntry();
                            FillDetails(ref objDVOJournalDetailsEntry, dr);
                            addDetailsToHeader(ref lstDVOJournalHeaderEntry, ref objDVOJournalDetailsEntry, index);
                            doc_no = temp;
                        }
                        else
                        {
                            int temp1 = dr["doc_no"] == DBNull.Value ? temp = 0 : temp = Convert.ToInt32(dr["doc_no"]);
                            objDVOJournalHeaderEntry = null;
                            index++;
                            objDVOJournalHeaderEntry = new DVOJournalHeaderEntry();
                            FillHeader(ref objDVOJournalHeaderEntry, dr);
                            addtoList(ref lstDVOJournalHeaderEntry, ref objDVOJournalHeaderEntry, index);

                            DVOJournalDetailsEntry objDVOJournalDetailsEntry = new DVOJournalDetailsEntry();
                            FillDetails(ref objDVOJournalDetailsEntry, dr);
                            addDetailsToHeader(ref lstDVOJournalHeaderEntry, ref objDVOJournalDetailsEntry, index);
                            doc_no = temp1;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOJournalHeaderEntry;
        }

        public static DataSet GetList(ref DVOJournalHeaderEntry objJournalHeaderEntry)
        {
            Object[] parameters = new object[0];
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet dsExpExp = objDalBaseClass.GetData(ref parameters, typeof(DVOJournalHeaderEntry), objJournalHeaderEntry.Get_Ledger_List);
            return dsExpExp;
        }
        private static void FillDetails(ref DVOJournalDetailsEntry objDVOJournalDetailsEntry, DataRow dr)
        {
            //            dbo.stgjourd.orig_journal, dbo.stgjourd.line_no, 
            //dbo.stgjourd.acct_no, dbo.stgjourd.department, dbo.stgjourd.amount, dbo.stgjourd.debit_credit


            if (!Convert.IsDBNull(dr[10])) objDVOJournalDetailsEntry.orig_journal = Convert.ToString(dr[10]);//
            if (!Convert.IsDBNull(dr[11])) objDVOJournalDetailsEntry.line_no = Convert.ToInt32(dr[11]);//
            if (!Convert.IsDBNull(dr[12])) objDVOJournalDetailsEntry.acct_no = Convert.ToInt32(dr[12]);//
            if (!Convert.IsDBNull(dr[13])) objDVOJournalDetailsEntry.department = Convert.ToString(dr[13]);//
            if (!Convert.IsDBNull(dr[14]))
            {
                objDVOJournalDetailsEntry.amount = Convert.ToDecimal(dr[14]);//
                if (Convert.ToSingle(dr[14]) < 0)
                {
                    objDVOJournalDetailsEntry.amountdc = -(Convert.ToDecimal(dr[14]));//
                }
                else
                {
                    objDVOJournalDetailsEntry.amountdc = Convert.ToDecimal(dr[14]);//
                }
                objDVOJournalDetailsEntry.tmpamount = Convert.ToDecimal(dr[14]);//
            }
            if (!Convert.IsDBNull(dr[15]))
            {
                if (Convert.ToString(dr[15]) == "C")
                {
                    objDVOJournalDetailsEntry.debit_credit = "CR";//
                }
                else if (Convert.ToString(dr[15]) == "D")
                {
                    objDVOJournalDetailsEntry.debit_credit = "DB";//
                }
            }
            objDVOJournalDetailsEntry.Editable = 1;
            if (!Convert.IsDBNull(dr[17])) objDVOJournalDetailsEntry.acct_desc = Convert.ToString(dr[17]);
            if (!Convert.IsDBNull(dr[18])) objDVOJournalDetailsEntry.keyvalue = Convert.ToString(dr[18]);
            if (!Convert.IsDBNull(dr[19])) objDVOJournalDetailsEntry.AccountType = Convert.ToString(dr[19]);
            if (!Convert.IsDBNull(dr[20])) objDVOJournalDetailsEntry.AccountTypeId = Convert.ToInt32(dr[20]);
            if (!Convert.IsDBNull(dr[22])) objDVOJournalDetailsEntry.Rowid = Convert.ToInt32(dr[22]);

        }
        /// <summary>
        /// Calling data layer's get All Data
        /// Passing no Parameters since we are accessing all the data
        /// </summary>
        /// <returns>List of records in form of generic list of Type lstDVOJournalHeaderEntry Defined in the common Layer</returns>
        public static List<DVOJournalHeaderEntry> GetAllJournalEntries()
        {
            List<DVOJournalHeaderEntry> lstDVOJournalHeaderEntry = new List<DVOJournalHeaderEntry>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOJournalHeaderEntry)))
                {

                    //**************************Updated by sanjay*********************//
                    //SET COLUMN NAMES
                    ds.Tables[0].Columns[0].ColumnName = "doc_no";
                    ds.Tables[0].Columns[1].ColumnName = "doc_desc";
                    ds.Tables[0].Columns[2].ColumnName = "doc_date";
                    ds.Tables[0].Columns[3].ColumnName = "doc_src";
                    ds.Tables[0].Columns[4].ColumnName = "auto_rev";
                    ds.Tables[0].Columns[5].ColumnName = "file_type";
                    ds.Tables[0].Columns[6].ColumnName = "posted";
                    ds.Tables[0].Columns[7].ColumnName = "ok_to_post";
                    ds.Tables[0].Columns[8].ColumnName = "batch_id";
                    ds.Tables[0].Columns[9].ColumnName = "user_id";
                    ds.Tables[0].Columns[10].ColumnName = "orig_journal";
                    ds.Tables[0].Columns[11].ColumnName = "line_no";
                    ds.Tables[0].Columns[12].ColumnName = "acct_no";
                    ds.Tables[0].Columns[13].ColumnName = "department";
                    ds.Tables[0].Columns[14].ColumnName = "amount";
                    ds.Tables[0].Columns[15].ColumnName = "debit_credit";
                    ds.Tables[0].Columns[16].ColumnName = "src_desc";
                    ds.Tables[0].Columns[17].ColumnName = "acct_desc";
                    ds.Tables[0].Columns[18].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[19].ColumnName = "acct_type";
                    ds.Tables[0].Columns[20].ColumnName = "acct_type_id";

                    //*****************************************************************

                    int doc_no = 0;
                    int index = -1;
                    if (ds.Tables[0].Rows.Count != 0) 
                    doc_no = ds.Tables[0].Rows[0]["doc_no"] == DBNull.Value ? doc_no = 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["doc_no"]);
                    DVOJournalHeaderEntry objDVOJournalHeaderEntry = null;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int temp = dr["doc_no"] == DBNull.Value ? temp = 0 : temp = Convert.ToInt32(dr["doc_no"]);
                        if (doc_no == temp)
                        {
                            if (objDVOJournalHeaderEntry == null)
                            {
                                index++;
                                //                            dbo.stgjoure.doc_no, dbo.stgjoure.doc_desc, dbo.stgjoure.doc_date, dbo.stgjoure.doc_src,
                                //dbo.stgjoure.auto_rev, dbo.stgjoure.file_type,dbo.stgjoure.posted, dbo.stgjoure.ok_to_post, 
                                //dbo.stgjoure.batch_id, dbo.stgjoure.user_id, 

                                objDVOJournalHeaderEntry = new DVOJournalHeaderEntry();
                                FillHeader(ref objDVOJournalHeaderEntry, dr);

                                addtoList(ref lstDVOJournalHeaderEntry, ref objDVOJournalHeaderEntry, index);
                            }
                            DVOJournalDetailsEntry objDVOJournalDetailsEntry = new DVOJournalDetailsEntry();
                            FillDetails(ref objDVOJournalDetailsEntry, dr);
                            addDetailsToHeader(ref lstDVOJournalHeaderEntry, ref objDVOJournalDetailsEntry, index);
                            doc_no = temp;
                        }
                        else
                        {
                            int temp1 = dr["doc_no"] == DBNull.Value ? temp = 0 : temp = Convert.ToInt32(dr["doc_no"]);
                            objDVOJournalHeaderEntry = null;
                            index++;
                            objDVOJournalHeaderEntry = new DVOJournalHeaderEntry();
                            FillHeader(ref objDVOJournalHeaderEntry, dr);
                            addtoList(ref lstDVOJournalHeaderEntry, ref objDVOJournalHeaderEntry, index);

                            DVOJournalDetailsEntry objDVOJournalDetailsEntry = new DVOJournalDetailsEntry();
                            FillDetails(ref objDVOJournalDetailsEntry, dr);
                            addDetailsToHeader(ref lstDVOJournalHeaderEntry, ref objDVOJournalDetailsEntry, index);
                            doc_no = temp1;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOJournalHeaderEntry;
        }
        private static void FillHeader(ref DVOJournalHeaderEntry objDVOJournalHeaderEntry, DataRow dr)
        {
            if (!Convert.IsDBNull(dr[0])) objDVOJournalHeaderEntry.doc_no = Convert.ToInt64(dr[0]);//doc_no
            if (!Convert.IsDBNull(dr[1])) objDVOJournalHeaderEntry.doc_desc = Convert.ToString(dr[1]);//doc_desc
            if (!Convert.IsDBNull(dr[2])) objDVOJournalHeaderEntry.doc_date = dr[2].ToString();//doc_date
            if (!Convert.IsDBNull(dr[3])) objDVOJournalHeaderEntry.doc_src = Convert.ToString(dr[3]);//doc_src
            if (!Convert.IsDBNull(dr[4])) objDVOJournalHeaderEntry.auto_rev = Convert.ToString(dr[4]);//auto_rev
            if (!Convert.IsDBNull(dr[5])) objDVOJournalHeaderEntry.file_type = Convert.ToString(dr[5]);//file_type
            if (!Convert.IsDBNull(dr[6])) objDVOJournalHeaderEntry.posted = Convert.ToString(dr[6]);//posted
            if (!Convert.IsDBNull(dr[7])) objDVOJournalHeaderEntry.ok_to_post = Convert.ToString(dr[7]);//ok_to_post
            if (!Convert.IsDBNull(dr[8])) objDVOJournalHeaderEntry.batch_id = Convert.ToInt32(dr[8]);//batch_id
            if (!Convert.IsDBNull(dr[9])) objDVOJournalHeaderEntry.user_id = Convert.ToString(dr[9]);//user_id
            if (!Convert.IsDBNull(dr[16])) objDVOJournalHeaderEntry.src_desc = Convert.ToString(dr[16]);//user_id
            //*******************************Added by Sunil Pahwa on 27/01/09 *********************************
            if (!Convert.IsDBNull(dr[21])) objDVOJournalHeaderEntry.rowid = Convert.ToInt32(Convert.ToString(dr[21])); ;//rowid
            //***************************************************************************************************

        }

        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOApprovalSystemUserInfo Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static int InsertJournalEntries(ref DVOJournalHeaderEntry objDVOJournalHeaderEntry)
        {
            int Success = 0;
            object[] headerparameters = new object[9];
            //Commented by sanjay, doc_no is not required 
            //headerparameters[0] = objDVOJournalHeaderEntry.doc_no;
            headerparameters[0] = objDVOJournalHeaderEntry.doc_desc;
            headerparameters[1] = objDVOJournalHeaderEntry.doc_date;
            headerparameters[2] = objDVOJournalHeaderEntry.doc_src;
            headerparameters[3] = objDVOJournalHeaderEntry.auto_rev;
            headerparameters[4] = objDVOJournalHeaderEntry.file_type;
            headerparameters[5] = objDVOJournalHeaderEntry.posted;
            headerparameters[6] = objDVOJournalHeaderEntry.ok_to_post;
            headerparameters[7] = objDVOJournalHeaderEntry.batch_id;
            headerparameters[8] = objDVOJournalHeaderEntry.user_id;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref headerparameters, typeof(DVOJournalHeaderEntry));
                if (ds.Tables.Count > 0)
                {
                    objDVOJournalHeaderEntry.doc_no = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                }
                List<DVOJournalDetailsEntry> lstDVOJournalDetailsEntry = new List<DVOJournalDetailsEntry>();
                lstDVOJournalDetailsEntry = (List<DVOJournalDetailsEntry>)objDVOJournalHeaderEntry.DVOJournalDetailsEntries;
               
                foreach (DVOJournalDetailsEntry obj in lstDVOJournalDetailsEntry)
                {
                    object[] detailparameters = new object[7];
                    detailparameters[0] = "GJ";
                    detailparameters[1] = objDVOJournalHeaderEntry.doc_no;
                    detailparameters[2] = 1;
                    detailparameters[3] = obj.acct_no;
                    detailparameters[4] = "000";
                    detailparameters[5] = obj.amountdc;
                    if (obj.debit_credit == "CR")
                    {
                        obj.debit_credit = "C";
                    }
                    else
                    {
                        obj.debit_credit = "D";
                    }
                    detailparameters[6] = obj.debit_credit;

                    int docno=objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref detailparameters, typeof(DVOJournalDetailsEntry));
                    detailparameters = null;
                    
                }
                
                    //update the table(stgcntrc) with docno                       
                    object[] parameters = new object[11];

                    parameters[0] = objDVOJournalHeaderEntry.doc_no;
                    parameters[1] = objDVOJournalHeaderEntry.doc_desc;
                    parameters[2] = objDVOJournalHeaderEntry.doc_date;
                    parameters[3] = objDVOJournalHeaderEntry.doc_src;
                    parameters[4] = objDVOJournalHeaderEntry.auto_rev;
                    parameters[5] = objDVOJournalHeaderEntry.file_type;
                    parameters[6] = objDVOJournalHeaderEntry.posted;
                    parameters[7] = objDVOJournalHeaderEntry.ok_to_post;
                    parameters[8] = objDVOJournalHeaderEntry.batch_id;
                    parameters[9] = objDVOJournalHeaderEntry.user_id;
                    objDVOJournalHeaderEntry.check = 1;
                    parameters[10] = objDVOJournalHeaderEntry.check;
                    objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOJournalHeaderEntry));
                
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
        public static int DeleteJournalEntries(ref DVOJournalHeaderEntry objDVOJournalHeaderEntry)
        {
            int Success = 0;
            object[] headerparameters = new object[1];
            headerparameters[0] = objDVOJournalHeaderEntry.doc_no;
            object[] detailparameters = new object[1];
            detailparameters[0] = objDVOJournalHeaderEntry.doc_no;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                Success = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref headerparameters, typeof(DVOJournalHeaderEntry));
                //if (Success >= 0)
                //{
                //    Success = objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref detailparameters, typeof(DVOJournalDetailsEntry));
                //}
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
        //public static int UpdateJournalEntries(ref DVOJournalHeaderEntry objDVOJournalHeaderEntry)
        //{
        //    int Success = 0;
        //    object[] headerparameters = new object[11];
        //    headerparameters[0] = objDVOJournalHeaderEntry.doc_no;
        //    headerparameters[1] = objDVOJournalHeaderEntry.doc_desc;
        //    headerparameters[2] = objDVOJournalHeaderEntry.doc_date;
        //    headerparameters[3] = objDVOJournalHeaderEntry.doc_src;
        //    headerparameters[4] = objDVOJournalHeaderEntry.auto_rev;
        //    headerparameters[5] = objDVOJournalHeaderEntry.file_type;
        //    headerparameters[6] = objDVOJournalHeaderEntry.posted;
        //    headerparameters[7] = objDVOJournalHeaderEntry.ok_to_post;
        //    headerparameters[8] = objDVOJournalHeaderEntry.batch_id;
        //    headerparameters[9] = objDVOJournalHeaderEntry.user_id;
        //    headerparameters[10] = objDVOJournalHeaderEntry.check;
        //    DALBaseClass objDALBaseClass = DALBaseClassHelper.GetDAL();
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    }
        //    try
        //    {
        //        objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref headerparameters, typeof(DVOJournalHeaderEntry));
        //        List<DVOJournalDetailsEntry> lstDVOJournalDetailsEntry = new List<DVOJournalDetailsEntry>();
        //        lstDVOJournalDetailsEntry = (List<DVOJournalDetailsEntry>)objDVOJournalHeaderEntry.DVOJournalDetailsEntries;
        //        foreach (DVOJournalDetailsEntry obj in lstDVOJournalDetailsEntry)
        //        {
        //            object[] detailparameters = new object[8];
        //            detailparameters[0] = "GJ";
        //            detailparameters[1] = objDVOJournalHeaderEntry.doc_no;
        //            detailparameters[2] = 1;
        //            detailparameters[3] = obj.acct_no;
        //            detailparameters[4] = "000";
        //            detailparameters[5] = obj.amountdc;
        //            detailparameters[6] = obj.debit_credit;
        //            detailparameters[7] = obj.Editable;

        //            Success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref detailparameters, typeof(DVOJournalDetailsEntry));
        //            detailparameters = null;
        //        }
        //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objTransaction = null;
        //        AllSuccessFullyLocked = true;
        //    }
        //    return Success;
        //}

        //Method to get document source type
        public static List<DVOJournalHeaderEntry> GetSource(ref DVOJournalHeaderEntry objJournalHeaderEntry)
        {
            object[] Parameter = new object[0];
            List<DVOJournalHeaderEntry> lstJournalHeaderEntry = new List<DVOJournalHeaderEntry>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOJournalHeaderEntry), objJournalHeaderEntry.Get_Document_source))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOJournalHeaderEntry obj = new DVOJournalHeaderEntry();
                    obj.src_key = dr[0].ToString().Trim();//"p_src_key"
                    obj.src_desc = dr[1].ToString().Trim();//"p_src_desc"
                    lstJournalHeaderEntry.Add(obj);

                }
                return lstJournalHeaderEntry;


            }
        }

        public static void UpdateJournalEntries(ref object TransactionObject, ref DVOJournalHeaderEntry UpdateDVOJournalHeaderEntry,
           ref List<DVOJournalDetailsEntry> listNewDVOJournalDetailsEntry,
              ref  List<DVOJournalDetailsEntry> listModifiedDVOJournalDetailsEntry,
              ref  List<DVOJournalDetailsEntry> listDeleteDVOJournalDetailsEntry, bool IsAllDelete)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            AllSuccessFullyLocked = true;
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int success = 0;

            object[] headerparameters = new object[11];
            headerparameters[0] = UpdateDVOJournalHeaderEntry.doc_no;
            headerparameters[1] = UpdateDVOJournalHeaderEntry.doc_desc;
            headerparameters[2] = UpdateDVOJournalHeaderEntry.doc_date;
            headerparameters[3] = UpdateDVOJournalHeaderEntry.doc_src;
            headerparameters[4] = UpdateDVOJournalHeaderEntry.auto_rev;
            headerparameters[5] = UpdateDVOJournalHeaderEntry.file_type;
            headerparameters[6] = UpdateDVOJournalHeaderEntry.posted;
            headerparameters[7] = UpdateDVOJournalHeaderEntry.ok_to_post;
            headerparameters[8] = UpdateDVOJournalHeaderEntry.batch_id;
            headerparameters[9] = UpdateDVOJournalHeaderEntry.user_id;
            headerparameters[10] = UpdateDVOJournalHeaderEntry.check;
            try
            {


                object o = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref headerparameters, UpdateDVOJournalHeaderEntry.UPD_STGJOURE, true);
                if (o == DBNull.Value || o == null || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
                    throw new Exception("Error has occurred while updaiong header info.");

                if (listNewDVOJournalDetailsEntry.Count > 0)
                    UpdaetData(ref objTransaction, listNewDVOJournalDetailsEntry, "ADD");

                if (listModifiedDVOJournalDetailsEntry.Count > 0)
                    UpdaetData(ref objTransaction, listModifiedDVOJournalDetailsEntry, "UPD");

                if (IsAllDelete)
                {
                    DeleteAllDetails(ref objTransaction, UpdateDVOJournalHeaderEntry.doc_no);
                }
                else
                {
                    if (listDeleteDVOJournalDetailsEntry.Count > 0)
                        UpdaetData(ref objTransaction, listDeleteDVOJournalDetailsEntry, "DEL");
                }

                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }
        public static void UpdaetData(ref object TransactionObject, object objP,string RowStatus)
        {
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                List<DVOJournalDetailsEntry> objList = (List<DVOJournalDetailsEntry>)objP;
                object[] detailparameters = new object[9];
                foreach (DVOJournalDetailsEntry obj in objList)
                {
                    detailparameters[0] = "GJ";
                    detailparameters[1] = obj.doc_no;
                    detailparameters[2] = 1;
                    detailparameters[3] = obj.acct_no;
                    detailparameters[4] = "000";
                    detailparameters[5] = obj.amountdc;
                    detailparameters[6] = obj.debit_credit;
                    detailparameters[7] = obj.Rowid;
                    detailparameters[8] = RowStatus;

                    object o = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref detailparameters, objList[0].UPD_STGJOURD,true);
                    if (o == DBNull.Value || o == null || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
                        throw new Exception("Error has occurred while updaiong detail info.");
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }

        }

        public static void DeleteAllDetails(ref object TransactionObject, long doc_no)
        {
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                DVOJournalDetailsEntry obj = new DVOJournalDetailsEntry();
                object[] parameters = new object[1];
                parameters[0] = doc_no;
                objDALBaseClass.DeleteData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOJournalDetailsEntry));
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }

        }

        public static int UpdateJournalEntriesInfo(ref object TransactionObject, ref DVOJournalHeaderEntry UpdateDVOJournalHeaderEntry)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            AllSuccessFullyLocked = true;
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int success = 0;

            object[] headerparameters = new object[11];
            headerparameters[0] = UpdateDVOJournalHeaderEntry.doc_no;
            headerparameters[1] = UpdateDVOJournalHeaderEntry.doc_desc;
            headerparameters[2] = UpdateDVOJournalHeaderEntry.doc_date;
            headerparameters[3] = UpdateDVOJournalHeaderEntry.doc_src;
            headerparameters[4] = UpdateDVOJournalHeaderEntry.auto_rev;
            headerparameters[5] = UpdateDVOJournalHeaderEntry.file_type;
            headerparameters[6] = UpdateDVOJournalHeaderEntry.posted;
            headerparameters[7] = UpdateDVOJournalHeaderEntry.ok_to_post;
            headerparameters[8] = UpdateDVOJournalHeaderEntry.batch_id;
            headerparameters[9] = UpdateDVOJournalHeaderEntry.user_id;
            headerparameters[10] = UpdateDVOJournalHeaderEntry.check;
            ////DALBaseClass objDALBaseClass = DALBaseClassHelper.GetDAL();
            //if (objTransaction == null)
            //{
            //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //}
            try
            {
                objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref headerparameters, typeof(DVOJournalHeaderEntry));
                List<DVOJournalDetailsEntry> lstDVOJournalDetailsEntry = new List<DVOJournalDetailsEntry>();
                lstDVOJournalDetailsEntry = (List<DVOJournalDetailsEntry>)UpdateDVOJournalHeaderEntry.DVOJournalDetailsEntries;
                foreach (DVOJournalDetailsEntry obj in lstDVOJournalDetailsEntry)
                {
                    object[] detailparameters = new object[8];
                    detailparameters[0] = "GJ";
                    detailparameters[1] = UpdateDVOJournalHeaderEntry.doc_no;
                    detailparameters[2] = 1;
                    detailparameters[3] = obj.acct_no;
                    detailparameters[4] = "000";
                    detailparameters[5] = obj.amountdc;
                    detailparameters[6] = obj.debit_credit;
                    detailparameters[7] = obj.Editable;

                    success = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref detailparameters, typeof(DVOJournalDetailsEntry));
                    detailparameters = null;
                }
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }


    }
}
  