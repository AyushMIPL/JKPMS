using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOsecusrmdlothrol : DVOBase
    {
        private int _rowid;
        private int _userid; // integer default 0 not null,
        private int _roleid; // integer default 0 not null,
        private int _moduleid;// integer default 0 not null,
        private int _addpermission;//     smallint default 0,
        private int _updatepermission;// smallint default 0,
        private int _deletepermission;// smallint default 0,
        private int _findpermission; //smallint default 0,
        private int _browsepermission; // smallint default 0,
        private int _nextpermission; // smallint default 0,
        private int _previouspermission; // smallint default 0,
        private int _tabpermission; // smallint default 0,
        private int _optionspermission; // smallint default 0,
        private int _initpermission; // smallint default 0,
        private int _active;// smallint default 0,
        private int _status; //smallint default 0,
        private int _insertby; //integer default 0,
        private string _insertdate; // datetime year to fraction(3) default current year to fraction(3),
        private string _insertmachineinfo; // char(50) default 'APP',
        private int _updateby; // integer default 0,
        private string _updatedate; // datetime year to fraction(3) default current year to fraction(3),
        private string _updatemachineinfo; // char(50) default 'APP'

        #region Constructor
        public DVOsecusrmdlothrol()
        {
            _rowid = 0;
            _userid = 0;
            _roleid = 0;
            _moduleid = 0;
            _addpermission = 0;
            _updatepermission = 0;
            _deletepermission = 0;
            _findpermission = 0;
            _browsepermission = 0;
            _nextpermission = 0;
            _previouspermission = 0;
            _tabpermission = 0;
            _optionspermission = 0;
            _initpermission = 0;
            _active = 0;
            _status = 0;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;
        }
        #endregion Constructor

        #region Public Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public int roleid
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        public int moduleid
        {
            get { return _moduleid; }
            set { _moduleid = value; }
        }
        public int addpermission
        {
            get { return _addpermission; }
            set { _addpermission = value; }
        }
        public int updatepermission
        {
            get { return _updatepermission; }
            set { _updatepermission = value; }
        }
        public int deletepermission
        {
            get { return _deletepermission; }
            set { _deletepermission = value; }
        }
        public int findpermission
        {
            get { return _findpermission; }
            set { _findpermission = value; }
        }
        public int browsepermission
        {
            get { return _browsepermission; }
            set { _browsepermission = value; }
        }
        public int nextpermission
        {
            get { return _nextpermission; }
            set { _nextpermission = value; }
        }
        public int previouspermission
        {
            get { return _previouspermission; }
            set { _previouspermission = value; }
        }
        public int tabpermission
        {
            get { return _tabpermission; }
            set { _tabpermission = value; }
        }
        public int optionspermission
        {
            get { return _optionspermission; }
            set { _optionspermission = value; }
        }
        public int initpermission
        {
            get { return _initpermission; }
            set { _initpermission = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
        public int status
        {
            get { return _status; }
            set { _status = value; }
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
        #endregion Public Properties


        #region Stored-Procedures
        public string AUTHENTICATION_SPNAME
        {
            get { return "uspsecauthenticate"; }
        }
        //Added by Sunil Pahwa 
        public override string INSERT_SPNAME
        {
            get { return "uspusrmdlothrolins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspusrmdlothrolupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspusrmdlothroldel"; }
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
            get { return "secusrmdlothrol"; }
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
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
