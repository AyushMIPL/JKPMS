using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOCapRevByMinistry:DVOBase
    {

         private string _period_month;
         private string _period_year;

       
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
        public DVOCapRevByMinistry()
        {
            _period_month = string.Empty;
            _period_year = string.Empty;
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
            get { return "uspCapRevByMinis"; }
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
           // sql.Append("select caprevbymins1.ministry ministry1,caprevbymins1.budgetedamt,allministry.ministry,");
           // sql.Append("  allministry.desc description, caprevbymins1.budgetedyear,caprevbymins2.ministry ministry2,");
           // sql.Append("caprevbymins2.description desc ,caprevbymins2.p_yeartodate,");
           // sql.Append("caprevbymins2.period_month,caprevbymins2.period_year");
           // sql.Append(" FROM  allministry,outer (caprevbymins2,caprevbymins1)");
           // sql.Append(" where allministry.ministry= caprevbymins2.ministry");
           // sql.Append(" and caprevbymins1.ministry=caprevbymins2.ministry");
           // sql.Append(" and caprevbymins1.budgetedyear='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
           // sql.Append(" and caprevbymins2.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
           // sql.Append("  and caprevbymins2.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
           // sql.Append(" and (period_year <>'' OR   p_yeartodate <>''  OR    period_month <>'')" );
           //sql.Append("  ORDER By  allministry.ministry");
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
sql.Append(" and PayrollGLAccounts.acct_type='CAPREV'");
sql.Append(" group by PayrollGLAccounts.keyvalue[1,2],Master_Segment.desc");

            return sql.ToString();
        
        }
        #endregion StoreProcedures
}
}
