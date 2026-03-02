using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 20-06-2009 table used stipurcd for Inventory Received Details
    public class DVOInventoryReceivedstipurcd : DVOBase
    {
        private int _rowid;
        private int _doc_no;
        private int _line_no;
        private string _item_code;
        private string _warehouse_code;
        private decimal _purch_qty;
        private decimal _purch_unit_cost;
        private decimal _purch_factor;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        //Added By Rahul Jain on 07/07/09 using in update inventory received
        private decimal _extension;
        private string _purch_unit;
        private string _item_desc;

         #region Constructor
        public DVOInventoryReceivedstipurcd()
        {
            _rowid = 0;
            _doc_no = 0;
            _line_no  = 0;
            _item_code = "";
            _warehouse_code  = "";
            _purch_qty = 0.0M;
            _purch_unit_cost =0.0M;
            _purch_factor = 0.0M;
            _extension = 0.0M;
            _purch_unit = "";
            _item_desc = "";

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
        public decimal purch_factor
        {
            get { return _purch_factor; }
            set { _purch_factor = value; }
        }
        public decimal purch_qty
        {
            get { return _purch_qty; }
            set { _purch_qty = value; }
        }
        public decimal purch_unit_cost
        {
            get { return _purch_unit_cost; }
            set { _purch_unit_cost = value; }
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
        public decimal extension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        public string purch_unit
        {
            get { return _purch_unit; }
            set { _purch_unit = value; }
        }
        public string item_desc
        {
            get { return _item_desc; }
            set { _item_desc = value; }
        }

        #endregion


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspinvrecdtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvrecdtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvrecdtldel"; }
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


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT rowid,doc_no,line_no,item_code,warehouse_code,");
            sql.Append(" purch_qty,purch_unit_cost,purch_factor");
            sql.Append(" FROM  stipurcd where 1=1");
            
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no = " + parameters[0]);
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND rowid =" + parameters[1]);
            sql.Append(" order by item_code");

            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
