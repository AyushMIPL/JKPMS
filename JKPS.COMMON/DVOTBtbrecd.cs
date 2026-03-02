using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : Rahul Jain
    /// Date : 28 oct 2009
    /// Description :common class for treasury bill  table tbrecd
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOTBtbrecd : DVOBase
    {
        private int _Rowid;
        private int _tbr_doc_no;
        private int _issue_num;
        private string _tend_code;
        private int _ar_cr_doc_no;
        private int _tbschid;
        private string _ok_to_post;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        private decimal _amt_rec;
        private decimal _amt_bal;
        private int _batch_id;

        #region Constructor
        public DVOTBtbrecd()
        {
            _Rowid = 0;
            _tbr_doc_no = 0;
            _issue_num = 0;
            _tend_code = string.Empty;
            _ar_cr_doc_no = 0;
            _tbschid = 0;
            _ok_to_post = string.Empty;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;

            _amt_bal = 0;
            _amt_rec = 0;
            _batch_id = 0;
        }
        #endregion

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int tbr_doc_no
        {
            get { return _tbr_doc_no; }
            set { _tbr_doc_no = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }
        public int ar_cr_doc_no
        {
            get { return _ar_cr_doc_no; }
            set { _ar_cr_doc_no = value; }
        }
        public int issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }
        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
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

        public decimal amt_rec
        {
            get { return _amt_rec; }
            set { _amt_rec = value; }
        }
        public decimal amt_bal
        {
            get { return _amt_bal; }
            set { _amt_bal = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usp_ins_tbrecd"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_tbrecd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_del_tbrecd"; }
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
            get { return "tbrecd"; }
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
        public string GET_RECEIVED_AMT
        {
            get { return "uspgetamtrec"; }
        }
        public string SET_ARDOCNO_TBRECD
        {
            get { return "usprecardoc"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbrecd.rowid,tbrecd.tbr_doc_no,tbrecd.tbschid,tbrecd.issue_num,tbrecd.tend_code,tbrecd.amt_rec, ");
            sql.Append("tbrecd.amt_bal,tbrecd.ar_cr_doc_no,tbrecd.ok_to_post  ");
            sql.Append(" from tbrecd where 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//rowid
                    sql.Append(" AND tbrecd.rowid =" + parameters[0]);
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//tbschid
                    sql.Append(" AND tbrecd.tbschid =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)//issue_num
                    sql.Append(" AND tbrecd.issue_num =" + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() != string.Empty)//tend_code
                    sql.Append(" AND tbrecd.tend_code =" + parameters[3].ToString());
            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND tbrecd.batch_id = " + parameters[4].ToString());
            sql.Append(" order by tbrecd.rowid ");
            return sql.ToString();
        }
        //Get data for TB receive Posting Query
        public string FIND_TBrecdDETAILS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct tbrecd.rowid,tbrecd.tbr_doc_no,tbrecd.issue_num,tbrecd.tend_code, ");
            sql.Append(" tbrecd.ar_cr_doc_no,tbrecd.tbschid,tbrecd.ok_to_post,tbrecd.amt_rec, ");
            sql.Append(" tbrecd.amt_bal,tbissued.amt_issued,tbissued.amt_per_100, ");
            sql.Append(" tbschemes.tbschname,tbclients.tend_name ");
            sql.Append(" from tbrecd,tbissued,tbschemes,tbclients where ");
            sql.Append(" tbissued.tend_code = tbrecd.tend_code ");
            sql.Append(" AND tbclients.tend_code = tbrecd.tend_code ");
            sql.Append(" AND tbissued.tbschid = tbrecd.tbschid ");
            sql.Append(" AND tbissued.issue_num = tbrecd.issue_num ");
            sql.Append(" AND tbrecd.tbschid =tbschemes.tbschid ");
            sql.Append(" AND tbissued.bill_status <> 'C' ");
            //sql.Append(" AND tbrecd.ok_to_post in('N','Y')");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbrecd.tbschid =" + parameters[0]);
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//issue_num
                    sql.Append(" AND tbrecd.issue_num =" + parameters[1]);
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0) //batch_id
                    sql.Append(" AND tbrecd.batch_id = " + parameters[2]);
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() != string.Empty) //check_post
                    if (parameters[3].ToString().Trim() == "POST")
                        sql.Append(" AND tbrecd.ok_to_post in('Y')");
                    else
                        sql.Append(" AND tbrecd.ok_to_post in('N','Y')");

            sql.Append(" order by tbrecd.tbr_doc_no,tbrecd.tend_code ");
            return sql.ToString();
        }

        //public string FIND_TBrecdDETAILS
        //{
        //    get { return "usptbrecddtlget"; }
        //}
        //update ar_cr_doc_no and ok_to_post
        public string UPDATE_TBRECD
        {
            get { return "usptbrecdupd"; }
        }
        #endregion store-procedures

    }
}
