using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPayableListingStpinvce : DVOBase
    {
        private int _doc_no;
        private string _inv_no;
        private string _department;
        private string _file_type;
        private int _ref_no;
        private string _inv_desc;
        private string _doc_date;
        private string _vend_code;
        private string _pay_to_code;
        private string _posted;
        private string _recurring;
        private string _terms_code;
        private string _inv_date;
        private string _to_pay_date;
        private string _due_date;
        private string _disc_date;
        private decimal _disc_pct;
        private string _po_date;
        private string _po_no;
        private int _disc_acct_no;
        private string _disc_department;
        private decimal _disc_amount;
        private string _disc_debit_credit;
        private int _ap_acct_no;
        private string _ap_department;
        private decimal _ap_amount;
        private string _ap_debit_credit;
        private string _ok_to_post;
        private int _cash_acct_no;
        private string _cash_department;
        private string _recurr_ref;
        private string _def_mtaxcd;
        private string _gross_entry;
        private string _currency_code;
        private decimal _curr_ex_rate;
        private decimal _home_curr_amount;
        private string _fix_date_flag;
        private int _batch_id;
        private int _recurr_cnt;
        private string _min_voucher_no;
        private string _tre_voucher_no;
        private int _required_approval;
        private int _current_approval;
        private int _acd_id;
        private string _pay_method;
        private int _line_no;
        private decimal _goods_amt;
        private int _check;
        private string _orig_journal;
        private decimal _amount;

        private int _inv_doc_no;
        private int _inv_chg_flag;

        private int _rowid;
        #region Constructor

        public DVOPayableListingStpinvce()
        {
            _doc_no = 0;
            _inv_no = string.Empty;
            _department = string.Empty;
            _file_type = string.Empty;
            _ref_no = 0;
            _inv_desc = string.Empty;
            _doc_date = string.Empty;
            _vend_code = string.Empty;
            _pay_to_code = string.Empty;
            _posted = string.Empty;
            _recurring = string.Empty;
            _terms_code = string.Empty;
            _inv_date = string.Empty;
            _to_pay_date = string.Empty;
            _due_date = string.Empty;
            _disc_date = string.Empty;
            _disc_pct = 0;
            _po_date = string.Empty;
            _po_no = string.Empty;
            _disc_acct_no = 0;
            _disc_department = string.Empty;
            _disc_amount = 0;
            _disc_debit_credit = string.Empty;
            _ap_acct_no = 0;
            _ap_department = string.Empty;
            _ap_amount = 0;
            _ap_debit_credit = string.Empty;
            _ok_to_post = string.Empty;
            _cash_acct_no = 0;
            _cash_department = string.Empty;
            _recurr_ref = string.Empty;
            _def_mtaxcd = string.Empty;
            _gross_entry = string.Empty;
            _currency_code = string.Empty;
            _curr_ex_rate = 0;
            _home_curr_amount = 0;
            _fix_date_flag = string.Empty;
            _batch_id = 0;
            _recurr_cnt = 0;
            _min_voucher_no = string.Empty;
            _tre_voucher_no = string.Empty;
            _required_approval = 0;
            _current_approval = 0;
            _acd_id = 0;
            _pay_method = string.Empty;
            _line_no = 0;
            _goods_amt = 0;
            _check = 0;
            _orig_journal = string.Empty;
            _batch_id = 0;
            _amount = 0.0M;

            _inv_doc_no = 0;
            _inv_chg_flag = 0;
            _rowid = 0;
        }

        #endregion Constructor


        #region Public Properties

        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string inv_no
        {
            get { return _inv_no; }
            set { _inv_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string file_type
        {
            get { return _file_type; }
            set { _file_type = value; }
        }
        public int ref_no
        {
            get { return _ref_no; }
            set { _ref_no = value; }
        }
        public string inv_desc
        {
            get { return _inv_desc; }
            set { _inv_desc = value; }
        }
        public string doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
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
        public string posted
        {
            get { return _posted; }
            set { _posted = value; }
        }
        public string recurring
        {
            get { return _recurring; }
            set { _recurring = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string inv_date
        {
            get { return _inv_date; }
            set { _inv_date = value; }
        }
        public string to_pay_date
        {
            get { return _to_pay_date; }
            set { _to_pay_date = value; }
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
        public decimal disc_pct
        {
            get { return _disc_pct; }
            set { _disc_pct = value; }
        }
        public string po_date
        {
            get { return _po_date; }
            set { _po_date = value; }
        }
        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }
        public int disc_acct_no
        {
            get { return _disc_acct_no; }
            set { _disc_acct_no = value; }
        }
        public string disc_department
        {
            get { return _disc_department; }
            set { _disc_department = value; }
        }
        public decimal disc_amount
        {
            get { return _disc_amount; }
            set { _disc_amount = value; }
        }
        public string disc_debit_credit
        {
            get { return _disc_debit_credit; }
            set { _disc_debit_credit = value; }
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
        public decimal ap_amount
        {
            get { return _ap_amount; }
            set { _ap_amount = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string ap_debit_credit
        {
            get { return _ap_debit_credit; }
            set { _ap_debit_credit = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
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
        public string recurr_ref
        {
            get { return _recurr_ref; }
            set { _recurr_ref = value; }
        }
        public string def_mtaxcd
        {
            get { return _def_mtaxcd; }
            set { _def_mtaxcd = value; }
        }
        public string gross_entry
        {
            get { return _gross_entry; }
            set { _gross_entry = value; }
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
        public string fix_date_flag
        {
            get { return _fix_date_flag; }
            set { _fix_date_flag = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public int recurr_cnt
        {
            get { return _recurr_cnt; }
            set { _recurr_cnt = value; }
        }
        public string min_voucher_no
        {
            get { return _min_voucher_no; }
            set { _min_voucher_no = value; }
        }
        public string tre_voucher_no
        {
            get { return _tre_voucher_no; }
            set { _tre_voucher_no = value; }
        }
        public int required_approval
        {
            get { return _required_approval; }
            set { _required_approval = value; }
        }
        public int current_approval
        {
            get { return _current_approval; }
            set { _current_approval = value; }
        }
        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }

        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public decimal goods_amt
        {
            get { return _goods_amt; }
            set { _goods_amt = value; }
        }
        public int check
        {
            get { return _check; }
            set { _check = value; }
        }
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }

        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public int inv_chg_flag
        {
            get { return _inv_chg_flag; }
            set { _inv_chg_flag = value; }
        }

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        #endregion Public Properties


        #region Stored-Procedures
        public string GET_PAY_LISTING
        {
            get { return "usppaylistingrpt"; }
        }
        public string CHECK_POST
        {
            get { return "uspcheckpost1"; }
        }

        //*******************************Added by Bharat Dhall *************************************
        public string VALIDATE_ACCOUNT_ENTRY
        {
            get { return "uspValActEntry"; }
        }
        //******************************************************************************************
        //*******************************Added by Rohit Wadhwa *************************************
        public string VALIDATE_ACCOUNTNEXPENSE
        {
            get { return "uspchkexp"; }
        }
        //******************************************************************************************

        //******************************Sunil Pahwa*************************************************
        public string GET_COUNT_FROM_STPINVCE
        {
            get { return "uspimosinvceget"; }
        }
        public string UPD_STPINVCE
        {
            get { return "uspimstinvUpd"; }
        }
        //Sunil Pahwa on[10/8/09] for Order Entry Edit List
        public string GET_ORDER_ENTRY_INFO
        {
            get { return "uspordrentryedtget"; }
        }

        //*******************************************************************************************
        public override string INSERT_SPNAME
        {
            get { return "usppaylistingIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppaylistingUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppaylistingDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usppaylistingGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "usppaylistingGetAl"; }
        }
        public override string TABLE_NAME
        {
            get { return "stpinvce"; }
        }

        public override int UNIQUE_ID
        {
            get { return _doc_no; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT doc_no AS p_doc_no, inv_no AS p_inv_no, department AS v_department, file_type AS p_file_type, ref_no AS p_ref_no, ");
            sql.Append(" inv_desc AS p_inv_desc, doc_date AS p_doc_date, vend_code AS p_vend_code, pay_to_code AS p_pay_to_code, posted AS p_posted,  ");
            sql.Append(" recurring AS p_recurring, terms_code AS p_terms_code, inv_date AS p_inv_date, to_pay_date AS p_to_pay_date,  ");
            sql.Append(" due_date AS p_due_date, disc_date AS p_disc_date, disc_pct AS p_disc_pct, po_date AS p_po_date, po_no AS p_po_no,  ");
            sql.Append(" disc_acct_no AS v_disc_acct_no, disc_department AS v_disc_department, disc_amount AS v_disc_amount,  ");
            sql.Append(" disc_debit_credit AS v_disc_dbt_crdt, ap_acct_no AS v_ap_acct_no, ap_department AS v_ap_department,  ");
            sql.Append(" ap_amount AS v_ap_amount, ap_debit_credit AS v_ap_debit_credit, ok_to_post AS v_ok_to_post, cash_acct_no AS v_cash_acct_no,  ");
            sql.Append(" cash_department AS v_cash_department, recurr_ref AS v_recurr_ref, def_mtaxcd AS v_def_mtaxcd, gross_entry AS v_gross_entry,  ");
            sql.Append(" currency_code AS v_currency_code, curr_ex_rate AS v_curr_ex_rate, home_curr_amount AS v_home_curr_amount,  ");
            sql.Append(" fix_date_flag AS p_fix_date_flag, batch_id AS p_batch_id, recurr_cnt AS p_recurr_cnt, min_voucher_no AS v_min_voucher_no,  ");
            sql.Append(" tre_voucher_no AS v_tre_voucher_no, required_approval AS v_require_approval, current_approval AS v_current_approval,  ");
            sql.Append(" acd_id AS v_acd_id, pay_method AS p_pay_method ");
            sql.Append(" FROM stpinvce ");
            sql.Append(" WHERE 1=1 ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append("and  doc_no = " + parameters[0].ToString()); //Modified By Rahul Jain on 19/01/2009
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(inv_no) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(file_type) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND ref_no = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(inv_desc) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND doc_date = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(vend_code) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(pay_to_code) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(posted) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
                else
                    sql.Append(" AND Rtrim(posted) NOT IN ('P','C')");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(recurring) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(terms_code)= '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND inv_date = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND to_pay_date = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)
                    sql.Append(" AND due_date = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[14] != null)
                if (parameters[14].ToString() != string.Empty)
                    sql.Append(" AND disc_date = '" + parameters[14].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[15]) > 0)
                sql.Append(" AND disc_pct = " + parameters[15].ToString());
            if (parameters[16] != null)
                if (parameters[16].ToString() != string.Empty)
                    sql.Append(" AND po_date = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[17] != null)
                if (parameters[17].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(po_no) = '" + parameters[17].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[18] != null)
                if (parameters[18].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(fix_date_flag) = '" + parameters[18].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[19]) > 0)
                sql.Append(" AND recurr_cnt = " + parameters[19].ToString());
            if (parameters[20] != null)
                if (parameters[20].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(pay_method) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[21]) > 0)
                sql.Append(" AND batch_id = " + parameters[21].ToString());

            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
