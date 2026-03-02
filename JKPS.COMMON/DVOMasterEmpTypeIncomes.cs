using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmpTypeIncomes : DVOBase
    {
        int _Rowid;
        string _type_code;//char(6) not null ,
        string _inc_code;// char(6),
        int _line_no;// smallint,
        decimal? _inc_rate;// decimal(18,8),
        decimal? _inc_number;// decimal(18,8),
        decimal? _inc_hours;// decimal(12),
        int _acct_no;// integer,
        string _department;// char(3),
        decimal? _lo_inc_amt;// decimal(12),
        decimal? _hi_inc_amt;// decimal(12)

        string _acct_type;
        string _acct_desc;
        string _keyvalue;

        #region Constructor

        public DVOMasterEmpTypeIncomes()
        {
            _Rowid = 0;
            _type_code = string.Empty;//char(6) not null ,
            _inc_code = string.Empty;// char(6),
            _line_no = 0;// smallint,
            _inc_rate = null;// decimal(18,8),
            _inc_number = null;// decimal(18,8),
            _inc_hours = null;// decimal(12),
            _acct_no = 0;// integer,
            _department = string.Empty;// char(3),
            _lo_inc_amt = null;// decimal(12),
            _hi_inc_amt = null;// decimal(12)

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

        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }

        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }

        public decimal? inc_rate
        {
            get { return _inc_rate; }
            set { _inc_rate = value; }
        }

        public decimal? inc_number
        {
            get { return _inc_number; }
            set { _inc_number = value; }
        }

        public decimal? inc_hours
        {
            get { return _inc_hours; }
            set { _inc_hours = value; }
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
            get { return "USP_EmpTypIdIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpTypIdUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpTypeIdDel"; }
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
            get { return "MasterEmpTypeIncomes"; }
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
            sql.Append("SELECT  MasterEmpTypeIncomes.Emp_type_ID,MasterEmpTypeIncomes.type_code,MasterEmpTypeIncomes.inc_code,MasterEmpTypeIncomes.line_no,MasterEmpTypeIncomes.inc_rate,MasterEmpTypeIncomes.inc_number,");
            sql.Append(" MasterEmpTypeIncomes.inc_hours,MasterEmpTypeIncomes.acct_no,MasterEmpTypeIncomes.department,MasterEmpTypeIncomes.lo_inc_amt,MasterEmpTypeIncomes.hi_inc_amt,");
            sql.Append(" PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_desc,PayrollGLAccounts.keyvalue ");
            sql.Append(" FROM MasterEmpTypeIncomes LEFT Outer Join PayrollGLAccounts ON  MasterEmpTypeIncomes.acct_no=PayrollGLAccounts.acct_no ");
            sql.Append(" WHERE 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND Emp_Type_Ded_ID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND type_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND inc_code = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
