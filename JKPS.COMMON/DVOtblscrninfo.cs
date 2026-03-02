using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOtblscrninfo : DVOBase
    {
        int _rowid;
        string _screen_type;
        string _screen_name;
        string _primary_tblname;
        string _record_key;
        string _notes;
        string _date_entered;
        int _entered_by;
        string _machine_info;
        int _active;

        public DVOtblscrninfo()
        {
            _rowid = 0;
            _screen_type = string.Empty;
            _screen_name = string.Empty;
            _primary_tblname = string.Empty;
            _record_key = string.Empty;
            _notes = string.Empty;
            _date_entered = "01/01/1900";//date,
            _entered_by = 0;
            _machine_info = string.Empty;
            _active = 0;
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string screen_type
        {
            get { return _screen_type; }
            set { _screen_type = value; }
        }
        public string screen_name
        {
            get { return _screen_name; }
            set { _screen_name = value; }
        }
        public string primary_tblname
        {
            get { return _primary_tblname; }
            set { _primary_tblname = value; }
        }
        public string record_key
        {
            get { return _record_key; }
            set { _record_key = value; }
        }
        public string notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        public string date_entered
        {
            get { return _date_entered; }
            set { _date_entered = value; }
        }
        public int entered_by
        {
            get { return _entered_by; }
            set { _entered_by = value; }
        }
        public string machine_info
        {
            get { return _machine_info; }
            set { _machine_info = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }

        #region Stored-Procedures
        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usptblscrninfoupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }
        //
        public override string FIND_SPNAME
        {
            get { return ""; }
        }
        //********sunil till not in use
        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        //***********************
        public override string TABLE_NAME
        {
            get { return "tblscrninfo"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

       
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select screen_type,screen_name,primary_tblname,record_key,notes ");
            sql.Append(" from tblscrninfo ");
            sql.Append(" where active=1  ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(screen_name) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(primary_tblname) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(record_key) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(screen_type) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
