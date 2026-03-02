using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOUpdPurchaseClasses:DVOBase 
    {

       public DVOUpdPurchaseClasses()
       {


       }






         #region Stored-Procedures

        public string AUTHENTICATION_SPNAME
        {
            get { return "uspsecauthenticate"; }
        }
        
        public override string INSERT_SPNAME
        {
            get { return "uspsecuserins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsecuserupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspsecuserdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspsecuserget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsecusergetall"; }
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
            sql.Append("SELECT  u.UserId p_userid, u.RoleId p_roleid, r.Role v_role, FirstName p_firstname,");
            sql.Append(" LastName p_lastname, LoginId p_loginid, EmpId p_empid, Department p_department,");
            sql.Append(" u.Active p_active FROM secUsers u, secRole r ");
            sql.Append(" WHERE r.RoleId = u.RoleId AND r.Active = 1");
            if (Convert.ToInt32(parameters[0]) > 0)//UserId
                sql.Append(" AND UserId = " + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//FirstName
                sql.Append(" AND Rtrim(FirstName) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//LastName
                sql.Append(" AND Rtrim(LastName) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//LoginId
                sql.Append(" AND Rtrim(LoginId) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//EmployeeId
                sql.Append(" AND Rtrim(EmpId) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//Department
                sql.Append(" AND Rtrim(Department) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[6]) > 0)//RoleId
                sql.Append(" AND u.RoleId = " + parameters[6].ToString());
            if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//Active
                sql.Append("AND u.Active = " + parameters[7].ToString().Trim());

            return sql.ToString();
        }

        #endregion Stored-Procedures
    
    }
}
