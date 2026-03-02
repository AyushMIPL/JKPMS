using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSalaryCode : DVOBase
    {
        private int _RowID;
        private string _code;
        private decimal? _per_anum;

        public DVOSalaryCode()
        {
            _RowID = 0;
            _code = string.Empty;
            _per_anum = null;
        }

        #region Properties
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
        #endregion Properties

        #region Stored-Procedures

        public string AUTHENTICATION_SPNAME
        {
            get { return "uspsecauthenticate"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspinyscaleins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinyscaleupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinyscaledel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspinscaleget"; }
        }
        public string FIND_DETAIL
        {
            get { return "uspinyscalecheck"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "inyscale"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
       
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT code p_code,per_annum p_per_annum, ");
            sql.Append("rowid p_rowid ");
            sql.Append(" from inyscale  where 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND code LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (Convert.ToDecimal(parameters[1]) != 0)
                if (Convert.ToInt32 (parameters[1])> 0 && parameters[1] != null)
                    sql.Append(" AND per_annum =" + parameters[1].ToString().Trim());

            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND rowid=" + Convert.ToInt32(parameters[2]));

            sql.Append(" order by p_rowid,p_code");

         
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
