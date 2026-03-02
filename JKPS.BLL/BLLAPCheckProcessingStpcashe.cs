using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JKPS.COMMON;
using JKPS.DL;

namespace JKPS.BLL
{
    public class BLLAPCheckProcessingStpcashe
    {
        public static int InsertData(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd, out int NewDocNo, bool shouldUpdateRequireApproval)
        {
            NewDocNo = 0;
            decimal debitAmountInserted = 0, CreditAmountInserted = 0;
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
                object[] parameters = new object[28];
                //parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                if (objDVOAPCheckProcessingStpcashe.chk_date == string.Empty) objDVOAPCheckProcessingStpcashe.chk_date = "01/01/1900";
                parameters[1] = objDVOAPCheckProcessingStpcashe.chk_date;
                parameters[2] = objDVOAPCheckProcessingStpcashe.vend_code;
                parameters[3] = objDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[4] = objDVOAPCheckProcessingStpcashe.check_no;
                parameters[5] = objDVOAPCheckProcessingStpcashe.doc_desc;
                parameters[6] = objDVOAPCheckProcessingStpcashe.cash_amt;
                parameters[7] = objDVOAPCheckProcessingStpcashe.cash_acct;
                parameters[8] = objDVOAPCheckProcessingStpcashe.cash_department;
                parameters[9] = objDVOAPCheckProcessingStpcashe.cash_deb_cred;
                parameters[10] = objDVOAPCheckProcessingStpcashe.oa_amt;
                parameters[11] = objDVOAPCheckProcessingStpcashe.oa_acct;
                parameters[12] = objDVOAPCheckProcessingStpcashe.oa_department;
                parameters[13] = objDVOAPCheckProcessingStpcashe.oa_deb_cred;
                parameters[14] = objDVOAPCheckProcessingStpcashe.print_chk;
                parameters[15] = objDVOAPCheckProcessingStpcashe.ok_to_post;
                parameters[16] = objDVOAPCheckProcessingStpcashe.chk_printed;
                parameters[17] = objDVOAPCheckProcessingStpcashe.ap_type;
                parameters[18] = objDVOAPCheckProcessingStpcashe.batch_id;
                parameters[19] = objDVOAPCheckProcessingStpcashe.min_voucher_no;
                parameters[20] = objDVOAPCheckProcessingStpcashe.tre_voucher_no;
                parameters[21] = objDVOAPCheckProcessingStpcashe.bus_name;
                parameters[22] = objDVOAPCheckProcessingStpcashe.current_approval;
                parameters[23] = objDVOAPCheckProcessingStpcashe.acd_id;
                parameters[24] = objDVOAPCheckProcessingStpcashe.required_approval;
                parameters[25] = objDVOAPCheckProcessingStpcashe.insertby;
                parameters[26] = objDVOAPCheckProcessingStpcashe.insertdate;
                parameters[27] = objDVOAPCheckProcessingStpcashe.insertmachineinfo;

                //Object nullTransactionObject = null;
                //NewDocNo = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref nullTransactionObject);
                NewDocNo = BLLAccountingLiberary.Auto_Next_APDocNo();
                if (NewDocNo > 0)
                {
                    parameters[0] = NewDocNo;
                    object objres = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOAPCheckProcessingStpcashe.INSERT_SPNAME_NEW);
                    if (objres == null || Convert.ToInt32(objres) < 1)
                        throw new Exception("Error occured during inserting of new Accounts-Payable document.");
                    if (objDVOAPCheckProcessingStpcashe.cash_deb_cred == "DB")
                        debitAmountInserted = objDVOAPCheckProcessingStpcashe.cash_amt;
                    else if (objDVOAPCheckProcessingStpcashe.cash_deb_cred == "CR")
                        CreditAmountInserted = objDVOAPCheckProcessingStpcashe.cash_amt;

                    foreach (DVOAPCheckProcessingDetailStpcashd obj in listDVOAPCheckProcessingDetailStpcashd)
                        obj.doc_no = NewDocNo;
                    decimal detaildebitAmount = 0, detailcreditAmount = 0;
                    BLLAPCheckProcessingDetailStpcashd.InsertData(ref objTransaction, ref listDVOAPCheckProcessingDetailStpcashd, out detaildebitAmount, out detailcreditAmount);
                   // if (NewDocNo > 0 && shouldUpdateRequireApproval)
                      //  UpdateRequiredApproval(ref objTransaction, ApprovalTables.NonAPChecks, NewDocNo);

                    debitAmountInserted += detaildebitAmount;
                    CreditAmountInserted += detailcreditAmount;

                    if (objDVOAPCheckProcessingStpcashe.oa_deb_cred == "DB")
                        debitAmountInserted += objDVOAPCheckProcessingStpcashe.oa_amt;
                    else if (objDVOAPCheckProcessingStpcashe.oa_deb_cred == "CR")
                        CreditAmountInserted += objDVOAPCheckProcessingStpcashe.oa_amt;
                }
                else
                    throw new Exception("Account-Payable Control table is locked or empty.");


                ////int newDocNo = 0;
                //using (DataSet ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAPCheckProcessingStpcashe)))
                //{
                //    if (ds != null)
                //        if (ds.Tables.Count > 0)
                //            if (ds.Tables[0].Rows.Count > 0)
                //            {
                //                int procStatus = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                //                if (procStatus < 1)
                //                    throw new Exception();

                //                NewDocNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"v_NewDocNo"
                //                foreach (DVOAPCheckProcessingDetailStpcashd obj in listDVOAPCheckProcessingDetailStpcashd)
                //                    obj.doc_no = NewDocNo;

