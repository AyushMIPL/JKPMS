using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOCapExpByMinistryAndObject:DVOBase
    {
         private string _period_month;
         private string _period_year;
         private string _ministry;
            
        # region Properties

        public string month
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
        public DVOCapExpByMinistryAndObject()
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
            get { return "uspCapExpByMinisAndObj"; }
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
        public string GET_CAPREV
        {
            get { return "uspcaprevget"; }
        }
        public string GET_RECEXP
        {
            get { return "usprecexpget"; }
        }
        public string GET_RECREV
        {
            get { return "usprecrevget"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
                StringBuilder sql = new StringBuilder(); 
                sql.Append("select PayrollGLAccounts.keyvalue[1,2] ministry ,PayrollGLAccounts.keyvalue[14,15] objCode,");
                sql.Append(" Master_Segment.desc p_desc,balance p_yeartodate, inbestid.revised  budgetedamt,inbestid.year");      
                sql.Append(" from PayrollGLAccounts,outer stxchrtd,Master_Segment,outer inbestid");
                sql.Append(" where PayrollGLAccounts.acct_no=stxchrtd.acct_no");
                sql.Append(" and Master_Segment.keyvalue=PayrollGLAccounts.keyvalue[14,15]");
                sql.Append(" and PayrollGLAccounts.acct_type='CAPEXP'");
                sql.Append(" and Master_Segment.segmentid=32");
                sql.Append(" and inbestid.account=PayrollGLAccounts.acct_no");
                sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" and inbestid.year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" and stxchrtd.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" and  PayrollGLAccounts.keyvalue[1,2]='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                
                return sql.ToString();          
        }
        
      

   #endregion   StoreProcedures
    }
}
