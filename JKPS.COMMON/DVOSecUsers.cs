using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By: Chand
    public class DVOSecUsers : DVOBase
    {
        private int _UserId;
        private string _FirstName;
        private string _LastName;
        private string _LoginId;
        private string _Password;
        private string _EmailId;
        private string _EmpId;
        private string _Department;
        private int _RoleId;
        private string _RoleName;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        private string _Active;
        private int _ModuleId;
        private bool _isSysUser;
        private string _UserExpiresOn;
        private string _LastPwdUpddate;
        private string _PwdexpiresOn;
        private DateTime _UserExpiresDate;
        private int _loginlogid;
        private string _MenuStyle;
        private DateTime _serverDate;
        private string _curPeriod;
        private string _curYear;

        #region Constructor

        public DVOSecUsers()
        {
            _UserId = 0;
            _FirstName = string.Empty;
            _LastName = string.Empty;
            _LoginId = string.Empty;
            _Password = string.Empty;
            _EmailId = string.Empty;
            _EmpId = string.Empty;
            _Department = string.Empty;
            _RoleId = 0;
            _RoleName = string.Empty;
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
            _Active = string.Empty;
            _ModuleId = 0;
            _UserExpiresOn = string.Empty;
            _UserExpiresDate =  Convert.ToDateTime("01/01/1900");
            _LastPwdUpddate = string.Empty;
            _PwdexpiresOn = string.Empty;
            _loginlogid = 0;
            _MenuStyle = string.Empty;
        }

        #endregion Constructor

        #region Public Properties
        public int ModuleID
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
       

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
    
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
       

        public string EmployeeId
        {
            get { return _EmpId; }
            set { _EmpId = value; }
        }

        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
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

        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
       
        public bool isSysUser
        {
            get
            {
                return _isSysUser;
            }
            set
            {
                _isSysUser = value;
            }
        }

        public string UserExpiresOn
        {
            get { return _UserExpiresOn; }
            set { _UserExpiresOn = value; }
        }
        public string LastPwdUpddate
        {
            get { return _LastPwdUpddate; }
            set { _LastPwdUpddate = value; }
        }
        public string PwdexpiresOn
        {
            get { return _PwdexpiresOn; }
            set { _PwdexpiresOn = value; }
        }

        public DateTime UserExpiresDate
        {
            get { return _UserExpiresDate; }
            set { _UserExpiresDate = value; }
        }
        public int loginlogid
        {
            get { return _loginlogid; }
            set { _loginlogid = value; }
        }

        public string MenuStyle
        {
            get { return _MenuStyle; }
            set { _MenuStyle = value; }
        }
        public DateTime ServerDate
        {
            get { return _serverDate; }
            set { _serverDate = value; }
        }
        public string CurPeriod
        {
            get { return _curPeriod; }
            set { _curPeriod = value; }
        }
        public string CurYear
        {
            get { return _curYear; }
            set { _curYear = value; }
        }
        #endregion Public Properties

        #region Stored-Procedures

   

        public string AUTHENTICATION_SPNAME
        {
            get { return "USP_UserValidate"; }
        }
        
        public override string INSERT_SPNAME
        {
            get { return "USP_SecUserIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_SecUserUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_SecUserDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SecUserGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_SecUserGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "SECUsers"; }
        }

        public override int UNIQUE_ID
        {
            get { return _UserId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string FIND_EXISTLOGINID
        {
            get { return "USP_SecUserExist"; }
        }       
        public string UPDATE_STYLE
        {
            get { return "USP_MenuStyleUpd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  u.UserId p_userid, u.RoleId p_roleid, r.Role v_role, FirstName p_firstname,");
            sql.Append(" LastName p_lastname, LoginId p_loginid, EmpId p_empid, Department p_department,");
            sql.Append(" u.Active p_active,u.UserExpiresOn p_UserExpiresOn ,u.LastPwdUpddate p_LastPwdUpddate,u.PwdexpiresOn p_PwdexpiresOn,");
            sql.Append(" emailid p_emailid ,menustyle p_menustyle FROM secUsers u, secRole r ");
            sql.Append(" WHERE r.RoleId = u.RoleId AND r.Active = 1");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND UserId = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(FirstName) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");//FirstName

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(LastName) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)//LoginId
                    sql.Append(" AND Rtrim(LoginId) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

         
          
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//EmployeeId
                sql.Append(" AND Rtrim(EmpId) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Department) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToInt32(parameters[6]) > 0)//RoleId
                sql.Append(" AND u.RoleId = " + parameters[6].ToString());
            if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//Active
                sql.Append(" AND u.Active = " + parameters[7].ToString().Trim());
            
            if(parameters[8] !=null)
                 if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null )//&& Convert.ToDateTime(parameters[8]) != Convert.ToDateTime("01/01/1900"))//UserExpiresOn
                     sql.Append(" AND u.UserExpiresOn = '" + parameters[8] +"'");
            if(parameters[9] != null)
                 if (parameters[9].ToString() != string.Empty && parameters[9].ToString() != null && Convert.ToDateTime(parameters[9]) != Convert.ToDateTime("01/01/1900"))//LastPwdUpddate
                      sql.Append(" AND u.LastPwdUpddate = '" + Convert.ToDateTime(parameters[9]) + "'");
            
              if (parameters[10] != null)
                  if (parameters[10].ToString() != string.Empty)
                      sql.Append(" AND Rtrim(u.emailid) LIKE '" + parameters[10].ToString().Trim().Replace("'", "''") + "%'");//EmailID
            //*********************************************

            return sql.ToString();
        }

        public  string GET_USER(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  u.UserId p_userid, u.RoleId p_roleid, '', FirstName p_firstname,");
            sql.Append(" LastName p_lastname, LoginId p_loginid, EmpId p_empid, Department p_department,");
            sql.Append(" u.Active p_active,u.UserExpiresOn p_UserExpiresOn ,u.LastPwdUpddate p_LastPwdUpddate,u.PwdexpiresOn p_PwdexpiresOn,");
            //Added by Bharat Dhall [04/10/2009]
            sql.Append(" emailid p_emailid ,menustyle p_menustyle FROM secUsers u ");
            //*************************************
            sql.Append(" WHERE 1 = 1");
            if (Convert.ToInt32(parameters[0]) > 0)//UserId
                sql.Append(" AND UserId = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(FirstName) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");//FirstName

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTrim(LastName) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)//LoginId
                    sql.Append(" AND RTrim(LoginId) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");



            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//EmployeeId
                sql.Append(" AND RTrim(EmpId) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND RTrim(Department) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToInt32(parameters[6]) > 0)//RoleId
                sql.Append(" AND u.RoleId = " + parameters[6].ToString());
            if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//Active
                sql.Append(" AND u.Active = " + parameters[7].ToString().Trim());
            //**************Added By Rahul Jain on 19/11/2008*************
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)//&& Convert.ToDateTime(parameters[8]) != Convert.ToDateTime("01/01/1900"))//UserExpiresOn
                    sql.Append(" AND u.UserExpiresOn = '" + parameters[8] + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty && parameters[9].ToString() != null && Convert.ToDateTime(parameters[9]) != Convert.ToDateTime("01/01/1900"))//LastPwdUpddate
                    sql.Append(" AND u.LastPwdUpddate = '" + Convert.ToDateTime(parameters[9]) + "'");
            //************************************************************

            //Added by Bharat Dhall [04/10/2009]
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND RTrim(u.emailid) LIKE '" + parameters[10].ToString().Trim().Replace("'", "''") + "%'");//EmailID
            //*********************************************

            return sql.ToString();
        }

        public string GET_USERS_BY_ROLEID(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT UserId FROM secUsers WHERE Active=1 ");
            if (Convert.ToInt32(parameters[0]) > 0)//RoleId
                sql.Append(" AND RoleId = " + parameters[0].ToString());
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
