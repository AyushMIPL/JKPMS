using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOEmployeeInfoLogEmpUpdLog : DVOBase
    {
        int _RowId;
        string _empl_code;
        string _field_name;
        string _old_value;
        string _new_value;
        int _update_by;
        string _update_machine;
        string _update_date;//datetime
        string _startdate;
        string _enddate;
        #region Constructor

        public DVOEmployeeInfoLogEmpUpdLog()
        {
            _RowId = 0;
            _empl_code = string.Empty;
            _field_name = string.Empty;
            _old_value = string.Empty;
            _new_value = string.Empty;
            _update_by = 0;
            _update_machine = string.Empty;
            _update_date = "01/01/1900";
            _startdate = string.Empty;
            _enddate = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string field_name
        {
            get { return _field_name; }
            set { _field_name = value; }
        }
        public string old_value
        {
            get { return _old_value; }
            set { _old_value = value; }
        }
        public string new_value
        {
            get { return _new_value; }
            set { _new_value = value; }
        }
        public int update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
        }
        public string update_machine
        {
            get { return _update_machine; }
            set { _update_machine = value; }
        }
        public string update_date
        {
            get { return _update_date; }
            set { _update_date = value; }
        }

        public string startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        public string enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }
        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpInfoLogIns"; }
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "EmployeeUpdLog"; }
        }
        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT EmployeeUpdLog.updlogid,EmployeeUpdLog.field_name,EmployeeUpdLog.old_value,EmployeeUpdLog.new_value,");
            sql.Append(" EmployeeUpdLog.update_by,EmployeeUpdLog.update_machine,EmployeeUpdLog.update_date,secusers.loginid,EmployeeUpdLog.empl_code");
            sql.Append(" FROM EmployeeUpdLog,secusers,MasterEmployee WHERE EmployeeUpdLog.update_by = secusers.userid and EmployeeUpdLog.empl_code=MasterEmployee.empl_code");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim() != string.Empty)
                    sql.Append(" AND EmployeeUpdLog.empl_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim() != string.Empty)
                    sql.Append(" AND MasterEmployee.soc_sec_num = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" AND EmployeeUpdLog.update_by = " + parameters[2].ToString());

            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" AND date(EmployeeUpdLog.update_date) >= date('" + parameters[3].ToString().Replace("'", "''") + "')");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" AND date(EmployeeUpdLog.update_date) <= date('" + parameters[4].ToString().Replace("'", "''") + "')");
            
   

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
