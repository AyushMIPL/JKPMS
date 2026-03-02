using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOLoginLog : DVOBase
    {
        private int _UserId;
        private string _LoginId;
        private string _FirstName;
        private string _LastName;
        private DateTime _dateFrom=Convert.ToDateTime("01/01/1900");
        private DateTime _dateto=Convert.ToDateTime("01/01/1900");
        private DateTime _LogOutTime;
        private int _loginlogid;
          #region Constructor

        public DVOLoginLog()
        {
            _UserId = 0;
            _LoginId = string.Empty;
            _dateFrom = Convert.ToDateTime("1/1/1900");
            _dateto = Convert.ToDateTime("1/1/1900");
            _LogOutTime = Convert.ToDateTime("1/1/1900");
            _loginlogid = 0;
       
        }

        #endregion Constructor
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
                set { _LoginId=value;}
        }
        public DateTime dateto
        {
            get { return _dateto; }
            set { _dateto = value; }
        }
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }

        public DateTime LogOutTime
        {
            get { return _LogOutTime; }
            set { _LogOutTime = value; }
        }
        public int loginlogid
        {
            get { return _loginlogid; }
            set { _loginlogid = value; }
        }

        #region Stored-Procedures

        public string AUTHENTICATION_SPNAME
        {
            get { return ""; }
        }

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_SecLoginLogGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
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

        public string INSERT_LOGOUT_TIME
        {
            get { return "USP_LogOutTimeIns"; }

        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT l.UserId p_UserId,l.LoginId p_LoginId,s.FirstName p_FirstName,");
            sql.Append(" s.LastName p_LastName,s.EmpId p_EmpId,s.Department p_Department ,s.Active p_active ,");
            sql.Append(" l.LoginTime p_LoginTime,l.MachineInfo p_MachineInfo,l.success p_success ,r.Role p_Role FROM secLoginLog l  LEFT Outer JOIN secusers s ON l.userid=s.userid LEFT outer JOIN secrole r ON r.RoleId = s.RoleId ");
            sql.Append(" WHERE  1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)//LoginId
                    sql.Append(" AND Rtrim(l.LoginId) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)//FirstName
                if (parameters[1].ToString() != string.Empty)//FirstName
                    sql.Append(" AND Rtrim(s.FirstName)  LIKE '%" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)//FirstName
                if (parameters[2].ToString() != string.Empty)//LastName
                    sql.Append(" AND Rtrim(s.LastName)  LIKE '%" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null && Convert.ToDateTime(parameters[3]) != Convert.ToDateTime("01/01/1900"))//dateFrom
                    //sql.Append(" AND l.LoginTime >= '" + Convert.ToDateTime(parameters[3]).ToString("yyyy-MM-dd hh:mm:ss.fff").Replace("'", "''") + "'");
                    sql.Append(" AND l.LoginTime >= '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (parameters[4]!=null)
                if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null && Convert.ToDateTime(parameters[4]) != Convert.ToDateTime("01/01/1900"))//dateFrom
                    sql.Append(" AND l.LoginTime <= '" + parameters[4].ToString().Replace("'", "''") + "'");

            //************************************************************

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
