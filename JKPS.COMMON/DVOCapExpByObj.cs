using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOCapExpByObj:DVOBase
    {
        private string _period_month;
        private string _period_year;
        #region Constructure

        public DVOCapExpByObj()
        {
            _period_month = string.Empty;
            _period_year = string.Empty;
        }
        #endregion Constructure

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
   
        #region StoreProcedures
        public override string INSERT_SPNAME
        {
            get {return   ""; }
        }

        public override string UPDATE_SPNAME
        {
            get {return   ""; }
        }

        public override string DELETE_SPNAME
        {
            get {return  ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspCapExpByObj"; }
        }

        public override string ALL_SPNAME
        {
            get {return   ""; }
        }

        public override string TABLE_NAME
        {
            get { return   ""; }
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
       public string GET_EXP_REV_ACT
       {
           get { return "uspExpRevSumGet"; }
       }
       public string GET_BANKACCTBAL
       {
           get { return "uspBankAcctBal"; }
       }
       public string GET_Est_Amt
       {
           get { return "uspEstAmtGet"; }
       }
       public string GET_EST_CAPEXP_BY_OBJECT_CODE
       {
           get { return "uspestcapexpoc"; }
       }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.keyvalue[14,15] p_objcode,desc p_Desc ,sum(balance) p_yeartodate,");
            sql.Append("sum(this_month) p_this_month , sum(activity) P_Activity");
            sql.Append(" FROM PayrollGLAccounts,stxchrtd,Master_Segment where acct_type='RECEXP'");
            sql.Append(" and PayrollGLAccounts.acct_no=stxchrtd.acct_no and  ");
            sql.Append(" PayrollGLAccounts.keyvalue[14,15]=Master_Segment.keyvalue");
            sql.Append(" and PayrollGLAccounts.acct_cat !='U'");
            sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and stxchrtd.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and Master_Segment.segmentid=26");
            sql.Append(" group by PayrollGLAccounts.keyvalue[14,15],desc");
            
            sql.Append(" UNION");

            sql.Append("  select PayrollGLAccounts.keyvalue[14,15] p_objcode,desc p_Desc ,sum(balance) p_yeartodate,");
            sql.Append("sum(this_month) p_this_month , sum(activity) P_Activity");
            sql.Append(" FROM PayrollGLAccounts,stxchrtd,Master_Segment where acct_type='CAPEXP'");
            sql.Append(" and PayrollGLAccounts.acct_no=stxchrtd.acct_no and  ");
            sql.Append("PayrollGLAccounts.keyvalue[14,15]=Master_Segment.keyvalue");
            sql.Append(" and PayrollGLAccounts.acct_cat !='U'");
            sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and stxchrtd.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and Master_Segment.segmentid=32");
            sql.Append(" group by PayrollGLAccounts.keyvalue[14,15],desc");
            
        

            return sql.ToString();
        }
        #endregion  StoreProcedures
    }
}
