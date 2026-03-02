using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPostGL :DVOBase
    {
        private string _post_or_check; 
        private string _orig_journal;
        private Int32 _doc_no;
        private Int32 _post_no;
        private DateTime _post_date;
        private DateTime _doc_date;
        private string _ref_code;
        private string _doc_desc;
        private string _inv_chk_no;
        private Int32 _acct_no;
        private string _department;
        private decimal _amount;
        private string _debit_credit;
        //** Added by sarvjeet 
        private string _Auto_rev;
     //added by sunil
       private int _inv_doc_no;

        #region Constructor
       public DVOPostGL()
      {
     
          _post_or_check = string.Empty;
          _orig_journal = string.Empty;
          _doc_no = 0;
          _post_no = 0;
          _post_date = Convert.ToDateTime("01/01/1900");
          _doc_date = Convert.ToDateTime("01/01/1900");
          _ref_code = string.Empty;
          _doc_desc = string.Empty;
          _inv_chk_no = string.Empty;
          _acct_no = 0;
          _department = "000";
          _amount = 0.0M;
          _debit_credit = string.Empty;
          _Auto_rev = string.Empty;

          _inv_doc_no = 0;
      }
      #endregion Constructor

      #region public properties
      public string post_or_check
       {
           get { return _post_or_check; }
           set { _post_or_check = value; }
       }
       public string orig_journal
       {
           get { return _orig_journal; }
           set { _orig_journal = value; }
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
       public string inv_chk_no
       {
           get { return _inv_chk_no; }
           set { _inv_chk_no = value; }
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
       public string Auto_rev
       {
           get { return _Auto_rev; }
           set { _Auto_rev = value; }
       }



       public int inv_doc_no
       {
           get { return _inv_doc_no ; }
           set { _inv_doc_no  = value; }
       }
      #endregion public properties
       
        #region Stored-Procedures

       //public string AUTHENTICATION_SPNAME
       //{
       //    get { return "uspsecauthenticate"; }
       //}

       public string GET_CKRGRROW
       {
           get { return "uspMaster_Cash_Accountsget"; }
       }
       public override string INSERT_SPNAME
       {
           get { return "uspckrgdins"; }
       }

       public override string UPDATE_SPNAME
       {
           get { return "uspupdstgjoure"; }
       }

       public override string DELETE_SPNAME
       {
           get { return ""; }
       }
       public string POSTGL
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
       public string INSERT_STXCKRGD
       {

           get { return "uspckrgdins"; }
       }
       public string INSERT_PY_STXCKRGD
       {
           get { return "usppyckrgdins"; }
       }
       public string UPDATE_STGJOURE
       {
           get { return "uspupdstgjoure"; }
       }
       public string GET_Master_Cash_Accounts
       {
           get { return "uspxckrgrget"; }
       }
       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();

           return sql.ToString();
       }
       #endregion store-procedures

    }
}
