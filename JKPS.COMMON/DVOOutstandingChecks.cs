using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public  class DVOOutstandingChecks:DVOBase 
    {
     
         private int _acct_no ; 
         private string _departmen; 
         private string _acct_type ; 
         private string _acct_desc ;
         private  string _acct_cat ;
         private string _processing_seq;  
         private string _incr_with_crdt ;
         private string _subtotal_group ;
         private string _keyvalu ;






       // private int _acct_no;
        private string _department;
       
       //private string _acct_type;
       private string _keyvalue;

        #region Constructor
        public DVOOutstandingChecks()
       {
         _acct_no=0 ; 
         _departmen=string.Empty ; 
        _acct_type=string.Empty  ; 
        _acct_desc=string.Empty  ;
       _acct_cat=string.Empty  ;
        _processing_seq=string.Empty ;  
       _incr_with_crdt=string.Empty  ;
     _subtotal_group=string.Empty  ;
  _keyvalu =string.Empty ;








         //  _acct_no = 0;
          _department = string.Empty;

         //  _acct_type = string.Empty;
           _keyvalue = string.Empty;


       }
        #endregion Constructor
       #region public properties

     

       //private string _acct_type;
       //private string _acct_desc;
       //private string _acct_cat;
       //private string _processing_seq;
       //private string _incr_with_crdt;
       //private string _subtotal_group;
       //private string _keyvalue;

       public int  acct_no
       {
           get { return _acct_no ; }
           set { _acct_no  = value; }
       }

       public string departmen
       {
           get { return _departmen; }
           set { _departmen = value; }
       }
       public string acct_type
       {
           get { return _acct_type; }
           set { _acct_type = value; }
       }
       public string acct_desc
       {
           get { return _acct_desc; }
           set { _acct_desc = value; }
       }

       public string acct_cat
       {
           get { return _acct_cat; }
           set { _acct_cat = value; }
       }
       public string processing_seq
       {
           get { return _processing_seq; }
           set { _processing_seq = value; }
       }

    
       public string incr_with_crdt
       {
           get { return _incr_with_crdt; }
           set { _incr_with_crdt = value; }
       }
       public string subtotal_group
       {
           get { return _subtotal_group; }
           set { _subtotal_group = value; }
       }
       public string keyvalu
       {
           get { return _keyvalu; }
           set { _keyvalu = value; }
       }

       //public int acct_no
       //{
       //    get { return _acct_no; }
       //    set { _acct_no = value; }
       //}
       public string  department
       {
           get { return _department; }
           set { _department  = value; }
       }

       //public string AccountType
       //{
       //    get { return _acct_type; }
       //    set { _acct_type = value; }
       //}
       public string Keyvalue
       {
           get { return _keyvalue; }
           set { _keyvalue = value; }
       }
      #endregion  properties 


       #region Stored-Procedures

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
           get { return "uspOutstChecksGet"; }
       }

       public override string ALL_SPNAME
       {
           get { return "uspoutschecksGetAl"; }
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
       public override string FIND_QUERY(ref Object[] parameters)
       {
           System.Text.StringBuilder sql = new StringBuilder();
           sql.Append("select Master_Cash_Accounts.acct_no,Master_Cash_Accounts.department,stxckrgd.orig_journal,");
           sql.Append(" stxckrgd.doc_no,stxckrgd.inv_chk_no ,stxckrgd.amount,stxckrgd.debit_credit,");
           sql.Append(" stxckrgd.reconciled,stxckrgd.chk_voided,");
           sql.Append(" stxtranr.post_no,stxtranr.post_date ,stxtranr.doc_date ,");
           sql.Append(" stxtranr.ref_code,stxtranr.doc_desc,stpvendr.vend_code,");
           sql.Append(" stpvendr.bus_name,stpvendr.acct_bal ,stpvendr.on_acct_amt, ");
           sql.Append(" stpvendr.cash_department,stpvendr.exp_acct_no,stpvendr.acct_bal_date,");
           sql.Append(" stpvendr.on_acct_date,stpvendr.mtax_frght,stpvendr.mtax_misc,");
           sql.Append(" PayrollGLAccounts.acct_type ,PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_cat , ");
           sql.Append(" PayrollGLAccounts.processing_seq ,PayrollGLAccounts.incr_with_crdt,PayrollGLAccounts.keyvalue ");
           sql.Append(" from Master_Cash_Accounts, stxckrgd, stxtranr, stpvendr, outer PayrollGLAccounts ");

           sql.Append(" Where Master_Cash_Accounts.acct_no = stxckrgd.acct_no and ");

           sql.Append(" Master_Cash_Accounts.department = stxckrgd.department and  ");
           sql.Append(" Master_Cash_Accounts.acct_no =PayrollGLAccounts.acct_no and ");
           sql.Append(" stxckrgd.orig_journal = stxtranr.orig_journal and  ");
           sql.Append(" stxckrgd.doc_no = stxtranr.doc_no and ");
           sql.Append(" stxtranr.ref_code = stpvendr.vend_code and ");
           sql.Append(" stxckrgd.orig_journal = 'CD' and ");
           sql.Append(" stxckrgd.reconciled = 'N' and ");
           sql.Append(" stxckrgd.chk_voided = 'N'");


           if (Convert.ToInt32(parameters[0]) > 0)//Level
               sql.Append(" AND Master_Cash_Accounts.acct_no  = " + parameters[0].ToString());
           
           //if (parameters[0] != null)
           //    if (parameters[0].ToString() != string.Empty)
           //        sql.Append(" AND Rtrim(PayrollGLAccounts.keyvalue) LIKE '" + parameters[0].ToString().Trim() + "%'");

           if (parameters[1] != null)
               if (parameters[1].ToString() != string.Empty)
                   sql.Append(" AND Rtrim(Master_Cash_Accounts.department) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
     
           return sql.ToString();
       }
       #endregion store-procedures

    }

}
