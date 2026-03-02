using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOCapExpMntRpt : DVOBase
    {
        private string _period_month;
        private string _period_year;
        private string _ministry;

        # region Properties

        public string Month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string Year
        {
            get { return _period_year; }
            set { _period_year = value; }

        }
        public string Ministry
        {
            get { return _ministry; }
            set { _ministry = value; }
        }
        # endregion Properties

        #region Constructure
        public DVOCapExpMntRpt()
        {
            _period_month = string.Empty;
            _period_year = string.Empty;
            _ministry = string.Empty;
        }

        #endregion Constructure

        #region StoreProcedures
        
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { return "uspCapExpMntRpt"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
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

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select inbestid.approved, inbestid.revised, PayrollGLAccounts.acct_no, PayrollGLAccounts.keyvalue, PayrollGLAccounts.acct_desc");
            sql.Append(" from PayrollGLAccounts, inbestid where PayrollGLAccounts.acct_type = 'CAPEXP' AND PayrollGLAccounts.acct_cat = 'U' ");
            sql.Append(" AND PayrollGLAccounts.acct_no = inbestid.account ");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" and inbestid.year ='" + parameters[1].ToString().Trim() + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" and PayrollGLAccounts.keyvalue LIKE '"+parameters[2].ToString().Trim()+"%'");

            sql.Append(" order by PayrollGLAccounts.keyvalue");
            return sql.ToString();
        }

        public string FIND_STXCHRTD(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" select keyvalue, PayrollGLAccounts.acct_no, balance, activity, this_month ");
            sql.Append(" from PayrollGLAccounts, stxchrtd where  acct_cat <> 'U' and acct_type = 'CAPEXP' ");
            sql.Append(" and PayrollGLAccounts.acct_no = stxchrtd.acct_no ");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" and stxchrtd.period_month ='" + parameters[0].ToString().Trim() + "'");

            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" and stxchrtd.period_year ='" + parameters[1].ToString().Trim() + "'");

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" and PayrollGLAccounts.keyvalue LIKE '" + parameters[2].ToString().Trim() + "%'");
            sql.Append(" order by PayrollGLAccounts.keyvalue");
            return sql.ToString();
        }

        #endregion StoreProcedures
    }
}
