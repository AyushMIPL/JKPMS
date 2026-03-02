using System;
using System.Collections.Generic;
using System.Text;
namespace JKPS.COMMON
{
  
       //Written By : Chandrasekhar on 24-06-2009 table used 'sticadjd' for adjusting Inventory 
        // against Physical Count
    public class DVOInventoryCountSheetDtl : DVOBase
    {
        private int _rowid;
        private int _doc_no;
    	private int _page_no;
    	private int _line_no;
    	private string _item_code;
    	private decimal _qty_on_hand;
    	private decimal _adj_qty;
    	private decimal _count_qty;
    	private DateTime _l_mod_date;
    	private string  _l_mod_time;
    	private string _l_mod_id;
    	private string _warehouse_code;
        private string _desc1;
        private string _desc2;
        private string _count_desc;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        private int _first_value;
        private int _second_value;
        private int _operator;

       


         #region Constructor
        public DVOInventoryCountSheetDtl()
        {
            _doc_no = 0;
            _page_no = 0;
            _line_no  = 0;
            _item_code=string.Empty;
            _qty_on_hand = 0.0M;
            _adj_qty=0.0M;
            _count_qty=0.0M;
            _l_mod_date=DateTime.Now;
            _l_mod_time  = string.Empty;
            _l_mod_id = string.Empty;
            _warehouse_code=string.Empty;
            _desc1 = string.Empty;
            _desc2 = string.Empty;
            _count_desc = string.Empty;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
            _first_value=0;
            _second_value=0;
            _operator = 0;

        }
        #endregion 

        #region Property

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
        
        public int page_no
        {
            get { return _page_no; }
            set { _page_no = value; }
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
        public decimal qty_on_hand
        {
            get { return _qty_on_hand; }
            set { _qty_on_hand = value; }
        }
        public decimal adj_qty
        {
            get { return _adj_qty; }
            set { _adj_qty = value; }
        }
        
        public decimal count_qty
        {
            get { return _count_qty; }
            set { _count_qty = value; }
        }
        public DateTime l_mod_date
        {
            get { return _l_mod_date; }
            set { _l_mod_date = value; }
        }
        public string l_mod_time
        {
            get { return _l_mod_time; }
            set { _l_mod_time = value; }
        }
        public string l_mod_id
        {
            get { return _l_mod_id; }
            set { _l_mod_id = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public string desc1
        {
            get { return _desc1; }
            set { _desc1 = value; }
        }
        public string desc2
        {
            get { return _desc2; }
            set { _desc2 = value; }
        }
        public string count_desc
        {
            get { return _count_desc; }
            set { _count_desc = value; }
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

        public int first_value
        {
            get { return _first_value; }
            set { _first_value = value; }
        }
        public int second_value
        {
            get { return _second_value; }
            set { _second_value = value; }
        }
        public int oprator
        {
            get { return _operator; }
            set { _operator  = value; }
        }

        #endregion


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspICPCdtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspICPCdtlupd"; }
        }

        public override string DELETE_SPNAME
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
            get { return "sticadjd"; }
        }

        public override int UNIQUE_ID
        {
            get { return _doc_no; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string GET_OVER_SHORT_REPORT_INFO1
        {
            get { return "usppriovsrtget1"; }
        }
        public string DELETE_COUNTS
        {
            get { return "uspcountsdel"; }
        }
        public string UPDATE_CADJE
        {
            get { return "uspsticadjeupd"; }
        }
        public string GET_INV_COUNTS
        {
            get { return "uspinvcountget"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select dtl.doc_no,dtl.page_no,dtl.line_no,dtl.item_code,dtl.qty_on_hand,dtl.adj_qty,dtl.count_qty,dtl.l_mod_date,");
            sql.Append("dtl.l_mod_time,dtl.l_mod_id,dtl.warehouse_code,iteminfo.desc1,iteminfo.desc2,hdr.count_desc from sticadje hdr,sticadjd dtl,stiinvtr iteminfo ");
            sql.Append(" where hdr.doc_no=dtl.doc_no and dtl.item_code=iteminfo.item_code ");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND dtl.doc_no = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND dtl.page_no = " + parameters[1].ToString());
            if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dtl.item_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToDecimal(parameters[3]) > 0.0M)
                sql.Append(" AND dtl.qty_on_hand = " + parameters[3].ToString());
            if (Convert.ToDecimal(parameters[4]) > 0.0M)
                sql.Append(" AND dtl.count_qty = " + parameters[4].ToString());
            if (Convert.ToDecimal(parameters[5]) > 0.0M)
                sql.Append(" AND dtl.adj_qty = " + parameters[5].ToString()); 
            return sql.ToString();
        }
        

        #endregion Stored-Procedures  
    
        
    }
}
