using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DAL;


namespace JKPS.COMMON
{
    /// <summary>
    /// DVO for Form "Update Daily Exchange Rates"
    /// Created by : Shrishanshu on 250709
    /// BLL : BLLUpdDailyExRates
    /// tbl :  stxdcrtr
    /// </summary>
    public class DVOUpdDailyExRates : DVOBase
    {
        #region Private Declaration

        private string _from_currency_code;
        private string _to_currency_code;
        private string _rate_type;
        private DateTime _currency_date;
        private decimal _rate;
        private int _rowid;

        #endregion Private Declaration

        #region Public Memebers

        public DVOUpdDailyExRates()
        {
            _from_currency_code = string.Empty;
            _to_currency_code = string.Empty;
            _rate_type = string.Empty;
            _currency_date = Convert.ToDateTime(null);
            _rate = 0.0M;
            _rowid = 0;

        }
        public string from_currency_code
        {
            get { return _from_currency_code; }
            set { _from_currency_code = value; }
        }
        public string to_currency_code
        {
            get { return _to_currency_code; }
            set { _to_currency_code = value; }
        }
        public string rate_type
        {
            get { return _rate_type; }
            set { _rate_type = value; }
        }
        public DateTime currency_date
        {
            get { return _currency_date; }
            set { _currency_date = value; }
        }
        public decimal rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        #endregion Public Members

        #region Stored Procedure

        public override string INSERT_SPNAME
        {
            get { return "uspstxdcrtrins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstxdcrtrupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstxdcrtrdel"; }
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
            get { return "stxdcrtr"; }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select  rowid , from_currency_code ,to_currency_code,");
            sql.Append(" rate_type ,currency_date , rate from stxdcrtr ");
            sql.Append(" where 1=1");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)
                sql.Append(" and from_currency_code = '" + parameters[0].ToString().Replace("'", "''") + "'");
            
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)
                sql.Append(" and to_currency_code LIKE '" + parameters[1].ToString().Replace("'", "''") + "'");
           
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)
                sql.Append(" and rate_type LIKE '" + parameters[2].ToString().Replace("'", "''") + "'");
           
            if (parameters[3] !=null)
                if (Convert.ToDateTime( parameters[3])!=Convert.ToDateTime(null))
               sql.Append(" and currency_date = '" + Convert.ToDateTime(parameters[3]).ToString("MM/dd/yyyy")+"'");
            
            if (Convert.ToDecimal(parameters[4]) > 0)
                sql.Append(" and rate = " + parameters[4].ToString());
           
            return sql.ToString();
        }

        #endregion Stored Procedures
    }
}
