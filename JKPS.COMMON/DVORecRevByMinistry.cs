using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVORecRevByMinistry:DVOBase
    {

        private string _period_month;
        private string _period_year;

    public DVORecRevByMinistry()
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
            get { return "uspRccRevByMinistry"; }
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
            //sql.Append("select recrevbymins1.ministry ministry1 ,recrevbymins1.budgetedamt,allministry.ministry,");
            //sql.Append("allministry.desc description, recrevbymins1.budgetedyear,recrevbymins2.ministry ministry2,");
            //sql.Append("recrevbymins2.description desc,recrevbymins2.p_yeartodate,");
            //sql.Append("recrevbymins2.period_month,recrevbymins2.period_year");
            //sql.Append(" FROM allministry, outer (recrevbymins2,recrevbymins1)");
            //sql.Append(" where allministry.ministry= recrevbymins2.ministry");
            //sql.Append(" and recrevbymins1.ministry=recrevbymins2.ministry");
            //sql.Append(" and recrevbymins1.budgetedyear='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and recrevbymins2.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and recrevbymins2.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            
            //sql.Append(" ORDER By allministry.ministry");
            //return sql.ToString();
            sql.Append("select PayrollGLAccounts.keyvalue[1,2] ministry,");
            sql.Append(" Master_Segment.desc description , ");
            sql.Append(" sum(balance) p_yeartodate ,sum(revised) budgetedamt ");
            sql.Append(" from PayrollGLAccounts, outer stxchrtd, outer inbestid,Master_Segment ");
            sql.Append(" where PayrollGLAccounts.acct_no =stxchrtd.acct_no");
            sql.Append(" and PayrollGLAccounts.acct_no =account and ");
            sql.Append(" year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and segmentid=23");
            sql.Append(" and PayrollGLAccounts.keyvalue[1,2] =Master_Segment.keyvalue");

            sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "' and stxchrtd.period_year ='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and PayrollGLAccounts.acct_type='RECREV'");
            sql.Append(" group by PayrollGLAccounts.keyvalue[1,2],Master_Segment.desc");
            return sql.ToString();
        }
        # endregion Constructure
    }
}
