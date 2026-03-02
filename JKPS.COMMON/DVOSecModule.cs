using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSecModule : DVOBase
    {
        private int _ModuleId;
        private string _Module;
        private string _Description;
        private int _Level;
        private string _ParentId;
        private string _FormName;
        private string _ParentName;
        private int _ModuleTypeId;
        private string _ModuleTypeName;
        private string _Option;
        private string _ImagePath;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        
        #region Constructor

        public DVOSecModule()
        {
            _ModuleId = 0;
            _Module = string.Empty;
            _Description = string.Empty;
            _Level = 0;
            _ParentId = string.Empty;
            _FormName = string.Empty;
            _ParentName = string.Empty;
            _ModuleTypeId = 0;
            _ModuleTypeName = string.Empty;
            _Option = string.Empty;
            _ImagePath = string.Empty;
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        public string ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        
        public int ModuleTypeId
        {
            get { return _ModuleTypeId; }
            set { _ModuleTypeId = value; }
        }

        public string ModuleTypeName
        {
            get { return _ModuleTypeName; }
            set { _ModuleTypeName = value; }
        }

        public string Option
        {
            get { return _Option; }
            set { _Option = value; }
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
            get { return "USP_SecModuleIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_SecModuleUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_SecModuleDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SecModuleGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_SecModuleGetAll"; }
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
            sql.Append("SELECT distinct m.ModuleId p_moduleid,m.Module p_module,m.Description v_description,m.Level p_level,m.ParentId p_parentid,");
            sql.Append(" m.FormName v_formname,m.ModuleTypeId p_moduletypeid,mt.ModuleType v_moduletype,m.[Option] p_option,mt.ImagePath v_imagepath,");
            sql.Append("(select m1.module from secmodule m1 where m1.moduleid=m.parentid) p_parentname ");
            sql.Append(" FROM secModule m,secModuleType mt WHERE m.ModuleTypeId = mt.ModuleTypeId  and m.Active = 1 AND mt.Active = 1");

            if (Convert.ToInt32(parameters[0]) > 0)//ModuleId
                sql.Append(" AND m.ModuleId = " + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//Module
                sql.Append(" AND RTRIM(m.Module)like  TRIM('" + parameters[1].ToString() + "%')");
            if (Convert.ToInt32(parameters[2]) > 0)//Level
                sql.Append(" AND m.Level = " + parameters[2].ToString());
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//ParentId
                sql.Append(" AND m.ParentId = " + parameters[3].ToString());
            if (Convert.ToInt32(parameters[4]) > 0)//ModuleTypeId
                sql.Append(" AND m.ModuleTypeId = " + parameters[4].ToString());
            if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//Option
                sql.Append(" AND RTRIM(m.Option) = TRIM('" + parameters[5].ToString() + "')");

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
