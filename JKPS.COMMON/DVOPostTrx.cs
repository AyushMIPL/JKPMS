using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPostTrx :DVOBase
    {
        private string _post_or_check;
        private string _orig_journal;
        private Int32 _doc_no;
        private Int32 _post_no;
        private  DateTime _post_date;
        private  DateTime _doc_date; 
        private string _ref_code;
        private  string _doc_desc;
        


      #region Constructor
        public DVOPostTrx()
      {
          _post_or_check = string.Empty;
          _orig_journal = string.Empty;
          _doc_no = 0;
          _post_no = 0;
          _post_date = DateTime.Now;
          _doc_date = DateTime.Now;
          _ref_code = string.Empty;
          _doc_desc = string.Empty;
      }
      #endregion Constructor

      #region public properties
        public string post_or_check
      {
          get { return _post_or_check; }
          set { _post_or_check = value; }
      }

        public string orig_journal
      {
          get { return _orig_journal; }
          set { _orig_journal = value; }
      }
        public Int32 doc_no
      {
          get { return _doc_no; }
          set { _doc_no = value; }
      }

        public Int32 post_no
      {
          get { return _post_no; }
          set { _post_no = value; }
      }
        public DateTime post_date
      {
          get { return _post_date; }
          set { _post_date = value; }
      }
        public DateTime doc_date
      {
          get { return _doc_date; }
          set { _doc_date = value; }
      }
        public string ref_code
      {
          get { return _ref_code; }
          set { _ref_code = value; }
      }
        public string doc_desc
        {
            get { return _doc_desc; }
            set { _doc_desc = value; }
        }
      #endregion public properties


      #region Stored-Procedures

      //public string AUTHENTICATION_SPNAME
      //{
      //    get { return "uspsecauthenticate"; }
      //}

      public override string INSERT_SPNAME
      {
          get { return "uspglposttrx"; }
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
      //Written by Sarvjeet On 15/05/2008 
      //
      public string GET_XTRANRDOCNO
      {
          get { return "uspxtranrchk"; }
      }
       public string GET_GTRANRDOCNO
       {
           get { return "uspgtranrchk"; }
       }
      public override string FIND_QUERY(ref Object[] parameters)
      {
          System.Text.StringBuilder sql = new StringBuilder();
   
          return sql.ToString();
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

       public string PY_TRX
       {
           get { return "usppytrxins"; }
       }
       
      #endregion store-procedures

    }
}
