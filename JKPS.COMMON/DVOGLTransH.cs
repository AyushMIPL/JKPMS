using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
 public   class DVOGLTransH :DVOBase
    {
         private string _orig_journal;
        private int _doc_no;
        private string _acct_period;
          
        private string _acct_year;
        private string _status;        
       
        #region Constructor

        public DVOGLTransH()
        {
            _orig_journal=string.Empty;
            _doc_no=0;
            _acct_period=string.Empty;
            _acct_year=string.Empty;
            _status=string.Empty;        
        }

        #endregion Constructor

        #region Public Properties


     public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }

     public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }

     public string acct_period
        {
            get { return _acct_period; }
            set { _acct_period = value; }
        }

     public string acct_year
        {
            get { return _acct_year; }
            set { _acct_year = value; }
        }
     public string status
        {
            get { return _status; }
            set { _status = value; }
        }


        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspstgtranins"; }
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
       public string INSERT_STGTRANR
       {
         get { return "uspstgtranins"; }
       }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           
            return sql.ToString();
        }
     public string PY_STGTRANR_INS
     {
         get { return "usppygtranins"; }
     }
        #endregion Stored-Procedures

    }
}
