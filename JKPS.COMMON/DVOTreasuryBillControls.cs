using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 04 June 2009
    /// Description :Treasury Bill Control form
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOTreasuryBillControls : DVOBase
    {
        private int _Rowid;
        private int _bank_acct_no;
        private string _bank_acct_no_keyvalue;
        private string _bank_acct_no_accounttype;
        private int _deposit_acct_no;
        private string _deposit_acct_no_keyvalue;
        private string _deposit_acct_no_accounttype;
        private int _interest_acct_no;
        private string _interest_acct_no_keyvalue;
        private string _interest_acct_no_accounttype;
        private int _pay_acct_no;
        private string _pay_acct_no_keyvalue;
        private string _pay_acct_no_accounttype;
        private int _next_receipt_no;
        private int _current_issue_num;
        private decimal _amount_per_100;
        private int _Insertby;
        private DateTime _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private DateTime _Updatedate;
        private string _UpdateMachineInfo;
        private int _default_scheme;
        private string _scheme_name;

         
        private string _chk_stat_lmt;
        private string _chk_no_of_prsn;
        private string _chk_min_amt;
        private int _tb_post_no;
        private int _approval_code;

        private string _chk_max_amt;
        private string _rstct_pnd_rcv;
        #region Constructor
        public DVOTreasuryBillControls()
        {
            _Rowid = 0;
            _bank_acct_no = 0;
            _deposit_acct_no = 0;
            _interest_acct_no = 0;
            _pay_acct_no = 0;
            _next_receipt_no = 0;
            _current_issue_num = 0;
            _amount_per_100 = 0;
            _Insertby = 0;
            _InsertDate = Convert.ToDateTime("01/01/1900");
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _Updatedate = Convert.ToDateTime("01/01/1900");
            _UpdateMachineInfo = string.Empty;
            _default_scheme = 0;
            _scheme_name = string.Empty;
            _chk_stat_lmt = string.Empty;
            _chk_no_of_prsn = string.Empty;
            _chk_min_amt = string.Empty;
            _approval_code = 0;
            _tb_post_no = 0;

            _chk_max_amt=string.Empty;
            _rstct_pnd_rcv=string.Empty;


        }
        #endregion Constructor
        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int default_scheme
        {
            get { return _default_scheme; }
            set { _default_scheme = value; }
        }

        public string  scheme_name
        {
            get { return _scheme_name ; }
            set { _scheme_name = value; }
        }
        public int bank_acct_no
        {
            get { return _bank_acct_no; }
            set { _bank_acct_no = value; }
        }
        public string bank_acct_no_keyvalue
        {
            get { return _bank_acct_no_keyvalue; }
            set { _bank_acct_no_keyvalue = value; }
        }
        public string bank_acct_no_accounttype
        {
            get { return _bank_acct_no_accounttype; }
            set { _bank_acct_no_accounttype = value; }
        }
        public int deposit_acct_no
        {
            get { return _deposit_acct_no; }
            set { _deposit_acct_no = value; }
        }
        public string deposit_acct_no_keyvalue
        {
            get { return _deposit_acct_no_keyvalue; }
            set { _deposit_acct_no_keyvalue = value; }
        }
        public string deposit_acct_no_accounttype
        {
            get { return _deposit_acct_no_accounttype; }
            set { _deposit_acct_no_accounttype = value; }
        } 
           
        public int interest_acct_no
        {
            get { return _interest_acct_no; }
            set { _interest_acct_no = value; }
        }
        public string interest_acct_no_keyvalue
        {
            get { return _interest_acct_no_keyvalue; }
            set { _interest_acct_no_keyvalue = value; }
        }
        public string interest_acct_no_accounttype
        {
            get { return _interest_acct_no_accounttype; }
            set { _interest_acct_no_accounttype = value; }
        }
        public int pay_acct_no
        {
            get { return _pay_acct_no; }
            set { _pay_acct_no = value; }
        }
        public string pay_acct_no_keyvalue
        {
            get { return _pay_acct_no_keyvalue; }
            set { _pay_acct_no_keyvalue = value; }
        }
        public string pay_acct_no_accounttype
        {
            get { return _pay_acct_no_accounttype; }
            set { _pay_acct_no_accounttype = value; }
        }
        public int next_receipt_no
        {
            get { return _next_receipt_no; }
            set { _next_receipt_no = value; }
        }
        public int current_issue_num
        {
            get { return _current_issue_num; }
            set { _current_issue_num = value; }
        }
        public decimal amount_per_100
        {
            get { return _amount_per_100; }
            set { _amount_per_100 = value; }
        }
        public int InsertBy
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }

        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value;}
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value;}
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public DateTime Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }


        public string chk_stat_lmt
        {
            get { return _chk_stat_lmt; }
            set { _chk_stat_lmt = value; }
        }
        public string chk_no_of_prsn
        {
            get { return _chk_no_of_prsn; }
            set { _chk_no_of_prsn = value; }
        }
        public string chk_min_amt
        {
            get { return _chk_min_amt; }
            set { _chk_min_amt = value; }
        }

        public int tb_post_no
        {
            get { return _tb_post_no; }
            set { _tb_post_no = value; }
        }
        public int approval_code
        {
            get { return _approval_code; }
            set { _approval_code = value; }
        }

        public string chk_max_amt
        {
            get { return _chk_max_amt; }
            set { _chk_max_amt = value; }
        }
         public string rstct_pnd_rcv
        {
            get { return _rstct_pnd_rcv; }
            set { _rstct_pnd_rcv = value; }
        }

        #endregion public properties
        #region Stored-Procedures

        public string GET_CNTRL_INFO
        {
            get { return "usp_get_cntrl_info"; }
        }
        public string GET_DEFAULT_SCHEME
        {
            get { return "usp_get_def_schm"; }
        }
        public override string INSERT_SPNAME
        {
            get { return "usp_ins_trea_bill"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_trea_bill"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "usp_get_trea_bill"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "tbcntrc"; }
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


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT rowid,class_code,class_desc FROM tbclasses where 1=1");
            //if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//class_code
            //    sql.Append(" AND class_code = '" + parameters[0].ToString() + "'");
            //if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//class_desc
            //    sql.Append(" AND class_desc LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
