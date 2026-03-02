using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public  class DVOUpdateInventoryShippedstiselld:DVOBase
    {
       private int _doc_no;
       private int _line_no;
       private string _item_code;
       private string _warehouse_code;
       private decimal _sell_qty;
       private decimal _price; 
       private decimal _sell_factor;
       private string _is_recurr;
       private string _sell_unit;

       private int _rowid;
       private decimal _extension;
       private decimal _qty_on_hand;



        public DVOUpdateInventoryShippedstiselld()
        {
             _doc_no=0;
             _line_no=0;
             _item_code=string.Empty;
             _warehouse_code=string.Empty;
             _sell_qty=0;
             _price=0;
             _sell_factor=0;
             _is_recurr=string.Empty;

             _rowid = 0;
             _extension = 0.0M;
             _qty_on_hand = 0.0M;
             _sell_unit = string.Empty;

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
        public decimal sell_qty
        {
            get { return _sell_qty; }
            set { _sell_qty = value; }
        }
        public decimal price
        {
            get { return _price; }
            set { _price = value; }
        }
        public decimal sell_factor
        {
            get { return _sell_factor; }
            set { _sell_factor = value; }
        }
        public string is_recurr
        {
            get { return _is_recurr; }
            set { _is_recurr = value; }
        }
        public decimal extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        public decimal qty_on_hand
        {
            get { return _qty_on_hand; }
            set { _qty_on_hand = value; }
        }

      public string sell_unit
      {
          get { return _sell_unit; }
          set { _sell_unit = value; }
      }
       

          #region Stored-Procedures

        
        public override string INSERT_SPNAME
        {
            get { return "uspinvshipdtlins"; }
        }

        public override string UPDATE_SPNAME
        {   
            get { return "uspinvStipeddtlUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvShipeddtldel"; }
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
            get { return "stiselld"; }
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

        public string INSERT_INV_SHIPPED_DTL_INFO
        {
            get { return ""; }//uspinvshippeddtlins
        }

        public string GET_INV_SHIPPED_DTL_INFO
        {
            get { return "uspinvshippdtlget"; }
        }
        public string UPD_INV_SHIPPED_DTL_INFO
        {
            get { return "uspinvStipeddtlUpd"; }
        }
        public string DELETE_GROUP_DETAIL
        {
            get { return "uspinvShipeddtldel"; }
        }


      public string GET_INFO
      {
          get { return "uspstilocardtlget"; }
      }
      
       


       
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            return sql.ToString();
        }

        #endregion Stored-Procedures

    
    }
}
