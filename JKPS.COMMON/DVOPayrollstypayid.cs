using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPayrollstypayid : DVOBase
    {
         private int _RowID;
        private int _Doc_no;
        private int _line_no;
        private string _inc_code;  //date

        private decimal? _inc_rate;
        private decimal? _number;
        private decimal? _hours;
        private decimal? _amount;
        private int _acct_no;
        private string _department;
        private int _mod_flag;
        private string _add_code;
        private string _inc_type;
        private decimal? _lo_inc_amt;
        private decimal? _hi_inc_amt;

        private Boolean _lo_inc_amt_null;
        private Boolean _hi_inc_amt_null;

        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;

        string _acctKeyvalue;
        string _acctAccountType;
        int _acctAccountTypeId;

        //Added By Rajeev 
        //Aim: To use in inc_post
        //Date :09/01/09
        private string _description_MasterIncCodes;

        string _empl_code;
        private int _error;
        string _pay_date;
     
        #region Constructor

        public DVOPayrollstypayid()
        
        { 
            _RowID = 0;
            _Doc_no = 0;
            _line_no = 0;
            _inc_code = string.Empty;
            _inc_rate = null;
            _number = null;
            _hours = null;
            _amount = null;
            _acct_no = 0;
            _department = "000";
            _mod_flag=0;
            _add_code = string.Empty;
            _lo_inc_amt = null;
            _hi_inc_amt = null;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;
            _inc_type = string.Empty;
            _lo_inc_amt_null = false;
            _hi_inc_amt_null = false;

            _acctKeyvalue = string.Empty;
            _acctAccountType = string.Empty;
            _acctAccountTypeId = 0;

            _description_MasterIncCodes = string.Empty;

            _empl_code = string.Empty;
            _error = 0;
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
        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }
        public Boolean hi_inc_amt_null
        {
            get { return _hi_inc_amt_null; }
            set { _hi_inc_amt_null = value; }
        }
        public Boolean lo_inc_amt_null
        {
            get { return _lo_inc_amt_null; }
            set { _lo_inc_amt_null = value; }
        }
        public decimal? inc_rate
        {
            get { return _inc_rate; }
            set { _inc_rate = value; }
        }
        public decimal? number
        {
            get { return _number; }
            set { _number = value; }
        }
        public decimal? hours
        {
            get { return _hours; }
            set { _hours = value; }

        }
        public decimal? amount
        {
            get { return _amount; }
            set { _amount = value; }
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
        public string inc_type
        {
            get { return _inc_type; }
            set { _inc_type = value; }
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
        public decimal? lo_inc_amt
        {
            get { return _lo_inc_amt; }
            set { _lo_inc_amt = value; }
        }
        public decimal? hi_inc_amt
        {
            get { return _hi_inc_amt; }
            set { _hi_inc_amt = value; }
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
        }//
        public string acctAccountType
        {
            get { return _acctAccountType; }
            set { _acctAccountType = value; }
        }
        public int acctAccountTypeId
        {
            get { return _acctAccountTypeId; }
            set { _acctAccountTypeId = value; }
        }

        public string description_MasterIncCodes
        {
            get { return _description_MasterIncCodes; }
            set { _description_MasterIncCodes = value; }
        }

        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public int error
        {
            get { return _error; }
            set { _error = value; }
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
            get { return "USP_PPIncomes"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspoayidupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppayiddel"; }
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
            get { return "stypayid"; }
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
        public string FIND_checkpaydtl1
        {
            get { return "usp_checkpaydtl1"; }
        }
        public string FIND_stypayidamt
        {
            get { return "uspstypayidamt"; }
        }
        public string FIND_stypayidinc_ytd
        {
            get { return "uspstypayidinc_ytd"; }
        }

        //Added By Rajeev
        //Aim: To perform the operation for function inc_post()
        //Date 09/01/09
        public string FIND_INCOME
        {
            get { return "uspinc_postget"; }
        }
        //End of Modification By Rajeev

        public string INSERT_STYPAYID
        {
            get { return "USP_PPIncomes"; }
        }
        public string DELETE_STYPAYID
        {
            get { return "uspdeletestypayid"; }
        }
   
        public string FIND_PYINCOMES
        {
            get { return "uspypayidget"; }
        }

        //Added By Sunil
        //Aim: Used in insert Bonus for Employees
        //Date 7/12/2009
        public string INS_STYPAYID_INFO
        {
            get { return "uspstypayidins"; }
        }
        //Added By Sunil
        //Aim: Used in GET Employees with Bonus
        //Date 7/12/2009
        public string GET_BONUS_INFO
        {
            get { return "uspempbnspayreget"; }
        }
        //Added By Rahul Jain on 16/01/2010
        public string FIND_checkpaydtl11
        {
            get { return "USP_CheckPayDtl"; }
        }
        //Added by sunil Pahwa on 21/01/10
        public string FIND_checkpaydtl01
        {
            get { return "usp_checkpaydtl01"; }
        }
        //Added By Rahul JAin on 21/01/2010
        public string FIND_stypayidamt1
        {
            get { return "USP_PPIncomesamt1"; }
        }
        public string FIND_stypayidinc_ytd1
        {
            get { return "USP_PPIncomesYTD1"; }
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
        public string GET_ALLPAYID(ref Object[] parameters)
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
            sql.Append(" SELECT Process_PayIncomes.doc_no,Process_PayIncomes.line_no,Process_PayIncomes.inc_code,");
            sql.Append(" Process_PayIncomes.inc_rate ,Process_PayIncomes.number,Process_PayIncomes.hours,Process_PayIncomes.amount,");
            sql.Append(" Process_PayIncomes.acct_no,Process_PayIncomes.department,Process_PayIncomes.add_code,Process_PayIncomes.lo_inc_amt,");
            sql.Append(" Process_PayIncomes.hi_inc_amt,MasterEmployee.empl_code,MasterIncCodes.inc_type,Process_PayEmployee.pay_date ");
            sql.Append(" FROM Process_PayIncomes,Process_PayEmployee,MasterEmployee,MasterIncCodes where 1=1");
            sql.Append(" and Process_PayIncomes.doc_no = Process_PayEmployee.doc_no");
            sql.Append(" and Process_PayEmployee.empl_code = MasterEmployee.empl_code");
            sql.Append(" and Process_PayIncomes.inc_code = MasterIncCodes.inc_code");
            if (parameters[0] != null)
                if(parameters[0].ToString().Trim().Length>0)
                    sql.Append(" and Process_PayIncomes.doc_no in(" + parameters[0].ToString() + ")");

            return sql.ToString();
        }

        public string GetAllIncomes(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Process_PayIncomes.inc_code,MasterIncCodes.description,Process_PayIncomes.amount,Process_PayIncomes.add_code,");
            sql.Append(" Process_PayIncomes.number,Process_PayIncomes.inc_rate,Process_PayIncomes.hours,Process_PayIncomes.lo_inc_amt,");
            sql.Append(" Process_PayIncomes.hi_inc_amt,Process_PayIncomes.acct_no,Process_PayIncomes.department,Process_PayIncomes.line_no,Process_PayIncomes.doc_no");
            sql.Append(" from Process_PayIncomes, MasterIncCodes");
            sql.Append(" where MasterIncCodes.inc_code = Process_PayIncomes.inc_code");
            sql.Append(" and Process_PayIncomes.doc_no in(" + parameters[0].ToString() + ")");
            return sql.ToString();
        }

       

        

        #endregion Stored-Procedures
    }
}
