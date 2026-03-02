using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
namespace JKPS.BLL
{
    public class BLLDirectDepositMedia
    {
        public static DataTable GetDirectDepositeData(ref DVOddmStypddreAndStypddrd objSearch, bool shouldMakeAPEntry, int apBatchID)
        {
            DataSet ds = null;
            string ErrorMessage = string.Empty;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //DVOAPCheckProcessingStpcashe objStpcashe = new DVOAPCheckProcessingStpcashe();
            List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();
            DataTable objDataTable = new DataTable();
            bool doc_err = false;
            decimal arCashAmount = 0;
            object[] parametres = new object[6];
            try
            {
                //get payroll default configuration 
                objstycntrcList = BLLUpdPayDefault.GetAllPayrollDefaults();

                //get direct deposit media data
                parametres[0] = objSearch.BanckCode;
                parametres[1] = objSearch.Cash_acct_no;
                parametres[2] = objSearch.GenCheck;
                parametres[3] = objSearch.Date;
                parametres[4] = objSearch.bank_code;
                parametres[5] = objSearch.District;
                ds = objDALBaseClass.GetData(ref parametres, typeof(DVOddmStypddreAndStypddrd));


                ds.Tables[0].Columns[0].ColumnName = "v_bank_desc";
                ds.Tables[0].Columns[1].ColumnName = "v_amount";
                ds.Tables[0].Columns[2].ColumnName = "v_bank_acct_no";
                ds.Tables[0].Columns[3].ColumnName = "v_chk_digit";
                ds.Tables[0].Columns[4].ColumnName = "v_dfi_dest";
                ds.Tables[0].Columns[5].ColumnName = "v_empl_code";
                ds.Tables[0].Columns[6].ColumnName = "v_empl_name";
                ds.Tables[0].Columns[7].ColumnName = "v_pay_date";
                ds.Tables[0].Columns[8].ColumnName = "v_trace_number";
                ds.Tables[0].Columns[9].ColumnName = "v_trans_code";
                ds.Tables[0].Columns[10].ColumnName = "v_bank_code";
                ds.Tables[0].Columns[11].ColumnName = "v_batch_date";
                ds.Tables[0].Columns[12].ColumnName = "v_batch_no";
                ds.Tables[0].Columns[13].ColumnName = "v_dfi_immed";
                ds.Tables[0].Columns[14].ColumnName = "v_doc_no";
                ds.Tables[0].Columns[15].ColumnName = "v_entry_desc";
                ds.Tables[0].Columns[16].ColumnName = "v_file_id";
                ds.Tables[0].Columns[17].ColumnName = "v_svc_class";
                ds.Tables[0].Columns[18].ColumnName = "v_used";
                ds.Tables[0].Columns[19].ColumnName = "v_suppliercode";
                ds.Tables[0].Columns[20].ColumnName = "v_company_name";
                ds.Tables[0].Columns[21].ColumnName = "ActtType";
                ds.Tables[0].Columns.Add("priority_code");
                ds.Tables[0].Columns.Add("file_hash");
                ds.Tables[0].Columns.Add("create_date");
                ds.Tables[0].Columns.Add("resrv_blank5");
                ds.Tables[0].Columns.Add("ein_number");
                ds.Tables[0].Columns.Add("immed_dest_dfi");
                ds.Tables[0].Columns.Add("immed_chk_digit");
                ds.Tables[0].Columns.Add("immed_dest_name");
                ds.Tables[0].Columns.Add("file_id");
                ds.Tables[0].Columns.Add("Problem0");
                ds.Tables[0].Columns.Add("recordsize");
                ds.Tables[0].Columns.Add("blockfactor");
                ds.Tables[0].Columns.Add("formatcode");
                ds.Tables[0].Columns.Add("entryclass");
                ds.Tables[0].Columns.Add("statuscode");
                ds.Tables[0].Columns.Add("IntegralAmount", typeof(Int32));
                ds.Tables[0].Columns.Add("FractionalAmount", typeof(Int32));
                ds.Tables[0].Columns.Add("ACTINFO", typeof(String));
                ds.Tables[0].Columns.Add("AMOUNTINCENTS", typeof(Int32));

                objDataTable = ds.Tables[0].Clone();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //Commented by Sarvjeet on 02/02/2010,
                    //Now check will be created for every bank on bank code group.  
                    //if (objSearch.GenCheck.Trim() == "Y")
                    //{
                    //    objStpcashe = new DVOAPCheckProcessingStpcashe();
                    //    objStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //    //objStpcashe.doc_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref objTransaction);
                    //    //objStpcashe.vend_code = ds.Tables[0].Rows[0]["v_suppliercode"].ToString().Trim();
                    //    objStpcashe.doc_desc = "PAYROLL DIR.DEP. COVERAGE";
                    //    objStpcashe.pay_to_code = "PAYTO";
                    //    objStpcashe.cash_amt = 0;
                    //    objStpcashe.cash_acct = 951;
                    //    objStpcashe.cash_department = "000";
                    //    objStpcashe.cash_deb_cred = "CR";
                    //    objStpcashe.print_chk = "Y";
                    //    objStpcashe.ok_to_post = "N";
                    //    objStpcashe.chk_printed = "N";
                    //    objStpcashe.ap_type = "N";
                    //    //objStpcashe.bus_name = ds.Tables[0].Rows[0]["v_bank_desc"].ToString().Trim();
                    //    objStpcashe.current_approval = -1; // Makes it approved and ready for processing
                    //    objStpcashe.batch_id = apBatchID;
                    //}

                    //get field_id.........


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        DataRow drn = objDataTable.NewRow();
                        DVOddmStypddreAndStypddrd objdata = new DVOddmStypddreAndStypddrd();

                        objdata.doc_no = (dr["v_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) : 0);
            //objdata.Amount = (dr["v_amount"] != DBNull.Value ? Convert.ToDecimal(dr["v_amount"]) : 0.0M);
            //objdata.empl_code = (dr["v_empl_code"] != DBNull.Value ? Convert.ToString(dr["v_empl_code"]) : string.Empty);
            //objdata.bank_code = (dr["v_bank_code"] != DBNull.Value ? Convert.ToString(dr["v_bank_code"]) : string.Empty);
            //objdata.bank_acct_no = (dr["v_bank_acct_no"] != DBNull.Value ? Convert.ToString(dr["v_bank_acct_no"]) : string.Empty);
            //objdata.bank_desc = (dr["v_bank_desc"] != DBNull.Value ? Convert.ToString(dr["v_bank_desc"]) : string.Empty);
            //dr["ein_number"] = objstycntrcList[0].ein_number;
            //dr["immed_dest_dfi"] = objstycntrcList[0].immed_dest_dfi;
            //dr["immed_chk_digit"] = objstycntrcList[0].immed_chk_digit;
            //dr["immed_dest_name"] = objstycntrcList[0].immed_dest_name;

            objdata.Amount = (dr["v_amount"] != DBNull.Value ? Convert.ToDecimal(dr["v_amount"]) : 0.0M);
            objdata.empl_code = (dr["v_empl_code"] != DBNull.Value ? Convert.ToString(dr["v_empl_code"]) : string.Empty);
            objdata.bank_code = (dr["v_bank_code"] != DBNull.Value ? Convert.ToString(dr["v_bank_code"]) : string.Empty);
            objdata.bank_acct_no = (dr["v_bank_acct_no"] != DBNull.Value ? Convert.ToString(dr["v_bank_acct_no"]) : string.Empty);
            objdata.bank_desc = (dr["v_bank_desc"] != DBNull.Value ? Convert.ToString(dr["v_bank_desc"]) : string.Empty);
            dr["ein_number"] = objstycntrcList[0].ein_number;
            dr["immed_dest_dfi"] = objstycntrcList[0].immed_dest_dfi;
            dr["immed_chk_digit"] = objstycntrcList[0].immed_chk_digit;
            dr["immed_dest_name"] = objstycntrcList[0].immed_dest_name;

            dr["recordsize"] = "94";
                        dr["blockfactor"] = "10";
                        dr["formatcode"] = "1";
                        dr["entryclass"] = "PPD";
                        dr["statuscode"] = "1";

                        string ein_nu = dr["ein_number"].ToString();
                        if (ein_nu.Trim().Length >= 10)
                        {
                            dr["ein_number"] = "1" + "," + ein_nu.Substring(1, 2) + "," + ein_nu.Substring(4, 10);
                        }
                        dr["priority_code"] = "01";
                        dr["create_date"] = DateTime.Now.ToShortDateString();
                        dr["file_id"] = GetField_id();

                        // remove decimal point
                        dr["v_amount"] = objdata.Amount;// *100;

                        dr["FractionalAmount"] = Math.Abs(Decimal.Subtract(objdata.Amount, decimal.Floor(objdata.Amount)) * 100);
                        dr["IntegralAmount"] = Math.Abs(Decimal.Subtract(objdata.Amount, Decimal.Subtract(objdata.Amount, decimal.Floor(objdata.Amount))));
                        dr["AMOUNTINCENTS"] = objdata.Amount * 100;
                        if (dr["v_empl_name"] != DBNull.Value)
                        {
                            if (dr["v_empl_name"].ToString().Trim().Length > 0)
                            {
                                dr["v_empl_name"] = dr["v_empl_name"].ToString().Trim().Replace(',', ' ');
                            }
                        }
                        // Get the type of account for this entry
                        //object[] Empbd_param = new object[3];
                        //Empbd_param[0] = objdata.empl_code;
                        //Empbd_param[1] = objdata.bank_code;
                        //Empbd_param[2] = objdata.bank_acct_no;
                        //object type = objDALBaseClass.ExecuteScalar(ref Empbd_param, objdata.GET_TYPEOFACCT);
                        // string typea;
                        if ((dr["ActtType"] == DBNull.Value) || (dr["ActtType"] == String.Empty))
                        {
                            dr["resrv_blank5"] = "S";
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() != "S" && dr["ActtType"].ToString().Trim() != "C")
                        {
                            dr["resrv_blank5"] = "S";
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() == "S")
                        {
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() == "C")
                        {
                            dr["ACTINFO"] = "DDA";
                        }
                        //  type = dr["ActtType"].ToString().Trim();
                        //if (type == null || (type.ToString().Trim() != "S" && type.ToString().Trim() != "C"))
                        //{
                        //    dr["resrv_blank5"] = "S";
                        //}

                        //Calculate out total for the cash amount.
                        arCashAmount += objdata.Amount;
                        dr["v_bank_desc"] = objdata.bank_desc;

                        //process on after doc_no group..........
                        bool _postingStatus = false;
                        if (ds.Tables[0].Rows.Count != i + 1)
                        {
                            if (ds.Tables[0].Rows[i + 1]["v_doc_no"] != DBNull.Value)
                                if (objdata.doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["v_doc_no"]))
                                {
                                    _postingStatus = true;
                                }
                        }
                        else if (ds.Tables[0].Rows.Count == i + 1)
                            _postingStatus = true;

                        if (_postingStatus)
                        {
                            //updates individual rows in stypddre processed in
                            //this direct deposit tape.  The rows are marked as used, given a
                            //date stamp, and a unique file_id.
                            object[] updparameter = new object[3];
                            //updparameter[0] = Convert.ToDateTime(dr["create_date"]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            updparameter[0] = dr["create_date"].ToString(); //.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            updparameter[1] = objdata.doc_no;
                            updparameter[2] = dr["file_id"].ToString();
                            object upsresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updparameter, objdata.UPD_YDDRE, true);
                            if (upsresult != null)
                            {
                                if (upsresult.ToString().Trim() != string.Empty)
                                {
                                    if (Convert.ToInt32(upsresult) != 1)
                                    {
                                        doc_err = true;
                                        ErrorMessage = "Error :An Sql Error has occurred while updating (stypddre)";
                                    }
                                }
                            }
                        }
                        //Create check for every bank.. 
                        bool bankChanged = false;
                        if (ds.Tables[0].Rows.Count != i + 1)
                        {

                            if (objdata.bank_code.Trim() !=ds.Tables[0].Rows[i + 1]["v_bank_code"].ToString().Trim())
                                bankChanged = true;

                        }
                        else if (ds.Tables[0].Rows.Count == i + 1)
                            bankChanged = true;

                        if (bankChanged)
                        {
                            if (objSearch.GenCheck.Trim() == "Y")
                            {
                                // Insert into stpcashe ...........
                                DVOAPCheckProcessingStpcashe objStpcashe = new DVOAPCheckProcessingStpcashe();
                                objStpcashe.chk_date = DateTime.Now.ToShortDateString(); ; //.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                //objStpcashe.doc_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref objTransaction);
                                //objStpcashe.vend_code = ds.Tables[0].Rows[0]["v_suppliercode"].ToString().Trim();
                                objStpcashe.doc_desc = "PAYROLL DIR.DEP. COVERAGE";
                                objStpcashe.pay_to_code = "PAYTO";
                                objStpcashe.cash_amt = arCashAmount;
                                objStpcashe.cash_acct = 1000;
                                objStpcashe.cash_department = "000";
                                objStpcashe.cash_deb_cred = "CR";
                                objStpcashe.print_chk = "Y";
                                objStpcashe.ok_to_post = "N";
                                objStpcashe.chk_printed = "N";
                                objStpcashe.ap_type = "N";
                                //objStpcashe.bus_name = ds.Tables[0].Rows[0]["v_bank_desc"].ToString().Trim();
                                objStpcashe.current_approval = -1; // Makes it approved and ready for processing
                                objStpcashe.batch_id = apBatchID;
                                objStpcashe.vend_code = dr["v_suppliercode"].ToString().Trim();
                                objStpcashe.bus_name = dr["v_bank_desc"].ToString().Trim();
                                // Insert into stpcashd ...........
                                DVOAPCheckProcessingDetailStpcashd objstpcashd = new DVOAPCheckProcessingDetailStpcashd();
                                objstpcashd.doc_no = objStpcashe.doc_no;
                                objstpcashd.dist_acct = 1010;// objStpcashe.cash_acct;
                                objstpcashd.dist_deb_cred = "DB";
                                objstpcashd.dist_department = "000";
                                objstpcashd.dist_amt = arCashAmount;
                                List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                                listDVOAPCheckProcessingDetailStpcashd.Add(objstpcashd);
                                int NewDocNo = 0;
                                int res = 0;
                                if (shouldMakeAPEntry)
                                {
                                   res = BLLAPCheckProcessingStpcashe.InsertData(ref objTransaction, ref objStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out NewDocNo, false);
                                    if (res != 1)
                                    {
                                        doc_err = true;
                                        ErrorMessage = "Error :An Sql Error has occurred while updating (stpcashe)";
                                    }
                                }
                                //commit all work........
                                if (!doc_err)
                                {
                                    //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                }
                                else
                                {
                                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    objDataTable.Rows.Clear();
                                    drn["Problem0"] = ErrorMessage;
                                    objDataTable.Rows.Add(drn);
                                    return objDataTable;
                                }
                            }
                            arCashAmount = 0;
                        }
                        drn.ItemArray = dr.ItemArray;
                        objDataTable.Rows.Add(drn);
                    }
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);

                }
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return objDataTable;
        }
        public static DataTable GetDirectDepositeDataforExcel(ref DVOddmStypddreAndStypddrd objSearch, bool shouldMakeAPEntry, int apBatchID)
        {
            DataSet ds = null;
            string ErrorMessage = string.Empty;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //DVOAPCheckProcessingStpcashe objStpcashe = new DVOAPCheckProcessingStpcashe();
            List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();
            DataTable objDataTable = new DataTable();
            bool doc_err = false;
            decimal arCashAmount = 0;
            object[] parametres = new object[3];
            try
            {
                //get payroll default configuration 
                objstycntrcList = BLLUpdPayDefault.GetAllPayrollDefaults();

                //get direct deposit media data
                parametres[0] = objSearch.BanckCode;
                parametres[1] = objSearch.Cash_acct_no;
                parametres[2] = objSearch.GenCheck;
                ds = objDALBaseClass.GetData(ref parametres, typeof(DVOddmStypddreAndStypddrd));


                ds.Tables[0].Columns[0].ColumnName = "v_bank_desc";
                ds.Tables[0].Columns[1].ColumnName = "v_amount";
                ds.Tables[0].Columns[2].ColumnName = "v_bank_acct_no";
                ds.Tables[0].Columns[3].ColumnName = "v_chk_digit";
                ds.Tables[0].Columns[4].ColumnName = "v_dfi_dest";
                ds.Tables[0].Columns[5].ColumnName = "v_empl_code";
                ds.Tables[0].Columns[6].ColumnName = "v_empl_name";
                ds.Tables[0].Columns[7].ColumnName = "v_pay_date";
                ds.Tables[0].Columns[8].ColumnName = "v_trace_number";
                ds.Tables[0].Columns[9].ColumnName = "v_trans_code";
                ds.Tables[0].Columns[10].ColumnName = "v_bank_code";
                ds.Tables[0].Columns[11].ColumnName = "v_batch_date";
                ds.Tables[0].Columns[12].ColumnName = "v_batch_no";
                ds.Tables[0].Columns[13].ColumnName = "v_dfi_immed";
                ds.Tables[0].Columns[14].ColumnName = "v_doc_no";
                ds.Tables[0].Columns[15].ColumnName = "v_entry_desc";
                ds.Tables[0].Columns[16].ColumnName = "v_file_id";
                ds.Tables[0].Columns[17].ColumnName = "v_svc_class";
                ds.Tables[0].Columns[18].ColumnName = "v_used";
                ds.Tables[0].Columns[19].ColumnName = "v_suppliercode";
                ds.Tables[0].Columns[20].ColumnName = "v_company_name";
                ds.Tables[0].Columns[21].ColumnName = "ActtType";
                ds.Tables[0].Columns.Add("priority_code");
                ds.Tables[0].Columns.Add("file_hash");
                ds.Tables[0].Columns.Add("create_date");
                ds.Tables[0].Columns.Add("resrv_blank5");
                ds.Tables[0].Columns.Add("ein_number");
                ds.Tables[0].Columns.Add("immed_dest_dfi");
                ds.Tables[0].Columns.Add("immed_chk_digit");
                ds.Tables[0].Columns.Add("immed_dest_name");
                ds.Tables[0].Columns.Add("file_id");
                ds.Tables[0].Columns.Add("Problem0");
                ds.Tables[0].Columns.Add("recordsize");
                ds.Tables[0].Columns.Add("blockfactor");
                ds.Tables[0].Columns.Add("formatcode");
                ds.Tables[0].Columns.Add("entryclass");
                ds.Tables[0].Columns.Add("statuscode");
                ds.Tables[0].Columns.Add("IntegralAmount", typeof(Int32));
                ds.Tables[0].Columns.Add("FractionalAmount", typeof(Int32));
                ds.Tables[0].Columns.Add("ACTINFO", typeof(String));
                ds.Tables[0].Columns.Add("AMOUNTINCENTS", typeof(Int32));

                objDataTable = ds.Tables[0].Clone();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //Commented by Sarvjeet on 02/02/2010,
                    //Now check will be created for every bank on bank code group.  
                    //if (objSearch.GenCheck.Trim() == "Y")
                    //{
                    //    objStpcashe = new DVOAPCheckProcessingStpcashe();
                    //    objStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //    //objStpcashe.doc_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref objTransaction);
                    //    //objStpcashe.vend_code = ds.Tables[0].Rows[0]["v_suppliercode"].ToString().Trim();
                    //    objStpcashe.doc_desc = "PAYROLL DIR.DEP. COVERAGE";
                    //    objStpcashe.pay_to_code = "PAYTO";
                    //    objStpcashe.cash_amt = 0;
                    //    objStpcashe.cash_acct = 951;
                    //    objStpcashe.cash_department = "000";
                    //    objStpcashe.cash_deb_cred = "CR";
                    //    objStpcashe.print_chk = "Y";
                    //    objStpcashe.ok_to_post = "N";
                    //    objStpcashe.chk_printed = "N";
                    //    objStpcashe.ap_type = "N";
                    //    //objStpcashe.bus_name = ds.Tables[0].Rows[0]["v_bank_desc"].ToString().Trim();
                    //    objStpcashe.current_approval = -1; // Makes it approved and ready for processing
                    //    objStpcashe.batch_id = apBatchID;
                    //}

                    //get field_id.........


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        DataRow drn = objDataTable.NewRow();
                        DVOddmStypddreAndStypddrd objdata = new DVOddmStypddreAndStypddrd();

                        objdata.doc_no = (dr["v_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) : 0);
                        objdata.Amount = (dr["v_amount"] != DBNull.Value ? Convert.ToDecimal(dr["v_amount"]) : 0.0M);
                        objdata.empl_code = (dr["v_empl_code"] != DBNull.Value ? Convert.ToString(dr["v_empl_code"]) : string.Empty);
                        objdata.bank_code = (dr["v_bank_code"] != DBNull.Value ? Convert.ToString(dr["v_bank_code"]) : string.Empty);
                        objdata.bank_acct_no = (dr["v_bank_acct_no"] != DBNull.Value ? Convert.ToString(dr["v_bank_acct_no"]) : string.Empty);
                        objdata.bank_desc = (dr["v_bank_desc"] != DBNull.Value ? Convert.ToString(dr["v_bank_desc"]) : string.Empty);
                        dr["ein_number"] = objstycntrcList[0].ein_number;
                        dr["immed_dest_dfi"] = objstycntrcList[0].immed_dest_dfi;
                        dr["immed_chk_digit"] = objstycntrcList[0].immed_chk_digit;
                        dr["immed_dest_name"] = objstycntrcList[0].immed_dest_name;

                        dr["recordsize"] = "94";
                        dr["blockfactor"] = "10";
                        dr["formatcode"] = "1";
                        dr["entryclass"] = "PPD";
                        dr["statuscode"] = "1";

                        string ein_nu = dr["ein_number"].ToString();
                        if (ein_nu.Trim().Length >= 10)
                        {
                            dr["ein_number"] = "1" + "," + ein_nu.Substring(1, 2) + "," + ein_nu.Substring(4, 10);
                        }
                        dr["priority_code"] = "01";
                        dr["create_date"] = DVOApplicationUserInfo.CurrentDate;
                        dr["file_id"] = GetField_id();

                        // remove decimal point
                        dr["v_amount"] = objdata.Amount;// *100;

                        dr["FractionalAmount"] = Math.Abs(Decimal.Subtract(objdata.Amount, decimal.Floor(objdata.Amount)) * 100);
                        dr["IntegralAmount"] = Math.Abs(Decimal.Subtract(objdata.Amount, Decimal.Subtract(objdata.Amount, decimal.Floor(objdata.Amount))));
                        dr["AMOUNTINCENTS"] = objdata.Amount * 100;
                        if (dr["v_empl_name"] != DBNull.Value)
                        {
                            if (dr["v_empl_name"].ToString().Trim().Length > 0)
                            {
                                dr["v_empl_name"] = dr["v_empl_name"].ToString().Trim().Replace(',', ' ');
                            }
                        }
                        // Get the type of account for this entry
                        //object[] Empbd_param = new object[3];
                        //Empbd_param[0] = objdata.empl_code;
                        //Empbd_param[1] = objdata.bank_code;
                        //Empbd_param[2] = objdata.bank_acct_no;
                        //object type = objDALBaseClass.ExecuteScalar(ref Empbd_param, objdata.GET_TYPEOFACCT);
                        // string typea;
                        if ((dr["ActtType"] == DBNull.Value) || (dr["ActtType"] == String.Empty))
                        {
                            dr["resrv_blank5"] = "S";
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() != "S" && dr["ActtType"].ToString().Trim() != "C")
                        {
                            dr["resrv_blank5"] = "S";
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() == "S")
                        {
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() == "C")
                        {
                            dr["ACTINFO"] = "DDA";
                        }
                        //  type = dr["ActtType"].ToString().Trim();
                        //if (type == null || (type.ToString().Trim() != "S" && type.ToString().Trim() != "C"))
                        //{
                        //    dr["resrv_blank5"] = "S";
                        //}

                        //Calculate out total for the cash amount.
                        arCashAmount += objdata.Amount;
                        dr["v_bank_desc"] = objdata.bank_desc;

                        //process on after doc_no group..........
                        bool _postingStatus = false;
                        if (ds.Tables[0].Rows.Count != i + 1)
                        {
                            if (ds.Tables[0].Rows[i + 1]["v_doc_no"] != DBNull.Value)
                                if (objdata.doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["v_doc_no"]))
                                {
                                    _postingStatus = true;
                                }
                        }
                        else if (ds.Tables[0].Rows.Count == i + 1)
                            _postingStatus = true;

                        if (_postingStatus)
                        {
                            //updates individual rows in stypddre processed in
                            //this direct deposit tape.  The rows are marked as used, given a
                            //date stamp, and a unique file_id.
                            object[] updparameter = new object[3];
                            updparameter[0] = Convert.ToDateTime(dr["create_date"]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            updparameter[1] = objdata.doc_no;
                            updparameter[2] = dr["file_id"].ToString();
                            object upsresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updparameter, objdata.UPD_YDDRE, true);
                            if (upsresult != null)
                            {
                                if (upsresult.ToString().Trim() != string.Empty)
                                {
                                    if (Convert.ToInt32(upsresult) != 1)
                                    {
                                        doc_err = true;
                                        ErrorMessage = "Error :An Sql Error has occurred while updating (stypddre)";
                                    }
                                }
                            }
                        }
                        //Create check for every bank.. 
                        bool bankChanged = false;
                        if (ds.Tables[0].Rows.Count != i + 1)
                        {

                            if (objdata.bank_code.Trim() != ds.Tables[0].Rows[i + 1]["v_bank_code"].ToString().Trim())
                                bankChanged = true;

                        }
                        else if (ds.Tables[0].Rows.Count == i + 1)
                            bankChanged = true;

                        if (bankChanged)
                        {
                            if (objSearch.GenCheck.Trim() == "Y")
                            {
                                // Insert into stpcashe ...........
                                DVOAPCheckProcessingStpcashe objStpcashe = new DVOAPCheckProcessingStpcashe();
                                objStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                //objStpcashe.doc_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref objTransaction);
                                //objStpcashe.vend_code = ds.Tables[0].Rows[0]["v_suppliercode"].ToString().Trim();
                                objStpcashe.doc_desc = "PAYROLL DIR.DEP. COVERAGE";
                                objStpcashe.pay_to_code = "PAYTO";
                                objStpcashe.cash_amt = arCashAmount;
                                objStpcashe.cash_acct = 1000;
                                objStpcashe.cash_department = "000";
                                objStpcashe.cash_deb_cred = "CR";
                                objStpcashe.print_chk = "Y";
                                objStpcashe.ok_to_post = "N";
                                objStpcashe.chk_printed = "N";
                                objStpcashe.ap_type = "N";
                                //objStpcashe.bus_name = ds.Tables[0].Rows[0]["v_bank_desc"].ToString().Trim();
                                objStpcashe.current_approval = -1; // Makes it approved and ready for processing
                                objStpcashe.batch_id = apBatchID;
                                objStpcashe.vend_code = dr["v_suppliercode"].ToString().Trim();
                                objStpcashe.bus_name = dr["v_bank_desc"].ToString().Trim();
                                // Insert into stpcashd ...........
                                DVOAPCheckProcessingDetailStpcashd objstpcashd = new DVOAPCheckProcessingDetailStpcashd();
                                objstpcashd.doc_no = objStpcashe.doc_no;
                                objstpcashd.dist_acct = 1010;// objStpcashe.cash_acct;
                                objstpcashd.dist_deb_cred = "DB";
                                objstpcashd.dist_department = "000";
                                objstpcashd.dist_amt = arCashAmount;
                                List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                                listDVOAPCheckProcessingDetailStpcashd.Add(objstpcashd);
                                int NewDocNo = 0;
                                int res = 0;
                                if (shouldMakeAPEntry)
                                {
                                    res = BLLAPCheckProcessingStpcashe.InsertData(ref objTransaction, ref objStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out NewDocNo, false);
                                    if (res != 1)
                                    {
                                        doc_err = true;
                                        ErrorMessage = "Error :An Sql Error has occurred while updating (stpcashe)";
                                    }
                                }
                                //commit all work........
                                if (!doc_err)
                                {
                                    //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                }
                                else
                                {
                                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    objDataTable.Rows.Clear();
                                    drn["Problem0"] = ErrorMessage;
                                    objDataTable.Rows.Add(drn);
                                    return objDataTable;
                                }
                            }
                            arCashAmount = 0;
                        }
                        drn.ItemArray = dr.ItemArray;
                        objDataTable.Rows.Add(drn);
                    }
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);

                }
                throw ex;
            }
            return objDataTable;
        }

        public static DataTable GetDuplicateDirectDepositeData(ref DVOddmStypddreAndStypddrd objSearch, bool shouldMakeAPEntry)
        {
            DataSet ds = null;
            string ErrorMessage = string.Empty;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //DVOAPCheckProcessingStpcashe objStpcashe = new DVOAPCheckProcessingStpcashe();
            List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();
            DataTable objDataTable = new DataTable();
            bool doc_err = false;
            object[] parametres = new object[4];
            try
            {
                //get payroll default configuration 
                objstycntrcList = BLLUpdPayDefault.GetAllPayrollDefaults();

                //get direct deposit media data
                parametres[0] = objSearch.BanckCode;
                parametres[1] = objSearch.Cash_acct_no;
                parametres[2] = objSearch.GenCheck;
                parametres[3] = objSearch.Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                ds = objDALBaseClass.GetData((new DVOddmStypddreAndStypddrd()).GET_DUPLICATE_DIRECT_DEPOSIT_MEDIA_ITEM(ref parametres));


                ds.Tables[0].Columns[0].ColumnName = "v_bank_desc";
                ds.Tables[0].Columns[1].ColumnName = "v_amount";
                ds.Tables[0].Columns[2].ColumnName = "v_bank_acct_no";
                ds.Tables[0].Columns[3].ColumnName = "v_chk_digit";
                ds.Tables[0].Columns[4].ColumnName = "v_dfi_dest";
                ds.Tables[0].Columns[5].ColumnName = "v_empl_code";
                ds.Tables[0].Columns[6].ColumnName = "v_empl_name";
                ds.Tables[0].Columns[7].ColumnName = "v_pay_date";
                ds.Tables[0].Columns[8].ColumnName = "v_trace_number";
                ds.Tables[0].Columns[9].ColumnName = "v_trans_code";
                ds.Tables[0].Columns[10].ColumnName = "v_bank_code";
                ds.Tables[0].Columns[11].ColumnName = "v_batch_date";
                ds.Tables[0].Columns[12].ColumnName = "v_batch_no";
                ds.Tables[0].Columns[13].ColumnName = "v_dfi_immed";
                ds.Tables[0].Columns[14].ColumnName = "v_doc_no";
                ds.Tables[0].Columns[15].ColumnName = "v_entry_desc";
                ds.Tables[0].Columns[16].ColumnName = "v_file_id";
                ds.Tables[0].Columns[17].ColumnName = "v_svc_class";
                ds.Tables[0].Columns[18].ColumnName = "v_used";
                ds.Tables[0].Columns[19].ColumnName = "v_suppliercode";
                ds.Tables[0].Columns[20].ColumnName = "v_company_name";
                ds.Tables[0].Columns[21].ColumnName = "ActtType";
                ds.Tables[0].Columns.Add("priority_code");
                ds.Tables[0].Columns.Add("file_hash");
                ds.Tables[0].Columns.Add("create_date");
                ds.Tables[0].Columns.Add("resrv_blank5");
                ds.Tables[0].Columns.Add("ein_number");
                ds.Tables[0].Columns.Add("immed_dest_dfi");
                ds.Tables[0].Columns.Add("immed_chk_digit");
                ds.Tables[0].Columns.Add("immed_dest_name");
                ds.Tables[0].Columns.Add("file_id");
                ds.Tables[0].Columns.Add("Problem0");
                ds.Tables[0].Columns.Add("recordsize");
                ds.Tables[0].Columns.Add("blockfactor");
                ds.Tables[0].Columns.Add("formatcode");
                ds.Tables[0].Columns.Add("entryclass");
                ds.Tables[0].Columns.Add("statuscode");
                ds.Tables[0].Columns.Add("IntegralAmount", typeof(Int32));
                ds.Tables[0].Columns.Add("FractionalAmount", typeof(Int32));
                ds.Tables[0].Columns.Add("ACTINFO", typeof(String));
                ds.Tables[0].Columns.Add("AMOUNTINCENTS", typeof(Int32));

                objDataTable = ds.Tables[0].Clone();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //if (objSearch.GenCheck.Trim() == "Y")
                    //{
                    //    objStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //    //objStpcashe.doc_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref objTransaction);
                    //    objStpcashe.vend_code = ds.Tables[0].Rows[0]["v_suppliercode"].ToString().Trim();
                    //    objStpcashe.doc_desc = "PAYROLL DIR.DEP. COVERAGE";
                    //    objStpcashe.pay_to_code = "PAYTO";
                    //    objStpcashe.cash_amt = 0;
                    //    objStpcashe.cash_acct = 951;
                    //    objStpcashe.cash_department = "000";
                    //    objStpcashe.cash_deb_cred = "CR";
                    //    objStpcashe.print_chk = "Y";
                    //    objStpcashe.ok_to_post = "N";
                    //    objStpcashe.chk_printed = "N";
                    //    objStpcashe.ap_type = "N";
                    //    objStpcashe.bus_name = ds.Tables[0].Rows[0]["v_bank_desc"].ToString().Trim();
                    //    objStpcashe.current_approval = -1; // Makes it approved and ready for processing
                    //}

                    //get field_id.........


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        DataRow drn = objDataTable.NewRow();
                        DVOddmStypddreAndStypddrd objdata = new DVOddmStypddreAndStypddrd();

                        objdata.doc_no = (dr["v_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) : 0);
                        objdata.Amount = (dr["v_amount"] != DBNull.Value ? Convert.ToDecimal(dr["v_amount"]) : 0.0M);
                        objdata.empl_code = (dr["v_empl_code"] != DBNull.Value ? Convert.ToString(dr["v_empl_code"]) : string.Empty);
                        objdata.bank_code = (dr["v_bank_code"] != DBNull.Value ? Convert.ToString(dr["v_bank_code"]) : string.Empty);
                        objdata.bank_acct_no = (dr["v_bank_acct_no"] != DBNull.Value ? Convert.ToString(dr["v_bank_acct_no"]) : string.Empty);
                        objdata.bank_desc = (dr["v_bank_desc"] != DBNull.Value ? Convert.ToString(dr["v_bank_desc"]) : string.Empty);
                        dr["ein_number"] = objstycntrcList[0].ein_number;
                        dr["immed_dest_dfi"] = objstycntrcList[0].immed_dest_dfi;
                        dr["immed_chk_digit"] = objstycntrcList[0].immed_chk_digit;
                        dr["immed_dest_name"] = objstycntrcList[0].immed_dest_name;

                        dr["recordsize"] = "94";
                        dr["blockfactor"] = "10";
                        dr["formatcode"] = "1";
                        dr["entryclass"] = "PPD";
                        dr["statuscode"] = "1";

                        string ein_nu = dr["ein_number"].ToString();
                        if (ein_nu.Trim().Length >= 10)
                        {
                            dr["ein_number"] = "1" + "," + ein_nu.Substring(1, 2) + "," + ein_nu.Substring(4, 10);
                        }
                        dr["priority_code"] = "01";
                        dr["create_date"] = DVOApplicationUserInfo.CurrentDate;
                        dr["file_id"] = GetField_id();

                        // remove decimal point
                        dr["v_amount"] = objdata.Amount;// *100;

                        dr["FractionalAmount"] = Math.Abs(Decimal.Subtract(objdata.Amount, decimal.Floor(objdata.Amount)) * 100);
                        dr["IntegralAmount"] = Math.Abs(Decimal.Subtract(objdata.Amount, Decimal.Subtract(objdata.Amount, decimal.Floor(objdata.Amount))));
                        dr["AMOUNTINCENTS"] = objdata.Amount * 100;
                        if (dr["v_empl_name"] != DBNull.Value)
                        {
                            if (dr["v_empl_name"].ToString().Trim().Length > 0)
                            {
                                dr["v_empl_name"] = dr["v_empl_name"].ToString().Trim().Replace(',', ' ');
                            }
                        }
                        // Get the type of account for this entry
                        //object[] Empbd_param = new object[3];
                        //Empbd_param[0] = objdata.empl_code;
                        //Empbd_param[1] = objdata.bank_code;
                        //Empbd_param[2] = objdata.bank_acct_no;
                        //object type = objDALBaseClass.ExecuteScalar(ref Empbd_param, objdata.GET_TYPEOFACCT);
                        // string typea;
                        if ((dr["ActtType"] == DBNull.Value) || (dr["ActtType"] == String.Empty))
                        {
                            dr["resrv_blank5"] = "S";
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() != "S" && dr["ActtType"].ToString().Trim() != "C")
                        {
                            dr["resrv_blank5"] = "S";
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() == "S")
                        {
                            dr["ACTINFO"] = "SAV";
                        }
                        else if (dr["ActtType"].ToString().Trim() == "C")
                        {
                            dr["ACTINFO"] = "DDA";
                        }
                        //  type = dr["ActtType"].ToString().Trim();
                        //if (type == null || (type.ToString().Trim() != "S" && type.ToString().Trim() != "C"))
                        //{
                        //    dr["resrv_blank5"] = "S";
                        //}

                        //Calculate out total for the cash amount.
                        //objStpcashe.cash_amt += objdata.Amount;
                        dr["v_bank_desc"] = objdata.bank_desc;

                        ////process on after doc_no group..........
                        //bool _postingStatus = false;
                        //if (ds.Tables[0].Rows.Count != i + 1)
                        //{
                        //    if (ds.Tables[0].Rows[i + 1]["v_doc_no"] != DBNull.Value)
                        //        if (objdata.doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["v_doc_no"]))
                        //        {
                        //            _postingStatus = true;
                        //        }
                        //}
                        //else if (ds.Tables[0].Rows.Count == i + 1)
                        //    _postingStatus = true;

                        //if (_postingStatus)
                        //{
                        //    //updates individual rows in stypddre processed in
                        //    //this direct deposit tape.  The rows are marked as used, given a
                        //    //date stamp, and a unique file_id.
                        //    object[] updparameter = new object[3];
                        //    updparameter[0] = Convert.ToDateTime(dr["create_date"]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        //    updparameter[1] = objdata.doc_no;
                        //    updparameter[2] = dr["file_id"].ToString();
                        //    object upsresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updparameter, objdata.UPD_YDDRE, true);
                        //    if (upsresult != null)
                        //    {
                        //        if (upsresult.ToString().Trim() != string.Empty)
                        //        {
                        //            if (Convert.ToInt32(upsresult) != 1)
                        //            {
                        //                doc_err = true;
                        //                ErrorMessage = "Error :An Sql Error has occurred while updating (stypddre)";
                        //            }
                        //        }
                        //    }
                        //}
                        //Last Row...........
                        if (ds.Tables[0].Rows.Count == i + 1)
                        {
                            //if (objSearch.GenCheck.Trim() == "Y")
                            //{
                            //    // Insert into stpcashe ...........
                            //    //if (!InsertIntoStpcashe(ref objStpcashe, ref objTransaction))
                            //    //{
                            //    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            //    //    objDataTable.Rows.Clear();
                            //    //    drn["Problem0"] = "Error : An Sql Error has occurred while inserting into (Stpcashe)";
                            //    //    objDataTable.Rows.Add(drn);
                            //    //    return objDataTable;
                            //    //}
                            //    // Insert into stpcashd ...........
                            //    DVOAPCheckProcessingDetailStpcashd objstpcashd = new DVOAPCheckProcessingDetailStpcashd();
                            //    objstpcashd.doc_no = objStpcashe.doc_no;
                            //    objstpcashd.dist_acct = 956;// objStpcashe.cash_acct;
                            //    objstpcashd.dist_deb_cred = "DB";
                            //    objstpcashd.dist_department = "000";
                            //    objstpcashd.dist_amt = objStpcashe.cash_amt;
                            //    List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                            //    listDVOAPCheckProcessingDetailStpcashd.Add(objstpcashd);
                            //    //if (!InsertIntoStpcashd(ref objstpcashd, ref objTransaction))
                            //    //{
                            //    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            //    //    objDataTable.Rows.Clear();
                            //    //    drn["Problem0"] = "Error :An Sql Error has occurred while inserting into (Stpcashd)";
                            //    //    objDataTable.Rows.Add(drn);
                            //    //    return objDataTable;
                            //    //}

                            //    int NewDocNo = 0;
                            //    int res = 0;
                            //    if (shouldMakeAPEntry)
                            //    {
                            //        res = BLLAPCheckProcessingStpcashe.InsertData(ref objTransaction, ref objStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out NewDocNo, false);
                            //        if (res != 1)
                            //        {
                            //            doc_err = true;
                            //            ErrorMessage = "Error :An Sql Error has occurred while updating (stpcashe)";
                            //        }
                            //    }
                            //    //commit all work........
                            //    if (!doc_err)
                            //    {
                            //        //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                            //    }
                            //    else
                            //    {
                            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            //        objDataTable.Rows.Clear();
                            //        drn["Problem0"] = ErrorMessage;
                            //        objDataTable.Rows.Add(drn);
                            //        return objDataTable;
                            //    }
                            //}
                        }
                        drn.ItemArray = dr.ItemArray;
                        objDataTable.Rows.Add(drn);
                    }
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);

                }
                throw ex;
            }
            return objDataTable;
        }
        public static DataTable GetDirectDepositeDataForMultipleBankCode(ref DVOddmStypddreAndStypddrd objSearch, bool shouldMakeAPEntry, ref System.Collections.ArrayList _arrBankCodes, int apBatchID, ref DVOPYBatchProcessStybatchr pObjBatch)
        {
            /*
            Added by Sarvjeet on 22/10/2010
            To implemented Payroll batch process into Direct Deposit Media. Nedd to add
            A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'GetDirectDepositeDataForMultipleBankCode' function
            and remove comment from the code written for batch process logic.      
           */
            DataSet ds = null;
            string ErrorMessage = string.Empty;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DVOAPCheckProcessingStpcashe objStpcashe = new DVOAPCheckProcessingStpcashe();
            List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();
            DataTable objDataTable = new DataTable();
            DataTable dtMain = new DataTable();
            bool doc_err = false;
            object[] parametres = new object[3];
            //Added by Sarvjeet on 22/01/2010..
            #region Declare Variables for Batch Process
            StringBuilder errorMassage = new StringBuilder();
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            int recordsSearched = 0;
            int recordsProcessed = 0;
            bool IsProessIns = false;
            #endregion
            try
            {
                //Added by Sarvjeet on 22/01/2010..
                #region Insert Process Start Info..

                object objTrx = null;
                objProcessDtl.pybatchid = pObjBatch.pybatchid;
                objProcessDtl.processname = "Direct Deposit Media";
                objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
                BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
                IsProessIns = true;

                #endregion
                //get payroll default configuration 
                objstycntrcList = BLLUpdPayDefault.GetAllPayrollDefaults();

                foreach (string strBankCode in _arrBankCodes)
                {

                    //get direct deposit media data
                    parametres[0] = strBankCode;//objSearch.BanckCode;
                    parametres[1] = objSearch.Cash_acct_no;
                    parametres[2] = objSearch.GenCheck;
                    ds = objDALBaseClass.GetData(ref parametres, typeof(DVOddmStypddreAndStypddrd), objSearch.GET_DDMData);
                    //for (int i = 10; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    ds.Tables[0].Rows.RemoveAt(i);
                    //    i--;
                    //}
                    recordsSearched =+ ds.Tables[0].Rows.Count;
                    ds.Tables[0].Columns[0].ColumnName = "v_bank_desc";
                    ds.Tables[0].Columns[1].ColumnName = "v_amount";
                    ds.Tables[0].Columns[2].ColumnName = "v_bank_acct_no";
                    ds.Tables[0].Columns[3].ColumnName = "v_chk_digit";
                    ds.Tables[0].Columns[4].ColumnName = "v_dfi_dest";
                    ds.Tables[0].Columns[5].ColumnName = "v_empl_code";
                    ds.Tables[0].Columns[6].ColumnName = "v_empl_name";
                    ds.Tables[0].Columns[7].ColumnName = "v_pay_date";
                    ds.Tables[0].Columns[8].ColumnName = "v_trace_number";
                    ds.Tables[0].Columns[9].ColumnName = "v_trans_code";
                    ds.Tables[0].Columns[10].ColumnName = "v_bank_code";
                    ds.Tables[0].Columns[11].ColumnName = "v_batch_date";
                    ds.Tables[0].Columns[12].ColumnName = "v_batch_no";
                    ds.Tables[0].Columns[13].ColumnName = "v_dfi_immed";
                    ds.Tables[0].Columns[14].ColumnName = "v_doc_no";
                    ds.Tables[0].Columns[15].ColumnName = "v_entry_desc";
                    ds.Tables[0].Columns[16].ColumnName = "v_file_id";
                    ds.Tables[0].Columns[17].ColumnName = "v_svc_class";
                    ds.Tables[0].Columns[18].ColumnName = "v_used";
                    ds.Tables[0].Columns[19].ColumnName = "v_suppliercode";
                    ds.Tables[0].Columns[20].ColumnName = "v_company_name";
                    ds.Tables[0].Columns.Add("priority_code");
                    ds.Tables[0].Columns.Add("file_hash");
                    ds.Tables[0].Columns.Add("create_date");
                    ds.Tables[0].Columns.Add("resrv_blank5");
                    ds.Tables[0].Columns.Add("ein_number");
                    ds.Tables[0].Columns.Add("immed_dest_dfi");
                    ds.Tables[0].Columns.Add("immed_chk_digit");
                    ds.Tables[0].Columns.Add("immed_dest_name");
                    ds.Tables[0].Columns.Add("file_id");
                    ds.Tables[0].Columns.Add("Problem0");
                    ds.Tables[0].Columns.Add("recordsize");
                    ds.Tables[0].Columns.Add("blockfactor");
                    ds.Tables[0].Columns.Add("formatcode");
                    ds.Tables[0].Columns.Add("entryclass");
                    ds.Tables[0].Columns.Add("statuscode");
                    objDataTable = ds.Tables[0].Clone();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        if (objSearch.GenCheck.Trim() == "Y")
                        {
                            objStpcashe.chk_date = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            //objStpcashe.doc_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "ap_doc_no", ref objTransaction);
                            objStpcashe.vend_code = ds.Tables[0].Rows[0]["v_suppliercode"].ToString().Trim();
                            objStpcashe.doc_desc = "PAYROLL DIR.DEP. COVERAGE";// +objStpcashe.chk_date.ToString();
                            objStpcashe.pay_to_code = "PAYTO";
                            objStpcashe.cash_amt = 0;
                            objStpcashe.cash_acct = objSearch.Cash_acct_no;
                            objStpcashe.cash_department = "000";
                            objStpcashe.cash_deb_cred = "CR";
                            objStpcashe.print_chk = "Y";
                            objStpcashe.ok_to_post = "N";
                            objStpcashe.chk_printed = "N";
                            objStpcashe.ap_type = "N";
                            objStpcashe.bus_name = ds.Tables[0].Rows[0]["v_bank_desc"].ToString().Trim();
                            objStpcashe.current_approval = -1; // Makes it approved and ready for processing
                            objStpcashe.batch_id = apBatchID;
                        }

                        //get field_id.........


                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            DataRow drn = objDataTable.NewRow();
                            DVOddmStypddreAndStypddrd objdata = new DVOddmStypddreAndStypddrd();

                            objdata.doc_no = (dr["v_doc_no"] != DBNull.Value ? Convert.ToInt32(dr["v_doc_no"]) : 0);
                            objdata.Amount = (dr["v_amount"] != DBNull.Value ? Convert.ToDecimal(dr["v_amount"]) : 0);
                            objdata.empl_code = (dr["v_empl_code"] != DBNull.Value ? Convert.ToString(dr["v_empl_code"]) : string.Empty);
                            objdata.bank_code = (dr["v_bank_code"] != DBNull.Value ? Convert.ToString(dr["v_bank_code"]) : string.Empty);
                            objdata.bank_acct_no = (dr["v_bank_acct_no"] != DBNull.Value ? Convert.ToString(dr["v_bank_acct_no"]) : string.Empty);
                            objdata.bank_desc = (dr["v_bank_desc"] != DBNull.Value ? Convert.ToString(dr["v_bank_desc"]) : string.Empty);
                            dr["ein_number"] = objstycntrcList[0].ein_number;
                            dr["immed_dest_dfi"] = objstycntrcList[0].immed_dest_dfi;
                            dr["immed_chk_digit"] = objstycntrcList[0].immed_chk_digit;
                            dr["immed_dest_name"] = objstycntrcList[0].immed_dest_name;

                            dr["recordsize"] = "94";
                            dr["blockfactor"] = "10";
                            dr["formatcode"] = "1";
                            dr["entryclass"] = "PPD";
                            dr["statuscode"] = "1";

                            string ein_nu = dr["ein_number"].ToString();
                            if (ein_nu.Trim().Length >= 10)
                            {
                                dr["ein_number"] = "1" + "," + ein_nu.Substring(1, 2) + "," + ein_nu.Substring(4, 10);
                            }
                            dr["priority_code"] = "01";
                            dr["create_date"] = DVOApplicationUserInfo.CurrentDate;
                            dr["file_id"] = GetField_id();

                            // remove decimal point
                            dr["v_amount"] = objdata.Amount * 100;
                            // Get the type of account for this entry
                            object[] Empbd_param = new object[3];
                            Empbd_param[0] = objdata.empl_code;
                            Empbd_param[1] = objdata.bank_code;
                            Empbd_param[2] = objdata.bank_acct_no;
                            object type = objDALBaseClass.ExecuteScalar(ref Empbd_param, objdata.GET_TYPEOFACCT);
                            if (type == null || (type.ToString().Trim() != "S" && type.ToString().Trim() != "C"))
                            {
                                dr["resrv_blank5"] = "S";
                            }

                            //Calculate out total for the cash amount.
                            objStpcashe.cash_amt += objdata.Amount;
                            dr["v_bank_desc"] = objdata.bank_desc;

                            //process on after doc_no group..........
                            bool _postingStatus = false;
                            if (ds.Tables[0].Rows.Count != i + 1)
                            {
                                if (ds.Tables[0].Rows[i + 1]["v_doc_no"] != DBNull.Value)
                                    if (objdata.doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["v_doc_no"]))
                                    {
                                        _postingStatus = true;
                                    }
                            }
                            else if (ds.Tables[0].Rows.Count == i + 1)
                                _postingStatus = true;

                            if (_postingStatus)
                            {
                                //updates individual rows in stypddre processed in
                                //this direct deposit tape.  The rows are marked as used, given a
                                //date stamp, and a unique file_id.
                                object[] updparameter = new object[3];
                                updparameter[0] = Convert.ToDateTime(dr["create_date"]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                updparameter[1] = objdata.doc_no;
                                updparameter[2] = dr["file_id"].ToString();
                                object upsresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updparameter, objdata.UPD_YDDRE, true);
                                if (upsresult != null)
                                {
                                    if (upsresult.ToString().Trim() != string.Empty)
                                    {
                                        if (Convert.ToInt32(upsresult) != 1)
                                        {
                                            doc_err = true;
                                            ErrorMessage = "Error :An Sql Error has occurred while updating (stypddre)";
                                        }
                                    }
                                }
                            }
                            //Last Row...........
                            if (ds.Tables[0].Rows.Count == i + 1)
                            {
                                if (objSearch.GenCheck.Trim() == "Y")
                                {
                                    // Insert into stpcashe ...........
                                    //if (!InsertIntoStpcashe(ref objStpcashe, ref objTransaction))
                                    //{
                                    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    //    objDataTable.Rows.Clear();
                                    //    drn["Problem0"] = "Error : An Sql Error has occurred while inserting into (Stpcashe)";
                                    //    objDataTable.Rows.Add(drn);
                                    //    return objDataTable;
                                    //}
                                    // Insert into stpcashd ...........
                                    DVOAPCheckProcessingDetailStpcashd objstpcashd = new DVOAPCheckProcessingDetailStpcashd();
                                    objstpcashd.doc_no = objStpcashe.doc_no;
                                    objstpcashd.dist_acct = objStpcashe.cash_acct;
                                    objstpcashd.dist_deb_cred = "DB";
                                    objstpcashd.dist_department = "000";
                                    objstpcashd.dist_amt = objStpcashe.cash_amt;
                                    List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd = new List<DVOAPCheckProcessingDetailStpcashd>();
                                    listDVOAPCheckProcessingDetailStpcashd.Add(objstpcashd);
                                    //if (!InsertIntoStpcashd(ref objstpcashd, ref objTransaction))
                                    //{
                                    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    //    objDataTable.Rows.Clear();
                                    //    drn["Problem0"] = "Error :An Sql Error has occurred while inserting into (Stpcashd)";
                                    //    objDataTable.Rows.Add(drn);
                                    //    return objDataTable;
                                    //}

                                    int NewDocNo = 0;
                                    int res = 0;
                                    if (shouldMakeAPEntry)
                                    {
                                     //   res = BLLAPCheckProcessingStpcashe.InsertData(ref objTransaction, ref objStpcashe, ref listDVOAPCheckProcessingDetailStpcashd, out NewDocNo, false);
                                        if (res != 1)
                                        {
                                            doc_err = true;
                                            ErrorMessage = "Error :An Sql Error has occurred while updating (stpcashe)";
                                        }
                                    }
                                    //commit all work........
                                    if (!doc_err)
                                    {
                                        //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                    }
                                    else
                                    {

                                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                        objDataTable.Rows.Clear();
                                        errorMassage.Append("[" + ErrorMessage+ "]");
                                        drn["Problem0"] = ErrorMessage;
                                        objDataTable.Rows.Add(drn);
                                        return objDataTable;
                                    }
                                }
                            }
                            drn.ItemArray = dr.ItemArray;
                            objDataTable.Rows.Add(drn);
                            //recordsProcessed++;
                        }

                    }
                    //else
                    //{
                    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //}
                    if (dtMain.Columns.Count <= 0)
                        dtMain = objDataTable.Clone();
                    dtMain.Merge(objDataTable);
                }
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                recordsProcessed = recordsSearched;
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                errorMassage.Append("[" + ex.Message + "]");
                throw ex;
            }
            finally
            {
                //Added by Sarvjeet on 22/01/2010..
                #region Record process detail..
                if (IsProessIns)
                {
                    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
                    object objTrx = null;
                    objProcessDtl.recordssearched = recordsSearched;
                    objProcessDtl.recordsprocessed = recordsProcessed;
                    objProcessDtl.status = 1;
                    objProcessDtl.errormessage = errorMassage.ToString();
                    objList.Add(objProcessDtl);
                    BLLPYBatchProcessDetailStybatchd.UpdateData(ref objTrx, ref objList);
                }
                else
                {
                    object objTrx = null;
                    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
                    DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
                    obj.pybatchid = pObjBatch.pybatchid;
                    obj.processname = "Direct Deposit Media";
                    //obj.processstartedon = pObjBatch.startedon;
                    //obj.processendedon = pObjBatch.endedon;
                    obj.recordssearched = recordsSearched;
                    obj.recordsprocessed = recordsProcessed;
                    obj.status = 1;
                    obj.searchcriteria = pObjBatch.searchcriteria;
                    obj.errormessage = errorMassage.ToString();
                    objList.Add(obj);
                    BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);
                }
                #endregion
            }
            return dtMain;
        }
        public static List<string> GetBanckCodeList()
        {
            List<string> objList = new List<string>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDALBaseClass.GetAllData(typeof(DVOddmStypddreAndStypddrd)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string str = string.Empty;
                    str = (dr[0] != null ? dr[0].ToString() : string.Empty);
                    if (str.Trim() != string.Empty)
                    {
                        objList.Add(str.Trim());
                    }
                }

            }
            return objList;
        }

        public static bool InsertIntoStpcashe(ref DVOAPCheckProcessingStpcashe objStpcashe, ref object objTrx)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[14];
            try
            {
                parameters[0] = Convert.ToDateTime(objStpcashe.chk_date);
                parameters[1] = objStpcashe.doc_no;
                parameters[2] = objStpcashe.vend_code;
                parameters[3] = objStpcashe.doc_desc;
                parameters[4] = objStpcashe.cash_amt;
                parameters[5] = objStpcashe.cash_acct;
                parameters[6] = objStpcashe.cash_department;
                parameters[7] = objStpcashe.cash_deb_cred;
                parameters[8] = objStpcashe.print_chk;
                parameters[9] = objStpcashe.ok_to_post;
                parameters[10] = objStpcashe.chk_printed;
                parameters[11] = objStpcashe.ap_type;
                parameters[12] = objStpcashe.bus_name;
                parameters[13] = objStpcashe.current_approval;
                object result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objStpcashe.INS_CASHE_DDM, true);
                if (result != null)
                {
                    if (result.ToString().Trim() != string.Empty)
                    {
                        if (Convert.ToInt32(result) != 1)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
            return true;

        }
        public static bool InsertIntoStpcashd(ref DVOAPCheckProcessingDetailStpcashd objStpcashd, ref object objTrx)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[5];
            try
            {
                parameters[0] = objStpcashd.doc_no;
                parameters[1] = objStpcashd.dist_acct;
                parameters[2] = objStpcashd.dist_deb_cred;
                parameters[3] = objStpcashd.dist_department;
                parameters[4] = objStpcashd.dist_amt;
                object result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objStpcashd.INS_CASHD_DDM, true);
                if (result != null)
                {
                    if (result.ToString().Trim() != string.Empty)
                    {
                        if (Convert.ToInt32(result) != 1)
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
            return true;
        }

        public static string GetField_id()
        {
            string rptfield_id = string.Empty;
            object o_field_id = null; // holds result of sel on max(file_id)
            string field_id = string.Empty; //stores possible file_id's
            field_id = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameter = new object[1];
            parameter[0] = DVOApplicationUserInfo.CurrentDate;
            o_field_id = objDALBaseClass.ExecuteScalar(ref parameter, (new DVOddmStypddreAndStypddrd()).GET_FIELD_ID);
            if (o_field_id == null || o_field_id.ToString().Trim() == string.Empty)
            {
                rptfield_id = "0";
            }
            else
            {
                for (int n = 0; n < 62; n++)
                {
                    if (o_field_id.ToString().Trim() == field_id.Substring(n, 1))
                    {
                        if (n != 61)//In case of z
                            rptfield_id = field_id.Substring(n + 1, 1);
                        else
                            rptfield_id = "0";
                        break;
                    }
                }
            }
            return rptfield_id;
        }

    }
}
