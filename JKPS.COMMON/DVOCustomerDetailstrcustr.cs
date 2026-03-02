using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By :Rahul Jain on 21-05-2009 getting Customer Address from strcustr tables
    public class DVOCustomerDetailstrcustr : DVOBase
    {
        int _Rowid;
        string _cust_code;
        string _bridge_code;
        string _bus_name;
        string _taxable;
        string _contact;
        string _phone;
        string _fax_phone;
        string _address1;
        string _address2;
        string _city;
        string _state;
        string _zip;
        string _country;
        string _ar_type;
        string _preferred;
        string _frequent;
        int _stmt_cycle;
        string _fin_chg;
        decimal _credit_limit;
        decimal _order_limit;
        string _terms_code;
        string _act_grp;
        int _ar_acct_dflt;
        string _ar_department_dflt;
        string _stmt_date; //date                                    
        decimal _stmt_amount;
        decimal _acct_bal;
        string _obtained_date;    //date                                    
        string _last_order_date; //date                                    
        string _last_pay_date;  //date                                    
        string _inactive_date; //date                                    
        decimal _on_acct_amt;
        decimal _arch_bal;
        string _sls_psn_code;
        string _trd_ds_code;
        string _st_tx_code;
        string _co_tx_code;
        string _ci_tx_code;
        string _comm_code;
        string _pay_method;
        string _card_no;
        string _exp_date;
        string _card_holder;
        string _cc_method;
        string _mtax_fc;
        string _currency_code;
        string _mtax_freight;
        string _mtax_misc;
        string _ship_via_cd;

        public DVOCustomerDetailstrcustr()
        {
            int _Rowid=0;
            string _cust_code=string.Empty;
            string _bridge_code = string.Empty;
            string _bus_name = string.Empty;
            string _taxable = string.Empty;
            string _contact = string.Empty;
            string _phone = string.Empty;
            string _fax_phone = string.Empty;
            string _address1 = string.Empty;
            string _address2 = string.Empty;
            string _city = string.Empty;
            string _state = string.Empty;
            string _zip = string.Empty;
            string _country = string.Empty;
            string _ar_type = string.Empty;
            string _preferred = string.Empty;
            string _frequent = string.Empty;
            int _stmt_cycle=0;
            string _fin_chg = string.Empty;
            decimal _credit_limit=0.0M;
            decimal _order_limit=0.0M;
            string _terms_code = string.Empty;
            string _act_grp = string.Empty;
            int _ar_acct_dflt=0;
            string _ar_department_dflt = string.Empty;
            string _stmt_date = "01/01/1900"; //date                                    
            decimal _stmt_amount=0.0M;
            decimal _acct_bal=0.0M;
            string _obtained_date="01/01/1900";    //date                                    
            string _last_order_date="01/01/1900"; //date                                    
            string _last_pay_date="01/01/1900";  //date                                    
            string _inactive_date="01/01/1900"; //date                                    
            decimal _on_acct_amt=0.0M;
            decimal _arch_bal = 0.0M;
            string _sls_psn_code=string.Empty;
            string _trd_ds_code = string.Empty;
            string _st_tx_code = string.Empty;
            string _co_tx_code = string.Empty;
            string _ci_tx_code = string.Empty;
            string _comm_code = string.Empty;
            string _pay_method = string.Empty;
            string _card_no = string.Empty;
            string _exp_date = string.Empty;
            string _card_holder = string.Empty;
            string _cc_method = string.Empty;
            string _mtax_fc = string.Empty;
            string _currency_code = string.Empty;
            string _mtax_freight = string.Empty;
            string _mtax_misc = string.Empty;
            string _ship_via_cd = string.Empty;
        }
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public string bridge_code
        {
            get { return _bridge_code; }
            set { _bridge_code = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }
        public string taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string fax_phone
        {
            get { return _fax_phone; }
            set { _fax_phone = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string ar_type
        {
            get { return _ar_type; }
            set { _ar_type = value; }
        }
        public string preferred
        {
            get { return _preferred; }
            set { _preferred = value; }
        }
        public string frequent
        {
            get { return _frequent; }
            set { _frequent = value; }
        }
        public int stmt_cycle
        {
            get { return _stmt_cycle; }
            set { _stmt_cycle = value; }
        }
        public string fin_chg
        {
            get { return _fin_chg; }
            set { _fin_chg = value; }
        }
        public decimal credit_limit
        {
            get { return _credit_limit; }
            set { _credit_limit = value; }
        }
        public decimal order_limit
        {
            get { return _order_limit; }
            set { _order_limit = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string act_grp
        {
            get { return _act_grp; }
            set { _act_grp = value; }
        }
        public int ar_acct_dflt
        {
            get { return _ar_acct_dflt; }
            set { _ar_acct_dflt = value; }
        }
        public string ar_department_dflt
        {
            get { return _ar_department_dflt; }
            set { _ar_department_dflt = value; }
        }
        public string stmt_date
        {
            get { return _stmt_date; }
            set { _stmt_date = value; }
        }
        public decimal stmt_amount
        {
            get { return _stmt_amount; }
            set { _stmt_amount = value; }
        }
        public decimal acct_bal
        {
            get { return _acct_bal; }
            set { _acct_bal = value; }
        }
        public string obtained_date
        {
            get { return _obtained_date; }
            set { _obtained_date = value; }
        }
        public string last_order_date
        {
            get { return _last_order_date; }
            set { _last_order_date = value; }
        }
        public string last_pay_date
        {
            get { return _last_pay_date; }
            set { _last_pay_date = value; }
        }
        public string inactive_date
        {
            get { return _inactive_date; }
            set { _inactive_date = value; }
        }
        public decimal on_acct_amt
        {
            get { return _on_acct_amt; }
            set { _on_acct_amt = value; }
        }
        public decimal arch_bal
        {
            get { return _arch_bal; }
            set { _arch_bal = value; }
        }
        public string sls_psn_code
        {
            get { return _sls_psn_code; }
            set { _sls_psn_code = value; }
        }
        public string trd_ds_code
        {
            get { return _trd_ds_code; }
            set { _trd_ds_code = value; }
        }
        public string st_tx_code
        {
            get { return _st_tx_code; }
            set { _st_tx_code = value; }
        }
        public string co_tx_code
        {
            get { return _co_tx_code; }
            set { _co_tx_code = value; }
        }
        public string ci_tx_code
        {
            get { return _ci_tx_code; }
            set { _ci_tx_code = value; }
        }
        public string comm_code
        {
            get { return _comm_code; }
            set { _comm_code = value; }
        }
        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }
        public string card_no
        {
            get { return _card_no; }
            set { _card_no = value; }
        }
        public string exp_date
        {
            get { return _exp_date; }
            set { _exp_date = value; }
        }
        public string card_holder
        {
            get { return _card_holder; }
            set { _card_holder = value; }
        }
        public string cc_method
        {
            get { return _cc_method; }
            set { _cc_method = value; }
        }
        public string mtax_fc
        {
            get { return _mtax_fc; }
            set { _mtax_fc = value; }
        }
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string mtax_freight
        {
            get { return _mtax_freight; }
            set { _mtax_freight = value; }
        }
        public string mtax_misc
        {
            get { return _mtax_misc; }
            set { _mtax_misc = value; }
        }
        public string ship_via_cd
        {
            get { return _ship_via_cd; }
            set { _ship_via_cd = value; }
        }




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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "strcustr"; }
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
        //Using in Print Purchase ordre report
        public string GETCustAddress
        {
            get { return "uspcustaddrget"; }
        }
        public string GetAllCustAddress
        {
            get { return "uspcustaddrgetall"; }
        }

        public string GETCUSTADDRESS_FOR_PICKING_DOC
        {
            get { return "usppdocstrcustrget"; }
        }


        //Added by sunil on [21/07/2009]for get Sales persons Summary info
        public string GET_SALES_PERSONS_BUS_NAME
        {
            get { return "uspsalpersonbnget"; }
        }

        //Added by sunil on [27/07/2009]for get Inv Momos Info
        public string GET_ADDRESS
        {
            get { return "uspimoscustrget"; }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
