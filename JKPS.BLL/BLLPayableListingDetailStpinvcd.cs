using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL 
{
    public class BLLPayableListingDetailStpinvcd
    {
        public static List<DVOPayableListingDetailStpinvcd> GetData(ref DVOPayableListingDetailStpinvcd objDVOPayableListingDetailStpinvcd)
        {
            List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd = new List<DVOPayableListingDetailStpinvcd>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[8];
                parameters[0] = objDVOPayableListingDetailStpinvcd.doc_no;
                parameters[1] = objDVOPayableListingDetailStpinvcd.line_no;
                parameters[2] = objDVOPayableListingDetailStpinvcd.acct_no;
                parameters[3] = objDVOPayableListingDetailStpinvcd.department;
                parameters[4] = objDVOPayableListingDetailStpinvcd.amount;
                parameters[5] = objDVOPayableListingDetailStpinvcd.debit_credit;
                parameters[6] = objDVOPayableListingDetailStpinvcd.mtax_code;
                parameters[7] = objDVOPayableListingDetailStpinvcd.goods_amt;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPayableListingDetailStpinvcd)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DVOPayableListingDetailStpinvcd tobjDVOPayableListingDetailStpinvcd = new DVOPayableListingDetailStpinvcd();
                                tobjDVOPayableListingDetailStpinvcd.doc_no = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//p_doc_no
                                tobjDVOPayableListingDetailStpinvcd.line_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);//p_line_no
                                tobjDVOPayableListingDetailStpinvcd.acct_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);//p_acct_no
                                tobjDVOPayableListingDetailStpinvcd.department = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);//p_department
                                tobjDVOPayableListingDetailStpinvcd.amount = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0);//p_amount
                                tobjDVOPayableListingDetailStpinvcd.debit_credit = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//p_debit_credit
                                tobjDVOPayableListingDetailStpinvcd.mtax_code = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//p_mtax_code
                                tobjDVOPayableListingDetailStpinvcd.goods_amt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);//"p_goods_amt"
                                tobjDVOPayableListingDetailStpinvcd.account_type = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//v_acct_type
                                tobjDVOPayableListingDetailStpinvcd.account_description = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);//v_acct_desc
                                tobjDVOPayableListingDetailStpinvcd.account_category = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);//v_acct_cat
                                tobjDVOPayableListingDetailStpinvcd.keyvalue = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//"v_keyvalue"
                                tobjDVOPayableListingDetailStpinvcd.account_typeId = (dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0);//v_acct_typeid
                                tobjDVOPayableListingDetailStpinvcd.RowId = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);//v_rowid

                                listDVOPayableListingDetailStpinvcd.Add(tobjDVOPayableListingDetailStpinvcd);
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                listDVOPayableListingDetailStpinvcd = new List<DVOPayableListingDetailStpinvcd>();
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOPayableListingDetailStpinvcd;
        }

        public static List<DVOPayableListingDetailStpinvcd> GetAllData()
        {
            List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd = new List<DVOPayableListingDetailStpinvcd>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL(); 
            try
            {
                using (DataSet ds = objDALBaseClass.GetAllData(typeof(DVOPayableListingDetailStpinvcd)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                DVOPayableListingDetailStpinvcd tobjDVOPayableListingDetailStpinvcd = new DVOPayableListingDetailStpinvcd();
                                tobjDVOPayableListingDetailStpinvcd.doc_no = (dr[0] != DBNull.Value ? Convert.ToInt32(dr[0]) : 0);//p_doc_no
                                tobjDVOPayableListingDetailStpinvcd.line_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);//p_line_no
                                tobjDVOPayableListingDetailStpinvcd.acct_no = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2]) : 0);//p_acct_no
                                tobjDVOPayableListingDetailStpinvcd.department = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);//p_department
                                tobjDVOPayableListingDetailStpinvcd.amount = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0);//p_amount
                                tobjDVOPayableListingDetailStpinvcd.debit_credit = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);//p_debit_credit
                                tobjDVOPayableListingDetailStpinvcd.mtax_code = (dr[6] != DBNull.Value ? dr[6].ToString().Trim() : string.Empty);//p_mtax_code
                                tobjDVOPayableListingDetailStpinvcd.goods_amt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);//"p_goods_amt"
                                tobjDVOPayableListingDetailStpinvcd.account_type = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);//v_acct_type
                                tobjDVOPayableListingDetailStpinvcd.account_description = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);//v_acct_desc
                                tobjDVOPayableListingDetailStpinvcd.account_category = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);//v_acct_cat
                                tobjDVOPayableListingDetailStpinvcd.keyvalue = (dr[11] != DBNull.Value ? dr[11].ToString().Trim() : string.Empty);//"v_keyvalue"
                                tobjDVOPayableListingDetailStpinvcd.account_typeId = (dr[12] != DBNull.Value ? Convert.ToInt32(dr[12]) : 0);//v_acct_typeid
                                tobjDVOPayableListingDetailStpinvcd.RowId = (dr[13] != DBNull.Value ? Convert.ToInt32(dr[13]) : 0);//v_rowid

                                listDVOPayableListingDetailStpinvcd.Add(tobjDVOPayableListingDetailStpinvcd);
                            }
                }
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                listDVOPayableListingDetailStpinvcd = new List<DVOPayableListingDetailStpinvcd>();
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOPayableListingDetailStpinvcd;
        }

        public static int InsertData(ref object objTransaction, ref List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                foreach (DVOPayableListingDetailStpinvcd obj in listDVOPayableListingDetailStpinvcd)
                {
                    object[] parameters = new object[8];
                    parameters[0] = obj.doc_no;
                    parameters[1] = obj.line_no;
                    parameters[2] = obj.acct_no;
                    parameters[3] = obj.department;
                    parameters[4] = obj.amount;
                    parameters[5] = obj.debit_credit;
                    parameters[6] = obj.mtax_code;
                    parameters[7] = obj.goods_amt;

                    object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.INSERT_SPNAME);
                    if (o == null)
                        throw new Exception();
                    else if (Convert.ToInt32(o) < 1)
                        throw new Exception();

                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        public static int UpdateData(ref object objTransaction, ref List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                foreach (DVOPayableListingDetailStpinvcd obj in listDVOPayableListingDetailStpinvcd)
                {
                    object[] parameters = new object[9];
                    parameters[0] = obj.RowId;
                    parameters[1] = obj.doc_no;
                    parameters[2] = obj.line_no;
                    parameters[3] = obj.acct_no;
                    parameters[4] = obj.department;
                    parameters[5] = obj.amount;
                    parameters[6] = obj.debit_credit;
                    parameters[7] = obj.mtax_code;
                    parameters[8] = obj.goods_amt;

                    object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.UPDATE_SPNAME);
                    if (o == null)
                        throw new Exception();
                    else if (Convert.ToInt32(o) < 1)
                        throw new Exception();

                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        public static int DeleteData(ref object objTransaction, ref List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            try
            {
                foreach (DVOPayableListingDetailStpinvcd obj in listDVOPayableListingDetailStpinvcd)
                {
                    object[] parameters = new object[2];
                    parameters[0] = obj.RowId;
                    parameters[1] = obj.doc_no;

                    object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, obj.DELETE_SPNAME);
                    if (o == null)
                        throw new Exception();
                    else if (Convert.ToInt32(o) < 1)
                        throw new Exception();

                    parameters = null;
                }
                objDALBaseClass = null;
                if (!statusObjTransaction)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        //public static int DeleteData(ref object objTransaction, ref DVOPayableListingDetailStpinvcd objDVOPayableListingDetailStpinvcd)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    bool statusObjTransaction = true;
        //    if (objTransaction == null)
        //    {
        //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //        statusObjTransaction = false;
        //    }
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

        //    try
        //    {
        //        if (objDVOPayableListingDetailStpinvcd != null)
        //        {
        //            object[] parameters = new object[1];
        //            parameters[0] = objDVOPayableListingDetailStpinvcd.doc_no;

        //            object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPayableListingDetailStpinvcd.DELETE_SPNAME);
        //            if (o == null)
        //                throw new Exception();
        //            else if (Convert.ToInt32(o) < 1)
        //                throw new Exception();

        //            parameters = null;
        //        }
        //        objDALBaseClass = null;
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!statusObjTransaction)
        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        throw ex;
        //    }
        //    return 0;
        //}
    }
}






