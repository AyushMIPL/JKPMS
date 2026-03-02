using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmpTypeDeductions : DVOBase
    {
        int _Rowid;
        string _type_code;// char(6) not null constraint "root".n307_110,
        string _ded_code;// char(6),
        int _line_no;// smallint,
        decimal? _ded_rate;// decimal(18,8),
        decimal? _ded_limit;//t decimal(12),
        string _ded_apply;// char(1),
        int _acct_no;// integer,
        string _department;// char(3),
        decimal? _lo_ded_amt;// decimal(12),
        decimal? _hi_ded_amt;// decimal(12),
        decimal? _pay_limit;// decimal(12)

        string _acct_type;// char(3),
        string _acct_desc;// char(3),
        string _keyvalue;// char(3),
       

        #region Constructor

        public DVOMasterEmpTypeDeductions()
        {
            _Rowid = 0;
            _type_code = string.Empty;// char(6) not null
            _ded_code = string.Empty;// char(6),
            _line_no = 0;// smallint,
            _ded_rate = null;// decimal(18,8),
            _ded_limit = null;//t decimal(12),
            _ded_apply = string.Empty;// char(1),
            _acct_no = 0;// integer,
            _department = string.Empty;// char(3),
            _lo_ded_amt = null;// decimal(12),
            _hi_ded_amt = null;// decimal(12),
            _pay_limit = null;// decimal(12)

             _acct_type=string.Empty;
             _acct_desc=string.Empty;
             _keyvalue=string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        public string type_code
        {
            get { return _type_code; }
            set { _type_code = value; }
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

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }


        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpTypDdIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpTypDdUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpTypDeddDel"; }
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
            get { return "MasterEmpTypeDeductions"; }
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

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MasterEmpTypeDeductions.Emp_Type_Ded_ID,MasterEmpTypeDeductions.type_code,MasterEmpTypeDeductions.ded_code,MasterEmpTypeDeductions.line_no,MasterEmpTypeDeductions.ded_rate,MasterEmpTypeDeductions.ded_limit,");
            sql.Append(" MasterEmpTypeDeductions.ded_apply,MasterEmpTypeDeductions.acct_no,MasterEmpTypeDeductions.department,MasterEmpTypeDeductions.lo_ded_amt,MasterEmpTypeDeductions.hi_ded_amt,MasterEmpTypeDeductions.pay_limit,");
            sql.Append(" PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_desc,PayrollGLAccounts.keyvalue ");
            sql.Append(" FROM MasterEmpTypeDeductions LEFT Outer JOIN PayrollGLAccounts  ON  MasterEmpTypeDeductions.acct_no=PayrollGLAccounts.acct_no ");
            sql.Append(" WHERE  1=1 " );
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND Emp_Type_Ded_ID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND type_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND ded_code = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
