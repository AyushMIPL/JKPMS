using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 24/06/09 Using Inventory Posting
    //Table Used : stiserle
    public class DVOInventorystiserle: DVOBase 
    {
        private string _orig_journal;
        private int _doc_no;
        private int _line_no;
        private int _ship_no;
        private string _lot_no;
        private string _serial_no;
        private decimal _lot_qty;
        private string _item_code;
        private string _warehouse_code;
        private string _in_out;
        private int _rowid;
        private decimal _cost;
        public DVOInventorystiserle()
        {
            _rowid = 0;
            _orig_journal = "";
            _doc_no = 0;
            _line_no = 0;
            _ship_no = 0;
            _lot_no = "";
            _serial_no = "";
            _lot_qty = 0.0M;
            _item_code = "";
            _warehouse_code = "";
            _in_out = "";
            _cost = 0;
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
        public int ship_no
        {
            get { return _ship_no; }
            set { _ship_no = value; }
        }
        public string lot_no
        {
            get { return _lot_no; }
            set { _lot_no = value; }
        }
        public string serial_no
        {
            get { return _serial_no; }
            set { _serial_no = value; }
        }
        public decimal lot_qty
        {
            get { return _lot_qty; }
            set { _lot_qty = value; }
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
        public string in_out
        {
            get { return _in_out; }
            set { _in_out = value; }
        }
        public int RowId
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
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
            get { return "stiserle"; }
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

        public string GETc_serle
        {
            get { return "uspc_serle"; }
        }
        public string GETcs_serlde
        {
            get { return "uspcs_serlde"; }
        }
        public string GETcb_serlde
        {
            get { return "uspcb_serlde"; }
        }
        public string Updateuspu_pop_lot
        {
            get { return "uspu_pop_lot"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
