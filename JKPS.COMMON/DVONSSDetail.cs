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
    public class DVONSSDetail : DVOBase
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

        //Added By Rahul Jain on 21-07-09
        private int _cash_received;

        private int _batch_id;
        private int _ar_doc_ref;
        //Constructor used to initialize the class data member.
        #region public Constructor

        public DVONSSDetail()
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
            _postorcheck = string.Empty;
            _seq_no = 0;

            //****************************************************************************
            _cash_received = 0;
            _batch_id = 0;
            _ar_doc_ref = 0;
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

        public int cash_received
        {
            get { return _cash_received; }
            set { _cash_received = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public int ar_doc_ref
        {
            get { return _ar_doc_ref; }
            set { _ar_doc_ref = value; }
        }

        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspnssContIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspnssContUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspnssContDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspNSSDetailGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        //***************Added By Rahul jain on 2/12/2008********************
        public string FIND_NSS_REPAYEMNTS_BY_YEAR
        {
            get { return "uspNSSRepaidDtlGet"; }
        }
        //********************************************************************
        //***************Added By Rahul jain on 4/12/2008********************
        public string FIND_NSS_CONTRIBUTION_BY_YEAR
        {
            get { return "uspNSSContriDtlGet"; }
        }
        //********************************************************************
        //***************Added By Rahul jain on 4/12/2008********************
        public string FIND_NSS_MONTHLY_DETAIL
        {
            get { return "uspNSSMonthDtlGet"; }
        }
        //********************************************************************
        //***************Added By Rahul jain on 5/12/2008********************
        public string FIND_NSS_TRANSEDIT_DETAIL
        {
            get { return "uspNSSEditListGet"; }
        }
        //***************Added By Rahul jain on 04/18/2009********************
        public string GET_NSSCLIENT_BY_CONTRACT_DATE
        {
            get { return "uspnssclientsget"; }
        }
        //********************************************************************
        //***************Added By Rahul jain on 04/18/2009********************
        public string GET_NSSDETAIL_ALL
        {
            get { return "uspnssdetailget"; }
        }
        //********************************************************************
        public string GET_NSS_TRANS_DATA
        {
            get { return "uspnsstranget"; }
        }
        public string GET_NSS_DEP_WED
        {
            get { return "uspnssdepwedget"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT nc.account_no p_account_no, nc.contract_no p_contract_no,nc.contract_date p_contract_date,nc.last_name p_last_name, nc.first_name p_first_name,");
            sql.Append(" nc.title p_title, nc.address_1 p_address_1,nc.address_2 p_address_2,nc.telephone p_telephone,nc.defaulted_count p_defaulted_count,nc.current_bal p_current_bal,");
            sql.Append(" nc.nss_status p_nss_status,nc.notes p_notes, nc.no_of_payments p_no_of_payments,nc.interest_pc p_interest_pc,nc.last_period p_last_period,");
            sql.Append(" nc.interest_paid p_interest_paid,nc.date_paid p_date_paid, nc.bonus_months p_bonus_months,nc.monthly_contrib p_monthly_contrib,");
            sql.Append(" nc.bonus_paid p_bonus_paid, nc.bonus_date p_bonus_date,nc.anniv_date4 p_anniv_date4,nc.anniv_date5 p_anniv_date5,nc.date_stopped p_date_stopped,nc.stop_months p_stop_months, nc.stop_pc p_stop_pc,");
            sql.Append(" nc.stop_count p_stop_count,nc.paid_by_operator p_paid_by_operator,nc.calculate_now p_calculate_now,nc.calc_months p_calc_months, nc.calc_anniv p_calc_anniv,");
            sql.Append(" nc.calc_anniv_mth p_calc_anniv_mth,nd.payment_date p_payment_date  , nd.for_period p_for_period ,nd.voucher_no p_voucher_no  , nd.amount p_amount,nd.run_total p_run_total , nd.trans_flag p_trans_flag");
            sql.Append(" from nss_clients nc,outer nss_detail nd where nc.account_no=nd.account_no");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND nc.account_no=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(nc.nss_status) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(nc.first_name) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(nc.last_name) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append(" order by nd.payment_date ,nd.for_period ");
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
        public string FIND_ACCT_DETAIL(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT nc.account_no,nc.contract_no,nc.last_name, ");
            sql.Append(" nc.first_name,nc.title,nc.nss_status,nc.date_paid, ");
            sql.Append(" nd.payment_date,Sum(nd.amount) ");
            sql.Append(" FROM nss_clients nc,nss_detail nd ");
            sql.Append(" WHERE nc.account_no=nd.account_no ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND nc.account_no=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(nc.nss_status) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty && !parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND nc.contract_date >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND nc.contract_date <= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            sql.Append(" group by nc.account_no,nc.contract_no,nc.last_name,nc.first_name,");
            sql.Append(" nc.title,nc.nss_status,nc.date_paid,nd.payment_date");
            sql.Append(" order by nc.account_no,nd.payment_date ");
            return sql.ToString();
        }
        #endregion Stored-Procedures

    }
}
