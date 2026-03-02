using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPostAP:DVOBase
    { 
       
        // There are 30 parameter in ap_post function......
        // parameters for ap_post...
        private string _post_or_check;
        private string _orig_journal;
        private int _doc_no;
        private int _post_no;
        private DateTime _post_date;
        private DateTime _doc_date;
        private string _ref_code;
        private string _doc_desc;
        private string _inv_chk_no;
        private string _doc_type;   
        private string _act_type;
        private int _inv_doc_no;
        private decimal _amount;
        private DateTime _disc_date;
        private decimal _disc_bal;      
        private decimal _to_pay_amount;
        private decimal _to_take_disc;
        private DateTime _to_pay_date;
        private string  _pay_to_code;
        private DateTime _due_date;
        private int _ap_acct_no;
        private string _ap_department;
        private string _po_no;
        private DateTime _po_date;
        private DateTime _inv_date;
        private int _cash_acct_no;
        private string _cash_department;
        private string _currency_code;
        private decimal _curr_ex_rate;
        private decimal _home_curr_amount;
        //End parameters for ap_post...

        private string _inv_desc;
        private decimal _disc_amount;            
        private int _check ;
        private int _batch_id;
        private int _doc_count;
        private string _vend_code;
        private int _rowid;



      #region Constructor
        public DVOPostAP()
      {
         _post_or_check=string.Empty;
         _orig_journal=string.Empty;
         _doc_no=0;
         _vend_code=string.Empty;
         _post_no=0;
         _post_date =Convert.ToDateTime("01/01/1900") ;
         _doc_date = Convert.ToDateTime("01/01/1900");
         _ref_code=string.Empty;
         _doc_desc=string.Empty;
         _inv_chk_no=string.Empty;
         _doc_type=string.Empty;
         _act_type=string.Empty;
         _inv_doc_no=0;
         _inv_desc = string.Empty;
         _amount=0;
         _disc_date = Convert.ToDateTime("01/01/1900");
         _disc_amount = 0;
         _disc_bal=0;
         _to_pay_amount=0;
         _to_take_disc=0;
         _to_pay_date = Convert.ToDateTime("01/01/1900");
         _pay_to_code = "";
         _due_date = Convert.ToDateTime("01/01/1900");
         _ap_acct_no=0;
         _ap_department = "000";
         _po_no=string.Empty;
         _po_date = Convert.ToDateTime("01/01/1900");
         _inv_date = Convert.ToDateTime("01/01/1900");
         _cash_acct_no=0;
         _cash_department = "000";
         _currency_code=string.Empty; 
         _curr_ex_rate=0; 
         _home_curr_amount=0;
         _check = 0;
         _doc_count = 0;
         _batch_id = 0;
         _rowid = 0;
            
        
      }
      #endregion Constructor

      #region public properties 
       
      public string post_or_check
      {
          get { return _post_or_check ; }
          set { _post_or_check  = value; }
      }

      public string orig_journal
      {
          get { return _orig_journal ; }
          set { _orig_journal = value; }
      }
      public int doc_no
      {
          get { return _doc_no; }
          set { _doc_no  = value; }
      }
      public string vend_code
      {
          get { return _vend_code; }
          set { _vend_code = value;}
      }
      public int post_no
      {
          get { return _post_no ;  }
          set { _post_no  = value; }
      }
      public DateTime  post_date
      {
          get { return _post_date ; }
          set { _post_date  = value; }
      }
  
      public DateTime doc_date
      {
          get { return _doc_date ; }
          set { _doc_date  = value; }
      }
      public string ref_code
      {
          get { return _ref_code ; }
          set { _ref_code  = value; }
      }
      public string doc_desc
      {
          get { return _doc_desc ; }
          set { _doc_desc  = value; }
      }
      public string inv_chk_no
      {
          get { return _inv_chk_no; }
          set { _inv_chk_no = value; }
      }
      public string inv_desc
      {
          get { return _inv_desc; }
          set { _inv_desc = value; }
      }
      public string doc_type
      {
          get { return _doc_type; }
          set { _doc_type = value; }
      }
      public string act_type
      {
          get { return _act_type; }
          set { _act_type = value; }
      }
      
      public int inv_doc_no
      {
          get { return _inv_doc_no ; }
          set { _inv_doc_no  = value; }
      }
      public decimal amount
      {
          get { return _amount ; }
          set { _amount  = value; }
      }
      public DateTime disc_date
      {
          get { return _disc_date ; }
          set { _disc_date  = value; }
      }
      public decimal disc_bal
      {
          get { return _disc_bal ; }
          set { _disc_bal  = value; }
      }
      public decimal disc_amount
      {
          get { return _disc_amount; }
          set { _disc_amount = value;}
      }
        public decimal to_pay_amount
      {
          get { return _to_pay_amount ; }
          set { _to_pay_amount  = value; }
      }

        public decimal to_take_disc
      {
          get { return _to_take_disc ; }
          set { _to_take_disc  = value; }
      }
      public DateTime to_pay_date
      {
          get { return _to_pay_date ; }
          set { _to_pay_date  = value; }
      }
      public string  pay_to_code
      {
          get { return _pay_to_code ; }
          set { _pay_to_code  = value; }
      }
      public DateTime due_date
      {
          get { return _due_date ; }
          set { _due_date  = value; }
      }
      public int ap_acct_no
      {
          get { return _ap_acct_no ; }
          set { _ap_acct_no  = value; }
      } 
        
      public string ap_department
      {
          get { return _ap_department ; }
          set { _ap_department  = value; }
      }
      public string po_no
      {
          get { return _po_no ; }
          set { _po_no  = value; }
      }
      public DateTime po_date
      {
          get { return _po_date ; }
          set { _po_date  = value; }
      }
      public DateTime inv_date
      {
          get { return _inv_date ; }
          set { _inv_date  = value; }
      } 
        
       public int cash_acct_no
       {
          get { return _cash_acct_no ; }
          set { _cash_acct_no  = value; }
       }
       public string cash_department
       {
          get { return _cash_department ; }
          set { _cash_department  = value; }
       }
       public string currency_code
       {
          get { return _currency_code ; }
          set { _currency_code  = value; }
       }
        public decimal curr_ex_rate
      {
          get { return _curr_ex_rate ; }
          set { _curr_ex_rate  = value; }
      }
      public decimal home_curr_amount
      {
          get { return _home_curr_amount; }
          set { _home_curr_amount = value; }
      }
      public int check
      {
          get { return _check; }
          set { _check = value; }
      }
        public int batch_id
      {
          get { return _batch_id; }
          set { _batch_id = value; }
      }
      public int doc_count
      {
        get { return _doc_count; }
        set { _doc_count = value; }
      }
       public int row_id
       {
           get { return _rowid; }
           set { _rowid = value; }
       }
    #endregion public properties


      #region Stored-Procedures

      public string Update_trx
      {
           get { return "usppaylistingTrx"; }
      }
      public string GET_PAY_LISTING
      {
          get { return "usppaylistingRpt"; }
      }
      public string CHECK_POST
      {
          get { return "uspcheckpost1"; }
      }
      public override string INSERT_SPNAME
      {
          get { return "uspcheckpost"; }
      }

      public override string UPDATE_SPNAME
      {
          get { return "uspcheckpost"; }
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
      public override string TABLE_NAME
      {
          get { return "stpvendr"; }
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
      public string FIND_COUNT_STPTRANAR
      {
       get {return  "usp_cnt_ptranr";}
      }
        public string INSERT_STPTRANAR
      {
        get { return "usp_stptranr_ins";}
      }
       public string FIND_stpvendr_amt
      {
       get { return "usp_stpvendr_amt"; }
      }
      public string INSERT_STPACTVD
      {
         get { return "usp_stpactvd_ins"; }
      }
      public string INSERT_STPOPEND
      {
       get { return "usp_stpopend_ins"; } 
      }
      public string GET_STPVENDR_ROWID
      {
          get { return "usp_rowid_ptranr"; }
      }
      public string UPDATE_STPVENDR
      {
        get { return "usp_stpvendr_upd"; }
      }
      public string UPDATE_STPVENDR1
      {
        get { return "usp_stpvendr_upd1"; }
      }
      public string UPDATE_STPOPEND1
      {
         get { return "usp_stpopend_upd1"; }
      }
      public string UPDATE_STPOPEND2
      {
        get { return "usp_stpopend_upd2"; }
      }
        public string UPDATE_STPINVCE
        {
            get { return "usp_stpinvce_upd"; }
        }
        public string UPDATE_STPINVCE1
        {
            get { return "usp_stpinvce_upd1"; }
        }
        public string FIND_LASTCHECKNO
        {
            get { return "USP_LastCheckNo"; }
        }
      public override string FIND_QUERY(ref Object[] parameters)
      {
         // Added by Sarvjeet On 20 December 2008
          StringBuilder sql = new StringBuilder();
          sql.Append("select * from stpcntrc");
          return sql.ToString();
      }
      #endregion store-procedures




  }
}
