using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOtblusrqueries : DVOBase
    {
        private int _rowid;
        private int _qid;
        private int _userid;
        private string _query;
        private string _qname;
        private string _category;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        public DVOtblusrqueries()
        {
            _rowid = 0;
            _qid = 0;
            _userid = 0;
            _query = string.Empty;
            _qname = string.Empty;
            _category = string.Empty;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;
        }

        #region Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int qid
        {
            get { return _qid; }
            set { _qid = value; }
        }
        public int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public string query
        {
            get { return _query; }
            set { _query = value; }
        }
        public string qname
        {
            get { return _qname; }
            set { _qname = value; }
        }
        public string category
        {
            get { return _category; }
            set { _category = value; }
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
            get { return "uspusrqueriesins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspusrqueriesupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspusrqueriesdel"; }
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
            get { return "tblusrqueries"; }
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
            sql.Append("SELECT qid,userid,query,qname,category,rowid ");
            sql.Append(" FROM tblusrqueries where 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  qid= " + Convert.ToInt32(parameters[0].ToString()));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND  userid=" + Convert.ToInt32(parameters[1].ToString()));
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(query) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(qname) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(category) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            
            sql.Append(" order by qid ");
            return sql.ToString();
        }
        
        #endregion Store Procedures
    }
}
