using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOsbvoidclsacct : DVOBase
    {
        private int _rowid;
        private int _doc_no;// serial,
        private string _acct_id;
        private string _acct_cat;
        private int _acct_no;
        private string _void_date;
        private string _ok_to_post;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        #region Constructor
        public DVOsbvoidclsacct()
        {
            _rowid = 0;
            _doc_no = 0;
            _acct_id = string.Empty;
            _acct_cat = string.Empty;
            _acct_no = 0;
            _void_date = string.Empty;
            _ok_to_post = string.Empty;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;
        }
        #endregion Constructor

        #region Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string acct_id
        {
            get { return _acct_id; }
            set { _acct_id = value; }
        }
        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string void_date
        {
            get { return _void_date; }
            set { _void_date = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }

        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }

        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
        #endregion Properties

        #region Store Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspsbvdclsacctins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsbvdclsacctupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspsbvdclsacctdel"; }
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
            get { return "sbvoidclsacct"; }
        }

        public override int UNIQUE_ID
        {
            get { return rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //Get Void Close Account transaction detail
        public string GET_SBVOIDCLOSEACCTR
        {
            get { return "uspsbvoidclsacct"; }
        }
        //Update sbclientsr account status
        public string UPD_SBCLIENTSR
        {
            get { return "uspsbacctstaupd"; }
        }
        //update sbtranr
        public string UPD_SBTRANR
        {
            get { return "uspsbtranrupd"; }
        }
        //update ok_to_post
        public string UPDATE_SBVOIDCLSACCT
        {
            get { return "uspsbvoidclsupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbvoidclsacct.doc_no,sbvoidclsacct.acct_id,sbvoidclsacct.acct_cat,sbvoidclsacct.acct_no,sbvoidclsacct.ok_to_post,sbvoidclsacct.void_date,sbvoidclsacct.rowid ");
            sql.Append(" FROM sbvoidclsacct where 1=1 and sbvoidclsacct.ok_to_post NOT IN ('P')");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  sbvoidclsacct.acct_id='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND  sbvoidclsacct.doc_no=" + Convert.ToInt32(parameters[1].ToString()));
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sbvoidclsacct.ok_to_post) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND void_date(sbvoidclsacct.doc_date) = date('" + parameters[3].ToString().Trim().Replace("'", "''") + "')");

            sql.Append(" order by sbvoidclsacct.doc_no");
            return sql.ToString();
        }
        public string FIND_DATA_SB_POSTTRANR(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbposttranr.postdoc_no,sbposttranr.acct_id, ");
            sql.Append(" sbposttranr.acct_cat,sbposttranr.acct_no,sbposttranr.acct_status, ");
            sql.Append(" sbposttranr.doc_no,sbposttranr.tran_type,sbposttranr.tarn_no,sbposttranr.doc_date , ");
            sql.Append(" sbposttranr.deposit_amt,sbposttranr.withdrawn_amt,sbposttranr.acct_balance,");
            sql.Append(" sbposttranr.operator,sbposttranr.aparrefno,sbposttranr.rowid ");
            sql.Append(" FROM sbposttranr ");
            sql.Append(" WHERE 1=1 AND sbposttranr.acct_status='CLOSED' ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  sbposttranr.acct_id='" + parameters[0].ToString().Replace("'", "''") + "'");
            sql.Append(" ORDER BY sbposttranr.rowid desc ");
            return sql.ToString();
        }

        #endregion Store Procedures
    }
}
