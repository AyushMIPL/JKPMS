using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public  class DVORecRevByObj:DVOBase
    {
        private string _period_month;
        private string _period_year;

    public DVORecRevByObj()
        {
            _period_month = string.Empty;
            _period_year = string.Empty;
        }

        # region Properties

        public string _month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string _Year
        {
            get { return _period_year; }
            set { _period_year = value; }
                 
        }
        # endregion Properties
        #region Constructure
       

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
            get { return "uspRecRevByObj";}
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
        public string GET_RECREV_EST
        {
            get { return "uspestrecrevoc"; }
        }
        public override string FIND_QUERY(ref object[] parameters) 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.keyvalue[9,10] p_objcode,desc p_Desc ,sum(balance) p_yeartodate,");
            sql.Append("sum(this_month) p_this_month , sum(activity) P_Activity");
            sql.Append(" FROM PayrollGLAccounts,stxchrtd,Master_Segment where acct_type='RECREV'");
            sql.Append(" and PayrollGLAccounts.acct_no=stxchrtd.acct_no and  ");
            sql.Append("PayrollGLAccounts.keyvalue[9,10]=Master_Segment.keyvalue");
            sql.Append(" and PayrollGLAccounts.acct_cat !='U'");
            sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and stxchrtd.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and Master_Segment.segmentid=34");
            sql.Append(" group by PayrollGLAccounts.keyvalue[9,10],desc");
            sql.Append(" ORDER BY PayrollGLAccounts.keyvalue[9,10]");
                         
            return sql.ToString();
        }
        # endregion Constructure
    }
}
