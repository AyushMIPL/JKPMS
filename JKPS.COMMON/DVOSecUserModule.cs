using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSecUserModule : DVOBase
    {
        private int _UserModuleId;
        private int _UserId;
        private int _RoleId;
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

        private string _UserLoginId;
        private string _UserEmpId;
        private string _UserFirstName;
        private string _UserLastName;

        #region Constructor

        public DVOSecUserModule()
        {
            _UserModuleId = 0;
            _UserId = 0;
            _RoleId = 0;
            _ModuleId = 0;
            _ModuleName = string.Empty;
            _ModuleParentId = 0;
            _ModuleTypeId = 0;
            _ModuleType = string.Empty;
            _ModuleImage = string.Empty;
            _Option = string.Empty;
            _FormName = string.Empty;
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

            _UserLoginId = string.Empty;
            _UserEmpId = string.Empty;
            _UserFirstName = string.Empty;
            _UserLastName = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int UserModuleId
        {
            get { return _UserModuleId; }
            set { _UserModuleId = value; }
        }

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
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



        public string UserLoginId
        {
            get { return _UserLoginId; }
            set { _UserLoginId = value; }
        }

        public string UserEmpId
        {
            get { return _UserEmpId; }
            set { _UserEmpId = value; }
        }

        public string UserFirstName
        {
            get { return _UserFirstName; }
            set { _UserFirstName = value; }
        }

        public string UserLastName
        {
            get { return _UserLastName; }
            set { _UserLastName = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public string INSERT_USERMODULE
        {
            get { return "uspusrmodulins2"; }// uspusrmodulins
        }

        public string UPDATE_USERMODULE
        {
            get { return "uspusrmodulupd2"; }// uspusrmodulupd
        }

        public string DELETE_USERMODULE
        {
            get { return "uspusrmoduldel2"; }// uspusrmoduldel
        }

        public override string INSERT_SPNAME
        {
            get { return "uspsecusrmodulins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsecusrmodulupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspsecusrmoduldel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspsecusrmodulget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsecusrmdlgetall"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  um.Rolemoduleid p_usermoduleid, u.userid p_userid,u.loginid v_loginid,");
            sql.Append(" u.firstname p_firstname,u.lastname p_lastname,u.empid p_empid,um.roleid p_roleid,");
            sql.Append(" (select role from secrole where roleid=u.roleid and active=1) v_rolename,");
            sql.Append(" um.moduleid p_moduleid, m.module v_module,m.parentid v_parentid,");
            sql.Append(" um.addpermission v_add, um.updatepermission v_update,um.deletepermission v_delete,");
            sql.Append(" um.findpermission v_find, um.browsepermission v_browse,um.nextpermission v_next,");
            sql.Append(" um.previouspermission v_previous,um.tabpermission v_tab,um.optionspermission v_options,");
            sql.Append(" um.initpermission v_init,m.parentid v_parentmoduleid,m.[option] v_moduleoption,");
            sql.Append(" m.formname v_moduleform,m.moduletypeid v_moduletypeid,mt.moduletype v_moduletype,mt.imagepath v_imagepath");
            sql.Append(" FROM secrolemodule um,secusers u,secmodule m,secmoduletype mt");
            sql.Append(" WHERE um.roleid = u.roleid AND u.active = 1");
            sql.Append(" AND m.moduleid = um.moduleid AND m.active = 1");
            sql.Append(" AND um.active = 1 AND mt.moduletypeid = m.moduletypeid ANd mt.active = 1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//UserModuleId
                    sql.Append(" AND um.RoleModuleID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//UserId
                    sql.Append(" AND u.UserId = " + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[2]) > 0)//RoleId
                        sql.Append(" AND um.RoleId = " + parameters[2].ToString().Trim());
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) > 0)//ModuleId
                    sql.Append(" AND um.ModuleId = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)//UserLoginId
                    sql.Append(" AND Rtrim(u.LoginId) =  trim('" + parameters[4].ToString().Trim().Replace("'", "''") + "')");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)//UserEmpId
                    sql.Append(" AND Rtrim(u.EmpId) =  trim('" + parameters[5].ToString().Trim().Replace("'", "''") + "')");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)//UserFirstName
                    sql.Append(" AND Rtrim(u.FirstName) LIKE  '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim().Length > 0)//UserLastName
                    sql.Append(" AND Rtrim(u.LastName) LIKE  '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append(" ORDER BY m.[option],um.ModuleId");

            return sql.ToString();
        }

        public string FIND_USER_PERMISSION(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  um.usermoduleid p_usermoduleid, um.userid p_userid,u.loginid v_loginid,");
            sql.Append(" u.firstname p_firstname,u.lastname p_lastname,u.empid p_empid,um.roleid p_roleid,");
            sql.Append(" (select role from secrole where roleid=u.roleid and active=1) v_rolename,");
            sql.Append(" um.moduleid p_moduleid, m.module v_module,m.parentid v_parentid,");
            sql.Append(" um.addpermission v_add, um.updatepermission v_update,um.deletepermission v_delete,");
            sql.Append(" um.findpermission v_find, um.browsepermission v_browse,um.nextpermission v_next,");
            sql.Append(" um.previouspermission v_previous,um.tabpermission v_tab,um.optionspermission v_options,");
            sql.Append(" um.initpermission v_init,m.parentid v_parentmoduleid,m.option v_moduleoption,");
            sql.Append(" m.formname v_moduleform,m.moduletypeid v_moduletypeid,mt.moduletype v_moduletype,mt.imagepath v_imagepath, ");
            sql.Append(" u.active v_active ");
            sql.Append(" FROM secusermodule um,secusers u,secmodule m,secmoduletype mt");
            sql.Append(" WHERE um.userid = u.userid ");
            sql.Append(" AND m.moduleid = um.moduleid AND m.active = 1");
            sql.Append(" AND um.active = 1 AND mt.moduletypeid = m.moduletypeid ANd mt.active = 1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)//UserModuleId
                    sql.Append(" AND um.UserModuleId = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)//UserId
                    sql.Append(" AND um.UserId = " + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    if (Convert.ToInt32(parameters[2]) > 0)//RoleId
                        sql.Append(" AND um.RoleId = " + parameters[2].ToString().Trim());
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) > 0)//ModuleId
                    sql.Append(" AND um.ModuleId = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)//UserLoginId
                    sql.Append(" AND Rtrim(u.LoginId) =  trim('" + parameters[4].ToString().Trim().Replace("'", "''") + "')");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)//UserEmpId
                    sql.Append(" AND Rtrim(u.EmpId) =  trim('" + parameters[5].ToString().Trim().Replace("'", "''") + "')");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)//UserFirstName
                    sql.Append(" AND Rtrim(u.FirstName) LIKE  '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim().Length > 0)//UserLastName
                    sql.Append(" AND Rtrim(u.LastName) LIKE  '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append(" ORDER BY m.option,um.ModuleId");

            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
