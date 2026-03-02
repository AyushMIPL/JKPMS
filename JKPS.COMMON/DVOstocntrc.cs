using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstocntrc : DVOBase
    {
        private string _disc_frght;
        private string _tax_frght;
        private string _taxable;
        private string _st_tx_code;
        private string _co_tx_code;
        private string _ci_tx_code;
        private string _warehouse_code;
        private int _retention_days;
        private int _due_days;
        private string _cm_reason;
        private string _dm_reason;
        private int _ar_acct_no;
        private int _cash_acct_no;
        private int _visa_acct_no;
        private int _sales_acct_no;
        private int _disc_acct_no;
        private int _frght_acct_no;
        private int _inv_acct_no;
        private int _cog_acct_no;
        private int _scrap_acct_no;
        private string _use_department;
        private int _oe_doc_no;
        private int _oe_inv_doc_no;
        private int _oe_post_no;
        private string _order_type;
        private string _line_type;
        private string _terms_code;
        private string _change_terms;
        private string _cod_ok;
        private string _one_time_cust;
        private string _ack_kit_exp;
        private string _pic_kit_exp;
        private string _mfs_kit_exp;
        private string _inv_kit_exp;
        private string _ack_note;
        private string _pic_note;
        private string _shp_note;
        private string _inv_note;
        private string _pay_method;
        private string _fob_point;
        private string _ship_via;
        private string _mtaxg_code;
        private string _use_batch_inv;
        private string _use_approv_post;
        private string _approval_code;
        private int _rowid;
        public DVOstocntrc()
        {
            _rowid = 0;
            _disc_frght = string.Empty;
            _tax_frght = string.Empty;
            _taxable = string.Empty;
            _st_tx_code = string.Empty;
            _co_tx_code = string.Empty;
            _ci_tx_code = string.Empty;
            _warehouse_code = string.Empty;
            _retention_days = 0;
            _due_days = 0;
            _cm_reason = string.Empty;
            _dm_reason = string.Empty;
            _ar_acct_no = 0;
            _cash_acct_no = 0;
            _visa_acct_no = 0;
            _sales_acct_no = 0;
            _disc_acct_no = 0;
            _frght_acct_no = 0;
            _inv_acct_no = 0;
            _cog_acct_no = 0;
            _scrap_acct_no = 0;
            _use_department = string.Empty;
            _oe_doc_no = 0;
            _oe_inv_doc_no = 0;
            _oe_post_no = 0;
            _order_type = string.Empty;
            _line_type = string.Empty;
            _terms_code = string.Empty;
            _change_terms = string.Empty;
            _cod_ok = string.Empty;
            _one_time_cust = string.Empty;
            _ack_kit_exp = string.Empty;
            _pic_kit_exp = string.Empty;
            _mfs_kit_exp = string.Empty;
            _inv_kit_exp = string.Empty;
            _ack_note = string.Empty;
            _pic_note = string.Empty;
            _shp_note = string.Empty;
            _inv_note = string.Empty;
            _pay_method = string.Empty;
            _fob_point = string.Empty;
            _ship_via = string.Empty;
            _mtaxg_code = string.Empty;
            _use_batch_inv = string.Empty;
            _use_approv_post = string.Empty;
            _approval_code = string.Empty;
        }

        public string disc_frght
        {
            get { return _disc_frght; }
            set { _disc_frght = value; }
        }
        public string tax_frght
        {
            get { return _tax_frght; }
            set { _tax_frght = value; }
        }
        public string taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
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
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public int retention_days
        {
            get { return _retention_days; }
            set { _retention_days = value; }
        }
        public int due_days
        {
            get { return _due_days; }
            set { _due_days = value; }
        }
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string cm_reason
        {
            get { return _cm_reason; }
            set { _cm_reason = value; }
        }
        public string dm_reason
        {
            get { return _dm_reason; }
            set { _dm_reason = value; }
        }
        public int ar_acct_no
        {
            get { return _ar_acct_no; }
            set { _ar_acct_no = value; }
        }
        public int cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no = value; }
        }
        public int visa_acct_no
        {
            get { return _visa_acct_no; }
            set { _visa_acct_no = value; }
        }
        public int sales_acct_no
        {
            get { return _sales_acct_no; }
            set { _sales_acct_no = value; }
        }
        public int disc_acct_no
        {
            get { return _disc_acct_no; }
            set { _disc_acct_no = value; }
        }
        public int frght_acct_no
        {
            get { return _frght_acct_no; }
            set { _frght_acct_no = value; }
        }
        public int inv_acct_no
        {
            get { return _inv_acct_no; }
            set { _inv_acct_no = value; }
        }
        public int cog_acct_no
        {
            get { return _cog_acct_no; }
            set { _cog_acct_no = value; }
        }
        public int scrap_acct_no
        {
            get { return _scrap_acct_no; }
            set { _scrap_acct_no = value; }
        }
        public string use_department
        {
            get { return _use_department; }
            set { _use_department = value; }
        }
        public int oe_doc_no
        {
            get { return _oe_doc_no; }
            set { _oe_doc_no = value; }
        }
        public int oe_inv_doc_no
        {
            get { return _oe_inv_doc_no; }
            set { _oe_inv_doc_no = value; }
        }
        public int oe_post_no
        {
            get { return _oe_post_no; }
            set { _oe_post_no = value; }
        }
        public string order_type
        {
            get { return _order_type; }
            set { _order_type = value; }
        }
        public string line_type
        {
            get { return _line_type; }
            set { _line_type = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string change_terms
        {
            get { return _change_terms; }
            set { _change_terms = value; }
        }
        public string cod_ok
        {
            get { return _cod_ok; }
            set { _cod_ok = value; }
        }
        public string one_time_cust
        {
            get { return _one_time_cust; }
            set { _one_time_cust = value; }
        }
        public string ack_kit_exp
        {
            get { return _ack_kit_exp; }
            set { _ack_kit_exp = value; }
        }
        public string pic_kit_exp
        {
            get { return _pic_kit_exp; }
            set { _pic_kit_exp = value; }
        }
        public string mfs_kit_exp
        {
            get { return _mfs_kit_exp; }
            set { _mfs_kit_exp = value; }
        }
        public string inv_kit_exp
        {
            get { return _inv_kit_exp; }
            set { _inv_kit_exp = value; }
        }
        public string ack_note
        {
            get { return _ack_note; }
            set { _ack_note = value; }
        }
        public string pic_note
        {
            get { return _pic_note; }
            set { _pic_note = value; }
        }
        public string shp_note
        {
            get { return _shp_note; }
            set { _shp_note = value; }
        }
        public string inv_note
        {
            get { return _inv_note; }
            set { _inv_note = value; }
        }
        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }
        public string fob_point
        {
            get { return _fob_point; }
            set { _fob_point = value; }
        }
        public string ship_via
        {
            get { return _ship_via; }
            set { _ship_via = value; }
        }
        public string mtaxg_code
        {
            get { return _mtaxg_code; }
            set { _mtaxg_code = value; }
        }
        public string use_batch_inv
        {
            get { return _use_batch_inv; }
            set { _use_batch_inv = value; }
        }
        public string use_approv_post
        {
            get { return _use_approv_post; }
            set { _use_approv_post = value; }
        }
        public string approval_code
        {
            get { return _approval_code; }
            set { _approval_code = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspodrdftupd"; }
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
            get { return "uspodrdftget"; }
        }
        public override string TABLE_NAME
        {
            get { return "stocntrc"; }
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
        //Addes bY rahul Jain using in Print Order Acknoledgement report
        public string GET_ACK_REC
        {
            get { return "uspackexpget"; }
        }
        //Addes bY rahul Jain using in Print Packing Slip report
        public string GET_MFS_EXP
        {
            get { return "uspmfsexpget"; }
        }
        //Added by Sunil Pahwa 
        public string GET_ONE_TIME_CUST
        {
            get { return "uspimosscntrcgetal"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select rowid,disc_frght,tax_frght,taxable,st_tx_code,co_tx_code,ci_tx_code,");
            str.Append(" warehouse_code,retention_days,due_days,cm_reason,dm_reason,ar_acct_no,");
            str.Append(" cash_acct_no,visa_acct_no,sales_acct_no,disc_acct_no,frght_acct_no,inv_acct_no,");
            str.Append(" cog_acct_no,scrap_acct_no,use_department,oe_doc_no,oe_inv_doc_no,oe_post_no,");
            str.Append(" order_type,line_type,terms_code,change_terms,cod_ok,one_time_cust,ack_kit_exp,");
            str.Append(" pic_kit_exp,mfs_kit_exp,inv_kit_exp,ack_note,pic_note,shp_note,inv_note,pay_method,");
            str.Append(" fob_point,ship_via,mtaxg_code,use_batch_inv,use_approv_post,approval_code");
            str.Append(" from stocntrc Where 1=1");

            if (parameters[0] != null && Convert.ToInt32(parameters[0]) > 0)
                str.Append(" and rowid = " + parameters[0].ToString());

            return str.ToString();
        }

        #endregion Stored-Procedures
    }
}
