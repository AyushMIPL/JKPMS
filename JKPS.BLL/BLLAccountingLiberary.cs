using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using JKPS.DL;
using JKPS.COMMON;
using System.IO;

namespace JKPS.BLL
{
    /// <summary>
    /// Implemented by :sanjay
    /// Date : 06/10/2008
    /// Description : This class basically used for Accounting Methods. 
    /// Modified by:
    /// Modified Date :
    /// Description :
    /// </summary>
    public class BLLAccountingLiberary
    {

        /// <summary>
        /// This method takes the A/P transaction data as arguments,
        /// and either posts to A/P or checks for an ok posting.
        /// It is designed to be run in "CHECK" mode during the edit list phase,
        /// and in "POST" mode during the posting phase.          /// 
        /// <param name="objPostAP">reference of DVOPostAP type object as a collection of parameters of search criteria.</param>
        /// <returns>returns true/false based on pass/fail of the method</returns>
        /// <summary>
        public static int ap_post(ref DVOPostAP objPostAP, ref List<DVOPostAP> listPostAP)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //set default value of parameters and pass to routines as per criteria
            object[] parameters = new object[27];
            parameters[0] = 0;              //@check int,
            parameters[1] = string.Empty;   //@orig_journal char(2),
            parameters[2] = 0;              //@doc_no int,
            parameters[3] = 0;              //@inv_chk_no char(12),
            parameters[4] = string.Empty;   //@doc_type char(2),
            parameters[5] = 0;              //@amount float(12,2),
            parameters[6] = string.Empty;   //@currency_code char(3),
            parameters[7] = 0;              //@curr_ex_rate decimal(16,2),
            parameters[8] = 0;              //@home_curr_amount decimal(12, 2)
            parameters[9] = string.Empty;   //@vend_code char(6)
            parameters[10] = string.Empty;  //@pay_to_code char(6)
            parameters[11] = string.Empty;  //@disc_amount char(30)
            parameters[12] = "01/01/1900";  //@to_pay_date date
            parameters[13] = "01/01/1900";  //@due_date date
            parameters[14] = string.Empty;  //@inv_desc char(30)
            parameters[15] = "01/01/1900";  //@disc_date date
            parameters[16] = 0;             //@ap_acct_no int
            parameters[17] = string.Empty;  //@ap_department char(3)
            parameters[18] = string.Empty;  //@po_no char(10)
            parameters[19] = "01/01/1900";  //@po_date date
            parameters[20] = 0;             //@cash_acct_no int
            parameters[21] = string.Empty;  //@cash_department char(3)
            parameters[22] = "01/01/1900";  //@inv_date date
            parameters[23] = 0;             //@to_pay_amt decimal(12,2)
            parameters[24] = 0;             //@to_take_disc decimal(12,2)
            parameters[25] = 0;             //@disc_bal decimal(12,2)
            parameters[26] = "01/01/1900";  //@doc_date date

