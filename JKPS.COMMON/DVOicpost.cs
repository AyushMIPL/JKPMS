using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By Rahul jain on 22/06/09 for Inventory posting 
    public class DVOicpost : DVOBase
    {

        private string _post_or_check;
        private string _orig_journal;
        private int _doc_no;
        private int _old_doc;
        private int _post_no;
        private DateTime _post_date;
        private DateTime _doc_date;
        private string _ref_code;
        private string _doc_desc;
        private string _doc_type;
        private string _ref_no;
        private int _line_no;
        private int _ship_no;
        private string _item_code;
        private string _warehouse_code;
        private string _to_warehouse;
        private decimal _qty;
        private decimal _cost;
        private decimal _price;
        private string _in_statistics;

        public DVOicpost()
        {
            _post_or_check = "";
            _orig_journal = "";
            _doc_no = 0;
            _old_doc = 0;
            _post_no = 0;
            _post_date = Convert.ToDateTime("01/01/1900");
            _doc_date = Convert.ToDateTime("01/01/1900");
            _ref_code = "";
            _doc_desc = "";
            _doc_type = "";
            _ref_no = "";
            _line_no = 0;
            _ship_no = 0;
            _item_code = "";
            _warehouse_code = "";
            _to_warehouse = "";
            _qty = 0;
            _cost = 0;
            _price = 0;
            _in_statistics = "";
        }

        public string post_or_check
        {
            get { return _post_or_check; }
            set { _post_or_check = value; }
        }
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int old_doc
        {
            get { return _old_doc; }
            set { _old_doc = value; }
        }
        public int post_no
        {
            get { return _post_no; }
            set { _post_no = value; }
        }
        public DateTime post_date
        {
            get { return _post_date; }
            set { _post_date = value; }
        }
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string ref_code
        {
            get { return _ref_code; }
            set { _ref_code = value; }
        }
        public string doc_desc
        {
            get { return _doc_desc; }
            set { _doc_desc = value; }
        }
        public string doc_type
        {
            get { return _doc_type; }
            set { _doc_type = value; }
        }
        public string ref_no
        {
            get { return _ref_no; }
            set { _ref_no = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public int ship_no
        {
            get { return _ship_no; }
            set { _ship_no = value; }
        }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public string to_warehouse
        {
            get { return _to_warehouse; }
            set { _to_warehouse = value; }
        }
        public decimal qty
        {
            get { return _qty ; }
            set { _qty = value; }
        }
        public decimal cost
        {
            get { return _cost ; }
            set { _cost = value; }
        }
        public decimal price
        {
            get { return _price ; }
            set { _price = value; }
        }
        public string in_statistics
        {
            get { return _in_statistics ; }
            set { _in_statistics = value; }
        }

        #region Stored-Procedures

        //public string AUTHENTICATION_SPNAME
        //{
        //    get { return "uspsecauthenticate"; }
        //}

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
        public string POSTGL
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
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
