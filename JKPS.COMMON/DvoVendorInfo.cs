using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{


    public class DvoVendorInfo : DVOBase
    {
        private string _vend_code;
        private string _bus_name;

        private string _zip;

        //private string _terms_code;

        public DvoVendorInfo()
        {

            _vend_code = string.Empty;
            _bus_name = string.Empty;

            _zip = string.Empty;


        }



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



        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
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
            get { return "uspVenInfoGet"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  r.vend_code p_vend_code, r.bus_name p_bus_name,r.contact p_contact,");
            sql.Append(" r.phone p_phone ,r.address1 p_address1,r.address2 p_address2, r.city p_city,");
            sql.Append(" r.state p_state ,r.zip p_zip,r.country p_country,r.credit_limit p_credit_limit,");
            sql.Append(" r.terms_code p_terms_code,r.act_grp p_act_grp ,r.spec_billing p_spec_billing,");
            sql.Append(" r.last_pay_date p_last_pay_date ,r.hold_pymnt p_hold_pymnt ,r.take_dscnt ");
            sql.Append(" p_take_dscnt,r.acct_bal  p_acct_bal , r.on_acct_amt p_on_acct_amt ,");
            sql.Append(" r.print_1099 p_print_1099 ,s.keyvalue p_keyvalue,r.ap_acct_dflt p_ap_acct_dflt,");
            sql.Append(" r.ap_department_dflt p_ap_dept_dflt,r.cash_acct_no p_cash_acct_no,r.cash_department");
            sql.Append(" p_cash_department,r.federal_tax_id p_federal_tax_id,r.currency_code p_currency_code ");
            sql.Append("  from stpvendr r,outer PayrollGLAccounts s");
            sql.Append(" WHERE r.cash_acct_no=s.acct_no  ");


            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(vend_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bus_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND zip = '" + parameters[2].ToString().Trim()+"'");


            return sql.ToString();
        }


        public string FIND_VENDOR_BALANCE_INFO(ref Object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT  stpvendr.vend_code p_vend_code,stpvendr.bus_name p_bus_name, ");
            sql.Append(" stpvendr.contact p_contact,stpvendr.phone p_phone,stpvendr.acct_bal p_acct_bal,");
            sql.Append(" stpopend.balance p_balance,stpopend.inv_no p_inv_no,stpopend.inv_desc p_inv_desc,");
            sql.Append(" stpopend.inv_date p_inv_date,stpopend.disc_amt p_disc_amt,stpopend.disc_bal p_disc_bal,stpopend.orig_amount p_orig_amount");
            sql.Append(" from stpinvce,stpvendr,stpopend");
            sql.Append(" where stpinvce.vend_code=stpopend.vend_code ");
            sql.Append(" and stpinvce.vend_code=stpvendr.vend_code ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stpvendr.vend_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stpvendr.bus_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

          
            return sql.ToString();
        }

        //Added By Rahul jain using in Print Vendor Invoice Detail
        public string FIND_VENDOR_INVOICE_DETAILS
        {
            get { return "uspvndinvdetail"; }
        }

    }

}

