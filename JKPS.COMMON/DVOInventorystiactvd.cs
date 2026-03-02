using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By :Rahul Jain on 24/06/09 Using in Inventory Posting 
    //Table Used : stiactvd
    public class DVOInventorystiactvd :DVOBase
    {
        private int _rowid;
        private string _orig_journal;
        private int _doc_no;
        private int _line_no;
        private string _item_code;
        private string _warehouse_code;
        private decimal _qty;
        private decimal _cost;
        private decimal _price;
        private string _comm_code;
        private string _item_class;
        private string _in_statistics;

        #region Constructor
        public DVOInventorystiactvd()
        {
            _rowid = 0;
            _orig_journal = "";
            _doc_no = 0;
            _line_no = 0;
            _item_code = "";
            _warehouse_code = "";
            _qty = 0.0M;
            _cost = 0.0M;
            _price = 0.0M;
            _comm_code = "";
            _item_class = "";
            _in_statistics = "";
        }
        #endregion Constructor

        #region Property
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
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
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string item_code
        {
            get { return _item_code ; }
            set { _item_code = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public decimal qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public decimal price
        {
            get { return _price ; }
            set { _price = value; }
        }
        public string comm_code
        {
            get { return _comm_code ; }
            set { _comm_code = value; }
        }
        public string item_class
        {
            get { return _item_class; }
            set { _item_class = value; }
        }
        public string in_statistics
        {
            get { return _in_statistics ; }
            set { _in_statistics = value; }
        }
        #endregion Property

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
            get { return "stiactvd"; }
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
        //Added By Rahul jain on 24/06/09 Using in Post Inventory report
        public  string INSERT_STIACTVD
        {
            get { return "uspi_actv"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
