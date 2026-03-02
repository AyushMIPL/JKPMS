using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPayrollStypayod : DVOBase
    {
         private int _RowID;
        private int _Doc_no;
        private int _line_no;
        private string _obl_code;  //date

        private decimal? _obl_rate;
        private decimal? _amount;
        private int _acct_no;
        private string _department;
        private int _bal_acct_no;
        private string _bal_dept;

        private int _mod_flag;
        private string _add_code;
        private string _obl_type;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;
        private decimal? _obl_limit;
        private decimal? _pay_limit;
        private decimal? _obl_ytd;

        string _acctKeyvalue;
        string _acctAccountType;
        string _balacctKeyvalue;
        string _balacctAccountType;
        int _acctAccountTypeId;
        int _balacctAccountTypeId;

        //Added By Rajeev 
        //Aim: To use in inc_post
        //Date :09/01/09
        private string _description_MasterIncCodes;

        string _pay_date;

        #region Constructor      


        public DVOPayrollStypayod()
        {
            _RowID = 0;
            _Doc_no = 0;
            _line_no = 0;
            _obl_code = string.Empty;
            _obl_rate = null;
            _amount = null;
            _acct_no = 0;
            _department = "000";
            _mod_flag = 0;
            _add_code = string.Empty;

            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;
            _bal_acct_no = 0;
            _bal_dept = "000";
            _obl_type = string.Empty;
            _obl_limit = null;
            _pay_limit = null;
            _obl_ytd = null;

            _acctKeyvalue = string.Empty;
            _acctAccountType = string.Empty;
            _balacctKeyvalue = string.Empty;
            _balacctAccountType = string.Empty;
            _acctAccountTypeId = 0;
            _balacctAccountTypeId = 0;

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
        public string obl_code
        {
            get { return _obl_code; }
            set { _obl_code = value; }
        }
        public int bal_acct_no
        {
            get { return _bal_acct_no; }
            set { _bal_acct_no = value; }
        }
        public string bal_dept
        {
            get { return _bal_dept; }
            set { _bal_dept = value; }
        }
        public string obl_type
        {
            get { return _obl_type; }
            set { _obl_type = value; }
        }
        public decimal? obl_rate
        {
            get { return _obl_rate; }
            set { _obl_rate = value; }
        }
        public decimal? obl_ytd
        {
            get { return _obl_ytd; }
            set { _obl_ytd = value; }
        }

        public decimal? pay_limit
        {
            get { return _pay_limit; }
            set { _pay_limit = value; }
        }
        public decimal? obl_limit
        {
            get { return _obl_limit; }
            set { _obl_limit = value; }
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
        public string acctAccountType
        {
            get { return _acctAccountType; }
            set { _acctAccountType = value; }
        }
        public string balacctKeyvalue
        {
            get { return _balacctKeyvalue; }
            set { _balacctKeyvalue = value; }
        }
        public string balacctAccountType
        {
            get { return _balacctAccountType; }
            set { _balacctAccountType = value; }
        }
        public int acctAccountTypeId
        {
            get { return _acctAccountTypeId; }
            set { _acctAccountTypeId = value; }
        }
        public int balacctAccountTypeId
        {
            get { return _balacctAccountTypeId; }
            set { _balacctAccountTypeId = value; }
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
            get { return "USP_PPObligationsIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_PPobligationsUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppayoddel"; }
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
            get { return "stypayod"; }
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
        public string INSERT_STYPAYOD
        {
            get { return "USP_PPObligationsIns"; }
        }
        public string FIND_stypayodytd
        {
            get { return "uspstypayodytd"; }
        }
        public string DELETE_STYPAYOD
        {
            get { return "uspdeletestypayod"; }
        }
        public string FIND_stypayodytd1
        {
            get { return "uspstypayodytd1"; }
        }
        public string FIND_stypayodytd2
        {
            get { return "uspstypayodytd2"; }
        }
        //Added By Sarvjeet
        //Aim: Used in Update Payroll Entries Form to get payroll obligations
        //Date 27/07/09
        public string FIND_PYOBLICATIONS
        {
            get { return "uspypayodget"; }
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
                    sql.Append(" AND Rtrim(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
           if (parameters[3] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(first_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(last_name) LIKE  '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND Rtrim(type_code) = " + parameters[5].ToString());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty && Convert.ToDateTime(parameters[6].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Rtrim(job_code) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
          
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(job_title) = '" + parameters[7].ToString().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty && Convert.ToDateTime(parameters[8].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND pay_period = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
             
            return sql.ToString();
        }

        //Added By Rajeev
        //Aim: To perform the operation for function obl_post()
        //Date 13/01/09
        public string FIND_OBLIGATION_POST
        {
            get { return "uspobl_postget"; }
        }
        //End of Modification By Rajeev

        public string GET_ALLPAYOD(ref Object[] parameters)
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
            sql.Append(" SELECT Process_Payobligations.doc_no,stypayod.line_no,Process_Payobligations.obl_code,Process_Payobligations.obl_rate,Process_Payobligations.amount,Process_Payobligations.acct_no,");
            sql.Append(" Process_Payobligations.department,Process_Payobligations.bal_acct_no,Process_Payobligations.bal_dept,Process_Payobligations.mod_flag,Process_Payobligations.add_code,Process_PayEmployee.pay_date ");
            sql.Append(" FROM Process_Payobligations,Process_PayEmployee where Process_Payobligations.doc_no=Process_PayEmployee.doc_no");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" and Process_Payobligations.doc_no in(" + parameters[0].ToString() + ")");


            return sql.ToString();
        }

        public string GetAllObligations(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT Process_Payobligations.obl_code,MasterOblCodes.description,Process_Payobligations.amount,Process_Payobligations.obl_rate,");
            sql.Append(" Process_Payobligations.acct_no,Process_Payobligations.department,Process_Payobligations.bal_acct_no,Process_Payobligations.bal_dept,");
            sql.Append(" Process_Payobligations.line_no,Process_Payobligations.add_code,Process_Payobligations.doc_no");
            sql.Append(" FROM  Process_Payobligations, MasterOblCodes");
            sql.Append(" WHERE MasterOblCodes.obl_code = Process_Payobligations.obl_code");
            sql.Append(" AND Process_Payobligations.doc_no in(" + parameters[0].ToString() + ")");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
