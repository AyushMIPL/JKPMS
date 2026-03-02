using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
using System.Data;
/*
*    (..)
 0   ____   0
  \ / __ \ /
 --/ (__) \--
  =\      /=
    -|--|-
   | |##| |
    \ ## /
      ##
Shrishanshu Mishra
 */
namespace JKPS.BLL
{
    /// <summary>
    /// This BLL is coded for the "Update Requisition"
    /// By: Shrishanshu
    /// </summary>
    public class BLLRequitions
    {

        /// <summary>
        /// Function to Insert Header Data to "sturqste"
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static object InsertRequisitionHeader(DVOUpdateRequisitionH objDVO, ref List<DVOUpdateRequisitionD> listSearchDVOUpdateRequisitionD)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameter = new object[11];

            parameter[0] = objDVO.doc_no;
            parameter[1] = objDVO.requestor_code;
            parameter[2] = objDVO.whse_shipto;
            parameter[3] = objDVO.po_type;
            parameter[4] = objDVO.request_status;
            parameter[5] = objDVO.request_no;
            parameter[6] = objDVO.request_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameter[7] = objDVO.requiredDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameter[8] = objDVO.authorization_code;
            parameter[9] = objDVO.requestor_min;
            parameter[10] = objDVO.requestor_dept;
            object obj = null;
            try
            {
                obj = (objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVO.INSERT_SPNAME));
                if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) > 0)
                {
                    foreach (DVOUpdateRequisitionD objt in listSearchDVOUpdateRequisitionD)
                        objt.doc_no = Convert.ToInt32(obj);

                    int i = BLLRequitions.InsertRequisitionDetails(ref objTransaction, ref listSearchDVOUpdateRequisitionD);
                    if (i == 1)
                    {
                        if (objTransaction != null)
                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        return obj;
                    }
                    else
                    {
                        if (objTransaction != null)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return null;
                    }
                }
                else
                {
                    if (objTransaction != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    return null;
                }
                return obj;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return obj;
        }

        /// <summary>
        /// Function to Update Header Data to "sturqste"
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int UpdateRequisitionHeader(ref object TransactionObject, ref DVOUpdateRequisitionH objDVO, ref List<DVOUpdateRequisitionD> listSearchDVOUpdateRequisitionD)
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
                object[] parameter = new object[11];
                parameter[0] = objDVO.rowid;
                parameter[1] = objDVO.doc_no;
                parameter[2] = objDVO.requestor_code;
                parameter[3] = objDVO.whse_shipto;
                parameter[4] = objDVO.po_type;
                parameter[5] = objDVO.request_status;
                parameter[6] = objDVO.request_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameter[7] = objDVO.requiredDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameter[8] = objDVO.authorization_code;
                parameter[9] = objDVO.requestor_min;
                parameter[10] = objDVO.requestor_dept;
                object obj;
                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameter, objDVO.UPDATE_SPNAME);
                if (obj != null)
                {
                    if (obj.ToString() == "1")
                    {
                        DVOUpdateRequisitionD objDVOD = new DVOUpdateRequisitionD();
                        objDVOD.doc_no = objDVO.doc_no;
                        int i = BLLRequitions.DeleteRequisitionDetails(ref TransactionObject, ref objDVOD);
                        if (i == 1)
                        {
                            int ii = BLLRequitions.InsertRequisitionDetails(ref TransactionObject, ref listSearchDVOUpdateRequisitionD);
                            if (ii == 1)
                            {
                                if (!statusObjTransaction && TransactionObject != null)
                                    objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                                return 1;
                            }
                            else
                                throw new Exception();
                        }
                        else
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new Exception();

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }

        /// <summary>
        /// Function to Delete Header Data from "sturqste"
        /// </summary>
        /// <param name="objDVO"></param>
        /// <returns></returns>
        public static int DeleteRequisitionHeader(ref DVOUpdateRequisitionH objDVO)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVO.doc_no;
                object obj = null;
                obj = objDalBaseClass.DeleteData_ByTransaction(ref objTransection, ref parameters, typeof(DVOUpdateRequisitionH), true);//.ExecuteScalar_ByTransaction(//(ref objTransection, ref parameters, objDVO.DELETE_SPNAME,true);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();

                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        /// <summary>
        /// Function to Get Header Data from "sturqste"
        ///</summary>
        /// <returns></returns>
        public static List<DVOUpdateRequisitionH> GetAllRequitionHeader(ref DVOUpdateRequisitionH objDVODVOUpdateRequisitionH)
        {
            object[] parameters = new object[10];
            parameters[0] = objDVODVOUpdateRequisitionH.requestor_code;
            parameters[1] = objDVODVOUpdateRequisitionH.authorization_code;
            parameters[2] = objDVODVOUpdateRequisitionH.request_no;
            parameters[3] = objDVODVOUpdateRequisitionH.doc_no;
            parameters[4] = objDVODVOUpdateRequisitionH.whse_shipto;
            if (objDVODVOUpdateRequisitionH.requiredDate != Convert.ToDateTime(null))
                parameters[5] = objDVODVOUpdateRequisitionH.requiredDate;
            else
                parameters[5] = null;
            if (objDVODVOUpdateRequisitionH.request_date != Convert.ToDateTime(null))
                parameters[6] = objDVODVOUpdateRequisitionH.request_date;
            else
                parameters[6] = null;
            parameters[7] = objDVODVOUpdateRequisitionH.po_type;
            parameters[8] = objDVODVOUpdateRequisitionH.request_status;
            parameters[9] = objDVODVOUpdateRequisitionH.rowid;

            List<DVOUpdateRequisitionH> objListDVOUpdateRequisitionH = new List<DVOUpdateRequisitionH>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateRequisitionH)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOUpdateRequisitionH tempobjDVOUpdateRequisitionH = new DVOUpdateRequisitionH();

                    tempobjDVOUpdateRequisitionH.request_no = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                    tempobjDVOUpdateRequisitionH.doc_no = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                    if (dr[2] != DBNull.Value && dr[2].ToString().Trim() != string.Empty)
                        tempobjDVOUpdateRequisitionH.request_date = Convert.ToDateTime(dr[2]);
                    if (dr[3] != DBNull.Value && dr[3].ToString().Trim() != string.Empty)
                        tempobjDVOUpdateRequisitionH.requiredDate = Convert.ToDateTime(dr[3]);
                    tempobjDVOUpdateRequisitionH.requestor_code = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.authorization_code = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.request_status = dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.po_type = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.whse_shipto = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.rowid = Convert.ToInt32(dr[9]);
                    //requestor_min,requestor_dept,
                    //gl_acct_no,lvl1aprvl,lvl1aprvl_by,lvl1aprvl_date,lvl1aprvl_machinfo
                    tempobjDVOUpdateRequisitionH.requestor_min = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.requestor_dept = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.gl_acct_no = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
                    tempobjDVOUpdateRequisitionH.lvl1aprvl = dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0;

                    tempobjDVOUpdateRequisitionH.keyvalue = dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.acct_desc = dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.acct_type = dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty;

                    objListDVOUpdateRequisitionH.Add(tempobjDVOUpdateRequisitionH);
                }
            }
            return objListDVOUpdateRequisitionH;
        }

        /// <summary>
        /// Function to Get Last "Doc_No", in table "sturqste".
        /// Used for Increment and Auto Doc_No.
        /// </summary>
        /// <returns></returns>
        public static int GetLastDocNumber()
        {
            DVOUpdateRequisitionH objDVOUpdateRequisitionH = new DVOUpdateRequisitionH();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                DataSet ds = objDalBaseClass.GetData(typeof(DVOUpdateRequisitionH), objDVOUpdateRequisitionH.Get_Last_Doc_No);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0].ToString());
                }
                else
                    return 0;
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// Function to Insert Details Data to "sturqstd"
        /// </summary>
        /// <param name="objDVOD"></param>
        /// <returns></returns>
        public static int InsertRequisitionDetails(ref object objTransaction, ref  List<DVOUpdateRequisitionD> objLDVOD)
        {
            int success = 0;
            object[] parameter = new object[30];

            foreach (DVOUpdateRequisitionD objDVOD in objLDVOD)
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();

                parameter[0] = objDVOD.doc_no;
                parameter[1] = objDVOD.line_no;
                parameter[2] = objDVOD.line_type;
                parameter[3] = objDVOD.line_stage;
                parameter[4] = objDVOD.item_code;
                parameter[5] = objDVOD.desc1;
                parameter[6] = objDVOD.desc2;
                parameter[7] = objDVOD.unit;
                parameter[8] = objDVOD.ordr_qty;
                parameter[9] = objDVOD.instruct_code;
                parameter[10] = objDVOD.reference_no;
                parameter[11] = objDVOD.whse_shipto;
                parameter[12] = objDVOD.whse_billto;
                parameter[13] = objDVOD.vend_code;
                parameter[14] = objDVOD.requestor_code;
                parameter[15] = objDVOD.request_no;
                parameter[16] = objDVOD.request_date;
                parameter[17] = objDVOD.authorization_code;
                parameter[18] = objDVOD.acct_no;
                parameter[19] = objDVOD.req_post_no;
                parameter[20] = objDVOD.po_doc_no;
                parameter[21] = objDVOD.po_line_no;
                parameter[22] = objDVOD.recv_qty;
                parameter[23] = objDVOD.ref_type;
                parameter[24] = objDVOD.ref_doc_no;
                parameter[25] = objDVOD.ref_line_no;
                parameter[26] = objDVOD.ref_ship_no;
                parameter[27] = objDVOD.cost;
                parameter[28] = objDVOD.net_amount;
                parameter[29] = objDVOD.gl_acct_no;

                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                try
                {
                    DataSet dsRes = new DataSet();
                    object obj;

                    obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.INSERT_SPNAME);
                    if (obj != null)
                    {
                        if (obj.ToString() != "1")
                        {
                            return 0;
                        }
                    }
                    else
                    { return 0; }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    throw ex;
                }
            }
            return 1;
        }

        /// <summary>
        /// Function to Update Details Data to "sturqstd"
        /// </summary>
        /// <param name="objDVOD"></param>
        /// <returns></returns>
        public static int UpdateRequisitionDetails(DVOUpdateRequisitionD objDVOD)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();

            int success = 0;
            object[] parameter = new object[31];

            parameter[0] = objDVOD.rowid;
            parameter[1] = objDVOD.doc_no;
            parameter[2] = objDVOD.line_no;
            parameter[3] = objDVOD.line_type;
            parameter[4] = objDVOD.line_stage;
            parameter[5] = objDVOD.item_code;
            parameter[6] = objDVOD.desc1;
            parameter[7] = objDVOD.desc2;
            parameter[8] = objDVOD.unit;
            parameter[9] = objDVOD.ordr_qty;
            parameter[10] = objDVOD.instruct_code;
            parameter[11] = objDVOD.reference_no;
            parameter[12] = objDVOD.whse_shipto;
            parameter[13] = objDVOD.whse_billto;
            parameter[14] = objDVOD.vend_code;
            parameter[15] = objDVOD.requestor_code;
            parameter[16] = objDVOD.request_no;
            parameter[17] = objDVOD.request_date;
            parameter[18] = objDVOD.authorization_code;
            parameter[19] = objDVOD.acct_no;
            parameter[20] = objDVOD.req_post_no;
            parameter[21] = objDVOD.po_doc_no;
            parameter[22] = objDVOD.po_line_no;
            parameter[23] = objDVOD.recv_qty;
            parameter[24] = objDVOD.ref_type;
            parameter[25] = objDVOD.ref_doc_no;
            parameter[26] = objDVOD.ref_line_no;
            parameter[27] = objDVOD.ref_ship_no;
            parameter[28] = objDVOD.cost;
            parameter[29] = objDVOD.net_amount;
            parameter[30] = objDVOD.gl_acct_no;


            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object obj;

                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.UPDATE_SPNAME);
                success = Convert.ToInt32(obj);
                if (success > 0)
                {
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                if (success == 1)
                {
                    return 1;
                }

                else if (success == 2)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;

        }

        /// <summary>
        /// Function to Delete Details Data from "sturqstd"
        /// </summary>
        /// <param name="objDVOD"></param>
        /// <returns></returns>
        public static int DeleteRequisitionDetails(ref object objTransaction, ref DVOUpdateRequisitionD objDVOD)
        {
            int success = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {

                object[] parameters = new object[1];
                parameters[0] = objDVOD.doc_no;
                object obj;
                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOD.DELETE_SPNAME);
                if (obj != null)
                {
                    if (obj.ToString() == "1")
                    {
                        return 1;
                    }
                    else { return 0; }
                }
                else { return 0; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return 1;
        }

        /// <summary>
        /// Function to Get Details Data from "sturqstd"
        /// </summary>
        /// <param name="objDVOD"></param>
        /// <returns></returns>
        public static List<DVOUpdateRequisitionD> GetAllRequitionDetails(ref DVOUpdateRequisitionD objDVOD)
        {
            object[] parameter = new object[1];

            parameter[0] = objDVOD.doc_no;
            //parameter[1] = objDVOD.whse_shipto;
            //parameter[2] = objDVOD.requestor_code;
            //parameter[3] = objDVOD.request_no;
            //parameter[4] = objDVOD.request_date; 
            //parameter[5] = objDVOD.authorization_code;

            List<DVOUpdateRequisitionD> objList = new List<DVOUpdateRequisitionD>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateRequisitionD), objDVOD.FIND_SPNAME))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOUpdateRequisitionD tempobjDVOD = new DVOUpdateRequisitionD();

                    tempobjDVOD.rowid = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                    tempobjDVOD.doc_no = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                    tempobjDVOD.line_no = dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0;
                    tempobjDVOD.line_type = dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty;
                    tempobjDVOD.line_stage = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                    tempobjDVOD.item_code = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                    tempobjDVOD.desc1 = dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty;
                    tempobjDVOD.desc2 = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    tempobjDVOD.unit = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                    tempobjDVOD.ordr_qty = dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0;
                    tempobjDVOD.reference_no = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
                    tempobjDVOD.whse_shipto = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
                    tempobjDVOD.requestor_code = dr[12] != DBNull.Value ? dr[12].ToString().Trim() : string.Empty;
                    tempobjDVOD.request_no = dr[13] != DBNull.Value ? dr[13].ToString().Trim() : string.Empty;
                    if (dr[14] != DBNull.Value && dr[14].ToString().Trim() != string.Empty)
                        tempobjDVOD.request_date = Convert.ToDateTime(dr[14]);
                    tempobjDVOD.authorization_code = dr[15] != DBNull.Value ? dr[15].ToString().Trim() : string.Empty;

                    objList.Add(tempobjDVOD);
                }
            }
            return objList;
        }

        public static DataSet GetRequisitionDetails(ref DVOUpdateRequisitionD objDVOD)
        {
            DataSet ds = new DataSet();
            object[] parameter = new object[1];
            parameter[0] = objDVOD.doc_no;

            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                ds = objDalBaseClass.GetData(ref parameter, typeof(DVOUpdateRequisitionD), objDVOD.FIND_SPNAME);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }

        public static DataSet GetRequisitionDetailsForApproval(ref DVOUpdateRequisitionD objDVOD)
        {
            DataSet ds = new DataSet();
            object[] parameter = new object[1];
            parameter[0] = objDVOD.doc_no;
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                ds = objDalBaseClass.GetData(objDVOD.FIND_DATA_FOR_APPROVAL(ref parameter));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }
        public static DataSet GetRequisitionDetailsForEnquiry(ref DVOUpdateRequisitionD objDVOD)
        {
            DataSet ds = new DataSet();
            object[] parameter = new object[1];
            parameter[0] = objDVOD.doc_no;
            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                ds = objDalBaseClass.GetData(objDVOD.FIND_DETAIL_FOR_ENQUIRY(ref parameter));
                if (ds == null || ds.Tables.Count <= 0)
                    throw new Exception("Some Problem has occurred while fetching detail lines");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return ds;
        }
        public static int UpdateRequisitionDetails(ref object objTransaction, ref DVOUpdateRequisitionD objDVOD)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                /*
p_rowid integer,
p_doc_no  integer,
p_line_no smallint,
p_line_type char(3),
p_line_stage char(3),
p_item_code char(20),
p_desc1 char(30),
p_desc2 char(30),
p_unit  char(2),
p_ordr_qty decimal(12),
p_instruct_code char(6),
p_reference_no char(13),
p_whse_shipto char(10),
p_whse_billto char(10),
p_vend_code char(6),
p_requestor_code char(6),
p_request_no char(10),
p_request_date date,
p_autho_code char(6),
p_acct_no integer,
p_req_post_no integer,
p_po_doc_no integer,
p_po_line_no smallint,
p_recv_qty decimal(12),
p_ref_type char(2),
p_ref_doc_no integer,
p_ref_line_no integer,
p_ref_ship_no integer,
p_cost decimal(12),
p_net_amount decimal(12),
p_gl_acct_no integer

                 */

                object[] parameter = new object[31];
                parameter[0] = objDVOD.rowid;
                parameter[1] = objDVOD.doc_no;
                parameter[2] = objDVOD.line_no;
                parameter[3] = objDVOD.line_type;
                parameter[4] = objDVOD.line_stage;
                parameter[5] = objDVOD.item_code;
                parameter[6] = objDVOD.desc1;
                parameter[7] = objDVOD.desc2;
                parameter[8] = objDVOD.unit;
                parameter[9] = objDVOD.ordr_qty;
                parameter[10] = objDVOD.instruct_code;
                parameter[11] = objDVOD.reference_no;
                parameter[12] = objDVOD.whse_shipto;
                parameter[13] = objDVOD.whse_billto;
                parameter[14] = objDVOD.vend_code;
                parameter[15] = objDVOD.requestor_code;
                parameter[16] = objDVOD.request_no;
                parameter[17] = objDVOD.request_date;
                parameter[18] = objDVOD.authorization_code;
                parameter[19] = objDVOD.acct_no;
                parameter[20] = objDVOD.req_post_no;
                parameter[21] = objDVOD.po_doc_no;
                parameter[22] = objDVOD.po_line_no;
                parameter[23] = objDVOD.recv_qty;
                parameter[24] = objDVOD.ref_type;
                parameter[25] = objDVOD.ref_doc_no;
                parameter[26] = objDVOD.ref_line_no;
                parameter[27] = objDVOD.ref_ship_no;
                parameter[28] = objDVOD.cost;
                parameter[29] = objDVOD.net_amount;
                parameter[30] = objDVOD.gl_acct_no;

                object RetValue;

                RetValue = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.UPDATE_SPNAME);
                // Return when Transection Details updated Successfully
                if (RetValue != null)
                {
                    if (Convert.ToInt32(RetValue) == 1)
                    {
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    }
                    else
                    {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    }
                    return Convert.ToInt32(RetValue);

                }
                else
                {
                    return Convert.ToInt32(RetValue);
                }

            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManager.Publish(ex);
                return 0;
            }


        }

        public static int CheckforUniqueRequestNo(int ReqNo)
        {
            DVOUpdateRequisitionD objDVOD = new DVOUpdateRequisitionD();
            object obj = new object();
            object[] parameter = new object[1];
            parameter[0] = ReqNo;

            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();


                obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOD.GetUniqueReqNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Convert.ToInt32(obj);

        }
        public static int GetOrderTypeINsturqste(ref DVOUpdateRequisitionH objDVOUpdateRequisitionH)
        {
            int count = 0;
            object[] parameters = new object[1];
            parameters[0] = objDVOUpdateRequisitionH.po_type;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(objDVOUpdateRequisitionH.FIND_POTYPE(ref parameters));
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return count;
        }


        public static int UpdateApprovalRequisition(ref object TransactionObject, ref DVOUpdateRequisitionH objDVO, ref List<DVOUpdateRequisitionD> listApproveRequisitionD, ref List<DVOUpdateRequisitionD> listCancelRequisitionD)
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
                object[] parameter = new object[6];
                parameter[0] = objDVO.rowid;
                parameter[1] = objDVO.doc_no;
                parameter[2] = objDVO.gl_acct_no;
                parameter[3] = objDVO.lvl1aprvl;
                parameter[4] = objDVO.lvl1aprvl_by;
                parameter[5] = objDVO.lvl1aprvl_machinfo;

                object obj;
                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameter, objDVO.UPDATE_APPROVAL_REQUISTION);
                if (obj != null)
                {
                    if (obj.ToString() == "1")
                    {
                        int i1 = 0; int i2 = 0;
                        if (listApproveRequisitionD.Count > 0)
                        {
                            i1 = BLLRequitions.UpdateApproveRequisitionDetails(ref TransactionObject, ref listApproveRequisitionD);
                            if (i1 == 1)
                            { }
                            else
                                throw new Exception();
                        }
                        if (listCancelRequisitionD.Count > 0)
                        {
                            i2 = BLLRequitions.UpdateCancelRequisitionDetails(ref TransactionObject, ref listApproveRequisitionD);
                            if (i2 == 1)
                            { }
                            else
                                throw new Exception();
                        }
                        if (!statusObjTransaction && TransactionObject != null)
                            objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                        return 1;
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new Exception();

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }
        public static int UpdateApproveRequisitionDetails(ref object objTransaction, ref  List<DVOUpdateRequisitionD> objLDVOD)
        {
            int success = 0;
            object[] parameter = new object[6];

            foreach (DVOUpdateRequisitionD objDVOD in objLDVOD)
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                parameter[0] = objDVOD.rowid;
                parameter[1] = objDVOD.doc_no;
                parameter[2] = objDVOD.line_no;
                parameter[3] = objDVOD.lvl1aprv_qty;
                parameter[4] = objDVOD.lvl1aprv_cost;
                parameter[5] = objDVOD.lvl1aprv_vndr;
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                try
                {
                    DataSet dsRes = new DataSet();
                    object obj;

                    obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.UPDATE_REQ_DTL_APPROVAL);
                    if (obj != null)
                    {
                        if (obj.ToString() != "1")
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    throw ex;
                }
            }
            return 1;
        }
        public static int UpdateCancelRequisitionDetails(ref object objTransaction, ref  List<DVOUpdateRequisitionD> objLDVOD)
        {
            int success = 0;
            object[] parameter = new object[3];

            foreach (DVOUpdateRequisitionD objDVOD in objLDVOD)
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                parameter[0] = objDVOD.rowid;
                parameter[1] = objDVOD.doc_no;
                parameter[2] = objDVOD.line_no;
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                try
                {
                    DataSet dsRes = new DataSet();
                    object obj;

                    obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.UPDATE_REQ_DTL_CANCEL);
                    if (obj != null)
                    {
                        if (obj.ToString() != "1")
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    throw ex;
                }
            }
            return 1;
        }

        public static int CancelRequisitionApproval(ref DVOUpdateRequisitionH objDVO)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransection = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVO.doc_no;
                object obj = null;
                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVO.CANCEL_REQUISTION);
                if (obj == null)
                    throw new Exception();
                else if (Convert.ToInt32(obj) < 1)
                    throw new Exception();

                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }
        public static List<DVOUpdateRequisitionH> GetRequitionForApprovalByCPU(ref DVOUpdateRequisitionH objDVODVOUpdateRequisitionH)
        {
            object[] parameters = new object[1];
            parameters[0] = objDVODVOUpdateRequisitionH.requestor_min;

            List<DVOUpdateRequisitionH> objListDVOUpdateRequisitionH = new List<DVOUpdateRequisitionH>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(objDVODVOUpdateRequisitionH.GET_DATA_FOR_APPROVAL_BY_CPU(ref parameters)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateRequisitionH tempobjDVOUpdateRequisitionH = new DVOUpdateRequisitionH();
                    tempobjDVOUpdateRequisitionH.request_no = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                    tempobjDVOUpdateRequisitionH.doc_no = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                    if (dr[2] != DBNull.Value && dr[2].ToString().Trim() != string.Empty)
                        tempobjDVOUpdateRequisitionH.request_date = Convert.ToDateTime(dr[2]);
                    if (dr[3] != DBNull.Value && dr[3].ToString().Trim() != string.Empty)
                        tempobjDVOUpdateRequisitionH.requiredDate = Convert.ToDateTime(dr[3]);
                    tempobjDVOUpdateRequisitionH.requestor_code = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.authorization_code = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.request_status = dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.po_type = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.whse_shipto = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.rowid = Convert.ToInt32(dr[9]);
                    tempobjDVOUpdateRequisitionH.requestor_min = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.requestor_dept = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.gl_acct_no = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
                    tempobjDVOUpdateRequisitionH.lvl1aprvl = dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0;
                    tempobjDVOUpdateRequisitionH.lvl1aprvl_by = dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0;
                    tempobjDVOUpdateRequisitionH.lvl1aprvl_date = dr[15] != DBNull.Value ? Convert.ToDateTime(dr[15]) : Convert.ToDateTime(null);
                    tempobjDVOUpdateRequisitionH.lvl1aprvl_machinfo = dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.keyvalue = dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.acct_desc = dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.acct_type = dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty;
                    tempobjDVOUpdateRequisitionH.approvBy = dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty;

                    objListDVOUpdateRequisitionH.Add(tempobjDVOUpdateRequisitionH);
                }
            }
            return objListDVOUpdateRequisitionH;
        }

        public static List<DVOUpdateRequisitionH> GetRequitionEnquiry(ref DVOUpdateRequisitionH objDVODVOUpdateRequisitionH)
        {
            object[] parameters = new object[2];
            parameters[0] = objDVODVOUpdateRequisitionH.requestor_min;
            parameters[1] = objDVODVOUpdateRequisitionH.requestor_dept;

            List<DVOUpdateRequisitionH> objListDVOUpdateRequisitionH = new List<DVOUpdateRequisitionH>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            using (DataSet ds = objDalBaseClass.GetData(objDVODVOUpdateRequisitionH.GET_DATA_FOR_ENQUIRY(ref parameters)))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DVOUpdateRequisitionH tempobjDVOUpdateRequisitionH = new DVOUpdateRequisitionH();
                        tempobjDVOUpdateRequisitionH.request_no = dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0;
                        tempobjDVOUpdateRequisitionH.doc_no = dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0;
                        if (dr[2] != DBNull.Value && dr[2].ToString().Trim() != string.Empty)
                            tempobjDVOUpdateRequisitionH.request_date = Convert.ToDateTime(dr[2]);
                        if (dr[3] != DBNull.Value && dr[3].ToString().Trim() != string.Empty)
                            tempobjDVOUpdateRequisitionH.requiredDate = Convert.ToDateTime(dr[3]);
                        tempobjDVOUpdateRequisitionH.requestor_code = dr[4] != DBNull.Value ? dr[4].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.authorization_code = dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.request_status = dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.po_type = dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.whse_shipto = dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.rowid = Convert.ToInt32(dr[9]);
                        tempobjDVOUpdateRequisitionH.requestor_min = dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.requestor_dept = dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.gl_acct_no = dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0;
                        tempobjDVOUpdateRequisitionH.lvl1aprvl = dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0;
                        tempobjDVOUpdateRequisitionH.lvl1aprvl_by = dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0;
                        tempobjDVOUpdateRequisitionH.lvl1aprvl_date = dr[15] != DBNull.Value ? Convert.ToDateTime(dr[15]) : Convert.ToDateTime(null);
                        tempobjDVOUpdateRequisitionH.lvl1aprvl_machinfo = dr[16] != DBNull.Value ? dr[16].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.keyvalue = dr[17] != DBNull.Value ? dr[17].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.acct_desc = dr[18] != DBNull.Value ? dr[18].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.acct_type = dr[19] != DBNull.Value ? dr[19].ToString().Trim() : string.Empty;
                        tempobjDVOUpdateRequisitionH.approvBy = dr[20] != DBNull.Value ? dr[20].ToString().Trim() : string.Empty;

                        objListDVOUpdateRequisitionH.Add(tempobjDVOUpdateRequisitionH);
                    }
                }
            }
            return objListDVOUpdateRequisitionH;
        }

        public static int UpdateCPUApprovalRequisition(ref object TransactionObject, ref DVOUpdateRequisitionH objDVO, ref List<DVOUpdateRequisitionD> listApproveRequisitionD, ref List<DVOUpdateRequisitionD> listCancelRequisitionD, int DtlLineCount)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            int success = 0;
            int CountApp = 0;
            int CountCan = 0;
            if (TransactionObject == null)
            {
                TransactionObject = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameter = new object[6];
                parameter[0] = objDVO.rowid;
                parameter[1] = objDVO.doc_no;
                parameter[2] = objDVO.procaprvl;
                parameter[3] = objDVO.procaprvl_by;
                parameter[4] = objDVO.procaprvl_machinfo;
                CountApp = listApproveRequisitionD.Count;
                CountCan = listCancelRequisitionD.Count;
                if (CountApp > 0 && CountCan > 0)
                {
                    if (DtlLineCount == (CountApp + CountCan))
                        parameter[5] = 1;//status COM
                    else
                        parameter[5] = 3;//status no change
                }
                else if (DtlLineCount == CountApp)
                    parameter[5] = 1;//status COM
                else if (DtlLineCount == CountCan)
                    parameter[5] = 2;//status CAN
                else
                    parameter[5] = 3;//status no change


                object obj;
                obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameter, objDVO.UPDATE_CPU_APPROVAL_REQUISTION);
                if (obj != null)
                {
                    if (obj.ToString() == "1")
                    {
                        int i1 = 0; int i2 = 0;
                        if (listApproveRequisitionD.Count > 0)
                        {

                            i1 = BLLRequitions.UpdateCPUApproveRequisitionDetails(ref TransactionObject, ref listApproveRequisitionD);
                            if (i1 == 1)
                            { }
                            else
                                throw new Exception();
                        }
                        if (listCancelRequisitionD.Count > 0)
                        {

                            i2 = BLLRequitions.UpdateCPUCancelRequisitionDetails(ref TransactionObject, ref listApproveRequisitionD);
                            if (i2 == 1)
                            { }
                            else
                                throw new Exception();
                        }
                        #region Commented By Rahul -----------------------
                        ////if total detail line is processed then update PO status is COM
                        //if (DtlLineCount == (CountApp + CountCan))
                        //{
                        //    object obj1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameter, objDVO.UPDATE_CPU_APPROVAL_REQSTATUS);
                        //    if (obj1 != null)
                        //    {
                        //        if (obj1.ToString() == "1")
                        //        { }
                        //        else
                        //            throw new Exception();
                        //    }
                        //    else
                        //        throw new Exception();
                        //}
                        ////Update status COM
                        //if (DtlLineCount == CountApp)
                        //{
                        //    object obj1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameter, objDVO.UPDATE_CPU_APPROVAL_REQSTATUS);
                        //    if (obj1 != null)
                        //    {
                        //        if (obj1.ToString() == "1")
                        //        { }
                        //        else
                        //            throw new Exception();
                        //    }
                        //    else
                        //        throw new Exception();
                        //}
                        ////Update Status CAN
                        //if (DtlLineCount == CountCan)
                        //{
                        //    object obj1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref TransactionObject, ref parameter, objDVO.UPDATE_CPU_APPROVAL_REQSTATUS_CAN);
                        //    if (obj1 != null)
                        //    {
                        //        if (obj1.ToString() == "1")
                        //        { }
                        //        else
                        //            throw new Exception();
                        //    }
                        //    else
                        //        throw new Exception();
                        //}
                        #endregion Commented By Rahul
                        if (!statusObjTransaction && TransactionObject != null)
                            objDALBaseClassHelper.CommitTransaction(ref TransactionObject);
                        return 1;
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new Exception();

            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && TransactionObject != null)
                    objDALBaseClassHelper.RollbackTransaction(ref TransactionObject);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return success;
        }
        public static int UpdateCPUApproveRequisitionDetails(ref object objTransaction, ref  List<DVOUpdateRequisitionD> objLDVOD)
        {
            int success = 0;
            object[] parameter = new object[7];

            foreach (DVOUpdateRequisitionD objDVOD in objLDVOD)
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                parameter[0] = objDVOD.rowid;
                parameter[1] = objDVOD.doc_no;
                parameter[2] = objDVOD.line_no;
                parameter[3] = objDVOD.procaprv_qty;
                parameter[4] = objDVOD.procaprv_cost;
                parameter[5] = objDVOD.procaprv_vndr;
                parameter[6] = objDVOD.procnotes;
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                try
                {
                    DataSet dsRes = new DataSet();
                    object obj;

                    obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.UPDATE_REQ_DTL_CPU_APPROVAL);
                    if (obj != null)
                    {
                        if (obj.ToString() != "1")
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    throw ex;
                }
            }
            return 1;
        }
        public static int UpdateCPUCancelRequisitionDetails(ref object objTransaction, ref  List<DVOUpdateRequisitionD> objLDVOD)
        {
            int success = 0;
            object[] parameter = new object[3];

            foreach (DVOUpdateRequisitionD objDVOD in objLDVOD)
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                parameter[0] = objDVOD.rowid;
                parameter[1] = objDVOD.doc_no;
                parameter[2] = objDVOD.line_no;
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                try
                {
                    DataSet dsRes = new DataSet();
                    object obj;

                    obj = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter, objDVOD.UPDATE_REQ_DTL_CPU_CANCEL);
                    if (obj != null)
                    {
                        if (obj.ToString() != "1")
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    throw ex;
                }
            }
            return 1;
        }

        public static int GET_Account_Type_ID(string account_Type)
        {
            DVOGLAccountTypeMaintenance objDVOD = new DVOGLAccountTypeMaintenance();
            object obj = new object();
            object[] parameter = new object[1];
            parameter[0] = account_Type;

            try
            {
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();


                obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOD.Get_Account_ID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Convert.ToInt32(obj);

        }
    }
}
