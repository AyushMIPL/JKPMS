using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOApprovalSystemUserInfo :DVOBase
    {
        private DVOSystemUserInfo _objDVOSystemUserInfo;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        
        #region Constructor

        public DVOApprovalSystemUserInfo()
        {
            //_ModuleId = 0;
            //_Module = string.Empty;
            //_Description = string.Empty;
            //_Level = 0;
            //_ParentId = string.Empty;
            //_FormName = string.Empty;
            //_ModuleTypeId = 0;
            //_ModuleTypeName = string.Empty;
            //_Option = string.Empty;
            //_ImagePath = string.Empty;
            //_InsertBy = 0;
            //_InsertMachineInfo = string.Empty;
            //_UpdateBy = 0;
            //_UpdateMachineInfo = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public DVOSystemUserInfo objDVOSystemUserInfo
        {
            get { return _objDVOSystemUserInfo; }
            set { _objDVOSystemUserInfo = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspsecmoduleins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsecmoduleupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspsecmoduledel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspsecmoduleget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsecmodulegetall"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
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
        #endregion Stored-Procedures
    }
}
