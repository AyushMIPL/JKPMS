using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPayrollstypaydd :DVOBase
    {
        private int _RowID;
        private int _Doc_no;
        private int _line_no;
        private string _ded_code;  //date

        private decimal? _ded_rate;
        private decimal? _amount;  
        private int _acct_no;
        private string _department;
        private int _mod_flag;
        private string _add_code;
       private decimal? _lo_ded_amt;
       private decimal? _hi_ded_amt;

        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;
       private string _ded_taxred;
       private bool _dedflag;
       private string _ded_type;
       private decimal? _pay_limit;
       private string _yearrollover;
       private decimal? _ded_limit;
       private decimal? _ded_ytd;
       private string _tax_code;
       private Boolean _lo_ded_amt_null;
       private Boolean _hi_ded_amt_null;

       string _acctKeyvalue;
       string _acctAccoutType;
       int _accountTypeId;

       //Added By Rajeev 
       //Aim: To use in ded_post
       //Date :10/01/09
       private string _description_MasterIncCodes;
       string _pay_date;

      // private decimal balanceamt
        #region Constructor

        public DVOPayrollstypaydd()
        {
            _RowID = 0;
            _Doc_no = 0;
            _line_no = 0;
           _ded_code = string.Empty;
            _ded_rate = null;
            _amount = null;
            _acct_no = 0;
            _department = "000";
            _mod_flag=0;
            _add_code = string.Empty;
            _lo_ded_amt = null;
            _hi_ded_amt = null;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;
            _ded_taxred = string.Empty;
            _dedflag = false;
            _ded_type = string.Empty;
            _pay_limit = null;
            _ded_limit = null;
            _ded_ytd = null;
            _yearrollover=string.Empty;
            _tax_code = string.Empty;
            _lo_ded_amt_null = false;
            _hi_ded_amt_null = false;

            _pay_limit = 0;
            _yearrollover = string.Empty;
            _acctKeyvalue = string.Empty;
            _acctAccoutType = string.Empty;
            _accountTypeId = 0;

            _description_MasterIncCodes = string.Empty;

            _pay_date = "01/01/1900";
        }

        #endregion Constructor

        #region Properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public int Doc_no
        {
            get { return _Doc_no; }
            set { _Doc_no = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public Boolean hi_ded_amt_null
        {
            get { return _hi_ded_amt_null; }
            set { _hi_ded_amt_null = value; }
        }
        public Boolean lo_ded_amt_null
        {
            get { return _lo_ded_amt_null; }
            set { _lo_ded_amt_null = value; }
        }

        public decimal? ded_rate
        {
            get { return _ded_rate; }
            set { _ded_rate = value; }
        }
        public decimal? amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
       public decimal? ded_ytd
       {
           get { return _ded_ytd; }
           set { _ded_ytd = value; }
       }
       public decimal? ded_limit
       {
           get { return _ded_limit; }
           set { _ded_limit = value; }
       }
       public string tax_code
       {
           get { return _tax_code; }
           set { _tax_code = value; }
       }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }


        public int mod_flag
        {
            get { return _mod_flag; }
            set { _mod_flag = value; }
        }

        public string add_code
        {
            get { return _add_code; }
            set { _add_code = value; }
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
       public string ded_taxred
       {
           get { return _ded_taxred; }
           set { _ded_taxred = value; }
       }
       public string ded_type
       {
           get { return _ded_type; }
           set { _ded_type = value; }
       }
       public bool dedflag
       {
           get { return _dedflag; }
           set { _dedflag = value; }
       }
       public decimal? pay_limit
       {
           get { return _pay_limit; }
           set { _pay_limit = value; }
       }
       public string yearrollover
       {
           get { return _yearrollover; }
           set { _yearrollover = value; }
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

       public string acctKeyvalue
       {
           get { return _acctKeyvalue; }
           set { _acctKeyvalue = value; }
       }
       public string acctAccoutType
       {
           get { return _acctAccoutType; }
           set { _acctAccoutType = value; }
       }//
       public int accountTypeId
       {
           get { return _accountTypeId; }
           set { _accountTypeId = value; }
       }

       public string description_MasterIncCodes
       {
           get { return _description_MasterIncCodes; }
           set { _description_MasterIncCodes = value; }
       }

       public string pay_date
       {
           get { return _pay_date; }
           set { _pay_date = value; }
       }


        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_PPDeductionsIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_PayDDUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_PayDDDel"; }
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
            get { return "Process_PayDeductions"; }
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
       public string DeductionsYTD
       {
           get { return "uspdedytdget"; }
       }
       public string FIND_checkpaydtl2
       {
           get { return "usp_checkpaydtl2"; }
       }
       public string FIND_stypayddamt
       {
           get { return "uspstypayddamt"; }
       }
       public string FIND_stypayddinc_ytd
       {
           get { return "uspstypayddinc_ytd"; }
       }
       public string INSERT_STYPARDD
       {
           get { return "USP_PPDeductionsIns"; }
       }
       public string FIND_stypayddytd
       {
           get { return "uspstypayddytd"; }
       }
       public string FIND_stypayddytd1
       {
           get { return "uspstypayddytd1"; }
       }
       public string FIND_stypayddytd2
       {
           get { return "uspstypayddytd2"; }
       }
       public string FIND_week_allow
       {
           get { return "usp_week_allow"; }
       }
       public string FIND_biweek_allow
       {
           get { return "usp_biweek_allow"; }
       }
       public string FIND_smonth_allow
       {
           get { return "usp_smonth_allow"; }
       }
       public string FIND_month_allow
       {
           get { return "usp_month_allow"; }
       }
       public string FIND_quarter_allow
       {
           get { return "usp_quarter_allow"; }
       }
       public string FIND_syear_allow
       {
           get { return "usp_syear_allow"; }
       }
       public string FIND_year_allow
       {
           get { return "usp_year_allow"; }
       }
       public string FIND_misc_allow
       {
           get { return "usp_misc_allow"; }
       }
       public string FIND_TaxValueGet
       {
           get { return "usp_TaxValueGet"; }
       }
       public string FIND_usp_tbl_check
       {
           get { return "usp_tbl_check"; }
       }

       //Added By Rajeev
       //Aim: To perform the operation for function ded_post()
       //Date 10/01/09
       public string FIND_DEDUCTION
       {
           get { return "uspded_postget"; }
       }
       //End of Modification By Rajeev

       //Added By Sarvjeet
       //Aim: Used in Update Payroll Entries Form to get payroll deductions
       //Date 27/07/09
       public string FIND_PYDEDUCTIONS
       {
           get { return "uspypayddget"; }
       }
       public string GET_YEARROLLOVER
       {
           get { return "uspyearrolloverget"; }
       }
       public string GET_BALANCEAMT
       {
           get { return "uspbalanceamt"; }
       }
       //Added By Rahul jain on 16/01/2010
       public string FIND_checkpaydtl22
       {
           get { return "USP_CheckPayDtlNew"; }
       }
       public string DELETE_STYPARDD
       {
           get { return "uspdeletestypaydd"; }
       }
       //Added By Sunil Pahwa on 21/01/2010
       public string FIND_checkpaydtl02
       {
           get { return "usp_checkpaydtl02"; }
       }
       //Added By Rahul jain on 21/01//2010
       public string FIND_stypayddamt1
       {
           get { return "USP_PPDeductionsamt1"; }
       }
       public string FIND_stypayddinc_ytd1
       {
           get { return "USP_PPDeductionsYTD1"; }
       }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT empl_code,soc_sec_num,first_name,last_name,");
            sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
            sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
            sql.Append(" statax_code,loctax_code,");
            sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
            sql.Append(" RowID");
            sql.Append(" FROM MasterEmployee");
            if (Convert.ToBoolean(parameters[9]) == true)
            {
                sql.Append(", EmployeeTCard_Header");
            }

            sql.Append(" WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND RowID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(empl_code) = '" + parameters[1].ToString().Trim() + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(soc_sec_num) = '" + parameters[2].ToString().Trim() + "'");
           if (parameters[3] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(first_name) LIKE  '" + parameters[3].ToString().Trim() + "%'");

                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(last_name) LIKE  '" + parameters[4].ToString().Trim() + "%'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND Rtrim(type_code) = " + parameters[5].ToString());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty && Convert.ToDateTime(parameters[6].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Rtrim(job_code) = '" + parameters[6].ToString().Trim() + "'");
          
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(job_title) = '" + parameters[7].ToString() + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty && Convert.ToDateTime(parameters[8].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND pay_period = '" + parameters[8].ToString().Trim() + "'");
             
            return sql.ToString();
        }

       public string GET_ALLPAYDD(ref Object[] parameters)
       {
           //StringBuilder sqlSubQuery = new StringBuilder();
           //sqlSubQuery.Append(" SELECT Process_PayEmployee.doc_no ");
           //sqlSubQuery.Append(" FROM Process_PayEmployee,MasterEmployee WHERE Process_PayEmployee.empl_code=MasterEmployee.empl_code");
           //sqlSubQuery.Append(" AND ok_to_post <> 'C'");
           //if (parameters[0] != null)
           //    if (parameters[0].ToString().Trim() != string.Empty)
           //        sqlSubQuery.Append(" AND Rtrim(type_code) MATCHES '" + parameters[0].ToString().Trim().Replace("'", "''") + "*'");
           //if (parameters[1] != null)
           //    if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[1].ToString().Trim().Contains("0001"))
           //        sqlSubQuery.Append(" AND pay_date = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

           StringBuilder sql = new StringBuilder();
           sql.Append(" SELECT Process_PayDeductions.doc_no,Process_PayDeductions.line_no,Process_PayDeductions.ded_code,Process_PayDeductions.ded_rate,Process_PayDeductions.amount,");
           sql.Append(" Process_PayDeductions.acct_no,Process_PayDeductions.department,Process_PayDeductions.mod_flag,Process_PayDeductions.add_code,Process_PayDeductions.lo_ded_amt,Process_PayDeductions.hi_ded_amt,Process_PayEmployee.pay_date ");
           sql.Append(" FROM Process_PayDeductions,Process_PayEmployee where Process_PayDeductions.doc_no=Process_PayEmployee.doc_no");
           if (parameters[0] != null)
               if (parameters[0].ToString().Trim().Length > 0)
                   sql.Append(" and Process_PayDeductions.doc_no in(" + parameters[0].ToString() + ")");


           return sql.ToString();
       }

       public string GetAllDeductions(ref Object[] parameters)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append(" SELECT Process_PayDeductions.ded_code,MasterDedcodes.description,Process_PayDeductions.amount,Process_PayDeductions.add_code,Process_PayDeductions.ded_rate,Process_PayDeductions.lo_ded_amt,");
           sql.Append(" Process_PayDeductions.hi_ded_amt,Process_PayDeductions.acct_no,Process_PayDeductions.department,Process_PayDeductions.line_no,Process_PayDeductions.doc_no");
           sql.Append(" from Process_PayDeductions, MasterDedcodes");
           sql.Append(" where  MasterDedcodes.ded_code = Process_PayDeductions.ded_code");
           sql.Append(" and Process_PayDeductions.doc_no in(" + parameters[0].ToString() + ")");
           return sql.ToString();

       }

        #endregion Stored-Procedures
    }
}
