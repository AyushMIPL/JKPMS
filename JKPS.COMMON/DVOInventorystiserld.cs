using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By Rahul Jain on 24/06/09 Using in Post Inventory report
    //Table Used :stiserld
    public class DVOInventorystiserld : DVOBase
    {
        private int _rowid;
        private string _item_code;
        private string _warehouse_code;
        private int _seq_no;
        private string _lot_no;
        private string _serial_no;
        private decimal _lot_qty;
        private decimal _cost;
        private string _vend_code;

        public DVOInventorystiserld()
        {
            _rowid = 0;
            _item_code = "";
            _warehouse_code = "";
            _seq_no=0;
            _lot_no = "";
            _serial_no = "";
            _lot_qty = 0.0M;
            _cost = 0.0M;
            _vend_code = "";
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
        public int seq_no
        {
            get { return _seq_no ; }
            set { _seq_no = value; }
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
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public int RowId
        {
            get { return _rowid; }
            set { _rowid = value; }
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
            get { return "stiserld"; }
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
        //Added By Rahul Jain on 24/06/09 using in Post Inventory function
        public string INSERT_STISERLD
        {
            get { return "usppi_serld"; }
        }
        public string GETc_get_lot
        {
            get { return "uspc_get_lot"; }
        }
        public string Update_u_psh_lot
        {
            get { return "uspu_psh_lot"; }
        }
        public string GETc1_serld
        {
            get { return "uspc1_serld"; }
        }
        public string Updateu_serld
        {
            get { return "uspu_serld"; }
        }

        public string Deleted_serld
        {
            get { return "uspd_serld"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