                //                BLLAPCheckProcessingDetailStpcashd.InsertData(ref objTransaction, ref listDVOAPCheckProcessingDetailStpcashd);
                //                if (NewDocNo > 0 && shouldUpdateRequireApproval)
                //                    UpdateRequiredApproval(ref objTransaction, ApprovalTables.NonAPChecks, NewDocNo);
                //            }
                //}
                parameters = null;
                objDALBaseClass = null;
                if (debitAmountInserted == CreditAmountInserted)
                {
                    if (!statusObjTransaction)
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                    throw new Exception("Inserted Check-Amount is not matching with inserted detail-lines amount.");
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


        public static List<DVOAPCheckProcessingStpcashe> GetData(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe = new List<DVOAPCheckProcessingStpcashe>();

            try
            {
                object[] parameters = new object[14];
                parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                if (objDVOAPCheckProcessingStpcashe.chk_date == string.Empty)
                    objDVOAPCheckProcessingStpcashe.chk_date = null;
                parameters[1] = objDVOAPCheckProcessingStpcashe.chk_date;
                parameters[2] = objDVOAPCheckProcessingStpcashe.print_chk;
                parameters[3] = objDVOAPCheckProcessingStpcashe.ok_to_post;
                parameters[4] = objDVOAPCheckProcessingStpcashe.vend_code;
                parameters[5] = objDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[6] = objDVOAPCheckProcessingStpcashe.check_no;
                parameters[7] = objDVOAPCheckProcessingStpcashe.min_voucher_no;
                parameters[8] = objDVOAPCheckProcessingStpcashe.tre_voucher_no;
                parameters[9] = objDVOAPCheckProcessingStpcashe.doc_desc;
                parameters[10] = objDVOAPCheckProcessingStpcashe.batch_id;
                parameters[11] = objDVOAPCheckProcessingStpcashe.ap_type;
                parameters[12] = objDVOAPCheckProcessingStpcashe.RowId;
                //Added by Rohit for Business Name on 10/03/2009
                parameters[13] = objDVOAPCheckProcessingStpcashe.bus_name.Trim();

                //parameters[13] = objDVOAPCheckProcessingStpcashe.current_approval;
                //parameters[14] = objDVOAPCheckProcessingStpcashe.required_approval;
                //parameters[15] = objDVOAPCheckProcessingStpcashe.account_type;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOAPCheckProcessingStpcashe tobjDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe())
                                {
                                    tobjDVOAPCheckProcessingStpcashe.chk_date = (dr[0] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr[0]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//p_chk_date
                                    tobjDVOAPCheckProcessingStpcashe.doc_no = (dr[1] == DBNull.Value) ? 0 : Convert.ToInt32(dr[1]);//p_doc_no
                                    tobjDVOAPCheckProcessingStpcashe.vend_code = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//p_vend_code
                                    tobjDVOAPCheckProcessingStpcashe.pay_to_code = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_pay_to_code
                                    tobjDVOAPCheckProcessingStpcashe.gross_entry = (dr[4] == DBNull.Value) ? string.Empty : dr[4].ToString().Trim();//v_gross_entry
                                    tobjDVOAPCheckProcessingStpcashe.def_mtaxcd = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//v_def_mtaxcd
                                    tobjDVOAPCheckProcessingStpcashe.check_no = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_check_no
                                    tobjDVOAPCheckProcessingStpcashe.doc_desc = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_doc_desc
                                    tobjDVOAPCheckProcessingStpcashe.cash_amt = (dr[8] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[8]);//v_cash_amt
                                    tobjDVOAPCheckProcessingStpcashe.cash_acct = (dr[9] == DBNull.Value) ? 0 : Convert.ToInt32(dr[9]);//v_cash_acct
                                    tobjDVOAPCheckProcessingStpcashe.cash_department = (dr[10] == DBNull.Value) ? string.Empty : dr[10].ToString().Trim();//v_cash_department
                                    tobjDVOAPCheckProcessingStpcashe.cash_deb_cred = (dr[11] == DBNull.Value) ? string.Empty : dr[11].ToString().Trim();//v_cash_deb_cred
                                    tobjDVOAPCheckProcessingStpcashe.oa_amt = (dr[12] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[12]);//v_oa_amt
                                    tobjDVOAPCheckProcessingStpcashe.oa_acct = (dr[13] == DBNull.Value) ? 0 : Convert.ToInt32(dr[13]);//v_oa_acct
                                    tobjDVOAPCheckProcessingStpcashe.oa_department = (dr[14] == DBNull.Value) ? string.Empty : dr[14].ToString().Trim();//v_oa_department
                                    tobjDVOAPCheckProcessingStpcashe.oa_deb_cred = (dr[15] == DBNull.Value) ? string.Empty : dr[15].ToString().Trim();//v_oa_deb_cred
                                    tobjDVOAPCheckProcessingStpcashe.print_chk = (dr[16] == DBNull.Value) ? string.Empty : dr[16].ToString().Trim();//p_print_chk
                                    tobjDVOAPCheckProcessingStpcashe.ok_to_post = (dr[17] == DBNull.Value) ? string.Empty : dr[17].ToString().Trim();//p_ok_to_post
                                    tobjDVOAPCheckProcessingStpcashe.chk_printed = (dr[18] == DBNull.Value) ? string.Empty : dr[18].ToString().Trim();//v_chk_printed
                                    tobjDVOAPCheckProcessingStpcashe.ap_type = (dr[19] == DBNull.Value) ? string.Empty : dr[19].ToString().Trim();//v_ap_type
                                    tobjDVOAPCheckProcessingStpcashe.batch_id = (dr[20] == DBNull.Value) ? 0 : Convert.ToInt32(dr[20]);//p_batch_id
                                    tobjDVOAPCheckProcessingStpcashe.min_voucher_no = (dr[21] == DBNull.Value) ? string.Empty : dr[21].ToString().Trim();//v_min_voucher_no
                                    tobjDVOAPCheckProcessingStpcashe.tre_voucher_no = (dr[22] == DBNull.Value) ? string.Empty : dr[22].ToString().Trim();//v_tre_voucher_no
                                    tobjDVOAPCheckProcessingStpcashe.bus_name = (dr[23] == DBNull.Value) ? string.Empty : dr[23].ToString().Trim();//v_bus_name
                                    tobjDVOAPCheckProcessingStpcashe.required_approval = (dr[24] == DBNull.Value) ? 0 : Convert.ToInt32(dr[24]);//v_required_approval
                                    tobjDVOAPCheckProcessingStpcashe.current_approval = (dr[25] == DBNull.Value) ? 0 : Convert.ToInt32(dr[25]);//v_current_approval
                                    tobjDVOAPCheckProcessingStpcashe.acd_id = (dr[26] == DBNull.Value) ? 0 : Convert.ToInt32(dr[26]);//v_acd_id
                                    tobjDVOAPCheckProcessingStpcashe.RowId = (dr[27] == DBNull.Value) ? 0 : Convert.ToInt32(dr[27]);//v_Rowid
                                    tobjDVOAPCheckProcessingStpcashe.keyvalue = (dr[28] == DBNull.Value) ? string.Empty : dr[28].ToString().Trim();//v_keyvalue

                                    //tobjDVOAPCheckProcessingStpcashe.entry_date = (dr[29] == DBNull.Value) ? string.Empty : dr[29].ToString().Trim();
                                    //tobjDVOAPCheckProcessingStpcashe.collected_by = (dr[30] == DBNull.Value) ? string.Empty : dr[30].ToString().Trim();
                                    //tobjDVOAPCheckProcessingStpcashe.collected_date = (dr[31] == DBNull.Value) ? string.Empty : dr[31].ToString().Trim();
                                    //tobjDVOAPCheckProcessingStpcashe.printed_by = (dr[32] == DBNull.Value) ? 0 : Convert.ToInt32(dr[32]);
                                    //tobjDVOAPCheckProcessingStpcashe.printed_date = (dr[33] == DBNull.Value) ? string.Empty : dr[33].ToString().Trim();
                                    
                                    listDVOAPCheckProcessingStpcashe.Add(tobjDVOAPCheckProcessingStpcashe);
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
            return listDVOAPCheckProcessingStpcashe;
        }

        public static List<DVOAPCheckProcessingStpcashe> GetDataByDataReader(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe = new List<DVOAPCheckProcessingStpcashe>();

            try
            {
                object[] parameters = new object[14];
                parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                if (objDVOAPCheckProcessingStpcashe.chk_date == string.Empty)
                    objDVOAPCheckProcessingStpcashe.chk_date = null;
                parameters[1] = objDVOAPCheckProcessingStpcashe.chk_date;
                parameters[2] = objDVOAPCheckProcessingStpcashe.print_chk;
                parameters[3] = objDVOAPCheckProcessingStpcashe.ok_to_post;
                parameters[4] = objDVOAPCheckProcessingStpcashe.vend_code;
                parameters[5] = objDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[6] = objDVOAPCheckProcessingStpcashe.check_no;
                parameters[7] = objDVOAPCheckProcessingStpcashe.min_voucher_no;
                parameters[8] = objDVOAPCheckProcessingStpcashe.tre_voucher_no;
                parameters[9] = objDVOAPCheckProcessingStpcashe.doc_desc;
                parameters[10] = objDVOAPCheckProcessingStpcashe.batch_id;
                parameters[11] = objDVOAPCheckProcessingStpcashe.ap_type;
                parameters[12] = objDVOAPCheckProcessingStpcashe.RowId;
                //Added by Rohit for Business Name on 10/03/2009
                parameters[13] = objDVOAPCheckProcessingStpcashe.bus_name.Trim();

                //parameters[13] = objDVOAPCheckProcessingStpcashe.current_approval;
                //parameters[14] = objDVOAPCheckProcessingStpcashe.required_approval;
                //parameters[15] = objDVOAPCheckProcessingStpcashe.account_type;

                IDataReader dr = objDALBaseClass.GetDataByReader(ref parameters, typeof(DVOAPCheckProcessingStpcashe));

                while (dr.Read())
                {
                    using (DVOAPCheckProcessingStpcashe tobjDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe())
                    {
                        tobjDVOAPCheckProcessingStpcashe.chk_date = (dr[0] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr[0]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//p_chk_date
                        tobjDVOAPCheckProcessingStpcashe.doc_no = (dr[1] == DBNull.Value) ? 0 : Convert.ToInt32(dr[1]);//p_doc_no
                        tobjDVOAPCheckProcessingStpcashe.vend_code = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//p_vend_code
                        tobjDVOAPCheckProcessingStpcashe.pay_to_code = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_pay_to_code
                        tobjDVOAPCheckProcessingStpcashe.gross_entry = (dr[4] == DBNull.Value) ? string.Empty : dr[4].ToString().Trim();//v_gross_entry
                        tobjDVOAPCheckProcessingStpcashe.def_mtaxcd = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//v_def_mtaxcd
                        tobjDVOAPCheckProcessingStpcashe.check_no = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_check_no
                        tobjDVOAPCheckProcessingStpcashe.doc_desc = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_doc_desc
                        tobjDVOAPCheckProcessingStpcashe.cash_amt = (dr[8] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[8]);//v_cash_amt
                        tobjDVOAPCheckProcessingStpcashe.cash_acct = (dr[9] == DBNull.Value) ? 0 : Convert.ToInt32(dr[9]);//v_cash_acct
                        tobjDVOAPCheckProcessingStpcashe.cash_department = (dr[10] == DBNull.Value) ? string.Empty : dr[10].ToString().Trim();//v_cash_department
                        tobjDVOAPCheckProcessingStpcashe.cash_deb_cred = (dr[11] == DBNull.Value) ? string.Empty : dr[11].ToString().Trim();//v_cash_deb_cred
                        tobjDVOAPCheckProcessingStpcashe.oa_amt = (dr[12] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[12]);//v_oa_amt
                        tobjDVOAPCheckProcessingStpcashe.oa_acct = (dr[13] == DBNull.Value) ? 0 : Convert.ToInt32(dr[13]);//v_oa_acct
                        tobjDVOAPCheckProcessingStpcashe.oa_department = (dr[14] == DBNull.Value) ? string.Empty : dr[14].ToString().Trim();//v_oa_department
                        tobjDVOAPCheckProcessingStpcashe.oa_deb_cred = (dr[15] == DBNull.Value) ? string.Empty : dr[15].ToString().Trim();//v_oa_deb_cred
                        tobjDVOAPCheckProcessingStpcashe.print_chk = (dr[16] == DBNull.Value) ? string.Empty : dr[16].ToString().Trim();//p_print_chk
                        tobjDVOAPCheckProcessingStpcashe.ok_to_post = (dr[17] == DBNull.Value) ? string.Empty : dr[17].ToString().Trim();//p_ok_to_post
                        tobjDVOAPCheckProcessingStpcashe.chk_printed = (dr[18] == DBNull.Value) ? string.Empty : dr[18].ToString().Trim();//v_chk_printed
                        tobjDVOAPCheckProcessingStpcashe.ap_type = (dr[19] == DBNull.Value) ? string.Empty : dr[19].ToString().Trim();//v_ap_type
                        tobjDVOAPCheckProcessingStpcashe.batch_id = (dr[20] == DBNull.Value) ? 0 : Convert.ToInt32(dr[20]);//p_batch_id
                        tobjDVOAPCheckProcessingStpcashe.min_voucher_no = (dr[21] == DBNull.Value) ? string.Empty : dr[21].ToString().Trim();//v_min_voucher_no
                        tobjDVOAPCheckProcessingStpcashe.tre_voucher_no = (dr[22] == DBNull.Value) ? string.Empty : dr[22].ToString().Trim();//v_tre_voucher_no
                        tobjDVOAPCheckProcessingStpcashe.bus_name = (dr[23] == DBNull.Value) ? string.Empty : dr[23].ToString().Trim();//v_bus_name
                        tobjDVOAPCheckProcessingStpcashe.required_approval = (dr[24] == DBNull.Value) ? 0 : Convert.ToInt32(dr[24]);//v_required_approval
                        tobjDVOAPCheckProcessingStpcashe.current_approval = (dr[25] == DBNull.Value) ? 0 : Convert.ToInt32(dr[25]);//v_current_approval
                        tobjDVOAPCheckProcessingStpcashe.acd_id = (dr[26] == DBNull.Value) ? 0 : Convert.ToInt32(dr[26]);//v_acd_id
                        tobjDVOAPCheckProcessingStpcashe.RowId = (dr[27] == DBNull.Value) ? 0 : Convert.ToInt32(dr[27]);//v_Rowid
                        tobjDVOAPCheckProcessingStpcashe.keyvalue = (dr[28] == DBNull.Value) ? string.Empty : dr[28].ToString().Trim();//v_keyvalue

                        //tobjDVOAPCheckProcessingStpcashe.entry_date = (dr[29] == DBNull.Value) ? string.Empty : dr[29].ToString().Trim();
                        //tobjDVOAPCheckProcessingStpcashe.collected_by = (dr[30] == DBNull.Value) ? string.Empty : dr[30].ToString().Trim();
                        //tobjDVOAPCheckProcessingStpcashe.collected_date = (dr[31] == DBNull.Value) ? string.Empty : dr[31].ToString().Trim();
                        //tobjDVOAPCheckProcessingStpcashe.printed_by = (dr[32] == DBNull.Value) ? 0 : Convert.ToInt32(dr[32]);
                        //tobjDVOAPCheckProcessingStpcashe.printed_date = (dr[33] == DBNull.Value) ? string.Empty : dr[33].ToString().Trim();

                        listDVOAPCheckProcessingStpcashe.Add(tobjDVOAPCheckProcessingStpcashe);
                    }
                }
                dr.Close();
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOAPCheckProcessingStpcashe;
        }

        public static List<DVOAPCheckProcessingStpcashe> GetAllData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe = new List<DVOAPCheckProcessingStpcashe>();

            try
            {
                using (DataSet ds = objDALBaseClass.GetAllData(typeof(DVOAPCheckProcessingStpcashe)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOAPCheckProcessingStpcashe tobjDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe())
                                {
                                    tobjDVOAPCheckProcessingStpcashe.chk_date = (dr[0] == DBNull.Value) ? string.Empty : dr[0].ToString().Trim();//p_chk_date
                                    tobjDVOAPCheckProcessingStpcashe.doc_no = (dr[1] == DBNull.Value) ? 0 : Convert.ToInt32(dr[1]);//p_doc_no
                                    tobjDVOAPCheckProcessingStpcashe.vend_code = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//p_vend_code
                                    tobjDVOAPCheckProcessingStpcashe.pay_to_code = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_pay_to_code
                                    tobjDVOAPCheckProcessingStpcashe.gross_entry = (dr[4] == DBNull.Value) ? string.Empty : dr[4].ToString().Trim();//v_gross_entry
                                    tobjDVOAPCheckProcessingStpcashe.def_mtaxcd = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//v_def_mtaxcd
                                    tobjDVOAPCheckProcessingStpcashe.check_no = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_check_no
                                    tobjDVOAPCheckProcessingStpcashe.doc_desc = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_doc_desc
                                    tobjDVOAPCheckProcessingStpcashe.cash_amt = (dr[8] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[8]);//v_cash_amt
                                    tobjDVOAPCheckProcessingStpcashe.cash_acct = (dr[9] == DBNull.Value) ? 0 : Convert.ToInt32(dr[9]);//v_cash_acct
                                    tobjDVOAPCheckProcessingStpcashe.cash_department = (dr[10] == DBNull.Value) ? string.Empty : dr[10].ToString().Trim();//v_cash_department
                                    tobjDVOAPCheckProcessingStpcashe.cash_deb_cred = (dr[11] == DBNull.Value) ? string.Empty : dr[11].ToString().Trim();//v_cash_deb_cred
                                    tobjDVOAPCheckProcessingStpcashe.oa_amt = (dr[12] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[12]);//v_oa_amt
                                    tobjDVOAPCheckProcessingStpcashe.oa_acct = (dr[13] == DBNull.Value) ? 0 : Convert.ToInt32(dr[13]);//v_oa_acct
                                    tobjDVOAPCheckProcessingStpcashe.oa_department = (dr[14] == DBNull.Value) ? string.Empty : dr[14].ToString().Trim();//v_oa_department
                                    tobjDVOAPCheckProcessingStpcashe.oa_deb_cred = (dr[15] == DBNull.Value) ? string.Empty : dr[15].ToString().Trim();//v_oa_deb_cred
                                    tobjDVOAPCheckProcessingStpcashe.print_chk = (dr[16] == DBNull.Value) ? string.Empty : dr[16].ToString().Trim();//p_print_chk
                                    tobjDVOAPCheckProcessingStpcashe.ok_to_post = (dr[17] == DBNull.Value) ? string.Empty : dr[17].ToString().Trim();//p_ok_to_post
                                    tobjDVOAPCheckProcessingStpcashe.chk_printed = (dr[18] == DBNull.Value) ? string.Empty : dr[18].ToString().Trim();//v_chk_printed
                                    tobjDVOAPCheckProcessingStpcashe.ap_type = (dr[19] == DBNull.Value) ? string.Empty : dr[19].ToString().Trim();//v_ap_type
                                    tobjDVOAPCheckProcessingStpcashe.batch_id = (dr[20] == DBNull.Value) ? 0 : Convert.ToInt32(dr[20]);//p_batch_id
                                    tobjDVOAPCheckProcessingStpcashe.min_voucher_no = (dr[21] == DBNull.Value) ? string.Empty : dr[21].ToString().Trim();//v_min_voucher_no
                                    tobjDVOAPCheckProcessingStpcashe.tre_voucher_no = (dr[22] == DBNull.Value) ? string.Empty : dr[22].ToString().Trim();//v_tre_voucher_no
                                    tobjDVOAPCheckProcessingStpcashe.bus_name = (dr[23] == DBNull.Value) ? string.Empty : dr[23].ToString().Trim();//v_bus_name
                                    tobjDVOAPCheckProcessingStpcashe.required_approval = (dr[24] == DBNull.Value) ? 0 : Convert.ToInt32(dr[24]);//v_required_approval
                                    tobjDVOAPCheckProcessingStpcashe.current_approval = (dr[25] == DBNull.Value) ? 0 : Convert.ToInt32(dr[25]);//v_current_approval
                                    tobjDVOAPCheckProcessingStpcashe.acd_id = (dr[26] == DBNull.Value) ? 0 : Convert.ToInt32(dr[26]);//v_acd_id
                                    tobjDVOAPCheckProcessingStpcashe.RowId = (dr[27] == DBNull.Value) ? 0 : Convert.ToInt32(dr[27]);//v_Rowid
                                    tobjDVOAPCheckProcessingStpcashe.keyvalue = (dr[28] == DBNull.Value) ? string.Empty : dr[28].ToString().Trim();//v_keyvalue
                                    
                                    listDVOAPCheckProcessingStpcashe.Add(tobjDVOAPCheckProcessingStpcashe);
                                }
                            }
                }
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOAPCheckProcessingStpcashe;
        }

        public static int DeleteData(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                object[] parameters = new object[4];
                parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                parameters[1] = objDVOAPCheckProcessingStpcashe.updateby;
                parameters[2] = objDVOAPCheckProcessingStpcashe.updatedate;
                parameters[3] = objDVOAPCheckProcessingStpcashe.updatemachineinfo;

                //DVOAPCheckProcessingDetailStpcashd obj = new DVOAPCheckProcessingDetailStpcashd();
                //obj.doc_no = objDVOAPCheckProcessingStpcashe.doc_no;
                //BLLAPCheckProcessingDetailStpcashd.DeleteData(ref objTransaction, ref obj);
                //obj = null;
                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOAPCheckProcessingStpcashe.DELETE_SPNAME);
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

        public static int UpdateData(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd)
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
                object[] parameters = new object[27];
                parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                if (objDVOAPCheckProcessingStpcashe.chk_date == string.Empty) objDVOAPCheckProcessingStpcashe.chk_date = "01/01/1900";
                parameters[1] = objDVOAPCheckProcessingStpcashe.chk_date;
                parameters[2] = objDVOAPCheckProcessingStpcashe.vend_code;
                parameters[3] = objDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[4] = objDVOAPCheckProcessingStpcashe.check_no;
                parameters[5] = objDVOAPCheckProcessingStpcashe.doc_desc;
                parameters[6] = objDVOAPCheckProcessingStpcashe.cash_amt;
                parameters[7] = objDVOAPCheckProcessingStpcashe.cash_acct;
                parameters[8] = objDVOAPCheckProcessingStpcashe.cash_department;
                parameters[9] = objDVOAPCheckProcessingStpcashe.cash_deb_cred;
                parameters[10] = objDVOAPCheckProcessingStpcashe.oa_amt;
                parameters[11] = objDVOAPCheckProcessingStpcashe.oa_acct;
                parameters[12] = objDVOAPCheckProcessingStpcashe.oa_department;
                parameters[13] = objDVOAPCheckProcessingStpcashe.oa_deb_cred;
                parameters[14] = objDVOAPCheckProcessingStpcashe.print_chk;
                parameters[15] = objDVOAPCheckProcessingStpcashe.ok_to_post;
                parameters[16] = objDVOAPCheckProcessingStpcashe.chk_printed;
                parameters[17] = objDVOAPCheckProcessingStpcashe.ap_type;
                parameters[18] = objDVOAPCheckProcessingStpcashe.batch_id;
                parameters[19] = objDVOAPCheckProcessingStpcashe.min_voucher_no;
                parameters[20] = objDVOAPCheckProcessingStpcashe.tre_voucher_no;
                parameters[21] = objDVOAPCheckProcessingStpcashe.bus_name;
                parameters[22] = objDVOAPCheckProcessingStpcashe.current_approval;
                parameters[23] = objDVOAPCheckProcessingStpcashe.acd_id;
                parameters[24] = objDVOAPCheckProcessingStpcashe.updateby;
                parameters[25] = objDVOAPCheckProcessingStpcashe.updatedate;
                parameters[26] = objDVOAPCheckProcessingStpcashe.updatemachineinfo;
                //parameters[24] = objDVOAPCheckProcessingStpcashe.required_approval;

                object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOAPCheckProcessingStpcashe.UPDATE_SPNAME);
                if (o == null)
                    throw new Exception();
                else if (Convert.ToInt32(o) < 1)
                    throw new Exception();

                DVOAPCheckProcessingDetailStpcashd tobj = new DVOAPCheckProcessingDetailStpcashd();
                tobj.doc_no = objDVOAPCheckProcessingStpcashe.doc_no;
                List<DVOAPCheckProcessingDetailStpcashd> lstDelobj = new List<DVOAPCheckProcessingDetailStpcashd>();
                lstDelobj.Add(tobj);
                BLLAPCheckProcessingDetailStpcashd.DeleteData(ref objTransaction, ref lstDelobj);
                decimal DebitAmountInserted = 0, CreditAmountInserted = 0;
                BLLAPCheckProcessingDetailStpcashd.InsertData(ref objTransaction, ref listDVOAPCheckProcessingDetailStpcashd, out DebitAmountInserted, out CreditAmountInserted);
               // UpdateRequiredApproval(ref objTransaction, ApprovalTables.NonAPChecks, objDVOAPCheckProcessingStpcashe.doc_no);
                tobj = null;
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

        public static int UpdateData(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, ref List<DVOAPCheckProcessingDetailStpcashd> listNewDVOAPCheckProcessingDetailStpcashd,
            ref List<DVOAPCheckProcessingDetailStpcashd> listUpdateDVOAPCheckProcessingDetailStpcashd,
            ref List<DVOAPCheckProcessingDetailStpcashd> listDeleteDVOAPCheckProcessingDetailStpcashd,
            bool DeleteAllDetailLines, out int DocumentNo)
        {
            DocumentNo = objDVOAPCheckProcessingStpcashe.doc_no;
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
                object[] parameters = new object[27];
                parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                if (objDVOAPCheckProcessingStpcashe.chk_date == string.Empty) objDVOAPCheckProcessingStpcashe.chk_date = "01/01/1900";
                parameters[1] = objDVOAPCheckProcessingStpcashe.chk_date;
                parameters[2] = objDVOAPCheckProcessingStpcashe.vend_code;
                parameters[3] = objDVOAPCheckProcessingStpcashe.pay_to_code;
                parameters[4] = objDVOAPCheckProcessingStpcashe.check_no;
                parameters[5] = objDVOAPCheckProcessingStpcashe.doc_desc;
                parameters[6] = objDVOAPCheckProcessingStpcashe.cash_amt;
                parameters[7] = objDVOAPCheckProcessingStpcashe.cash_acct;
                parameters[8] = objDVOAPCheckProcessingStpcashe.cash_department;
                parameters[9] = objDVOAPCheckProcessingStpcashe.cash_deb_cred;
                parameters[10] = objDVOAPCheckProcessingStpcashe.oa_amt;
                parameters[11] = objDVOAPCheckProcessingStpcashe.oa_acct;
                parameters[12] = objDVOAPCheckProcessingStpcashe.oa_department;
                parameters[13] = objDVOAPCheckProcessingStpcashe.oa_deb_cred;
                parameters[14] = objDVOAPCheckProcessingStpcashe.print_chk;
                parameters[15] = objDVOAPCheckProcessingStpcashe.ok_to_post;
                parameters[16] = objDVOAPCheckProcessingStpcashe.chk_printed;
                parameters[17] = objDVOAPCheckProcessingStpcashe.ap_type;
                parameters[18] = objDVOAPCheckProcessingStpcashe.batch_id;
                parameters[19] = objDVOAPCheckProcessingStpcashe.min_voucher_no;
                parameters[20] = objDVOAPCheckProcessingStpcashe.tre_voucher_no;
                parameters[21] = objDVOAPCheckProcessingStpcashe.bus_name;
                parameters[22] = objDVOAPCheckProcessingStpcashe.current_approval;
                parameters[23] = objDVOAPCheckProcessingStpcashe.acd_id;
                parameters[24] = objDVOAPCheckProcessingStpcashe.updateby;
                parameters[25] = objDVOAPCheckProcessingStpcashe.updatedate;
                parameters[26] = objDVOAPCheckProcessingStpcashe.updatemachineinfo;
                //parameters[24] = objDVOAPCheckProcessingStpcashe.required_approval;

                using (DataSet ds = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, objDVOAPCheckProcessingStpcashe.UPDATE_SPNAME))
                {
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int procStatus = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                                if (procStatus < 1)
                                    throw new Exception();

                                DocumentNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//"v_NewDocNo"
                                if (listNewDVOAPCheckProcessingDetailStpcashd.Count > 0)
                                    foreach (DVOAPCheckProcessingDetailStpcashd obj in listNewDVOAPCheckProcessingDetailStpcashd)
                                        obj.doc_no = DocumentNo;
                                if (listUpdateDVOAPCheckProcessingDetailStpcashd.Count > 0)
                                    foreach (DVOAPCheckProcessingDetailStpcashd obj in listUpdateDVOAPCheckProcessingDetailStpcashd)
                                        obj.doc_no = DocumentNo;
                                if (listDeleteDVOAPCheckProcessingDetailStpcashd.Count > 0)
                                    foreach (DVOAPCheckProcessingDetailStpcashd obj in listDeleteDVOAPCheckProcessingDetailStpcashd)
                                        obj.doc_no = DocumentNo;

                                if (DeleteAllDetailLines)
                                {
                                    BLLAPCheckProcessingDetailStpcashd.DeleteAllDetailLines(ref objTransaction, objDVOAPCheckProcessingStpcashe.doc_no);
                                }
                                else
                                {
                                    BLLAPCheckProcessingDetailStpcashd.DeleteData(ref objTransaction, ref listDeleteDVOAPCheckProcessingDetailStpcashd);
                                    BLLAPCheckProcessingDetailStpcashd.UpdateData(ref objTransaction, ref listUpdateDVOAPCheckProcessingDetailStpcashd);
                                }
                                decimal DebitAmountInserted = 0, CreditAmountInserted = 0;
                                BLLAPCheckProcessingDetailStpcashd.InsertData(ref objTransaction, ref listNewDVOAPCheckProcessingDetailStpcashd, out DebitAmountInserted, out CreditAmountInserted);
                               // UpdateRequiredApproval(ref objTransaction, ApprovalTables.NonAPChecks, objDVOAPCheckProcessingStpcashe.doc_no);

                                if (objDVOAPCheckProcessingStpcashe.cash_acct > 0 && objDVOAPCheckProcessingStpcashe.check_no.Trim().Length > 0)
                                {
                                    string _accounttype = string.Empty, _accountkeyvalue = string.Empty, _accountdescription = string.Empty;
                                    int _accounttypeid = 0;
                                    BLLCommonUtilities.GetAccountInformation(objDVOAPCheckProcessingStpcashe.cash_acct, out _accountkeyvalue, out _accounttype, out _accounttypeid, out _accountdescription);
                                    if (_accountkeyvalue.Trim() == "08082000920002" || _accountkeyvalue.Trim() == "08082000920003")
                                    {
                                        parameters = new object[8];
                                        parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                                        parameters[1] = objDVOAPCheckProcessingStpcashe.check_no;
                                        parameters[2] = DVOApplicationUserInfo.UserId;
                                        parameters[3] = "IFMS APPLICATION";
                                        parameters[4] = objDVOAPCheckProcessingStpcashe.cash_amt;
                                        parameters[5] = DVOApplicationUserInfo.UserId;
                                        parameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                        parameters[7] = DVOApplicationUserInfo.MachineInfo;

                                        object InsStatus = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objDVOAPCheckProcessingStpcashe.INSERT_APCHECKRECORD, true);
                                        if (Convert.ToInt32(InsStatus) != 1)
                                            throw new Exception();
                                    }
                                }
                            }
                }
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

        //public static int UpdateData(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe, ref List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd)
        //{
        //    object objTransaction = objDALBaseClassHelper.GetTransactionObject();
        //    DALBaseClass objDALBaseClass = DALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        object[] parameters = new object[21];
        //        parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
        //        if (objDVOAPCheckProcessingStpcashe.chk_date == string.Empty) objDVOAPCheckProcessingStpcashe.chk_date = "01/01/1900";
        //        parameters[1] = objDVOAPCheckProcessingStpcashe.chk_date;
        //        parameters[2] = objDVOAPCheckProcessingStpcashe.vend_code;
        //        parameters[3] = objDVOAPCheckProcessingStpcashe.pay_to_code;
        //        parameters[4] = objDVOAPCheckProcessingStpcashe.check_no;
        //        parameters[5] = objDVOAPCheckProcessingStpcashe.doc_desc;
        //        parameters[6] = objDVOAPCheckProcessingStpcashe.cash_amt;
        //        parameters[7] = objDVOAPCheckProcessingStpcashe.cash_acct;
        //        parameters[8] = objDVOAPCheckProcessingStpcashe.cash_department;
        //        parameters[9] = objDVOAPCheckProcessingStpcashe.cash_deb_cred;
        //        parameters[10] = objDVOAPCheckProcessingStpcashe.oa_amt;
        //        parameters[11] = objDVOAPCheckProcessingStpcashe.print_chk;
        //        parameters[12] = objDVOAPCheckProcessingStpcashe.ok_to_post;
        //        parameters[13] = objDVOAPCheckProcessingStpcashe.chk_printed;
        //        parameters[14] = objDVOAPCheckProcessingStpcashe.ap_type;
        //        parameters[15] = objDVOAPCheckProcessingStpcashe.batch_id;
        //        parameters[16] = objDVOAPCheckProcessingStpcashe.min_voucher_no;
        //        parameters[17] = objDVOAPCheckProcessingStpcashe.tre_voucher_no;
        //        parameters[18] = objDVOAPCheckProcessingStpcashe.bus_name;
        //        parameters[19] = objDVOAPCheckProcessingStpcashe.current_approval;
        //        parameters[20] = objDVOAPCheckProcessingStpcashe.acd_id;

        //        objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOAPCheckProcessingStpcashe));

        //        DVOAPCheckProcessingDetailStpcashd tobj = new DVOAPCheckProcessingDetailStpcashd();
        //        tobj.doc_no = objDVOAPCheckProcessingStpcashe.doc_no;
        //        BLLAPCheckProcessingDetailStpcashd.DeleteData(ref objTransaction, ref tobj);
        //        BLLAPCheckProcessingDetailStpcashd.InsertData(ref objTransaction, ref listDVOAPCheckProcessingDetailStpcashd);

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
        //        return 0;
        //    }
        //    return 0;
        //}

        //public static int UpdateRequiredApproval(ref object objTransaction, ApprovalTables appTab, int DocNo)
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
        //        object[] parameters = new object[2];
        //        parameters[0] = DocNo;
        //        parameters[1] = BLLAccountingLiberary.GetRequiredApprovalLevel(appTab, DocNo);

        //        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOAPCheckProcessingStpcashe()).UPDATE_REQUIRED_APPROVAL);
        //        if (o == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(o) < 1)
        //            throw new Exception();
                
        //        parameters = null;
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
        //    return 1;
        //}

        public static bool CheckDateStatus(string chkDate)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = chkDate;
                object obj = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOAPCheckProcessingStpcashe()).CHECK_DATE_STATUS);
                if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
                    throw new Exception("Error occured to check current period.");
                else
                    if (obj.ToString().Trim().ToUpper() == "O")
                        return true;

