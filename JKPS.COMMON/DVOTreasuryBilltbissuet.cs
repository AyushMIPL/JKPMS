using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTreasuryBilltbissuet : DVOBase
    {
        private int _RowId;
        private int _tbschid;
        private int _issue_num;
        private string _tend_code;
        private decimal _amt_issued;
        private string _tend_status;
        private decimal _amt_payout;
        private decimal _amt_nxt_rollover;
        private decimal _amt_per_100;
        private string _ok_to_post;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        #region Constructor
        public DVOTreasuryBilltbissuet()
        {
            _RowId = 0;                             
            _tbschid=0;
            _issue_num=0;
            _tend_code = string.Empty;
            _amt_issued = 0;
            _tend_status = string.Empty;
            _amt_payout = 0;
            _amt_nxt_rollover = 0;
            _amt_per_100 = 0;
            _ok_to_post = string.Empty;
            _insertby=0;
            _insertdate=string.Empty;
            _insertmachineinfo=string.Empty;
            _updateby=0;
            _updatedate=string.Empty;
            _updatemachineinfo=string.Empty;
        }
        #endregion Constructor

        #region public properties
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
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
        public decimal amt_issued
        {
            get { return _amt_issued; }
            set { _amt_issued = value; }
        }
        public string tend_status
        {
            get { return _tend_status; }
            set { _tend_status = value; }
        }
        public decimal amt_nxt_rollover
        {
            get { return _amt_nxt_rollover; }
            set { _amt_nxt_rollover = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public decimal amt_payout
        {
            get { return _amt_payout; }
            set { _amt_payout = value; }
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
        #endregion public properties


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usp_ins_tbissuet"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_tbissuet"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_del_tbissuet"; }
        }

        public override string FIND_SPNAME
        {
            get { return " "; }
        }
        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "tbissuet"; }
        }
        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string FIND_TBISSUET
        {
            get { return "usptbissetget"; }
        }
        //Written by Sarvjeet On 19/11/2009 , Use to update ok_to_post into tbisset as 'P'
        public string UPDATE_ISSUET
        {
            get { return "usptbissuetupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuet.rowid,tbissuet.tbschid,tbissuet.issue_num,tbissuet.tend_code,tbissuet.tend_status,tbissuet.amt_issued, ");
            sql.Append("tbissuet.amt_payout,tbissuet.amt_nxt_rollover,tbissuet.ok_to_post,tbissuet.amt_per_100  ");
            sql.Append(" from tbissuet where 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//rowid
                    sql.Append(" AND tbissuet.rowid =" + parameters[0]);
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//tbschid
                    sql.Append(" AND tbissuet.tbschid =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)//issue_num
                    sql.Append(" AND tbissuet.issue_num =" + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() !=string.Empty)//tend_code
                    sql.Append(" AND tbissuet.tend_code =" + parameters[3].ToString());
            return sql.ToString();
        }
        public string FIND_DATA_TBISSUET(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuet.rowid,tbissuet.tbschid,tbissuet.issue_num,tbissuet.tend_code,tbissuet.tend_status,tbissuet.amt_issued, ");
            sql.Append("tbissuet.amt_payout,tbissuet.amt_nxt_rollover,tbissuet.ok_to_post,tbissuet.amt_per_100  ");
            sql.Append(" from tbissuet where ok_to_post='N' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbissuet.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//issue_num
                    sql.Append(" AND tbissuet.issue_num =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty)//tend_code
                    sql.Append(" AND tbissuet.tend_code =" + parameters[2].ToString());
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
