using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOLockTableStatus : DVOBase
    {
        private int _RowId;
        private int _LockedRowId;
        private string _TableName;
        private string _PrimFields;
        private string _PrimValues;
        private string _WhereCond;
        private int _LockByUserId;
        private string _LockByUserLoginId;
        private string _LockByUserFirstName;
        private string _LockDatetime;
        private string _LockByMachInfo;
        private string _Release;
        private int _RelByUserId;
        private string _RelByUserLoginId;
        private string _RelByUserFirstName;
        private string _RelDateTime;
        private string _RelByMachInfo;

        #region Constructor

        public DVOLockTableStatus()
        {
            _RowId = 0;
            _LockedRowId = 0;
            _TableName = string.Empty;
            _PrimFields = string.Empty;
            _PrimValues = string.Empty;
            _WhereCond = string.Empty;
            _LockByUserId = 0;
            _LockByUserLoginId = string.Empty;
            _LockByUserFirstName = string.Empty;
            _LockDatetime = string.Empty;
            _LockByMachInfo = string.Empty;
            _Release = string.Empty;
            _RelByUserId = 0;
            _RelByUserLoginId = string.Empty;
            _RelByUserFirstName = string.Empty;
            _RelDateTime = string.Empty;
            _RelByMachInfo = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int LockedRowId
        {
            get { return _LockedRowId; }
            set { _LockedRowId = value; }
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        public string PrimFields
        {
            get { return _PrimFields; }
            set { _PrimFields = value; }
        }
        public string PrimValues
        {
            get { return _PrimValues; }
            set { _PrimValues = value; }
        }
        public string WhereCond
        {
            get { return _WhereCond; }
            set { _WhereCond = value; }
        }
        public int LockByUserId
        {
            get { return _LockByUserId; }
            set { _LockByUserId = value; }
        }
        public string LockByUserLoginId
        {
            get { return _LockByUserLoginId; }
            set { _LockByUserLoginId = value; }
        }
        public string LockByUserFirstName
        {
            get { return _LockByUserFirstName; }
            set { _LockByUserFirstName = value; }
        } 
        public string LockDatetime
        {
            get { return _LockDatetime; }
            set { _LockDatetime = value; }
        }
        public string LockByMachInfo
        {
            get { return _LockByMachInfo; }
            set { _LockByMachInfo = value; }
        }
        public string Release
        {
            get { return _Release; }
            set { _Release = value; }
        }
        public int RelByUserId
        {
            get { return _RelByUserId; }
            set { _RelByUserId = value; }
        }
        public string RelByUserLoginId
        {
            get { return _RelByUserLoginId; }
            set { _RelByUserLoginId = value; }
        }
        public string RelByUserFirstName
        {
            get { return _RelByUserFirstName; }
            set { _RelByUserFirstName = value; }
        }
        public string RelDateTime
        {
            get { return _RelDateTime; }
            set { _RelDateTime = value; }
        }
        public string RelByMachInfo
        {
            get { return _RelByMachInfo; }
            set { _RelByMachInfo = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspLockTableUpd"; }
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
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "locktablestatus"; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string CLEAN_LOCK
        {
            get { return "usplocktbstatusn"; }
         }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select locktablestatus.rowid v_rowid,lockbyuserid v_lockbyuserid,lockdatetime v_lockdatetime,");
            sql.Append("locktablestatus.insertdate v_insertdate,lockbymachinfo v_machineinfo,tablename v_tablename, lockedrowid v_lockedrowid,release v_release ,s.loginid v_loginid");
            sql.Append(" from locktablestatus , outer secusers s ");
            sql.Append(" where s.userid=locktablestatus.lockbyuserid and release=0 ");
            sql.Append(" ORDER BY  locktablestatus.lockedrowid ");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
