using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using JKPS.CommonUtilities;

namespace JKPS.BLL
{
  public  class BLLPrintChk
    {
        public static DataTable PrintNonAPChecks(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objSearch, out string Err_msg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //bool statusObjTransaction = true;
            //if (objTransaction == null)
            //{
            //    objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            //    statusObjTransaction = false;
            //}
            //object objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();

            DataTable objDataTable = new DataTable();
            //DataSet ds;
            Err_msg = string.Empty;
            //bool newCheckNumberUsed = true;
            //int newCheckNo = 0;
            try
            {
                #region Old Code

                //if (objSearch.ap_type.Trim() == "N")
                //{
                //    Object[] Parameter = new object[2];
                //    Parameter[0] = objSearch.batch_id;
                //    Parameter[1] = objSearch.cash_acct;
                //    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_NONAPCHECKPRINT);
                //    if (ds != null)
                //        if (ds.Tables.Count > 0)
                //            if (ds.Tables[0].Rows.Count > 0)
                //            {
                //                //SET COLUMN NAMES
                //                ds.Tables[0].Columns[0].ColumnName = "dist_deb_cred";
                //                ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                //                ds.Tables[0].Columns[2].ColumnName = "due_date";
                //                ds.Tables[0].Columns[3].ColumnName = "dist_acct";
                //                ds.Tables[0].Columns[4].ColumnName = "inv_doc_no";
                //                ds.Tables[0].Columns[5].ColumnName = "oa_amt";
                //                ds.Tables[0].Columns[6].ColumnName = "oa_deb_cred";
                //                ds.Tables[0].Columns[7].ColumnName = "ap_type";
                //                ds.Tables[0].Columns[8].ColumnName = "doc_desc";
                //                ds.Tables[0].Columns[9].ColumnName = "doc_no";
                //                ds.Tables[0].Columns[10].ColumnName = "check_no";
                //                ds.Tables[0].Columns[11].ColumnName = "chk_date";
                //                ds.Tables[0].Columns[12].ColumnName = "cash_acct";
                //                ds.Tables[0].Columns[13].ColumnName = "cash_department";
                //                ds.Tables[0].Columns[14].ColumnName = "city";
                //                ds.Tables[0].Columns[15].ColumnName = "state";
                //                ds.Tables[0].Columns[16].ColumnName = "zip";
                //                ds.Tables[0].Columns[17].ColumnName = "country";
                //                ds.Tables[0].Columns[18].ColumnName = "address2";
                //                ds.Tables[0].Columns[19].ColumnName = "bus_name";
                //                ds.Tables[0].Columns[20].ColumnName = "vend_code";
                //                ds.Tables[0].Columns[21].ColumnName = "dist_amt";
                //                ds.Tables[0].Columns[22].ColumnName = "keyvalue";
                //                ds.Tables[0].Columns[23].ColumnName = "acct_desc";
                //                ds.Tables[0].Columns[24].ColumnName = "cash_amt";
                //                objDataTable = ds.Tables[0].Clone();
                //            }
                //    //else
                //    //{
                //    //    if (objTransaction!=null)
                //    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //    //}
                //}
                //else
                //{
                //    Object[] Parameter = new object[2];
                //    Parameter[0] = objSearch.batch_id;
                //    Parameter[1] = objSearch.cash_acct;
                //    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_APCHECKPRINT);
                //    if (ds != null)
                //        if (ds.Tables.Count > 0)
                //            if (ds.Tables[0].Rows.Count > 0)
                //            {
                //                //SET COLUMN NAMES
                //                ds.Tables[0].Columns[0].ColumnName = "disc_amt";
                //                ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                //                ds.Tables[0].Columns[2].ColumnName = "dist_acct";
                //                ds.Tables[0].Columns[3].ColumnName = "dist_amt";
                //                ds.Tables[0].Columns[4].ColumnName = "dist_deb_cred";
                //                ds.Tables[0].Columns[5].ColumnName = "due_date";
                //                ds.Tables[0].Columns[6].ColumnName = "inv_doc_no";
                //                ds.Tables[0].Columns[7].ColumnName = "inv_no";
                //                ds.Tables[0].Columns[8].ColumnName = "ap_type";
                //                ds.Tables[0].Columns[9].ColumnName = "bus_name";
                //                ds.Tables[0].Columns[10].ColumnName = "cash_acct";
                //                ds.Tables[0].Columns[11].ColumnName = "cash_amt";
                //                ds.Tables[0].Columns[12].ColumnName = "cash_department";
                //                ds.Tables[0].Columns[13].ColumnName = "check_no";
                //                ds.Tables[0].Columns[14].ColumnName = "chk_date";
                //                ds.Tables[0].Columns[15].ColumnName = "doc_desc";
                //                ds.Tables[0].Columns[16].ColumnName = "doc_no";
                //                ds.Tables[0].Columns[17].ColumnName = "oa_amt";
                //                ds.Tables[0].Columns[18].ColumnName = "oa_deb_cred";
                //                ds.Tables[0].Columns[19].ColumnName = "vend_code";
                //                ds.Tables[0].Columns[20].ColumnName = "inv_date";
                //                ds.Tables[0].Columns[21].ColumnName = "orig_amount";
                //                ds.Tables[0].Columns[22].ColumnName = "address1";
                //                ds.Tables[0].Columns[23].ColumnName = "address2";
                //                ds.Tables[0].Columns[24].ColumnName = "city";
                //                ds.Tables[0].Columns[25].ColumnName = "country";
                //                ds.Tables[0].Columns[26].ColumnName = "pay_to_name";
                //                ds.Tables[0].Columns[27].ColumnName = "state";
                //                ds.Tables[0].Columns[28].ColumnName = "zip";
                //                ds.Tables[0].Columns[29].ColumnName = "acct_desc";
                //                ds.Tables[0].Columns[30].ColumnName = "keyvalue";

                //                objDataTable = ds.Tables[0].Clone();
                //            }
                //    //else
                //    //{
                //    //    if (objTransaction != null)
                //    //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //    //}
                //}



                //if (ds != null)
                //    if (ds.Tables.Count > 0)
                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {
                //            int doc_no = 0;
                //            DataRow dr = ds.Tables[0].Rows[i];

                //            if (ds.Tables[0].Columns.Contains("pay_to_name") && ds.Tables[0].Columns.Contains("bus_name"))
                //                if (dr["pay_to_name"] == DBNull.Value || dr["pay_to_name"] != null || dr["pay_to_name"].ToString().Trim() == string.Empty)
                //                    dr["pay_to_name"] = dr["bus_name"].ToString().Trim();

                //            if (dr["doc_no"] != DBNull.Value)
                //            {
                //                doc_no = Convert.ToInt32(dr["doc_no"]);
                //            }
                //            bool doc_no_change_status = false;
                //            if (i != 0) 
                //            {
                //                if (doc_no != Convert.ToInt32(ds.Tables[0].Rows[i - 1]["doc_no"]))
                //                    doc_no_change_status = true;
                //            }
                //            else if (i == 0)
                //                doc_no_change_status = true;
                //            if (doc_no_change_status)                            
                //            {     
                //                object[] parameters = new object[2];
                //                parameters[0] = doc_no;
                //                 // if check number is null in stpcashe then get last_chkno 
                //                //increment by one and update stpcntrc by new check number. 
                //                //Generate Check Number.....
                //                if (newCheckNumberUsed)
                //                {
                //                    newCheckNo = 0;
                //                    object nullTransactionObject = null;
                //                    if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                //                        //newCheckNo = BLLAccountingLiberary.Auto_Next("stpcntrc", "last_chkno", ref nullTransactionObject);
                //                        //newCheckNo = BLLAccountingLiberary.Auto_Next_With_LockAndSelect("stpcntrc", "last_chkno", ref nullTransactionObject);
                //                        newCheckNo = BLLAccountingLiberary.Auto_Next_APCheckNo(ref nullTransactionObject);
                //                    else newCheckNo = Convert.ToInt32(dr["check_no"]);
                //                }
                //                //************* Added By Bharat Dhall[09/29/2009] *************
                //                if (newCheckNo <= 0) continue;
                //                //************************************************************
                //                parameters[1] = newCheckNo;
                //                //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                //                DataSet dschk = objDalBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, (new DvoCheckListing()).FIND_SPNAME);
                //                if (dschk != null && dschk.Tables.Count > 0 && dschk.Tables[0].Rows.Count > 0
                //                    && dschk.Tables[0].Rows[0][0] != DBNull.Value && dschk.Tables[0].Rows[0][0] != null
                //                    && dschk.Tables[0].Rows[0][0].ToString().Trim().Length > 0
                //                    && Convert.ToInt32(dschk.Tables[0].Rows[0][0]) == 1)
                //                {
                //                    //if (Convert.ToString(dschk.Tables[0].Rows[0][1]).Trim() == "Y")
                //                    //{
                //                    int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref objTransaction, "stpcashe", doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                //                    if (LockStatus != 1)
                //                    {
                //                        ///////////////////////////////////////////
                //                        ////////////////////////////////////////////
                //                    }
                //                    else
                //                    {
                //                        dr["check_no"] = dschk.Tables[0].Rows[0][2];
                //                        DataRow[] rows = ds.Tables[0].Select("doc_no = " + doc_no);
                //                        for (int j = 0; j < rows.Length; j++)
                //                        {
                //                            DataRow drn = objDataTable.NewRow();
                //                            drn.ItemArray = rows[j].ItemArray;
                //                            drn["check_no"] = dschk.Tables[0].Rows[0][2];
                //                            if (dschk.Tables[0].Rows[0][2] != DBNull.Value && dschk.Tables[0].Rows[0][2] != null && dschk.Tables[0].Rows[0][2].ToString().Trim().Length > 0 && Convert.ToInt32(dschk.Tables[0].Rows[0][2]) == newCheckNo)
                //                                newCheckNumberUsed = true;
                //                            else
                //                                newCheckNumberUsed = false;
                //                            objDataTable.Rows.Add(drn);
                //                        }
                //                    }
                //                    //}
                //                }
                //                else
                //                {
                //                    //for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                //                    //    if (objTransaction != null)
                //                    //        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                //                    if (!statusObjTransaction && objTransaction != null)
                //                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                //                    //ExceptionManagement.ExceptionManager.Publish(new Exception(dschk.Tables[0].Rows[0][0].ToString()));
                //                    //Err_msg = " Some Checks will not show , Problem has occurred while Generating Check Number.)";
                //                    return objDataTable;
                //                }
                //            }
                //        }
                //if (!statusObjTransaction && objTransaction != null)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);

                #endregion Old Code

                object[] parameters = new object[5];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                parameters[2] = objSearch.ap_type.Trim();
                parameters[3] = objSearch.batch_id;
                parameters[4] = objSearch.cash_acct;

                DataSet ds = objDalBaseClass.GetData(ref parameters, objSearch.GetType(), objSearch.GETNONAPCheck);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ds.Tables[0].Columns[0].ColumnName = "disc_amt";
                    ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                    ds.Tables[0].Columns[2].ColumnName = "dist_acct";
                    ds.Tables[0].Columns[3].ColumnName = "dist_amt";
                    ds.Tables[0].Columns[4].ColumnName = "dist_deb_cred";
                    ds.Tables[0].Columns[5].ColumnName = "due_date";
                    ds.Tables[0].Columns[6].ColumnName = "inv_doc_no";
                    ds.Tables[0].Columns[7].ColumnName = "inv_no";
                    ds.Tables[0].Columns[8].ColumnName = "ap_type";
                    ds.Tables[0].Columns[9].ColumnName = "bus_name";
                    ds.Tables[0].Columns[10].ColumnName = "cash_acct";
                    ds.Tables[0].Columns[11].ColumnName = "cash_amt";
                    ds.Tables[0].Columns[12].ColumnName = "cash_department";
                    ds.Tables[0].Columns[13].ColumnName = "check_no";
                    ds.Tables[0].Columns[14].ColumnName = "chk_date";
                    ds.Tables[0].Columns[15].ColumnName = "doc_desc";
                    ds.Tables[0].Columns[16].ColumnName = "doc_no";
                    ds.Tables[0].Columns[17].ColumnName = "oa_amt";
                    ds.Tables[0].Columns[18].ColumnName = "oa_deb_cred";
                    ds.Tables[0].Columns[19].ColumnName = "vend_code";
                    ds.Tables[0].Columns[20].ColumnName = "inv_date";
                    ds.Tables[0].Columns[21].ColumnName = "orig_amount";
                    ds.Tables[0].Columns[22].ColumnName = "address1";
                    ds.Tables[0].Columns[23].ColumnName = "address2";
                    ds.Tables[0].Columns[24].ColumnName = "city";
                    ds.Tables[0].Columns[25].ColumnName = "country";
                    ds.Tables[0].Columns[26].ColumnName = "pay_to_name";
                    ds.Tables[0].Columns[27].ColumnName = "state";
                    ds.Tables[0].Columns[28].ColumnName = "zip";
                    ds.Tables[0].Columns[29].ColumnName = "acct_desc";
                    ds.Tables[0].Columns[30].ColumnName = "keyvalue";

                    objDataTable = ds.Tables[0].Copy();

                    if (objSearch.ap_type.Trim() == "N")
                    {
                        objDataTable.Columns.Remove("disc_amt");
                        objDataTable.Columns.Remove("inv_no");
                        objDataTable.Columns.Remove("inv_date");
                        objDataTable.Columns.Remove("orig_amount");
                        objDataTable.Columns.Remove("address1");
                        objDataTable.Columns.Remove("pay_to_name");
                    }
                }
                //if (!statusObjTransaction && objTransaction != null)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                //if (!statusObjTransaction && objTransaction != null)
                //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                throw ex;
            }
            Err_msg = "";

            //remove all rows from table, where checkno is '0' or blank or null
            foreach (DataRow dr in objDataTable.Rows)
            {
                if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length <= 0 || dr["check_no"].ToString().Trim() == "0")
                    objDataTable.Rows.Remove(dr);
            }
            objDataTable.AcceptChanges();
            return objDataTable;
        }
        //Added by Sunil Pahwa on for UnPrinted checks 28/07/09
        public static DataTable ShowUnprintedChecks(ref DVOAPCheckProcessingStpcashe objSearch, out string Err_msg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();

            DataTable objDataTable = new DataTable();
            DataSet ds;
            Err_msg = string.Empty;
            try
            {
                if (objSearch.ap_type.Trim() == "N")
                {
                    Object[] Parameter = new object[2];
                    Parameter[0] = objSearch.batch_id;
                    Parameter[1] = objSearch.keyvalue;
                    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_UNP_NONAPCHECKPRINT);
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //for (int i = 10; i < ds.Tables[0].Rows.Count; i++)
                                //    ds.Tables[0].Rows[i].Delete();
                                //ds.AcceptChanges();

                                //SET COLUMN NAMES
                                ds.Tables[0].Columns[0].ColumnName = "dist_deb_cred";
                                ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                                ds.Tables[0].Columns[2].ColumnName = "due_date";
                                ds.Tables[0].Columns[3].ColumnName = "dist_acct";
                                ds.Tables[0].Columns[4].ColumnName = "inv_doc_no";
                                ds.Tables[0].Columns[5].ColumnName = "oa_amt";
                                ds.Tables[0].Columns[6].ColumnName = "oa_deb_cred";
                                ds.Tables[0].Columns[7].ColumnName = "ap_type";
                                ds.Tables[0].Columns[8].ColumnName = "doc_desc";
                                ds.Tables[0].Columns[9].ColumnName = "doc_no";
                                ds.Tables[0].Columns[10].ColumnName = "check_no";
                                ds.Tables[0].Columns[11].ColumnName = "chk_date";
                                ds.Tables[0].Columns[12].ColumnName = "cash_acct";
                                ds.Tables[0].Columns[13].ColumnName = "cash_department";
                                ds.Tables[0].Columns[14].ColumnName = "city";
                                ds.Tables[0].Columns[15].ColumnName = "state";
                                ds.Tables[0].Columns[16].ColumnName = "zip";
                                ds.Tables[0].Columns[17].ColumnName = "country";
                                ds.Tables[0].Columns[18].ColumnName = "address2";
                                ds.Tables[0].Columns[19].ColumnName = "bus_name";
                                ds.Tables[0].Columns[20].ColumnName = "vend_code";
                                ds.Tables[0].Columns[21].ColumnName = "dist_amt";
                                ds.Tables[0].Columns[22].ColumnName = "keyvalue";
                                ds.Tables[0].Columns[23].ColumnName = "acct_desc";
                                ds.Tables[0].Columns[24].ColumnName = "cash_amt";
                                ds.Tables[0].Columns[25].ColumnName = "accounttypeid";
                                objDataTable = ds.Tables[0].Clone();
                            }
                    //else
                    //{
                    //    if (objTransaction!=null)
                    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //}
                }
                else
                {
                    Object[] Parameter = new object[2];
                    Parameter[0] = objSearch.batch_id;
                    Parameter[1] = objSearch.keyvalue;
                    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_UNP_APCHECKPRINT);
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //SET COLUMN NAMES
                                ds.Tables[0].Columns[0].ColumnName = "disc_amt";
                                ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                                ds.Tables[0].Columns[2].ColumnName = "dist_acct";
                                ds.Tables[0].Columns[3].ColumnName = "dist_amt";
                                ds.Tables[0].Columns[4].ColumnName = "dist_deb_cred";
                                ds.Tables[0].Columns[5].ColumnName = "due_date";
                                ds.Tables[0].Columns[6].ColumnName = "inv_doc_no";
                                ds.Tables[0].Columns[7].ColumnName = "inv_no";
                                ds.Tables[0].Columns[8].ColumnName = "ap_type";
                                ds.Tables[0].Columns[9].ColumnName = "bus_name";
                                ds.Tables[0].Columns[10].ColumnName = "cash_acct";
                                ds.Tables[0].Columns[11].ColumnName = "cash_amt";
                                ds.Tables[0].Columns[12].ColumnName = "cash_department";
                                ds.Tables[0].Columns[13].ColumnName = "check_no";
                                ds.Tables[0].Columns[14].ColumnName = "chk_date";
                                ds.Tables[0].Columns[15].ColumnName = "doc_desc";
                                ds.Tables[0].Columns[16].ColumnName = "doc_no";
                                ds.Tables[0].Columns[17].ColumnName = "oa_amt";
                                ds.Tables[0].Columns[18].ColumnName = "oa_deb_cred";
                                ds.Tables[0].Columns[19].ColumnName = "vend_code";
                                ds.Tables[0].Columns[20].ColumnName = "inv_date";
                                ds.Tables[0].Columns[21].ColumnName = "orig_amount";
                                ds.Tables[0].Columns[22].ColumnName = "address1";
                                ds.Tables[0].Columns[23].ColumnName = "address2";
                                ds.Tables[0].Columns[24].ColumnName = "city";
                                ds.Tables[0].Columns[25].ColumnName = "country";
                                ds.Tables[0].Columns[26].ColumnName = "pay_to_name";
                                ds.Tables[0].Columns[27].ColumnName = "state";
                                ds.Tables[0].Columns[28].ColumnName = "zip";
                                ds.Tables[0].Columns[29].ColumnName = "acct_desc";
                                ds.Tables[0].Columns[30].ColumnName = "keyvalue";
                                ds.Tables[0].Columns[31].ColumnName = "accounttypeid";
                                objDataTable = ds.Tables[0].Clone();
                            }
                    //else
                    //{
                    //    if (objTransaction != null)
                    //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //}
                }

                if (ds != null)
                    if (ds.Tables.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int doc_no = 0;
                            DataRow dr = ds.Tables[0].Rows[i];

                            if (ds.Tables[0].Columns.Contains("pay_to_name") && ds.Tables[0].Columns.Contains("bus_name"))
                                if (dr["pay_to_name"] == DBNull.Value || dr["pay_to_name"] != null || dr["pay_to_name"].ToString().Trim() == string.Empty)
                                    dr["pay_to_name"] = dr["bus_name"].ToString().Trim();

                            if (dr["doc_no"] != DBNull.Value)
                            {
                                doc_no = Convert.ToInt32(dr["doc_no"]);
                            }
                            bool doc_no_change_status = false;
                            if (i != 0)
                            {
                                if (doc_no != Convert.ToInt32(ds.Tables[0].Rows[i - 1]["doc_no"]))
                                {
                                    doc_no_change_status = true;
                                }
                            }
                            else if (i == 0)
                                doc_no_change_status = true;
                            if (doc_no_change_status)
                            {      // if check number is null in stpcashe then get last_chkno 
                                //increment by one and update stpcntrc by new check number. 
                                //Generate Check Number.....
                                object[] parameters = new object[1];
                                parameters[0] = doc_no;
                                //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                                DataSet dschk = objDalBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, (new DvoCheckListing()).FIND_SPNAME);
                                if (Convert.ToInt32(dschk.Tables[0].Rows[0][0]) == 1)
                                {
                                    ////if (Convert.ToString(dschk.Tables[0].Rows[0][1]).Trim() == "Y")
                                    ////{
                                    //int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref objTransaction, "stpcashe", doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false);
                                    //if (LockStatus != 1)
                                    //{
                                    //    ///////////////////////////////////////////
                                    //    ////////////////////////////////////////////
                                    //}
                                    //else
                                    //{
                                    dr["check_no"] = dschk.Tables[0].Rows[0][2];
                                    DataRow[] rows = ds.Tables[0].Select("doc_no = " + doc_no);
                                    for (int j = 0; j < rows.Length; j++)
                                    {
                                        DataRow drn = objDataTable.NewRow();
                                        drn.ItemArray = rows[j].ItemArray;
                                        drn["check_no"] = dschk.Tables[0].Rows[0][2];
                                        objDataTable.Rows.Add(drn);
                                    }
                                    //}
                                    //}
                                }
                                else
                                {
                                    //if (!statusObjTransaction)
                                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                                        if (objTransaction != null)
                                            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                    ExceptionManagement.ExceptionManager.Publish(new Exception(dschk.Tables[0].Rows[0][0].ToString()));
                                    //Err_msg = " Some Checks will not show , Problem has occurred while Generating Check Number.)";
                                    return objDataTable;
                                }
                            }
                        }
                //if (!statusObjTransaction)
                if (objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    //if (!statusObjTransaction)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
                throw ex;
            }
            Err_msg = "";
            return objDataTable;
        }
        //*END********************

        public static DataTable PrintChecks(ref object objTransaction, DataTable objDataTable, out string Err_msg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //bool statusObjTransaction = true;
            //if (objTransaction == null)
            //{
            //    objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
            //    statusObjTransaction = false;
            //}
            //object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataTable objPrintChkDT = objDataTable.Clone();
            Err_msg = string.Empty;
            //objTransaction = null;
            try
            {
                //bool _ChkNoStatus = true;
                int _doc_no = 0, _check_no = 0, _prev_doc_no = 0;
                decimal _cash_amt = 0;
                bool doc_no_change_status = false;

                for (int i = 0; i <= objDataTable.Rows.Count - 1; i++)
                {
                    #region Old Code

                    //DVOAPCheckProcessingStpcashe objCheckProcessing = new DVOAPCheckProcessingStpcashe();
                    //DataRow dr = objDataTable.Rows[i];
                    //if (dr["doc_no"] != DBNull.Value)
                    //    objCheckProcessing.doc_no = Convert.ToInt32(dr["doc_no"]);
                    //objCheckProcessing.check_no = dr["check_no"].ToString().Trim();
                    //objCheckProcessing.cash_amt = (dr["cash_amt"] != DBNull.Value && dr["cash_amt"].ToString().Trim().Length > 0) ? Convert.ToDecimal(dr["cash_amt"]) : 0;
                    //if ((objCheckProcessing.check_no.Trim() != string.Empty ? Convert.ToInt32(objCheckProcessing.check_no) : 0) > 0)
                    //{
                    //    bool doc_no_change_status = false;
                    //    if (i != 0)
                    //    {
                    //        if (objCheckProcessing.doc_no != Convert.ToInt32(objDataTable.Rows[i - 1]["doc_no"]))
                    //            doc_no_change_status = true;
                    //    }
                    //    else if (i == 0)
                    //        doc_no_change_status = true;
                    //    if (doc_no_change_status)
                    //    {
                    //        //objTransaction = objDALBaseClassHelper.GetTransactionObject();

                    //        //Update the status of chk_printed(table stpcashe) 'N' to 'Y', TO avoid repetion of printed checks
                    //        Object[] TermsParameter = new object[1];
                    //        TermsParameter[0] = objCheckProcessing.doc_no;
                    //        object updStatus = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref TermsParameter, objCheckProcessing.UPD_CHECK_PRINT_STS, true);
                    //        if (Convert.ToInt32(updStatus) != 1)
                    //        {
                    //            if (!statusObjTransaction && objTransaction != null)
                    //                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //            Err_msg = " Some Checks will not Print , Problem has occurred while updating 'stpcashe'";
                    //            Exception ex = new Exception(Err_msg);
                    //            throw ex;
                    //        }

                    //        //Insert into apchecksrecord
                    //        object[] parameters = new object[8];
                    //        parameters[0] = objCheckProcessing.doc_no;
                    //        parameters[1] = objCheckProcessing.check_no;
                    //        parameters[2] = DVOApplicationUserInfo.UserId;
                    //        parameters[3] = "JKPS APPLICATION";
                    //        parameters[4] = objCheckProcessing.cash_amt;
                    //        parameters[5] = DVOApplicationUserInfo.UserId;
                    //        parameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    //        parameters[7] = DVOApplicationUserInfo.MachineInfo;
                    //        object InsStatus = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objCheckProcessing.INSERT_APCHECKRECORD, true);
                    //        if (Convert.ToInt32(InsStatus) != 1)
                    //        {
                    //            if (!statusObjTransaction && objTransaction != null)
                    //                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //            Err_msg = " Some Checks will not Print , Problem has occurred while Inserting 'apchecksrecord'.";
                    //            Exception ex = new Exception(Err_msg);
                    //            throw ex;
                    //        }
                    //        else
                    //        {
                    //            DataRow[] rows = objDataTable.Select("doc_no = " + objCheckProcessing.doc_no);
                    //            for (int j = 0; j < rows.Length; j++)
                    //            {
                    //                DataRow Pdr = objPrintChkDT.NewRow();
                    //                Pdr.ItemArray = rows[j].ItemArray;
                    //                objPrintChkDT.Rows.Add(Pdr);
                    //            }
                    //            if (objTransaction != null)
                    //            {
                    //                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", objCheckProcessing.doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    objDataTable.Rows.RemoveAt(i);
                    //    i--;
                    //    _ChkNoStatus = false; 
                    //}

                    #endregion Old Code


                    DataRow dr = objDataTable.Rows[i];
                    _doc_no = dr["doc_no"] != DBNull.Value && dr["doc_no"].ToString().Trim().Length > 0 ? Convert.ToInt32(dr["doc_no"]) : 0;
                    _check_no = dr["check_no"] != DBNull.Value && dr["check_no"].ToString().Trim().Length > 0 ? Convert.ToInt32(dr["check_no"]) : 0;
                    _cash_amt = dr["cash_amt"] != DBNull.Value && dr["cash_amt"].ToString().Trim().Length > 0 ? Convert.ToDecimal(dr["cash_amt"]) : 0;
                    if (_check_no > 0)
                    {
                        doc_no_change_status = false;
                        if (i != 0)
                        {
                            _prev_doc_no = objDataTable.Rows[i - 1]["doc_no"] != DBNull.Value && objDataTable.Rows[i - 1]["doc_no"].ToString().Trim().Length > 0 ? Convert.ToInt32(objDataTable.Rows[i - 1]["doc_no"]) : 0;
                            if (_doc_no != _prev_doc_no)
                                doc_no_change_status = true;
                        }
                        else if (i == 0)
                            doc_no_change_status = true;

                        if (doc_no_change_status)
                        {
                            object[] parameters = new object[6];
                            parameters[0] = _doc_no;
                            parameters[1] = _check_no;
                            parameters[2] = _cash_amt;
                            parameters[3] = _prev_doc_no;
                            parameters[4] = DVOApplicationUserInfo.UserId; ;
                            parameters[5] = DVOApplicationUserInfo.MachineInfo;

                            object obj = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOAPCheckProcessingStpcashe()).PRINT_AP_CHECK_ONE_PROC);
                        }

                        DataRow[] rows = objDataTable.Select("doc_no = " + _doc_no);
                        for (int j = 0; j < rows.Length; j++)
                        {
                            DataRow Pdr = objPrintChkDT.NewRow();
                            Pdr.ItemArray = rows[j].ItemArray;
                            objPrintChkDT.Rows.Add(Pdr);
                        }
                    }
                }

                //if (!statusObjTransaction && objTransaction != null)
                //    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //if (!_ChkNoStatus)
                //{
                //    System.Windows.Forms.MessageBox.Show("Some checks without check no. have not been printed.","JKPS",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    for (int i = 0; i < objDataTable.Rows.Count; i++)
                    {
                        DataRow dr = objDataTable.Rows[i];
                        if (dr["doc_no"] != DBNull.Value && dr["doc_no"].ToString().Trim().Length > 0)
                            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    }
                    //if (!statusObjTransaction && objTransaction != null)
                    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    objPrintChkDT.Clear();
                }
                throw ex;
            }
            Err_msg = "";
            return objPrintChkDT;
        }

        //*******Added by Sunil Pahwa  Updating UnPrinted Checks*********************** 
        public static DataTable PrintUPChecks(DataTable objDataTable, out string Err_msg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataTable objPrintChkDT = new DataTable();
            objPrintChkDT = objDataTable.Clone();
            Err_msg = string.Empty;

            //object objTransaction = null;
            try
            {
                for (int i = 0; i <= objDataTable.Rows.Count - 1; i++)
                {
                    DVOAPCheckProcessingStpcashe objCheckProcessing = new DVOAPCheckProcessingStpcashe();
                    DataRow dr = objDataTable.Rows[i];
                    if (dr["doc_no"] != DBNull.Value)
                        objCheckProcessing.doc_no = Convert.ToInt32(dr["doc_no"]);
                    objCheckProcessing.check_no = dr["check_no"].ToString();
                    objCheckProcessing.cash_amt = (dr["cash_amt"] != DBNull.Value && dr["cash_amt"].ToString().Trim().Length > 0) ? Convert.ToDecimal(dr["cash_amt"]) : 0;

                    bool doc_no_change_status = false;
                    if (i != 0)
                    {
                        if (objCheckProcessing.doc_no != Convert.ToInt32(objDataTable.Rows[i - 1]["doc_no"]))
                        {
                            doc_no_change_status = true;
                        }
                    }
                    else if (i == 0)
                        doc_no_change_status = true;
                    if (doc_no_change_status)
                    {
                        //objTransaction = objDALBaseClassHelper.GetTransactionObject();

                        //Update the status of chk_printed(table stpcashe) 'N' to 'Y', TO avoid repetion of printed checks
                        Object[] TermsParameter = new object[1];
                        TermsParameter[0] = objCheckProcessing.doc_no;
                        object updStatus = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref TermsParameter, objCheckProcessing.UPD_UNP_CHECKPRINT, true);
                        if (Convert.ToInt32(updStatus) != 1)
                        {
                            if (objTransaction != null)
                                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            Err_msg = " Some Checks will not Print , Problem has occurred while updating 'stpcashe'";
                            Exception ex = new Exception(Err_msg);
                            throw ex;
                        }

                        //Insert into apchecksrecord
                        object[] parameters = new object[8];
                        parameters[0] = objCheckProcessing.doc_no;
                        parameters[1] = objCheckProcessing.check_no;
                        parameters[2] = DVOApplicationUserInfo.UserId;
                        parameters[3] = "JKPS APPLICATION";
                        parameters[4] = objCheckProcessing.cash_amt;
                        parameters[5] = DVOApplicationUserInfo.UserId;
                        parameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[7] = DVOApplicationUserInfo.MachineInfo;
                        object InsStatus = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objCheckProcessing.INSERT_APCHECKRECORD, true);
                        if (Convert.ToInt32(InsStatus) != 1)
                        {
                            if (objTransaction != null)
                                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            Err_msg = " Some Checks will not Print , Problem has occurred while Inserting 'apchecksrecord'.";
                            Exception ex = new Exception(Err_msg);
                            throw ex;
                        }
                        else
                        {
                            DataRow[] rows = objDataTable.Select("doc_no = " + objCheckProcessing.doc_no);
                            for (int j = 0; j < rows.Length; j++)
                            {
                                DataRow Pdr = objPrintChkDT.NewRow();
                                Pdr.ItemArray = rows[j].ItemArray;
                                objPrintChkDT.Rows.Add(Pdr);
                            }
                            if (objTransaction != null)
                            {
                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", objCheckProcessing.doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                            }
                        }
                    }
                }
                if (objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    for (int i = 0; i <= objDataTable.Rows.Count - 1; i++)
                    {
                        DataRow dr = objDataTable.Rows[i];
                        if (dr["doc_no"] != DBNull.Value)
                            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    }
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    objPrintChkDT.Clear();
                }
                return objPrintChkDT;
            }
            Err_msg = "";
            return objPrintChkDT;
        }
        //**END******************************************


        public static DataSet VarifyCheckStatus(ref DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe)
        {
            object[] parameters = new object[2];
            parameters[0] = objDVOAPCheckProcessingStpcashe.doc_no;
            parameters[1] = objDVOAPCheckProcessingStpcashe.check_no;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOAPCheckProcessingStpcashe), objDVOAPCheckProcessingStpcashe.GET_VARIFYCHECK);
            return ds;
        }
        public static DataSet ShowDuplicateChecks(ref DVOAPCheckProcessingStpcashe objSearch)
        {

            DataSet ds;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {

                if (objSearch.ap_type.Trim() == "N")
                {
                    Object[] Parameter = new object[7];
                    Parameter[0] = objSearch.batch_id;
                    Parameter[1] = objSearch.cash_acct;
                    Parameter[2] = objSearch.ap_type.Trim();
                    Parameter[3] = objSearch.check_no.Trim();
                    Parameter[4] = objSearch.doc_no;
                    Parameter[5] = objSearch.check_no_to;
                    Parameter[6] = objSearch.doc_no_to;
                    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_DUPCHECKS);
                    ds.Tables[0].Columns[0].ColumnName = "dist_deb_cred";
                    ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                    ds.Tables[0].Columns[2].ColumnName = "due_date";
                    ds.Tables[0].Columns[3].ColumnName = "dist_acct";
                    ds.Tables[0].Columns[4].ColumnName = "inv_doc_no";
                    ds.Tables[0].Columns[5].ColumnName = "oa_amt";
                    ds.Tables[0].Columns[6].ColumnName = "oa_deb_cred";
                    ds.Tables[0].Columns[7].ColumnName = "ap_type";
                    ds.Tables[0].Columns[8].ColumnName = "doc_desc";
                    ds.Tables[0].Columns[9].ColumnName = "doc_no";
                    ds.Tables[0].Columns[10].ColumnName = "check_no";
                    ds.Tables[0].Columns[11].ColumnName = "chk_date";
                    ds.Tables[0].Columns[12].ColumnName = "cash_acct";
                    ds.Tables[0].Columns[13].ColumnName = "cash_department";
                    ds.Tables[0].Columns[14].ColumnName = "city";
                    ds.Tables[0].Columns[15].ColumnName = "state";
                    ds.Tables[0].Columns[16].ColumnName = "zip";
                    ds.Tables[0].Columns[17].ColumnName = "country";
                    ds.Tables[0].Columns[18].ColumnName = "address2";
                    ds.Tables[0].Columns[19].ColumnName = "bus_name";
                    ds.Tables[0].Columns[20].ColumnName = "vend_code";
                    ds.Tables[0].Columns[21].ColumnName = "dist_amt";
                    ds.Tables[0].Columns[22].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[23].ColumnName = "acct_desc";
                    ds.Tables[0].Columns[24].ColumnName = "cash_amt";
                }
                else
                {
                    Object[] Parameters = new object[6];
                    Parameters[0] = objSearch.batch_id;
                    Parameters[1] = objSearch.cash_acct;
                    Parameters[2] = objSearch.check_no;
                    Parameters[3] = objSearch.doc_no;
                    Parameters[4] = objSearch.check_no_to;
                    Parameters[5] = objSearch.doc_no_to;
                    ds = objDalBaseClass.GetData(ref Parameters, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_APDUPCHECKPRINT);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //SET COLUMN NAMES
                        ds.Tables[0].Columns[0].ColumnName = "disc_amt";
                        ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                        ds.Tables[0].Columns[2].ColumnName = "dist_acct";
                        ds.Tables[0].Columns[3].ColumnName = "dist_amt";
                        ds.Tables[0].Columns[4].ColumnName = "dist_deb_cred";
                        ds.Tables[0].Columns[5].ColumnName = "due_date";
                        ds.Tables[0].Columns[6].ColumnName = "inv_doc_no";
                        ds.Tables[0].Columns[7].ColumnName = "inv_no";
                        ds.Tables[0].Columns[8].ColumnName = "ap_type";
                        ds.Tables[0].Columns[9].ColumnName = "bus_name";
                        ds.Tables[0].Columns[10].ColumnName = "cash_acct";
                        ds.Tables[0].Columns[11].ColumnName = "cash_amt";
                        ds.Tables[0].Columns[12].ColumnName = "cash_department";
                        ds.Tables[0].Columns[13].ColumnName = "check_no";
                        ds.Tables[0].Columns[14].ColumnName = "chk_date";
                        ds.Tables[0].Columns[15].ColumnName = "doc_desc";
                        ds.Tables[0].Columns[16].ColumnName = "doc_no";
                        ds.Tables[0].Columns[17].ColumnName = "oa_amt";
                        ds.Tables[0].Columns[18].ColumnName = "oa_deb_cred";
                        ds.Tables[0].Columns[19].ColumnName = "vend_code";
                        ds.Tables[0].Columns[20].ColumnName = "inv_date";
                        ds.Tables[0].Columns[21].ColumnName = "orig_amount";
                        ds.Tables[0].Columns[22].ColumnName = "address1";
                        ds.Tables[0].Columns[23].ColumnName = "address2";
                        ds.Tables[0].Columns[24].ColumnName = "city";
                        ds.Tables[0].Columns[25].ColumnName = "country";
                        ds.Tables[0].Columns[26].ColumnName = "pay_to_name";
                        ds.Tables[0].Columns[27].ColumnName = "state";
                        ds.Tables[0].Columns[28].ColumnName = "zip";
                        ds.Tables[0].Columns[29].ColumnName = "acct_desc";
                        ds.Tables[0].Columns[30].ColumnName = "keyvalue";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        public static DataTable PrintDuplicateChecks(DataTable dupobjDataTable, string Notes, out string Errormsg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                for (int i = 0; i <= dupobjDataTable.Rows.Count - 1; i++)
                {

                    DVOAPCheckProcessingStpcashe objCheckProcessing = new DVOAPCheckProcessingStpcashe();
                    DataRow dr = dupobjDataTable.Rows[i];
                    if (dr["doc_no"] != DBNull.Value)
                        objCheckProcessing.doc_no = Convert.ToInt32(dr["doc_no"]);
                    objCheckProcessing.check_no = dr["check_no"].ToString().Trim();
                    objCheckProcessing.cash_amt = (dr["cash_amt"] != DBNull.Value && dr["cash_amt"].ToString().Trim().Length > 0) ? Convert.ToDecimal(dr["cash_amt"]) : 0;

                    bool doc_no_change_status = false;
                    if (i != 0)
                    {
                        if (objCheckProcessing.doc_no != Convert.ToInt32(dupobjDataTable.Rows[i - 1]["doc_no"]))
                        {
                            doc_no_change_status = true;
                        }
                    }
                    else if (i == 0)
                        doc_no_change_status = true;
                    if (doc_no_change_status)
                    {
                        //Insert into apchecksrecord
                        object[] parameters = new object[8];
                        parameters[0] = objCheckProcessing.doc_no;
                        parameters[1] = objCheckProcessing.check_no;
                        parameters[2] = DVOApplicationUserInfo.UserId;
                        parameters[3] = Notes;
                        parameters[4] = objCheckProcessing.cash_amt;
                        parameters[5] = DVOApplicationUserInfo.UserId;
                        parameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                        parameters[7] = DVOApplicationUserInfo.MachineInfo;
                        object InsStatus = objDalBaseClass.ExecuteScalar(ref parameters, objCheckProcessing.INSERT_APCHECKRECORD);
                        //object InsStatus = objDalBaseClass.ExecuteProcedure(ref parameters, objCheckProcessing.INSERT_APCHECKRECORD);
                        if (Convert.ToInt32(InsStatus) != 1)
                        {
                            Errormsg = " Some Checks will not Print , Problem has occurred while Inserting 'apchecksrecord')";
                            return dupobjDataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Errormsg = "";
            return dupobjDataTable;
        }

        ////************* Added by Bharat Dhall[March 26, 2009]***********
        //public static void ResetDocumentCheckNo(int DocumentNo)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    try
        //    {
        //        object[] parameters = new object[1];
        //        parameters[0] = DocumentNo;
        //        object cmdStatus = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOAPCheckProcessingStpcashe()).RESET_CHECKNO);
        //        if (cmdStatus == null)
        //            throw new Exception();
        //        else if (Convert.ToInt32(cmdStatus) < 1)
        //            throw new Exception();
        //    }
        //    catch(Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //    }
        //}


        public static DataTable ShowChecksWithPostDated(ref object objTransaction, ref DVOAPCheckProcessingStpcashe objSearch, out string Err_msg)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
                statusObjTransaction = false;
            }
            //object objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();

            DataTable objDataTable = new DataTable();
            DataSet ds;
            Err_msg = string.Empty;
            try
            {
                if (objSearch.ap_type.Trim() == "N")
                {
                    Object[] Parameter = new object[2];
                    Parameter[0] = objSearch.batch_id;
                    Parameter[1] = objSearch.cash_acct;
                    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_NONAPCHK_POSTDATED);
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //SET COLUMN NAMES
                                ds.Tables[0].Columns[0].ColumnName = "dist_deb_cred";
                                ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                                ds.Tables[0].Columns[2].ColumnName = "due_date";
                                ds.Tables[0].Columns[3].ColumnName = "dist_acct";
                                ds.Tables[0].Columns[4].ColumnName = "inv_doc_no";
                                ds.Tables[0].Columns[5].ColumnName = "oa_amt";
                                ds.Tables[0].Columns[6].ColumnName = "oa_deb_cred";
                                ds.Tables[0].Columns[7].ColumnName = "ap_type";
                                ds.Tables[0].Columns[8].ColumnName = "doc_desc";
                                ds.Tables[0].Columns[9].ColumnName = "doc_no";
                                ds.Tables[0].Columns[10].ColumnName = "check_no";
                                ds.Tables[0].Columns[11].ColumnName = "chk_date";
                                ds.Tables[0].Columns[12].ColumnName = "cash_acct";
                                ds.Tables[0].Columns[13].ColumnName = "cash_department";
                                ds.Tables[0].Columns[14].ColumnName = "city";
                                ds.Tables[0].Columns[15].ColumnName = "state";
                                ds.Tables[0].Columns[16].ColumnName = "zip";
                                ds.Tables[0].Columns[17].ColumnName = "country";
                                ds.Tables[0].Columns[18].ColumnName = "address2";
                                ds.Tables[0].Columns[19].ColumnName = "bus_name";
                                ds.Tables[0].Columns[20].ColumnName = "vend_code";
                                ds.Tables[0].Columns[21].ColumnName = "dist_amt";
                                ds.Tables[0].Columns[22].ColumnName = "keyvalue";
                                ds.Tables[0].Columns[23].ColumnName = "acct_desc";
                                ds.Tables[0].Columns[24].ColumnName = "cash_amt";
                                objDataTable = ds.Tables[0].Clone();
                            }
                    //else
                    //{
                    //    if (objTransaction!=null)
                    //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //}
                }
                else
                {
                    Object[] Parameter = new object[2];
                    Parameter[0] = objSearch.batch_id;
                    Parameter[1] = objSearch.cash_acct;
                    ds = objDalBaseClass.GetData(ref Parameter, typeof(DVOAPCheckProcessingStpcashe), objSearch.GET_APCHECK_POSTDATED);
                    if (ds != null)
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //SET COLUMN NAMES
                                ds.Tables[0].Columns[0].ColumnName = "disc_amt";
                                ds.Tables[0].Columns[1].ColumnName = "disc_deb_cred";
                                ds.Tables[0].Columns[2].ColumnName = "dist_acct";
                                ds.Tables[0].Columns[3].ColumnName = "dist_amt";
                                ds.Tables[0].Columns[4].ColumnName = "dist_deb_cred";
                                ds.Tables[0].Columns[5].ColumnName = "due_date";
                                ds.Tables[0].Columns[6].ColumnName = "inv_doc_no";
                                ds.Tables[0].Columns[7].ColumnName = "inv_no";
                                ds.Tables[0].Columns[8].ColumnName = "ap_type";
                                ds.Tables[0].Columns[9].ColumnName = "bus_name";
                                ds.Tables[0].Columns[10].ColumnName = "cash_acct";
                                ds.Tables[0].Columns[11].ColumnName = "cash_amt";
                                ds.Tables[0].Columns[12].ColumnName = "cash_department";
                                ds.Tables[0].Columns[13].ColumnName = "check_no";
                                ds.Tables[0].Columns[14].ColumnName = "chk_date";
                                ds.Tables[0].Columns[15].ColumnName = "doc_desc";
                                ds.Tables[0].Columns[16].ColumnName = "doc_no";
                                ds.Tables[0].Columns[17].ColumnName = "oa_amt";
                                ds.Tables[0].Columns[18].ColumnName = "oa_deb_cred";
                                ds.Tables[0].Columns[19].ColumnName = "vend_code";
                                ds.Tables[0].Columns[20].ColumnName = "inv_date";
                                ds.Tables[0].Columns[21].ColumnName = "orig_amount";
                                ds.Tables[0].Columns[22].ColumnName = "address1";
                                ds.Tables[0].Columns[23].ColumnName = "address2";
                                ds.Tables[0].Columns[24].ColumnName = "city";
                                ds.Tables[0].Columns[25].ColumnName = "country";
                                ds.Tables[0].Columns[26].ColumnName = "pay_to_name";
                                ds.Tables[0].Columns[27].ColumnName = "state";
                                ds.Tables[0].Columns[28].ColumnName = "zip";
                                ds.Tables[0].Columns[29].ColumnName = "acct_desc";
                                ds.Tables[0].Columns[30].ColumnName = "keyvalue";

                                objDataTable = ds.Tables[0].Clone();
                            }
                    //else
                    //{
                    //    if (objTransaction != null)
                    //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //}
                }

                if (ds != null)
                    if (ds.Tables.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int doc_no = 0;
                            DataRow dr = ds.Tables[0].Rows[i];

                            if (ds.Tables[0].Columns.Contains("pay_to_name") && ds.Tables[0].Columns.Contains("bus_name"))
                                if (dr["pay_to_name"] == DBNull.Value || dr["pay_to_name"] != null || dr["pay_to_name"].ToString().Trim() == string.Empty)
                                    dr["pay_to_name"] = dr["bus_name"].ToString().Trim();

                            if (dr["doc_no"] != DBNull.Value)
                            {
                                doc_no = Convert.ToInt32(dr["doc_no"]);
                            }
                            bool doc_no_change_status = false;
                            if (i != 0)
                            {
                                if (doc_no != Convert.ToInt32(ds.Tables[0].Rows[i - 1]["doc_no"]))
                                {
                                    doc_no_change_status = true;
                                }
                            }
                            else if (i == 0)
                                doc_no_change_status = true;
                            if (doc_no_change_status)
                            {
                                object[] parameters = new object[2];
                                parameters[0] = doc_no;
                                // if check number is null in stpcashe then get last_chkno 
                                //increment by one and update stpcntrc by new check number. 
                                //Generate Check Number.....
                                int newCheckNo = 0;
                                if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                                    //newCheckNo = BLLAccountingLiberary.Auto_Next("stpcntrc", "last_chkno", ref objTransaction);
                                    newCheckNo = BLLAccountingLiberary.Auto_Next_APCheckNo();

                                else newCheckNo = Convert.ToInt32(dr["check_no"]);
                                parameters[1] = newCheckNo;
                                //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                                DataSet dschk = objDalBaseClass.ExecuteDataSet_ByTransaction(ref objTransaction, ref parameters, (new DvoCheckListing()).FIND_SPNAME);
                                if (Convert.ToInt32(dschk.Tables[0].Rows[0][0]) == 1)
                                {
                                    //if (Convert.ToString(dschk.Tables[0].Rows[0][1]).Trim() == "Y")
                                    //{
                                    int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref objTransaction, "stpcashe", doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false);
                                    if (LockStatus != 1)
                                    {
                                        ///////////////////////////////////////////
                                        ////////////////////////////////////////////
                                    }
                                    else
                                    {
                                        dr["check_no"] = dschk.Tables[0].Rows[0][2];
                                        DataRow[] rows = ds.Tables[0].Select("doc_no = " + doc_no);
                                        for (int j = 0; j < rows.Length; j++)
                                        {
                                            DataRow drn = objDataTable.NewRow();
                                            drn.ItemArray = rows[j].ItemArray;
                                            drn["check_no"] = dschk.Tables[0].Rows[0][2];
                                            objDataTable.Rows.Add(drn);
                                        }
                                    }
                                    //}
                                }
                                else
                                {
                                    if (!statusObjTransaction && objTransaction != null)
                                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                    for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                                        if (objTransaction != null)
                                            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "stpcashe", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                    ExceptionManagement.ExceptionManager.Publish(new Exception(dschk.Tables[0].Rows[0][0].ToString()));
                                    //Err_msg = " Some Checks will not show , Problem has occurred while Generating Check Number.)";
                                    return objDataTable;
                                }
                            }
                        }
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                throw ex;
            }
            Err_msg = "";
            //remove all rows from table, where checkno is '0' or blank or null
            foreach (DataRow dr in objDataTable.Rows)
            {
                if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                    objDataTable.Rows.Remove(dr);
            }
            objDataTable.AcceptChanges();
            return objDataTable;
        }

        //Added By Rahul Jain On 26/08/2009 for getting Reconciled Checks Details  Report
        public static DataSet GetChecksReconciled(ref DVOCheckRegisterstpchkreconcile objstpchkreconcile)
        {
            DataSet ds = new DataSet();
            try
            {
                Object[] parameters = new object[3];

                parameters[0] = objstpchkreconcile.FromDate;
                parameters[1] = objstpchkreconcile.ToDate;
                parameters[2] = objstpchkreconcile.insertby;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                ds = objDalBaseClass.GetData(objstpchkreconcile.FIND_RECONCILED_CHECKS(ref parameters));
                return ds;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return ds;
        }
        //*************************************************************

    }
}
