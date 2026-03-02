using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAPUpdatetoPay:DVOBase
    {
           private string _vend_code;
           private string _bus_name;
           private string _pay_to_code;
           private string _pay_to_name;
           private decimal _on_acct_amt;
           private decimal _acct_bal;
           private string _currency_code;
           private string _inv_no;
           private int _doc_no;
           private string _inv_desc;
           private string _inv_date;
           private decimal _orig_amount;
           private decimal _disc_amt;
           private decimal _balance;
           private decimal _disc_bal;
           private string _due_date;
           private string _disc_date;
           private string _ap_keyvalue;
           private string _po_no;
           private string _po_date;
           private decimal _home_curr_amount;
           private decimal _curr_ex_rate;
           private decimal _to_pay_amt;
           private decimal _to_take_disc;
           private string _to_pay_date;
           private string _cash_keyvalue;
        private int _rowid;
                 
        #region Constructor

        public DVOAPUpdatetoPay()
        {
           _vend_code=string.Empty;
           _bus_name=string.Empty;
           _pay_to_code=string.Empty;
           _pay_to_name=string.Empty;
           _on_acct_amt=0;
           _acct_bal=0;
           _currency_code=string.Empty;
           _inv_no=string.Empty;
           _doc_no=0;
           _inv_desc=string.Empty;
           _inv_date = string.Empty;
           _orig_amount=0;
           _disc_amt=0;
           _balance=0;
           _disc_bal=0;
           _due_date = "01/01/1900";
           _disc_date = "01/01/1900";
           _ap_keyvalue=string.Empty;
           _po_no=string.Empty;
           _po_date = string.Empty;
           _home_curr_amount=0;
           _curr_ex_rate=0;
           _to_pay_amt=0;
           _to_take_disc=0;
           _to_pay_date = "01/01/1900";
           _cash_keyvalue=string.Empty;
           _rowid = 0;
        
    }

        #endregion Constructor

        #region Public Properties

        public string vend_code
        {
            get {return _vend_code; }
            set { _vend_code = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }
        public string pay_to_code
        {
            get { return _pay_to_code; }
            set { _pay_to_code = value; }
        }
        public string pay_to_name
        {
            get { return _pay_to_name; }
            set { _pay_to_name = value; }
        }
        public decimal on_acct_amt
        {
            get { return _on_acct_amt; }
            set { _on_acct_amt = value; }
        }
        public decimal acct_bal
        {
            get { return _acct_bal; }
            set { _acct_bal = value; }
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
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string inv_desc
        {
            get { return _inv_desc; }
            set { _inv_desc = value; }
        }
        public string inv_date
        {
            get { return _inv_date; }
            set { _inv_date = value; }
        }
        public decimal orig_amount
        {
            get { return _orig_amount; }
            set { _orig_amount = value; }
        }
        public decimal disc_amt
        {
            get { return _disc_amt; }
            set { _disc_amt = value; }
        }
        public decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public decimal disc_bal
        {
            get { return _disc_bal; }
            set { _disc_bal = value; }
        }
        public string due_date
        {
            get { return _due_date; }
            set { _due_date = value; }
        }
        public string disc_date
        {
            get { return _disc_date; }
            set { _disc_date = value; }
        }
        public string ap_keyvalue
        {
            get { return _ap_keyvalue; }
            set { _ap_keyvalue = value; }
        }
        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }
        public string po_date
        {
            get { return _po_date; }
            set { _po_date = value; }
        }
        public decimal home_curr_amount
        {
            get { return _home_curr_amount; }
            set { _home_curr_amount = value; }
        }
        public decimal curr_ex_rate
        {
            get { return _curr_ex_rate; }
            set { _curr_ex_rate = value; }
        }
        public decimal to_pay_amt
        {
            get { return _to_pay_amt; }
            set { _to_pay_amt = value; }
        }
        public decimal to_take_disc
        {
            get { return _to_take_disc; }
            set { _to_take_disc = value; }
        }
        public string to_pay_date
        {
            get { return _to_pay_date; }
            set { _to_pay_date = value; }
        }
        public string cash_keyvalue
        {
            get { return _cash_keyvalue; }
            set { _cash_keyvalue = value; }
        }

        public int Rowid
        {
            get { return _rowid ; }
            set { _rowid  = value; }
        }
        
        #endregion Public Properties

        #region Stored-Procedures

       
        public string GET_VENDOR_CASH_REQUIRE_CODE
        {
            get { return ""; }
        }
        //*****************************************************
       

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspappayvinfupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspappayvinfget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }


        public override string TABLE_NAME
        {
            get { return "stpopend"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid ; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stpopend.vend_code, stpvendr.bus_name, stpopend.pay_to_code,");
            sql.Append("stpvendr.bus_name, stpvendr.on_acct_amt, stpvendr.acct_bal,");
            sql.Append("stpopend.currency_code, stpopend.inv_no, stpopend.doc_no, ");
            sql.Append("stpopend.inv_desc, stpopend.inv_date, stpopend.orig_amount,");
            sql.Append("stpopend.disc_amt, stpopend.balance, stpopend.disc_bal, stpopend.due_date, ");
            sql.Append("stpopend.disc_date, stpopend.ap_acct_no,PayrollGLAccounts1.keyvalue as ap_keyvalue,");
            sql.Append("stpopend.po_no, stpopend.po_date, stpopend.home_curr_amount, ");
            sql.Append("stpopend.curr_ex_rate, stpopend.to_pay_amt, stpopend.to_take_disc,");
            sql.Append("stpopend.to_pay_date, stpopend.cash_acct_no,PayrollGLAccounts.keyvalue as cash_keyvalue,stpopend.rowid");
            sql.Append(" FROM stpopend,stpvendr,PayrollGLAccounts,PayrollGLAccounts PayrollGLAccounts1 where 1=1 ");
            sql.Append(" and stpopend.vend_code = stpvendr.vend_code ");
            sql.Append(" and stpopend.cash_acct_no = PayrollGLAccounts.acct_no ");
            sql.Append(" and stpopend.ap_acct_no = PayrollGLAccounts1.acct_no "); //Added By Rahul Jain on 5/2/2009 for getting keyvalue
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)  //vend_code
                sql.Append(" and stpopend.vend_code = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)  //pay_to_code
                sql.Append(" and stpopend.pay_to_code = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)  //currency_code
                sql.Append(" and stpopend.currency_code = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)   //invoice number
                sql.Append(" and stpopend.inv_no = '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[4]) > 0)    //doc no
                sql.Append(" and stpopend.doc_no = " + Convert.ToInt32(parameters[4].ToString()));
            if (Convert.ToInt32(parameters[7]) > 0)    //orig_amount
                sql.Append(" and stpopend.orig_amount = " + Convert.ToDecimal(parameters[7].ToString()));
            if (Convert.ToInt32(parameters[8]) > 0)    //disc_amt
                sql.Append(" and stpopend.disc_amt = " + Convert.ToDecimal(parameters[8].ToString()));
            if (Convert.ToInt32(parameters[9]) > 0)    //balance
                sql.Append(" and stpopend.balance = " + Convert.ToDecimal(parameters[9].ToString()));
            if (Convert.ToInt32(parameters[10]) > 0)    //disc_bal
                sql.Append(" and stpopend.disc_bal = " + Convert.ToDecimal(parameters[10].ToString()));





            if (Convert.ToInt32(parameters[20]) > 0)//rowid
                sql.Append(" and stpopend.rowid = " + parameters[20].ToString());
            
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
