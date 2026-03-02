using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOInvTransactionStitrane:DVOBase
    {
        #region Private Members
        private int   _doc_no;
        private DateTime _doc_date ;
        private string  _tran_no   ;
        private string  _tran_desc;
        private string _ok_post;
        #endregion


        #region Constructor
        public DVOInvTransactionStitrane()
        {

            _doc_no = 0;
            _doc_date = Convert.ToDateTime(null);
            _tran_no=string.Empty;
            _tran_desc=string.Empty;
            _ok_post=string.Empty;

        }
        #endregion

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
        public string tran_no
        {
            get { return _tran_no; }
            set { value = _tran_no; }
        }
        public string tran_desc
        {
            get { return _tran_desc; }
            set { _tran_desc = value; }
        }
        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }

        #endregion

        #region Store Procedures
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
            get { throw new Exception("The method or operation is not implemented."); }
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
        public string GET_INV_TRENSF
        {
            get { return "uspinvtransfget"; }
        }
       public string UPDATE_STITRANE
       {
           get { return "uspstitraneupd"; }
       }
       public string GET_whse_dept_code
       {
           get { return "usp_whsedeptcode"; }
       }
        #endregion
    }
}
