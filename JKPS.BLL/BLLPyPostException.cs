using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JKPS.COMMON;
using JKPS.DL;
using ExceptionManagement;

namespace JKPS.BLL
{
  public class BLLPyPostException
  {
    #region Globle variable Used
    static DVOMasterEmployee GlobalObjDVOMasterEmployee;
    static DVOPayrollProcess_PayEmployee GlobalObjDVOPayrollProcess_PayEmployee;
    static List<DVOUpdatePayDefaults> objGloabalPayDefaultsListStycntrc;

    // variable used in ded_post
    static Boolean dfica_flag = false;
    static decimal fica_ded = 0.0M;
    static Boolean dmedicare_flag = false;
    static decimal medcr_ded = 0.0M;
    static Boolean dftax_flag = false;
    // variable used in obl_post
    static bool ofuta_flag = false;
    static bool ofica_flag = false;
    static decimal fica_obl = 0.0M;
    static bool omedicare_flag = false;
    static decimal medcr_obl = 0.0M;
    // variable used in inc_post
    static decimal sick_accum = 0.0M;
    static decimal vac_accum = 0.0M;
    //Variable used in py_post
    static string py_installed;
    //Variable used To pass in py_post as an output
    static decimal py_c_accum = 0.0M;
    static decimal py_i_accum = 0.0M;
    static decimal py_d_accum = 0.0M;
    static decimal py_ox_accum = 0.0M;
    static decimal py_ol_accum = 0.0M;
    static int py_status = 0;
    static string py_description = string.Empty;

    #endregion Globle variable Used

    public DataSet GetExceptionReports(ref DVOPayrollProcess_PayEmployee objSCDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee objSCDVOMasterEmployee, string CHECK_POST, ref DVOPYBatchProcessStybatchr pObjBatch)
    {
      //Added by Sarvjeet on 21/01/2010
      //To implement batch process, add a parameter 'ref DVOPYBatchProcessStybatchr pObjBatch'
      //in 'GetExceptionReports' function and remove comment form region 'Insert Process Start Info..'
      //and code written in finaly block wthin region 'Record process detail..'

      //Initialize 
      #region Initialize Variables

      decimal act_accrual = 0.0M;
      decimal act_limit = 0.0M;
      decimal dflt_limit = 0.0M;
      int empl_allow = 0;
      int entry_count = 0;
      int sick_acc_tmp = 0;
      int vac_acc_tmp = 0;
      decimal d_tot_hour = 0.0M;
      int status = 0;
      int new_doc_no = 0;
      int gl_status = 0;
      bool _ok_to_post = true;
      DataSet ds = null;
      Boolean dmedicare_lmt = false;
      Boolean ofica_lmt = false;
      Boolean omedicare_lmt = false;
      Boolean ofuta_lmt = false;
      Boolean dftax_xmt = false;
      string errMsg = string.Empty;
      Boolean Bonus_Check = false;

      // variable used in ded_post
      dfica_flag = false;
      fica_ded = 0.0M;
      dmedicare_flag = false;
      medcr_ded = 0.0M;
      dftax_flag = false;
      //variable used in obl_post
      ofuta_flag = false;
      ofica_flag = false;
      fica_obl = 0.0M;
      omedicare_flag = false;
      medcr_obl = 0.0M;
      // variable used in inc_post
      sick_accum = 0.0M;
      vac_accum = 0.0M;
      //Variable used in py_post
      //py_installed;
      //Variable used To pass in py_post as an output
      py_c_accum = 0.0M;
      py_i_accum = 0.0M;
      py_d_accum = 0.0M;
      py_ox_accum = 0.0M;
      py_ol_accum = 0.0M;
      py_status = 0;
      py_description = string.Empty;

      #endregion

      BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      string processName = string.Empty;
      if (CHECK_POST == "CHECK")
        processName = "Payroll Exception Report";
      else if (CHECK_POST == "EDIT")
        processName = "Print Detail Edit List";
      else if (CHECK_POST == "POST")
        processName = "Post Payroll Entries";

      DataSet FinalDs = new DataSet();
      DataTable objDataTable = new DataTable();
      DataTable objGLSumTable = new DataTable();
      //make a DataTable Object for holding GL Summary Data.....
      objGLSumTable.Columns.Add("doc_no", typeof(int));
      objGLSumTable.Columns.Add("doc_date", typeof(DateTime));
      objGLSumTable.Columns.Add("acctno", typeof(int));
      objGLSumTable.Columns.Add("amount", typeof(decimal));
      objGLSumTable.Columns.Add("debit_credit");
      objGLSumTable.Columns.Add("keyvalue");
      objGLSumTable.Columns.Add("acct_desc");
      objGLSumTable.Columns.Add("flexdept");
      //make a DataTable Object to hold Income,Deduction and Obligation details.......
      DataTable ObjDetailTable = new DataTable();
      ObjDetailTable.Columns.Add("doc_no", typeof(int));
      ObjDetailTable.Columns.Add("code_line");
      ObjDetailTable.Columns.Add("pay_code");
      ObjDetailTable.Columns.Add("pay_desc");
      ObjDetailTable.Columns.Add("account");
      ObjDetailTable.Columns.Add("amount", typeof(decimal));
      ObjDetailTable.Columns.Add("lo_hi_amt", typeof(decimal));
      ObjDetailTable.Columns.Add("lo_flag", typeof(Boolean));
      //ObjDetailTable.Columns.Add("hi_flag", typeof(Boolean));
      string Curr_Emp_Code = string.Empty;
      object objTransaction = objDALBaseClassHelper.GetTransactionObject();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();


      #region Declare Variables for Batch Process
      StringBuilder errorMassage = new StringBuilder();
      DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
      int recordsSearched = 0;
      int recordsProcessed = 0;
      bool IsProessIns = false;
      #endregion



      try
      {
        #region Insert Process Start Info..

        object objTrx = null;
        objProcessDtl.pybatchid = pObjBatch.pybatchid;
        objProcessDtl.processname = processName;
        objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
        BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
        IsProessIns = true;

        #endregion

        ds = BLLPyPostException.ml_getCount(ref objSCDVOPayrollProcess_PayEmployee, ref objSCDVOMasterEmployee, CHECK_POST);
        recordsSearched = ds.Tables[0].Rows.Count;
        #region Set the column name..and add columns for message and problems......
        ds.Tables[0].Columns[0].ColumnName = "first_name"; //styemplr
        ds.Tables[0].Columns[1].ColumnName = "flexdeptaccttype"; //styemplr
        ds.Tables[0].Columns[2].ColumnName = "last_name";//styemplr
        ds.Tables[0].Columns[3].ColumnName = "last_pay";//styemplr
        ds.Tables[0].Columns[4].ColumnName = "middle_name";//styemplr
        ds.Tables[0].Columns[5].ColumnName = "pay_period";//styemplr
        ds.Tables[0].Columns[6].ColumnName = "sick_allowed";//styemplr
        ds.Tables[0].Columns[7].ColumnName = "sick_code";//styemplr
        ds.Tables[0].Columns[8].ColumnName = "sick_used";//styemplr
        ds.Tables[0].Columns[9].ColumnName = "soc_sec_num";//styemplr
        ds.Tables[0].Columns[10].ColumnName = "terminated";//styemplr
        ds.Tables[0].Columns[11].ColumnName = "vac_allowed";//styemplr
        ds.Tables[0].Columns[12].ColumnName = "vac_code";//styemplr
        ds.Tables[0].Columns[13].ColumnName = "vac_used";//styemplr
        ds.Tables[0].Columns[14].ColumnName = "cash_acct_no"; //--Process_PayEmployee.
        ds.Tables[0].Columns[15].ColumnName = "cash_amount"; //--Process_PayEmployee
        ds.Tables[0].Columns[16].ColumnName = "check_no"; //--Process_PayEmployee
        ds.Tables[0].Columns[17].ColumnName = "ded_fedtax"; //--Process_PayEmployee
        ds.Tables[0].Columns[18].ColumnName = "ded_fica";//--Process_PayEmployee
        ds.Tables[0].Columns[19].ColumnName = "ded_loctax";//--Process_PayEmployee
        ds.Tables[0].Columns[20].ColumnName = "ded_medicare";//--Process_PayEmployee
        ds.Tables[0].Columns[21].ColumnName = "ded_other";//--Process_PayEmployee
        ds.Tables[0].Columns[22].ColumnName = "ded_statax";//--Process_PayEmployee
        ds.Tables[0].Columns[23].ColumnName = "department";//--Process_PayEmployee
        ds.Tables[0].Columns[24].ColumnName = "doc_date";//--Process_PayEmployee
        ds.Tables[0].Columns[25].ColumnName = "doc_no";//--Process_PayEmployee
        ds.Tables[0].Columns[26].ColumnName = "empl_code";//--Process_PayEmployee
        ds.Tables[0].Columns[27].ColumnName = "eop_date";//--Process_PayEmployee
        ds.Tables[0].Columns[28].ColumnName = "inc_expense";//--Process_PayEmployee
        ds.Tables[0].Columns[29].ColumnName = "inc_gross";//--Process_PayEmployee
        ds.Tables[0].Columns[30].ColumnName = "inc_net";//--Process_PayEmployee
        ds.Tables[0].Columns[31].ColumnName = "inc_taxable";//--Process_PayEmployee
        ds.Tables[0].Columns[32].ColumnName = "obl_fica";//--Process_PayEmployee
        ds.Tables[0].Columns[33].ColumnName = "obl_futa";//--Process_PayEmployee
        ds.Tables[0].Columns[34].ColumnName = "obl_medicare";//--Process_PayEmployee
        ds.Tables[0].Columns[35].ColumnName = "obl_other";//--Process_PayEmployee
        ds.Tables[0].Columns[36].ColumnName = "obl_total";//--Process_PayEmployee
        ds.Tables[0].Columns[37].ColumnName = "pay_date";//--Process_PayEmployee
        ds.Tables[0].Columns[38].ColumnName = "total_hours";//--Process_PayEmployee
        ds.Tables[0].Columns[39].ColumnName = "emp_cash_acct";//styemplr
        ds.Tables[0].Columns[40].ColumnName = "emp_department";//styemplr
        ds.Tables[0].Columns[41].ColumnName = "accrue_vac"; //--Process_PayEmployee
        ds.Tables[0].Columns[42].ColumnName = "accrue_sick";//--Process_PayEmployee
        ds.Tables[0].Columns[43].ColumnName = "print_check";//--Process_PayEmployee
        ds.Tables[0].Columns[44].ColumnName = "deposit";//--Process_PayEmployee

        // add columns to display  message and problems.
        ds.Tables[0].Columns.Add("warn_0");
        ds.Tables[0].Columns.Add("warn_1");
        ds.Tables[0].Columns.Add("warn_2");
        ds.Tables[0].Columns.Add("warn_3");
        ds.Tables[0].Columns.Add("warn_4");
        ds.Tables[0].Columns.Add("warn_5");
        ds.Tables[0].Columns.Add("warn_6");
        ds.Tables[0].Columns.Add("warn_7");
        ds.Tables[0].Columns.Add("warn_8");
        ds.Tables[0].Columns.Add("warn_9");
        ds.Tables[0].Columns.Add("warn_10");
        ds.Tables[0].Columns.Add("warn_11");
        ds.Tables[0].Columns.Add("warn_12");
        ds.Tables[0].Columns.Add("warn_13");

        ds.Tables[0].Columns.Add("Problem0");
        ds.Tables[0].Columns.Add("Problem1");
        ds.Tables[0].Columns.Add("Problem2");
        ds.Tables[0].Columns.Add("Problem3");
        ds.Tables[0].Columns.Add("Problem4");
        ds.Tables[0].Columns.Add("Problem5");
        ds.Tables[0].Columns.Add("Problem6");
        ds.Tables[0].Columns.Add("Problem7");
        ds.Tables[0].Columns.Add("Problem8");
        ds.Tables[0].Columns.Add("Problem9");

        ds.Tables[0].Columns.Add("err_0");
        ds.Tables[0].Columns.Add("err_1");
        ds.Tables[0].Columns.Add("err_2");
        ds.Tables[0].Columns.Add("err_3");
        ds.Tables[0].Columns.Add("err_4");
        ds.Tables[0].Columns.Add("err_5");

        // Column Added for form only 
        ds.Tables[0].Columns.Add("post_seq");
        ds.Tables[0].Columns.Add("basicSalary");
        ds.Tables[0].Columns.Add("oiTaxable");
        ds.Tables[0].Columns.Add("oiNonTaxable");

        ds.Tables[0].Columns.Add("bonus_check");
        ds.Tables[0].Columns.Add("ded_total");
        ds.Tables[0].Columns.Add("txt_doc_no");

        ds.Tables[0].Columns.Add("grand_basic");
        ds.Tables[0].Columns.Add("grand_oi_tax");
        ds.Tables[0].Columns.Add("grand_oi_nontax");
        ds.Tables[0].Columns.Add("grand_ded");
        ds.Tables[0].Columns.Add("ok_to_post");

        ds.Tables[0].Columns.Add("ded_ssd");
        ds.Tables[0].Columns.Add("ded_ssl");
        ds.Tables[0].Columns.Add("obl_ssd");
        ds.Tables[0].Columns.Add("obl_ssib");

        ds.Tables[0].Columns.Add("pay_code");
        ds.Tables[0].Columns.Add("code_desc");
        ds.Tables[0].Columns.Add("pay_acct_no");
        ds.Tables[0].Columns.Add("pay_dept");
        ds.Tables[0].Columns.Add("pay_amount");
        ds.Tables[0].Columns.Add("pay_lo_amt");
        ds.Tables[0].Columns.Add("pay_hi_amt");
        ds.Tables[0].Columns.Add("flexdept");  // TYPE LIKE PayrollGLAccounts.keyvalue
        ds.Tables[0].Columns.Add("flexdeptdesc");
        ds.Tables[0].Columns.Add("cash_acct_key");
        ds.Tables[0].Columns.Add("CurrentPeriod");
        ds.Tables[0].Columns.Add("CurrentYear");
        //Changes Incurred By Rohit to get New Exception Report Format for HR ,Specially changes needed for Wages
        ds.Tables[0].Columns.Add("IncomeCodeREG");
        ds.Tables[0].Columns.Add("IncomeRateREG");
        ds.Tables[0].Columns.Add("IncomeHoursREG");
        ds.Tables[0].Columns.Add("IncomeCodeOT");
        ds.Tables[0].Columns.Add("IncomeRateOT");
        ds.Tables[0].Columns.Add("IncomeHoursOT");
        ds.Tables[0].Columns.Add("IncomeCodeDT");
        ds.Tables[0].Columns.Add("IncomeRateDT");
        ds.Tables[0].Columns.Add("IncomeHoursDT");

        // formonly.flexdept TYPE LIKE PayrollGLAccounts.keyvalue
        //c3  = formonly.flexdeptdesc TYPE char(100)
        objDataTable = ds.Tables[0].Clone();
        string CurrMonth, CurrYear;
        //CurrMonth = ReportingUtilities.GetCurr_periodstgcntrc();
        //CurrYear = ReportingUtilities.GetCurr_yearstgcntrc();
        CurrMonth = DVOApplicationUserInfo.CurPeriod;
        CurrYear = DVOApplicationUserInfo.CurYear;
        #endregion Set the column name..and add columns for message and problems......

        if (ds.Tables[0].Rows.Count > 0)
        {

          #region BeforeFirstRow

          //Getting the default data from control table stycntrc
          //And assign it to a global decleared list
          objGloabalPayDefaultsListStycntrc = new List<DVOUpdatePayDefaults>();
          DVOUpdatePayDefaults obj = new DVOUpdatePayDefaults();
          objGloabalPayDefaultsListStycntrc = BLLUpdPayDefault.GetPayrollDefaults(ref obj);
          if (objGloabalPayDefaultsListStycntrc[0].post_gl == "")
          {
            objGloabalPayDefaultsListStycntrc[0].post_gl = "Y";
          }

          int Post_no = 0;
          if (CHECK_POST == "POST")
          {
            //Post_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_post_no", ref objTransaction);
            Post_no = BLLAccountingLiberary.Auto_Next_PYPostNo();
            if (Post_no == 0)
            {
              //Added by Sunil 
              objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
              //************************************
              throw new Exception("Error has occurred while generating Posting Seq No.");
            }
            ds.Tables[0].Rows[0]["post_seq"] = Post_no;
          }
          else
          {
            ds.Tables[0].Rows[0]["post_seq"] = Post_no;
            new_doc_no = 0;
          }

          #endregion BeforeFirstRow

          DVOPostGLGlobal objDVOPostGLGlobal = new DVOPostGLGlobal();

          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
            DataRow dr = ds.Tables[0].Rows[i];
            objDataTable.NewRow();

            GlobalObjDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            GlobalObjDVOMasterEmployee = new DVOMasterEmployee();

            #region initialize the form Only fields
            Curr_Emp_Code = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : "";

            dr["grand_basic"] = 0;
            dr["grand_oi_tax"] = 0;
            dr["grand_oi_nontax"] = 0;
            dr["grand_ded"] = 0;
            dr["post_seq"] = Post_no;
            if (dr["sick_allowed"] == DBNull.Value)
              dr["sick_allowed"] = 0;
            if (dr["vac_allowed"] == DBNull.Value)
              dr["vac_allowed"] = 0;
            if (dr["cash_acct_no"] == DBNull.Value)
              dr["cash_acct_no"] = 0;
            if (dr["cash_amount"] == DBNull.Value)
              dr["cash_amount"] = 0;
            if (dr["terminated"] == DBNull.Value)
              dr["terminated"] = Convert.ToDateTime(null);
            if (dr["last_pay"] == DBNull.Value)
              dr["last_pay"] = Convert.ToDateTime(null);
            if (dr["sick_used"] == DBNull.Value)
              dr["sick_used"] = 0;
            if (dr["vac_used"] == DBNull.Value)
              dr["vac_used"] = 0;
            if (dr["inc_expense"] == DBNull.Value)
              dr["inc_expense"] = 0;
            if (dr["inc_net"] == DBNull.Value)
              dr["inc_net"] = 0;
            if (dr["inc_gross"] == DBNull.Value)
              dr["inc_gross"] = 0;
            if (dr["total_hours"] == DBNull.Value)
              dr["total_hours"] = 0;
            dr["ok_to_post"] = true;
            dr["CurrentPeriod"] = CurrMonth;
            dr["CurrentYear"] = CurrYear;
            if (dr["department"] == DBNull.Value)
              dr["department"] = "000";
            if (dr["emp_department"] == DBNull.Value)
              dr["emp_department"] = "000";
            #endregion

            #region Before calling function every row
            if (dr["doc_no"] != DBNull.Value)
              GlobalObjDVOPayrollProcess_PayEmployee.Doc_no = Convert.ToInt32(dr["doc_no"]);

            if (dr["cash_acct_no"] != DBNull.Value)
              GlobalObjDVOPayrollProcess_PayEmployee.Cash_acct_no = Convert.ToInt32(dr["cash_acct_no"]);
            if (dr["cash_amount"] != DBNull.Value)
              GlobalObjDVOPayrollProcess_PayEmployee.cash_amount = Convert.ToDecimal(dr["cash_amount"]);
            if (ds.Tables[0].Rows.Count != i + 1)
            {
              if (ds.Tables[0].Rows[i + 1]["doc_no"] != DBNull.Value && ds.Tables[0].Rows[i + 1]["doc_no"].ToString().Trim() != string.Empty)
              {
                dr["txt_doc_no"] = Convert.ToInt32(ds.Tables[0].Rows[i + 1]["doc_no"]);
              }
            }
            else
            {
              dr["txt_doc_no"] = -100;
            }
            //Get keyvalue for Cash Account..........
            string keyvalue = string.Empty;
            int id = 0;
            string acct_type = string.Empty;
            string acct_desc = string.Empty;
            int acct_no = GlobalObjDVOPayrollProcess_PayEmployee.Cash_acct_no;
            BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
            dr["cash_acct_key"] = keyvalue;

            #endregion Before calling function every row

            #region  Pre before group processing
            // Get the basic income amount for this cheque
            decimal dsBasic = BLLPyPostException.GetSumOfBasicAmount(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
            //Assign Basic sallery and calculate GrandTotal
            dr["basicSalary"] = dsBasic;
            dr["grand_basic"] = Convert.ToDecimal(dr["grand_basic"]) + Convert.ToDecimal(dr["basicSalary"]);

            // Get the taxable other income for this cheque
            decimal dsTaxableStmt = BLLPyPostException.GetSumOfTaxableStatement(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
            //Assign Taxable sallery 
            dr["oiTaxable"] = dsTaxableStmt;
            dr["grand_oi_tax"] = Convert.ToDecimal(dr["grand_oi_tax"]) + Convert.ToDecimal(dr["oiTaxable"]);

            //Get the non-taxable other income for this cheque
            decimal dsNonTaxableStmt = BLLPyPostException.GetSumOfNonTaxableStatement(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
            //Assign NonTaxable sallery 
            dr["oiNonTaxable"] = dsNonTaxableStmt;
            dr["grand_oi_nontax"] = Convert.ToDecimal(dr["grand_oi_nontax"]) + Convert.ToDecimal(dr["oiNonTaxable"]);


            //Check Bonus Status
            string dsbonusCheck = BLLPyPostException.GetBonusCheck(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no, dr["empl_code"].ToString().Trim());
            if (dsbonusCheck.Trim() != "")
            {
              dr["bonus_check"] = dsbonusCheck;
            }
            else
            {
              dr["bonus_check"] = "N";
            }
            if (dr["bonus_check"].ToString().Trim() == "Y")
            {
              Bonus_Check = true;
            }
            //Calculate Deduction Total
            decimal dsDedTotal = BLLPyPostException.GetSumDeductionTotal(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
            dr["ded_total"] = dsDedTotal;
            dr["grand_ded"] = Convert.ToDecimal(dr["grand_ded"]) + Convert.ToDecimal(dr["ded_total"]);
            //get the posting document number
            //start transactions for the doc_no
            if (CHECK_POST == "POST")
            {
              //get the posting document number

              //new_doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref objTransaction);
              //dr["txt_doc_no"] = new_doc_no;

              // start data base transactions for the document itself
              // some systems do not offer sufficient locks to use
              // row-level locking.  If your kernel can be tuned to
              //provide an adequate number of row-level locks, this
              // table-level locking will not be necessary.
              //lock table stypayid in share mode
              //lock table stypaydd in share mode
              //lock table stypayod in share mode
            }
            else
            {
              //# let rpt.txt_doc_no = "UNASSIGNED"
              //# let new_doc_no = new_doc_no + 1
              //new_doc_no += 1;
              // dr["txt_doc_no"] = new_doc_no;
            }

            //Find out the "special" obligation and deduction amounts.  This is needed only for the Exceptions report.
            if (CHECK_POST == "CHECK")
            {
              decimal ded_ssd = 0.0M;
              decimal ded_ssl = 0.0M;
              decimal obl_ssd = 0.0M;
              decimal obl_ssib = 0.0M;

              decimal DS_ded_ssd = BLLPyPostException.GetSumDeductionTotalSSD(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              ded_ssd = DS_ded_ssd;
              dr["ded_ssd"] = DS_ded_ssd;

              decimal DS_ded_ssl = BLLPyPostException.GetSumDeductionTotalSSL(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              ded_ssl = DS_ded_ssl;
              dr["ded_ssl"] = DS_ded_ssl;

              decimal DS_obl_ssd = BLLPyPostException.GetSumObligationTotalSSD(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              obl_ssd = DS_obl_ssd;
              dr["obl_ssd"] = DS_obl_ssd;

              decimal DS_obl_ssib = BLLPyPostException.GetSumObligationTotalSSIB(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              obl_ssib = DS_obl_ssib;
              dr["obl_ssib"] = DS_obl_ssib;
              if (objSCDVOMasterEmployee.TypeCode.ToString().Trim().Contains("WAG"))
              {
                object[] DsIncomeREG = new object[3];
                object[] DsIncomeOT = new object[3];
                object[] DsIncomeDT = new object[3];
                DsIncomeREG = BLLPyPostException.GetSumIncome(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no, "REGPY");
                dr["IncomeCodeREG"] = DsIncomeREG[0];
                dr["IncomeRateREG"] = DsIncomeREG[2];
                dr["IncomeHoursREG"] = DsIncomeREG[1];
                DsIncomeOT = BLLPyPostException.GetSumIncome(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no, "OT");
                dr["IncomeCodeOT"] = DsIncomeOT[0];
                dr["IncomeRateOT"] = DsIncomeOT[2];
                dr["IncomeHoursOT"] = DsIncomeOT[1];
                DsIncomeDT = BLLPyPostException.GetSumIncome(GlobalObjDVOPayrollProcess_PayEmployee.Doc_no, "DT");
                dr["IncomeCodeDT"] = DsIncomeDT[0];
                dr["IncomeRateDT"] = DsIncomeDT[2];
                dr["IncomeHoursDT"] = DsIncomeDT[1];
              }

            }

            #endregion  Pre before group processing

            #region call on_every_row function.........

            on_every_row(ref objTransaction, ref objDVOPostGLGlobal, ref GlobalObjDVOPayrollProcess_PayEmployee, ref GlobalObjDVOMasterEmployee, objDataTable, dr, out status, CHECK_POST, new_doc_no, ref objGLSumTable, ref ObjDetailTable);

            #endregion call on_every_row function.........

            #region processing on after doc_no group

            bool _postingStatus = false;
            if (ds.Tables[0].Rows.Count != i + 1)
            {
              if (GlobalObjDVOPayrollProcess_PayEmployee.Doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["doc_no"]))
              {
                _postingStatus = true;
              }
            }
            else if (ds.Tables[0].Rows.Count == i + 1)
              _postingStatus = true;
            if (_postingStatus)
            {

              //decimal d_tot_net = 0.0M;
              //decimal d_tot_gross = 0.0M;
              //decimal grand_d_hour = 0.0M;
              //decimal grand_d_net = 0.0M;
              //decimal tot_gross = 0.0M;
              //accumulate department totals

              dr["inc_net"] = Convert.ToDecimal(dr["inc_net"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());

              dr["inc_gross"] = Convert.ToDecimal(dr["inc_gross"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());
              //d_tot_hour += Convert.ToDecimal(dr["total_hours"].ToString().Trim());
              //grand_d_hour = grand_d_hour + Convert.ToDecimal(dr["total_hours"].ToString().Trim());
              //grand_d_net = grand_d_net + Convert.ToDecimal(dr["inc_net"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());
              //tot_gross = tot_gross + Convert.ToDecimal(dr["inc_gross"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());

              if (medcr_ded != medcr_obl)
              {
                //dr["ok_to_post"] = false;
                dr["warn_12"] = "**** Warning: Medicare Deduction not equal to Medicare Obligation.";
                dr["Problem8"] = "**** Warning:  Possible Medicare discrepancies exist in this report.";
              }

              if (fica_ded != fica_obl)
              {
                //dr["ok_to_post"] = false;
                dr["warn_13"] = "**** Warning: FICA Deduction not equal to FICA Obligation.";
                dr["Problem9"] = "**** **** Warning:  Possible FICA discrepancies exist in this report.";
              }
              if (Convert.ToBoolean(dr["ok_to_post"]))
              {
                //py_last(out int py_status, out string py_description,decimal py_c_accum,decimal py_i_accum,decimal py_d_accum,decimal py_ox_accum,decimal py_ol_accum)
                if (py_last(out py_status, out py_description, py_c_accum, py_i_accum, py_d_accum, py_ox_accum, py_ol_accum))
                {
                  if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
                  {
                    if (!BLLAccountingLiberary.gl_last(ref objDVOPostGLGlobal))
                    {

                      dr["ok_to_post"] = false;
                      dr["err_4"] = "**** Error: document does not balance.";

                      //dr["Problem4"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        throw new Exception("Error: document does not balance.");
                      }
                    }
                  }
                }
                else
                {
                  if (py_status == 10)
                  {

                    dr["ok_to_post"] = false;
                    dr["err_4"] = "***** Error: This document does not balance.";

                    //dr["Problem4"] = "ON";
                    if (CHECK_POST == "POST")
                    {
                      throw new Exception("Error: document does not balance.");
                    }
                  }
                  else if (py_status == 1)
                  {

                    dr["ok_to_post"] = false;
                    dr["err_1"] = "**** Error: Payroll is not installed.";

                    //dr["Problem1"] = "ON";
                    if (CHECK_POST == "POST")
                    {
                      throw new Exception("Error: Payroll is not installed.");
                    }
                  }

                }

              }

              //check pay frequency   //Need to make this function pay_time
              if (Convert.ToBoolean(dr["ok_to_post"]) && !objBLLPayrollFunctions.pay_time(Convert.ToDateTime(dr["last_pay"]), Convert.ToDateTime(dr["eop_date"]), dr["pay_period"].ToString().Trim(), Bonus_Check))
              {

                dr["err_5"] = "**** Warning: Employee not due to be paid. Last paid:" + Convert.ToDateTime(dr["last_pay"]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //dr["Problem5"] = "ON";
              }

              // update last pay date, sick accrual, vacation accrual
              if (Convert.ToBoolean(dr["ok_to_post"]))
              {

                if (CHECK_POST == "POST")
                {
                  if (dr["bonus_check"].ToString().Trim() == "Y")
                  {

                    int upd_result = BLLPyPostException.UPDATE_SICK_AND_VACATION(ref objTransaction, ref objDVOExceptionReports, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["sick_used"]), sick_accum, vac_accum, Convert.ToDecimal(dr["vac_used"]));
                    if (upd_result == 0)
                    {

                      dr["ok_to_post"] = false;

                      dr["err_2"] = "**** Error: Unable to update employee data.";
                      //dr["Problem2"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        throw new Exception("Error: Unable to update employee data.");
                      }
                    }
                  }
                  else
                  {
                    if (Convert.ToDateTime(dr["last_pay"]) > Convert.ToDateTime(dr["eop_date"]))
                    {

                      int upd_result = BLLPyPostException.UPDATE_SICK_AND_VACATION(ref objTransaction, ref objDVOExceptionReports, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["sick_used"]), sick_accum, vac_accum, Convert.ToDecimal(dr["vac_used"]));
                      if (upd_result == 0)
                      {
                        dr["ok_to_post"] = false;

                        dr["err_2"] = "**** Error: Unable to update employee data.";
                        //dr["Problem2"] = "ON";
                        if (CHECK_POST == "POST")
                        {
                          throw new Exception(" Error: Unable to update employee data.");
                        }
                      }
                    }
                    else
                    {

                      int upd_result = BLLPyPostException.UPDATE_SICK_PAY_AND_VACATION(ref objTransaction, ref objDVOExceptionReports, Convert.ToDateTime(dr["eop_date"]), dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["sick_used"]), sick_accum, vac_accum, Convert.ToDecimal(dr["vac_used"]));
                      if (upd_result == 0)
                      {

                        dr["ok_to_post"] = false;

                        dr["err_2"] = "**** Error: Unable to update employee data.";
                        //dr["Problem2"] = "ON";
                        if (CHECK_POST == "POST")
                        {

                          throw new Exception(" Error: Unable to update employee data.");
                        }
                      }
                    }
                  }

                  if (dr["accrue_vac"].ToString().Trim() == "Y")
                  {
                    BLLPyPostException.vac_time_accrue(ref objTransaction, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["total_hours"]), Convert.ToDecimal(dr["vac_allowed"]), Convert.ToDateTime(dr["eop_date"]), out status);
                    if (status == 1)
                    {

                      dr["ok_to_post"] = false;
                      dr["err_2"] = "**** Error: Unable to calculate vacation time accrual.";

                      //dr["Problem2"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        if (objTransaction != null)
                          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        throw new Exception(" Error: Unable to calculate vacation time accrual.");
                      }
                    }
                  }
                  if (dr["accrue_sick"].ToString().Trim() == "Y")
                  {
                    BLLPyPostException.sick_time_accrue(ref objTransaction, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["total_hours"]), Convert.ToDecimal(dr["vac_allowed"]), Convert.ToDateTime(dr["eop_date"]), out status);
                    if (status == 1)
                    {

                      dr["ok_to_post"] = false;

                      dr["err_2"] = "**** Error: Unable to calculate sick time accrueal.";
                      //dr["Problem2"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        if (objTransaction != null)
                          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);

                        throw new Exception(" Error: Unable to calculate vacation time accrueal.");
                      }

                    }
                  }
                }
              }


              if (CHECK_POST == "POST")
              {
                // delete and commit work or roll back
                if (Convert.ToBoolean(dr["ok_to_post"]) && BLLPyPostException.py_delete(ref objTransaction, ref objDVOExceptionReports, GlobalObjDVOPayrollProcess_PayEmployee.Doc_no))
                {
                  //commit work
                  //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                {
                  if (CHECK_POST == "POST")
                  {
                    if (objTransaction != null)
                      objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    throw new Exception("This document was not posted--the document number will not be re-used.");
                  }
                }

              }
              else
              {
                if (Convert.ToBoolean(dr["ok_to_post"]))
                {

                  int upd_result = BLLPyPostException.UPDATE_OK_TO_POST_STATUS(ref objTransaction, ref objDVOExceptionReports, "Y", GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                  if (upd_result == 0)
                  {

                    dr["err_0"] = "**** This document has errors.";
                    dr["Problem0"] = "**** Unable to update ok_to_post status.";
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    errorMassage.Append("[" + Curr_Emp_Code + "-" + "Unable to update ok_to_post status." + "]");
                    if (ds.Tables[0].Rows.Count != i + 1)
                      objTransaction = objDALBaseClassHelper.GetTransactionObject();
                  }
                  else
                  {
                    //commit work
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    recordsProcessed++;
                    if (ds.Tables[0].Rows.Count != i + 1)
                    {
                      objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    }
                    else
                    {
                      dr["Problem0"] = "***** Report completed successfully. No errors detected.";
                    }
                  }

                }
                else
                {
                  objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                  if (ds.Tables[0].Rows.Count != i + 1)
                    objTransaction = objDALBaseClassHelper.GetTransactionObject();
                  dr["Problem0"] = "**** Some documents in this report have errors.";
                  errorMassage.Append("[" + Curr_Emp_Code + "-" + "" + "]");
                }
              }

              //check for negative check amount
              if (Convert.ToDecimal(dr["cash_amount"]) < 0)
              {
                dr["warn_0"] = "**** Warning: Check amount negative -check will not print.";
                //dr["Problem5"] = "ON";
              }
              // check for temination date

              if (Convert.ToDateTime(dr["terminated"]) > Convert.ToDateTime(dr["eop_date"]))
              {
                dr["warn_1"] = "**** Warning: Employee terminated prior to payroll period.";
                //dr["Problem5"] = "ON";
              }

              //verify that FICA deduction has not reached limit
              int dup_ssn;
              dup_ssn = BLLGeneratePaySlipDetails.Get_dup_ssn(dr["soc_sec_num"].ToString().Trim());
              if (dfica_flag)
              {

                if (dup_ssn > 1)
                {
                  //DataSet dedSumAmount = BLLPyPostException.GetDeductionSumAmount(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["soc_sec_num"].ToString().Trim());//Edited by sanjay
                  decimal dedSumAmount = BLLPyPostException.GetDeductionSumAmount(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["soc_sec_num"].ToString().Trim());

                  if (dedSumAmount > 0)
                  {
                    //if (dedSumAmount.Tables[0].Rows[0][0] != DBNull.Value)
                    //act_accrual = Convert.ToDecimal(dedSumAmount.Tables[0].Rows[0][0]);
                    act_accrual = dedSumAmount;
                  }
                }
                else
                {
                  //DataSet dedSumAmount = BLLPyPostException.GetDeductionSumAmountOne(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["empl_code"].ToString().Trim());
                  //if (dedSumAmount.Tables[0].Rows[0][0] != DBNull.Value)
                  //    act_accrual = Convert.ToDecimal(dedSumAmount.Tables[0].Rows[0][0]);
                  decimal dedSumAmount = BLLPyPostException.GetDeductionSumAmountOne(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["empl_code"].ToString().Trim());
                  if (dedSumAmount > 0)
                    act_accrual = dedSumAmount;
                }

                //DataSet dsDedDfltLimit = BLLPyPostException.GET_ACT_DFLT_LIMIT(Convert.ToString(dr["empl_code"]), objGloabalPayDefaultsListStycntrc[0].fica_code);
                decimal dsDedDfltLimit = BLLPyPostException.GET_ACT_DFLT_LIMIT(Convert.ToString(dr["empl_code"]), objGloabalPayDefaultsListStycntrc[0].fica_code);
                //if (dsDedDfltLimit.Tables[0].Rows.Count > 0)
                //{
                //    if (act_limit == 0.0M)
                //    {
                //        if (dsDedDfltLimit.Tables[0].Rows[0][0] != DBNull.Value)
                //            act_limit = Convert.ToDecimal(dsDedDfltLimit.Tables[0].Rows[0][1]); //dflt_limit
                //    }
                //    else if (act_accrual >= act_limit)
                //    {
                //        dfica_flag = true;  //dfica_lmt
                //    }
                //}                                    
                if (act_limit == 0.0M)
                {
                  if (dsDedDfltLimit > 0)
                    act_limit = dsDedDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  dfica_flag = true;  //dfica_lmt
                }

              }

              // verify that medicare deduction has not reached limit

              if (dmedicare_flag)
              {

                if (dup_ssn > 1)
                {
                  decimal dedSumAmount = BLLPyPostException.GetDeductionSumAmount(objGloabalPayDefaultsListStycntrc[0].medicare_code, dr["soc_sec_num"].ToString().Trim());
                  if (dedSumAmount > 0)
                    act_accrual = dedSumAmount;

                }
                else
                {
                  decimal dedSumAmount = BLLPyPostException.GetDeductionSumAmountOne(objGloabalPayDefaultsListStycntrc[0].medicare_code, dr["empl_code"].ToString().Trim());
                  if (dedSumAmount > 0)
                    act_accrual = dedSumAmount;
                }

                decimal dsDedDfltLimit = BLLPyPostException.GET_ACT_DFLT_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].medicare_code);
                if (act_limit == 0.0M)
                {
                  if (dsDedDfltLimit > 0)
                    act_limit = dsDedDfltLimit;//dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  dmedicare_lmt = true;
                }

              }

              // verify that FICA obligation has not reached limit

              if (ofica_flag)
              {

                if (dup_ssn > 1)
                {
                  decimal oblSumAmount = BLLPyPostException.GetObligationSumAmount(objGloabalPayDefaultsListStycntrc[0].fica_ob_code, dr["soc_sec_num"].ToString().Trim());
                  if (oblSumAmount > 0)
                  {
                    act_accrual = oblSumAmount;
                  }
                }
                else
                {
                  decimal oblSumAmount = BLLPyPostException.GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].fica_ob_code, dr["empl_code"].ToString().Trim());
                  if (oblSumAmount > 0)
                    act_accrual = oblSumAmount;
                }

                decimal dsOblDfltLimit = BLLPyPostException.GET_ACT_DFLT_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].fica_ob_code);

                if (act_limit == 0.0M)
                {
                  if (dsOblDfltLimit > 0)
                    act_limit = dsOblDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  ofica_lmt = true;
                }

              }

              //verify that medicare obligation has not reached limit

              if (omedicare_flag)
              {
                if (dup_ssn > 1)
                {
                  decimal oblSumAmount = BLLPyPostException.GetObligationSumAmount(objGloabalPayDefaultsListStycntrc[0].medicare_ob_code, dr["soc_sec_num"].ToString().Trim());
                  if (oblSumAmount > 0)
                  {
                    act_accrual = oblSumAmount;
                  }
                }
                else
                {
                  act_accrual = BLLPyPostException.GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].medicare_ob_code, dr["empl_code"].ToString().Trim());
                  //decimal oblSumAmount = BLLPyPostException.GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].medicare_ob_code, dr["empl_code"].ToString().Trim());
                  //if (oblSumAmount.Tables[0].Rows.Count > 0)
                  //{
                  //    if (oblSumAmount.Tables[0].Rows[0][0] != DBNull.Value)
                  //        act_accrual = Convert.ToDecimal(oblSumAmount.Tables[0].Rows[0][0]);
                  //}
                }

                decimal dsOblDfltLimit = BLLPyPostException.GET_ACT_DFLT_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].medicare_ob_code);
                if (act_limit == 0.0M)
                {
                  if (dsOblDfltLimit > 0)
                    act_limit = dsOblDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  omedicare_lmt = true;
                }

              }

              // verify that FUTA obligation has not reached limit

              if (ofuta_flag)
              {

                if (dup_ssn > 1)
                {
                  decimal oblSumAmount = BLLPyPostException.GetObligationSumAmount(objGloabalPayDefaultsListStycntrc[0].futa_code, dr["soc_sec_num"].ToString().Trim());
                  if (oblSumAmount > 0)
                  {
                    act_accrual = oblSumAmount;
                  }
                }
                else
                {
                  decimal oblSumAmount = BLLPyPostException.GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].futa_code, dr["empl_code"].ToString().Trim());
                  if (oblSumAmount > 0)
                    act_accrual = oblSumAmount;
                }

                decimal dsOblDfltLimit = BLLPyPostException.GET_ACT_DFLT_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].futa_code);
                if (act_limit == 0.0M)
                {
                  if (dsOblDfltLimit > 0)
                    act_limit = dsOblDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  ofuta_lmt = true;
                }

              }

              //verify that employee is not EXEMPT from federal income tax
              if (dftax_flag)
              {
                int dsAllowence = BLLPyPostException.GET_ALLOWENCE(dr["empl_code"].ToString().Trim());
                if (dsAllowence > 0)
                {
                  if (dsAllowence == 99 && dr["soc_sec_num"] == DBNull.Value)
                  {
                    dftax_xmt = true;
                  }
                }
                else
                {
                  break;
                }
              }
              //removed the warning messages for FICA, FUTA, and Medicare -- Nevis and St. Kitts do not have these
              //if this is the edit list, hold the temporary sick/vacation
              // accumulated on this paycheck.
              if (CHECK_POST != "POST")
              {
                sick_acc_tmp = Convert.ToInt32(sick_accum);
                vac_acc_tmp = Convert.ToInt32(vac_accum);
              }

              //get current sick and vacation used values (may have changed
              //since the rpt record was loaded if other employees with same
              //social security number were paid sick or vacation pay)

              DataSet dsSickAndVacation = BLLPyPostException.GetSumOfSickAndVacation(dr["soc_sec_num"].ToString().Trim());
              if (dsSickAndVacation.Tables[0].Rows[0][0] == DBNull.Value)
                dsSickAndVacation.Tables[0].Rows[0][0] = 0;
              if (dsSickAndVacation.Tables[0].Rows[0][1] == DBNull.Value)
                dsSickAndVacation.Tables[0].Rows[0][1] = 0;
              // if it is the edit list then the values selected into
              // sick_accum and vac_accum do not include the current payroll
              if (dsSickAndVacation.Tables[0].Rows.Count > 0)
              {
                if (CHECK_POST != "POST")
                {
                  sick_accum = sick_acc_tmp + Convert.ToInt32(dsSickAndVacation.Tables[0].Rows[0][0]);
                  vac_accum = vac_acc_tmp + Convert.ToInt32(dsSickAndVacation.Tables[0].Rows[0][1]);
                }
              }

              //check for excess sick and vacation pay
              if (sick_accum > Convert.ToInt32(dr["sick_allowed"]))
              {
                dr["warn_4"] = "**** Warning: Excess sick leave has been indicated.";
                //dr["Problem5"] = "ON";
              }
              if (vac_accum > Convert.ToInt32(dr["vac_allowed"]))
              {
                dr["warn_5"] = "**** Warning: Excess vacation leave has been indicated.";
                //dr["Problem5"] = "ON";
              }
              objDVOPostGLGlobal = new DVOPostGLGlobal();
              #region Initialize Variables
              act_accrual = 0.0M;
              act_limit = 0.0M;
              dflt_limit = 0.0M;
              empl_allow = 0;
              entry_count = 0;
              sick_acc_tmp = 0;
              vac_acc_tmp = 0;
              d_tot_hour = 0.0M;
              status = 0;
              new_doc_no = 0;
              gl_status = 0;
              dmedicare_lmt = false;
              ofica_lmt = false;
              omedicare_lmt = false;
              ofuta_lmt = false;
              dftax_xmt = false;
              Bonus_Check = false;
              // variable used in ded_post
              dfica_flag = false;
              fica_ded = 0.0M;
              dmedicare_flag = false;
              medcr_ded = 0.0M;
              dftax_flag = false;
              //variable used in obl_post
              ofuta_flag = false;
              ofica_flag = false;
              fica_obl = 0.0M;
              omedicare_flag = false;
              medcr_obl = 0.0M;
              // variable used in inc_post
              sick_accum = 0.0M;
              vac_accum = 0.0M;
              //Variable used in py_post
              //py_installed;
              //Variable used To pass in py_post as an output
              py_c_accum = 0.0M;
              py_i_accum = 0.0M;
              py_d_accum = 0.0M;
              py_ox_accum = 0.0M;
              py_ol_accum = 0.0M;
              py_status = 0;
              py_description = string.Empty;
              #endregion
            }
            #endregion processing on after doc_no group

            #region commit work
            //commit transaction on last row..........
            if (ds.Tables[0].Rows.Count == i + 1)
            {
              //commit work 
              if (CHECK_POST == "POST")
              {
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                dr["Problem0"] = "**** These documents have been posted successfully.";
                recordsProcessed = ds.Tables[0].Rows.Count;
              }

            }
            #endregion

            objDataTable.Rows.Add(dr.ItemArray);
          }
        }
        else
        {
          //errorMassage.Append("[No Element to Process]");
        }
        FinalDs.Tables.Add(objDataTable);
        FinalDs.Tables.Add(objGLSumTable);
        FinalDs.Tables.Add(ObjDetailTable);
        FinalDs.Tables[0].TableName = "DSEXCEPTIONREPORTS";
        FinalDs.Tables[1].TableName = "GlSumTable";
        FinalDs.Tables[2].TableName = "DtlTable";
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        errorMassage.Append("[" + ex.Message + "]");
        if (CHECK_POST == "POST")
        {
          objDataTable.Rows.Clear();
          objGLSumTable.Rows.Clear();
          if (objDataTable.Columns.Contains("Problem0") && objDataTable.Columns.Contains("Problem1"))
          {
            DataRow dr = objDataTable.NewRow();
            dr["Problem0"] = "**** Posting has been terminated for empl_code -  " + Curr_Emp_Code;
            dr["Problem1"] = "**** " + ex.Message;
            objDataTable.Rows.Add(dr.ItemArray);
            FinalDs.Tables.Add(objDataTable);
            FinalDs.Tables.Add(objGLSumTable);
            FinalDs.Tables.Add(ObjDetailTable);
            FinalDs.Tables[0].TableName = "DSEXCEPTIONREPORTS";
            FinalDs.Tables[1].TableName = "GlSumTable";
            FinalDs.Tables[2].TableName = "DtlTable";
          }
          else
          {
            ExceptionManagement.ExceptionManager.Publish(ex);
            throw ex;
          }
        }
        else
        {
          ExceptionManagement.ExceptionManager.Publish(ex);
          //throw ex;
        }

      }
      finally
      {
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
          obj.processname = processName;
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
      //FinalDs.Tables[0].WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DSExceptionReports.xsd");
      //FinalDs.Tables[1].WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\GlSumTable.xsd");
      //FinalDs.Tables[2].WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DtlTable.xsd");
      return FinalDs;
    }

    public static void on_every_row(ref object objTransection, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DVOPayrollProcess_PayEmployee newGlobalObjDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee newGlobalObjDVOMasterEmployee, DataTable objDataTable, DataRow dr, out int status, string CHECK_POST, int new_doc_no, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      //DataRow drn = objDataTable.NewRow();
      status = 0;
      int old_cash_acct;
      decimal old_cash_amount;
      // post the document to stytranr, styactvd, stgtranr, and styacvtd
      // post the check   
      try
      {
        //used to determine the payroll department for each employee
        //For calling this function set the properties value of
        //EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = dr["empl_code"].ToString().Trim();
        objDVOFlexSegCommon.AccountType = dr["flexdeptaccttype"].ToString().Trim();
        string keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);
        if (keyvalue != "")
        {
          dr["flexdept"] = keyvalue;
          int AccountNumber = 0;
          string AccountType = string.Empty;
          int AccountTypeId = 0;
          string AccountDescription = string.Empty;
          BLLCommonUtilities.GetAccountInformation(keyvalue, out AccountNumber, out AccountType, out AccountTypeId, out AccountDescription);
          dr["flexdeptdesc"] = AccountDescription;
        }


        // determine if the employee uses direct deposit.  We cannot use the flag
        // Process_PayEmployee.deposit because o_dposit sets it to "N" after creating the
        //direct deposit entries.
        DataSet dsCashAccountAmount = BLLPyPostException.GetCashAccountAmount(newGlobalObjDVOPayrollProcess_PayEmployee.Doc_no, dr["empl_code"].ToString().Trim());
        //if the payroll document is not linked to a direct deposit entry, then the
        //employee does not use direct deposit.  Post the check as usual.     
        if (dsCashAccountAmount.Tables[0].Rows.Count > 0)
        {   // if employee has direct deposit then save the old values........
          old_cash_acct = Convert.ToInt32(dr["cash_acct_no"]);
          old_cash_amount = Convert.ToDecimal(dr["cash_amount"]);
          foreach (DataRow draccount in dsCashAccountAmount.Tables[0].Rows)
          {
            dr["cash_acct_no"] = Convert.ToInt32(draccount[0]);
            dr["cash_amount"] = Convert.ToDecimal(draccount[1]);
            if (!BLLPayrollFunctions.chk_post(ref objTransection, ref objDVOPostGLGlobal, ref newGlobalObjDVOPayrollProcess_PayEmployee, ref newGlobalObjDVOMasterEmployee, objGloabalPayDefaultsListStycntrc[0], objDataTable, ref dr, out status, CHECK_POST, new_doc_no, out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum, ref GLSumTable))
            {
              if (CHECK_POST == "POST")
              {
                throw new Exception("Posting has been terminated, " + py_description + " " + objDVOPostGLGlobal.description);
              }

            }
          }
          //restore the values
          dr["cash_acct_no"] = old_cash_acct;
          dr["cash_amount"] = old_cash_amount;
        }
        else
        {
          //employee does not use direct deposit.  Post the check as usual
          if (!BLLPayrollFunctions.chk_post(ref objTransection, ref objDVOPostGLGlobal, ref newGlobalObjDVOPayrollProcess_PayEmployee, ref newGlobalObjDVOMasterEmployee, objGloabalPayDefaultsListStycntrc[0], objDataTable, ref dr, out status, CHECK_POST, new_doc_no, out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum, ref GLSumTable))
          {
            if (CHECK_POST == "POST")
            {
              throw new Exception(py_description + " " + objDVOPostGLGlobal.description);
            }
          }
        }
        //post the income
        string err_desc = string.Empty;
        if (!BLLPyPostException.inc_post(ref objTransection, out err_desc, ref objDVOPostGLGlobal, ref dr, CHECK_POST, ref GLSumTable, ref ObjDetailTable))
        {
          if (CHECK_POST == "POST")
          {
            throw new Exception(err_desc);
          }
        }
        //post the deduction
        if (!BLLPyPostException.ded_post(ref objTransection, out err_desc, ref objDVOPostGLGlobal, ref dr, CHECK_POST, ref GLSumTable, ref ObjDetailTable))
        {
          if (CHECK_POST == "POST")
          {
            throw new Exception(err_desc);
          }
        }
        //post the obligation
        if (!BLLPyPostException.obl_post(ref objTransection, out err_desc, ref objDVOPostGLGlobal, ref dr, CHECK_POST, ref GLSumTable, ref ObjDetailTable))
        {
          if (CHECK_POST == "POST")
          {
            throw new Exception(err_desc);
          }
        }
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;

      }

    }

    private static bool inc_post(ref object objTransection, out string err_desc, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DataRow dr, string CHECK_POST, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      // used as a output parameter
      int py_status;
      string py_description;
      int inc_count;
      Boolean lo_flag = false;
      Boolean hi_flag = false;
      decimal pay_total = 0.0M;
      int line_number = 0;
      int prev_check = 0;
      //int empl_count = 0;
      string db_cr;
      err_desc = string.Empty;
      try
      {
        //empl_count = Get_empl_count(dr["empl_code"].ToString().Trim());
        List<DVOPayrollstypayid> objDVOPyrollstypayid = new List<DVOPayrollstypayid>();
        objDVOPyrollstypayid = BLLPyPostException.GetIncomeDataForINC_Post(Convert.ToInt32(dr["doc_no"]));
        if (objDVOPyrollstypayid.Count > 0)
        {
          for (inc_count = 0; inc_count < objDVOPyrollstypayid.Count; inc_count++)
          {
            if (objDVOPyrollstypayid[inc_count].amount != 0)
            {
              lo_flag = false;
              hi_flag = false;
              if (!objDVOPyrollstypayid[inc_count].lo_inc_amt_null)
              {
                // set flag for exceptions reporting (low end)
                dr["pay_lo_amt"] = objDVOPyrollstypayid[inc_count].lo_inc_amt;  //did not get this data table column still
                if (objDVOPyrollstypayid[inc_count].amount < objDVOPyrollstypayid[inc_count].lo_inc_amt)
                {
                  lo_flag = true;
                }
              }
              if (!objDVOPyrollstypayid[inc_count].hi_inc_amt_null)
              {
                dr["pay_hi_amt"] = objDVOPyrollstypayid[inc_count].hi_inc_amt;  //did not get this data table column still
                if (objDVOPyrollstypayid[inc_count].amount > objDVOPyrollstypayid[inc_count].hi_inc_amt)
                {
                  hi_flag = true;
                }
              }

              if (pay_total == 0)
              {
                //Prepare report outline
              }
              // build details outline
              if (CHECK_POST == "CHECK")
              {

                if (objDVOPyrollstypayid[inc_count].inc_code.Trim() != string.Empty && objDVOPyrollstypayid[inc_count].amount != 0)
                {
                  if (lo_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = objDVOPyrollstypayid[inc_count].inc_code;
                    drdtl["amount"] = objDVOPyrollstypayid[inc_count].amount;
                    drdtl["lo_hi_amt"] = dr["pay_lo_amt"];
                    drdtl["lo_flag"] = true;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }
                  if (hi_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = objDVOPyrollstypayid[inc_count].inc_code;
                    drdtl["amount"] = objDVOPyrollstypayid[inc_count].amount;
                    drdtl["lo_hi_amt"] = dr["pay_hi_amt"];
                    drdtl["lo_flag"] = false;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }

                }


              }
              // build detail for EditList........
              else if (CHECK_POST == "EDIT")
              {
                DataRow drdtl = ObjDetailTable.NewRow();
                drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                drdtl["code_line"] = "0"; // Here "0" represents  Income Code.
                drdtl["pay_code"] = objDVOPyrollstypayid[inc_count].inc_code;
                drdtl["pay_desc"] = objDVOPyrollstypayid[inc_count].description_MasterIncCodes;
                drdtl["amount"] = objDVOPyrollstypayid[inc_count].amount;
                //Get Keyvalue for account........
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                if (objDVOPyrollstypayid[inc_count].acct_no == 0)
                  objDVOPyrollstypayid[inc_count].acct_no = objGloabalPayDefaultsListStycntrc[0].exp_acct;
                BLLCommonUtilities.GetAccountInformation(objDVOPyrollstypayid[inc_count].acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drdtl["account"] = keyvalue;
                ObjDetailTable.Rows.Add(drdtl.ItemArray);

              }
              dr["pay_code"] = objDVOPyrollstypayid[inc_count].inc_code;
              dr["code_desc"] = objDVOPyrollstypayid[inc_count].description_MasterIncCodes;
              dr["pay_acct_no"] = objDVOPyrollstypayid[inc_count].acct_no;
              dr["pay_dept"] = objDVOPyrollstypayid[inc_count].Department;
              dr["pay_amount"] = objDVOPyrollstypayid[inc_count].amount;
              pay_total = pay_total + Convert.ToDecimal(dr["pay_amount"]);

              //make sure values are valid
              if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
              {
                dr["pay_acct_no"] = objGloabalPayDefaultsListStycntrc[0].exp_acct; // dr["exp_acct"];
                if (Convert.ToInt32(dr["pay_acct_no"].ToString().Trim()) == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No expense account number.";
                  err_desc = "Error: No expense account number.";
                  if (CHECK_POST == "POST")
                  {
                    dr["ok_to_post"] = false;
                    return false;
                  }
                }
              }

              if (dr["pay_dept"].ToString().Trim() == string.Empty)
              {
                dr["pay_dept"] = dr["department"].ToString().Trim();
              }

              if (CHECK_POST == "POST")
              {
                // If this is a duplicate of an existing code for this
                // employee code, just acquire information to update
                // the accumulation buckets for the first instance
                // of the code in the employee record.
                // If the code is brand new to the employee,
                // (add_code = "Y", then we adjust the line_no
                // value further down in this function.
                if (objDVOPyrollstypayid[inc_count].add_code.Trim() == "Z" || objDVOPyrollstypayid[inc_count].add_code.Trim() == "Y")
                {
                  line_number = Get_MIN_LINENUMBER(dr["empl_code"].ToString().Trim(), dr["pay_code"].ToString().Trim());
                  if (line_number != 0)
                  {
                    objDVOPyrollstypayid[inc_count].add_code = "Z";
                    objDVOPyrollstypayid[inc_count].line_no = line_number;
                  }
                }
              }

              // Post the income to payroll
              //        if not py_post(check_post, "PY", new_doc_no, post_no, today,
              //rpt.doc_date, rpt.pay_date, rpt.empl_code, "PAYROLL ENTRY",
              //rpt.check_no, rpt.pay_code, "B", rpt.pay_amount,
              //rpt.pay_acct_no, rpt.pay_dept, rpt.eop_date,
              //inc_ref.number, inc_ref.hours, inc_ref.inc_rate,
              //inc_ref.line_no)

              if (!BLLPayrollFunctions.py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"].ToString().Trim()), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "B", Convert.ToDecimal(dr["pay_amount"]), Convert.ToInt32(dr["pay_acct_no"]), dr["pay_dept"].ToString().Trim(), Convert.ToDateTime(dr["eop_date"].ToString().Trim()), objDVOPyrollstypayid[inc_count].number.ToString().Trim(), objDVOPyrollstypayid[inc_count].hours, Convert.ToString(objDVOPyrollstypayid[inc_count].inc_rate), Convert.ToString(objDVOPyrollstypayid[inc_count].line_no), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum))
              {
                if (py_status == 1)
                {
                  //payroll not installed
                  dr["ok_to_post"] = false;
                  dr["err_1"] = "**** Error: " + py_description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
                else if (py_status == 4) //document number out of sequence
                {
                  dr["err_5"] = "**** Error: " + py_description;
                  //return false;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: " + py_description;
                  err_desc = py_description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                  //return false;
                }
              }

              //if the amount is negative, reverse the sense of the debit/credit
              //and reverse the amount
              db_cr = "D";
              decimal amount = Convert.ToDecimal(dr["pay_amount"]);
              if (amount < 0)
              {

                db_cr = "C";
                amount = amount * (-1);
              }
              // post check to general ledger if stycntrc.post_gl field is set to Y

              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //  THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //  END IF


                //        if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, rpt.pay_acct_no, rpt.pay_dept, amount,
                //db_cr)


                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"].ToString().Trim());
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Convert.ToInt32(dr["pay_acct_no"]);
                objDVOPostGL.department = dr["pay_dept"].ToString().Trim();
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = BLLAccountingLiberary.Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }
                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }

                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL, stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }

              // sick pay accumulation
              if (dr["pay_code"].ToString().Trim() == dr["sick_code"].ToString().Trim())
              {
                sick_accum += (objDVOPyrollstypayid[inc_count].number ?? 0);
              }
              //vacation pay accumulation
              if (dr["pay_code"].ToString().Trim() == dr["vac_code"].ToString().Trim())
              {
                vac_accum += (objDVOPyrollstypayid[inc_count].number ?? 0);
              }
              if (CHECK_POST == "POST")
              {
                // test to see if we need to add the code to the empl record
                if (objDVOPyrollstypayid[inc_count].add_code == "Y")
                {
                  // new code for the employee
                  //get array size for this employee for line_no value
                  if (prev_check != 123)
                  {
                    prev_check = 123;
                    line_number = Get_MAX_LINENUMBER(dr["empl_code"].ToString().Trim());
                    if (line_number > 0)
                    {
                      line_number += 1;
                    }
                    else
                    {
                      line_number = 0;
                    }
                  }
                  //append row to employee record for future accruals
                  // call updt_emp_inc(inc_ref.inc_code, inc_ref.acct_no,inc_ref.department, line_number, inc_ref.amount)
                  string upd_status;
                  int i = BLLPyPostException.updt_emp_inc(ref objTransection, objDVOPyrollstypayid[inc_count].inc_code, (objDVOPyrollstypayid[inc_count].acct_no).ToString().Trim(), objDVOPyrollstypayid[inc_count].Department, line_number, objDVOPyrollstypayid[inc_count].amount, dr["empl_code"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), out upd_status);
                  if (i != 1)
                  {
                    dr["warn_11"] = upd_status;
                    if (CHECK_POST == "POST")
                    {
                      err_desc = upd_status;
                      return false;
                    }
                  }
                }
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private static bool ded_post(ref object objTransection, out string err_desc, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DataRow dr, string CHECK_POST, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      // used as a output parameter
      int py_status;
      string py_description;
      //this function prepares the report rows for the deduction data and
      //calls py_post and gl_post to post to styactvd and stgactvd.
      string db_cr;
      err_desc = string.Empty;
      int ded_count = 0;
      Boolean lo_flag = false;
      Boolean hi_flag = false;
      decimal pay_total = 0.0M;
      int line_number = 0;
      int prev_check = 0;
      string nss_ded_code = string.Empty;
      DVOExceptionReports objDvoExpReports = new DVOExceptionReports();
      try
      {
        //empl_count = Get_empl_countdd(dr["empl_code"].ToString().Trim());
        List<DVOPayrollstypaydd> objDVOPyrollstypaydd = new List<DVOPayrollstypaydd>();
        //set the deduction row variables
        objDVOPyrollstypaydd = BLLPyPostException.GetIncomeDataForDED_Post(Convert.ToInt32(dr["doc_no"]));
        if (objDVOPyrollstypaydd.Count > 0)
        {
          for (ded_count = 0; ded_count < objDVOPyrollstypaydd.Count; ded_count++)
          {
            if (objDVOPyrollstypaydd[ded_count].amount != 0)
            {
              lo_flag = false;
              hi_flag = false;
              if (!objDVOPyrollstypaydd[ded_count].lo_ded_amt_null)
              {
                // set flag for exceptions reporting (low end)
                dr["pay_lo_amt"] = objDVOPyrollstypaydd[ded_count].lo_ded_amt;  //did not get this data table column still
                if (objDVOPyrollstypaydd[ded_count].amount < objDVOPyrollstypaydd[ded_count].lo_ded_amt)
                {
                  lo_flag = true;
                }
              }
              if (!objDVOPyrollstypaydd[ded_count].hi_ded_amt_null)
              {
                dr["pay_hi_amt"] = objDVOPyrollstypaydd[ded_count].hi_ded_amt;  //did not get this data table column still
                if (objDVOPyrollstypaydd[ded_count].amount > objDVOPyrollstypaydd[ded_count].hi_ded_amt)
                {
                  hi_flag = true;
                }
              }
              if (pay_total == 0)
              {
                //Prepare report outline
              }

              // build details for exception report....

              if (CHECK_POST == "CHECK")
              {
                if (objDVOPyrollstypaydd[ded_count].ded_code.Trim() != string.Empty && objDVOPyrollstypaydd[ded_count].amount != 0)
                {
                  if (lo_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = objDVOPyrollstypaydd[ded_count].ded_code;
                    drdtl["amount"] = objDVOPyrollstypaydd[ded_count].amount;
                    drdtl["lo_hi_amt"] = dr["pay_lo_amt"];
                    drdtl["lo_flag"] = true;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }
                  if (hi_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = objDVOPyrollstypaydd[ded_count].ded_code;
                    drdtl["amount"] = objDVOPyrollstypaydd[ded_count].amount;
                    drdtl["lo_hi_amt"] = dr["pay_hi_amt"];
                    drdtl["lo_flag"] = false;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }

                }
              }
              // build detail for EditList........
              else if (CHECK_POST == "EDIT")
              {
                DataRow drdtl = ObjDetailTable.NewRow();
                drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                drdtl["code_line"] = "1"; // Here "0" represents  Deduction Code.
                drdtl["pay_code"] = objDVOPyrollstypaydd[ded_count].ded_code.Trim();
                drdtl["pay_desc"] = objDVOPyrollstypaydd[ded_count].description_MasterIncCodes.Trim();
                drdtl["amount"] = objDVOPyrollstypaydd[ded_count].amount;
                //Get Keyvalue for account........
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                if (objDVOPyrollstypaydd[ded_count].acct_no == 0)
                  objDVOPyrollstypaydd[ded_count].acct_no = objGloabalPayDefaultsListStycntrc[0].liab_acct;
                BLLCommonUtilities.GetAccountInformation(objDVOPyrollstypaydd[ded_count].acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drdtl["account"] = keyvalue;
                ObjDetailTable.Rows.Add(drdtl.ItemArray);

              }
              dr["pay_code"] = objDVOPyrollstypaydd[ded_count].ded_code;
              dr["code_desc"] = objDVOPyrollstypaydd[ded_count].description_MasterIncCodes;
              dr["pay_acct_no"] = objDVOPyrollstypaydd[ded_count].acct_no;
              dr["pay_dept"] = objDVOPyrollstypaydd[ded_count].Department;
              dr["pay_amount"] = objDVOPyrollstypaydd[ded_count].amount;
              pay_total = pay_total + Convert.ToDecimal(dr["pay_amount"]);

              //make sure values are valid
              if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
              {
                dr["pay_acct_no"] = objGloabalPayDefaultsListStycntrc[0].liab_acct;// dr["liab_acct"];   // didn't get the column dr["liab_acct"];
                if (Convert.ToInt32(dr["pay_acct_no"].ToString().Trim()) == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No liablility account number.";
                  //dr["Problem2"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "No liablility account number.";
                    return false;
                  }
                }
              }

              if (dr["pay_dept"].ToString().Trim() == string.Empty)
              {
                dr["pay_dept"] = dr["department"].ToString().Trim();   //rpt.emp_department
              }

              if (CHECK_POST == "POST")
              {
                // If this is a duplicate of an existing code for this
                // employee code, just acquire information to update
                // the accumulation buckets for the first instance
                // of the code in the employee record.
                // If the code is brand new to the employee,
                // (add_code = "Y", then we adjust the line_no
                // value further down in this function.
                if (objDVOPyrollstypaydd[ded_count].add_code == "Z" || objDVOPyrollstypaydd[ded_count].add_code == "Y")
                {
                  line_number = Get_MIN_LINENUMBERDD(dr["empl_code"].ToString().Trim(), dr["pay_code"].ToString().Trim());
                  if (line_number != 0)
                  {
                    objDVOPyrollstypaydd[ded_count].add_code = "Z";
                    objDVOPyrollstypaydd[ded_count].line_no = line_number;
                  }
                }
              }
              // Commented by Sarvjeet Verma On 03/06/2009.
              //**********Need to be implemented ************************************************************
              //if amount submitted to be posted is greater than or less than allowed amount,then have to post
              //allowed amount only,in such case have to maintain record of remaining amount.
              //remaining amount should go into any other account like suspense account.   
              //***************************************************************************************
              #region Post Nss Deduction........
              int status = 0;
              decimal nss_amount = 0;
              // # lets post the deduction code to the nss if required
              //Get Nss Deduction Code from nsscontrol table.........
              if (nss_ded_code == string.Empty)
                nss_ded_code = get_nss_ded_code();
              //make sure this  deduction is Nss deduction ...
              if (dr["pay_code"].ToString().Trim() == nss_ded_code)
              {
                // everything went ok with the deduction
                //lets post the deduction code if is part of the nss      
                //if (!BLLPyPostException.nss_check_post(ref objTransection, CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"].ToString().Trim()), DateTime.Now, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["pay_amount"]), "NSS PAYROLL", dr["pay_code"].ToString().Trim(), Convert.ToInt32(objDVOPyrollstypaydd[ded_count].line_no.ToString().Trim()), out status, out nss_amount, out err_desc))
                //{
                //    if (CHECK_POST == "POST")
                //    {
                //        dr["ok_to_post"] = false;
                //        return false;
                //    }
                //    else
                //    {
                //        dr["ok_to_post"] = false;
                //        dr["err_1"] = err_desc;
                //    }
                //}
                //else
                //{
                //    if (status == 1)
                //        dr["warn_5"] = err_desc;
                //    dr["pay_amount"] = nss_amount;
                //}
              }
              #endregion


              // post the deduction to payroll
              if (!BLLPayrollFunctions.py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "C", Convert.ToDecimal(dr["pay_amount"]), Convert.ToInt32(dr["pay_acct_no"]), dr["pay_dept"].ToString().Trim(), Convert.ToDateTime(dr["eop_date"]), "0", 0, objDVOPyrollstypaydd[ded_count].ded_rate.ToString().Trim(), objDVOPyrollstypaydd[ded_count].line_no.ToString().Trim(), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum))
              {
                //payroll not installed
                dr["ok_to_post"] = false;
                dr["err_1"] = "**** Error: " + py_description;
                //dr["Problem1"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = py_description;
                  return false;
                }

              }
              if (py_status == 0)
              {
                // # Update the balance of any year rollover type of deduction code (ie. codes that are not zeroed out
                // # at the end of th year and continue until the balance is zero such as for load repayments)
                if (objDVOPyrollstypaydd[0].yearrollover == "Y")
                {
                  //Get the current employee's balance if any
                  DataSet dsAccountBal = BLLPyPostException.Get_DED_BALANCE(dr["pay_code"].ToString().Trim(), dr["empl_code"].ToString().Trim());
                  if (dsAccountBal.Tables[0].Rows.Count > 0)
                  {
                    if (dsAccountBal.Tables[0].Rows[0][1] != DBNull.Value)
                    {
                      if (Convert.ToDecimal(dsAccountBal.Tables[0].Rows[0][1]) > 0)
                      {
                        // Now subtract the amount reducing the value of the balance
                        int updBalRetvalue = BLLPyPostException.UPDATE_DED_BALANCE(ref objTransection, ref objDvoExpReports, Convert.ToInt32(dsAccountBal.Tables[0].Rows[0][0]), Convert.ToDecimal(dr["pay_amount"]));
                        if (updBalRetvalue != 1)
                        {
                          //RollBack Transection
                          if (CHECK_POST == "POST")
                          {
                            err_desc = "An SQL Error has occurred while updating MasterEmployeeDeductions";
                            return false;
                          }
                        }

                      }

                    }
                  }
                }
              }


              //if the amount is negative, reverse the sense of the debit/credit
              //and reverse the amount
              db_cr = "C";
              decimal amount = Convert.ToDecimal(dr["pay_amount"]);
              if (amount < 0)
              {

                db_cr = "D";
                amount = amount * (-1);
              }
              // post check to general ledger if stycntrc.post_gl field is set to Y

              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //   THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //   END IF


                //        if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, rpt.pay_acct_no, rpt.pay_dept, amount,
                //db_cr)


                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"].ToString().Trim());
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Convert.ToInt32(dr["pay_acct_no"]);
                objDVOPostGL.department = dr["pay_dept"].ToString().Trim();
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = BLLAccountingLiberary.Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }

                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }
                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                //dr["Problem7"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL,stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }

              // check for federal tax deduction
              if (objDVOPyrollstypaydd[ded_count].ded_code == objGloabalPayDefaultsListStycntrc[0].fedtax_code) //// dr["fedtax_code"].ToString().Trim()
              {
                dftax_flag = true;
              }
              //check for fica deduction

              if (objDVOPyrollstypaydd[ded_count].ded_code == objGloabalPayDefaultsListStycntrc[0].fica_code)//dr["fica_code"].ToString().Trim()
              {
                dfica_flag = true;
                fica_ded = fica_ded + objDVOPyrollstypaydd[ded_count].amount ?? 0; //make nullable decimal By Rahul
              }

              //check for medicare deduction
              if (objDVOPyrollstypaydd[ded_count].ded_code == objGloabalPayDefaultsListStycntrc[0].medicare_code)//dr["medicare_code"].ToString().Trim()
              {
                dmedicare_flag = true;
                medcr_ded = medcr_ded + objDVOPyrollstypaydd[ded_count].amount ?? 0;//make nullable decimal By Rahul
              }


              if (CHECK_POST == "POST")
              {
                // test to see if we need to add the code to the empl record
                if (objDVOPyrollstypaydd[ded_count].add_code == "Y")
                {
                  // new code for the employee
                  //get array size for this employee for line_no value
                  if (prev_check != 123)
                  {
                    prev_check = 123;
                    line_number = Get_MAX_LINENUMBERDD(dr["empl_code"].ToString().Trim());
                    if (line_number > 0)
                    {
                      line_number += 1;
                    }
                    else
                    {
                      line_number = 0;
                    }
                  }
                  //append row to employee record for future accruals
                  //         call updt_emp_ded(ded_ref.ded_code, ded_ref.acct_no,
                  //ded_ref.department, line_number, ded_ref.amount)
                  string upd_status;
                  //make nullable decimal By Rahul
                  int i = BLLPyPostException.updt_emp_ded(ref objTransection, objDVOPyrollstypaydd[ded_count].ded_code, objDVOPyrollstypaydd[ded_count].acct_no.ToString().Trim(), objDVOPyrollstypaydd[ded_count].Department, line_number, objDVOPyrollstypaydd[ded_count].amount ?? 0, dr["empl_code"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), Convert.ToDateTime(dr["eop_date"].ToString().Trim()), out upd_status);
                  if (i != 1)
                  {
                    dr["warn_11"] = upd_status;
                    err_desc = upd_status;
                    return false;
                  }
                }
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private static bool obl_post(ref object objTransection, out string err_desc, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DataRow dr, string CHECK_POST, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      // used as a output parameter
      int py_status;
      string py_description;

      decimal amount = 0.0M; // like stypayod.amount
      decimal pay_total = 0.0M;  //like stypayod.amount,
      string db_cr;
      int line_number = 0;
      int prev_check = 0;
      int empl_count = 0;
      int obl_count = 0;
      err_desc = string.Empty;
      try
      {
        List<DVOPayrollStypayod> objDVOPayrollStypayod = new List<DVOPayrollStypayod>();
        objDVOPayrollStypayod = BLLPyPostException.GetObligationDataForOBL_Post(Convert.ToInt32(dr["doc_no"]));
        if (objDVOPayrollStypayod.Count > 0)
        {
          for (obl_count = 0; obl_count < objDVOPayrollStypayod.Count; obl_count++)
          {
            if (objDVOPayrollStypayod[obl_count].amount != 0)
            {
              // build details outline
              dr["pay_code"] = objDVOPayrollStypayod[obl_count].obl_code;
              dr["code_desc"] = objDVOPayrollStypayod[obl_count].description_MasterIncCodes;
              dr["pay_acct_no"] = objDVOPayrollStypayod[obl_count].acct_no;
              dr["pay_dept"] = objDVOPayrollStypayod[obl_count].Department;
              dr["pay_amount"] = objDVOPayrollStypayod[obl_count].amount;
              pay_total = pay_total + Convert.ToDecimal(dr["pay_amount"]);
              // build detail for EditList........
              if (CHECK_POST == "EDIT")
              {
                DataRow drdtl = ObjDetailTable.NewRow();
                drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                drdtl["code_line"] = "2"; // Here "0" represents  Obligation Code.
                drdtl["pay_code"] = objDVOPayrollStypayod[obl_count].obl_code;
                drdtl["pay_desc"] = objDVOPayrollStypayod[obl_count].description_MasterIncCodes;
                drdtl["amount"] = objDVOPayrollStypayod[obl_count].amount;
                //Get Keyvalue for account........
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                if (objDVOPayrollStypayod[obl_count].acct_no == 0)
                  objDVOPayrollStypayod[obl_count].acct_no = objGloabalPayDefaultsListStycntrc[0].exp_acct;
                BLLCommonUtilities.GetAccountInformation(objDVOPayrollStypayod[obl_count].acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drdtl["account"] = keyvalue;
                ObjDetailTable.Rows.Add(drdtl.ItemArray);

              }
              //make sure values are valid
              if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
              {
                dr["pay_acct_no"] = objGloabalPayDefaultsListStycntrc[0].exp_acct;// dr["exp_acct"];
                if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No expense account number.";
                  // dr["Problem2"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "Error: No expense account number.";
                    return false;
                  }
                }
              }

              if (dr["pay_dept"].ToString().Trim() == string.Empty)
              {
                dr["pay_dept"] = dr["department"].ToString().Trim();
              }

              if (CHECK_POST == "POST")
              {
                // If this is a duplicate of an existing code for this
                // employee code, just acquire information to update
                // the accumulation buckets for the first instance
                // of the code in the employee record.
                // If the code is brand new to the employee,
                // (add_code = "Y", then we adjust the line_no
                // value further down in this function.
                if (objDVOPayrollStypayod[obl_count].add_code == "Z" || objDVOPayrollStypayod[obl_count].add_code == "Y")
                {
                  line_number = GET_MIN_LINENUMBER_OBL(dr["empl_code"].ToString().Trim(), dr["pay_code"].ToString().Trim());
                  if (line_number != 0)
                  {
                    objDVOPayrollStypayod[obl_count].add_code = "Z";
                    objDVOPayrollStypayod[obl_count].line_no = line_number;
                  }
                }
              }

              //  post the obligation credit to payroll................
              if (!BLLPayrollFunctions.py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "D", Convert.ToDecimal(dr["pay_amount"]), Convert.ToInt32(dr["pay_acct_no"]), dr["pay_dept"].ToString().Trim(), Convert.ToDateTime(dr["eop_date"]), "0", 0, objDVOPayrollStypayod[obl_count].obl_rate.ToString().Trim(), objDVOPayrollStypayod[obl_count].line_no.ToString().Trim(), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum))
              {
                if (py_status == 1)
                {
                  //payroll not installed
                  dr["ok_to_post"] = false;
                  dr["err_1"] = "**** Error: " + py_description;
                  //dr["Problem1"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
                else if (py_status == 4) //document number out of sequence
                {
                  dr["err_5"] = "**** Error: " + py_description;
                  //dr["Problem5"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: " + py_description;
                  //dr["Problem2"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
              }

              //if the amount is negative, reverse the sense of the debit/credit
              //and reverse the amount
              db_cr = "D";
              amount = Convert.ToDecimal(dr["pay_amount"]);
              if (amount < 0)
              {

                db_cr = "C";
                amount = amount * (-1);
              }
              // post obligation credit to G/L if stycntrc.post_gl  field is set to Y
              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //   THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //   END IF


                //if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, rpt.pay_acct_no, rpt.pay_dept, amount,
                //db_cr)


                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Convert.ToInt32(dr["pay_acct_no"]);
                objDVOPostGL.department = dr["pay_dept"].ToString().Trim();
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = BLLAccountingLiberary.Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }
                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }
                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                //dr["Problem7"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL,stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }

              // make sure values are valid
              if (objDVOPayrollStypayod[obl_count].bal_acct_no == 0)
              {
                objDVOPayrollStypayod[obl_count].bal_acct_no = objGloabalPayDefaultsListStycntrc[0].liab_acct;
                if (objDVOPayrollStypayod[obl_count].bal_acct_no == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No liablility account number.";
                  // return false;
                }
              }

              if (objDVOPayrollStypayod[obl_count].bal_dept.Trim() == string.Empty)
              {
                objDVOPayrollStypayod[obl_count].bal_dept = dr["emp_department"].ToString().Trim();
              }


              //post the obligation debit to payroll

              //if not py_post(check_post, "PY", new_doc_no, post_no, today,rpt.doc_date, rpt.pay_date, rpt.empl_code, "PAYROLL ENTRY",rpt.check_no, rpt.pay_code, "E",rpt.pay_amount,obl_ref.bal_acct_no, obl_ref.bal_dept, rpt.eop_date, "","", obl_ref.obl_rate, obl_ref.line_no)

              if (!BLLPayrollFunctions.py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "E", Convert.ToDecimal(dr["pay_amount"]), objDVOPayrollStypayod[obl_count].bal_acct_no, objDVOPayrollStypayod[obl_count].bal_dept, Convert.ToDateTime(dr["eop_date"]), "0", 0, objDVOPayrollStypayod[obl_count].obl_rate.ToString().Trim(), objDVOPayrollStypayod[obl_count].line_no.ToString().Trim(), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum))
              {
                if (py_status == 1)
                {
                  //payroll not installed
                  dr["ok_to_post"] = false;
                  dr["err_1"] = "**** Error: " + py_description;
                  //dr["Problem1"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
                else if (py_status == 4) //document number out of sequence
                {
                  dr["err_5"] = "**** Error: " + py_description;
                  //dr["Problem5"] = "ON";
                  // return false;
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: " + py_description;
                  //dr["Problem2"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = py_description;
                    return false;
                  }
                }
              }

              //reverse the sense of the debit/credit
              if (db_cr == "D")
              {
                db_cr = "C";
              }
              else
              {
                db_cr = "D";
              }

              // post obligation credit to G/L if stycntrc.post_gl  field is set to Y

              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //   THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //   END IF


                //if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, obl_ref.bal_acct_no, obl_ref.bal_dept,
                //amount, db_cr)



                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = objDVOPayrollStypayod[obl_count].bal_acct_no;
                objDVOPostGL.department = objDVOPayrollStypayod[obl_count].bal_dept;
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = BLLAccountingLiberary.Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }
                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }
                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                string keyvalue = string.Empty;
                int id = 0;
                string acct_type = string.Empty;
                string acct_desc = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                //dr["Problem7"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL,stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }



              // check for futa obligation
              if (objDVOPayrollStypayod[obl_count].obl_code == objGloabalPayDefaultsListStycntrc[0].futa_code) // dr["futa_code"].ToString().Trim()
              {
                ofuta_flag = true;
              }
              //check for fica obligation
              if (objDVOPayrollStypayod[obl_count].obl_code == objGloabalPayDefaultsListStycntrc[0].fica_ob_code)  //dr["fica_ob_code"].ToString().Trim()
              {
                ofica_flag = true;
                fica_obl = fica_obl + objDVOPayrollStypayod[obl_count].amount ?? 0; //make nullable decimal By Rahul
              }

              //check for medicare obligation
              if (objDVOPayrollStypayod[obl_count].obl_code == objGloabalPayDefaultsListStycntrc[0].medicare_ob_code)//dr["medicare_ob_code"].ToString().Trim()
              {
                omedicare_flag = true;
                medcr_obl = medcr_obl + objDVOPayrollStypayod[obl_count].amount ?? 0;//make nullable decimal By Rahul
              }

              if (CHECK_POST == "POST")
              {
                // test to see if we need to add the code to the empl record
                if (objDVOPayrollStypayod[obl_count].add_code == "Y")
                {
                  // new code for the employee
                  //get array size for this employee for line_no value
                  if (prev_check != 123)
                  {
                    prev_check = 123;
                    line_number = Get_MAX_LINENUMBER_OBL(dr["empl_code"].ToString().Trim());
                    if (line_number > 0)
                    {
                      line_number += 1;
                    }
                    else
                    {
                      line_number = 0;
                    }
                  }
                  //# append row to employee record for future accruals
                  // call updt_emp_obl(obl_ref.obl_code, obl_ref.acct_no,
                  //         obl_ref.department, obl_ref.bal_acct_no,
                  //         obl_ref.bal_dept, line_number, obl_ref.amount)
                  string upd_status;
                  //make nullable decimal By Rahul
                  int i = BLLPyPostException.updt_emp_obl(ref objTransection, objDVOPayrollStypayod[obl_count].obl_code, objDVOPayrollStypayod[obl_count].acct_no.ToString().Trim(), objDVOPayrollStypayod[obl_count].Department, line_number, objDVOPayrollStypayod[obl_count].amount ?? 0, dr["empl_code"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), objDVOPayrollStypayod[obl_count].bal_acct_no, objDVOPayrollStypayod[obl_count].bal_dept, out upd_status);
                  if (i != 1)
                  {
                    dr["warn_11"] = upd_status;
                    if (CHECK_POST == "POST")
                    {
                      err_desc = upd_status;
                      return false;
                    }
                  }
                }
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private static int updt_emp_inc(ref object objTransection, string inc_code, string acct_no, string dept, int line_number, decimal? amount, string empl_code, DateTime payDate, out string upd_status)
    {
      int FunRetValue = 0;
      int quarter;
      int RetValue = 0;
      upd_status = string.Empty;
      quarter = BLLPayrollFunctions.qtr_number(payDate);
      try
      {
        if (quarter > 4 && quarter < 1)
        {
          return FunRetValue;
        }
        else
        {
          DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
          switch (quarter)
          {
            case 1:
              {
                // values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, amount, 0, 0, 0, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.inc_qtd2 = 0;

                objDVOMasterEmployeeIncomes.inc_qtd3 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd4 = 0;
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            case 2:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, amount, 0, 0, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd2 = Convert.ToDecimal(amount);

                objDVOMasterEmployeeIncomes.inc_qtd3 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd4 = 0;
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            case 3:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, 0, amount, 0, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd2 = 0;

                objDVOMasterEmployeeIncomes.inc_qtd3 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.inc_qtd4 = 0;
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            default:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, 0, 0, amount, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd2 = 0;

                objDVOMasterEmployeeIncomes.inc_qtd3 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd4 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
          }
        }
        return FunRetValue;
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
    }

    private static int updt_emp_ded(ref object objTransection, string ded_code, string acct_no, string dept, int line_number, decimal amount, string empl_code, DateTime payDate, DateTime eop_date, out string upd_status)
    {
      upd_status = string.Empty;
      int FunRetValue = 0;
      int quarter;
      int RetValue = 0;
      quarter = BLLPayrollFunctions.qtr_number(payDate);
      try
      {
        if (quarter > 4 && quarter < 1)
        {
          upd_status = "**** Warning: unable to update employee record with new code.";
          return FunRetValue;
        }
        else
        {
          DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
          switch (quarter)
          {
            case 1:
              {
                //values (rpt.empl_code, ded_code, line_number,0, 0, "A", acct_no, dept, amount,0 , 0,0, amount, rpt.eop_date, "", "", "", 0.0 )

                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_qtd2 = 0;

                objDVOMasterEmployeeDeductions.ded_qtd3 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd4 = 0;
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            case 2:
              {
                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd2 = Convert.ToDecimal(amount); ;

                objDVOMasterEmployeeDeductions.ded_qtd3 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd4 = 0;
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            case 3:
              {
                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd2 = 0;

                objDVOMasterEmployeeDeductions.ded_qtd3 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_qtd4 = 0;
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            default:
              {
                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd2 = 0;

                objDVOMasterEmployeeDeductions.ded_qtd3 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd4 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
          }
        }
        return FunRetValue;
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
    }

    private static int updt_emp_obl(ref object objTransection, string obl_code, string acct_no, string dept, int line_number, decimal amount, string empl_code, DateTime payDate, int bal_acct_no, string bal_dept, out string upd_status)
    {
      upd_status = string.Empty;
      int FunRetValue = 0;
      int quarter;
      int RetValue = 0;
      quarter = BLLPayrollFunctions.qtr_number(payDate);
      try
      {
        if (quarter > 4 && quarter < 1)
        {
          upd_status = "**** Warning: unable to update employee record with new code.";
          return FunRetValue;
        }
        else
        {
          DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
          switch (quarter)
          {
            case 1:
              {
                //values (rpt.empl_code, obl_code, line_number, 0, 0,acct_no, dept, bal_acct_no, bal_dept, amount, 0, 0,0, amount)
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = Convert.ToDecimal(amount);

                objDVOMasterEmployeeObligations.obl_qtd2 = 0;
                objDVOMasterEmployeeObligations.obl_qtd3 = 0;
                objDVOMasterEmployeeObligations.obl_qtd4 = 0;
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            case 2:
              {
                //values (rpt.empl_code, obl_code, line_number, 0, 0,acct_no, dept, bal_acct_no, bal_dept, 0, amount, 0,0, amount)
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = 0;

                objDVOMasterEmployeeObligations.obl_qtd2 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.obl_qtd3 = 0;
                objDVOMasterEmployeeObligations.obl_qtd4 = 0;
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            case 3:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, 0, amount, 0, amount, "", "")
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = 0;

                objDVOMasterEmployeeObligations.obl_qtd2 = 0;
                objDVOMasterEmployeeObligations.obl_qtd3 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.obl_qtd4 = 0;
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            default:
              {
                // values (rpt.empl_code, obl_code, line_number, 0, 0,acct_no, dept, bal_acct_no, bal_dept, 0, 0, 0,amount, amount)
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = 0;

                objDVOMasterEmployeeObligations.obl_qtd2 = 0;
                objDVOMasterEmployeeObligations.obl_qtd3 = 0;
                objDVOMasterEmployeeObligations.obl_qtd4 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
          }
        }
        return FunRetValue;
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
    }

    private static bool vac_time_accrue(ref object objTransection, string empl_code, decimal Total_hour, decimal vac_allowed, DateTime eop_date, out int status)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      try
      {
        status = 0;
        decimal add_vac_time = 0.0M;
        decimal vac_incr_amt = 0.0M;
        int factor = 1;

        DataSet ds_a_acc_record = null;
        DataSet dsaccr_emp = BLLPyPostException.FIND_ACCRUAL_EMP_RECORD(empl_code);

        if (dsaccr_emp.Tables[0].Rows.Count > 0)
        {
          if (dsaccr_emp.Tables[0].Rows[0][0] != DBNull.Value)
          {
            ds_a_acc_record = BLLPyPostException.FIND_ACCRUAL_TIMED_DETAILS(dsaccr_emp.Tables[0].Rows[0][0].ToString().Trim());
            if (ds_a_acc_record.Tables[0].Rows.Count > 0)
            {
            }
            else
            {
              return false;
            }
          }
          else
          {
            return false;
            //no accrual code is setup, no point in processing
            //return
            // break;
          }
        }
        //default vac time accrual counter to one
        if (dsaccr_emp.Tables[0].Rows[0][1] == DBNull.Value)
        {
          dsaccr_emp.Tables[0].Rows[0][1] = 1;
        }

        //default vacation time accrual lapse indicator to zero
        if (ds_a_acc_record.Tables[0].Rows[0][3] == DBNull.Value)
        {
          ds_a_acc_record.Tables[0].Rows[0][3] = 0;
        }

        if (ds_a_acc_record.Tables[0].Rows[0][0] == DBNull.Value)
        {
          //Compairing method 
          if (ds_a_acc_record.Tables[0].Rows[0][0].ToString().Trim() == "H")
          {
            vac_incr_amt = Total_hour;
          }
          else
          {
            vac_incr_amt = 1;
          }
        }
        BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
        string add_vac_time1 = objBLLPayrollFunctions.Al_Round("d", Convert.ToDecimal(ds_a_acc_record.Tables[0].Rows[0][1]));
        add_vac_time = Convert.ToDecimal(add_vac_time1);

        //make sure enough pay periods have elapsed before accrual begins
        //if accr_emp.vac_lapse_date is not null or a_lapse = 0
        if (dsaccr_emp.Tables[0].Rows[0][2] != DBNull.Value || Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][3]) == 0)
        {
          //if (accr_emp.vac_accr_ctr + vac_incr_amt) < a_freq
          if ((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1].ToString().Trim()) + vac_incr_amt) < Convert.ToDecimal(ds_a_acc_record.Tables[0].Rows[0][2]))
          {
            //Update_Vac_Acc_Counter
            int i = BLLPyPostException.Update_VAC_Ctr(ref objTransection, ref objDVOExceptionReports, empl_code, Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1].ToString().Trim()), vac_incr_amt);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            //a_diff= (accr_emp.vac_accr_ctr + vac_incr_amt) - a_freq
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + vac_incr_amt) - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]));
            while (a_diff > Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]))
            {
              a_diff = a_diff - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]);
              factor = factor + 1;
            }
            decimal new_vac_allowed = vac_allowed + (add_vac_time * factor);
            int i = BLLPyPostException.UPDATE_VAC_Allowed_VAC_Counter(ref objTransection, ref objDVOExceptionReports, empl_code, new_vac_allowed, a_diff);
            if (i == 0)
            {
              status = 1;
              return false;
            }

            //The lapse was 0 so employee should accrue. Date must be set
            if (dsaccr_emp.Tables[0].Rows[0][2] == DBNull.Value)
            {
              i = BLLPyPostException.UPDATE_VAC_Lapse(ref objTransection, ref objDVOExceptionReports, empl_code, eop_date);
              if (i == 0)
              {
                status = 1;
                return false;
              }
            }
          }
        }
        else
        {
          //if lapse has not been reached yet just increment counter,
          //otherwise set past lapse flag and reset counter
          if ((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + vac_incr_amt) < Convert.ToDecimal(ds_a_acc_record.Tables[0].Rows[0][3]))
          {
            decimal new_vac_accr_ctr = 0.0M;
            new_vac_accr_ctr = Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + vac_incr_amt;
            int i = BLLPyPostException.UPDATE_VAC_Lapse_Control_1(ref objTransection, ref objDVOExceptionReports, empl_code, new_vac_accr_ctr, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            //a_diff =(accr_emp.vac_accr_ctr + vac_incr_amt) - a_lapse
            // initialize multiplier for accrual increment
            factor = 1;
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + vac_incr_amt) - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][3]));
            // cycle through to eliminate backlog if any exists
            while (a_diff > Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]))
            {
              a_diff = a_diff - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]);
              factor = factor + 1;
            }
            int i = BLLPyPostException.UPDATE_VAC_Lapse_Control(ref objTransection, ref objDVOExceptionReports, empl_code, a_diff, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private static bool sick_time_accrue(ref object objTransection, string empl_code, decimal Total_hour, decimal Sick_allowed, DateTime eop_date, out int status)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      try
      {
        status = 0;
        decimal add_sick_time = 0.0M;
        decimal sick_incr_amt = 0.0M;
        int factor = 1;

        DataSet ds_a_acc_record = null;
        DataSet dsaccr_emp = BLLPyPostException.FIND_ACCRUAL_SICK_EMP_RECORD(empl_code);

        if (dsaccr_emp.Tables[0].Rows.Count > 0)
        {
          if (dsaccr_emp.Tables[0].Rows[0][0] != DBNull.Value)
          {
            ds_a_acc_record = BLLPyPostException.FIND_ACCRUAL_TIMED_DETAILS(dsaccr_emp.Tables[0].Rows[0][0].ToString().Trim());
            if (ds_a_acc_record.Tables[0].Rows.Count > 0)
            {
            }
            else
            {
              return false;
            }
          }
          else
          {
            return false;
            //no accrual code is setup, no point in processing
            //return
            // break;
          }
        }
        // default sick time accrual counter to one
        if (dsaccr_emp.Tables[0].Rows[0][1] == DBNull.Value)
        {
          //sick_accr_ctr=1
          dsaccr_emp.Tables[0].Rows[0][1] = 1;
        }

        //default sick time accrual lapse indicator to zero
        if (ds_a_acc_record.Tables[0].Rows[0][3] == DBNull.Value)
        {
          // a_lapse = 0
          ds_a_acc_record.Tables[0].Rows[0][3] = 0;
        }

        if (ds_a_acc_record.Tables[0].Rows[0][0] == DBNull.Value)
        {
          //Compairing method 
          if (ds_a_acc_record.Tables[0].Rows[0][0].ToString().Trim() == "H")
          {
            sick_incr_amt = Total_hour;
          }
          else
          {
            sick_incr_amt = 1;
          }
        }
        BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
        string add_sick_time1 = objBLLPayrollFunctions.Al_Round("d", Convert.ToDecimal(ds_a_acc_record.Tables[0].Rows[0][1]));
        add_sick_time = Convert.ToDecimal(add_sick_time1);

        //make sure enough time has elapsed before accrual begins
        // if accr_emp.sick_lapse_date is not null or a_lapse = 0
        if (dsaccr_emp.Tables[0].Rows[0][2] != DBNull.Value || Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][3]) == 0)
        {
          //check that this is a pay period in which time gets accrued
          //if (accr_emp.sick_accr_ctr + sick_incr_amt) < a_freq
          if ((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + sick_incr_amt) < Convert.ToDecimal(ds_a_acc_record.Tables[0].Rows[0][2]))
          {
            //UPDATE_SICK_Counter
            int i = BLLPyPostException.UPDATE_SICK_Ctr(ref objTransection, ref objDVOExceptionReports, empl_code, Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]), sick_incr_amt);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            // initialize multiplier for accrual increment,  factor 
            //a_diff= (accr_emp.sick_accr_ctr + sick_incr_amt) - a_freq
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + sick_incr_amt) - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]));
            //while a_diff > a_freq
            while (a_diff > Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]))
            {
              a_diff = a_diff - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]);
              factor = factor + 1;
            }
            decimal new_Sick_allowed = Sick_allowed + (add_sick_time * factor);
            int i = BLLPyPostException.UPDATE_Sick_Allowed_Sick_Counter(ref objTransection, ref objDVOExceptionReports, empl_code, new_Sick_allowed, a_diff);
            if (i == 0)
            {
              status = 1;
              return false;
            }

            //The lapse was 0 so employee should accrue. Date must be set
            if (dsaccr_emp.Tables[0].Rows[0][2] == DBNull.Value)
            {
              i = BLLPyPostException.UPDATE_SICK_Lapse_DATE(ref objTransection, ref objDVOExceptionReports, empl_code, eop_date);
              if (i == 0)
              {
                status = 1;
                return false;
              }
            }
          }
        }
        else
        {
          //if lapse has not been reached yet just increment counter,
          //otherwise set past lapse flag and reset counter
          //if accr_emp.sick_accr_ctr < a_lapse
          if (Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) < Convert.ToDecimal(ds_a_acc_record.Tables[0].Rows[0][3]))
          {
            decimal new_sic_accr_ctr = 0.0M;
            new_sic_accr_ctr = Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + sick_incr_amt;
            int i = BLLPyPostException.UPDATE_Control_Lapse1_Without_Date(ref objTransection, ref objDVOExceptionReports, empl_code, new_sic_accr_ctr, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            //a_diff =(accr_emp.vac_accr_ctr + vac_incr_amt) - a_lapse
            // initialize multiplier for accrual increment
            factor = 1;
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dsaccr_emp.Tables[0].Rows[0][1]) + sick_incr_amt) - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][3]));
            // cycle through to eliminate backlog if any exists
            //while a_diff > a_freq
            while (a_diff > Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]))
            {
              a_diff = a_diff - Convert.ToInt32(ds_a_acc_record.Tables[0].Rows[0][2]);
              factor = factor + 1;
            }
            int i = BLLPyPostException.UPDATE_Sick_Control_Lapse_DATE(ref objTransection, ref objDVOExceptionReports, empl_code, a_diff, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;

    }

    public static Boolean py_last(out int py_status, out string py_description, decimal py_c_accum, decimal py_i_accum, decimal py_d_accum, decimal py_ox_accum, decimal py_ol_accum)
    {
      bool py_last_status = true;
      py_status = 0;
      py_description = string.Empty;
      if (py_installed == "N")
      {
        py_status = 1;
        py_description = "Payroll Isn't Installed";
        py_last_status = true;
      }

      //make sure balancing accumulations match
      if ((py_c_accum + py_d_accum != py_i_accum) || (py_ox_accum != py_ol_accum))
      {
        py_status = 10;
        py_description = "Document Doesn't Balance";
        py_last_status = false;
      }
      return py_last_status;
    }

    private static Boolean py_delete(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, int docNumber)
    {
      // this function deletes a payroll document
      // Changing the status of ok_to_post
      int i = BLLPyPostException.UPDATE_OK_TO_POST_STATUS(ref objTransection, ref objDVOExceptionReports, "P", docNumber);
      if (i == 1)
        return true;
      else
        return false;
    }


    #region check nss posting details
    //        #  This routine checks to make to see if a  ref_code + ded_code combination
    //#  exists for a NSS account.
    //#  It also verifies that the amount trying to post to the NSS account is
    //#  acceptable
    //#  You should call this procedure BEFORE you actually post the deduction
    //#  code.  In case the amount gets changed.
    //#  Difference between this function and the check_nss function, this one
    //#  just verifies that all the data is OK with the criteria.
    //#
    //#  Data elements passed:
    //#    ref_code char(6),       employee code
    //#    amount like stgactvd.amount,   signed amount
    //#                            (+increases, -decreases balances)
    //#    act_code char(6),       activity code
    //#                            (income, deduction, obligation code)
    //#    line_no                 line_no for the ref_code
    //#
    //# status and there descriptions
    //# 0 - Successful - amount = amount posted
    //# 1 - NSS is not installed, nothing to post - amount = 0
    //# 2 - NSS is installed, but no account types found, nothing to post - amount = 0
    //# 3 - NSS is installed, but no account found, nothing to post - amount = 0
    //# 4 - NSS is installed, more than one account found, nothing to post -
    //#     amount = 0
    //# 5 - trying to add to a repaid/inactive account, nothing to post - amount = 0
    //# 6 - amount submitted to be posted is greater than allowed, post only the
    //#     allowed amount only - amount = amount posted


    //# What this process does:
    //#
    //# - check to see if the NSS is installed.  If not, just return
    //# - try to find a NSS account with the employee/deduction code combination
    //# - once an account is found, make sure there is enough room left in the
    //#   account for the contribution
    //# - no posting is done
    #endregion check nss posting details

    //private string curs_check_nss_prep; // char(1),   # defined the cursors yet?
    //private string curs_post_nss_prep;  // char(1),   # defined the cursors yet?


    private static bool check_nss(string ref_code, decimal amount, string act_code, int line_no, out int nss_status, out decimal amount_to_post)
    {
      //assigning initial value to out parameter
      nss_status = 0;
      amount_to_post = 0.0M;
      Boolean nss_installed = false;
      int tmp_count = 0;
      decimal allowed_amount = 0.0M;
      int err_no = 0;
      //first time... fill nss_installed
      string TabName = Get_nss_check_ControlTable();
      if (TabName != "")
      {
        nss_installed = true;
      }
      else
      {
        nss_installed = false;
      }
      if (nss_installed)
      {
        tmp_count = Get_nss_check_ControlTable_Count();
        if (tmp_count == 0)
        {
          //NSS is installed, but no account types found, nothing to post - amount = 0
          nss_status = 2;
          amount_to_post = 0;
          return false;
        }
      }

      else
      {
        //NSS is not installed, nothing to post - amount = 0
        nss_status = 1;
        amount_to_post = 0;
        return false;
      }
      // now let's make sure we can find an account for
      // this employee/deduction
      DataSet dsInfoCurs = Get_nss_check_Information(ref_code);
      if (dsInfoCurs.Tables[0].Rows.Count > 0)
      {
        tmp_count = dsInfoCurs.Tables[0].Rows.Count;
      }
      if (tmp_count == 0)
      {
        // no account founds
        // this will occur 'most' of the time
        nss_status = 3;
        amount_to_post = 0;
        return false;

      }
      else if (tmp_count > 1)
      {
        //more than one account found ('should' never happen)
        nss_status = 4;
        amount_to_post = 0;
        return false;

      }
      else if (tmp_count == 1 && dsInfoCurs.Tables[0].Rows[0][0].ToString().Trim() != "ACTIVE")//nss_status
      {
        // trying to add to a non-active account, bad
        nss_status = 5;
        amount_to_post = 0;
        return false;
      }
      else
      {
        //lets make sure that we have enough room left in the nss
        //account for this contribution
        if (dsInfoCurs.Tables[0].Rows[0][2] == DBNull.Value)//current_bal
        {
          //Assigning the value to Contribution_Total
          dsInfoCurs.Tables[0].Rows[0][2] = 0;//current_bal 
        }
        if (dsInfoCurs.Tables[0].Rows[0][1] == DBNull.Value)//monthly_contrib        
        {
          //Assigning the value to monthly_contrib   
          dsInfoCurs.Tables[0].Rows[0][1] = 0;//monthly_contrib   12     
        }
        //dsInfoCurs.Tables[0].Rows[0][3] -  no_of_payments 

        //allowedamount =(contribution_amount*duration)-contribution_Total
        allowed_amount = (Convert.ToDecimal(dsInfoCurs.Tables[0].Rows[0][1]) * Convert.ToInt32(dsInfoCurs.Tables[0].Rows[0][3])) - Convert.ToDecimal(dsInfoCurs.Tables[0].Rows[0][2]);
        if (allowed_amount < amount)
        {
          //amount submitted to be posted is greater than allowed, post only the
          //allowed amount only - amount = amount posted
          nss_status = 6;
          amount_to_post = allowed_amount;
          return true;
        }
        else
        {
          nss_status = 0;
          amount_to_post = amount;
          return true;
        }
      }

    }


    private static bool nss_check_post(ref object objTransection, string post_or_check, string orig_journal, int ref_doc_no, DateTime doc_date, string ref_code, decimal ded_amount, string doc_desc, string act_code, int line_no, out int status, out decimal nss_amount, out string err_desc)
    {
      //assigning initial value to out parameter
      decimal post_nss_amount = 0;
      int seq_no = 0;
      err_desc = string.Empty;
      status = 0;
      Boolean past_nss = true;
      nss_amount = 0;
      StringBuilder date = new StringBuilder();
      //first time... fill nss_installed
      string TabName = Get_nss_check_ControlTable();
      if (TabName != string.Empty)
      {
        // now let's make sure we can find an account for
        // this employee/deduction
        //Get Employee Information .........from nss_clients table
        DataSet dsInfoCurs = Get_nss_check_Information(ref_code);
        if (dsInfoCurs.Tables[0].Rows.Count > 0)
        {
          if (post_or_check == "POST")
          {
            #region Get Date having format "ddmmyy"...
            if (post_or_check == "POST")
            {
              int day = DVOApplicationUserInfo.CurrentDate.Day;
              if (day.ToString().Trim().Length == 1)
                date.Append("0" + day.ToString().Trim());
              else
                date.Append(day.ToString().Trim());
              int month = DVOApplicationUserInfo.CurrentDate.Month;
              if (month.ToString().Trim().Length == 1)
                date.Append("0" + month.ToString().Trim());
              else
                date.Append(month.ToString().Trim());
              string year = DVOApplicationUserInfo.CurrentDate.Year.ToString().Trim().Substring(2, 2);
              date.Append(year.ToString().Trim());
            }
            #endregion
            foreach (DataRow dremp in dsInfoCurs.Tables[0].Rows)
            {
              DVONSSDetail nssclient = new DVONSSDetail();
              nssclient.account_no = dremp[4].ToString().Trim();
              nssclient.contract_no = dremp[5].ToString().Trim();
              nssclient.current_bal = (dremp[2] != DBNull.Value ? Convert.ToDecimal(dremp[2]) : 0);
              nssclient.monthly_contrib = (dremp[1] != DBNull.Value ? Convert.ToDecimal(dremp[1]) : 0);
              nssclient.no_of_payments = (dremp[3] != DBNull.Value ? Convert.ToInt32(dremp[3]) : 0);
              nssclient.nss_status = dremp[0].ToString().Trim();
              nssclient.last_period = (dremp[6] != DBNull.Value ? Convert.ToInt32(dremp[6]) : 0);
              int for_period = 0;
              if (nssclient.last_period == 12)
                for_period = 0;
              else
                for_period = nssclient.last_period;

              //Get no of payment due for this account..
              int due_no = get_no_of_payment_delay(ref nssclient);

              for (int j = 0; j <= due_no; j++)
              {
                post_nss_amount = nssclient.monthly_contrib;
                for_period = for_period + 1;
                if (ded_amount >= post_nss_amount)
                {
                  past_nss = true;
                }
                else if (ded_amount == 0)
                {   //Here nss deduction amount is less than amount need to be post.
                    //means there are some nss accounts need to pay.
                  past_nss = false;
                  err_desc = "";
                }
                else
                {
                  // ded_amount is not zero but it is sufficient to pay for any period.
                  // remaining ded_amount should go into any other account like suspense account.
                  past_nss = false;
                  err_desc = "";
                }
                if (past_nss)
                {
                  #region Insert into nss_hdr,nss_dtl tables.........

                  // Insert data into nss_hdr table...........
                  DVONssTransectionDetails objDVONssTrnDetailsIns = new DVONssTransectionDetails();
                  object[] parameters = new object[12];
                  parameters[0] = "PY" + Convert.ToDateTime(date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);  //Voucher_no
                  parameters[1] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ; //payment date
                  parameters[2] = DVOApplicationUserInfo.LoginId;
                  parameters[3] = "N";
                  parameters[4] = "";
                  parameters[5] = 0;
                  //Parameters used For Only SQL Server
                  parameters[6] = objDVONssTrnDetailsIns.InsertMachineInfo;
                  parameters[7] = objDVONssTrnDetailsIns.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameters[8] = objDVONssTrnDetailsIns.InsertBy;
                  parameters[9] = objDVONssTrnDetailsIns.UpdateMachineInfo;
                  parameters[10] = objDVONssTrnDetailsIns.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameters[11] = objDVONssTrnDetailsIns.UpdateBy;
                  DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                  DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                  DataSet Ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVONssTransection));
                  if (Ds.Tables.Count > 0)
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                      seq_no = Convert.ToInt32(Ds.Tables[0].Rows[0][0]);
                      if (seq_no <= 0)
                      {
                        err_desc = "Error has occurred while inserting into nss_hdr";
                        return false;
                      }
                    }
                  //Insert into nss_dtl .............
                  object[] parameter = new object[12];
                  parameter[0] = seq_no;
                  parameter[1] = nssclient.account_no; //Nss AccountNo
                  parameter[2] = for_period;
                  parameter[3] = "A";
                  parameter[4] = post_nss_amount;
                  parameter[5] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  //Parameters used For Only SQL Server
                  parameter[6] = objDVONssTrnDetailsIns.InsertMachineInfo;
                  parameter[7] = objDVONssTrnDetailsIns.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameter[8] = objDVONssTrnDetailsIns.InsertBy;
                  parameter[9] = objDVONssTrnDetailsIns.UpdateMachineInfo;
                  parameter[10] = objDVONssTrnDetailsIns.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameter[11] = objDVONssTrnDetailsIns.UpdateBy;
                  DataSet Dsdtl = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameter, typeof(DVONssTransectionDetails));

                  if (Dsdtl.Tables.Count > 0)
                    if (Dsdtl.Tables[0].Rows.Count > 0)
                    {
                      int i = Convert.ToInt32(Dsdtl.Tables[0].Rows[0][0]);
                      if (i != 1)
                      {
                        err_desc = "Error has occurred while inserting into nss_dtl";
                        return false;
                      }
                    }

                  #endregion

                  ded_amount = ded_amount - post_nss_amount;
                }
              }

            }
            if (ded_amount != 0)
            {
              // ded_amount submitted to be posted is greater than allowed,then have to post.
              // remaining ded_amount should go into any other account like suspense account.
            }
          }
        }
        else
        {
          err_desc = "This Employee does not have account for nss deduction.";
          return false;
        }
      }
      else
      {
        err_desc = "Error: Nss  not installed";
        return false;
      }
      nss_amount = ded_amount;
      return true;
    }

    public static int get_no_of_payment_delay(ref DVONSSDetail objNSSDetail)
    {

      DataSet ds = ReportingUtilities.GetNSSDetail(ref objNSSDetail);
      DateTime DueDate = DVOApplicationUserInfo.CurrentDate;
      objNSSDetail.contract_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["p_contract_date"]);
      DueDate = objNSSDetail.contract_date;
      int defaulter = 0;
      int tranno = 1;
      int Monthdue = 0;
      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow dr = ds.Tables[0].Rows[i];
        if (DueDate.Year == objNSSDetail.contract_date.Year)
        {
          Monthdue = DueDate.Month - objNSSDetail.contract_date.Month + 1;
        }
        else
        {
          Monthdue = 12 * (DueDate.Year - objNSSDetail.contract_date.Year) + (DueDate.Month - objNSSDetail.contract_date.Month) + 1;
        }
        if ((Monthdue == tranno))
        {
          if (Convert.ToDateTime(dr["p_payment_date"]).Year == DueDate.Year)
          {
            if (Convert.ToDateTime(dr["p_payment_date"]).Month > DueDate.Month)
            {
              defaulter = defaulter + 1;

              DueDate = DueDate.AddMonths(1);
              tranno = tranno + 1;
            }
            else
            {
              tranno = tranno + 1;
              DueDate = DueDate.AddMonths(1);
            }

          }
          else
          {
            if (Convert.ToDateTime(dr["p_payment_date"]).Year > DueDate.Year)
            {
              if (Convert.ToDateTime(dr["p_payment_date"]).Month != DueDate.Month)
              {
                defaulter = defaulter + 1;

                DueDate = DueDate.AddMonths(1);
                tranno = tranno + 1;
              }
              else
              {
                tranno = tranno + 1;
                DueDate = DueDate.AddMonths(1);
              }
            }
            else
            {

              tranno = tranno + 1;
              DueDate = DueDate.AddMonths(1);

            }
          }
        }
      }
      return defaulter;
    }


    /// <summary>
    ///  # used to determine the payroll department for each employee in the DataSet
    ///  # Select all the data we need into the Dataset
    ///  We need to do this so that we can build the flexDept for
    /// each employee using flexseg_load(...) and then order by this value.
    /// </summary>
    /// <param name="objSCDVOPayrollProcess_PayEmployee"></param>
    /// <param name="objSCDVOMasterEmployee"></param>
    /// <returns></returns>
    private static DataSet ml_getCount(ref DVOPayrollProcess_PayEmployee objSCDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee objSCDVOMasterEmployee, string CHECK_POST)
    {
      DataSet dsEmpPay = null;
      try
      {
        object[] parameter = new object[11];
        //Get Search Criteria in Styemplr
        parameter[0] = objSCDVOMasterEmployee.FirstName;
        parameter[1] = objSCDVOMasterEmployee.LastName;
        parameter[2] = objSCDVOMasterEmployee.PayPeriod;
        parameter[3] = objSCDVOMasterEmployee.TypeCode;
        parameter[4] = objSCDVOMasterEmployee.JobCode;
        parameter[5] = objSCDVOMasterEmployee.JobTitle;

        //Get Search Criteria in Process_PayEmployee
        parameter[6] = objSCDVOPayrollProcess_PayEmployee.EmplCode;
        parameter[7] = objSCDVOPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameter[8] = objSCDVOPayrollProcess_PayEmployee.Cash_acct_no;
        //This is last_pay from styemplr
        parameter[9] = objSCDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameter[10] = CHECK_POST;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        dsEmpPay = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return dsEmpPay;
    }

    /// <summary>
    /// used for direct deposit
    /// Get Cash Account Number And Amount
    ///  # determine if the employee uses direct deposit.  
    ///  We cannot use the flag Process_PayEmployee.deposit because o_dposit
    ///  sets it to "N" after creating the direct deposit entries.

    /// </summary>
    /// <param name="PayDocNumber"></param>
    /// <returns>Data set with two attributes</returns>
    private static DataSet GetCashAccountAmount(int PayDocNumber_stypddrd, string Empl_Code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      //Get Search Criteria in  stypddrd
      parameter[0] = PayDocNumber_stypddrd;
      parameter[1] = Empl_Code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCashAccountN0 = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_CSH_ACCOUNT_AMOUNT);
      return dsCashAccountN0;

    }

    /// <summary>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions 
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>Data set with one attributes</returns>
    //private static DataSet GetDeductionSumAmount(string Ded_Code_MasterEmployeeDeductions, string soc_sec_num_styemplr)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = Ded_Code_MasterEmployeeDeductions;
    //    parameter[1] = soc_sec_num_styemplr;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsDedYtd = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_YTD);
    //    return dsDedYtd;

    //}
    /// <summary>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions 
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>decimal value</returns>
    private static decimal GetDeductionSumAmount(string Ded_Code_MasterEmployeeDeductions, string soc_sec_num_styemplr)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = Ded_Code_MasterEmployeeDeductions;
      parameter[1] = soc_sec_num_styemplr;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_YTD);
      if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0)//dsDedYtd
        return Convert.ToDecimal(obj);
      return 0;

    }


    /// <Get Deduction SumAmount from MasterEmployeeIncomes>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions  on the basis of employee code
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="empl_code_MasterEmployeeDeductions"></param>
    /// <returns>Data set with one attributes</returns>
    //private static DataSet GetDeductionSumAmountOne(string Ded_Code_MasterEmployeeDeductions, string empl_code_MasterEmployeeDeductions)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = Ded_Code_MasterEmployeeDeductions;
    //    parameter[1] = empl_code_MasterEmployeeDeductions;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsDedYtd1 = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_YTD1);
    //    return dsDedYtd1;

    //}
    /// <Get Deduction SumAmount from MasterEmployeeIncomes>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions  on the basis of employee code
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="empl_code_MasterEmployeeDeductions"></param>
    /// <returns>decimal value</returns>
    private static decimal GetDeductionSumAmountOne(string Ded_Code_MasterEmployeeDeductions, string empl_code_MasterEmployeeDeductions)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = Ded_Code_MasterEmployeeDeductions;
      parameter[1] = empl_code_MasterEmployeeDeductions;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_YTD1);
      if (obj != null)//dsDedYtd1
        if (obj.ToString().Trim() != string.Empty)
          return Convert.ToDecimal(obj);
      return 0;

    }

    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>Data set with one attributes</returns>
    //private static DataSet GetObligationSumAmount(string obl_code_MasterEmployeeObligations, string soc_sec_num_styemplr)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = obl_code_MasterEmployeeObligations;
    //    parameter[1] = soc_sec_num_styemplr;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsOblYtd = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBLYTD);
    //    return dsOblYtd;

    //}
    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>decimal value</returns>
    private static decimal GetObligationSumAmount(string obl_code_MasterEmployeeObligations, string soc_sec_num_styemplr)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = obl_code_MasterEmployeeObligations;
      parameter[1] = soc_sec_num_styemplr;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBLYTD);
      if (obj != null)//dsOblYtd
        return Convert.ToDecimal(obj);
      return 0;

    }

    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="empl_code_MasterEmployeeObligations"></param>
    /// <returns> Data set with one attributes</returns>
    //private static DataSet GetObligationSumAmountOne(string obl_code_MasterEmployeeObligations, string empl_code_MasterEmployeeObligations)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = obl_code_MasterEmployeeObligations;
    //    parameter[1] = empl_code_MasterEmployeeObligations;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsOblYtd1 = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBLYTD1);
    //    return dsOblYtd1;

    //}

    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="empl_code_MasterEmployeeObligations"></param>
    /// <returns> decimal value</returns>
    private static decimal GetObligationSumAmountOne(string obl_code_MasterEmployeeObligations, string empl_code_MasterEmployeeObligations)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = obl_code_MasterEmployeeObligations;
      parameter[1] = empl_code_MasterEmployeeObligations;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBLYTD1);
      if (obj != null)//dsOblYtd1
        if (obj.ToString().Trim() != string.Empty)
          return Convert.ToDecimal(obj);
      return 0;
    }

    /// <summary>
    /// To get the sum of sick and vacation
    /// </summary>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>Data set with Two attributes</returns>
    private static DataSet GetSumOfSickAndVacation(string soc_sec_num_styemplr)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = soc_sec_num_styemplr;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSickAndVac = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_SICK_VACATION);
      return dsSickAndVac;

    }

    /// <summary>
    /// To get the sum of basic amount
    /// </summary>
    /// <param name="DocNumber_stypayid"></param>
    /// <returns></returns>
    private static decimal GetSumOfBasicAmount(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsBasicSum = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_BASIC_AMOUNT);
      //return dsBasicSum;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_BASIC_AMOUNT);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }
    }

    /// <summary>
    /// To get the sum of taxable statement 
    /// </summary>
    /// <param name="DocNumber_stypayid"></param>
    /// <returns></returns>
    private static decimal GetSumOfTaxableStatement(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsTaxStmt = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_TAXABLE_STMT);
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_TAXABLE_STMT);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;

      }
      else
      {
        return 0.0M;
      }

    }

    /// <summary>
    /// To get the sum of non taxable statement 
    /// </summary>
    /// <param name="DocNumber_stypayid"></param>
    /// <returns></returns>
    private static decimal GetSumOfNonTaxableStatement(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      // DataSet dsNonTaxStmt = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_NON_TAXABLE_STMT);
      // return dsNonTaxStmt;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_NON_TAXABLE_STMT);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }
    }


    #region Get the value to Precess before doc number

    private static decimal GetSumDeductionTotal(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumDedTotal = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_TOTAL);
      //return dssumDedTotal;

      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_TOTAL);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }

    private static decimal GetSumDeductionTotalSSD(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      // DataSet dssumDedTotalSSD = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSD);
      // return dssumDedTotalSSD;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSD);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }

    private static decimal GetSumDeductionTotalSSL(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumDedTotalSSL = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSl);
      //return dssumDedTotalSSL;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSl);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }
    }

    private static decimal GetSumObligationTotalSSD(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumOblTotalSSD = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSD);
      //return dssumOblTotalSSD;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSD);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }

    private static decimal GetSumObligationTotalSSIB(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumOblTotalSSIB = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSlB);
      //return dssumOblTotalSSIB;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSlB);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }
    private static object[] GetSumIncome(int DocNumber_stypayid, string Inc_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = DocNumber_stypayid;
      parameter[1] = Inc_code.ToString().Trim();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumOblTotalSSIB = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSlB);
      //return dssumOblTotalSSIB;
      object[] RetValue = new object[3];
      DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_ANY_Income);
      if (ds.Tables.Count > 0)
      {
        if (ds.Tables[0].Rows.Count > 0)
        {
          RetValue[0] = ds.Tables[0].Rows[0][0];
          RetValue[1] = ds.Tables[0].Rows[0][1];
          RetValue[2] = ds.Tables[0].Rows[0][2];
        }
        else
        {
          RetValue[0] = 0.0M;
          RetValue[1] = 0.0M;
          RetValue[2] = 0.0M;
        }
      }

      return RetValue;
    }

    private static string GetBonusCheck(int DocNumber_stypayid, string empl_number_styemplr)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = empl_number_styemplr;
      parameter[1] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsbonuschk = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_BONUS_CHECK);
      // return dsbonuschk;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_BONUS_CHECK);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return RetValue[0].ToString().Trim();
        else return "";
      }
      else
      {
        return "";
      }
    }
    #endregion Get the value to Precess before doc number

    #region Get the value to process chk_post()

    public static int GetCount1(string orig_journal, int doc_no)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = orig_journal;
      parameter[1] = doc_no;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNonTaxStmt = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_COUNT_1);
      if (dsNonTaxStmt.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsNonTaxStmt.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    public static int Insert1_In_stytranr(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string orig_journal, int doc_no, string check_no, DateTime pay_date, DateTime eop_date)
    {
      //int success = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[11];
        parameters[0] = orig_journal;
        parameters[1] = doc_no;
        parameters[2] = check_no;
        parameters[3] = pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;

        //Parameters used For Only SQL Server
        parameters[5] = objDVOExceptionReports.InsertMachineInfo;
        parameters[6] = objDVOExceptionReports.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.InsertBy;
        parameters[8] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[9] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[10] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.INSERT_INS_1, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int Insert2_In_styactvd(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string orig_journal, int doc_no, string act_code, string act_type, decimal amount, decimal number, decimal hour, decimal rate, int acct_no, string dept_code)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[16];
        parameters[0] = orig_journal;
        parameters[1] = doc_no;
        parameters[2] = act_code;
        parameters[3] = act_type;
        parameters[4] = amount;
        parameters[5] = number;
        parameters[6] = hour;
        parameters[7] = rate;
        parameters[8] = acct_no;
        parameters[9] = dept_code;

        //Parameters used For Only SQL Server
        parameters[10] = objDVOExceptionReports.InsertMachineInfo;
        parameters[11] = objDVOExceptionReports.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[12] = objDVOExceptionReports.InsertBy;
        parameters[13] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[14] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[15] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.INSERT_INS_2);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_INC1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd1, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd1;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC1);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_INC2(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd2, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd2;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;
        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC2);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_INC3(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd3, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd3;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC3);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_INC4(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd4, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd4;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC4);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_DED1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd1, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd1;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED1);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_DED2(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd2, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd2;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED2);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_DED3(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd3, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd3;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED4);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_DED4(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd4, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd4;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED4);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_OBL1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd1, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd1;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL1);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_OBL2(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd2, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd2;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL2);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_OBL3(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd3, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd3;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL3);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_OBL4(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd4, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd4;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL4);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static DataSet GetTableName()
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[0];

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTBLName = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_TABLENAME);
      return dsTBLName;
    }

    public static int UPDATE_3_MasterEmployeeIncomes(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = inc_ytd;
        parameters[1] = empl_code;
        parameters[2] = inc_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_3_MasterEmployeeIncomes);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_4_MasterEmployeeDeductions(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = ded_ytd;
        parameters[1] = empl_code;
        parameters[2] = ded_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_4_MasterEmployeeDeductions);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_5_MasterEmployeeObligations(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = obl_ytd;
        parameters[1] = empl_code;
        parameters[2] = obl_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_5_MasterEmployeeObligations);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_6_7_MasterEmployeeDeductions(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, DateTime ded_date, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = ded_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[1] = empl_code;
        parameters[2] = ded_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_6_MasterEmployeeDeductions);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    #endregion Get the value to process chk_post()

    #region Get the value to process inc_post

    private static List<DVOPayrollstypayid> GetIncomeDataForINC_Post(int DocNumber_stypayid)
    {
      List<DVOPayrollstypayid> objDVOPayrollstypayidlist = new List<DVOPayrollstypayid>();
      DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsNonTaxStmt = objDalBaseClass.GetData(ref parameter,typeof(DVOPayrollstypayid),objDVOPayrollstypayid.FIND_INCOME);
      //return dsNonTaxStmt;
      using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypayid), objDVOPayrollstypayid.FIND_INCOME))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOPayrollstypayid tempobjDVOPayrollstypayid = new DVOPayrollstypayid();
          tempobjDVOPayrollstypayid.inc_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.description_MasterIncCodes = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.amount = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0);
          tempobjDVOPayrollstypayid.add_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.number = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0);
          tempobjDVOPayrollstypayid.inc_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
          tempobjDVOPayrollstypayid.hours = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
          if (dr[7] == DBNull.Value)
            tempobjDVOPayrollstypayid.lo_inc_amt_null = true;
          tempobjDVOPayrollstypayid.lo_inc_amt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
          if (dr[8] == DBNull.Value)
            tempobjDVOPayrollstypayid.hi_inc_amt_null = true;
          tempobjDVOPayrollstypayid.hi_inc_amt = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0);
          tempobjDVOPayrollstypayid.acct_no = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
          tempobjDVOPayrollstypayid.Department = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.line_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
          objDVOPayrollstypayidlist.Add(tempobjDVOPayrollstypayid);

        }
      }
      return objDVOPayrollstypayidlist;
    }
    private static int Get_empl_count(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsEmpl_Count = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_emp_count);
      if (dsEmpl_Count.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsEmpl_Count.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }
    private static int Get_MIN_LINENUMBER(string empl_code, string Inc_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = empl_code.Trim();
      parameter[1] = Inc_code.Trim();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLine_number = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_MIN_LINENUMBER);
      if (dsLine_number.Tables[0].Rows.Count > 0)
      {
        return dsLine_number.Tables[0].Rows[0][0] != DBNull.Value ? Convert.ToInt32(dsLine_number.Tables[0].Rows[0][0]) : 0;
      }
      else
      {
        return 0;
      }

    }
    private static int Get_MAX_LINENUMBER(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLine_number = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_MAX_LINENUMBER);
      if (dsLine_number.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsLine_number.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    #endregion Get the value to process inc_post

    #region Get the value to process ded_post

    private static List<DVOPayrollstypaydd> GetIncomeDataForDED_Post(int DocNumber_stypayid)
    {
      List<DVOPayrollstypaydd> objDVOPayrollstypayddlist = new List<DVOPayrollstypaydd>();
      DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypayid), objDVOPayrollstypaydd.FIND_DEDUCTION))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOPayrollstypaydd tempobjDVOPayrollstypaydd = new DVOPayrollstypaydd();
          tempobjDVOPayrollstypaydd.ded_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.description_MasterIncCodes = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.amount = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0);
          tempobjDVOPayrollstypaydd.add_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0);
          if (dr[5] == DBNull.Value)
            tempobjDVOPayrollstypaydd.lo_ded_amt_null = true;
          tempobjDVOPayrollstypaydd.lo_ded_amt = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
          if (dr[6] == DBNull.Value)
            tempobjDVOPayrollstypaydd.hi_ded_amt_null = true;
          tempobjDVOPayrollstypaydd.hi_ded_amt = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
          tempobjDVOPayrollstypaydd.acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
          tempobjDVOPayrollstypaydd.Department = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.line_no = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
          objDVOPayrollstypayddlist.Add(tempobjDVOPayrollstypaydd);

        }
      }
      return objDVOPayrollstypayddlist;
    }

    private static int Get_empl_countdd(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsEmpl_Count = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_emp_countdd);
      if (dsEmpl_Count.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsEmpl_Count.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    private static int Get_MIN_LINENUMBERDD(string empl_code, string Inc_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = empl_code;
      parameter[1] = Inc_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLine_number = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_MIN_LINENUMBERDD);
      if (dsLine_number.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsLine_number.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    private static int Get_MAX_LINENUMBERDD(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLine_number = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_MAX_LINENUMBERDD);
      if (dsLine_number.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsLine_number.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    public static int UPDATE_DED_BALANCE(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, int RowID, decimal Pay_amount)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = RowID;
        parameters[1] = Pay_amount;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED_BALANCE);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    private static DataSet Get_DED_BALANCE(string ded_code, string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = ded_code;
      parameter[1] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsdedBalance = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_DED_BALANCE);
      return dsdedBalance;
    }

    #endregion Get the value to process ded_post

    #region Get the value to process nss_check

    private static DataSet Get_nss_check_Information(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTableName = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_CHECK_INFORMATION);
      return dsTableName;
    }

    private static string Get_nss_check_ControlTable()
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[0];

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTableName = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_CHECK_CONTROL_TABLE);
      if (dsTableName.Tables[0].Rows.Count > 0)
      {
        return dsTableName.Tables[0].Rows[0][0].ToString().Trim();
      }
      else
      {
        return "";
      }

    }

    private static int Get_nss_check_ControlTable_Count()
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[0];


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTabCount = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_CHECK_CONTROL_TABLE_COUNT);
      if (dsTabCount.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsTabCount.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    private static string get_nss_ded_code()
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] param = new object[0];
      object dsTabCount = objDalBaseClass.ExecuteScalar(ref param, (new DVOExceptionReports().GET_NSS_DED_CODE));
      if (dsTabCount != null)
        return dsTabCount.ToString().Trim();
      else
        return string.Empty;
    }

    #endregion Get the value to process nss_check

    #region Get or set value to process during nss_post

    public static int Insert1_NSS_POST(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string orig_journal, int doc_no, DateTime doc_date, string contract_no, decimal amount, int ref_doc_no, string description)
    {
      //int success = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[13];
        parameters[0] = orig_journal;
        parameters[1] = doc_no;
        parameters[2] = doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[3] = contract_no;
        parameters[4] = amount;
        parameters[5] = ref_doc_no;
        parameters[6] = description;

        //Parameters used For Only SQL Server
        parameters[7] = objDVOExceptionReports.InsertMachineInfo;
        parameters[8] = objDVOExceptionReports.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[9] = objDVOExceptionReports.InsertBy;
        parameters[10] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[11] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[12] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.INSERT_NSS_POST_INSERT1);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_NSS_POST_AcctUpd(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal Contribution_Total, int Contract_number)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = Contribution_Total;
        parameters[1] = Contract_number;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_NSS_POST_AcctUpd);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    private static int Get_Count_stytranr(string orig_journal, int doc_number)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = orig_journal;
      parameter[1] = doc_number;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCount = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_POST_Count_stytranr);
      if (dsCount.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsCount.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    #endregion get or set value to process during nss_post

    #region Get the value to process obl_post

    public static List<DVOPayrollStypayod> GetObligationDataForOBL_Post(int DocNumber_stypayod)
    {
      List<DVOPayrollStypayod> objDVOPayrollStypayodlist = new List<DVOPayrollStypayod>();
      DVOPayrollStypayod objDVOPayrollStypayod = new DVOPayrollStypayod();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayod;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollStypayod), objDVOPayrollStypayod.FIND_OBLIGATION_POST))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          //             v_obl_code,v_description,v_amount,v_obl_rate,v_acct_no,v_department,
          //v_bal_acct_no,v_bal_dept,v_line_no,v_add_code
          DVOPayrollStypayod tempobjDVOPayrollStypayod = new DVOPayrollStypayod();
          tempobjDVOPayrollStypayod.obl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.description_MasterIncCodes = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.amount = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0);
          tempobjDVOPayrollStypayod.obl_rate = (dr[3] != DBNull.Value ? Convert.ToDecimal(dr[3]) : 0);
          tempobjDVOPayrollStypayod.acct_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
          tempobjDVOPayrollStypayod.Department = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.bal_acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
          tempobjDVOPayrollStypayod.bal_dept = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.line_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
          tempobjDVOPayrollStypayod.add_code = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);

          objDVOPayrollStypayodlist.Add(tempobjDVOPayrollStypayod);
        }
      }
      return objDVOPayrollStypayodlist;
    }
    public static int Get_empl_count_obligation(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsEmpl_Count = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_emp_count_obl);
      if (dsEmpl_Count.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsEmpl_Count.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }
    public static int GET_MIN_LINENUMBER_OBL(string empl_code, string Obl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = empl_code;
      parameter[1] = Obl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLine_number = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_MIN_LINENUMBER_OBL);
      if (dsLine_number.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsLine_number.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }
    public static int Get_MAX_LINENUMBER_OBL(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsLine_number = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_MAX_LINENUMBER_OBL);
      if (dsLine_number.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsLine_number.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }
    #endregion Get the value to process obl_post

    #region Get the value to process After_doc_number

    public static int UPDATE_SICK_AND_VACATION(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sick_used, decimal sick_accum, decimal vac_accum, decimal vac_used)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = empl_code;
        parameters[1] = sick_used;
        parameters[2] = sick_accum;
        parameters[3] = vac_accum;
        parameters[4] = vac_used;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_SICK_AND_VACATION, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        {
          return 0;
        }
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
        //return 0;
      }
      return 1;
    }

    public static int UPDATE_SICK_PAY_AND_VACATION(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, DateTime eop_date, string empl_code, decimal sick_used, decimal sick_accum, decimal vac_accum, decimal vac_used)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[9];
        parameters[0] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[1] = empl_code;
        parameters[2] = sick_used;
        parameters[3] = sick_accum;
        parameters[4] = vac_accum;
        parameters[5] = vac_used;
        //Parameters used For Only SQL Server             
        parameters[6] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[7] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[8] = objDVOExceptionReports.UpdateBy;

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_SICK_PAY_AND_VACATION, true);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        {
          return 0;
        }

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
        //return 0;
      }
      return 1;
    }

    public static int UPDATE_OK_TO_POST_STATUS(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string oktopostStatus, int docNumber)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = oktopostStatus;
        parameters[1] = docNumber;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = objDVOExceptionReports.UpdateBy;



        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OK_TO_POST_STATUS);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        {
          return 0;
        }

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
        //return 0;
      }
      return 1;

    }


    //private static DataSet GET_ACT_DFLT_LIMIT(string empl_code, string Pay_code)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = empl_code;
    //    parameter[1] = Pay_code;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsAccountDeft = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.GET_ACT_DFLT_LIMIT);
    //    return dsAccountDeft;

    //}
    private static decimal GET_ACT_DFLT_LIMIT(string empl_code, string Pay_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = empl_code;
      parameter[1] = Pay_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.GET_ACT_DFLT_LIMIT);
      if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0)//dsAccountDeft
        return Convert.ToDecimal(obj);
      return 0;

    }

    //private static DataSet GET_ALLOWENCE(string empl_code)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[1];
    //    parameter[0] = empl_code;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsAllowence = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.GET_ALLOWENCE);
    //    return dsAllowence;

    //}
    private static int GET_ALLOWENCE(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.GET_ALLOWENCE);
      if (obj != null)//dsAllowence
        return Convert.ToInt32(obj);
      return 0;

    }

    #endregion Get the value to process After_doc_number

    #region get the value to process vac_time_accrue

    private static DataSet FIND_ACCRUAL_EMP_RECORD(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet accr_emp_details = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_ACCRUAL_EMP_RECORD);
      return accr_emp_details;

    }

    private static DataSet FIND_ACCRUAL_TIMED_DETAILS(string vac_accr_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = vac_accr_code.Trim();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet accr_Timed_details = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_ACCRUAL_TIMED_DETAILS);
      return accr_Timed_details;

    }

    public static int Update_VAC_Ctr(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal vac_accr_ctr, decimal vac_incr_amt)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = vac_accr_ctr;
        parameters[2] = vac_incr_amt;

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[5] = objDVOExceptionReports.UpdateBy;

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_VAC_Ctr, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Allowed_VAC_Counter(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal New_vac_allowed, int a_diff)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = New_vac_allowed;
        parameters[2] = a_diff;

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_VAC_Allowed_VAC_Counter, true);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Lapse(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = empl_code;
        parameters[1] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate;
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_VAC_Lapse, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Lapse_Control(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal vac_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = vac_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Control_Lapse, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Lapse_Control_1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal vac_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = vac_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Control_Lapse1, true);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    #endregion get the value to process vac_time_accrue

    #region get the value to process Sick_time_accrue

    private static DataSet FIND_ACCRUAL_SICK_EMP_RECORD(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet accr_emp_details = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_ACCRUAL_SICK_EMP_RECORD);
      return accr_emp_details;

    }


    public static int UPDATE_SICK_Ctr(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sic_accr_ctr, decimal sic_incr_amt)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = sic_accr_ctr;
        parameters[2] = sic_incr_amt;


        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_SICK_Ctr, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_Sick_Allowed_Sick_Counter(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal New_sic_allowed, int a_diff)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = New_sic_allowed;
        parameters[2] = a_diff;


        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Sick_Allowed_Sick_Counter, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_SICK_Lapse_DATE(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = empl_code;
        parameters[1] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOExceptionReports), objDVOExceptionReports.UPDATE_SICK_Lapse_DATE);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_Sick_Control_Lapse_DATE(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sic_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = sic_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Sick_Control_Lapse_DATE, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;

    }

    public static int UPDATE_Control_Lapse1_Without_Date(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sic_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = sic_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Control_Lapse1_Without_Date, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    #endregion get the value to process Sick_time_accrue
  }

  public class BLLPyPostExceptionNew : IDisposable
  {
    #region Globle variable Used
    DVOMasterEmployee GlobalObjDVOMasterEmployee;
    DVOPayrollProcess_PayEmployee GlobalObjDVOPayrollProcess_PayEmployee;
    List<DVOUpdatePayDefaults> objGloabalPayDefaultsListStycntrc;

    // variable used in ded_post
    Boolean dfica_flag = false;
    decimal fica_ded = 0.0M;
    Boolean dmedicare_flag = false;
    decimal medcr_ded = 0.0M;
    Boolean dftax_flag = false;
    // variable used in obl_post
    bool ofuta_flag = false;
    bool ofica_flag = false;
    decimal fica_obl = 0.0M;
    bool omedicare_flag = false;
    decimal medcr_obl = 0.0M;
    // variable used in inc_post
    decimal sick_accum = 0.0M;
    decimal vac_accum = 0.0M;
    //Variable used in py_post
    string py_installed;
    //Variable used To pass in py_post as an output
    decimal py_c_accum = 0.0M;
    decimal py_i_accum = 0.0M;
    decimal py_d_accum = 0.0M;
    decimal py_ox_accum = 0.0M;
    decimal py_ol_accum = 0.0M;
    int py_status = 0;
    string py_description = string.Empty;
    string err_msg = string.Empty;
    DataSet dsDDAmount = null;
    DataSet dsIncomes = null;
    DataSet dsDeductions = null;
    DataSet dsObligations = null;
    DataSet dsPayrollGLAccounts = null;
    DataSet dsPayrollDepartments = null;
    DataSet dsMinLineId = null;
    DataSet dsMaxLineId = null;
    DataSet dsMinLinedd = null;
    DataSet dsMaxLinedd = null;
    DataSet dsMinLineod = null;
    DataSet dsMaxLineod = null;
    DataSet dsStyaccrr = null;
    DataSet dsDeductionSumAmount = null;
    DataSet dsDeductionSumAmount1 = null;
    DataSet dsDeductionLimits = null;
    DataSet dsOblgationSumAmount = null;
    DataSet dsOblgationSumAmount1 = null;
    DataSet dsOblgationLimits = null;
    DataSet dsSickAndVacation = null;
    DataSet dsStxperdr = null;
    #endregion Globle variable Used

    public DataSet GetExceptionReports(ref DVOPayrollProcess_PayEmployee objSCDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee objSCDVOMasterEmployee, string CHECK_POST, ref DVOPYBatchProcessStybatchr pObjBatch)
    {

      #region Initialize Variables

      decimal act_accrual = 0.0M;
      decimal act_limit = 0.0M;
      decimal dflt_limit = 0.0M;
      int empl_allow = 0;
      int entry_count = 0;
      int sick_acc_tmp = 0;
      int vac_acc_tmp = 0;
      decimal d_tot_hour = 0.0M;
      int status = 0;
      int new_doc_no = 0;
      int gl_status = 0;
      bool _ok_to_post = true;
      DataSet ds = null;
      Boolean dmedicare_lmt = false;
      Boolean ofica_lmt = false;
      Boolean omedicare_lmt = false;
      Boolean ofuta_lmt = false;
      Boolean dftax_xmt = false;
      //string errMsg = string.Empty;
      Boolean Bonus_Check = false;

      // variable used in ded_post
      dfica_flag = false;
      fica_ded = 0.0M;
      dmedicare_flag = false;
      medcr_ded = 0.0M;
      dftax_flag = false;
      //variable used in obl_post
      ofuta_flag = false;
      ofica_flag = false;
      fica_obl = 0.0M;
      omedicare_flag = false;
      medcr_obl = 0.0M;
      // variable used in inc_post
      sick_accum = 0.0M;
      vac_accum = 0.0M;
      //Variable used in py_post
      //py_installed;
      //Variable used To pass in py_post as an output
      py_c_accum = 0.0M;
      py_i_accum = 0.0M;
      py_d_accum = 0.0M;
      py_ox_accum = 0.0M;
      py_ol_accum = 0.0M;
      py_status = 0;
      py_description = string.Empty;


      BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      string processName = string.Empty;
      if (CHECK_POST == "CHECK")
        processName = "Payroll Exception Report";
      else if (CHECK_POST == "EDIT")
        processName = "Print Detail Edit List";
      else if (CHECK_POST == "POST")
        processName = "Post Payroll Entries";

      DataSet FinalDs = new DataSet();
      DataTable objDataTable = new DataTable();
      DataTable objGLSumTable = new DataTable();
      //make a DataTable Object for holding GL Summary Data.....
      objGLSumTable.Columns.Add("doc_no", typeof(int));
      objGLSumTable.Columns.Add("doc_date", typeof(DateTime));
      objGLSumTable.Columns.Add("acctno", typeof(int));
      objGLSumTable.Columns.Add("amount", typeof(decimal));
      objGLSumTable.Columns.Add("debit_credit");
      objGLSumTable.Columns.Add("keyvalue");
      objGLSumTable.Columns.Add("acct_desc");
      objGLSumTable.Columns.Add("flexdept");
      //make a DataTable Object to hold Income,Deduction and Obligation details.......
      DataTable ObjDetailTable = new DataTable();
      ObjDetailTable.Columns.Add("doc_no", typeof(int));
      ObjDetailTable.Columns.Add("code_line");
      ObjDetailTable.Columns.Add("pay_code");
      ObjDetailTable.Columns.Add("pay_desc");
      ObjDetailTable.Columns.Add("account");
      ObjDetailTable.Columns.Add("amount", typeof(decimal));
      ObjDetailTable.Columns.Add("lo_hi_amt", typeof(decimal));
      ObjDetailTable.Columns.Add("lo_flag", typeof(Boolean));
      //ObjDetailTable.Columns.Add("hi_flag", typeof(Boolean));
      string Curr_Emp_Code = string.Empty;
      object objTransaction = null;
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      #endregion

      #region Declare Variables for Batch Process
      StringBuilder errorMassage = new StringBuilder();
      DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
      int recordsSearched = 0;
      int recordsProcessed = 0;
      bool IsProessIns = false;
      #endregion

      try
      {

        #region Insert Process Start Info..

        object objTrx = null;
        objProcessDtl.pybatchid = pObjBatch.pybatchid;
        objProcessDtl.processname = processName;
        objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
        BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
        IsProessIns = true;

        #endregion

        #region Get Data..
        string arrDocNo = string.Empty;
        string SearchedEmployeeList = string.Empty;
        string SearchedSocSecNo = string.Empty;
        DataSet dsAmount = null;
        ds = GetPayrollEntries(ref objSCDVOPayrollProcess_PayEmployee, ref objSCDVOMasterEmployee, CHECK_POST, out arrDocNo, out SearchedEmployeeList, out SearchedSocSecNo, out dsAmount);
        //bd
        //System.Windows.Forms.MessageBox.Show("Employee list : " + DateTime.Now.ToString());

        recordsSearched = ds.Tables[0].Rows.Count;
        objDataTable = ds.Tables[0].Clone();
        #endregion

        if (ds.Tables[0].Rows.Count > 0)
        {
          #region BeforeFirstRow
          object[] param = new object[2];
          param[0] = arrDocNo;
          param[1] = CHECK_POST;
          DataSet dsSumIncome = null;
          if (CHECK_POST == "CHECK")
          {
            dsSumIncome = objDALBaseClass.GetData(objDVOExceptionReports.GetSumIncomes(ref param));
            if (dsSumIncome.Tables.Count > 0)
            {
              dsSumIncome.Tables[0].Columns[0].ColumnName = "amount";
              dsSumIncome.Tables[0].Columns[1].ColumnName = "hours";
              dsSumIncome.Tables[0].Columns[2].ColumnName = "inc_rate";
              dsSumIncome.Tables[0].Columns[3].ColumnName = "doc_no";
              dsSumIncome.Tables[0].Columns[4].ColumnName = "inc_code";
            }
          }
          //bd
          //System.Windows.Forms.MessageBox.Show("Sum of incomes : " + DateTime.Now.ToString());

          //Get Check amount, if employee is using direct deposit...
          object[] ddparam = new object[2];
          ddparam[0] = arrDocNo;
          ddparam[1] = SearchedEmployeeList;
          dsDDAmount = objDALBaseClass.GetData(objDVOExceptionReports.GetDDAccountAmount(ref ddparam));
          //bd
          //System.Windows.Forms.MessageBox.Show("Direct deposit all data : " + DateTime.Now.ToString());
          //Get All Incomes..
          dsIncomes = objDALBaseClass.GetData((new DVOPayrollstypayid()).GetAllIncomes(ref param));
          //bd
          //System.Windows.Forms.MessageBox.Show("all incomes : " + DateTime.Now.ToString());
          //Get All Deduction..
          dsDeductions = objDALBaseClass.GetData((new DVOPayrollstypaydd()).GetAllDeductions(ref param));
          //bd
          //System.Windows.Forms.MessageBox.Show("all deductions : " + DateTime.Now.ToString());
          //Get All Obligations..
          dsObligations = objDALBaseClass.GetData((new DVOPayrollStypayod()).GetAllObligations(ref param));
          //bd
          //System.Windows.Forms.MessageBox.Show("all obligations : " + DateTime.Now.ToString());

          object[] EmpParam = new object[1];
          EmpParam[0] = SearchedEmployeeList;
          //Get All Min Line no for Inc Code..
          dsMinLineId = objDALBaseClass.GetData(objDVOExceptionReports.GetMinLineforInc(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("min line no for income : " + DateTime.Now.ToString());
          //Get All Max Line no for Incomes..
          dsMaxLineId = objDALBaseClass.GetData(objDVOExceptionReports.GetMaxLineforInc(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("max line no for income : " + DateTime.Now.ToString());

          //Get All Min Line no for Ded Code..
          dsMinLinedd = objDALBaseClass.GetData(objDVOExceptionReports.GetMinLineforDed(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("min line no for deductions : " + DateTime.Now.ToString());
          //Get All Max Line no for Ded Code..
          dsMaxLinedd = objDALBaseClass.GetData(objDVOExceptionReports.GetMaxLineforDed(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("max line no for deductions : " + DateTime.Now.ToString());

          //Get All Min Line no for Obl Code..
          dsMinLineod = objDALBaseClass.GetData(objDVOExceptionReports.GetMinLineforObl(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("min line no for obligations : " + DateTime.Now.ToString());
          //Get All Max Line no for Obl Code..
          dsMaxLineod = objDALBaseClass.GetData(objDVOExceptionReports.GetMaxLineforObl(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("max line no for obligations : " + DateTime.Now.ToString());

          //Get All account info from PayrollGLAccounts..
          LoadAllPayrollGLAccountsData();

          //Get All Payroll Departments 

          LoadAllPayrollDepartments();
          //bd
          //System.Windows.Forms.MessageBox.Show("PayrollGLAccounts : " + DateTime.Now.ToString());
          //Gat all Styaccrr...
          GET_ALL_ACCRUAL_TIMED_DETAILS();
          //bd
          //System.Windows.Forms.MessageBox.Show("accrual time detail : " + DateTime.Now.ToString());
          //Get all deduction sum amounts..
          object[] ssParam = new object[1];
          ssParam[0] = SearchedSocSecNo;
          //Get deuction amount based on SocSec no..
          dsDeductionSumAmount = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_SUM_DED_YTD(ref ssParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("sum of deductionsYTD on SSN : " + DateTime.Now.ToString());
          //Get deuction amount based on employee code...
          dsDeductionSumAmount1 = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_SUM_DED_YTD1(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("sum of deductionsYTD on empl : " + DateTime.Now.ToString());
          //Get deduction limits..
          dsDeductionLimits = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_ACT_DFLT_LIMIT(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("deduction limit : " + DateTime.Now.ToString());

          //Get oblgation amount based on SocSec no..
          dsOblgationSumAmount = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_SUM_OBLYTD(ref ssParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("sum of obligationYTD on SSN : " + DateTime.Now.ToString());
          //Get oblgation amount based on Empl_Code no..
          dsOblgationSumAmount1 = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_SUM_OBLYTD1(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("sum of obligationYTD on empl : " + DateTime.Now.ToString());
          //Get Oblgations limits..
          dsOblgationLimits = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_OBl_DFLT_LIMITS(ref EmpParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("obligation limit : " + DateTime.Now.ToString());
          //dsSickAndVacation
          dsSickAndVacation = objDALBaseClass.GetData(objDVOExceptionReports.FIND_ALL_SUM_SICK_VACATION(ref ssParam));
          //bd
          //System.Windows.Forms.MessageBox.Show("sum of sick & vacation : " + DateTime.Now.ToString());

          //Get all Stxperdr..

          //****************commented code by tara
          // dsStxperdr = objDALBaseClass.GetData(typeof(DVOExceptionReports), objDVOExceptionReports.GetAllStxperdr);
          //if (dsStxperdr.Tables.Count > 0)
          //{
          //    dsStxperdr.Tables[0].Columns[0].ColumnName = "period";
          //    dsStxperdr.Tables[0].Columns[1].ColumnName = "period_year";
          //    dsStxperdr.Tables[0].Columns[2].ColumnName = "start_date";
          //    dsStxperdr.Tables[0].Columns[3].ColumnName = "end_date";
          //}
          GetAllKeyLength();
          GetALLSegmentValInformation();


          //bd
          //System.Windows.Forms.MessageBox.Show("stxperdr : " + DateTime.Now.ToString());
          //Getting the default data from control table stycntrc
          //And assign it to a global decleared list
          objGloabalPayDefaultsListStycntrc = new List<DVOUpdatePayDefaults>();
          DVOUpdatePayDefaults obj = new DVOUpdatePayDefaults();
          objGloabalPayDefaultsListStycntrc = BLLUpdPayDefault.GetPayrollDefaults(ref obj);
          if (objGloabalPayDefaultsListStycntrc[0].post_gl == "")
          {
            objGloabalPayDefaultsListStycntrc[0].post_gl = "Y";
          }
          //bd
          //System.Windows.Forms.MessageBox.Show("stycntrc : " + DateTime.Now.ToString());

          int Post_no = 0;
          //objTransaction = objDALBaseClassHelper.GetTransactionObject();
          if (CHECK_POST == "POST")
          {
            object objTrxNull = null;
            //Post_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_post_no", ref objTrxNull);
            Post_no = BLLAccountingLiberary.Auto_Next_PYPostNo();
            if (Post_no == 0)
            {
              //Added by Sunil 
              if (objTrxNull != null)
                objDALBaseClassHelper.RollbackTransaction(ref objTrxNull);
              //************************************
              throw new Exception("Error has occurred while generating Posting Seq No.");
            }
            ds.Tables[0].Rows[0]["post_seq"] = Post_no;
          }
          else
          {
            ds.Tables[0].Rows[0]["post_seq"] = Post_no;
            new_doc_no = 0;
          }
          string CurrMonth, CurrYear;
          //CurrMonth = ReportingUtilities.GetCurr_periodstgcntrc();
          //CurrYear = ReportingUtilities.GetCurr_yearstgcntrc();
          CurrMonth = DVOApplicationUserInfo.CurPeriod;
          CurrYear = DVOApplicationUserInfo.CurYear;
          //bd
          //System.Windows.Forms.MessageBox.Show("current period, year : " + DateTime.Now.ToString());
          #endregion BeforeFirstRow

          DVOPostGLGlobal objDVOPostGLGlobal = new DVOPostGLGlobal();
          if (CHECK_POST == "POST")
            objTransaction = objDALBaseClassHelper.GetTransactionObject();

          //System.Windows.Forms.MessageBox.Show(DateTime.Now.ToString());

          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {
            DataRow dr = ds.Tables[0].Rows[i];
            objDataTable.NewRow();
            GlobalObjDVOPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            GlobalObjDVOMasterEmployee = new DVOMasterEmployee();


            if (CHECK_POST != "POST")
              objTransaction = objDALBaseClassHelper.GetTransactionObject();

            #region initialize the form Only fields
            Curr_Emp_Code = dr["empl_code"] != DBNull.Value ? dr["empl_code"].ToString().Trim() : "";
            err_msg = string.Empty;
            dr["grand_basic"] = 0;
            dr["grand_oi_tax"] = 0;
            dr["grand_oi_nontax"] = 0;
            dr["grand_ded"] = 0;
            dr["post_seq"] = Post_no;
            if (dr["sick_allowed"] == DBNull.Value)
              dr["sick_allowed"] = 0;
            if (dr["vac_allowed"] == DBNull.Value)
              dr["vac_allowed"] = 0;
            if (dr["cash_acct_no"] == DBNull.Value)
              dr["cash_acct_no"] = 0;
            if (dr["cash_amount"] == DBNull.Value)
              dr["cash_amount"] = 0;
            if (dr["terminated"] == DBNull.Value)
              dr["terminated"] = Convert.ToDateTime(null);
            if (dr["last_pay"] == DBNull.Value)
              dr["last_pay"] = Convert.ToDateTime(null);
            if (dr["sick_used"] == DBNull.Value)
              dr["sick_used"] = 0;
            if (dr["vac_used"] == DBNull.Value)
              dr["vac_used"] = 0;
            if (dr["inc_expense"] == DBNull.Value)
              dr["inc_expense"] = 0;
            if (dr["inc_net"] == DBNull.Value)
              dr["inc_net"] = 0;
            if (dr["inc_gross"] == DBNull.Value)
              dr["inc_gross"] = 0;
            if (dr["total_hours"] == DBNull.Value)
              dr["total_hours"] = 0;
            dr["ok_to_post"] = true;
            dr["CurrentPeriod"] = CurrMonth;
            dr["CurrentYear"] = CurrYear;
            if (dr["department"] == DBNull.Value)
              dr["department"] = "000";
            if (dr["emp_department"] == DBNull.Value)
              dr["emp_department"] = "000";
            #endregion

            #region Before calling function every row
            if (dr["doc_no"] != DBNull.Value)
              GlobalObjDVOPayrollProcess_PayEmployee.Doc_no = Convert.ToInt32(dr["doc_no"]);
            GlobalObjDVOPayrollProcess_PayEmployee.EmplCode = Curr_Emp_Code;
            if (dr["cash_acct_no"] != DBNull.Value)
              GlobalObjDVOPayrollProcess_PayEmployee.Cash_acct_no = Convert.ToInt32(dr["cash_acct_no"]);
            if (dr["cash_amount"] != DBNull.Value)
              GlobalObjDVOPayrollProcess_PayEmployee.cash_amount = Convert.ToDecimal(dr["cash_amount"]);
            GlobalObjDVOPayrollProcess_PayEmployee.bonus = dr["bonus"] != DBNull.Value ? Convert.ToString(dr["bonus"]).Trim() : string.Empty;

            if (ds.Tables[0].Rows.Count != i + 1)
            {
              if (ds.Tables[0].Rows[i + 1]["doc_no"] != DBNull.Value && ds.Tables[0].Rows[i + 1]["doc_no"].ToString().Trim() != string.Empty)
              {
                dr["txt_doc_no"] = Convert.ToInt32(ds.Tables[0].Rows[i + 1]["doc_no"]);
              }
            }
            else
            {
              dr["txt_doc_no"] = -100;
            }
            if (i == 0)
              objSCDVOPayrollProcess_PayEmployee.pay_date = Convert.ToDateTime(dr["pay_date"]);


            //Get keyvalue for Cash Account..........
            //string keyvalue = string.Empty;
            //int id = 0;
            //string acct_type = string.Empty;
            string acct_desc = string.Empty;
            //int acct_no = GlobalObjDVOPayrollProcess_PayEmployee.Cash_acct_no;
            //BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
            dr["cash_acct_key"] = GetAccountInfo(GlobalObjDVOPayrollProcess_PayEmployee.Cash_acct_no, out acct_desc);

            #endregion Before calling function every row

            #region  Pre before group processing
            #region Assign basic amounts..

            if (dsAmount.Tables.Count > 0)
            {
              //Get the basic income amount for this cheque
              DataRow[] draAmts = dsAmount.Tables[0].Select("const=1 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              if (draAmts.Length > 0)
                dr["basicSalary"] = draAmts[0]["amount"];
              else
                dr["basicSalary"] = 0;
              dr["grand_basic"] = Convert.ToDecimal(dr["grand_basic"]) + Convert.ToDecimal(dr["basicSalary"]);

              // Get the taxable other income for this cheque
              draAmts = dsAmount.Tables[0].Select("const=2 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              if (draAmts.Length > 0)
                dr["oiTaxable"] = draAmts[0]["amount"];
              else
                dr["oiTaxable"] = 0;
              dr["grand_oi_tax"] = Convert.ToDecimal(dr["grand_oi_tax"]) + Convert.ToDecimal(dr["oiTaxable"]);

              //Get the non-taxable other income for this cheque
              draAmts = dsAmount.Tables[0].Select("const=3 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              if (draAmts.Length > 0)
                dr["oiNonTaxable"] = draAmts[0]["amount"];
              else
                dr["oiNonTaxable"] = 0;
              dr["grand_oi_nontax"] = Convert.ToDecimal(dr["grand_oi_nontax"]) + Convert.ToDecimal(dr["oiNonTaxable"]);

              //Calculate Deduction Total
              draAmts = dsAmount.Tables[0].Select("const=4 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
              if (draAmts.Length > 0)
                dr["ded_total"] = draAmts[0]["amount"];
              else
                dr["ded_total"] = 0;
              dr["grand_ded"] = Convert.ToDecimal(dr["grand_ded"]) + Convert.ToDecimal(dr["ded_total"]);

              //Find out the "special" obligation and deduction amounts.  This is needed only for the Exceptions report.
              if (CHECK_POST == "CHECK")
              {
                decimal ded_fli = 0.0M;
                decimal ded_masa = 0.0M;
                decimal ded_nci = 0.0M;
                decimal ded_ba = 0.0M;
                decimal ded_fmi = 0.0M;
                decimal ded_others = 0.0M;

                //Get Sum Deduction Total FLI
                draAmts = dsAmount.Tables[0].Select("const=5 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                if (draAmts.Length > 0)
                  dr["ded_ssd"] = draAmts[0]["amount"];
                else
                  dr["ded_ssd"] = 0;

                ded_fli = Convert.ToDecimal(dr["ded_ssd"]);

                //Get SumDeduction Total MASA
                draAmts = dsAmount.Tables[0].Select("const=6 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                if (draAmts.Length > 0)
                  dr["ded_ssl"] = draAmts[0]["amount"];
                else
                  dr["ded_ssl"] = 0;

                ded_masa = Convert.ToDecimal(dr["ded_ssl"]);

                //Get Sum Obligation Total NCI 
                draAmts = dsAmount.Tables[0].Select("const=7 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                if (draAmts.Length > 0)
                  dr["obl_ssd"] = draAmts[0]["amount"];
                else
                  dr["obl_ssd"] = 0;

                ded_nci = Convert.ToDecimal(dr["obl_ssd"]);

                //Get SumObligation Total BA -- Changed From BA TO NEL
                draAmts = dsAmount.Tables[0].Select("const=8 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                if (draAmts.Length > 0)
                  dr["obl_ssib"] = draAmts[0]["amount"];
                else
                  dr["obl_ssib"] = 0;

                ded_ba = Convert.ToDecimal(dr["obl_ssib"]);

                //Get SumObligation Total FMI -- Changed From FMI TO AIS
                draAmts = dsAmount.Tables[0].Select("const=9 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                if (draAmts.Length > 0)
                  dr["obl_ssd1"] = draAmts[0]["amount"];
                else
                  dr["obl_ssd1"] = 0;

                ded_fmi = Convert.ToDecimal(dr["obl_ssd1"]);

                //Get SumObligation Total 'GOA', 'AMC', 'ATU','AC','ADB','ADF'
                draAmts = dsAmount.Tables[0].Select("const=10 and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                if (draAmts.Length > 0)
                  dr["obl_ssib1"] = draAmts[0]["amount"];
                else
                  dr["obl_ssib1"] = 0;

                ded_others = Convert.ToDecimal(dr["obl_ssib1"]);
              }
            }
            if (CHECK_POST == "CHECK")
            {
              if (objSCDVOMasterEmployee.TypeCode.Contains("WAG"))
              {
                if (dsSumIncome.Tables[0].Rows.Count > 0)
                {
                  DataRow[] DraIncomeREG = dsSumIncome.Tables[0].Select("inc_code='REGPY' and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                  if (DraIncomeREG.Length > 0)
                  {
                    dr["IncomeCodeREG"] = DraIncomeREG[0][0];
                    dr["IncomeRateREG"] = DraIncomeREG[0][2];
                    dr["IncomeHoursREG"] = DraIncomeREG[0][1];
                  }
                  else
                  {
                    dr["IncomeCodeREG"] = 0;
                    dr["IncomeRateREG"] = 0;
                    dr["IncomeHoursREG"] = 0;
                  }
                  DataRow[] DraIncomeOT = dsSumIncome.Tables[0].Select("inc_code='OT' and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                  if (DraIncomeOT.Length > 0)
                  {
                    dr["IncomeCodeOT"] = DraIncomeOT[0][0];
                    dr["IncomeRateOT"] = DraIncomeOT[0][2];
                    dr["IncomeHoursOT"] = DraIncomeOT[0][1];
                  }
                  else
                  {
                    dr["IncomeCodeOT"] = 0;
                    dr["IncomeRateOT"] = 0;
                    dr["IncomeHoursOT"] = 0;
                  }

                  DataRow[] DraIncomeDT = dsSumIncome.Tables[0].Select("inc_code='DT' and doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                  if (DraIncomeDT.Length > 0)
                  {
                    dr["IncomeCodeDT"] = DraIncomeDT[0][0];
                    dr["IncomeRateDT"] = DraIncomeDT[0][2];
                    dr["IncomeHoursDT"] = DraIncomeDT[0][1];
                  }
                  else
                  {
                    dr["IncomeCodeDT"] = 0;
                    dr["IncomeRateDT"] = 0;
                    dr["IncomeHoursDT"] = 0;
                  }
                }
              }
            }
            //Check Bonus Status
            if (GlobalObjDVOPayrollProcess_PayEmployee.bonus != string.Empty)
            {
              dr["bonus_check"] = GlobalObjDVOPayrollProcess_PayEmployee.bonus;
            }
            else
            {
              dr["bonus_check"] = "N";
            }
            if (dr["bonus_check"].ToString().Trim() == "Y")
            {
              Bonus_Check = true;
            }
            #endregion

            #endregion  Pre before group processing

            #region Post Into Stxtranr and Stytranr...
            //make sure values are valid
            if (dr["pay_date"] == DBNull.Value)
              dr["pay_date"] = dr["doc_date"];
            if (dr["eop_date"] == DBNull.Value)
              dr["eop_date"] = dr["pay_date"];

            if (dr["check_no"] != DBNull.Value && dr["check_no"] != null && dr["check_no"].ToString().Trim().Length > 0 && Convert.ToInt32(dr["check_no"]) > 0) { }
            else dr["check_no"] = 0;

            //if (dr["check_no"] != DBNull.Value)
            //{
            //    if (dr["check_no"].ToString().Trim() == string.Empty)
            //    {
            //        dr["check_no"] = 0;
            //    }
            //}
            //else
            //    dr["check_no"] = 0;

            bool IsCheckPrinted = (dr["print_check"].ToString().Trim() == "N" && Convert.ToInt32(dr["check_no"]) != 0) && dr["deposit"].ToString().Trim() == "N";
            if (CHECK_POST == "POST" && !IsCheckPrinted)
            {
              dr["ok_to_post"] = false;
              dr["err_3"] = "**** Error: No check number and/or document has not been printed/deposited.";
              err_msg = "Error: No check number and/or document has not been printed/deposited.";
              //if (CHECK_POST == "POST")
              //{
              throw new Exception(err_msg);
              //}
            }

            if (CHECK_POST == "POST")
            {
              DVOPostTrx ObjPostTransactions = new DVOPostTrx();
              ObjPostTransactions.post_or_check = CHECK_POST;
              ObjPostTransactions.orig_journal = "PY";
              ObjPostTransactions.doc_no = GlobalObjDVOPayrollProcess_PayEmployee.Doc_no;
              ObjPostTransactions.post_no = Post_no;
              ObjPostTransactions.post_date = DVOApplicationUserInfo.CurrentDate;
              ObjPostTransactions.doc_date = Convert.ToDateTime(dr["doc_date"]);
              ObjPostTransactions.ref_code = GlobalObjDVOPayrollProcess_PayEmployee.EmplCode;
              ObjPostTransactions.doc_desc = "PAYROLL ENTRY";
              string[] period = new string[2];
              period = what_period(ObjPostTransactions.doc_date);
              MakePyTrx(ref ObjPostTransactions, dr["check_no"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"]), Convert.ToDateTime(dr["eop_date"]), period, ref objTransaction);
            }

            #endregion

            #region call on_every_row function.........

            //on_every_row(ref objTransaction, ref objDVOPostGLGlobal, ref GlobalObjDVOPayrollProcess_PayEmployee, ref GlobalObjDVOMasterEmployee, objDataTable, dr, out status, CHECK_POST, new_doc_no, ref objGLSumTable, ref ObjDetailTable);

            #endregion call on_every_row function.........

            #region processing on after doc_no group

            bool _postingStatus = false;
            if (ds.Tables[0].Rows.Count != i + 1)
            {
              if (GlobalObjDVOPayrollProcess_PayEmployee.Doc_no != Convert.ToInt32(ds.Tables[0].Rows[i + 1]["doc_no"]))
              {
                _postingStatus = true;
              }
            }
            else if (ds.Tables[0].Rows.Count == i + 1)
              _postingStatus = true;
            if (_postingStatus)
            {

              //decimal d_tot_net = 0.0M;
              //decimal d_tot_gross = 0.0M;
              //decimal grand_d_hour = 0.0M;
              //decimal grand_d_net = 0.0M;
              //decimal tot_gross = 0.0M;
              //accumulate department totals

              dr["inc_net"] = Convert.ToDecimal(dr["inc_net"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());

              dr["inc_gross"] = Convert.ToDecimal(dr["inc_gross"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());
              //d_tot_hour += Convert.ToDecimal(dr["total_hours"].ToString().Trim());
              //grand_d_hour = grand_d_hour + Convert.ToDecimal(dr["total_hours"].ToString().Trim());
              //grand_d_net = grand_d_net + Convert.ToDecimal(dr["inc_net"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());
              //tot_gross = tot_gross + Convert.ToDecimal(dr["inc_gross"].ToString().Trim()) + Convert.ToDecimal(dr["inc_expense"].ToString().Trim());

              if (medcr_ded != medcr_obl)
              {
                //dr["ok_to_post"] = false;
                dr["warn_12"] = "**** Warning: Medicare Deduction not equal to Medicare Obligation.";
                dr["Problem8"] = "**** Warning:  Possible Medicare discrepancies exist in this report.";
              }

              if (fica_ded != fica_obl)
              {
                //dr["ok_to_post"] = false;
                dr["warn_13"] = "**** Warning: FICA Deduction not equal to FICA Obligation.";
                dr["Problem9"] = "**** **** Warning:  Possible FICA discrepancies exist in this report.";
              }
              if (Convert.ToBoolean(dr["ok_to_post"]))
              {
                //py_last(out int py_status, out string py_description,decimal py_c_accum,decimal py_i_accum,decimal py_d_accum,decimal py_ox_accum,decimal py_ol_accum)
                if (py_last(out py_status, out py_description, py_c_accum, py_i_accum, py_d_accum, py_ox_accum, py_ol_accum))
                {
                  if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
                  {
                    if (!BLLAccountingLiberary.gl_last(ref objDVOPostGLGlobal))
                    {

                      dr["ok_to_post"] = false;
                      dr["err_4"] = "**** Error: document does not balance.";
                      err_msg = "Error: document does not balance.";

                      //dr["Problem4"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        throw new Exception("Error: document does not balance.");
                      }
                    }
                  }
                }
                else
                {
                  if (py_status == 10)
                  {

                    dr["ok_to_post"] = false;
                    dr["err_4"] = "***** Error: This document does not balance.";
                    err_msg = "Error: document does not balance.";

                    //dr["Problem4"] = "ON";
                    if (CHECK_POST == "POST")
                    {
                      throw new Exception("Error: document does not balance.");
                    }
                  }
                  else if (py_status == 1)
                  {

                    dr["ok_to_post"] = false;
                    dr["err_1"] = "**** Error: Payroll is not installed.";
                    err_msg = "Error: Payroll is not installed.";

                    //dr["Problem1"] = "ON";
                    if (CHECK_POST == "POST")
                    {
                      throw new Exception("Error: Payroll is not installed.");
                    }
                  }

                }

              }

              //check pay frequency   //Need to make this function pay_time
              if (Convert.ToBoolean(dr["ok_to_post"]) && !objBLLPayrollFunctions.pay_time(Convert.ToDateTime(dr["last_pay"]), Convert.ToDateTime(dr["eop_date"]), dr["pay_period"].ToString().Trim(), Bonus_Check))
              {

                dr["err_5"] = "**** Warning: Employee not due to be paid. Last paid:" + Convert.ToDateTime(dr["last_pay"]).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //dr["Problem5"] = "ON";
              }

              // update last pay date, sick accrual, vacation accrual
              if (Convert.ToBoolean(dr["ok_to_post"]))
              {

                if (CHECK_POST == "POST")
                {
                  if (dr["bonus_check"].ToString().Trim() == "Y")
                  {

                    int upd_result = UPDATE_SICK_AND_VACATION(ref objTransaction, ref objDVOExceptionReports, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["sick_used"]), sick_accum, vac_accum, Convert.ToDecimal(dr["vac_used"]));
                    if (upd_result == 0)
                    {

                      dr["ok_to_post"] = false;

                      dr["err_2"] = "**** Error: Unable to update employee data.";
                      err_msg = "Error: Unable to update employee data.";
                      //dr["Problem2"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        throw new Exception("Error: Unable to update employee data.");
                      }
                    }
                  }
                  else
                  {
                    if (Convert.ToDateTime(dr["last_pay"]) > Convert.ToDateTime(dr["eop_date"]))
                    {

                      int upd_result = UPDATE_SICK_AND_VACATION(ref objTransaction, ref objDVOExceptionReports, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["sick_used"]), sick_accum, vac_accum, Convert.ToDecimal(dr["vac_used"]));
                      if (upd_result == 0)
                      {
                        dr["ok_to_post"] = false;
                        err_msg = "Error: Unable to update employee data.";
                        dr["err_2"] = "**** Error: Unable to update employee data.";
                        //dr["Problem2"] = "ON";
                        if (CHECK_POST == "POST")
                        {
                          throw new Exception(" Error: Unable to update employee data.");
                        }
                      }
                    }
                    else
                    {

                      int upd_result = UPDATE_SICK_PAY_AND_VACATION(ref objTransaction, ref objDVOExceptionReports, Convert.ToDateTime(dr["eop_date"]), dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["sick_used"]), sick_accum, vac_accum, Convert.ToDecimal(dr["vac_used"]));
                      if (upd_result == 0)
                      {

                        dr["ok_to_post"] = false;
                        err_msg = "Error: Unable to update employee data.";
                        dr["err_2"] = "**** Error: Unable to update employee data.";
                        //dr["Problem2"] = "ON";
                        if (CHECK_POST == "POST")
                        {

                          throw new Exception(" Error: Unable to update employee data.");
                        }
                      }
                    }
                  }

                  if (dr["accrue_vac"].ToString().Trim() == "Y")
                  {
                    vac_time_accrue(ref objTransaction, dr, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["total_hours"]), Convert.ToDecimal(dr["vac_allowed"]), Convert.ToDateTime(dr["eop_date"]), out status);
                    if (status == 1)
                    {

                      dr["ok_to_post"] = false;
                      dr["err_2"] = "**** Error: Unable to calculate vacation time accrual.";
                      err_msg = "Unable to calculate vacation time accrual.";

                      //dr["Problem2"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        if (objTransaction != null)
                          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        throw new Exception(" Error: Unable to calculate vacation time accrual.");
                      }
                    }
                  }
                  if (dr["accrue_sick"].ToString().Trim() == "Y")
                  {
                    sick_time_accrue(ref objTransaction, dr, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["total_hours"]), Convert.ToDecimal(dr["vac_allowed"]), Convert.ToDateTime(dr["eop_date"]), out status);
                    if (status == 1)
                    {

                      dr["ok_to_post"] = false;
                      err_msg = "Unable to calculate sick time accrual.";
                      dr["err_2"] = "**** Error: Unable to calculate sick time accrueal.";
                      //dr["Problem2"] = "ON";
                      if (CHECK_POST == "POST")
                      {
                        if (objTransaction != null)
                          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);

                        throw new Exception(" Error: Unable to calculate vacation time accrueal.");
                      }

                    }
                  }
                }
              }


              if (CHECK_POST == "POST")
              {
                // delete and commit work or roll back
                if (Convert.ToBoolean(dr["ok_to_post"]) && py_delete(ref objTransaction, ref objDVOExceptionReports, GlobalObjDVOPayrollProcess_PayEmployee.Doc_no))
                {
                  //commit work
                  //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                }
                else
                {
                  if (CHECK_POST == "POST")
                  {
                    if (objTransaction != null)
                      objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    throw new Exception("This document was not posted--the document number will not be re-used.");
                  }
                }

              }
              else
              {
                if (Convert.ToBoolean(dr["ok_to_post"]))
                {

                  int upd_result = UPDATE_OK_TO_POST_STATUS(ref objTransaction, ref objDVOExceptionReports, "Y", GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
                  if (upd_result == 0)
                  {

                    dr["err_0"] = "**** This document has errors.";
                    dr["Problem0"] = "**** Unable to update ok_to_post status.";
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    errorMassage.Append("[" + Curr_Emp_Code + "-" + "Unable to update ok_to_post status." + "]");
                    //if (ds.Tables[0].Rows.Count != i + 1)
                    //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
                  }
                  else
                  {
                    //commit work
                    objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    recordsProcessed++;
                    if (ds.Tables[0].Rows.Count == i + 1)
                    {
                      dr["Problem0"] = "***** Report completed successfully. No errors detected.";
                    }
                    //else
                    //{
                    //    dr["Problem0"] = "***** Report completed successfully. No errors detected.";
                    //}
                  }

                }
                else
                {
                  objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                  //if (ds.Tables[0].Rows.Count != i + 1)
                  //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
                  dr["Problem0"] = "**** Some documents in this report have errors.";
                  errorMassage.Append("[" + Curr_Emp_Code + "-" + err_msg + "]");
                }
              }

              //check for negative check amount
              if (Convert.ToDecimal(dr["cash_amount"]) < 0)
              {
                dr["warn_0"] = "**** Warning: Check amount negative -check will not print.";
                //dr["Problem5"] = "ON";
              }
              // check for temination date
              //Following Code commented by neeraj as per requested JKPS team
              //if (Convert.ToDateTime(dr["terminated"]) > Convert.ToDateTime(dr["eop_date"]))
              //{
              //    dr["warn_1"] = "**** Warning: Employee terminated prior to payroll period.";
              //    //dr["Problem5"] = "ON";
              //}

              //verify that FICA deduction has not reached limit
              int dup_ssn = 0;
              //dup_ssn = BLLGeneratePaySlipDetails.Get_dup_ssn(dr["soc_sec_num"].ToString().Trim());
              if (dsDuplicateSSN != null && dsDuplicateSSN.Tables.Count > 0)
              {
                DataRow[] drs = dsDuplicateSSN.Tables[0].Select("SSN = '" + dr["soc_sec_num"].ToString().Trim() + "'");
                if (drs.Length > 0)
                  dup_ssn = drs[0]["COUNT"] != DBNull.Value ? Convert.ToInt32(drs[0]["COUNT"]) : 0;
              }
              if (dfica_flag)
              {

                if (dup_ssn > 1)
                {
                  //DataSet dedSumAmount = GetDeductionSumAmount(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["soc_sec_num"].ToString().Trim());//Edited by sanjay
                  decimal dedSumAmount = GetDeductionSumAmount(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["soc_sec_num"].ToString().Trim());

                  if (dedSumAmount > 0)
                  {
                    //if (dedSumAmount.Tables[0].Rows[0][0] != DBNull.Value)
                    //act_accrual = Convert.ToDecimal(dedSumAmount.Tables[0].Rows[0][0]);
                    act_accrual = dedSumAmount;
                  }
                }
                else
                {
                  //DataSet dedSumAmount = GetDeductionSumAmountOne(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["empl_code"].ToString().Trim());
                  //if (dedSumAmount.Tables[0].Rows[0][0] != DBNull.Value)
                  //    act_accrual = Convert.ToDecimal(dedSumAmount.Tables[0].Rows[0][0]);
                  decimal dedSumAmount = GetDeductionSumAmountOne(objGloabalPayDefaultsListStycntrc[0].fica_code, dr["empl_code"].ToString().Trim());
                  if (dedSumAmount > 0)
                    act_accrual = dedSumAmount;
                }

                //DataSet dsDedDfltLimit = GET_ACT_DFLT_LIMIT(Convert.ToString(dr["empl_code"]), objGloabalPayDefaultsListStycntrc[0].fica_code);
                decimal dsDedDfltLimit = GET_ACT_DFLT_DED_LIMIT(Convert.ToString(dr["empl_code"]), objGloabalPayDefaultsListStycntrc[0].fica_code);
                //if (dsDedDfltLimit.Tables[0].Rows.Count > 0)
                //{
                //    if (act_limit == 0.0M)
                //    {
                //        if (dsDedDfltLimit.Tables[0].Rows[0][0] != DBNull.Value)
                //            act_limit = Convert.ToDecimal(dsDedDfltLimit.Tables[0].Rows[0][1]); //dflt_limit
                //    }
                //    else if (act_accrual >= act_limit)
                //    {
                //        dfica_flag = true;  //dfica_lmt
                //    }
                //}                                    
                if (act_limit == 0.0M)
                {
                  //if (dsDedDfltLimit > 0)
                  act_limit = dsDedDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  dfica_flag = true;  //dfica_lmt
                }

              }

              // verify that medicare deduction has not reached limit

              if (dmedicare_flag)
              {

                if (dup_ssn > 1)
                {
                  decimal dedSumAmount = GetDeductionSumAmount(objGloabalPayDefaultsListStycntrc[0].medicare_code, dr["soc_sec_num"].ToString().Trim());
                  if (dedSumAmount > 0)
                    act_accrual = dedSumAmount;

                }
                else
                {
                  decimal dedSumAmount = GetDeductionSumAmountOne(objGloabalPayDefaultsListStycntrc[0].medicare_code, dr["empl_code"].ToString().Trim());
                  if (dedSumAmount > 0)
                    act_accrual = dedSumAmount;
                }

                decimal dsDedDfltLimit = GET_ACT_DFLT_DED_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].medicare_code);
                if (act_limit == 0.0M)
                {
                  //if (dsDedDfltLimit > 0)
                  act_limit = dsDedDfltLimit;//dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  dmedicare_lmt = true;
                }

              }

              // verify that FICA obligation has not reached limit

              if (ofica_flag)
              {

                if (dup_ssn > 1)
                {
                  decimal oblSumAmount = GetObligationSumAmount(objGloabalPayDefaultsListStycntrc[0].fica_ob_code, dr["soc_sec_num"].ToString().Trim());
                  if (oblSumAmount > 0)
                  {
                    act_accrual = oblSumAmount;
                  }
                }
                else
                {
                  decimal oblSumAmount = GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].fica_ob_code, dr["empl_code"].ToString().Trim());
                  if (oblSumAmount > 0)
                    act_accrual = oblSumAmount;
                }

                decimal dsOblDfltLimit = GET_ACT_DFLT_OBL_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].fica_ob_code);

                if (act_limit == 0.0M)
                {
                  //if (dsOblDfltLimit > 0)
                  act_limit = dsOblDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  ofica_lmt = true;
                }

              }

              //verify that medicare obligation has not reached limit

              if (omedicare_flag)
              {
                if (dup_ssn > 1)
                {
                  decimal oblSumAmount = GetObligationSumAmount(objGloabalPayDefaultsListStycntrc[0].medicare_ob_code, dr["soc_sec_num"].ToString().Trim());
                  if (oblSumAmount > 0)
                  {
                    act_accrual = oblSumAmount;
                  }
                }
                else
                {
                  act_accrual = GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].medicare_ob_code, dr["empl_code"].ToString().Trim());
                  //decimal oblSumAmount = GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].medicare_ob_code, dr["empl_code"].ToString().Trim());
                  //if (oblSumAmount.Tables[0].Rows.Count > 0)
                  //{
                  //    if (oblSumAmount.Tables[0].Rows[0][0] != DBNull.Value)
                  //        act_accrual = Convert.ToDecimal(oblSumAmount.Tables[0].Rows[0][0]);
                  //}
                }

                decimal dsOblDfltLimit = GET_ACT_DFLT_OBL_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].medicare_ob_code);
                if (act_limit == 0.0M)
                {
                  //if (dsOblDfltLimit > 0)
                  act_limit = dsOblDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  omedicare_lmt = true;
                }

              }

              // verify that FUTA obligation has not reached limit

              if (ofuta_flag)
              {

                if (dup_ssn > 1)
                {
                  decimal oblSumAmount = GetObligationSumAmount(objGloabalPayDefaultsListStycntrc[0].futa_code, dr["soc_sec_num"].ToString().Trim());
                  if (oblSumAmount > 0)
                  {
                    act_accrual = oblSumAmount;
                  }
                }
                else
                {
                  decimal oblSumAmount = GetObligationSumAmountOne(objGloabalPayDefaultsListStycntrc[0].futa_code, dr["empl_code"].ToString().Trim());
                  if (oblSumAmount > 0)
                    act_accrual = oblSumAmount;
                }

                decimal dsOblDfltLimit = GET_ACT_DFLT_OBL_LIMIT(dr["empl_code"].ToString().Trim(), objGloabalPayDefaultsListStycntrc[0].futa_code);
                if (act_limit == 0.0M)
                {
                  //if (dsOblDfltLimit > 0)
                  act_limit = dsOblDfltLimit; //dflt_limit
                }
                else if (act_accrual >= act_limit)
                {
                  ofuta_lmt = true;
                }

              }

              //verify that employee is not EXEMPT from federal income tax
              if (dftax_flag)
              {
                //int dsAllowence = GET_ALLOWENCE(dr["empl_code"].ToString().Trim());
                if (dr["allowances"] == DBNull.Value)
                  dr["allowances"] = 0;
                if (Convert.ToInt32(dr["allowances"]) > 0)
                {
                  if (Convert.ToInt32(dr["allowances"]) == 99 && dr["soc_sec_num"] == DBNull.Value)
                  {
                    dftax_xmt = true;
                  }
                }
                else
                {
                  break;
                }
              }
              //removed the warning messages for FICA, FUTA, and Medicare -- Nevis and St. Kitts do not have these
              //if this is the edit list, hold the temporary sick/vacation
              // accumulated on this paycheck.
              if (CHECK_POST != "POST")
              {
                sick_acc_tmp = Convert.ToInt32(sick_accum);
                vac_acc_tmp = Convert.ToInt32(vac_accum);
              }

              //get current sick and vacation used values (may have changed
              //since the rpt record was loaded if other employees with same
              //social security number were paid sick or vacation pay)

              //DataSet dsSickAndVacation = GetSumOfSickAndVacation(dr["soc_sec_num"].ToString().Trim());
              //if (dsSickAndVacation.Tables[0].Rows[0][0] == DBNull.Value)
              //    dsSickAndVacation.Tables[0].Rows[0][0] = 0;
              //if (dsSickAndVacation.Tables[0].Rows[0][1] == DBNull.Value)
              //    dsSickAndVacation.Tables[0].Rows[0][1] = 0;



              // if it is the edit list then the values selected into
              // sick_accum and vac_accum do not include the current payroll
              if (dsSickAndVacation.Tables[0].Rows.Count > 0)
              {
                DataRow[] dra = dsSickAndVacation.Tables[0].Select("soc_sec_num='" + dr["soc_sec_num"].ToString().Trim() + "'");
                if (dra.Length > 0)
                {
                  if (dra[0][0] == DBNull.Value)
                    dra[0][0] = 0;
                  if (dra[0][1] == DBNull.Value)
                    dra[0][1] = 0;
                  if (CHECK_POST != "POST")
                  {
                    sick_accum = sick_acc_tmp + Convert.ToInt32(dra[0][0]);
                    vac_accum = vac_acc_tmp + Convert.ToInt32(dra[0][1]);
                  }
                }
              }

              //check for excess sick and vacation pay
              if (sick_accum > Convert.ToInt32(dr["sick_allowed"]))
              {
                dr["warn_4"] = "**** Warning: Excess sick leave has been indicated.";
                //dr["Problem5"] = "ON";
              }
              if (vac_accum > Convert.ToInt32(dr["vac_allowed"]))
              {
                dr["warn_5"] = "**** Warning: Excess vacation leave has been indicated.";
                //dr["Problem5"] = "ON";
              }
              objDVOPostGLGlobal = new DVOPostGLGlobal();
              #region Initialize Variables
              act_accrual = 0.0M;
              act_limit = 0.0M;
              dflt_limit = 0.0M;
              empl_allow = 0;
              entry_count = 0;
              sick_acc_tmp = 0;
              vac_acc_tmp = 0;
              d_tot_hour = 0.0M;
              status = 0;
              new_doc_no = 0;
              gl_status = 0;
              dmedicare_lmt = false;
              ofica_lmt = false;
              omedicare_lmt = false;
              ofuta_lmt = false;
              dftax_xmt = false;
              Bonus_Check = false;
              // variable used in ded_post
              dfica_flag = false;
              fica_ded = 0.0M;
              dmedicare_flag = false;
              medcr_ded = 0.0M;
              dftax_flag = false;
              //variable used in obl_post
              ofuta_flag = false;
              ofica_flag = false;
              fica_obl = 0.0M;
              omedicare_flag = false;
              medcr_obl = 0.0M;
              // variable used in inc_post
              sick_accum = 0.0M;
              vac_accum = 0.0M;
              //Variable used in py_post
              //py_installed;
              //Variable used To pass in py_post as an output
              py_c_accum = 0.0M;
              py_i_accum = 0.0M;
              py_d_accum = 0.0M;
              py_ox_accum = 0.0M;
              py_ol_accum = 0.0M;
              py_status = 0;
              py_description = string.Empty;
              #endregion
            }
            #endregion processing on after doc_no group

            #region commit work
            //commit transaction on last row..........
            if (ds.Tables[0].Rows.Count == i + 1)
            {
              if (CHECK_POST == "POST")
              {
                #region Update Income/Deduction/Obligation QTD/YTD
                //***********  Added by Bharat Dhall [02-11-2010]  **************************
                //****** to update quaterly/yearly total incomes/deduction/obligation *******


                string pay_date = objSCDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                int j = UpdateQtdYtdIncome(pay_date);
                if (j <= 0)
                  throw new Exception("Error occured during updation of incomes Qty/Ytd of employees.");
                j = UpdateQtdYtdDeduction(pay_date);
                if (j <= 0)
                  throw new Exception("Error occured during updation of deductions Qty/Ytd of employees.");
                j = UpdateQtdYtdObligation(pay_date);
                if (j <= 0)
                  throw new Exception("Error occured during updation of obligations Qty/Ytd of employees.");

                //***************************************************************************
                #endregion Update Income/Deduction/Obligation QTD/YTD

                #region GL Posting

                DVOPostGL objtmpDVOPostGL = new DVOPostGL();
                DVOGLTRanActVD objtmpDVOGLTRanActVD = new DVOGLTRanActVD();
                object[] trxParameters = new object[24];
                object[] tranrparameters = new object[13];
                object[] styactvdParameters = new object[16];
                object[] stgactvdParameters = new object[6];
                object[] stxckrgdParameters = new object[7];
                if (arrlistParameters1.Count > 0)
                  foreach (object objpram in arrlistParameters1)
                  {
                    tranrparameters = (object[])objpram;
                    trxParameters[0] = "Y";
                    trxParameters[1] = tranrparameters[0];//orig_journal
                    trxParameters[2] = tranrparameters[1];//doc_no
                    trxParameters[3] = tranrparameters[2];//post_no
                    trxParameters[4] = tranrparameters[3];//post_date
                    trxParameters[5] = tranrparameters[4];//doc_date
                    trxParameters[6] = tranrparameters[5];//ref_code
                    trxParameters[7] = tranrparameters[6];//doc_desc
                    trxParameters[8] = tranrparameters[7];//user_id
                    trxParameters[9] = tranrparameters[8];// check_no
                    trxParameters[10] = tranrparameters[9];//pay_date
                    trxParameters[11] = tranrparameters[10];//eop_date
                    trxParameters[12] = tranrparameters[11];//acct_period
                    trxParameters[13] = tranrparameters[12];//acct_year

                    if (arrlistParameters4.Count > 0)
                      for (int n = 0; n < arrlistParameters4.Count; n++)
                      {

                        styactvdParameters = (object[])arrlistParameters4[n];
                        if (Convert.ToInt32(trxParameters[2]) != Convert.ToInt32(styactvdParameters[1]))
                        {
                          break;
                        }
                        stgactvdParameters = (object[])arrlistParameters2[n];
                        stxckrgdParameters = (object[])arrlistParameters3[n];

                        trxParameters[14] = stgactvdParameters[2];//acct_no
                        trxParameters[15] = stgactvdParameters[3];//department
                        trxParameters[16] = stgactvdParameters[4];//amount
                        trxParameters[17] = stgactvdParameters[5];//debit_credit

                        trxParameters[18] = stxckrgdParameters[4];//inv_chk_no

                        trxParameters[19] = styactvdParameters[2];//act_code
                        trxParameters[20] = styactvdParameters[3];//act_type
                        trxParameters[21] = styactvdParameters[5];//number
                        trxParameters[22] = styactvdParameters[6];//hours
                        trxParameters[23] = styactvdParameters[7];//rate

                        object result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref trxParameters, objtmpDVOGLTRanActVD.INSERT_PYTRX, true);
                        if (result == DBNull.Value || result == null || result.ToString().Trim().Length <= 0 || Convert.ToInt32(result) != 1)
                        {
                          throw new Exception("Error has occurred while posting trx.");
                        }
                        trxParameters[0] = "N";
                        arrlistParameters2.RemoveAt(n);
                        arrlistParameters3.RemoveAt(n);
                        arrlistParameters4.RemoveAt(n);
                        n--;
                      }
                  }

                //*******************************************
                //object[] parameters = new object[6];
                //if (arrlistParameters2.Count > 0)
                //    foreach (object objparam in arrlistParameters2)
                //    {
                //        parameters = (object[])objparam;
                //        //parameters[0] = ObjPostGLTransaction.orig_journal;
                //        //parameters[1] = ObjPostGLTransaction.doc_no;
                //        //parameters[2] = ObjPostGLTransaction.acct_no;
                //        //parameters[3] = ObjPostGLTransaction.department;
                //        //parameters[4] = ObjPostGLTransaction.amount;
                //        //parameters[5] = ObjPostGLTransaction.debit_credit;
                //        object result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objtmpDVOGLTRanActVD.INSERT_STGACTVD, true);
                //        if (result == DBNull.Value || result == null || result.ToString().Trim().Length <= 0 || Convert.ToInt32(result) != 1)
                //        {
                //            throw new Exception("Error has occurred while posting into (stgactvd).");
                //        }
                //    }

                //parameters = new object[7];
                //if (arrlistParameters3.Count > 0)
                //    foreach (object objparam in arrlistParameters3)
                //    {
                //        parameters = (object[])objparam;
                //        //parameters[0] = ObjPostGLTransaction.orig_journal;
                //        //parameters[1] = ObjPostGLTransaction.doc_no;
                //        //parameters[2] = ObjPostGLTransaction.acct_no;
                //        //parameters[3] = ObjPostGLTransaction.department;
                //        //parameters[4] = ObjPostGLTransaction.inv_chk_no.Trim().Substring(0, 10);
                //        //parameters[5] = ObjPostGLTransaction.amount;
                //        //parameters[6] = ObjPostGLTransaction.debit_credit;

                //        object Insresult = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransaction, ref parameters, objtmpDVOPostGL.INSERT_PY_STXCKRGD, true);
                //        if (Insresult == DBNull.Value || Insresult == null || Insresult.ToString().Trim().Length <= 0 || Convert.ToInt32(Insresult) != 1)
                //        {
                //            throw new Exception("Error has occurred while posting into (stxchrgd).");
                //        }
                //    }
                //parameters = new object[16];
                //if (arrlistParameters4.Count > 0)
                //    foreach (object objparam in arrlistParameters4)
                //    { 
                //        parameters = (object[])objparam;

                //        object o = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDVOExceptionReports.INSERT_INS_2);
                //        if (o == DBNull.Value || o == null || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
                //            throw new Exception("Error has occurred while posting into (Styactvd).");
                //    }


                #endregion GL Posting

                //commit work 
                //if (CHECK_POST == "POST")
                //{
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                Curr_Emp_Code = string.Empty;
                dr["Problem0"] = "**** These documents have been posted successfully.";
                recordsProcessed = ds.Tables[0].Rows.Count;
              }

            }
            #endregion

            objDataTable.Rows.Add(dr.ItemArray);

            //if (CHECK_POST != "POST")
            //{
            //    if (objTransaction != null)
            //    { 

            //    }
            //}
          }
        }
        else
        {
          //errorMassage.Append("[No Element to Process]");
        }
        FinalDs.Tables.Add(objDataTable);
        FinalDs.Tables.Add(objGLSumTable);
        FinalDs.Tables.Add(ObjDetailTable);
        FinalDs.Tables[0].TableName = "DSEXCEPTIONREPORTS";
        FinalDs.Tables[1].TableName = "GlSumTable";
        FinalDs.Tables[2].TableName = "DtlTable";
      }
      catch (Exception ex)
      {
        if (objTransaction != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
        errorMassage.Append("[" + ex.Message + "]");
        if (CHECK_POST == "POST")
        {
          objDataTable.Rows.Clear();
          objGLSumTable.Rows.Clear();
          if (objDataTable.Columns.Contains("Problem0") && objDataTable.Columns.Contains("Problem1"))
          {
            DataRow dr = objDataTable.NewRow();
            if (Curr_Emp_Code.Trim().Length > 0)
              dr["Problem0"] = "**** Posting has been terminated for empl_code -  " + Curr_Emp_Code;
            dr["Problem1"] = "**** " + ex.Message;
            objDataTable.Rows.Add(dr.ItemArray);
            FinalDs.Tables.Add(objDataTable);
            FinalDs.Tables.Add(objGLSumTable);
            FinalDs.Tables.Add(ObjDetailTable);
            FinalDs.Tables[0].TableName = "DSEXCEPTIONREPORTS";
            FinalDs.Tables[1].TableName = "GlSumTable";
            FinalDs.Tables[2].TableName = "DtlTable";
          }
          else
          {
            ExceptionManagement.ExceptionManager.Publish(ex);
            throw ex;
          }
        }
        else
        {
          ExceptionManagement.ExceptionManager.Publish(ex);
          throw ex;
        }

      }
      finally
      {
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
          obj.processname = processName;
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

      //FinalDs.Tables[0].WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DSExceptionReports.xsd");
      //FinalDs.Tables[1].WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\GlSumTable.xsd");
      //FinalDs.Tables[2].WriteXmlSchema(@"D:\Sujeet_Sir\Himanshu_Rajput\App.Web\Reports\DtlTable.xsd");

      return FinalDs;
    }


    //****** following three functions to update qty/ytd incomes/deductions/obligations of employees
    // ******    after posting payrolls

    private int UpdateQtdYtdIncome(string Pay_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

      try
      {
        object[] parameters = new object[2];
        parameters[0] = Pay_date;

        string[] arrEmpTypes = SearchedEmployeeTypes.Split(',');
        if (arrEmpTypes.Length > 0)
          foreach (string emp_type in arrEmpTypes)
          {
            parameters[1] = emp_type;
            object o = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOExceptionReports()).UPDATE_INCOME_QTD_YTD);
            if (o == DBNull.Value && o == null && o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
              throw new Exception("Error occured during updation of incomes Qty/Ytd of employees.");


          }
        parameters = null;
        objDALBaseClass = null;

        return 1;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    private int UpdateQtdYtdDeduction(string Pay_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

      try
      {
        object[] parameters = new object[2];
        parameters[0] = Pay_date;

        string[] arrEmpTypes = SearchedEmployeeTypes.Split(',');
        if (arrEmpTypes.Length > 0)
          foreach (string emp_type in arrEmpTypes)
          {
            parameters[1] = emp_type;
            object o = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOExceptionReports()).UPDATE_DEDUCTION_QTD_YTD);
            if (o == DBNull.Value && o == null && o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
              throw new Exception("Error occured during updation of incomes Qty/Ytd of employees.");
          }
        parameters = null;
        objDALBaseClass = null;

        return 1;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    private int UpdateQtdYtdObligation(string Pay_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

      try
      {
        object[] parameters = new object[2];
        parameters[0] = Pay_date;

        string[] arrEmpTypes = SearchedEmployeeTypes.Split(',');
        if (arrEmpTypes.Length > 0)
          foreach (string emp_type in arrEmpTypes)
          {
            parameters[1] = emp_type;
            object o = objDALBaseClass.ExecuteScalar(ref parameters, (new DVOExceptionReports()).UPDATE_OBLIGATION_QTD_YTD);
            if (o == DBNull.Value && o == null && o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
              throw new Exception("Error occured during updation of incomes Qty/Ytd of employees.");
          }
        parameters = null;
        objDALBaseClass = null;

        return 1;
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return 0;
    }

    //************************************************************************************************

    static string SearchedEmployeeTypes = string.Empty;
    private static DataSet GetPayrollEntries(ref DVOPayrollProcess_PayEmployee objSCDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee objSCDVOMasterEmployee, string CHECK_POST, out string arrDocNo, out string SearchedEmployeeList, out string SearchedSocSecNo, out DataSet dsAmounts)
    {
      DataSet ds = null;
      arrDocNo = string.Empty;
      SearchedEmployeeList = string.Empty;
      SearchedSocSecNo = string.Empty;
      SearchedEmployeeTypes = string.Empty;
      dsAmounts = new DataSet();
      try
      {
        object[] parameter = new object[13];
        //Get Search Criteria in Styemplr
        parameter[0] = objSCDVOMasterEmployee.FirstName;
        parameter[1] = objSCDVOMasterEmployee.LastName;
        parameter[2] = objSCDVOMasterEmployee.PayPeriod;
        parameter[3] = objSCDVOMasterEmployee.TypeCode;
        parameter[4] = objSCDVOMasterEmployee.JobCode;
        parameter[5] = objSCDVOMasterEmployee.JobTitle;

        //Get Search Criteria in Process_PayEmployee
        parameter[6] = objSCDVOPayrollProcess_PayEmployee.EmplCode;
        parameter[7] = objSCDVOPayrollProcess_PayEmployee.eop_date == DateTime.MinValue ? "1900/01/01" : objSCDVOPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameter[8] = objSCDVOPayrollProcess_PayEmployee.Cash_acct_no;
        //This is last_pay from styemplr
        parameter[9] = objSCDVOPayrollProcess_PayEmployee.pay_date == DateTime.MinValue ? "1900/01/01" : objSCDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameter[10] = CHECK_POST;
        parameter[12] = objSCDVOMasterEmployee.District;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        ds = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports));
        #region Set the column name..and add columns for message and problems......
        ds.Tables[0].Columns[0].ColumnName = "first_name"; //styemplr
        ds.Tables[0].Columns[1].ColumnName = "flexdeptaccttype"; //styemplr
        ds.Tables[0].Columns[2].ColumnName = "last_name";//styemplr
        ds.Tables[0].Columns[3].ColumnName = "last_pay";//styemplr
        ds.Tables[0].Columns[4].ColumnName = "middle_name";//styemplr
        ds.Tables[0].Columns[5].ColumnName = "pay_period";//styemplr
        ds.Tables[0].Columns[6].ColumnName = "sick_allowed";//styemplr
        ds.Tables[0].Columns[7].ColumnName = "sick_code";//styemplr
        ds.Tables[0].Columns[8].ColumnName = "sick_used";//styemplr
        ds.Tables[0].Columns[9].ColumnName = "soc_sec_num";//styemplr
        ds.Tables[0].Columns[10].ColumnName = "terminated";//styemplr
        ds.Tables[0].Columns[11].ColumnName = "vac_allowed";//styemplr
        ds.Tables[0].Columns[12].ColumnName = "vac_code";//styemplr
        ds.Tables[0].Columns[13].ColumnName = "vac_used";//styemplr
        ds.Tables[0].Columns[14].ColumnName = "cash_acct_no"; //--Process_PayEmployee.
        ds.Tables[0].Columns[15].ColumnName = "cash_amount"; //--Process_PayEmployee
        ds.Tables[0].Columns[16].ColumnName = "check_no"; //--Process_PayEmployee
        ds.Tables[0].Columns[17].ColumnName = "ded_fedtax"; //--Process_PayEmployee
        ds.Tables[0].Columns[18].ColumnName = "ded_fica";//--Process_PayEmployee
        ds.Tables[0].Columns[19].ColumnName = "ded_loctax";//--Process_PayEmployee
        ds.Tables[0].Columns[20].ColumnName = "ded_medicare";//--Process_PayEmployee
        ds.Tables[0].Columns[21].ColumnName = "ded_other";//--Process_PayEmployee
        ds.Tables[0].Columns[22].ColumnName = "ded_statax";//--Process_PayEmployee
        ds.Tables[0].Columns[23].ColumnName = "department";//--Process_PayEmployee
        ds.Tables[0].Columns[24].ColumnName = "doc_date";//--Process_PayEmployee
        ds.Tables[0].Columns[25].ColumnName = "doc_no";//--Process_PayEmployee
        ds.Tables[0].Columns[26].ColumnName = "empl_code";//--Process_PayEmployee
        ds.Tables[0].Columns[27].ColumnName = "eop_date";//--Process_PayEmployee
        ds.Tables[0].Columns[28].ColumnName = "inc_expense";//--Process_PayEmployee
        ds.Tables[0].Columns[29].ColumnName = "inc_gross";//--Process_PayEmployee
        ds.Tables[0].Columns[30].ColumnName = "inc_net";//--Process_PayEmployee
        ds.Tables[0].Columns[31].ColumnName = "inc_taxable";//--Process_PayEmployee
        ds.Tables[0].Columns[32].ColumnName = "obl_fica";//--Process_PayEmployee
        ds.Tables[0].Columns[33].ColumnName = "obl_futa";//--Process_PayEmployee
        ds.Tables[0].Columns[34].ColumnName = "obl_medicare";//--Process_PayEmployee
        ds.Tables[0].Columns[35].ColumnName = "obl_other";//--Process_PayEmployee
        ds.Tables[0].Columns[36].ColumnName = "obl_total";//--Process_PayEmployee
        ds.Tables[0].Columns[37].ColumnName = "pay_date";//--Process_PayEmployee
        ds.Tables[0].Columns[38].ColumnName = "total_hours";//--Process_PayEmployee
        ds.Tables[0].Columns[39].ColumnName = "emp_cash_acct";//styemplr
        ds.Tables[0].Columns[40].ColumnName = "emp_department";//styemplr
        ds.Tables[0].Columns[41].ColumnName = "accrue_vac"; //--Process_PayEmployee
        ds.Tables[0].Columns[42].ColumnName = "accrue_sick";//--Process_PayEmployee
        ds.Tables[0].Columns[43].ColumnName = "print_check";//--Process_PayEmployee
        ds.Tables[0].Columns[44].ColumnName = "deposit";//--Process_PayEmployee
        ds.Tables[0].Columns[45].ColumnName = "bonus";//--Process_PayEmployee
        ds.Tables[0].Columns[46].ColumnName = "vac_accr_code";//--styemplr
        ds.Tables[0].Columns[47].ColumnName = "vac_accr_ctr";//--styemplr
        ds.Tables[0].Columns[48].ColumnName = "vac_lapse_date";//--styemplr
        ds.Tables[0].Columns[49].ColumnName = "sick_accr_code";//--styemplr
        ds.Tables[0].Columns[50].ColumnName = "sick_accr_ctr";//--styemplr
        ds.Tables[0].Columns[51].ColumnName = "sick_lapse_date";//--styemplr
        ds.Tables[0].Columns[52].ColumnName = "allowances";//--styemplr
        ds.Tables[0].Columns[53].ColumnName = "type_code";//--styemplr
        ds.Tables[0].Columns[54].ColumnName = "PayProcess_ID";//styemplr
        ds.Tables[0].Columns[55].ColumnName = "ApplicationReferenceNo";//styemplr
                                                                       //ds.Tables[0].Columns[].ColumnName = "";//--styemplr


        //add columns to display  message and problems.
        ds.Tables[0].Columns.Add("warn_0");
        ds.Tables[0].Columns.Add("warn_1");
        ds.Tables[0].Columns.Add("warn_2");
        ds.Tables[0].Columns.Add("warn_3");
        ds.Tables[0].Columns.Add("warn_4");
        ds.Tables[0].Columns.Add("warn_5");
        ds.Tables[0].Columns.Add("warn_6");
        ds.Tables[0].Columns.Add("warn_7");
        ds.Tables[0].Columns.Add("warn_8");
        ds.Tables[0].Columns.Add("warn_9");
        ds.Tables[0].Columns.Add("warn_10");
        ds.Tables[0].Columns.Add("warn_11");
        ds.Tables[0].Columns.Add("warn_12");
        ds.Tables[0].Columns.Add("warn_13");

        ds.Tables[0].Columns.Add("Problem0");
        ds.Tables[0].Columns.Add("Problem1");
        ds.Tables[0].Columns.Add("Problem2");
        ds.Tables[0].Columns.Add("Problem3");
        ds.Tables[0].Columns.Add("Problem4");
        ds.Tables[0].Columns.Add("Problem5");
        ds.Tables[0].Columns.Add("Problem6");
        ds.Tables[0].Columns.Add("Problem7");
        ds.Tables[0].Columns.Add("Problem8");
        ds.Tables[0].Columns.Add("Problem9");

        ds.Tables[0].Columns.Add("err_0");
        ds.Tables[0].Columns.Add("err_1");
        ds.Tables[0].Columns.Add("err_2");
        ds.Tables[0].Columns.Add("err_3");
        ds.Tables[0].Columns.Add("err_4");
        ds.Tables[0].Columns.Add("err_5");

        // Column Added for form only 
        ds.Tables[0].Columns.Add("post_seq");
        ds.Tables[0].Columns.Add("basicSalary");
        ds.Tables[0].Columns.Add("oiTaxable");
        ds.Tables[0].Columns.Add("oiNonTaxable");

        ds.Tables[0].Columns.Add("bonus_check");
        ds.Tables[0].Columns.Add("ded_total");
        ds.Tables[0].Columns.Add("txt_doc_no");

        ds.Tables[0].Columns.Add("grand_basic");
        ds.Tables[0].Columns.Add("grand_oi_tax");
        ds.Tables[0].Columns.Add("grand_oi_nontax");
        ds.Tables[0].Columns.Add("grand_ded");
        ds.Tables[0].Columns.Add("ok_to_post");

        ds.Tables[0].Columns.Add("ded_ssd");
        ds.Tables[0].Columns.Add("ded_ssl");
        ds.Tables[0].Columns.Add("obl_ssd");
        ds.Tables[0].Columns.Add("obl_ssib");
        ds.Tables[0].Columns.Add("obl_ssd1");
        ds.Tables[0].Columns.Add("obl_ssib1");

        ds.Tables[0].Columns.Add("pay_code");
        ds.Tables[0].Columns.Add("code_desc");
        ds.Tables[0].Columns.Add("pay_acct_no");
        ds.Tables[0].Columns.Add("pay_dept");
        ds.Tables[0].Columns.Add("pay_amount");
        ds.Tables[0].Columns.Add("pay_lo_amt");
        ds.Tables[0].Columns.Add("pay_hi_amt");
        ds.Tables[0].Columns.Add("flexdept");  // TYPE LIKE PayrollGLAccounts.keyvalue
        ds.Tables[0].Columns.Add("flexdeptdesc");
        ds.Tables[0].Columns.Add("cash_acct_key");
        ds.Tables[0].Columns.Add("CurrentPeriod");
        ds.Tables[0].Columns.Add("CurrentYear");
        //Changes Incurred By Rohit to get New Exception Report Format for HR ,Specially changes needed for Wages
        ds.Tables[0].Columns.Add("IncomeCodeREG");
        ds.Tables[0].Columns.Add("IncomeRateREG");
        ds.Tables[0].Columns.Add("IncomeHoursREG");
        ds.Tables[0].Columns.Add("IncomeCodeOT");
        ds.Tables[0].Columns.Add("IncomeRateOT");
        ds.Tables[0].Columns.Add("IncomeHoursOT");
        ds.Tables[0].Columns.Add("IncomeCodeDT");
        ds.Tables[0].Columns.Add("IncomeRateDT");
        ds.Tables[0].Columns.Add("IncomeHoursDT");
        #endregion Set the column name..and add columns for message and problems......

        if (ds.Tables[0].Rows.Count > 0)
        {

          int doc_no = 0;
          string empl_code = string.Empty;
          string soc_sec_no = string.Empty;
          string type_code = string.Empty;
          System.Collections.ArrayList alTypeCode = new System.Collections.ArrayList();
          foreach (DataRow dr in ds.Tables[0].Rows)
          {
            doc_no = dr["doc_no"] != DBNull.Value ? Convert.ToInt32(dr["doc_no"]) : 0;
            empl_code = dr["empl_code"] != DBNull.Value ? Convert.ToString(dr["empl_code"]).Trim() : string.Empty;
            soc_sec_no = dr["soc_sec_num"] != DBNull.Value ? Convert.ToString(dr["soc_sec_num"]).Trim() : string.Empty;
            type_code = dr["type_code"] != DBNull.Value ? Convert.ToString(dr["type_code"]).Trim() : string.Empty;

            if (arrDocNo.Length > 0)
              arrDocNo += "," + doc_no.ToString();
            else
              arrDocNo = doc_no.ToString();

            if (SearchedEmployeeList.Trim().Length > 0)
              SearchedEmployeeList += ",";
            SearchedEmployeeList += "'" + empl_code + "'";

            if (SearchedSocSecNo.Trim().Length > 0)
              SearchedSocSecNo += ",";
            SearchedSocSecNo += "'" + soc_sec_no + "'";

            if (alTypeCode.Count == 0)
            {
              alTypeCode.Add(type_code.Trim());
              SearchedEmployeeTypes += type_code.Trim();
            }
            else
            {
              if (!alTypeCode.Contains(type_code))
              {
                SearchedEmployeeTypes += "," + type_code.Trim();
                alTypeCode.Add(type_code.Trim());
              }

            }

            //if (SearchedEmployeeTypes.Trim().Length <= 0)
            //    SearchedEmployeeTypes += ",";
            //if (SearchedEmployeeTypes.IndexOf("," + type_code.Trim() + ",") < 0)
            //    SearchedEmployeeTypes += type_code.Trim();
            //if (SearchedEmployeeTypes.IndexOf(",") == 0)
            //    SearchedEmployeeTypes = SearchedEmployeeTypes.Substring(1);
            //if (SearchedEmployeeTypes.LastIndexOf(",") == SearchedEmployeeTypes.Length - 1)
            //    SearchedEmployeeTypes = SearchedEmployeeTypes.Substring(0, SearchedEmployeeTypes.Length - 1);
          }
          dsAmounts = objDalBaseClass.GetData((new DVOExceptionReports()).GetPayAmounts(ref parameter));
          if (dsAmounts.Tables.Count > 0)
          {
            dsAmounts.Tables[0].Columns[0].ColumnName = "const";
            dsAmounts.Tables[0].Columns[1].ColumnName = "doc_no";
            dsAmounts.Tables[0].Columns[2].ColumnName = "amount";

          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return ds;
    }

    public void on_every_row(ref object objTransection, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DVOPayrollProcess_PayEmployee newGlobalObjDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee newGlobalObjDVOMasterEmployee, DataTable objDataTable, DataRow dr, out int status, string CHECK_POST, int new_doc_no, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      //DataRow drn = objDataTable.NewRow();
      status = 0;
      int old_cash_acct = 0;
      decimal old_cash_amount = 0;
      // post the document to stytranr, styactvd, stgtranr, and styacvtd
      // post the check   
      try
      {
        //used to determine the payroll department for each employee
        //For calling this function set the properties value of
        //EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
        DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
        objDVOFlexSegCommon.EntityType = "styemplr";
        objDVOFlexSegCommon.Code = dr["empl_code"].ToString().Trim();
        objDVOFlexSegCommon.AccountType = dr["flexdeptaccttype"].ToString().Trim();
        string keyvalue = Flexseg_Load(ref objDVOFlexSegCommon);
        if (keyvalue != "")
        {
          dr["flexdept"] = keyvalue;
          //int AccountNumber = 0;
          //string AccountType = string.Empty;
          //int AccountTypeId = 0;
          //string AccountDescription = string.Empty;

          //BLLCommonUtilities.GetAccountInformation(keyvalue, out AccountNumber, out AccountType, out AccountTypeId, out AccountDescription);
          dr["flexdeptdesc"] = GetDepartmentinfo(keyvalue);
        }

        #region Post Check Amount..

        //Determine if the employee uses direct deposit.  We cannot use the flag
        //Process_PayEmployee.deposit because o_dposit sets it to "N" after creating the
        //direct deposit entries.
        DataRow[] draDDAccount = dsDDAmount.Tables[0].Select("empl_code='" + GlobalObjDVOPayrollProcess_PayEmployee.EmplCode.Trim() + "'" + " and pay_doc_no=" + GlobalObjDVOPayrollProcess_PayEmployee.Doc_no);
        //if the payroll document is not linked to a direct deposit entry, then the
        //employee does not use direct deposit.  Post the check as usual.    

        if (draDDAccount.Length > 0)
        {   // if employee has direct deposit then save the old values........
          foreach (DataRow draccount in draDDAccount)
          {
            dr["cash_acct_no"] = Convert.ToInt32(draccount[0]);
            dr["cash_amount"] = Convert.ToDecimal(draccount[1]);
            if (!chk_post(ref objTransection, ref objDVOPostGLGlobal, ref newGlobalObjDVOPayrollProcess_PayEmployee, ref newGlobalObjDVOMasterEmployee, objGloabalPayDefaultsListStycntrc[0], objDataTable, ref dr, out status, CHECK_POST, new_doc_no, out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum, ref GLSumTable))
            {
              if (CHECK_POST == "POST")
              {
                throw new Exception("Posting has been terminated, " + py_description + " " + objDVOPostGLGlobal.description);
              }

            }
          }
          //restore the values
          dr["cash_acct_no"] = old_cash_acct;
          dr["cash_amount"] = old_cash_amount;
        }
        else
        {
          //employee does not use direct deposit.  Post the check as usual
          if (!chk_post(ref objTransection, ref objDVOPostGLGlobal, ref newGlobalObjDVOPayrollProcess_PayEmployee, ref newGlobalObjDVOMasterEmployee, objGloabalPayDefaultsListStycntrc[0], objDataTable, ref dr, out status, CHECK_POST, new_doc_no, out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum, ref GLSumTable))
          {
            if (CHECK_POST == "POST")
            {
              throw new Exception(py_description + " " + objDVOPostGLGlobal.description);
            }
          }
        }
        #endregion

        //post the income
        string err_desc = string.Empty;
        if (!inc_post(ref objTransection, out err_desc, ref objDVOPostGLGlobal, ref dr, CHECK_POST, ref GLSumTable, ref ObjDetailTable))
        {
          if (CHECK_POST == "POST")
          {
            throw new Exception(err_desc);
          }
        }
        //post the deduction
        if (!ded_post(ref objTransection, out err_desc, ref objDVOPostGLGlobal, ref dr, CHECK_POST, ref GLSumTable, ref ObjDetailTable))
        {
          if (CHECK_POST == "POST")
          {
            throw new Exception(err_desc);
          }
        }
        //post the obligation
        if (!obl_post(ref objTransection, out err_desc, ref objDVOPostGLGlobal, ref dr, CHECK_POST, ref GLSumTable, ref ObjDetailTable))
        {
          if (CHECK_POST == "POST")
          {
            throw new Exception(err_desc);
          }
        }
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;

      }

    }

    public bool chk_post(ref object objTransection, ref DVOPostGLGlobal objNewDVOPostGLGlobal, ref DVOPayrollProcess_PayEmployee newGlobalObjDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee newGlobalObjDVOMasterEmployee, DVOUpdatePayDefaults objDVOUpdatePayDefaultsstycntrc, DataTable objDataTable, ref DataRow dr, out int status, string CHECK_POST, int new_doc_no, out int py_status, out string py_description, out string py_installed, ref decimal py_c_accum, ref decimal py_i_accum, ref decimal py_d_accum, ref decimal py_ox_accum, ref decimal py_ol_accum, ref DataTable GLSumTable)
    {
      //This function calls py_post and gl_post to post the check to
      //styactvd, stgtranr, and stgactvd.
      try
      {
        //Initializing out parameter
        py_status = 0;
        py_description = "";
        py_installed = "";

        status = 0;

        string db_cr = string.Empty;
        // make sure a check number has been assigned and printed

        //if (dr["check_no"] != DBNull.Value)
        //{
        //    if (dr["check_no"].ToString().Trim() == string.Empty)
        //    {
        //        dr["check_no"] = 0;
        //    }
        //}
        //else
        //    dr["check_no"] = 0;

        //if ((dr["print_check"].ToString().Trim() == "N" && Convert.ToInt32(dr["check_no"]) != 0) && dr["deposit"].ToString().Trim() == "N")
        //{
        //    dr["ok_to_post"] = false;
        //    dr["err_3"] = "**** Error: No check number and/or document has not been printed/deposited.";
        //    err_msg="Error: No check number and/or document has not been printed/deposited.";
        //}
        //else
        //{
        //    if (CHECK_POST == "POST")
        //    {
        //        dr["ok_to_post"] = false;
        //        py_description = "Error: No check number and/or document has not been printed/deposited.";
        //        return false;
        //    }
        //}
        ////make sure values are valid
        //if (dr["pay_date"] == DBNull.Value)
        //    dr["pay_date"] = dr["doc_date"];
        //if (dr["eop_date"] == DBNull.Value)
        //    dr["eop_date"] = dr["pay_date"];
        if (dr["cash_acct_no"] == DBNull.Value)
        {
          dr["cash_acct_no"] = dr["emp_cash_acct"]; //still did not get this column
          if (dr["cash_acct_no"] == DBNull.Value)
          {
            dr["cash_acct_no"] = dr["py_cash_acct"];
            if (dr["cash_acct_no"] == DBNull.Value)
            {
              //status = 1;
              dr["ok_to_post"] = false;
              dr["err_2"] = "**** Error: No cash account number.";
              err_msg = "Error: No cash account number.";


            }
          }
        }
        if (dr["department"] == DBNull.Value)
          dr["department"] = dr["emp_department"]; //still did not get this column               
                                                   //Call py_post...
        py_post(ref objTransection, objDVOUpdatePayDefaultsstycntrc, CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString(), "PAYROLL ENTRY", dr["check_no"].ToString(), "CHECK", "A", Convert.ToDecimal(dr["cash_amount"]), Convert.ToInt32(dr["cash_acct_no"]), dr["department"].ToString(), Convert.ToDateTime(dr["eop_date"]), "0", Convert.ToDecimal(dr["total_hours"]), "0", "0", out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum);
        //if the amount is negative, reverse the sense of the debit/credit
        //and reverse the amount
        db_cr = "C";
        decimal amount = Convert.ToDecimal(dr["cash_amount"]);
        if (amount < 0)
        {

          db_cr = "D";
          amount = amount * (-1);
        }
        // post check to general ledger if stycntrc.post_gl field is set to Y

        if (objDVOUpdatePayDefaultsstycntrc.post_gl == "Y")
        {
          //insert the required information into the temporary table to be printed
          // after each payroll department
          //IF exceptions
          //   THEN
          //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
          //   END IF
          // CHECK_POST, "PY", Convert.ToInt32(dr["txt_doc_no"].ToString()), Convert.ToInt32(dr["post_seq"].ToString()), DateTime.Now, Convert.ToDateTime(dr["doc_date"].ToString()), Convert.ToDateTime(dr["pay_date"].ToString()), dr["empl_code"].ToString(), "PAYROLL ENTRY", dr["check_no"].ToString(), "CHECK", "A", Convert.ToDecimal(dr["cash_amount"].ToString()), Convert.ToInt32(dr["cash_acct_no"].ToString()), dr["department"].ToString(), Convert.ToDateTime(dr["eop_date"].ToString()), "", dr["total_hours"].ToString(), "", "", out py_status, out py_description))
          //string post_or_check, string orig_journal, int doc_no, int post_no, DateTime post_date, DateTime doc_date, DateTime pay_date, string ref_code, string doc_desc, string check_no, string act_code, string act_type, decimal amount, int acct_no, string dept_code, DateTime eop_date, string number, decimal hours, string rate, string line_no, out int py_status, out string py_description

          //        if not gl_post(check_post, "PY", new_doc_no, post_no, today,
          //rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY", rpt.check_no,
          //rpt.cash_acct_no, rpt.department, amount, db_cr)                 
          DVOPostGL objDVOPostGL = new DVOPostGL();
          objDVOPostGL.post_or_check = CHECK_POST;
          objDVOPostGL.orig_journal = "PY";
          objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
          objNewDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
          objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
          objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
          objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
          objDVOPostGL.ref_code = dr["empl_code"].ToString();
          objDVOPostGL.doc_desc = "PAYROLL ENTRY";
          objDVOPostGL.inv_chk_no = dr["check_no"].ToString();
          objDVOPostGL.acct_no = Convert.ToInt32(dr["cash_acct_no"].ToString());
          objDVOPostGL.amount = amount;
          objDVOPostGL.debit_credit = db_cr;
          objNewDVOPostGLGlobal = Gl_post(ref objDVOPostGL, ref objNewDVOPostGLGlobal, ref objTransection);
          if (objNewDVOPostGLGlobal.sql_error != 0)
          {
            if (CHECK_POST == "POST")
            {
              objNewDVOPostGLGlobal.description = "**** Error:" + "An SQL Error has occured while posting into GL";
              return false;
            }
          }
          if (objNewDVOPostGLGlobal.status == 0 || objNewDVOPostGLGlobal.status == 3)
          {
          }
          else
          {
            //status = 1;
            dr["ok_to_post"] = false;
            dr["err_3"] = "**** Error: " + objNewDVOPostGLGlobal.description;
            dr["Problem3"] = "*****Error has been detected on this report.";
            err_msg = "Error has been detected on this report.";
            if (CHECK_POST == "POST")
            {
              return false;
            }
          }

          #region Collect data for GL Summary ..........
          DataRow drGL = GLSumTable.NewRow();
          drGL["doc_no"] = objDVOPostGL.doc_no;
          drGL["doc_date"] = objDVOPostGL.doc_date;
          drGL["acctno"] = objDVOPostGL.acct_no;
          drGL["amount"] = objDVOPostGL.amount;
          drGL["debit_credit"] = objDVOPostGL.debit_credit;
          drGL["flexdept"] = dr["flexdept"];
          //string keyvalue = string.Empty;
          //int id = 0;
          //string acct_type = string.Empty;
          //string acct_desc = string.Empty;
          //int acct_no = objDVOPostGL.acct_no;
          //BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
          string acct_type = string.Empty;
          string keyvalue = string.Empty;
          int acct_no = objDVOPostGL.acct_no;
          keyvalue = GetAccountInfo(acct_no, out acct_type);
          drGL["keyvalue"] = keyvalue;
          drGL["acct_desc"] = acct_type;
          GLSumTable.Rows.Add(drGL.ItemArray);
          #endregion
        }
        else
        {
          dr["ok_to_post"] = false;
          dr["err_3"] = "****Error: Cannot post into GL because stycntrc.post_gl field is not set to Y";
          dr["Problem3"] = "*****Error has been detected on this report.";
          objNewDVOPostGLGlobal.description = "Error: Cannot post into GL because stycntrc.post_gl field is not set to Y";
          err_msg = objNewDVOPostGLGlobal.description;
          return false;
        }


      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public void py_post(ref object objTransection, DVOUpdatePayDefaults objPaydefaultsStycntrc, string post_or_check, string orig_journal, int doc_no, int post_no, DateTime post_date, DateTime doc_date, DateTime pay_date, string ref_code, string doc_desc, string check_no, string act_code, string act_type, decimal amount, int acct_no, string dept_code, DateTime eop_date, string number, decimal? hours, string rate, string line_no, out int py_status, out string py_description, out string py_installed, ref decimal py_c_accum, ref decimal py_i_accum, ref decimal py_d_accum, ref decimal py_ox_accum, ref decimal py_ol_accum)
    {
      py_status = 0;
      py_description = "";
      py_installed = "";
      try
      {
        switch (act_type)
        {
          case "A":
            py_c_accum += amount;
            break;
          case "B":
            py_i_accum += amount;
            break;
          case "C":
            py_d_accum += amount;
            break;
          case "D":
            py_ox_accum += amount;
            break;
          case "E":
            py_ol_accum += amount;
            break;
          default:
            break;
        }

        //return true on check
        if (post_or_check != "POST")
        {
          return;
        }
        // Insert the styactvd row
        int RetvalIns2 = Insert2_In_styactvd(ref objTransection, orig_journal, doc_no, act_code, act_type, Convert.ToDecimal(amount), Convert.ToDecimal(number), Convert.ToDecimal(hours), Convert.ToDecimal(rate), Convert.ToInt32(acct_no), dept_code);
        if (RetvalIns2 != 1)
        {
          throw new Exception("Cannot insert a new document into styactvd.");
        }
        #region Update accumulation
        //********************************************************
        //update accumulation for the income ,deduction and obligation... 
        //Commented by sarvjeet on 09/02/2010...

        //Calculate Quarter Number
        //quarter = qtr_number(pay_date);
        //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();



        //switch (act_type)
        //{
        //    case "B":
        //        {
        //            #region Update Income Accumulation when accountType=B

        //            int retvalue = 0;
        //            DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
        //            int rowid = GetMasterEmployeeIncomesRowId(ref_code, act_code, i_line_no);
        //            objDVOMasterEmployeeIncomes.Rowid = rowid;
        //            int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
        //            if (i == 1)
        //            {
        //                try
        //                {
        //                    switch (quarter)
        //                    {
        //                        case 1:
        //                            {
        //                                retvalue = UPDATE_INC1(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeIncomes = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update income accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                        case 2:
        //                            {
        //                                retvalue = UPDATE_INC2(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeIncomes = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update income accumulation.";
        //                                    return false;
        //                                }

        //                            }
        //                            break;
        //                        case 3:
        //                            {
        //                                retvalue = UPDATE_INC3(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeIncomes = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update income accumulation.";
        //                                    return false;
        //                                }

        //                            }
        //                            break;
        //                        case 4:
        //                            {
        //                                retvalue = UPDATE_INC4(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeIncomes = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update income accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                objDVOMasterEmployeeIncomes = null;
        //                py_status = 7;
        //                py_description = "Could not lock MasterEmployeeIncomes";
        //                return false;

        //            }
        //            #endregion Update Income Accumulation when accountType=B
        //        }
        //        break;
        //    case "C":
        //        {
        //            // update accumulation and last taken if a deduction
        //            #region Update Deduction Accumulation when accountType=C
        //            int retvalue = 0;
        //            int retval6 = 0;
        //            DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
        //            int rowid = GetMasterEmployeeDeductionsRowId(ref_code, act_code, i_line_no);
        //            objDVOMasterEmployeeDeductions.Rowid = rowid;
        //            int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
        //            if (i == 1)
        //            {
        //                try
        //                {
        //                    switch (quarter)
        //                    {
        //                        case 1:
        //                            {
        //                                retvalue = UPDATE_DED1(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                retval6 = UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1 || retval6 != 1)
        //                                {

        //                                    objDVOMasterEmployeeDeductions = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update deduction accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                        case 2:
        //                            {
        //                                retvalue = UPDATE_DED2(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                retval6 = UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1 || retval6 != 1)
        //                                {

        //                                    objDVOMasterEmployeeDeductions = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update deduction accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                        case 3:
        //                            {
        //                                retvalue = UPDATE_DED3(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                retval6 = UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1 || retval6 != 1)
        //                                {

        //                                    objDVOMasterEmployeeDeductions = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update deduction accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                        case 4:
        //                            {
        //                                retvalue = UPDATE_DED4(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                retval6 = UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1 || retval6 != 1)
        //                                {

        //                                    objDVOMasterEmployeeDeductions = null;
        //                                    py_status = 7;
        //                                    py_description = "Cannot update deduction accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                objDVOMasterEmployeeDeductions = null;
        //                py_status = 7;
        //                py_description = "Could not lock MasterEmployeeDeductions";
        //                return false;
        //            }

        //            #endregion Update Deduction Accumulation when accountType=C
        //        }
        //        break;
        //    case "D":
        //        {
        //            // update accumulation and last taken if a Obligation
        //            #region Update Deduction Accumulation when accountType=C
        //            int retvalue = 0;

        //            DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
        //            int rowid = GetMasterEmployeeObligationsRowId(ref_code, act_code, i_line_no);
        //            objDVOMasterEmployeeObligations.Rowid = rowid;
        //            int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false, false);
        //            if (i == 1)
        //            {
        //                try
        //                {
        //                    switch (quarter)
        //                    {
        //                        case 1:
        //                            {                       // UPDATE_OBL1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd1, decimal obl_ytd, string empl_code, string obl_code, int line_no)
        //                                retvalue = UPDATE_OBL1(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeObligations = null;
        //                                    py_status = 8;
        //                                    py_description = "Cannot update obligation accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                        case 2:
        //                            {
        //                                retvalue = UPDATE_OBL2(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeObligations = null;
        //                                    py_status = 8;
        //                                    py_description = "Cannot update obligation accumulation.";
        //                                    return false;
        //                                }

        //                            }
        //                            break;
        //                        case 3:
        //                            {
        //                                retvalue = UPDATE_OBL3(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeObligations = null;
        //                                    py_status = 8;
        //                                    py_description = "Cannot update obligation accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                        case 4:
        //                            {
        //                                retvalue = UPDATE_OBL4(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
        //                                BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                                if (retvalue != 1)
        //                                {
        //                                    objDVOMasterEmployeeObligations = null;
        //                                    py_status = 8;
        //                                    py_description = "Cannot update obligation accumulation.";
        //                                    return false;
        //                                }
        //                            }
        //                            break;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                objDVOMasterEmployeeObligations = null;
        //                py_status = 8;
        //                py_description = "Could not lock MasterEmployeeObligations";
        //                return false;
        //            }
        //        }
        //        break;

        //            #endregion Update Deduction Accumulation when accountType=C
        //    default:
        //        break;
        //}
        //}
        #endregion

      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }

    private bool inc_post(ref object objTransection, out string err_desc, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DataRow dr, string CHECK_POST, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      // used as a output parameter
      int py_status;
      string py_description;
      int inc_count;
      Boolean lo_flag = false;
      Boolean hi_flag = false;
      decimal pay_total = 0.0M;
      int line_number = 0;
      int prev_check = 0;
      //int empl_count = 0;
      string db_cr;
      err_desc = string.Empty;
      try
      {
        //empl_count = Get_empl_count(dr["empl_code"].ToString().Trim());
        //List<DVOPayrollstypayid> objDVOPyrollstypayid = new List<DVOPayrollstypayid>();
        //objDVOPyrollstypayid = GetIncomeDataForINC_Post(Convert.ToInt32(dr["doc_no"]));
        DataRow[] draIncomes = dsIncomes.Tables[0].Select("doc_no =" + Convert.ToInt32(dr["doc_no"]), "line_no");
        if (draIncomes.Length > 0)
        {
          foreach (DataRow drInc in draIncomes)
          {
            DVOPayrollstypayid Stypayid = new DVOPayrollstypayid();
            Stypayid.inc_code = (drInc[0] != DBNull.Value ? drInc[0].ToString().Trim().Trim() : string.Empty);
            Stypayid.description_MasterIncCodes = (drInc[1] != DBNull.Value ? drInc[1].ToString().Trim() : string.Empty);
            Stypayid.amount = (drInc[2] != DBNull.Value ? Convert.ToDecimal(drInc[2]) : 0);
            Stypayid.add_code = (drInc[3] != DBNull.Value ? drInc[3].ToString().Trim() : string.Empty);
            Stypayid.number = (drInc[4] != DBNull.Value ? Convert.ToDecimal(drInc[4]) : 0);
            Stypayid.inc_rate = (drInc[5] != DBNull.Value ? Convert.ToDecimal(drInc[5]) : 0);
            Stypayid.hours = (drInc[6] != DBNull.Value ? Convert.ToDecimal(drInc[6]) : 0);
            if (drInc[7] == DBNull.Value)
              Stypayid.lo_inc_amt_null = true;
            Stypayid.lo_inc_amt = (drInc[7] != DBNull.Value ? Convert.ToDecimal(drInc[7]) : 0);
            if (drInc[8] == DBNull.Value)
              Stypayid.hi_inc_amt_null = true;
            Stypayid.hi_inc_amt = (drInc[8] != DBNull.Value ? Convert.ToDecimal(drInc[8]) : 0);
            Stypayid.acct_no = (drInc[9] != DBNull.Value ? Convert.ToInt32(drInc[9]) : 0);
            Stypayid.Department = (drInc[10] != DBNull.Value ? drInc[10].ToString().Trim() : string.Empty);
            Stypayid.line_no = (drInc[11] != DBNull.Value ? Convert.ToInt32(drInc[11]) : 0);

            if (Stypayid.amount != 0)
            {
              lo_flag = false;
              hi_flag = false;
              if (!Stypayid.lo_inc_amt_null)
              {
                // set flag for exceptions reporting (low end)
                dr["pay_lo_amt"] = Stypayid.lo_inc_amt;  //did not get this data table column still
                if (Stypayid.amount < Stypayid.lo_inc_amt)
                {
                  lo_flag = true;
                }
              }
              if (!Stypayid.hi_inc_amt_null)
              {
                dr["pay_hi_amt"] = Stypayid.hi_inc_amt;  //did not get this data table column still
                if (Stypayid.amount > Stypayid.hi_inc_amt)
                {
                  hi_flag = true;
                }
              }

              if (pay_total == 0)
              {
                //Prepare report outline
              }
              // build details outline
              if (CHECK_POST == "CHECK")
              {

                if (Stypayid.inc_code.Trim() != string.Empty && Stypayid.amount != 0)
                {
                  if (lo_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = Stypayid.inc_code;
                    drdtl["amount"] = Stypayid.amount;
                    drdtl["lo_hi_amt"] = dr["pay_lo_amt"];
                    drdtl["lo_flag"] = true;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }
                  if (hi_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = Stypayid.inc_code;
                    drdtl["amount"] = Stypayid.amount;
                    drdtl["lo_hi_amt"] = dr["pay_hi_amt"];
                    drdtl["lo_flag"] = false;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }

                }


              }
              // build detail for EditList........
              else if (CHECK_POST == "EDIT")
              {
                DataRow drdtl = ObjDetailTable.NewRow();
                drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                drdtl["code_line"] = "0"; // Here "0" represents  Income Code.
                drdtl["pay_code"] = Stypayid.inc_code;
                drdtl["pay_desc"] = Stypayid.description_MasterIncCodes;
                drdtl["amount"] = Stypayid.amount;
                //Get Keyvalue for account........
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //if (Stypayid.acct_no == 0)
                //    Stypayid.acct_no = objGloabalPayDefaultsListStycntrc[0].exp_acct;
                //BLLCommonUtilities.GetAccountInformation(Stypayid.acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                string acct_type = string.Empty;
                string keyvalue = string.Empty;
                if (Stypayid.acct_no == 0)
                  Stypayid.acct_no = objGloabalPayDefaultsListStycntrc[0].exp_acct;
                keyvalue = GetAccountInfo(Stypayid.acct_no, out acct_type);
                drdtl["account"] = keyvalue;
                ObjDetailTable.Rows.Add(drdtl.ItemArray);

              }
              dr["pay_code"] = Stypayid.inc_code;
              dr["code_desc"] = Stypayid.description_MasterIncCodes;
              dr["pay_acct_no"] = Stypayid.acct_no;
              dr["pay_dept"] = Stypayid.Department;
              dr["pay_amount"] = Stypayid.amount;
              pay_total = pay_total + Convert.ToDecimal(dr["pay_amount"]);

              //make sure values are valid
              if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
              {
                dr["pay_acct_no"] = objGloabalPayDefaultsListStycntrc[0].exp_acct; // dr["exp_acct"];
                if (Convert.ToInt32(dr["pay_acct_no"].ToString().Trim()) == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No expense account number.";
                  err_msg = "Error: No expense account number.";
                  if (CHECK_POST == "POST")
                  {
                    dr["ok_to_post"] = false;
                    return false;
                  }
                }
              }

              if (dr["pay_dept"].ToString().Trim() == string.Empty)
              {
                dr["pay_dept"] = dr["department"].ToString().Trim();
              }

              if (CHECK_POST == "POST")
              {
                // If this is a duplicate of an existing code for this
                // employee code, just acquire information to update
                // the accumulation buckets for the first instance
                // of the code in the employee record.
                // If the code is brand new to the employee,
                // (add_code = "Y", then we adjust the line_no
                // value further down in this function.
                if (Stypayid.add_code.Trim() == "Z" || Stypayid.add_code.Trim() == "Y")
                {
                  line_number = Get_MIN_LINENUMBER(dr["empl_code"].ToString().Trim(), dr["pay_code"].ToString().Trim());
                  if (line_number != 0)
                  {
                    Stypayid.add_code = "Z";
                    Stypayid.line_no = line_number;
                  }
                }
              }

              // Post the income to payroll
              //        if not py_post(check_post, "PY", new_doc_no, post_no, today,
              //rpt.doc_date, rpt.pay_date, rpt.empl_code, "PAYROLL ENTRY",
              //rpt.check_no, rpt.pay_code, "B", rpt.pay_amount,
              //rpt.pay_acct_no, rpt.pay_dept, rpt.eop_date,
              //inc_ref.number, inc_ref.hours, inc_ref.inc_rate,
              //inc_ref.line_no)

              py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"].ToString().Trim()), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "B", Convert.ToDecimal(dr["pay_amount"]), Convert.ToInt32(dr["pay_acct_no"]), dr["pay_dept"].ToString().Trim(), Convert.ToDateTime(dr["eop_date"].ToString().Trim()), Stypayid.number.ToString().Trim(), Stypayid.hours, Convert.ToString(Stypayid.inc_rate), Convert.ToString(Stypayid.line_no), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum);

              //    if (py_status == 1)
              //    {
              //        //payroll not installed
              //        dr["ok_to_post"] = false;
              //        dr["err_1"] = "**** Error: " + py_description;
              //        err_msg = py_description;
              //        if (CHECK_POST == "POST")
              //        {
              //            err_msg = py_description;
              //            return false;
              //        }
              //    }
              //    else if (py_status == 4) //document number out of sequence
              //    {
              //        dr["err_5"] = "**** Error: " + py_description;
              //        err_msg = py_description;
              //        //return false;
              //        if (CHECK_POST == "POST")
              //        {
              //            err_desc = py_description;
              //            return false;
              //        }
              //    }
              //    else
              //    {
              //        dr["ok_to_post"] = false;
              //        dr["err_2"] = "**** Error: " + py_description;
              //        err_msg = py_description;
              //        if (CHECK_POST == "POST")
              //        {
              //            err_desc = py_description;
              //            return false;
              //        }
              //        //return false;
              //    }
              //}

              //if the amount is negative, reverse the sense of the debit/credit
              //and reverse the amount
              db_cr = "D";
              decimal amount = Convert.ToDecimal(dr["pay_amount"]);
              if (amount < 0)
              {

                db_cr = "C";
                amount = amount * (-1);
              }
              // post check to general ledger if stycntrc.post_gl field is set to Y

              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //  THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //  END IF


                //        if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, rpt.pay_acct_no, rpt.pay_dept, amount,
                //db_cr)


                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"].ToString().Trim());
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Convert.ToInt32(dr["pay_acct_no"]);
                objDVOPostGL.department = dr["pay_dept"].ToString().Trim();
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }
                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  err_msg = objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }

                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //int acct_no = objDVOPostGL.acct_no;
                //BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                string acct_desc = string.Empty;
                string keyvalue = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                keyvalue = GetAccountInfo(acct_no, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL, stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }

              // sick pay accumulation
              if (dr["pay_code"].ToString().Trim() == dr["sick_code"].ToString().Trim())
              {
                sick_accum += (Stypayid.number ?? 0);
              }
              //vacation pay accumulation
              if (dr["pay_code"].ToString().Trim() == dr["vac_code"].ToString().Trim())
              {
                vac_accum += (Stypayid.number ?? 0);
              }
              if (CHECK_POST == "POST")
              {
                // test to see if we need to add the code to the empl record
                if (Stypayid.add_code == "Y")
                {
                  // new code for the employee
                  //get array size for this employee for line_no value
                  if (prev_check != 123)
                  {
                    prev_check = 123;
                    line_number = Get_MAX_LINENUMBER(dr["empl_code"].ToString().Trim());
                    if (line_number > 0)
                    {
                      line_number += 1;
                    }
                    else
                    {
                      line_number = 0;
                    }
                  }
                  //append row to employee record for future accruals
                  // call updt_emp_inc(inc_ref.inc_code, inc_ref.acct_no,inc_ref.department, line_number, inc_ref.amount)
                  string upd_status;
                  int i = updt_emp_inc(ref objTransection, Stypayid.inc_code, (Stypayid.acct_no).ToString().Trim(), Stypayid.Department, line_number, Stypayid.amount, dr["empl_code"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), out upd_status);
                  if (i != 1)
                  {
                    dr["warn_11"] = upd_status;
                    if (CHECK_POST == "POST")
                    {
                      err_desc = upd_status;
                      return false;
                    }
                  }
                }
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }


    private bool ded_post(ref object objTransection, out string err_desc, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DataRow dr, string CHECK_POST, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      // used as a output parameter
      int py_status;
      string py_description;
      //this function prepares the report rows for the deduction data and
      //calls py_post and gl_post to post to styactvd and stgactvd.
      string db_cr;
      err_desc = string.Empty;
      int ded_count = 0;
      Boolean lo_flag = false;
      Boolean hi_flag = false;
      decimal pay_total = 0.0M;
      int line_number = 0;
      int prev_check = 0;
      string nss_ded_code = string.Empty;
      DVOExceptionReports objDvoExpReports = new DVOExceptionReports();
      try
      {
        //empl_count = Get_empl_countdd(dr["empl_code"].ToString().Trim());
        //List<DVOPayrollstypaydd> objDVOPyrollstypaydd = new List<DVOPayrollstypaydd>();
        //set the deduction row variables

        //objDVOPyrollstypaydd = GetIncomeDataForDED_Post(Convert.ToInt32(dr["doc_no"]));

        DVOPayrollstypaydd Stypaydd = null;
        DataRow[] draDeductions = dsDeductions.Tables[0].Select("doc_no =" + Convert.ToInt32(dr["doc_no"]), "line_no");

        if (draDeductions.Length > 0)
        {
          foreach (DataRow drDed in draDeductions)
          {
            Stypaydd = new DVOPayrollstypaydd();
            Stypaydd.ded_code = (drDed[0] != DBNull.Value ? drDed[0].ToString().Trim() : string.Empty);
            Stypaydd.description_MasterIncCodes = (drDed[1] != DBNull.Value ? drDed[1].ToString().Trim() : string.Empty);
            Stypaydd.amount = (drDed[2] != DBNull.Value ? Convert.ToDecimal(drDed[2]) : 0);
            Stypaydd.add_code = (drDed[3] != DBNull.Value ? drDed[3].ToString().Trim() : string.Empty);
            Stypaydd.ded_rate = (drDed[4] != DBNull.Value ? Convert.ToDecimal(drDed[4]) : 0);
            if (drDed[5] == DBNull.Value)
              Stypaydd.lo_ded_amt_null = true;
            Stypaydd.lo_ded_amt = (drDed[5] != DBNull.Value ? Convert.ToDecimal(drDed[5]) : 0);
            if (drDed[6] == DBNull.Value)
              Stypaydd.hi_ded_amt_null = true;
            Stypaydd.hi_ded_amt = (drDed[6] != DBNull.Value ? Convert.ToDecimal(drDed[6]) : 0);
            Stypaydd.acct_no = (drDed[7] != DBNull.Value ? Convert.ToInt32(drDed[7]) : 0);
            Stypaydd.Department = (drDed[8] != DBNull.Value ? drDed[8].ToString().Trim() : string.Empty);
            Stypaydd.line_no = (drDed[9] != DBNull.Value ? Convert.ToInt32(drDed[9]) : 0);
            if (Stypaydd.amount != 0)
            {
              lo_flag = false;
              hi_flag = false;
              if (!Stypaydd.lo_ded_amt_null)
              {
                // set flag for exceptions reporting (low end)
                dr["pay_lo_amt"] = Stypaydd.lo_ded_amt;  //did not get this data table column still
                if (Stypaydd.amount < Stypaydd.lo_ded_amt)
                {
                  lo_flag = true;
                }
              }
              if (!Stypaydd.hi_ded_amt_null)
              {
                dr["pay_hi_amt"] = Stypaydd.hi_ded_amt;  //did not get this data table column still
                if (Stypaydd.amount > Stypaydd.hi_ded_amt)
                {
                  hi_flag = true;
                }
              }
              if (pay_total == 0)
              {
                //Prepare report outline
              }

              // build details for exception report....

              if (CHECK_POST == "CHECK")
              {
                if (Stypaydd.ded_code.Trim() != string.Empty && Stypaydd.amount != 0)
                {
                  if (lo_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = Stypaydd.ded_code;
                    drdtl["amount"] = Stypaydd.amount;
                    drdtl["lo_hi_amt"] = dr["pay_lo_amt"];
                    drdtl["lo_flag"] = true;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }
                  if (hi_flag)
                  {
                    DataRow drdtl = ObjDetailTable.NewRow();
                    drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                    drdtl["pay_code"] = Stypaydd.ded_code;
                    drdtl["amount"] = Stypaydd.amount;
                    drdtl["lo_hi_amt"] = dr["pay_hi_amt"];
                    drdtl["lo_flag"] = false;
                    ObjDetailTable.Rows.Add(drdtl.ItemArray);
                  }

                }
              }
              // build detail for EditList........
              else if (CHECK_POST == "EDIT")
              {
                DataRow drdtl = ObjDetailTable.NewRow();
                drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                drdtl["code_line"] = "1"; // Here "0" represents  Deduction Code.
                drdtl["pay_code"] = Stypaydd.ded_code.Trim();
                drdtl["pay_desc"] = Stypaydd.description_MasterIncCodes.Trim();
                drdtl["amount"] = Stypaydd.amount;
                //Get Keyvalue for account........
                string acct_desc = string.Empty;
                string keyvalue = string.Empty;
                if (Stypaydd.acct_no == 0)
                  Stypaydd.acct_no = objGloabalPayDefaultsListStycntrc[0].liab_acct;
                keyvalue = GetAccountInfo(Stypaydd.acct_no, out acct_desc);
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //if (Stypaydd.acct_no == 0)
                //    Stypaydd.acct_no = objGloabalPayDefaultsListStycntrc[0].liab_acct;
                //BLLCommonUtilities.GetAccountInformation(Stypaydd.acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                drdtl["account"] = keyvalue;
                ObjDetailTable.Rows.Add(drdtl.ItemArray);

              }
              dr["pay_code"] = Stypaydd.ded_code;
              dr["code_desc"] = Stypaydd.description_MasterIncCodes;
              dr["pay_acct_no"] = Stypaydd.acct_no;
              dr["pay_dept"] = Stypaydd.Department;
              dr["pay_amount"] = Stypaydd.amount;
              pay_total = pay_total + Convert.ToDecimal(dr["pay_amount"]);

              //make sure values are valid
              if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
              {
                dr["pay_acct_no"] = objGloabalPayDefaultsListStycntrc[0].liab_acct;// dr["liab_acct"];   // didn't get the column dr["liab_acct"];
                if (Convert.ToInt32(dr["pay_acct_no"].ToString().Trim()) == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No liablility account number.";
                  err_msg = "Error: No liablility account number.";
                  //dr["Problem2"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "No liablility account number.";
                    return false;
                  }
                }
              }

              if (dr["pay_dept"].ToString().Trim() == string.Empty)
              {
                dr["pay_dept"] = dr["department"].ToString().Trim();   //rpt.emp_department
              }

              if (CHECK_POST == "POST")
              {
                // If this is a duplicate of an existing code for this
                // employee code, just acquire information to update
                // the accumulation buckets for the first instance
                // of the code in the employee record.
                // If the code is brand new to the employee,
                // (add_code = "Y", then we adjust the line_no
                // value further down in this function.
                if (Stypaydd.add_code == "Z" || Stypaydd.add_code == "Y")
                {
                  line_number = Get_MIN_LINENUMBERDD(dr["empl_code"].ToString().Trim(), dr["pay_code"].ToString().Trim());
                  if (line_number != 0)
                  {
                    Stypaydd.add_code = "Z";
                    Stypaydd.line_no = line_number;
                  }
                }
              }
              // Commented by Sarvjeet Verma On 03/06/2009.
              //**********Need to be implemented ************************************************************
              //if amount submitted to be posted is greater than or less than allowed amount,then have to post
              //allowed amount only,in such case have to maintain record of remaining amount.
              //remaining amount should go into any other account like suspense account.   
              //***************************************************************************************
              #region Post Nss Deduction........
              int status = 0;
              decimal nss_amount = 0;
              // # lets post the deduction code to the nss if required
              //Get Nss Deduction Code from nsscontrol table.........
              //if (nss_ded_code == string.Empty)
              // nss_ded_code = get_nss_ded_code();
              //make sure this  deduction is Nss deduction ...
              if (dr["pay_code"].ToString().Trim() == nss_ded_code)
              {
                // everything went ok with the deduction
                //lets post the deduction code if is part of the nss      
                //if (!nss_check_post(ref objTransection, CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"].ToString().Trim()), DateTime.Now, dr["empl_code"].ToString().Trim(), Convert.ToDecimal(dr["pay_amount"]), "NSS PAYROLL", dr["pay_code"].ToString().Trim(), Convert.ToInt32(Stypaydd.line_no.ToString().Trim()), out status, out nss_amount, out err_desc))
                //{
                //    if (CHECK_POST == "POST")
                //    {
                //        dr["ok_to_post"] = false;
                //        return false;
                //    }
                //    else
                //    {
                //        dr["ok_to_post"] = false;
                //        dr["err_1"] = err_desc;
                //    }
                //}
                //else
                //{
                //    if (status == 1)
                //        dr["warn_5"] = err_desc;
                //    dr["pay_amount"] = nss_amount;
                //}
              }
              #endregion


              // post the deduction to payroll
              py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "C", Convert.ToDecimal(dr["pay_amount"]), Convert.ToInt32(dr["pay_acct_no"]), dr["pay_dept"].ToString().Trim(), Convert.ToDateTime(dr["eop_date"]), "0", 0, Stypaydd.ded_rate.ToString().Trim(), Stypaydd.line_no.ToString().Trim(), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum);


              // # Update the balance of any year rollover type of deduction code (ie. codes that are not zeroed out
              // # at the end of th year and continue until the balance is zero such as for load repayments)
              if (Stypaydd.yearrollover == "Y")
              {
                //Get the current employee's balance if any
                DataSet dsAccountBal = Get_DED_BALANCE(dr["pay_code"].ToString().Trim(), dr["empl_code"].ToString().Trim());
                if (dsAccountBal.Tables[0].Rows.Count > 0)
                {
                  if (dsAccountBal.Tables[0].Rows[0][1] != DBNull.Value)
                  {
                    if (Convert.ToDecimal(dsAccountBal.Tables[0].Rows[0][1]) > 0)
                    {
                      // Now subtract the amount reducing the value of the balance
                      int updBalRetvalue = UPDATE_DED_BALANCE(ref objTransection, ref objDvoExpReports, Convert.ToInt32(dsAccountBal.Tables[0].Rows[0][0]), Convert.ToDecimal(dr["pay_amount"]));
                      if (updBalRetvalue != 1)
                      {
                        //RollBack Transection
                        if (CHECK_POST == "POST")
                        {
                          err_desc = "An SQL Error has occurred while updating MasterEmployeeDeductions";
                          return false;
                        }
                      }

                    }

                  }
                }
              }



              //if the amount is negative, reverse the sense of the debit/credit
              //and reverse the amount
              db_cr = "C";
              decimal amount = Convert.ToDecimal(dr["pay_amount"]);
              if (amount < 0)
              {

                db_cr = "D";
                amount = amount * (-1);
              }
              // post check to general ledger if stycntrc.post_gl field is set to Y

              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //   THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //   END IF


                //        if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, rpt.pay_acct_no, rpt.pay_dept, amount,
                //db_cr)


                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Convert.ToInt32(dr["pay_acct_no"]);
                objDVOPostGL.department = dr["pay_dept"].ToString().Trim();
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }

                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  err_msg = objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }
                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //int acct_no = objDVOPostGL.acct_no;
                //BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                string acct_desc = string.Empty;
                string keyvalue = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                keyvalue = GetAccountInfo(acct_no, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                //dr["Problem7"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL,stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }

              // check for federal tax deduction
              if (Stypaydd.ded_code == objGloabalPayDefaultsListStycntrc[0].fedtax_code) //// dr["fedtax_code"].ToString().Trim()
              {
                dftax_flag = true;
              }
              //check for fica deduction

              if (Stypaydd.ded_code == objGloabalPayDefaultsListStycntrc[0].fica_code)//dr["fica_code"].ToString().Trim()
              {
                dfica_flag = true;
                fica_ded = fica_ded + Stypaydd.amount ?? 0; //make nullable decimal By Rahul
              }

              //check for medicare deduction
              if (Stypaydd.ded_code == objGloabalPayDefaultsListStycntrc[0].medicare_code)//dr["medicare_code"].ToString().Trim()
              {
                dmedicare_flag = true;
                medcr_ded = medcr_ded + Stypaydd.amount ?? 0;//make nullable decimal By Rahul
              }


              if (CHECK_POST == "POST")
              {
                // test to see if we need to add the code to the empl record
                if (Stypaydd.add_code == "Y")
                {
                  // new code for the employee
                  //get array size for this employee for line_no value
                  if (prev_check != 123)
                  {
                    prev_check = 123;
                    line_number = Get_MAX_LINENUMBERDD(dr["empl_code"].ToString().Trim());
                    if (line_number > 0)
                    {
                      line_number += 1;
                    }
                    else
                    {
                      line_number = 0;
                    }
                  }
                  //append row to employee record for future accruals
                  //         call updt_emp_ded(ded_ref.ded_code, ded_ref.acct_no,
                  //ded_ref.department, line_number, ded_ref.amount)
                  string upd_status;
                  //make nullable decimal By Rahul
                  int i = updt_emp_ded(ref objTransection, Stypaydd.ded_code, Stypaydd.acct_no.ToString().Trim(), Stypaydd.Department, line_number, Stypaydd.amount ?? 0, dr["empl_code"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), Convert.ToDateTime(dr["eop_date"].ToString().Trim()), out upd_status);
                  if (i != 1)
                  {
                    dr["warn_11"] = upd_status;
                    err_desc = upd_status;
                    return false;
                  }
                }
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private bool obl_post(ref object objTransection, out string err_desc, ref DVOPostGLGlobal objDVOPostGLGlobal, ref DataRow dr, string CHECK_POST, ref DataTable GLSumTable, ref DataTable ObjDetailTable)
    {
      // used as a output parameter
      int py_status;
      string py_description;

      decimal amount = 0.0M; // like stypayod.amount
      decimal pay_total = 0.0M;  //like stypayod.amount,
      string db_cr;
      int line_number = 0;
      int prev_check = 0;
      int empl_count = 0;
      int obl_count = 0;
      err_desc = string.Empty;
      try
      {
        //List<DVOPayrollStypayod> objDVOPayrollStypayod = new List<DVOPayrollStypayod>();
        //objDVOPayrollStypayod = GetObligationDataForOBL_Post(Convert.ToInt32(dr["doc_no"]));
        DataRow[] draObligations = dsObligations.Tables[0].Select("doc_no =" + Convert.ToInt32(dr["doc_no"]), "line_no");
        if (draObligations.Length > 0)
        {
          foreach (DataRow drObl in draObligations)
          {
            DVOPayrollStypayod Stypayod = new DVOPayrollStypayod();
            Stypayod.obl_code = (drObl[0] != DBNull.Value ? drObl[0].ToString().Trim() : string.Empty);
            Stypayod.description_MasterIncCodes = (drObl[1] != DBNull.Value ? drObl[1].ToString().Trim() : string.Empty);
            Stypayod.amount = (drObl[2] != DBNull.Value ? Convert.ToDecimal(drObl[2]) : 0);
            Stypayod.obl_rate = (drObl[3] != DBNull.Value ? Convert.ToDecimal(drObl[3]) : 0);
            Stypayod.acct_no = (drObl[4] != DBNull.Value ? Convert.ToInt32(drObl[4]) : 0);
            Stypayod.Department = (drObl[5] != DBNull.Value ? drObl[5].ToString().Trim() : string.Empty);
            Stypayod.bal_acct_no = (drObl[6] != DBNull.Value ? Convert.ToInt32(drObl[6]) : 0);
            Stypayod.bal_dept = (drObl[7] != DBNull.Value ? drObl[7].ToString().Trim() : string.Empty);
            Stypayod.line_no = (drObl[8] != DBNull.Value ? Convert.ToInt32(drObl[8]) : 0);
            Stypayod.add_code = (drObl[9] != DBNull.Value ? drObl[9].ToString().Trim() : string.Empty);

            if (Stypayod.amount != 0)
            {
              // build details outline
              dr["pay_code"] = Stypayod.obl_code;
              dr["code_desc"] = Stypayod.description_MasterIncCodes;
              dr["pay_acct_no"] = Stypayod.acct_no;
              dr["pay_dept"] = Stypayod.Department;
              dr["pay_amount"] = Stypayod.amount;
              pay_total = pay_total + Convert.ToDecimal(dr["pay_amount"]);
              // build detail for EditList........
              if (CHECK_POST == "EDIT")
              {
                DataRow drdtl = ObjDetailTable.NewRow();
                drdtl["doc_no"] = Convert.ToInt32(dr["doc_no"]);
                drdtl["code_line"] = "2"; // Here "0" represents  Obligation Code.
                drdtl["pay_code"] = Stypayod.obl_code;
                drdtl["pay_desc"] = Stypayod.description_MasterIncCodes;
                drdtl["amount"] = Stypayod.amount;
                //Get Keyvalue for account........
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //if (Stypayod.acct_no == 0)
                //    Stypayod.acct_no = objGloabalPayDefaultsListStycntrc[0].exp_acct;
                //BLLCommonUtilities.GetAccountInformation(Stypayod.acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                string acct_desc = string.Empty;
                string keyvalue = string.Empty;
                if (Stypayod.acct_no == 0)
                  Stypayod.acct_no = objGloabalPayDefaultsListStycntrc[0].exp_acct;
                keyvalue = GetAccountInfo(Stypayod.acct_no, out acct_desc);
                drdtl["account"] = keyvalue;
                ObjDetailTable.Rows.Add(drdtl.ItemArray);

              }
              //make sure values are valid
              if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
              {
                dr["pay_acct_no"] = objGloabalPayDefaultsListStycntrc[0].exp_acct;// dr["exp_acct"];
                if (Convert.ToInt32(dr["pay_acct_no"]) == 0)
                {
                  dr["ok_to_post"] = false;
                  dr["err_2"] = "**** Error: No expense account number.";
                  err_msg = "Error: No expense account number.";
                  // dr["Problem2"] = "ON";
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "Error: No expense account number.";
                    return false;
                  }
                }
              }

              if (dr["pay_dept"].ToString().Trim() == string.Empty)
              {
                dr["pay_dept"] = dr["department"].ToString().Trim();
              }

              if (CHECK_POST == "POST")
              {
                // If this is a duplicate of an existing code for this
                // employee code, just acquire information to update
                // the accumulation buckets for the first instance
                // of the code in the employee record.
                // If the code is brand new to the employee,
                // (add_code = "Y", then we adjust the line_no
                // value further down in this function.
                if (Stypayod.add_code == "Z" || Stypayod.add_code == "Y")
                {
                  line_number = GET_MIN_LINENUMBER_OBL(dr["empl_code"].ToString().Trim(), dr["pay_code"].ToString().Trim());
                  if (line_number != 0)
                  {
                    Stypayod.add_code = "Z";
                    Stypayod.line_no = line_number;
                  }
                }
              }

              //  post the obligation credit to payroll................
              py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "D", Convert.ToDecimal(dr["pay_amount"]), Convert.ToInt32(dr["pay_acct_no"]), dr["pay_dept"].ToString().Trim(), Convert.ToDateTime(dr["eop_date"]), "0", 0, Stypayod.obl_rate.ToString().Trim(), Stypayod.line_no.ToString().Trim(), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum);



              //if the amount is negative, reverse the sense of the debit/credit
              //and reverse the amount
              db_cr = "D";
              amount = Convert.ToDecimal(dr["pay_amount"]);
              if (amount < 0)
              {

                db_cr = "C";
                amount = amount * (-1);
              }
              // post obligation credit to G/L if stycntrc.post_gl  field is set to Y
              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //   THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //   END IF


                //if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, rpt.pay_acct_no, rpt.pay_dept, amount,
                //db_cr)


                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Convert.ToInt32(dr["pay_acct_no"]);
                objDVOPostGL.department = dr["pay_dept"].ToString().Trim();
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }
                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  err_msg = objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }
                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //int acct_no = objDVOPostGL.acct_no;
                //BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                string acct_desc = string.Empty;
                string keyvalue = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                keyvalue = GetAccountInfo(acct_no, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                //dr["Problem7"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL,stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }

              // make sure values are valid
              if (Stypayod.bal_acct_no == 0)
              {
                Stypayod.bal_acct_no = objGloabalPayDefaultsListStycntrc[0].liab_acct;
                if (Stypayod.bal_acct_no == 0)
                {
                  dr["ok_to_post"] = false;
                  err_msg = "Error: No liablility account number.";
                  dr["err_2"] = "**** Error: No liablility account number.";
                  // return false;
                }
              }

              if (Stypayod.bal_dept.Trim() == string.Empty)
              {
                Stypayod.bal_dept = dr["emp_department"].ToString().Trim();
              }


              //post the obligation debit to payroll

              //if not py_post(check_post, "PY", new_doc_no, post_no, today,rpt.doc_date, rpt.pay_date, rpt.empl_code, "PAYROLL ENTRY",rpt.check_no, rpt.pay_code, "E",rpt.pay_amount,obl_ref.bal_acct_no, obl_ref.bal_dept, rpt.eop_date, "","", obl_ref.obl_rate, obl_ref.line_no)

              py_post(ref objTransection, objGloabalPayDefaultsListStycntrc[0], CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString().Trim(), "PAYROLL ENTRY", dr["check_no"].ToString().Trim(), dr["pay_code"].ToString().Trim(), "E", Convert.ToDecimal(dr["pay_amount"]), Stypayod.bal_acct_no, Stypayod.bal_dept, Convert.ToDateTime(dr["eop_date"]), "0", 0, Stypayod.obl_rate.ToString().Trim(), Stypayod.line_no.ToString().Trim(), out py_status, out py_description, out py_installed, ref py_c_accum, ref py_i_accum, ref py_d_accum, ref py_ox_accum, ref py_ol_accum);



              //reverse the sense of the debit/credit
              if (db_cr == "D")
              {
                db_cr = "C";
              }
              else
              {
                db_cr = "D";
              }

              // post obligation credit to G/L if stycntrc.post_gl  field is set to Y

              if (objGloabalPayDefaultsListStycntrc[0].post_gl == "Y")
              {
                //insert the required information into the temporary table to be printed
                // after each payroll department
                //IF exceptions
                //   THEN
                //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                //   END IF


                //if not gl_post(check_post, "PY", new_doc_no, post_no,
                //today, rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY",
                //rpt.check_no, obl_ref.bal_acct_no, obl_ref.bal_dept,
                //amount, db_cr)



                DVOPostGL objDVOPostGL = new DVOPostGL();
                objDVOPostGL.post_or_check = CHECK_POST;
                objDVOPostGL.orig_journal = "PY";
                objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                objDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
                objDVOPostGL.ref_code = dr["empl_code"].ToString().Trim();
                objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                objDVOPostGL.inv_chk_no = dr["check_no"].ToString().Trim();
                objDVOPostGL.acct_no = Stypayod.bal_acct_no;
                objDVOPostGL.department = Stypayod.bal_dept;
                objDVOPostGL.amount = amount;
                objDVOPostGL.debit_credit = db_cr;
                objDVOPostGLGlobal = Gl_post(ref objDVOPostGL, ref objDVOPostGLGlobal, ref objTransection);
                if (objDVOPostGLGlobal.sql_error != 0)
                {
                  if (CHECK_POST == "POST")
                  {
                    err_desc = "An SQL Error has occured while posting into GL";
                    return false;
                  }
                }
                if (objDVOPostGLGlobal.status == 0 || objDVOPostGLGlobal.status == 3)
                {
                }
                else
                {
                  dr["ok_to_post"] = false;
                  err_msg = objDVOPostGLGlobal.description;
                  dr["err_3"] = "**** Error: " + objDVOPostGLGlobal.description;
                  if (CHECK_POST == "POST")
                  {
                    err_desc = objDVOPostGLGlobal.description;
                    return false;
                  }
                }
                #region Collect data for GL Summary ..........
                DataRow drGL = GLSumTable.NewRow();
                drGL["doc_no"] = objDVOPostGL.doc_no;
                drGL["doc_date"] = objDVOPostGL.doc_date;
                drGL["acctno"] = objDVOPostGL.acct_no;
                drGL["amount"] = objDVOPostGL.amount;
                drGL["debit_credit"] = objDVOPostGL.debit_credit;
                drGL["flexdept"] = dr["flexdept"];
                //string keyvalue = string.Empty;
                //int id = 0;
                //string acct_type = string.Empty;
                //string acct_desc = string.Empty;
                //int acct_no = objDVOPostGL.acct_no;
                //BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                string acct_desc = string.Empty;
                string keyvalue = string.Empty;
                int acct_no = objDVOPostGL.acct_no;
                keyvalue = GetAccountInfo(acct_no, out acct_desc);
                drGL["keyvalue"] = keyvalue;
                drGL["acct_desc"] = acct_desc;
                GLSumTable.Rows.Add(drGL.ItemArray);
                #endregion
              }
              else
              {
                //dr["Problem7"] = "ON";
                if (CHECK_POST == "POST")
                {
                  err_desc = "Cannot post into GL,stycntrc.post_gl field is not set to Y";
                  return false;
                }
              }



              // check for futa obligation
              if (Stypayod.obl_code == objGloabalPayDefaultsListStycntrc[0].futa_code) // dr["futa_code"].ToString().Trim()
              {
                ofuta_flag = true;
              }
              //check for fica obligation
              if (Stypayod.obl_code == objGloabalPayDefaultsListStycntrc[0].fica_ob_code)  //dr["fica_ob_code"].ToString().Trim()
              {
                ofica_flag = true;
                fica_obl = fica_obl + Stypayod.amount ?? 0; //make nullable decimal By Rahul
              }

              //check for medicare obligation
              if (Stypayod.obl_code == objGloabalPayDefaultsListStycntrc[0].medicare_ob_code)//dr["medicare_ob_code"].ToString().Trim()
              {
                omedicare_flag = true;
                medcr_obl = medcr_obl + Stypayod.amount ?? 0;//make nullable decimal By Rahul
              }

              if (CHECK_POST == "POST")
              {
                // test to see if we need to add the code to the empl record
                if (Stypayod.add_code == "Y")
                {
                  // new code for the employee
                  //get array size for this employee for line_no value
                  if (prev_check != 123)
                  {
                    prev_check = 123;
                    line_number = Get_MAX_LINENUMBER_OBL(dr["empl_code"].ToString().Trim());
                    if (line_number > 0)
                    {
                      line_number += 1;
                    }
                    else
                    {
                      line_number = 0;
                    }
                  }
                  //# append row to employee record for future accruals
                  // call updt_emp_obl(obl_ref.obl_code, obl_ref.acct_no,
                  //         obl_ref.department, obl_ref.bal_acct_no,
                  //         obl_ref.bal_dept, line_number, obl_ref.amount)
                  string upd_status;
                  //make nullable decimal By Rahul
                  int i = updt_emp_obl(ref objTransection, Stypayod.obl_code, Stypayod.acct_no.ToString().Trim(), Stypayod.Department, line_number, Stypayod.amount ?? 0, dr["empl_code"].ToString().Trim(), Convert.ToDateTime(dr["pay_date"].ToString().Trim()), Stypayod.bal_acct_no, Stypayod.bal_dept, out upd_status);
                  if (i != 1)
                  {
                    dr["warn_11"] = upd_status;
                    if (CHECK_POST == "POST")
                    {
                      err_desc = upd_status;
                      return false;
                    }
                  }
                }
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private int updt_emp_inc(ref object objTransection, string inc_code, string acct_no, string dept, int line_number, decimal? amount, string empl_code, DateTime payDate, out string upd_status)
    {
      int FunRetValue = 0;
      int quarter;
      int RetValue = 0;
      upd_status = string.Empty;
      quarter = BLLPayrollFunctions.qtr_number(payDate);
      try
      {
        if (quarter > 4 && quarter < 1)
        {
          return FunRetValue;
        }
        else
        {
          DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
          switch (quarter)
          {
            case 1:
              {
                // values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, amount, 0, 0, 0, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.inc_qtd2 = 0;

                objDVOMasterEmployeeIncomes.inc_qtd3 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd4 = 0;
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            case 2:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, amount, 0, 0, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd2 = Convert.ToDecimal(amount);

                objDVOMasterEmployeeIncomes.inc_qtd3 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd4 = 0;
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            case 3:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, 0, amount, 0, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd2 = 0;

                objDVOMasterEmployeeIncomes.inc_qtd3 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.inc_qtd4 = 0;
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            default:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, 0, 0, amount, amount, "", "")
                objDVOMasterEmployeeIncomes.empl_code = empl_code;
                objDVOMasterEmployeeIncomes.inc_code = inc_code;
                objDVOMasterEmployeeIncomes.line_no = line_number;
                objDVOMasterEmployeeIncomes.inc_rate = 0;
                objDVOMasterEmployeeIncomes.inc_number = 0;

                objDVOMasterEmployeeIncomes.inc_hours = 0;
                objDVOMasterEmployeeIncomes.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeIncomes.department = dept;
                objDVOMasterEmployeeIncomes.inc_qtd1 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd2 = 0;

                objDVOMasterEmployeeIncomes.inc_qtd3 = 0;
                objDVOMasterEmployeeIncomes.inc_qtd4 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.inc_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeIncomes.lo_inc_amt = 0;
                objDVOMasterEmployeeIncomes.hi_inc_amt = 0;
                RetValue = BLLMasterEmployeeIncomes.InsertData(ref objTransection, ref objDVOMasterEmployeeIncomes, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
          }
        }
        return FunRetValue;
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
    }

    private int updt_emp_ded(ref object objTransection, string ded_code, string acct_no, string dept, int line_number, decimal amount, string empl_code, DateTime payDate, DateTime eop_date, out string upd_status)
    {
      upd_status = string.Empty;
      int FunRetValue = 0;
      int quarter;
      int RetValue = 0;
      quarter = BLLPayrollFunctions.qtr_number(payDate);
      try
      {
        if (quarter > 4 && quarter < 1)
        {
          upd_status = "**** Warning: unable to update employee record with new code.";
          return FunRetValue;
        }
        else
        {
          DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
          switch (quarter)
          {
            case 1:
              {
                //values (rpt.empl_code, ded_code, line_number,0, 0, "A", acct_no, dept, amount,0 , 0,0, amount, rpt.eop_date, "", "", "", 0.0 )

                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_qtd2 = 0;

                objDVOMasterEmployeeDeductions.ded_qtd3 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd4 = 0;
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            case 2:
              {
                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd2 = Convert.ToDecimal(amount); ;

                objDVOMasterEmployeeDeductions.ded_qtd3 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd4 = 0;
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            case 3:
              {
                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd2 = 0;

                objDVOMasterEmployeeDeductions.ded_qtd3 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_qtd4 = 0;
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
            default:
              {
                objDVOMasterEmployeeDeductions.empl_code = empl_code;
                objDVOMasterEmployeeDeductions.ded_code = ded_code;
                objDVOMasterEmployeeDeductions.line_no = line_number;
                objDVOMasterEmployeeDeductions.ded_rate = 0;
                objDVOMasterEmployeeDeductions.ded_limit = 0;

                objDVOMasterEmployeeDeductions.ded_apply = "A";
                objDVOMasterEmployeeDeductions.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeDeductions.department = dept;
                objDVOMasterEmployeeDeductions.ded_qtd1 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd2 = 0;

                objDVOMasterEmployeeDeductions.ded_qtd3 = 0;
                objDVOMasterEmployeeDeductions.ded_qtd4 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeDeductions.ded_date = Convert.ToString(eop_date);
                objDVOMasterEmployeeDeductions.lo_ded_amt = 0;

                objDVOMasterEmployeeDeductions.hi_ded_amt = 0;
                objDVOMasterEmployeeDeductions.pay_limit = 0;
                objDVOMasterEmployeeDeductions.balanceamt = 0;
                RetValue = BLLMasterEmployeeDeductions.InsertData(ref objTransection, ref objDVOMasterEmployeeDeductions, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }
              }
              break;
          }
        }
        return FunRetValue;
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
    }

    private int updt_emp_obl(ref object objTransection, string obl_code, string acct_no, string dept, int line_number, decimal amount, string empl_code, DateTime payDate, int bal_acct_no, string bal_dept, out string upd_status)
    {
      upd_status = string.Empty;
      int FunRetValue = 0;
      int quarter;
      int RetValue = 0;
      quarter = BLLPayrollFunctions.qtr_number(payDate);
      try
      {
        if (quarter > 4 && quarter < 1)
        {
          upd_status = "**** Warning: unable to update employee record with new code.";
          return FunRetValue;
        }
        else
        {
          DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
          switch (quarter)
          {
            case 1:
              {
                //values (rpt.empl_code, obl_code, line_number, 0, 0,acct_no, dept, bal_acct_no, bal_dept, amount, 0, 0,0, amount)
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = Convert.ToDecimal(amount);

                objDVOMasterEmployeeObligations.obl_qtd2 = 0;
                objDVOMasterEmployeeObligations.obl_qtd3 = 0;
                objDVOMasterEmployeeObligations.obl_qtd4 = 0;
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            case 2:
              {
                //values (rpt.empl_code, obl_code, line_number, 0, 0,acct_no, dept, bal_acct_no, bal_dept, 0, amount, 0,0, amount)
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = 0;

                objDVOMasterEmployeeObligations.obl_qtd2 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.obl_qtd3 = 0;
                objDVOMasterEmployeeObligations.obl_qtd4 = 0;
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            case 3:
              {
                //values (rpt.empl_code, inc_code, line_number, 0, 0,"", acct_no, dept, 0, 0, amount, 0, amount, "", "")
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = 0;

                objDVOMasterEmployeeObligations.obl_qtd2 = 0;
                objDVOMasterEmployeeObligations.obl_qtd3 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.obl_qtd4 = 0;
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
            default:
              {
                // values (rpt.empl_code, obl_code, line_number, 0, 0,acct_no, dept, bal_acct_no, bal_dept, 0, 0, 0,amount, amount)
                objDVOMasterEmployeeObligations.empl_code = empl_code;
                objDVOMasterEmployeeObligations.obl_code = obl_code;
                objDVOMasterEmployeeObligations.line_no = line_number;
                objDVOMasterEmployeeObligations.obl_rate = 0;
                objDVOMasterEmployeeObligations.obl_limit = 0;

                objDVOMasterEmployeeObligations.acct_no = Convert.ToInt32(acct_no);
                objDVOMasterEmployeeObligations.department = dept;
                objDVOMasterEmployeeObligations.bal_acct_no = bal_acct_no;
                objDVOMasterEmployeeObligations.bal_dept = bal_dept;
                objDVOMasterEmployeeObligations.obl_qtd1 = 0;

                objDVOMasterEmployeeObligations.obl_qtd2 = 0;
                objDVOMasterEmployeeObligations.obl_qtd3 = 0;
                objDVOMasterEmployeeObligations.obl_qtd4 = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.obl_ytd = Convert.ToDecimal(amount);
                objDVOMasterEmployeeObligations.pay_limit = 0;

                RetValue = BLLMasterEmployeeObligations.InsertData(ref objTransection, ref objDVOMasterEmployeeObligations, false);
                if (RetValue == 1)
                {
                  FunRetValue = 1;
                }
                else
                {
                  upd_status = "**** Warning: unable to update employee record with new code.";
                }

              }
              break;
          }
        }
        return FunRetValue;
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
    }

    private bool vac_time_accrue(ref object objTransection, DataRow dr, string empl_code, decimal Total_hour, decimal vac_allowed, DateTime eop_date, out int status)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      try
      {
        status = 0;
        decimal add_vac_time = 0.0M;
        decimal vac_incr_amt = 0.0M;
        int factor = 1;
        object[] objDetails = new object[4];
        objDetails[0] = string.Empty;
        objDetails[1] = 0;
        objDetails[2] = 0;
        objDetails[3] = 0;
        //DataSet ds_a_acc_record = null;
        //DataSet dsaccr_emp = FIND_ACCRUAL_EMP_RECORD(empl_code);
        //(vac_accr_code,vac_accr_ctr,vac_lapse_date)
        //dsaccr_emp.Tables[0].Rows[0][0]--dr["vac_accr_code"]
        //dsaccr_emp.Tables[0].Rows[0][1]--dr["vac_accr_ctr"]
        //dr["vac_lapse_date"]--dr["vac_lapse_date"]

        //if (dsaccr_emp.Tables[0].Rows.Count > 0)
        //{
        if (dr["vac_accr_code"] != DBNull.Value && dr["vac_accr_code"].ToString().Trim().Length > 0)
        {
          //ds_a_acc_record = FIND_ACCRUAL_TIMED_DETAILS(dsaccr_emp.Tables[0].Rows[0][0].ToString().Trim());
          //(accr_method,accr_rate,accr_freq,accr_lapse)
          DataRow[] dra = dsStyaccrr.Tables[0].Select("accr_code='" + dr["vac_accr_code"].ToString().Trim() + "'");
          if (dra.Length > 0)
          {
            objDetails[0] = dra[0][1].ToString().Trim();//accr_method
            objDetails[1] = dra[0][2] != DBNull.Value ? Convert.ToDecimal(dra[0][2]) : 0;  //accr_rate
            objDetails[2] = dra[0][3] != DBNull.Value ? Convert.ToDecimal(dra[0][3]) : 0; //accr_freq
            objDetails[3] = dra[0][4] != DBNull.Value ? Convert.ToDecimal(dra[0][4]) : 0;//accr_lapse

          }
          else
          {
            return false;
          }
        }
        else
        {
          return false;
          //no accrual code is setup, no point in processing
          //return
          // break;
        }
        //}
        //default vac time accrual counter to one
        if (dr["vac_accr_ctr"] == DBNull.Value)
        {
          dr["vac_accr_ctr"] = 1;
        }
        //need to verify this code..
        if (objDetails[0] != null)
        {
          //Compairing method 
          if (objDetails[0].ToString().Trim() == "H")
          {
            vac_incr_amt = Total_hour;
          }
          else
          {
            vac_incr_amt = 1;
          }
        }
        BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
        string add_vac_time1 = objBLLPayrollFunctions.Al_Round("d", Convert.ToDecimal(objDetails[1]));
        add_vac_time = Convert.ToDecimal(add_vac_time1);

        //make sure enough pay periods have elapsed before accrual begins
        //if accr_emp.vac_lapse_date is not null or a_lapse = 0
        //if (dr["vac_lapse_date"] == DBNull.Value)
        //    dr["vac_lapse_date"] = 0;
        //Convert.ToInt32(dr["vac_lapse_date"]);
        if (dr["vac_lapse_date"] != DBNull.Value || Convert.ToInt32(objDetails[3]) == 0)
        {

          //if (accr_emp.vac_accr_ctr + vac_incr_amt) < a_freq
          if ((Convert.ToDecimal(dr["vac_accr_ctr"]) + vac_incr_amt) < Convert.ToDecimal(objDetails[2]))
          {
            //Update_Vac_Acc_Counter
            int i = Update_VAC_Ctr(ref objTransection, ref objDVOExceptionReports, empl_code, Convert.ToDecimal(dr["vac_accr_ctr"]), vac_incr_amt);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            //a_diff= (accr_emp.vac_accr_ctr + vac_incr_amt) - a_freq
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dr["vac_accr_ctr"]) + vac_incr_amt) - Convert.ToInt32(objDetails[2]));

            //******** Added by Bharat Dhall to replace commented while loop *************
            int tmp_a_diff = 0, tmp_factor = 0;
            tmp_factor = Math.DivRem(a_diff, Convert.ToInt32(objDetails[2]), out tmp_a_diff);

            if (tmp_a_diff > 0)
              tmp_factor++;
            else
              tmp_a_diff = Convert.ToInt32(objDetails[2]);

            a_diff = tmp_a_diff;
            factor = tmp_factor;

            //while (a_diff > Convert.ToInt32(objDetails[2]))
            //{
            //    a_diff = a_diff - Convert.ToInt32(objDetails[2]);
            //    factor = factor + 1;
            //}
            //******************************************************************************

            decimal new_vac_allowed = vac_allowed + (add_vac_time * factor);
            int i = UPDATE_VAC_Allowed_VAC_Counter(ref objTransection, ref objDVOExceptionReports, empl_code, new_vac_allowed, a_diff);
            if (i == 0)
            {
              status = 1;
              return false;
            }

            //The lapse was 0 so employee should accrue. Date must be set
            if (dr["vac_lapse_date"] == DBNull.Value)
            {
              i = UPDATE_VAC_Lapse(ref objTransection, ref objDVOExceptionReports, empl_code, eop_date);
              if (i == 0)
              {
                status = 1;
                return false;
              }
            }
          }
        }
        else
        {
          //if lapse has not been reached yet just increment counter,
          //otherwise set past lapse flag and reset counter
          if ((Convert.ToDecimal(dr["vac_accr_ctr"]) + vac_incr_amt) < Convert.ToDecimal(objDetails[3]))
          {
            decimal new_vac_accr_ctr = 0.0M;
            new_vac_accr_ctr = Convert.ToDecimal(dr["vac_accr_ctr"]) + vac_incr_amt;
            int i = UPDATE_VAC_Lapse_Control_1(ref objTransection, ref objDVOExceptionReports, empl_code, new_vac_accr_ctr, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            //a_diff =(accr_emp.vac_accr_ctr + vac_incr_amt) - a_lapse
            // initialize multiplier for accrual increment
            factor = 1;
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dr["vac_accr_ctr"]) + vac_incr_amt) - Convert.ToInt32(objDetails[3]));
            // cycle through to eliminate backlog if any exists
            while (a_diff > Convert.ToInt32(objDetails[2]))
            {
              a_diff = a_diff - Convert.ToInt32(objDetails[2]);
              factor = factor + 1;
            }
            int i = UPDATE_VAC_Lapse_Control(ref objTransection, ref objDVOExceptionReports, empl_code, a_diff, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;
    }

    private bool sick_time_accrue(ref object objTransection, DataRow dr, string empl_code, decimal Total_hour, decimal Sick_allowed, DateTime eop_date, out int status)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      try
      {
        status = 0;
        decimal add_sick_time = 0.0M;
        decimal sick_incr_amt = 0.0M;
        int factor = 1;
        object[] objDetails = new object[4];
        objDetails[0] = string.Empty;
        objDetails[1] = 0;
        objDetails[2] = 0;
        objDetails[3] = 0;
        //DataSet ds_a_acc_record = null;
        //DataSet dsaccr_emp = FIND_ACCRUAL_SICK_EMP_RECORD(empl_code);
        //sick_accr_code,sick_accr_ctr,sick_lapse_date
        //dsaccr_emp.Tables[0].Rows[0][0] -- dr["sick_accr_code"]
        //dsaccr_emp.Tables[0].Rows[0][1] -- dr["sick_accr_ctr"]
        //dsaccr_emp.Tables[0].Rows[0][2] -- dr["sick_lapse_date"]

        //if (dsaccr_emp.Tables[0].Rows.Count > 0)
        //{
        if (dr["sick_accr_code"] != DBNull.Value && dr["sick_accr_code"].ToString().Trim().Length > 0)
        {
          //ds_a_acc_record = FIND_ACCRUAL_TIMED_DETAILS(dsaccr_emp.Tables[0].Rows[0][0].ToString().Trim());

          DataRow[] dra = dsStyaccrr.Tables[0].Select("accr_code='" + dr["sick_accr_code"].ToString().Trim() + "'");
          if (dra.Length > 0)
          {
            objDetails[0] = dra[0][1].ToString().Trim();//accr_method
            objDetails[1] = dra[0][2] != DBNull.Value ? Convert.ToDecimal(dra[0][2]) : 0;  //accr_rate
            objDetails[2] = dra[0][3] != DBNull.Value ? Convert.ToDecimal(dra[0][3]) : 0; //accr_freq
            objDetails[3] = dra[0][4] != DBNull.Value ? Convert.ToDecimal(dra[0][4]) : 0;//accr_lapse


          }
          else
          {
            return false;
          }
        }
        else
        {
          return false;
          //no accrual code is setup, no point in processing
          //return
          // break;
        }
        //}
        // default sick time accrual counter to one
        if (dr["sick_accr_ctr"] == DBNull.Value)
        {
          //sick_accr_ctr=1
          dr["sick_accr_ctr"] = 1;
        }

        //default sick time accrual lapse indicator to zero
        if (objDetails[3] == null)
        {
          // a_lapse = 0
          objDetails[3] = 0;
        }

        if (objDetails[0] != null)
        {
          //Compairing method 
          if (objDetails[0].ToString().Trim() == "H")
          {
            sick_incr_amt = Total_hour;
          }
          else
          {
            sick_incr_amt = 1;
          }
        }
        BLLPayrollFunctions objBLLPayrollFunctions = new BLLPayrollFunctions();
        string add_sick_time1 = objBLLPayrollFunctions.Al_Round("d", Convert.ToDecimal(objDetails[1]));
        add_sick_time = Convert.ToDecimal(add_sick_time1);

        //make sure enough time has elapsed before accrual begins
        // if accr_emp.sick_lapse_date is not null or a_lapse = 0
        if (dr["sick_lapse_date"] != DBNull.Value || Convert.ToInt32(objDetails[3]) == 0)
        {
          //check that this is a pay period in which time gets accrued
          //if (accr_emp.sick_accr_ctr + sick_incr_amt) < a_freq
          if ((Convert.ToDecimal(dr["sick_accr_ctr"]) + sick_incr_amt) < Convert.ToDecimal(objDetails[2]))
          {
            //UPDATE_SICK_Counter
            int i = UPDATE_SICK_Ctr(ref objTransection, ref objDVOExceptionReports, empl_code, Convert.ToDecimal(dr["sick_accr_ctr"]), sick_incr_amt);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            // initialize multiplier for accrual increment,  factor 
            //a_diff= (accr_emp.sick_accr_ctr + sick_incr_amt) - a_freq
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dr["sick_accr_ctr"]) + sick_incr_amt) - Convert.ToInt32(objDetails[2]));
            //while a_diff > a_freq
            //while (a_diff > Convert.ToInt32(objDetails[2]))
            //{
            //    a_diff = a_diff - Convert.ToInt32(objDetails[2]);
            //    factor = factor + 1;
            //}
            decimal new_Sick_allowed = Sick_allowed + (add_sick_time * factor);
            int i = UPDATE_Sick_Allowed_Sick_Counter(ref objTransection, ref objDVOExceptionReports, empl_code, new_Sick_allowed, a_diff);
            if (i == 0)
            {
              status = 1;
              return false;
            }

            //The lapse was 0 so employee should accrue. Date must be set
            if (dr["sick_lapse_date"] == DBNull.Value)
            {
              i = UPDATE_SICK_Lapse_DATE(ref objTransection, ref objDVOExceptionReports, empl_code, eop_date);
              if (i == 0)
              {
                status = 1;
                return false;
              }
            }
          }
        }
        else
        {
          //if lapse has not been reached yet just increment counter,
          //otherwise set past lapse flag and reset counter
          //if accr_emp.sick_accr_ctr < a_lapse
          if (Convert.ToDecimal(dr["sick_accr_ctr"]) < Convert.ToDecimal(objDetails[3]))
          {
            decimal new_sic_accr_ctr = 0.0M;
            new_sic_accr_ctr = Convert.ToDecimal(dr["sick_accr_ctr"]) + sick_incr_amt;
            int i = UPDATE_Control_Lapse1_Without_Date(ref objTransection, ref objDVOExceptionReports, empl_code, new_sic_accr_ctr, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
          else
          {
            //a_diff =(accr_emp.vac_accr_ctr + vac_incr_amt) - a_lapse
            // initialize multiplier for accrual increment
            factor = 1;
            int a_diff = Convert.ToInt32((Convert.ToDecimal(dr["sick_accr_ctr"]) + sick_incr_amt) - Convert.ToInt32(objDetails[3]));
            // cycle through to eliminate backlog if any exists
            //while a_diff > a_freq

            //******** Added by Bharat Dhall to replace commented while loop *************
            int tmp_a_diff = 0, tmp_factor = 0;
            tmp_factor = Math.DivRem(a_diff, Convert.ToInt32(objDetails[2]), out tmp_a_diff);

            if (tmp_a_diff > 0)
              tmp_factor++;
            else
              tmp_a_diff = Convert.ToInt32(objDetails[2]);

            a_diff = tmp_a_diff;
            factor = tmp_factor;

            //while (a_diff > Convert.ToInt32(objDetails[2]))
            //{
            //    a_diff = a_diff - Convert.ToInt32(objDetails[2]);
            //    factor = factor + 1;
            //}
            //******************************************************************************

            int i = UPDATE_Sick_Control_Lapse_DATE(ref objTransection, ref objDVOExceptionReports, empl_code, a_diff, eop_date);
            if (i == 0)
            {
              status = 1;
              return false;
            }
          }
        }

      }
      catch (Exception ex)
      {
        ExceptionManager.Publish(ex);
        //if (objTransection != null)
        // objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        throw ex;
      }
      return true;

    }

    public Boolean py_last(out int py_status, out string py_description, decimal py_c_accum, decimal py_i_accum, decimal py_d_accum, decimal py_ox_accum, decimal py_ol_accum)
    {
      bool py_last_status = true;
      py_status = 0;
      py_description = string.Empty;
      if (py_installed == "N")
      {
        py_status = 1;
        py_description = "Payroll Isn't Installed";
        py_last_status = true;
      }

      //make sure balancing accumulations match
      if ((py_c_accum + py_d_accum != py_i_accum) || (py_ox_accum != py_ol_accum))
      {
        py_status = 10;
        py_description = "Document Doesn't Balance";
        py_last_status = false;
      }
      return py_last_status;
    }

    private Boolean py_delete(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, int docNumber)
    {
      // this function deletes a payroll document
      // Changing the status of ok_to_post
      int i = UPDATE_OK_TO_POST_STATUS(ref objTransection, ref objDVOExceptionReports, "P", docNumber);
      if (i == 1)
        return true;
      else
        return false;
    }


    #region check nss posting details
    //        #  This routine checks to make to see if a  ref_code + ded_code combination
    //#  exists for a NSS account.
    //#  It also verifies that the amount trying to post to the NSS account is
    //#  acceptable
    //#  You should call this procedure BEFORE you actually post the deduction
    //#  code.  In case the amount gets changed.
    //#  Difference between this function and the check_nss function, this one
    //#  just verifies that all the data is OK with the criteria.
    //#
    //#  Data elements passed:
    //#    ref_code char(6),       employee code
    //#    amount like stgactvd.amount,   signed amount
    //#                            (+increases, -decreases balances)
    //#    act_code char(6),       activity code
    //#                            (income, deduction, obligation code)
    //#    line_no                 line_no for the ref_code
    //#
    //# status and there descriptions
    //# 0 - Successful - amount = amount posted
    //# 1 - NSS is not installed, nothing to post - amount = 0
    //# 2 - NSS is installed, but no account types found, nothing to post - amount = 0
    //# 3 - NSS is installed, but no account found, nothing to post - amount = 0
    //# 4 - NSS is installed, more than one account found, nothing to post -
    //#     amount = 0
    //# 5 - trying to add to a repaid/inactive account, nothing to post - amount = 0
    //# 6 - amount submitted to be posted is greater than allowed, post only the
    //#     allowed amount only - amount = amount posted


    //# What this process does:
    //#
    //# - check to see if the NSS is installed.  If not, just return
    //# - try to find a NSS account with the employee/deduction code combination
    //# - once an account is found, make sure there is enough room left in the
    //#   account for the contribution
    //# - no posting is done
    #endregion check nss posting details

    //private string curs_check_nss_prep; // char(1),   # defined the cursors yet?
    //private string curs_post_nss_prep;  // char(1),   # defined the cursors yet?


    private bool check_nss(string ref_code, decimal amount, string act_code, int line_no, out int nss_status, out decimal amount_to_post)
    {
      //assigning initial value to out parameter
      nss_status = 0;
      amount_to_post = 0.0M;
      Boolean nss_installed = false;
      int tmp_count = 0;
      decimal allowed_amount = 0.0M;
      int err_no = 0;
      //first time... fill nss_installed
      string TabName = Get_nss_check_ControlTable();
      if (TabName != "")
      {
        nss_installed = true;
      }
      else
      {
        nss_installed = false;
      }
      if (nss_installed)
      {
        tmp_count = Get_nss_check_ControlTable_Count();
        if (tmp_count == 0)
        {
          //NSS is installed, but no account types found, nothing to post - amount = 0
          nss_status = 2;
          amount_to_post = 0;
          return false;
        }
      }

      else
      {
        //NSS is not installed, nothing to post - amount = 0
        nss_status = 1;
        amount_to_post = 0;
        return false;
      }
      // now let's make sure we can find an account for
      // this employee/deduction
      DataSet dsInfoCurs = Get_nss_check_Information(ref_code);
      if (dsInfoCurs.Tables[0].Rows.Count > 0)
      {
        tmp_count = dsInfoCurs.Tables[0].Rows.Count;
      }
      if (tmp_count == 0)
      {
        // no account founds
        // this will occur 'most' of the time
        nss_status = 3;
        amount_to_post = 0;
        return false;

      }
      else if (tmp_count > 1)
      {
        //more than one account found ('should' never happen)
        nss_status = 4;
        amount_to_post = 0;
        return false;

      }
      else if (tmp_count == 1 && dsInfoCurs.Tables[0].Rows[0][0].ToString().Trim() != "ACTIVE")//nss_status
      {
        // trying to add to a non-active account, bad
        nss_status = 5;
        amount_to_post = 0;
        return false;
      }
      else
      {
        //lets make sure that we have enough room left in the nss
        //account for this contribution
        if (dsInfoCurs.Tables[0].Rows[0][2] == DBNull.Value)//current_bal
        {
          //Assigning the value to Contribution_Total
          dsInfoCurs.Tables[0].Rows[0][2] = 0;//current_bal 
        }
        if (dsInfoCurs.Tables[0].Rows[0][1] == DBNull.Value)//monthly_contrib        
        {
          //Assigning the value to monthly_contrib   
          dsInfoCurs.Tables[0].Rows[0][1] = 0;//monthly_contrib   12     
        }
        //dsInfoCurs.Tables[0].Rows[0][3] -  no_of_payments 

        //allowedamount =(contribution_amount*duration)-contribution_Total
        allowed_amount = (Convert.ToDecimal(dsInfoCurs.Tables[0].Rows[0][1]) * Convert.ToInt32(dsInfoCurs.Tables[0].Rows[0][3])) - Convert.ToDecimal(dsInfoCurs.Tables[0].Rows[0][2]);
        if (allowed_amount < amount)
        {
          //amount submitted to be posted is greater than allowed, post only the
          //allowed amount only - amount = amount posted
          nss_status = 6;
          amount_to_post = allowed_amount;
          return true;
        }
        else
        {
          nss_status = 0;
          amount_to_post = amount;
          return true;
        }
      }

    }


    private bool nss_check_post(ref object objTransection, string post_or_check, string orig_journal, int ref_doc_no, DateTime doc_date, string ref_code, decimal ded_amount, string doc_desc, string act_code, int line_no, out int status, out decimal nss_amount, out string err_desc)
    {
      //assigning initial value to out parameter
      decimal post_nss_amount = 0;
      int seq_no = 0;
      err_desc = string.Empty;
      status = 0;
      Boolean past_nss = true;
      nss_amount = 0;
      StringBuilder date = new StringBuilder();
      //first time... fill nss_installed
      string TabName = Get_nss_check_ControlTable();
      if (TabName != string.Empty)
      {
        // now let's make sure we can find an account for
        // this employee/deduction
        //Get Employee Information .........from nss_clients table
        DataSet dsInfoCurs = Get_nss_check_Information(ref_code);
        if (dsInfoCurs.Tables[0].Rows.Count > 0)
        {
          if (post_or_check == "POST")
          {
            #region Get Date having format "ddmmyy"...
            if (post_or_check == "POST")
            {
              int day = DVOApplicationUserInfo.CurrentDate.Day;
              if (day.ToString().Trim().Length == 1)
                date.Append("0" + day.ToString().Trim());
              else
                date.Append(day.ToString().Trim());
              int month = DVOApplicationUserInfo.CurrentDate.Month;
              if (month.ToString().Trim().Length == 1)
                date.Append("0" + month.ToString().Trim());
              else
                date.Append(month.ToString().Trim());
              string year = DVOApplicationUserInfo.CurrentDate.Year.ToString().Trim().Substring(2, 2);
              date.Append(year.ToString().Trim());
            }
            #endregion
            foreach (DataRow dremp in dsInfoCurs.Tables[0].Rows)
            {
              DVONSSDetail nssclient = new DVONSSDetail();
              nssclient.account_no = dremp[4].ToString().Trim();
              nssclient.contract_no = dremp[5].ToString().Trim();
              nssclient.current_bal = (dremp[2] != DBNull.Value ? Convert.ToDecimal(dremp[2]) : 0);
              nssclient.monthly_contrib = (dremp[1] != DBNull.Value ? Convert.ToDecimal(dremp[1]) : 0);
              nssclient.no_of_payments = (dremp[3] != DBNull.Value ? Convert.ToInt32(dremp[3]) : 0);
              nssclient.nss_status = dremp[0].ToString().Trim();
              nssclient.last_period = (dremp[6] != DBNull.Value ? Convert.ToInt32(dremp[6]) : 0);
              int for_period = 0;
              if (nssclient.last_period == 12)
                for_period = 0;
              else
                for_period = nssclient.last_period;

              //Get no of payment due for this account..
              int due_no = get_no_of_payment_delay(ref nssclient);

              for (int j = 0; j <= due_no; j++)
              {
                post_nss_amount = nssclient.monthly_contrib;
                for_period = for_period + 1;
                if (ded_amount >= post_nss_amount)
                {
                  past_nss = true;
                }
                else if (ded_amount == 0)
                {   //Here nss deduction amount is less than amount need to be post.
                    //means there are some nss accounts need to pay.
                  past_nss = false;
                  err_desc = "";
                }
                else
                {
                  // ded_amount is not zero but it is sufficient to pay for any period.
                  // remaining ded_amount should go into any other account like suspense account.
                  past_nss = false;
                  err_desc = "";
                }
                if (past_nss)
                {
                  #region Insert into nss_hdr,nss_dtl tables.........

                  // Insert data into nss_hdr table...........
                  DVONssTransectionDetails objDVONssTrnDetailsIns = new DVONssTransectionDetails();
                  object[] parameters = new object[12];
                  parameters[0] = "PY" + Convert.ToDateTime(date).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);  //Voucher_no
                  parameters[1] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ; //payment date
                  parameters[2] = DVOApplicationUserInfo.LoginId;
                  parameters[3] = "N";
                  parameters[4] = "";
                  parameters[5] = 0;
                  //Parameters used For Only SQL Server
                  parameters[6] = objDVONssTrnDetailsIns.InsertMachineInfo;
                  parameters[7] = objDVONssTrnDetailsIns.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameters[8] = objDVONssTrnDetailsIns.InsertBy;
                  parameters[9] = objDVONssTrnDetailsIns.UpdateMachineInfo;
                  parameters[10] = objDVONssTrnDetailsIns.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameters[11] = objDVONssTrnDetailsIns.UpdateBy;
                  DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                  DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                  DataSet Ds = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameters, typeof(DVONssTransection));
                  if (Ds.Tables.Count > 0)
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                      seq_no = Convert.ToInt32(Ds.Tables[0].Rows[0][0]);
                      if (seq_no <= 0)
                      {
                        err_desc = "Error has occurred while inserting into nss_hdr";
                        return false;
                      }
                    }
                  //Insert into nss_dtl .............
                  object[] parameter = new object[12];
                  parameter[0] = seq_no;
                  parameter[1] = nssclient.account_no; //Nss AccountNo
                  parameter[2] = for_period;
                  parameter[3] = "A";
                  parameter[4] = post_nss_amount;
                  parameter[5] = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  //Parameters used For Only SQL Server
                  parameter[6] = objDVONssTrnDetailsIns.InsertMachineInfo;
                  parameter[7] = objDVONssTrnDetailsIns.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameter[8] = objDVONssTrnDetailsIns.InsertBy;
                  parameter[9] = objDVONssTrnDetailsIns.UpdateMachineInfo;
                  parameter[10] = objDVONssTrnDetailsIns.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
                  parameter[11] = objDVONssTrnDetailsIns.UpdateBy;
                  DataSet Dsdtl = objDALBaseClass.InsertAndGetData_ByTransaction(ref objTransection, ref parameter, typeof(DVONssTransectionDetails));

                  if (Dsdtl.Tables.Count > 0)
                    if (Dsdtl.Tables[0].Rows.Count > 0)
                    {
                      int i = Convert.ToInt32(Dsdtl.Tables[0].Rows[0][0]);
                      if (i != 1)
                      {
                        err_desc = "Error has occurred while inserting into nss_dtl";
                        return false;
                      }
                    }

                  #endregion

                  ded_amount = ded_amount - post_nss_amount;
                }
              }

            }
            if (ded_amount != 0)
            {
              // ded_amount submitted to be posted is greater than allowed,then have to post.
              // remaining ded_amount should go into any other account like suspense account.
            }
          }
        }
        else
        {
          err_desc = "This Employee does not have account for nss deduction.";
          return false;
        }
      }
      else
      {
        err_desc = "Error: Nss  not installed";
        return false;
      }
      nss_amount = ded_amount;
      return true;
    }

    public int get_no_of_payment_delay(ref DVONSSDetail objNSSDetail)
    {

      DataSet ds = ReportingUtilities.GetNSSDetail(ref objNSSDetail);
      DateTime DueDate = DVOApplicationUserInfo.CurrentDate;
      objNSSDetail.contract_date = Convert.ToDateTime(ds.Tables[0].Rows[0]["p_contract_date"]);
      DueDate = objNSSDetail.contract_date;
      int defaulter = 0;
      int tranno = 1;
      int Monthdue = 0;
      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
      {
        DataRow dr = ds.Tables[0].Rows[i];
        if (DueDate.Year == objNSSDetail.contract_date.Year)
        {
          Monthdue = DueDate.Month - objNSSDetail.contract_date.Month + 1;
        }
        else
        {
          Monthdue = 12 * (DueDate.Year - objNSSDetail.contract_date.Year) + (DueDate.Month - objNSSDetail.contract_date.Month) + 1;
        }
        if ((Monthdue == tranno))
        {
          if (Convert.ToDateTime(dr["p_payment_date"]).Year == DueDate.Year)
          {
            if (Convert.ToDateTime(dr["p_payment_date"]).Month > DueDate.Month)
            {
              defaulter = defaulter + 1;

              DueDate = DueDate.AddMonths(1);
              tranno = tranno + 1;
            }
            else
            {
              tranno = tranno + 1;
              DueDate = DueDate.AddMonths(1);
            }

          }
          else
          {
            if (Convert.ToDateTime(dr["p_payment_date"]).Year > DueDate.Year)
            {
              if (Convert.ToDateTime(dr["p_payment_date"]).Month != DueDate.Month)
              {
                defaulter = defaulter + 1;

                DueDate = DueDate.AddMonths(1);
                tranno = tranno + 1;
              }
              else
              {
                tranno = tranno + 1;
                DueDate = DueDate.AddMonths(1);
              }
            }
            else
            {

              tranno = tranno + 1;
              DueDate = DueDate.AddMonths(1);

            }
          }
        }
      }
      return defaulter;
    }


    /// <summary>
    ///  # used to determine the payroll department for each employee in the DataSet
    ///  # Select all the data we need into the Dataset
    ///  We need to do this so that we can build the flexDept for
    /// each employee using flexseg_load(...) and then order by this value.
    /// </summary>
    /// <param name="objSCDVOPayrollProcess_PayEmployee"></param>
    /// <param name="objSCDVOMasterEmployee"></param>
    /// <returns></returns>
    private DataSet ml_getCount(ref DVOPayrollProcess_PayEmployee objSCDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee objSCDVOMasterEmployee, string CHECK_POST)
    {
      DataSet dsEmpPay = null;
      try
      {
        object[] parameter = new object[11];
        //Get Search Criteria in Styemplr
        parameter[0] = objSCDVOMasterEmployee.FirstName;
        parameter[1] = objSCDVOMasterEmployee.LastName;
        parameter[2] = objSCDVOMasterEmployee.PayPeriod;
        parameter[3] = objSCDVOMasterEmployee.TypeCode;
        parameter[4] = objSCDVOMasterEmployee.JobCode;
        parameter[5] = objSCDVOMasterEmployee.JobTitle;

        //Get Search Criteria in Process_PayEmployee
        parameter[6] = objSCDVOPayrollProcess_PayEmployee.EmplCode;
        parameter[7] = objSCDVOPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameter[8] = objSCDVOPayrollProcess_PayEmployee.Cash_acct_no;
        //This is last_pay from styemplr
        parameter[9] = objSCDVOPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameter[10] = CHECK_POST;
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        dsEmpPay = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return dsEmpPay;
    }

    /// <summary>
    /// used for direct deposit
    /// Get Cash Account Number And Amount
    ///  # determine if the employee uses direct deposit.  
    ///  We cannot use the flag Process_PayEmployee.deposit because o_dposit
    ///  sets it to "N" after creating the direct deposit entries.

    /// </summary>
    /// <param name="PayDocNumber"></param>
    /// <returns>Data set with two attributes</returns>
    private DataSet GetCashAccountAmount(int PayDocNumber_stypddrd, string Empl_Code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      //Get Search Criteria in  stypddrd
      parameter[0] = PayDocNumber_stypddrd;
      parameter[1] = Empl_Code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCashAccountN0 = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_CSH_ACCOUNT_AMOUNT);
      return dsCashAccountN0;

    }

    /// <summary>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions 
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>Data set with one attributes</returns>
    //private static DataSet GetDeductionSumAmount(string Ded_Code_MasterEmployeeDeductions, string soc_sec_num_styemplr)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = Ded_Code_MasterEmployeeDeductions;
    //    parameter[1] = soc_sec_num_styemplr;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsDedYtd = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_YTD);
    //    return dsDedYtd;

    //}
    /// <summary>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions 
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>decimal value</returns>
    private decimal GetDeductionSumAmount(string Ded_Code_MasterEmployeeDeductions, string soc_sec_num_styemplr)
    {
      decimal amount = 0;
      if (dsDeductionSumAmount != null && dsDeductionSumAmount.Tables.Count > 0)
      {
        DataRow[] dra = dsDeductionSumAmount.Tables[0].Select("ded_code='" + Ded_Code_MasterEmployeeDeductions + "'" + " and soc_sec_num='" + soc_sec_num_styemplr + "'");
        if (dra.Length > 0)
        {
          amount = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
        }
      }
      return amount;

      //DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      //object[] parameter = new object[2];
      //parameter[0] = Ded_Code_MasterEmployeeDeductions;
      //parameter[1] = soc_sec_num_styemplr;

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_YTD);
      //if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0)//dsDedYtd
      //    return Convert.ToDecimal(obj);
      //return 0;

    }




    /// <Get Deduction SumAmount from MasterEmployeeIncomes>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions  on the basis of employee code
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="empl_code_MasterEmployeeDeductions"></param>
    /// <returns>Data set with one attributes</returns>
    //private static DataSet GetDeductionSumAmountOne(string Ded_Code_MasterEmployeeDeductions, string empl_code_MasterEmployeeDeductions)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = Ded_Code_MasterEmployeeDeductions;
    //    parameter[1] = empl_code_MasterEmployeeDeductions;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsDedYtd1 = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_YTD1);
    //    return dsDedYtd1;

    //}
    /// <Get Deduction SumAmount from MasterEmployeeIncomes>
    /// To get the sum of ded_ytd from the table MasterEmployeeDeductions  on the basis of employee code
    /// </summary>
    /// <param name="Ded_Code_MasterEmployeeDeductions"></param>
    /// <param name="empl_code_MasterEmployeeDeductions"></param>
    /// <returns>decimal value</returns>
    private decimal GetDeductionSumAmountOne(string Ded_Code_MasterEmployeeDeductions, string empl_code_MasterEmployeeDeductions)
    {
      decimal amount = 0;
      if (dsDeductionSumAmount1 != null && dsDeductionSumAmount1.Tables.Count > 0)
      {
        DataRow[] dra = dsDeductionSumAmount1.Tables[0].Select("ded_code='" + Ded_Code_MasterEmployeeDeductions + "'" + " and empl_code='" + empl_code_MasterEmployeeDeductions + "'");
        if (dra.Length > 0)
        {
          amount = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
        }
      }
      return amount;
      //DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      //object[] parameter = new object[2];
      //parameter[0] = Ded_Code_MasterEmployeeDeductions;
      //parameter[1] = empl_code_MasterEmployeeDeductions;

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_YTD1);
      //if (obj != null)//dsDedYtd1
      //    if (obj.ToString().Trim() != string.Empty)
      //        return Convert.ToDecimal(obj);
      //return 0;

    }

    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>Data set with one attributes</returns>
    //private static DataSet GetObligationSumAmount(string obl_code_MasterEmployeeObligations, string soc_sec_num_styemplr)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = obl_code_MasterEmployeeObligations;
    //    parameter[1] = soc_sec_num_styemplr;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsOblYtd = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBLYTD);
    //    return dsOblYtd;

    //}
    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>decimal value</returns>
    private decimal GetObligationSumAmount(string obl_code_MasterEmployeeObligations, string soc_sec_num_styemplr)
    {
      decimal amount = 0;
      if (dsOblgationSumAmount != null && dsOblgationSumAmount.Tables.Count > 0)
      {
        DataRow[] dra = dsOblgationSumAmount.Tables[0].Select("obl_code='" + obl_code_MasterEmployeeObligations + "'" + " and soc_sec_num='" + soc_sec_num_styemplr + "'");
        if (dra.Length > 0)
        {
          amount = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
        }
      }
      return amount;
      //DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      //object[] parameter = new object[2];
      //parameter[0] = obl_code_MasterEmployeeObligations;
      //parameter[1] = soc_sec_num_styemplr;

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBLYTD);
      //if (obj != null)//dsOblYtd
      //    return Convert.ToDecimal(obj);
      //return 0;

    }

    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="empl_code_MasterEmployeeObligations"></param>
    /// <returns> Data set with one attributes</returns>
    //private static DataSet GetObligationSumAmountOne(string obl_code_MasterEmployeeObligations, string empl_code_MasterEmployeeObligations)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = obl_code_MasterEmployeeObligations;
    //    parameter[1] = empl_code_MasterEmployeeObligations;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsOblYtd1 = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBLYTD1);
    //    return dsOblYtd1;

    //}

    /// <summary>
    /// To get the sum of obl_ytd from the table MasterEmployeeObligations 
    /// </summary>
    /// <param name="obl_code_MasterEmployeeObligations"></param>
    /// <param name="empl_code_MasterEmployeeObligations"></param>
    /// <returns> decimal value</returns>
    private decimal GetObligationSumAmountOne(string obl_code_MasterEmployeeObligations, string empl_code_MasterEmployeeObligations)
    {
      decimal amount = 0;
      if (dsOblgationSumAmount1 != null && dsOblgationSumAmount1.Tables.Count > 0)
      {
        DataRow[] dra = dsOblgationSumAmount1.Tables[0].Select("obl_code='" + obl_code_MasterEmployeeObligations + "'" + " and empl_code='" + empl_code_MasterEmployeeObligations + "'");
        if (dra.Length > 0)
        {
          amount = dra[0][0] != DBNull.Value ? Convert.ToDecimal(dra[0][0]) : 0;
        }
      }
      return amount;
      //DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      //object[] parameter = new object[2];
      //parameter[0] = obl_code_MasterEmployeeObligations;
      //parameter[1] = empl_code_MasterEmployeeObligations;

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBLYTD1);
      //if (obj != null)//dsOblYtd1
      //    if (obj.ToString().Trim() != string.Empty)
      //        return Convert.ToDecimal(obj);
      //return 0;
    }

    /// <summary>
    /// To get the sum of sick and vacation
    /// </summary>
    /// <param name="soc_sec_num_styemplr"></param>
    /// <returns>Data set with Two attributes</returns>
    private DataSet GetSumOfSickAndVacation(string soc_sec_num_styemplr)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = soc_sec_num_styemplr;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsSickAndVac = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_SICK_VACATION);
      return dsSickAndVac;

    }

    /// <summary>
    /// To get the sum of basic amount
    /// </summary>
    /// <param name="DocNumber_stypayid"></param>
    /// <returns></returns>
    private decimal GetSumOfBasicAmount(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsBasicSum = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_BASIC_AMOUNT);
      //return dsBasicSum;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_BASIC_AMOUNT);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }
    }

    /// <summary>
    /// To get the sum of taxable statement 
    /// </summary>
    /// <param name="DocNumber_stypayid"></param>
    /// <returns></returns>
    private decimal GetSumOfTaxableStatement(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsTaxStmt = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_TAXABLE_STMT);
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_TAXABLE_STMT);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;

      }
      else
      {
        return 0.0M;
      }

    }

    /// <summary>
    /// To get the sum of non taxable statement 
    /// </summary>
    /// <param name="DocNumber_stypayid"></param>
    /// <returns></returns>
    private decimal GetSumOfNonTaxableStatement(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      // DataSet dsNonTaxStmt = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_NON_TAXABLE_STMT);
      // return dsNonTaxStmt;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_NON_TAXABLE_STMT);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }
    }


    #region Get the value to Precess before doc number

    private decimal GetSumDeductionTotal(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumDedTotal = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_TOTAL);
      //return dssumDedTotal;

      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_TOTAL);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }

    private decimal GetSumDeductionTotalSSD(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      // DataSet dssumDedTotalSSD = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSD);
      // return dssumDedTotalSSD;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSD);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }

    private decimal GetSumDeductionTotalSSL(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumDedTotalSSL = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSl);
      //return dssumDedTotalSSL;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_DED_TOTAL_SSl);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }
    }

    private decimal GetSumObligationTotalSSD(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumOblTotalSSD = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSD);
      //return dssumOblTotalSSD;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSD);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }

    private decimal GetSumObligationTotalSSIB(int DocNumber_stypayid)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumOblTotalSSIB = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSlB);
      //return dssumOblTotalSSIB;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSlB);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return Convert.ToDecimal(RetValue[0]);
        else return 0.0M;
      }
      else
      {
        return 0.0M;
      }

    }
    private object[] GetSumIncome(int DocNumber_stypayid, string Inc_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = DocNumber_stypayid;
      parameter[1] = Inc_code.ToString().Trim();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dssumOblTotalSSIB = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_OBL_TOTAL_SSlB);
      //return dssumOblTotalSSIB;
      object[] RetValue = new object[3];
      DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_SUM_ANY_Income);
      if (ds.Tables.Count > 0)
      {
        if (ds.Tables[0].Rows.Count > 0)
        {
          RetValue[0] = ds.Tables[0].Rows[0][0];
          RetValue[1] = ds.Tables[0].Rows[0][1];
          RetValue[2] = ds.Tables[0].Rows[0][2];
        }
        else
        {
          RetValue[0] = 0.0M;
          RetValue[1] = 0.0M;
          RetValue[2] = 0.0M;
        }
      }

      return RetValue;
    }

    private string GetBonusCheck(int DocNumber_stypayid, string empl_number_styemplr)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = empl_number_styemplr;
      parameter[1] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsbonuschk = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_BONUS_CHECK);
      // return dsbonuschk;
      object[] RetValue = new object[1];
      RetValue[0] = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.FIND_BONUS_CHECK);
      if (RetValue[0] != null)
      {
        if (RetValue[0].ToString().Trim().Length > 0)
          return RetValue[0].ToString().Trim();
        else return "";
      }
      else
      {
        return "";
      }
    }
    #endregion Get the value to Precess before doc number

    #region Get the value to process chk_post()

    public int GetCount1(string orig_journal, int doc_no)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = orig_journal;
      parameter[1] = doc_no;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsNonTaxStmt = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_COUNT_1);
      if (dsNonTaxStmt.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsNonTaxStmt.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    public int Insert1_In_stytranr(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string orig_journal, int doc_no, string check_no, DateTime pay_date, DateTime eop_date)
    {
      //int success = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[11];
        parameters[0] = orig_journal;
        parameters[1] = doc_no;
        parameters[2] = check_no;
        parameters[3] = pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;

        //Parameters used For Only SQL Server
        parameters[5] = objDVOExceptionReports.InsertMachineInfo;
        parameters[6] = objDVOExceptionReports.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.InsertBy;
        parameters[8] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[9] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[10] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.INSERT_INS_1, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    System.Collections.ArrayList arrlistParameters4 = new System.Collections.ArrayList();
    public int Insert2_In_styactvd(ref object objTransection, string orig_journal, int doc_no, string act_code, string act_type, decimal amount, decimal number, decimal hour, decimal rate, int acct_no, string dept_code)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      try
      {
        object[] parameters = new object[16];
        parameters[0] = orig_journal;
        parameters[1] = doc_no;
        parameters[2] = act_code;
        parameters[3] = act_type;
        parameters[4] = amount;
        parameters[5] = number;
        parameters[6] = hour;
        parameters[7] = rate;
        parameters[8] = acct_no;
        parameters[9] = dept_code;

        //Parameters used For Only SQL Server
        parameters[10] = objDVOExceptionReports.InsertMachineInfo;
        parameters[11] = objDVOExceptionReports.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[12] = objDVOExceptionReports.InsertBy;
        parameters[13] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[14] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[15] = objDVOExceptionReports.UpdateBy;

        arrlistParameters4.Add(parameters);

        //object[] RetValue = new object[1];
        //object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.INSERT_INS_2);
        //if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        //    return 0;
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_INC1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd1, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd1;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC1);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_INC2(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd2, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd2;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;
        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC2);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_INC3(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd3, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd3;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC3);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_INC4(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_qtd4, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = inc_qtd4;
        parameters[1] = inc_ytd;
        parameters[2] = empl_code;
        parameters[3] = inc_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_INC4);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_DED1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd1, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd1;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED1);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_DED2(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd2, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd2;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED2);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_DED3(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd3, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd3;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED4);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_DED4(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_qtd4, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = ded_qtd4;
        parameters[1] = ded_ytd;
        parameters[2] = empl_code;
        parameters[3] = ded_code;
        parameters[4] = line_no;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED4);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_OBL1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd1, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd1;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL1);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_OBL2(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd2, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd2;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL2);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_OBL3(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd3, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd3;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL3);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_OBL4(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd4, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = obl_qtd4;
        parameters[1] = obl_ytd;
        parameters[2] = empl_code;
        parameters[3] = obl_code;
        parameters[4] = line_no;


        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OBL4);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public DataSet GetTableName()
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[0];

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTBLName = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_TABLENAME);
      return dsTBLName;
    }

    public int UPDATE_3_MasterEmployeeIncomes(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal inc_ytd, string empl_code, string inc_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = inc_ytd;
        parameters[1] = empl_code;
        parameters[2] = inc_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_3_MasterEmployeeIncomes);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_4_MasterEmployeeDeductions(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal ded_ytd, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = ded_ytd;
        parameters[1] = empl_code;
        parameters[2] = ded_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_4_MasterEmployeeDeductions);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_5_MasterEmployeeObligations(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_ytd, string empl_code, string obl_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = obl_ytd;
        parameters[1] = empl_code;
        parameters[2] = obl_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_5_MasterEmployeeObligations);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_6_7_MasterEmployeeDeductions(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, DateTime ded_date, string empl_code, string ded_code, int line_no)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[7];
        parameters[0] = ded_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[1] = empl_code;
        parameters[2] = ded_code;
        parameters[3] = line_no;


        //Parameters used For Only SQL Server             
        parameters[4] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[5] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[6] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_6_MasterEmployeeDeductions);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    #endregion Get the value to process chk_post()

    #region Get the value to process inc_post

    private List<DVOPayrollstypayid> GetIncomeDataForINC_Post(int DocNumber_stypayid)
    {
      List<DVOPayrollstypayid> objDVOPayrollstypayidlist = new List<DVOPayrollstypayid>();
      DVOPayrollstypayid objDVOPayrollstypayid = new DVOPayrollstypayid();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //DataSet dsNonTaxStmt = objDalBaseClass.GetData(ref parameter,typeof(DVOPayrollstypayid),objDVOPayrollstypayid.FIND_INCOME);
      //return dsNonTaxStmt;
      using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypayid), objDVOPayrollstypayid.FIND_INCOME))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOPayrollstypayid tempobjDVOPayrollstypayid = new DVOPayrollstypayid();
          tempobjDVOPayrollstypayid.inc_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.description_MasterIncCodes = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.amount = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0);
          tempobjDVOPayrollstypayid.add_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.number = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0);
          tempobjDVOPayrollstypayid.inc_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
          tempobjDVOPayrollstypayid.hours = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
          if (dr[7] == DBNull.Value)
            tempobjDVOPayrollstypayid.lo_inc_amt_null = true;
          tempobjDVOPayrollstypayid.lo_inc_amt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
          if (dr[8] == DBNull.Value)
            tempobjDVOPayrollstypayid.hi_inc_amt_null = true;
          tempobjDVOPayrollstypayid.hi_inc_amt = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0);
          tempobjDVOPayrollstypayid.acct_no = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
          tempobjDVOPayrollstypayid.Department = (dr[10] != DBNull.Value ? dr[10].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypayid.line_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
          objDVOPayrollstypayidlist.Add(tempobjDVOPayrollstypayid);

        }
      }
      return objDVOPayrollstypayidlist;
    }
    private int Get_empl_count(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsEmpl_Count = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_emp_count);
      if (dsEmpl_Count.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsEmpl_Count.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }
    private int Get_MIN_LINENUMBER(string empl_code, string Inc_code)
    {
      int lineNo = 0;
      if (dsMinLineId != null)
        if (dsMinLineId.Tables.Count > 0)
          if (dsMinLineId.Tables[0].Rows.Count > 0)
          {
            DataRow[] draLine = dsMinLineId.Tables[0].Select("inc_code = '" + Inc_code + "'" + " and empl_code='" + empl_code + "'");
            if (draLine.Length > 0)
            {
              lineNo = draLine[0][0] != DBNull.Value ? Convert.ToInt32(draLine[0][0]) : 0;
            }
          }
      return lineNo;
    }
    private int Get_MAX_LINENUMBER(string empl_code)
    {
      int lineNo = 0;
      if (dsMaxLineId != null)
        if (dsMaxLineId.Tables.Count > 0)
          if (dsMaxLineId.Tables[0].Rows.Count > 0)
          {
            DataRow[] draLine = dsMaxLineId.Tables[0].Select("empl_code='" + empl_code + "'");
            if (draLine.Length > 0)
            {
              lineNo = draLine[0][0] != DBNull.Value ? Convert.ToInt32(draLine[0][0]) : 0;
            }
          }
      return lineNo;

    }

    #endregion Get the value to process inc_post

    #region Get the value to process ded_post

    private static List<DVOPayrollstypaydd> GetIncomeDataForDED_Post(int DocNumber_stypayid)
    {
      List<DVOPayrollstypaydd> objDVOPayrollstypayddlist = new List<DVOPayrollstypaydd>();
      DVOPayrollstypaydd objDVOPayrollstypaydd = new DVOPayrollstypaydd();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayid;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypayid), objDVOPayrollstypaydd.FIND_DEDUCTION))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          DVOPayrollstypaydd tempobjDVOPayrollstypaydd = new DVOPayrollstypaydd();
          tempobjDVOPayrollstypaydd.ded_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.description_MasterIncCodes = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.amount = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0);
          tempobjDVOPayrollstypaydd.add_code = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0);
          if (dr[5] == DBNull.Value)
            tempobjDVOPayrollstypaydd.lo_ded_amt_null = true;
          tempobjDVOPayrollstypaydd.lo_ded_amt = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
          if (dr[6] == DBNull.Value)
            tempobjDVOPayrollstypaydd.hi_ded_amt_null = true;
          tempobjDVOPayrollstypaydd.hi_ded_amt = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
          tempobjDVOPayrollstypaydd.acct_no = (dr[7] != DBNull.Value ? Convert.ToInt32(dr[7]) : 0);
          tempobjDVOPayrollstypaydd.Department = (dr[8] != DBNull.Value ? dr[8].ToString().Trim() : string.Empty);
          tempobjDVOPayrollstypaydd.line_no = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
          objDVOPayrollstypayddlist.Add(tempobjDVOPayrollstypaydd);

        }
      }
      return objDVOPayrollstypayddlist;
    }

    private int Get_empl_countdd(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsEmpl_Count = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_emp_countdd);
      if (dsEmpl_Count.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsEmpl_Count.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    private int Get_MIN_LINENUMBERDD(string empl_code, string ded_code)
    {
      int lineNo = 0;
      if (dsMinLinedd != null)
        if (dsMinLinedd.Tables.Count > 0)
          if (dsMinLinedd.Tables[0].Rows.Count > 0)
          {
            DataRow[] draLine = dsMinLinedd.Tables[0].Select("ded_code = '" + ded_code + "'" + " and empl_code='" + empl_code + "'");
            if (draLine.Length > 0)
            {
              lineNo = draLine[0][0] != DBNull.Value ? Convert.ToInt32(draLine[0][0]) : 0;
            }
          }
      return lineNo;

    }

    private int Get_MAX_LINENUMBERDD(string empl_code)
    {
      int lineNo = 0;
      if (dsMaxLinedd != null)
        if (dsMaxLinedd.Tables.Count > 0)
          if (dsMaxLinedd.Tables[0].Rows.Count > 0)
          {
            DataRow[] draLine = dsMaxLinedd.Tables[0].Select("empl_code='" + empl_code + "'");
            if (draLine.Length > 0)
            {
              lineNo = draLine[0][0] != DBNull.Value ? Convert.ToInt32(draLine[0][0]) : 0;
            }
          }
      return lineNo;

    }

    public int UPDATE_DED_BALANCE(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, int RowID, decimal Pay_amount)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = RowID;
        parameters[1] = Pay_amount;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_DED_BALANCE);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    private DataSet Get_DED_BALANCE(string ded_code, string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = ded_code;
      parameter[1] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsdedBalance = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_DED_BALANCE);
      return dsdedBalance;
    }

    #endregion Get the value to process ded_post

    #region Get the value to process nss_check

    private DataSet Get_nss_check_Information(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTableName = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_CHECK_INFORMATION);
      return dsTableName;
    }

    private string Get_nss_check_ControlTable()
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[0];

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTableName = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_CHECK_CONTROL_TABLE);
      if (dsTableName.Tables[0].Rows.Count > 0)
      {
        return dsTableName.Tables[0].Rows[0][0].ToString().Trim();
      }
      else
      {
        return "";
      }

    }

    private int Get_nss_check_ControlTable_Count()
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[0];


      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsTabCount = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_CHECK_CONTROL_TABLE_COUNT);
      if (dsTabCount.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsTabCount.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    private string get_nss_ded_code()
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object[] param = new object[0];
      object dsTabCount = objDalBaseClass.ExecuteScalar(ref param, (new DVOExceptionReports().GET_NSS_DED_CODE));
      if (dsTabCount != null)
        return dsTabCount.ToString().Trim();
      else
        return string.Empty;
    }

    #endregion Get the value to process nss_check

    #region Get or set value to process during nss_post

    public int Insert1_NSS_POST(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string orig_journal, int doc_no, DateTime doc_date, string contract_no, decimal amount, int ref_doc_no, string description)
    {
      //int success = 0;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[13];
        parameters[0] = orig_journal;
        parameters[1] = doc_no;
        parameters[2] = doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[3] = contract_no;
        parameters[4] = amount;
        parameters[5] = ref_doc_no;
        parameters[6] = description;

        //Parameters used For Only SQL Server
        parameters[7] = objDVOExceptionReports.InsertMachineInfo;
        parameters[8] = objDVOExceptionReports.InsertDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[9] = objDVOExceptionReports.InsertBy;
        parameters[10] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[11] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[12] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.INSERT_NSS_POST_INSERT1);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public int UPDATE_NSS_POST_AcctUpd(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal Contribution_Total, int Contract_number)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = Contribution_Total;
        parameters[1] = Contract_number;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_NSS_POST_AcctUpd);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    private int Get_Count_stytranr(string orig_journal, int doc_number)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[2];
      parameter[0] = orig_journal;
      parameter[1] = doc_number;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsCount = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_NSS_POST_Count_stytranr);
      if (dsCount.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsCount.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }

    #endregion get or set value to process during nss_post

    #region Get the value to process obl_post

    public List<DVOPayrollStypayod> GetObligationDataForOBL_Post(int DocNumber_stypayod)
    {
      List<DVOPayrollStypayod> objDVOPayrollStypayodlist = new List<DVOPayrollStypayod>();
      DVOPayrollStypayod objDVOPayrollStypayod = new DVOPayrollStypayod();
      object[] parameter = new object[1];
      parameter[0] = DocNumber_stypayod;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      using (DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollStypayod), objDVOPayrollStypayod.FIND_OBLIGATION_POST))
      {
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          //             v_obl_code,v_description,v_amount,v_obl_rate,v_acct_no,v_department,
          //v_bal_acct_no,v_bal_dept,v_line_no,v_add_code
          DVOPayrollStypayod tempobjDVOPayrollStypayod = new DVOPayrollStypayod();
          tempobjDVOPayrollStypayod.obl_code = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.description_MasterIncCodes = (dr[1] != DBNull.Value ? dr[1].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.amount = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0);
          tempobjDVOPayrollStypayod.obl_rate = (dr[3] != DBNull.Value ? Convert.ToDecimal(dr[3]) : 0);
          tempobjDVOPayrollStypayod.acct_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
          tempobjDVOPayrollStypayod.Department = (dr[5] != DBNull.Value ? dr[5].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.bal_acct_no = (dr[6] != DBNull.Value ? Convert.ToInt32(dr[6]) : 0);
          tempobjDVOPayrollStypayod.bal_dept = (dr[7] != DBNull.Value ? dr[7].ToString().Trim() : string.Empty);
          tempobjDVOPayrollStypayod.line_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
          tempobjDVOPayrollStypayod.add_code = (dr[9] != DBNull.Value ? dr[9].ToString().Trim() : string.Empty);

          objDVOPayrollStypayodlist.Add(tempobjDVOPayrollStypayod);
        }
      }
      return objDVOPayrollStypayodlist;
    }
    public int Get_empl_count_obligation(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet dsEmpl_Count = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_emp_count_obl);
      if (dsEmpl_Count.Tables[0].Rows.Count > 0)
      {
        return Convert.ToInt32(dsEmpl_Count.Tables[0].Rows[0][0]);
      }
      else
      {
        return 0;
      }

    }
    public int GET_MIN_LINENUMBER_OBL(string empl_code, string Obl_code)
    {
      int lineNo = 0;
      if (dsMinLineod != null)
        if (dsMinLineod.Tables.Count > 0)
          if (dsMinLineod.Tables[0].Rows.Count > 0)
          {
            DataRow[] draLine = dsMinLineod.Tables[0].Select("obl_code = '" + Obl_code + "'" + " and empl_code='" + empl_code + "'");
            if (draLine.Length > 0)
            {
              lineNo = draLine[0][0] != DBNull.Value ? Convert.ToInt32(draLine[0][0]) : 0;
            }
          }
      return lineNo;
    }
    public int Get_MAX_LINENUMBER_OBL(string empl_code)
    {
      int lineNo = 0;
      if (dsMaxLineod != null)
        if (dsMaxLineod.Tables.Count > 0)
          if (dsMaxLineod.Tables[0].Rows.Count > 0)
          {
            DataRow[] draLine = dsMaxLineod.Tables[0].Select("empl_code='" + empl_code + "'");
            if (draLine.Length > 0)
            {
              lineNo = draLine[0][0] != DBNull.Value ? Convert.ToInt32(draLine[0][0]) : 0;
            }
          }
      return lineNo;

    }
    #endregion Get the value to process obl_post

    #region Get the value to process After_doc_number

    public int UPDATE_SICK_AND_VACATION(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sick_used, decimal sick_accum, decimal vac_accum, decimal vac_used)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[8];
        parameters[0] = empl_code;
        parameters[1] = sick_used;
        parameters[2] = sick_accum;
        parameters[3] = vac_accum;
        parameters[4] = vac_used;

        //Parameters used For Only SQL Server             
        parameters[5] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[6] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[7] = objDVOExceptionReports.UpdateBy;
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_SICK_AND_VACATION, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        {
          return 0;
        }
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
        //return 0;
      }
      return 1;
    }

    public int UPDATE_SICK_PAY_AND_VACATION(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, DateTime eop_date, string empl_code, decimal sick_used, decimal sick_accum, decimal vac_accum, decimal vac_used)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[9];
        parameters[0] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[1] = empl_code;
        parameters[2] = sick_used;
        parameters[3] = sick_accum;
        parameters[4] = vac_accum;
        parameters[5] = vac_used;
        //Parameters used For Only SQL Server             
        parameters[6] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[7] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[8] = objDVOExceptionReports.UpdateBy;

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_SICK_PAY_AND_VACATION, true);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        {
          return 0;
        }

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
        //return 0;
      }
      return 1;
    }

    public int UPDATE_OK_TO_POST_STATUS(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string oktopostStatus, int docNumber)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = oktopostStatus;
        parameters[1] = docNumber;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[4] = objDVOExceptionReports.UpdateBy;



        object o = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_OK_TO_POST_STATUS);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
        {
          return 0;
        }

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
        //return 0;
      }
      return 1;

    }


    //private static DataSet GET_ACT_DFLT_LIMIT(string empl_code, string Pay_code)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[2];
    //    parameter[0] = empl_code;
    //    parameter[1] = Pay_code;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsAccountDeft = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.GET_ACT_DFLT_LIMIT);
    //    return dsAccountDeft;

    //}
    private decimal GET_ACT_DFLT_DED_LIMIT(string empl_code, string Pay_code)
    {



      decimal amount = 0;
      if (dsDeductionLimits != null && dsDeductionLimits.Tables.Count > 0)
      {
        DataRow[] dra = dsDeductionLimits.Tables[0].Select("ded_code='" + Pay_code + "'" + " and empl_code='" + empl_code + "'");
        if (dra.Length > 0)
        {
          amount = dra[0][1] != DBNull.Value ? Convert.ToDecimal(dra[0][1]) : 0;//dflt_limit
        }
      }
      return amount;

      //DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      //object[] parameter = new object[2];
      //parameter[0] = empl_code;
      //parameter[1] = Pay_code;

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.GET_ACT_DFLT_LIMIT);
      //if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0)//dsAccountDeft
      //    return Convert.ToDecimal(obj);
      //return 0;

    }
    private decimal GET_ACT_DFLT_OBL_LIMIT(string empl_code, string Pay_code)
    {



      decimal amount = 0;
      if (dsOblgationLimits != null && dsOblgationLimits.Tables.Count > 0)
      {
        DataRow[] dra = dsOblgationLimits.Tables[0].Select("obl_code='" + Pay_code + "'" + " and empl_code='" + empl_code + "'");
        if (dra.Length > 0)
        {
          amount = dra[0][1] != DBNull.Value ? Convert.ToDecimal(dra[0][1]) : 0;//dflt_limit
        }
      }
      return amount;

      //DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      //object[] parameter = new object[2];
      //parameter[0] = empl_code;
      //parameter[1] = Pay_code;

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      //object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.GET_ACT_DFLT_LIMIT);
      //if (obj != DBNull.Value && obj != null && obj.ToString().Trim().Length > 0)//dsAccountDeft
      //    return Convert.ToDecimal(obj);
      //return 0;

    }

    //private static DataSet GET_ALLOWENCE(string empl_code)
    //{
    //    DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
    //    object[] parameter = new object[1];
    //    parameter[0] = empl_code;

    //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
    //    DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
    //    DataSet dsAllowence = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.GET_ALLOWENCE);
    //    return dsAllowence;

    //}

    private int GET_ALLOWENCE(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      object obj = objDalBaseClass.ExecuteScalar(ref parameter, objDVOExceptionReports.GET_ALLOWENCE);
      if (obj != null)//dsAllowence
        return Convert.ToInt32(obj);
      return 0;

    }

    #endregion Get the value to process After_doc_number

    #region get the value to process vac_time_accrue

    private static DataSet FIND_ACCRUAL_EMP_RECORD(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet accr_emp_details = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_ACCRUAL_EMP_RECORD);
      return accr_emp_details;

    }

    private static DataSet FIND_ACCRUAL_TIMED_DETAILS(string vac_accr_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = vac_accr_code.Trim();

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet accr_Timed_details = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_ACCRUAL_TIMED_DETAILS);
      return accr_Timed_details;

    }

    public void GET_ALL_ACCRUAL_TIMED_DETAILS()
    {

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      dsStyaccrr = objDalBaseClass.GetData("select accr_code, accr_method,accr_rate,accr_freq,accr_lapse from MasterAccuralCodes");
    }

    public static int Update_VAC_Ctr(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal vac_accr_ctr, decimal vac_incr_amt)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = vac_accr_ctr;
        parameters[2] = vac_incr_amt;

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[5] = objDVOExceptionReports.UpdateBy;

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_VAC_Ctr, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Allowed_VAC_Counter(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal New_vac_allowed, int a_diff)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = New_vac_allowed;
        parameters[2] = a_diff;

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_VAC_Allowed_VAC_Counter, true);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Lapse(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = empl_code;
        parameters[1] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate;
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_VAC_Lapse, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Lapse_Control(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal vac_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = vac_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Control_Lapse, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;
      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_VAC_Lapse_Control_1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal vac_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = vac_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Control_Lapse1, true);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    #endregion get the value to process vac_time_accrue

    #region get the value to process Sick_time_accrue

    private static DataSet FIND_ACCRUAL_SICK_EMP_RECORD(string empl_code)
    {
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      object[] parameter = new object[1];
      parameter[0] = empl_code;

      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DataSet accr_emp_details = objDalBaseClass.GetData(ref parameter, typeof(DVOExceptionReports), objDVOExceptionReports.FIND_ACCRUAL_SICK_EMP_RECORD);
      return accr_emp_details;

    }


    public static int UPDATE_SICK_Ctr(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sic_accr_ctr, decimal sic_incr_amt)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = sic_accr_ctr;
        parameters[2] = sic_incr_amt;


        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];
        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_SICK_Ctr, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_Sick_Allowed_Sick_Counter(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal New_sic_allowed, int a_diff)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = New_sic_allowed;
        parameters[2] = a_diff;


        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Sick_Allowed_Sick_Counter, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_SICK_Lapse_DATE(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[5];
        parameters[0] = empl_code;
        parameters[1] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[2] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[3] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[4] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.UpdateData_ByTransaction(ref objTransection, ref parameters, typeof(DVOExceptionReports), objDVOExceptionReports.UPDATE_SICK_Lapse_DATE);

        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    public static int UPDATE_Sick_Control_Lapse_DATE(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sic_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = sic_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Sick_Control_Lapse_DATE, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;

    }

    public static int UPDATE_Control_Lapse1_Without_Date(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, string empl_code, decimal sic_accr_ctr, DateTime eop_date)
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        object[] parameters = new object[6];
        parameters[0] = empl_code;
        parameters[1] = sic_accr_ctr;
        parameters[2] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

        //Parameters used For Only SQL Server             
        parameters[3] = objDVOExceptionReports.UpdateMachineInfo;
        parameters[4] = objDVOExceptionReports.UpdateDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        parameters[5] = objDVOExceptionReports.UpdateBy;

        //object[] RetValue = new object[1];

        object o = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, objDVOExceptionReports.UPDATE_Control_Lapse1_Without_Date, true);
        if (o == DBNull.Value || o.ToString().Trim().Length <= 0 || Convert.ToInt32(o) != 1)
          return 0;

      }
      catch (Exception ex)
      {
        if (objTransection != null)
          objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }
      return 1;
    }

    #endregion get the value to process Sick_time_accrue

    private void LoadAllPayrollGLAccountsData()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {

        dsPayrollGLAccounts = objDalBaseClass.GetData((new DVOGLPayrollGLAccounts()).GetGLAccounts());
        if (dsPayrollGLAccounts != null && dsPayrollGLAccounts.Tables.Count > 0)
        {
          dsPayrollGLAccounts.Tables[0].Columns[0].ColumnName = "acct_no";
          dsPayrollGLAccounts.Tables[0].Columns[1].ColumnName = "acct_type";
          dsPayrollGLAccounts.Tables[0].Columns[2].ColumnName = "keyvalue";
          dsPayrollGLAccounts.Tables[0].Columns[3].ColumnName = "acct_desc";
          dsPayrollGLAccounts.Tables[0].Columns[4].ColumnName = "incr_with_crdt";
        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }

    private void LoadAllPayrollDepartments()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {

        dsPayrollDepartments = objDalBaseClass.GetData(typeof(DVOFlexSegment), (new DVOFlexSegment()).GET_SEGMENT_Data.ToString());
        if (dsPayrollDepartments != null && dsPayrollDepartments.Tables.Count > 0)
        {
          dsPayrollDepartments.Tables[0].Columns[0].ColumnName = "keyvalue";
          dsPayrollDepartments.Tables[0].Columns[1].ColumnName = "acct_desc";

        }
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
    }
    public string GetAccountInfo(int accountNo, out string acctDesc)
    {
      acctDesc = string.Empty;
      //LoadAllPayrollGLAccountsData();
      string keyvalue = string.Empty;
      if (dsPayrollGLAccounts != null && dsPayrollGLAccounts.Tables.Count > 0)
      {
        DataRow[] drs = dsPayrollGLAccounts.Tables[0].Select("acct_no =" + accountNo);
        if (drs.Length > 0)
        {
          keyvalue = drs[0]["keyvalue"].ToString().Trim();
          acctDesc = drs[0]["acct_desc"].ToString().Trim();
        }
      }
      return keyvalue;

    }

    public string GetAccountInfo(string keyvalue)
    {
      string acctDesc = string.Empty;
      if (dsPayrollGLAccounts != null && dsPayrollGLAccounts.Tables.Count > 0)
      {
        DataRow[] drs = dsPayrollGLAccounts.Tables[0].Select("keyvalue = '" + keyvalue + "'");
        if (drs.Length > 0)
        {
          acctDesc = drs[0]["acct_desc"].ToString().Trim();
        }
      }
      return acctDesc;

    }

    public string GetDepartmentinfo(string keyvalue)
    {
      if (keyvalue.Length >= 3)
      {
        keyvalue = keyvalue.Substring(0, 3);
      }
      string acctDesc = string.Empty;
      if (dsPayrollDepartments != null && dsPayrollDepartments.Tables.Count > 0)
      {
        DataRow[] drs = dsPayrollDepartments.Tables[0].Select("keyvalue = '" + keyvalue + "'");
        if (drs.Length > 0)
        {
          acctDesc = drs[0]["acct_desc"].ToString().Trim();
        }
      }
      return acctDesc;

    }


    DataSet dsDuplicateSSN = new DataSet();
    private void LoadAllDupSSN()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        dsDuplicateSSN = objDalBaseClass.GetData((new DVOPayrollautopay()).FIND_DUPSSN_QUERY());
        if (dsDuplicateSSN != null && dsDuplicateSSN.Tables.Count > 0)
        {
          dsDuplicateSSN.Tables[0].Columns[0].ColumnName = "SSN";
          dsDuplicateSSN.Tables[0].Columns[1].ColumnName = "COUNT";
        }
      }
      catch (Exception ex) { }
    }

    public static int qtr_number(DateTime pay_date)
    {
      if (pay_date.Month == 1 || pay_date.Month == 2 || pay_date.Month == 3)
        return 1;
      else if (pay_date.Month == 4 || pay_date.Month == 5 || pay_date.Month == 6)
        return 2;
      else if (pay_date.Month == 7 || pay_date.Month == 8 || pay_date.Month == 9)
        return 3;
      else
        return 4;
    }

    //object[] parameters = new object[25];
    System.Collections.ArrayList arrlistParameters1 = new System.Collections.ArrayList();
    public void MakePyTrx(ref DVOPostTrx ObjPostTransactions, string check_no, DateTime pay_date, DateTime eop_date, string[] period, ref object objTransection)
    {

      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {


        object[] parameters = new object[13];
        parameters[0] = ObjPostTransactions.orig_journal;
        parameters[1] = ObjPostTransactions.doc_no;
        parameters[2] = ObjPostTransactions.post_no;
        parameters[3] = ObjPostTransactions.post_date;
        parameters[4] = ObjPostTransactions.doc_date;
        if (ObjPostTransactions.ref_code.Trim().Length > 6)
          ObjPostTransactions.ref_code = ObjPostTransactions.ref_code.Substring(0, 5);
        parameters[5] = ObjPostTransactions.ref_code.Trim();
        if (ObjPostTransactions.doc_desc.Trim().Length > 30)
          ObjPostTransactions.doc_desc = ObjPostTransactions.doc_desc.Substring(0, 29);
        parameters[6] = ObjPostTransactions.doc_desc.Trim();
        parameters[7] = "";
        parameters[8] = check_no;
        parameters[9] = pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[10] = eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture); ;
        parameters[11] = period[0];
        parameters[12] = period[1];
        arrlistParameters1.Add(parameters);

        //object Result = objDALBaseClass.ExecuteProcedure_ByTransaction(ref objTransection, ref parameters, ObjPostTransactions.PY_TRX, true);
        //if (Result == DBNull.Value || Result == null || Result.ToString().Trim().Length <= 0 || Convert.ToInt32(Result)!=1)
        //    throw new Exception("Error has occurred while posting PY Trx.");


      }
      catch (Exception ex)
      {
        //if (objTransection != null)
        //objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        ExceptionManager.Publish(ex);
        throw ex;
      }

    }

    //System.Collections.ArrayList arrlistParameters1 = new System.Collections.ArrayList();
    System.Collections.ArrayList arrlistParameters2 = new System.Collections.ArrayList();
    System.Collections.ArrayList arrlistParameters3 = new System.Collections.ArrayList();

    public DVOPostGLGlobal Gl_post(ref DVOPostGL ObjPostGLTransaction, ref DVOPostGLGlobal ObjPostGLTransactionGlobal, ref object objTrx)
    {

      //int Status = 0;
      //string Doc_Status = "N";
      //int getid;      // something to get a rowid into
      //Int32 wait_win; //: has wait window been opened?,
      //Int32 wait_counter;// number of times to wait
      //Int32 trx_status;
      //bool sql_error = false;
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        if (ObjPostGLTransaction.doc_no != ObjPostGLTransactionGlobal.next_doc_no)
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

          //if (ObjPostGLTransaction.post_or_check == "POST")
          //{
          //object[] parameters = new object[5];
          //parameters[0] = ObjPostGLTransaction.orig_journal;
          //parameters[1] = ObjPostGLTransaction.doc_no;
          //parameters[2] = period[0];
          //parameters[3] = period[1];
          //parameters[4] = "N";

          //arrlistParameters1.Add(parameters);
          //object result1 = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, (new DVOGLTransH()).PY_STGTRANR_INS, true);
          //if (result1==DBNull.Value || result1==null || result1.ToString().Trim().Length<=0 || Convert.ToInt32(result1) != 1)
          //{
          //    throw new Exception("Error has occurred while posting into (stgtranr).");
          //}
          //}
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
        if (ObjPostGLTransaction.post_or_check == "POST")
        {
          object[] stgactvdParameters = new object[6];
          stgactvdParameters[0] = ObjPostGLTransaction.orig_journal;
          stgactvdParameters[1] = ObjPostGLTransaction.doc_no;
          stgactvdParameters[2] = ObjPostGLTransaction.acct_no;
          stgactvdParameters[3] = ObjPostGLTransaction.department;
          stgactvdParameters[4] = ObjPostGLTransaction.amount;
          stgactvdParameters[5] = ObjPostGLTransaction.debit_credit;

          arrlistParameters2.Add(stgactvdParameters);
          //object result = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref stgactvdParameters, (new DVOGLTRanActVD()).INSERT_STGACTVD, true);
          //if (result == DBNull.Value || result == null || result.ToString().Trim().Length <= 0 || Convert.ToInt32(result) != 1)
          //{
          //    throw new Exception("Error has occurred while posting into (stgactvd).");
          //}
          //Check this account no exist in stxckrgr then make an entry into stxchrgd 
          //*******************************************************
          if (ObjPostGLTransaction.department.Trim().Length <= 0)
            ObjPostGLTransaction.department = "000";
          Object[] Parameters = new Object[7];
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

          arrlistParameters3.Add(Parameters);

          //object Insresult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref Parameters, ObjPostGLTransaction.INSERT_PY_STXCKRGD, true);
          //if (Insresult == DBNull.Value || Insresult == null || Insresult.ToString().Trim().Length <= 0 || Convert.ToInt32(Insresult) != 1)
          //{
          //    throw new Exception("Error has occurred while posting into (stxchrgd).");
          //}
        }
        //if (sql_error)
        //{
        //    ObjPostGLTransactionGlobal.sql_error = 1;
        //}
        //else
        //{
        //    ObjPostGLTransactionGlobal.sql_error = 0;
        //}
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        //if (objTransection != null)
        //    objDALBaseClassHelper.RollbackTransaction(ref objTransection);
        //ex.HelpLink = ObjPostGLTransaction.doc_no.ToString();
        throw ex;
      }
      return ObjPostGLTransactionGlobal;

    }
    public string[] AcctChrTn(Int32 acct_no)
    {
      string[] Acctdetails = new string[2];
      if (dsPayrollGLAccounts != null && dsPayrollGLAccounts.Tables.Count > 0)
      {
        DataRow[] dra = dsPayrollGLAccounts.Tables[0].Select("acct_no=" + acct_no);
        if (dra.Length > 0)
        {
          Acctdetails[0] = dra[0]["acct_desc"].ToString().Trim();
          Acctdetails[1] = dra[0]["incr_with_crdt"].ToString().Trim();

        }
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

      return Acctdetails;
    }
    public string[] what_period(DateTime ObjTrxDate)
    {
      string[] period = new string[2];
      period[0] = string.Empty;
      period[1] = string.Empty;
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

    DataSet dsAllSegmentValue = new DataSet();
    private void GetALLSegmentValInformation()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      dsAllSegmentValue = objDalBaseClass.GetData(typeof(DVOExceptionReports), objDVOExceptionReports.GET_ALL_SEGMENT);
      if (dsAllSegmentValue != null && dsAllSegmentValue.Tables.Count > 0)
      {
        dsAllSegmentValue.Tables[0].Columns[0].ColumnName = "keyvalue";
        dsAllSegmentValue.Tables[0].Columns[1].ColumnName = "position";
        dsAllSegmentValue.Tables[0].Columns[2].ColumnName = "length";
        dsAllSegmentValue.Tables[0].Columns[3].ColumnName = "abbreviation";
        dsAllSegmentValue.Tables[0].Columns[4].ColumnName = "code";
        dsAllSegmentValue.Tables[0].Columns[5].ColumnName = "accounttype";
      }
    }

    //To Call this function 
    //Pass Parameter : EntityType , Code  And Account Type
    //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
    //This Function calculate the keyvalue and return it as a string value     
    public string Flexseg_Load(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      int locPosition, locLength;
      //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
      List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
      int locKeyLength;
      string ret_keyvalue = string.Empty;
      string copyKeyval = string.Empty;
      //DataSet loadFlexFeptPrepDS = GetSegmentValInformation(ref tempDvoFexprm_entity_code_accType);
      DataRow[] loadFlexFeptPrepDra = GetSegmentValInformation(ref tempDvoFexprm_entity_code_accType);
      int KeyLengthPrep = GetKeyLengthInformation(ref tempDvoFexprm_entity_code_accType);

      //Getting KeyLength value
      if (KeyLengthPrep > 0)
      {
        locKeyLength = Convert.ToInt32(KeyLengthPrep);
        if (locKeyLength > 0)
        {
          //Concatination the ret_value with #, Uptill the length of locKeyvalue
          for (int i = 1; i <= locKeyLength; i++)
          {
            ret_keyvalue = ret_keyvalue + "#";
          }
        }
      }
      if (loadFlexFeptPrepDra.Length > 0)
      {
        foreach (DataRow dr in loadFlexFeptPrepDra)
        {
          DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon();
          tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
          tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
          tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
          tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

          //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
          locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
          locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
          copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

          StringBuilder retKeyValueBuilder = new StringBuilder(ret_keyvalue);

          // Replace characters using a foreach loop
          int startIndex = locPosition - 1; // Adjust for zero-based index
          foreach (char c in copyKeyval)
          {
            if (startIndex < retKeyValueBuilder.Length) // Check if the index is within bounds
            {
              retKeyValueBuilder[startIndex] = c; // Replace character at the startIndex
              startIndex++;
            }
          }

          // Convert StringBuilder back to string
          ret_keyvalue = retKeyValueBuilder.ToString();

          //tempobjDvoFlexSegVal.Ret_Keyvalue = ret_keyvalue;
          objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
        }

      }
      return ret_keyvalue;

    }

    //Function used to get the information of segment value
    //Filter Criteria "Entity_Type,Account_Type and Code"
    // Value Return Keyvalue,Position,Length,Abbreviation
    private DataRow[] GetSegmentValInformation(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      //DataSet ds = null;
      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      //object[] parameters = new object[3];
      DataRow[] dra = null;
      try
      {

        dra = dsAllSegmentValue.Tables[0].Select("code='" + tempDvoFexprm_entity_code_accType.Code + "' and accounttype='" + tempDvoFexprm_entity_code_accType.AccountType + "'", "position");

        //parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
        //parameters[1] = tempDvoFexprm_entity_code_accType.Code;
        //parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;

        //ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), tempDvoFexprm_entity_code_accType.FIND_SPNAME);


        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    parameters = null;
        //    objDALBaseClass = null;
        //    return ds;
        //}
      }
      catch (Exception ex)
      {
        //parameters = null;
        //objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }
      return dra;
    }

    //Function used to get the length of the keyvalue, According to account type.
    //Table used ingflxkh
    private int GetKeyLengthInformation(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
    {
      int RetKeyval = 0;
      //DataSet ds = null;
      //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      //DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
      try
      {
        //object[] parameters = new object[1];
        //parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;
        DataRow[] dra = dsAllKeyLength.Tables[0].Select("accounttype='" + tempDvoFexprm_entity_code_accType.AccountType + "'");
        //ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
        {
          if (dra.Length > 0)
            RetKeyval = Convert.ToInt32(dra[0]["keylength"]);
        }
        //parameters = null;
        //objDALBaseClass = null;
      }
      catch (Exception ex)
      {
        //objDALBaseClass = null;
        ExceptionManagement.ExceptionManager.Publish(ex);
        throw ex;
      }
      return RetKeyval;
    }

    DataSet dsAllKeyLength = new DataSet();
    private void GetAllKeyLength()
    {
      DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
      DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
      DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
      dsAllKeyLength = objDalBaseClass.GetData(typeof(DVOExceptionReports), objDVOExceptionReports.Get_ALL_KEY_LENGHT);
      if (dsAllKeyLength != null && dsAllKeyLength.Tables.Count > 0)
      {
        dsAllKeyLength.Tables[0].Columns[0].ColumnName = "keylength";
        dsAllKeyLength.Tables[0].Columns[1].ColumnName = "accounttype";
      }
    }


    public static DataSet GetEmployeeToAssignManualChecks()
    {
      DataSet ds = null;
      try
      {
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
        Object[] parameters = new Object[12];
        parameters[10] = "POST";
        parameters[11] = "Y";
        ds = objDalBaseClass.GetData(objDVOExceptionReports.FIND_QUERY(ref parameters));
      }
      catch (Exception ex)
      {
        ExceptionManagement.ExceptionManager.Publish(ex);
        ds = new DataSet();
      }

      return ds;
    }

    public static int AssignManualCheckNoToEmployee(long ProcessId, long CheckNo, int ForceUpdate, out string ErrorMessage)
    {
      ErrorMessage = string.Empty;
      try
      {
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
        DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
        Object[] parameters = new Object[3];
        parameters[0] = ProcessId;
        parameters[1] = CheckNo;
        parameters[2] = ForceUpdate;

        object result = objDalBaseClass.ExecuteScalar(ref parameters, objDVOExceptionReports.AssignManualCheckNoToEmployee);
        if (result != null && result.ToString().Length > 0)
          if (Convert.ToInt32(result) == 1)
            return 1;
          else if (Convert.ToInt32(result) == 2)
            return 2;
      }
      catch (Exception ex)
      {
        ErrorMessage = ex.Message;
        ExceptionManagement.ExceptionManager.Publish(ex);
      }

      return 0;
    }

    #region IDisposable Members

    public void Dispose() { }

    #endregion
  }
}
