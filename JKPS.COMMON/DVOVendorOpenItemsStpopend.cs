using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Created By: Bharat Dhall
    /// Date Create: 07 October, 2008
    /// </summary>
    public class DVOVendorOpenItemsStpopend : DVOBase
    {
        private int _RowID;
        private string _vend_code;
        private string _pay_to_code;
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
        private int _ap_acct_no;
        private string _ap_department;
        private string _po_no;
        private string _po_date;
        private decimal _to_pay_amt;
        private decimal _to_take_disc;
        private string _to_pay_date;
        private int _cash_acct_no;
        private string _cash_department;
        private string _currency_code;
        private decimal _curr_ex_rate;
        private decimal _home_curr_amount;
        private string _last_pay_date;

        private string _pay_method;

        #region Constructor

        public DVOVendorOpenItemsStpopend()
        {
            _RowID = 0;
            _vend_code = string.Empty;
            _pay_to_code = string.Empty;
            _inv_no = string.Empty;
            _doc_no = 0;
            _inv_desc = string.Empty;
            _inv_date = string.Empty;
            _orig_amount = 0;
            _disc_amt = 0;
            _balance = 0;
            _disc_bal = 0;
            _due_date = string.Empty;
            _disc_date = string.Empty;
            _ap_acct_no = 0;
            _ap_department = string.Empty;
            _po_no = string.Empty;
            _po_date = string.Empty;
            _to_pay_amt = 0;
            _to_take_disc = 0;
            _to_pay_date = string.Empty;
            _cash_acct_no = 0;
            _cash_department = string.Empty;
            _currency_code = string.Empty;
            _curr_ex_rate = 0;
            _home_curr_amount = 0;
            _last_pay_date = string.Empty;
            _pay_method = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string pay_to_code
        {
            get { return _pay_to_code; }
            set { _pay_to_code = value; }
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
        public int ap_acct_no
        {
            get { return _ap_acct_no; }
            set { _ap_acct_no = value; }
        }
        public string ap_department
        {
            get { return _ap_department; }
            set { _ap_department = value; }
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
        public int cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no = value; }
        }
        public string cash_department
        {
            get { return _cash_department; }
            set { _cash_department = value; }
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
        public string last_pay_date
        {
            get { return _last_pay_date; }
            set { _last_pay_date = value; }
        }

        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        //*********************  Added by Bharat Dhall [31 December, 2008] ******************
        public string VENDOR_PENDING_INVOICES
        {
            get { return "uspVndPndInvGet"; }
        }
        //**********************************************************************************

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
            get { return "uspVndOpnItmGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspVndOpnItmGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "stpopend"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT op.vend_code p_vend_code,op.pay_to_code p_pay_to_code,op.inv_no p_inv_no,op.doc_no p_doc_no,op.inv_desc v_inv_desc,");
            sql.Append(" op.inv_date p_inv_date,orig_amount v_orig_amount,disc_amt v_disc_amt,balance v_balance,disc_bal v_disc_bal,");
            sql.Append(" op.due_date p_due_date,op.disc_date p_disc_date ,op.ap_acct_no p_ap_acct_no ,op.ap_department v_ap_department,");
            sql.Append(" op.po_no p_po_no,op.po_date p_po_date,to_pay_amt v_to_pay_amt,to_take_disc v_to_take_disc,op.to_pay_date v_to_pay_date,");
            sql.Append(" op.cash_acct_no p_cash_acct_no,op.cash_department v_cash_department,op.currency_code v_currency_code,op.curr_ex_rate v_curr_ex_rate,");
            sql.Append(" op.home_curr_amount v_home_curr_amount,last_pay_date v_last_pay_date,inv.pay_method v_pay_method,op.RowId v_RowId");
            sql.Append(" FROM stpopend op, OUTER stpinvce inv");
            sql.Append(" WHERE op.doc_no=inv.doc_no and op.balance<>0 ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(op.vend_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(op.pay_to_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(op.inv_no) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND op.doc_no = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND op.inv_date = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND op.due_date = '" + parameters[5].ToString().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND op.disc_date = '" + parameters[6].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND op.ap_acct_no = " + parameters[7].ToString());
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(op.po_no) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND op.po_date = '" + parameters[9].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND op.cash_acct_no = " + parameters[10].ToString());
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND op.RowId = " + parameters[11].ToString());

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
