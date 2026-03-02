using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public class DVOExceptionReports : DVOBase
  {
    private string _InsertMachineInfo;
    private DateTime _InsertDate;
    private int _InsertBy;

    private string _UpdateMachineInfo;
    private DateTime _UpdateDate;
    private int _UpdateBy;

    public DVOExceptionReports()
    {
      _InsertMachineInfo = "App";
      _InsertDate = DateTime.Now;
      _InsertBy = -1;
      _UpdateMachineInfo = "App";
      _UpdateDate = DateTime.Now;
      _UpdateBy = -1;
    }

    //Properties used for only SQL Server

    public string InsertMachineInfo
    {
      get { return _InsertMachineInfo; }
      set { _InsertMachineInfo = value; }
    }
    public DateTime InsertDate
    {
      get { return _InsertDate; }
      set { _InsertDate = value; }
    }
    public int InsertBy
    {
      get { return _InsertBy; }
      set { _InsertBy = value; }
    }
    public string UpdateMachineInfo
    {
      get { return _UpdateMachineInfo; }
      set { _UpdateMachineInfo = value; }
    }
    public DateTime UpdateDate
    {
      get
      {
        return _UpdateDate;
      }
      set { _UpdateDate = value; }
    }
    public int UpdateBy
    {
      get { return _UpdateBy; }
      set { _UpdateBy = value; }
    }



    #region Stored-Procedures

    public override string INSERT_SPNAME
    {
      get { return ""; }
    }

    public override string UPDATE_SPNAME
    {
      get { return ""; }
    }

    public override string DELETE_SPNAME
    {
      get { return ""; }
    }

    public override string FIND_SPNAME
    {
      get { return ""; }
    }

    public override string ALL_SPNAME
    {
      get { return ""; }
    }
    public override string TABLE_NAME
    {
      get { return ""; }
    }

    public override int UNIQUE_ID
    {
      get { return 0; }
    }

    public override string NOTES_TABLE_RECORD_ID
    {
      get { return string.Empty; }
      set { throw new Exception("The method or operation is not implemented."); }
    }

    public string FIND_CSH_ACCOUNT_AMOUNT
    {
      get { return "uspCshAccAmtget"; }//uspcshaccamtget
    }

    public string FIND_SUM_DED_YTD
    {
      get { return "uspSumdedytdget"; }//uspsumdedytdget
    }

    public string GET_ALL_SEGMENT
    {
      get { return "USP_PYSegValComGet"; }
    }
    public string Get_ALL_KEY_LENGHT
    {
      get { return "USP_PYSegComKeyLen"; }
    }
    public string FIND_ALL_SUM_DED_YTD(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();

      sql.Append(" select sum(MasterEmployeeDeductions.ded_ytd),MasterEmployeeDeductions.ded_code,MasterEmployee.soc_sec_num");
      sql.Append(" from MasterEmployeeDeductions, MasterEmployee");
      sql.Append(" where MasterEmployeeDeductions.empl_code = MasterEmployee.empl_code ");
      sql.Append(" and MasterEmployee.soc_sec_num in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeDeductions.ded_code,MasterEmployee.soc_sec_num");
      return sql.ToString();
    }

    public string FIND_SUM_DED_YTD1
    {
      get { return "uspSumdedytd1get"; }//uspsumdedytd1get
    }
    public string FIND_ALL_SUM_DED_YTD1(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" Select sum(MasterEmployeeDeductions.ded_ytd),MasterEmployeeDeductions.ded_code,MasterEmployeeDeductions.empl_code");
      sql.Append(" from MasterEmployeeDeductions where 1=1 ");
      sql.Append(" and MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeDeductions.ded_code,MasterEmployeeDeductions.empl_code");

      return sql.ToString();
    }

    public string FIND_SUM_OBLYTD
    {
      get { return "uspSumoblytdget"; }//uspsumoblytdget
    }
    public string FIND_ALL_SUM_OBLYTD(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select sum(MasterEmployeeObligations.obl_ytd),MasterEmployeeObligations.obl_code,MasterEmployee.soc_sec_num");
      sql.Append(" from MasterEmployeeObligations, MasterEmployee");
      sql.Append(" where MasterEmployeeObligations.empl_code = MasterEmployee.empl_code ");
      sql.Append(" and MasterEmployee.soc_sec_num in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeObligations.obl_code,MasterEmployee.soc_sec_num");
      return sql.ToString();
    }
    public string FIND_SUM_OBLYTD1
    {
      get { return "uspSumoblytd1get"; }//uspsumoblytd1get
    }

    public string FIND_ALL_SUM_OBLYTD1(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select sum(MasterEmployeeObligations.obl_ytd),MasterEmployeeObligations.obl_code,MasterEmployeeObligations.empl_code");
      sql.Append(" from MasterEmployeeObligations");
      sql.Append(" where MasterEmployeeObligations.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeObligations.obl_code,MasterEmployeeObligations.empl_code");
      return sql.ToString();
    }
    public string FIND_ALL_OBl_DFLT_LIMITS(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select DISTINCT MasterEmployeeObligations.obl_limit, MasterOblCodes.dflt_limit,");
      sql.Append(" MasterEmployeeObligations.obl_code,MasterEmployeeObligations.empl_code ");
      sql.Append(" from MasterEmployeeObligations, MasterOblCodes");
      sql.Append(" where  MasterOblCodes.obl_code = MasterEmployeeObligations.obl_code");
      sql.Append(" and MasterEmployeeObligations.empl_code in (" + parameters[0].ToString() + ")");
      return sql.ToString();
    }


    public string FIND_SUM_SICK_VACATION
    {
      get { return "uspSumSickVacget"; }//uspsumsickvacget
    }
    public string FIND_ALL_SUM_SICK_VACATION(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select sum(MasterEmployee.sick_used),sum(MasterEmployee.vac_used),MasterEmployee.soc_sec_num");
      sql.Append(" from MasterEmployee");
      sql.Append(" where MasterEmployee.soc_sec_num in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployee.soc_sec_num");
      return sql.ToString();
    }
    public string FIND_SUM_BASIC_AMOUNT
    {
      get { return "uspSumBasicAmtget"; }//uspsumbasicamtget
    }

    public string FIND_SUM_TAXABLE_STMT
    {
      get { return "uspTaxSalStmtget"; }//usptaxsalstmtget
    }

    public string FIND_SUM_NON_TAXABLE_STMT
    {
      get { return "uspNonTaxSalStmget"; }//uspnontaxsalstmget
    }
    //Procedure to call while before doc number
    public string FIND_SUM_DED_TOTAL
    {
      get { return "uspsumdedTotalget"; }//uspsumdedtotalget
    }

    public string FIND_SUM_DED_TOTAL_SSD
    {
      get { return "uspsumdedssdget"; }
    }

    public string FIND_SUM_DED_TOTAL_SSl
    {
      get { return "uspsumdedsslget"; }
    }
    public string FIND_SUM_OBL_TOTAL_SSD
    {
      get { return "uspsumoblssdget"; }
    }
    public string FIND_SUM_OBL_TOTAL_SSlB
    {
      get { return "uspsumoblssibget"; }
    }
    //Added By ROhit 
    public string FIND_SUM_ANY_Income
    {
      get { return "uspsumanyincome"; }
    }
    public string FIND_BONUS_CHECK
    {
      get { return "uspbonuschkget"; }
    }
    public string GET_NSS_DED_CODE
    {
      get { return "uspnssdedcode"; }
    }
    //End Procedure to call while before doc number

    #region procedure to call on chk_post
    public string UPDATE_INC1
    {
      get { return "uspchkpostinc1upd"; }
    }

    public string UPDATE_INC2
    {
      get { return "uspchkpostinc2upd"; }
    }

    public string UPDATE_INC3
    {
      get { return "uspchkpostinc3upd"; }
    }

    public string UPDATE_INC4
    {
      get { return "uspchkpostinc4upd"; }
    }


    public string UPDATE_DED1
    {
      get { return "uspchkpostded1upd"; }
    }

    public string UPDATE_DED2
    {
      get { return "uspchkpostded2upd"; }
    }

    public string UPDATE_DED3
    {
      get { return "uspchkpostded3upd"; }
    }

    public string UPDATE_DED4
    {
      get { return "uspchkpostded4upd"; }
    }

    public string UPDATE_OBL1
    {
      get { return "uspchkpostobl1upd"; }
    }

    public string UPDATE_OBL2
    {
      get { return "uspchkpostobl2upd"; }
    }

    public string UPDATE_OBL3
    {
      get { return "uspchkpostobl3upd"; }
    }

    public string UPDATE_OBL4
    {
      get { return "uspchkpostobl4upd"; }
    }

    public string FIND_COUNT_1
    {
      get { return "uspchkpostcnt1get"; }
    }

    public string INSERT_INS_1
    {
      get { return "uspchkpost1ins"; }
    }

    public string INSERT_INS_2
    {
      get { return "uspchkpost2ins"; }
    }

    public string FIND_TABLENAME
    {
      get { return "uspchkpostTblget"; }
    }

    public string UPDATE_3_MasterEmployeeIncomes
    {
      get { return "uspchkpost3upd"; }
    }

    public string UPDATE_4_MasterEmployeeDeductions
    {
      get { return "uspchkpost4upd"; }
    }

    public string UPDATE_5_MasterEmployeeObligations
    {
      get { return "uspchkpost5upd"; }
    }

    public string UPDATE_6_MasterEmployeeDeductions
    {
      get { return "uspchkpost6upd"; }
    }

    //Procedure to call during lockint
    public string GET_MasterEmployeeIncomes_ROWID
    {
      get { return "uspMasterEmployeeIncomesrowget"; }
    }

    public string GET_MasterEmployeeDeductions_ROWID
    {
      get { return "uspMasterEmployeeDeductionsrowget"; }
    }

    public string GET_MasterEmployeeObligations_ROWID
    {
      get { return "uspMasterEmployeeObligationsrowget"; }
    }

    #endregion procedure to call on chk_post

    #region procedure to call on inc_post

    public string FIND_emp_count
    {
      get { return "uspincpostempcount"; }
    }

    public string FIND_MIN_LINENUMBER
    {
      get { return "uspincpostminline"; }
    }
    public string FIND_MAX_LINENUMBER
    {
      get { return "uspincpostmaxline"; }
    }

    #endregion procedure to call on inc_post


    #region procedure to call on ded_post

    public string FIND_emp_countdd
    {
      get { return "uspdedpostempcount"; }
    }

    public string FIND_MIN_LINENUMBERDD
    {
      get { return "uspdedpostminline"; }
    }
    public string FIND_MAX_LINENUMBERDD
    {
      get { return "uspdedpostmaxline"; }
    }

    public string FIND_DED_BALANCE
    {
      get { return "uspdedPostbalget"; }
    }

    public string UPDATE_DED_BALANCE
    {
      get { return "uspdedpostbalupd"; }
    }


    #endregion procedure to call on ded_post

    #region Procedure to call on nss_check

    public string FIND_NSS_CHECK_INFORMATION
    {
      get { return "uspnsschkinfoget"; }
    }

    public string FIND_NSS_CHECK_CONTROL_TABLE
    {
      get { return "uspnsschktableget"; }
    }

    public string FIND_NSS_CHECK_CONTROL_TABLE_COUNT
    {
      get { return "uspnsschkcountget"; }
    }

    #endregion Procedure to call on nss_check

    #region Procedure to call on nss_post

    public string INSERT_NSS_POST_INSERT1
    {
      get { return "uspnsspost1ins"; }
    }

    public string UPDATE_NSS_POST_AcctUpd
    {
      get { return "uspNSSpostAcctupd"; }
    }
    public string FIND_NSS_POST_Count_stytranr
    {
      get { return "uspnsspostCountget"; }
    }

    #endregion Procedure to call on nss_post

    #region procedure to call on obl_post

    public string FIND_emp_count_obl
    {
      get { return "uspoblpostempcount"; }
    }

    public string FIND_MIN_LINENUMBER_OBL
    {
      get { return "uspoblpostminline"; }
    }
    public string FIND_MAX_LINENUMBER_OBL
    {
      get { return "uspoblpostmaxline"; }
    }

    #endregion procedure to call on inc_post

    #region procedure to call on after_doc_no
    public string UPDATE_SICK_AND_VACATION
    {
      get { return "uspdSickVacupd"; }//uspdsickvacupd
    }

    public string UPDATE_SICK_PAY_AND_VACATION
    {
      get { return "uspdSickVacPayupd"; }
    }

    public string UPDATE_OK_TO_POST_STATUS
    {
      get { return "USP_OktoPostUpd"; }
    }

    public string GET_ACT_DFLT_LIMIT
    {
      get { return "uspdeddfltLimGet"; }
    }

    public string FIND_ALL_ACT_DFLT_LIMIT(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select DISTINCT MasterEmployeeDeductions.ded_limit, MasterDedcodes.dflt_limit,MasterEmployeeDeductions.ded_code,MasterEmployeeDeductions.empl_code ");
      sql.Append(" from MasterEmployeeDeductions, MasterDedcodes");
      sql.Append(" where  MasterDedcodes.ded_code = MasterEmployeeDeductions.ded_code");
      sql.Append(" and MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString() + ")");

      return sql.ToString();
    }

    public string GET_ALLOWENCE
    {
      get { return "uspallowenceGet"; }//uspallowenceget
    }

    public string GetAllMaster_periods
    {
      get { return "uspaccperiodall"; }
    }
    #endregion procedure to call on after_doc_no


    #region procedure to call on vac_time_accrue
    public string FIND_ACCRUAL_EMP_RECORD
    {
      get { return "uspaccremprecGet"; }
    }

    public string FIND_ACCRUAL_TIMED_DETAILS
    {
      get { return "uspaccrTimeGet"; }//uspaccrtimeget
    }

    public string UPDATE_VAC_Ctr
    {
      get { return "uspvacaccrctrupd"; }
    }

    public string UPDATE_VAC_Allowed_VAC_Counter
    {
      get { return "uspvacallCtrupd"; }
    }

    public string UPDATE_VAC_Lapse
    {
      get { return "uspLapseDateupd"; }
    }

    public string UPDATE_Control_Lapse
    {
      get { return "uspCntLapseupd"; }
    }

    public string UPDATE_Control_Lapse1
    {
      get { return "uspCntLapse1upd"; }
    }
    #endregion procedure to call on vac_time_accrue


    #region procedure to call on Sick_time_accrue

    public string FIND_ACCRUAL_SICK_EMP_RECORD
    {
      get { return "uspsickemprecGet"; }//uspsickemprecget
    }

    //public string FIND_ACCRUAL_TIMED_DETAILS
    //{
    //    get { return "uspaccrTimeGet"; }
    //}

    public string UPDATE_SICK_Ctr
    {
      get { return "uspSickaccrctrupd"; }
    }

    public string UPDATE_Sick_Allowed_Sick_Counter
    {
      get { return "uspSickallCtrupd"; }
    }

    public string UPDATE_SICK_Lapse_DATE
    {
      get { return "uspLapSickDateupd"; }
    }

    public string UPDATE_Sick_Control_Lapse_DATE
    {
      get { return "uspCntDtLapseupd"; }
    }

    public string UPDATE_Control_Lapse1_Without_Date
    {
      get { return "uspCntDtLapse1upd"; }  //Update without Date
    }

    #endregion procedure to call on sick_time_accrue


    public override string FIND_QUERY(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();


      sql.Append("select MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
      sql.Append("MasterEmployee.last_pay,MasterEmployee.middle_name,MasterEmployee.pay_period,");
      sql.Append("MasterEmployee.sick_allowed,MasterEmployee.sick_code,MasterEmployee.sick_used,");
      sql.Append("MasterEmployee.soc_sec_num,MasterEmployee.terminated,MasterEmployee.vac_allowed,");
      sql.Append("MasterEmployee.vac_code,MasterEmployee.vac_used,Process_PayEmployee.cash_acct_no,");
      sql.Append("Process_PayEmployee.cash_amount, Process_PayEmployee.check_no,Process_PayEmployee.ded_fedtax,");
      sql.Append("Process_PayEmployee.ded_fica,Process_PayEmployee.ded_loctax,Process_PayEmployee.ded_medicare,");
      sql.Append("Process_PayEmployee.ded_other,Process_PayEmployee.ded_statax,Process_PayEmployee.department,");
      sql.Append("Process_PayEmployee.doc_date,Process_PayEmployee.doc_no,Process_PayEmployee.empl_code,");
      sql.Append("Process_PayEmployee.eop_date,Process_PayEmployee.inc_expense,Process_PayEmployee.inc_gross,");
      sql.Append("Process_PayEmployee.inc_net,Process_PayEmployee.inc_taxable,Process_PayEmployee.obl_fica,");
      sql.Append("Process_PayEmployee.obl_futa,Process_PayEmployee.obl_medicare,Process_PayEmployee.obl_other,");
      sql.Append("Process_PayEmployee.obl_total,Process_PayEmployee.pay_date,Process_PayEmployee.total_hours,");
      sql.Append("MasterEmployee.cash_acct emp_cash_acct,MasterEmployee.department emp_department,");
      sql.Append("Process_PayEmployee.accrue_vac,Process_PayEmployee.accrue_sick, Process_PayEmployee.print_check,");
      sql.Append("Process_PayEmployee.deposit,Process_PayEmployee.bonus, ");
      //Added by Sarvjeet on 08/02/2010..
      sql.Append(" MasterEmployee.vac_accr_code,MasterEmployee.vac_accr_ctr,MasterEmployee.vac_lapse_date, ");
      sql.Append(" MasterEmployee.sick_accr_code,MasterEmployee.sick_accr_ctr,MasterEmployee.sick_lapse_date,MasterEmployee.allowances ");
      sql.Append(" ,MasterEmployee.type_code,PayProcess_ID,MasterEmployee.ApplicationReferenceNo ");
      sql.Append(" from Process_PayEmployee with(nolock),MasterEmployee with(nolock) where Process_PayEmployee.empl_code = MasterEmployee.empl_code");

      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE  '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE  '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[2] != null)
        if (parameters[2].ToString().Trim() != string.Empty)
          sql.Append(" AND MasterEmployee.pay_period = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[3] != null)
        if (parameters[3].ToString().Trim() != string.Empty)
          sql.Append(" AND MasterEmployee.type_code  LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[4] != null)
        if (parameters[4].ToString().Trim() != string.Empty)
          sql.Append(" AND MasterEmployee.job_code = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[5] != null)
        if (parameters[5].ToString().Trim() != string.Empty)
          sql.Append(" AND MasterEmployee.job_title = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");

      if (parameters[6] != null)
        if (parameters[6].ToString() != string.Empty)
          sql.Append(" AND Process_PayEmployee.empl_code = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[7] != null)
        if (parameters[7].ToString().Trim() != string.Empty && !parameters[7].ToString().Trim().Contains("1900") && !parameters[7].ToString().Trim().Contains("0001"))
          //if (Convert.ToDateTime(parameters[7]) !=Convert.ToDateTime(null))
          sql.Append(" AND Process_PayEmployee.eop_date =  '" + parameters[7].ToString().Trim() + "'");
      if (parameters[8] != null)
        if (parameters[8].ToString().Trim() != string.Empty)
          if (parameters[8].ToString() != "0")
            sql.Append(" AND Process_PayEmployee.cash_acct_no=" + Convert.ToInt32(parameters[8]));
      if (parameters[9] != null)
        if (parameters[9].ToString().Trim() != string.Empty && !parameters[9].ToString().Trim().Contains("1900") && !parameters[9].ToString().Trim().Contains("0001"))
          //if (Convert.ToDateTime(parameters[9]) != Convert.ToDateTime(null))
          sql.Append(" AND (MasterEmployee.last_pay = '" + parameters[9].ToString().Trim() + "'" + " " + "or MasterEmployee.last_pay is null  or MasterEmployee.last_pay is not null)");
      if (parameters.Length > 10)
        if (parameters[10].ToString().Trim() != "POST")
          sql.Append(" and Process_PayEmployee.ok_to_post not in ('P', 'C')");
        else
          sql.Append(" and Process_PayEmployee.ok_to_post='Y'");

      if (parameters.Length > 12 && parameters[12] != null && parameters[12].ToString().Length > 0)
          sql.Append(" AND MasterEmployee.SelectDistrict IN (" + parameters[12].ToString() + ")");

      if (parameters.Length > 11 && parameters[11] != null && parameters[11].ToString().Length > 0)
        sql.Append(" AND Process_PayEmployee.print_check = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");


      return sql.ToString();

    }

    public string AssignManualCheckNoToEmployee
    {
      get { return "USP_PyAssignManualCheckNo"; }
    }


    public string GetPayAmounts(ref Object[] parameters)
    {
      StringBuilder sqlSubQuery = new StringBuilder();
      sqlSubQuery.Append(" select Process_PayEmployee.doc_no");
      sqlSubQuery.Append(" from Process_PayEmployee,MasterEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code");
      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND Rtrim(MasterEmployee.first_name) LIKE  '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND Rtrim(MasterEmployee.last_name) LIKE  '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[2] != null)
        if (parameters[2].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND MasterEmployee.pay_period = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[3] != null)
        if (parameters[3].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND MasterEmployee.type_code  LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[4] != null)
        if (parameters[4].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND MasterEmployee.job_code = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[5] != null)
        if (parameters[5].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND MasterEmployee.job_title = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");

      if (parameters[6] != null)
        if (parameters[6].ToString() != string.Empty)
          sqlSubQuery.Append(" AND Process_PayEmployee.empl_code = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[7] != null)
        if (parameters[7].ToString().Trim() != string.Empty && !parameters[7].ToString().Trim().Contains("1900") && !parameters[7].ToString().Trim().Contains("0001"))
          //if (Convert.ToDateTime(parameters[7]) !=Convert.ToDateTime(null))
          sqlSubQuery.Append(" AND Process_PayEmployee.eop_date =  '" + parameters[7].ToString().Trim() + "'");
      if (parameters[8] != null)
        if (parameters[8].ToString().Trim() != string.Empty)
          if (parameters[8].ToString() != "0")
            sqlSubQuery.Append(" AND Process_PayEmployee.cash_acct_no=" + Convert.ToInt32(parameters[8]));
      if (parameters[9] != null)
        if (parameters[9].ToString().Trim() != string.Empty && !parameters[9].ToString().Trim().Contains("1900") && !parameters[9].ToString().Trim().Contains("0001"))
          //if (Convert.ToDateTime(parameters[9]) != Convert.ToDateTime(null))
          sqlSubQuery.Append(" AND (MasterEmployee.last_pay = '" + parameters[9].ToString().Trim() + "'" + " " + "or MasterEmployee.last_pay is null  or MasterEmployee.last_pay is not null)");
      if (parameters.Length > 10)
        if (parameters[10].ToString().Trim() != "POST")
          sqlSubQuery.Append(" and Process_PayEmployee.ok_to_post not in ('P', 'C')");
        else
          sqlSubQuery.Append(" and Process_PayEmployee.ok_to_post='Y'");

      StringBuilder sql = new StringBuilder();
      //Get the basic income amount for this cheque
      sql.Append(" SELECT 1,Process_PayIncomes.doc_no, SUM(amount)");
      sql.Append(" FROM Process_PayIncomes  WHERE Process_PayIncomes.inc_code = 'BASIC' AND Process_PayIncomes.amount IS NOT NULL ");
      sql.Append(" AND Process_PayIncomes.doc_no in (" + sqlSubQuery + ")");
      sql.Append(" group by Process_PayIncomes.doc_no ");
      sql.Append(" UNION ");

      //Get the taxable other income for this cheque
      sql.Append(" SELECT 2,Process_PayIncomes.doc_no,SUM( amount ) FROM MasterIncCodes, Process_PayIncomes ");
      sql.Append(" WHERE Process_PayIncomes.inc_code = MasterIncCodes.inc_code");
      sql.Append(" and MasterIncCodes.inc_type <> 'F' AND MasterIncCodes.inc_code <> 'BASIC' AND Process_PayIncomes.amount IS NOT NULL ");
      sql.Append(" AND Process_PayIncomes.doc_no in (" + sqlSubQuery + ")");
      sql.Append(" group by Process_PayIncomes.doc_no ");
      sql.Append(" UNION ");

      //Get the non-taxable other income for this cheque
      sql.Append(" SELECT 3,Process_PayIncomes.doc_no , SUM( amount ) ");
      sql.Append(" FROM MasterIncCodes, Process_PayIncomes WHERE Process_PayIncomes.inc_code = MasterIncCodes.inc_code ");
      sql.Append(" AND MasterIncCodes.inc_type = 'F' AND MasterIncCodes.inc_code <> 'BASIC' AND Process_PayIncomes.amount IS NOT NULL ");
      sql.Append(" AND Process_PayIncomes.doc_no in (" + sqlSubQuery + ")");
      sql.Append(" group by Process_PayIncomes.doc_no ");
      sql.Append(" UNION ");

      //Calculate Deduction Total
      sql.Append(" select 4,Process_PayDeductions.doc_no,sum(amount) from Process_PayDeductions");
      sql.Append(" where Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
      sql.Append(" group by Process_PayDeductions.doc_no");

      if (parameters[10].ToString().Trim() == "CHECK")
      {
        sql.Append(" UNION ");
        //Get Sum Deduction Total FLI
        sql.Append(" SELECT 5,Process_PayDeductions.doc_no, SUM(amount)  FROM Process_PayDeductions ");
        sql.Append(" where Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" AND ded_code IN ('FLI')");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get Sum Deduction Total MASA
        sql.Append(" SELECT 6,Process_PayDeductions.doc_no,sum(amount) FROM Process_PayDeductions where ded_code IN ('MASA')");
        sql.Append(" and Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get SumObligation Total NCI
        sql.Append(" SELECT 7,Process_PayDeductions.doc_no, SUM(amount)  FROM Process_PayDeductions where ded_code IN ('NCI')");
        sql.Append(" and Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get SumObligation Total NEL
        sql.Append(" SELECT 8,Process_PayDeductions.doc_no, SUM(amount)  FROM Process_PayDeductions where ded_code IN ('NEL')"); //BA TO NEL
        sql.Append(" and Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get SumObligation Total AIS
        sql.Append(" SELECT 9,Process_PayDeductions.doc_no, SUM(amount)  FROM Process_PayDeductions where ded_code IN ('AIS')");//FMI TO AIS
        sql.Append(" and Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get Sum Obligation Total SSIB
        sql.Append(" SELECT 10, Process_PayDeductions.doc_no, SUM(amount)  FROM Process_PayDeductions where ded_code IN ('GOA', 'AMC', 'ATU','AC','ADB','ADF')");
        sql.Append(" and Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_PayDeductions.doc_no");
        /*
        //Get SumObligation Total SSD
        sql.Append(" SELECT 7,Process_Payobligations.doc_no, SUM(amount)  FROM Process_Payobligations where obl_code IN ('SOC-ER', 'SOC-CP', 'WSSD', 'SOC-DP', 'SOC-WG')");
        sql.Append(" and Process_Payobligations.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_Payobligations.doc_no");
        sql.Append(" UNION ");

        //Get Sum Obligation Total SSIB
        sql.Append(" SELECT 8, Process_Payobligations.doc_no, SUM(amount)  FROM Process_Payobligations where obl_code IN ('EIB', 'EIB-CP', 'EIB-DP','EIB-WG')");
        sql.Append(" and Process_Payobligations.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_Payobligations.doc_no");*/
      }

      /*if (parameters[10].ToString().Trim() == "CHECK")
      {
        sql.Append(" UNION ");
        //Get Sum Deduction Total SSD
        sql.Append(" SELECT 5,Process_PayDeductions.doc_no, SUM(amount)  FROM Process_PayDeductions ");
        sql.Append(" where Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" AND ded_code IN ('SSD','SOC-EE','WSSD')");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get Sum Deduction Total SSL
        sql.Append(" SELECT 6,Process_PayDeductions.doc_no,sum(amount) FROM Process_PayDeductions where ded_code IN ('LEVYEE')");
        sql.Append(" and Process_PayDeductions.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_PayDeductions.doc_no");
        sql.Append(" UNION ");

        //Get SumObligation Total SSD
        sql.Append(" SELECT 7,Process_Payobligations.doc_no, SUM(amount)  FROM Process_Payobligations where obl_code IN ('SOC-ER', 'SOC-CP', 'WSSD', 'SOC-DP', 'SOC-WG')");
        sql.Append(" and Process_Payobligations.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_Payobligations.doc_no");
        sql.Append(" UNION ");

        //Get Sum Obligation Total SSIB
        sql.Append(" SELECT 8, Process_Payobligations.doc_no, SUM(amount)  FROM Process_Payobligations where obl_code IN ('EIB', 'EIB-CP', 'EIB-DP','EIB-WG')");
        sql.Append(" and Process_Payobligations.doc_no in (" + sqlSubQuery + ")");
        sql.Append(" group by Process_Payobligations.doc_no");
      }*/

      return sql.ToString();

    }

    public string GetSumIncomes(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" SELECT SUM(amount),Sum(hours),Sum(inc_rate),doc_no,inc_code");
      sql.Append(" FROM Process_PayIncomes ");
      sql.Append(" WHERE Process_PayIncomes.doc_no in (" + parameters[0].ToString() + ")");
      sql.Append(" AND inc_code in('REGPY','DT','OT') group by doc_no,inc_code");
      return sql.ToString();
    }
    public string GetDDAccountAmount(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" SELECT MasterBanks.cash_acct_no, Process_DirectDeposit_Details.amount,Process_DirectDeposit_Details.pay_doc_no,Process_DirectDeposit_Details.empl_code");
      sql.Append(" FROM MasterBanks, Process_DirectDeposit_Header, Process_DirectDeposit_Details");
      sql.Append(" WHERE Process_DirectDeposit_Details.pay_doc_no in (" + parameters[0].ToString() + ")");
      //sql.Append(" and Process_DirectDeposit_Details.empl_code in (" + parameters[1].ToString() + ")");
      sql.Append(" and MasterBanks.bank_code = Process_DirectDeposit_Header.bank_code");
      sql.Append(" AND Process_DirectDeposit_Header.doc_no = Process_DirectDeposit_Details.doc_no");

      return sql.ToString();
    }


    public string GetMinLineforInc(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select min(line_no),MasterEmployeeIncomes.inc_code,MasterEmployeeIncomes.empl_code");
      sql.Append(" from MasterEmployeeIncomes");
      sql.Append(" where MasterEmployeeIncomes.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeIncomes.inc_code,MasterEmployeeIncomes.empl_code");
      return sql.ToString();
    }
    public string GetMaxLineforInc(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select max(line_no),MasterEmployeeIncomes.empl_code");
      sql.Append(" from MasterEmployeeIncomes");
      sql.Append(" where MasterEmployeeIncomes.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeIncomes.empl_code");
      return sql.ToString();
    }

    public string GetMinLineforDed(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select min(line_no) ,MasterEmployeeDeductions.ded_code,MasterEmployeeDeductions.empl_code ");
      sql.Append(" from MasterEmployeeDeductions");
      sql.Append(" where MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeDeductions.ded_code,MasterEmployeeDeductions.empl_code");
      return sql.ToString();

    }
    public string GetMaxLineforDed(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append(" select max(line_no) ,MasterEmployeeDeductions.empl_code ");
      sql.Append(" from MasterEmployeeDeductions");
      sql.Append(" where MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeDeductions.empl_code");
      return sql.ToString();
    }

    public string GetMinLineforObl(ref Object[] parameters)
    {

      StringBuilder sql = new StringBuilder();
      sql.Append(" select min(line_no),MasterEmployeeObligations.obl_code,MasterEmployeeObligations.empl_code");
      sql.Append(" from MasterEmployeeObligations");
      sql.Append(" where  MasterEmployeeObligations.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeObligations.obl_code,MasterEmployeeObligations.empl_code");
      return sql.ToString();

    }
    public string GetMaxLineforObl(ref Object[] parameters)
    {

      StringBuilder sql = new StringBuilder();
      sql.Append(" select max(line_no),MasterEmployeeObligations.empl_code");
      sql.Append(" from MasterEmployeeObligations");
      sql.Append(" where  MasterEmployeeObligations.empl_code in (" + parameters[0].ToString() + ")");
      sql.Append(" group by MasterEmployeeObligations.empl_code");
      return sql.ToString();

    }

    //*********  Added by Bharat Dhall [02/11/2010] *****************
    public string UPDATE_INCOME_QTD_YTD
    {
      get { return "uspempincytdupd"; }
    }
    public string UPDATE_DEDUCTION_QTD_YTD
    {
      get { return "uspempdedytdupd"; }
    }
    public string UPDATE_OBLIGATION_QTD_YTD
    {
      get { return "uspempoblytdupd"; }
    }
    //***************************************************************

    #endregion Stored-Procedures
  }


}
