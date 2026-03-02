using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
///<Development and modification Details>
/// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
///1.) Make property for change password                    Rahul Jain                                   11/15/2008(DD)
///2.) 
///<summery>
{
   public class DVOChangePwd : DVOBase
    {
        private int _UserId;
        private string _FirstName;
        private string _LastName;
        private string _LoginId;
        private string _OLDPassword;
        private string _NEWPassword;
        private string _LastPwdChnageDate;
        private int _UpdateBy;
        private string _UpdateMachineInfo;


        #region Constructor

        public DVOChangePwd()
          { 
            _UserId = 0;
            _FirstName = string.Empty;
            _LastName = string.Empty;
            _LoginId = string.Empty;
            _OLDPassword = string.Empty;
            _NEWPassword = string.Empty;
            _LastPwdChnageDate = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
          }

        #endregion

          #region Public Properties
         
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

          public string OLDPassword
          {
              get { return _OLDPassword; }
              set { _OLDPassword = value; }
          }

         public string NEWPassword
          {
              get { return _NEWPassword; }
              set { _NEWPassword = value; }
          }

        public string LastPwdChnageDate
          {
              get { return _LastPwdChnageDate; }
              set { _LastPwdChnageDate = value; }

          }
       public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

       public int UpdateBy
       {
           get { return _UpdateBy; }
           set { _UpdateBy = value; }

       }
          #endregion Properties

          #region Stored-Procedures

          public string AUTHENTICATION_SPNAME
          {
              get { return "uspsecauthenticate"; }
          }

          public override string INSERT_SPNAME
          {
              get { return ""; }
          }

          public override string UPDATE_SPNAME
          {
              get { return "USP_ChangePwdUpd"; }
          }

          public override string DELETE_SPNAME
          {
              get { return ""; }
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
              get { return "secusers"; }
          }

          public override int UNIQUE_ID
          {
              get { return UserId; }
          }

          public override string NOTES_TABLE_RECORD_ID
          {
              get { return string.Empty; }
              set { throw new Exception("The method or operation is not implemented."); }
          }
       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();
           sql.Append("SELECT  u.UserId p_userid, u.RoleId p_roleid, r.Role v_role, FirstName p_firstname,");
           sql.Append(" LastName p_lastname, LoginId p_loginid, EmpId p_empid, Department p_department,");
           sql.Append(" u.Active p_active,u.UserExpiresOn p_UserExpiresOn ,u.LastPwdUpddate p_LastPwdUpddate,u.PwdexpiresOn p_PwdexpiresOn FROM secUsers u, secRole r ");
           sql.Append(" WHERE r.RoleId = u.RoleId AND r.Active = 1");
           if (Convert.ToInt32(parameters[0]) > 0)//UserId
               sql.Append(" AND UserId = " + parameters[0].ToString());
           if (parameters[1] != null)
               if (parameters[1].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(FirstName) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");//FirstName

           if (parameters[2] != null)
               if (parameters[2].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(LastName) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");


           if (parameters[3] != null)
               if (parameters[3].ToString() != string.Empty)//LoginId
                   sql.Append(" AND Rtrim(LoginId) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");



           if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//EmployeeId
               sql.Append(" AND Rtrim(EmpId) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
           if (parameters[5] != null)
               if (parameters[5].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(Department) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

           if (Convert.ToInt32(parameters[6]) > 0)//RoleId
               sql.Append(" AND u.RoleId = " + parameters[6].ToString());
           if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//Active
               sql.Append("AND u.Active = " + parameters[7].ToString().Trim());
           if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)//UserExpiresOn
               sql.Append("AND u.UserExpiresOn = " + parameters[8].ToString().Trim());

         



              return sql.ToString();
          }

          #endregion Stored-Procedures
      }
  }
