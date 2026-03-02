using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstxnoted :DVOBase
    {
        private int _Rowid;
        private string _filename;
        private string _record_key;
        private int _line_no;
        private string _data;

        #region Constructor
        public DVOstxnoted()
        {
            _Rowid = 0;
            _filename = string.Empty;
            _record_key = string.Empty;
            _line_no = 0;
            _data = string.Empty;
        }
         #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        public string record_key
        {
            get { return _record_key ; }
            set { _record_key = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string data
        {
            get { return _data; }
            set { _data = value; }
        }
         #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpNotesIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpNotesUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpNotesDel"; }
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
            get { return "ApplicationNotes"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //Using in Print Purchase ordre report
        public string GETstxnoted
        {
            get { return "uspordnotesget"; }
        }
        //Get all stxnoted where filename='stuordre'
        public string GetAllStxnoted
        {
            get { return "uspordnotesgetall"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT s1.fileid v_rowid,s1.filenam v_filename, ");
            sql.Append(" s1.record_key v_record_key,s1.line_no v_line_no, s1.data v_data");
            sql.Append(" FROM ApplicationNotes s1 WHERE 1=1");

            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND s1.fileid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND s1.filenam = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(s1.record_key) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) > 0)
                    sql.Append(" AND s1.line_no = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(s1.data) LIKE '%" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append("order by s1.record_key");

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
