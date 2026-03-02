using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSecRoleModule : DVOBase
    {
        private int _RoleModuleId;
        private int _RoleId;
        private string _RoleName;
        private int _ModuleId;
        private string _ModuleName;
        private int _ModuleParentId;
        private string _Option;
        private string _FormName;
        private int _ModuleTypeId;
        private string _ModuleType;
        private string _ModuleImage;
        private int _AddPermission;
        private int _UpdatePermission;
        private int _DeletePermission;
        private int _FindPermission;
        private int _BrowsePermission;
        private int _NextPermission;
        private int _PreviousPermission;
        private int _TabPermission;
        private int _OptionsPermission;
        private int _InitPermission;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;

        #region Constructor

        public DVOSecRoleModule()
        {
            _RoleModuleId = 0;
            _RoleId = 0;
            _RoleName = string.Empty;
            _ModuleId = 0;
            _ModuleName = string.Empty;
            _ModuleParentId = 0;
            _Option = string.Empty;
            _FormName = string.Empty;
            _ModuleTypeId = 0;
            _ModuleType = string.Empty;
            _ModuleImage = string.Empty;
            _AddPermission = 0;
            _UpdatePermission = 0;
            _DeletePermission = 0;
            _FindPermission = 0;
            _BrowsePermission = 0;
            _NextPermission = 0;
            _PreviousPermission = 0;
            _TabPermission = 0;
            _OptionsPermission = 0;
            _InitPermission = 0;
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int RoleModuleId
        {
            get { return _RoleModuleId; }
            set { _RoleModuleId = value; }
        }

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        public int ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        public string ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        public int ModuleParentId
        {
            get { return _ModuleParentId; }
            set { _ModuleParentId = value; }
        }

        public string Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }

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

        public string ModuleImage
        {
            get { return _ModuleImage; }
            set { _ModuleImage = value; }
        }

        public int AddPermission
        {
            get { return _AddPermission; }
            set { _AddPermission = value; }
        }

        public int UpdatePermission
        {
            get { return _UpdatePermission; }
            set { _UpdatePermission = value; }
        }

        public int DeletePermission
        {
            get { return _DeletePermission; }
            set { _DeletePermission = value; }
        }

        public int FindPermission
        {
            get { return _FindPermission; }
            set { _FindPermission = value; }
        }

        public int BrowsePermission
        {
            get { return _BrowsePermission; }
            set { _BrowsePermission = value; }
        }

        public int NextPermission
        {
            get { return _NextPermission; }
            set { _NextPermission = value; }
        }

        public int PreviousPermission
        {
            get { return _PreviousPermission; }
            set { _PreviousPermission = value; }
        }

        public int TabPermission
        {
            get { return _TabPermission; }
            set { _TabPermission = value; }
        }

        public int OptionsPermission
        {
            get { return _OptionsPermission; }
            set { _OptionsPermission = value; }
        }

        public int InitPermission
        {
            get { return _InitPermission; }
            set { _InitPermission = value; }
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
            get { return "USP_SecRolModulIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_SecRolModulUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_SecRolModulDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SecRolModulGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_SecRolMdlGetAll"; }
        }

       
        public string COUNT_USER_MODULE_IN_ROLE
        {
            get { return "USP_UsrModiRolGet"; }
        }
        public string UPD_USRMDL
        {
            get { return "USP_UsrMudlUpd2"; }//uspusrmudlupd 
        }
        public string CHK_URS_ROLE
        {
            get { return "USP_ChkUsrinRole"; }
        }
        
        public string INSERT_ROLEMODULE
        {
            get { return "USP_RolModulIns"; } //usprolmodulins
        }

        public string UPDATE_ROLEMODULE
        {
            get { return "USP_RolModulUpd"; }  //usprolmodulupd
        }

        public string DELETE_ROLEMODULE
        {
            get { return "USP_RolModulDel"; }  //usprolmoduldel
        }
        //------------------------------------------------------

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rm.RoleModuleId p_rolemoduleid,rm.RoleId p_roleid,r.Role v_role,rm.ModuleId p_moduleid,m.Module v_module,");
            sql.Append(" m.ParentId v_parentid,rm.AddPermission v_add,rm.UpdatePermission v_update,rm.DeletePermission v_delete,rm.FindPermission v_find,");
            sql.Append(" rm.BrowsePermission v_browse,rm.NextPermission v_next,rm.PreviousPermission v_previous,rm.TabPermission v_tab,");
            sql.Append(" rm.OptionsPermission v_options,rm.InitPermission v_init,m.[Option] v_moduleoption,m.FormName v_moduleform,");
            sql.Append(" m.ModuleTypeId v_moduletypeid,mt.ModuleType v_moduletype,mt.ImagePath v_imagepath");
            sql.Append(" FROM secRoleModule rm, secModule m, secRole r,secModuleType mt");
            sql.Append(" WHERE rm.Active = 1 AND m.ModuleId = rm.ModuleId AND m.Active = 1");
            sql.Append(" AND rm.RoleId = r.RoleId AND r.Active = 1 AND mt.ModuleTypeId = m.ModuleTypeId AND mt.Active = 1");

            if (Convert.ToInt32(parameters[0]) > 0)//RoleModuleId
                sql.Append(" AND rm.RoleModuleId = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)//RoleId
                sql.Append("AND rm.RoleId = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)//ModuleId
                sql.Append("AND rm.ModuleId = " + parameters[2].ToString());

            return sql.ToString();
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
