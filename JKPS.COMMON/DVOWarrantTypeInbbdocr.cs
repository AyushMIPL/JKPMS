using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOWarrantTypeInbbdocr : DVOBase
    {
        private string _Type;
        private string _Description;
        private string _MustBalance;
        private string _BudApprov;
        private string _FullKeyRequired;
        private string _StartEndRequired;
        private string _DollarsOrPercent;

        #region Constructor

        public DVOWarrantTypeInbbdocr()
        {
            _Type = string.Empty;
            _Description = string.Empty;
            _MustBalance = string.Empty;
            _BudApprov = string.Empty;
            _FullKeyRequired = string.Empty;
            _StartEndRequired = string.Empty;
            _DollarsOrPercent = string.Empty;
        }

        #endregion Constructor

        #region Properties

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string MustBalance
        {
            get { return _MustBalance; }
            set { _MustBalance = value; }
        }

        public string BudApprov
        {
            get { return _BudApprov; }
            set { _BudApprov = value; }
        }

        public string FullKeyRequired
        {
            get { return _FullKeyRequired; }
            set { _FullKeyRequired = value; }
        }

        public string StartEndRequired
        {
            get { return _StartEndRequired; }
            set { _StartEndRequired = value; }
        }

        public string DollarsOrPercent
        {
            get { return _DollarsOrPercent; }
            set { _DollarsOrPercent = value; }
        }

        #endregion Properties

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
            get { return "uspWarntypeGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspWarntypeGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT type p_type,desc p_desc,mustbalance p_mustbalance,budapprov p_budapprov,");
            sql.Append(" fullkeyreqd p_fullkeyreqd,startendreqd p_startendreqd,dollarsorpercnt p_dollarsorpercent");
            sql.Append(" FROM inbbdocr");
            sql.Append(" WHERE 1=1");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND type = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND desc = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND mustbalance = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND budapprov = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND fullkeyreqd = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND startendreqd = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND dollarsorpercnt = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
