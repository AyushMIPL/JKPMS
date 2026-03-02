using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOExpnRevPreviousYearComp:DVOBase
    {
       private string _CurrentMonth;
        private string _CurrentYear;
        private string _PreviousYear;


       #region Constructure
       public DVOExpnRevPreviousYearComp()
       {
           _CurrentMonth = string.Empty;
           _CurrentYear = string.Empty;
           _PreviousYear = string.Empty;
       }

       #endregion Constructure
       
        # region Properties

        public string CurrentMonth
        {
            get { return _CurrentMonth; }
            set { _CurrentMonth = value; }
        }
        public string CurrentYear
        {
            get { return _CurrentYear; }
            set { _CurrentYear = value; }

        }
       public string PreviousYear
       {
        get {return _PreviousYear;}
        set{_PreviousYear=value;}
       }
        # endregion Properties
        
        #region StoreProcedure
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
            get { return "uspExpNRevPreYearComp";}
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
           sql.Append(" select  PayrollGLAccounts.acct_type, sum(balance) p_yeartodate,");
           sql.Append(" sum(activity) p_current,stxchrtd.period_year");    
           sql.Append(" from   stxchrtd,PayrollGLAccounts  where ");
           sql.Append(" stxchrtd.acct_no=PayrollGLAccounts.acct_no");
           sql.Append(" and stxchrtd.period_month ='05'");
           sql.Append(" and PayrollGLAccounts.acct_type!='BELLIN'");
           sql.Append(" group by PayrollGLAccounts.acct_type,stxchrtd.period_year");
           return sql.ToString();
        }
        #endregion StoreProcedure
    }
}
