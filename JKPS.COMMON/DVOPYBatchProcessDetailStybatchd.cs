using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPYBatchProcessDetailStybatchd : DVOBase
    {
        int _pybatchid;
        int _batchprocessid;//serial,
        string _processname;
        string _processstartedon;//datetime year to fraction(3),
        string _processendedon;//datetime year to fraction(3),
        int _recordssearched;
        int _recordsprocessed;
        int _status;
        string _searchcriteria;
        string _errormessage;
        int _insertby;
        string _insertdate;//datetime year to fraction(3),
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;//datetime year to fraction(3),
        string _updatemachineinfo;
        private string _startTime;
        private string _endTime;
        #region Constructor

        public DVOPYBatchProcessDetailStybatchd()
        {
            _pybatchid = 0;
            _batchprocessid = 0;//serial,
            _processname = string.Empty;
            _processstartedon = "01/01/1900";//datetime year to fraction(3),
            _processendedon = "01/01/1900";//datetime year to fraction(3),
            _recordssearched = 0;
            _recordsprocessed = 0;
            _status = 0;
            _searchcriteria = string.Empty;
            _errormessage = string.Empty;
            _insertby = DVOApplicationUserInfo.UserId;
            _insertdate = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//datetime year to fraction(3),
            _insertmachineinfo = DVOApplicationUserInfo.MachineInfo;
            _updateby = DVOApplicationUserInfo.UserId;
            _updatedate = DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);//datetime year to fraction(3),
            _updatemachineinfo = DVOApplicationUserInfo.MachineInfo;
            _startTime = string.Empty;
            _endTime = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int pybatchid
        {
            get { return _pybatchid; }
            set { _pybatchid = value; }
        }
        public int batchprocessid
        {
            get { return _batchprocessid; }
            set { _batchprocessid = value; }
        }
        public string processname
        {
            get { return _processname; }
            set { _processname = value; }
        }
        public string processstartedon
        {
            get { return _processstartedon; }
            set { _processstartedon = value; }
        }
        public string processendedon
        {
            get { return _processendedon; }
            set { _processendedon = value; }
        }
        public int recordssearched
        {
            get { return _recordssearched; }
            set { _recordssearched = value; }
        }
        public int recordsprocessed
        {
            get { return _recordsprocessed; }
            set { _recordsprocessed = value; }
        }
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string searchcriteria
        {
            get { return _searchcriteria; }
            set { _searchcriteria = value; }
        }
        public string errormessage
        {
            get { return _errormessage; }
            set { _errormessage = value; }
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
        public string StartTine
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        public string EndTine
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_PyBtchDtlIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_PyBtchDtlUpd"; }
        }

        public string DELETE_ALL_DETAILS
        {
            get { return "USP_PyBtchDtlDel"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_PYAllBtchDtlDel"; }
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
            get { return "Payroll_Process_Details"; }
        }

        public override int UNIQUE_ID
        {
            get { return _batchprocessid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string ProcessIns
        {
            get { return "USP_ProcessIns"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT pybatchid v_pybatchid,batchprocessid v_batchprocessid,processname v_processname,");
            sql.Append(" processstartedon v_processstartedon,processendedon v_processendedon,recordssearched v_recordssearched,");
            sql.Append(" recordsprocessed v_recordsprocessed,status v_status,errormessage v_errormessage,");
            sql.Append(" insertby v_insertby,insertdate v_insertdate,insertmachineinfo v_insmacinfo,");
            sql.Append(" updateby v_updateby,updatedate v_updatedate,updatemachineinfo v_updmacinfo");
            sql.Append(" FROM Payroll_Process_Details WHERE 1=1");

            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND pybatchid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND batchprocessid = " + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(processname) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" AND processstartedon = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" AND processendedon = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND recordssearched = " + parameters[5].ToString());
            if (parameters[6] != null)
                if (Convert.ToInt32(parameters[6]) > 0)
                    sql.Append(" AND recordsprocessed = " + parameters[6].ToString());
            if (parameters[7] != null)
                if (Convert.ToInt32(parameters[7]) > 0)
                    sql.Append(" AND status = " + parameters[7].ToString());
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim().Length > 0)
                    sql.Append(" AND errormessage = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        #endregion store-procedures
    }
}
