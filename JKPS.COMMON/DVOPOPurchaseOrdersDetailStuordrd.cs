using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPOPurchaseOrdersDetailStuordrd : DVOBase
    {
        int _rowid;
        int _doc_no;
        int _line_no;
        string _cm_dm_reason;
        string _mtaxg_code;
        string _line_type;
        string _line_stage;
        int _receiver_printed;
        string _request_date;//date,
        string _po_date;//date,
        string _rcpt_date;//date,
        string _inv_date;//date,
        string _required_date;//date,
        string _whse_shipto;
        string _whse_billto;
        string _item_code;
        string _desc1;
        string _desc2;
        string _td_disc_allowed;
        string _bo_allowed;
        decimal _ordr_qty;
        decimal _rlse_qty;
        decimal _rjct_qty;
        decimal _recv_qty;
        decimal _cost_qty;
        decimal _acpt_qty;
        decimal _exp_rec_qty;
        decimal _exp_inv_qty;
        string _sell_unit;
        string _purch_unit;
        string _stock_unit;
        decimal _unit_factor;
        decimal _cost;
        int _gl_acct_no;
        decimal _net_price;
        string _department;
        string _instruct_code;
        string _authorization_code;
        string _inspection_code;
        string _alias_code;
        decimal _weight;
        string _staging_area;
        int _order_doc_no;
        int _order_line_no;
        int _order_ship_no;
        string _note_flag;
        decimal _unit_tax;

        int _insertby;
        string _insertdate;
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;
        string _updatemachineinfo;

        //sturecte
        string _po_no;
        string _vendorCode;
        string _receipt_date;

        #region Constructor

        public DVOPOPurchaseOrdersDetailStuordrd()
        {
            _rowid = 0;
            _doc_no = 0;
            _line_no = 0;
            _cm_dm_reason = string.Empty;
            _mtaxg_code = string.Empty;
            _line_type = string.Empty;
            _line_stage = string.Empty;
            _receiver_printed = 0;
            _request_date = "01/01/1900";//date,
            _po_date = "01/01/1900";//date,
            _rcpt_date = "01/01/1900";//date,
            _inv_date = "01/01/1900";//date,
            _required_date = "01/01/1900";//date,
            _whse_shipto = string.Empty;
            _whse_billto = string.Empty;
            _item_code = string.Empty;
            _desc1 = string.Empty;
            _desc2 = string.Empty;
            _td_disc_allowed = string.Empty;
            _bo_allowed = string.Empty;
            _ordr_qty = 0;
            _rlse_qty = 0;
            _rjct_qty = 0;
            _recv_qty = 0;
            _cost_qty = 0;
            _acpt_qty = 0;
            _exp_rec_qty = 0;
            _exp_inv_qty = 0;
            _sell_unit = string.Empty;
            _purch_unit = string.Empty;
            _stock_unit = string.Empty;
            _unit_factor = 0;
            _cost = 0;
            _gl_acct_no = 0;
            _net_price = 0;
            _department = string.Empty;
            _instruct_code = string.Empty;
            _authorization_code = string.Empty;
            _inspection_code = string.Empty;
            _alias_code = string.Empty;
            _weight = 0;
            _staging_area = string.Empty;
            _order_doc_no = 0;
            _order_line_no = 0;
            _order_ship_no = 0;
            _note_flag = string.Empty;
            _unit_tax = 0;

            _insertby = 0;
            _insertdate = "01/01/1900";// date
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";// date
            _updatemachineinfo = string.Empty;


            _po_no = string.Empty;
            _vendorCode = string.Empty;
            _receipt_date = "01/01/1900";// date

        }

        #endregion Constructor

        #region public properties

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
        public string cm_dm_reason
        {
            get { return _cm_dm_reason; }
            set { _cm_dm_reason = value; }
        }
        public string mtaxg_code
        {
            get { return _mtaxg_code; }
            set { _mtaxg_code = value; }
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
        public int receiver_printed
        {
            get { return _receiver_printed; }
            set { _receiver_printed = value; }
        }
        public string request_date
        {
            get { return _request_date; }
            set { _request_date = value; }
        }
        public string po_date
        {
            get { return _po_date; }
            set { _po_date = value; }
        }
        public string rcpt_date
        {
            get { return _rcpt_date; }
            set { _rcpt_date = value; }
        }
        public string inv_date
        {
            get { return _inv_date; }
            set { _inv_date = value; }
        }
        public string required_date
        {
            get { return _required_date; }
            set { _required_date = value; }
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
        public string td_disc_allowed
        {
            get { return _td_disc_allowed; }
            set { _td_disc_allowed = value; }
        }
        public string bo_allowed
        {
            get { return _bo_allowed; }
            set { _bo_allowed = value; }
        }
        public decimal ordr_qty
        {
            get { return _ordr_qty; }
            set { _ordr_qty = value; }
        }
        public decimal rlse_qty
        {
            get { return _rlse_qty; }
            set { _rlse_qty = value; }
        }
        public decimal rjct_qty
        {
            get { return _rjct_qty; }
            set { _rjct_qty = value; }
        }
        public decimal recv_qty
        {
            get { return _recv_qty; }
            set { _recv_qty = value; }
        }
        public decimal cost_qty
        {
            get { return _cost_qty; }
            set { _cost_qty = value; }
        }
        public decimal acpt_qty
        {
            get { return _acpt_qty; }
            set { _acpt_qty = value; }
        }
        public decimal exp_rec_qty
        {
            get { return _exp_rec_qty; }
            set { _exp_rec_qty = value; }
        }
        public decimal exp_inv_qty
        {
            get { return _exp_inv_qty; }
            set { _exp_inv_qty = value; }
        }
        public string sell_unit
        {
            get { return _sell_unit; }
            set { _sell_unit = value; }
        }
        public string purch_unit
        {
            get { return _purch_unit; }
            set { _purch_unit = value; }
        }
        public string stock_unit
        {
            get { return _stock_unit; }
            set { _stock_unit = value; }
        }
        public decimal unit_factor
        {
            get { return _unit_factor; }
            set { _unit_factor = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public int gl_acct_no
        {
            get { return _gl_acct_no; }
            set { _gl_acct_no = value; }
        }
        public decimal net_price
        {
            get { return _net_price; }
            set { _net_price = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string instruct_code
        {
            get { return _instruct_code; }
            set { _instruct_code = value; }
        }
        public string authorization_code
        {
            get { return _authorization_code; }
            set { _authorization_code = value; }
        }
        public string inspection_code
        {
            get { return _inspection_code; }
            set { _inspection_code = value; }
        }
        public string alias_code
        {
            get { return _alias_code; }
            set { _alias_code = value; }
        }
        public decimal weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public string staging_area
        {
            get { return _staging_area; }
            set { _staging_area = value; }
        }
        public int order_doc_no
        {
            get { return _order_doc_no; }
            set { _order_doc_no = value; }
        }
        public int order_line_no
        {
            get { return _order_line_no; }
            set { _order_line_no = value; }
        }
        public int order_ship_no
        {
            get { return _order_ship_no; }
            set { _order_ship_no = value; }
        }
        public string note_flag
        {
            get { return _note_flag; }
            set { _note_flag = value; }
        }
        public decimal unit_tax
        {
            get { return _unit_tax; }
            set { _unit_tax = value; }
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
        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }

        public string receipt_date
        {
            get { return _receipt_date; }
            set { _receipt_date = value; }
        }


        #endregion public properties

        #region Stored-Procedures
        //****Added by Sunil Pahwa************
        public string GET_DELIVERY_SLIP_DETAIL
        {
            get { return "uspdeliverslipget"; }
        }
        //************************************
        public override string INSERT_SPNAME
        {
            get { return "usppurorddtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppurorddtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppurorddtldel"; }
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
            get { return "stuordrd"; }
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
        //Use in Print Report PO
        public string UPDATE_PO_DETAIL
        {
            get { return "uspstuordrupd"; }
        }
        public string UPDATE_PO_DETAIL_2
        {
            get { return "uspstuordr2upd"; }
        }
        public string Update_u_ordrd2
        {
            get { return "uspu_ordrd2"; }
        }

        //Added By Rahul Jain on 04-06-2009 using in Post Receipts report
        public string GET_stuordrd_curs
        {
            get { return "uspstuordrd_curs"; }
        }
        public string Update_str_newline
        {
            get { return "uspstr_newline"; }
        }
        public string Get_str_hilo
        {
            get { return "uspstr_hilo"; }
        }

        //Added By Rahul Jain on 19/01/2010
        public string UPDATE_LINE_STAGE
        {
            get { return "usppodtlupd"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT s2.doc_no v_doc_no,s2.line_no v_line_no,s2.cm_dm_reason v_cm_dm_reason,s2.mtaxg_code v_mtaxg_code,");
            sql.Append(" s2.line_type v_line_type,s2.line_stage v_line_stage,s2.receiver_printed v_receiver_printed,");
            sql.Append(" s2.request_date v_request_date,s2.po_date v_po_date,s2.rcpt_date v_rcpt_date,s2.inv_date v_inv_date,");
            sql.Append(" s2.required_date v_required_date,s2.whse_shipto v_whse_shipto,s2.whse_billto v_whse_billto,");
            sql.Append(" s2.item_code v_item_code,s2.desc1 v_desc1,s2.desc2 v_desc2,s2.td_disc_allowed v_td_disc_allowed,");
            sql.Append(" s2.bo_allowed v_bo_allowed,s2.ordr_qty v_ordr_qty,s2.rlse_qty v_rlse_qty,s2.rjct_qty v_rjct_qty,");
            sql.Append(" s2.recv_qty v_recv_qty,s2.cost_qty v_cost_qty,s2.acpt_qty v_acpt_qty,s2.exp_rec_qty v_exp_rec_qty,");
            sql.Append(" s2.exp_inv_qty v_exp_inv_qty,s2.sell_unit v_sell_unit,s2.purch_unit v_purch_unit,s2.stock_unit v_stock_unit,");
            sql.Append(" s2.unit_factor v_unit_factor,s2.cost v_cost,s2.gl_acct_no v_gl_acct_no,s2.net_price v_net_price,");
            sql.Append(" s2.department v_department,s2.instruct_code v_instruct_code,s2.authorization_code v_authoriz_code,");
            sql.Append(" s2.inspection_code v_inspection_code,s2.alias_code v_alias_code,s2.weight v_weight,");
            sql.Append(" s2.staging_area v_staging_area,s2.order_doc_no v_order_doc_no,s2.order_line_no v_order_line_no,");
            sql.Append(" s2.order_ship_no v_order_ship_no,s2.note_flag v_note_flag,s2.unit_tax v_unit_tax,s2.rowid v_rowid");
            sql.Append(" FROM stuordrd s2 WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND s2.RowId = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND s2.doc_no = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND s2.line_no = " + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s2.line_type) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s2.line_stage) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");


            //if (Convert.ToInt32(parameters[0]) > 0)
            //    sql.Append(" AND doc_no = " + parameters[0].ToString());
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND chk_date = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(print_chk) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(ok_to_post) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            //    else
            //        sql.Append(" AND Rtrim(ok_to_post) NOT IN ('P','C')");
            //if (parameters[4] != null)
            //    if (parameters[4].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(vend_code) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[5] != null)
            //    if (parameters[5].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(pay_to_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[6] != null)
            //    if (parameters[6].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(check_no) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[7] != null)
            //    if (parameters[7].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(min_voucher_no) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[8] != null)
            //    if (parameters[8].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(tre_voucher_no) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[9] != null)
            //    if (parameters[9].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(doc_desc) LIKE '" + parameters[9].ToString().Trim().Replace("'", "''") + "%'");
            //if (Convert.ToInt32(parameters[10]) > 0)
            //    sql.Append(" AND batch_id = " + parameters[10].ToString());
            //if (parameters[11] != null)
            //    if (parameters[11].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(ap_type) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            //if (Convert.ToInt32(parameters[12]) > 0)
            //    sql.Append(" AND stpcashe.RowId = " + parameters[12].ToString());
            //if (parameters[13].ToString() != string.Empty)
            //    sql.Append(" AND stpcashe. bus_name LIke'%" + parameters[13].ToString().Trim().Replace("'", "''") + "%'");
            //////if (Convert.ToInt32(parameters[13]) > 0)
            ////sql.Append(" AND current_approval = " + parameters[13].ToString());
            ////if (Convert.ToInt32(parameters[14]) > 0)
            ////    sql.Append(" AND required_approval = " + parameters[14].ToString());
            ////if (parameters[15] != null)
            ////    if (parameters[15].ToString() != string.Empty)
            ////        sql.Append(" AND Rtrim(PayrollGLAccounts.acct_type) = '" + parameters[15].ToString().Trim() + "'");
            ////     sql.Append("order by doc_no");
            return sql.ToString();
        }

        public string FIND_GOODS_REC_BY_GL_CODE(ref Object[] parameters)
        {


            System.Text.StringBuilder sql = new StringBuilder();


            sql.Append(" select stuordrd.gl_acct_no,stuordrd.department,sturecte.po_no, ");
            sql.Append(" sturecte.receipt_date,stuordrd.item_code,sturectd.recv_qty,");
            sql.Append(" stuordrd.cost, stuordre.currency_code,stuordre.currency_rate,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc ");
            sql.Append(" from sturecte, sturectd, stuordre, stuordrd ,PayrollGLAccounts ");
            sql.Append(" where sturecte.rec_doc_no = sturectd.rec_doc_no and ");
            sql.Append(" sturectd.po_doc_no = stuordrd.doc_no and ");
            sql.Append(" sturectd.po_line_no = stuordrd.line_no and ");
            sql.Append(" stuordre.doc_no = stuordrd.doc_no and ");
            sql.Append(" stuordrd.gl_acct_no = PayrollGLAccounts.acct_no ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stuordre.po_no= '" + parameters[0].ToString().Trim() + "'");


           // if (Convert.ToInt32(parameters[1]) > 0)
            //    sql.Append(" AND stuordrd.gl_acct_no = " + parameters[1].ToString());


            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stuordrd.item_code ='" + parameters[2].ToString().Trim() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND sturecte.receipt_date = '" + parameters[3].ToString().Trim() + "'"); //Convert.ToDateTime(parameters[3]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND PayrollGLAccounts.acct_type ='" + parameters[4].ToString().Trim() + "'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND PayrollGLAccounts.keyvalue  LIKE '" + parameters[5].ToString().Trim() + "%'");


            sql.Append(" order by stuordrd.gl_acct_no, sturecte.receipt_date ");
            return sql.ToString();
        }

        #endregion store-procedures
    }

    /// <summary>
    /// Comparer to make DVOPOPurchaseOrdersDetailStuordrd class to comparable for dist_amt
    /// </summary>
    public class DVOPOPurchaseOrdersDetailStuordrd_NetPrice_Comparer : IComparer<DVOPOPurchaseOrdersDetailStuordrd>
    {
        #region IComparer<Student> Members

        public int Compare(DVOPOPurchaseOrdersDetailStuordrd obj1, DVOPOPurchaseOrdersDetailStuordrd obj2)
        {
            int returnValue = 1;
            if (obj1 != null && obj2 != null)
            {
                returnValue = obj2.net_price.CompareTo(obj1.net_price);
            }

            return returnValue;
        }

        #endregion
    }
}