                parameters = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }

        public static bool CheckNumberStatus(string CheckNumber)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = CheckNumber;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), (new DVOAPCheckProcessingStpcashe()).CHECK_NUMBER_STATUS))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
                                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 0)
                                        return true;
                }
                parameters = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }

        public static decimal GetVendor_OnAccount_Amount(string VendorCode)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = VendorCode;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), (new DVOAPCheckProcessingStpcashe()).GET_ONACCOUNT_AMOUNT))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
                                    return Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                }
                parameters = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            return 0;
        }

        public static List<DVOAPCheckProcessingStpcashe> GetCheckStatusInfo(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {
            //try
            //{
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe = new List<DVOAPCheckProcessingStpcashe>();

                object[] parameter = new object[1];
                parameter[0] = objDVOAPCheckProcessingStpcashe.batch_id;
                DataSet ds = objDALBaseClass.GetData(ref parameter, typeof(DVOAPCheckProcessingStpcashe), objDVOAPCheckProcessingStpcashe.GET_INFO_FOR_CHECK_STATUS);

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            using (DVOAPCheckProcessingStpcashe PCbjDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe())
                            {
                                PCbjDVOAPCheckProcessingStpcashe.chk_date = (dr[0] == DBNull.Value) ? string.Empty : dr[0].ToString();//v_chk_date
                                PCbjDVOAPCheckProcessingStpcashe.doc_no = (dr[1] == DBNull.Value) ? 0 : Convert.ToInt32(dr[1]);//v_doc_no
                                PCbjDVOAPCheckProcessingStpcashe.vend_code = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//
                                PCbjDVOAPCheckProcessingStpcashe.doc_desc = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();
                                PCbjDVOAPCheckProcessingStpcashe.cash_amt = (dr[4] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[4]);//
                                PCbjDVOAPCheckProcessingStpcashe.chk_printed = (dr[5] == DBNull.Value) ? string.Empty : Convert.ToString(dr[5]);//
                                PCbjDVOAPCheckProcessingStpcashe.print_chk = (dr[6] == DBNull.Value) ? string.Empty : Convert.ToString(dr[6]);//

                                PCbjDVOAPCheckProcessingStpcashe.RowId = (dr[7] == DBNull.Value) ? 0 : Convert.ToInt32(dr[7]);//



                                listDVOAPCheckProcessingStpcashe.Add(PCbjDVOAPCheckProcessingStpcashe);
                            }
                        }

                objDALBaseClass = null;
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //}
            return listDVOAPCheckProcessingStpcashe;

        }

        public static int updateChkStatus(ref object objTransaction, ref DVOAPCheckProcessingStpcashe UpobjDVOAPCheckProcessingStpcashe)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object  success = 0;

            try
            {
            object[] updparameter = new object[1];
            updparameter[0]=UpobjDVOAPCheckProcessingStpcashe.batch_id;

            success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref updparameter, typeof(DVOAPCheckProcessingStpcashe), UpobjDVOAPCheckProcessingStpcashe.UPD_INFO_FOR_CHECK_STATUS);

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
            return Convert.ToInt32(success);
        }

        public static int updateInfoOFCheckStatus(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object success = 0;

            try
            {
                
            object[] UpdParam = new object[10];
            UpdParam[0] = objDVOAPCheckProcessingStpcashe.doc_no;
            UpdParam[1] = Convert.ToDateTime(objDVOAPCheckProcessingStpcashe.chk_date);
            UpdParam[2] = objDVOAPCheckProcessingStpcashe.print_chk;
            UpdParam[3] = objDVOAPCheckProcessingStpcashe.collected_by;
            UpdParam[4] = Convert.ToDateTime(objDVOAPCheckProcessingStpcashe.collected_date);
            UpdParam[5] = objDVOAPCheckProcessingStpcashe.printed_by;
            UpdParam[6] = Convert.ToDateTime(objDVOAPCheckProcessingStpcashe.printed_date);
            UpdParam[7] = objDVOAPCheckProcessingStpcashe.updateby;
            UpdParam[8] = objDVOAPCheckProcessingStpcashe.updatemachineinfo;
            UpdParam[9] = Convert.ToDateTime(objDVOAPCheckProcessingStpcashe.updatedate);


            success = objDALBaseClass.UpdateData_ByTransaction(ref  objTransaction, ref UpdParam, typeof(DVOAPCheckProcessingStpcashe), objDVOAPCheckProcessingStpcashe.UPD_INFO_IN_STPCASHE);

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
            return Convert.ToInt32(success);



        }

        public static int InsertInoStpcashe(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objStpcashe, out int NewDocNo)
        {
            NewDocNo = 0;
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
                object[] parameters = new object[28];
                //parameters[0] = objStpcashe.doc_no;
                if (objStpcashe.chk_date == string.Empty) objStpcashe.chk_date = "01/01/1900";
                parameters[1] = objStpcashe.chk_date;
                parameters[2] = objStpcashe.vend_code;
                parameters[3] = objStpcashe.pay_to_code;
                parameters[4] = objStpcashe.check_no;
                parameters[5] = objStpcashe.doc_desc;
                parameters[6] = objStpcashe.cash_amt;
                parameters[7] = objStpcashe.cash_acct;
                parameters[8] = objStpcashe.cash_department;
                parameters[9] = objStpcashe.cash_deb_cred;
                parameters[10] = objStpcashe.oa_amt;
                parameters[11] = objStpcashe.oa_acct;
                parameters[12] = objStpcashe.oa_department;
                parameters[13] = objStpcashe.oa_deb_cred;
                parameters[14] = objStpcashe.print_chk;
                parameters[15] = objStpcashe.ok_to_post;
                parameters[16] = objStpcashe.chk_printed;
                parameters[17] = objStpcashe.ap_type;
                parameters[18] = objStpcashe.batch_id;
                parameters[19] = objStpcashe.min_voucher_no;
                parameters[20] = objStpcashe.tre_voucher_no;
                parameters[21] = objStpcashe.bus_name;
                parameters[22] = objStpcashe.current_approval;
                parameters[23] = objStpcashe.acd_id;
                parameters[24] = objStpcashe.required_approval;
                parameters[25] = objStpcashe.insertby;
                parameters[26] = objStpcashe.insertdate;
                parameters[27] = objStpcashe.insertmachineinfo;

                //Object nullTransactionObject = null;
                //NewDocNo = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref nullTransactionObject);
                NewDocNo = BLLAccountingLiberary.Auto_Next_APDocNo();
                if (NewDocNo > 0)
                {
                    parameters[0] = NewDocNo;
                    object objres = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objStpcashe.INSERT_SPNAME_NEW);
                    if (objres == null || Convert.ToInt32(objres) < 1)
                        throw new Exception("Error occured during inserting of new Accounts-Payable document."); 
                }
                else
                    throw new Exception("Account-Payable Control table is locked or empty.");

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

        //Added By Rahul JAin 14/12/2009
        public static List<DVOAPCheckProcessingStpcashe> GetDataByDocNo(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe = new List<DVOAPCheckProcessingStpcashe>();

            try
            {
                object[] parameters = new object[1];
                parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
                using (DataSet ds = objDALBaseClass.GetData(objDVOAPCheckProcessingStpcashe.FIND_DATA_BY_DOCNO(ref parameters)))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOAPCheckProcessingStpcashe tobjDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe())
                                {
                                    tobjDVOAPCheckProcessingStpcashe.chk_date = (dr[0] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr[0]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//p_chk_date
                                    tobjDVOAPCheckProcessingStpcashe.doc_no = (dr[1] == DBNull.Value) ? 0 : Convert.ToInt32(dr[1]);//p_doc_no
                                    tobjDVOAPCheckProcessingStpcashe.vend_code = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();//p_vend_code
                                    tobjDVOAPCheckProcessingStpcashe.pay_to_code = (dr[3] == DBNull.Value) ? string.Empty : dr[3].ToString().Trim();//p_pay_to_code
                                    tobjDVOAPCheckProcessingStpcashe.gross_entry = (dr[4] == DBNull.Value) ? string.Empty : dr[4].ToString().Trim();//v_gross_entry
                                    tobjDVOAPCheckProcessingStpcashe.def_mtaxcd = (dr[5] == DBNull.Value) ? string.Empty : dr[5].ToString().Trim();//v_def_mtaxcd
                                    tobjDVOAPCheckProcessingStpcashe.check_no = (dr[6] == DBNull.Value) ? string.Empty : dr[6].ToString().Trim();//p_check_no
                                    tobjDVOAPCheckProcessingStpcashe.doc_desc = (dr[7] == DBNull.Value) ? string.Empty : dr[7].ToString().Trim();//p_doc_desc
                                    tobjDVOAPCheckProcessingStpcashe.cash_amt = (dr[8] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[8]);//v_cash_amt
                                    tobjDVOAPCheckProcessingStpcashe.cash_acct = (dr[9] == DBNull.Value) ? 0 : Convert.ToInt32(dr[9]);//v_cash_acct
                                    tobjDVOAPCheckProcessingStpcashe.cash_department = (dr[10] == DBNull.Value) ? string.Empty : dr[10].ToString().Trim();//v_cash_department
                                    tobjDVOAPCheckProcessingStpcashe.cash_deb_cred = (dr[11] == DBNull.Value) ? string.Empty : dr[11].ToString().Trim();//v_cash_deb_cred
                                    tobjDVOAPCheckProcessingStpcashe.oa_amt = (dr[12] == DBNull.Value) ? 0 : Convert.ToDecimal(dr[12]);//v_oa_amt
                                    tobjDVOAPCheckProcessingStpcashe.oa_acct = (dr[13] == DBNull.Value) ? 0 : Convert.ToInt32(dr[13]);//v_oa_acct
                                    tobjDVOAPCheckProcessingStpcashe.oa_department = (dr[14] == DBNull.Value) ? string.Empty : dr[14].ToString().Trim();//v_oa_department
                                    tobjDVOAPCheckProcessingStpcashe.oa_deb_cred = (dr[15] == DBNull.Value) ? string.Empty : dr[15].ToString().Trim();//v_oa_deb_cred
                                    tobjDVOAPCheckProcessingStpcashe.print_chk = (dr[16] == DBNull.Value) ? string.Empty : dr[16].ToString().Trim();//p_print_chk
                                    tobjDVOAPCheckProcessingStpcashe.ok_to_post = (dr[17] == DBNull.Value) ? string.Empty : dr[17].ToString().Trim();//p_ok_to_post
                                    tobjDVOAPCheckProcessingStpcashe.chk_printed = (dr[18] == DBNull.Value) ? string.Empty : dr[18].ToString().Trim();//v_chk_printed
                                    tobjDVOAPCheckProcessingStpcashe.ap_type = (dr[19] == DBNull.Value) ? string.Empty : dr[19].ToString().Trim();//v_ap_type
                                    tobjDVOAPCheckProcessingStpcashe.batch_id = (dr[20] == DBNull.Value) ? 0 : Convert.ToInt32(dr[20]);//p_batch_id
                                    tobjDVOAPCheckProcessingStpcashe.min_voucher_no = (dr[21] == DBNull.Value) ? string.Empty : dr[21].ToString().Trim();//v_min_voucher_no
                                    tobjDVOAPCheckProcessingStpcashe.tre_voucher_no = (dr[22] == DBNull.Value) ? string.Empty : dr[22].ToString().Trim();//v_tre_voucher_no
                                    tobjDVOAPCheckProcessingStpcashe.bus_name = (dr[23] == DBNull.Value) ? string.Empty : dr[23].ToString().Trim();//v_bus_name
                                    tobjDVOAPCheckProcessingStpcashe.required_approval = (dr[24] == DBNull.Value) ? 0 : Convert.ToInt32(dr[24]);//v_required_approval
                                    tobjDVOAPCheckProcessingStpcashe.current_approval = (dr[25] == DBNull.Value) ? 0 : Convert.ToInt32(dr[25]);//v_current_approval
                                    tobjDVOAPCheckProcessingStpcashe.acd_id = (dr[26] == DBNull.Value) ? 0 : Convert.ToInt32(dr[26]);//v_acd_id
                                    tobjDVOAPCheckProcessingStpcashe.RowId = (dr[27] == DBNull.Value) ? 0 : Convert.ToInt32(dr[27]);//v_Rowid
                                    tobjDVOAPCheckProcessingStpcashe.keyvalue = (dr[28] == DBNull.Value) ? string.Empty : dr[28].ToString().Trim();//v_keyvalue

                                    //tobjDVOAPCheckProcessingStpcashe.entry_date = (dr[29] == DBNull.Value) ? string.Empty : dr[29].ToString().Trim();
                                    //tobjDVOAPCheckProcessingStpcashe.collected_by = (dr[30] == DBNull.Value) ? string.Empty : dr[30].ToString().Trim();
                                    //tobjDVOAPCheckProcessingStpcashe.collected_date = (dr[31] == DBNull.Value) ? string.Empty : dr[31].ToString().Trim();
                                    //tobjDVOAPCheckProcessingStpcashe.printed_by = (dr[32] == DBNull.Value) ? 0 : Convert.ToInt32(dr[32]);
                                    //tobjDVOAPCheckProcessingStpcashe.printed_date = (dr[33] == DBNull.Value) ? string.Empty : dr[33].ToString().Trim();

                                    listDVOAPCheckProcessingStpcashe.Add(tobjDVOAPCheckProcessingStpcashe);
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
            return listDVOAPCheckProcessingStpcashe;
        }




        public static List<DVOAPCheckProcessingStpcashe> GET_INFO(ref DVOAPCheckProcessingStpcashe objCheckProcessingStpcashe)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe = new List<DVOAPCheckProcessingStpcashe>();

            try
            {
                 object[] parameters = new object[1];
                parameters[0] = objCheckProcessingStpcashe.check_no;;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), objCheckProcessingStpcashe.GET_COUNT_FROM_STPCASHE))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOAPCheckProcessingStpcashe toobjDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe())
                                {
                                    toobjDVOAPCheckProcessingStpcashe.doc_no = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0]);//p_doc_no
                                    toobjDVOAPCheckProcessingStpcashe.check_no = (dr[1] == DBNull.Value) ? string.Empty : dr[1].ToString().Trim();//check_no
                                    toobjDVOAPCheckProcessingStpcashe.ok_to_post = (dr[2] == DBNull.Value) ? string.Empty : dr[2].ToString().Trim();

                                    listDVOAPCheckProcessingStpcashe.Add(toobjDVOAPCheckProcessingStpcashe);

                                }
                            }
                }
            }
            catch(Exception Ex)
            {
            }
            return listDVOAPCheckProcessingStpcashe;

        }


        public static bool CheckRecordStatusInApchecksrecord(int doc_no)
        {
            DVOAPCheckProcessingStpcashe objCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = doc_no;
                object obj1 = objDALBaseClass.ExecuteScalar(ref parameters, objCheckProcessingStpcashe.GetCheckPrintStatus);
                if (obj1 != DBNull.Value)
                {
                    if (Convert.ToInt32(obj1) > 0)
                    {
                        //check already printed there is no need to perform any operation.
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                parameters = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return true;
            }
            return true;
        }

        public static int UpdateMannualCheckStatus(ref object objTransaction, ref DVOAPCheckProcessingStpcashe UpobjDVOAPCheckProcessingStpcashe)
        {
            string Err_msg = string.Empty;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object success = 0;

            try
            {
                object[] parameters = new object[8];
                parameters[0] = UpobjDVOAPCheckProcessingStpcashe.doc_no; // Convert.ToInt32(dr["doc_no"]);
                parameters[1] = UpobjDVOAPCheckProcessingStpcashe.check_no;//dr["check_no"].ToString().Trim();
                parameters[2] = DVOApplicationUserInfo.UserId;
                parameters[3] = "IFMS APPLICATION - ACCOUNTS PAYABLE MANUAL";
                parameters[4] = UpobjDVOAPCheckProcessingStpcashe.cash_amt;// Convert.ToDecimal(dr["cash_amt"]);
                parameters[5] = DVOApplicationUserInfo.UserId;
                parameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[7] = DVOApplicationUserInfo.MachineInfo;

               //Update the status of chk_printed(table stpcashe) 'N' to 'Y', TO avoid repetion of printed checks
               //Insert data into table Apchecksrecord                                            
               //RETURN TYPE:--ERROR_NUM,Ins_Status,Upd_Status,Ins_RecMsg,Upd_APMsg;

                DataSet dsPrintCheck = objDALBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, UpobjDVOAPCheckProcessingStpcashe.INSERT_UPDATE_CHECKRECORD_AND_STPCASHE);
               //Checking whether any error occoured, checking Update status of STPCASHE, 
               //Cheking Insert Status APCHECKSRECORD and also checking status of lock release 
               if (dsPrintCheck != null)
               {
                   if (dsPrintCheck.Tables[0].Rows.Count > 0)
                   {
                       if (Convert.ToInt32(dsPrintCheck.Tables[0].Rows[0][5]) == 0)
                       {
                           if (Convert.ToInt32(dsPrintCheck.Tables[0].Rows[0][0]) == 1 && Convert.ToInt32(dsPrintCheck.Tables[0].Rows[0][1]) == 1 && Convert.ToInt32(dsPrintCheck.Tables[0].Rows[0][2]) == 1)
                           {
                               success = 1;
                           }
                           else
                           {
                               if (objTransaction != null)
                               {
                                   objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                               }
                               if (dsPrintCheck.Tables[0].Rows[0][3].ToString().Trim() == "NO")
                               {
                                   Err_msg = " Problem has occurred while Inserting 'apchecksrecord'.";
                                   Exception ex = new Exception(Err_msg);
                                   throw ex;
                               }
                               if (dsPrintCheck.Tables[0].Rows[0][4].ToString().Trim() == "NO")
                               {
                                   Err_msg = "Problem has occurred while updating 'stpcashe'";
                                   Exception ex = new Exception(Err_msg);
                                   throw ex;
                               }

                           }
                       }
                   }
               }
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
            return Convert.ToInt32(success);
        }

    }
}
