using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstgstder : DVOBase
    {
        private int _Rowid;
        private int _doc_no;
        private string _doc_desc;
        private string _doc_src;
        private string _eop_rev;
        private string _file_type;
        private string _post_type;

        public DVOstgstder()
        {
            _Rowid = 0;
            _doc_no = 0;
            _doc_desc = string.Empty;
            _doc_src = string.Empty;
            _eop_rev = string.Empty;
            _file_type = string.Empty;
            _post_type = string.Empty;
        }
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string doc_desc
        {
            get { return _doc_desc; }
            set { _doc_desc = value; }
        }
        public string doc_src
        {
            get { return _doc_src; }
            set { _doc_src = value; }
        }
        public string eop_rev
        {
            get { return _eop_rev; }
            set { _eop_rev = value; }
        }
        public string file_type
        {
            get { return _file_type; }
            set { _file_type = value; }
        }
        public string post_type
        {
            get { return _post_type; }
            set { _post_type = value; }
        }


        #region Stored Procedure
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
            get { return "stgstder"; }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            return sql.ToString();
        }
        //Added by Rahul Jain on 17/12/2009 Using in Pring Selected recurring Documents
        public string FIND_RECURRING_DOCS(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stgjourd.debit_credit, stgjourd.line_no,  stgstder.doc_no, ");
            sql.Append(" stgstder.post_type,  stgstder.doc_desc, stgstder.eop_rev, ");
            sql.Append(" stgstder.doc_src,  stgjourd.acct_no,  stgjourd.department,  ");
            sql.Append(" PayrollGLAccounts.acct_desc, stgjourd.amount ,PayrollGLAccounts.keyvalue  ");
            sql.Append(" from PayrollGLAccounts, stgstder, stgjourd   ");
            sql.Append(" where 1=1  ");
            sql.Append(" and stgstder.doc_no = stgjourd.doc_no  ");
            sql.Append(" and stgstder.file_type = stgjourd.orig_journal  ");
            sql.Append(" and  PayrollGLAccounts.acct_no = stgjourd.acct_no ");
            sql.Append(" order by stgstder.doc_no, stgjourd.line_no ");
            return sql.ToString();
        }
        #endregion
    }
}
