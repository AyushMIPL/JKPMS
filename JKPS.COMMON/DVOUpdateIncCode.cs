using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdateIncCode : DVOBase
    {
        private string _inc_code;
        private string _description;
        private decimal? _dflt_num;
        private decimal? _dflt_rate;
        private decimal? _dflt_hours;
        private int? _dflt_acct;
        private string _dflt_dept;
        private string _inc_type;
        private decimal? _dflt_lo_inc_amt;
        private decimal? _dflt_hi_inc_amt;
        private string _non_qual;
        private string _dfltaccounttype;
        private string _dfltkeyvalue;
        private string _dfltacctdesc;
        private int _Rowid;
        #region Constructure
        public DVOUpdateIncCode()
        {
            _inc_code = string.Empty;
            _description = null;
            _dflt_num = null;
            _dflt_rate = null;
            _dflt_hours = null;
            _dflt_acct = null;
            _dflt_dept = null;
            _dflt_hi_inc_amt = null;
            _dflt_lo_inc_amt = null;
            _inc_type = null;
            _non_qual = null;
            _dfltaccounttype = null;
            _dfltkeyvalue = null;
            _dfltacctdesc = null;
            _Rowid = 0;

        }
        #endregion Constructure
        #region Properties
        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public Nullable<decimal> dflt_num
        {
            get { return _dflt_num; }
            set { _dflt_num = value; }
        }
        public Nullable<decimal> dflt_rate
        {
            get { return _dflt_rate; }
            set { _dflt_rate = value; }
        }
        public Nullable<decimal> dflt_hours
        {
            get { return _dflt_hours; }
            set { _dflt_hours = value; }
        }
        public Nullable<int> acct_no
        {
            get { return _dflt_acct; }
            set { _dflt_acct = value; }
        }
        public string inc_type
        {
            get { return _inc_type; }
            set { _inc_type = value; }
        }
        public Nullable<decimal> dflt_lo_inc_amt
        {
            get { return _dflt_lo_inc_amt; }
            set { _dflt_lo_inc_amt = value; }
        }
        public Nullable<decimal> dflt_hi_inc_amt
        {
            get { return _dflt_hi_inc_amt; }
            set { _dflt_hi_inc_amt = value; }
        }
        public string non_qual
        {
            get { return _non_qual; }
            set { _non_qual = value; }
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
        public string dfltacctdesc
        {
            get { return _dfltacctdesc; }
            set { _dfltacctdesc = value; }
        }
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        public int? dflt_acct
        {
            get { return _dflt_acct; }
            set { _dflt_acct = value; }
        }
        public string dflt_dept
        {
            get { return _dflt_dept; }
            set { _dflt_dept = value; }
        }

        #endregion Properties
        #region Procedures
        public override string INSERT_SPNAME
        {
            get { return "USP_IncCodeIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_IncCodeUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_IncCodeDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_IncCodeGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_IncCodeGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterIncCodes"; }
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
            sql.Append("select inc_code,description,dflt_num,dflt_rate,dflt_hours,dflt_acct,dflt_dept,inc_type,");
            sql.Append(" dflt_lo_inc_amt,dflt_hi_inc_amt,non_qual,dfltaccounttype,inc_code_id from  MasterIncCodes where 1=1");
           if(parameters[1]!=null)
            if (parameters[1].ToString() != string.Empty)
                sql.Append(" and Rtrim(inc_code)='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
            {
                if ( parameters[2].ToString() != string.Empty)
                    sql.Append(" and Rtrim(description) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            }
            if (parameters[3] != null)
                sql.Append(" and dflt_num=" + Convert.ToDecimal(parameters[3]));
            if (parameters[4] != null)
                sql.Append(" and dflt_rate=" + Convert.ToDecimal(parameters[4]));
            if (parameters[5] != null)
                sql.Append(" and dflt_hours=" + Convert.ToDecimal(parameters[5]));
            if(parameters[6]!=null)
                sql.Append(" and dflt_acct=" + Convert.ToDecimal(parameters[6]));
            if (parameters[7] != null)
                sql.Append(" and dflt_dept=" + Convert.ToDecimal(parameters[7]));
            if (parameters[8] != null)
            {
                if (parameters[8].ToString() != null && parameters[8].ToString() != string.Empty)
                    sql.Append(" and Rtrim(inc_type)='" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            }
            if (parameters[9] != null)
                sql.Append(" and dflt_lo_inc_amt=" + Convert.ToDecimal(parameters[8]));
            if (parameters[10] != null)
                sql.Append(" and dflt_hi_inc_amt=" + Convert.ToDecimal(parameters[9]));
            
            if (parameters[9] != null)
            {
                if (parameters[9].ToString() != null && parameters[9].ToString() != string.Empty)
                    sql.Append(" and Rtrim(non_qual )='" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            }
            //Added by Sunil Pahwa on 10/2/09

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND inc_code_id = " + parameters[0].ToString());
            sql.Append(" order by inc_code ");
            return sql.ToString();
        }
        public string GetIncomeDataOnly(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select inc_code,");
            sql.Append("  dfltaccounttype from  MasterIncCodes where 1=1");
            if (parameters[0] != null)
              if (parameters[0].ToString() != string.Empty)
                sql.Append(" AND Rtrim(inc_code)='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        public string FIND_DESC()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select inc_code,description from MasterIncCodes");
            return sql.ToString();
        }

        #endregion Procedures
    }
}
