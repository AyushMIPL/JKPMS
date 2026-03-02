using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmployeeDeductions : DVOBase
    {
        private string _empl_code;
        private string _ded_code;
        private int _line_no;
        private decimal? _ded_rate;
        private decimal? _ded_limit;
        private string _ded_apply;
        private int _acct_no;
        private string _department;
        private decimal? _ded_qtd1;
        private decimal? _ded_qtd2;
        private decimal? _ded_qtd3;
        private decimal? _ded_qtd4;
        private decimal? _ded_ytd;
        private string _ded_date;//date
        private decimal? _lo_ded_amt;
        private decimal? _hi_ded_amt;
        private decimal? _pay_limit;
        private decimal? _balanceamt;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;
        private int _rowid;
        private string _acct_no_kv;
        int _acct_no_typeid;
        string _acct_no_type;

        private string _SSN;
        private string _typeCode;
        private string _lastName;
        private string _firstName;
        private string _empl_status;
        private string _jobCode;
        private string _jobTitle;
        private string _pay_period;
        private string _lastPay;//date
       
        private string _dfltaccounttype;
        private string _dfltkeyvalue;
        private string _description;
        private string _ded_type;
        private string _ded_taxred;
        private decimal? _dflt_rate;
        private decimal? _dflt_limit;
        private decimal? _dflt_pay_limit;
        private string _yearrollover;
        private decimal? _dflt_lo_ded_amt;
        private decimal? _dflt_hi_ded_amt;
        private int _dflt_acct;
        private string _dflt_dept;
        private string _dflt_apply;
        private string _tax_code;
        //*************************        ***********
        #region Constructor

        public DVOMasterEmployeeDeductions()
        {
            _empl_code = string.Empty;
            _ded_code = string.Empty;
            _line_no = 0;
            _ded_rate = null;
            _ded_limit = null;
            _ded_apply = string.Empty;
            _acct_no = 0;
            _department = string.Empty;
            _ded_qtd1 = null;
            _ded_qtd2 = null;
            _ded_qtd3 = null;
            _ded_qtd4 = null;
            _ded_ytd = null;
            _ded_date = "01/01/1900";//date
            _lo_ded_amt = null;
            _hi_ded_amt = null;
            _pay_limit = null;
            _balanceamt = null;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";//date
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";//date
            _UpdateBy = 0;
            _rowid = 0;
            _acct_no_kv = string.Empty;
            _acct_no_typeid = 0;
            _acct_no_type = string.Empty;

            _SSN = string.Empty;
            _typeCode = string.Empty;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _empl_status = string.Empty;
            _jobCode = string.Empty;
            _jobTitle = string.Empty;
            _pay_period = string.Empty;
            _lastPay = "01/01/1900";//date
            //**************29/12/2008******************
            //Added by   :Rohit Wadhwa
            //Aim :    To get Default Deductions values from MasterDedcodes for each deduction code in employee with join
            //******************************************
            _description = string.Empty;
            _ded_type = string.Empty;
            _ded_taxred = string.Empty;
            _dflt_rate = null;
            _dflt_limit = null;
            _dflt_acct = 0;
            _dflt_dept = string.Empty;
            _dflt_apply = string.Empty;
            _dflt_hi_ded_amt = null;
            _dflt_lo_ded_amt = null;
          
            _dfltaccounttype = string.Empty;
           _dfltkeyvalue = string.Empty;
           _dflt_pay_limit = null;
            _yearrollover = string.Empty;
            _tax_code = string.Empty;
            //*******************************************
        }

        #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public decimal? ded_rate
        {
            get { return _ded_rate; }
            set { _ded_rate = value; }
        }
        public decimal? ded_limit
        {
            get { return _ded_limit; }
            set { _ded_limit = value; }
        }
        public string ded_apply
        {
            get { return _ded_apply; }
            set { _ded_apply = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public decimal? ded_qtd1
        {
            get { return _ded_qtd1; }
            set { _ded_qtd1 = value; }
        }
        public decimal? ded_qtd2
        {
            get { return _ded_qtd2; }
            set { _ded_qtd2 = value; }
        }
        public decimal? ded_qtd3
        {
            get { return _ded_qtd3; }
            set { _ded_qtd3 = value; }
        }
        public decimal? ded_qtd4
        {
            get { return _ded_qtd4; }
            set { _ded_qtd4 = value; }
        }
        public decimal? ded_ytd
        {
            get { return _ded_ytd; }
            set { _ded_ytd = value; }
        }
        public string ded_date
        {
            get { return _ded_date; }
            set { _ded_date = value; }
        }
        public decimal? lo_ded_amt
        {
            get { return _lo_ded_amt; }
            set { _lo_ded_amt = value; }
        }
        public decimal? hi_ded_amt
        {
            get { return _hi_ded_amt; }
            set { _hi_ded_amt = value; }
        }
        public decimal? pay_limit
        {
            get { return _pay_limit; }
            set { _pay_limit = value; }
        }
        public decimal? balanceamt
        {
            get { return _balanceamt; }
            set { _balanceamt = value; }
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

        public string acct_no_kv
        {
            get { return _acct_no_kv; }
            set { _acct_no_kv = value; }
        }
        public int acct_no_typeid
        {
            get { return _acct_no_typeid; }
            set { _acct_no_typeid = value; }
        }
        public string acct_no_type
        {
            get { return _acct_no_type; }
            set { _acct_no_type = value; }
        }

        public string SSN
        {
            get { return _SSN; }
            set { _SSN = value; }
        }
        public string typeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }
        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string empl_status
        {
            get { return _empl_status; }
            set { _empl_status = value; }
        }
        public string jobCode
        {
            get { return _jobCode; }
            set { _jobCode = value; }
        }
        public string jobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }
        public string pay_period
        {
            get { return _pay_period; }
            set { _pay_period = value; }
        }
        public string lastPay
        {
            get { return _lastPay; }
            set { _lastPay = value; }
        }
        //**************29/12/2008******************
        //Added by   :Rohit Wadhwa
        //Aim :    To get Default Deductions values from MasterDedcodes for each deduction code in employee with join
        //******************************************
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string ded_type
        {
            get { return _ded_type; }
            set { _ded_type = value; }
        }
        public string ded_taxred
        {
            get { return _ded_taxred; }
            set { _ded_taxred = value; }
        }
        public string tax_code
        {
            get { return _tax_code; }
            set { _tax_code = value; }
        }
        public decimal? dflt_rate
        {
            get { return _dflt_rate; }
            set { _dflt_rate = value; }
        }
        public decimal? dflt_limit
        {
            get { return _dflt_limit; }
            set { _dflt_limit = value; }
        }
        public int dflt_acct
        {
            get { return _dflt_acct; }
            set { _dflt_acct = value; }
        }
        public string dflt_dept
        {
            get { return _dflt_dept; }
            set { _dflt_dept = value; }
        }
        public string dflt_apply
        {
            get { return _dflt_apply; }
            set { _dflt_apply = value; }
        }
        public decimal? dflt_hi_ded_amt
        {
            get { return _dflt_hi_ded_amt; }
            set { _dflt_hi_ded_amt = value; }
        }
        public decimal? dflt_lo_ded_amt
        {
            get { return _dflt_lo_ded_amt; }
            set { _dflt_lo_ded_amt = value; }
        }
        public string dfltaccounttype
        {
            get { return _dfltaccounttype; }
            set { _dfltaccounttype = value; }
        }
        public string dfltkeyvalue
        {
            get { return _dfltkeyvalue; }
            set { _dfltkeyvalue = value; }
        }
        public decimal? dflt_pay_limit
        {
            get { return _dflt_pay_limit; }
            set { _dflt_pay_limit = value; }
        }
        public string yearrollover
        {
            get { return _yearrollover; }
            set { _yearrollover = value; }
        }
   
        //********************************************
        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpDedCodIns"; }
        }
      
        public  string EmpDedanddefaults
        {
            get { return "USP_EmpDedRef"; }
        }
        public string FIND_EMPDEDANDDEFAULTS_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select MasterDedcodes.dfltaccounttype,MasterDedcodes.dfltkeyvalue,MasterEmployeeDeductions.ded_code,");
            sql.Append(" MasterEmployeeDeductions.line_no,MasterEmployeeDeductions.ded_rate,MasterEmployeeDeductions.ded_limit,");
            sql.Append(" MasterEmployeeDeductions.pay_limit,MasterEmployeeDeductions.balanceamt, MasterEmployeeDeductions.ded_apply,");
            sql.Append(" MasterEmployeeDeductions.lo_ded_amt,MasterEmployeeDeductions.hi_ded_amt,MasterEmployeeDeductions.acct_no,");
            sql.Append(" MasterEmployeeDeductions.department,MasterEmployeeDeductions.ded_ytd,MasterEmployeeDeductions.ded_date,");
            sql.Append(" MasterDedcodes.description,MasterDedcodes.ded_type,MasterDedcodes.ded_taxred,");
            sql.Append(" MasterDedcodes.dflt_rate,MasterDedcodes.dflt_limit,   MasterDedcodes.dflt_pay_limit,");
            sql.Append(" MasterDedcodes.yearrollover,MasterDedcodes.dflt_lo_ded_amt,MasterDedcodes.dflt_hi_ded_amt,");
            sql.Append(" MasterDedcodes.dflt_acct,MasterDedcodes.dflt_dept,MasterDedcodes.dflt_apply,MasterEmployeeDeductions.empl_code,");
            sql.Append(" MasterEmployee.soc_sec_num");
            sql.Append(" from MasterEmployeeDeductions, MasterDedcodes,MasterEmployee");
            sql.Append(" where MasterEmployeeDeductions.ded_code = MasterDedcodes.ded_code");
            sql.Append(" and MasterEmployeeDeductions.empl_code = MasterEmployee.empl_code");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" and MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString().Trim() + ")");
            //sql.Append(" and MasterEmployeeDeductions.empl_code in (SELECT MasterEmployee.empl_code FROM MasterEmployee WHERE 1=1");

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
            sql.Append(" order by MasterEmployeeDeductions.line_no");

            return sql.ToString();
        }
        public string FIND_EMPDED_TAXCALC_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT DISTINCT ded_code,week_allow,biweek_allow,smonth_allow,month_allow,quarter_allow,");
            sql.Append(" year_allow,misc_allow,tax_year,syear_allow,hrs_week_allow,");
            sql.Append(" hrs_biweek_allow,hrs_smonth_allow,hrs_month_allow,hrs_quarter_allow,");
            sql.Append(" hrs_syear_allow,hrs_year_allow,hrs_misc_allow,allow_or_limit");
            sql.Append(" FROM Deductions_Tax_Table_Header");
            sql.Append(" WHERE 1=1");
            // add check for payroll date
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0 && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
                    sql.Append(" AND Deductions_Tax_Table_Header.tax_year = YEAR('" + parameters[1].ToString().Trim() + "')");

            sql.Append(" and Deductions_Tax_Table_Header.ded_code IN (");

            sql.Append(" select MasterEmployeeDeductions.ded_code");
            sql.Append(" from MasterEmployeeDeductions, MasterDedcodes");
            sql.Append(" where MasterEmployeeDeductions.ded_code = MasterDedcodes.ded_code");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" and MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString().Trim() + ")");
            //sql.Append(" and MasterEmployeeDeductions.empl_code in (SELECT MasterEmployee.empl_code FROM MasterEmployee WHERE 1=1");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.empl_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.soc_sec_num) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[3].ToString().Trim() != string.Empty)
            //    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[4] != null)
            //    if (parameters[4].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.type_code) matches '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[5] != null)
            //    if (parameters[5].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[6] != null)
            //    if (parameters[6].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_title) = '" + parameters[6].ToString().Replace("'", "''") + "'");
            //if (parameters[7] != null)
            //    if (parameters[7].ToString().Trim().Length > 0)
            //        sql.Append(" AND MasterEmployee.pay_period = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and not exists (select empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C'))");
            //// add check for termination date
            //if (parameters[8] != null)
            //    if (parameters[8].ToString().Trim().Length > 0 && !parameters[8].ToString().Trim().Contains("1900") && !parameters[8].ToString().Trim().Contains("0001"))
            //    {
            //        sql.Append(" and (MasterEmployee.terminated is null ");
            //        sql.Append(" or MasterEmployee.terminated >='" + parameters[8].ToString().Trim() + "')");
            //    }
            //sql.Append(" )");
            sql.Append(" )");
            //sql.Append(" order by MasterEmployeeDeductions.line_no");

            return sql.ToString();
        }
        public string FIND_EMPDED_TAXCALC_DETAIL_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT tax_year,ded_code,pay_period,marital_stat,over_amt,base_amt,tax_rate,order_no");
            sql.Append(" FROM Deductions_Tax_Table_Details");
            sql.Append(" WHERE 1=1");
            // add check for payroll date
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0 && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
                    sql.Append(" AND Deductions_Tax_Table_Details.tax_year = YEAR('" + parameters[1].ToString().Trim() + "')");

            sql.Append(" AND Deductions_Tax_Table_Details.ded_code IN (");

            sql.Append(" select MasterEmployeeDeductions.ded_code");
            sql.Append(" from MasterEmployeeDeductions, MasterDedcodes");
            sql.Append(" where MasterEmployeeDeductions.ded_code = MasterDedcodes.ded_code");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" and MasterEmployeeDeductions.empl_code in (" + parameters[0].ToString().Trim() + ")");
            //sql.Append(" and MasterEmployeeDeductions.empl_code in (SELECT MasterEmployee.empl_code FROM MasterEmployee WHERE 1=1");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.empl_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.soc_sec_num) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.first_name) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[3].ToString().Trim() != string.Empty)
            //    sql.Append(" AND Rtrim(MasterEmployee.last_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[4] != null)
            //    if (parameters[4].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.type_code) matches '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[5] != null)
            //    if (parameters[5].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[6] != null)
            //    if (parameters[6].ToString().Trim().Length > 0)
            //        sql.Append(" AND Rtrim(MasterEmployee.job_title) = '" + parameters[6].ToString().Replace("'", "''") + "'");
            //if (parameters[7] != null)
            //    if (parameters[7].ToString().Trim().Length > 0)
            //        sql.Append(" AND MasterEmployee.pay_period = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and not exists (select empl_code from Process_PayEmployee,MasterEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C'))");
            //// add check for termination date
            //if (parameters[8] != null)
            //    if (parameters[8].ToString().Trim().Length > 0 && !parameters[8].ToString().Trim().Contains("1900") && !parameters[8].ToString().Trim().Contains("0001"))
            //    {
            //        sql.Append(" and (MasterEmployee.terminated is null ");
            //        sql.Append(" or MasterEmployee.terminated >='" + parameters[8].ToString().Trim() + "')");
            //    }
            //sql.Append(" )");
            sql.Append(" )");
            //sql.Append(" order by MasterEmployeeDeductions.line_no");

            return sql.ToString();
        }
       
         public  string DeductioncodeTaxget
        {
            get { return "USP_DedTaxCodeGet"; }
        }
        //********************************************
        public override string UPDATE_SPNAME
        {
            get { return "Usp_EmpDedUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpDedDel"; }
        }
              
        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_EmpDedCodGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterEmployeeDeductions"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string GET_ROWID
        {
            get { return "USP_PayDD_RowId"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT empl_code p_empl_code,ded_code p_ded_code,line_no p_line_no,ded_rate p_ded_rate,ded_limit p_ded_limit,");
            sql.Append(" ded_apply p_ded_apply,MasterEmployeeDeductions.acct_no p_acct_no,department p_department,ded_qtd1 p_ded_qtd1,");
            sql.Append(" ded_qtd2 p_ded_qtd2,ded_qtd3 p_ded_qtd3,ded_qtd4 p_ded_qtd4,ded_ytd p_ded_ytd,ded_date p_ded_date,");
            sql.Append(" lo_ded_amt p_lo_ded_amt,hi_ded_amt p_hi_ded_amt,pay_limit p_pay_limit,balanceamt p_balanceamt,");
            sql.Append(" PayrollGLAccounts.keyvalue acct_no_kv,Flex_struct_Header.id acct_no_typeid,PayrollGLAccounts.acct_type ");
            sql.Append(" FROM MasterEmployeeDeductions LEFT OUTER JOIN (PayrollGLAccounts INNER JOIN Flex_struct_Header ON PayrollGLAccounts.acct_type=Flex_struct_Header.accounttype) ON  MasterEmployeeDeductions.acct_no=PayrollGLAccounts.acct_no WHERE 1= 1  ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND line_no = " + parameters[0].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(ded_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND ded_rate = " + parameters[3].ToString().Trim());
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND ded_limit = " + parameters[4].ToString().Trim());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(ded_apply) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND MasterEmployeeDeductions.acct_no = " + parameters[6].ToString().Trim());
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(department) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND ded_qtd1 = " + parameters[8].ToString().Trim());
            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND ded_qtd2 = " + parameters[9].ToString().Trim());
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND ded_qtd3 = " + parameters[10].ToString().Trim());
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND ded_qtd4 = " + parameters[11].ToString().Trim());
            if (Convert.ToInt32(parameters[12]) > 0)
                sql.Append(" AND ded_ytd = " + parameters[12].ToString().Trim());
            if (parameters[13] != null)
                if (parameters[13].ToString().Trim() != string.Empty && !parameters[13].ToString().Trim().Contains("1900"))
                    sql.Append(" AND ded_date = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND lo_ded_amt = " + parameters[14].ToString().Trim());
            if (Convert.ToInt32(parameters[15]) > 0)
                sql.Append(" AND hi_ded_amt = " + parameters[15].ToString().Trim());
            if (Convert.ToInt32(parameters[16]) > 0)
                sql.Append(" AND pay_limit = " + parameters[16].ToString().Trim());
            if (Convert.ToInt32(parameters[17]) > 0)
                sql.Append(" AND balanceamt = " + parameters[17].ToString().Trim());

            if ((parameters[18] == null || parameters[18].ToString() == string.Empty) &&//_SSN
                (parameters[19] == null || parameters[19].ToString() == string.Empty) &&//_typeCode
                (parameters[20] == null || parameters[20].ToString() == string.Empty) &&//_lastName
                (parameters[21] == null || parameters[21].ToString() == string.Empty) &&//_firstName
                (parameters[22] == null || parameters[22].ToString() == string.Empty) &&//_empl_status
                (parameters[23] == null || parameters[23].ToString() == string.Empty) &&//_jobCode
                (parameters[24] == null || parameters[24].ToString() == string.Empty) &&//_jobTitle
                (parameters[25] == null || parameters[25].ToString() == string.Empty) &&//_pay_period
                (parameters[26] == null || parameters[26].ToString() == string.Empty || parameters[26].ToString().Trim() == "01/01/1900"))//_lastPay
            {
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            }
            else
            {
                sql.Append(" AND RTRIM(empl_code) IN (");
                sql.Append(" SELECT empl_code FROM MasterEmployee WHERE 1=1 ");
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[18] != null)
                    if (parameters[18].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[18].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[19] != null)
                    if (parameters[19].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[20] != null)
                    if (parameters[20].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(last_name) LIKE  '" + parameters[20].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[21] != null)
                    if (parameters[21].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[21].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[22] != null)
                    if (parameters[22].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[23] != null)
                    if (parameters[23].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Code) = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[24] != null)
                    if (parameters[24].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Title) = '" + parameters[24].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[25] != null)
                    if (parameters[25].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[25].ToString().Trim() + "'");
                if (parameters[26] != null)
                    if (parameters[26].ToString() != string.Empty && !parameters[26].ToString().Trim().Contains("1900"))
                        sql.Append(" AND last_pay = '" + parameters[26].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" )");
            }

            return sql.ToString();
        }
        public string GET_EMPDED_TAXCALC(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT DISTINCT ded_code,week_allow,biweek_allow,smonth_allow,month_allow,quarter_allow,");
            sql.Append(" year_allow,misc_allow,tax_year,syear_allow,hrs_week_allow,");
            sql.Append(" hrs_biweek_allow,hrs_smonth_allow,hrs_month_allow,hrs_quarter_allow,");
            sql.Append(" hrs_syear_allow,hrs_year_allow,hrs_misc_allow,allow_or_limit");
            sql.Append(" FROM Deductions_Tax_Table_Header WHERE 1=1");
            if (parameters[1] != null && parameters[1].ToString().Trim().Length > 0 && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
                sql.Append(" AND Deductions_Tax_Table_Header.tax_year = YEAR('" + parameters[1].ToString().Trim() + "')");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" and Deductions_Tax_Table_Header.ded_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
       

        #endregion store-procedures
    }
}
