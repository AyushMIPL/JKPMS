using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPOInvoicesStuinvce : DVOBase
    {
        int _rowid;
        int _inv_doc_no;
        int _inv_post_no;
        string _inv_post_date;//datetime,
        string _pay_to_code;
        string _description;
        string _inv_date;//datetime,
        string _inv_no;
        string _terms_code;
        string _pay_date;//datetime,
        string _due_date;//datetime,
        string _discount_date;//datetime,
        decimal _discount_percent;
        string _po_no;
        int _po_doc_no;
        decimal _misc_amount;
        decimal _frght_amount;
        decimal _goods_total;
        decimal _tax_total;
        decimal _inv_total;
        decimal _diff_total;
        string _ok_to_post;
        string _currency_code;
        string _curr_rate_type;
        decimal _currency_rate;
        int _batch_id;
        int _insertby;
        string _insertdate;//datetime,
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;//datetime,
        string _updatemachineinfo;
        

        #region Constructor

        public DVOPOInvoicesStuinvce()
        {
            _rowid = 0;
            _inv_doc_no = 0;
            _inv_post_no = 0;
            _inv_post_date = "01/01/1900";//datetime,
            _pay_to_code = string.Empty;
            _description = string.Empty;
            _inv_date = "01/01/1900";//datetime,
            _inv_no = string.Empty;
            _terms_code = string.Empty;
            _pay_date = "01/01/1900";//datetime,
            _due_date = "01/01/1900";//datetime,
            _discount_date = "01/01/1900";//datetime,
            _discount_percent = 0;
            _po_no = string.Empty;
            _po_doc_no = 0;
            _misc_amount = 0;
            _frght_amount = 0;
            _goods_total = 0;
            _tax_total = 0;
            _inv_total = 0;
            _diff_total = 0;
            _ok_to_post = string.Empty;
            _currency_code = string.Empty;
            _curr_rate_type = string.Empty;
            _currency_rate = 0;
            _batch_id = 0;
            _insertby = 0;
            _insertdate = "01/01/1900";//datetime,
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//datetime,
            _updatemachineinfo = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public int inv_post_no
        {
            get { return _inv_post_no; }
            set { _inv_post_no = value; }
        }
        public string inv_post_date
        {
            get { return _inv_post_date; }
            set { _inv_post_date = value; }
        }
        public string pay_to_code
        {
            get { return _pay_to_code; }
            set { _pay_to_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string inv_date
        {
            get { return _inv_date; }
            set { _inv_date = value; }
        }
        public string inv_no
        {
            get { return _inv_no; }
            set { _inv_no = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string pay_date
        {
            get { return _pay_date; }
            set { _pay_date = value; }
        }
        public string due_date
        {
            get { return _due_date; }
            set { _due_date = value; }
        }
        public string discount_date
        {
            get { return _discount_date; }
            set { _discount_date = value; }
        }
        public decimal discount_percent
        {
            get { return _discount_percent; }
            set { _discount_percent = value; }
        }
        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }
        public int po_doc_no
        {
            get { return _po_doc_no; }
            set { _po_doc_no = value; }
        }
        public decimal misc_amount
        {
            get { return _misc_amount; }
            set { _misc_amount = value; }
        }
        public decimal frght_amount
        {
            get { return _frght_amount; }
            set { _frght_amount = value; }
        }
        public decimal goods_total
        {
            get { return _goods_total; }
            set { _goods_total = value; }
        }
        public decimal tax_total
        {
            get { return _tax_total; }
            set { _tax_total = value; }
        }
        public decimal inv_total
        {
            get { return _inv_total; }
            set { _inv_total = value; }
        }
        public decimal diff_total
        {
            get { return _diff_total; }
            set { _diff_total = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string curr_rate_type
        {
            get { return _curr_rate_type; }
            set { _curr_rate_type = value; }
        }
        public decimal currency_rate
        {
            get { return _currency_rate; }
            set { _currency_rate = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usppoinvins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppoinvupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppoinvdel"; }
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
            get { return "stuinvce"; }
        }
        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT i1.rowid p_rowid,i1.inv_doc_no p_inv_doc_no,i1.inv_post_no p_inv_post_no,i1.inv_post_date p_inv_post_date,");
            sql.Append(" i1.pay_to_code p_pay_to_code,i1.description p_description,i1.inv_date p_inv_date,i1.inv_no p_inv_no,");
            sql.Append(" i1.terms_code p_terms_code,i1.pay_date p_pay_date,i1.due_date p_due_date,i1.discount_date p_discount_date,");
            sql.Append(" i1.discount_percent p_discount_percent,i1.po_no p_po_no,i1.po_doc_no p_po_doc_no,i1.misc_amount p_misc_amount,");
            sql.Append(" i1.frght_amount p_frght_amount,i1.goods_total p_goods_total,i1.tax_total p_tax_total,i1.inv_total p_inv_total,");
            sql.Append(" i1.diff_total p_diff_total,i1.ok_to_post p_ok_to_post,i1.currency_code p_currency_code,i1.curr_rate_type p_curr_rate_type,");
            sql.Append(" i1.currency_rate p_currency_rate,i1.batch_id p_batch_id");
            sql.Append(" FROM stuinvce i1 WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND i1.rowid = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND i1.inv_doc_no = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND i1.inv_post_no = " + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() != string.Empty && !parameters[3].ToString().Trim().Contains("1900"))
                    sql.Append(" AND i1.inv_post_date = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.pay_to_code) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.description) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim() != string.Empty && !parameters[6].ToString().Trim().Contains("1900"))
                    sql.Append(" AND i1.inv_date = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.inv_no) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.terms_code) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString().Trim() != string.Empty && !parameters[9].ToString().Trim().Contains("1900"))
                    sql.Append(" AND i1.pay_date = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString().Trim() != string.Empty && !parameters[10].ToString().Trim().Contains("1900"))
                    sql.Append(" AND i1.due_date = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString().Trim() != string.Empty && !parameters[11].ToString().Trim().Contains("1900"))
                    sql.Append(" AND i1.discount_date = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[12]) > 0)
                sql.Append(" AND i1.discount_percent = " + parameters[12].ToString());
            if (parameters[13] != null)
                if (parameters[13].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.po_no) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND i1.po_doc_no = " + parameters[14].ToString());
            if (Convert.ToDecimal(parameters[15]) > 0)
                sql.Append(" AND i1.misc_amount = " + parameters[15].ToString());
            if (Convert.ToDecimal(parameters[16]) > 0)
                sql.Append(" AND i1.frght_amount = " + parameters[16].ToString());
            if (Convert.ToDecimal(parameters[17]) > 0)
                sql.Append(" AND i1.goods_total = " + parameters[17].ToString());
            if (Convert.ToDecimal(parameters[18]) > 0)
                sql.Append(" AND i1.tax_total = " + parameters[18].ToString());
            if (Convert.ToDecimal(parameters[19]) > 0)
                sql.Append(" AND i1.inv_total = " + parameters[19].ToString());
            if (Convert.ToDecimal(parameters[20]) > 0)
                sql.Append(" AND i1.diff_total = " + parameters[20].ToString());
            if (parameters[21] != null)
                if (parameters[21].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.ok_to_post) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[22] != null)
                if (parameters[22].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.currency_code) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[23] != null)
                if (parameters[23].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(i1.curr_rate_type) = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[24]) > 0)
                sql.Append(" AND i1.currency_rate = " + parameters[24].ToString());
            if (Convert.ToInt32(parameters[25]) > 0)
                sql.Append(" AND i1.batch_id = " + parameters[25].ToString());

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
