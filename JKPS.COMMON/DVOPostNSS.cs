using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)     DVo For NSS Client Details                    Rahul(D)                                    29/11/2008(DD)
    ///2.) 
    ///<summery>
    public class DVOPostNSS : DVOBase
    {
        //Declare Private Variable
        private string _account_no;
        private string _contract_no;
        private DateTime _contract_date;
        private string _last_name;
        private string _first_name;
        private string _title;
        private string _address_1;
        private string _address_2;
        private string _telephone;
        private int _defaulted_count;


        private decimal _current_bal;
        private string _nss_status;
        private string _notes;
        private int _no_of_payments;
        private decimal _interest_pc;
        private int _last_period;
        private decimal _interest_paid;
        private DateTime _date_paid;
        private int _bonus_months;
        private decimal _bonus_paid;

        private decimal _monthly_contrib;
        private DateTime _bonus_date;
        private DateTime _anniv_date4;
        private DateTime _anniv_date5;
        private DateTime _date_stopped;
        private int _stop_months;
        private decimal _stop_pc;
        private int _stop_count;
        private string _paid_by_operator;
        private string _calculate_now;

        private int _calc_months;
        private int _calc_anniv;
        private int _calc_anniv_mth;

        //*******************Added By Rahul Jain on 1/12/2008****************************
        private DateTime _payment_date;
        private int _for_period;
        private string _voucher_no;
        private decimal _amount;
        private decimal _run_total;
        private string _trans_flag;
        private DateTime _dateFrom = Convert.ToDateTime("01/01/1900");
        private DateTime _dateto = Convert.ToDateTime("01/01/1900");
        private string _PaidYear;
        private string _postorcheck;
        private int _seq_no;
        //*******************************************************************************

        /// <summary>
        /// Private variable applicable only for SQL-Server
        /// </summary
        private int _RowID;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        //Constructor used to initialize the class data member.
        #region public Constructor

        public DVOPostNSS()
        {
            _RowID = 0;
            _account_no = string.Empty;
            _contract_no = string.Empty;
            _contract_date = Convert.ToDateTime("01/01/1900");
            _last_name = string.Empty;
            _first_name = string.Empty;
            _title = string.Empty; ;
            _address_1 = string.Empty;
            _address_2 = string.Empty;
            _telephone = string.Empty;
            _defaulted_count = 0;

            _current_bal = 0.0M;
            _nss_status = string.Empty;
            _notes = string.Empty;
            _no_of_payments = 0;
            _interest_pc = 0.0M;
            _last_period = 0;
            _interest_paid = 0.0M;
            _date_paid = Convert.ToDateTime("01/01/1900");
            _bonus_months = 0;
            _bonus_paid = 0.0M;

            _monthly_contrib = 0.0M;
            _bonus_date = Convert.ToDateTime("01/01/1900");
            _anniv_date4 = Convert.ToDateTime("01/01/1900");
            _anniv_date5 = Convert.ToDateTime("01/01/1900");
            _date_stopped = Convert.ToDateTime("01/01/1900");
            _stop_months = 0;
            _stop_pc = 0.0M;
            _stop_count = 0;
            _paid_by_operator = string.Empty;
            _calculate_now = string.Empty;

            _calc_months = 0;
            _calc_anniv = 0;
            _calc_anniv_mth = 0;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

            //*******************Added By Rahul Jain on 1/12/2008*************************
            _payment_date = Convert.ToDateTime("01/01/1900");
            _for_period = 0;
            _voucher_no = string.Empty;
            _amount = 0.0M;
            _run_total = 0.0M;
            _trans_flag = string.Empty;
            _dateFrom = Convert.ToDateTime("1/1/1900");
            _dateto = Convert.ToDateTime("1/1/1900");
            _PaidYear = string.Empty;
            _seq_no = 0;
            _postorcheck = string.Empty;

            //****************************************************************************

        }
        #endregion

        #region StartProperties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }

        public string account_no
        {
            get { return _account_no; }
            set { _account_no = value; }
        }
        public string contract_no
        {
            get { return _contract_no; }
            set { _contract_no = value; }
        }
        public DateTime contract_date
        {
            get { return _contract_date; }
            set { _contract_date = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string address_1
        {
            get { return _address_1; }
            set { _address_1 = value; }
        }
        public string address_2
        {
            get { return _address_2; }
            set { _address_2 = value; }
        }
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
        public int defaulted_count
        {
            get { return _defaulted_count; }
            set { _defaulted_count = value; }
        }
        public decimal current_bal
        {
            get { return _current_bal; }
            set { _current_bal = value; }
        }
        public string nss_status
        {
            get { return _nss_status; }
            set { _nss_status = value; }
        }
        public string notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        public int no_of_payments
        {
            get { return _no_of_payments; }
            set { _no_of_payments = value; }
        }
        public decimal interest_pc
        {
            get { return _interest_pc; }
            set { _interest_pc = value; }
        }
        public int last_period
        {
            get { return _last_period; }
            set { _last_period = value; }
        }
        public decimal interest_paid
        {
            get { return _interest_paid; }
            set { _interest_paid = value; }
        }
        public DateTime date_paid
        {
            get { return _date_paid; }
            set { _date_paid = value; }
        }
        public int bonus_months
        {
            get { return _bonus_months; }
            set { _bonus_months = value; }
        }
        public decimal bonus_paid
        {
            get { return _bonus_paid; }
            set { _bonus_paid = value; }
        }
        public decimal monthly_contrib
        {
            get { return _monthly_contrib; }
            set { _monthly_contrib = value; }
        }
        public DateTime bonus_date
        {
            get { return _bonus_date; }
            set { _bonus_date = value; }
        }
        public DateTime anniv_date4
        {
            get { return _anniv_date4; }
            set { _anniv_date4 = value; }
        }
        public DateTime anniv_date5
        {
            get { return _anniv_date5; }
            set { _anniv_date5 = value; }
        }
        public DateTime date_stopped
        {
            get { return _date_stopped; }
            set { _date_stopped = value; }
        }
        public int stop_months
        {
            get { return _stop_months; }
            set { _stop_months = value; }
        }
        public decimal stop_pc
        {
            get { return _stop_pc; }
            set { _stop_pc = value; }
        }
        public int stop_count
        {
            get { return _stop_count; }
            set { _stop_count = value; }
        }
        public string paid_by_operator
        {
            get { return _paid_by_operator; }
            set { _paid_by_operator = value; }
        }
        public string calculate_now
        {
            get { return _calculate_now; }
            set { _calculate_now = value; }
        }
        public int calc_months
        {
            get { return _calc_months; }
            set { _calc_months = value; }
        }
        public int calc_anniv
        {
            get { return _calc_anniv; }
            set { _calc_anniv = value; }
        }
        public int calc_anniv_mth
        {
            get { return _calc_anniv_mth; }
            set { _calc_anniv_mth = value; }
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
            get
            {
                return _UpdateDate;
            }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        //**************************Added By Rahul jain on 1/12/2008****************
        public DateTime payment_date
        {
            get { return _payment_date; }
            set { _payment_date = value; }
        }
        public int for_period
        {
            get { return _for_period; }
            set { _for_period = value; }
        }
        public string voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public decimal run_total
        {
            get { return _run_total; }
            set { _run_total = value; }
        }
        public string trans_flag
        {
            get { return _trans_flag; }
            set { _trans_flag = value; }
        }
        public DateTime dateto
        {
            get { return _dateto; }
            set { _dateto = value; }
        }
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
        public string PaidYear
        {
            get { return _PaidYear; }
            set { _PaidYear = value; }
        }
        public int seq_no
        {
            get { return _seq_no; }
            set { _seq_no = value; }
        }
        public string postorcheck
        {
            get { return _postorcheck; }
            set { _postorcheck = value; }

        }

        //**************************************************************************


        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspnssdetailins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspupdatenssclient"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspdeletensshdrdtl"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public string FIND_NSS_MAXRUNTOTAL
        {
            get { return "uspNSSMaxRunTotGet"; }
        }
        public string UPDATE_NSS_CLIENT_STATUS
        {
            get { return "uspnssclientupdate"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select no_of_payments npays ,rowid  nrow from nss_clients");
            sql.Append(" where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND nss_clients.account_no=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            return sql.ToString();
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
        public string UPDARDOCNO
        {
            get { return "uspnssdetailupd"; }
        }
        #endregion Stored-Procedures

    }
}
