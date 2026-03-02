using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public class DVOPayrollautopay : DVOBase
  {
    private int _RowID;
    private string _EmplCode;
    private string _SocSecNum;
    private string _TypeCode;
    private string _FirstName;
    private string _LastName;
    private int _CashAcct;
    private string _Department;
    private string _Terminated;//date
    private string _PayPeriod;
    private int _Allowances;
    private int _StateAllow;
    private string _MaritalStat;
    private string _VacCode;
    private decimal _VacAllowed;
    private decimal _VacUsed;
    private string _SickCode;
    private decimal _SickAllowed;
    private decimal _SickUsed;
    private DateTime _LastPay;
    private string _HoldPayment;
    private string _StaTaxCode;
    private string _LocTaxCode;
    private string _SickAccrCodr;
    private int _SickAccrCtr;
    //Added by Rahul on 26/12/2008 
    private int _flexdeptacctno;
    private string _flexdeptkeyvalue;

    private string _DirDept;
    private string _FlexDeptAcctType;
    private string _LastIncDate;//date
    private string _InsertMachineInfo;
    private string _InsertDate;//date
    private int _InsertBy;
    private string _UpdateMachineInfo;
    private string _UpdateDate;//date
    private int _UpdateBy;

    //Properties added By Rahul on 26/12/2008 for Print Automatic Payroll Reports : search criteria
    private string _Process_TimeCard;
    private DateTime _Payroll_Date;
    private DateTime _EOP_Date;
    private string _BonusCeck;
    private string _Employee_Type;
    private string _Jod_Code;
    private string _Title;
    private string _FullTime;

    string _address1;
    string _address2;
    string _gender;
    private DateTime _start_date;
    private string _district;



    #region Constructor

    public DVOPayrollautopay()
    {
      _RowID = 0;
      _EmplCode = string.Empty;
      _SocSecNum = string.Empty;
      _TypeCode = string.Empty;
      _FirstName = string.Empty;
      _LastName = string.Empty;
      _CashAcct = 0;
      _Department = string.Empty;


      _Terminated = "01/01/1900";

      _PayPeriod = string.Empty;
      _Allowances = 0;
      _StateAllow = 0;
      _MaritalStat = string.Empty;
      _VacCode = string.Empty;
      _VacAllowed = Convert.ToDecimal(null);
      _VacUsed = Convert.ToDecimal(null);
      _SickCode = string.Empty;
      _SickAllowed = 0;
      _SickUsed = 0;
      _LastPay = Convert.ToDateTime(null);
      _HoldPayment = string.Empty;
      _StaTaxCode = string.Empty;
      _LocTaxCode = string.Empty;

      _DirDept = string.Empty;

      _FlexDeptAcctType = string.Empty;
      _LastIncDate = "01/01/1900";
      _flexdeptacctno = 0;
      _flexdeptkeyvalue = string.Empty;

      _InsertMachineInfo = string.Empty;
      _InsertDate = "01/01/1900";
      _InsertBy = 0;
      _UpdateMachineInfo = string.Empty;
      _UpdateDate = "01/01/1900";
      _UpdateBy = 0;

      //Properties added By Rahul on 26/12/2008 for Print Automatic Payroll Reports : search criteria
      _Process_TimeCard = string.Empty;
      _Payroll_Date = Convert.ToDateTime(null);
      _EOP_Date = Convert.ToDateTime(null);
      _BonusCeck = string.Empty;
      _Employee_Type = string.Empty;
      _Jod_Code = string.Empty;
      _Title = string.Empty;
      _FullTime = string.Empty;

      _address1 = string.Empty;
      _address2 = string.Empty;
      _gender = string.Empty;
      _start_date = Convert.ToDateTime(null);
    }

    #endregion Constructor

    #region Properties

    public int RowID
    {
      get { return _RowID; }
      set { _RowID = value; }
    }
    public string EmplCode
    {
      get { return _EmplCode; }
      set { _EmplCode = value; }
    }
    public string SocSecNum
    {
      get { return _SocSecNum; }
      set { _SocSecNum = value; }
    }
    public string FirstName
    {
      get { return _FirstName; }
      set { _FirstName = value; }
    }
    public string LastName
    {
      get { return _LastName; }
      set { _LastName = value; }
    }
    public int CashAcct
    {
      get { return _CashAcct; }
      set { _CashAcct = value; }
    }
    public string Department
    {
      get { return _Department; }
      set { _Department = value; }
    }
    public string Terminated
    {
      get { return _Terminated; }
      set { _Terminated = value; }
    }
    public string PayPeriod
    {
      get { return _PayPeriod; }
      set { _PayPeriod = value; }
    }
    public int Allowances
    {
      get { return _Allowances; }
      set { _Allowances = value; }
    }
    public int StateAllow
    {
      get { return _StateAllow; }
      set { _StateAllow = value; }
    }
    public string MaritalStat
    {
      get { return _MaritalStat; }
      set { _MaritalStat = value; }
    }
    public string VacCode
    {
      get { return _VacCode; }
      set { _VacCode = value; }
    }
    public decimal VacAllowed
    {
      get { return _VacAllowed; }
      set { _VacAllowed = value; }
    }
    public decimal VacUsed
    {
      get { return _VacUsed; }
      set { _VacUsed = value; }
    }
    public string SickCode
    {
      get { return _SickCode; }
      set { _SickCode = value; }
    }
    public decimal SickAllowed
    {
      get { return _SickAllowed; }
      set { _SickAllowed = value; }
    }
    public decimal SickUsed
    {
      get { return _SickUsed; }
      set { _SickUsed = value; }
    }
    public DateTime LastPay
    {
      get { return _LastPay; }
      set { _LastPay = value; }
    }
    public string HoldPayment
    {
      get { return _HoldPayment; }
      set { _HoldPayment = value; }
    }
    public string StaTaxCode
    {
      get { return _StaTaxCode; }
      set { _StaTaxCode = value; }
    }
    public string LocTaxCode
    {
      get { return _LocTaxCode; }
      set { _LocTaxCode = value; }
    }

    public string DirDept
    {
      get { return _DirDept; }
      set { _DirDept = value; }
    }
    public int Flexdeptacctno
    {
      get { return _flexdeptacctno; }
      set { _flexdeptacctno = value; }
    }
    public string Flexdeptkeyvalue
    {
      get { return _flexdeptkeyvalue; }
      set { _flexdeptkeyvalue = value; }
    }
    public string FlexDeptAcctType
    {
      get { return _FlexDeptAcctType; }
      set { _FlexDeptAcctType = value; }
    }
    public string LastIncDate
    {
      get { return _LastIncDate; }
      set { _LastIncDate = value; }
    }


    public string InsertMachineInfo
    {
      get { return _InsertMachineInfo; }
      set { _InsertMachineInfo = value; }
    }
    public string InsertDate
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
    public string UpdateDate
    {
      get { return _UpdateDate; }
      set { _UpdateDate = value; }
    }
    public int UpdateBy
    {
      get { return _UpdateBy; }
      set { _UpdateBy = value; }
    }
    //Properties added By Rahul on 26/12/2008 for Print Automatic Payroll Reports : search criteria
    public string Process_TimeCard
    {
      get { return _Process_TimeCard; }
      set { _Process_TimeCard = value; }
    }
    public DateTime Payroll_Date
    {
      get { return _Payroll_Date; }
      set { _Payroll_Date = value; }
    }
    public DateTime EOP_Date
    {
      get { return _EOP_Date; }
      set { _EOP_Date = value; }
    }
    public string BonusCeck
    {
      get { return _BonusCeck; }
      set { _BonusCeck = value; }
    }
    public string Employee_Type
    {
      get { return _Employee_Type; }
      set { _Employee_Type = value; }
    }
    public string Job_Code
    {
      get { return _Jod_Code; }
      set { _Jod_Code = value; }
    }
    public string Title
    {
      get { return _Title; }
      set { _Title = value; }
    }
    public string FullTime
    {
      get { return _FullTime; }
      set { _FullTime = value; }
    }
    public string address1
    {
      get { return _address1; }
      set { _address1 = value; }
    }
    public string address2
    {
      get { return _address2; }
      set { _address2 = value; }
    }
    public string gender
    {
      get { return _gender; }
      set { _gender = value; }
    }
    public DateTime Start_date
    {
      get { return _start_date; }
      set { _start_date = value; }
    }

    public string District
    {
      get { return _district; }
      set { _district = value; }
    }
    #endregion Properties

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
      get { return "styemplr"; }
    }

    public override int UNIQUE_ID
    {
      get { return _RowID; }
    }

    public override string NOTES_TABLE_RECORD_ID
    {
      get { return string.Empty; }
      set { throw new Exception("The method or operation is not implemented."); }
    }
    public string DUP_SSN
    {
      get { return "uspemplcntget"; }
    }
    public string DUP_SSN_PAY
    {
      get { return "uspdupssnpayget"; }
    }
    public string EmployeeIncomerefer
    {
      get { return "uspempincomeref"; }
    }
    public string FIND_EMPLOYEEINCOMEREFER_QUERY(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select distinct MasterIncCodes.dfltaccounttype,'0',MasterEmployeeIncomes.inc_code,MasterEmployeeIncomes.line_no,");
      sql.Append(" MasterEmployeeIncomes.line_no,MasterEmployeeIncomes.inc_rate,MasterEmployeeIncomes.inc_number,MasterEmployeeIncomes.inc_hours,");
      sql.Append(" MasterEmployeeIncomes.lo_inc_amt,MasterEmployeeIncomes.hi_inc_amt,MasterEmployeeIncomes.acct_no,MasterEmployeeIncomes.department,");
      sql.Append(" MasterIncCodes.description,MasterIncCodes.dflt_num,MasterIncCodes.dflt_rate,");
      sql.Append(" MasterIncCodes.dflt_hours,MasterIncCodes.dflt_lo_inc_amt,MasterIncCodes.dflt_hi_inc_amt,MasterIncCodes.dflt_acct,");
      sql.Append(" MasterIncCodes.dflt_dept,MasterIncCodes.inc_type,MasterEmployeeIncomes.empl_code");
      sql.Append(" from MasterEmployeeIncomes, MasterIncCodes");
      sql.Append(" where MasterEmployeeIncomes.inc_code = MasterIncCodes.inc_code");
      if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
        sql.Append(" and MasterEmployeeIncomes.empl_code in (" + parameters[0].ToString().Trim() + ")");
      //sql.Append(" and MasterEmployeeIncomes.empl_code in (SELECT MasterEmployee.empl_code FROM MasterEmployee WHERE 1=1");

      //if (parameters[0] != null)
      //    if (parameters[0].ToString().Trim() != string.Empty)
      //        sql.Append(" AND Rtrim(MasterEmployee.empl_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
      //if (parameters[1] != null)
      //    if (parameters[1].ToString().Trim() != string.Empty)
      //        sql.Append(" AND Rtrim(MasterEmployee.soc_sec_num) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      //if (parameters[2] != null)
      //    if (parameters[2].ToString().Trim() != string.Empty)
      //        sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
      //if (parameters[3].ToString().Trim() != string.Empty)
      //    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
      //if (parameters[4] != null)
      //    if (parameters[4].ToString().Trim() != string.Empty)
      //        sql.Append(" AND Rtrim(MasterEmployee.type_code) matches '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
      //if (parameters[5] != null)
      //    if (parameters[5].ToString().Trim() != string.Empty)
      //        sql.Append(" AND Rtrim(MasterEmployee.job_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
      //if (parameters[6] != null)
      //    if (parameters[6].ToString().Trim() != string.Empty)
      //        sql.Append(" AND Rtrim(MasterEmployee.job_title) = '" + parameters[6].ToString().Replace("'", "''") + "'");
      //if (parameters[7] != null)
      //    if (parameters[7].ToString().Trim() != string.Empty)
      //        sql.Append(" AND MasterEmployee.pay_period = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
      //sql.Append(" and not exists (select empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C'))");
      //// add check for termination date
      //if (parameters[8] != null)
      //    if (parameters[8].ToString().Trim() != string.Empty && !parameters[8].ToString().Trim().Contains("1900") && !parameters[8].ToString().Trim().Contains("0001"))
      //    {
      //        sql.Append(" and (MasterEmployee.terminated is null ");
      //        sql.Append(" or MasterEmployee.terminated >='" + parameters[8].ToString().Trim() + "')");
      //    }
      //sql.Append(" )");
      sql.Append(" order by MasterEmployeeIncomes.line_no");

      return sql.ToString();
    }
    public string FIND_DUPSSN_QUERY()
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select soc_sec_num,count(empl_code) from MasterEmployee group by soc_sec_num having count(empl_code)>1");
      return sql.ToString();
    }
    public string FIND_DUPSSN_PAY_QUERY()
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select soc_sec_num,MasterEmployee.empl_code,count(MasterEmployee.empl_code) from MasterEmployee,Process_PayEmployee");
      sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code");
      sql.Append(" group by soc_sec_num,MasterEmployee.empl_code");
      sql.Append(" having count(MasterEmployee.empl_code)>1");
      return sql.ToString();
    }

    public string EMPLOYEEMAXLINENOGET
    {
      get { return "uspgetmaxnoempl"; }
    }
    public string DeductionTaxCodeGet
    {
      get { return "uspdedtaxcodeget"; }
    }
    public override string FIND_QUERY(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT MasterEmployee.empl_code,soc_sec_num,first_name,last_name,");
      sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
      sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
      sql.Append(" statax_code,loctax_code,");
      sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
      sql.Append(" MasterEmployee.EmployeeID");
      sql.Append(" FROM MasterEmployee");

      // if processing timecards only, then join MasterEmployee to EmployeeTCard_Header to ensure
      // that payroll entries are not created for employees with no timecards

      if (Convert.ToBoolean(parameters[9]) == true)
      {
        sql.Append(", EmployeeTCard_Header");
      }

      sql.Append(" where  (MasterEmployee.hold_pymnt is null or MasterEmployee.hold_pymnt != 'Y')");
      if (Convert.ToInt32(parameters[0]) > 0)
        sql.Append(" AND MasterEmployee.EmployeeID = " + parameters[0].ToString());
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(MasterEmployee.empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[2] != null)
        if (parameters[2].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[3] != null)
        if (parameters[3].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(first_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

      if (parameters[4].ToString().Trim() != string.Empty)
        sql.Append(" AND Rtrim(last_name) LIKE  '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[5] != null)
        if (parameters[5].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '%" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[6] != null)
        if (parameters[6].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(job_code) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");

      if (parameters[7] != null)
        if (parameters[7].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(job_title) = '" + parameters[7].ToString().Replace("'", "''") + "'");
      if (parameters[8] != null)
        if (parameters[8].ToString().Trim() != string.Empty)
          sql.Append(" AND pay_period = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
      sql.Append(" and not exists (select Process_PayEmployee.empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C'))");
      // add check for termination date
      if (parameters[10] != null)
        if (parameters[10].ToString().Trim() != string.Empty && !parameters[10].ToString().Trim().Contains("1900") && !parameters[10].ToString().Trim().Contains("0001"))
        {
          sql.Append(" and (MasterEmployee.terminated is null ");
          sql.Append(" or MasterEmployee.terminated >='" + parameters[10].ToString().Trim() + "')");
        }

      if (parameters[11] != null)
        if (parameters[11].ToString().Trim() != string.Empty)
          sql.Append(" AND MasterEmployee.SelectDistrict IN (" + parameters[11].ToString() + ")");

      if (Convert.ToBoolean(parameters[9]) == true)
      {
        sql.Append(" and MasterEmployee.empl_code=EmployeeTCard_Header.empl_code and EmployeeTCard_Header.used_flag='N'");
      }

      return sql.ToString();
    }


    public string FIND_PAYROLL_ENTRY_TO_UPDATE(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT MasterEmployee.empl_code,soc_sec_num,first_name,last_name,");
      sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
      sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
      sql.Append(" statax_code,loctax_code,");
      sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
      sql.Append(" MasterEmployee.RowID");
      sql.Append(" FROM MasterEmployee");

      // if processing timecards only, then join MasterEmployee to EmployeeTCard_Header to ensure
      // that payroll entries are not created for employees with no timecards

      if (Convert.ToBoolean(parameters[9]) == true)
      {
        //sql.Append(", EmployeeTCard_Header");
      }

      sql.Append(" where  (MasterEmployee.hold_pymnt is null or MasterEmployee.hold_pymnt != 'Y')");
      if (Convert.ToInt32(parameters[0]) > 0)
        sql.Append(" AND MasterEmployee.RowID = " + parameters[0].ToString());
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(MasterEmployee.empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[2] != null)
        if (parameters[2].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[3] != null)
        if (parameters[3].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(first_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

      if (parameters[4].ToString().Trim() != string.Empty)
        sql.Append(" AND Rtrim(last_name) LIKE  '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[5] != null)
        if (parameters[5].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[6] != null)
        if (parameters[6].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(job_code) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");

      if (parameters[7] != null)
        if (parameters[7].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(job_title) = '" + parameters[7].ToString().Replace("'", "''") + "'");
      if (parameters[8] != null)
        if (parameters[8].ToString().Trim() != string.Empty)
          sql.Append(" AND pay_period = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
      //sql.Append(" and not exists (select empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C'))");
      // add check for termination date
      if (parameters[10] != null)
        if (parameters[10].ToString().Trim() != string.Empty && !parameters[10].ToString().Trim().Contains("1900") && !parameters[10].ToString().Trim().Contains("0001"))
        {
          sql.Append(" and (MasterEmployee.terminated is null ");
          sql.Append(" or MasterEmployee.terminated >='" + parameters[10].ToString().Trim() + "')");
        }

      if (Convert.ToBoolean(parameters[9]) == true)
      {
        //sql.Append(" and MasterEmployee.empl_code=EmployeeTCard_Header.empl_code and EmployeeTCard_Header.used_flag='N'");
      }

      return sql.ToString();
    }



    //Added by Sarvjeet on 25/01/2010, Used in Check Selected Payroll..
    public string FIND_PERSION_PROCESSED(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();

      sql.Append(" SELECT doc_no,Process_PayEmployee.empl_code,doc_date,pay_date,eop_date ,print_check ,cash_acct_no,Process_PayEmployee.department,cash_amount, ");
      sql.Append(" check_no,inc_gross,ded_fica,inc_taxable,ded_medicare,ded_fedtax,ded_statax,ded_loctax,ded_other,");
      sql.Append(" obl_futa,obl_fica,obl_medicare,obl_other ,inc_net,inc_expense,total_hours,ok_to_post,accrue_sick,");
      sql.Append(" accrue_vac,bonus,deposit,obl_total ");
      sql.Append(" FROM Process_PayEmployee,MasterEmployee WHERE Process_PayEmployee.empl_code=MasterEmployee.empl_code");
      sql.Append(" AND ok_to_post<>'C'");

      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "*'");
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
          sql.Append(" AND pay_date = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      return sql.ToString();
    }
    public string FIND_WAGE_PERSON_PROCESSED(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();

      sql.Append(" SELECT doc_no,Process_PayEmployee.empl_code,doc_date,pay_date,eop_date ,print_check ,cash_acct_no,Process_PayEmployee.department,cash_amount, ");
      sql.Append(" check_no,inc_gross,ded_fica,inc_taxable,ded_medicare,ded_fedtax,ded_statax,ded_loctax,ded_other,");
      sql.Append(" obl_futa,obl_fica,obl_medicare,obl_other ,inc_net,inc_expense,total_hours,ok_to_post,accrue_sick,");
      sql.Append(" accrue_vac,bonus,deposit,obl_total ");
      sql.Append(" FROM Process_PayEmployee,MasterEmployee WHERE Process_PayEmployee.empl_code=MasterEmployee.empl_code");
      sql.Append(" AND ok_to_post<>'C'");

      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "*'");
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
          sql.Append(" AND pay_date >= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      if (parameters[2] != null)
        if (parameters[2].ToString().Trim() != string.Empty && !parameters[2].ToString().Trim().Contains("1900") && !parameters[2].ToString().Trim().Contains("0001"))
          sql.Append(" AND pay_date <= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      //sql.Append(" AND MasterEmployee.empl_code in ('48792')");
      return sql.ToString();
    }

    //Added by Sarvjeet on 25/01/2010, Used in Check Selected Payroll..
    public string FIND_PERSION_TO_BE_PROCESSED(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT MasterEmployee.empl_code,soc_sec_num,first_name,last_name,");
      sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
      sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
      sql.Append(" statax_code,loctax_code,");
      sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
      sql.Append(" MasterEmployee.RowID,address1,address2,gender");
      sql.Append(" FROM MasterEmployee");
      sql.Append(" where  (MasterEmployee.hold_pymnt is null or MasterEmployee.hold_pymnt != 'Y')");

      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

      sql.Append(" and not exists (select Process_PayEmployee.empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code ");
      sql.Append(" and Process_PayEmployee.pay_date < '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      sql.Append(" and Process_PayEmployee.ok_to_post not in ('P','C'))");

      sql.Append(" and not exists (select Process_PayEmployee.empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code ");
      sql.Append(" and Process_PayEmployee.pay_date between '" + parameters[2].ToString() + "' and " + parameters[1].ToString());
      sql.Append(" and Process_PayEmployee.ok_to_post <> 'C')");

      //add check for termination date
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
        {
          sql.Append(" and (MasterEmployee.terminated is null ");
          sql.Append(" or MasterEmployee.terminated >='" + parameters[1].ToString().Trim() + "')");

        }
      //
      //sql.Append(" and MasterEmployee.empl_code='27709' ");
      return sql.ToString();
    }
    public string FIND_WAGE_PERSION_TO_BE_PROCESSED(ref Object[] parameters)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT MasterEmployee.empl_code,soc_sec_num,first_name,last_name,");
      sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
      sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
      sql.Append(" statax_code,loctax_code,");
      sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
      sql.Append(" MasterEmployee.RowID,address1,address2,gender");
      sql.Append(" FROM MasterEmployee");
      sql.Append(" where  (MasterEmployee.hold_pymnt is null or MasterEmployee.hold_pymnt != 'Y')");

      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

      sql.Append(" and not exists (select Process_PayEmployee.empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code ");
      sql.Append(" and Process_PayEmployee.pay_date < '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
      sql.Append(" and Process_PayEmployee.ok_to_post not in ('P','C'))");

      //add check for termination date
      if (parameters[1] != null)
        if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
        {
          sql.Append(" and (MasterEmployee.terminated is null ");
          sql.Append(" or MasterEmployee.terminated >='" + parameters[1].ToString().Trim() + "')");

        }
      //
      //sql.Append(" and MasterEmployee.empl_code='27709'");
      return sql.ToString();
    }


    public string FIND_PERSION_TO_PRINT_LETTRER(ref Object[] parameters)
    {
      StringBuilder sqlSubQuery = new StringBuilder();

      sqlSubQuery.Append(" SELECT Process_PayEmployee.empl_code");
      sqlSubQuery.Append(" FROM Process_PayEmployee,MasterEmployee WHERE Process_PayEmployee.empl_code=MasterEmployee.empl_code");
      sqlSubQuery.Append(" AND ok_to_post<>'C'");
      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sqlSubQuery.Append(" AND Rtrim(type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

      if ((parameters[2] != null && parameters[2].ToString().Trim().Length > 0 && !parameters[2].ToString().Trim().Contains("1900") && !parameters[2].ToString().Trim().Contains("0001"))
          && parameters[1].ToString() != parameters[2].ToString())
      {
        if (parameters[1] != null)
          if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
            sqlSubQuery.Append(" AND pay_date >= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
        if (parameters[2] != null)
          if (parameters[2].ToString().Trim() != string.Empty && !parameters[2].ToString().Trim().Contains("1900") && !parameters[2].ToString().Trim().Contains("0001"))
            sqlSubQuery.Append(" AND pay_date <= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
      }

      else
      {
        if (parameters[1] != null)
          if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
            sqlSubQuery.Append(" AND pay_date = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

      }
      //sqlSubQuery.Append(" AND MasterEmployee.empl_code in('48792')");

      StringBuilder sql = new StringBuilder();
      sql.Append(" SELECT MasterEmployee.empl_code,soc_sec_num,first_name,last_name,");
      sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
      sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
      sql.Append(" statax_code,loctax_code,");
      sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
      sql.Append(" MasterEmployee.RowID,address1,address2,gender");
      sql.Append(" FROM MasterEmployee");
      sql.Append(" where  (MasterEmployee.hold_pymnt is null or MasterEmployee.hold_pymnt != 'Y')");

      if (parameters[0] != null)
        if (parameters[0].ToString().Trim() != string.Empty)
          sql.Append(" AND Rtrim(type_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

      sql.Append("AND MasterEmployee.empl_code in (" + sqlSubQuery + ")");


      return sql.ToString();
    }

    #endregion Stored-Procedures
  }
}
