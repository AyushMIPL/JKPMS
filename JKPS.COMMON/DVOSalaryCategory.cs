using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By: Rahul Jain on 11/12/2008
    //Aim : Use for salary category 
    public class DVOSalaryCategory : DVOBase
    {
        private int _RowID;
        private string _code;
        private string _desc;
        private string _scale_code;

        public DVOSalaryCategory()
        {
            _RowID = 0;
            _code = string.Empty;
            _scale_code = string.Empty;
            _desc = string.Empty;
        }
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Scale_code
        {
            get { return _scale_code; }
            set { _scale_code = value; }
        }
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        #region Stored-Procedures

        //******************  Added by Bharat Dhall [19 December, 2008] ************
        public string GET_MIN_MAX_SCALE_CODE
        {
            get { return "upsCatMinMaxSclCod"; }
        }
        //**************************************************************************

        public override string INSERT_SPNAME
        {
            get { return "uspinycateeins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinycateeupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspinycateeGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspinyscalegetAll"; }
        }
        public string FIND_DETAIL
        {
            get { return "uspinycatscaleget"; }
        }
        public string FIND_DETAIL_BY_SCALECODE
        {
            get { return "uspperannumget"; }
        }
        public override string TABLE_NAME
        {
            get { return "inycatee"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT code p_code,desc p_desc, ");
            sql.Append(" rowid p_rowid ");
            sql.Append(" from inycatee where 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND code LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND desc LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");


            if (Convert.ToInt32(parameters[2]) > 0)//rowid
                sql.Append(" AND rowid = " + parameters[2].ToString());

            sql.Append(" order by code");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
