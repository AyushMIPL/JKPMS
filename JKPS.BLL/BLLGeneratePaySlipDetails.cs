using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;
namespace JKPS.BLL
{
    public class BLLGeneratePaySlipDetails
    {
        public static DataTable GetPayslipDetails(ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee, ref DVOPYBatchProcessStybatchr pObjBatch)
        {
            /*
              Added by Sarvjeet on 22/10/2010
              To implemented Payroll batch process into Generate Payslip Detail. Nedd to add
              A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'GetPayslipDetails' function
              and remove comment from the code written for batch process logic.   
         
            */ 
            DataSet ds = null;
            string PayMonth;
            string PayYear;
            //DateTime StarDatePayYear;
            //DateTime EndDatePayYear;

            DataTable objDataTable = new DataTable();
            object objTransaction = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            //Added by Sarvjeet on 22/01/2010..
            #region Declare Variables for Batch Process
            StringBuilder errorMassage = new StringBuilder();
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            int recordsSearched = 0;
            int recordsProcessed = 0;
            bool IsProessIns = false;
            #endregion

            object objLockTransaction = null;
            DvoUpdatePayableDefDetails objAPDefault = new DvoUpdatePayableDefDetails();
            int _currentCheckNo = 0;

            try
            {
                //Added by Sarvjeet on 22/01/2010..
                #region Insert Process Start Info..

                object objTrx = null;
                objProcessDtl.pybatchid = pObjBatch.pybatchid;
                objProcessDtl.processname = "Generate Pay Slip Details";
                objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
                BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
                IsProessIns = true;

                #endregion

                object[] paramerers = new object[4];
                paramerers[0] = objDVOPayrollProcess_PayEmployee.deposit.Trim();
                paramerers[1] = objDVOPayrollProcess_PayEmployee.Cash_acct_no;
                paramerers[2] = objDVOPayrollProcess_PayEmployee.TypeCode.Trim();
                paramerers[3] = objDVOPayrollProcess_PayEmployee.District.Trim();

                ds = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.GetListofPersonstoProcess(ref paramerers));
                if (ds != null && ds.Tables.Count > 0)
                {
                    #region Set the column name..and add columns ......
                    ds.Tables[0].Columns[0].ColumnName = "address1";
                    ds.Tables[0].Columns[1].ColumnName = "address2";
                    ds.Tables[0].Columns[2].ColumnName = "city";
                    ds.Tables[0].Columns[3].ColumnName = "first_name";
                    ds.Tables[0].Columns[4].ColumnName = "last_name";
                    ds.Tables[0].Columns[5].ColumnName = "middle_name";
                    ds.Tables[0].Columns[6].ColumnName = "soc_sec_num";
                    ds.Tables[0].Columns[7].ColumnName = "state";
                    ds.Tables[0].Columns[8].ColumnName = "zip";
                    ds.Tables[0].Columns[9].ColumnName = "cash_amount";
                    ds.Tables[0].Columns[10].ColumnName = "check_no";
                    ds.Tables[0].Columns[11].ColumnName = "department";
                    ds.Tables[0].Columns[12].ColumnName = "doc_date";
                    ds.Tables[0].Columns[13].ColumnName = "doc_no";
                    ds.Tables[0].Columns[14].ColumnName = "empl_code";
                    ds.Tables[0].Columns[15].ColumnName = "eop_date";
                    ds.Tables[0].Columns[16].ColumnName = "pay_date";
                    ds.Tables[0].Columns[17].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[18].ColumnName = "acct_desc";
                    ds.Tables[0].Columns[19].ColumnName = "mailid";
                    ds.Tables[0].Columns.Add("inc_code");
                    ds.Tables[0].Columns.Add("inc_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_num");
                    ds.Tables[0].Columns.Add("inc_hours", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("ded_code");
                    ds.Tables[0].Columns.Add("ded_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("ded_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_err");
                    ds.Tables[0].Columns.Add("ded_err");
                    ds.Tables[0].Columns.Add("inc_count");
                    ds.Tables[0].Columns.Add("ded_count");
                    ds.Tables[0].Columns.Add("cheque_amt", typeof(decimal));

                    ds.Tables[0].Columns.Add("t_inc_num", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_hours", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_ded_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_ded_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("email", typeof(string));
                    //ds.Tables[0].Columns.Add("acct_desc");
                    //ds.Tables[0].Columns.Add("keyvalue");
                    objDataTable = ds.Tables[0].Clone();
                    #endregion Set the column name..and add columns ......
                    recordsSearched = ds.Tables[0].Rows.Count;
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        errorMassage.Append("[No Element to Process]");
                        return objDataTable;
                    }

                    objLockTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
                    List<DvoUpdatePayableDefDetails> objstpcntrcList = BLLUpdatePayableDefaults.GetAllInfo();
                    if (objstpcntrcList != null && objstpcntrcList.Count > 0)
                    {
                        objAPDefault = objstpcntrcList[0];
                        int l = LockAPDefaultRecord(ref objLockTransaction, ref objAPDefault);
                        if (l == 1)
                        {
                            objstpcntrcList = BLLUpdatePayableDefaults.GetAllInfo();
                            if (objstpcntrcList != null && objstpcntrcList.Count > 0)
                            {
                                _currentCheckNo = objstpcntrcList[0].last_chkno;
                            }
                            ReleaseAndUpdateAPDefaultRecord(true, false, ref objLockTransaction, ref objAPDefault, _currentCheckNo + recordsSearched);
                        }
                        else
                        {
                            throw new Exception("Accounts-Payable Controls table already locked by another user.");
                        }
                    }

                    #region Get All Data

                    int dup_flag = 0;
                    int dup_ssn = 0;
                    decimal t_inc_num = 0;
                    decimal t_inc_hours = 0;
                    decimal t_inc_amount = 0;
                    decimal t_inc_ytd = 0;
                    decimal t_ded_amount = 0;
                    decimal t_ded_ytd = 0;
                    string _keyvalue = string.Empty;
                    string _acctdesc = string.Empty;

                    //Added By Rahul jain on 16/Jan/2010 For get all Incomes of Persons
                    //DataSet dsIncome = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.GetListofPersonsIncome(ref paramerers));
                    DataSet dsIncome = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_checkpaydtl11);
                    if (dsIncome != null)
                    {
                        dsIncome.Tables[0].Columns[0].ColumnName = "line_no";
                        dsIncome.Tables[0].Columns[1].ColumnName = "inc_code";
                        dsIncome.Tables[0].Columns[2].ColumnName = "number";
                        dsIncome.Tables[0].Columns[3].ColumnName = "hours";
                        dsIncome.Tables[0].Columns[4].ColumnName = "amount";
                        dsIncome.Tables[0].Columns[5].ColumnName = "amount1";
                        dsIncome.Tables[0].Columns[6].ColumnName = "doc_no";
                        dsIncome.Tables[0].Columns[7].ColumnName = "empl_code";
                    }
                    //Added By Rahul jain on 16/01/2010 For Get All deduction of Persons
                    DataSet dsDeductions = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_checkpaydtl22);
                    if (dsDeductions != null)
                    {
                        dsDeductions.Tables[0].Columns[0].ColumnName = "line_no";
                        dsDeductions.Tables[0].Columns[1].ColumnName = "ded_code";
                        dsDeductions.Tables[0].Columns[2].ColumnName = "amount";
                        dsDeductions.Tables[0].Columns[3].ColumnName = "amount1";
                        dsDeductions.Tables[0].Columns[4].ColumnName = "doc_no";
                        dsDeductions.Tables[0].Columns[5].ColumnName = "empl_code";
                    }

                    DataSet ds_flag = objDALBaseClass.GetData(typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).FIND_dup_flag1);
                    if (ds_flag != null)
                    {
                        ds_flag.Tables[0].Columns[0].ColumnName = "empl_code";
                        ds_flag.Tables[0].Columns[1].ColumnName = "count_flag";
                    }
                    DataSet ds_ssn = objDALBaseClass.GetData(typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).FIND_dup_ssn1);
                    if (ds_ssn != null)
                    {
                        ds_ssn.Tables[0].Columns[0].ColumnName = "soc_sec_num";
                        ds_ssn.Tables[0].Columns[1].ColumnName = "count_ssn";
                    }
                    //pay slip date
                    int _Year = DVOApplicationUserInfo.CurrentDate.Year;

                    //calculate amount income YTD total

                    object[] parameterytd1 = new object[1];
                    parameterytd1[0] = _Year;
                    DataSet ds_ytd1 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_stypayidamt1);
                    if (ds_ytd1 != null)
                    {
                        ds_ytd1.Tables[0].Columns[0].ColumnName = "empl_code";
                        ds_ytd1.Tables[0].Columns[1].ColumnName = "doc_no";
                        ds_ytd1.Tables[0].Columns[2].ColumnName = "inc_code";
                        ds_ytd1.Tables[0].Columns[3].ColumnName = "amount";
                    }

