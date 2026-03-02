using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
 public  class DVOAccountsPayableJournal:DVOBase 
    {
     //private string _doc_date;
     private string _startDate;
     private string _EndDate;

     public DVOAccountsPayableJournal()
     {
         _startDate = "1/1/1900";
          _EndDate = "1/1/2100";

     }

     public string  startDate
     {
         get { return _startDate; }
         set { _startDate = value; }
     }
     public string  EndDate
     {
         get { return _EndDate; }
         set { _EndDate = value; }
     }




        #region Stored-Procedures

        //**********************Added by sanjay***********************
        public string GET_CASH_DIS_JOURNAL
        {
            get { return "uspcashdisjournal"; }
        }   
       //***************************************************************
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
            get { return "uspAcPayJournalGet"; }
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


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stxtranr.orig_journal,stxtranr.doc_no,stxtranr.post_no, ");
            sql.Append(" stxtranr.post_date,stxtranr.doc_date,stxtranr.ref_code,stxtranr.doc_desc,  ");
            sql.Append(" stxtranr.user_id,stgactvd.orig_journal ,stgactvd.doc_no ,stgactvd.acct_no, ");
            sql.Append(" stgactvd.department,stgactvd.amount,stgactvd.debit_credit,stpvendr.vend_code, ");
            sql.Append(" stpvendr.bus_name,stptranr.orig_journal,stptranr.doc_no,");

            sql.Append(" stptranr.inv_chk_no,stptranr.doc_type,PayrollGLAccounts.acct_no,  ");
            sql.Append(" PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_cat, ");
            sql.Append(" PayrollGLAccounts.processing_seq,PayrollGLAccounts.incr_with_crdt,PayrollGLAccounts.subtotal_group, ");

            sql.Append(" PayrollGLAccounts.keyvalue from stxtranr,stgactvd,stpvendr,outer stptranr, ");
            sql.Append(" outer PayrollGLAccounts where  stxtranr.doc_no=stgactvd.doc_no and ");
            sql.Append(" stxtranr.orig_journal=stgactvd.orig_journal and ");
            sql.Append(" stptranr.doc_no=stgactvd.doc_no and ");
            sql.Append(" stptranr.orig_journal=stgactvd.orig_journal and ");

            sql.Append(" stptranr.orig_journal=stgactvd.orig_journal and ");
            sql.Append(" stpvendr.vend_code=stxtranr.ref_code and  ");
            sql.Append(" stgactvd.acct_no=PayrollGLAccounts.acct_no and ");
            sql.Append(" stxtranr.orig_journal in('AP','PU') ");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//
                sql.Append(" and doc_date>='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//
                sql.Append(" and doc_date<='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            
            sql.Append(" order by stxtranr.doc_no,stgactvd.acct_no ");


            return sql.ToString();
        }

        #endregion Stored-Procedures


    }
}
