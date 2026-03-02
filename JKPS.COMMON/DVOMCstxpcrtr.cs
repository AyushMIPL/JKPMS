using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 27-07-2009 get or set property of stxpcrtr table.
    //Use in Multicurrency Module
    public class DVOMCstxpcrtr : DVOBase
    {
        private int _Rowid;
        private string _from_currency_code;
        private string _to_currency_code;
        private string _rate_type;
        private string _period;
        private string _period_year;
        private decimal _rate;

        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        public DVOMCstxpcrtr()
        {
            _Rowid = 0;
            _from_currency_code = string.Empty;
            _to_currency_code = string.Empty;
            _rate_type = string.Empty;
            _period = string.Empty;
            _period_year = string.Empty;
            _rate = 0.0M;


            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

        }

        #region Public Property

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
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
        public string period
        {
            get { return _period; }
            set { _period = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public decimal rate 
        {
            get { return _rate; }
            set { _rate = value; }
        }
        //Properties used for only SQL Server

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }

        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }

        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        #endregion


        #region Stored Procedure

        public override string INSERT_SPNAME
        {
            get { return "uspstxpcrtrins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstxpcrtrupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstxpcrtrdel"; }
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
            get { return "stxpcrtr"; }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,from_currency_code ,to_currency_code,");
            sql.Append(" rate_type ,period ,period_year, rate from stxpcrtr ");
            sql.Append(" where 1=1");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)
                sql.Append(" and from_currency_code = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)
                sql.Append(" and rate_type LIKE '" + parameters[1].ToString().Replace("'", "''") + "'");

            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)
                sql.Append(" and period = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)
                sql.Append(" and period_year = '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[4]) > 0)
                sql.Append(" and rate = " + parameters[4].ToString());

            if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)
                sql.Append(" and to_currency_code = '" + parameters[5].ToString().Replace("'", "''") + "'");
            
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Order Status
        /// Created By : Rahul
        /// Created Date : 27/07/09
        /// </summary>
        public string FINDQUERY_PERIODEXCHENGE_RATES(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stxcrtpr.rate_desc,stxcurrr.description, ");
            sql.Append(" stxpcrtr.from_currency_code,stxpcrtr.period, ");
            sql.Append(" stxpcrtr.period_year,stxpcrtr.rate,stxpcrtr.rate_type ");
            sql.Append(" FROM  stxpcrtr,stxcrtpr,stxcurrr  ");
            sql.Append(" WHERE stxcurrr.currency_code = stxpcrtr.from_currency_code  ");
            sql.Append(" and stxcrtpr.rate_type = stxpcrtr.rate_type ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" and stxpcrtr.from_currency_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stxpcrtr.rate_type = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stxpcrtr.period = '" + parameters[2].ToString() + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stxpcrtr.period_year = '" + parameters[3].ToString() + "'");
            if (Convert.ToDecimal(parameters[4]) > 0)
                sql.Append(" AND stxpcrtr.rate =" + parameters[4]);
            sql.Append(" ORDER BY stxpcrtr.from_currency_code ");
            return sql.ToString();
        }

        #endregion Stored Procedures
    }
}