            //To keep track on transactions
            int process = 0;
            try
            {
                //Start first transaction
                process = 1;
                objPostAP.check = 4;
                parameters[0] = objPostAP.check;
                parameters[1] = objPostAP.orig_journal;
                parameters[2] = objPostAP.doc_no;
                parameters[3] = objPostAP.inv_chk_no;
                parameters[4] = objPostAP.doc_type;

                int result1 = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));
                if (result1 > -1)
                {
                    process = 2;
                    objPostAP.check = 5;
                    parameters[0] = objPostAP.check;
                    parameters[1] = objPostAP.orig_journal;
                    parameters[2] = objPostAP.doc_no;
                    parameters[3] = objPostAP.inv_chk_no;
                    parameters[5] = objPostAP.amount;
                    parameters[6] = objPostAP.currency_code;
                    parameters[7] = objPostAP.curr_ex_rate;
                    parameters[8] = objPostAP.home_curr_amount;
                    int result5 = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));

                    if (result5 > -1 && objPostAP.doc_type == "IN")
                    {
                        process = 3;
                        objPostAP.check = 6;
                        parameters[0] = objPostAP.check;
                        parameters[2] = objPostAP.doc_no;
                        parameters[3] = objPostAP.inv_chk_no;
                        parameters[5] = objPostAP.amount;
                        parameters[6] = objPostAP.currency_code;
                        parameters[7] = objPostAP.curr_ex_rate;
                        parameters[8] = objPostAP.home_curr_amount;
                        parameters[9] = objPostAP.vend_code;
                        parameters[10] = objPostAP.pay_to_code;
                        parameters[12] = objPostAP.to_pay_date;
                        parameters[13] = objPostAP.due_date;
                        parameters[14] = objPostAP.inv_desc;
                        parameters[15] = objPostAP.disc_date;
                        parameters[16] = objPostAP.ap_acct_no;
                        parameters[17] = objPostAP.ap_department;
                        parameters[18] = objPostAP.po_no;
                        parameters[19] = objPostAP.po_date;
                        parameters[20] = objPostAP.cash_acct_no;
                        parameters[21] = objPostAP.cash_department;
                        parameters[22] = objPostAP.doc_date;
                        parameters[23] = objPostAP.to_pay_amount;
                        parameters[24] = objPostAP.to_take_disc;
                        parameters[25] = objPostAP.disc_bal;
                        parameters[26] = objPostAP.doc_date;
                        int result6 = objDALBaseClass.InsertData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));
                        if (result6 > -1)
                        {
                            if (objPostAP.inv_doc_no == 0)
                            {
                                process = 4;
                                objPostAP.check = 7;
                                parameters[0] = objPostAP.check;
                                parameters[5] = objPostAP.amount;
                                parameters[9] = objPostAP.vend_code;
                                int result7 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));
                            }
                            else
                            {
                                if (objPostAP.act_type == "A")
                                {
                                    process = 5;
                                    objPostAP.check = 8;
                                    parameters[0] = objPostAP.check;
                                    parameters[2] = objPostAP.doc_no;
                                    parameters[8] = objPostAP.home_curr_amount;
                                    parameters[23] = objPostAP.to_pay_amount;
                                    int result8 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));
                                }
                                else
                                {
                                    process = 6;
                                    objPostAP.check = 9;
                                    int result9 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));


                                }
                            }
                        }
                        //Set the status Posted ='Y'(table=stpinvce) after successfully posted of document
                        objPostAP.check = 10;
                        parameters[0] = objPostAP.check;
                        parameters[2] = objPostAP.doc_no;
                        int result10 = objDALBaseClass.UpdateData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPostAP));



                    }

                }
                parameters = null;
                objDALBaseClass = null;
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                return 11;

            }
            catch (Exception ex)
            {
                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                return process;
            }
            return process;
        }

        //******************************Updated by sanjay *******************************
        public static DataSet GET_AP_CHECKS(ref DVOPayableListingStpinvce objPayableListing)
        {
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = objPayableListing.check;
                parameters[1] = objPayableListing.orig_journal;
                parameters[2] = objPayableListing.doc_no;
                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), objPayableListing.CHECK_POST);
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

        public static DataSet UPD_TRX(ref DVOPostAP objPayableListing)
        {
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = objPayableListing.batch_id;
                parameters[1] = objPayableListing.doc_count;

                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPostAP), objPayableListing.Update_trx);
                {

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
       
        //******************************** Added by Bharat Dhall *********************

        /// <summary>
        /// To check that entered keyvalue,accountNumber is valid to make journal and amount is valid for that
        /// account
        /// </summary>
        /// <param name="LoginId">id of logined user</param>
        /// <param name="AccountTypeEntered">AccountType of account</param>
        /// <param name="AccountNumberEntered">Account Number</param>
        /// <param name="keyvalueEntered">Key-Value of Account</param>
        /// <param name="AmountRequested">Amount Requested for entered account</param>
        /// <returns></returns>
        //public static Hashtable ValidateKeyValue_Account(string LoginId, string AccountTypeEntered, int AccountNumberEntered, string keyvalueEntered, decimal AmountRequested, string Module)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    DataSet ds = new DataSet();
        //    String KeyvalueData = keyvalueEntered;
        //    int AccountTypeId = 0;

        //    decimal BudgetAllocated = 0, AmountSpent = 0, UnpostedInvoice = 0, UnpostedGLAmount = 0, CommittedAmount = 0;
        //    Hashtable ReturnHash = new Hashtable();
        //    ReturnHash.Add("Status", 0);
        //    ReturnHash.Add("BudgetAllocated", BudgetAllocated);
        //    ReturnHash.Add("AmountSpent", AmountSpent);
        //    ReturnHash.Add("UnpostedInvoice", UnpostedInvoice);
        //    ReturnHash.Add("UnpostedGLAmount", UnpostedGLAmount);
        //    ReturnHash.Add("CommittedAmount", CommittedAmount);
        //    ReturnHash.Add("AllowedAmount", 0);

        //    try
        //    {
        //        //get AccountTypeId from AccountType
        //        DVOGLAccountTypeMaintenance objDVOGLAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
        //        objDVOGLAccountTypeMaintenance.accounttype = AccountTypeEntered;
        //        List<DVOGLAccountTypeMaintenance> listDVOGLAccountTypeMaintenance = BLLGLAccountTypeMaintenance.GetAccountTypeMaintenance(ref objDVOGLAccountTypeMaintenance);
        //        if (listDVOGLAccountTypeMaintenance.Count > 0)
        //            AccountTypeId = listDVOGLAccountTypeMaintenance[0].id;
        //        listDVOGLAccountTypeMaintenance = null;
        //        objDVOGLAccountTypeMaintenance = null;

        //        object[] parameters = new object[9];
        //        parameters[0] = 0;              //@case int,
        //        parameters[1] = string.Empty;   //@LoginId char(20),
        //        parameters[2] = string.Empty;   //@AccountTypeEntered char(10),
        //        parameters[3] = 0;              //@AccountNumberEntered int,
        //        parameters[4] = string.Empty;   //@keyvalueEntered char(10),
        //        parameters[5] = string.Empty;   //@year char(10),
        //        parameters[6] = string.Empty;   //@set char(6),
        //        parameters[7] = string.Empty;   //@month char(2),
        //        parameters[8] = 0;              //@searchedAccountNumber

        //        //if (HasAccountPermission(LoginId, keyvalueEntered, AccountTypeId, Module))

        //        //check user has accouont-permission or not
        //        parameters[0] = 1;
        //        parameters[1] = LoginId;
        //        parameters[2] = AccountTypeEntered;
        //        int acount = 0;
        //        ds = new DataSet();
        //        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //        if (ds.Tables.Count > 0)
        //            if (ds.Tables[0].Rows.Count > 0)
        //                acount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //        acount = 1;
        //        //if count greater than 0, means user have permission to use
        //        if (acount > 0)
        //        {
        //            parameters[0] = 2;
        //            parameters[2] = AccountTypeEntered;
        //            parameters[3] = AccountNumberEntered;
        //            string accountCategory = string.Empty;
        //            ds = new DataSet();
        //            ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //            if (ds.Tables.Count > 0)
        //                if (ds.Tables[0].Rows.Count > 0)
        //                    accountCategory = (ds.Tables[0].Rows[0][1] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][1].ToString().Trim();//"v_actCat"

        //            if (accountCategory == string.Empty)
        //            {
        //                parameters[0] = 3;
        //                parameters[2] = AccountTypeEntered;
        //                ds = new DataSet();
        //                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                if (ds.Tables.Count > 0)
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                        accountCategory = (ds.Tables[0].Rows[0][1] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][1].ToString().Trim();//"v_actCat"
        //            }

        //            if (accountCategory == "F")
        //            {
        //                int count = 0;
        //                int searchedAccountNumber = 0;

        //                int loopStatus = 0;
        //            check1: parameters[0] = 4;
        //                parameters[2] = AccountTypeEntered;
        //                parameters[4] = keyvalueEntered;
        //                count = 0;
        //                searchedAccountNumber = 0;
        //                ds = new DataSet();
        //                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                if (ds.Tables.Count > 0)
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                    {
        //                        count = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //                        searchedAccountNumber = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][2]);//"v_actNo"
        //                    }

        //                if (count > 1)
        //                {
        //                    //System.Windows.Forms.MessageBox.Show("There can't be more than one associated budget values");
        //                    //return false;
        //                    ReturnHash["Status"] = -1;
        //                    return ReturnHash;
        //                }
        //                else if (count < 1)
        //                {
        //                    parameters[0] = 5;
        //                    parameters[2] = AccountTypeEntered;
        //                    int positionsCount = 0;
        //                    int position = 0;
        //                    int length = 0;
        //                    ds = new DataSet();
        //                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                    if (ds.Tables.Count > 0)
        //                        if (ds.Tables[0].Rows.Count > loopStatus)
        //                        {
        //                            positionsCount = (ds.Tables[0].Rows[loopStatus][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[loopStatus][0]);//"v_count"
        //                            position = (ds.Tables[0].Rows[loopStatus][3] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[loopStatus][3]);//"v_pos"
        //                            length = (ds.Tables[0].Rows[loopStatus][4] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[loopStatus][4]);//"v_lgth"
        //                        }
        //                    if (positionsCount > 0)
        //                    {
        //                        if (keyvalueEntered.Length >= position && keyvalueEntered.Length >= position + length - 1)
        //                        {
        //                            keyvalueEntered = keyvalueEntered.Remove(position - 1, 2);
        //                            keyvalueEntered = keyvalueEntered.Insert(position - 1, (new String('#', length)));
        //                        }
        //                        loopStatus += 1;
        //                        if (loopStatus <= positionsCount)
        //                            goto check1;
        //                    }
        //                    if (searchedAccountNumber <= 0)
        //                    {
        //                        //System.Windows.Forms.MessageBox.Show("There isn't account for this keyvalue");
        //                        //return false;
        //                        ReturnHash["Status"] = -2;
        //                        return ReturnHash;
        //                    }
        //                }

        //                if (searchedAccountNumber > 0)
        //                {
        //                    searchedAccountNumber = 0;
        //                    //first of all get current year,month and set
        //                    string year = BLLGLWarrantEntryInbwarah.GetCurrentAccountingYear();
        //                    string set = year;
        //                    string month = BLLGLWarrantEntryInbwarah.GetCurrentAccountingMonth();

        //                    parameters[0] = 6;
        //                    parameters[2] = AccountTypeEntered;
        //                    parameters[4] = keyvalueEntered;
        //                    parameters[5] = year;
        //                    parameters[6] = set;
        //                    //parameters[8] = searchedAccountNumber;
        //                    decimal allocateToDate = 0;
        //                    ds = new DataSet();
        //                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                    if (ds.Tables.Count > 0)
        //                        if (ds.Tables[0].Rows.Count > 0)
        //                        {
        //                            searchedAccountNumber = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][2]);//"v_actNo"
        //                            allocateToDate = (ds.Tables[0].Rows[0][5] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][5]);//"v_alocdate"
        //                        }

        //                    if (searchedAccountNumber <= 0)
        //                    {
        //                        //System.Windows.Forms.MessageBox.Show("There is no associated account");
        //                        //return false;
        //                        ReturnHash["Status"] = -3;
        //                        return ReturnHash;
        //                    }
        //                    else
        //                    {
        //                        parameters[0] = 7;
        //                        parameters[5] = year;
        //                        parameters[6] = set;
        //                        parameters[8] = searchedAccountNumber;
        //                        int Amount = 0;
        //                        ds = new DataSet();
        //                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                        if (ds.Tables.Count > 0)
        //                            if (ds.Tables[0].Rows.Count > 0)
        //                            {
        //                                Amount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //                            }

        //                        //if (Amount < 0)
        //                        //    Amount = 0;
        //                        allocateToDate += Amount;
        //                        BudgetAllocated = allocateToDate;
        //                        ReturnHash["BudgetAllocated"] = BudgetAllocated;

        //                        decimal TotalSpent = 0;
        //                        //    keyvalueEntered = KeyvalueData.ToString().Trim();
        //                        //for posted GL enteries
        //                        parameters[0] = 14;
        //                        parameters[4] = keyvalueEntered.Replace("#", "_");
        //                        //parameters[5] = year;
        //                        //parameters[7] = month;
        //                        int sAccountnumber = 0;
        //                        string incrementWithCredit = "";
        //                        decimal spentBalance = 0;
        //                        DataSet dsActs = new DataSet();
        //                        dsActs = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                        if (dsActs.Tables.Count > 0)
        //                            if (dsActs.Tables[0].Rows.Count > 0)
        //                                foreach (DataRow dr in dsActs.Tables[0].Rows)
        //                                {
        //                                    sAccountnumber = (dr[2] == DBNull.Value) ? 0 : Convert.ToInt32(dr[2]);//"v_actNo"
        //                                    if (sAccountnumber > 0)
        //                                    {
        //                                        parameters[0] = 8;
        //                                        parameters[5] = year;
        //                                        parameters[7] = month;
        //                                        parameters[8] = sAccountnumber;
        //                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                        if (ds.Tables.Count > 0)
        //                                            if (ds.Tables[0].Rows.Count > 0)
        //                                            {
        //                                                incrementWithCredit = (ds.Tables[0].Rows[0][6] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][6].ToString().Trim();//"v_inccrt"
        //                                                spentBalance = (ds.Tables[0].Rows[0][7] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][7]);//"v_balance"
        //                                            }

        //                                        if (spentBalance < 0)
        //                                            spentBalance = 0;
        //                                        TotalSpent += spentBalance;
        //                                        AmountSpent += spentBalance;
        //                                        if (ReturnHash["AmountSpent"] != null)
        //                                            ReturnHash["AmountSpent"] = Convert.ToDecimal(ReturnHash["AmountSpent"]) + AmountSpent;
        //                                        else
        //                                            ReturnHash["AmountSpent"] = AmountSpent;

        //                                        //for unposted GL enteries
        //                                        parameters[0] = 9;
        //                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
        //                                        ds = new DataSet();
        //                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                        if (ds.Tables.Count > 0)
        //                                            if (ds.Tables[0].Rows.Count > 0)
        //                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                                {
        //                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
        //                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//"v_dbtcrd"
        //                                                    //if (_amount < 0)
        //                                                    //    _amount = 0;
        //                                                    if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                    {
        //                                                        if (_amount > 0)
        //                                                        {
        //                                                            TotalSpent += _amount;
        //                                                            UnpostedGLAmount += _amount;
        //                                                            ReturnHash["UnpostedGLAmount"] = UnpostedGLAmount;
        //                                                        }
        //                                                    }
        //                                                    if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                    {
        //                                                        if (_amount < 0)
        //                                                        {
        //                                                            TotalSpent -= _amount;
        //                                                            UnpostedGLAmount -= _amount;
        //                                                            ReturnHash["UnpostedGLAmount"] = UnpostedGLAmount;
        //                                                        }
        //                                                    }
        //                                                }

        //                                        //for unposted invoices
        //                                        parameters[0] = 10;
        //                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
        //                                        ds = new DataSet();
        //                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                        if (ds.Tables.Count > 0)
        //                                            if (ds.Tables[0].Rows.Count > 0)
        //                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                                {
        //                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
        //                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//"v_dbtcrd"
        //                                                    //if (_amount < 0)
        //                                                    //    _amount = 0;
        //                                                    if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                    {
        //                                                        if (_amount > 0)
        //                                                        {
        //                                                            TotalSpent += _amount;
        //                                                            UnpostedInvoice += _amount;
        //                                                            ReturnHash["UnpostedInvoice"] = UnpostedInvoice;
        //                                                        }
        //                                                    }
        //                                                    if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                    {
        //                                                        if (_amount < 0)
        //                                                        {
        //                                                            TotalSpent -= _amount;
        //                                                            UnpostedInvoice -= _amount;
        //                                                            ReturnHash["UnpostedInvoice"] = UnpostedInvoice;
        //                                                        }
        //                                                    }
        //                                                }

        //                                        //entered and/or edited (but still unposted) disbursements
        //                                        parameters[0] = 11;
        //                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
        //                                        ds = new DataSet();
        //                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                        if (ds.Tables.Count > 0)
        //                                            if (ds.Tables[0].Rows.Count > 0)
        //                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                                {
        //                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
        //                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//v_dbtcrd"
        //                                                    //if (_amount < 0)
        //                                                    //    _amount = 0;
        //                                                    if ((_debitCredit == "DB" && incrementWithCredit == "N") || (_debitCredit == "CR" && incrementWithCredit == "Y"))
        //                                                    {
        //                                                        if (_amount > 0)
        //                                                        {
        //                                                            TotalSpent += _amount;
        //                                                            CommittedAmount += _amount;
        //                                                            ReturnHash["CommittedAmount"] = CommittedAmount;
        //                                                        }
        //                                                    }
        //                                                    if ((_debitCredit == "DB" && incrementWithCredit == "Y") || (_debitCredit == "CR" && incrementWithCredit == "N"))
        //                                                    {
        //                                                        if (_amount < 0)
        //                                                        {
        //                                                            TotalSpent -= _amount;
        //                                                            CommittedAmount -= _amount;
        //                                                            ReturnHash["CommittedAmount"] = CommittedAmount;
        //                                                        }
        //                                                    }
        //                                                }

        //                                        //UNPOSTED GENERAL JOURNAL
        //                                        parameters[0] = 12;
        //                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
        //                                        ds = new DataSet();
        //                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                        if (ds.Tables.Count > 0)
        //                                            if (ds.Tables[0].Rows.Count > 0)
        //                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                                {
        //                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
        //                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//"v_dbtcrd"
        //                                                    //if (_amount < 0)
        //                                                    //    _amount = 0;
        //                                                    if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                    {
        //                                                        if (_amount > 0)
        //                                                        {
        //                                                            TotalSpent += _amount;
        //                                                            CommittedAmount += _amount;
        //                                                            ReturnHash["CommittedAmount"] = CommittedAmount;
        //                                                        }
        //                                                    }
        //                                                    if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                    {
        //                                                        if (_amount < 0)
        //                                                        {
        //                                                            TotalSpent -= _amount;
        //                                                            CommittedAmount -= _amount;
        //                                                            ReturnHash["CommittedAmount"] = CommittedAmount;
        //                                                        }
        //                                                    }
        //                                                }

        //                                        parameters[0] = 13;
        //                                        parameters[5] = year;
        //                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
        //                                        decimal totAmount = 0;
        //                                        ds = new DataSet();
        //                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                        if (ds.Tables.Count > 0)
        //                                            if (ds.Tables[0].Rows.Count > 0)
        //                                                totAmount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][0]);//"v_count"
        //                                        if (totAmount > 0)
        //                                            TotalSpent += totAmount;
        //                                    }
        //                                    AmountSpent = 0;
        //                                }


        //                        if (TotalSpent + AmountRequested <= allocateToDate)
        //                        {
        //                            //return true;
        //                            ReturnHash["Status"] = 1;
        //                            return ReturnHash;
        //                        }
        //                        else
        //                        {
        //                            //System.Windows.Forms.MessageBox.Show("Amount beyond the allowed, only " + Convert.ToString(allocateToDate - TotalSpent) + " is allowed.");
        //                            //return false;
        //                            ReturnHash["AllowedAmount"] = (allocateToDate - TotalSpent);
        //                            ReturnHash["Status"] = -4;
        //                            return ReturnHash;
        //                        }
        //                    }
        //                }
        //            }
        //            else if (accountCategory != "F" && AccountTypeEntered == "BELLIN")
        //            {
        //                int Status = 0;
        //                decimal AllowedAmount = 0;
        //                CheckExpense_BELLIN_Accounts(accountCategory, AccountTypeEntered, keyvalueEntered, AmountRequested,
        //                    out Status, out BudgetAllocated, out AmountSpent, out UnpostedInvoice,
        //                    out UnpostedGLAmount, out CommittedAmount, out AllowedAmount);
        //                ReturnHash["Status"] = Status;
        //                ReturnHash["BudgetAllocated"] = BudgetAllocated;
        //                ReturnHash["AmountSpent"] = AmountSpent;
        //                ReturnHash["UnpostedInvoice"] = UnpostedInvoice;
        //                ReturnHash["UnpostedGLAmount"] = UnpostedGLAmount;
        //                ReturnHash["CommittedAmount"] = CommittedAmount;
        //                ReturnHash["AllowedAmount"] = AllowedAmount;
        //                return ReturnHash;
        //            }
        //            else
        //            {
        //                //return true;
        //                ReturnHash["Status"] = 1;
        //                return ReturnHash;
        //            }
        //        }
        //        else
        //        {
        //            //System.Windows.Forms.MessageBox.Show("You haven't permission to make Entries for selected Account");
        //            //return false;
        //            ReturnHash["Status"] = -5;
        //            return ReturnHash;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        //return false;
        //        ReturnHash["Status"] = 0;
        //        return ReturnHash;
        //    }
        //    //return false;
        //    ReturnHash["Status"] = 0;
        //    return ReturnHash;
        //}

        public static Hashtable ValidateKeyValue_Account(string LoginId, string AccountTypeEntered, int AccountNumberEntered, string keyvalueEntered, decimal AmountRequested, string Module)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = new DataSet();
            String KeyvalueData = keyvalueEntered;
            int AccountTypeId = 0;
            String Message = "";
            decimal BudgetAllocated = 0, AmountSpent = 0, UnpostedInvoice = 0, UnpostedGLAmount = 0, CommittedAmount = 0;
            Hashtable ReturnHash = new Hashtable();
            ReturnHash.Add("Status", 0);
            ReturnHash.Add("StatusMessage", Message);
            ReturnHash.Add("BudgetAllocated", BudgetAllocated);
            ReturnHash.Add("AmountSpent", AmountSpent);
            ReturnHash.Add("UnpostedInvoice", UnpostedInvoice);
            ReturnHash.Add("UnpostedGLAmount", UnpostedGLAmount);
            ReturnHash.Add("CommittedAmount", CommittedAmount);
            ReturnHash.Add("AllowedAmount", 0);

            try
            {
                object[] parameters = new object[11];
                parameters[0] = LoginId.Trim();             //Login ID ,
                parameters[1] = AccountTypeEntered.Trim();   //@LoginId char(20),
                parameters[2] = keyvalueEntered.Trim();   //@AccountTypeEntered char(10),
                parameters[3] = AmountRequested;
                parameters[4] = string.Empty;              //@AccountNumberEntered int,
                parameters[5] = string.Empty;   //@keyvalueEntered char(10),
                parameters[6] = string.Empty;   //@year char(10),
                parameters[7] = string.Empty;
                parameters[8] = "gl";   //@set char(6),
                parameters[9] = "budget_checking";   //@month char(2),
                parameters[10] = Module;              //@searchedAccountNumber
                Decimal TotalBudget = 0.0M;
                Decimal TotalExpense = 0.0M;
                Decimal TotalPendingPos = 0.0M;
                Decimal TotalPendingGlenteries = 0.0M;
                Decimal TotalGlAdjustment = 0.0M;
                Decimal TotalBudgetAdjustment = 0.0M;
                Decimal TotalPendingChecks = 0.0M;
                Decimal AmountSpenttillyet = 0.0M;
                Decimal UnpostedGlEnteries = 0.0M;
                Decimal UnpaidInvoices = 0.0M;
                String HavePermission = "";
                String BudgetLevel = "";
                String ErrorMessage = "";
                Int32 Status = 0;
                Decimal CommittedAmounts = 0.0M;
                //***********************************ADDED BY ROHIT WADHWA TO REMOVE THE LENGTH PROCESS OF VARIFICATION ************
                //******************************************************************
                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNTNEXPENSE);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ErrorMessage = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? String.Empty : Convert.ToString(ds.Tables[0].Rows[0][2]);//Status of The Keyvalue Verification 
                        //if the Status of the Keyvalue verification is one than it means success All other values contain different Unsuccess Reasons ;
                        //By Default we are assigning Zero to it .
                        Status = (ds.Tables[0].Rows[0][3] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][3]);//Status of The Keyvalue Verification 
                        TotalBudget = (ds.Tables[0].Rows[0][4] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][4]);//TotalBudget
                        TotalBudgetAdjustment = (ds.Tables[0].Rows[0][5] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][5]);//Total Budget Adjustment
                        TotalExpense = (ds.Tables[0].Rows[0][6] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][6]);//Total Expense
                        AmountSpenttillyet = (ds.Tables[0].Rows[0][7] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][7]);//"v_count"
                        TotalPendingPos = (ds.Tables[0].Rows[0][8] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][8]);//"v_count"
                        TotalPendingChecks = (ds.Tables[0].Rows[0][9] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][9]);//"v_count"
                        UnpostedGlEnteries = (ds.Tables[0].Rows[0][10] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][10]);//"v_count"

                        TotalGlAdjustment = (ds.Tables[0].Rows[0][11] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][11]);//"v_count"
                        UnpaidInvoices = (ds.Tables[0].Rows[0][12] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][12]);//"v_count"


                    }
                }

                ReturnHash["Status"] = Status;
                ReturnHash["StatusMessage"] = ErrorMessage;
                ReturnHash["BudgetAllocated"] = TotalBudget;
                ReturnHash["AmountSpent"] = AmountSpenttillyet;
                ReturnHash["UnpostedInvoice"] = UnpostedInvoice;
                ReturnHash["UnpostedGLAmount"] = UnpostedGLAmount;
                ReturnHash["CommittedAmount"] = CommittedAmount;
                ReturnHash["AllowedAmount"] = TotalBudget - TotalExpense;
            }

            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                //return false;
                ReturnHash["Status"] = 9;
                return ReturnHash;
            }
            return ReturnHash;
        }


        public static void CheckExpense_BELLIN_Accounts(string accountCategory, string AccountTypeEntered,
            string keyvalueEntered, decimal AmountRequested,
            out int Status, out decimal BudgetAllocated, out decimal AmountSpent,out decimal UnpostedInvoice,
            out decimal UnpostedGLAmount, out decimal CommittedAmount, out decimal AllowedAmount)
        {
            Status = 0;
            BudgetAllocated = 0;
            AmountSpent = 0;
            UnpostedInvoice = 0;
            UnpostedGLAmount = 0;
            CommittedAmount = 0;
            AllowedAmount = 0;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet ds = new DataSet();
            string incrementWithCredit = "";
            int GoBelowZero = 0;

            try
            {
                object[] parameters = new object[9];
                parameters[0] = 0;              //@case int,
                parameters[1] = string.Empty;   //@LoginId char(20),
                parameters[2] = string.Empty;   //@AccountTypeEntered char(10),
                parameters[3] = 0;              //@AccountNumberEntered int,
                parameters[4] = string.Empty;   //@keyvalueEntered char(10),
                parameters[5] = string.Empty;   //@year char(10),
                parameters[6] = string.Empty;   //@set char(6),
                parameters[7] = string.Empty;   //@month char(2),
                parameters[8] = 0;              //@searchedAccountNumber

                if (accountCategory != "F" && AccountTypeEntered == "BELLIN")
                {
                    int count = 0;
                    int searchedAccountNumber = 0;

                    parameters[0] = 15;
                    parameters[2] = AccountTypeEntered;
                    parameters[4] = keyvalueEntered;
                    count = 0;
                    searchedAccountNumber = 0;
                    ds = new DataSet();
                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            count = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
                            searchedAccountNumber = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][2]);//"v_actNo"
                            GoBelowZero = (ds.Tables[0].Rows[0][3] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][3]);//"v_pos"
                        }
                    if (count > 1)
                    {
                        Status = -1;
                        return;
                    }
                    else if (count < 1)
                    {
                        Status = -2;
                        return;
                    }

                    if (searchedAccountNumber > 0)
                    {
                        #region Calculate Expense
                        //first of all get current year,month and set
                        string year = ""; ;//= BLLGLWarrantEntryInbwarah.GetCurrentAccountingYear();
                        string set = year;
                        string month = ""; ;//=// BLLGLWarrantEntryInbwarah.GetCurrentAccountingMonth();

                        decimal TotalSpent = 0;
                        //for posted GL enteries
                        parameters[0] = 14;
                        parameters[4] = keyvalueEntered.Replace("#", "_");
                        int sAccountnumber = 0;
                        decimal spentBalance = 0;
                        DataSet dsActs = new DataSet();
                        dsActs = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                        if (dsActs.Tables.Count > 0)
                            if (dsActs.Tables[0].Rows.Count > 0)
                                foreach (DataRow dr in dsActs.Tables[0].Rows)
                                {
                                    sAccountnumber = (dr[2] == DBNull.Value) ? 0 : Convert.ToInt32(dr[2]);//"v_actNo"
                                    if (sAccountnumber > 0)
                                    {
                                        parameters[0] = 8;
                                        parameters[5] = year;
                                        parameters[7] = month;
                                        parameters[8] = sAccountnumber;
                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                                        if (ds.Tables.Count > 0)
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                incrementWithCredit = (ds.Tables[0].Rows[0][6] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][6].ToString().Trim();//"v_inccrt"
                                                spentBalance = (ds.Tables[0].Rows[0][7] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][7]);//"v_balance"
                                            }

                                        if (spentBalance < 0)
                                            spentBalance = 0;
                                        TotalSpent += spentBalance;
                                        AmountSpent = spentBalance;

                                        //for unposted GL enteries
                                        parameters[0] = 9;
                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
                                        ds = new DataSet();
                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                                        if (ds.Tables.Count > 0)
                                            if (ds.Tables[0].Rows.Count > 0)
                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
                                                {
                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//"v_dbtcrd"
                                                    if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
                                                    {
                                                        if (_amount > 0)
                                                        {
                                                            TotalSpent += _amount;
                                                            UnpostedGLAmount += _amount;
                                                        }
                                                    }
                                                    if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
                                                    {
                                                        if (_amount < 0)
                                                        {
                                                            TotalSpent -= _amount;
                                                            UnpostedGLAmount -= _amount;
                                                        }
                                                    }
                                                }

                                        //for unposted invoices
                                        parameters[0] = 10;
                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
                                        ds = new DataSet();
                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                                        if (ds.Tables.Count > 0)
                                            if (ds.Tables[0].Rows.Count > 0)
                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
                                                {
                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//"v_dbtcrd"
                                                    if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
                                                    {
                                                        if (_amount > 0)
                                                        {
                                                            TotalSpent += _amount;
                                                            UnpostedInvoice += _amount;
                                                        }
                                                    }
                                                    if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
                                                    {
                                                        if (_amount < 0)
                                                        {
                                                            TotalSpent -= _amount;
                                                            UnpostedInvoice -= _amount;
                                                        }
                                                    }
                                                }

                                        //entered and/or edited (but still unposted) disbursements
                                        parameters[0] = 11;
                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
                                        ds = new DataSet();
                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                                        if (ds.Tables.Count > 0)
                                            if (ds.Tables[0].Rows.Count > 0)
                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
                                                {
                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//v_dbtcrd"
                                                    if ((_debitCredit == "DB" && incrementWithCredit == "N") || (_debitCredit == "CR" && incrementWithCredit == "Y"))
                                                    {
                                                        if (_amount > 0)
                                                        {
                                                            TotalSpent += _amount;
                                                            CommittedAmount += _amount;
                                                        }
                                                    }
                                                    if ((_debitCredit == "DB" && incrementWithCredit == "Y") || (_debitCredit == "CR" && incrementWithCredit == "N"))
                                                    {
                                                        if (_amount < 0)
                                                        {
                                                            TotalSpent -= _amount;
                                                            CommittedAmount -= _amount;
                                                        }
                                                    }
                                                }

                                        //UNPOSTED GENERAL JOURNAL
                                        parameters[0] = 12;
                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
                                        ds = new DataSet();
                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                                        if (ds.Tables.Count > 0)
                                            if (ds.Tables[0].Rows.Count > 0)
                                                foreach (DataRow dr1 in ds.Tables[0].Rows)
                                                {
                                                    int _amount = (dr1[8] == DBNull.Value) ? 0 : Convert.ToInt32(dr1[8]);//"v_amount"
                                                    string _debitCredit = (dr1[9] == DBNull.Value) ? "" : dr1[9].ToString().Trim();//"v_dbtcrd"
                                                    if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
                                                    {
                                                        if (_amount > 0)
                                                        {
                                                            TotalSpent += _amount;
                                                            CommittedAmount += _amount;
                                                        }
                                                    }
                                                    if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
                                                    {
                                                        if (_amount < 0)
                                                        {
                                                            TotalSpent -= _amount;
                                                            CommittedAmount -= _amount;
                                                        }
                                                    }
                                                }

                                        parameters[0] = 13;
                                        parameters[8] = sAccountnumber;//searchedAccountNumber;
                                        decimal totAmount = 0;
                                        ds = new DataSet();
                                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
                                        if (ds.Tables.Count > 0)
                                            if (ds.Tables[0].Rows.Count > 0)
                                                totAmount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][0]);//"v_count"
                                        if (totAmount > 0)
                                            TotalSpent += totAmount;
                                    }
                                }
                        #endregion Calculate Expense

                        if (incrementWithCredit == "Y")
                        {
                            AllowedAmount = TotalSpent;
                            TotalSpent -= AmountRequested;
                        }
                        else if (incrementWithCredit == "N")
                        {
                            AllowedAmount = -1 * TotalSpent;
                            TotalSpent += AmountRequested;
                        }

                        if (GoBelowZero == 0 && TotalSpent < 0)
                        {
                            Status = -6;
                            return;
                        }
                        else
                        {
                            Status = 1;
                            return;
                        }
                    }
                }
                else
                {
                    //return true;
                    Status = 1;
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return false;
            Status = 0;
            return;
        }



        //public static bool ValidateKeyValue_Account(string LoginId, string AccountTypeEntered, int AccountNumberEntered, string keyvalueEntered, decimal AmountRequested, string Module)
        //{
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        //    DataSet ds = new DataSet();
        //    int AccountTypeId = 0;
        //    try
        //    {
        //        //get AccountTypeId from AccountType
        //        DVOGLAccountTypeMaintenance objDVOGLAccountTypeMaintenance = new DVOGLAccountTypeMaintenance();
        //        objDVOGLAccountTypeMaintenance.accounttype = AccountTypeEntered;
        //        List<DVOGLAccountTypeMaintenance> listDVOGLAccountTypeMaintenance = BLLGLAccountTypeMaintenance.GetAccountTypeMaintenance(ref objDVOGLAccountTypeMaintenance);
        //        if (listDVOGLAccountTypeMaintenance.Count > 0)
        //            AccountTypeId = listDVOGLAccountTypeMaintenance[0].id;
        //        listDVOGLAccountTypeMaintenance = null;
        //        objDVOGLAccountTypeMaintenance = null;

        //        object[] parameters = new object[9];
        //        parameters[0] = 0;              //@case int,
        //        parameters[1] = string.Empty;   //@LoginId char(20),
        //        parameters[2] = string.Empty;   //@AccountTypeEntered char(10),
        //        parameters[3] = 0;              //@AccountNumberEntered int,
        //        parameters[4] = string.Empty;   //@keyvalueEntered char(10),
        //        parameters[5] = string.Empty;   //@year char(10),
        //        parameters[6] = string.Empty;   //@set char(6),
        //        parameters[7] = string.Empty;   //@month char(2),
        //        parameters[8] = 0;              //@searchedAccountNumber

        //        //if (HasAccountPermission(LoginId, keyvalueEntered, AccountTypeId, Module))

        //        //check user has accouont-permission or not
        //        parameters[0] = 1;
        //        parameters[1] = LoginId;
        //        parameters[2] = AccountTypeEntered;
        //        int acount = 0;
        //        ds = new DataSet();
        //        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //        if (ds.Tables.Count > 0)
        //            if (ds.Tables[0].Rows.Count > 0)
        //                acount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //        //if count greater than 0, means user have permission to use
        //        if (acount > 0)
        //        {
        //            parameters[0] = 2;
        //            parameters[2] = AccountTypeEntered;
        //            parameters[3] = AccountNumberEntered;
        //            string accountCategory = string.Empty;
        //            ds = new DataSet();
        //            ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //            if (ds.Tables.Count > 0)
        //                if (ds.Tables[0].Rows.Count > 0)
        //                    accountCategory = (ds.Tables[0].Rows[0][1] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][1].ToString();//"v_actCat"

        //            if (accountCategory == string.Empty)
        //            {
        //                parameters[0] = 3;
        //                parameters[2] = AccountTypeEntered;
        //                ds = new DataSet();
        //                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                if (ds.Tables.Count > 0)
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                        accountCategory = (ds.Tables[0].Rows[0][1] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][1].ToString();//"v_actCat"
        //            }

        //            if (accountCategory == "F")
        //            {
        //                int loopStatus = 0;
        //            check1: parameters[0] = 4;
        //                parameters[2] = AccountTypeEntered;
        //                parameters[4] = keyvalueEntered;
        //                int count = 0;
        //                int searchedAccountNumber = 0;
        //                ds = new DataSet();
        //                ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                if (ds.Tables.Count > 0)
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                    {
        //                        count = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //                        searchedAccountNumber = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][2]);//"v_actNo"
        //                    }

        //                if (count > 1)
        //                {
        //                    System.Windows.Forms.MessageBox.Show("There can't be more than one associated budget values");
        //                    return false;
        //                }
        //                else if (count < 1)
        //                {
        //                    parameters[0] = 5;
        //                    parameters[2] = AccountTypeEntered;
        //                    int positionsCount = 0;
        //                    int position = 0;
        //                    int length = 0;
        //                    ds = new DataSet();
        //                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                    if (ds.Tables.Count > 0)
        //                        if (ds.Tables[0].Rows.Count > 0)
        //                        {
        //                            positionsCount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //                            position = (ds.Tables[0].Rows[0][3] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][3]);//"v_pos"
        //                            length = (ds.Tables[0].Rows[0][4] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][4]);//"v_lgth"
        //                        }
        //                    if (positionsCount > 0)
        //                    {
        //                        if (keyvalueEntered.Length >= position && keyvalueEntered.Length >= position + length - 1)
        //                        {
        //                            keyvalueEntered = keyvalueEntered.Remove(position - 1, 2);
        //                            keyvalueEntered = keyvalueEntered.Insert(position - 1, (new String('#', length)));
        //                        }
        //                        loopStatus += 1;
        //                        if (loopStatus <= positionsCount)
        //                            goto check1;
        //                    }
        //                    if (searchedAccountNumber <= 0)
        //                    {
        //                        System.Windows.Forms.MessageBox.Show("There isn't account for this keyvalue");
        //                        return false;
        //                    }
        //                }

        //                if (searchedAccountNumber > 0)
        //                {
        //                    //first of all get current year,month and set
        //                    string year = BLLGLWarrantEntryInbwarah.GetCurrentAccountingYear();
        //                    string set = year;
        //                    string month = BLLGLWarrantEntryInbwarah.GetCurrentAccountingMonth();

        //                    parameters[0] = 6;
        //                    parameters[2] = AccountTypeEntered;
        //                    parameters[4] = keyvalueEntered;
        //                    parameters[5] = year;
        //                    parameters[6] = set;
        //                    //parameters[8] = searchedAccountNumber;
        //                    decimal allocateToDate = 0;
        //                    ds = new DataSet();
        //                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                    if (ds.Tables.Count > 0)
        //                        if (ds.Tables[0].Rows.Count > 0)
        //                        {
        //                            searchedAccountNumber = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][2]);//"v_actNo"
        //                            allocateToDate = (ds.Tables[0].Rows[0][5] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][5]);//"v_alocdate"
        //                        }

        //                    if (searchedAccountNumber <= 0)
        //                    {
        //                        System.Windows.Forms.MessageBox.Show("There is no associated account");
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        parameters[0] = 7;
        //                        parameters[5] = year;
        //                        parameters[6] = set;
        //                        parameters[8] = searchedAccountNumber;
        //                        int Amount = 0;
        //                        ds = new DataSet();
        //                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                        if (ds.Tables.Count > 0)
        //                            if (ds.Tables[0].Rows.Count > 0)
        //                            {
        //                                Amount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"
        //                            }

        //                        if (Amount < 0)
        //                            Amount = 0;
        //                        allocateToDate += Amount;

        //                        decimal TotalSpent = 0;
        //                        //for posted GL enteries
        //                        parameters[0] = 8;
        //                        parameters[4] = keyvalueEntered;
        //                        parameters[5] = year;
        //                        parameters[7] = month;
        //                        int sAccountnumber = 0;
        //                        string incrementWithCredit = "";
        //                        decimal spentBalance = 0;
        //                        ds = new DataSet();
        //                        ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                        if (ds.Tables.Count > 0)
        //                            if (ds.Tables[0].Rows.Count > 0)
        //                                foreach (DataRow dr in ds.Tables[0].Rows)
        //                                {
        //                                    sAccountnumber = (ds.Tables[0].Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][2]);//"v_actNo"
        //                                    incrementWithCredit = (ds.Tables[0].Rows[0][6] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][6].ToString();//"v_inccrt"
        //                                    spentBalance = (ds.Tables[0].Rows[0][7] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[7].Rows[0][2]);//"v_balance"

        //                                    if (spentBalance < 0)
        //                                        spentBalance = 0;
        //                                    TotalSpent += spentBalance;

        //                                    //for unposted GL enteries
        //                                    parameters[0] = 9;
        //                                    parameters[8] = searchedAccountNumber;
        //                                    ds = new DataSet();
        //                                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                    if (ds.Tables.Count > 0)
        //                                        if (ds.Tables[0].Rows.Count > 0)
        //                                            foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                            {
        //                                                int _amount = (ds.Tables[0].Rows[0][8] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][8]);//"v_amount"
        //                                                string _debitCredit = (ds.Tables[0].Rows[0][9] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][9].ToString();//"v_dbtcrd"
        //                                                //if (_amount < 0)
        //                                                //    _amount = 0;
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                {
        //                                                    if (_amount > 0)
        //                                                    { TotalSpent += _amount; }
        //                                                }
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                {
        //                                                    if (_amount < 0)
        //                                                    { TotalSpent -= _amount; }
        //                                                }
        //                                            }

        //                                    //for unposted invoices
        //                                    parameters[0] = 10;
        //                                    parameters[8] = searchedAccountNumber;
        //                                    ds = new DataSet();
        //                                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                    if (ds.Tables.Count > 0)
        //                                        if (ds.Tables[0].Rows.Count > 0)
        //                                            foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                            {
        //                                                int _amount = (ds.Tables[0].Rows[0][8] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][8]);//"v_amount"
        //                                                string _debitCredit = (ds.Tables[0].Rows[0][9] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][9].ToString();//"v_dbtcrd"
        //                                                //if (_amount < 0)
        //                                                //    _amount = 0;
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                {
        //                                                    if (_amount > 0)
        //                                                    { TotalSpent += _amount; }
        //                                                }
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                {
        //                                                    if (_amount < 0)
        //                                                    { TotalSpent -= _amount; }
        //                                                }
        //                                            }

        //                                    //entered and/or edited (but still unposted) disbursements
        //                                    parameters[0] = 11;
        //                                    parameters[8] = searchedAccountNumber;
        //                                    ds = new DataSet();
        //                                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                    if (ds.Tables.Count > 0)
        //                                        if (ds.Tables[0].Rows.Count > 0)
        //                                            foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                            {
        //                                                int _amount = (ds.Tables[0].Rows[0][8] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][8]);//"v_amount"
        //                                                string _debitCredit = (ds.Tables[0].Rows[0][9] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][9].ToString();//v_dbtcrd"
        //                                                //if (_amount < 0)
        //                                                //    _amount = 0;
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                {
        //                                                    if (_amount > 0)
        //                                                    { TotalSpent += _amount; }
        //                                                }
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                {
        //                                                    if (_amount < 0)
        //                                                    { TotalSpent -= _amount; }
        //                                                }
        //                                            }

        //                                    //UNPOSTED GENERAL JOURAL
        //                                    parameters[0] = 12;
        //                                    parameters[8] = searchedAccountNumber;
        //                                    ds = new DataSet();
        //                                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                    if (ds.Tables.Count > 0)
        //                                        if (ds.Tables[0].Rows.Count > 0)
        //                                            foreach (DataRow dr1 in ds.Tables[0].Rows)
        //                                            {
        //                                                int _amount = (ds.Tables[0].Rows[0][8] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][8]);//"v_amount"
        //                                                string _debitCredit = (ds.Tables[0].Rows[0][9] == DBNull.Value) ? "" : ds.Tables[0].Rows[0][9].ToString();//"v_dbtcrd"
        //                                                //if (_amount < 0)
        //                                                //    _amount = 0;
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "N") || (_debitCredit == "C" && incrementWithCredit == "Y"))
        //                                                {
        //                                                    if (_amount > 0)
        //                                                    { TotalSpent += _amount; }
        //                                                }
        //                                                if ((_debitCredit == "D" && incrementWithCredit == "Y") || (_debitCredit == "C" && incrementWithCredit == "N"))
        //                                                {
        //                                                    if (_amount < 0)
        //                                                    { TotalSpent -= _amount; }
        //                                                }
        //                                            }

        //                                    parameters[0] = 13;
        //                                    parameters[8] = searchedAccountNumber;
        //                                    decimal totAmount = 0;
        //                                    ds = new DataSet();
        //                                    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
        //                                    if (ds.Tables.Count > 0)
        //                                        if (ds.Tables[0].Rows.Count > 0)
        //                                            totAmount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[0][0]);//"v_count"
        //                                    if (totAmount > 0)
        //                                        TotalSpent += totAmount;
        //                                }


        //                        if (TotalSpent + AmountRequested <= allocateToDate)
        //                        { return true; }
        //                        else
        //                        {
        //                            System.Windows.Forms.MessageBox.Show("Amount beyond the allowed, only " + Convert.ToString(allocateToDate - TotalSpent) + " is allowed.");
        //                            return false;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            { return true; }
        //        }
        //        else
        //        {
        //            System.Windows.Forms.MessageBox.Show("You haven't permission to make Entries for selected Account");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        return false;
        //    }
        //    return false;
        //}

        //22 October, 2008
        public static bool HasAccountPermission(string LoginId, string KeyValue, int AccountTypeId, string Module)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[4];
                parameters[0] = LoginId;
                parameters[1] = KeyValue;
                parameters[2] = AccountTypeId;
                parameters[3] = Module;

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).USER_ACCOUNT_PERMISSION))
                {
                    int count = 0;
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string _tempKV = KeyValue;
                                string _accMask = dr[0].ToString().Trim();
                                try
                                {
                                    for (int i = 0; i < _accMask.Length; i++)
                                    {
                                        if (_accMask[i] == '#')
                                        {
                                            _tempKV = _tempKV.Remove(i, 1);
                                            _tempKV = _tempKV.Insert(i, "#");
                                        }
                                    }
                                }
                                catch { }

                                if (_tempKV == _accMask)
                                    count++;
                            }
                            //dr[0].ToString().Replace("#", "?");
                        }
                    //if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    //    if (ds.Tables[0].Rows[0][0].ToString() != string.Empty)
                    //        if (Convert.ToBoolean(ds.Tables[0].Rows[0][0]))
                    //            return true;
                    if (count > 0)
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


            //DALBaseClass objDalBaseClass = DALBaseClassHelper.GetDAL();
            //DataSet ds = new DataSet();
            //try
            //{
            //    object[] parameters = new object[9];
            //    parameters[0] = 0;              //@case int,
            //    parameters[1] = string.Empty;   //@LoginId char(20),
            //    parameters[2] = string.Empty;   //@AccountTypeEntered char(10),
            //    parameters[3] = 0;              //@AccountNumberEntered int,
            //    parameters[4] = string.Empty;   //@keyvalueEntered char(10),
            //    parameters[5] = string.Empty;   //@year char(10),
            //    parameters[6] = string.Empty;   //@set char(6),
            //    parameters[7] = string.Empty;   //@month char(2),
            //    parameters[8] = 0;              //@searchedAccountNumber


            //    parameters[0] = 1;
            //    parameters[1] = LoginId;
            //    parameters[2] = AccountType;
            //    int permissionOfAccount = 0;
            //    ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayableListingStpinvce), (new DVOPayableListingStpinvce()).VALIDATE_ACCOUNT_ENTRY);
            //    if (ds.Tables.Count > 0)
            //        if (ds.Tables[0].Rows.Count > 0)
            //            permissionOfAccount = (ds.Tables[0].Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0]);//"v_count"

            //    if (permissionOfAccount > 0)
            //        return true;
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManagement.ExceptionManager.Publish(ex);
            //    return false;
            //}
            //return false;
        }
        //22 October, 2008
        public static bool Check_User_Account(string LoginId, string KeyValue, int AccountTypeId, string Module, out int AcdId, out int ApprovalLevel, out string Message)
        {
            AcdId = 0;
            ApprovalLevel = 0;
            Message = string.Empty;

            int _AccountNo = 0;

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object objtransaction = objDALBaseClassHelper.GetTransactionObject();
            try
            {
                if (HasAccountPermission(LoginId, KeyValue, AccountTypeId, Module))
                {
                    object[] parameters = new object[6];
                    parameters[0] = 1;//case
                    parameters[1] = LoginId;//LoginId
                    parameters[2] = KeyValue;//KeyValue
                    parameters[3] = AccountTypeId;//AccountTypeId
                    parameters[4] = Module;//Module
                    parameters[5] = "";//AccountMask

                    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_USER_ACCOUNT))
                    {
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                _AccountNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;
                            }
                        if (_AccountNo <= 0)
                        {
                            AcdId = 0;
                            ApprovalLevel = 0;
                            Message = "Invalid account number!";
                        }
                        else
                        {
                            parameters[0] = 2;//case
                            using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_USER_ACCOUNT))
                            {
                                int _count = 0, _count2 = 0;
                                string _AccMask = string.Empty;

                                if (ds2.Tables.Count > 0)
                                    if (ds2.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in ds2.Tables[0].Rows)
                                        {
                                            _AccMask = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;
                                            _AccMask = _AccMask.Replace('#', '_');

                                            parameters[0] = 3;
                                            parameters[5] = _AccMask;

                                            using (DataSet ds3 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_USER_ACCOUNT))
                                            {
                                                if (ds3.Tables.Count > 0)
                                                    if (ds3.Tables[0].Rows.Count > 0)
                                                    {
                                                        _count = (ds3.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds3.Tables[0].Rows[0][0]) : 0;
                                                    }
                                            }
                                            if (_count > 0)
                                                break;


                                            //dr[2] = _AccMask;
                                        }
                                        //ds.Tables[0].AcceptChanges();

                                        if (_count > 0)
                                        {
                                            parameters[0] = 4;
                                            using (DataSet ds4 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_USER_ACCOUNT))
                                            {
                                                if (ds4.Tables.Count > 0)
                                                    if (ds4.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataRow dr2 in ds4.Tables[0].Rows)
                                                        {
                                                            _AccMask = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;
                                                            _AccMask = _AccMask.Replace('#', '_');

                                                            parameters[0] = 3;
                                                            parameters[5] = _AccMask;

                                                            using (DataSet ds5 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_USER_ACCOUNT))
                                                            {
                                                                if (ds5.Tables.Count > 0)
                                                                    if (ds5.Tables[0].Rows.Count > 0)
                                                                    {
                                                                        _count2 = (ds5.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds5.Tables[0].Rows[0][0]) : 0;
                                                                    }
                                                            }
                                                            if (_count2 > 0)
                                                            {
                                                                AcdId = (ds4.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds4.Tables[0].Rows[0][0]) : 0;
                                                                ApprovalLevel = (ds4.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds4.Tables[0].Rows[0][1]) : 0;
                                                                Message = "OK.";
                                                                break;
                                                            }
                                                        }
                                                    }
                                            }
                                        }
                                    }
                                if (_count <= 0)
                                {
                                    AcdId = 0;
                                    ApprovalLevel = 0;
                                    Message = "You do not have access to expense from this account.";
                                }
                            }
                        }

                        if (AcdId > 0 && ApprovalLevel > 0)
                            return true;


                        //if (ds.Tables.Count > 0)
                        //    if (ds.Tables[0].Rows.Count > 0)
                        //    {
                        //        DataRow dr = ds.Tables[0].Rows[0];
                        //        AcdId = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;
                        //        ApprovalLevel = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;
                        //        Message = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;

                        //        if (AcdId > 0 && ApprovalLevel > 0)
                        //            return true;
                        //    }
                    }
                    parameters = null;
                }
                else
                {
                    Message = "You do not have access to expense from this account.";
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }

            return false;
        }
        //22 October, 2008
        //public static int GetRequiredApprovalLevel(ApprovalTables approvalTable, int DocNo)
        //{
        //    int _requiredApprovalLevel = 0;
        //    try
        //    {
        //        //Get Approval Levels for NonAPChecks
        //        if (approvalTable == ApprovalTables.NonAPChecks)
        //        {
        //            DVOAPCheckProcessingStpcashe objDVOAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
        //            objDVOAPCheckProcessingStpcashe.doc_no = DocNo;
        //            List<DVOAPCheckProcessingStpcashe> listDVOAPCheckProcessingStpcashe=null;// = BLLAPCheckProcessingStpcashe.GetData(ref objDVOAPCheckProcessingStpcashe);
        //            if (listDVOAPCheckProcessingStpcashe.Count > 0)
        //            {
        //                DVOASApprovalClassesInfo objDVOASApprovalClassesInfo = new DVOASApprovalClassesInfo();
        //                objDVOASApprovalClassesInfo.acd_id = listDVOAPCheckProcessingStpcashe[0].acd_id;
        //                List<DVOASApprovalClassesInfo> listDVOASApprovalClassesInfo=null ;//= BLLASClassesInfo.GetApprovalClassesInfoWithDetail(ref objDVOASApprovalClassesInfo);
        //                if (listDVOASApprovalClassesInfo.Count > 0)
        //                {
        //                    DataSet dsAppLev =null;//= BLLASClassesInfo.GetApprovalLevel(ref objDVOASApprovalClassesInfo);

        //                    if (listDVOAPCheckProcessingStpcashe[0].cash_amt >= Convert.ToDecimal(listDVOASApprovalClassesInfo[0].total_amount))
        //                    {
        //                        if (dsAppLev.Tables.Count > 0)
        //                            if (dsAppLev.Tables[0].Rows.Count > 0)
        //                            {
        //                                _requiredApprovalLevel = (dsAppLev.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(dsAppLev.Tables[0].Rows[0][0]) : 0;//"approval_level"
        //                            }
        //                    }

        //                    if (_requiredApprovalLevel <= 0)
        //                    {
        //                        DVOAPCheckProcessingStpcashe objAPCheckProcessingStpcashe = new DVOAPCheckProcessingStpcashe();
        //                        objAPCheckProcessingStpcashe.vend_code = listDVOAPCheckProcessingStpcashe[0].vend_code;
        //                        objAPCheckProcessingStpcashe.pay_to_code = listDVOAPCheckProcessingStpcashe[0].pay_to_code;
        //                        objAPCheckProcessingStpcashe.cash_acct = listDVOAPCheckProcessingStpcashe[0].cash_acct;
        //                        objAPCheckProcessingStpcashe.cash_department = listDVOAPCheckProcessingStpcashe[0].cash_department;
        //                        DVOAPCheckProcessingDetailStpcashd objDVOAPCheckProcessingDetailStpcashd = new DVOAPCheckProcessingDetailStpcashd();
        //                        objDVOAPCheckProcessingDetailStpcashd.doc_no = DocNo;
        //                        List<DVOAPCheckProcessingDetailStpcashd> listDVOAPCheckProcessingDetailStpcashd=null ;//= BLLAPCheckProcessingDetailStpcashd.GetData(ref objAPCheckProcessingStpcashe, ref objDVOAPCheckProcessingDetailStpcashd);
        //                        if (listDVOAPCheckProcessingDetailStpcashd.Count > 0)
        //                        {
        //                            listDVOAPCheckProcessingDetailStpcashd.Sort(new DVOAPCheckProcessingDetailStpcashd_DistAmt_Comparer());
        //                            foreach (DVOAPCheckProcessingDetailStpcashd obj in listDVOAPCheckProcessingDetailStpcashd)
        //                            {
        //                                if (obj.dist_amt >= Convert.ToDecimal(listDVOASApprovalClassesInfo[0].line_amount))
        //                                {
        //                                    if (dsAppLev.Tables.Count > 0)
        //                                        if (dsAppLev.Tables[0].Rows.Count > 0)
        //                                            _requiredApprovalLevel = (dsAppLev.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(dsAppLev.Tables[0].Rows[0][0]) : 0;
        //                                }
        //                            }
        //                        }
        //                        listDVOAPCheckProcessingDetailStpcashd = null;
        //                        objDVOAPCheckProcessingDetailStpcashd = null;
        //                        //}
        //                        //if (_requiredApprovalLevel <= 0)
        //                        //{
        //                        if (dsAppLev.Tables.Count > 0)
        //                            if (dsAppLev.Tables[0].Rows.Count > 0)
        //                                foreach (DataRow dr in dsAppLev.Tables[0].Rows)
        //                                {
        //                                    if (listDVOAPCheckProcessingStpcashe[0].cash_amt >= Convert.ToDecimal(dr[1]))
        //                                        _requiredApprovalLevel = Convert.ToInt32(dr[0]);
        //                                }
        //                    }
        //                    dsAppLev = null;
        //                }
        //                listDVOASApprovalClassesInfo = null;
        //                objDVOASApprovalClassesInfo = null;
        //            }
        //            listDVOAPCheckProcessingStpcashe = null;
        //            objDVOAPCheckProcessingStpcashe = null;
        //        }
        //        //*************  Added by Bharat Dhall [06/03/2009] *************
        //        //Get Approval Level for PurchaseOrders
        //        else if (approvalTable == ApprovalTables.PurchaseOrder)
        //        {
        //            DVOPOPurchaseOrdersStuordre objDVOPOPurchaseOrdersStuordre = new DVOPOPurchaseOrdersStuordre();
        //            objDVOPOPurchaseOrdersStuordre.doc_no = DocNo;
        //            List<DVOPOPurchaseOrdersStuordre> listDVOPOPurchaseOrdersStuordre=null ;//= BLLPOPurchaseOrdersStuordre.GetData(ref objDVOPOPurchaseOrdersStuordre);
        //            if (listDVOPOPurchaseOrdersStuordre.Count > 0)
        //            {
        //                DVOASApprovalClassesInfo objDVOASApprovalClassesInfo = new DVOASApprovalClassesInfo();
        //                objDVOASApprovalClassesInfo.acd_id = listDVOPOPurchaseOrdersStuordre[0].acd_id;
        //                List<DVOASApprovalClassesInfo> listDVOASApprovalClassesInfo=null;// = BLLASClassesInfo.GetApprovalClassesInfoWithDetail(ref objDVOASApprovalClassesInfo);
        //                if (listDVOASApprovalClassesInfo.Count > 0)
        //                {
        //                    DataSet dsAppLev=null;// = BLLASClassesInfo.GetApprovalLevel(ref objDVOASApprovalClassesInfo);

        //                    if (listDVOPOPurchaseOrdersStuordre[0].total_amount >= Convert.ToDecimal(listDVOASApprovalClassesInfo[0].total_amount))
        //                    {
        //                        if (dsAppLev.Tables.Count > 0)
        //                            if (dsAppLev.Tables[0].Rows.Count > 0)
        //                            {
        //                                _requiredApprovalLevel = (dsAppLev.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(dsAppLev.Tables[0].Rows[0][0]) : 0;//"approval_level"
        //                            }
        //                    }

        //                    if (_requiredApprovalLevel <= 0)
        //                    {
        //                        DVOPOPurchaseOrdersDetailStuordrd objDVOPOPurchaseOrdersDetailStuordrd = new DVOPOPurchaseOrdersDetailStuordrd();
        //                        objDVOPOPurchaseOrdersDetailStuordrd.doc_no = DocNo;
        //                        List<DVOPOPurchaseOrdersDetailStuordrd> listDVOPOPurchaseOrdersDetailStuordrd=null ;//= BLLPOPurchaseOrdersDetailStuordrd.GetData(ref objDVOPOPurchaseOrdersDetailStuordrd);
        //                        if (listDVOPOPurchaseOrdersDetailStuordrd.Count > 0)
        //                        {
        //                            listDVOPOPurchaseOrdersDetailStuordrd.Sort(new DVOPOPurchaseOrdersDetailStuordrd_NetPrice_Comparer());
        //                            foreach (DVOPOPurchaseOrdersDetailStuordrd obj in listDVOPOPurchaseOrdersDetailStuordrd)
        //                            {
        //                                if (obj.net_price >= Convert.ToDecimal(listDVOASApprovalClassesInfo[0].line_amount))
        //                                {
        //                                    if (dsAppLev.Tables.Count > 0)
        //                                        if (dsAppLev.Tables[0].Rows.Count > 0)
        //                                            _requiredApprovalLevel = (dsAppLev.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(dsAppLev.Tables[0].Rows[0][0]) : 0;
        //                                }
        //                            }
        //                        }
        //                        listDVOPOPurchaseOrdersDetailStuordrd = null;
        //                        objDVOPOPurchaseOrdersDetailStuordrd = null;

        //                        if (dsAppLev.Tables.Count > 0)
        //                            if (dsAppLev.Tables[0].Rows.Count > 0)
        //                                foreach (DataRow dr in dsAppLev.Tables[0].Rows)
        //                                {
        //                                    if (listDVOPOPurchaseOrdersStuordre[0].total_amount >= Convert.ToDecimal(dr[1]))
        //                                        _requiredApprovalLevel = Convert.ToInt32(dr[0]);
        //                                }
        //                    }
        //                    dsAppLev = null;
        //                }
        //                listDVOASApprovalClassesInfo = null;
        //                objDVOASApprovalClassesInfo = null;
        //            }
        //            listDVOPOPurchaseOrdersStuordre = null;
        //            objDVOPOPurchaseOrdersStuordre = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //        return 0;
        //    }
        //    return _requiredApprovalLevel;
        //}

        ///// <summary>
        /// Get Account-Number of Treasury Bill Account
        /// </summary>
        /// <returns></returns>
        public static int GetTreasuryBillAccountNumber()
        {
            int TreasuryBillAccountNumber = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[0];
                object obj = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOCommonEntities()).TREASURY_BILL_ACCNT_NO);
                if (obj != null)
                    if (obj.ToString() != string.Empty)
                        TreasuryBillAccountNumber = Convert.ToInt32(obj);

                obj = null;
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return TreasuryBillAccountNumber;
        }
        //********************************************************************************************
        //************************** ADDED BY ROHIT WADHWA ON 18th of October 2008***************
        ///     This routine takes the G/L transaction data as arguments,
        ///and either posts to G/L or checks for an ok posting.
        /// It is designed to be run in "CHECK" mode during the edit list phase,
        ///and in "POST" mode during the posting phase.
        /// All routines that post into the  general ledger module should
        /// do so via this routine.
        /// This line is called once for each G/L transaction
        /// 
        #region POSTING TRANSACTION TO GENERAL LEDGER
        public static DVOPostGLGlobal Gl_post(ref DVOPostGL ObjPostGLTransaction, ref DVOPostGLGlobal ObjPostGLTransactionGlobal, ref object objTrx)
        {
            //  status and description variables returned in the post_gl record:
            // 0 - Successful
            // 1 - Cannot Read G/L Control Table.
            //     - return false
            //3- Document Number out of Sequence
            //    - (warning condition only) - continue
            //4- Duplicate Document Number Exists
            //   return false
            //5 -  Cannot Insert a New Document
            // return false
            // 6 Account Not Listed in Chart of Accounts
            //   -  return false
            //This process is designed to be run in conjunction with the gl_last()
            // library function that checks to make sure all credits match all debits and returns false if they don't.
            int Status = 0;
            string Doc_Status = "N";
            int getid;      // something to get a rowid into
            Int32 wait_win; //: has wait window been opened?,
            Int32 wait_counter;// number of times to wait
            Int32 trx_status;
            bool sql_error = false;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //   object objTransaction = new object();
            //     objTransaction =objDALBaseClassHelper.GetTransactionObject();
            Object[] Parameters = new Object[7];
            Parameters[0] = null;
            Parameters[1] = null;
            Parameters[2] = null;
            Parameters[3] = null;
            Parameters[4] = null;
            Parameters[5] = null;
            Parameters[6] = null;


            //  object objTransaction = objDALBaseClassHelper.GetTransactionObject();

            try
            {
                if (ObjPostGLTransaction.doc_no == ObjPostGLTransactionGlobal.next_doc_no)
                {
                    // return ObjPostGLTransactionGlobal;
                }
                else
                {
                    if (ObjPostGLTransactionGlobal.next_doc_no != -100)
                    {
                        if (ObjPostGLTransactionGlobal.next_doc_no != ObjPostGLTransaction.doc_no + 1)
                        {
                            if (ObjPostGLTransaction.post_or_check == "CHECK")
                            {
                                ObjPostGLTransactionGlobal.status = 3;
                                ObjPostGLTransactionGlobal.description = "Document Number out of Sequence";
                            }

                        }
                    }
                    string[] period = new string[2];
                    period = what_period(ObjPostGLTransaction.doc_date);
                    ObjPostGLTransactionGlobal.pstmonth = period[0].ToString();
                    ObjPostGLTransactionGlobal.pstyear = period[1].ToString();
                    DVOPostTrx ObjPostTransactions = new DVOPostTrx();
                    ObjPostTransactions.orig_journal = ObjPostGLTransaction.orig_journal;
                    ObjPostTransactions.post_or_check = ObjPostGLTransaction.post_or_check;
                    ObjPostTransactions.doc_no = ObjPostGLTransaction.doc_no;
                    ObjPostTransactions.post_no = ObjPostGLTransaction.post_no;
                    ObjPostTransactions.post_date = ObjPostGLTransaction.post_date;
                    ObjPostTransactions.doc_date = ObjPostGLTransaction.doc_date;
                    ObjPostTransactions.ref_code = ObjPostGLTransaction.ref_code;
                    ObjPostTransactions.doc_desc = ObjPostGLTransaction.doc_desc;
                    //Modified by Sarvjeet On 14/05/2009 added a Transactions parameter into trx_post function
                    trx_status = trx_post(ref ObjPostTransactions, ref objTrx);
                    if (trx_status == 1)
                    {
                        ObjPostGLTransactionGlobal.status = 5;
                        ObjPostGLTransactionGlobal.description = "Cannot Insert A New Document.,Stxtranr";
                        return ObjPostGLTransactionGlobal;

                    }
                    else if (trx_status == 2)
                    {
                        ObjPostGLTransactionGlobal.status = 4;
                        ObjPostGLTransactionGlobal.description = "Duplicate Document Number Exists.,(stxtranr)";
                        return ObjPostGLTransactionGlobal;
                    }

                    if (ObjPostGLTransaction.post_or_check == "POST")
                    {
                        // insert the stgtranr record
                        //*****************************************************************
                        // Modified by Sarvjeet, ModifiedDate - 24/11/2008, 
                        // Description - To set size of parameters array,
                        // because previous Parameter count did not match with procedure's parameter count
                        //*****************************************************************

                        //*****************************************************************
                        // Added by Sarvjeet On  15/05/2009
                        // Description - To Check  doc_no exist in Stgtranr ,If exist then
                        // do not insert.   
                        //*****************************************************************
                        //check if this is a new document.
                        bool IsExist = false;
                        object[] chkparameter = new object[2];
                        chkparameter[0] = ObjPostGLTransaction.orig_journal;
                        chkparameter[1] = ObjPostGLTransaction.doc_no;
                        object doc_no = objDalBaseClass.ExecuteScalar(ref chkparameter, ObjPostTransactions.GET_GTRANRDOCNO);
                        if (doc_no != null)
                        {
                            if (doc_no.ToString().Trim() != string.Empty)
                            {
                                IsExist = true;
                            }
                        }
                        if (!IsExist)
                        {
                            object[] parameters = new object[5];
                            parameters[0] = ObjPostGLTransaction.orig_journal;
                            parameters[1] = ObjPostGLTransaction.doc_no;
                            parameters[2] = period[0];
                            parameters[3] = period[1];
                            parameters[4] = Doc_Status;
                            object result1 = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, (new DVOGLTransH()).INSERT_STGTRANR, true);
                            if (Convert.ToInt32(result1) != 1)
                            {
                                sql_error = true;
                            }
                        }
                    }
                }
                string[] Acctdetails = new string[2];
                Acctdetails = AcctChrTn(ObjPostGLTransaction.acct_no);
                ObjPostGLTransactionGlobal.acct_desc = Acctdetails[0].ToString();
                if (Acctdetails[1] != string.Empty)
                {
                    ObjPostGLTransactionGlobal.incr_with_crdt = Acctdetails[1].ToString();
                }
                ObjPostGLTransactionGlobal.signed_amount = ObjPostGLTransaction.amount;
                if (ObjPostGLTransaction.debit_credit == "D")
                {
                    if (ObjPostGLTransactionGlobal.incr_with_crdt == "CR")
                    {
                        ObjPostGLTransactionGlobal.signed_amount = ObjPostGLTransaction.amount * -(1);

                    }
                    ObjPostGLTransactionGlobal.db_accum = ObjPostGLTransactionGlobal.db_accum + ObjPostGLTransaction.amount;
                }
                else
                {
                    if (ObjPostGLTransactionGlobal.incr_with_crdt == "DB")
                    {
                        ObjPostGLTransactionGlobal.signed_amount = ObjPostGLTransaction.amount * -(1);

                    }
                    ObjPostGLTransactionGlobal.cr_accum = ObjPostGLTransactionGlobal.cr_accum + ObjPostGLTransaction.amount;
                }
                if (ObjPostGLTransactionGlobal.acct_desc == "NOT FOUND")
                {
                    if (ObjPostGLTransaction.acct_no == 0)
                    {
                        ObjPostGLTransactionGlobal.description = "Account Not Found";
                    }
                    else
                    {
                        ObjPostGLTransactionGlobal.description = "Account Not Found :" + ObjPostGLTransaction.acct_no.ToString();
                    }
                    ObjPostGLTransactionGlobal.status = 6;

                }
                if (ObjPostGLTransaction.post_or_check != "POST")
                {
                    //ObjPostGLTransactionGlobal.status = 0;
                }
                else
                {
                    // insert the stgactvd row
                    // Modified by - Sarvjeet, ModifiedDate - 24/11/2008, 
                    // Description - To set size of parameters array,
                    // because previous Parameter count did not match with procedure's parameter count.
                    //**********************************************
                    //Added by Sarvjeet Vrema On 28/09/09 to prevent duplicate record insertion.......
                    //if (CheckIntoStgactvd(ref ObjPostGLTransaction))

                    object[] stgactvdParameters = new object[6];
                    stgactvdParameters[0] = ObjPostGLTransaction.orig_journal;
                    stgactvdParameters[1] = ObjPostGLTransaction.doc_no;
                    stgactvdParameters[2] = ObjPostGLTransaction.acct_no;
                    stgactvdParameters[3] = ObjPostGLTransaction.department;
                    stgactvdParameters[4] = ObjPostGLTransaction.amount;
                    stgactvdParameters[5] = ObjPostGLTransaction.debit_credit;
                    object result = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref stgactvdParameters, (new DVOGLTRanActVD()).INSERT_STGACTVD, true);
                    if (Convert.ToInt32(result) != 1)
                    {
                        sql_error = true;
                    }

                    //Check if account_no exist in stxckrgr then make an entry into stxchrgd. 
                    Parameters[0] = ObjPostGLTransaction.orig_journal;
                    Parameters[1] = ObjPostGLTransaction.doc_no;
                    Parameters[2] = ObjPostGLTransaction.acct_no;
                    Parameters[3] = ObjPostGLTransaction.department;
                    if (ObjPostGLTransaction.inv_chk_no.Trim().Length > 10)
                        Parameters[4] = ObjPostGLTransaction.inv_chk_no.Trim().Substring(0, 10);
                    else
                        Parameters[4] = ObjPostGLTransaction.inv_chk_no.Trim();
                    Parameters[5] = ObjPostGLTransaction.amount;
                    Parameters[6] = ObjPostGLTransaction.debit_credit;

                    object Insresult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref Parameters, ObjPostGLTransaction.INSERT_PY_STXCKRGD, true);
                    if (Insresult == DBNull.Value || Insresult == null || Insresult.ToString().Trim().Length <= 0 || Convert.ToInt32(Insresult) != 1)
                    {
                        throw new Exception("Error has occurred while posting into (stxchrgd).");
                    }

                    //**************************************************************************************
                    // insert the stxckrgd row if there is a row in stxckrgr
                    // for this account number.  Also, try to obtain a lock
                    // on the row so nobody can update while the insert is occuring.
                    // try to get a lock...
                    // Modified by - Sarvjeet, ModifiedDate - 24/11/2008, 
                    // Description - To set size of parameters array,
                    // because previous Parameter count did not match with procedure's parameter count.
                    //object[] param = new object[2];
                    //param[0] = ObjPostGLTransaction.acct_no;
                    //if (ObjPostGLTransaction.department.Trim() != string.Empty)
                    //    param[1] = ObjPostGLTransaction.department.Trim();
                    //else
                    //    param[1] = "000";
                    //object acct_no = objDalBaseClass.ExecuteScalar(ref param, ObjPostGLTransaction.GET_STXCKRGR);
                    //if (acct_no != null)
                    //{
                    //    if (acct_no.ToString().Trim() != string.Empty)
                    //    {
                    //    locking:
                    //        int i = BLLCommonUtilities.LockCurrentRecord(ref objTrx, "stxckrgr", ObjPostGLTransaction.acct_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false);
                    //        if (i == 1)
                    //        {

                    //            //*******************************************************
                    //            Parameters[0] = ObjPostGLTransaction.orig_journal;
                    //            Parameters[1] = ObjPostGLTransaction.doc_no;
                    //            Parameters[2] = ObjPostGLTransaction.acct_no;
                    //            Parameters[3] = ObjPostGLTransaction.department;
                    //            if (ObjPostGLTransaction.inv_chk_no.Trim().Length > 10)
                    //                Parameters[4] = ObjPostGLTransaction.inv_chk_no.Trim().Substring(0, 10);
                    //            else
                    //                Parameters[4] = ObjPostGLTransaction.inv_chk_no.Trim();
                    //            Parameters[5] = ObjPostGLTransaction.amount;
                    //            Parameters[6] = ObjPostGLTransaction.debit_credit;
                    //            try
                    //            {
                    //                object Insresult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref Parameters, ObjPostGLTransaction.INSERT_STXCKRGD, true);
                    //                if (Convert.ToInt32(Insresult) != 1)
                    //                {
                    //                    sql_error = true;
                    //                }
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, "stxckrgr", ObjPostGLTransaction.acct_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    //                throw ex;
                    //            }
                    //            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, "stxckrgr", ObjPostGLTransaction.acct_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    //        }
                    //        else
                    //        {
                    //            if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You want to wait to release record?", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                    //            {
                    //                System.Threading.Thread.Sleep(10000);
                    //                goto locking;
                    //            }
                    //            else
                    //            {
                    //                sql_error = true;
                    //            }
                    //        }
                    //    }
                    //}
                    //******************************************************************
                }
                if (sql_error)
                {
                    ObjPostGLTransactionGlobal.sql_error = 1;
                }
                else
                {
                    ObjPostGLTransactionGlobal.sql_error = 0;
                }
            }

            catch (Exception ex)
            {
                ex.HelpLink = ObjPostGLTransaction.doc_no.ToString();
                throw ex;
            }
            finally
            {
            }
            return ObjPostGLTransactionGlobal;



        }


        // Written By Rohit on 19th of October 2008
        // This function is passed a transaction date from which it
        // determines the accounting period and year which are returned.

        //public static string[] what_period(DateTime ObjTrxDate)
        //{
        //    DVOCommonEntities ObjCommonEntities = new DVOCommonEntities();
        //    string[] period = new string[3];
        //    period[0] = null;
        //    period[1] = null;
        //    period[2] = null;
        //    object[] parameters = new object[1];
        //    parameters[0] = ObjTrxDate;
        //    DataSet ds = null;
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

        //    ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), ObjCommonEntities.GETACCTPERIOD);

        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            period[0] = ds.Tables[0].Rows[0][0].ToString();
        //            period[1] = ds.Tables[0].Rows[0][1].ToString();
        //            period[2] = ObjTrxDate.ToString().ToString();
        //        }
        //    }
        //    else
        //    {
        //        period[0] = null;
        //        period[1] = null;
        //        period[2] = null;
        //    }
        //    return period;
        //}

        public static string[] what_period(DateTime ObjTrxDate)
        {
            string[] period = new string[2];
            period[0] = string.Empty;
            period[1] = string.Empty;
            if (dsStxperdr == null || dsStxperdr.Tables.Count<=0)
            {
                GetAllStxperdr();
            }
            if (dsStxperdr != null && dsStxperdr.Tables[0].Rows.Count > 0)
            {
                string trxDate = ObjTrxDate.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DataRow[] dra = dsStxperdr.Tables[0].Select("start_date<=#" + trxDate + "# and end_date>=#" + trxDate + "#");
                if (dra.Length > 0)
                {
                    period[0] = dra[0][0].ToString();
                    period[1] = dra[0][1].ToString();
                }
            }
            return period;
        }

        // Written By Rohit on 18th of October 2008
        //This routine purpose is to check the G/L
        //  posting for accuracy before committing work.

        public static bool gl_last(ref DVOPostGLGlobal ObjPostGLTransactionGlobal)
        {
            //returns True or False , True of Debits total Match Credit else False
            bool Status;
            if (ObjPostGLTransactionGlobal.cr_accum == ObjPostGLTransactionGlobal.db_accum)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }
            return Status;
        }
        //Written By Rohit on 20th of October 2008
        // This function is passed an account number, and from
        // that, it returns that accounts description, and it's
        //type (either "CR" or "DB" type)

        public static string[] AcctChrTn(Int32 acct_no)
        {
            DVOCommonEntities ObjCommonEntities = new DVOCommonEntities();
            string[] Acctdetails = new string[2];
            try
            {

                //execute the stored procedure "uspaccchrtn" to get Account
                DataSet ds = null;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                object[] parameters = new object[1];
                parameters[0] = acct_no;
                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), ObjCommonEntities.GETACCOUNTDETAILS);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Acctdetails[0] = ds.Tables[0].Rows[0][0].ToString().Trim();
                        Acctdetails[1] = ds.Tables[0].Rows[0][1].ToString().Trim();

                    }
                }
                else
                {
                    Acctdetails[0] = null;
                    Acctdetails[1] = null;

                }
                if ((Acctdetails[0] == null && Acctdetails[1] == null) || (Acctdetails[0].ToString() == string.Empty && Acctdetails[1].ToString() == string.Empty))
                {
                    Acctdetails[0] = "NOT FOUND";
                    Acctdetails[1] = "";
                }
                else
                {
                    if (Acctdetails[1].Trim().ToString() == "Y")
                    {
                        Acctdetails[1] = "CR";
                    }
                    else if (Acctdetails[1].Trim().ToString() == "N")
                    {
                        Acctdetails[1] = "DB";
                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {


            }
            return Acctdetails;
        }

        // Written By Rohit on 18th of October 2008

        ///This routine posts a row to stxtranr for each document.  It is called
        ///by each library posting function (eg. gl_post, ar_post and so on) once
        ///for each document.  It is designed to be run in "CHECK" mode during
        ///the edit list phase, and in "POST" mode during the posting phase.  (In
        ///CHECK mode, it does nothing.) Because this routine may be called more
        ///than once for a single transaction, it must detect when a new document
        ///starts, and only post then.

        //Modified by Sarvjeet On 14/05/2009 added a Transactions parameter into trx_post
        public static int trx_post(ref DVOPostTrx ObjPostTransactions, ref object objtrx)
        {
            ///This routine takes the G/L transaction data as arguments,
            ///and either posts to G/L or checks for an ok posting.
            ///It is designed to be run in "CHECK" mode during the edit list phase,
            ///and in "POST" mode during the posting phase.

            ///All routines that post into the  general ledger module should
            ///do so via this routine.
            ///This line is called once for each G/L transaction
            ///#  Data elements passed:
            ///    post_or_check           "POST" or "CHECK"  tells trx_post to post
            ///                              a row or do nothing (for edit lists).
            ///    orig_journal            original journal (AR, OE, etc...).
            ///    doc_no                  document number (sequential and congruent
            ///                              for each document within a journal).
            ///    post_no                 posting number.
            ///    post_date               posting date.
            ///    doc_date                document entry date.
            ///    ref_code                reference (customer, vendor, employee etc.)
            ///    doc_desc                document description.
            ///
            ///  Return values:
            ///         0 = if "CHECK" or if "POST" and posted ok
            /// What this process does:
            ///    if "CHECK" then just return.
            ///    if "POST" then insert the row into stxtranr
            ///    
            //  string p_prepared = "Y";


            //******************************************************************************
            //******************************************************************************
            //Modified by Sarvjeet On 14/05/2009 to insert data by Transaction..............
            // This function is being used in BLLPayrollFunctions,BLLAP_Post,BLLGeneralLibrary.
            // that's why changes have been made in these file also.
            //******************************************************************************
            string p_orig_journal = "99";
            string p_userid = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                if (ObjPostTransactions.post_or_check != "POST")
                {
                    return 0;
                }
                else if (ObjPostTransactions.post_or_check == "POST")
                {
                    //check if this is a new document.if not, just return.
                    object[] chkparameter = new object[2];
                    chkparameter[0] = ObjPostTransactions.orig_journal;
                    chkparameter[1] = ObjPostTransactions.doc_no;
                    //chkparameter[2] = ObjPostTransactions.post_no;    
                    //chkparameter[3] = ObjPostTransactions.post_date; 
                    //chkparameter[4] = ObjPostTransactions.doc_date;  

                    object doc_no = objDALBaseClass.ExecuteScalar(ref chkparameter, ObjPostTransactions.GET_XTRANRDOCNO);
                    if (doc_no != null)
                    {
                        if (doc_no.ToString().Trim() != string.Empty)
                            return 0;
                    }
                    p_orig_journal = ObjPostTransactions.orig_journal;
                    if (p_orig_journal == "GJ" && ObjPostTransactions.ref_code == "GENJRN")
                        p_userid = DVOApplicationUserInfo.LoginId;
                    else
                        p_userid = "";

                    object[] parameters = new object[8];
                    parameters[0] = ObjPostTransactions.orig_journal;   // orig_journal char(2) not null ,
                    parameters[1] = ObjPostTransactions.doc_no;         //doc_no integer not null ,
                    parameters[2] = ObjPostTransactions.post_no;        //post_no integer,
                    parameters[3] = ObjPostTransactions.post_date;      //post_date date,
                    parameters[4] = ObjPostTransactions.doc_date;       //doc_date date not null ,
                    if (ObjPostTransactions.ref_code.Trim().Length > 6)
                        ObjPostTransactions.ref_code = ObjPostTransactions.ref_code.Substring(0, 5);
                    parameters[5] = ObjPostTransactions.ref_code.Trim();//ref_code char(6),
                    if (ObjPostTransactions.doc_desc.Trim().Length > 30)
                        ObjPostTransactions.doc_desc = ObjPostTransactions.doc_desc.Substring(0, 29);
                    parameters[6] = ObjPostTransactions.doc_desc.Trim(); //doc_desc char(30),
                    parameters[7] = p_userid;
                    //Modified by Sarvjeet On 14/05/2009 to insert data by Transaction..............
                    object Result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objtrx, ref parameters, ObjPostTransactions.INSERT_SPNAME, true);
                    if (Result != null)
                        if (Result.ToString().Trim() != string.Empty)
                            return Convert.ToInt32(Result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 1;
        }
        // Written by Sarvjeet on 26 of Nov. 2008
        //  Update the journal entry or mark it as posted if auto-reverse.
        public static bool UpdateStgjoure(ref DVOPostGL objDVOPostGL, ref object objTrx)
        {
            bool status = true;
            object[] Parameters = new object[4];
            Parameters[0] = objDVOPostGL.doc_no;
            Parameters[1] = "REVERSE OF:" + objDVOPostGL.doc_no.ToString();
            Parameters[2] = objDVOPostGL.Auto_rev;
            Parameters[3] = objDVOPostGL.post_or_check;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object i = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref Parameters, objDVOPostGL.UPDATE_STGJOURE, true);
            if (Convert.ToInt32(i) != 1)
            {
                status = false;
            }
            return status;

        }

        #endregion

        public static int Auto_Next(string table_name, string column_name, ref object objTrx)
        {
            //This function increments a column in a control table by 1,
            //locks the column (fetch first) in the table, then builds the sql script 
            //to increment the column based upon column_name.  If it cannot obtain a 
            //lock within 5 seconds, it then pulls up a window
            //with the message:
            int next_number = 0;
            int rownum = 0;
            int Columnval = 0;
            int i = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTrx == null)
            {
                objTrx = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DVOCommonEntities ObjDvoCommonentities = new DVOCommonEntities();
            try
            {
                object[] parameters = new object[4];
                parameters[0] = table_name;
                parameters[1] = column_name;
                while (i == 0)
                {

                    i = BLLCommonUtilities.LockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                    if (i == 1)
                    {
                        DataSet ds = new DataSet();
                        ds = objDALBaseClass.GetData(ObjDvoCommonentities.AutonextgetQuery(ref parameters));
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                {
                                    rownum = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                                }
                                if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                                {
                                    Columnval = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                                }
                                parameters[2] = rownum;
                                parameters[3] = Columnval + 1;
                                next_number = Columnval + 1;
                                //Update has to be Done .(ref parameters, typeof(DVOCommonEntities)
                                objDALBaseClass.ExecuteQuery_ByTransaction(ref objTrx, ObjDvoCommonentities.AutoNextUpdateQuery(ref parameters));
                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                if (!statusObjTransaction && objTrx != null)
                                    objDALBaseClassHelper.CommitTransaction(ref objTrx);
                            }
                        }
                    }
                    else
                    {
                        //if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You want to wait to release record?", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                        //{
                        //    System.Threading.Thread.Sleep(10000);

                        //}
                        //else
                        //{
                        if (!statusObjTransaction && objTrx != null)
                            objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                        return 0;
                        //}
                    }
                }


            }
            catch (Exception ex)
            {
                if (i == 1)
                {
                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                }
                if (!statusObjTransaction && objTrx != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                throw ex;
            }

            return next_number;
        }
        //****************ADDED BY RAHUL JAIN ON 05-FEB-2010 *********************************
        //This function increments a column in a control table by 1,
        //locks the column (fetch first based on search) in the table, then builds the sql script 
        //to increment the column based upon column_name.  If it cannot obtain a 
        //lock within 5 seconds, it then pulls up a window
        //with the message:
        public static int Auto_Next(string table_name, string column_name, string CurrSet, ref object objTrx)
        {
            int next_number = 0;
            int rownum = 0;
            int Columnval = 0;
            int i = 0;
            int UniqueId = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTrx == null)
            {
                objTrx = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DVOUpdateBudgetControlDefaults objDVOUpdateBudgetControlDefaults = new DVOUpdateBudgetControlDefaults();
            try
            {
                object[] Searchparameters = new object[1];
                Searchparameters[0] = CurrSet.Trim();
                DataSet ds = objDALBaseClass.GetData(objDVOUpdateBudgetControlDefaults.GET_ROWID_BY_ACTIVE_SET(ref Searchparameters));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                        {
                            UniqueId = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            if (UniqueId > 0)
            {
                DVOCommonEntities ObjDvoCommonentities = new DVOCommonEntities();
                try
                {
                    object[] parameters = new object[4];
                    parameters[0] = table_name;
                    parameters[1] = column_name;
                    parameters[2] = UniqueId;
                    while (i == 0)
                    {

                        i = BLLCommonUtilities.LockCurrentRecord(ref objTrx, table_name, UniqueId, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                        if (i == 1)
                        {
                            DataSet ds = new DataSet();
                            ds = objDALBaseClass.GetData(ObjDvoCommonentities.AutonextgetQueryBudget(ref parameters));
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                    {
                                        rownum = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                                    }
                                    if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                                    {
                                        Columnval = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                                    }
                                    parameters[2] = rownum;
                                    parameters[3] = Columnval + 1;
                                    next_number = Columnval + 1;
                                    //Update has to be Done .(ref parameters, typeof(DVOCommonEntities)
                                    objDALBaseClass.ExecuteQuery_ByTransaction(ref objTrx, ObjDvoCommonentities.AutoNextUpdateQuery(ref parameters));
                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                    if (!statusObjTransaction && objTrx != null)
                                        objDALBaseClassHelper.CommitTransaction(ref objTrx);
                                }
                            }
                        }
                        else
                        {
                            if (!statusObjTransaction && objTrx != null)
                                objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (i == 1)
                    {
                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    }
                    if (!statusObjTransaction && objTrx != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                    throw ex;
                }
            }
            return next_number;
        }
        //*********************************************END OF RAHUL CODE****************************************



        #region Accounts Payable

        /// <summary>
        /// This Function used to get the new check number by increment 1 of last_chkno in stpcntrc
        /// After Getting the new check number updating table with new check number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_APCheckNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newApCheck = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_APCHECK_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newApCheck = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newAPCheckNo
                    }
                }
                parameters = null;
                return newApCheck;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newApCheck;
        }

        /// <summary>
        /// This Function used to get the new document number by increment 1 of ap_doc_no in stpcntrc
        /// After Getting the new document number updating table with new document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_APDocNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newApDoc = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_APDOC_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newApDoc = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newAPDocNo
                    }
                }
                parameters = null;
                return newApDoc;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newApDoc;
        }

        /// <summary>
        /// This Function used to get the new AP-Post number by increment 1 of ap_post_no in stpcntrc
        /// After Getting the new AP-Post number updating table with new AP-Post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_APPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newApPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_APPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newApPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newBdgPostNo
                    }
                }
                parameters = null;
                return newApPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newApPostNo;
        }

        /// <summary>
        /// This Function used to get the CD-Document check number by increment 1 of cd_post_no in stpcntrc
        /// After Getting the new CD-Document number updating table with new CD-Document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_AP_CDPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newApCdPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_AP_CDPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newApCdPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newApCdPostNo
                    }
                }
                parameters = null;
                return newApCdPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newApCdPostNo;
        }

        #endregion Accounts Payable

        #region Accounts Receivable

        /// <summary>
        /// This Function used to get the new document number by increment 1 of ar_doc_no in strcntrc
        /// After Getting the new document number updating table with new document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_ARDocNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newArDoc = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_ARDOC_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newArDoc = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newARDocNo
                    }
                }
                parameters = null;
                return newArDoc;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newArDoc;
        }

        /// <summary>
        /// This Function used to get the new CR-Post number by increment 1 of cr_post_no in strcntrc
        /// After Getting the new CR-Post number updating table with new CR-Post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_AR_CRPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newArCrPostno = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_AR_CRPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newArCrPostno = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newARDocNo
                    }
                }
                parameters = null;
                return newArCrPostno;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newArCrPostno;
        }

        #endregion Accounts Receivable

        #region General Ledger

        /// <summary>
        /// This Function used to get the new GeneralJournal-Document number by increment 1 of genjrn_post_no in stgcntrc
        /// After Getting the new GeneralJournal-Document number updating table with new GeneralJournal-Document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_GJPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newGjPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_GJPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newGjPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newGjPostNo
                    }
                }
                parameters = null;
                return newGjPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newGjPostNo;
        }

        /// <summary>
        /// This Function used to get the new GeneralLedger-Post number by increment 1 of genled_post_no in stgcntrc
        /// After Getting the new GeneralLedger-Post number updating table with new GeneralLedger-Post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_GLPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newGlPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_GLPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newGlPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newGlPostNo
                    }
                }
                parameters = null;
                return newGlPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newGlPostNo;
        }

        /// <summary>
        /// This Function used to get the new GeneralJournal-Document number by increment 1 of genjrn_doc_no in stgcntrc
        /// After Getting the new GeneralJournal-Document number updating table with new GeneralJournal-Document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_GJDocNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newGjDocNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_GJDOCNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newGjDocNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newGJDocNo
                    }
                }
                parameters = null;
                return newGjDocNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newGjDocNo;
        }

        #endregion General Ledger

        #region Inventory Control

        /// <summary>
        /// This Function used to get the new post number by increment 1 of post_no in sticntrc
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_ICPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newIcPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_ICPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newIcPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newIcPostNo
                    }
                }
                parameters = null;
                return newIcPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newIcPostNo;
        }

        /// <summary>
        /// This Function used to get the new document number by increment 1 of doc_no in sticntrc
        /// After Getting the new document number updating table with new document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_ICDocNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newIcDocNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_ICDOCNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newIcDocNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newIcDocNo
                    }
                }
                parameters = null;
                return newIcDocNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newIcDocNo;
        }

        #endregion Inventory Control

        #region NSS

        /// <summary>
        /// This Function used to get the new post number by increment 1 of nss_post_no in nsscontrol
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_NSSPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newNssPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_NSSPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newNssPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newNSSPostNo
                    }
                }
                parameters = null;
                return newNssPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newNssPostNo;
        }

        #endregion NSS

        #region Order Entry

        /// <summary>
        /// This Function used to get the new post number by increment 1 of oe_post_no in stocntrc
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_OEPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newOePostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_OEPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newOePostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newOePostNo
                    }
                }
                parameters = null;
                return newOePostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newOePostNo;
        }

        #endregion Order Entry

        #region Payroll

        /// <summary>
        /// This Function used to get the new post number by increment 1 of py_post_no in stycntrc
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_PYPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newPyPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_PYPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newPyPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newPYPostNo
                    }
                }
                parameters = null;
                return newPyPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newPyPostNo;
        }

        /// <summary>
        /// This Function used to get the new document number by increment 1 of py_doc_no in stycntrc
        /// After Getting the new document number updating table with new document number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_PYDocNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newPyDocNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_PYDOCNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newPyDocNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newPYDocNo
                    }
                }
                parameters = null;
                return newPyDocNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newPyDocNo;
        }

        #endregion Payroll

        #region Purchasing

        /// <summary>
        /// This Function used to get the new receipt-post number by increment 1 of rec_post_no in stucntrc
        /// After Getting the new receipt-post number updating table with new receipt-post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_PU_RECPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newPuRecPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_PU_RECPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newPuRecPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newPuRecPostNo
                    }
                }
                parameters = null;
                return newPuRecPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newPuRecPostNo;
        }

        /// <summary>
        /// This Function used to get the new invoice-post number by increment 1 of inv_post_no in stucntrc
        /// After Getting the new invoice-post number updating table with new invoice-post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_PU_INVPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newPuInvPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_PU_INVPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newPuInvPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newPuInvPostNo
                    }
                }
                parameters = null;
                return newPuInvPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newPuInvPostNo;
        }

        #endregion Purchasing

        #region Saving Bank

        /// <summary>
        /// This Function used to get the new post number by increment 1 of sb_post_no in sbcontrols
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_SBPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newSbPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_SBPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newSbPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newSBPostNo
                    }
                }
                parameters = null;
                return newSbPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newSbPostNo;
        }

        #endregion Saving Bank

        #region Treasury Bills

        /// <summary>
        /// This Function used to get the new post number by increment 1 of tb_post_no in tbcntrc
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_TBPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newTbPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_TBPOSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newTbPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newTBPostNo
                    }
                }
                parameters = null;
                return newTbPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newTbPostNo;
        }

        /// <summary>
        /// This Function used to get the new tender-code by increment 1 of current_tend_code in tbcntrc
        /// After Getting the new tender-code updating table with new tender-code
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_TB_TenderCode()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newTbTendCode = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_TB_TENDCODE_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newTbTendCode = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newTbTendCode
                    }
                }
                parameters = null;
                return newTbTendCode;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newTbTendCode;
        }

        /// <summary>
        /// This Function used to get the new receipt number by increment 1 of next_receipt_no in tbcntrc
        /// After Getting the new receipt number updating table with new receipt number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_TB_ReceiptNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newTbReceiptNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_TB_RCPTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newTbReceiptNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newTbReceiptNo
                    }
                }
                parameters = null;
                return newTbReceiptNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newTbReceiptNo;
        }

        #endregion Treasury Bills

        #region Budget Control

        /// <summary>
        /// This Function used to get the new post number by increment 1 of post_seq_no in inbcntrc
        /// After Getting the new post number updating table with new post number
        /// </summary>
        /// <returns></returns>
        public static int Auto_Next_BudgetPostNo()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            int newBdgPostNo = 0;
            try
            {
                object[] parameters = new object[2];
                parameters[0] = DVOApplicationUserInfo.UserId;
                parameters[1] = DVOApplicationUserInfo.MachineInfo;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).NEW_BDG_POSTNO_GET))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        object obj = ds.Tables[0].Rows[0][1] != DBNull.Value ? ds.Tables[0].Rows[0][1] : null;
                        if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0 && Convert.ToInt32(obj) == 1)
                            newBdgPostNo = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][0]) : 0;//newBdgPostNo
                    }
                }
                parameters = null;
                return newBdgPostNo;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return newBdgPostNo;
        }

        #endregion Budget Control







        /// <summary>
        /// This Function used to get the new check number for every check
        /// Before assigning the new check number from the control table (stpcntrc) it has to be lock it
        /// So no any check got the same number.
        /// After Getting the new check number it releasing the lock and updating it with new check number
        /// </summary>
        /// <param name="table_name"></param>
        /// <param name="column_name"></param>
        /// <param name="objTrx"></param>
        /// <returns></returns>
        public static int Auto_Next_With_LockAndSelect(string table_name, string column_name, ref object objTrx)
        {
            //This function increments a column in a control table by 1,
            //locks the column (fetch first) in the table, then builds the sql script 
            //to increment the column based upon column_name.  If it cannot obtain a 
            //lock within 5 seconds, it then pulls up a window
            //with the message:
            int next_number = 0;
            int rownum = 0;
            int Columnval = 0;
            int i = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTrx == null)
            {
                objTrx = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DVOCommonEntities ObjDvoCommonentities = new DVOCommonEntities();
            try
            {
                object[] parameters = new object[4];
                parameters[0] = table_name;
                parameters[1] = column_name;
                while (i == 0)
                {

                    i = BLLCommonUtilities.LockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                    if (i == 1)
                    {
                        DataSet ds = new DataSet();
                        ds = objDALBaseClass.GetData(ObjDvoCommonentities.AutonextgetQuery(ref parameters));
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                {
                                    rownum = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                                }
                                if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                                {
                                    Columnval = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                                }
                                parameters[2] = rownum;
                                parameters[3] = Columnval + 1;
                                next_number = Columnval + 1;
                                //Update has to be Done .(ref parameters, typeof(DVOCommonEntities)
                                objDALBaseClass.ExecuteQuery_ByTransaction(ref objTrx, ObjDvoCommonentities.AutoNextUpdateQuery(ref parameters));
                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                if (!statusObjTransaction && objTrx != null)
                                    objDALBaseClassHelper.CommitTransaction(ref objTrx);
                            }
                        }
                    }
                    else
                    {
                        //if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You want to wait to release record?", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                        //{
                        //    System.Threading.Thread.Sleep(10000);

                        //}
                        //else
                        //{
                            if (!statusObjTransaction && objTrx != null)
                                objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                            return 0;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                if (i == 1)
                {
                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                }
                if (!statusObjTransaction && objTrx != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                throw ex;
            }

            return next_number;
        }

        /// <summary>
        /// This Function used to get the new check number for every check
        /// Before assigning the new check number from the control table (stpcntrc) it has to be lock it
        /// So no any check got the same number.
        /// After Getting the new check number it releasing the lock and updating it with new check number
        /// </summary>
        /// <param name="table_name"></param>
        /// <param name="column_name"></param>
        /// <param name="CountRecord">how much increment you want to get in 'column_name'</param>
        /// <param name="objTrx"></param>
        /// <returns>Current value of 'column_name' in 'table_name'</returns>
        public static int CurrentValue_With_LockAndSelect(string table_name, string column_name, int CountRecord, ref object objTrx)
        {
            //This function increments a column in a control table by CountRecord,
            //locks the column (fetch first) in the table, then builds the sql script 
            //to increment the column based upon column_name.  If it cannot obtain a 
            //lock within 5 seconds, it then pulls up a window
            //with the message:
            int current_number = 0;
            int rownum = 0;
            int Columnval = 0;
            int i = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTrx == null)
            {
                objTrx = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            DVOCommonEntities ObjDvoCommonentities = new DVOCommonEntities();
            try
            {
                object[] parameters = new object[4];
                parameters[0] = table_name;
                parameters[1] = column_name;
                while (i == 0)
                {

                    i = BLLCommonUtilities.LockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false);
                    if (i == 1)
                    {
                        DataSet ds = new DataSet();
                        ds = objDALBaseClass.GetData(ObjDvoCommonentities.AutonextgetQuery(ref parameters));
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                {
                                    rownum = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                                }
                                if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                                {
                                    Columnval = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                                }
                                parameters[2] = rownum;
                                parameters[3] = Columnval + CountRecord;
                                current_number = Columnval + 1;
                                //Update has to be Done .(ref parameters, typeof(DVOCommonEntities)
                                objDALBaseClass.ExecuteQuery_ByTransaction(ref objTrx, ObjDvoCommonentities.AutoNextUpdateQuery(ref parameters));
                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                if (!statusObjTransaction && objTrx != null)
                                    objDALBaseClassHelper.CommitTransaction(ref objTrx);
                            }
                        }
                    }
                    else
                    {
                        if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You want to wait to release record?", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                        {
                            System.Threading.Thread.Sleep(10000);

                        }
                        else
                        {
                            if (!statusObjTransaction && objTrx != null)
                                objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                            return 0;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                if (i == 1)
                {
                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTrx, table_name, 1, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                }
                if (!statusObjTransaction && objTrx != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTrx);
                throw ex;
            }

            return current_number;
        }

        //************************** ADDED BY SARVJEET VERMA ON 2th of December 2008***************
        //G/L Activity to Ledger Posting 
        public static DVOPostGLGlobal GL_Detail_Post(ref DVOPostGLDetail objDVOPostGLDetail, ref DVOPostGLGlobal ObjPostGLDtlTransactionGlobal, ref object objTrx)
        {

            //  status and description variables returned in the post_gl record:
            // 0 - Successful
            // 1 - The previous document has a null or blank department code
            // 2- New row insertion in stxchrtd failed , 
            // 3- updation in stxchrtd failed 
            // 4 Account Not Listed in Chart of Accounts
            //   -  return false
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            bool sel_stxchrtd = true;
            bool post_prior_period = false;
            bool repost = false;
            bool endYear = false;
            bool sql_error = false;
            try
            {
                objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                if (objDVOPostGLDetail.debit_credit == "C")
                {
                    ObjPostGLDtlTransactionGlobal.cr_accum = ObjPostGLDtlTransactionGlobal.cr_accum + objDVOPostGLDetail.amount;
                }
                else
                {
                    ObjPostGLDtlTransactionGlobal.db_accum = ObjPostGLDtlTransactionGlobal.db_accum + objDVOPostGLDetail.amount;
                }
                if (objDVOPostGLDetail.post_or_check == "POST")
                {
                    string[] Acctdetails = new string[2];
                    if (objDVOPostGLDetail.acct_no != 0)
                    {
                        Acctdetails = AcctChrTn(objDVOPostGLDetail.acct_no);
                        ObjPostGLDtlTransactionGlobal.acct_desc = Acctdetails[0].ToString();
                        if (ObjPostGLDtlTransactionGlobal.acct_desc == "NOT FOUND")
                        {
                            if (objDVOPostGLDetail.acct_no == 0)
                            {
                                ObjPostGLDtlTransactionGlobal.description = "Account Not Found";
                            }
                            else
                            {
                                ObjPostGLDtlTransactionGlobal.description = "Account Not Found :" + objDVOPostGLDetail.acct_no.ToString();
                            }
                            ObjPostGLDtlTransactionGlobal.status = 4;
                        }
                    }
                    else
                    {
                        ObjPostGLDtlTransactionGlobal.description = "";
                    }
                    if (Acctdetails[1] != string.Empty)
                    {
                        ObjPostGLDtlTransactionGlobal.incr_with_crdt = Acctdetails[1].ToString();
                    }


                    if (((objDVOPostGLDetail.debit_credit == "C") && (ObjPostGLDtlTransactionGlobal.incr_with_crdt == "CR")) || ((objDVOPostGLDetail.debit_credit == "D") && (ObjPostGLDtlTransactionGlobal.incr_with_crdt == "DB")))
                    {
                        ObjPostGLDtlTransactionGlobal.signed_amount = objDVOPostGLDetail.amount;

                    }
                    else
                    {
                        ObjPostGLDtlTransactionGlobal.signed_amount = objDVOPostGLDetail.amount * -(1);
                    }
                    object[] parameters = new object[4];
                    parameters[0] = objDVOPostGLDetail.acct_no;
                    parameters[1] = objDVOPostGLDetail.department;
                    parameters[2] = objDVOPostGLDetail.period_month;
                    parameters[3] = objDVOPostGLDetail.period_year;
                    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOPostGLDetail), objDVOPostGLDetail.FIND_SPNAME))
                    {
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            sel_stxchrtd = false;
                        }
                        else if (ds.Tables[0].Rows.Count == 1)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() != null && ds.Tables[0].Rows[0][0].ToString() != string.Empty)
                                objDVOPostGLDetail.Activity = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
                            if (ds.Tables[0].Rows[0][1].ToString() != null && ds.Tables[0].Rows[0][1].ToString() != string.Empty)
                                objDVOPostGLDetail.balance = Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString());
                            if (ds.Tables[0].Rows[0][2].ToString() != null && ds.Tables[0].Rows[0][2].ToString() != string.Empty)
                                objDVOPostGLDetail.this_month = Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString());
                            if (ds.Tables[0].Rows[0][3].ToString() != null && ds.Tables[0].Rows[0][3].ToString() != string.Empty)
                                objDVOPostGLDetail.budget = Convert.ToDecimal(ds.Tables[0].Rows[0][3].ToString());
                        }
                        if (!sel_stxchrtd)
                        {
                            //# period for account not found in stxchrtd - create new row
                            object[] Insparametrs = new object[4];
                            Insparametrs[0] = objDVOPostGLDetail.acct_no;
                            Insparametrs[1] = objDVOPostGLDetail.department;
                            Insparametrs[2] = objDVOPostGLDetail.Curr_month;
                            Insparametrs[3] = objDVOPostGLDetail.Curr_Year;
                            object result = objDALBaseClass.InsertData_ByTransaction(ref objTrx, ref Insparametrs, typeof(DVOPostGLDetail), true);
                            if (Convert.ToInt32(result) != 1)
                            {
                                sql_error = true;
                                //rollback
                            }
                            DataSet dsfresh = objDALBaseClass.GetData(ref parameters, typeof(DVOPostGLDetail), objDVOPostGLDetail.FIND_SPNAME);
                            if (dsfresh.Tables[0].Rows.Count == 1)
                            {
                                if (dsfresh.Tables[0].Rows[0][0].ToString() != null && dsfresh.Tables[0].Rows[0][0].ToString() != string.Empty)
                                    objDVOPostGLDetail.Activity = Convert.ToDecimal(dsfresh.Tables[0].Rows[0][0].ToString());
                                if (dsfresh.Tables[0].Rows[0][1].ToString() != null && dsfresh.Tables[0].Rows[0][1].ToString() != string.Empty)
                                    objDVOPostGLDetail.balance = Convert.ToDecimal(dsfresh.Tables[0].Rows[0][1].ToString());
                                if (dsfresh.Tables[0].Rows[0][2].ToString() != null && dsfresh.Tables[0].Rows[0][2].ToString() != string.Empty)
                                    objDVOPostGLDetail.this_month = Convert.ToDecimal(dsfresh.Tables[0].Rows[0][2].ToString());
                                if (dsfresh.Tables[0].Rows[0][3].ToString() != null && dsfresh.Tables[0].Rows[0][3].ToString() != string.Empty)
                                    objDVOPostGLDetail.budget = Convert.ToDecimal(dsfresh.Tables[0].Rows[0][3].ToString());
                            }
                        }
                    }
                }

                // ObjPostGLDtlTransactionGlobal.signed_amount = objDVOPostGLDetail.amount;


                // Department code must be a character or number

                if (objDVOPostGLDetail.department == null || objDVOPostGLDetail.department == string.Empty)
                {

                    ObjPostGLDtlTransactionGlobal.status = 1;

                    //"****The previous document has a null or blank department code.
                }

                //--------- determine if the row already exists in stxchrtd
                if (objDVOPostGLDetail.post_or_check == "POST")
                {


                    // post to either stxchrtd.this_month or stxchrtd.activity
                    // period 00 transactions (end of year adjusting entries) are
                    // created when a revenue or expense account has been posted to in
                    // a prior year.  These are the only transactions posted directly to
                    // stxchrtd.activity
                    if (objDVOPostGLDetail.period_month == "00")
                    {
                        if (objDVOPostGLDetail.Activity == 0.0M)
                        {
                            objDVOPostGLDetail.Activity = ObjPostGLDtlTransactionGlobal.signed_amount;
                        }
                        else
                        {
                            objDVOPostGLDetail.Activity = objDVOPostGLDetail.Activity + ObjPostGLDtlTransactionGlobal.signed_amount;
                        }

                    }
                    else
                    {
                        // all non-period 00 transactions post to stxchrtd.this_mont
                        if (objDVOPostGLDetail.this_month == 0.0M)
                        {
                            objDVOPostGLDetail.this_month = ObjPostGLDtlTransactionGlobal.signed_amount;
                        }
                        else
                        {
                            objDVOPostGLDetail.this_month = objDVOPostGLDetail.this_month + ObjPostGLDtlTransactionGlobal.signed_amount;


                        }

                    }
                    // # calculate the stxchrtd.balance column. in most cases updating
                    //# stxchrtd.balance would not be necessary since p_recalc will update
                    //# account balances. (however, p_recalc cannot recalculate the balance
                    //# for the record. that represents the first period/year under g/l
                    //# control for an account/department) and it is possible that p_genled
                    //# may post to that record.

                    objDVOPostGLDetail.balance = objDVOPostGLDetail.balance + ObjPostGLDtlTransactionGlobal.signed_amount;

                    // # update the stxchrtd record

                    object[] Updparametrs = new object[8];
                    Updparametrs[0] = objDVOPostGLDetail.acct_no;
                    Updparametrs[1] = objDVOPostGLDetail.department;
                    Updparametrs[2] = objDVOPostGLDetail.period_month;
                    Updparametrs[3] = objDVOPostGLDetail.period_year;
                    Updparametrs[4] = objDVOPostGLDetail.Activity;
                    Updparametrs[5] = Convert.ToDecimal(objDVOPostGLDetail.balance);
                    Updparametrs[6] = objDVOPostGLDetail.this_month;
                    Updparametrs[7] = objDVOPostGLDetail.budget;

                    object updresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref Updparametrs, objDVOPostGLDetail.updstxchtd, true);
                    if (Convert.ToInt32(updresult) != 1)
                    {
                        sql_error = true;
                        //rollback
                        // call end_report("**** An SQL error has occured while posting.")
                    }

                    //  if posting to an expense or revenue account in a prior year an
                    // adjusting transaction must be created for period 00 of the
                    // following year. the balancing posting (period 00) is to retained
                    // earnings. these adjusting entries result in all expense and
                    // revenue accounts having a zero beginning balance in the first
                    // period of the year.

                    // each adjusting transaction is posted to the g/l activity
                    // tables (stxtranr, stgtranr, & stgactvd) with orig_journal of "YE",
                    // stgtranr.status of "N" (not posted) and stxtranr.post_date set
                    // to today.  the next run of p_genled, will post these transactions.

                    // these adjusting transactions are similar to the transaction
                    // created by the begin a new year processing that occurs when a year
                    // is closed out.
                    if (objDVOPostGLDetail.period_year.Trim() != string.Empty && objDVOPostGLDetail.period_month.Trim() != string.Empty)
                    {
                        if (Convert.ToInt32(objDVOPostGLDetail.period_year) < Convert.ToInt32(objDVOPostGLDetail.Curr_Year))
                        {
                            post_prior_period = true;

                            int stxcntrcincome = ReportingUtilities.GetIncome();
                            if (objDVOPostGLDetail.acct_no >= stxcntrcincome)
                            {
                                if (!post_retained(repost, endYear, objDVOPostGLDetail, objTrx))
                                {
                                    sql_error = true;

                                }

                            }

                        }
                        else
                        {
                            if (Convert.ToInt32(objDVOPostGLDetail.period_month) < Convert.ToInt32(objDVOPostGLDetail.Curr_month))
                            {
                                post_prior_period = true;
                            }

                        }
                    }
                }
                if (sql_error)
                {
                    ObjPostGLDtlTransactionGlobal.sql_error = 1;
                }
                else
                {
                    ObjPostGLDtlTransactionGlobal.sql_error = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjPostGLDtlTransactionGlobal;

        }
        /// <summary>
        ///  this function creates a new end of year type transaction for the
        /// prior year's retained earnings account and the current account.
        ///  it is only called if posting an income statement type account into
        ///  a prior year.
        public static bool post_retained(bool repost, bool end_year, DVOPostGLDetail objDVOPostGLDetail, object objTrx)
        {

            // if this program was invoked by the reposting program (run from
            // the system administration menu), then do not under any
            // circumstances create the period "00" adjusting entries.
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            if (repost == true)
            {
                return true;
            }

            // if this program was invoked with the -2 flag then end_year
            // was set to true in ml_defaults (second running of p_genled).
            // in this case don't create another period 00 adjusting entry to
            // retained earnings and the expense/revenue account(s) involved.
            // this situation could only arise when posting an expense or revenue
            // account into a period over 2 years prior to the current year.
            // this would be a rare circumstance, but it must be handled.

            if (end_year == true)
            {
                return true;
            }
            //to be implimented
            //post the transaction header records:
            try
            {
                DVOGLstxtranr ObjGLstxtranr = new DVOGLstxtranr();
                DVOGLstgtranr ObjGLstgtranr = new DVOGLstgtranr();
                ObjGLstxtranr.orig_journal = "YE";
                ObjGLstgtranr.orig_journal = "YE";
                //get the next doc_no
                object[] parameter = new object[0];
                ObjGLstxtranr.doc_no = Convert.ToInt32(ObjDALBaseClass.ExecuteScalar(ref parameter, ObjGLstxtranr.GET_NEXT_DOC_NO));
                ObjGLstgtranr.doc_no = ObjGLstxtranr.doc_no;
                ObjGLstxtranr.post_no = objDVOPostGLDetail.post_no;
                ObjGLstxtranr.post_date = DVOApplicationUserInfo.CurrentDate;
                ObjGLstgtranr.acct_period = "00";
                ObjGLstgtranr.acct_year = Convert.ToString(Convert.ToInt32(objDVOPostGLDetail.period_year) + 1);
                // set doc_date to first day of the 00 period
                object[] stgtranrparameter = new object[2];
                stgtranrparameter[0] = ObjGLstgtranr.acct_period;
                stgtranrparameter[1] = ObjGLstgtranr.acct_year;
                object _start_date = ObjDALBaseClass.ExecuteScalar(ref stgtranrparameter, (new DVOGLBeginPeriod()).Find_Master_periodsStartDate);
                if (_start_date == null || _start_date.ToString() == string.Empty)
                {
                    ObjGLstxtranr.doc_date = DVOApplicationUserInfo.CurrentDate;
                }
                else
                {
                    ObjGLstxtranr.doc_date = Convert.ToDateTime(_start_date);
                }

                ObjGLstxtranr.ref_code = objDVOPostGLDetail.doc_no.ToString();
                ObjGLstxtranr.doc_desc = ObjGLstxtranr.orig_journal + " ADJ: POST INTO PRIOR YEAR";
                ObjGLstgtranr.status = "N";
                //insert into stxtranr
                if (!InsertIntoStxtranr(ref ObjGLstxtranr, ref objTrx))
                {
                    return false;
                }
                if (!InsertIntoStgtranr(ref ObjGLstgtranr, ref objTrx))
                {
                    return false;
                }
                //post the first detail line into stgactvd
                DVOGLstgactvd objDVOGLstgactvd = new DVOGLstgactvd();
                objDVOGLstgactvd.orig_journal = ObjGLstxtranr.orig_journal;
                objDVOGLstgactvd.doc_no = ObjGLstxtranr.doc_no;
                objDVOGLstgactvd.acct_no = objDVOPostGLDetail.acct_no;
                objDVOGLstgactvd.department = objDVOPostGLDetail.department;
                objDVOGLstgactvd.amount = objDVOPostGLDetail.amount;
                if (objDVOPostGLDetail.debit_credit.Trim() == "D")
                {
                    objDVOGLstgactvd.debit_credit = "C";
                }
                else
                {
                    objDVOGLstgactvd.debit_credit = "D";
                }
                if (!InsertIntoStgactvd(ref objDVOGLstgactvd, ref objTrx))
                {
                    return false;
                }
                // post the second detail line into stgactvd
                DVOUpdateLedgerDefaults objDVOUpdateLedgerDefaults = new DVOUpdateLedgerDefaults();
                objDVOUpdateLedgerDefaults.part = "H";
              //  objDVOUpdateLedgerDefaults = BLLUpdateLedgerDefaults.GetLedgerDefaultInfo(ref objDVOUpdateLedgerDefaults);
                objDVOGLstgactvd.acct_no = objDVOUpdateLedgerDefaults.retain_earnings;
                objDVOGLstgactvd.department = "000";
                objDVOGLstgactvd.amount = objDVOPostGLDetail.amount;
                objDVOGLstgactvd.debit_credit = objDVOPostGLDetail.debit_credit;
                if (!InsertIntoStgactvd(ref objDVOGLstgactvd, ref objTrx))
                {
                    return false;
                }


            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// This function accepts a flex key account number and account type
        /// and determines whether or not the corresponding budget will be
        /// exceeded if the requested amount is spent.  A warning message is
        /// printed if the budget would be exceeded.
        /// The return value from the function is TRUE if the budget information
        /// for the keyvalue was found and the requested expenditure will not
        /// exceed the budget limit.  Otherwise returns FALSE.
        /// </summary>
        /// <param name="p_Keyvalue"></param>
        /// <param name="p_AccountType"></param>
        /// <param name="p_AmountRequested"></param>
        /// <param name="ActiveBudgetYear"></param>
        /// <param name="ActiveBudgetSet"></param>
        /// <param name="CurrentMonth"></param>
        /// <param name="CurrentYear"></param>
        /// <returns></returns>
        public static bool CheckExpense(string p_Keyvalue, string p_AccountType, decimal p_AmountRequested, string ActiveBudgetYear, string ActiveBudgetSet, string CurrentMonth, string CurrentYear)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            string budget_key = string.Empty;
            decimal allocatedtodate = 0;
            decimal total_spent = 0;
            int acct_no = 0;
            string incr_with_crdt = string.Empty;
            decimal balance = 0;
            decimal amount = 0;
            string debit_credit = string.Empty;
            string staBudgetCheck = string.Empty;

            try
            {
                // get the level of budget checking from stxparmd
                staBudgetCheck = GetBudgetCheckingLevel("gl", "budget_checking");
                // if we are not checking budget limits, then just return
                if (staBudgetCheck == "N")
                    return true;

                //determine the budget keyvalue
                budget_key = Find_Budget_Key(p_Keyvalue, p_AccountType);
                if (budget_key == null || budget_key == string.Empty)
                {
                    //either warn the user and let them continue, or else prevent the user
                    //from continuing, depending upon the level of budget checking
                    //added the test for staBudgetCheck = "E" and allow the user to continue
                    //if the budget check is "W" if they so decide
                    if (staBudgetCheck == "E")
                        return false;
                    else if (staBudgetCheck == "W")
                    {
                        if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You are about to create an account without a budget account.", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                            return true;
                        else
                            return false;
                    }
                }

                object[] parameters = new object[8];
                parameters[0] = 0;//case

                //find the amount that can be spent by all accounts drawing from the budget account
                parameters[0] = 1;
                parameters[1] = budget_key;
                parameters[2] = p_AccountType;
                parameters[3] = ActiveBudgetYear;
                parameters[4] = ActiveBudgetSet;
                parameters[5] = 0;
                parameters[6] = "";
                parameters[7] = "";

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            allocatedtodate = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToDecimal(ds.Tables[0].Rows[0][0]) : 0;
                            acct_no = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                        }
                        if (ds.Tables[0].Rows.Count <= 0 || allocatedtodate <= 0)
                        {
                            if (staBudgetCheck == "E")
                                return false;
                            else if (staBudgetCheck == "W")
                            {
                                if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You are about to create an account without a budget account.", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                                    return true;
                                else
                                    return false;
                            }
                        }
                    }
                }

                //find the budget adjustments that may lower the amount available
                parameters[0] = 2;
                parameters[3] = ActiveBudgetYear;
                parameters[4] = ActiveBudgetSet;
                parameters[5] = acct_no;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            amount = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToDecimal(ds.Tables[0].Rows[0][0]) : 0;
                }
                // "amount" in the following stmt is less than or equal to zero
                allocatedtodate = allocatedtodate + amount;
                total_spent = 0;

                budget_key = budget_key.Replace("#", "_");

                parameters[0] = 3;
                parameters[1] = budget_key;
                parameters[6] = CurrentMonth;
                parameters[7] = CurrentYear;
                using (DataSet ds1 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                {
                    balance = 0;
                    acct_no = 0;
                    incr_with_crdt = string.Empty;
                    // add the account balance to the total amount spent by all accounts
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                balance = (dr[0] != DBNull.Value) ? Convert.ToDecimal(dr[0]) : 0;
                                acct_no = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;
                                incr_with_crdt = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;
                                if (balance < 0) balance = 0;

                                total_spent = total_spent + balance;

                                // fetch the GL entries that have not yet been posted to account balances
                                parameters[0] = 4;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                // fetch the entered and/or edited (but still unposted) invoices
                                parameters[0] = 5;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                // get the entered and/or edited (but still unposted) disbursements
                                parameters[0] = 6;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                parameters[0] = 7;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                parameters[0] = 8;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                total_spent = total_spent + amount;
                                            }
                                }
                            }
                    }

                    if ((total_spent + p_AmountRequested) <= allocatedtodate)
                    {
                        return true;
                    }
                }
                //message_window
                System.Windows.Forms.MessageBox.Show("The expenditures for accounts falling under budget-account " + p_AccountType + " " + budget_key.Replace('_', '#') + " are currently at " + total_spent.ToString() + ".\nThe requested amount exceeds the budget limit of " + allocatedtodate.ToString() + ".");

                //either warn the user and let them continue, or else prevent the user
                //from continuing, depending upon the level of budget checking
                //added the test for staBudgetCheck = "E" and allow the user to continue
                //if the budget check is "W" if they so decide
                if (staBudgetCheck == "E")
                    return false;

                if (staBudgetCheck == "W")
                {
                    if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Continuing will exceed the budget limit.", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }
        /// <summary>
        /// This function accepts a flex key account number and account type
        /// and determines whether or not the corresponding budget will be
        /// exceeded if the requested amount is spent.  A warning message is
        /// printed if the budget would be exceeded.
        /// The return value from the function is TRUE if the budget information
        /// for the keyvalue was found and the requested expenditure will not
        /// exceed the budget limit.  Otherwise returns FALSE.
        /// </summary>
        /// <param name="p_Keyvalue"></param>
        /// <param name="p_AccountType"></param>
        /// <param name="p_AmountRequested"></param>
        /// <param name="ActiveBudgetYear"></param>
        /// <param name="ActiveBudgetSet"></param>
        /// <param name="CurrentMonth"></param>
        /// <param name="CurrentYear"></param>
        /// <returns></returns>
        public static bool CheckExpense(string p_Keyvalue, string p_AccountType, decimal p_AmountRequested, string ActiveBudgetYear, string ActiveBudgetSet, string CurrentMonth, string CurrentYear, out string strMessage)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            string budget_key = string.Empty;
            decimal allocatedtodate = 0;
            decimal total_spent = 0;
            int acct_no = 0;
            string incr_with_crdt = string.Empty;
            decimal balance = 0;
            decimal amount = 0;
            string debit_credit = string.Empty;
            string staBudgetCheck = string.Empty;
            strMessage = string.Empty;

            try
            {
                // get the level of budget checking from stxparmd
                staBudgetCheck = GetBudgetCheckingLevel("gl", "budget_checking");
                // if we are not checking budget limits, then just return
                if (staBudgetCheck == "N")
                    return true;

                //determine the budget keyvalue
                budget_key = Find_Budget_Key(p_Keyvalue, p_AccountType);
                if (budget_key == null || budget_key == string.Empty)
                {
                    //either warn the user and let them continue, or else prevent the user
                    //from continuing, depending upon the level of budget checking
                    //added the test for staBudgetCheck = "E" and allow the user to continue
                    //if the budget check is "W" if they so decide
                    if (staBudgetCheck == "E")
                        return false;
                    else if (staBudgetCheck == "W")
                    {
                        if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You are about to create an account without a budget account.", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                            return true;
                        else
                            return false;
                    }
                }

                object[] parameters = new object[8];
                parameters[0] = 0;//case

                //find the amount that can be spent by all accounts drawing from the budget account
                parameters[0] = 1;
                parameters[1] = budget_key;
                parameters[2] = p_AccountType;
                parameters[3] = ActiveBudgetYear;
                parameters[4] = ActiveBudgetSet;
                parameters[5] = 0;
                parameters[6] = "";
                parameters[7] = "";

                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            allocatedtodate = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToDecimal(ds.Tables[0].Rows[0][0]) : 0;
                            acct_no = (ds.Tables[0].Rows[0][1] != DBNull.Value) ? Convert.ToInt32(ds.Tables[0].Rows[0][1]) : 0;
                        }
                        if (ds.Tables[0].Rows.Count <= 0 || allocatedtodate <= 0)
                        {
                            if (staBudgetCheck == "E")
                                return false;
                            else if (staBudgetCheck == "W")
                            {
                                if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("You are about to create an account without a budget account.", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                                    return true;
                                else
                                    return false;
                            }
                        }
                    }
                }

                //find the budget adjustments that may lower the amount available
                parameters[0] = 2;
                parameters[3] = ActiveBudgetYear;
                parameters[4] = ActiveBudgetSet;
                parameters[5] = acct_no;
                using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            amount = (ds.Tables[0].Rows[0][0] != DBNull.Value) ? Convert.ToDecimal(ds.Tables[0].Rows[0][0]) : 0;
                }
                // "amount" in the following stmt is less than or equal to zero
                allocatedtodate = allocatedtodate + amount;
                total_spent = 0;

                budget_key = budget_key.Replace("#", "_");

                parameters[0] = 3;
                parameters[1] = budget_key;
                parameters[6] = CurrentMonth;
                parameters[7] = CurrentYear;
                using (DataSet ds1 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                {
                    balance = 0;
                    acct_no = 0;
                    incr_with_crdt = string.Empty;
                    // add the account balance to the total amount spent by all accounts
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                balance = (dr[0] != DBNull.Value) ? Convert.ToDecimal(dr[0]) : 0;
                                acct_no = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;
                                incr_with_crdt = (dr[2] != DBNull.Value) ? dr[2].ToString().Trim() : string.Empty;
                                if (balance < 0) balance = 0;

                                total_spent = total_spent + balance;

                                // fetch the GL entries that have not yet been posted to account balances
                                parameters[0] = 4;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                // fetch the entered and/or edited (but still unposted) invoices
                                parameters[0] = 5;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                // get the entered and/or edited (but still unposted) disbursements
                                parameters[0] = 6;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                parameters[0] = 7;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                debit_credit = (dr2[2] != DBNull.Value) ? dr2[2].ToString().Trim() : string.Empty;

                                                if ((debit_credit == "D" && incr_with_crdt == "N") || (debit_credit == "C" && incr_with_crdt == "Y"))
                                                    if (amount > 0)
                                                        total_spent = total_spent + amount;

                                                if ((debit_credit == "D" && incr_with_crdt == "Y") || (debit_credit == "C" && incr_with_crdt == "N"))
                                                    if (amount < 0)
                                                        total_spent = total_spent - amount;
                                            }
                                }

                                parameters[0] = 8;
                                parameters[5] = acct_no;
                                using (DataSet ds2 = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).CHECK_EXPENSE))
                                {
                                    if (ds2.Tables.Count > 0)
                                        if (ds2.Tables[0].Rows.Count > 0)
                                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                            {
                                                amount = (dr2[0] != DBNull.Value) ? Convert.ToDecimal(dr2[0]) : 0;
                                                total_spent = total_spent + amount;
                                            }
                                }
                            }
                    }

                    if ((total_spent + p_AmountRequested) <= allocatedtodate)
                    {
                        return true;
                    }
                }
                //message_window
                //System.Windows.Forms.MessageBox.Show("The expenditures for accounts falling under budget-account " + p_AccountType + " " + budget_key.Replace('_', '#') + " are currently at " + total_spent.ToString() + ".\nThe requested amount exceeds the budget limit of " + allocatedtodate.ToString() + ".");
                strMessage = "The expenditures for accounts falling under budget-account " + p_AccountType + " " + budget_key.Replace('_', '#') + " are currently at " + total_spent.ToString() + ".\nThe requested amount exceeds the budget limit of " + allocatedtodate.ToString() + ".";
                //either warn the user and let them continue, or else prevent the user
                //from continuing, depending upon the level of budget checking
                //added the test for staBudgetCheck = "E" and allow the user to continue
                //if the budget check is "W" if they so decide
                if (staBudgetCheck == "E")
                    return false;

                if (staBudgetCheck == "W")
                {
                    if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Continuing will exceed the budget limit.", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
            return false;
        }
        /// <summary>
        /// To get the level of budget checking from stxparmd
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="AccessKey"></param>
        /// <returns></returns>
        public static string GetBudgetCheckingLevel(string Module, string AccessKey)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = Module;
                parameters[1] = AccessKey;
                object obj = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOCommonEntities()).GET_BUDGET_CHECKING_LEVEL);

                if (obj != DBNull.Value && obj != null)
                    return obj.ToString().Trim();
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }
            return "";
        }
        /// <summary>
        /// To Determine the Budget-Keyvalue using Keyvalue & AccountType
        /// </summary>
        /// <param name="Keyvalue"></param>
        /// <param name="AccountType"></param>
        /// <returns></returns>
        public static string Find_Budget_Key(string Keyvalue, string AccountType)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[3];
                parameters[0] = 0;//case
                parameters[1] = AccountType;//acct_type
                parameters[2] = Keyvalue;//keyvalue

                int count = 0;
                int position = 0, length = 0;

                parameters[0] = 1;
                object obj = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOCommonEntities()).FIND_BUDGET_KEY);

                if (obj != DBNull.Value && obj != null)
                    count = Convert.ToInt32(obj);

                if (count == 1)
                    return Keyvalue;
                else if (count > 1)
                    return "";
                else
                {
                    obj = null;
                    count = 0;
                    parameters[0] = 2;
                    using (DataSet ds = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities), (new DVOCommonEntities()).FIND_BUDGET_KEY))
                    {
                        if (ds.Tables.Count > 0)
                            if (ds.Tables[0].Rows.Count > 0)
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    position = (dr[0] != DBNull.Value) ? Convert.ToInt32(dr[0]) : 0;
                                    length = (dr[1] != DBNull.Value) ? Convert.ToInt32(dr[1]) : 0;
                                    StringBuilder _kvalue = new StringBuilder(Keyvalue);
                                    for (int i = 0; i < length; i++)
                                    {
                                        _kvalue.Insert(position - 1 + i, "#");
                                        _kvalue.Remove(position + i, 1);
                                    }

                                    parameters[0] = 3;
                                    parameters[2] = _kvalue.ToString();
                                    obj = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOCommonEntities()).FIND_BUDGET_KEY);
                                    if (obj != DBNull.Value && obj != null)
                                        count = Convert.ToInt32(obj);

                                    if (count == 1)
                                        return Keyvalue;
                                    else if (count > 1)
                                        return "";
                                }
                    }
                }
                obj = null;
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }

            return "";
        }
        /// <summary>
        /// To Get Actual KeyValue for entered Keyvalue and Account type
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="AccountType"></param>
        /// <returns></returns>
        public static string GetActualKeyvalue(string KeyValue, string AccountType)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                KeyValue = KeyValue.Replace('#', '_');
                //for (int i = 0; i < KeyValue.Length; i++)
                //{
                //    if (KeyValue[i] == '#')
                //    {
                //        KeyValue = KeyValue.Insert(i, "_");
                //        KeyValue = KeyValue.Remove(i + 1, 1);
                //    }
                //}
                object[] parameters = new object[2];
                parameters[0] = KeyValue;
                parameters[1] = AccountType;
                object obj = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOCommonEntities()).GET_ACTUAL_KEYVALUE);

                parameters = null;
                objDALBaseClass = null;

                if (obj != DBNull.Value && obj != null)
                    return obj.ToString().Trim();
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }
            return "";
        }
        public static bool InsertIntoStgtranr(ref DVOGLstgtranr objDVOGLstgtranr, ref object objTrx)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameter = new object[5];
                parameter[0] = objDVOGLstgtranr.orig_journal;
                parameter[1] = objDVOGLstgtranr.doc_no;
                parameter[2] = objDVOGLstgtranr.acct_period;
                parameter[3] = objDVOGLstgtranr.acct_year;
                parameter[4] = objDVOGLstgtranr.status;
                object result = ObjDALBaseClass.InsertData_ByTransaction(ref objTrx, ref parameter, typeof(DVOGLstgtranr), true);
                if (result.ToString().Trim() != "1")
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool InsertIntoStxtranr(ref DVOGLstxtranr objDVOGLstxtranr, ref object objTrx)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameter = new object[8];
                parameter[0] = objDVOGLstxtranr.orig_journal;
                parameter[1] = objDVOGLstxtranr.doc_no;
                parameter[2] = objDVOGLstxtranr.post_no;
                parameter[3] = objDVOGLstxtranr.post_date;
                parameter[4] = objDVOGLstxtranr.doc_date;
                parameter[5] = objDVOGLstxtranr.ref_code;
                parameter[6] = objDVOGLstxtranr.doc_desc;
                parameter[7] = "";
                object result = ObjDALBaseClass.InsertData_ByTransaction(ref objTrx, ref parameter, typeof(DVOGLstxtranr), objDVOGLstxtranr.INSERT_STXTRANR);
                if (result.ToString().Trim() != "1")
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }

            return true;
        }
        public static bool InsertIntoStgactvd(ref DVOGLstgactvd objDVOGLstgactvd, ref object objTrx)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[6];
                parameters[0] = objDVOGLstgactvd.orig_journal;
                parameters[1] = objDVOGLstgactvd.doc_no;
                parameters[2] = objDVOGLstgactvd.acct_no;
                parameters[3] = objDVOGLstgactvd.department;
                parameters[4] = objDVOGLstgactvd.amount;
                parameters[5] = objDVOGLstgactvd.debit_credit;
                object result = ObjDALBaseClass.InsertData_ByTransaction(ref objTrx, ref parameters, typeof(DVOGLstgactvd), true);
                if (result.ToString().Trim() != "1")
                {
                    return false;
                }

            }
            catch
            {
                return false;

            }
            return true;
        }
        ///***************************************Added by Sarvjeet Verma On 08/04/2009**********************************
        /// <summary>
        /// deletes all stxtranr data that does not have activity
        /// This function will delete all stxtranr data that
        /// have no joins to any other st?tranr tables
        /// </summary>
        public static bool Delete_Trx(DateTime del_date, object ObjTrx)
        {
            try
            {
                // get  data from stxtranr <= del_date
                object[] parameter = new object[1];
                parameter[0] = del_date;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DataSet temds = objDalBaseClass.GetData(ref parameter, typeof(DVODeleteOldActivity), (new DVODeleteOldActivity()).Gettranr);

                //get all table name that matches "st?tranr"
                DataSet xtranrTables = objDalBaseClass.GetAllData(typeof(DVODeleteOldActivity));
                DataTable objxtranr = new DataTable();
                objxtranr = temds.Tables[0].Clone();
                //Copy all rows from temp DataSet to objxtranr that do not have a join to st?tranr tables........
                for (int i = 0; i < temds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = temds.Tables[0].Rows[i];

                    bool Isexist = false;
                    foreach (DataRow drt in xtranrTables.Tables[0].Rows)
                    {
                        string Table = drt[0].ToString().Trim();
                        if (Table != "stxtranr")
                        {
                            string sql = "select rowid from " + Table + " where  orig_journal = '" + dr[0].ToString().Trim() + "'" + "  and doc_no= " + dr[1].ToString().Trim();
                            DataSet dsrowid = objDalBaseClass.GetData(sql);
                            if (dsrowid.Tables[0].Rows.Count != 0)
                            {
                                if (dsrowid.Tables[0].Rows[0][0] != DBNull.Value)
                                {
                                    if (dsrowid.Tables[0].Rows[0][0].ToString().Trim() != string.Empty)
                                    {
                                        Isexist = true;
                                        break;
                                    }
                                }
                            }


                        }
                    }
                    if (!Isexist)
                    {
                        DataRow drnew = objxtranr.NewRow();
                        objxtranr.Rows.Add(drnew);
                        drnew.ItemArray = dr.ItemArray;
                    }
                }
                //remove all user defined information associated with the stxtranr
                foreach (DataRow dr in objxtranr.Rows)
                {
                    object[] delParameter = new object[2];
                    delParameter[0] = dr[0].ToString().Trim();
                    delParameter[1] = Convert.ToInt32(dr[1].ToString().Trim());
                    object result = objDalBaseClass.ExecuteProcedure_ByTransaction(ref ObjTrx, ref delParameter, (new DVODeleteOldActivity()).DeleteStxtranr, true);
                    if (Convert.ToInt32(result) != 1)
                    {
                        return false;
                    }
                    if (!xfer_udf("stxtranr," + dr[0].ToString().Trim(), "stxtranr," + dr[0].ToString().Trim(), dr[0].ToString().Trim(), dr[0].ToString().Trim(), "D", ObjTrx))
                    {
                        return false;
                    }
                }

            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool xfer_udf(string old_fname, string new_fname, string old_key, string new_key, string xfer_type, object ObjTrx)
        {

            // This function will copy, move, or delete user define fields and notes
            // from one key to another.  this is frequenty used by posting
            // programs to transfer the link from the entry table to the the
            // transaction table.

            //xfer_type: "M" - move   - copy udf's from old key to new key
            // - delete udf's using old key
            //"C" - move   - copy udf's from old key to new key
            //"D" - delete - delete udf's with old key and old_fname
            // udf_move_type - process a move type transfer
            // when xfer_type = "M"
            //call move_notes(old_fname, new_fname, old_key, new_key)
            //if not udf_label(old_fname, new_fname) then return end if
            //call move_udf(old_fname, new_fname, old_key, new_key)
            // udf_copy_type - process a copy type transfer
            // when xfer_type = "C"
            //call copy_notes(old_fname, new_fname, old_key, new_key)
            //if not udf_label(old_fname, new_fname) then return end if
            //call copy_udf(old_fname, new_fname, old_key, new_key)
            //#_udf_delete - process a delete request
            try
            {
                if (xfer_type == "D")
                {
                    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                    object[] delParameter = new object[2];
                    delParameter[0] = old_fname.Trim();
                    delParameter[1] = old_key.Trim();
                    object result = objDalBaseClass.ExecuteProcedure_ByTransaction(ref ObjTrx, ref delParameter, (new DVODeleteOldActivity()).DeleteNotes, true);
                    if (Convert.ToInt32(result) != 1)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;

        }


        public static List<DVOGLstxtranr> GetDataCount(ref DVOGLstxtranr objPchecks)
        {
            List<DVOGLstxtranr> listDVOAPCheckProcessingStpcashe = new List<DVOGLstxtranr>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = objPchecks.doc_no;

                using (DataSet ds = ObjDALBaseClass.GetData(ref parameters, typeof(DVOGLstxtranr), objPchecks.COUNT_STXTRANR_RECORDS))
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                using (DVOGLstxtranr tobDVOGLstxtranr = new DVOGLstxtranr())
                                {

                                    if (!Convert.IsDBNull(dr[0])) tobDVOGLstxtranr.count = Convert.ToInt32(dr[0]);
                                    listDVOAPCheckProcessingStpcashe.Add(tobDVOGLstxtranr);
                                }

                            }
                }
                parameters = null;
                ObjDALBaseClass = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return listDVOAPCheckProcessingStpcashe;
        }

        public static bool CheckIntoStgactvd(ref DVOPostGL ObjPostGLTransaction)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                Object[] paramters = new object[4];
                paramters[0] = ObjPostGLTransaction.orig_journal;
                paramters[1] = ObjPostGLTransaction.doc_no;
                paramters[2] = ObjPostGLTransaction.acct_no;
                paramters[3] = ObjPostGLTransaction.amount;
                object _count = ObjDALBaseClass.ExecuteScalar(ref paramters, (new DVOGLTRanActVD()).CHECK_STGACTVD);
                if (Convert.ToUInt32(_count) == 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
            return false;
        }

        public static DataTable GetAccountsWithDiff(string period, string year)
        {
            DataTable objDataTable = new DataTable();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            DVOGLActivitySummary obj = new DVOGLActivitySummary();
            try
            {
                Object[] parameter = new object[2];
                parameter[0] = period;
                parameter[1] = year;

                DataSet dsTd = ObjDALBaseClass.GetData(obj.LoadStxchrtd(ref parameter));
                DataSet dsAct = ObjDALBaseClass.GetData(obj.FIND_ACT(ref parameter));
                objDataTable.Columns.Add("AcctNo", typeof(Int32));
                objDataTable.Columns.Add("Keyvalue");
                objDataTable.Columns.Add("acct_desc");
                objDataTable.Columns.Add("acct_type");
                objDataTable.Columns.Add("tdbalance", typeof(decimal));
                objDataTable.Columns.Add("endbalance", typeof(decimal));
                objDataTable.Columns.Add("actbalance", typeof(decimal));

                foreach (DataRow dr in dsTd.Tables[0].Rows)
                {
                    DVOGLstxchrtd objTd = new DVOGLstxchrtd();
                    objTd.acct_no = dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0;
                    objTd.activity = dr["activity"] != DBNull.Value ? Convert.ToDecimal(dr["activity"]) : 0;
                    objTd.this_month = dr["this_month"] != DBNull.Value ? Convert.ToDecimal(dr["this_month"]) : 0;
                    objTd.balance = dr["balance"] != DBNull.Value ? Convert.ToDecimal(dr["balance"]) : 0;

                    decimal endBalance = objTd.activity + objTd.this_month;
                    decimal DbAmt = 0;
                    decimal CrAmt = 0;
                    decimal acctBalance = 0;
                    DataRow[] dra = dsAct.Tables[0].Select("acct_no=" + objTd.acct_no);
                    if (dra.Length > 0)
                    {

                        if (dra[0][2].ToString().Trim() == "D")
                        {
                            DbAmt = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
                        }
                        else
                        {
                            CrAmt = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
                        }
                        if (dra.Length > 1)
                        {
                            if (dra[1][2].ToString().Trim() == "D")
                            {
                                DbAmt = dra[1][0] != DBNull.Value ? Convert.ToDecimal(dra[1][0]) : 0;
                            }
                            else
                            {
                                CrAmt = dra[1][0] != DBNull.Value ? Convert.ToDecimal(dra[1][0]) : 0;
                            }
                        }
                    }
                    if (dr["incr_with_crdt"].ToString() == "Y")
                    {
                        acctBalance = CrAmt - DbAmt;
                    }
                    else
                    {
                        acctBalance = DbAmt - CrAmt;

                    }
                    //if (acctBalance < 0)
                    //    acctBalance = acctBalance * -1;
                    if (((dr["acct_cat"].ToString() == "A" || dr["acct_cat"].ToString() == "E" || dr["acct_cat"].ToString() == "F") && dr["incr_with_crdt"].ToString() == "Y")
  ||

  ((dr["acct_cat"].ToString() == "B" || dr["acct_cat"].ToString() == "C" || dr["acct_cat"].ToString() == "D") && dr["incr_with_crdt"].ToString() == "N"))
                    {
                        endBalance = -1 * endBalance;
                    }



                    if ((endBalance != acctBalance) && (endBalance + acctBalance != 0))
                    {
                        DataRow drn = objDataTable.NewRow();
                        drn["AcctNo"] = dr["acct_no"];
                        drn["Keyvalue"] = dr["keyvalue"];
                        drn["acct_desc"] = dr["acct_desc"];
                        drn["acct_type"] = dr["acct_type"];
                        drn["tdbalance"] = objTd.balance;
                        drn["endbalance"] = endBalance;
                        drn["actbalance"] = acctBalance;
                        objDataTable.Rows.Add(drn.ItemArray);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objDataTable;
        }

        public static List<string>  GetTxtAccountsWithDiff(string period, string year)
        {
            
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass ObjDALBaseClass = objDALBaseClassHelper.GetDAL();
            DVOGLActivitySummary obj = new DVOGLActivitySummary();
            List<string> strList = new List<string>();
            bool IsFirst = true;
            string strHdr = string.Empty;
            string strDtl = string.Empty;
            try
            {
                Object[] parameter = new object[2];
                parameter[0] = period;
                parameter[1] = year;
                DataSet dsTd = ObjDALBaseClass.GetData(obj.LoadStxchrtd(ref parameter));
                DataSet dsAct = ObjDALBaseClass.GetData(obj.FIND_ACT(ref parameter));
                foreach (DataRow dr in dsTd.Tables[0].Rows)
                {
                    DVOGLstxchrtd objTd = new DVOGLstxchrtd();
                    objTd.acct_no = dr["acct_no"] != DBNull.Value ? Convert.ToInt32(dr["acct_no"]) : 0;
                    objTd.activity = dr["activity"] != DBNull.Value ? Convert.ToDecimal(dr["activity"]) : 0;
                    objTd.this_month = dr["this_month"] != DBNull.Value ? Convert.ToDecimal(dr["this_month"]) : 0;
                    objTd.balance = dr["balance"] != DBNull.Value ? Convert.ToDecimal(dr["balance"]) : 0;
                    string keyvalue = dr["keyvalue"] != DBNull.Value ? Convert.ToString(dr["keyvalue"]).Trim() : string.Empty;
                    string acct_desc = dr["acct_desc"] != DBNull.Value ? Convert.ToString(dr["acct_desc"]).Trim() : string.Empty;
                    string acct_type = dr["acct_type"] != DBNull.Value ? Convert.ToString(dr["acct_type"]).Trim() : string.Empty;
                    decimal endBalance = objTd.activity + objTd.this_month;
                    decimal DbAmt = 0;
                    decimal CrAmt = 0;
                    decimal acctBalance = 0;
                    DataRow[] dra = dsAct.Tables[0].Select("acct_no=" + objTd.acct_no);
                    if (dra.Length > 0)
                    {

                        if (dra[0][2].ToString().Trim() == "D")
                        {
                            DbAmt = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
                        }
                        else
                        {
                            CrAmt = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
                        }
                        if (dra.Length > 1)
                        {
                            if (dra[1][2].ToString().Trim() == "D")
                            {
                                DbAmt = dra[1][0] != DBNull.Value ? Convert.ToDecimal(dra[1][0]) : 0;
                            }
                            else
                            {
                                CrAmt = dra[1][0] != DBNull.Value ? Convert.ToDecimal(dra[1][0]) : 0;
                            }
                        }
                    }
                    if (dr["incr_with_crdt"].ToString() == "Y")
                    {
                        acctBalance = CrAmt - DbAmt;
                    }
                    else
                    {
                        acctBalance = DbAmt - CrAmt;

                    }
                    //if (acctBalance < 0)
                    //    acctBalance = acctBalance * -1;
                    if (((dr["acct_cat"].ToString() == "A" || dr["acct_cat"].ToString() == "E" || dr["acct_cat"].ToString() == "F") && dr["incr_with_crdt"].ToString() == "Y")
  ||

  ((dr["acct_cat"].ToString() == "B" || dr["acct_cat"].ToString() == "C" || dr["acct_cat"].ToString() == "D") && dr["incr_with_crdt"].ToString() == "N"))
                    {
                        endBalance = -1 * endBalance;
                    }



                    if ((endBalance != acctBalance) && (endBalance + acctBalance != 0))
                    {   
                        
                        if (IsFirst)
                        {
                            strHdr = "-- Accounts With Differences for period " + period + "/" + year + "  -- ";
                            IsFirst = false;
                            strList.Add(strHdr);
                        }
                        strDtl = acct_type +", " +objTd.acct_no +  ", " + keyvalue + " - " + acct_desc + " ," + endBalance.ToString() + " ," + acctBalance.ToString();
                        strList.Add(strDtl);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strList;
        }

        static DataSet dsStxperdr = new DataSet();
        public static void GetAllStxperdr()
        {
            try
            {   
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                
                dsStxperdr = objDALBaseClass.GetData(typeof(DVOExceptionReports), objDVOExceptionReports.GetAllMaster_periods);
                if (dsStxperdr.Tables.Count > 0)
                {
                    dsStxperdr.Tables[0].Columns[0].ColumnName = "period";
                    dsStxperdr.Tables[0].Columns[1].ColumnName = "period_year";
                    dsStxperdr.Tables[0].Columns[2].ColumnName = "start_date";
                    dsStxperdr.Tables[0].Columns[3].ColumnName = "end_date";
                }
            }
            catch(Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        
    }
}

