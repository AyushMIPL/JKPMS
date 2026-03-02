using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPostGLDetail:DVOBase
    {
        private string _post_or_check;
        private string _orig_journal;
        private Int32 _doc_no;
        private Int32 _post_no;
        private DateTime _post_date;
        private DateTime _doc_date;
        private string _ref_code;
        private string _doc_desc;
        private Int32 _acct_no;
        private string _department;
        private decimal _amount;
        private string _debit_credit;
        private string _period_month;
        private string _period_year;
        private string _CurrMonth;
        private string _CurrYear;
        // Table Stxchrtd
        private decimal _Activity;
        private decimal _balance;
        private decimal _this_month;
        private decimal _budget;
        //Table MasterCompany
        private int _income;

        public DVOPostGLDetail()
        {
            _post_or_check = string.Empty;
         
            _doc_no = 0;
            _post_no = 0;
            _post_date = Convert.ToDateTime("01/01/1900");
            _doc_date = Convert.ToDateTime("01/01/1900");
            _ref_code = string.Empty;
            _doc_desc = string.Empty;
            _acct_no = 0;
            _department = string.Empty;
            _amount = 0.0M;
            _debit_credit = string.Empty;
            _period_month = string.Empty;
            _period_year = string.Empty;
            _Activity = 0.0M;
            _balance = 0.0M;
            _budget = 0.0M;
            _this_month = 0.0M;
            _CurrMonth = string.Empty;
            _CurrYear = string.Empty;
            _income = 0;
            _orig_journal = string.Empty;
        }
        public int Income
        {
            get { return _income; }
            set { _income = value; }
        }
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }
        public string post_or_check
        {
            get { return _post_or_check; }
            set { _post_or_check = value; }
        }
        public Int32 doc_no
       {
           get { return _doc_no; }
           set { _doc_no = value; }
       }
        public Int32 post_no
       {
           get { return _post_no; }
           set { _post_no = value; }
       }

       public DateTime post_date
       {
           get { return _post_date; }
           set { _post_date = value; }
       }
       public DateTime doc_date
       {
           get { return _doc_date; }
           set { _doc_date = value; }
       }
       public string ref_code
       {
           get { return _ref_code; }
           set { _ref_code = value; }
       }
       public string doc_desc
       {
           get { return _doc_desc; }
           set { _doc_desc = value; }
       }
      
       public Int32 acct_no
       {
           get { return _acct_no; }
           set { _acct_no = value; }
       }
       public string department
       {
           get { return _department; }
           set { _department = value; }
       }
       public decimal amount
       {
           get { return _amount; }
           set { _amount = value; }
       }
       public string debit_credit
       {
           get { return _debit_credit; }
           set { _debit_credit = value; }
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
        public decimal Activity
        {
            get { return _Activity; }
            set { _Activity = value; }
        }
        public decimal balance
        {
            get { return _balance; }
            set {_balance=value; }

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
        public string Curr_month
        { get { return _CurrMonth; }
          set { _CurrMonth = value; }
        }
        public string Curr_Year
        {
            get { return _CurrYear; }
            set { _CurrYear = value; }
        }
        public override string INSERT_SPNAME
        {
            get { return "uspstxcrtdIns"; }
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
            get { return "uspstxcrtdget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspxcntrc_inc"; }
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
        public string UpdTranar
        {
            get { return "uspstgtranrupd"; }
        }
        public string Postretained
        {
            get { return "usppostretained"; }
        }
        public string updstxchtd
        { get { return "uspstxcrtdupd"; } }

        public string GET_PERIOD
        {
            get { return "uspperiodget"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            return sql.ToString();
        }
    }
}
