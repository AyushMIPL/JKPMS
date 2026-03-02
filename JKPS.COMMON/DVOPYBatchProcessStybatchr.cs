using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPYBatchProcessStybatchr : DVOBase
    {
        private int _pybatchid;
        private string  _startedon;
        private string _endedon;
        private string _searchcriteria;
        private int _recordsearched;
        private int _recordcound;
        private string _errormessage;
        private int _status;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;
        private string _Districts;

        public DVOPYBatchProcessStybatchr()
        {
            _pybatchid = 0;
            _startedon = "01/01/1900";
            _endedon = "01/01/1900";
            _searchcriteria = string.Empty;
            _recordsearched = 0;
            _recordcound = 0;
            _errormessage = string.Empty;
            _status = 0;
            _insertby = 0;
            _insertdate = "01/01/1900";
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate="01/01/1900";
            _updatemachineinfo = string.Empty;
            _Districts = string.Empty;

        }

        #region Public Properties
        public int pybatchid
        {
            get { return _pybatchid; }
            set { _pybatchid = value; }
        }


        public string  startedon
        {
            get { return _startedon; }
            set { _startedon = value; }

        }

        public string endedon
        {
            get { return _endedon; }
            set { _endedon = value; }

        }
        
        public string searchcriteria
        {
            get { return _searchcriteria; }
            set { _searchcriteria = value; }

        }
        public int recordsearched
        {
            get { return _recordsearched; }
            set { _recordsearched = value; }

        }
        public int recordcound
        {
            get { return _recordcound; }
            set { _recordcound = value; }

        }
        public string errormessage
        {
            get { return _errormessage; }
            set { _errormessage = value; }

        }

        public int status
        {
            get { return _status; }
            set { _status = value; }

        }

        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }

        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }

        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }

        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }

        }
        public string  updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }

        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }

       

        public string processedStartedOn { get; set; }

        public string Districts
        {
            get { return _Districts; }
            set { _Districts = value; }

        }
        #endregion Properties


        #region Stored-Procedures

        //public string AUTHENTICATION_SPNAME
        //{
        //    get { return ""; }
        //}

        public override string INSERT_SPNAME
        {
            get { return "USP_PYBtchIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_PyBtchUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_PyBtchDelete"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public string GET_ACTIVE_BATCHES
        {
            get { return "USP_PYActvBtchGet"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }

        public string FIND_CHECKS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select MasterEmployee.first_name,MasterEmployee.middle_name, MasterEmployee.last_name,");
            sql.Append(" Process_PayEmployee.empl_code,Process_PayEmployee.pay_date, Process_PayEmployee.eop_date,PayrollGLAccounts.keyvalue,");
            sql.Append(" Process_PayEmployee.print_check, Process_PayEmployee.check_no,Process_PayEmployee.ok_to_post,");
            sql.Append(" Process_PayEmployee.doc_no, Process_PayEmployee.cash_amount, Process_PayEmployee.deposit,");
            sql.Append(" doc_date");//sql.Append(" Process_PayEmployee.rowid,doc_date");//Commented By Neeraj there is now rowid column in Process_PayEmployee
            sql.Append(" from MasterEmployee,Process_PayEmployee, PayrollGLAccounts");//sql.Append(" from MasterEmployee,Process_PayEmployee, outer PayrollGLAccounts");//Commented By Neeraj out join sytax error
            sql.Append(" where MasterEmployee.empl_code=Process_PayEmployee.empl_code");
            sql.Append(" and Process_PayEmployee.cash_acct_no=PayrollGLAccounts.acct_no");
            sql.Append(" and Process_PayEmployee.ok_to_post='Y'");//sql.Append(" and Process_PayEmployee.ok_to_post='P'");//By Neeraj 'P' to 'Y'
            sql.Append(" and Process_PayEmployee.deposit='N'");
            sql.Append(" and Process_PayEmployee.check_no is not null");
            sql.Append(" and Process_PayEmployee.check_no <> '0'");
           
            if (parameters[0] != null && parameters[1].ToString().Trim().Length > 0)
                if (parameters[0].ToString().Trim() != Convert.ToDateTime(null).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture))
                    sql.Append(" and Process_PayEmployee.pay_date >= '" + parameters[0].ToString().Trim() + "'");

            if (parameters[1] != null && parameters[1].ToString().Trim().Length > 0)
                if (parameters[1].ToString().Trim() != Convert.ToDateTime(null).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture))
                    sql.Append(" and Process_PayEmployee.pay_date <='" + parameters[1].ToString().Trim() + "'");

            
           
           
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }

}


