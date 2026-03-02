using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLUpdateAccountSubGroups:DVOBase
    {
        private string _acct_type;
        private string _keyvalue;
        private string _acct_desc;
        private string _subtotal_group;
        private long _acct_no;
        private int _rowid;
        
        #region Constructor

        public DVOGLUpdateAccountSubGroups()
        {
            _acct_type = string.Empty;
            _keyvalue = string.Empty;
            _acct_desc = string.Empty;
            _subtotal_group = string.Empty;
            _acct_no = 0;
            _rowid=0;
        }

        #endregion Constructor

        #region Public Properties

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }

        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }

        public string subtotal_group
        {
            get { return _subtotal_group; }
            set { _subtotal_group = value; }
        }

        public long acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public  int rowid
        {
            get{return _rowid;}
            set {_rowid=value;}
        }

        

        #endregion Public Properties

        #region Stored-Procedures

      
        public override string INSERT_SPNAME
        {
            get { return "uspglacctsubgpins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspglacctsubgpupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspglacctsubgpdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspglacctsubgpget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspglacctsubgpall"; }
        }

        public string CHK_ACCOUNT_BEFORE_INSERT
        {
            get { return "uspglacctsubgpchk"; }
        }
        public override string TABLE_NAME
        {
            get { return "PayrollGLAccounts"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {

            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT acct_type,keyvalue,acct_desc,subtotal_group,acct_no,rowid from PayrollGLAccounts");
            sql.Append(" WHERE 1 = 1 ");
            if (parameters[0].ToString() != string.Empty && parameters[0] != null)
                sql.Append(" AND Rtrim(acct_type) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[0] != null)
                sql.Append(" AND Rtrim(keyvalue) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2].ToString() != string.Empty && parameters[0] != null)
                sql.Append(" AND Rtrim(acct_desc)  LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3].ToString() != string.Empty && parameters[0] != null)
                sql.Append(" AND Rtrim(subtotal_group) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            //****Added by Sunil Pahwa on 24/01/09 
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND rowid = " + parameters[4].ToString());
            //*************************************

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}

   
