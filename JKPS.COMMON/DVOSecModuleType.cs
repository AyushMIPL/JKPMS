using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSecModuleType : DVOBase
    {
        private int _ModuleTypeId;
        private string _ModuleType;
        private string _ImagePath;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;

        #region Constructor

        public DVOSecModuleType()
        {
            _ModuleTypeId = 0;
            _ModuleType = string.Empty;
            _ImagePath = string.Empty;
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int ModuleTypeId
        {
            get { return _ModuleTypeId; }
            set { _ModuleTypeId = value; }
        }

        public string ModuleType
        {
            get { return _ModuleType; }
            set { _ModuleType = value; }
        }

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
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
            get { return "USP_SecModulTypIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_SecModultypUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_SecModultypDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SecModultypGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_SecMdlTypGetAll"; }
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
            sql.Append("SELECT ModuleTypeId p_moduletypeid,ModuleType p_moduletype,ImagePath v_imagepath");
            sql.Append(" FROM secModuleType WHERE Active = 1");

            if (Convert.ToInt32(parameters[0]) > 0)//ModuleTypeId
                sql.Append(" AND ModuleTypeId = " + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//ModuleType
                sql.Append("AND Rtrim(Module) = '" + parameters[1].ToString().Replace("'", "''") + "%'");

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
