using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPrintVoteBook:DVOBase 
   {
       private string _acct_type;
       private string _StartingMonth;
       private string _EndingMonth;
       private string _ReportingYear;
       private string _keyvalue;
       private string _preiousperiod;
       private string _set;
       private DateTime _Date;
       private string _year;
       private string _PrevYear;
        # region Properties
       public string PreviousPeriod
       {
           get { return _preiousperiod; }
           set { _preiousperiod = value; }
       }
       public string PreviousYear
        {
          get{ return  _PrevYear;}
          set{ _PrevYear=value;}
        }
       public string AccountType
       {
           get { return _acct_type; }
           set { _acct_type = value; }
       }
       public string ReportingYear
       {
           get { return _ReportingYear; }
           set { _ReportingYear = value; }
       }

       public string StartingMonth
       {
           get { return _StartingMonth; }
           set { _StartingMonth = value; }
       }
       public string EndingMonth
       {
           get { return _EndingMonth; }
           set { _EndingMonth = value; }
       }

       public string Keyvalue
       {
           get { return _keyvalue; }
           set { _keyvalue = value; }
       }
       public string Set
       {
           get { return _set; }
           set { _set = value; }
       }
       public DateTime Date
       {
           get { return _Date; }
           set { _Date = value; }
       }
       public string Year
       {
           get { return _year; }
           set { _year = value; }
       }

        # endregion Properties
        #region Constructure
        public DVOPrintVoteBook()
        {
            _acct_type = string.Empty;
            _EndingMonth = string.Empty;
            _PrevYear = string.Empty;
            _keyvalue = string.Empty;
            _StartingMonth = string.Empty;
            _ReportingYear = string.Empty;
            _preiousperiod = string.Empty;
            _set = string.Empty;
            _Date = Convert.ToDateTime(null);
        }

         #endregion Constructure
        #region StoreProcedures
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { return "uspvotebkookstxtd"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspCurr_period"; }
        }
       public string GetCurrentYear
       {
           get { return "uspCurr_year"; }
       }

        public override string FIND_QUERY(ref object[] parameters)
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
       public string GET_Bud_AllocVb
       {
           get { return "uspbudalloget"; }
       }
       public string GET_votebkookstxtr
       {
           get { return "uspvotebkookstxtr"; }
       }
       public string GET_UNPOSTEDJGADJ
       {
           get { return "uspunpjgadjget"; }
       }
       public string GET_UNPOSTED_INVOICE
       {
           get { return "uspunpinvget"; }
       }
       public string GET_UNPOSTED_APCHECK
       {
           get { return "uspunpapchkget"; }
       }
       public string GET_UNPOSTED_PURCHORD
       {
           get { return "uspunppurchord"; }
       }
       public string GET_BUD_ADJ
       {
           get { return "uspBudAdjGet"; }
       }
   #endregion   StoreProcedures
    }
}
