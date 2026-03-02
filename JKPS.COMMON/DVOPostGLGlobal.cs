using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPostGLGlobal :DVOBase
    {
        private string _gl_installed;
        private Int32 _next_doc_no;
        private Int32 _status;
        private string _description;
        private string _acct_year;
        private string _acct_desc;
        private string _incr_with_crdt;
        private decimal _signed_amount;
        private decimal _cr_accum;
        private decimal _db_accum;
        private string _pstmonth;
        private string _pstyear;
        private int _sql_error;
     #region Constructor
       public DVOPostGLGlobal()
      {
          _gl_installed = string.Empty;
          _next_doc_no =0;
          _status = 0;
          _description = string.Empty;
          _acct_year = string.Empty;
          _acct_desc = string.Empty;
          _incr_with_crdt = string.Empty;
          _signed_amount = 0.0M;
          _cr_accum = 0.0M;
          _db_accum = 0.0M;
          _pstmonth = "00";
          _pstyear = "1900";
          _sql_error = 0;
      }
      #endregion Constructor
      #region public properties
      public string gl_installed
      {
          get { return gl_installed; }
          set { _gl_installed = value; }
      }

      public Int32 next_doc_no
      {
          get { return _next_doc_no; }
          set { _next_doc_no = value; }
      }
      public Int32 status
      {
          get { return _status; }
          set { _status = value; }
      }

      public string description
      {
          get { return _description; }
          set { _description = value; }
      }
      public string acct_year
      {
          get { return _acct_year; }
          set { _acct_year = value; }
      }
      public string acct_desc
      {
          get { return _acct_desc; }
          set { _acct_desc = value; }
      }
      public string incr_with_crdt
      {
          get { return _incr_with_crdt; }
          set { _incr_with_crdt = value; }
      }
      public decimal signed_amount
      {
          get { return _signed_amount; }
          set { _signed_amount = value; }
      }
      public decimal cr_accum
      {
          get { return _cr_accum; }
          set { _cr_accum = value; }
      }
      public decimal db_accum
      {
          get { return _db_accum; }
          set { _db_accum = value; }
      }
      public string pstmonth
      {
          get { return _pstmonth; }
          set { _pstmonth = value; }
      }
      public string pstyear
      {
          get { return _pstyear; }
          set { _pstyear = value; }
      }
       public int sql_error
       {
           get { return _sql_error; }
           set { _sql_error = value; }
       }
      #endregion

      #region Stored-Procedures

      //public string AUTHENTICATION_SPNAME
      //{
      //    get { return "uspsecauthenticate"; }
      //}

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
      public string POSTGL
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


      public override string FIND_QUERY(ref Object[] parameters)
      {
          System.Text.StringBuilder sql = new StringBuilder();

          return sql.ToString();
      }
      #endregion store-procedures
  }
}
