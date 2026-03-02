using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOInventorystitranr : DVOBase
    {
        private int _rowid;
        private string _orig_journal;
        private int _doc_no;
        private string _doc_type;
        private string _ref_no;

        #region Constructor
        public DVOInventorystitranr()
        {
            _rowid = 0;
            _orig_journal = string.Empty;
            _doc_no = 0;
            _doc_type = string.Empty;
            _ref_no = string.Empty;

        }
        #endregion Constructor

        #region Public Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

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

        public string doc_type
        {
            get { return _doc_type; }
            set { _doc_type = value; }
        }

        public string ref_no
        {
            get { return _ref_no; }
            set { _ref_no = value; }
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
            get { return "stitranr"; }
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
        public string INSERT_STITRANR
        {
            get { return "uspi_tran"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }

        #endregion Stored-Procedures

    }
}
