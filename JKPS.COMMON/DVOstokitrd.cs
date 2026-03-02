using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{ 
   
    /// <summary>
    /// Update Kit Definition
    /// Detail :Table-stokitrd
    /// Created by : Shrishanshu on 210709
    /// </summary>

   public  class DVOstokitrd:DVOBase
    {
       /* Table Coulmns
       * kit_code             char(15)
       * line_no              smallint
       * item_code            char(20)
       * ordr_qty             decimal(12)
       * include_price        char(1)
       */
        private string _kit_code;
        private int _line_no;
        private string _item_code;
        private decimal _ordr_qty;
        private string _include_price;
       private int _rowid;
        public DVOstokitrd()
        {
            _rowid = 0;
            _kit_code = string.Empty;
            _line_no = 0;
            _item_code = string.Empty;
            _ordr_qty = 0.0M;
            _include_price = string.Empty;
 
        }

        public  string kit_code
        {
            get{return _kit_code;}
            set{_kit_code=value;}
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }

       public int rowid
       {
           get { return _rowid; }
           set { _rowid  = value; }
       }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public decimal ordr_qty
        {
            get { return _ordr_qty; }
            set { _ordr_qty = value; }
        }
        public string include_price
        {
            get { return _include_price; }
            set { _include_price = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspstokitrdins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstokitrdupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstokitrddel"; }
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
            get { return "stokitrd"; }
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
            sql.Append("Select rowid,kit_code, line_no, item_code,");
             sql.Append(" ordr_qty, include_price from stokitrd");
            sql.Append(" where 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(kit_code) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
