using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPostICGlobal
    {

        private int _status;
        private int _inv_acct_no;
        private int _cog_acct_no;
        private int _sales_acct_no;
        private string _description;
        private int _last_doc_no;
        private decimal _trx_amount;
        private decimal _adj_amount;
        private string _terms_disc;
        private int _next_doc_no;
        private int _adj_acct_no;
        private int _count_acct_no;
        private string _cost_method;
        public DVOPostICGlobal()
        {
            _status = 0;
            _description = string.Empty;
            _inv_acct_no = 0;
            _cog_acct_no = 0;
            _sales_acct_no = 0;
            _last_doc_no = 0;
            _trx_amount = 0.0M;
            _adj_amount = 0.0M;
            _terms_disc = "";
            _next_doc_no = 0;
            _adj_acct_no = 0;
            _count_acct_no = 0;
            _cost_method = "";

        }
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        public int adj_acct_no
        {
            get { return _adj_acct_no; }
            set { _adj_acct_no = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
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
        public int sales_acct_no
        {
            get { return _sales_acct_no; }
            set { _sales_acct_no = value; }
        }
        public int last_doc_no
        {
            get { return _last_doc_no; }
            set { _last_doc_no = value; }
        }
        public decimal trx_amount
        {
            get { return _trx_amount; }
            set { _trx_amount = value; }
        }
        public decimal adj_amount
        {
            get { return _adj_amount; }
            set { _adj_amount = value; }
        }
        public string terms_disc
        {
            get { return _terms_disc; }
            set { _terms_disc = value; }
        }
        public int next_doc_no
        {
            get { return _next_doc_no; }
            set { _next_doc_no = value; }
        }
        public int count_acct_no
        {
            get { return _count_acct_no; }
            set { _count_acct_no = value; }
        }
        public string cost_method
        {
            get { return _cost_method; }
            set { _cost_method = value; }
        }
    }
}
