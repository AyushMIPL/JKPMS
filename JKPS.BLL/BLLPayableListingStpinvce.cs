using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLPayableListingStpinvce
    {
        public static int InsertData(ref DVOPayableListingStpinvce objDVOPayableListingStpinvce, ref List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[30];
                parameters[0] = objDVOPayableListingStpinvce.inv_no;
                parameters[1] = objDVOPayableListingStpinvce.department;
                parameters[2] = objDVOPayableListingStpinvce.file_type;
                parameters[3] = objDVOPayableListingStpinvce.ref_no;
                parameters[4] = objDVOPayableListingStpinvce.inv_desc;
                parameters[5] = objDVOPayableListingStpinvce.doc_date;
                parameters[6] = objDVOPayableListingStpinvce.vend_code;
                parameters[7] = objDVOPayableListingStpinvce.pay_to_code;
                parameters[8] = objDVOPayableListingStpinvce.posted;
                parameters[9] = objDVOPayableListingStpinvce.recurring;
                parameters[10] = objDVOPayableListingStpinvce.terms_code;
                parameters[11] = objDVOPayableListingStpinvce.inv_date;
                parameters[12] = objDVOPayableListingStpinvce.to_pay_date;
                parameters[13] = objDVOPayableListingStpinvce.due_date;
                parameters[14] = objDVOPayableListingStpinvce.disc_date;
                parameters[15] = objDVOPayableListingStpinvce.disc_pct;
                parameters[16] = objDVOPayableListingStpinvce.po_date;
                parameters[17] = objDVOPayableListingStpinvce.po_no;
                parameters[18] = objDVOPayableListingStpinvce.cash_acct_no;
                parameters[19] = objDVOPayableListingStpinvce.fix_date_flag;
                parameters[20] = objDVOPayableListingStpinvce.batch_id;
                parameters[21] = objDVOPayableListingStpinvce.recurr_cnt;
                parameters[22] = objDVOPayableListingStpinvce.pay_method;
                parameters[23] = objDVOPayableListingStpinvce.ap_acct_no;
                parameters[24] = objDVOPayableListingStpinvce.ap_department;
                parameters[25] = objDVOPayableListingStpinvce.ap_amount;
                parameters[26] = objDVOPayableListingStpinvce.ap_debit_credit;
                parameters[27] = objDVOPayableListingStpinvce.cash_department;
                parameters[28] = objDVOPayableListingStpinvce.disc_amount;
                parameters[29] = objDVOPayableListingStpinvce.ok_to_post;

                using (DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPayableListingStpinvce)))
                {
                    int procStatus = 0;
                    int newDocNo = 0;
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            procStatus = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                            if (procStatus < 1)
                                throw new Exception();

                            newDocNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"v_NewDocNo"
                            foreach (DVOPayableListingDetailStpinvcd obj in listDVOPayableListingDetailStpinvcd)
                                obj.doc_no = newDocNo;

                            BLLPayableListingDetailStpinvcd.InsertData(ref objTransaction, ref listDVOPayableListingDetailStpinvcd);
                        }
                }
                parameters = null;
                objDALBaseClass = null;

                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        public static List<DVOPayableListingStpinvce> GetData(ref DVOPayableListingStpinvce objDVOPayableListingStpinvce)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOPayableListingStpinvce> listDVOPayableListingStpinvce = new List<DVOPayableListingStpinvce>();

            try
            {
                object[] parameters = new object[22];
                parameters[0] = objDVOPayableListingStpinvce.doc_no;
                parameters[1] = objDVOPayableListingStpinvce.inv_no;
                parameters[2] = objDVOPayableListingStpinvce.file_type;
                parameters[3] = objDVOPayableListingStpinvce.ref_no;
                parameters[4] = objDVOPayableListingStpinvce.inv_desc;
                if (objDVOPayableListingStpinvce.doc_date == string.Empty)
                    objDVOPayableListingStpinvce.doc_date = null;
                parameters[5] = objDVOPayableListingStpinvce.doc_date;
                parameters[6] = objDVOPayableListingStpinvce.vend_code;
                parameters[7] = objDVOPayableListingStpinvce.pay_to_code;
                parameters[8] = objDVOPayableListingStpinvce.posted;
                parameters[9] = objDVOPayableListingStpinvce.recurring;
                parameters[10] = objDVOPayableListingStpinvce.terms_code;
                if (objDVOPayableListingStpinvce.inv_date == string.Empty)
                    objDVOPayableListingStpinvce.inv_date = null;
                parameters[11] = objDVOPayableListingStpinvce.inv_date;
                if (objDVOPayableListingStpinvce.to_pay_date == string.Empty)
                    objDVOPayableListingStpinvce.to_pay_date = null;
                parameters[12] = objDVOPayableListingStpinvce.to_pay_date;
                if (objDVOPayableListingStpinvce.due_date == string.Empty)
                    objDVOPayableListingStpinvce.due_date = null;
                parameters[13] = objDVOPayableListingStpinvce.due_date;
                if (objDVOPayableListingStpinvce.disc_date == string.Empty)
                    objDVOPayableListingStpinvce.disc_date = null;
                parameters[14] = objDVOPayableListingStpinvce.disc_date;
                parameters[15] = objDVOPayableListingStpinvce.disc_pct;
                if (objDVOPayableListingStpinvce.po_date == string.Empty)
                    objDVOPayableListingStpinvce.po_date = null;
                parameters[16] = objDVOPayableListingStpinvce.po_date;
                parameters[17] = objDVOPayableListingStpinvce.po_no;
                parameters[18] = objDVOPayableListingStpinvce.fix_date_flag;
                parameters[19] = objDVOPayableListingStpinvce.recurr_cnt;
                parameters[20] = objDVOPayableListingStpinvce.pay_method;
                parameters[21] = objDVOPayableListingStpinvce.batch_id;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOPayableListingStpinvce tobjDVOPayableListingStpinvce = new DVOPayableListingStpinvce())
                                {
                                    tobjDVOPayableListingStpinvce.doc_no = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0]);//p_doc_no
                                    tobjDVOPayableListingStpinvce.inv_no = (dr[1] == DBNull.Value) ? string.Empty : dr[1].ToString().Trim();//p_inv_no
                                    tobjDVOPayableListingStpinvce.department = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//v_department
                                    tobjDVOPayableListingStpinvce.file_type = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_file_type
                                    tobjDVOPayableListingStpinvce.ref_no = (dr[4] == DBNull.Value) ? 0 : Convert.ToInt32(dr[4]);//p_ref_no
                                    tobjDVOPayableListingStpinvce.inv_desc = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//p_inv_desc
                                    tobjDVOPayableListingStpinvce.doc_date = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_doc_date
                                    tobjDVOPayableListingStpinvce.vend_code = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_vend_code
                                    tobjDVOPayableListingStpinvce.pay_to_code = (dr[8] == DBNull.Value) ? string.Empty : dr[8].ToString().Trim();//p_pay_to_code
                                    tobjDVOPayableListingStpinvce.posted = (dr[9] == DBNull.Value) ? string.Empty : dr[9].ToString().Trim();//p_posted
                                    tobjDVOPayableListingStpinvce.recurring = (dr[10] == DBNull.Value) ? string.Empty : dr[10].ToString().Trim();//p_recurring
                                    tobjDVOPayableListingStpinvce.terms_code = (dr[11] == DBNull.Value) ? string.Empty : dr[11].ToString().Trim();//p_terms_code
                                    tobjDVOPayableListingStpinvce.inv_date = (dr[12] == DBNull.Value) ? string.Empty : dr[12].ToString().Trim();//p_inv_date
                                    tobjDVOPayableListingStpinvce.to_pay_date = (dr[13] == DBNull.Value) ? string.Empty : dr[13].ToString().Trim();//p_to_pay_date
                                    tobjDVOPayableListingStpinvce.due_date = (dr[14] == DBNull.Value) ? string.Empty : dr[14].ToString().Trim();//p_due_date
                                    tobjDVOPayableListingStpinvce.disc_date = (dr[15] == DBNull.Value) ? string.Empty : dr[15].ToString().Trim();//p_disc_date
                                    tobjDVOPayableListingStpinvce.disc_pct = (dr[16] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[16]);//p_disc_pct
                                    tobjDVOPayableListingStpinvce.po_date = (dr[17] == DBNull.Value) ? string.Empty : dr[17].ToString().Trim();//p_po_date
                                    tobjDVOPayableListingStpinvce.po_no = (dr[18] == DBNull.Value) ? string.Empty : dr[18].ToString().Trim();//p_po_no
                                    tobjDVOPayableListingStpinvce.disc_acct_no = (dr[19] == DBNull.Value) ? 0 : Convert.ToInt32(dr[19]);//v_disc_acct_no
                                    tobjDVOPayableListingStpinvce.disc_department = (dr[20] == DBNull.Value) ? string.Empty : dr[20].ToString().Trim();//v_disc_department
                                    tobjDVOPayableListingStpinvce.disc_amount = (dr[21] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[21]);//v_disc_amount
                                    tobjDVOPayableListingStpinvce.disc_debit_credit = (dr[22] == DBNull.Value) ? string.Empty : dr[22].ToString().Trim();//v_disc_debit_credit
                                    tobjDVOPayableListingStpinvce.ap_acct_no = (dr[23] == DBNull.Value) ? 0 : Convert.ToInt32(dr[23]);//v_ap_acct_no
                                    tobjDVOPayableListingStpinvce.ap_department = (dr[24] == DBNull.Value) ? string.Empty : dr[24].ToString().Trim();//v_ap_department
                                    tobjDVOPayableListingStpinvce.ap_amount = (dr[25] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[25]);//v_ap_amount
                                    tobjDVOPayableListingStpinvce.ap_debit_credit = (dr[26] == DBNull.Value) ? string.Empty : dr[26].ToString().Trim();//v_ap_debit_credit
                                    tobjDVOPayableListingStpinvce.ok_to_post = (dr[27] == DBNull.Value) ? string.Empty : dr[27].ToString().Trim();//v_ok_to_post
                                    tobjDVOPayableListingStpinvce.cash_acct_no = (dr[28] == DBNull.Value) ? 0 : Convert.ToInt32(dr[28]);//
                                    tobjDVOPayableListingStpinvce.cash_department = (dr[29] == DBNull.Value) ? string.Empty : dr[29].ToString().Trim();//v_cash_department
                                    tobjDVOPayableListingStpinvce.recurr_ref = (dr[30] == DBNull.Value) ? string.Empty : dr[30].ToString().Trim();//v_recurr_ref
                                    tobjDVOPayableListingStpinvce.def_mtaxcd = (dr[31] == DBNull.Value) ? string.Empty : dr[31].ToString().Trim();//v_def_mtaxcd
                                    tobjDVOPayableListingStpinvce.gross_entry = (dr[32] == DBNull.Value) ? string.Empty : dr[32].ToString().Trim();//v_gross_entry
                                    tobjDVOPayableListingStpinvce.currency_code = (dr[33] == DBNull.Value) ? string.Empty : dr[33].ToString().Trim();//v_currency_code
                                    tobjDVOPayableListingStpinvce.curr_ex_rate = (dr[34] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[34]);//v_curr_ex_rate
                                    tobjDVOPayableListingStpinvce.home_curr_amount = (dr[35] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[35]);//v_home_curr_amount
                                    tobjDVOPayableListingStpinvce.fix_date_flag = (dr[36] == DBNull.Value) ? string.Empty : dr[36].ToString().Trim();//p_fix_date_flag
                                    tobjDVOPayableListingStpinvce.batch_id = (dr[37] == DBNull.Value) ? 0 : Convert.ToInt32(dr[37]);//p_batch_id
                                    tobjDVOPayableListingStpinvce.recurr_cnt = (dr[38] == DBNull.Value) ? 0 : Convert.ToInt32(dr[38]);//p_recurr_cnt
                                    tobjDVOPayableListingStpinvce.min_voucher_no = (dr[39] == DBNull.Value) ? string.Empty : dr[39].ToString().Trim();//v_min_voucher_no
                                    tobjDVOPayableListingStpinvce.tre_voucher_no = (dr[40] == DBNull.Value) ? string.Empty : dr[40].ToString().Trim();//v_tre_voucher_no
                                    tobjDVOPayableListingStpinvce.required_approval = (dr[41] == DBNull.Value) ? 0 : Convert.ToInt32(dr[41]);//v_required_approval
                                    tobjDVOPayableListingStpinvce.current_approval = (dr[42] == DBNull.Value) ? 0 : Convert.ToInt32(dr[42]);//v_current_approval
                                    tobjDVOPayableListingStpinvce.acd_id = (dr[43] == DBNull.Value) ? 0 : Convert.ToInt32(dr[43]);//v_acd_id
                                    tobjDVOPayableListingStpinvce.pay_method = (dr[44] == DBNull.Value) ? string.Empty : dr[44].ToString().Trim();//p_pay_method

                                    listDVOPayableListingStpinvce.Add(tobjDVOPayableListingStpinvce);
                                }
                            }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOPayableListingStpinvce;
        }

        public static List<DVOPayableListingStpinvce> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOPayableListingStpinvce> listDVOPayableListingStpinvce = new List<DVOPayableListingStpinvce>();

            try
            {
                using (DataSet ds = objDALBaseClass.GetAllData(typeof(DVOPayableListingStpinvce)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOPayableListingStpinvce tobjDVOPayableListingStpinvce = new DVOPayableListingStpinvce())
                                {
                                    tobjDVOPayableListingStpinvce.doc_no = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0]);//p_doc_no
                                    tobjDVOPayableListingStpinvce.inv_no = (dr[1] == DBNull.Value) ? string.Empty : dr[1].ToString().Trim();//p_inv_no
                                    tobjDVOPayableListingStpinvce.department = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//v_department
                                    tobjDVOPayableListingStpinvce.file_type = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_file_type
                                    tobjDVOPayableListingStpinvce.ref_no = (dr[4] == DBNull.Value) ? 0 : Convert.ToInt32(dr[4]);//p_ref_no
                                    tobjDVOPayableListingStpinvce.inv_desc = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//p_inv_desc
                                    tobjDVOPayableListingStpinvce.doc_date = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_doc_date
                                    tobjDVOPayableListingStpinvce.vend_code = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_vend_code
                                    tobjDVOPayableListingStpinvce.pay_to_code = (dr[8] == DBNull.Value) ? string.Empty : dr[8].ToString().Trim();//p_pay_to_code
                                    tobjDVOPayableListingStpinvce.posted = (dr[9] == DBNull.Value) ? string.Empty : dr[9].ToString().Trim();//p_posted
                                    tobjDVOPayableListingStpinvce.recurring = (dr[10] == DBNull.Value) ? string.Empty : dr[10].ToString().Trim();//p_recurring
                                    tobjDVOPayableListingStpinvce.terms_code = (dr[11] == DBNull.Value) ? string.Empty : dr[11].ToString().Trim();//p_terms_code
                                    tobjDVOPayableListingStpinvce.inv_date = (dr[12] == DBNull.Value) ? string.Empty : dr[12].ToString().Trim();//p_inv_date
                                    tobjDVOPayableListingStpinvce.to_pay_date = (dr[13] == DBNull.Value) ? string.Empty : dr[13].ToString().Trim();//p_to_pay_date
                                    tobjDVOPayableListingStpinvce.due_date = (dr[14] == DBNull.Value) ? string.Empty : dr[14].ToString().Trim();//p_due_date
                                    tobjDVOPayableListingStpinvce.disc_date = (dr[15] == DBNull.Value) ? string.Empty : dr[15].ToString().Trim();//p_disc_date
                                    tobjDVOPayableListingStpinvce.disc_pct = (dr[16] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[16]);//p_disc_pct
                                    tobjDVOPayableListingStpinvce.po_date = (dr[17] == DBNull.Value) ? string.Empty : dr[17].ToString().Trim();//p_po_date
                                    tobjDVOPayableListingStpinvce.po_no = (dr[18] == DBNull.Value) ? string.Empty : dr[18].ToString().Trim();//p_po_no
                                    tobjDVOPayableListingStpinvce.disc_acct_no = (dr[19] == DBNull.Value) ? 0 : Convert.ToInt32(dr[19]);//v_disc_acct_no
                                    tobjDVOPayableListingStpinvce.disc_department = (dr[20] == DBNull.Value) ? string.Empty : dr[20].ToString().Trim();//v_disc_department
                                    tobjDVOPayableListingStpinvce.disc_amount = (dr[21] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[21]);//v_disc_amount
                                    tobjDVOPayableListingStpinvce.disc_debit_credit = (dr[22] == DBNull.Value) ? string.Empty : dr[22].ToString().Trim();//v_disc_debit_credit
                                    tobjDVOPayableListingStpinvce.ap_acct_no = (dr[23] == DBNull.Value) ? 0 : Convert.ToInt32(dr[23]);//v_ap_acct_no
                                    tobjDVOPayableListingStpinvce.ap_department = (dr[24] == DBNull.Value) ? string.Empty : dr[24].ToString().Trim();//v_ap_department
                                    tobjDVOPayableListingStpinvce.ap_amount = (dr[25] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[25]);//v_ap_amount
                                    tobjDVOPayableListingStpinvce.ap_debit_credit = (dr[26] == DBNull.Value) ? string.Empty : dr[26].ToString().Trim();//v_ap_debit_credit
                                    tobjDVOPayableListingStpinvce.ok_to_post = (dr[27] == DBNull.Value) ? string.Empty : dr[27].ToString().Trim();//v_ok_to_post
                                    tobjDVOPayableListingStpinvce.cash_acct_no = (dr[28] == DBNull.Value) ? 0 : Convert.ToInt32(dr[28]);//
                                    tobjDVOPayableListingStpinvce.cash_department = (dr[29] == DBNull.Value) ? string.Empty : dr[29].ToString().Trim();//v_cash_department
                                    tobjDVOPayableListingStpinvce.recurr_ref = (dr[30] == DBNull.Value) ? string.Empty : dr[30].ToString().Trim();//v_recurr_ref
                                    tobjDVOPayableListingStpinvce.def_mtaxcd = (dr[31] == DBNull.Value) ? string.Empty : dr[31].ToString().Trim();//v_def_mtaxcd
                                    tobjDVOPayableListingStpinvce.gross_entry = (dr[32] == DBNull.Value) ? string.Empty : dr[32].ToString().Trim();//v_gross_entry
                                    tobjDVOPayableListingStpinvce.currency_code = (dr[33] == DBNull.Value) ? string.Empty : dr[33].ToString().Trim();//v_currency_code
                                    tobjDVOPayableListingStpinvce.curr_ex_rate = (dr[34] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[34]);//v_curr_ex_rate
                                    tobjDVOPayableListingStpinvce.home_curr_amount = (dr[35] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[35]);//v_home_curr_amount
                                    tobjDVOPayableListingStpinvce.fix_date_flag = (dr[36] == DBNull.Value) ? string.Empty : dr[36].ToString().Trim();//p_fix_date_flag
                                    tobjDVOPayableListingStpinvce.batch_id = (dr[37] == DBNull.Value) ? 0 : Convert.ToInt32(dr[37]);//p_batch_id
                                    tobjDVOPayableListingStpinvce.recurr_cnt = (dr[38] == DBNull.Value) ? 0 : Convert.ToInt32(dr[38]);//p_recurr_cnt
                                    tobjDVOPayableListingStpinvce.min_voucher_no = (dr[39] == DBNull.Value) ? string.Empty : dr[39].ToString().Trim();//v_min_voucher_no
                                    tobjDVOPayableListingStpinvce.tre_voucher_no = (dr[40] == DBNull.Value) ? string.Empty : dr[40].ToString().Trim();//v_tre_voucher_no
                                    tobjDVOPayableListingStpinvce.required_approval = (dr[41] == DBNull.Value) ? 0 : Convert.ToInt32(dr[41]);//v_required_approval
                                    tobjDVOPayableListingStpinvce.current_approval = (dr[42] == DBNull.Value) ? 0 : Convert.ToInt32(dr[42]);//v_current_approval
                                    tobjDVOPayableListingStpinvce.acd_id = (dr[43] == DBNull.Value) ? 0 : Convert.ToInt32(dr[43]);//v_acd_id
                                    tobjDVOPayableListingStpinvce.pay_method = (dr[44] == DBNull.Value) ? string.Empty : dr[44].ToString().Trim();//p_pay_method

                                    listDVOPayableListingStpinvce.Add(tobjDVOPayableListingStpinvce);
                                }
                            }
                }
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOPayableListingStpinvce;
        }

        public static int DeleteData(ref DVOPayableListingStpinvce objDVOPayableListingStpinvce)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOPayableListingStpinvce.doc_no;

                //DVOPayableListingDetailStpinvcd obj = new DVOPayableListingDetailStpinvcd();
                //obj.doc_no = objDVOPayableListingStpinvce.doc_no;
                //BLLPayableListingDetailStpinvcd.DeleteData(ref objTransaction, ref obj);
                //obj = null;
                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPayableListingStpinvce.DELETE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                parameters = null;
                objDALBaseClass = null;

                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return 0;
        }

        //public static int UpdateData(ref DVOPayableListingStpinvce objDVOPayableListingStpinvce, ref List<DVOPayableListingDetailStpinvcd> listDVOPayableListingDetailStpinvcd)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    try
        //    {
        //        object[] parameters = new object[29];
        //        parameters[0] = objDVOPayableListingStpinvce.doc_no;
        //        parameters[1] = objDVOPayableListingStpinvce.inv_no;
        //        parameters[2] = objDVOPayableListingStpinvce.department;
        //        parameters[3] = objDVOPayableListingStpinvce.file_type;
        //        parameters[4] = objDVOPayableListingStpinvce.ref_no;
        //        parameters[5] = objDVOPayableListingStpinvce.inv_desc;
        //        parameters[6] = objDVOPayableListingStpinvce.doc_date;
        //        parameters[7] = objDVOPayableListingStpinvce.vend_code;
        //        parameters[8] = objDVOPayableListingStpinvce.pay_to_code;
        //        parameters[9] = objDVOPayableListingStpinvce.posted;
        //        parameters[10] = objDVOPayableListingStpinvce.recurring;
        //        parameters[11] = objDVOPayableListingStpinvce.terms_code;
        //        parameters[12] = objDVOPayableListingStpinvce.inv_date;
        //        parameters[13] = objDVOPayableListingStpinvce.to_pay_date;
        //        parameters[14] = objDVOPayableListingStpinvce.due_date;
        //        parameters[15] = objDVOPayableListingStpinvce.disc_date;
        //        parameters[16] = objDVOPayableListingStpinvce.disc_pct;
        //        parameters[17] = objDVOPayableListingStpinvce.po_date;
        //        parameters[18] = objDVOPayableListingStpinvce.po_no;
        //        parameters[19] = objDVOPayableListingStpinvce.cash_acct_no;
        //        parameters[20] = objDVOPayableListingStpinvce.fix_date_flag;
        //        parameters[21] = objDVOPayableListingStpinvce.batch_id;
        //        parameters[22] = objDVOPayableListingStpinvce.recurr_cnt;
        //        parameters[23] = objDVOPayableListingStpinvce.pay_method;
        //        parameters[24] = objDVOPayableListingStpinvce.ap_acct_no;
        //        parameters[25] = objDVOPayableListingStpinvce.ap_department;
        //        parameters[26] = objDVOPayableListingStpinvce.ap_amount;
        //        parameters[27] = objDVOPayableListingStpinvce.ap_debit_credit;
        //        parameters[28] = objDVOPayableListingStpinvce.cash_department;

        //        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPayableListingStpinvce.UPDATE_SPNAME);
        //        if (o == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(o) < 1)
        //            throw new Exception();

        //        DVOPayableListingDetailStpinvcd tobj = new DVOPayableListingDetailStpinvcd();
        //        tobj.doc_no = objDVOPayableListingStpinvce.doc_no;
        //        BLLPayableListingDetailStpinvcd.DeleteData(ref objTransaction, ref tobj);
        //        BLLPayableListingDetailStpinvcd.InsertData(ref objTransaction, ref listDVOPayableListingDetailStpinvcd);

        //        tobj = null;
        //        parameters = null;
        //        objDALBaseClass = null;

        //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        throw ex;
        //    }
        //    return 0;
        //}

        public static int UpdateData(ref object objTransaction, ref DVOPayableListingStpinvce objDVOPayableListingStpinvce, 
            ref List<DVOPayableListingDetailStpinvcd> listNewDVOPayableListingDetailStpinvcd, 
            ref List<DVOPayableListingDetailStpinvcd> listUpdateDVOPayableListingDetailStpinvcd, 
            ref List<DVOPayableListingDetailStpinvcd> listDeleteDVOPayableListingDetailStpinvcd)
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
                object[] parameters = new object[29];
                parameters[0] = objDVOPayableListingStpinvce.doc_no;
                parameters[1] = objDVOPayableListingStpinvce.inv_no;
                parameters[2] = objDVOPayableListingStpinvce.department;
                parameters[3] = objDVOPayableListingStpinvce.file_type;
                parameters[4] = objDVOPayableListingStpinvce.ref_no;
                parameters[5] = objDVOPayableListingStpinvce.inv_desc;
                parameters[6] = objDVOPayableListingStpinvce.doc_date;
                parameters[7] = objDVOPayableListingStpinvce.vend_code;
                parameters[8] = objDVOPayableListingStpinvce.pay_to_code;
                parameters[9] = objDVOPayableListingStpinvce.posted;
                parameters[10] = objDVOPayableListingStpinvce.recurring;
                parameters[11] = objDVOPayableListingStpinvce.terms_code;
                parameters[12] = objDVOPayableListingStpinvce.inv_date;
                parameters[13] = objDVOPayableListingStpinvce.to_pay_date;
                parameters[14] = objDVOPayableListingStpinvce.due_date;
                parameters[15] = objDVOPayableListingStpinvce.disc_date;
                parameters[16] = objDVOPayableListingStpinvce.disc_pct;
                parameters[17] = objDVOPayableListingStpinvce.po_date;
                parameters[18] = objDVOPayableListingStpinvce.po_no;
                parameters[19] = objDVOPayableListingStpinvce.cash_acct_no;
                parameters[20] = objDVOPayableListingStpinvce.fix_date_flag;
                parameters[21] = objDVOPayableListingStpinvce.batch_id;
                parameters[22] = objDVOPayableListingStpinvce.recurr_cnt;
                parameters[23] = objDVOPayableListingStpinvce.pay_method;
                parameters[24] = objDVOPayableListingStpinvce.ap_acct_no;
                parameters[25] = objDVOPayableListingStpinvce.ap_department;
                parameters[26] = objDVOPayableListingStpinvce.ap_amount;
                parameters[27] = objDVOPayableListingStpinvce.ap_debit_credit;
                parameters[28] = objDVOPayableListingStpinvce.cash_department;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOPayableListingStpinvce.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                BLLPayableListingDetailStpinvcd.DeleteData(ref objTransaction, ref listDeleteDVOPayableListingDetailStpinvcd);
                BLLPayableListingDetailStpinvcd.UpdateData(ref objTransaction, ref listUpdateDVOPayableListingDetailStpinvcd);
                BLLPayableListingDetailStpinvcd.InsertData(ref objTransaction, ref listNewDVOPayableListingDetailStpinvcd);

                //DVOPayableListingDetailStpinvcd tobj = new DVOPayableListingDetailStpinvcd();
                //tobj.doc_no = objDVOPayableListingStpinvce.doc_no;
                //BLLPayableListingDetailStpinvcd.DeleteData(ref objTransaction, ref tobj);
                //BLLPayableListingDetailStpinvcd.InsertData(ref objTransaction, ref listDVOPayableListingDetailStpinvcd);

                //tobj = null;
                parameters = null;
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



        //******************************Updated by sanjay *******************************
        public static DataSet GET_AP_CHECKS(ref DVOPayableListingStpinvce objPayableListing)
        {
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            DVOPayableListingStpinvce objPayableList = new DVOPayableListingStpinvce();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objPayableListing.check;
                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), objPayableList.CHECK_POST);
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {                           
                        }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return ds;
        }


        /// <summary>
        /// This method is use to insert new account type into database
        /// </summary>
        /// <param name="objGLAccountType">reference of DVOGLAccountTypeMaintenance type object as a collection of parameters of search criteria.</param>
        /// <returns>return integer variable for confirmation, either data is inserted or not</returns>
        public static int InsertInvoiceList1(ref DVOPayableListingStpinvce objPayableList, ref List<DVOPayableListingStpinvce> listPayableListing)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //To keep track on transactions
            int process=0;
            try
            {
                process = 1;
                object[] parameters = new object[4];
                parameters[0] = //objPayableList.accounttype;
                parameters[1] = //objPayableList.desc;
                parameters[2] = //objPayableList.dfltacctcat;
                parameters[3] = 0;
                using (DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPayableListingStpinvce)))
                {
                    if (true)
                    {
                        process = 2;
                        objPayableList.check = 5;
                        object[] parameters1 = new object[4];
                        parameters1[0] = //objPayableList.accounttype;
                        parameters1[1] = //objPayableList.desc;
                        parameters1[2] = //objPayableList.dfltacctcat;
                        parameters1[3] = 0;
                        using (DataSet ds1 = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPayableListingStpinvce)))
                        {
                            if (true)
                            {
                                process = 3;
                                objPayableList.check = 6;
                                object[] parameters2 = new object[4];                                
                                parameters2[0] = //objPayableList.accounttype;
                                parameters2[1] = //objPayableList.desc;
                                parameters2[2] = //objPayableList.dfltacctcat;
                                parameters2[3] = 0;
                                using (DataSet ds2 = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance)))
                                {
                                    if (true)
                                    {
                                        process = 4;
                                        objPayableList.check = 7;
                                        int result7 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance));
                                        if(result7 > 0)
                                        {
                                            process = 5;
                                            objPayableList.check = 8;
                                            int result8 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance));
                                            if (result8 > 0)
                                            {
                                                process = 6;
                                                objPayableList.check = 9;
                                                int result9 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOGLAccountTypeMaintenance));
                                            }
                                        }
                                    }
                                }
                            
                            }
                        }

                    } 
                }
                parameters = null;
                objDALBaseClass = null;
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 1;
            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                return process;
            }
            return process;
        }       

    }
}
