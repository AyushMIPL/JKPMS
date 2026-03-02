using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By: Rahul Jain on 11/12/2008
    //Aim : Use for update salary scale Category
    public class DVOSalaryScale : DVOBase
    {
        private string _code;
        private decimal? _per_anum;
        private string _cat_code;
        private int _line_no;
        private string _scale_code;
        private string _desc;
        private string _firstcode; 
        private string _lastcode;
        private int _RowID;

        public DVOSalaryScale()
        {
            _RowID = 0;
            _code = string.Empty;
            _per_anum = null;
            _cat_code = string.Empty;
            _line_no=0;
            _scale_code = string.Empty;
            _desc = string.Empty;
            _firstcode = string.Empty;
            _lastcode = string.Empty;
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
        public decimal? Per_anum
        {
            get { return _per_anum; }
            set { _per_anum = value; }
        }
        public string Cat_code
        {
            get { return _cat_code; }
            set { _cat_code = value; }
        }
        public int Line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
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
        public string First_code
        {
            get { return _firstcode; }
            set { _firstcode = value; }
        }
        public string Last_code
        {
            get { return _lastcode; }
            set { _lastcode = value; }
        }

        #region Stored-Procedures

        public string AUTHENTICATION_SPNAME
        {
            get { return "uspsecauthenticate"; }
        }

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
            get { return "uspinysalarycatdel"; }
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
        public  string DELETE_SALARY_DETAIL_BY_CODE
        {
            get { return "uspinycatDtlDel"; }
        }
        public string INSERT_SALARY_DETAIL_BY_CODE
        {
            get { return "uspinycatDtlins"; }
        }
        public string FIND_DETAIL_BY_CODE
        {
            get { return "usppaysalposget"; }
        }
        //public string FIND_LAST_CODE
        //{
        //    get { return ""; }
        //}
        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0 ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string FIND_SALARY_CODE
        {
            get { return "uspsalarycodebysel"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ic.code p_code,ic.desc p_desc, ");
            sql.Append("id.line_no p_line_no,id.scale_code p_scale_code,id.cat_code p_cat_code,ic.rowid p_rowid ");
            sql.Append(" from inycatee ic,inycated id where ic.code=id.cat_code ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND ic.code LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND ic.desc LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append(" order by ic.code,id.line_no");
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
