using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmpTypeObligations : DVOBase
    {
        int _Rowid;
        string _type_code;// char(6) not null constraint "root".n310_113,
        string _obl_code;// char(6),
        int _line_no;// smallint,
        decimal? _obl_rate;// decimal(18,8),
        decimal? _obl_limit;// decimal(12),
        int _acct_no;// integer,
        string _department;// char(3),
        int _bal_acct_no;// integer,
        string _bal_dept;// char(3),
        decimal? _pay_limit;// decimal(12)

        string _acct_type;
        string _acct_desc;
        string _keyvalue;

        string _acct_type1;
        string _acct_desc1;
        string _keyvalue1;
        #region Constructor

        public DVOMasterEmpTypeObligations()
        {
            _Rowid = 0;
            _type_code = string.Empty;// char(6) not null
            _obl_code = string.Empty;// char(6),
            _line_no = 0;// smallint,
            _obl_rate = null;// decimal(18,8),
            _obl_limit = null;// decimal(12),
            _acct_no = 0;// integer,
            _department = string.Empty;// char(3),
            _bal_acct_no = 0;// integer,
            _bal_dept = string.Empty;// char(3),
            _pay_limit = null;// decimal(12)

             _acct_type=string.Empty;
             _acct_desc=string.Empty;
             _keyvalue=string.Empty;
             _acct_type1 = string.Empty;
             _acct_desc1 = string.Empty;
             _keyvalue1 = string.Empty;
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
        public string obl_code
        {
            get { return _obl_code; }
            set { _obl_code = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public decimal? obl_rate
        {
            get { return _obl_rate; }
            set { _obl_rate = value; }
        }
        public decimal? obl_limit
        {
            get { return _obl_limit; }
            set { _obl_limit = value; }
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

        public string acct_type1
        {
            get { return _acct_type1; }
            set { _acct_type1 = value; }
        }
        public string acct_desc1
        {
            get { return _acct_desc1; }
            set { _acct_desc1 = value; }
        }
        public string keyvalue1
        {
            get { return _keyvalue1; }
            set { _keyvalue1 = value; }
        }


        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpTypOdIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpTypOdUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpTypObldDel"; }
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
            get { return "MasterEmpTypeObligations"; }
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
            sql.Append("SELECT MasterEmpTypeObligations.Emptype_obl_id,MasterEmpTypeObligations.type_code,MasterEmpTypeObligations.obl_code,MasterEmpTypeObligations.line_no,MasterEmpTypeObligations.obl_rate,MasterEmpTypeObligations.obl_limit,MasterEmpTypeObligations.acct_no,MasterEmpTypeObligations.department,");
            sql.Append(" MasterEmpTypeObligations.bal_acct_no,MasterEmpTypeObligations.bal_dept,MasterEmpTypeObligations.pay_limit,");
            sql.Append("s1.acct_type,s1.acct_desc,s1.keyvalue,s2.acct_type,s2.acct_desc,s2.keyvalue ");
            sql.Append(" FROM MasterEmpTypeObligations LEFT outer JOIN PayrollGLAccounts s1 ON MasterEmpTypeObligations.acct_no=s1.acct_no LEFT OUTER JOIN PayrollGLAccounts s2 ON MasterEmpTypeObligations.bal_acct_no=s2.acct_no WHERE 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND Emptype_obl_id = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND type_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND obl_code = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
