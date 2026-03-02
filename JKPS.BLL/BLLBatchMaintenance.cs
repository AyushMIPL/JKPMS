using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JKPS.DL;
using JKPS.COMMON;
using ExceptionManagement;
using JKPS.CommonUtilities;

namespace JKPS.BLL
{
    public class BLLBatchMaintenance
    {

        /// <summary>
        /// Assigning data captured from Data Entry Screen to object array
        /// Calling data layer's Update Data method Passing object array
        /// Also passing type of LedgerAccount so that stored procedure is picked up from Common Layer
        /// </summary>
        /// <param name="objDVOGeneralLedger">object Reference of the DVOApprovalSystemUserInfo Object i.e Data collected from Data Entry Screen</param>
        /// <returns>Returns the integer value of No of Record affected from Data layer</returns>
        public static int CreateNewBatch(ref DVOBatch objDVOBatch)
        {
            int _batchId = -1;
            object[] parameters = new object[15];
            parameters[0] = objDVOBatch.batch_id;
            parameters[1] = objDVOBatch.batch_type;
            parameters[2] = objDVOBatch.batch_status;
            parameters[3] = objDVOBatch.owner;
            parameters[4] = objDVOBatch.created_by;
            //Uncommented By Rohit on 05/11/2008 , because it was showing errror that it needs more parameters.
            //cheked informix stored procedure it was expecting p_create_date date,p_create_time char(8), also

            if (objDVOBatch.create_date == string.Empty)
            {
                parameters[5] = DBNull.Value;
            }
            else
            {
                parameters[5] = objDVOBatch.create_date;
            }
            parameters[6] = objDVOBatch.create_time;
            /////****************************************Place up to which was uncommented 
            parameters[7] = objDVOBatch.approved_by;
            if (objDVOBatch.approve_date == string.Empty)
            {
                parameters[8] = DBNull.Value;
            }
            else
            {
                parameters[8] = objDVOBatch.approve_date;
            }
            parameters[9] = objDVOBatch.approve_time;
            parameters[10] = objDVOBatch.posted_by;
            if (objDVOBatch.post_date == string.Empty)
            {
                parameters[11] = DBNull.Value;
            }
            else
            {
                parameters[11] = objDVOBatch.post_date;
            }
            parameters[12] = objDVOBatch.post_time;
            parameters[13] = objDVOBatch.post_seq;
            parameters[14] = objDVOBatch.total_trx;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                _batchId = Convert.ToInt32(objDALBaseClass.InsertData(ref parameters, typeof(DVOBatch), true));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return _batchId;
                //throw ex;
            }
            return _batchId;
        }
        public static int CreateNewBatch(ref DVOBatch objDVOBatch, ref object objTransaction)
        {
            int _batchId = -1;
            object[] parameters = new object[15];
            parameters[0] = objDVOBatch.batch_id;
            parameters[1] = objDVOBatch.batch_type;
            parameters[2] = objDVOBatch.batch_status;
            parameters[3] = objDVOBatch.owner;
            parameters[4] = objDVOBatch.created_by;
            //Uncommented By Rohit on 05/11/2008 , because it was showing errror that it needs more parameters.
            //cheked informix stored procedure it was expecting p_create_date date,p_create_time char(8), also

            if (objDVOBatch.create_date == string.Empty)
            {
                parameters[5] = DBNull.Value;
            }
            else
            {
                parameters[5] = objDVOBatch.create_date;
            }
            parameters[6] = objDVOBatch.create_time;
            /////****************************************Place up to which was uncommented 
            parameters[7] = objDVOBatch.approved_by;
            if (objDVOBatch.approve_date == string.Empty)
            {
                parameters[8] = DBNull.Value;
            }
            else
            {
                parameters[8] = objDVOBatch.approve_date;
            }
            parameters[9] = objDVOBatch.approve_time;
            parameters[10] = objDVOBatch.posted_by;
            if (objDVOBatch.post_date == string.Empty)
            {
                parameters[11] = DBNull.Value;
            }
            else
            {
                parameters[11] = objDVOBatch.post_date;
            }
            parameters[12] = objDVOBatch.post_time;
            parameters[13] = objDVOBatch.post_seq;
            parameters[14] = objDVOBatch.total_trx;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object o = objDALBaseClass.InsertData_ByTransaction(ref objTransaction,ref parameters, typeof(DVOBatch),true);
                if (o == DBNull.Value || o.ToString().Trim().Length <= 0)
                    throw new Exception();
                else
                    _batchId = Convert.ToInt32(o);
            }
            catch (Exception ex)
            {    

                ExceptionManager.Publish(ex);
                return _batchId;
                //throw ex;
            }
            return _batchId;
        }

        //public static List<DVOBatch> FetchCompleteBatchInformation()
        //{
        //    List<DVOBatch> lstDVOBatch = new List<DVOBatch>();
        //    DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        using (DataSet ds = objDalBaseClass.GetAllData(typeof(DVOBatch)))
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                DVOBatch objDVOBatch = new DVOBatch();
        //                objDVOBatch.batch_id = dr[0] == DBNull.Value ? objDVOBatch.batch_id = 0 : objDVOBatch.batch_id = Convert.ToInt32(dr[0]);//batch_id
        //                objDVOBatch.batch_type = dr[1] == DBNull.Value ? objDVOBatch.batch_type = string.Empty : objDVOBatch.batch_type = Convert.ToString(dr[1]);//batch_type
        //                objDVOBatch.batch_status = dr[2] == DBNull.Value ? objDVOBatch.batch_status = string.Empty : objDVOBatch.batch_status = Convert.ToString(dr[2]);//batch_id
        //                objDVOBatch.owner = dr[3] == DBNull.Value ? objDVOBatch.owner = string.Empty : objDVOBatch.owner = Convert.ToString(dr[3]);//batch_id
        //                objDVOBatch.created_by = dr[4] == DBNull.Value ? objDVOBatch.created_by = string.Empty : objDVOBatch.created_by = Convert.ToString(dr[4]);//batch_id
        //                objDVOBatch.create_date = dr[5] == DBNull.Value ? objDVOBatch.create_date = DateTime.MinValue : objDVOBatch.create_date = Convert.ToDateTime(dr[5]);//batch_id
        //                objDVOBatch.create_time = dr[6] == DBNull.Value ? objDVOBatch.create_time = string.Empty : objDVOBatch.create_time = Convert.ToString(dr[6]);//batch_id
        //                objDVOBatch.approved_by = dr[7] == DBNull.Value ? objDVOBatch.approved_by = string.Empty : objDVOBatch.approved_by = Convert.ToString(dr[7]);//batch_id
        //                objDVOBatch.approve_date = dr[8] == DBNull.Value ? objDVOBatch.approve_date = DateTime.MinValue : objDVOBatch.approve_date = Convert.ToDateTime(dr[8]);//batch_id
        //                objDVOBatch.approve_time = dr[9] == DBNull.Value ? objDVOBatch.approve_time = string.Empty : objDVOBatch.approve_time = Convert.ToString(dr[9]);//batch_id
        //                objDVOBatch.posted_by = dr[10] == DBNull.Value ? objDVOBatch.posted_by = string.Empty : objDVOBatch.posted_by = Convert.ToString(dr[10]);//batch_id
        //                objDVOBatch.post_date = dr[11] == DBNull.Value ? objDVOBatch.post_date = DateTime.Now : objDVOBatch.post_date = Convert.ToDateTime(dr[11]);//batch_id
        //                objDVOBatch.post_time = dr[12] == DBNull.Value ? objDVOBatch.post_time = string.Empty : objDVOBatch.post_time = Convert.ToString(dr[12]);//batch_id
        //                objDVOBatch.post_seq = dr[13] == DBNull.Value ? objDVOBatch.post_seq = 0 : objDVOBatch.post_seq = Convert.ToInt32(dr[13]);//batch_id
        //                objDVOBatch.total_trx = dr[14] == DBNull.Value ? objDVOBatch.total_trx = 0 : objDVOBatch.total_trx = Convert.ToInt32(dr[14]);//batch_id
        //                lstDVOBatch.Add(objDVOBatch);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        throw ex;
        //    }
        //    return lstDVOBatch;
        //}
        public static List<DVOBatch> FetchBatchInformation(ref DVOBatch SearchDVOBatch)
        {
            List<DVOBatch> lstDVOBatch = new List<DVOBatch>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[12];
            parameters[0] = SearchDVOBatch.batch_id;
            parameters[1] = SearchDVOBatch.batch_type;
            parameters[2] = SearchDVOBatch.batch_status;
            parameters[3] = SearchDVOBatch.owner;
            parameters[4] = SearchDVOBatch.created_by;
            if (SearchDVOBatch.create_date == string.Empty)
                SearchDVOBatch.create_date = null;
                parameters[5] = SearchDVOBatch.create_date != null ? SearchDVOBatch.create_date : null;
            //parameters[6] = SearchDVOBatch.create_time;
            parameters[6] = SearchDVOBatch.approved_by;
            if (SearchDVOBatch.approve_date == string.Empty)
                SearchDVOBatch.approve_date = null;
            parameters[7] = SearchDVOBatch.approve_date != null ? SearchDVOBatch.approve_date : null;
            //parameters[9] = SearchDVOBatch.approve_time;
            parameters[8] = SearchDVOBatch.posted_by;
            if (SearchDVOBatch.post_date == string.Empty)
                SearchDVOBatch.post_date = null;
            parameters[9] = SearchDVOBatch.post_date != null ? SearchDVOBatch.post_date : null;
            //parameters[12] = SearchDVOBatch.post_time;
            parameters[10] = SearchDVOBatch.post_seq;
            parameters[11] = SearchDVOBatch.total_trx;
            try
            {
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, SearchDVOBatch.GetType()))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOBatch objDVOBatch = new DVOBatch();
                        objDVOBatch.batch_id = dr[0] == DBNull.Value ? -1 : Convert.ToInt32(dr[0]);//batch_id
                        objDVOBatch.batch_type = dr[1] == DBNull.Value ? string.Empty : dr[1].ToString().Trim();//batch_type
                        objDVOBatch.batch_status = dr[2] == DBNull.Value ? string.Empty : dr[2].ToString().Trim();//batch_id
                        objDVOBatch.owner = dr[3] == DBNull.Value ? string.Empty : dr[3].ToString().Trim();//batch_id
                        objDVOBatch.created_by = dr[4] == DBNull.Value ? string.Empty : dr[4].ToString().Trim();//batch_id
                        objDVOBatch.create_date = dr[5] == DBNull.Value ? string.Empty : dr[5].ToString().Trim();//batch_id
                        //if (objDVOBatch.create_date!=string.Empty)
                        //{
                        //    objDVOBatch.create_date = Convert.ToDateTime(objDVOBatch.create_date).ToString("MM/dd/yyyy");
                        //}
                        objDVOBatch.create_time = dr[6] == DBNull.Value ? string.Empty : dr[6].ToString().Trim();//batch_id
                        objDVOBatch.approved_by = dr[7] == DBNull.Value ? string.Empty : dr[7].ToString().Trim();//batch_id
                        objDVOBatch.approve_date = dr[8] == DBNull.Value ? string.Empty : dr[8].ToString().Trim();//batch_id
                        //if (objDVOBatch.approve_date != string.Empty)
                        //{
                        //    objDVOBatch.approve_date = Convert.ToDateTime(objDVOBatch.approve_date).ToString("MM/dd/yyyy");
                        //}
                        objDVOBatch.approve_time = dr[9] == DBNull.Value ? string.Empty : dr[9].ToString().Trim();//batch_id
                        objDVOBatch.posted_by = dr[10] == DBNull.Value ? string.Empty : dr[10].ToString().Trim();//batch_id
                        objDVOBatch.post_date = dr[11] == DBNull.Value ? string.Empty : dr[11].ToString().Trim();//batch_id
                        //if (objDVOBatch.post_date != string.Empty)
                        //{
                        //    objDVOBatch.post_date = Convert.ToDateTime(objDVOBatch.post_date).ToString("MM/dd/yyyy");
                        //}
                        objDVOBatch.post_time = dr[12] == DBNull.Value ? string.Empty : dr[12].ToString().Trim();//batch_id
                        objDVOBatch.post_seq = dr[13] == DBNull.Value ? 0 : Convert.ToInt32(dr[13]);//batch_id
                        objDVOBatch.total_trx = dr[14] == DBNull.Value ? 0 : Convert.ToInt32(dr[14]);//batch_id
                        lstDVOBatch.Add(objDVOBatch);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return lstDVOBatch;
        }

        //********************************Added by Bharat Dhall****************************
        public static string GetBatchAppcode(string BatchType)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string BatchAppCode = string.Empty;
            try
            {
                object[] parameters = new object[1];
                parameters[0] = BatchType;
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), (new DVOBatch()).GET_BATCH_APPROVAL_CODE))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            BatchAppCode = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? ds.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;//"approval_code"
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return BatchAppCode;
        }
        //****************Updated/Written  by Rohit Wadhwa***********************
        //*****************11/10/2008***********************************
        //Written for Posting a Batch ***********************************
        public static bool PostBatch(ref DVOBatch objDVOBatch)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool result=true;
            try
            {
                object[] parameteres=new object[6];
                parameteres[0] = objDVOBatch.batch_id;
                parameteres[1] = objDVOBatch.batch_type;
                parameteres[2] = objDVOBatch.batch_user;
                parameteres[3] = objDVOBatch.post_seq;
                parameteres[4] = objDVOBatch.total_trx;
                parameteres[5] = objDVOBatch.batch_err;
                object  result1= objDalBaseClass.ExecuteScalar(ref parameteres, objDVOBatch.POST_BATCH);
                objDalBaseClass = null;

                if (Convert.ToInt32(result1)!=1)
                {
                    result = false;
                }          
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return result;
        }
            //*************************END BATCH POSTING**********************************
        public static void SetAsCurrentBatch(ref DVOBatch objDVOBatch)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objDVOBatch.batch_id;
                parameters[1] = objDVOBatch.batch_type;
                parameters[2] = objDVOBatch.owner;

                objDalBaseClass.ExecuteProcedure(ref parameters, objDVOBatch.SET_AS_CURRENT_BATCH);
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        public static bool SetAsCurrentBatch(ref DVOBatch objDVOBatch, ref object objTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objDVOBatch.batch_id;
                parameters[1] = objDVOBatch.batch_type;
                parameters[2] = objDVOBatch.owner;

                objDalBaseClass.ExecuteProcedure(ref parameters, objDVOBatch.SET_AS_CURRENT_BATCH);
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        public static bool GetBatchCancelStatus(ref DVOBatch objDVOBatch)
        {
            int BatchCount = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objDVOBatch.batch_id;
                parameters[1] = objDVOBatch.batch_type;

                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), objDVOBatch.GET_BATCH_CANCEL_STATUS))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            BatchCount = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"bat_count"
                }
                objDalBaseClass = null;
                if (BatchCount == 0) return true;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }

        public static void BatchCancel(ref DVOBatch objDVOBatch)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOBatch.batch_id;

                objDalBaseClass.ExecuteProcedure(ref parameters, objDVOBatch.BATCH_CANCEL);
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        public static void GetBatchInfo(string UserLoginId,string BatchType,out string CurrUser,out int CurrBatchId)//, out bool IsMasterBatch)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            CurrUser = string.Empty;
            CurrBatchId = -1;
            //IsMasterBatch = false;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = BatchType;
                parameters[1] = UserLoginId;
                //*********  Commented by Bharat Dhall (27 November, 2008)**********************
                // SP/PropertyName changed , but didn't assign proper parameters or use resulted dataset
                //
                ////SP/PROPERTY NAME Changed by ROHIT (06/11/2008)**********************
                //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), (new DVOBatch()).GET_BATCH_INFORMATION))
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), (new DVOBatch()).GET_BATCH_INFO))
                //******************************************************************************
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            CurrUser = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? ds.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;//"CurUser"
                            CurrBatchId = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : -1;//"BtchId"
                        }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        public static void GetActiveBatchForReports(string UserLoginId, string BatchType, out string CurrUser, out int CurrBatchId, out bool IsMasterBatch)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            CurrUser = string.Empty;
            CurrBatchId = -1;
            IsMasterBatch = false;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = BatchType;
                parameters[1] = UserLoginId;
                using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOBatch), (new DVOBatch()).GET_BATCH_INFO))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            CurrUser = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? ds.Tables[0].Rows[0][0].ToString().Trim() : string.Empty;//"CurUser"
                            CurrBatchId = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : -1;//"BtchId"
                            if (CurrBatchId == 0)
                            {
                                IsMasterBatch = true;
                                CurrBatchId = -1;
                                List<DVOBatch> lstDVOBatch = new List<DVOBatch>();
                                DVOBatch obj = new DVOBatch();
                                obj.batch_status = "ACT";
                                obj.batch_type = BatchType;
                                obj.owner = (DVOApplicationUserInfo.LoginId.Trim().Length > 20) ? DVOApplicationUserInfo.LoginId.Trim().Substring(20) : DVOApplicationUserInfo.LoginId.Trim();
                                lstDVOBatch = BLLBatchMaintenance.FetchBatchInformation(ref obj);
                                if (lstDVOBatch != null && lstDVOBatch.Count > 0)
                                    CurrBatchId = lstDVOBatch[0].batch_id;
                                obj = null;
                                lstDVOBatch = null;
                            }
                        }
                }
                objDalBaseClass = null;
            }
            catch (Exception ex)
            {
                objDalBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        //************************************************************************************
        //******* Written  by Sarvjeet on 27 Nov. 2008 **********
        //**** To update total trx after posting the documents *****  
        public static int Updatetotal_trx(int batchid,int doc_count)
        { 
          object[] Parameters=new object[2];
          Parameters[0] = batchid;
          Parameters[1] = doc_count;

          DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
          DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
          int Result = objDALBaseClass.UpdateData(ref Parameters, typeof(DVOBatch));
          return Result;
        }
    }
}
