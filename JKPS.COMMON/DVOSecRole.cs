using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSecRole : DVOBase
    {
        private int _RoleId;
        private string _Role;
        private string _Description;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        private int _Rowid;

        #region Constructor

        public DVOSecRole()
        {
            _RoleId = 0;
            _Role = string.Empty;
            _Description = string.Empty;
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
            _Rowid = 0;
        }

        #endregion Constructor

        #region Public Properties

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        public string Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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

        public int Rowid
        {
            get { return _Rowid ; }
            set { _Rowid  = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        
        public override string INSERT_SPNAME
        {
            get { return "USP_SecRoleins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_SecRoleUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_SecRoleDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SecRoleGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_SecRoleGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "secRole"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RoleId ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT roleid p_roleid,role p_role,description v_description,RoleID v_rowid FROM secRole");
            sql.Append(" WHERE Active = 1");
            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[0]) > 0)//RoleId
                sql.Append(" AND roleid = " + parameters[0].ToString());
             if(parameters[1].ToString() != null)
                 if (parameters[1].ToString() != string.Empty)
                sql.Append(" AND role ='" + parameters[1].ToString().Trim()+"'");
            if(parameters[2].ToString() != null)
               if(parameters[2].ToString() != string.Empty )
                sql.Append(" AND Rtrim(description) LIKE '" + parameters[2].ToString().Replace("'", "''") + "%'");

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
