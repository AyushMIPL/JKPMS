using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOsbvoidr : DVOBase
    {
        private int _rowid;
        private int _doc_no;// serial,
        private int _post_doc_no;//,int
        private int _tran_doc_no;
        private string _void_date;// date,
        private string _ok_to_post;// char(1),
        private int _insertby;// int,
        private string _insertdate;// date,
        private string _insertmachineinfo;// char(50),
        private int _updateby;// int,
        private string _updatedate;// date,
        private string _updatemachineinfo;//

        #region Constructor
        public DVOsbvoidr()
        {
            _rowid = 0;
            _doc_no = 0;
            _post_doc_no = 0;
            _tran_doc_no = 0;
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
        public int tran_doc_no
        {
            get { return _tran_doc_no; }
            set { _tran_doc_no = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int post_doc_no
        {
            get { return _post_doc_no; }
            set { _post_doc_no = value; }
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
            get { return "usp_ins_sbvoidr"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_sbvoidr"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_del_sbvoidr"; }
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
            get { return "sbvoidr"; }
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
        //Get Void transaction detail
        public string GET_SBVOIDR
        {
            get { return "uspsbvoidrget"; }
        }

        //update ok_to_post
        public string UPDATE_SBVOIDR
        {
            get { return "uspsbvoidrupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT sbvoidr.doc_no,sbvoidr.post_doc_no,sbvoidr.tran_doc_no,sbvoidr.ok_to_post,sbvoidr.void_date,sbvoidr.rowid");
            sql.Append(" FROM sbvoidr where 1=1 ");//AND sbvoidr.ok_to_post NOT IN ('P') ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  sbvoidr.post_doc_no=" + Convert.ToInt32(parameters[0].ToString()));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND  sbvoidr.doc_no=" + Convert.ToInt32(parameters[1].ToString()));
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sbvoidr.ok_to_post) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND void_date = '" + parameters[3].ToString().Trim() + "'");

            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND  sbvoidr.rowid=" + Convert.ToInt32(parameters[4].ToString()));

            sql.Append(" order by sbvoidr.doc_no");
            return sql.ToString();
        }
        public string FIND_DATA_SB_POSTTRANR(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT sbposttranr.postdoc_no,sbposttranr.acct_cat,sbposttranr.acct_no,sbposttranr.acct_status,sbposttranr.doc_no,sbposttranr.tran_type,sbposttranr.tarn_no,sbposttranr.doc_date , ");
            sql.Append(" sbposttranr.deposit_amt,sbposttranr.withdrawn_amt,sbposttranr.acct_balance,");
            sql.Append(" sbposttranr.operator,sbposttranr.acct_no ,sbposttranr.cash_received ,sbposttranr.aparrefno ");
            sql.Append(" FROM sbposttranr ");
            sql.Append(" WHERE 1=1  ");
            //sql.Append(" sbtranr.doc_no = sbvoidr.doc_no AND");
            //if (parameters[0] != null)
            //    if (Convert.ToInt32(parameters[0]) > 0)
            //        sql.Append(" AND  sbposttranr.postdoc_no=" + Convert.ToInt32(parameters[0].ToString()));

            //Modified by Sarvjeet[02/03/2010],here should be SB Transaction doc_no instead of postdoc_no..
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  sbposttranr.doc_no=" + Convert.ToInt32(parameters[0].ToString()));

            return sql.ToString();
        }

        public string CheckSbvoidr(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT count(*) from sbvoidr where ok_to_post <> 'C' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  sbvoidr.post_doc_no=" + Convert.ToInt32(parameters[0].ToString()));
            return sql.ToString();
        }

        #endregion Store Procedures
    }
}
