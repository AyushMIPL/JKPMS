using System;
using System.Collections.Generic;
using System.Text;


namespace JKPS.COMMON
{
   public  class DvoCheckListing:DVOBase 
   {
       //********* Added by Sarvjeet Verma On 26/12/2008 ***********
        private int _doc_no;
        private int _dist_acct;
        private decimal _dist_amt;
        private int _cash_acct;
        private decimal _cash_amt;
        private int _disc_acct;
        private decimal _disc_amt;
        private string _ap_type;
        private string _disc_deb_cred;
        private string _dist_deb_cred;
        private string _currency_code;
        private string _inv_no;
        private  int _inv_doc_no;
        private  string _dist_department;
        private  string _disc_department;
        private  string _description;
        private DateTime _chk_date;

        private string _CHECK_POST;
        private string _vend_code;
        private string _doc_desc;
        private string _check_no;
        private string _pay_to_code;
        private int _oa_acct;
        private string _oa_department;        
        private string _cash_department;
        private decimal _curr_ex_rate;
        private int _next_doc_no;
        private string _debit_credit;
        private int _Post_no;
        private string _stpcntrc_mtax_dsc;
       private decimal _oa_amt;
       private string _oa_deb_cred;
        public DvoCheckListing()
        {
          _doc_no=0;
          _disc_acct = 0;
          _disc_amt = 0;
          _dist_acct = 0;
          _dist_amt = 0;
          _cash_amt = 0;
          _cash_acct=0;
          _ap_type = string.Empty;
          _disc_deb_cred = string.Empty;
          _dist_deb_cred = string.Empty;
          _currency_code = string.Empty;
          _inv_no = string.Empty;
          _inv_doc_no = 0;
          _dist_department = string.Empty;
          _disc_department = string.Empty;
          _description = string.Empty;
          _chk_date = Convert.ToDateTime(null);
          _CHECK_POST = "CHECK";
          _vend_code = string.Empty;
          _doc_desc = string.Empty;
          _check_no = string.Empty;
           _pay_to_code=string.Empty;
           _oa_acct = 0;
           _oa_department = string.Empty;
           _cash_department = string.Empty;
           _curr_ex_rate = 0;
           _next_doc_no = 0;
           _debit_credit = string.Empty;
           _Post_no = 0;
           _stpcntrc_mtax_dsc = string.Empty;
           _oa_amt = 0;
           _oa_deb_cred = string.Empty;
        }


        public int doc_no
        {
           get { return _doc_no; }
           set { _doc_no = value;}
        }
        public int dist_acct
       {
           get { return _dist_acct; }
           set { _dist_acct = value; }
       }
        public decimal dist_amt
       {
           get { return _dist_amt; }
           set { _dist_amt = value; }
       }
        public int cash_acct
       {
           get { return _cash_acct; }
           set { _cash_acct = value; }
       }
        public decimal cash_amt
       {
           get { return _cash_amt; }
           set { _cash_amt = value; }
       }
        public int disc_acct
        {
           get { return _disc_acct; }
           set { _disc_acct = value; }
        }
        public decimal disc_amt
       {
           get { return _disc_amt; }
           set { _disc_amt = value; }
       }
        public string ap_type
        {
          get { return _ap_type; }
          set { _ap_type = value; }
        }
        public string disc_deb_cred
        {
           get { return _disc_deb_cred; }
           set { _disc_deb_cred = value; }
        }
        public string dist_deb_cred
         {
           get { return _dist_deb_cred; }
           set { _dist_deb_cred = value; }
         }
        public string currency_code
         {
           get { return _currency_code; }
           set { _currency_code = value; }
         }
        public string inv_no
       {
           get { return _inv_no; }
           set { _inv_no = value; }
       }
        public int inv_doc_no
       {
           get { return _inv_doc_no; }
           set { _inv_doc_no = value; }
       }
        public string dist_department
       {
           get { return _dist_department; }
           set { _dist_department = value; }
       }
        public string disc_department
       {
           get { return _disc_department; }
           set
           {
             _disc_department = value;       
           }
       }
        public string description
        {
           get { return _description; }
           set { _description = value; }
        }
        public DateTime chk_date
        {
           get { return _chk_date; }
           set { _chk_date = value; }
        }
        public string vend_code
       {
           get { return _vend_code; }
           set { _vend_code = value; }
       }
        public string CHECK_POST
       {
           get { return _CHECK_POST; }
           set { _CHECK_POST = value; }
       }
        public string doc_desc
       {
           get { return _doc_desc; }
           set { _doc_desc = value; }
       }
        public string check_no
       {
           get { return _check_no; }
           set { _check_no = value; }
       }
        public string pay_to_code
       {
           get { return _pay_to_code; }
           set { _pay_to_code = value; }
       }
        public int oa_acct
       {
           get { return _oa_acct; }
           set { _oa_acct = value; }
       }
        public string oa_department
       {
           get { return _oa_department; }
           set { _oa_department = value; }
       }
        public string cash_department
       {
           get { return _cash_department; }
           set { _cash_department = value; }
       }
        public decimal curr_ex_rate
       {
           get { return _curr_ex_rate; }
           set { _curr_ex_rate = value; }
       }
        public int next_doc_no
       { 
           get { return _next_doc_no; }
           set { _next_doc_no = value; }
       }
        public int Post_no
       {
           get { return _Post_no; }
           set { _Post_no = value; }

       }
        public string stpcntrc_mtax_dsc
       {
           get { return _stpcntrc_mtax_dsc; }
           set { _stpcntrc_mtax_dsc = value; }
       }
        public string debit_credit
       {
           get { return _debit_credit; }
           set { _debit_credit = value; }
       }
       public decimal oa_amt
       {
           get { return _oa_amt; }
           set { _oa_amt = value; }
       }
        public string oa_deb_cred 
       {
           get { return _oa_deb_cred; }
           set { _oa_deb_cred = value; }
       }
        public override string INSERT_SPNAME
        {
            get { return ""; }
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
            get { return "uspgenchkno"; }
        }

        public override string ALL_SPNAME
        {
            get { return "UspCListingGetAll"; }
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
       public string FIND_TENDCHECK
       {
           get { return "uspchecktend"; }
       }
       public string FIND_CHECK_COUNT
       {
           get { return "uspcheck_count"; }
       }
       public string FIND_CHECK_TYPE
       {
           get { return "uspcheck_typeget"; }
       }
       public string UPDATE_STPCASHE
       {
           get { return "usp_stpcashe_upd"; }
       }
       public string INSERT_INTTBPDD
       {
           get { return "usp_inttbpdd_ins"; }
       }
       public string FIND_CHECKLISTINFO
       {
           get { return "uspclistingget"; }
       }
       public string FIND_CHECKLISTINFO_BY_BATCH
       {
           get { return "uspclistbtchget"; }
       }

       public string FIND_CHECKLISTDTLINFO
       {
           get { return "uspclistingget"; }//uspclistingdtlget
       }
       public string UPDATE_CHK_NO_PCASHE
       {
           get { return "uspgenchkno"; }
       }
       public string GET_ASSIGNCHECK_NO_AND_LOCKTABLE
       {
           get { return "uspgenchknotmp"; }
       }

       public string FIND_CHECKLISTINFOAPPROVAL
       {
           get { return "uspappchklist"; }
       }
       
       public override string FIND_QUERY(ref object[] parameters)
       {
           return "";
       }

    }
}
