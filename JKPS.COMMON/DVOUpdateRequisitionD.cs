using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdateRequisitionD : DVOBase
    {
        #region Private Variables

        private int _rowid;
        private int _doc_no;
        private int _line_no;
        private string _line_type;
        private string _line_stage;
        private string _item_code;
        private string _desc1;
        private string _desc2;
        private string _unit;
        private decimal _ordr_qty;
        private string _instruct_code;
        private string _reference_no;
        private string _whse_shipto;
        private string _whse_billto;
        private string _vend_code;
        private string _requestor_code;
        private string _request_no;
        private DateTime _request_date;
        private string _authorization_code;
        private int _acct_no;
        private int _req_post_no;
        private int _po_doc_no;
        private int _po_line_no;
        private decimal _recv_qty;
        private string _ref_type;
        private int _ref_doc_no;
        private int _ref_line_no;
        private int _ref_ship_no;
        private decimal _cost;
        private decimal _net_amount;
        private int _gl_acct_no;
        private decimal _lvl1aprv_qty;
        private decimal _lvl1aprv_cost;
        private string _lvl1aprv_vndr;

        private decimal _procaprv_qty;
        private decimal _procaprv_cost;
        private string _procaprv_vndr;
        private string _procnotes;

        #endregion Private Variables

        #region Default Constructor
        public DVOUpdateRequisitionD()
        {
            _rowid = 0;
            _acct_no = 0;
            _authorization_code = string.Empty;
            _cost = 0;
            _desc1 = string.Empty;
            _desc2 = string.Empty;
            _doc_no = 0;
            _gl_acct_no = 0;
            _instruct_code = string.Empty;
            _item_code = string.Empty;
            _line_no = 0;
            _line_stage = string.Empty;
            _line_type = string.Empty;
            _net_amount = 0;
            _ordr_qty = 0;
            _po_doc_no = 0;
            _po_line_no = 0;
            _recv_qty = 0;
            _ref_doc_no = 0;
            _ref_line_no = 0;
            _ref_ship_no = 0;
            _ref_type = string.Empty;
            _reference_no = string.Empty;
            _req_post_no = 0;
            _request_date = Convert.ToDateTime(null);
            _request_no = string.Empty;
            _requestor_code = string.Empty;
            _rowid = 0;
            _unit = string.Empty;
            _vend_code = string.Empty;
            _whse_billto = string.Empty;
            _whse_shipto = string.Empty;
            _lvl1aprv_qty = 0;
            _lvl1aprv_cost = 0;
            _lvl1aprv_vndr = string.Empty;

            _procaprv_qty = 0;
            _procaprv_cost = 0;
            _procaprv_vndr = string.Empty;
            _procnotes = string.Empty;
        }
        #endregion Default Constructor

        #region Public Properties
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
        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        public decimal ordr_qty
        {
            get { return _ordr_qty; }
            set { _ordr_qty = value; }
        }
        public string instruct_code
        {
            get { return _instruct_code; }
            set { _instruct_code = value; }
        }
        public string reference_no
        {
            get { return _reference_no; }
            set { _reference_no = value; }
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
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string requestor_code
        {
            get { return _requestor_code; }
            set { _requestor_code = value; }
        }
        public string request_no
        {
            get { return _request_no; }
            set { _request_no = value; }
        }
        public DateTime request_date
        {
            get { return _request_date; }
            set { _request_date = value; }
        }
        public string authorization_code
        {
            get { return _authorization_code; }
            set { _authorization_code = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public int req_post_no
        {
            get { return _req_post_no; }
            set { _req_post_no = value; }
        }
        public int po_doc_no
        {
            get { return _po_doc_no; }
            set { _po_doc_no = value; }
        }
        public int po_line_no
        {
            get { return _po_line_no; }
            set { _po_line_no = value; }
        }
        public decimal recv_qty
        {
            get { return _recv_qty; }
            set { _recv_qty = value; }
        }
        public string ref_type
        {
            get { return _ref_type; }
            set { _ref_type = value; }
        }
        public int ref_doc_no
        {
            get { return _ref_doc_no; }
            set { _ref_doc_no = value; }
        }
        public int ref_line_no
        {
            get { return _ref_line_no; }
            set { _ref_line_no = value; }
        }
        public int ref_ship_no
        {
            get { return _ref_ship_no; }
            set { _ref_ship_no = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public decimal net_amount
        {
            get { return _net_amount; }
            set { _net_amount = value; }
        }
        public int gl_acct_no
        {
            get { return _gl_acct_no; }
            set { _gl_acct_no = value; }
        }
        public decimal lvl1aprv_cost
        {
            get { return _lvl1aprv_cost; }
            set { _lvl1aprv_cost = value; }
        }
        public decimal lvl1aprv_qty
        {
            get { return _lvl1aprv_qty; }
            set { _lvl1aprv_qty = value; }
        }
        public string lvl1aprv_vndr
        {
            get { return _lvl1aprv_vndr; }
            set { _lvl1aprv_vndr = value; }
        }
        public decimal procaprv_cost
        {
            get { return _procaprv_cost; }
            set { _procaprv_cost = value; }
        }
        public decimal procaprv_qty
        {
            get { return _procaprv_qty; }
            set { _procaprv_qty = value; }
        }
        public string procaprv_vndr
        {
            get { return _procaprv_vndr; }
            set { _procaprv_vndr = value; }
        }
        public string procnotes
        {
            get { return _procnotes; }
            set { _procnotes = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usp_sturqstd_ins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_sturqstd_upd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_sturqstd_del"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usp_sturqstd_get"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspReqDefGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "sturqstd"; }
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

        public string Get_Last_Doc_No
        {
            get { return "uspgetldocno"; }
        }

        public string GetUniqueReqNo
        {
            get { return "usp_chkreqno"; }
        }
        public  string UPDATE_REQ_DTL_APPROVAL
        {
            get { return "uspreqdtlapprvlupd"; }
        }
        public  string UPDATE_REQ_DTL_CANCEL
        {
            get { return "uspreqdtlcancelupd"; }
        }
        //Used in CPU Approval
        public string UPDATE_REQ_DTL_CPU_APPROVAL
        {
            get { return "uspreqdtlapprvlcpu"; }
        }
        public string UPDATE_REQ_DTL_CPU_CANCEL
        {
            get { return "uspreqdtlcancelcpu"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            //    sql.Append("select sturqstd.rowid v_rowid,sturqstd.doc_no v_doc_no,sturqstd.line_no v_line_no,");
            //    sql.Append("sturqstd.line_type v_line_type,sturqstd.line_stage v_line_stage,sturqstd.item_code v_item_code,sturqstd.desc1 "); 
            //    sql.Append("v_desc1,sturqstd.desc2 v_desc2,sturqstd.unit v_unit,sturqstd.ordr_qty v_ordr_qty,sturqstd.reference_no ");
            //    sql.Append(" v_reference_no, sturqstd.whse_shipto v_whse_shipto,sturqstd.requestor_code v_requestor_code,");
            //    sql.Append("sturqstd.request_no v_request_no,sturqstd.request_date v_request_date,sturqstd.authorization_code v_autho_code ");
            //    sql.Append(" from sturqstd where doc_n0=p_doc_n0  ");

            //    if (parameters[0] != null)
            //        if (Convert.ToInt32(parameters[0]) != 0)
            //            sql.Append(" AND sturqstd.doc_no =" + Convert.ToInt32(parameters[0]));

            //    if (parameters[1] != null)
            //        if (parameters[1].ToString() != string.Empty)
            //            sql.Append(" AND Rtrim(sturqstd.whse_shipto) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            //    if (parameters[2] != null)
            //        if (parameters[2].ToString() != string.Empty)
            //            sql.Append(" AND Rtrim(sturqstd.requestor_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            //    if (parameters[3] != null)
            //        if (parameters[3].ToString() != string.Empty)
            //            sql.Append("AND Rtrim(sturqste.request_no) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'"); 

            //    if (parameters[4] != null)
            //        if (Convert.ToDateTime(parameters[4]) != Convert.ToDateTime(null))
            //            sql.Append(" AND sturqstd.request_date = '" + Convert.ToDateTime(parameters[4]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            //    if (parameters[5] != null)
            //        if (parameters[5].ToString() != string.Empty)
            //            sql.Append(" AND Rtrim(sturqstd.authorization_code) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            return sql.ToString();
        }
        public string FIND_DATA_FOR_APPROVAL(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sturqstd.rowid,sturqstd.doc_no,sturqstd.line_no,sturqstd.line_type, ");
            sql.Append("sturqstd.line_stage,sturqstd.item_code,sturqstd.desc1,sturqstd.desc2, ");
            sql.Append("sturqstd.unit,sturqstd.ordr_qty,sturqstd.reference_no,sturqstd.whse_shipto, ");
            sql.Append("sturqstd.requestor_code,sturqstd.request_no,sturqstd.request_date, ");
            sql.Append("sturqstd.authorization_code,sturqstd.vend_code,sturqstd.cost, ");
            sql.Append("lvl1aprv_qty,lvl1aprv_cost,lvl1aprv_vndr ");
            sql.Append("FROM sturqstd WHERE sturqstd.line_stage='REQ' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append(" AND sturqstd.doc_no =" + Convert.ToInt32(parameters[0]));
            sql.Append(" Order by sturqstd.line_no ");
            return sql.ToString();
        }
        public string FIND_DETAIL_FOR_ENQUIRY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sturqstd.rowid,sturqstd.doc_no,sturqstd.line_no,sturqstd.line_type, ");
            sql.Append(" sturqstd.line_stage,sturqstd.item_code,sturqstd.desc1,sturqstd.desc2, ");
            sql.Append(" sturqstd.unit,sturqstd.ordr_qty,sturqstd.reference_no,sturqstd.whse_shipto, ");
            sql.Append(" sturqstd.requestor_code,sturqstd.request_no,sturqstd.request_date, ");
            sql.Append(" sturqstd.authorization_code,sturqstd.vend_code,sturqstd.cost, ");
            sql.Append(" lvl1aprv_qty,lvl1aprv_cost,lvl1aprv_vndr, ");
            sql.Append(" procaprv_qty,procaprv_cost,procaprv_vndr ");
            sql.Append(" FROM sturqstd WHERE 1=1 ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append(" AND sturqstd.doc_no =" + Convert.ToInt32(parameters[0]));
            sql.Append(" Order by sturqstd.line_no ");
            return sql.ToString();
        }
        #endregion Stored-Procedures

    }
}
