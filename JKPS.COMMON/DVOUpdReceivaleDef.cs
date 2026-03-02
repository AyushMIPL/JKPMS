using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdReceivaleDef :DVOBase
    {
         //Implemented by Sravjeet Verma.
        // table strcntrc
        private decimal _Tax_pct;
        private string _terms_code;
        private string _terms_desc;
        private string _disc_misc;
        private string _disc_frght;
        private string _disc_tax;
        private string _mtax_misc;
        private string _def_mtaxcd;
        private string _mtax_frght;
        private string _gross_entry;
        private string _mtax_fc;
        private string _mtax_dsc;
        private string _age_datetype;
        private string _age_dsc1;
        private string _age_dsc2;
        private string _age_dsc3;
        private string _age_dsc4;
        private int _age_per1;
        private int _age_per2;
        private int _age_per3;
        private string _ar_balanced;
        private string _use_batch_inv;
        private string _use_batch_rec;
        private string _use_approv_post;
        private string _approval_code;
        //table strtermr
        private string _due_days;
        private string _disc_days;
        private string _disc_pct;
        private string _mtaxg_desc;
        private int  _ar_acct_no ;                                      
        private int  _ar_sales_acct_no  ;              
        private int   _ar_tax_acct_no  ;                            
        private int _ar_frght_acct_no  ;                        
        private int  _ar_misc_acct_no ;                                 
        private int  _ar_fc_acct_no  ;                                  
        private int  _cr_cash_acct_no  ;                                
        private int  _cr_disc_acct_no ;
        private int _Rowid;
        int _ar_doc_no;
        string _chkbtchcsh;

       


        #region Constructor
        public DVOUpdReceivaleDef()
        {
            _Rowid = 0;
            _chkbtchcsh = string.Empty;
        }

        

        #endregion Constructor

        #region Public Properties
        public int ar_acct_no
        {
            get { return _ar_acct_no; }
            set { _ar_acct_no = value; }
        }
        public int ar_sales_acct_no
        {
            get { return _ar_sales_acct_no; }
            set { _ar_sales_acct_no = value; }
        }
        public int ar_tax_acct_no
        {
            get { return _ar_tax_acct_no; }
            set { _ar_tax_acct_no = value; }
        }
        public int ar_frght_acct_no
        {
            get { return _ar_frght_acct_no; }
            set {_ar_frght_acct_no =value; }
        }
        public int ar_misc_acct_no 
        {
         get {return _ar_misc_acct_no; }
            set { _ar_misc_acct_no = value; }
        }
        public int ar_fc_acct_no
        {
            get { return _ar_fc_acct_no; }
            set { _ar_fc_acct_no = value; }
        }
        public int cr_cash_acct_no
        {
            get { return _cr_cash_acct_no; }
            set { _cr_cash_acct_no = value; }
        }
        public int cr_disc_acct_no
        {
            get { return  _cr_disc_acct_no; }
            set { _cr_disc_acct_no = value; }
        }
      
        
        public decimal Tax_pct
        {
            get { return _Tax_pct; }
            set { _Tax_pct = value; }
        }
      
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }

        public string terms_desc
        {
            get { return _terms_desc; }
            set { _terms_desc = value; }
        }
        public string disc_misc
        { 
          get { return _disc_misc; }
          set { _disc_misc = value; }
        }
        public string disc_frght
        {
            get { return _disc_frght; }
            set { _disc_frght = value; }

        }

        public string disc_tax
        {
            get { return _disc_tax; }
            set {_disc_tax=value; }
        }
        public string mtax_misc
        {
            get { return _mtax_misc; }
            set { _mtax_misc = value; }
        }
        public string def_mtaxcd
        {
            get { return  _def_mtaxcd; }
            set { _def_mtaxcd = value; }
        }
        public string mtax_frght
        {
            get { return _mtax_frght; }
            set { _mtax_frght = value; }
        }
        public string gross_entry
        {
            get { return _gross_entry; }
            set { _gross_entry = value; }
         }
        public string mtax_fc
        {   
            get { return _mtax_fc; }
            set { _mtax_fc = value; } 
        }
        public string mtax_dsc
        {
            get { return _mtax_dsc; }
            set { _mtax_dsc = value; }
        }
        public string age_datetype
        {
            get { return _age_datetype; }
            set { _age_datetype=value; }
        }
        public string age_dsc1
        {
            get { return _age_dsc1; }
            set { _age_dsc1 = value; }
        }
        public string age_dsc2
        {
            get { return _age_dsc2; }
            set { _age_dsc2 = value; }
        }
        public string age_dsc3
        {
            get { return _age_dsc3; }
            set { _age_dsc3 = value; }
        }
        public string age_dsc4
        {
            get { return _age_dsc4; }
            set { _age_dsc4 = value; }
        }
        public int age_per1
        {
            get { return _age_per1; }
            set { _age_per1 = value; }
         }
        public int age_per2
        {
            get { return _age_per2; }
            set { _age_per2 = value; }
        }
        public int age_per3
        {
            get { return _age_per3; }
            set { _age_per3 = value; }
        }
        public string ar_balanced
        {
            get { return _ar_balanced; }
            set { _ar_balanced = value; }
        }
        public string use_batch_inv
        {
            get { return _use_batch_inv; }
            set { _use_batch_inv = value; }
        }
        public string use_batch_rec
        {
            get { return _use_batch_rec; }
            set { _use_batch_rec = value; }
        }
        public string use_approv_post
        {
            get { return _use_approv_post; }
            set {_use_approv_post=value ;}
        }
        public string approval_code
        {
            get { return _approval_code; }
            set { _approval_code = value; }
        }
        public string due_days 
        {
            get { return _due_days; }
            set { _due_days = value; }
        }

        public string disc_days
        {
            get { return _disc_days; }
            set { _disc_days = value; }
        }
        public string mtaxg_desc
        {
            get { return _mtaxg_desc; }
            set { _mtaxg_desc = value;}
        }

        public string disc_pct
        {
            get { return _disc_pct; }
            set { _disc_pct = value; }
        }
        public int Rowid
        {
            get { return _Rowid ; }
            set { _Rowid  = value; }
        }
        public int ar_doc_no
        {
            get { return _ar_doc_no; }
            set { _ar_doc_no = value; }
        }
        public string chkbtchcsh
        {
            get { return _chkbtchcsh; }
            set { _chkbtchcsh = value; }
        }
        
        # endregion Public Properties

        #region StoreProcedure
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspUpdRecDef"; }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { return "uspupdRecDefGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "strcntrc"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string UPD_ARDOCNO
        {
            get { return "uspardefardocnoupd"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public string GetMtaxgGetall
        { get { return "uspmtaxgetall"; } }
        #endregion StoreProcedure
    } 
}
