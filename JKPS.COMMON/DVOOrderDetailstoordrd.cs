using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOOrderDetailstoordrd :DVOBase
    {
        int _Rowid;
        int _doc_no;
        int _line_no;
        int _kit_group;
        int _kit_line_no;
        int _last_ship_line;
        string _line_type;
        string _like_type;
        string _hi_stage;
        string _lo_stage;
        string _cm_dm_reason;
        string _our_po_no;
        string _sls_psn_code;
        string _warehouse_code;
        string _item_code;
        string _desc1;
        string _desc2;
        string _alias_code;
        string _vend_code;
        string _interchanged;
        string _serialized;
        string _td_disc_allowed;
        string _tax;
        decimal _ordr_qty;
        decimal _back_qty;
        decimal _commit_qty;
        string _sell_unit;
        decimal _unit_factor;
        decimal _price;
        int _price_code;
        decimal _tax_amount;
        decimal _net_amount;
        decimal _ship_weight;
        int _inv_acct;
        string _inv_dept;
        int _sls_acct;
        string _sls_dept;
        int _cog_acct;
        string _cog_dept;
        string _intl_lic_no;
        string _price_lock;
        decimal _release_qty;
        decimal _resale_price;

        string _mfs_printed;
        string _cust_code;
        DateTime _ship_date;
        DateTime _order_date;
        string _order_no;

        //Added By Rajeev
        //Aim: To use in reports
        private string _stDate;
        private string _endDate;


        #region Constructor
        public DVOOrderDetailstoordrd()
        {
            _Rowid = 0;
            _doc_no = 0;
            _line_no = 0;
            _kit_group = 0;
            _kit_line_no = 0;
            _last_ship_line = 0;
            _line_type = string.Empty;
            _like_type = string.Empty;
            _hi_stage = string.Empty;
            _lo_stage = string.Empty;
            _cm_dm_reason = string.Empty;
            _our_po_no = string.Empty;
            _sls_psn_code = string.Empty;
            _warehouse_code = string.Empty;
            _item_code = string.Empty;
            _desc1 = string.Empty;
            _desc2 = string.Empty;
            _alias_code = string.Empty;
            _vend_code = string.Empty;
            _interchanged = string.Empty;
            _serialized = string.Empty;
            _td_disc_allowed = string.Empty;
            _tax = string.Empty;
            _ordr_qty = 0.0M;
            _back_qty = 0.0M;
            _commit_qty = 0.0M;
            _sell_unit = string.Empty;
            _unit_factor = 0.0M;
            _price = 0.0M;
            _price_code = 0;
            _tax_amount = 0.0M;
            _net_amount = 0.0M;
            _ship_weight = 0.0M;
            _inv_acct = 0;
            _inv_dept = string.Empty;
            _sls_acct = 0;
            _sls_dept = string.Empty;
            _cog_acct = 0;
            _cog_dept = string.Empty;
            _intl_lic_no = string.Empty;
            _price_lock = string.Empty;
            _release_qty = 0.0M;
            _resale_price = 0.0M;

            _mfs_printed = string.Empty;
            _cust_code = string.Empty;
            _ship_date = Convert.ToDateTime("01/01/1900");
            _order_date = Convert.ToDateTime("01/01/1900");
            _order_no = string.Empty;

            _stDate = string.Empty;
            _endDate = string.Empty;

        }
        #endregion

        #region Property   
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
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
        public int kit_group
        {
            get { return _kit_group; }
            set { _kit_group = value; }
        }
        public int kit_line_no
        {
            get { return _kit_line_no; }
            set { _kit_line_no = value; }
        }
        public int last_ship_line
        {
            get { return _last_ship_line; }
            set { _last_ship_line = value; }
        }
        public string line_type
        {
            get { return _line_type; }
            set { _line_type = value; }
        }
        public string like_type
        {
            get { return _like_type; }
            set { _like_type = value; }
        }
        public string hi_stage
        {
            get { return _hi_stage; }
            set { _hi_stage = value; }
        }
        public string lo_stage
        {
            get { return _lo_stage; }
            set { _lo_stage = value; }
        }
        public string cm_dm_reason
        {
            get { return _cm_dm_reason; }
            set { _cm_dm_reason = value; }
        }
        public string our_po_no
        {
            get { return _our_po_no; }
            set { _our_po_no = value; }
        }
        public string sls_psn_code
        {
            get { return _sls_psn_code; }
            set { _sls_psn_code = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
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
        public string alias_code
        {
            get { return _alias_code; }
            set { _alias_code = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string interchanged
        {
            get { return _interchanged; }
            set { _interchanged = value; }
        }
        public string serialized
        {
            get { return _serialized; }
            set { _serialized = value; }
        }
        public string td_disc_allowed
        {
            get { return _td_disc_allowed; }
            set { _td_disc_allowed = value; }
        }
        public string tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        public decimal ordr_qty
        {
            get { return _ordr_qty; }
            set { _ordr_qty = value; }
        }
        public decimal back_qty
        {
            get { return _back_qty; }
            set { _back_qty = value; }
        }
        public decimal commit_qty
        {
            get { return _commit_qty; }
            set { _commit_qty = value; }
        }
        public string sell_unit
        {
            get { return _sell_unit; }
            set { _sell_unit = value; }
        }
        public decimal unit_factor
        {
            get { return _unit_factor; }
            set { _unit_factor = value; }
        }
        public decimal price
        {
            get { return _price; }
            set { _price = value; }
        }
        public int price_code
        {
            get { return _price_code; }
            set { _price_code = value; }
        }
        public decimal tax_amount
        {
            get { return _tax_amount; }
            set { _tax_amount = value; }
        }
        public decimal net_amount
        {
            get { return _net_amount; }
            set { _net_amount = value; }
        }
        public decimal ship_weight
        {
            get { return _ship_weight; }
            set { _ship_weight = value; }
        }
        public int inv_acct
        {
            get { return _inv_acct; }
            set { _inv_acct = value; }
        }
        public string inv_dept
        {
            get { return _inv_dept; }
            set { _inv_dept = value; }
        }
        public int sls_acct
        {
            get { return _sls_acct; }
            set { _sls_acct = value; }
        }
        public string sls_dept
        {
            get { return _sls_dept; }
            set { _sls_dept = value; }
        }
         public int cog_acct
        {
            get { return _cog_acct; }
            set { _cog_acct = value; }
        }
        public string cog_dept
        {
            get { return _cog_dept; }
            set { _cog_dept = value; }
        }
        public string intl_lic_no
        {
            get { return _intl_lic_no; }
            set { _intl_lic_no = value; }
        }
        public string price_lock
        {
            get { return _price_lock; }
            set { _price_lock = value; }
        }

        public decimal release_qty
        {
            get { return _release_qty; }
            set { _release_qty = value; }
        }
        public decimal resale_price
        {
            get { return _resale_price; }
            set { _resale_price = value; }
        }

        public string mfs_printed
        {
            get { return _mfs_printed; }
            set { _mfs_printed = value; }
        }
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public DateTime ship_date
        {
            get { return _ship_date; }
            set { _ship_date = value; }
        }
        public DateTime order_date
        {
            get { return _order_date; }
            set { _order_date = value; }
        }
        public string order_no
        {
            get { return _order_no; }
            set { _order_no = value; }
        }

        public string stDate
        {
            get { return _stDate; }
            set { _stDate = value; }
        }
        public string endDate
        {
            get { return _endDate; }
            set { _endDate = value; }
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stoordrd"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //***Added by sunil pahwa *******************
         public  string GET_INFO_FOR_ORDER_ENTRY_FROM_DRD
        {
            get { return "uspordentdrdget"; }
        }
        
        

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Packing Slip
        /// Created By : Rahul
        /// Created Date : 16/07/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_PACKING_SLIP(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stoordrd.alias_code, stoordrd.desc1, stoordrd.desc2, ");
            sql.Append("stoordrd.interchanged, stoordrd.item_code, stoordrd.kit_group, ");
            sql.Append("stoordrd.sell_unit, stoordrd.serialized, stoordre.address1, ");
            sql.Append("stoordre.address2, stoordre.bus_name, stoordre.city, ");
            sql.Append("stoordre.country, stoordre.doc_no, stoordre.l_mod_time, ");
            sql.Append("stoordre.order_date, stoordre.order_no, stoordre.order_status, ");
            sql.Append("stoordre.po_no, stoordre.ship_via, stoordre.sls_psn_code, ");
            sql.Append("stoordre.staging_area, stoordre.state, stoordre.zip, stoshipd.line_no, ");
            sql.Append("stoshipd.pic_ticket_no, stoshipd.sell_to_code, stoshipd.ship_no, ");
            sql.Append("stoshipd.ship_qty, stoshipd.ship_to_code, stoshipd.shp_date, ");
            sql.Append("stoshipd.warehouse_code, stoordrd.rowid as detail_row");
            sql.Append(" From  stoordre, stoshipd, stoordrd  ");
            sql.Append(" WHERE stoordre.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoordrd.doc_no = stoshipd.doc_no  ");
            sql.Append(" and stoshipd.line_no = stoordrd.line_no");
            sql.Append(" and stoshipd.line_no > 0  ");
            sql.Append(" and (stoshipd.stage in ('PIC','SHP') or (stoshipd.stage = 'ORD' and stoshipd.pic_ticket_no = 0))  ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoshipd.mfs_printed   ='" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND stoshipd.shp_date = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (!parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stoordre.doc_no =" + parameters[5]);
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND stoshipd.warehouse_code = '" + parameters[6].ToString() + "'");

            
            sql.Append(" ORDER BY stoshipd.warehouse_code, stoordre.doc_no, stoshipd.sell_to_code, stoshipd.ship_to_code,  ");
            sql.Append(" stoshipd.shp_date, stoshipd.line_no ");
            return sql.ToString();
        }

        #region Report SalesPersonSummery
        /// <summary>
        /// Added By Rajeev 
        /// Aim : To Print Customer Details
        /// Date : 25/07/2009
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_SALESPERSON_SUMMERY(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select stoinvce.currency_code,stoinvce.currency_rate,stoinvce.inv_date,");
            sql.Append(" stoordrd.sls_psn_code,stoshipd.doc_no,stoshipd.gross_margin,stoshipd.net_amount ");
            sql.Append(" from stoordrd, stoshipd, stoinvce");
            sql.Append(" Where stoordrd.doc_no = stoshipd.doc_no and stoinvce.inv_doc_no = stoshipd.inv_doc_no and");
            sql.Append(" stoordrd.line_no = stoshipd.line_no and stoshipd.stage = 'PST'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.sls_psn_code = " + parameters[0].ToString());
            if (parameters[1] != null && parameters[2] != null)
                if (parameters[1].ToString() != string.Empty && parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoinvce.inv_date between '" + parameters[1].ToString() + "' and '" + parameters[2].ToString() + "'");

            sql.Append(" ORDER BY stoordrd.sls_psn_code");
            return sql.ToString();
        }

        public string SALESPERSON_NAME
        {
            get { return "uspslspsndesget"; }
        }

        public string PRODUCT_DESC
        {
            get { return "uspprdescget"; }
        }

        #endregion Report SalesPersonSummery


        public string FINDQUERY_SALESPERSON_DETAILS(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select stoinvce.currency_code,stoinvce.currency_rate,stoinvce.inv_date,stoinvce.sell_to_code,");
            sql.Append(" stoinvce.tax_amount,stoordrd.sls_psn_code,stoshipd.doc_no,stoshipd.inv_doc_no,stoshipd.net_amount ");
            sql.Append(" from stoordrd, stoshipd, stoinvce ");
            sql.Append(" Where stoordrd.doc_no = stoshipd.doc_no and stoinvce.inv_doc_no = stoshipd.inv_doc_no and");
            sql.Append(" stoordrd.line_no = stoshipd.line_no and stoshipd.stage = 'PST'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.sls_psn_code = " + parameters[0].ToString());
            if (parameters[1] != null && parameters[2] != null)
                if (parameters[1].ToString() != string.Empty && parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoinvce.inv_date between '" + parameters[1].ToString() + "' and '" + parameters[2].ToString() + "'");

            sql.Append(" ORDER BY stoordrd.sls_psn_code, stoshipd.inv_doc_no");
            return sql.ToString();
        }


        public string FINDQUERY_SALESPERSON_BYPRODUCT(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.item_class,stoinvce.inv_date,stoordrd.sls_psn_code,stoshipd.doc_no,stoshipd.gross_margin,stoshipd.net_amount");
            sql.Append("  ");
            sql.Append(" from stoordrd, stoinvce, stoshipd, outer(stiinvtr)");
            sql.Append(" where stiinvtr.item_code = stoshipd.item_code and stoordrd.doc_no = stoshipd.doc_no and stoordrd.line_no = stoshipd.line_no and");
            sql.Append(" stoinvce.inv_doc_no = stoshipd.inv_doc_no and stoshipd.stage = 'PST'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.sls_psn_code = " + parameters[0].ToString());
            if (parameters[1] != null && parameters[2] != null)
                if (parameters[1].ToString() != string.Empty && parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoinvce.inv_date between '" + parameters[1].ToString() + "' and '" + parameters[2].ToString() + "'");

            sql.Append(" ORDER BY stoordrd.sls_psn_code,stiinvtr.item_class");
            return sql.ToString();
        }

        #endregion store-procedures
    }
}
