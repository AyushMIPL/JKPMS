using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOStgjourd:DVOBase
    {

            private string  _orig_journal;    
            private int  _doc_no;            
            private int  _line_no;              
            private int  _acct_no ;        
            private string  _department;          
            private decimal  _amount;
            private string _debit_credit;
        
        public DVOStgjourd()
        {
            _orig_journal = string.Empty;
            _doc_no = 0;
            _line_no = 0;
            _acct_no = 0;
            _department = string.Empty;
            _debit_credit = string.Empty;
            _amount = Convert.ToDecimal(null); 
           
        }
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
          
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
         
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
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
            get { return "uspstgjourdGet"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string TABLE_NAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override int UNIQUE_ID
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string INSERT_STGJOURD
        {
            get { return "uspstgjourdIns"; }
        }
        public string GET_STGJOURD
        {
            get { return "uspstgjourdGet"; } //uspstgjourdget
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
