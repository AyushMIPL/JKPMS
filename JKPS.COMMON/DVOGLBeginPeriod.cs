using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLBeginPeriod : DVOBase
    {
        private string _curr_period;
        private string _period;
        private string _curr_year;
        private string _period_year;
        private string _start_date;
        private string _end_date;
        private string _curr_start_date;
        private string _curr_end_date;
        private string _balanced;
        private string _period_closed;
        private int _check;
        private int _acct_no;
        private string _dept;
        private decimal _budget;
        private decimal _activity;
        private decimal _this_month;
        private decimal _balance;
        
       
        #region Constructor

        public DVOGLBeginPeriod()
        {
            _curr_period = string.Empty;
            _period = string.Empty;
            _curr_year = string.Empty;
            _period_year = string.Empty;
            _start_date = string.Empty;
            _end_date = string.Empty;
            _curr_start_date = string.Empty;
            _curr_end_date = string.Empty;
            _balanced = string.Empty;
            _period_closed = string.Empty;
            _check= 0;
            _acct_no = 0;
            _dept = string.Empty;
            _budget=0;
            _activity=0;
            _this_month=0;
            _balance = 0;
             
        }

        #endregion Constructor

        #region Public Properties

        public string curr_period
        {
            get { return _curr_period; }
            set { _curr_period = value; }
        }
        public string period
        {
            get { return _period; }
            set { _period = value; }
        }
        public string curr_year
        {
            get { return _curr_year; }
            set { _curr_year = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public string start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public string curr_start_date
        {
            get { return _curr_start_date; }
            set { _curr_start_date = value; }
        }
        public string curr_end_date
        {
            get { return _curr_end_date; }
            set { _curr_end_date = value; }
        }
        public string balanced
        {
            get { return _balanced; }
            set { _balanced = value; }
        }
        public string period_closed
        {
            get { return _period_closed; }
            set { _period_closed = value; }
        }
        public int check
        {
            get { return _check; }
            set { _check = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string dept
        {
            get { return _dept; }
            set { _dept = value; }
        }
        public decimal budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
        public decimal activity
        {
            get { return _activity; }
            set { _activity = value; }
        }
        public decimal this_month
        {
            get { return _this_month; }
            set { _this_month = value; }
        }
        public decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
       


        #endregion Public Properties

        #region Stored-Procedures

        public string Get_Current_Period_Account
        {
            get { return "uspgetCurAcctInf"; }//uspgetcuracctinf
        }

        public string Get_Budget
        {
            get { return "uspgetBudgetInf"; }
        }

        public string Get_Current_Period
        {
            get { return "uspgetCurrentPrd" ; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspCurrentPrdIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspCurrentPrdUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspCurrentPrdDel"; }
        }
        public override string FIND_SPNAME
        {
            get { return "uspglnewPeriodget"; }
        }
        public string FIND_STGJOURE
        {
            get { return "uspstgjoureget"; }
        }
        public string Find_Master_periodsStartDate
        {
            get { return "uspxperdrstdate"; }
        }
        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public string FIND_Master_periods
        {
            get { return "uspMaster_periodsget"; }
         
        }
        public string FIND_STGTRANR
        {
            get { return "uspstgtranrget"; }
        }
        public string FIND_STXTRANR
        {
            get { return "uspstxtranrget"; }
        }
        public string Get_stxprdr
        {
            get { return "uspgetxperdr"; }
        }
        public string UPDATE_STXCHRTD_CURR
        {
            get { return "uspstxchrtdcurrupd"; }
        }
        public string GET_BUDGET
        {
            get { return "uspstxchrtdbudget"; }
        }
        public string DELETE_STXCHRTD
        {
            get { return "uspstxchrtddel"; }
        }
        public string INSERT_STPRDRNEW
        {
            get { return "uspstprdrIns"; }
        }
        public string GET_NEW_ACCT_INFO
        {
            get { return "uspgetNewAcctInf"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            
            return "";
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
        #endregion Stored-Procedures
    }
}
