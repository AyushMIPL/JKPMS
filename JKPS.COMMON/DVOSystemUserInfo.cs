using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSystemUserInfo : Object, IDisposable
    {
        public void Dispose() { }
        private int _user_id;
        private string _login_id;
        private string _first_name;
        private string _last_name;
        private string _position;
        //Added by sanjay
        private string _accttype;
        
        //private int _InsertBy;
        //private string _InsertMachineInfo;
        //private int _UpdateBy;
        //private string _UpdateMachineInfo;
        #region Constructor
        public DVOSystemUserInfo(int puser_id, string plogin_id, string pfirst_name, string plast_name, string pposition)
        {
        _user_id=puser_id;
        _login_id = plogin_id;
        _first_name = pfirst_name;
        _last_name = plast_name;
        _position = pposition;

        //_InsertBy = 0;
        //_InsertMachineInfo = string.Empty;
        //_UpdateBy = 0;
        //_UpdateMachineInfo = string.Empty;
        }
        public DVOSystemUserInfo()
        {
            //_user_id=0;
            //_login_id=string.Empty;
            //_first_name = string.Empty;
            //_last_name = string.Empty;
            //_position = string.Empty;
            //_InsertBy = 0;
            //_InsertMachineInfo = string.Empty;
            //_UpdateBy = 0;
            //_UpdateMachineInfo = string.Empty;
             
             _accttype=string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string login_id
        {
            get { return _login_id; }
            set { _login_id = value; }
        }
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string position
        {
            get { return _position; }
            set { _position = value; }
        }
        public string accttype
        {
            get { return _accttype; }
            set { _accttype = value; }
        }
        //public int InsertBy
        //{
        //    get { return _InsertBy; }
        //    set { _InsertBy = value; }
        //}

        //public string InsertMachineInfo
        //{
        //    get { return _InsertMachineInfo; }
        //    set { _InsertMachineInfo = value; }
        //}

        //public int UpdateBy
        //{
        //    get { return _UpdateBy; }
        //    set { _UpdateBy = value; }
        //}

        //public string UpdateMachineInfo
        //{
        //    get { return _UpdateMachineInfo; }
        //    set { _UpdateMachineInfo = value; }
        //}

        #endregion

        //#region Stored-Procedures
        public string Get_ACCT_MASK
        {
            get { return "uspacctmaskget"; }
        }
        public  string INSERT_SPNAME
        {
            get { return "uspsysuserinfoins"; }
        }

        public string UPDATE_SPNAME
        {
            get { return "uspsysuserinfoupd"; }
        }

        public string DELETE_SPNAME
        {
            get { return "uspsysuserinfodel"; }
        }

        //public override string FIND_SPNAME
        //{
        //    get { return "uspsecmoduleget"; }
        //}

        public  string ALL_SPNAME
        {
            get { return "uspSysUserGetAll"; }
        }
        //public override string FIND_QUERY(ref Object[] parameters)
        //{
        //    return "";
        //}

        //#endregion Stored-Procedures
    }
}
