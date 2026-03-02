using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)     Form For New Close Account(NSS)                    Rajeev(D)                                    08/12/2008(DD)
    ///2.) 
    ///<summery>
   public class DVONssCloseAccount :DVOBase
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

       private string _caseMode;

        ///<summary>
        /// Variable Used To Store The Data of TotalAmount
        /// </summary>
       private decimal _TotalReyPaymentAmount;
       private decimal _DethInterestRate;
       private bool _DeathChecked;




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
       private string _repaid_notes;

       public DVONssCloseAccount()
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
           _nss_status = "ACTIVE";
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

           _TotalReyPaymentAmount = 0.0M;
           _DethInterestRate = 0.0M;
           _DeathChecked = false;
           _repaid_notes = string.Empty;

           _caseMode = string.Empty;

       }

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

       public decimal TotalReyPaymentAmount
       {
           get { return _TotalReyPaymentAmount; }
           set { _TotalReyPaymentAmount = value; }
       }

       public decimal DethInterestRate
       {
           get { return _DethInterestRate; }
           set { _DethInterestRate = value; }
       }

       public bool DeathChecked
       {
           get { return _DeathChecked; }
           set { _DeathChecked = value; }
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

       public string repaid_notes
       {
           get { return _repaid_notes; }
           set { _repaid_notes = value; }
       }

       public string caseMode
       {
           get { return _caseMode; }
           set { _caseMode = value; }
       }
       #endregion Properties

       #region Stored-Procedures

       public override string INSERT_SPNAME
       {
           get { return ""; }
       }

       public override string UPDATE_SPNAME
       {
           get { return "uspstopcntupd"; }
       }

       public override string DELETE_SPNAME
       {
           get { return ""; }
       }

       public override string FIND_SPNAME
       {
           get { return ""; }
       }

       public override string ALL_SPNAME
       {
           get { return ""; }
       }

       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();
           sql.Append("select rowid v_rowid, account_no v_account_no ,contract_no v_contract_no,contract_date v_contract_date ,last_name v_last_name ,first_name v_first_name ,title v_title,");
           sql.Append("address_1 v_address_1,address_2 v_address_2,telephone v_telephone,defaulted_count v_defaulted_count,current_bal v_current_bal,nss_status v_nss_status,notes v_notes,");
           sql.Append("no_of_payments v_no_of_payments,interest_pc v_interest_pc,last_period v_last_period,interest_paid v_interest_paid,date_paid v_date_paid,bonus_months v_bonus_months,");
           sql.Append("bonus_paid v_bonus_paid,monthly_contrib v_monthly_contrib,bonus_date v_bonus_date,anniv_date4 v_anniv_date4,anniv_date5 v_anniv_date5,date_stopped v_date_stopped,");
           sql.Append("stop_months v_stop_months,stop_pc v_stop_pc,stop_count v_stop_count,paid_by_operator v_paid_by_operator,calculate_now v_calculate_now,calc_months v_calc_months,calc_anniv v_calc_anniv,");
           sql.Append("calc_anniv_mth v_calc_anniv_mth from nss_clients where 1=1 AND nss_status <> 'REPAID'");

           if (parameters[0] != null)
               if (parameters[0].ToString() != string.Empty)
                   sql.Append(" AND  account_no LIKE '" + parameters[0].ToString().Replace("'", "''") + "'");
           if (parameters[1] != null)
               if (parameters[1].ToString() != string.Empty)
                   sql.Append(" AND  contract_no=" + "'" + parameters[1].ToString().Replace("'", "''") + "'");
           if (parameters[2] != null)
               if (parameters[2].ToString() != "1/1/1900" && !parameters[2].ToString().Contains("1900"))
                   sql.Append(" AND  contract_date=" + "'" + parameters[2].ToString().Replace("'", "''") + "'");
           if (parameters[3] != null)
               if (parameters[3].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(last_name) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
           if (parameters[4] != null)
               if (parameters[4].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(first_name) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
           if (parameters[5] != null)
               if (parameters[5].ToString() != string.Empty)
                   if(parameters[5].ToString() != "0.0")
                   sql.Append(" AND  monthly_contrib=" +  parameters[5].ToString());
           if (parameters[6] != null)
               if (parameters[6].ToString() != "1/1/1900" && !parameters[6].ToString().Contains("1900"))
                   sql.Append(" AND  bonus_date=" + "'" + parameters[6].ToString().Replace("'", "''") + "'");
           if (parameters[7] != null)
               if (parameters[7].ToString() != "1/1/1900" && !parameters[7].ToString().Contains("1900"))
                   sql.Append(" AND  anniv_date4=" + "'" + parameters[7].ToString().Replace("'", "''") + "'");
           if (parameters[8] != null)
               if (parameters[8].ToString() != "1/1/1900" && !parameters[8].ToString().Contains("1900"))
                   sql.Append(" AND  anniv_date5=" + "'" + parameters[8].ToString().Replace("'", "''") + "'");
           if (parameters[9] != null)
               if (parameters[9].ToString() != string.Empty)
                   if (parameters[9].ToString() != "0.0")
                   sql.Append(" AND  current_bal=" + parameters[9].ToString());
           if (parameters[10] != null)
               if (parameters[10].ToString() != string.Empty)
                   if (Convert.ToUInt32(parameters[10].ToString())!= 0)
                   sql.Append(" AND  no_of_payments=" +  parameters[10].ToString());
          
           

           if (parameters[11] != null)
               if (parameters[11].ToString() != string.Empty)
                   sql.Append(" AND  nss_status=" + "'" + parameters[11].ToString().Replace("'", "''") + "'");



           if (parameters[12] != null)
               if (parameters[12].ToString() != string.Empty)
                   if (Convert.ToUInt32(parameters[12].ToString()) != 0)
                   sql.Append(" AND  defaulted_count=" +parameters[12].ToString() );
       //********************Added by Sunil Pahwa *******************
       if (Convert.ToInt32(parameters[13]) > 0)//rowid
           sql.Append(" AND rowid = " + parameters[13].ToString());
       //************************************************************************
           
           return sql.ToString();
       }

       public override string TABLE_NAME
       {
           get { return "nss_clients"; }
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

       ///<summery>
       /// Stored Procudure used to calculate the average Balence within the period
       ///</summery>
  

       public string AvgBet9To12
       {
           get { return "uspNsscalL9Get"; }
       }

       public string AVGBal1To12
       {
           get { return "uspNsscal112Get"; }
       }

       public string AVGBal13To24
       {
           get { return "uspNsscalc1324Get"; }
       }

       public string AVGBal25To35
       {
           get { return "uspNsscal2535Get"; }
       }
       //Added By Rahul Jain on 08-04-2009 for getting avg balance from 13 to 24 period 
       public string AVGBal1324
       {
           get { return "uspNsscal13To24Gt"; }
       }
       public string AVGBal112
       {
           get { return "uspNsscal1To12Get"; }
       }

       #endregion Stored-Procedures
    }
}
