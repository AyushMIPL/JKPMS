using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
 public   class DVOGLstxtranr :DVOBase
    {
        private string _orig_journal;
        private Int32 _doc_no;
        private Int32 _post_no;
        private DateTime _post_date;
        private DateTime _doc_date;
        private string _ref_code;
        private string _doc_desc;
        private string _user_id;

        private Int32 _Rowid;
     private int _count;

        #region Constructor
        public DVOGLstxtranr()
        {
            _orig_journal = string.Empty;
            _doc_no = 0;
            _post_no = 0;
            _post_date = Convert.ToDateTime(null);
            _doc_date = Convert.ToDateTime(null);
            _ref_code = string.Empty;
            _doc_desc = string.Empty;
            _user_id = string.Empty;
            _count = 0;
        }
        #endregion Constructor

        #region public properties
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
        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public int Rowid
        {
            get { return _Rowid ; }
            set { _Rowid  = value; }
        }
          public int count
          {
             get { return _count; }
             set { _count = value; }
          }

        #endregion public properties

        #region Stored-Procedures

    
        public override string INSERT_SPNAME
        {
            get { return "uspstxtranrins"; }
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
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string uspAccGrpsDtlget
        {
            get { return ""; }
        }

        public string GET_NEXT_DOC_NO
        {
            get { return "uspdocnoxtanr"; }
        }

        public string INSERT_STXTRANR
        {
         get { return "uspstxtranrIns"; }
        }

         public string COUNT_STXTRANR_RECORDS
         {
             get { return "uspstxtranrcouget"; }
         }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
         

            return sql.ToString();
        }

        #endregion store-procedures
    }
}
