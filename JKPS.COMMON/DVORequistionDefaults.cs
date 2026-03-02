using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVORequistionDefaults : DVOBase
    {
        private int _rowid;
        private int _min_days_req;// min_days_req
        private string _comm_level;//comm_level
        private int _min_dept;//min_dept
        private string _add_nonstk_itm;// add_nonstk_itm,
        private int _insertby;// int,
        private string _insertdate;// date,
        private string _insertmachineinfo;// char(50),
        private int _updateby;// int,
        private string _updatedate;// date,
        private string _updatemachineinfo;//

        #region Constructor
        public DVORequistionDefaults()
        {
            _rowid = 0;
            _min_days_req = 0;
            _comm_level = string.Empty;
            _min_dept = 0;
            _add_nonstk_itm = string.Empty;

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
        public int min_days_req
        {
            get { return _min_days_req; }
            set { _min_days_req = value; }
        }
        public string comm_level
        {
            get { return _comm_level; }
            set { _comm_level = value; }
        }
        public int min_dept
        {
            get { return _min_dept; }
            set { _min_dept = value; }
        }
        public string add_nonstk_itm
        {
            get { return _add_nonstk_itm; }
            set { _add_nonstk_itm = value; }
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
            get { return "usp_ins_sturqcntrc"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_sturqcntrc"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsturqdefget"; }
        }

        public override string TABLE_NAME
        {
            get { return "sturqcntrc"; }
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
            sql.Append("SELECT rowid,min_days_req,comm_level,min_dept,add_nonstk_itm ");
            sql.Append(" FROM  sturqcntrc");
            sql.Append(" WHERE 1=1");
            return sql.ToString();
        }
        
        #endregion Store Procedures
    }
}
