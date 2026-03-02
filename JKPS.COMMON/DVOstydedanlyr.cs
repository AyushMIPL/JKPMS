using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstydedanlyr :DVOBase
    {
        private int _rowid;
        private int _doc_no;
        private decimal _amount;
        private int _apdoc_no;
        private string _chk_name;
        private int _insertby;// int,
        private string _insertdate;// date,
        private string _insertmachineinfo;// char(50),
        private int _updateby;// int,
        private string _updatedate;// date,
        private string _updatemachineinfo;//

        #region Constructor
        public DVOstydedanlyr()
        {
            _rowid = 0;
            _doc_no = 0;
            _amount=0;
            _apdoc_no = 0;
            _chk_name = string.Empty;
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
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public int apdoc_no
        {
            get { return _apdoc_no; }
            set { _apdoc_no = value; }
        }
        public string chk_name
        {
            get { return _chk_name; }
            set { _chk_name = value; }
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
            get { return "uspdedanlyrins"; }//uspdedanlyrins
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }//usp_upd_sbvoidr
        }

        public override string DELETE_SPNAME
        {
            get { return ""; } //usp_del_sbvoidr
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
            get { return "stydedanlyr"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT sbvoidr.doc_no,sbvoidr.post_doc_no,sbvoidr.tran_doc_no,sbvoidr.ok_to_post,sbvoidr.void_date,sbvoidr.rowid");
            //sql.Append(" FROM sbvoidr where 1=1 ");//AND sbvoidr.ok_to_post NOT IN ('P') ");
            //if (parameters[0] != null)
            //    if (Convert.ToInt32(parameters[0]) > 0)
            //        sql.Append(" AND  sbvoidr.post_doc_no=" + Convert.ToInt32(parameters[0].ToString()));
            //if (parameters[1] != null)
            //    if (Convert.ToInt32(parameters[1]) > 0)
            //        sql.Append(" AND  sbvoidr.doc_no=" + Convert.ToInt32(parameters[1].ToString()));
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sbvoidr.ok_to_post) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND void_date(sbvoidr.doc_date) = date('" + parameters[3].ToString().Trim().Replace("'", "''") + "')");

            //sql.Append(" order by sbvoidr.doc_no");
            return sql.ToString();
        }
        
        #endregion Store Procedures
    }
}
