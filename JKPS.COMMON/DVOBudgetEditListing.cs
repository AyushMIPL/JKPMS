using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public   class DVOBudgetEditListing:DVOBase 
    {
      private string _keyvalue  ;
      private string _year;
      private string _set;
      private string _acct_type;
      private string _startingPeriod;
      private string _endingPeriod;
      private int _account;
      private decimal _amount;
      private int _doc_no;
      private string _oktopost;
      private int _line_no;
      public DVOBudgetEditListing()
      {
          _keyvalue = string.Empty;
          _year = string.Empty;
          _set = string.Empty;
          _acct_type = string.Empty;
          _startingPeriod = string.Empty;
          _endingPeriod = string.Empty;
          _account = 0;
          _amount = 0.0M;
          _doc_no = 0;
          _oktopost = string.Empty;
          _line_no = 0;
      }

      #region public properties
      public string keyvalue
      {
          get { return _keyvalue; }
          set { _keyvalue  = value; }
      }
      public int line_no
      {
          get { return _line_no; }
          set { _line_no = value; }
      }
      public string year
      {
          get { return _year ; }
          set { _year  = value; }
      }
      public string set
        {
            get { return _set ; }
            set { _set  = value; }
        }
      public string acct_type
      { 
          get { return _acct_type; }
          set { _acct_type = value; }
      }
      public string startingPeriod
      {
          get { return _startingPeriod; }
          set { _startingPeriod = value; }
      }
      public string endingPeriod
      {
          get { return _endingPeriod; }
          set { _endingPeriod = value; }
      }
      public int account
      {
          get { return _account; }
          set { _account = value; }
      }
      public decimal amount
      {
          get { return _amount; }
          set { _amount = value; }
      }
      public int doc_no
      {
          get { return _doc_no; }
          set { _doc_no = value; }
      }
      public string oktopost
      {
          get { return _oktopost; }
          set { _oktopost = value; }
      }
      #endregion properties


      #region Stored-Procedures

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
          get { return ""; }
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
      public string Getcountinbwh
      {
          get { return "uspcountinbwh"; }
      }
      public string INSERT_INBACTVD
      {
          get { return "uspinbactvdins"; }
      }
      public string UPDATEINBESTID
      { get { return "uspupdinbstid"; } }

      public  string GET_BUDGET_EDITLISTING_INFO
      {
          get { return "uspbelistinggetall"; }
      }
      public override string FIND_QUERY(ref Object[] parameters)
      {
           StringBuilder sql = new StringBuilder();
           sql.Append("select inbestid.rowid p_rowid,inbestid.year year, inbestid.set set,");
           sql.Append(" id p_id,inbestid.account p_account, inbestid.currestimate p_currestimate,");
           sql.Append(" inbestid.approved p_approved, inbestid.revised p_revised,");
           sql.Append(" inbestid.allocatedtodate p_allocatedtodate,inbestid.allocpercent");
           sql.Append(" p_allocpercent, inbestid.extra_funds p_extra_funds,");
           sql.Append(" PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc  from inbestid,");
           sql.Append(" PayrollGLAccounts where PayrollGLAccounts.acct_no = inbestid.account");
           sql.Append("  and acct_cat = 'U'");

            return sql.ToString();
      }

      #endregion Stored-Procedures

    }

}
