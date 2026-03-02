using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLstxchrtd : DVOBase
    {
        private int _RowID;
        private int _acct_no;
        private string _department;
        private string _period_month;
        private string _period_year;
        private decimal _activity;
        private decimal _balance;
        private decimal _this_month;
        private decimal _budget;
        private string _incr_with_crdt;

        #region Constructor
        public DVOGLstxchrtd()
        {
            _RowID = 0;
            _acct_no = 0;
            _department = string.Empty;
            _period_month = string.Empty;
            _period_year = string.Empty;
            _activity = 0;
            _balance = 0;
            _this_month = 0;
            _budget = 0;
            _incr_with_crdt = string.Empty;
        }
        #endregion Constructor

        #region Property
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string period_month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public decimal activity
        {
            get { return _activity; }
            set { _activity = value; }
        }
        public decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public decimal this_month
        {
            get { return _this_month; }
            set { _this_month = value; }
        }
        public decimal budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
        public string incr_with_crdt
        {
            get { return _incr_with_crdt; }
            set { _incr_with_crdt = value; }
        }

        #endregion Property

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspxrtdnewIns"; }//uspxrtdnewins
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstxchrtdUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspstxchrtdGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspnewyeardtlget"; }
        }
        public override string TABLE_NAME
        {
            get { return "stxchrtd"; }
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
        public string UPDATE_BALANCE
        {
            get { return "uspstxchrtdBalUpd"; }
        }
        public string Get_Account_details
        {
            get { return "uspgetacctdetails"; }
        }
        public string UpdateBalanceRecalculated
        {
            get { return "uspupdateacctbal"; }
        }
        public string GET_INC_STM
        {
            get { return "uspincstmdataget"; }
        }
        public string GET_COUNT
        {
            get { return "uspxchrtdcnt"; }
        }
        public string GET_TD_PER
        {
            get { return "uspincstmtdget"; }
        }
        public string GET_XPERDR_DATE
        {
            get { return "uspxperdrdate"; }
        }
        public string GET_RECALBAL
        {
            get { return "usprecalbalget"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid,acct_no,department,period_month ||'-'|| period_year v_period_year,");
            sql.Append(" activity,balance,this_month,budget FROM stxchrtd WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) != 0)
                sql.Append(" AND acct_no = " + parameters[0].ToString().Trim());
            if (Convert.ToInt32(parameters[1]) != 0)
                sql.Append(" AND rowid = " + parameters[1].ToString().Trim());

            sql.Append("  order by v_period_year desc");
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