                    DataSet ds_ytd2 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_stypayidinc_ytd1);
                    if (ds_ytd2 != null)
                    {
                        ds_ytd2.Tables[0].Columns[0].ColumnName = "empl_code";
                        ds_ytd2.Tables[0].Columns[1].ColumnName = "inc_code";
                        ds_ytd2.Tables[0].Columns[2].ColumnName = "soc_sec_num";
                        ds_ytd2.Tables[0].Columns[3].ColumnName = "amount";
                    }

                    //for deduction YTD total
                    DataSet ds_ytd3 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_stypayddamt1);
                    if (ds_ytd2 != null)
                    {
                        ds_ytd3.Tables[0].Columns[0].ColumnName = "empl_code";
                        ds_ytd3.Tables[0].Columns[1].ColumnName = "doc_no";
                        ds_ytd3.Tables[0].Columns[2].ColumnName = "ded_code";
                        ds_ytd3.Tables[0].Columns[3].ColumnName = "amount";
                    }
                    DataSet ds_ytd4 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_stypayddinc_ytd1);
                    if (ds_ytd4 != null)
                    {
                        ds_ytd4.Tables[0].Columns[0].ColumnName = "empl_code";
                        ds_ytd4.Tables[0].Columns[1].ColumnName = "ded_code";
                        ds_ytd4.Tables[0].Columns[2].ColumnName = "soc_sec_num";
                        ds_ytd4.Tables[0].Columns[3].ColumnName = "amount";
                    }

                    #endregion Get All Data

                    #region Process

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        //DataRow drn = objDataTable.NewRow();
                        DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee1 = new DVOPayrollProcess_PayEmployee();
                        _keyvalue = string.Empty;
                        _acctdesc = string.Empty;

                        if (dr["doc_no"] != DBNull.Value)
                            objDVOPayrollProcess_PayEmployee1.Doc_no = Convert.ToInt32(dr["doc_no"]);
                        if (dr["cash_amount"] != DBNull.Value)
                            objDVOPayrollProcess_PayEmployee1.cash_amount = Convert.ToDecimal(dr["cash_amount"]);
                        if (dr["check_no"] != DBNull.Value)
                            objDVOPayrollProcess_PayEmployee1.check_no = Convert.ToInt32(dr["check_no"]);
                        if (dr["pay_date"] != DBNull.Value)
                            objDVOPayrollProcess_PayEmployee1.pay_date = Convert.ToDateTime(dr["pay_date"]);
                        objDVOPayrollProcess_PayEmployee1.EmplCode = dr["empl_code"].ToString().Trim();
                        string LastName = dr["last_name"] != DBNull.Value ? Convert.ToString(dr["last_name"]).Trim() : string.Empty;
                        if (LastName.Trim() != string.Empty)
                            LastName = LastName + ",  ";
                        string FirstName = dr["first_name"] != DBNull.Value ? Convert.ToString(dr["first_name"]).Trim() : string.Empty;
                        //throw new Exception();
                        string MiddleName = dr["middle_name"] != DBNull.Value ? Convert.ToString(dr["middle_name"]).Trim() : string.Empty;
                        string email = dr["email"] != DBNull.Value ? Convert.ToString(dr["email"]).Trim() : string.Empty;
                        if (MiddleName.Trim() != string.Empty)
                            MiddleName = ",  " + MiddleName;
                        dr["last_name"] = LastName;
                        dr["first_name"] = FirstName;
                             dr["middle_name"] = MiddleName;

                        #region Commented Code
                        //Get Keyvalue and acc_desc for employee cash account... 
                        //Object[] Param = new object[2];
                        //Param[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                        //Param[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                        //DataSet dscashkey = objDALBaseClass.GetData(ref Param, typeof(DVOPayrollProcess_PayEmployee), objDVOPayrollProcess_PayEmployee1.GET_CASHKEY);
                        //if(dscashkey.Tables.Count>0)
                        //    if (dscashkey.Tables[0].Rows.Count > 0)
                        //    { 
                        //dr["keyvalue"]=dscashkey.Tables[0].Rows[0][0];
                        //dr["acct_desc"] = dscashkey.Tables[0].Rows[0][1];                      
                        //}
                        #endregion Commented Code

                        #region processing on before Emp_Code group..
                        bool _status = false;
                        if (i != 0)
                        {
                            if (objDVOPayrollProcess_PayEmployee1.EmplCode != ds.Tables[0].Rows[i - 1]["empl_code"].ToString())
                            {
                                _status = true;
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                _status = true;
                            }
                        }
                        if (_status)
                        {
                            //dup_flag = Get_dup_fla(objDVOPayrollProcess_PayEmployee1.EmplCode);
                            //dup_ssn = Get_dup_ssn(dr["soc_sec_num"].ToString());
                            //Added By Rahul jain on 21/01/2010
                            DataRow[] dra_flag = ds_flag.Tables[0].Select("( empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                            if (dra_flag.Length > 0)
                                dup_flag = Convert.ToInt32(dra_flag[0][1]);
                            DataRow[] dra_ssn = ds_ssn.Tables[0].Select("( soc_sec_num ='" + dr["soc_sec_num"].ToString().Trim() + "')");
                            if (dra_ssn.Length > 0)
                                dup_ssn = Convert.ToInt32(dra_ssn[0][1]);
                            //----------------------------------
                        }


                        #endregion processing on before Emp_Code group..

                        #region processing on before doc_no group..
                        bool _postingStatus0 = false;
                        if (i != 0)
                        {
                            if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(ds.Tables[0].Rows[i - 1]["doc_no"]))
                            {
                                _postingStatus0 = true;
                            }
                        }
                        else if (i == 0)
                            _postingStatus0 = true;
                        if (_postingStatus0)
                        {
                            //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                            if (objDVOPayrollProcess_PayEmployee1.check_no == 0)
                            {
                                objDVOPayrollProcess_PayEmployee1.check_no = ++_currentCheckNo;
                                dr["check_no"] = objDVOPayrollProcess_PayEmployee1.check_no;

                                ////objTransaction = objDALBaseClassHelper.GetTransactionObject();
                                ////int check_no = BLLAccountingLiberary.Auto_Next("stpcntrc", "last_chkno", ref objTransaction);
                                //int check_no = BLLAccountingLiberary.Auto_Next_APCheckNo(ref objTransaction);
                                //if (check_no == 0)
                                //    throw new Exception("Error has occurred while generating check number.");

                                //dr["check_no"] = check_no;
                                //objDVOPayrollProcess_PayEmployee1.check_no = check_no;
                            }
                            if (objDVOPayrollProcess_PayEmployee1.cash_amount < 0)
                            {
                                objDVOPayrollProcess_PayEmployee1.cash_amount = 0;
                            }
                            if (objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime("01/01/1900") && objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime(null))
                            {
                                PayMonth = objDVOPayrollProcess_PayEmployee1.pay_date.Month.ToString();
                                PayYear = objDVOPayrollProcess_PayEmployee1.pay_date.Year.ToString();
                                //string sdpy = "01" + "/" + "01" + "/" + PayYear;
                                //string edpy = "01" + "/" + "31" + "/" + PayYear;
                                //StarDatePayYear = DateTime.ParseExact(sdpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                //// EndDatePayYear = "01" + "/" + "31" + "/" + PayYear;
                                //EndDatePayYear = DateTime.ParseExact(edpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                            }
                        }
                        #endregion process on before doc_no group..

                        #region processing on_every_row ........

                        //List<DVOPayrollstypayid> objDVOPayrollstypayidList = GetEmployeeIcomeList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                        //List<DVOPayrollstypaydd> objDVOPayrollstypayddList = GetEmployeeDedList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);

                        //Added By Rahul jain on 16/01/2010
                        DataRow[] draIncomes = dsIncome.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " OR empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                        DataRow[] draDeductions = dsDeductions.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " OR empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                        //----------------------------------
                        int inc_count = 0;
                        int ded_count = 0;
                        inc_count = draIncomes.Length;// objDVOPayrollstypayidList.Count;  Commented By rahul jain 
                        ded_count = draDeductions.Length;// objDVOPayrollstypayddList.Count; Commented By rahul jain 
                        int loop_number = 0;
                        if (inc_count > ded_count)
                            loop_number = inc_count;
                        else
                            loop_number = ded_count;

                        for (int j = 0; j < loop_number; j++)
                        {
                            // Collect amounts from other payroll entries for the same employee code.
                            // for income
                            #region For incomes ----------------------
                            if (j < inc_count)
                            {
                                if (dup_flag > 1)
                                {
                                    decimal tmp_ytd_accrual1 = 0;
                                    //object[] parameter1 = new object[3];
                                    //parameter1[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                    //parameter1[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                    //parameter1[2] = draIncomes[j][1].ToString().Trim();//objDVOPayrollstypayidList[j].inc_code.Trim();

                                    //DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                    //object result1 = objDALBaseClass.ExecuteScalar(ref parameter1, objDVOPayrollstypayid.FIND_stypayidamt);
                                    //if (result1.ToString() != "")
                                    //{
                                    //    tmp_ytd_accrual1 = Convert.ToDecimal(result1);
                                    //}
                                    //Added By Rahul jain 0n 21/01/2020
                                    DataRow[] draytd1 = ds_ytd1.Tables[0].Select("doc_no <> " + objDVOPayrollProcess_PayEmployee1.Doc_no + " AND empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND inc_code ='" + draIncomes[j][1].ToString().Trim() + "'");
                                    if (draytd1.Length > 0)
                                    {
                                        for (int k = 0; k < draytd1.Length; k++)
                                        {
                                            tmp_ytd_accrual1 = tmp_ytd_accrual1 + (draytd1[k][3] != DBNull.Value ? Convert.ToDecimal(draytd1[k][3]) : 0);
                                        }
                                    }

                                    //inc_rate means ytd_accrual.
                                    //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual1;
                                    draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual1;
                                }
                                //Collect accrual amounts from other employee
                                //codes sharing the same soc. sec. num
                                //This is typically the same person under a new tax
                                //jurisdiction.
                                if (dup_ssn > 1)
                                {
                                    decimal tmp_ytd_accrual2 = 0;
                                    //object[] parameter2 = new object[3];
                                    //parameter2[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                    //parameter2[1] = dr["soc_sec_num"].ToString().Trim();
                                    //parameter2[2] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                    //DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                    //object result2 = objDALBaseClass.ExecuteScalar(ref parameter2, objDVOPayrollstypayid.FIND_stypayidinc_ytd);
                                    //if (result2.ToString() != "")
                                    //{
                                    //    tmp_ytd_accrual2 = Convert.ToDecimal(result2);
                                    //}
                                    //Added By Rahul jain 0n 21/01/2020
                                    DataRow[] draytd2 = ds_ytd2.Tables[0].Select("soc_sec_num = '" + dr["soc_sec_num"].ToString().Trim() + "' AND empl_code <> '" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND inc_code ='" + draIncomes[j][1].ToString().Trim() + "'");
                                    if (draytd2.Length > 0)
                                    {
                                        for (int f = 0; f < draytd2.Length; f++)
                                        {
                                            tmp_ytd_accrual2 = tmp_ytd_accrual2 + (draytd2[f][3] != DBNull.Value ? Convert.ToDecimal(draytd2[f][3]) : 0);
                                        }
                                    }
                                    //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual2;
                                    draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual2;
                                }
                                //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + objDVOPayrollstypayidList[j].amount;
                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + Convert.ToDecimal(draIncomes[j][4]);
                            }
                            #endregion For incomes ----------------------
                            // for deduction 
                            #region For deduction ----------------------
                            if (j < ded_count)
                            {
                                if (dup_flag > 1)
                                {
                                    decimal tmp_ytd_accrual3 = 0;
                                    //object[] parameter3 = new object[3];
                                    //parameter3[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                    //parameter3[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                    //parameter3[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                    //DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                    //object result3 = objDALBaseClass.ExecuteScalar(ref parameter3, objDVOPayrollstypaydd.FIND_stypayddamt);
                                    //if (result3.ToString() != "")
                                    //{
                                    //    tmp_ytd_accrual3 = Convert.ToDecimal(result3);
                                    //}
                                    //Added By Rahul jain 0n 21/01/2020
                                    DataRow[] draytd3 = ds_ytd3.Tables[0].Select("doc_no <> " + objDVOPayrollProcess_PayEmployee1.Doc_no + " AND empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND ded_code ='" + draDeductions[j][1].ToString().Trim() + "'");
                                    if (draytd3.Length > 0)
                                    {
                                        for (int p = 0; p < draytd3.Length; p++)
                                        {
                                            tmp_ytd_accrual3 = tmp_ytd_accrual3 + (draytd3[p][3] != DBNull.Value ? Convert.ToDecimal(draytd3[p][3]) : 0);
                                        }
                                    }
                                    draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual3;//objDVOPayrollstypayddList[j].ded_rate
                                }
                                if (dup_ssn > 1)
                                {
                                    decimal tmp_ytd_accrual4 = 0;
                                    //object[] parameter4 = new object[3];
                                    //parameter4[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                    //parameter4[1] = dr["soc_sec_num"].ToString().Trim();
                                    //parameter4[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                    //DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                    //object result4 = objDALBaseClass.ExecuteScalar(ref parameter4, objDVOPayrollstypaydd.FIND_stypayddinc_ytd);
                                    //if (result4.ToString() != "")
                                    //{
                                    //    tmp_ytd_accrual4 = Convert.ToDecimal(result4);
                                    //}
                                    //Added By Rahul jain 0n 21/01/2020
                                    DataRow[] draytd4 = ds_ytd4.Tables[0].Select("soc_sec_num = '" + dr["soc_sec_num"].ToString().Trim() + "' AND empl_code <> '" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND ded_code ='" + draDeductions[j][1].ToString().Trim() + "'");
                                    if (draytd4.Length > 0)
                                    {
                                        for (int q = 0; q < draytd4.Length; q++)
                                        {
                                            tmp_ytd_accrual4 = tmp_ytd_accrual4 + (draytd4[q][3] != DBNull.Value ? Convert.ToDecimal(draytd4[q][3]) : 0);
                                        }
                                    }
                                    //objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[j].ded_rate + tmp_ytd_accrual4;
                                    draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual4;//objDVOPayrollstypayddList[j].ded_rate
                                }
                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + Convert.ToDecimal(draDeductions[j][2]);//objDVOPayrollstypayddList[j].ded_rate + objDVOPayrollstypayddList[j].amount;
                            }
                            #endregion For deduction ----------------------


                            #region process 9 lines----------------------
                            //for (int j = 0; j < loop_number; j++)
                            //{
                            DataRow drn1 = objDataTable.NewRow();
                            drn1.ItemArray = dr.ItemArray;
                            if (j < inc_count)
                            {
                                //set the income fields
                                drn1["inc_code"] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                drn1["inc_num"] = draIncomes[j][2];// objDVOPayrollstypayidList[j].number;
                                drn1["inc_hours"] = draIncomes[j][3];//objDVOPayrollstypayidList[j].hours;
                                drn1["inc_amount"] = draIncomes[j][4];//objDVOPayrollstypayidList[j].amount;
                                drn1["inc_ytd"] = draIncomes[j][5];//objDVOPayrollstypayidList[j].inc_rate;

                                t_inc_num = t_inc_num + (draIncomes[j][2] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][2]) : 0);//objDVOPayrollstypayidList[j].number
                                t_inc_hours = t_inc_hours + (draIncomes[j][3] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][3]) : 0);//objDVOPayrollstypayidList[j].hours
                                t_inc_amount = t_inc_amount + (draIncomes[j][4] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][4]) : 0);//objDVOPayrollstypayidList[j].amount
                                t_inc_ytd = t_inc_ytd + (draIncomes[j][5] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][5]) : 0);//objDVOPayrollstypayidList[j].inc_rate
                            }
                            if (j < ded_count)
                            {
                                // set the deduction fields
                                drn1["ded_code"] = draDeductions[j][1].ToString().Trim();//objDVOPayrollstypayddList[j].ded_code;
                                drn1["ded_amount"] = draDeductions[j][2].ToString().Trim();//objDVOPayrollstypayddList[j].amount;
                                drn1["ded_ytd"] = draDeductions[j][3].ToString().Trim();//objDVOPayrollstypayddList[j].ded_rate;

                                t_ded_ytd = t_ded_ytd + (draDeductions[j][3] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][3]) : 0);// objDVOPayrollstypayddList[j].ded_rate ?? 0;//make nullable decimal By Rahul
                                t_ded_amount = t_ded_amount + (draDeductions[j][2] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][2]) : 0);//objDVOPayrollstypayddList[j].amount ?? 0; //make nullable decimal By Rahul
                            }
                            objDataTable.Rows.Add(drn1);
                            //}
                            #endregion process 9 lines----------------------

                        } // end for loop..


                        #region Commented Code....................
                        //for (int j = 0; j < inc_count - 1; j++)
                        //{
                        //    if (objDVOPayrollstypayidList[j].amount == 0)
                        //    {
                        //        for (int k = j + 1; k < inc_count; k++)
                        //        {
                        //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                        //            {  
                        //                if(k<inc_count)
                        //                {
                        //                string tmp_hold_inc_code = objDVOPayrollstypayidList[j].inc_code;
                        //                int tmp_hold_inc_line_no = objDVOPayrollstypayidList[j].line_no;
                        //                decimal tmp_hold_inc_number = objDVOPayrollstypayidList[j].number;
                        //                decimal tmp_hold_inc_hours = objDVOPayrollstypayidList[j].hours;
                        //                decimal tmp_hold_inc_amount = objDVOPayrollstypayidList[j].amount;
                        //                decimal tmp_hold_inc_accrual = objDVOPayrollstypayidList[j].inc_rate;

                        //                objDVOPayrollstypayidList[j].inc_code = objDVOPayrollstypayidList[k].inc_code; ;
                        //                objDVOPayrollstypayidList[j].line_no = objDVOPayrollstypayidList[k].line_no; ;
                        //                objDVOPayrollstypayidList[j].number = objDVOPayrollstypayidList[k].number;
                        //                objDVOPayrollstypayidList[j].hours = objDVOPayrollstypayidList[k].hours;
                        //                objDVOPayrollstypayidList[j].amount = objDVOPayrollstypayidList[k].amount;
                        //                objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[k].inc_rate;

                        //                objDVOPayrollstypayidList[k].inc_code = tmp_hold_inc_code;
                        //                objDVOPayrollstypayidList[k].line_no = tmp_hold_inc_line_no;
                        //                objDVOPayrollstypayidList[k].number = tmp_hold_inc_number;
                        //                objDVOPayrollstypayidList[k].hours = tmp_hold_inc_hours;
                        //                objDVOPayrollstypayidList[k].amount = tmp_hold_inc_amount;
                        //                objDVOPayrollstypayidList[k].inc_rate = tmp_hold_inc_accrual;
                        //                }

                        //            }
                        //        }
                        //    }
                        //}

                        //for (int j = 0; j < ded_count - 1; j++)
                        //{
                        //    if (objDVOPayrollstypayddList[j].amount == 0)
                        //    {
                        //        for (int k = j + 1; k < inc_count; k++)
                        //        {
                        //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                        //            {  
                        //                if(k<ded_count)
                        //                {
                        //                string tmp_hold_ded_code = objDVOPayrollstypayddList[j].ded_code;
                        //                int tmp_hold_ded_line_no = objDVOPayrollstypayddList[j].line_no;
                        //                decimal tmp_hold_ded_amount = objDVOPayrollstypayddList[j].amount;
                        //                decimal tmp_hold_ded_accrual = objDVOPayrollstypayddList[j].ded_rate;


                        //                objDVOPayrollstypayddList[j].ded_code = objDVOPayrollstypayddList[k].ded_code;
                        //                objDVOPayrollstypayddList[j].line_no = objDVOPayrollstypayddList[k].line_no;
                        //                objDVOPayrollstypayddList[j].amount = objDVOPayrollstypayddList[k].amount;
                        //                objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[k].ded_rate;

                        //                objDVOPayrollstypayddList[k].ded_code = tmp_hold_ded_code;
                        //                objDVOPayrollstypayddList[k].line_no = tmp_hold_ded_line_no;
                        //                objDVOPayrollstypayddList[k].amount = tmp_hold_ded_amount;
                        //                objDVOPayrollstypayddList[k].ded_rate = tmp_hold_ded_accrual;
                        //                }

                        //            }
                        //        }
                        //    }
                        //}
                        #endregion Commented Code
                        #region Commented Code....................
                        //if (inc_count > 9)
                        //{
                        //    for (int j = 10; j <= inc_count; j++)
                        //    {
                        //        //set the income fields
                        //        t_inc_num = t_inc_num + objDVOPayrollstypayidList[j].number;
                        //        t_inc_hours = t_inc_hours + objDVOPayrollstypayidList[j].hours;
                        //        t_inc_amount = t_inc_amount + objDVOPayrollstypayidList[j].amount;
                        //        t_inc_ytd = t_inc_ytd + objDVOPayrollstypayidList[j].inc_rate;
                        //    }
                        //}
                        //if (ded_count > 9)
                        //{
                        //    for (int j = 10; j <= ded_count; j++)
                        //    {
                        //        t_ded_ytd = t_ded_ytd + objDVOPayrollstypayddList[j].ded_ytd;
                        //        t_ded_amount = t_ded_amount + objDVOPayrollstypayddList[j].amount;

                        //    }
                        //}
                        #endregion Commented Code....................

                        #endregion processing on_every_row ........

                        #region process on doc_no group.........
                        bool _postingStatus = false;
                        if (ds.Tables[0].Rows.Count != i + 1)
                        {
                            if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["doc_no"]))
                            {
                                _postingStatus = true;
                            }
                        }
                        else if (ds.Tables[0].Rows.Count == i + 1)
                            _postingStatus = true;

                        if (_postingStatus && loop_number > 0)
                        {
                            //objDataTable.Rows[objDataTable.Rows.Count - 1]["inc_err"] = "";
                            //objDataTable.Rows[objDataTable.Rows.Count - 1]["ded_err"] = "";
                            if (inc_count >= 10)
                            {
                                //objDataTable.Rows[objDataTable.Rows.Count - 1]["inc_err"] = "* Some income not listed.";
                            }
                            if (ded_count >= 10)
                            {
                                //objDataTable.Rows[objDataTable.Rows.Count - 1]["ded_err"] = "* Some deductions not listed.";
                            }
                            if (objDVOPayrollProcess_PayEmployee1.check_no == 0)
                                throw new Exception("Error has occurred while generating check number.");
                            //update the check numbers and printed field
                            //update Process_PayEmployee
                            if (objDVOPayrollProcess_PayEmployee1.check_no != 0)
                            {
                                objTransaction = objDALBaseClassHelper.GetTransactionObject();

                                object[] updParameters = new object[3];
                                updParameters[0] = objDVOPayrollProcess_PayEmployee1.check_no;
                                updParameters[1] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                updParameters[2] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                object Process_PayEmployee_status = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updParameters, objDVOPayrollProcess_PayEmployee.update_Process_PayEmployee, true);

                                if (Process_PayEmployee_status == null)
                                    throw new Exception("Error has occurred while updating Process_PayEmployee.");
                                else if (Process_PayEmployee_status.ToString().Trim() == string.Empty)
                                    throw new Exception("Error has occurred while updating Process_PayEmployee.");
                                else if (Convert.ToInt32(Process_PayEmployee_status) != 1)
                                    throw new Exception("Error has occurred while updating Process_PayEmployee.");

                                //commit work
                                if (objTransaction != null)
                                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                            }

                            ////commit work
                            //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                            recordsProcessed++;
                            //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                            if (objDataTable.Rows.Count > 0)
                            {
                                objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_num"] = t_inc_num;
                                objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_hours"] = t_inc_hours;
                                objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_amount"] = t_inc_amount;
                                objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_ytd"] = t_inc_ytd;
                                objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_ytd"] = t_ded_ytd;
                                objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_amount"] = t_ded_amount;
                            }
                            // initialize the totals
                            t_inc_num = 0;
                            t_inc_hours = 0;
                            t_inc_amount = 0;
                            t_inc_ytd = 0;
                            t_ded_amount = 0;
                            t_ded_ytd = 0;

                        }
                        else
                        {
                            errorMassage.Append("[ There is no Income and Deduction for DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                        }

                        #endregion process on doc_no group.........
                    }

                    #endregion Process
                }
                else
                {
                    //errorMassage.Append("[No Element to Process]");
                    return objDataTable;
                }
            }
            catch (Exception ex)
            {
                errorMassage.Append("[" + ex.Message + "]");
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                if (objLockTransaction != null)
                    ReleaseAndUpdateAPDefaultRecord(false, true, ref objLockTransaction, ref objAPDefault, 0);
                ExceptionManagement.ExceptionManager.Publish(ex);
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
                    obj.processname = "Generate Pay Slip Details";
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
            return objDataTable;
        }
        /// <summary>
        /// status of Lock, if current record is locked than 1, otherwise 0.
        /// </summary>
        static int _ChangeLockStatus = 0;
        private static int LockAPDefaultRecord(ref object TransactionObject, ref DvoUpdatePayableDefDetails objDvoUpdatePayableDefDetails)
        {
            int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref TransactionObject, (iDVO)objDvoUpdatePayableDefDetails, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo);
            if (LockStatus != 1)
            {
                _ChangeLockStatus = 0;
                System.Windows.Forms.DialogResult d = System.Windows.Forms.MessageBox.Show("Want to wait to release the record?", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Threading.Thread.Sleep(10000);
                    LockAPDefaultRecord(ref TransactionObject, ref objDvoUpdatePayableDefDetails);
                }
            }
            else
            {
                _ChangeLockStatus = 1;
            }
            return _ChangeLockStatus;
        }

        private static void ReleaseAndUpdateAPDefaultRecord(bool IsCommit, bool IsRollback, ref object TransactionObject, ref  DvoUpdatePayableDefDetails objDvoUpdatePayableDefDetails, int NewAPCheckNo)
        {
            if (TransactionObject != null)
            {
                objDvoUpdatePayableDefDetails.last_chkno = NewAPCheckNo;
                if (NewAPCheckNo > 0)
                    BLLUpdatePayableDefaults.UpdateAPDefaults_LastCheckNo(ref TransactionObject, ref objDvoUpdatePayableDefDetails);
                BLLCommonUtilities.ReleaseLockCurrentRecord(ref TransactionObject, (iDVO)objDvoUpdatePayableDefDetails, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, IsCommit);
                if ((!IsCommit) && IsRollback)
                    BLLCommonUtilities.LockTransaction_Rollback(ref TransactionObject);
                TransactionObject = null;
                _ChangeLockStatus = 0;
            }
        }

        public static List<DVOPayrollstypayid> GetEmployeeIcomeList(int doc_no, string Emp_code)
        {
            List<DVOPayrollstypayid> objDVOPayrollstypayidList = new List<DVOPayrollstypayid>();

            object[] parameter1 = new object[2];
            parameter1[0] = doc_no;
            parameter1[1] = Emp_code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDALBaseClass.GetData(ref parameter1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_checkpaydtl1))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                    if (dr[0].ToString() != null && dr[0].ToString() != string.Empty)
                        objDVOPayrollstypayid.line_no = Convert.ToInt32(dr[0].ToString());
                    objDVOPayrollstypayid.inc_code = dr[1].ToString();
                    if (dr[2].ToString() != null && dr[2].ToString() != string.Empty)
                        objDVOPayrollstypayid.number = Convert.ToDecimal(dr[2].ToString());
                    if (dr[3].ToString() != null && dr[3].ToString() != string.Empty)
                        objDVOPayrollstypayid.hours = Convert.ToDecimal(dr[3].ToString());
                    if (dr[4].ToString() != null && dr[4].ToString() != string.Empty)
                        objDVOPayrollstypayid.amount = Convert.ToDecimal(dr[4].ToString());
                    if (dr[5].ToString() != null && dr[5].ToString() != string.Empty)
                        objDVOPayrollstypayid.inc_rate = Convert.ToDecimal(dr[5].ToString());
                    objDVOPayrollstypayidList.Add(objDVOPayrollstypayid);
                }
            }
            return objDVOPayrollstypayidList;
        }

        public static List<DVOPayrollstypaydd> GetEmployeeDedList(int doc_no, string Emp_code)
        {
            List<DVOPayrollstypaydd> objDVOPayrollstypayddList = new List<DVOPayrollstypaydd>();
            object[] parameter1 = new object[2];
            parameter1[0] = doc_no;
            parameter1[1] = Emp_code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            using (DataSet ds = objDALBaseClass.GetData(ref parameter1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_checkpaydtl2))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                    if (dr[0].ToString() != null && dr[0].ToString() != string.Empty)
                        objDVOPayrollstypaydd.line_no = Convert.ToInt32(dr[0].ToString());
                    objDVOPayrollstypaydd.ded_code = dr[1].ToString();
                    if (dr[2].ToString() != null && dr[2].ToString() != string.Empty)
                        objDVOPayrollstypaydd.amount = Convert.ToDecimal(dr[2].ToString());
                    if (dr[3].ToString() != null && dr[3].ToString() != string.Empty)
                        objDVOPayrollstypaydd.ded_rate = Convert.ToDecimal(dr[3].ToString());
                    objDVOPayrollstypayddList.Add(objDVOPayrollstypaydd);
                }
            }
            return objDVOPayrollstypayddList;
        }

        public static int Get_dup_fla(string Emp_code)
        {
            int i = 0;
            object[] parameter = new object[1];
            parameter[0] = Emp_code;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object result = objDALBaseClass.ExecuteScalar(ref parameter, (new DVOPayrollProcess_PayEmployee()).FIND_dup_flag);
            i = Convert.ToInt32(result);
            return i;
        }
        public static int Get_dup_ssn(string soc_sec_num)
        {
            int i = 0;
            object[] parameter = new object[1];
            parameter[0] = soc_sec_num;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object result = objDALBaseClass.ExecuteScalar(ref parameter, (new DVOPayrollProcess_PayEmployee()).FIND_dup_ssn);
            i = Convert.ToInt32(result);
            return i;
        }
        public static DataTable ShowPayrollChecks(ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee, ref DVOPYBatchProcessStybatchr pObjBatch, out int recordsProcessed)
        {
            /*
             Added by Sarvjeet on 22/10/2010
             To implemented Payroll batch process into Show Payroll Checks. Nedd to add
             A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'ShowPayrollChecks' function
             and remove comment from the code written for batch process logic.   
         
           */
           
            DataSet ds = null;

            string PayMonth;
            string PayYear;
            //DateTime StarDatePayYear;
            //DateTime EndDatePayYear;
            DataTable objDataTable = new DataTable();
            object objTransaction = null;
            int curDocNo = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //Added by Sarvjeet on 22/01/2010..
            #region Declare Variables for Batch Process
            StringBuilder errorMassage = new StringBuilder();
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            int recordsSearched = 0;
            recordsProcessed = 0;
            bool IsProessIns = false;
            #endregion

            object objLockTransaction = null;
          
            DvoUpdatePayableDefDetails objAPDefault = new DvoUpdatePayableDefDetails();
            int _currentCheckNo = 0;
            try
            {
                //Added by Sarvjeet on 22/01/2010..
                #region Insert Process Start Info..
                object objTrx = null;
                objProcessDtl.pybatchid = pObjBatch.pybatchid;
                objProcessDtl.processname = "Show Payroll Checks";
                objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
                BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
                IsProessIns = true;
                #endregion

                object[] paramerers = new object[3];
                paramerers[0] = objDVOPayrollProcess_PayEmployee.deposit.Trim();
                paramerers[1] = objDVOPayrollProcess_PayEmployee.Cash_acct_no;
                paramerers[2] = objDVOPayrollProcess_PayEmployee.TypeCode.Trim();

                ds = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.FINND_PayrollCheck(ref paramerers));
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        //errorMassage.Append("[No Element to Process]");
                        return objDataTable;
                    }
                    recordsSearched = ds.Tables[0].Rows.Count;
                    #region Set the column name..and add columns ......
                    ds.Tables[0].Columns[0].ColumnName = "address1";
                    ds.Tables[0].Columns[1].ColumnName = "address2";
                    ds.Tables[0].Columns[2].ColumnName = "city";
                    ds.Tables[0].Columns[3].ColumnName = "first_name";
                    ds.Tables[0].Columns[4].ColumnName = "last_name";
                    ds.Tables[0].Columns[5].ColumnName = "middle_name";
                    ds.Tables[0].Columns[6].ColumnName = "soc_sec_num";
                    ds.Tables[0].Columns[7].ColumnName = "state";
                    ds.Tables[0].Columns[8].ColumnName = "zip";
                    ds.Tables[0].Columns[9].ColumnName = "cash_amount";
                    ds.Tables[0].Columns[10].ColumnName = "check_no";
                    ds.Tables[0].Columns[11].ColumnName = "department";
                    ds.Tables[0].Columns[12].ColumnName = "doc_date";
                    ds.Tables[0].Columns[13].ColumnName = "doc_no";
                    ds.Tables[0].Columns[14].ColumnName = "empl_code";
                    ds.Tables[0].Columns[15].ColumnName = "eop_date";
                    ds.Tables[0].Columns[16].ColumnName = "pay_date";
                    ds.Tables[0].Columns[17].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[18].ColumnName = "acct_desc";
                    ds.Tables[0].Columns.Add("inc_code");
                    ds.Tables[0].Columns.Add("inc_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_num");
                    ds.Tables[0].Columns.Add("inc_hours", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("ded_code");
                    ds.Tables[0].Columns.Add("ded_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("ded_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_err");
                    ds.Tables[0].Columns.Add("ded_err");
                    ds.Tables[0].Columns.Add("inc_count");
                    ds.Tables[0].Columns.Add("ded_count");
                    ds.Tables[0].Columns.Add("cheque_amt", typeof(decimal));

                    ds.Tables[0].Columns.Add("t_inc_num", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_hours", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_ded_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_ded_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("Departmentcoding", typeof(string));
                    ds.Tables[0].Columns.Add("EmployerName", typeof(string));
                    ds.Tables[0].Columns.Add("ProgramName", typeof(string));
                    objDataTable = ds.Tables[0].Clone();
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        return objDataTable;
                    }

                    #endregion Set the column name..and add columns ......

                    int dup_flag = 0;
                    int dup_ssn = 0;

                    decimal t_inc_num = 0;
                    decimal t_inc_hours = 0;
                    decimal t_inc_amount = 0;
                    decimal t_inc_ytd = 0;
                    decimal t_ded_amount = 0;
                    decimal t_ded_ytd = 0;

                    objLockTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
                    List<DvoUpdatePayableDefDetails> objstpcntrcList = BLLUpdatePayableDefaults.GetAllInfo();
                    if (objstpcntrcList != null && objstpcntrcList.Count > 0)
                    {
                        objAPDefault = objstpcntrcList[0];
                        int l = LockAPDefaultRecord(ref objLockTransaction, ref objAPDefault);
                        if (l == 1)
                        {
                            objstpcntrcList = BLLUpdatePayableDefaults.GetAllInfo();
                            if (objstpcntrcList != null && objstpcntrcList.Count > 0)
                            {
                                _currentCheckNo = objstpcntrcList[0].last_chkno;

                                #region Get All Data

                                //For get all Incomes of Persons
                                //DataSet dsIncome = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.GetListofPersonsIncome(ref paramerers));
                                DataSet dsIncome = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_checkpaydtl11);
                                if (dsIncome != null)
                                {
                                    dsIncome.Tables[0].Columns[0].ColumnName = "line_no";
                                    dsIncome.Tables[0].Columns[1].ColumnName = "inc_code";
                                    dsIncome.Tables[0].Columns[2].ColumnName = "number";
                                    dsIncome.Tables[0].Columns[3].ColumnName = "hours";
                                    dsIncome.Tables[0].Columns[4].ColumnName = "amount";
                                    dsIncome.Tables[0].Columns[5].ColumnName = "amount1";
                                    dsIncome.Tables[0].Columns[6].ColumnName = "doc_no";
                                    dsIncome.Tables[0].Columns[7].ColumnName = "empl_code";
                                }
                                // For Get All deduction of Persons
                                DataSet dsDeductions = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_checkpaydtl22);
                                if (dsDeductions != null)
                                {
                                    dsDeductions.Tables[0].Columns[0].ColumnName = "line_no";
                                    dsDeductions.Tables[0].Columns[1].ColumnName = "ded_code";
                                    dsDeductions.Tables[0].Columns[2].ColumnName = "amount";
                                    dsDeductions.Tables[0].Columns[3].ColumnName = "amount1";
                                    dsDeductions.Tables[0].Columns[4].ColumnName = "doc_no";
                                    dsDeductions.Tables[0].Columns[5].ColumnName = "empl_code";
                                }

                                DataSet ds_flag = objDALBaseClass.GetData(typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).FIND_dup_flag1);
                                if (ds_flag != null)
                                {
                                    ds_flag.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_flag.Tables[0].Columns[1].ColumnName = "count_flag";
                                }
                                DataSet ds_ssn = objDALBaseClass.GetData(typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).FIND_dup_ssn1);
                                if (ds_ssn != null)
                                {
                                    ds_ssn.Tables[0].Columns[0].ColumnName = "soc_sec_num";
                                    ds_ssn.Tables[0].Columns[1].ColumnName = "count_ssn";
                                }
                                //pay slip date
                                int _Year = DVOApplicationUserInfo.CurrentDate.Year;

                                //calculate amount income YTD total
                                object[] parameterytd1 = new object[1];
                                parameterytd1[0] = _Year;
                                DataSet ds_ytd1 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_stypayidamt1);
                                if (ds_ytd1 != null)
                                {
                                    ds_ytd1.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd1.Tables[0].Columns[1].ColumnName = "doc_no";
                                    ds_ytd1.Tables[0].Columns[2].ColumnName = "inc_code";
                                    ds_ytd1.Tables[0].Columns[3].ColumnName = "amount";
                                }

                                DataSet ds_ytd2 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_stypayidinc_ytd1);
                                if (ds_ytd2 != null)
                                {
                                    ds_ytd2.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd2.Tables[0].Columns[1].ColumnName = "inc_code";
                                    ds_ytd2.Tables[0].Columns[2].ColumnName = "soc_sec_num";
                                    ds_ytd2.Tables[0].Columns[3].ColumnName = "amount";
                                }

                                //for deduction YTD total
                                DataSet ds_ytd3 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_stypayddamt1);
                                if (ds_ytd2 != null)
                                {
                                    ds_ytd3.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd3.Tables[0].Columns[1].ColumnName = "doc_no";
                                    ds_ytd3.Tables[0].Columns[2].ColumnName = "ded_code";
                                    ds_ytd3.Tables[0].Columns[3].ColumnName = "amount";
                                }
                                DataSet ds_ytd4 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_stypayddinc_ytd1);
                                if (ds_ytd4 != null)
                                {
                                    ds_ytd4.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd4.Tables[0].Columns[1].ColumnName = "ded_code";
                                    ds_ytd4.Tables[0].Columns[2].ColumnName = "soc_sec_num";
                                    ds_ytd4.Tables[0].Columns[3].ColumnName = "amount";
                                }

                                #endregion Get All Data

                                #region Process

                                DataRow[] dra = ds.Tables[0].Select("", "empl_code,doc_no");
                                for (int i = 0; i < dra.Length; i++)
                                {
                                    DataRow dr = dra[i];
                                    DataRow drn = objDataTable.NewRow();
                                    DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee1 = new DVOPayrollProcess_PayEmployee();
                                    if (dr["doc_no"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.Doc_no = Convert.ToInt32(dr["doc_no"]);
                                    if (dr["cash_amount"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.cash_amount = Convert.ToDecimal(dr["cash_amount"]);
                                    if (dr["check_no"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.check_no = Convert.ToInt32(dr["check_no"]);
                                    if (dr["pay_date"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.pay_date = Convert.ToDateTime(dr["pay_date"]);
                                    objDVOPayrollProcess_PayEmployee1.EmplCode = dr["empl_code"].ToString().Trim();
                                    string LastName = dr["last_name"] != DBNull.Value ? Convert.ToString(dr["last_name"]).Trim() : string.Empty;
                                    if (LastName.Trim() != string.Empty)
                                        LastName = LastName + ",  ";
                                    string FirstName = dr["first_name"] != DBNull.Value ? Convert.ToString(dr["first_name"]).Trim() : string.Empty;

                                    string MiddleName = dr["middle_name"] != DBNull.Value ? Convert.ToString(dr["middle_name"]).Trim() : string.Empty;
                                    if (MiddleName.Trim() != string.Empty)
                                        MiddleName = ",  " + MiddleName;
                                    dr["last_name"] = LastName;
                                    dr["first_name"] = FirstName;
                                    dr["middle_name"] = MiddleName;
                                    curDocNo = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                    #region Commented Code
                                    //Get Keyvalue and acc_desc for employee cash account... 
                                    //Object[] Param = new object[2];
                                    //Param[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                    //Param[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                    //DataSet dscashkey = objDALBaseClass.GetData(ref Param, typeof(DVOPayrollProcess_PayEmployee), objDVOPayrollProcess_PayEmployee1.GET_CASHKEY);
                                    //if(dscashkey.Tables.Count>0)
                                    //    if (dscashkey.Tables[0].Rows.Count > 0)
                                    //    { 
                                    //dr["keyvalue"]=dscashkey.Tables[0].Rows[0][0];
                                    //dr["acct_desc"] = dscashkey.Tables[0].Rows[0][1];                      
                                    //}
                                    #endregion Commented Code

                                    #region processing on before Emp_Code group..
                                    bool _status = false;
                                    if (i != 0)
                                    {
                                        if (objDVOPayrollProcess_PayEmployee1.EmplCode != dra[i - 1]["empl_code"].ToString().Trim())
                                        {
                                            _status = true;
                                        }
                                    }
                                    else
                                    {
                                        if (i == 0)
                                        {
                                            _status = true;
                                        }
                                    }
                                    if (_status)
                                    {
                                        //dup_flag = Get_dup_fla(objDVOPayrollProcess_PayEmployee1.EmplCode);
                                        //dup_ssn = Get_dup_ssn(dr["soc_sec_num"].ToString());

                                        DataRow[] dra_flag = ds_flag.Tables[0].Select("( empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                                        if (dra_flag.Length > 0)
                                            dup_flag = Convert.ToInt32(dra_flag[0][1]);
                                        DataRow[] dra_ssn = ds_ssn.Tables[0].Select("( soc_sec_num ='" + dr["soc_sec_num"].ToString().Trim() + "')");
                                        if (dra_ssn.Length > 0)
                                            dup_ssn = Convert.ToInt32(dra_ssn[0][1]);
                                        String Employercode = BLLMasterEmployee.GetFlexDeptKeyvalueNew(objDVOPayrollProcess_PayEmployee1.EmplCode.Trim(), "EXPENS");
                                        dr["EmployerName"] = Employercode.ToString();
                                    }

                                    #endregion processing on before Emp_Code group..

                                    #region processing on before doc_no group..
                                    bool _postingStatus0 = false;
                                    if (i != 0)
                                    {
                                        if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(dra[i - 1]["doc_no"]))
                                        {
                                            _postingStatus0 = true;
                                        }
                                    }
                                    else if (i == 0)
                                        _postingStatus0 = true;
                                    if (_postingStatus0)
                                    {
                                        objTransaction = objDALBaseClassHelper.GetTransactionObject();

                                        int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                                        if (LockStatus != 1)
                                        {
                                            if (objTransaction != null)
                                            {
                                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                                errorMassage.Append("[ Could not lock DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                                            }
                                            continue;
                                        }
                                        if (objDVOPayrollProcess_PayEmployee1.check_no == 0)
                                        {
                                            objDVOPayrollProcess_PayEmployee1.check_no = ++_currentCheckNo;
                                            dr["check_no"] = objDVOPayrollProcess_PayEmployee1.check_no;

                                            //int check_no = BLLAccountingLiberary.Auto_Next_APCheckNo(ref objTransaction);
                                            ////if (check_no == 0)
                                            ////    throw new Exception("Error has occurred while generating check number.");
                                            //dr["check_no"] = check_no;
                                            //if (check_no <= 0)
                                            //{
                                            //    if (objTransaction != null)
                                            //    {
                                            //        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                            //        errorMassage.Append("[Could Not Generate Check Number for DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                                            //    }
                                            //    continue;
                                            //}

                                            //objDVOPayrollProcess_PayEmployee1.check_no = check_no;
                                        }
                                        if (objDVOPayrollProcess_PayEmployee1.cash_amount < 0)
                                        {
                                            objDVOPayrollProcess_PayEmployee1.cash_amount = 0;
                                        }
                                        if (objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime("01/01/1900") && objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime(null))
                                        {
                                            PayMonth = objDVOPayrollProcess_PayEmployee1.pay_date.Month.ToString();
                                            PayYear = objDVOPayrollProcess_PayEmployee1.pay_date.Year.ToString();
                                            //string sdpy = "01" + "/" + "01" + "/" + PayYear;
                                            //string edpy = "01" + "/" + "31" + "/" + PayYear;
                                            //StarDatePayYear = DateTime.ParseExact(sdpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                            //// EndDatePayYear = "01" + "/" + "31" + "/" + PayYear;
                                            //EndDatePayYear = DateTime.ParseExact(edpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                        }
                                    }
                                    #endregion process on before doc_no group..

                                    #region processing on_every_row ........
                              
                                    //List<DVOPayrollstypayid> objDVOPayrollstypayidList = GetEmployeeIcomeList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                                    //List<DVOPayrollstypaydd> objDVOPayrollstypayddList = GetEmployeeDedList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                                    //Added By Rahul jain on 16/01/2010
                                    DataRow[] draIncomes = dsIncome.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " and empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                                    //Added By Rahul jain on 16/01/2010
                                    DataRow[] draDeductions = dsDeductions.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " and empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");

                                    int inc_count = 0;
                                    int ded_count = 0;
                                    inc_count = draIncomes.Length;// objDVOPayrollstypayidList.Count;
                                    ded_count = draDeductions.Length;// objDVOPayrollstypayddList.Count;
                                    int loop_number = 0;

                                    if (inc_count > ded_count)
                                    {
                                        loop_number = inc_count;
                                    }
                                    else
                                    {
                                        loop_number = ded_count;
                                    }
                                    for (int j = 0; j < loop_number; j++)
                                    {
                                        // for income
                                        // Collect amounts from other payroll entries
                                        //  for the same employee code.
                                        if (j < inc_count)
                                        {
                                            if (dup_flag > 1)
                                            {
                                                decimal tmp_ytd_accrual1 = 0;
                                                DataRow[] draytd1 = ds_ytd1.Tables[0].Select("doc_no <> " + objDVOPayrollProcess_PayEmployee1.Doc_no + " AND empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND inc_code ='" + draIncomes[j][1].ToString().Trim() + "'");
                                                if (draytd1.Length > 0)
                                                {
                                                    for (int k = 0; k < draytd1.Length; k++)
                                                    {
                                                        tmp_ytd_accrual1 = tmp_ytd_accrual1 + (draytd1[k][3] != DBNull.Value ? Convert.ToDecimal(draytd1[k][3]) : 0);
                                                    }
                                                }

                                                //inc_rate means ytd_accrual.
                                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual1;

                                                //decimal tmp_ytd_accrual1 = 0;
                                                //object[] parameter1 = new object[4];
                                                //parameter1[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter1[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                                //parameter1[2] = draIncomes[j][1].ToString().Trim();//objDVOPayrollstypayidList[j].inc_code.Trim();
                                                //parameter1[3] = DVOApplicationUserInfo.CurrentDate.Year.ToString();
                                                /////***********************************************************
                                                /////Have to Add a Filter in stored Procedure to use Ok_to_post="Y"
                                                /////********************** 
                                                //DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                                //object result1 = objDALBaseClass.ExecuteScalar(ref parameter1, objDVOPayrollstypayid.FIND_stypayidamt);
                                                //if (result1.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual1 = Convert.ToDecimal(result1);
                                                //}
                                                ////inc_rate means ytd_accrual.
                                                ////objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual1;
                                                //draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual1;
                                            }
                                            //Collect accrual amounts from other employee
                                            //codes sharing the same soc. sec. num
                                            //This is typically the same person under a new tax
                                            //jurisdiction.
                                            if (dup_ssn > 1)
                                            {
                                                decimal tmp_ytd_accrual2 = 0;
                                                DataRow[] draytd2 = ds_ytd2.Tables[0].Select("soc_sec_num = '" + dr["soc_sec_num"].ToString().Trim() + "' AND empl_code <> '" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND inc_code ='" + draIncomes[j][1].ToString().Trim() + "'");
                                                if (draytd2.Length > 0)
                                                {
                                                    for (int f = 0; f < draytd2.Length; f++)
                                                    {
                                                        tmp_ytd_accrual2 = tmp_ytd_accrual2 + (draytd2[f][3] != DBNull.Value ? Convert.ToDecimal(draytd2[f][3]) : 0);
                                                    }
                                                }
                                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual2;


                                                //decimal tmp_ytd_accrual2 = 0;
                                                //object[] parameter2 = new object[3];
                                                //parameter2[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter2[1] = dr["soc_sec_num"].ToString().Trim();
                                                //parameter2[2] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                                //DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                                //object result2 = objDALBaseClass.ExecuteScalar(ref parameter2, objDVOPayrollstypayid.FIND_stypayidinc_ytd);
                                                //if (result2.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual2 = Convert.ToDecimal(result2);
                                                //}
                                                ////objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual2;
                                                //draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual2;
                                            }
                                            //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + objDVOPayrollstypayidList[j].amount;
                                            draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + Convert.ToDecimal(draIncomes[j][4]);
                                        }
                                        // for deduction 
                                        if (j < ded_count)
                                        {
                                            if (dup_flag > 1)
                                            {
                                                decimal tmp_ytd_accrual3 = 0;
                                                DataRow[] draytd3 = ds_ytd3.Tables[0].Select("doc_no <> " + objDVOPayrollProcess_PayEmployee1.Doc_no + " AND empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND ded_code ='" + draDeductions[j][1].ToString().Trim() + "'");
                                                if (draytd3.Length > 0)
                                                {
                                                    for (int p = 0; p < draytd3.Length; p++)
                                                    {
                                                        tmp_ytd_accrual3 = tmp_ytd_accrual3 + (draytd3[p][3] != DBNull.Value ? Convert.ToDecimal(draytd3[p][3]) : 0);
                                                    }
                                                }
                                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual3;//objDVOPayrollstypayddList[j].ded_rate
                                

                                                //decimal tmp_ytd_accrual3 = 0;
                                                //object[] parameter3 = new object[4];
                                                //parameter3[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter3[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                                //parameter3[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                                //paramerers[3] = DVOApplicationUserInfo.CurrentDate.Year.ToString();
                                                //DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                                //object result3 = objDALBaseClass.ExecuteScalar(ref parameter3, objDVOPayrollstypaydd.FIND_stypayddamt);
                                                //if (result3.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual3 = Convert.ToDecimal(result3);
                                                //}
                                                //draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual3;//objDVOPayrollstypayddList[j].ded_rate
                                            }
                                            if (dup_ssn > 1)
                                            {
                                                decimal tmp_ytd_accrual4 = 0;
                                                DataRow[] draytd4 = ds_ytd4.Tables[0].Select("soc_sec_num = '" + dr["soc_sec_num"].ToString().Trim() + "' AND empl_code <> '" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND ded_code ='" + draDeductions[j][1].ToString().Trim() + "'");
                                                if (draytd4.Length > 0)
                                                {
                                                    for (int q = 0; q < draytd4.Length; q++)
                                                    {
                                                        tmp_ytd_accrual4 = tmp_ytd_accrual4 + (draytd4[q][3] != DBNull.Value ? Convert.ToDecimal(draytd4[q][3]) : 0);
                                                    }
                                                }
                                                //objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[j].ded_rate + tmp_ytd_accrual4;
                                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual4;//objDVOPayrollstypayddList[j].ded_rate
                                

                                                //decimal tmp_ytd_accrual4 = 0;
                                                //object[] parameter4 = new object[3];
                                                //parameter4[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter4[1] = dr["soc_sec_num"].ToString().Trim();
                                                //parameter4[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                                //DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                                //object result4 = objDALBaseClass.ExecuteScalar(ref parameter4, objDVOPayrollstypaydd.FIND_stypayddinc_ytd);
                                                //if (result4.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual4 = Convert.ToDecimal(result4);
                                                //}
                                                ////objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[j].ded_rate + tmp_ytd_accrual4;
                                                //draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual4;//objDVOPayrollstypayddList[j].ded_rate
                                            }
                                            draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + Convert.ToDecimal(draDeductions[j][2]);//objDVOPayrollstypayddList[j].ded_rate + objDVOPayrollstypayddList[j].amount;
                                        }


                                        // process 9 lines
                                        //for (int j = 0; j < loop_number; j++)
                                        //{
                                        DataRow drn1 = objDataTable.NewRow();
                                        drn1.ItemArray = dr.ItemArray;
                                        if (j < inc_count)
                                        {
                                            //set the income fields
                                            drn1["inc_code"] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                            drn1["inc_num"] = draIncomes[j][2];// objDVOPayrollstypayidList[j].number;
                                            drn1["inc_hours"] = draIncomes[j][3];//objDVOPayrollstypayidList[j].hours;
                                            drn1["inc_amount"] = draIncomes[j][4];//objDVOPayrollstypayidList[j].amount;
                                            drn1["inc_ytd"] = draIncomes[j][5];//objDVOPayrollstypayidList[j].inc_rate;

                                            t_inc_num = t_inc_num + (draIncomes[j][2] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][2]) : 0);//objDVOPayrollstypayidList[j].number
                                            t_inc_hours = t_inc_hours + (draIncomes[j][3] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][3]) : 0);//objDVOPayrollstypayidList[j].hours
                                            t_inc_amount = t_inc_amount + (draIncomes[j][4] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][4]) : 0);//objDVOPayrollstypayidList[j].amount
                                            t_inc_ytd = t_inc_ytd + (draIncomes[j][5] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][5]) : 0);//objDVOPayrollstypayidList[j].inc_rate
                                        }
                                        if (j < ded_count)
                                        {
                                            // set the deduction fields
                                            drn1["ded_code"] = draDeductions[j][1].ToString().Trim();//objDVOPayrollstypayddList[j].ded_code;
                                            drn1["ded_amount"] = draDeductions[j][2].ToString().Trim();//objDVOPayrollstypayddList[j].amount;
                                            drn1["ded_ytd"] = draDeductions[j][3].ToString().Trim();//objDVOPayrollstypayddList[j].ded_rate;

                                            t_ded_ytd = t_ded_ytd + (draDeductions[j][3] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][3]) : 0);// objDVOPayrollstypayddList[j].ded_rate ?? 0;//make nullable decimal By Rahul
                                            t_ded_amount = t_ded_amount + (draDeductions[j][2] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][2]) : 0);//objDVOPayrollstypayddList[j].amount ?? 0; //make nullable decimal By Rahul
                                        }
                                        objDataTable.Rows.Add(drn1);
                                        //}
                                    } // end for loop..
                                    #region Commented Code....................
                                    //for (int j = 0; j < inc_count - 1; j++)
                                    //{
                                    //    if (objDVOPayrollstypayidList[j].amount == 0)
                                    //    {
                                    //        for (int k = j + 1; k < inc_count; k++)
                                    //        {
                                    //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                                    //            {  
                                    //                if(k<inc_count)
                                    //                {
                                    //                string tmp_hold_inc_code = objDVOPayrollstypayidList[j].inc_code;
                                    //                int tmp_hold_inc_line_no = objDVOPayrollstypayidList[j].line_no;
                                    //                decimal tmp_hold_inc_number = objDVOPayrollstypayidList[j].number;
                                    //                decimal tmp_hold_inc_hours = objDVOPayrollstypayidList[j].hours;
                                    //                decimal tmp_hold_inc_amount = objDVOPayrollstypayidList[j].amount;
                                    //                decimal tmp_hold_inc_accrual = objDVOPayrollstypayidList[j].inc_rate;

                                    //                objDVOPayrollstypayidList[j].inc_code = objDVOPayrollstypayidList[k].inc_code; ;
                                    //                objDVOPayrollstypayidList[j].line_no = objDVOPayrollstypayidList[k].line_no; ;
                                    //                objDVOPayrollstypayidList[j].number = objDVOPayrollstypayidList[k].number;
                                    //                objDVOPayrollstypayidList[j].hours = objDVOPayrollstypayidList[k].hours;
                                    //                objDVOPayrollstypayidList[j].amount = objDVOPayrollstypayidList[k].amount;
                                    //                objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[k].inc_rate;

                                    //                objDVOPayrollstypayidList[k].inc_code = tmp_hold_inc_code;
                                    //                objDVOPayrollstypayidList[k].line_no = tmp_hold_inc_line_no;
                                    //                objDVOPayrollstypayidList[k].number = tmp_hold_inc_number;
                                    //                objDVOPayrollstypayidList[k].hours = tmp_hold_inc_hours;
                                    //                objDVOPayrollstypayidList[k].amount = tmp_hold_inc_amount;
                                    //                objDVOPayrollstypayidList[k].inc_rate = tmp_hold_inc_accrual;
                                    //                }

                                    //            }
                                    //        }
                                    //    }
                                    //}

                                    //for (int j = 0; j < ded_count - 1; j++)
                                    //{
                                    //    if (objDVOPayrollstypayddList[j].amount == 0)
                                    //    {
                                    //        for (int k = j + 1; k < inc_count; k++)
                                    //        {
                                    //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                                    //            {  
                                    //                if(k<ded_count)
                                    //                {
                                    //                string tmp_hold_ded_code = objDVOPayrollstypayddList[j].ded_code;
                                    //                int tmp_hold_ded_line_no = objDVOPayrollstypayddList[j].line_no;
                                    //                decimal tmp_hold_ded_amount = objDVOPayrollstypayddList[j].amount;
                                    //                decimal tmp_hold_ded_accrual = objDVOPayrollstypayddList[j].ded_rate;


                                    //                objDVOPayrollstypayddList[j].ded_code = objDVOPayrollstypayddList[k].ded_code;
                                    //                objDVOPayrollstypayddList[j].line_no = objDVOPayrollstypayddList[k].line_no;
                                    //                objDVOPayrollstypayddList[j].amount = objDVOPayrollstypayddList[k].amount;
                                    //                objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[k].ded_rate;

                                    //                objDVOPayrollstypayddList[k].ded_code = tmp_hold_ded_code;
                                    //                objDVOPayrollstypayddList[k].line_no = tmp_hold_ded_line_no;
                                    //                objDVOPayrollstypayddList[k].amount = tmp_hold_ded_amount;
                                    //                objDVOPayrollstypayddList[k].ded_rate = tmp_hold_ded_accrual;
                                    //                }

                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    #endregion Commented Code

                                    #region Commented Code...................
                                    //if (inc_count > 9)
                                    //{
                                    //    for (int j = 10; j <= inc_count; j++)
                                    //    {
                                    //        //set the income fields
                                    //        t_inc_num = t_inc_num + objDVOPayrollstypayidList[j].number;
                                    //        t_inc_hours = t_inc_hours + objDVOPayrollstypayidList[j].hours;
                                    //        t_inc_amount = t_inc_amount + objDVOPayrollstypayidList[j].amount;
                                    //        t_inc_ytd = t_inc_ytd + objDVOPayrollstypayidList[j].inc_rate;
                                    //    }
                                    //}
                                    //if (ded_count > 9)
                                    //{
                                    //    for (int j = 10; j <= ded_count; j++)
                                    //    {
                                    //        t_ded_ytd = t_ded_ytd + objDVOPayrollstypayddList[j].ded_ytd;
                                    //        t_ded_amount = t_ded_amount + objDVOPayrollstypayddList[j].amount;

                                    //    }
                                    //}
                                    #endregion Commented Code...................

                                    #endregion processing on_every_row ........

                                    #region process on doc_no group.........
                                    bool _postingStatus = false;
                                    if (dra.Length != i + 1)
                                    {
                                        if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(dra[i + 1]["doc_no"]))
                                        {
                                            _postingStatus = true;
                                        }
                                    }
                                    else if (dra.Length == i + 1)
                                        _postingStatus = true;

                                    if (_postingStatus && loop_number > 0)
                                    {
                                        //update Process_PayEmployee
                                        if (objDVOPayrollProcess_PayEmployee1.check_no != 0)
                                        {
                                            object[] updParameters = new object[3];
                                            updParameters[0] = objDVOPayrollProcess_PayEmployee1.check_no;
                                            updParameters[1] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                            updParameters[2] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                            Int32 Process_PayEmployee_status = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updParameters, objDVOPayrollProcess_PayEmployee.update_Process_PayEmployee1);
                                            if (Process_PayEmployee_status == null || Process_PayEmployee_status.ToString().Trim() == string.Empty || Convert.ToInt32(Process_PayEmployee_status) != 1)
                                                throw new Exception("Error has occurred while updating Process_PayEmployee.");
                                        }
                                        //commit work
                                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                        recordsProcessed++;
                                        //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                        if (objDataTable.Rows.Count > 0)
                                        {
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_num"] = t_inc_num;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_hours"] = t_inc_hours;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_amount"] = t_inc_amount;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_ytd"] = t_inc_ytd;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_ytd"] = t_ded_ytd;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_amount"] = t_ded_amount;
                                        }
                                        // initialize the totals
                                        t_inc_num = 0;
                                        t_inc_hours = 0;
                                        t_inc_amount = 0;
                                        t_inc_ytd = 0;
                                        t_ded_amount = 0;
                                        t_ded_ytd = 0;

                                    }
                                    else
                                    {
                                        //release & rollback current document
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                        errorMassage.Append("[ There is no Income and Deduction for DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                                    }
                                    #endregion process on doc_no group.........
                                }
                                //remove all rows from table, where checkno is '0' or blank or null
                                for (int i = 0; i < objDataTable.Rows.Count; i++)
                                {
                                    DataRow dr = objDataTable.Rows[i];
                                    if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                                    {
                                        objDataTable.Rows.Remove(dr);
                                        objDataTable.AcceptChanges();
                                        i--;
                                    }
                                }

                                //foreach (DataRow dr in objDataTable.Rows)
                                //{
                                //    if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                                //        objDataTable.Rows.Remove(dr);
                                //}
                                objDataTable.AcceptChanges();

                                #endregion Process
                            }
                            ReleaseAndUpdateAPDefaultRecord(true, false, ref objLockTransaction, ref objAPDefault, _currentCheckNo);
                        }
                    }
                }
                else
                {
                    //errorMassage.Append("[No Element to Process]");
                    return objDataTable;
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", curDocNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
                if (objDataTable.Columns.Count > 0)
                {
                    #region remove_rows
                    DataRow[] rm_rows = objDataTable.Select("doc_no = " + curDocNo);
                    for (int k = 0; k < rm_rows.Length; k++)
                    {
                        int rm_index = objDataTable.Rows.Count - 1;
                        objDataTable.Rows.RemoveAt(rm_index);
                    }
                    #endregion
                    System.Windows.Forms.MessageBox.Show("Some checks will not show,Error:" + ex.Message, "JKPS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return objDataTable;
                }
                else
                {
                    throw ex;
                }
                ExceptionManagement.ExceptionManager.Publish(ex);
                errorMassage.Append("[" + ex.Message + "]");

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
                    obj.processname = "Show Payroll Checks";
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
            return objDataTable;
        }

        public static DataTable ShowPayrollChecksWithDepartment(ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee, ref DVOPYBatchProcessStybatchr pObjBatch, out int recordsProcessed)
        {
            /*
             Added by Sarvjeet on 22/10/2010
             To implemented Payroll batch process into Show Payroll Checks. Nedd to add
             A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'ShowPayrollChecks' function
             and remove comment from the code written for batch process logic.   
         
           */
            DataSet ds = null;
            string PayMonth;
            string PayYear;
            //DateTime StarDatePayYear;
            //DateTime EndDatePayYear;
            DataTable objDataTable = new DataTable();
            object objTransaction = null;
            int curDocNo = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //Added by Sarvjeet on 22/01/2010..
            #region Declare Variables for Batch Process
            StringBuilder errorMassage = new StringBuilder();
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            int recordsSearched = 0;
            recordsProcessed = 0;
            bool IsProessIns = false;
            #endregion

            object objLockTransaction = null;
            DvoUpdatePayableDefDetails objAPDefault = new DvoUpdatePayableDefDetails();
            int _currentCheckNo = 0;
            try
            {
                //Added by Sarvjeet on 22/01/2010..
                #region Insert Process Start Info..
                object objTrx = null;
                objProcessDtl.pybatchid = pObjBatch.pybatchid;
                objProcessDtl.processname = "Show Payroll Checks";
                objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
                BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
                IsProessIns = true;
                #endregion

                object[] paramerers = new object[3];
                paramerers[0] = objDVOPayrollProcess_PayEmployee.deposit.Trim();
                paramerers[1] = objDVOPayrollProcess_PayEmployee.Cash_acct_no;
                paramerers[2] = objDVOPayrollProcess_PayEmployee.TypeCode.Trim();

                ds = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.FINND_PayrollCheckWithDepartment(ref paramerers));
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        //errorMassage.Append("[No Element to Process]");
                        return objDataTable;
                    }
                    recordsSearched = ds.Tables[0].Rows.Count;
                    #region Set the column name..and add columns ......
                    ds.Tables[0].Columns[0].ColumnName = "address1";
                    ds.Tables[0].Columns[1].ColumnName = "address2";
                    ds.Tables[0].Columns[2].ColumnName = "city";
                    ds.Tables[0].Columns[3].ColumnName = "first_name";
                    ds.Tables[0].Columns[4].ColumnName = "last_name";
                    ds.Tables[0].Columns[5].ColumnName = "middle_name";
                    ds.Tables[0].Columns[6].ColumnName = "soc_sec_num";
                    ds.Tables[0].Columns[7].ColumnName = "state";
                    ds.Tables[0].Columns[8].ColumnName = "zip";
                    ds.Tables[0].Columns[9].ColumnName = "cash_amount";
                    ds.Tables[0].Columns[10].ColumnName = "check_no";
                    ds.Tables[0].Columns[11].ColumnName = "department";
                    ds.Tables[0].Columns[12].ColumnName = "doc_date";
                    ds.Tables[0].Columns[13].ColumnName = "doc_no";
                    ds.Tables[0].Columns[14].ColumnName = "empl_code";
                    ds.Tables[0].Columns[15].ColumnName = "eop_date";
                    ds.Tables[0].Columns[16].ColumnName = "pay_date";
                    ds.Tables[0].Columns[17].ColumnName = "keyvalue";
                    ds.Tables[0].Columns[18].ColumnName = "acct_desc";
                    ds.Tables[0].Columns.Add("inc_code");
                    ds.Tables[0].Columns.Add("inc_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_num");
                    ds.Tables[0].Columns.Add("inc_hours", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("ded_code");
                    ds.Tables[0].Columns.Add("ded_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("ded_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("inc_err");
                    ds.Tables[0].Columns.Add("ded_err");
                    ds.Tables[0].Columns.Add("inc_count");
                    ds.Tables[0].Columns.Add("ded_count");
                    ds.Tables[0].Columns.Add("cheque_amt", typeof(decimal));

                    ds.Tables[0].Columns.Add("t_inc_num", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_hours", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_ded_amount", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_ded_ytd", typeof(decimal));
                    ds.Tables[0].Columns.Add("t_inc_ytd", typeof(decimal));
                    objDataTable = ds.Tables[0].Clone();
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        return objDataTable;
                    }

                    #endregion Set the column name..and add columns ......

                    int dup_flag = 0;
                    int dup_ssn = 0;

                    decimal t_inc_num = 0;
                    decimal t_inc_hours = 0;
                    decimal t_inc_amount = 0;
                    decimal t_inc_ytd = 0;
                    decimal t_ded_amount = 0;
                    decimal t_ded_ytd = 0;

                    objLockTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
                    List<DvoUpdatePayableDefDetails> objstpcntrcList = BLLUpdatePayableDefaults.GetAllInfo();
                    if (objstpcntrcList != null && objstpcntrcList.Count > 0)
                    {
                        objAPDefault = objstpcntrcList[0];
                        int l = LockAPDefaultRecord(ref objLockTransaction, ref objAPDefault);
                        if (l == 1)
                        {
                            objstpcntrcList = BLLUpdatePayableDefaults.GetAllInfo();
                            if (objstpcntrcList != null && objstpcntrcList.Count > 0)
                            {
                                _currentCheckNo = objstpcntrcList[0].last_chkno;

                                #region Get All Data

                                //For get all Incomes of Persons
                                //DataSet dsIncome = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.GetListofPersonsIncome(ref paramerers));
                                DataSet dsIncome = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_checkpaydtl11);
                                if (dsIncome != null)
                                {
                                    dsIncome.Tables[0].Columns[0].ColumnName = "line_no";
                                    dsIncome.Tables[0].Columns[1].ColumnName = "inc_code";
                                    dsIncome.Tables[0].Columns[2].ColumnName = "number";
                                    dsIncome.Tables[0].Columns[3].ColumnName = "hours";
                                    dsIncome.Tables[0].Columns[4].ColumnName = "amount";
                                    dsIncome.Tables[0].Columns[5].ColumnName = "amount1";
                                    dsIncome.Tables[0].Columns[6].ColumnName = "doc_no";
                                    dsIncome.Tables[0].Columns[7].ColumnName = "empl_code";
                                }
                                // For Get All deduction of Persons
                                DataSet dsDeductions = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_checkpaydtl22);
                                if (dsDeductions != null)
                                {
                                    dsDeductions.Tables[0].Columns[0].ColumnName = "line_no";
                                    dsDeductions.Tables[0].Columns[1].ColumnName = "ded_code";
                                    dsDeductions.Tables[0].Columns[2].ColumnName = "amount";
                                    dsDeductions.Tables[0].Columns[3].ColumnName = "amount1";
                                    dsDeductions.Tables[0].Columns[4].ColumnName = "doc_no";
                                    dsDeductions.Tables[0].Columns[5].ColumnName = "empl_code";
                                }

                                DataSet ds_flag = objDALBaseClass.GetData(typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).FIND_dup_flag1);
                                if (ds_flag != null)
                                {
                                    ds_flag.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_flag.Tables[0].Columns[1].ColumnName = "count_flag";
                                }
                                DataSet ds_ssn = objDALBaseClass.GetData(typeof(DVOPayrollProcess_PayEmployee), (new DVOPayrollProcess_PayEmployee()).FIND_dup_ssn1);
                                if (ds_ssn != null)
                                {
                                    ds_ssn.Tables[0].Columns[0].ColumnName = "soc_sec_num";
                                    ds_ssn.Tables[0].Columns[1].ColumnName = "count_ssn";
                                }
                                //pay slip date
                                int _Year = DVOApplicationUserInfo.CurrentDate.Year;

                                //calculate amount income YTD total
                                object[] parameterytd1 = new object[1];
                                parameterytd1[0] = _Year;
                                DataSet ds_ytd1 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_stypayidamt1);
                                if (ds_ytd1 != null)
                                {
                                    ds_ytd1.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd1.Tables[0].Columns[1].ColumnName = "doc_no";
                                    ds_ytd1.Tables[0].Columns[2].ColumnName = "inc_code";
                                    ds_ytd1.Tables[0].Columns[3].ColumnName = "amount";
                                }

                                DataSet ds_ytd2 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_stypayidinc_ytd1);
                                if (ds_ytd2 != null)
                                {
                                    ds_ytd2.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd2.Tables[0].Columns[1].ColumnName = "inc_code";
                                    ds_ytd2.Tables[0].Columns[2].ColumnName = "soc_sec_num";
                                    ds_ytd2.Tables[0].Columns[3].ColumnName = "amount";
                                }

                                //for deduction YTD total
                                DataSet ds_ytd3 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_stypayddamt1);
                                if (ds_ytd2 != null)
                                {
                                    ds_ytd3.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd3.Tables[0].Columns[1].ColumnName = "doc_no";
                                    ds_ytd3.Tables[0].Columns[2].ColumnName = "ded_code";
                                    ds_ytd3.Tables[0].Columns[3].ColumnName = "amount";
                                }
                                DataSet ds_ytd4 = objDALBaseClass.GetData(ref parameterytd1, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_stypayddinc_ytd1);
                                if (ds_ytd4 != null)
                                {
                                    ds_ytd4.Tables[0].Columns[0].ColumnName = "empl_code";
                                    ds_ytd4.Tables[0].Columns[1].ColumnName = "ded_code";
                                    ds_ytd4.Tables[0].Columns[2].ColumnName = "soc_sec_num";
                                    ds_ytd4.Tables[0].Columns[3].ColumnName = "amount";
                                }

                                #endregion Get All Data

                                #region Process

                                DataRow[] dra = ds.Tables[0].Select("", "empl_code,doc_no");
                                for (int i = 0; i < dra.Length; i++)
                                {
                                    DataRow dr = dra[i];
                                    DataRow drn = objDataTable.NewRow();
                                    DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee1 = new DVOPayrollProcess_PayEmployee();
                                    if (dr["doc_no"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.Doc_no = Convert.ToInt32(dr["doc_no"]);
                                    if (dr["cash_amount"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.cash_amount = Convert.ToDecimal(dr["cash_amount"]);
                                    if (dr["check_no"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.check_no = Convert.ToInt32(dr["check_no"]);
                                    if (dr["pay_date"] != DBNull.Value)
                                        objDVOPayrollProcess_PayEmployee1.pay_date = Convert.ToDateTime(dr["pay_date"]);
                                    objDVOPayrollProcess_PayEmployee1.EmplCode = dr["empl_code"].ToString().Trim();
                                    string LastName = dr["last_name"] != DBNull.Value ? Convert.ToString(dr["last_name"]).Trim() : string.Empty;
                                    if (LastName.Trim() != string.Empty)
                                        LastName = LastName + ",  ";
                                    string FirstName = dr["first_name"] != DBNull.Value ? Convert.ToString(dr["first_name"]).Trim() : string.Empty;

                                    string MiddleName = dr["middle_name"] != DBNull.Value ? Convert.ToString(dr["middle_name"]).Trim() : string.Empty;
                                    if (MiddleName.Trim() != string.Empty)
                                        MiddleName = ",  " + MiddleName;
                                    dr["last_name"] = LastName;
                                    dr["first_name"] = FirstName;
                                    dr["middle_name"] = MiddleName;
                                    curDocNo = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                    #region Commented Code
                                    //Get Keyvalue and acc_desc for employee cash account... 
                                    //Object[] Param = new object[2];
                                    //Param[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                    //Param[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                    //DataSet dscashkey = objDALBaseClass.GetData(ref Param, typeof(DVOPayrollProcess_PayEmployee), objDVOPayrollProcess_PayEmployee1.GET_CASHKEY);
                                    //if(dscashkey.Tables.Count>0)
                                    //    if (dscashkey.Tables[0].Rows.Count > 0)
                                    //    { 
                                    //dr["keyvalue"]=dscashkey.Tables[0].Rows[0][0];
                                    //dr["acct_desc"] = dscashkey.Tables[0].Rows[0][1];                      
                                    //}
                                    #endregion Commented Code

                                    #region processing on before Emp_Code group..
                                    bool _status = false;
                                    if (i != 0)
                                    {
                                        if (objDVOPayrollProcess_PayEmployee1.EmplCode != dra[i - 1]["empl_code"].ToString().Trim())
                                        {
                                            _status = true;
                                        }
                                    }
                                    else
                                    {
                                        if (i == 0)
                                        {
                                            _status = true;
                                        }
                                    }
                                    if (_status)
                                    {
                                        //dup_flag = Get_dup_fla(objDVOPayrollProcess_PayEmployee1.EmplCode);
                                        //dup_ssn = Get_dup_ssn(dr["soc_sec_num"].ToString());

                                        DataRow[] dra_flag = ds_flag.Tables[0].Select("( empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                                        if (dra_flag.Length > 0)
                                            dup_flag = Convert.ToInt32(dra_flag[0][1]);
                                        DataRow[] dra_ssn = ds_ssn.Tables[0].Select("( soc_sec_num ='" + dr["soc_sec_num"].ToString().Trim() + "')");
                                        if (dra_ssn.Length > 0)
                                            dup_ssn = Convert.ToInt32(dra_ssn[0][1]);
                                    }

                                    #endregion processing on before Emp_Code group..

                                    #region processing on before doc_no group..
                                    bool _postingStatus0 = false;
                                    if (i != 0)
                                    {
                                        if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(dra[i - 1]["doc_no"]))
                                        {
                                            _postingStatus0 = true;
                                        }
                                    }
                                    else if (i == 0)
                                        _postingStatus0 = true;
                                    if (_postingStatus0)
                                    {
                                        objTransaction = objDALBaseClassHelper.GetTransactionObject();

                                        int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                                        if (LockStatus != 1)
                                        {
                                            if (objTransaction != null)
                                            {
                                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                                errorMassage.Append("[ Could not lock DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                                            }
                                            continue;
                                        }
                                        if (objDVOPayrollProcess_PayEmployee1.check_no == 0)
                                        {
                                            objDVOPayrollProcess_PayEmployee1.check_no = ++_currentCheckNo;
                                            dr["check_no"] = objDVOPayrollProcess_PayEmployee1.check_no;

                                            //int check_no = BLLAccountingLiberary.Auto_Next_APCheckNo(ref objTransaction);
                                            ////if (check_no == 0)
                                            ////    throw new Exception("Error has occurred while generating check number.");
                                            //dr["check_no"] = check_no;
                                            //if (check_no <= 0)
                                            //{
                                            //    if (objTransaction != null)
                                            //    {
                                            //        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                            //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                            //        errorMassage.Append("[Could Not Generate Check Number for DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                                            //    }
                                            //    continue;
                                            //}

                                            //objDVOPayrollProcess_PayEmployee1.check_no = check_no;
                                        }
                                        if (objDVOPayrollProcess_PayEmployee1.cash_amount < 0)
                                        {
                                            objDVOPayrollProcess_PayEmployee1.cash_amount = 0;
                                        }
                                        if (objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime("01/01/1900") && objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime(null))
                                        {
                                            PayMonth = objDVOPayrollProcess_PayEmployee1.pay_date.Month.ToString();
                                            PayYear = objDVOPayrollProcess_PayEmployee1.pay_date.Year.ToString();
                                            //string sdpy = "01" + "/" + "01" + "/" + PayYear;
                                            //string edpy = "01" + "/" + "31" + "/" + PayYear;
                                            //StarDatePayYear = DateTime.ParseExact(sdpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                            //// EndDatePayYear = "01" + "/" + "31" + "/" + PayYear;
                                            //EndDatePayYear = DateTime.ParseExact(edpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                        }
                                    }
                                    #endregion process on before doc_no group..

                                    #region processing on_every_row ........

                                    //List<DVOPayrollstypayid> objDVOPayrollstypayidList = GetEmployeeIcomeList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                                    //List<DVOPayrollstypaydd> objDVOPayrollstypayddList = GetEmployeeDedList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                                    //Added By Rahul jain on 16/01/2010
                                    DataRow[] draIncomes = dsIncome.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " and empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                                    //Added By Rahul jain on 16/01/2010
                                    DataRow[] draDeductions = dsDeductions.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " and empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");

                                    int inc_count = 0;
                                    int ded_count = 0;
                                    inc_count = draIncomes.Length;// objDVOPayrollstypayidList.Count;
                                    ded_count = draDeductions.Length;// objDVOPayrollstypayddList.Count;
                                    int loop_number = 0;

                                    if (inc_count > ded_count)
                                    {
                                        loop_number = inc_count;
                                    }
                                    else
                                    {
                                        loop_number = ded_count;
                                    }
                                    for (int j = 0; j < loop_number; j++)
                                    {
                                        // for income
                                        // Collect amounts from other payroll entries
                                        //  for the same employee code.
                                        if (j < inc_count)
                                        {
                                            if (dup_flag > 1)
                                            {
                                                decimal tmp_ytd_accrual1 = 0;
                                                DataRow[] draytd1 = ds_ytd1.Tables[0].Select("doc_no <> " + objDVOPayrollProcess_PayEmployee1.Doc_no + " AND empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND inc_code ='" + draIncomes[j][1].ToString().Trim() + "'");
                                                if (draytd1.Length > 0)
                                                {
                                                    for (int k = 0; k < draytd1.Length; k++)
                                                    {
                                                        tmp_ytd_accrual1 = tmp_ytd_accrual1 + (draytd1[k][3] != DBNull.Value ? Convert.ToDecimal(draytd1[k][3]) : 0);
                                                    }
                                                }

                                                //inc_rate means ytd_accrual.
                                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual1;

                                                //decimal tmp_ytd_accrual1 = 0;
                                                //object[] parameter1 = new object[4];
                                                //parameter1[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter1[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                                //parameter1[2] = draIncomes[j][1].ToString().Trim();//objDVOPayrollstypayidList[j].inc_code.Trim();
                                                //parameter1[3] = DVOApplicationUserInfo.CurrentDate.Year.ToString();
                                                /////***********************************************************
                                                /////Have to Add a Filter in stored Procedure to use Ok_to_post="Y"
                                                /////********************** 
                                                //DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                                //object result1 = objDALBaseClass.ExecuteScalar(ref parameter1, objDVOPayrollstypayid.FIND_stypayidamt);
                                                //if (result1.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual1 = Convert.ToDecimal(result1);
                                                //}
                                                ////inc_rate means ytd_accrual.
                                                ////objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual1;
                                                //draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual1;
                                            }
                                            //Collect accrual amounts from other employee
                                            //codes sharing the same soc. sec. num
                                            //This is typically the same person under a new tax
                                            //jurisdiction.
                                            if (dup_ssn > 1)
                                            {
                                                decimal tmp_ytd_accrual2 = 0;
                                                DataRow[] draytd2 = ds_ytd2.Tables[0].Select("soc_sec_num = '" + dr["soc_sec_num"].ToString().Trim() + "' AND empl_code <> '" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND inc_code ='" + draIncomes[j][1].ToString().Trim() + "'");
                                                if (draytd2.Length > 0)
                                                {
                                                    for (int f = 0; f < draytd2.Length; f++)
                                                    {
                                                        tmp_ytd_accrual2 = tmp_ytd_accrual2 + (draytd2[f][3] != DBNull.Value ? Convert.ToDecimal(draytd2[f][3]) : 0);
                                                    }
                                                }
                                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual2;


                                                //decimal tmp_ytd_accrual2 = 0;
                                                //object[] parameter2 = new object[3];
                                                //parameter2[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter2[1] = dr["soc_sec_num"].ToString().Trim();
                                                //parameter2[2] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                                //DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                                //object result2 = objDALBaseClass.ExecuteScalar(ref parameter2, objDVOPayrollstypayid.FIND_stypayidinc_ytd);
                                                //if (result2.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual2 = Convert.ToDecimal(result2);
                                                //}
                                                ////objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual2;
                                                //draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual2;
                                            }
                                            //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + objDVOPayrollstypayidList[j].amount;
                                            draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + Convert.ToDecimal(draIncomes[j][4]);
                                        }
                                        // for deduction 
                                        if (j < ded_count)
                                        {
                                            if (dup_flag > 1)
                                            {
                                                decimal tmp_ytd_accrual3 = 0;
                                                DataRow[] draytd3 = ds_ytd3.Tables[0].Select("doc_no <> " + objDVOPayrollProcess_PayEmployee1.Doc_no + " AND empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND ded_code ='" + draDeductions[j][1].ToString().Trim() + "'");
                                                if (draytd3.Length > 0)
                                                {
                                                    for (int p = 0; p < draytd3.Length; p++)
                                                    {
                                                        tmp_ytd_accrual3 = tmp_ytd_accrual3 + (draytd3[p][3] != DBNull.Value ? Convert.ToDecimal(draytd3[p][3]) : 0);
                                                    }
                                                }
                                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual3;//objDVOPayrollstypayddList[j].ded_rate


                                                //decimal tmp_ytd_accrual3 = 0;
                                                //object[] parameter3 = new object[4];
                                                //parameter3[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter3[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                                //parameter3[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                                //paramerers[3] = DVOApplicationUserInfo.CurrentDate.Year.ToString();
                                                //DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                                //object result3 = objDALBaseClass.ExecuteScalar(ref parameter3, objDVOPayrollstypaydd.FIND_stypayddamt);
                                                //if (result3.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual3 = Convert.ToDecimal(result3);
                                                //}
                                                //draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual3;//objDVOPayrollstypayddList[j].ded_rate
                                            }
                                            if (dup_ssn > 1)
                                            {
                                                decimal tmp_ytd_accrual4 = 0;
                                                DataRow[] draytd4 = ds_ytd4.Tables[0].Select("soc_sec_num = '" + dr["soc_sec_num"].ToString().Trim() + "' AND empl_code <> '" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "'" + " AND ded_code ='" + draDeductions[j][1].ToString().Trim() + "'");
                                                if (draytd4.Length > 0)
                                                {
                                                    for (int q = 0; q < draytd4.Length; q++)
                                                    {
                                                        tmp_ytd_accrual4 = tmp_ytd_accrual4 + (draytd4[q][3] != DBNull.Value ? Convert.ToDecimal(draytd4[q][3]) : 0);
                                                    }
                                                }
                                                //objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[j].ded_rate + tmp_ytd_accrual4;
                                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual4;//objDVOPayrollstypayddList[j].ded_rate


                                                //decimal tmp_ytd_accrual4 = 0;
                                                //object[] parameter4 = new object[3];
                                                //parameter4[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                                //parameter4[1] = dr["soc_sec_num"].ToString().Trim();
                                                //parameter4[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                                //DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                                //object result4 = objDALBaseClass.ExecuteScalar(ref parameter4, objDVOPayrollstypaydd.FIND_stypayddinc_ytd);
                                                //if (result4.ToString() != "")
                                                //{
                                                //    tmp_ytd_accrual4 = Convert.ToDecimal(result4);
                                                //}
                                                ////objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[j].ded_rate + tmp_ytd_accrual4;
                                                //draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual4;//objDVOPayrollstypayddList[j].ded_rate
                                            }
                                            draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + Convert.ToDecimal(draDeductions[j][2]);//objDVOPayrollstypayddList[j].ded_rate + objDVOPayrollstypayddList[j].amount;
                                        }


                                        // process 9 lines
                                        //for (int j = 0; j < loop_number; j++)
                                        //{
                                        DataRow drn1 = objDataTable.NewRow();
                                        drn1.ItemArray = dr.ItemArray;
                                        if (j < inc_count)
                                        {
                                            //set the income fields
                                            drn1["inc_code"] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                            drn1["inc_num"] = draIncomes[j][2];// objDVOPayrollstypayidList[j].number;
                                            drn1["inc_hours"] = draIncomes[j][3];//objDVOPayrollstypayidList[j].hours;
                                            drn1["inc_amount"] = draIncomes[j][4];//objDVOPayrollstypayidList[j].amount;
                                            drn1["inc_ytd"] = draIncomes[j][5];//objDVOPayrollstypayidList[j].inc_rate;

                                            t_inc_num = t_inc_num + (draIncomes[j][2] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][2]) : 0);//objDVOPayrollstypayidList[j].number
                                            t_inc_hours = t_inc_hours + (draIncomes[j][3] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][3]) : 0);//objDVOPayrollstypayidList[j].hours
                                            t_inc_amount = t_inc_amount + (draIncomes[j][4] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][4]) : 0);//objDVOPayrollstypayidList[j].amount
                                            t_inc_ytd = t_inc_ytd + (draIncomes[j][5] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][5]) : 0);//objDVOPayrollstypayidList[j].inc_rate
                                        }
                                        if (j < ded_count)
                                        {
                                            // set the deduction fields
                                            drn1["ded_code"] = draDeductions[j][1].ToString().Trim();//objDVOPayrollstypayddList[j].ded_code;
                                            drn1["ded_amount"] = draDeductions[j][2].ToString().Trim();//objDVOPayrollstypayddList[j].amount;
                                            drn1["ded_ytd"] = draDeductions[j][3].ToString().Trim();//objDVOPayrollstypayddList[j].ded_rate;

                                            t_ded_ytd = t_ded_ytd + (draDeductions[j][3] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][3]) : 0);// objDVOPayrollstypayddList[j].ded_rate ?? 0;//make nullable decimal By Rahul
                                            t_ded_amount = t_ded_amount + (draDeductions[j][2] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][2]) : 0);//objDVOPayrollstypayddList[j].amount ?? 0; //make nullable decimal By Rahul
                                        }
                                        objDataTable.Rows.Add(drn1);
                                        //}
                                    } // end for loop..
                                    #region Commented Code....................
                                    //for (int j = 0; j < inc_count - 1; j++)
                                    //{
                                    //    if (objDVOPayrollstypayidList[j].amount == 0)
                                    //    {
                                    //        for (int k = j + 1; k < inc_count; k++)
                                    //        {
                                    //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                                    //            {  
                                    //                if(k<inc_count)
                                    //                {
                                    //                string tmp_hold_inc_code = objDVOPayrollstypayidList[j].inc_code;
                                    //                int tmp_hold_inc_line_no = objDVOPayrollstypayidList[j].line_no;
                                    //                decimal tmp_hold_inc_number = objDVOPayrollstypayidList[j].number;
                                    //                decimal tmp_hold_inc_hours = objDVOPayrollstypayidList[j].hours;
                                    //                decimal tmp_hold_inc_amount = objDVOPayrollstypayidList[j].amount;
                                    //                decimal tmp_hold_inc_accrual = objDVOPayrollstypayidList[j].inc_rate;

                                    //                objDVOPayrollstypayidList[j].inc_code = objDVOPayrollstypayidList[k].inc_code; ;
                                    //                objDVOPayrollstypayidList[j].line_no = objDVOPayrollstypayidList[k].line_no; ;
                                    //                objDVOPayrollstypayidList[j].number = objDVOPayrollstypayidList[k].number;
                                    //                objDVOPayrollstypayidList[j].hours = objDVOPayrollstypayidList[k].hours;
                                    //                objDVOPayrollstypayidList[j].amount = objDVOPayrollstypayidList[k].amount;
                                    //                objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[k].inc_rate;

                                    //                objDVOPayrollstypayidList[k].inc_code = tmp_hold_inc_code;
                                    //                objDVOPayrollstypayidList[k].line_no = tmp_hold_inc_line_no;
                                    //                objDVOPayrollstypayidList[k].number = tmp_hold_inc_number;
                                    //                objDVOPayrollstypayidList[k].hours = tmp_hold_inc_hours;
                                    //                objDVOPayrollstypayidList[k].amount = tmp_hold_inc_amount;
                                    //                objDVOPayrollstypayidList[k].inc_rate = tmp_hold_inc_accrual;
                                    //                }

                                    //            }
                                    //        }
                                    //    }
                                    //}

                                    //for (int j = 0; j < ded_count - 1; j++)
                                    //{
                                    //    if (objDVOPayrollstypayddList[j].amount == 0)
                                    //    {
                                    //        for (int k = j + 1; k < inc_count; k++)
                                    //        {
                                    //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                                    //            {  
                                    //                if(k<ded_count)
                                    //                {
                                    //                string tmp_hold_ded_code = objDVOPayrollstypayddList[j].ded_code;
                                    //                int tmp_hold_ded_line_no = objDVOPayrollstypayddList[j].line_no;
                                    //                decimal tmp_hold_ded_amount = objDVOPayrollstypayddList[j].amount;
                                    //                decimal tmp_hold_ded_accrual = objDVOPayrollstypayddList[j].ded_rate;


                                    //                objDVOPayrollstypayddList[j].ded_code = objDVOPayrollstypayddList[k].ded_code;
                                    //                objDVOPayrollstypayddList[j].line_no = objDVOPayrollstypayddList[k].line_no;
                                    //                objDVOPayrollstypayddList[j].amount = objDVOPayrollstypayddList[k].amount;
                                    //                objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[k].ded_rate;

                                    //                objDVOPayrollstypayddList[k].ded_code = tmp_hold_ded_code;
                                    //                objDVOPayrollstypayddList[k].line_no = tmp_hold_ded_line_no;
                                    //                objDVOPayrollstypayddList[k].amount = tmp_hold_ded_amount;
                                    //                objDVOPayrollstypayddList[k].ded_rate = tmp_hold_ded_accrual;
                                    //                }

                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    #endregion Commented Code

                                    #region Commented Code...................
                                    //if (inc_count > 9)
                                    //{
                                    //    for (int j = 10; j <= inc_count; j++)
                                    //    {
                                    //        //set the income fields
                                    //        t_inc_num = t_inc_num + objDVOPayrollstypayidList[j].number;
                                    //        t_inc_hours = t_inc_hours + objDVOPayrollstypayidList[j].hours;
                                    //        t_inc_amount = t_inc_amount + objDVOPayrollstypayidList[j].amount;
                                    //        t_inc_ytd = t_inc_ytd + objDVOPayrollstypayidList[j].inc_rate;
                                    //    }
                                    //}
                                    //if (ded_count > 9)
                                    //{
                                    //    for (int j = 10; j <= ded_count; j++)
                                    //    {
                                    //        t_ded_ytd = t_ded_ytd + objDVOPayrollstypayddList[j].ded_ytd;
                                    //        t_ded_amount = t_ded_amount + objDVOPayrollstypayddList[j].amount;

                                    //    }
                                    //}
                                    #endregion Commented Code...................

                                    #endregion processing on_every_row ........

                                    #region process on doc_no group.........
                                    bool _postingStatus = false;
                                    if (dra.Length != i + 1)
                                    {
                                        if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(dra[i + 1]["doc_no"]))
                                        {
                                            _postingStatus = true;
                                        }
                                    }
                                    else if (dra.Length == i + 1)
                                        _postingStatus = true;

                                    if (_postingStatus && loop_number > 0)
                                    {
                                        //update Process_PayEmployee
                                        if (objDVOPayrollProcess_PayEmployee1.check_no != 0)
                                        {
                                            object[] updParameters = new object[3];
                                            updParameters[0] = objDVOPayrollProcess_PayEmployee1.check_no;
                                            updParameters[1] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                            updParameters[2] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                            Int32 Process_PayEmployee_status = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updParameters, objDVOPayrollProcess_PayEmployee.update_Process_PayEmployee1);
                                            if (Process_PayEmployee_status == null || Process_PayEmployee_status.ToString().Trim() == string.Empty || Convert.ToInt32(Process_PayEmployee_status) != 1)
                                                throw new Exception("Error has occurred while updating Process_PayEmployee.");
                                        }
                                        //commit work
                                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                        recordsProcessed++;
                                        //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                        if (objDataTable.Rows.Count > 0)
                                        {
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_num"] = t_inc_num;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_hours"] = t_inc_hours;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_amount"] = t_inc_amount;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_ytd"] = t_inc_ytd;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_ytd"] = t_ded_ytd;
                                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_amount"] = t_ded_amount;
                                        }
                                        // initialize the totals
                                        t_inc_num = 0;
                                        t_inc_hours = 0;
                                        t_inc_amount = 0;
                                        t_inc_ytd = 0;
                                        t_ded_amount = 0;
                                        t_ded_ytd = 0;

                                    }
                                    else
                                    {
                                        //release & rollback current document
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                        errorMassage.Append("[ There is no Income and Deduction for DocNo - " + objDVOPayrollProcess_PayEmployee1.Doc_no + "]");
                                    }
                                    #endregion process on doc_no group.........
                                }
                                //remove all rows from table, where checkno is '0' or blank or null
                                for (int i = 0; i < objDataTable.Rows.Count; i++)
                                {
                                    DataRow dr = objDataTable.Rows[i];
                                    if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                                    {
                                        objDataTable.Rows.Remove(dr);
                                        objDataTable.AcceptChanges();
                                        i--;
                                    }
                                }

                                //foreach (DataRow dr in objDataTable.Rows)
                                //{
                                //    if (dr["check_no"] == DBNull.Value || dr["check_no"].ToString().Trim().Length == 0 || dr["check_no"].ToString().Trim() == "0")
                                //        objDataTable.Rows.Remove(dr);
                                //}
                                objDataTable.AcceptChanges();

                                #endregion Process
                            }
                            ReleaseAndUpdateAPDefaultRecord(true, false, ref objLockTransaction, ref objAPDefault, _currentCheckNo);
                        }
                    }
                }
                else
                {
                    //errorMassage.Append("[No Element to Process]");
                    return objDataTable;
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", curDocNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
                if (objDataTable.Columns.Count > 0)
                {
                    #region remove_rows
                    DataRow[] rm_rows = objDataTable.Select("doc_no = " + curDocNo);
                    for (int k = 0; k < rm_rows.Length; k++)
                    {
                        int rm_index = objDataTable.Rows.Count - 1;
                        objDataTable.Rows.RemoveAt(rm_index);
                    }
                    #endregion
                    System.Windows.Forms.MessageBox.Show("Some checks will not show,Error:" + ex.Message, "JKPS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return objDataTable;
                }
                else
                {
                    throw ex;
                }
                ExceptionManagement.ExceptionManager.Publish(ex);
                errorMassage.Append("[" + ex.Message + "]");

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
                    obj.processname = "Show Payroll Checks";
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
            return objDataTable;
        }

        public static DataTable PrintPayrollChecks(DataTable objDataTable,ref DVOPYBatchProcessStybatchr pObjBatch, int recordsSearched)
        {


            /*
             Added by Sarvjeet on 22/10/2010
             To implemented Payroll batch process into Print Payroll Checks. Nedd to add
             A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in 'ShowPayrollChecks' function
             and remove comment from the code written for batch process logic.   
           */
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataTable objPrintChkDT = new DataTable();
            objPrintChkDT = objDataTable.Clone();

            //Added by Sarvjeet on 22/01/2010..
            #region Declare Variables for Batch Process
            StringBuilder errorMassage = new StringBuilder();
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            //int recordsSearched = 0;
            int recordsProcessed = 0;
            bool IsProessIns = false;
            #endregion

            try
            {
                //Added by Sarvjeet on 22/01/2010..
                #region Insert Process Start Info..
                object objTrx = null;
                objProcessDtl.pybatchid = pObjBatch.pybatchid;
                objProcessDtl.processname = "Print Payroll Checks";
                objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
                BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
                IsProessIns = true;
                #endregion
                //recordsSearched = objDataTable.Rows.Count;
                int docNo = 0;
                string emplCode = string.Empty;
                int checkNo = 0;
                string prvEmp = string.Empty;
                string curEmp = string.Empty;
                decimal chkAmount = 0;
                object[] parameters = new object[8];
                object[] updParameters = new object[3];
                bool checkNoSts = true;
                foreach (DataRow dr in objDataTable.Rows)
                {
                    docNo = dr["doc_no"] != DBNull.Value ? Convert.ToInt32(dr["doc_no"]) : 0;
                    checkNo = dr["check_no"] != DBNull.Value ? Convert.ToInt32(dr["check_no"]) : 0;
                    emplCode = dr["empl_code"] != DBNull.Value ? Convert.ToString(dr["empl_code"]).Trim() : string.Empty;
                    chkAmount = dr["cash_amount"] != DBNull.Value ? Convert.ToDecimal(dr["cash_amount"]) : 0;
                    if (checkNo > 0)
                    {
                        curEmp = emplCode + docNo;
                        if (curEmp != prvEmp)
                        {
                            //Update check status in stypare..
                            updParameters[0] = checkNo;
                            updParameters[1] = emplCode;
                            updParameters[2] = docNo;
                            object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updParameters, (new DVOPayrollProcess_PayEmployee()).update_Process_PayEmployee, true);
                            if (o == null || o.ToString().Trim() == string.Empty || Convert.ToInt32(o) != 1)
                                throw new Exception("Error has occurred while updating Process_PayEmployee.");
                            //Make an entry into apchecksrecord
                            parameters[0] = docNo;
                            parameters[1] = checkNo;
                            parameters[2] = DVOApplicationUserInfo.UserId;
                            parameters[3] = "JKPS APPLICATION - PAYROLL";
                            parameters[4] = chkAmount;
                            parameters[5] = DVOApplicationUserInfo.UserId;
                            parameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            parameters[7] = DVOApplicationUserInfo.MachineInfo;
                            object InsStatus = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, (new DVOAPCheckProcessingStpcashe()).INSERT_APCHECKRECORD, true);
                            if (Convert.ToInt32(InsStatus) != 1)
                            {
                                throw new Exception(" Some Checks will not Print , Problem has occurred while Inserting 'apchecksrecord'.");
                            }
                            else
                            {
                                DataRow[] rows = objDataTable.Select("doc_no = " + docNo);
                                for (int j = 0; j < rows.Length; j++)
                                {
                                    DataRow Pdr = objPrintChkDT.NewRow();
                                    Pdr.ItemArray = rows[j].ItemArray;
                                    objPrintChkDT.Rows.Add(Pdr);
                                }
                                if (objTransaction != null)
                                {
                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", docNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                }
                            }
                            prvEmp = curEmp;
                            recordsProcessed++;
                        }

                    }
                    else
                    {
                        checkNoSts = false;
                        if (objTransaction != null)
                            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", docNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                        errorMassage.Append("[CheckNo not assigned for DocNo-" + docNo + "]"); 
                    }
                    
                }
                if (objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);


                if (!checkNoSts)
                {
                    System.Windows.Forms.MessageBox.Show("Some checks without check no. have not been printed.", "JKPS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    for (int i = 0; i <= objDataTable.Rows.Count - 1; i++)
                    {
                        DataRow dr = objDataTable.Rows[i];
                        if (i == 0 || (dr["doc_no"] != DBNull.Value && dr["doc_no"] != objDataTable.Rows[i - 1]["doc_no"]))
                            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    }
                    if (objTransaction != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    objPrintChkDT.Clear();
                }
                ExceptionManager.Publish(ex);
                errorMassage.Append("[" + ex.Message + "]");
                recordsProcessed = 0;
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
                    obj.processname = "Print Payroll Checks";
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
            return objPrintChkDT;
        }

        public static DataTable ShowDuplicatePayrollChecks(ref DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee)
        {
            DataSet ds = null;
            string PayMonth;
            string PayYear;
            DateTime StarDatePayYear;
            DateTime EndDatePayYear;
            DataTable objDataTable = new DataTable();
            object objTransaction = null;
            int curDocNo = 0;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] paramerers = new object[2];
                paramerers[0] = objDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//objDVOPayrollProcess_PayEmployee.pay_date;
                paramerers[1] = objDVOPayrollProcess_PayEmployee.EmplCode;
                //paramerers[2] = objDVOPayrollProcess_PayEmployee.TypeCode.Trim();

                ds = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.FIND_DUPLICATION_PayrollCheck(ref paramerers));

                #region Set the column name..and add columns ......
                ds.Tables[0].Columns[0].ColumnName = "address1";
                ds.Tables[0].Columns[1].ColumnName = "address2";
                ds.Tables[0].Columns[2].ColumnName = "city";
                ds.Tables[0].Columns[3].ColumnName = "first_name";
                ds.Tables[0].Columns[4].ColumnName = "last_name";
                ds.Tables[0].Columns[5].ColumnName = "middle_name";
                ds.Tables[0].Columns[6].ColumnName = "soc_sec_num";
                ds.Tables[0].Columns[7].ColumnName = "state";
                ds.Tables[0].Columns[8].ColumnName = "zip";
                ds.Tables[0].Columns[9].ColumnName = "cash_amount";
                ds.Tables[0].Columns[10].ColumnName = "check_no";
                ds.Tables[0].Columns[11].ColumnName = "department";
                ds.Tables[0].Columns[12].ColumnName = "doc_date";
                ds.Tables[0].Columns[13].ColumnName = "doc_no";
                ds.Tables[0].Columns[14].ColumnName = "empl_code";
                ds.Tables[0].Columns[15].ColumnName = "eop_date";
                ds.Tables[0].Columns[16].ColumnName = "pay_date";
                ds.Tables[0].Columns[17].ColumnName = "keyvalue";
                ds.Tables[0].Columns[18].ColumnName = "acct_desc";
                ds.Tables[0].Columns.Add("inc_code");
                ds.Tables[0].Columns.Add("inc_amount", typeof(decimal));
                ds.Tables[0].Columns.Add("inc_num");
                ds.Tables[0].Columns.Add("inc_hours", typeof(decimal));
                ds.Tables[0].Columns.Add("inc_ytd", typeof(decimal));
                ds.Tables[0].Columns.Add("ded_code");
                ds.Tables[0].Columns.Add("ded_amount", typeof(decimal));
                ds.Tables[0].Columns.Add("ded_ytd", typeof(decimal));
                ds.Tables[0].Columns.Add("inc_err");
                ds.Tables[0].Columns.Add("ded_err");
                ds.Tables[0].Columns.Add("inc_count");
                ds.Tables[0].Columns.Add("ded_count");
                ds.Tables[0].Columns.Add("cheque_amt", typeof(decimal));

                ds.Tables[0].Columns.Add("t_inc_num", typeof(decimal));
                ds.Tables[0].Columns.Add("t_inc_hours", typeof(decimal));
                ds.Tables[0].Columns.Add("t_inc_amount", typeof(decimal));
                ds.Tables[0].Columns.Add("t_ded_amount", typeof(decimal));
                ds.Tables[0].Columns.Add("t_ded_ytd", typeof(decimal));
                ds.Tables[0].Columns.Add("t_inc_ytd", typeof(decimal));
                objDataTable = ds.Tables[0].Clone();
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return objDataTable;
                }

                #endregion Set the column name..and add columns ......

                int dup_flag = 0;
                int dup_ssn = 0;

                decimal t_inc_num = 0;
                decimal t_inc_hours = 0;
                decimal t_inc_amount = 0;
                decimal t_inc_ytd = 0;
                decimal t_ded_amount = 0;
                decimal t_ded_ytd = 0;

                //Added By Rahul jain on 16/Jan/2010 For get all Incomes of Persons
                //DataSet dsIncome = objDALBaseClass.GetData(objDVOPayrollProcess_PayEmployee.GetListofPersonsIncome(ref paramerers));
                DataSet dsIncome = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypayid), (new DVOPayrollstypayid()).FIND_checkpaydtl01);
                if (dsIncome != null)
                {
                    dsIncome.Tables[0].Columns[0].ColumnName = "line_no";
                    dsIncome.Tables[0].Columns[1].ColumnName = "inc_code";
                    dsIncome.Tables[0].Columns[2].ColumnName = "number";
                    dsIncome.Tables[0].Columns[3].ColumnName = "hours";
                    dsIncome.Tables[0].Columns[4].ColumnName = "amount";
                    dsIncome.Tables[0].Columns[5].ColumnName = "amount1";
                    dsIncome.Tables[0].Columns[6].ColumnName = "doc_no";
                    dsIncome.Tables[0].Columns[7].ColumnName = "empl_code";
                }
                //Added By Rahul jain on 16/01/2010 For Get All deduction of Persons
                DataSet dsDeductions = objDALBaseClass.GetData(ref paramerers, typeof(DVOPayrollstypaydd), (new DVOPayrollstypaydd()).FIND_checkpaydtl02);
                if (dsDeductions != null)
                {
                    dsDeductions.Tables[0].Columns[0].ColumnName = "line_no";
                    dsDeductions.Tables[0].Columns[1].ColumnName = "ded_code";
                    dsDeductions.Tables[0].Columns[2].ColumnName = "amount";
                    dsDeductions.Tables[0].Columns[3].ColumnName = "amount1";
                    dsDeductions.Tables[0].Columns[4].ColumnName = "doc_no";
                    dsDeductions.Tables[0].Columns[5].ColumnName = "empl_code";
                }
                DataRow[] dra = ds.Tables[0].Select("", "empl_code,doc_no");
                for (int i = 0; i < dra.Length; i++)
                {
                    DataRow dr = dra[i];
                    DataRow drn = objDataTable.NewRow();
                    DVOPayrollProcess_PayEmployee objDVOPayrollProcess_PayEmployee1 = new DVOPayrollProcess_PayEmployee();
                    if (dr["doc_no"] != DBNull.Value)
                        objDVOPayrollProcess_PayEmployee1.Doc_no = Convert.ToInt32(dr["doc_no"]);
                    if (dr["cash_amount"] != DBNull.Value)
                        objDVOPayrollProcess_PayEmployee1.cash_amount = Convert.ToDecimal(dr["cash_amount"]);
                    if (dr["check_no"] != DBNull.Value)
                        objDVOPayrollProcess_PayEmployee1.check_no = Convert.ToInt32(dr["check_no"]);
                    if (dr["pay_date"] != DBNull.Value)
                        objDVOPayrollProcess_PayEmployee1.pay_date = Convert.ToDateTime(dr["pay_date"]);
                    objDVOPayrollProcess_PayEmployee1.EmplCode = dr["empl_code"].ToString().Trim();
                    string LastName = dr["last_name"] != DBNull.Value ? Convert.ToString(dr["last_name"]).Trim() : string.Empty;
                    if (LastName.Trim() != string.Empty)
                        LastName = LastName + ",  ";
                    string FirstName = dr["first_name"] != DBNull.Value ? Convert.ToString(dr["first_name"]).Trim() : string.Empty;

                    string MiddleName = dr["middle_name"] != DBNull.Value ? Convert.ToString(dr["middle_name"]).Trim() : string.Empty;
                    if (MiddleName.Trim() != string.Empty)
                        MiddleName = ",  " + MiddleName;
                    dr["last_name"] = LastName;
                    dr["first_name"] = FirstName;
                    dr["middle_name"] = MiddleName;
                    curDocNo = objDVOPayrollProcess_PayEmployee1.Doc_no;
                    #region Commented Code
                    //Get Keyvalue and acc_desc for employee cash account... 
                    //Object[] Param = new object[2];
                    //Param[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                    //Param[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                    //DataSet dscashkey = objDALBaseClass.GetData(ref Param, typeof(DVOPayrollProcess_PayEmployee), objDVOPayrollProcess_PayEmployee1.GET_CASHKEY);
                    //if(dscashkey.Tables.Count>0)
                    //    if (dscashkey.Tables[0].Rows.Count > 0)
                    //    { 
                    //dr["keyvalue"]=dscashkey.Tables[0].Rows[0][0];
                    //dr["acct_desc"] = dscashkey.Tables[0].Rows[0][1];                      
                    //}
                    #endregion Commented Code

                    #region processing on before Emp_Code group..
                    bool _status = false;
                    if (i != 0)
                    {
                        if (objDVOPayrollProcess_PayEmployee1.EmplCode != dra[i - 1]["empl_code"].ToString().Trim())
                        {
                            _status = true;
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            _status = true;
                        }
                    }
                    if (_status)
                    {
                        dup_flag = Get_dup_fla(objDVOPayrollProcess_PayEmployee1.EmplCode);
                        dup_ssn = Get_dup_ssn(dr["soc_sec_num"].ToString());
                    }

                    #endregion processing on before Emp_Code group..

                    #region processing on before doc_no group..
                    bool _postingStatus0 = false;
                    if (i != 0)
                    {
                        if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(dra[i - 1]["doc_no"]))
                        {
                            _postingStatus0 = true;
                        }
                    }
                    else if (i == 0)
                        _postingStatus0 = true;
                    if (_postingStatus0)
                    {
                        //objTransaction = objDALBaseClassHelper.GetTransactionObject();

                        //int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
                        //if (LockStatus != 1)
                        //{
                        //    if (objTransaction != null)
                        //    {
                        //        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                        //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        //    }
                        //    continue;
                        //}
                        //if (objDVOPayrollProcess_PayEmployee1.check_no == 0)
                        //{
                        //    int check_no = BLLAccountingLiberary.Auto_Next_APCheckNo(ref objTransaction);
                        //    if (check_no == 0)
                        //        throw new Exception("Error has occurred while generating check number.");

                        //    dr["check_no"] = check_no;
                        //    if (check_no <= 0)
                        //    {
                        //        if (objTransaction != null)
                        //        {
                        //            BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", objDVOPayrollProcess_PayEmployee1.Doc_no, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                        //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        //        }
                        //        continue;
                        //    }

                        //    objDVOPayrollProcess_PayEmployee1.check_no = check_no;
                        //}
                        if (objDVOPayrollProcess_PayEmployee1.cash_amount < 0)
                        {
                            objDVOPayrollProcess_PayEmployee1.cash_amount = 0;
                        }
                        if (objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime("01/01/1900") && objDVOPayrollProcess_PayEmployee1.pay_date != Convert.ToDateTime(null))
                        {
                            PayMonth = objDVOPayrollProcess_PayEmployee1.pay_date.Month.ToString();
                            PayYear = objDVOPayrollProcess_PayEmployee1.pay_date.Year.ToString();
                            string sdpy = "01" + "/" + "01" + "/" + PayYear;
                            string edpy = "01" + "/" + "31" + "/" + PayYear;
                            StarDatePayYear = DateTime.ParseExact(sdpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                            // EndDatePayYear = "01" + "/" + "31" + "/" + PayYear;
                            EndDatePayYear = DateTime.ParseExact(edpy, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                        }
                    }
                    #endregion process on before doc_no group..

                    #region processing on_every_row ........

                    //List<DVOPayrollstypayid> objDVOPayrollstypayidList = GetEmployeeIcomeList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                    //List<DVOPayrollstypaydd> objDVOPayrollstypayddList = GetEmployeeDedList(objDVOPayrollProcess_PayEmployee1.Doc_no, objDVOPayrollProcess_PayEmployee1.EmplCode);
                    //Added By Rahul jain on 16/01/2010
                    DataRow[] draIncomes = dsIncome.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " and empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");
                    //Added By Rahul jain on 16/01/2010
                    DataRow[] draDeductions = dsDeductions.Tables[0].Select("(doc_no=" + objDVOPayrollProcess_PayEmployee1.Doc_no + " and empl_code ='" + objDVOPayrollProcess_PayEmployee1.EmplCode.Trim() + "')");

                    int inc_count = 0;
                    int ded_count = 0;
                    inc_count = draIncomes.Length;// objDVOPayrollstypayidList.Count;
                    ded_count = draDeductions.Length;// objDVOPayrollstypayddList.Count;
                    int loop_number = 0;

                    if (inc_count > ded_count)
                    {
                        loop_number = inc_count;
                    }
                    else
                    {
                        loop_number = ded_count;
                    }
                    for (int j = 0; j < loop_number; j++)
                    {
                        // for income
                        // Collect amounts from other payroll entries
                        //  for the same employee code.
                        if (j < inc_count)
                        {
                            if (dup_flag > 1)
                            {
                                decimal tmp_ytd_accrual1 = 0;
                                object[] parameter1 = new object[4];
                                parameter1[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                parameter1[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                parameter1[2] = draIncomes[j][1].ToString().Trim();//objDVOPayrollstypayidList[j].inc_code.Trim();
                                parameter1[3] = DVOApplicationUserInfo.CurrentDate.Year.ToString();
                                ///***********************************************************
                                ///Have to Add a Filter in stored Procedure to use Ok_to_post="Y"
                                ///********************** 
                                DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                object result1 = objDALBaseClass.ExecuteScalar(ref parameter1, objDVOPayrollstypayid.FIND_stypayidamt);
                                if (result1.ToString() != "")
                                {
                                    tmp_ytd_accrual1 = Convert.ToDecimal(result1);
                                }
                                //inc_rate means ytd_accrual.
                                //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual1;
                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual1;
                            }
                            //Collect accrual amounts from other employee
                            //codes sharing the same soc. sec. num
                            //This is typically the same person under a new tax
                            //jurisdiction.
                            if (dup_ssn > 1)
                            {
                                decimal tmp_ytd_accrual2 = 0;
                                object[] parameter2 = new object[3];
                                parameter2[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                parameter2[1] = dr["soc_sec_num"].ToString().Trim();
                                parameter2[2] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
                                object result2 = objDALBaseClass.ExecuteScalar(ref parameter2, objDVOPayrollstypayid.FIND_stypayidinc_ytd);
                                if (result2.ToString() != "")
                                {
                                    tmp_ytd_accrual2 = Convert.ToDecimal(result2);
                                }
                                //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + tmp_ytd_accrual2;
                                draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + tmp_ytd_accrual2;
                            }
                            //objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[j].inc_rate + objDVOPayrollstypayidList[j].amount;
                            draIncomes[j][5] = Convert.ToDecimal(draIncomes[j][5]) + Convert.ToDecimal(draIncomes[j][4]);
                        }
                        // for deduction 
                        if (j < ded_count)
                        {
                            if (dup_flag > 1)
                            {
                                decimal tmp_ytd_accrual3 = 0;
                                object[] parameter3 = new object[4];
                                parameter3[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                parameter3[1] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                                parameter3[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                paramerers[3] = DVOApplicationUserInfo.CurrentDate.Year.ToString();
                                DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                object result3 = objDALBaseClass.ExecuteScalar(ref parameter3, objDVOPayrollstypaydd.FIND_stypayddamt);
                                if (result3.ToString() != "")
                                {
                                    tmp_ytd_accrual3 = Convert.ToDecimal(result3);
                                }
                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual3;//objDVOPayrollstypayddList[j].ded_rate
                            }
                            if (dup_ssn > 1)
                            {
                                decimal tmp_ytd_accrual4 = 0;
                                object[] parameter4 = new object[3];
                                parameter4[0] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                                parameter4[1] = dr["soc_sec_num"].ToString().Trim();
                                parameter4[2] = draDeductions[j][1].ToString().Trim(); //objDVOPayrollstypayddList[j].ded_code.Trim();
                                DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
                                object result4 = objDALBaseClass.ExecuteScalar(ref parameter4, objDVOPayrollstypaydd.FIND_stypayddinc_ytd);
                                if (result4.ToString() != "")
                                {
                                    tmp_ytd_accrual4 = Convert.ToDecimal(result4);
                                }
                                //objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[j].ded_rate + tmp_ytd_accrual4;
                                draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + tmp_ytd_accrual4;//objDVOPayrollstypayddList[j].ded_rate
                            }
                            draDeductions[j][3] = Convert.ToDecimal(draDeductions[j][3]) + Convert.ToDecimal(draDeductions[j][2]);//objDVOPayrollstypayddList[j].ded_rate + objDVOPayrollstypayddList[j].amount;
                        }


                        // process 9 lines
                        //for (int j = 0; j < loop_number; j++)
                        //{
                            DataRow drn1 = objDataTable.NewRow();
                            drn1.ItemArray = dr.ItemArray;
                            if (j < inc_count)
                            {
                                //set the income fields
                                drn1["inc_code"] = draIncomes[j][1].ToString().Trim();// objDVOPayrollstypayidList[j].inc_code;
                                drn1["inc_num"] = draIncomes[j][2];// objDVOPayrollstypayidList[j].number;
                                drn1["inc_hours"] = draIncomes[j][3];//objDVOPayrollstypayidList[j].hours;
                                drn1["inc_amount"] = draIncomes[j][4];//objDVOPayrollstypayidList[j].amount;
                                drn1["inc_ytd"] = draIncomes[j][5];//objDVOPayrollstypayidList[j].inc_rate;

                                t_inc_num = t_inc_num + (draIncomes[j][2] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][2]) : 0);//objDVOPayrollstypayidList[j].number
                                t_inc_hours = t_inc_hours + (draIncomes[j][3] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][3]) : 0);//objDVOPayrollstypayidList[j].hours
                                t_inc_amount = t_inc_amount + (draIncomes[j][4] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][4]) : 0);//objDVOPayrollstypayidList[j].amount
                                t_inc_ytd = t_inc_ytd + (draIncomes[j][5] != DBNull.Value ? Convert.ToDecimal(draIncomes[j][5]) : 0);//objDVOPayrollstypayidList[j].inc_rate
                            }
                            if (j < ded_count)
                            {
                                // set the deduction fields
                                drn1["ded_code"] = draDeductions[j][1].ToString().Trim();//objDVOPayrollstypayddList[j].ded_code;
                                drn1["ded_amount"] = draDeductions[j][2].ToString().Trim();//objDVOPayrollstypayddList[j].amount;
                                drn1["ded_ytd"] = draDeductions[j][3].ToString().Trim();//objDVOPayrollstypayddList[j].ded_rate;

                                t_ded_ytd = t_ded_ytd + (draDeductions[j][3] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][3]) : 0);// objDVOPayrollstypayddList[j].ded_rate ?? 0;//make nullable decimal By Rahul
                                t_ded_amount = t_ded_amount + (draDeductions[j][2] != DBNull.Value ? Convert.ToDecimal(draDeductions[j][2]) : 0);//objDVOPayrollstypayddList[j].amount ?? 0; //make nullable decimal By Rahul
                            }
                            objDataTable.Rows.Add(drn1);
                        //}
                    } // end for loop..
                    #region Commented Code....................
                    //for (int j = 0; j < inc_count - 1; j++)
                    //{
                    //    if (objDVOPayrollstypayidList[j].amount == 0)
                    //    {
                    //        for (int k = j + 1; k < inc_count; k++)
                    //        {
                    //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                    //            {  
                    //                if(k<inc_count)
                    //                {
                    //                string tmp_hold_inc_code = objDVOPayrollstypayidList[j].inc_code;
                    //                int tmp_hold_inc_line_no = objDVOPayrollstypayidList[j].line_no;
                    //                decimal tmp_hold_inc_number = objDVOPayrollstypayidList[j].number;
                    //                decimal tmp_hold_inc_hours = objDVOPayrollstypayidList[j].hours;
                    //                decimal tmp_hold_inc_amount = objDVOPayrollstypayidList[j].amount;
                    //                decimal tmp_hold_inc_accrual = objDVOPayrollstypayidList[j].inc_rate;

                    //                objDVOPayrollstypayidList[j].inc_code = objDVOPayrollstypayidList[k].inc_code; ;
                    //                objDVOPayrollstypayidList[j].line_no = objDVOPayrollstypayidList[k].line_no; ;
                    //                objDVOPayrollstypayidList[j].number = objDVOPayrollstypayidList[k].number;
                    //                objDVOPayrollstypayidList[j].hours = objDVOPayrollstypayidList[k].hours;
                    //                objDVOPayrollstypayidList[j].amount = objDVOPayrollstypayidList[k].amount;
                    //                objDVOPayrollstypayidList[j].inc_rate = objDVOPayrollstypayidList[k].inc_rate;

                    //                objDVOPayrollstypayidList[k].inc_code = tmp_hold_inc_code;
                    //                objDVOPayrollstypayidList[k].line_no = tmp_hold_inc_line_no;
                    //                objDVOPayrollstypayidList[k].number = tmp_hold_inc_number;
                    //                objDVOPayrollstypayidList[k].hours = tmp_hold_inc_hours;
                    //                objDVOPayrollstypayidList[k].amount = tmp_hold_inc_amount;
                    //                objDVOPayrollstypayidList[k].inc_rate = tmp_hold_inc_accrual;
                    //                }

                    //            }
                    //        }
                    //    }
                    //}

                    //for (int j = 0; j < ded_count - 1; j++)
                    //{
                    //    if (objDVOPayrollstypayddList[j].amount == 0)
                    //    {
                    //        for (int k = j + 1; k < inc_count; k++)
                    //        {
                    //            if (objDVOPayrollstypayidList[j].inc_rate < objDVOPayrollstypayidList[k].inc_rate)
                    //            {  
                    //                if(k<ded_count)
                    //                {
                    //                string tmp_hold_ded_code = objDVOPayrollstypayddList[j].ded_code;
                    //                int tmp_hold_ded_line_no = objDVOPayrollstypayddList[j].line_no;
                    //                decimal tmp_hold_ded_amount = objDVOPayrollstypayddList[j].amount;
                    //                decimal tmp_hold_ded_accrual = objDVOPayrollstypayddList[j].ded_rate;


                    //                objDVOPayrollstypayddList[j].ded_code = objDVOPayrollstypayddList[k].ded_code;
                    //                objDVOPayrollstypayddList[j].line_no = objDVOPayrollstypayddList[k].line_no;
                    //                objDVOPayrollstypayddList[j].amount = objDVOPayrollstypayddList[k].amount;
                    //                objDVOPayrollstypayddList[j].ded_rate = objDVOPayrollstypayddList[k].ded_rate;

                    //                objDVOPayrollstypayddList[k].ded_code = tmp_hold_ded_code;
                    //                objDVOPayrollstypayddList[k].line_no = tmp_hold_ded_line_no;
                    //                objDVOPayrollstypayddList[k].amount = tmp_hold_ded_amount;
                    //                objDVOPayrollstypayddList[k].ded_rate = tmp_hold_ded_accrual;
                    //                }

                    //            }
                    //        }
                    //    }
                    //}
                    #endregion Commented Code

                    #region Commented Code...................
                    //if (inc_count > 9)
                    //{
                    //    for (int j = 10; j <= inc_count; j++)
                    //    {
                    //        //set the income fields
                    //        t_inc_num = t_inc_num + objDVOPayrollstypayidList[j].number;
                    //        t_inc_hours = t_inc_hours + objDVOPayrollstypayidList[j].hours;
                    //        t_inc_amount = t_inc_amount + objDVOPayrollstypayidList[j].amount;
                    //        t_inc_ytd = t_inc_ytd + objDVOPayrollstypayidList[j].inc_rate;
                    //    }
                    //}
                    //if (ded_count > 9)
                    //{
                    //    for (int j = 10; j <= ded_count; j++)
                    //    {
                    //        t_ded_ytd = t_ded_ytd + objDVOPayrollstypayddList[j].ded_ytd;
                    //        t_ded_amount = t_ded_amount + objDVOPayrollstypayddList[j].amount;

                    //    }
                    //}
                    #endregion Commented Code...................

                    #endregion processing on_every_row ........

                    #region process on doc_no group.........
                    bool _postingStatus = false;
                    if (dra.Length != i + 1)
                    {
                        if (objDVOPayrollProcess_PayEmployee1.Doc_no != Convert.ToInt32(dra[i + 1]["doc_no"]))
                        {
                            _postingStatus = true;
                        }
                    }
                    else if (dra.Length == i + 1)
                        _postingStatus = true;

                    if (_postingStatus)
                    {

                        //No need to update Process_PayEmployee.Commented by Sunil Pahwa
                        //*****************************************************
                        //if (objDVOPayrollProcess_PayEmployee1.check_no != 0)
                        //{
                        //    object[] updParameters = new object[3];
                        //    updParameters[0] = objDVOPayrollProcess_PayEmployee1.check_no;
                        //    updParameters[1] = objDVOPayrollProcess_PayEmployee1.EmplCode.Trim();
                        //    updParameters[2] = objDVOPayrollProcess_PayEmployee1.Doc_no;
                        //    object Process_PayEmployee_status = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updParameters, objDVOPayrollProcess_PayEmployee.update_Process_PayEmployee1, true);
                        //    if (Process_PayEmployee_status == null || Process_PayEmployee_status.ToString().Trim() == string.Empty || Convert.ToInt32(Process_PayEmployee_status) != 1)
                        //        throw new Exception("Error has occurred while updating Process_PayEmployee.");
                        //}
                        //*****Comment Ended**********************************
                        //commit work
                        if (objTransaction != null)
                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        if (objDataTable.Rows.Count > 0)
                        {
                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_num"] = t_inc_num;
                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_hours"] = t_inc_hours;
                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_amount"] = t_inc_amount;
                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_inc_ytd"] = t_inc_ytd;
                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_ytd"] = t_ded_ytd;
                            objDataTable.Rows[objDataTable.Rows.Count - 1]["t_ded_amount"] = t_ded_amount;
                        }
                        // initialize the totals
                        t_inc_num = 0;
                        t_inc_hours = 0;
                        t_inc_amount = 0;
                        t_inc_ytd = 0;
                        t_ded_amount = 0;
                        t_ded_ytd = 0;

                    }
                    #endregion process on doc_no group.........
                }

            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", curDocNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                }
                if (objDataTable.Columns.Count > 0)
                {
                    #region remove_rows
                    DataRow[] rm_rows = objDataTable.Select("doc_no = " + curDocNo);
                    for (int k = 0; k < rm_rows.Length; k++)
                    {
                        int rm_index = objDataTable.Rows.Count - 1;
                        objDataTable.Rows.RemoveAt(rm_index);
                    }
                    #endregion
                    System.Windows.Forms.MessageBox.Show("Some checks will not show,Error:" + ex.Message, "JKPS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return objDataTable;
                }
                else
                {
                    throw ex;
                }
                ExceptionManagement.ExceptionManager.Publish(ex);

            }
            return objDataTable;
        }

        //public static int Get_dup_fla(string Emp_code)
        //{
        //    int i = 0;
        //    object[] parameter = new object[1];
        //    parameter[0] = Emp_code;
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    object result = objDALBaseClass.ExecuteScalar(ref parameter, (new DVOPayrollProcess_PayEmployee()).FIND_dup_flag);
        //    i = Convert.ToInt32(result);
        //    return i;
        //}
        //public static int Get_dup_ssn(string soc_sec_num)
        //{
        //    int i = 0;
        //    object[] parameter = new object[1];
        //    parameter[0] = soc_sec_num;
        //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
        //    object result = objDALBaseClass.ExecuteScalar(ref parameter, (new DVOPayrollProcess_PayEmployee()).FIND_dup_ssn);
        //    i = Convert.ToInt32(result);
        //    return i;
        //}

        //Print duplicateCheck

        public static DataTable PrintDuplicatePayrollChecks(DataTable objDataTable, string Notes)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataTable objPrintChkDT = new DataTable();
            objPrintChkDT = objDataTable.Clone();

            try
            {
                int docNo = 0;
                string emplCode = string.Empty;
                int checkNo = 0;
                string prvEmp = string.Empty;
                string curEmp = string.Empty;
                decimal chkAmount = 0;
                object[] parameters = new object[8];
                object[] updParameters = new object[3];
                bool checkNoSts = true;
                foreach (DataRow dr in objDataTable.Rows)
                {
                    docNo = dr["doc_no"] != DBNull.Value ? Convert.ToInt32(dr["doc_no"]) : 0;
                    checkNo = dr["check_no"] != DBNull.Value ? Convert.ToInt32(dr["check_no"]) : 0;
                    emplCode = dr["empl_code"] != DBNull.Value ? Convert.ToString(dr["empl_code"]).Trim() : string.Empty;
                    chkAmount = dr["cash_amount"] != DBNull.Value ? Convert.ToDecimal(dr["cash_amount"]) : 0;
                    if (checkNo > 0)
                    {
                        curEmp = emplCode + docNo;
                        if (curEmp != prvEmp)
                        {
                            //Update check status in stypare..
                            //Commented by Sunil Pahwa on 21/1009
                            //************************************
                            //updParameters[0] = checkNo;
                            //updParameters[1] = emplCode;
                            //updParameters[2] = docNo;
                            //object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref updParameters, (new DVOPayrollProcess_PayEmployee()).update_Process_PayEmployee, true);
                            //if (o == null || o.ToString().Trim() == string.Empty || Convert.ToInt32(o) != 1)
                            //    throw new Exception("Error has occurred while updating Process_PayEmployee.");
                            //Comment Ended****************************
                            object[] Insparameters = new object[8];
                            //Make an entry into apchecksrecord
                            Insparameters[0] = docNo;
                            Insparameters[1] = checkNo;
                            Insparameters[2] = DVOApplicationUserInfo.UserId;
                            Insparameters[3] = Notes;
                            Insparameters[4] = chkAmount;
                            Insparameters[5] = DVOApplicationUserInfo.UserId;
                            Insparameters[6] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            Insparameters[7] = DVOApplicationUserInfo.MachineInfo;
                            object InsStatus = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref Insparameters, (new DVOAPCheckProcessingStpcashe()).INSERT_APCHECKRECORD, true);
                            if (Convert.ToInt32(InsStatus) != 1)
                            {
                                throw new Exception(" Some Checks will not Print , Problem has occurred while Inserting 'apchecksrecord'.");
                            }
                            else
                            {
                                DataRow[] rows = objDataTable.Select("doc_no = " + docNo);
                                for (int j = 0; j < rows.Length; j++)
                                {
                                    DataRow Pdr = objPrintChkDT.NewRow();
                                    Pdr.ItemArray = rows[j].ItemArray;
                                    objPrintChkDT.Rows.Add(Pdr);
                                }
                                //*****Commented by Sunil Pahwa*****
                                //if (objTransaction != null)
                                //{
                                //    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", docNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                //}
                                //*********Comment Ended***********

                            }
                            prvEmp = curEmp;
                        }
                    }
                    else
                    {
                        checkNoSts = false;
                        //*****Commented by Sunil Pahwa*****
                        //if (objTransaction != null)
                        //    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", docNo, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                        //*********Comment Ended***********

                    }
                }
                if (objTransaction != null)
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);

                if (!checkNoSts)
                {
                    System.Windows.Forms.MessageBox.Show("Some checks without check no. have not been printed.", "JKPS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                {
                    //*****Commented by Sunil Pahwa*********
                    //for (int i = 0; i <= objDataTable.Rows.Count - 1; i++)
                    //{
                    //    DataRow dr = objDataTable.Rows[i];
                    //    if (i == 0 || (dr["doc_no"] != DBNull.Value && dr["doc_no"] != objDataTable.Rows[i - 1]["doc_no"]))
                    //        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransaction, "Process_PayEmployee", Convert.ToInt32(dr["doc_no"]), DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                    //}
                    //******Comment Ended***************
                    if (objTransaction != null)
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    objPrintChkDT.Clear();
                }
                ExceptionManager.Publish(ex);
                throw ex;
            }
            return objPrintChkDT;
        }



    }
}
