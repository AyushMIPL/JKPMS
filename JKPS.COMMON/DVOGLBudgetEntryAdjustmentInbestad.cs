using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLBudgetEntryAdjustmentInbestad : DVOBase
    {
        private int _RowId;
        private int _estid;
        private int _lineno;
        private decimal _adjustment;
        private string _enteredon;//datetime
        private string _enteredby;
        private string _comment;

        #region Constructor

        public DVOGLBudgetEntryAdjustmentInbestad()
        {
            _RowId = 0;
            _estid = 0;
            _lineno = 0;
            _adjustment = 0;
            _enteredon = "01/01/1900";//datetime
            _enteredby = string.Empty;
            _comment = string.Empty;
        }

        #endregion Constructor

        #region Public Property

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int estid
        {
            get { return _estid; }
            set { _estid = value; }
        }
        public int lineno
        {
            get { return _lineno; }
            set { _lineno = value; }
        }
        public decimal adjustment
        {
            get { return _adjustment; }
            set { _adjustment = value; }
        }
        public string enteredon
        {
            get { return _enteredon; }
            set { _enteredon = value; }
        }
        public string enteredby
        {
            get { return _enteredby; }
            set { _enteredby = value; }
        }
        public string comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        #endregion Public Property

        #region Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspGLBgtEntrAdjIns"; }//uspglbgtentradjins
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLBgtEntrAdjUpd"; }//uspglbgtentradjupd
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLBgtEntrAdjDel"; }//uspglbgtentradjdel
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLBgtEntrAdjGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "inbestad"; }
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
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT RowId v_RowId,estid v_estid,lineno v_lineno,adjustment v_adjustment,");
            sql.Append(" enteredon v_enteredon,enteredby v_enteredby,comment v_comment");
            sql.Append(" FROM inbestad WHERE 1=1 ");

            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" AND RowId = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)//estid
                sql.Append(" AND estid = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)//lineno
                sql.Append(" AND lineno = " + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Contains("1900"))//enteredon
                    sql.Append(" AND enteredon = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)//enteredby
                    sql.Append(" AND Rtrim(enteredby) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)//comment
                    sql.Append(" AND Rtrim(comment) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            return sql.ToString();
        }

        #endregion Procedures
    }
}
