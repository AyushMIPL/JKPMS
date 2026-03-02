using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOClaimEarnings : DVOBase
    {
        private int _RowId;
        private string _doc_date;
        private string _start_date;
        private string _End_date;
        private string _act_code;

        private string _empl_code;

        public DVOClaimEarnings()
        {
            _RowId = 0;
            _doc_date = string.Empty;
            _act_code = string.Empty;
            _empl_code = string.Empty;
            _start_date = string.Empty;
            _End_date = string.Empty;
        }
        public string doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }

        public string start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string End_date
        {
            get { return _End_date; }
            set { _End_date = value; }
        }

        public string act_code
        {
            get { return _act_code; }
            set { _act_code = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        #region Stored-Procedures

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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "styactvv"; }
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
            sql.Append(" select styactvv.amount, MasterEmployee.first_name,MasterEmployee.last_name,MasterEmployee.middle_name,styactvv.act_code, ");
            sql.Append(" MasterEmployee.empl_code from styactvv, MasterEmployee where styactvv.ref_code = MasterEmployee.empl_code ");
            sql.Append(" and styactvv.act_type = 'B'");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.empl_code) LIKE '" + parameters[0].ToString().Trim() + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(styactvv.act_code) LIKE '" + parameters[1].ToString().Trim() + "%'");

            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//YearTo
                sql.Append(" and styactvv.doc_date >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");//DocdateFrom
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//YearTo
                sql.Append(" and styactvv.doc_date <= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");//DocDateTo 
            sql.Append(" Order by MasterEmployee.last_name, MasterEmployee.first_name, MasterEmployee.middle_name ");
            return sql.ToString();
        }
        #endregion store-procedures

    }
}
