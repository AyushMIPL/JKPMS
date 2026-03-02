using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public  class DVOInvAdjStiadjme:DVOBase
    {

        private int _doc_no  ;
        private DateTime _doc_date ;
        private string  _adj_no;
        private string  _adj_desc;
        private int _adj_acct;
        private string _ok_post;
        
        public DVOInvAdjStiadjme()
        {
            _doc_no = 0;
            _doc_date = Convert.ToDateTime(null);
            _adj_no = string.Empty;
            _adj_desc = string.Empty;
            _adj_acct = 0;
            _ok_post = string.Empty;

        }

        #region Properties

        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string adj_no
        {
            get { return _adj_no; }
            set { _adj_no = value; }
        }
        public string adj_desc
        {
            get { return _adj_desc; }
            set { _adj_desc = value; }
        }
        public int adj_acct
        {
            get { return _adj_acct; }
            set { _adj_acct = value; }
        }
        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }
        #endregion

        #region Procedures
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string TABLE_NAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override int UNIQUE_ID
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public string GET_POSTADJ
        {
            get { return "uspadjustmentget"; }
        }
        public string UPD_ADJME
        {
            get { return "uspstiadjmeupd"; }
        }
        #endregion Procedures
    }
}
