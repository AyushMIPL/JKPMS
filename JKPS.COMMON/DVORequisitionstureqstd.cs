using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVORequisitionstureqstd :DVOBase
    {
        private int _rowid;
        private int _doc_no;
        private int _line_no;
        private string _line_type;
        private string _line_stage;
        private string _item_code;
        private int _item_cat_id;
        private string _description ;
        private string _unit; 
        private decimal _ordr_quantity ;
        private decimal _aprvd_quantity;
        private decimal _cost ;
        private decimal _aprvd_cost;
        private string _whse_shipto;
        private string _whse_billto;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        public DVORequisitionstureqstd()
        {
            _rowid=0;
             _doc_no=0;
            _line_no=0;
            _line_type=string.Empty;
            _line_stage=string.Empty;
            _item_code=string.Empty;
            _item_cat_id=0;
            _description=string.Empty;
            _unit=string.Empty;
            _ordr_quantity=0.0M;
            _aprvd_quantity=0.0M;
            _cost=0.0M;
            _aprvd_cost=0.0M;
            _whse_shipto=string.Empty;
            _whse_billto=string.Empty;
            _insertby=0;
            _insertdate=string.Empty;
            _insertmachineinfo=string.Empty;
            _updateby=0;
            _updatedate=string.Empty;
            _updatemachineinfo=string.Empty;

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
        public string line_type
        {
            get { return _line_type; }
            set { _line_type = value; }
        }
        public string line_stage
        {
            get { return _line_stage; }
            set { _line_stage = value; }
        }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public int item_cat_id
        {
            get { return _item_cat_id; }
            set { _item_cat_id = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        public decimal ordr_quantity
        {
            get { return _ordr_quantity; }
            set { _ordr_quantity = value; }
        }
        public decimal aprvd_quantity
        {
            get { return _aprvd_quantity; }
            set { _aprvd_quantity = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public decimal aprvd_cost
        {
            get { return _aprvd_cost; }
            set { _aprvd_cost = value; }
        }
        public string whse_shipto
        {
            get { return _whse_shipto; }
            set { _whse_shipto = value; }
        }
        public string whse_billto
        {
            get { return _whse_billto; }
            set { _whse_billto = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usprequidins"; }
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stureqstd"; }
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
        //Added By Rahul
        public string UPDATE_REQ_DTL_APPROVAL
        {
            get { return "uspreqdtlaprvl_upd"; }
        }
        public string UPDATE_REQ_DTL_CANCEL
        {
            get { return "uspreqdtlcan_upd"; }
        }
       

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append(" select sturqsor.rowid,sturqsor.requestor_code ,sturqsor.request_desc,");
            //sql.Append(" sturqsor.whse_shipto,sturqsor.approval_level,");
            //sql.Append(" stiwhser.description ");
            //sql.Append(" from sturqsor,outer stiwhser");
            //sql.Append(" where sturqsor.whse_shipto =stiwhser.whse_code ");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND requestor_code= '" + parameters[0].ToString().Trim() + "'");


            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(request_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            //if (Convert.ToInt32(parameters[2]) > 0)
            //    sql.Append(" AND approval_level = " + parameters[2].ToString());

            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(whse_shipto) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            //if (Convert.ToInt32(parameters[4]) > 0)
            //    sql.Append(" AND sturqsor.rowid = " + parameters[4].ToString());

            return sql.ToString();
        }

        public string FIND_DATA_FOR_APPROVAL(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stureqstd.rowid,stureqstd.doc_no,stureqstd.line_no,stureqstd.line_type, ");
            sql.Append("stureqstd.line_stage,stureqstd.item_code,stureqstd.item_cat_id,stureqstd.description, ");
            sql.Append("stureqstd.unit,stureqstd.ordr_quantity,stureqstd.cost, ");
            sql.Append("stureqstd.aprvd_quantity,stureqstd.aprvd_cost,stureqstd.whse_shipto, ");
            sql.Append("stureqstd.whse_billto  ");
            sql.Append("FROM stureqstd WHERE stureqstd.line_stage='REQ' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append(" AND stureqstd.doc_no =" + Convert.ToInt32(parameters[0]));
            sql.Append(" Order by stureqstd.line_no ");
            return sql.ToString();
        }




        #endregion Stored-Procedures

    }
}
