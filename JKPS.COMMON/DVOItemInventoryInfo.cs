using System;
using System.Collections.Generic;
using System.Text;
//Written By : Chandrasekhar on 29-06-2009 table used 'sticadje' for adjusting Inventory 
// against Physical Count
namespace JKPS.COMMON
{
    public class DVOItemInventoryInfo : DVOBase
    {
        private int _rowid;
        private string _item_code;
        private string _warehouse_code;
        private int _line_no;
        private string _count_cycle;
        private DateTime _purchase_date;
        private DateTime _count_date;
        private DateTime _sold_date;
        private string _obsolete;
        private DateTime _inactive_date;
        private DateTime _lst_act_date;
        private string _loc_aisle;
        private string _loc_row;
        private string _loc_bin;
        private string _stock_location;
        private decimal _avg_unit_cost;
        private decimal _purch_unit_cost;
        private decimal _last_cost;
        private string _comm_code;
        private decimal _price;
        private string _allow_bo;
        private string _taxable;
        private string _terms_disc;
        private string _trade_disc;
        private string _vend_code;
        private string _vend_prod_no;
        private string _abc_code;
        private decimal _reorder_point;
        private decimal _qty_reorder;
        private decimal _safety_stock;
        private decimal _safety_factor;
        private decimal _qty_on_hand;
        private decimal _last_qty;
        private DateTime _stk_out_date;
        private string _seasonal;
        private decimal _avg_ld_tm;
        private int _lst_ld_tm;
        private int _pri_ld_tm;
        private string _freez_flag;
        private DateTime _freez_date;
        private DateTime _freez_expir;
        private decimal _min_sell_qty;
        private decimal _usage_rate;


        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;





        #region Constructor
        public DVOItemInventoryInfo()
        {
            _rowid = 0;
            _item_code = string.Empty;
            _warehouse_code = string.Empty;
            _line_no = 0;
            _count_cycle = string.Empty;
            _purchase_date = DateTime.Now;
            _count_date = DateTime.Now;
            _sold_date = DateTime.Now;
            _obsolete = string.Empty;
            _inactive_date = DateTime.Now;
            _lst_act_date = DateTime.Now;
            _loc_aisle = string.Empty;
            _loc_row = string.Empty;
            _loc_bin = string.Empty;
            _stock_location = string.Empty;
            _avg_unit_cost = 0.0M;
            _purch_unit_cost = 0.0M;
            _last_cost = 0.0M;
            _comm_code = string.Empty;
            _price = 0.0M;
            _allow_bo = string.Empty;
            _taxable = string.Empty;
            _terms_disc = string.Empty;
            _trade_disc = string.Empty;
            _vend_code = string.Empty;
            _vend_prod_no = string.Empty;
            _abc_code = string.Empty;
            _reorder_point = 0.0M;
            _qty_reorder = 0.0M;
            _safety_stock = 0.0M;
            _safety_factor = 0.0M;
            _qty_on_hand = 0.0M;
            _last_qty = 0.0M;
            _stk_out_date = DateTime.Now;
            _seasonal = string.Empty;
            _avg_ld_tm = 0.0M;
            _lst_ld_tm = 0;
            _pri_ld_tm = 0;
            _freez_flag = string.Empty;
            _freez_date = DateTime.Now;
            _freez_expir = DateTime.Now;
            _min_sell_qty = 0.0M;
            _usage_rate = 0.0M;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }
        #endregion

        #region Property

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public decimal qty_on_hand
        {
            get { return _qty_on_hand; }
            set { _qty_on_hand = value; }
        }

        //Properties used for only SQL Server

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }


        #endregion


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

        public override string FIND_SPNAME
        {
            get { return "uspICWHStockget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspICWHStockget"; }
        }

        public override string TABLE_NAME
        {
            get { return "stipurcd"; }
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
        public string GET_OVER_SHORT_REPORT_INFO1
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
  