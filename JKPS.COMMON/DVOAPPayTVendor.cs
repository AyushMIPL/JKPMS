using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAPPayTVendor : DVOBase
    {
        private string _vend_code;
        private string _bus_name;
        private string _contact;
        private string _phone;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        private string _country;
        private decimal _credit_limit;
        private string _terms_code;
        private string _act_grp;
        private string _spec_billing;
        private long _ap_acct_dflt;
        private string _ap_department_dflt;
        private string _last_pay_date;
        private string _hold_pymnt;
        private string _take_dscnt;
        private decimal _acct_bal;
        private decimal _on_acct_amt;
        private decimal _arch_bal;
        private string _spec_shipping;
        private string _taxable;
        private string _bo_allowed;
        private string _pay_method;
        private string _buyer_code;
        private string _trd_ds_code;
        private int _eta_days;
        private string _st_tx_code;
        private string _co_tx_code;
        private string _ci_tx_code;
        private long _cash_acct_no;
        private string _cash_department;
        private long _exp_acct_no;
        private string _exp_department;
        private string _print_1099;
        private string _federal_tax_id;
        private string _currency_code;
        private string _acct_bal_date;
        private string _on_acct_date;
        private string _sdb_code;
        private int _vendor_rating;
        private string _fax_phone;
        private string _telex_no;
        private string _mtax_frght;
        private string _mtax_misc;
        //**********************************Pay To Vendor************************
        private string _pay_to_code;
        private string _pay_to_name;
        //***********************************************************************
        #region Constructor

        public DVOAPPayTVendor()
        {
            _vend_code = string.Empty;
            _bus_name = string.Empty;
            _contact = string.Empty;
            _phone = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _city = string.Empty;
            _state = string.Empty;
            _zip = string.Empty;
            _country = string.Empty;
            _credit_limit = 0;
            _terms_code = string.Empty;
            _act_grp = string.Empty;
            _spec_billing = string.Empty;
            _ap_acct_dflt = 0;
            _ap_department_dflt = string.Empty;
            _last_pay_date = string.Empty;
            _hold_pymnt = string.Empty;
            _take_dscnt = string.Empty;
            _acct_bal = 0;
            _on_acct_amt = 0;
            _arch_bal = 0;
            _spec_shipping = string.Empty;
            _taxable = string.Empty;
            _bo_allowed = string.Empty;
            _pay_method = string.Empty;
            _buyer_code = string.Empty;
            _trd_ds_code = string.Empty;
            _eta_days = 0;
            _st_tx_code = string.Empty;
            _co_tx_code = string.Empty;
            _ci_tx_code = string.Empty;
            _cash_acct_no = 0;
            _cash_department = string.Empty;
            _exp_acct_no = 0;
            _exp_department = string.Empty;
            _print_1099 = string.Empty;
            _federal_tax_id = string.Empty;
            _currency_code = string.Empty;
            _acct_bal_date = string.Empty;
            _on_acct_date = string.Empty;
            _sdb_code = string.Empty;
            _vendor_rating = 0;
            _fax_phone = string.Empty;
            _telex_no = string.Empty;
            _mtax_frght = string.Empty;
            _mtax_misc = string.Empty;
            //**********************************Pay To Vendor************************
            _pay_to_code = string.Empty;
            _pay_to_name = string.Empty;
            //***********************************************************************

        }

        #endregion Constructor

        #region Public Properties

        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
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
        public decimal credit_limit
        {
            get { return _credit_limit; }
            set { _credit_limit = value; }
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
        public string spec_billing
        {
            get { return _spec_billing; }
            set { _spec_billing = value; }
        }
        public long ap_acct_dflt
        {
            get { return _ap_acct_dflt; }
            set { _ap_acct_dflt = value; }
        }
        public string ap_department_dflt
        {
            get { return ap_department_dflt; }
            set { ap_department_dflt = value; }
        }
        public string last_pay_date
        {
            get { return _last_pay_date; }
            set { _last_pay_date = value; }
        }
        public string hold_pymnt
        {
            get { return _hold_pymnt; }
            set { _hold_pymnt = value; }
        }
        public string take_dscnt
        {
            get { return _take_dscnt; }
            set { _take_dscnt = value; }
        }
        public decimal acct_bal
        {
            get { return _acct_bal; }
            set { _acct_bal = value; }
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
        public string spec_shipping
        {
            get { return _spec_shipping; }
            set { _spec_shipping = value; }
        }
        public string taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }
        public string bo_allowed
        {
            get { return _bo_allowed; }
            set { _bo_allowed = value; }
        }
        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }
        public string buyer_code
        {
            get { return _buyer_code; }
            set { _buyer_code = value; }
        }
        public string trd_ds_code
        {
            get { return _trd_ds_code; }
            set { _trd_ds_code = value; }
        }
        public int eta_days
        {
            get { return _eta_days; }
            set { _eta_days = value; }
        }
        public string st_tx_code
        {
            get { return _st_tx_code; }
            set { _st_tx_code = value; }
        }
        public string co_tx_code
        {
            get { return _co_tx_code; }
            set { co_tx_code = value; }
        }
        public string ci_tx_code
        {
            get { return _ci_tx_code; }
            set { _ci_tx_code = value; }
        }
        public long cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no = value; }
        }
        public string cash_department
        {
            get { return _cash_department; }
            set { _cash_department = value; }
        }
        public long exp_acct_no
        {
            get { return _exp_acct_no; }
            set { _exp_acct_no = value; }
        }
        public string exp_department
        {
            get { return _exp_department; }
            set { _exp_department = value; }
        }
        public string print_1099
        {
            get { return _print_1099; }
            set { _print_1099 = value; }
        }
        public string federal_tax_id
        {
            get { return _federal_tax_id; }
            set { _federal_tax_id = value; }
        }
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string acct_bal_date
        {
            get { return _acct_bal_date; }
            set { _acct_bal_date = value; }
        }
        public string on_acct_date
        {
            get { return _on_acct_date; }
            set { _on_acct_date = value; }
        }
        public string sdb_code
        {
            get { return _sdb_code; }
            set { _sdb_code = value; }
        }
        public int vendor_rating
        {
            get { return _vendor_rating; }
            set { _vendor_rating = value; }
        }
        public string fax_phone
        {
            get { return _fax_phone; }
            set { _fax_phone = value; }
        }
        public string telex_no
        {
            get { return _telex_no; }
            set { _telex_no = value; }
        }
        public string mtax_frght
        {
            get { return _mtax_frght; }
            set { _mtax_frght = value; }
        }
        public string mtax_misc
        {
            get { return _mtax_misc; }
            set { _mtax_misc = value; }
        }

        //**********************************Pay To Vendor************************
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
        //***********************************************************************



        #endregion Public Properties

        #region Stored-Procedures

       

        public override string INSERT_SPNAME
        {
            get { return "uspappaytvenins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspappaytvenupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspappaytvendel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspappaytvenget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspappaytvengetall"; }
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
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  stpvendr.vend_code, stppytor.pay_to_code, stppytor.pay_to_name, stppytor.contact, stppytor.phone, stppytor.address1,");
            sql.Append("stppytor.address2, stppytor.city, stppytor.state, stppytor.zip, stppytor.country, stppytor.spec_billing,");
            sql.Append("stppytor.bo_allowed, stppytor.taxable, stppytor.take_dscnt, stppytor.eta_days, stppytor.buyer_code,");
            sql.Append("stppytor.trd_ds_code,stppytor.pay_method FROM  stpvendr,stppytor ");
            sql.Append("where stpvendr.vend_code = stppytor.vend_code ");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != string.Empty)//vend_code
                sql.Append(" and stpvendr.vend_code = '" + parameters[0].ToString().Replace("'", "''") + "'");


            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
