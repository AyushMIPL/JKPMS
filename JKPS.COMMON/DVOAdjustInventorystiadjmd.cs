using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAdjustInventorystiadjmd : DVOBase
    {
        private int _rowid;
        private  int  _doc_no ;
        private int _line_no;
        private string _item_code;
        private string _warehouse_code;
        private decimal _adj_qty;
        private decimal _adj_cost;
        private string  _adj_type;

        private string _stock_unit;
        private string _desc1;
        private string _desc2;
        private decimal _qty_on_hand;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        public DVOAdjustInventorystiadjmd()
        {
            _rowid = 0;
          _doc_no=0 ;
          _line_no=0;
          _item_code=string.Empty;
          _warehouse_code=string.Empty;
          _adj_qty=0;
          _adj_cost=0;
          _adj_type=string.Empty;

          _stock_unit=string.Empty;
          _desc1=string.Empty;
          _desc2=string.Empty;
          _qty_on_hand=0;

          _InsertMachineInfo = "App";
          _InsertDate = DateTime.Now;
          _InsertBy = -1;
          _UpdateMachineInfo = "App";
          _UpdateDate = DateTime.Now;
          _UpdateBy = -1;



        }
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
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public decimal adj_qty
        {
            get { return _adj_qty; }
            set { _adj_qty = value; }
        }
        
        public decimal adj_cost
        {
            get { return _adj_cost; }
            set { _adj_cost = value; }
        }
        public string  adj_type
        {
            get { return _adj_type; }
            set { _adj_type = value; }
        }
        public string  stock_unit
        {
            get { return _stock_unit; }
            set { _stock_unit = value; }
        }
        public string  desc1
        {
            get { return _desc1; }
            set { _desc1 = value; }
        }
        public string  desc2
        {
            get { return _desc2; }
            set { _desc2 = value; }
        }
        public decimal  qty_on_hand
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
         #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspinvadjustdtlIns"; }//uspinvadjdtlIns
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvadjdtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvadjdtldel"; }
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
            get { return "stiadjmd"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID//
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string GET_INV_ADJUST_DTL_INFO
        {
            get { return "uspinvadjustdtlget"; }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           

            return sql.ToString();
        }



        #endregion Stored-Procedures



    }
    
}
