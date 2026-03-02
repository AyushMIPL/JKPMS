using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa
    public class DVOUpdateInventoryTransferstitrand : DVOBase
    {
        #region Variables
        private int _rowid;
        private int     _doc_no ;
        private int _line_no;
        private string _item_code;
        private string _from_wh;
        private string _to_wh;
        private decimal _tran_qty;
        private string _is_recurr;

        private string _stock_unit;
        private string _warehouse_code;

        #endregion variables

        #region Constructor
        public DVOUpdateInventoryTransferstitrand()
        {
             _doc_no=0 ;
            _line_no = 0;
            _item_code = string.Empty;
            _from_wh = string.Empty;
            _to_wh = string.Empty;
            _tran_qty = 0;
            _is_recurr = string.Empty;
            _stock_unit = string.Empty;
            _warehouse_code = string.Empty;
        }
#endregion Constructor
        
    #region [Public Properties]
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
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
            get { return _item_code; }
            set { _item_code = value; }
        }
        public string from_wh
        {
            get { return _from_wh; }
            set { _from_wh = value; }
        }
        public string to_wh
        {
            get { return _to_wh; }
            set { _to_wh = value; }
        }
        public decimal tran_qty
        {
            get { return _tran_qty; }
            set { _tran_qty = value; }
        }
        public string is_recurr
        {
            get { return _is_recurr; }
            set { _is_recurr = value; }
        }
        public string stock_unit
        {
            get { return _stock_unit; }
            set { _stock_unit = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }

        #endregion Properties
   
    #region [Stored-Procedures]

        public override string INSERT_SPNAME
        {
            get { return "uspinvtransdtlIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvtransdtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvtransdtlldel"; }
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
            get { return "stitrand"; }
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


        public string GET_INV_TRANSFER_DTL_INFO
        {
            get { return "uspinvtransdtlget"; }
        }
        public string UPD_INV_TRANSFER_DTL_INFO
        {
            get { return "uspinvtrsansdtlins"; }
        }
        public string DELETE_INV_INFO
        {
            get { return "uspinvtransinfodel"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           

            return sql.ToString();
        }





        #endregion Stored-Procedures



    }

}
