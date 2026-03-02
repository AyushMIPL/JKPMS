using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  /// <summary>

  /// </summary>
  public class DVOMasterEmpTypes : DVOBase
  {
    private string _type_code;
    private string _description;
    private int _cash_acct = 0;
    private string _keyvalue;
    private string _department;
    private string _empl_status;
    private string _pay_period;
    private string _vac_code;
    private decimal? _vac_allowed;
    private string _sick_code;
    private decimal? _sick_allowed;
    private string _hold_pymnt;
    private string _statax_code;
    private string _loctax_code;
    private string _sick_accr_code;
    private string _vac_accr_code;

    private int _Rowid;


    #region Constructor
    public DVOMasterEmpTypes()
    {
      _type_code = string.Empty;
      _description = string.Empty;
      _cash_acct = 0;
      _keyvalue = string.Empty;
      _department = string.Empty;
      _empl_status = string.Empty;
      _pay_period = string.Empty;
      _vac_code = string.Empty;
      _vac_allowed = null;
      _sick_code = string.Empty;
      _sick_allowed = null;
      _hold_pymnt = string.Empty;
      _statax_code = string.Empty;
      _loctax_code = string.Empty;
      _sick_accr_code = string.Empty;
      _vac_accr_code = string.Empty;

      _Rowid = 0;
    }
    #endregion Constructor

    #region public properties

    public string type_code
    {
      get { return _type_code; }
      set { _type_code = value; }
    }
    public string description
    {
      get { return _description; }
      set { _description = value; }
    }

    public int cash_acct
    {
      get { return _cash_acct; }
      set { _cash_acct = value; }
    }
    public string keyvalue
    {
      get { return _keyvalue; }
      set { _keyvalue = value; }
    }
    public string department
    {
      get { return _department; }
      set { _department = value; }
    }
    public string empl_status
    {
      get { return _empl_status; }
      set { _empl_status = value; }
    }
    public string pay_period
    {
      get { return _pay_period; }
      set { _pay_period = value; }
    }
    public string vac_code
    {
      get { return _vac_code; }
      set { _vac_code = value; }
    }
    public decimal? vac_allowed
    {
      get { return _vac_allowed; }
      set { _vac_allowed = value; }
    }
    public string sick_code
    {
      get { return _sick_code; }
      set { _sick_code = value; }
    }
    public decimal? sick_allowed
    {
      get { return _sick_allowed; }
      set { _sick_allowed = value; }
    }
    public string hold_pymnt
    {
      get { return _hold_pymnt; }
      set { _hold_pymnt = value; }
    }
    public string statax_code
    {
      get { return _statax_code; }
      set { _statax_code = value; }
    }
    public string loctax_code
    {
      get { return _loctax_code; }
      set { _loctax_code = value; }
    }
    public string sick_accr_code
    {
      get { return _sick_accr_code; }
      set { _sick_accr_code = value; }
    }
    public string vac_accr_code
    {
      get { return _vac_accr_code; }
      set { _vac_accr_code = value; }
    }

    public int Rowid
    {
      get { return _Rowid; }
      set { _Rowid = value; }
    }



    #endregion public properties


    #region Stored-Procedures

    //public string AUTHENTICATION_SPNAME
    //{
    //    get { return "uspsecauthenticate"; }
    //}
    //Added By Sanjay
    public string GetEmpListByMinistry
    {
      get { return "uspEmpListByMnstry"; }
    }
    public string CheckExistingRecord
    {
      get { return "Usp_CheckForExistingRecord"; }
    }
    public string GetEmployeeType
    {
      get { return "USP_CheckEmpType"; }
    }
    public override string INSERT_SPNAME
    {
      get { return "USP_EmpTypeIns"; }
    }

    public override string UPDATE_SPNAME
    {
      get { return "USP_EmpTypeUpd"; }
    }

    public override string DELETE_SPNAME
    {
      get { return "USP_EmpTypeDel"; }
    }

    public override string FIND_SPNAME
    {
      get { return "usp_EmptypeGet"; }
    }

    public override string ALL_SPNAME
    {
      get { return ""; }
    }

    public override string TABLE_NAME
    {
      get { return "MasterEmpType"; }
    }

    public override int UNIQUE_ID
    {
      get { return _Rowid; }
    }

    public override string NOTES_TABLE_RECORD_ID
    {
      get { return string.Empty; }
      set { throw new Exception("The method or operation is not implemented."); }
    }
    //Added by Sunil Pahwa for Employee of one year for Bonus Purpose
    public string FIND_EMPTYPE_FOR_BONUS
    {
      get { return "uspempforbonusget"; }
    }


    // **** Added By Rahul Jain for getting Employee Type report Data on 21/12/2008 ****
    public string FIND_EMPTYPE
    {
      get { return "USP_EmpTypeGet"; }
    }
    public string FIND_EMPINCOME
    {
      get { return "USP_IncomeGet"; }
    }
    public string FIND_EMPDEDUCTION
    {
      get { return "USP_DeductionGet"; }
    }
    public string FIND_EMPOBLIGATION
    {
      get { return "USP_ObligationGet"; }
    }
    // *********************************************************************************

    public override string FIND_QUERY(ref Object[] parameters)
    {
      System.Text.StringBuilder sql = new StringBuilder();
      sql.Append(" select MasterEmpType.type_code ,MasterEmpType.description,MasterEmpType.cash_acct,");
      sql.Append(" MasterEmpType.department,MasterEmpType.empl_status,MasterEmpType.pay_period,MasterEmpType.vac_code,");
      sql.Append(" MasterEmpType.vac_allowed,MasterEmpType.sick_code,MasterEmpType.sick_allowed,");
      sql.Append(" MasterEmpType.hold_pymnt,MasterEmpType.statax_code,MasterEmpType.loctax_code,MasterEmpType.sick_accr_code,");
      sql.Append(" MasterEmpType.vac_accr_code,MasterEmpType.Emp_type_ID,PayrollGLAccounts.keyvalue");
      sql.Append(" from MasterEmpType LEFT Outer JOIN PayrollGLAccounts ON MasterEmpType.cash_acct=PayrollGLAccounts.acct_no");
      sql.Append(" Where 1=1 ");
      if (parameters[0] != null && parameters[0].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.type_code like '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[1] != null && parameters[1].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.description like '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[2] != null && parameters[2].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.empl_status = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[3] != null && parameters[3].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.pay_period = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[4] != null && parameters[4].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.vac_code = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
      if (Convert.ToDecimal(parameters[5]) > 0 && parameters[5] != null)
        sql.Append(" AND MasterEmpType.vac_allowed = " + parameters[5].ToString().Trim().Replace("'", "''") + "");
      if (parameters[6] != null && parameters[6].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.sick_code = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
      if (Convert.ToDecimal(parameters[7]) > 0 && parameters[7] != null)
        sql.Append(" AND MasterEmpType.sick_allowed = " + parameters[7].ToString().Trim().Replace("'", "''") + "");
      if (parameters[8] != null && parameters[8].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.hold_pymnt = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[9] != null && parameters[9].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.statax_code = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[10] != null && parameters[10].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.loctax_code = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[11] != null && parameters[11].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.sick_accr_code = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[12] != null && parameters[12].ToString() != string.Empty)
        sql.Append(" AND MasterEmpType.vac_accr_code = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
      //added by Sunil pahwa
      if (Convert.ToInt32(parameters[13]) > 0)
        sql.Append(" AND MasterEmpType.Emp_type_ID = " + parameters[13].ToString());


      return sql.ToString();
    }
    #endregion store-procedures




  }
}
