using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOEmpTypLogEmpsaltyplog : DVOBase
    {
        int _RowId;
        string _emp_code;
        string _type_code;
        string _date_assigned;//datetime
        string _date_changed;//datetime
        int _active;
        int _update_by;
        string _update_date;//datetime
        string _update_machine;

        #region Constructor

        public DVOEmpTypLogEmpsaltyplog()
        {
            _RowId = 0;
            _emp_code = string.Empty;
            _type_code = string.Empty;
            _date_assigned = "01/01/1900";//datetime
            _date_changed = "01/01/1900";//datetime
            _active = -1;
            _update_by = 0;
            _update_date = "01/01/1900";//datetime
            _update_machine = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string emp_code
        {
            get { return _emp_code; }
            set { _emp_code = value; }
        }
        public string type_code
        {
            get { return _type_code; }
            set { _type_code = value; }
        }
        public string date_assigned
        {
            get { return _date_assigned; }
            set { _date_assigned = value; }
        }
        public string date_changed
        {
            get { return _date_changed; }
            set { _date_changed = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
        public int update_by
        {
            get { return _update_by; }
            set { _update_by = value; }
        }
        public string update_date
        {
            get { return _update_date; }
            set { _update_date = value; }
        }
        public string update_machine
        {
            get { return _update_machine; }
            set { _update_machine = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpTypLogIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpTypLogUpd"; }
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
            get { return "EmpSalTypLog"; }
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
            sql.Append("SELECT EmpSalTypLog.updlogid,EmpSalTypLog.emp_code,EmpSalTypLog.type_code,");
            sql.Append(" EmpSalTypLog.date_assigned,EmpSalTypLog.date_changed,EmpSalTypLog.active,");
            sql.Append(" EmpSalTypLog.update_by,EmpSalTypLog.update_date,EmpSalTypLog.update_machine, secusers.loginid");
            sql.Append(" FROM EmpSalTypLog, secusers");
            sql.Append(" where EmpSalTypLog.update_by = secusers.userid");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim() != string.Empty)
                    sql.Append(" AND EmpSalTypLog.emp_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND EmpSalTypLog.update_by = " + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty && !parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND date(EmpSalTypLog.update_date) = date('" + parameters[2].ToString().Trim().Replace("'", "''") + "')");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
