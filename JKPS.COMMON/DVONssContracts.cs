using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVONssContracts : DVOBase
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

    //*******************Added By Rahul Jain on 6/12/2008 ****************************
    /// <summary>
    /// Private variable applicable only for getting NSS_details 
    /// </summary
    private DateTime _payment_date;
    private int _for_period;
    private string _voucher_no;
    private decimal _amount;
    private decimal _run_total;
    private string _trans_flag;
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

        private string _repaid_notes;
       //Constructor used to initialize the class data member.
       public DVONssContracts()
       {
           _RowID = 0;
           _account_no = string.Empty; 
           _contract_no = string.Empty;
          _contract_date=Convert.ToDateTime("01/01/1900");
          _last_name = string.Empty; 
          _first_name = string.Empty;
          _title = string.Empty; ;
          _address_1 = string.Empty;
          _address_2 = string.Empty;
          _telephone = string.Empty;
          _defaulted_count=0;

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

          _calc_months=0;
          _calc_anniv = 0;
          _calc_anniv_mth = 0;

          _InsertMachineInfo = "App";
          _InsertDate = DateTime.Now;
          _InsertBy = -1;
          _UpdateMachineInfo = "App";
          _UpdateDate = DateTime.Now;
          _UpdateBy = -1;

          //*******************Added By Rahul Jain on 6/12/2008*************************
          _payment_date = Convert.ToDateTime("01/01/1900");
          _for_period = 0;
          _voucher_no = string.Empty;
          _amount = 0.0M;
          _run_total = 0.0M;
          _trans_flag = string.Empty;
          //****************************************************************************
          _repaid_notes = string.Empty;
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
       //**************************Added By Rahul jain on 6/12/2008 ****************
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
        public string repaid_notes
        {
            get { return _repaid_notes; }
            set { _repaid_notes = value; }
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
          get { return "uspnssContGet"; }
      }

      public override string ALL_SPNAME
      {
          get { return ""; }
      }
      //***************Added By Rahul jain on 6/12/2008 fro getting Client details by account no ********************
      public string FIND_NSS_TRANS_DETAIL
      {
          get { return "uspNSSClientDtlGet"; }
      }
      //********************************************************************
      public override string FIND_QUERY(ref Object[] parameters)
      {
          System.Text.StringBuilder sql = new StringBuilder();
          sql.Append("select rowid v_rowid, account_no v_account_no ,contract_no v_contract_no,contract_date v_contract_date ,last_name v_last_name ,first_name v_first_name ,title v_title,");
          sql.Append("address_1 v_address_1,address_2 v_address_2,telephone v_telephone,defaulted_count v_defaulted_count,current_bal v_current_bal,nss_status v_nss_status,notes v_notes,");
          sql.Append("no_of_payments v_no_of_payments,interest_pc v_interest_pc,last_period v_last_period,interest_paid v_interest_paid,date_paid v_date_paid,bonus_months v_bonus_months,");
          sql.Append("bonus_paid v_bonus_paid,monthly_contrib v_monthly_contrib,bonus_date v_bonus_date,anniv_date4 v_anniv_date4,anniv_date5 v_anniv_date5,date_stopped v_date_stopped,");
          sql.Append("stop_months v_stop_months,stop_pc v_stop_pc,stop_count v_stop_count,paid_by_operator v_paid_by_operator,calculate_now v_calculate_now,calc_months v_calc_months,calc_anniv v_calc_anniv,");
          sql.Append("calc_anniv_mth v_calc_anniv_mth ,repaid_notes v_repaid_notes from nss_clients where 1=1");

          if (parameters[0] != null)
              if (parameters[0].ToString() != string.Empty)
                  sql.Append(" AND account_no=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
          if (parameters[1] != null)
              if (parameters[1].ToString() != string.Empty)
                  sql.Append(" AND first_name LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
          if (parameters[2] != null)
              if (parameters[2].ToString() != string.Empty)
                  sql.Append(" AND last_name LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
          if (parameters[3] != null)
              if (parameters[3].ToString() != string.Empty)
                  sql.Append(" AND address_1 LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
          if (parameters[4] != null)
              if (parameters[4].ToString() != string.Empty)
                  sql.Append(" AND address_2 LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
          if (parameters[5] != null)
              if (parameters[5].ToString() != string.Empty)
                  sql.Append(" AND telephone=" + "'" + parameters[5].ToString().Replace("'", "''") + "'");
          if (parameters[6] != null)
              if (parameters[6].ToString() != string.Empty)
                  sql.Append(" AND contract_no =" + "'" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
          if (parameters[7] != null)
              if (parameters[7].ToString().Trim().Length <= 0 && !parameters[7].ToString().Trim().Contains("1900") && parameters[7].ToString().Trim() == "01/01/0001")
                  sql.Append(" AND  contract_date=" + "'" + parameters[7].ToString().Trim() + "'");
          if (parameters[8] != null)
              if (parameters[8].ToString() != string.Empty && parameters[8].ToString()!=null && Convert.ToDecimal(parameters[8])!= Convert.ToDecimal("0.0") )
                  sql.Append(" AND  monthly_contrib=" + "'" + parameters[8].ToString().Replace("'", "''") + "'");
          if (parameters[9] != null)
              if (parameters[9].ToString() != string.Empty && parameters[9].ToString()!=null && Convert.ToInt32(parameters[9])!=Convert.ToInt32("0"))
                  sql.Append(" AND  defaulted_count=" + "'" + parameters[9].ToString().Replace("'", "''") + "'");
          if (parameters[10] != null)
              if (parameters[10].ToString() != string.Empty && !parameters[10].ToString().Trim().Contains("1900") && parameters[10].ToString().Trim() == "01/01/0001")
                  sql.Append(" AND bonus_date=" + "'" + parameters[10].ToString().Trim() + "'");
          if (parameters[11] != null)
              if (parameters[11].ToString() != string.Empty && !parameters[11].ToString().Trim().Contains("1900") && parameters[11].ToString().Trim() == "01/01/0001")
                  sql.Append(" AND anniv_date4=" + "'" + parameters[11].ToString().Trim() + "'");
          if (parameters[12] != null)
              if (parameters[12].ToString() != string.Empty && !parameters[12].ToString().Trim().Contains("1900") && parameters[12].ToString().Trim() == "01/01/0001")
                  sql.Append(" AND anniv_date5=" + "'" + parameters[12].ToString().Trim() + "'");
          if (parameters[13] != null)
              if (parameters[13].ToString() != string.Empty && parameters[13].ToString() != null && Convert.ToDecimal(parameters[13]) != Convert.ToDecimal("0.0"))
                  sql.Append(" AND current_bal=" + "'" + parameters[13].ToString().Replace("'", "''") + "'");
          if (parameters[14] != null)
              if (parameters[14].ToString() != string.Empty)
                  sql.Append(" AND nss_status=" + "'" + parameters[14].ToString().Replace("'", "''") + "'");
          if(parameters[15] !=null)
              if (parameters[15].ToString() != string.Empty && !parameters[15].ToString().Trim().Contains("1900") && parameters[15].ToString().Trim() == "01/01/0001")
                  sql.Append(" AND date_stopped=" + "'" + parameters[15].ToString().Trim() + "'");
          if(parameters[16] !=null)
              if(parameters[16].ToString() != string.Empty)
                  sql.Append(" AND notes LIKE " + "'" + parameters[16].ToString().Replace("'", "''") + "%'");
          if(parameters[17] !=null)
              if(parameters[17].ToString()!=string.Empty && parameters[17].ToString()!=null && Convert.ToInt32(parameters[17])!=Convert.ToInt32("0"))
                  sql.Append(" AND no_of_payments=" + "'" + parameters[17].ToString().Replace("'", "''") + "'");
          if (parameters[18] != null)
              if (parameters[18].ToString() != string.Empty && parameters[18].ToString() != null && Convert.ToDecimal(parameters[18]) != Convert.ToDecimal("0.0"))
                  sql.Append(" AND interest_paid=" + "'" + parameters[18].ToString().Replace("'", "''") + "'");
          if (parameters[19] != null)
              if (parameters[19].ToString() != string.Empty && parameters[19].ToString() != null && Convert.ToDecimal(parameters[19]) != Convert.ToDecimal("0.0"))
                  sql.Append(" AND bonus_paid=" + "'" + parameters[19].ToString().Replace("'", "''") + "'"); 
          if(parameters[20] !=null)
              if (parameters[20].ToString() != string.Empty && !parameters[20].ToString().Trim().Contains("1900") && parameters[20].ToString().Trim() == "01/01/0001")
                  sql.Append(" AND date_paid=" + "'" + parameters[20].ToString().Trim() + "'");
          if (parameters[21] != null)
              if (parameters[21].ToString() != string.Empty)
                  sql.Append(" AND title LIKE '" + parameters[21].ToString().Trim().Replace("'", "''") + "%'");
          //********************Added by Sunil Pahwa *******************
          if (Convert.ToInt32(parameters[22]) > 0)//rowid
              sql.Append(" AND rowid = " + parameters[22].ToString());
          //************************************************************************
          sql.Append(" order by account_no");
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

      #endregion Stored-Procedures
        //This is old Find_query 
        //public override string FIND_QUERY(ref Object[] parameters)
        //{
        //    System.Text.StringBuilder sql = new StringBuilder();
        //    sql.Append("select rowid v_rowid, account_no v_account_no ,contract_no v_contract_no,contract_date v_contract_date ,last_name v_last_name ,first_name v_first_name ,title v_title,");
        //    sql.Append("address_1 v_address_1,address_2 v_address_2,telephone v_telephone,defaulted_count v_defaulted_count,current_bal v_current_bal,nss_status v_nss_status,notes v_notes,");
        //    sql.Append("no_of_payments v_no_of_payments,interest_pc v_interest_pc,last_period v_last_period,interest_paid v_interest_paid,date_paid v_date_paid,bonus_months v_bonus_months,");
        //    sql.Append("bonus_paid v_bonus_paid,monthly_contrib v_monthly_contrib,bonus_date v_bonus_date,anniv_date4 v_anniv_date4,anniv_date5 v_anniv_date5,date_stopped v_date_stopped,");
        //    sql.Append("stop_months v_stop_months,stop_pc v_stop_pc,stop_count v_stop_count,paid_by_operator v_paid_by_operator,calculate_now v_calculate_now,calc_months v_calc_months,calc_anniv v_calc_anniv,");
        //    sql.Append("calc_anniv_mth v_calc_anniv_mth from nss_clients where 1=1");

        //    if (parameters[0] != null)
        //        if (parameters[0].ToString() != string.Empty)
        //            sql.Append(" AND  account_no=" + "'" + parameters[0].ToString() + "'");
        //    if (parameters[1] != null)
        //        if (parameters[1].ToString() != string.Empty)
        //            sql.Append(" AND  contract_no=" + "'" + parameters[1].ToString() + "'");
        //    if (parameters[2] != null)
        //        if (parameters[2].ToString() != "1/1/1900")
        //            sql.Append(" AND  contract_date=" + "'" + parameters[2].ToString() + "'");
        //    if (parameters[3] != null)
        //        if (parameters[3].ToString() != string.Empty)
        //            sql.Append(" AND Rtrim(last_name) LIKE '" + parameters[3].ToString().Trim() + "%'");
        //    if (parameters[4] != null)
        //        if (parameters[4].ToString() != string.Empty)
        //            sql.Append(" AND Rtrim(first_name) LIKE '" + parameters[4].ToString().Trim() + "%'");
        //    if (parameters[5] != null)
        //        if (parameters[5].ToString() != string.Empty)
        //            sql.Append(" AND Rtrim(address_1) LIKE '" + parameters[5].ToString().Trim() + "%'");
        //    if (parameters[6] != null)
        //        if (parameters[6].ToString() != string.Empty)
        //            sql.Append(" AND  telephone=" + "'" + parameters[6].ToString() + "'");
        //    return sql.ToString();
        //}
  }
}
