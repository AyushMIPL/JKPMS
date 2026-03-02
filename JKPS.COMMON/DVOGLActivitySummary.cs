using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOGLActivitySummary:DVOBase
    {
        private string _acct_type;
        private string _StartingMonth;
        private string _EndingMonth;
        private string _StartingYear;
        private string _EndingYear;
        private string _keyvalue;
         private string _orig_joun;
        #region Constructor
        public DVOGLActivitySummary()
        {
            _acct_type = string.Empty;
            _EndingMonth = string.Empty;
            _EndingYear = string.Empty;
            _keyvalue = string.Empty;
            _StartingMonth = string.Empty;
            _StartingYear = string.Empty;
            _orig_joun = string.Empty;
        }
        #endregion Constructor

        #region Properties
        public string AccountType
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string StartingYear
        {
            get { return _StartingYear; }
            set { _StartingYear = value; }
        }
        public string EndingYear
        {
            get { return _EndingYear; }
            set { _EndingYear = value; }
        }
        public string StartingMonth
        {
            get { return _StartingMonth; }
            set { _StartingMonth = value; }
        }
        public string EndingMonth
        {
            get { return _EndingMonth; }
            set { _EndingMonth = value; }
        }

        public string Keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
       public string orig_journal
       {
           get { return _orig_joun; }
           set { _orig_joun = value; }
       }
        #endregion Properties

        #region StoreProcedure
        public override string INSERT_SPNAME
        {
            get { return ""; }
        }
        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }
        public override string DELETE_SPNAME
        {
            get { return ""; }
        }
        public override string FIND_SPNAME
        {
            get { return "uspGLActivitySummary"; }
        }
        public override string ALL_SPNAME
        {
            get { return ""; }
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

            sql.Append("select Flex_struct_Header.accounttype accounttype,Flex_struct_Header.desc desc,PayrollGLAccounts.keyvalue,");
            sql.Append("PayrollGLAccounts.acct_desc keyvalueDesc, PayrollGLAccounts.acct_cat, PayrollGLAccounts.incr_with_crdt, stgactvd.amount,");
            sql.Append("stgactvd.debit_credit, stgtranr.acct_period,stgtranr.acct_year,PayrollGLAccounts.subtotal_group SubTotal, ");
            sql.Append(" stxtranr.post_date,s.acct_desc SubTotalDesc "); //Added By Rahul jain on 04-Jan-2010
            sql.Append(" from  PayrollGLAccounts,Flex_struct_Header, stxtranr, stgtranr, stgactvd, outer PayrollGLAccounts s ");
            sql.Append(" where stgtranr.status in ('N','P')");        
             if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//Acct_type
                 sql.Append(" and PayrollGLAccounts.acct_type='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
             if (parameters[6].ToString().Trim() != string.Empty && parameters[6].ToString() != null)//Acct_type
                 sql.Append(" and stxtranr.orig_journal='" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
             if (parameters[3].ToString() != parameters[5].ToString())
             {
                 if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//Acct_No
                 {
                     parameters[1] = parameters[1].ToString().Trim().Replace("*", "");
                     sql.Append("  and PayrollGLAccounts.keyvalue LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%" + "'");
                 }
                 if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//MonthFrom
                     sql.Append(" and stgtranr.acct_period >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                 if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//YearFrom
                     sql.Append(" and stgtranr.acct_year >= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
                 if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//MonthFrom
                     sql.Append(" and stgtranr.acct_period <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
                 if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//YearFrom
                     sql.Append(" and stgtranr.acct_year <= '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
             }
             else
             {
                 if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//YearFrom
                     sql.Append(" and stgtranr.acct_year = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
                 if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//Acct_No
                     sql.Append("  and PayrollGLAccounts.keyvalue LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                 if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//MonthFrom
                     sql.Append(" and stgtranr.acct_period >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                 if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//MonthFrom
                     sql.Append(" and stgtranr.acct_period <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
                 
             } 
             sql.Append(" and PayrollGLAccounts.acct_no = stgactvd.acct_no ");
             sql.Append(" and Flex_struct_Header.accounttype = PayrollGLAccounts.acct_type   ");
             sql.Append(" and s.keyvalue=PayrollGLAccounts.subtotal_group");
             sql.Append(" and stxtranr.doc_no = stgtranr.doc_no ");
             sql.Append(" and stxtranr.orig_journal = stgtranr.orig_journal ");
             sql.Append(" and stgtranr.doc_no = stgactvd.doc_no ");
             sql.Append(" and stgtranr.orig_journal = stgactvd.orig_journal");
          
            


            return sql.ToString();
        }

       public string FIND_ACT(ref object[] parameters)
       { 
            StringBuilder sql = new StringBuilder();

            sql.Append("select sum(stgactvd.amount), stgactvd.acct_no, stgactvd.debit_credit");
            sql.Append(" from stxtranr, stgtranr, stgactvd ");
            sql.Append(" where stgtranr.status in ('P')");  
          //  sql.Append(" and PayrollGLAccounts.acct_no = stgactvd.acct_no and ");
            sql.Append(" and  stxtranr.doc_no = stgtranr.doc_no and ");
            sql.Append(" stxtranr.orig_journal = stgtranr.orig_journal and ");
            sql.Append(" stgtranr.doc_no = stgactvd.doc_no and ");
            sql.Append(" stgtranr.orig_journal = stgactvd.orig_journal ");
            sql.Append(" and stgtranr.acct_period='" + parameters[0].ToString()+"'");
            sql.Append(" and  stgtranr.acct_year='" + parameters[1].ToString()+"'");
            sql.Append(" group by stgactvd.acct_no, stgactvd.debit_credit");

            return sql.ToString();
       }
       public string LoadStxchrtd(ref object[] parameters)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append(" select PayrollGLAccounts.acct_no,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_cat,PayrollGLAccounts.incr_with_crdt,activity,balance,this_month ");
           sql.Append(" from PayrollGLAccounts,stxchrtd where PayrollGLAccounts.acct_no=stxchrtd.acct_no ");
           sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString() + "'");
           sql.Append(" and  stxchrtd.period_year='" + parameters[1].ToString() + "'");
           return sql.ToString();
       }


        #endregion StoreProcedure
    }
}
