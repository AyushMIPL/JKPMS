using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOsecusrmdlwsirol:DVOBase
    {
        private int _userid;
        private int _roleid;
        private int _moduleid;
        private int _addpermission;
        private int _updatepermission;
        private int _deletepermission;
        private int _findpermission;
        private int _browsepermission;
        private int _nextpermission;
        private int _previouspermission;
        private int _tabpermission;
        private int _optionspermission;
        private int _initpermission;
        private int _active;
        private int _status;
        private int _insertby;
        private DateTime _insertdate;  //datetime //year to fraction(3) default current year to fraction(3),
        private string _insertmachineinfo;//char(50) default 'APP',
        private int _updateby; // integer,
        private DateTime _updatedate; // datetime year to fraction(3) default current year to fraction(3),
        private string _updatemachineinfo; // char(50) default 'APP'

        public DVOsecusrmdlwsirol()
        {
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
            _insertdate = Convert.ToDateTime("01/01/1900");  
            _insertmachineinfo = "APP";//char(50) default 'APP',
            _updateby = 0; // integer,
            _updatedate = Convert.ToDateTime("01/01/1900");
            _updatemachineinfo = "APP"; // char(50) default 'APP'

        }


        public int userid
        {
            get {return _userid ;}
            set{ _userid  =value  ;}
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
        public DateTime insertdate
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
        public DateTime updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
       

        public override string INSERT_SPNAME
        {
            get { return "uspusrmdlwsirolins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspusrmdlwsirolupd"; }
        }
      
        public override string DELETE_SPNAME
        {
            get { return "uspusrmdlwsiroldel"; }
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
            get { return ""; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           
            return sql.ToString();
        }

    }
}
