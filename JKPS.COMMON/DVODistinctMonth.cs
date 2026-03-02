using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVODistinctMonth : DVOBase
    {
        private string _period_month;


        # region Properties
        public string month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }


        #endregion Properties

        #region Stored-Procedures

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
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { return "uspDistnMGetAll"; }
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
        public string GET_DISTICT_MONTH_XPERDR
        {
            get { return "USP_DistinctMNTHPerdr"; }
        }
        // using in Multi Tax Analysis Summary reports
        public string GET_DISTINCT_MONTH
        {
            get { return "uspdistmonthgetall"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.keyvalue[1,2] ministry ,PayrollGLAccounts.keyvalue[9,10] objCode,");
            sql.Append(" Master_Segment.desc p_desc,balance p_yeartodate, inbestid.revised  budgetedamt");
            sql.Append(" from PayrollGLAccounts,outer stxchrtd,Master_Segment,outer inbestid");
            sql.Append(" where PayrollGLAccounts.acct_no=stxchrtd.acct_no");
            sql.Append(" and Master_Segment.keyvalue=PayrollGLAccounts.keyvalue[9,10]");
            sql.Append(" and PayrollGLAccounts.acct_type='CAPREV'");
            sql.Append(" and Master_Segment.segmentid=33");
            sql.Append(" and inbestid.account=PayrollGLAccounts.acct_no");
            sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and inbestid.year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and stxchrtd.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2].ToString() != string.Empty)
                sql.Append(" and  PayrollGLAccounts.keyvalue[1,2]='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
