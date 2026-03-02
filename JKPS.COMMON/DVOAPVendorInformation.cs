using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAPVendorInformation : DVOBase
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
        private int  _ap_acct_dflt;
        private string _ap_acct_keyvalue;
        private string _ap_acct_type;
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
        private string _cash_acct_no;
        private string _cash_acct_keyvalue;
        private string _cash_department;
        private string _exp_acct_no;
        private int _exp_acct_number;
        private string _exp_acct_type;
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
        private int _Rowid;
        #region Constructor

        public DVOAPVendorInformation()
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

            _ap_acct_dflt = 0; //string.Empty;
            _ap_acct_keyvalue = string.Empty;
            _ap_acct_type = string.Empty;
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
            _cash_acct_no = string.Empty;
            _cash_acct_keyvalue = string.Empty;
            _cash_department = string.Empty;
            _exp_acct_no = string.Empty;
            _exp_acct_number=0;
            _exp_acct_type = string.Empty;
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
            _Rowid = 0;

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
        public int  ap_acct_dflt
        {
            get { return _ap_acct_dflt; }
            set { _ap_acct_dflt = value; }
        }
        public string ap_acct_keyvalue
        {
            get { return _ap_acct_keyvalue; }
            set { _ap_acct_keyvalue = value; }
        }
        public string ap_acct_type
        {
            get { return _ap_acct_type; }
            set { _ap_acct_type = value; }
        }
        public string ap_department_dflt
        {
            get { return _ap_department_dflt; }
            set { _ap_department_dflt = value; }
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
            set { _co_tx_code = value; }
        }
        public string ci_tx_code
        {
            get { return _ci_tx_code; }
            set { _ci_tx_code = value; }
        }
        public string cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no = value; }
        }
        public string cash_acct_keyvalue
        {
            get { return _cash_acct_keyvalue; }
            set { _cash_acct_keyvalue = value; }
        }
        public string cash_department
        {
            get { return _cash_department; }
            set { _cash_department = value; }
        }
        public string exp_acct_no
        {
            get { return _exp_acct_no; }
            set { _exp_acct_no = value; }
        }
        public int  exp_acct_number
        {
            get { return _exp_acct_number; }
            set { _exp_acct_number = value; }
        }
      
        public string exp_acct_type
        {
            get { return _exp_acct_type; }
            set { _exp_acct_type = value; }
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

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }


        #endregion Public Properties

        #region Stored-Procedures

        //****************** Added by Bharat ******************

        public string VENDOR_PERSONAL_INFO
        {
            get { return "usp_get_vendorinfo"; }
        }
        public string GET_VENDOR_LEDGER_DOC
        {
            get { return "uspapvndldgrdocget"; }
        }
        public string GET_VENDOR_LEDGER_INV
        {
            get { return "uspapvndldgrinvget"; }
        }
        public string GET_VENDOR_AGING_CODE
        {
            get { return "uspapvndagingget"; }
        }
        public string GET_VENDOR_CASH_REQUIRE_CODE
        {
            get { return "uspapvndcshrqrget"; }
        }
        //*****************************************************       

        public override string INSERT_SPNAME
        {
            get { return "uspapvinfins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspapvinfupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspapvinfdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspapvinfget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspapvinfgetall"; }
        }



        public override string TABLE_NAME
        {
            get { return "MasterVendor"; }
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

        public string GET_ROWID
        {
            get { return "uspxvendrrowid"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT vend_code,bus_name,contact,phone,address1,address2,city,state,zip,country,");
            sql.Append("credit_limit,terms_code,act_grp,spec_billing,ap_acct_dflt,ap_department_dflt,last_pay_date, ");
            sql.Append("hold_pymnt,take_dscnt,acct_bal,on_acct_amt,arch_bal,spec_shipping,taxable, bo_allowed,");
            sql.Append("pay_method,buyer_code,trd_ds_code,eta_days,st_tx_code,co_tx_code,ci_tx_code,");
            sql.Append("cash_acct_no,cash_department,exp_acct_no,exp_department,print_1099,");
            sql.Append("federal_tax_id,currency_code,acct_bal_date,on_acct_date,sdb_code,");
            sql.Append("vendor_rating,fax_phone,telex_no,mtax_frght,mtax_misc,keyvalue AS ap_account,MasterVendor.vend_code_id,");
            sql.Append("(SELECT keyvalue FROM PayrollGLAccounts WHERE (acct_no = MasterVendor.cash_acct_no)) AS cash_account,");
            sql.Append("(SELECT acct_type FROM PayrollGLAccounts WHERE (acct_no = MasterVendor.ap_acct_dflt)) AS ap_acct_type,");
            sql.Append("(SELECT keyvalue FROM PayrollGLAccounts WHERE (acct_no = MasterVendor.exp_acct_no)) AS exp_account, ");
            sql.Append("(SELECT acct_type FROM PayrollGLAccounts WHERE (acct_no = MasterVendor.exp_acct_no)) AS exp_acct_type ");
            sql.Append("FROM MasterVendor LEFT Outer JOIN  PayrollGLAccounts ON MasterVendor.ap_acct_dflt = PayrollGLAccounts.acct_no where 1=1");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(vend_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bus_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(contact) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(address1) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(address2) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(city) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(state) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
           




            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(zip) LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(country) LIKE '" + parameters[9].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[15] != null)
                if (parameters[15].ToString() != string.Empty)
                    sql.Append(" AND last_pay_date = '" + Convert.ToDateTime(parameters[15]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            
            if (parameters[21] != null)
                if (parameters[21].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(taxable) LIKE '" + parameters[21].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[22] != null)
                if (parameters[22].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bo_allowed) LIKE '" + parameters[22].ToString().Trim().Replace("'", "''") + "%'");



            if (parameters[32] != null)
                if (parameters[32].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mtax_frght) LIKE '" + parameters[32].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[33] != null)
                if (parameters[33].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mtax_misc) LIKE '" + parameters[33].ToString().Trim().Replace("'", "''") + "%'");

            //********************Added By Sunil Pahwa on 02/02/09*******************************
            if (Convert.ToInt32(parameters[34]) > 0)
                sql.Append(" AND MasterVendor.vend_code_id = " + parameters[34].ToString());
            //***********************************************************************************

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
