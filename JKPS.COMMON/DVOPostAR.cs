using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVOPostAR:DVOBase
    {
      private string _post_or_check;
      private string _orig_journal; 
      private int _doc_no;
      private int  _post_no;
      private DateTime _post_date;
      private DateTime  _doc_date; 
      private string _ref_code;
      private string  _doc_desc;
      private string _inv_chk_no;
      private string  _doc_type;
      private string  _act_type;
      private int  _inv_doc_no;
      private decimal _amount; 
      private DateTime _disc_date;          
      private decimal _disc_bal; 
      private DateTime _due_date; 
      private int   _ar_acct_no; 
      private string  _ar_department;              
      private string  _po_no; 
      private DateTime _po_date;
      private string _currency_code;
      private decimal _curr_ex_rate;      
      private decimal _home_curr_amount;

       public DVOPostAR()
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
           _doc_type = string.Empty;
           _act_type = string.Empty;
           _inv_doc_no = 0;
           _amount = 0.0M;
           _disc_date = Convert.ToDateTime("01/01/1900");
           _due_date = Convert.ToDateTime("01/01/1900");
           _disc_bal = 0.0M;
           _ar_acct_no = 0;
           _ar_department = string.Empty;
           _po_no = string.Empty;
           _po_date = Convert.ToDateTime("01/01/1900");
           _currency_code = string.Empty;
           _curr_ex_rate = 0.0M;
           _home_curr_amount = 0.0M;
       }
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
       public Int32 ar_acct_no
       {
           get { return _ar_acct_no; }
           set { _ar_acct_no = value; }
       }
       public string ar_department
       {
           get { return _ar_department; }
           set { _ar_department = value; }
       }
       public decimal amount
       {
           get { return _amount; }
           set { _amount = value; }
       }

     
       public string act_type
       {
           get { return _act_type; }
           set { _act_type = value; }
       }
       public string doc_type
       {
           get { return _doc_type; }
           set { _doc_type = value; }
       }
       public DateTime disc_date
       { get { return _disc_date; }
         set { _disc_date = value; }
       }
       public DateTime due_date
       { get { return _due_date; }
           set { _due_date = value; }
       }
       public decimal disc_bal
       { get { return _disc_bal; }
           set { _disc_bal = value; }
       }
       public string po_no
       {
           get { return _po_no; }
           set { _po_no = value; }
       }
       public DateTime po_date
       {
           get { return _po_date; }
           set { _po_date = value;}
       
       }
       public string currency_code
       {
           get { return _currency_code; }
           set { _currency_code = value; }
       }
       public decimal curr_ex_rate
       {
           get { return _curr_ex_rate; }
           set { _curr_ex_rate = value; }
       }
       public decimal home_curr_amount
       {
           get { return _home_curr_amount; }
           set { _home_curr_amount = value; }
       }
       public int inv_doc_no
       {
           get { return _inv_doc_no; }
           set { _inv_doc_no = value; }
       }




       public override string INSERT_SPNAME
       {
           get {return ""; }
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
           get { return "usptb_check"; }
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
       public string INSERT_usptb_post
       {
           get { return "usptb_post"; }
       }
       public string FINDusprcntrc_b_r
       {
           get { return "usprcntrc_b_r"; }
       }
       public string Updatestrcasheupd
       {
           get { return "uspstrcasheupd"; }
       }
       public string CHECKTB
       {
           get { return "usptb_check"; }
       }
       public override string FIND_QUERY(ref object[] parameters)
       {
          return  "";
       }
   }
}
