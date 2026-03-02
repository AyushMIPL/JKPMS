using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOOrderstoordre : DVOBase
    {
        int _Rowid;
        int _doc_no;
        int _orig_doc_no;
        string _order_no;
        int _inv_doc_no;
        string _inv_no;
        string _po_no;
        int _pic_ticket_no;
        int _next_kit_group;
        string _ack_printed;
        string _order_type;
        string _like_type;
        string _order_status;
        string _hi_stage;
        string _lo_stage;
        string _bo_allowed;
        string _recur_unit;
        int _recur_every;
        int _recur_times;
        string _recur_through;//date,
        string _prev_recur;//date,
        string _next_recur;//date,
        int _num_releases;
        string _release_type;
        string _order_date;
        string _to_ship_date;
        string _alloc_date;
        string _ship_date;
        string _complete_date;
        string _warehouse_code;
        string _department;
        string _sls_psn_code;
        string _cust_code;
        string _ship_to_code;
        string _bill_to_code;
        string _bus_name;
        string _contact;
        string _address1;
        string _address2;
        string _city;
        string _state;
        string _zip;
        string _country;
        string _terms_code;
        string _terms_approval;
        string _pay_method;
        string _payment;
        string _card_no;
        string _exp_date;
        string _card_holder;
        string _check_no;
        string _trd_ds_code;
        string _trd_ds_type;
        decimal _trd_ds_rate;
        string _multi_shipto;
        decimal _tax_rate;
        string _staging_area;
        string _fob_point;
        string _ship_via;
        decimal _ship_weight;
        decimal _item_amount;
        decimal _discountable;
        decimal _trd_ds_amount;
        decimal _taxable;
        decimal _tax_amount;
        decimal _frght_amount;
        decimal _total_amount;
        string _create_date;
        string _create_time;
        string _create_id;
        string _l_mod_date;
        string _l_mod_time;
        string _l_mod_id;
        string _system_order;
        string _spr_no;
        string _cust_ord_date;
        string _cust_po_date;
        string _fact_ack_date;
        string _fact_rec_date;
        string _moto_rec_date;
        string _sent_to_wwop;
        string _mtaxg_code;
        string _intl_order;
        string _intl_lic_no;
        string _currency_code;
        string _curr_rate_type;
        decimal _currency_rate;
        string _edi_sent;
        string _blo_exp_date;
        string _dpas_rating;
        string _resale_cust;
        string _resale_po;
        decimal _actual_frght_amt;
        decimal _orig_frght_amt;

        public DVOOrderstoordre()
        {
            _Rowid = 0;
            _doc_no = 0;
            _orig_doc_no = 0;
            _order_no = string.Empty;
            _inv_doc_no = 0;
            _inv_no = string.Empty;
            _po_no = string.Empty;
            _pic_ticket_no = 0;
            _next_kit_group = 0;
            _ack_printed = string.Empty;
            _order_type = string.Empty;
            _like_type = string.Empty;
            _order_status = string.Empty;
            _hi_stage = string.Empty;
            _lo_stage = string.Empty;
            _bo_allowed = string.Empty;
            _recur_unit = string.Empty;
            _recur_every = 0;
            _recur_times = 0;
            _recur_through = "01/01/1900";//date,
            _prev_recur = "01/01/1900";//date,
            _next_recur = "01/01/1900";//date,
            _num_releases = 0;
            _release_type = string.Empty;
            _order_date = "01/01/1900"; //Date
            _to_ship_date = "01/01/1900";//Date 
            _alloc_date = "01/01/1900"; //Date
            _ship_date = "01/01/1900"; //Date
            _complete_date = "01/01/1900"; //Date
            _warehouse_code = string.Empty;
            _department = string.Empty;
            _sls_psn_code = string.Empty;
            _cust_code = string.Empty;
            _ship_to_code = string.Empty;
            _bill_to_code = string.Empty;
            _bus_name = string.Empty;
            _contact = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _city = string.Empty;
            _state = string.Empty;
            _zip = string.Empty;
            _country = string.Empty;
            _terms_code = string.Empty;
            _terms_approval = string.Empty;
            _pay_method = string.Empty;
            _payment = string.Empty;
            _card_no = string.Empty;
            _exp_date = string.Empty;
            _card_holder = string.Empty;
            _check_no = string.Empty;
            _trd_ds_code = string.Empty;
            _trd_ds_type = string.Empty;
            _trd_ds_rate = 0.0M;
            _multi_shipto = string.Empty;
            _tax_rate = 0.0M;
            _staging_area = string.Empty;
            _fob_point = string.Empty;
            _ship_via = string.Empty;
            _ship_weight = 0.0M;
            _item_amount = 0.0M;
            _discountable = 0.0M;
            _trd_ds_amount = 0.0M;
            _taxable = 0.0M;
            _tax_amount = 0.0M;
            _frght_amount = 0.0M;
            _total_amount = 0.0M;
            _create_date = "01/01/1900"; //date
            _create_time = string.Empty;
            _create_id = string.Empty;
            _l_mod_date = "01/01/1900"; //date
            _l_mod_time = string.Empty;
            _l_mod_id = string.Empty;
            _system_order = string.Empty;
            _spr_no = string.Empty;
            _cust_ord_date = "01/01/1900"; //date
            _cust_po_date = "01/01/1900"; //date
            _fact_ack_date = "01/01/1900"; //date
            _fact_rec_date = "01/01/1900"; //date
            _moto_rec_date = "01/01/1900"; //date
            _sent_to_wwop = string.Empty;
            _mtaxg_code = string.Empty;
            _intl_order = string.Empty;
            _intl_lic_no = string.Empty;
            _currency_code = string.Empty;
            _curr_rate_type = string.Empty;
            _currency_rate = 0.0M;
            _edi_sent = string.Empty;
            _blo_exp_date = string.Empty;
            _dpas_rating = string.Empty;
            _resale_cust = string.Empty;
            _resale_po = string.Empty;
            _actual_frght_amt = 0.0M;
            _orig_frght_amt = 0.0M;
        }

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
        public int orig_doc_no
        {
            get { return _orig_doc_no; }
            set { _orig_doc_no = value; }
        }
        public string order_no
        {
            get { return _order_no; }
            set { _order_no = value; }
        }
        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public string inv_no
        {
            get { return _inv_no; }
            set { _inv_no = value; }
        }
        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }
        public int pic_ticket_no
        {
            get { return _pic_ticket_no; }
            set { _pic_ticket_no = value; }
        }
        public int next_kit_group
        {
            get { return _next_kit_group; }
            set { _next_kit_group = value; }
        }
        public string ack_printed
        {
            get { return _ack_printed; }
            set { _ack_printed = value; }
        }
        public string order_type
        {
            get { return _order_type; }
            set { _order_type = value; }
        }
        public string like_type
        {
            get { return _like_type; }
            set { _like_type = value; }
        }
        public string order_status
        {
            get { return _order_status; }
            set { _order_status = value; }
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
        public string bo_allowed
        {
            get { return _bo_allowed; }
            set { _bo_allowed = value; }
        }
        public string recur_unit
        {
            get { return _recur_unit; }
            set { _recur_unit = value; }
        }
        public int recur_every
        {
            get { return _recur_every; }
            set { _recur_every = value; }
        }
        public int recur_times
        {
            get { return _recur_times; }
            set { _recur_times = value; }
        }
        public string recur_through
        {
            get { return _recur_through; }
            set { _recur_through = value; }
        }
        public string prev_recur
        {
            get { return _prev_recur; }
            set { _prev_recur = value; }
        }
        public string next_recur
        {
            get { return _next_recur; }
            set { _next_recur = value; }
        }
        public int num_releases
        {
            get { return _num_releases; }
            set { _num_releases = value; }
        }
        public string release_type
        {
            get { return _release_type; }
            set { _release_type = value; }
        }
        public string order_date
        {
            get { return _order_date; }
            set { _order_date = value; }
        }
        public string to_ship_date
        {
            get { return _to_ship_date; }
            set { _to_ship_date = value; }
        }
        public string alloc_date
        {
            get { return _alloc_date; }
            set { _alloc_date = value; }
        }
        public string ship_date
        {
            get { return _ship_date; }
            set { _ship_date = value; }
        }
        public string complete_date
        {
            get { return _complete_date; }
            set { _complete_date = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string sls_psn_code
        {
            get { return _sls_psn_code; }
            set { _sls_psn_code = value; }
        }
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public string ship_to_code
        {
            get { return _ship_to_code; }
            set { _ship_to_code = value; }
        }
        public string bill_to_code
        {
            get { return _bill_to_code; }
            set { _bill_to_code = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string terms_approval
        {
            get { return _terms_approval; }
            set { _terms_approval = value; }
        }
        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }
        public string payment
        {
            get { return _payment; }
            set { _payment = value; }
        }
        public string card_no
        {
            get { return _card_no; }
            set { _card_no = value; }
        }
        public string exp_date
        {
            get { return _exp_date; }
            set { _exp_date = value; }
        }
        public string card_holder
        {
            get { return _card_holder; }
            set { _card_holder = value; }
        }
        public string check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }
        public string trd_ds_code
        {
            get { return _trd_ds_code; }
            set { _trd_ds_code = value; }
        }
        public string trd_ds_type
        {
            get { return _trd_ds_type; }
            set { _trd_ds_type = value; }
        }
        public decimal trd_ds_rate
        {
            get { return _trd_ds_rate; }
            set { _trd_ds_rate = value; }
        }
        public string multi_shipto
        {
            get { return _multi_shipto; }
            set { _multi_shipto = value; }
        }
        public decimal tax_rate
        {
            get { return _tax_rate; }
            set { _tax_rate = value; }
        }
        public string staging_area
        {
            get { return _staging_area; }
            set { _staging_area = value; }
        }
        public string fob_point
        {
            get { return _fob_point; }
            set { _fob_point = value; }
        }
        public string ship_via
        {
            get { return _ship_via; }
            set { _ship_via = value; }
        }
        public decimal ship_weight
        {
            get { return _ship_weight; }
            set { _ship_weight = value; }
        }
        public decimal item_amount
        {
            get { return _item_amount; }
            set { _item_amount = value; }
        }
        public decimal discountable
        {
            get { return _discountable; }
            set { _discountable = value; }
        }
        public decimal trd_ds_amount
        {
            get { return _trd_ds_amount; }
            set { _trd_ds_amount = value; }
        }
        public decimal taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }
        public decimal tax_amount
        {
            get { return _tax_amount; }
            set { _tax_amount = value; }
        }
        public decimal frght_amount
        {
            get { return _frght_amount; }
            set { _frght_amount = value; }
        }
        public decimal total_amount
        {
            get { return _total_amount; }
            set { _total_amount = value; }
        }
        public string create_date
        {
            get { return _create_date; }
            set { _create_date = value; }
        }
        public string create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }
        public string create_id
        {
            get { return _create_id; }
            set { _create_id = value; }
        }
        public string l_mod_date
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
        public string system_order
        {
            get { return _system_order; }
            set { _system_order = value; }
        }
        public string spr_no
        {
            get { return _spr_no; }
            set { _spr_no = value; }
        }
        public string cust_ord_date
        {
            get { return _cust_ord_date; }
            set { _cust_ord_date = value; }
        }
        public string cust_po_date
        {
            get { return _cust_po_date; }
            set { _cust_po_date = value; }
        }
        public string fact_ack_date
        {
            get { return _fact_ack_date; }
            set { _fact_ack_date = value; }
        }
        public string fact_rec_date
        {
            get { return _fact_rec_date; }
            set { _fact_rec_date = value; }
        }
        public string moto_rec_date
        {
            get { return _moto_rec_date; }
            set { _moto_rec_date = value; }
        }
        public string sent_to_wwop
        {
            get { return _sent_to_wwop; }
            set { _sent_to_wwop = value; }
        }
        public string mtaxg_code
        {
            get { return _mtaxg_code; }
            set { _mtaxg_code = value; }
        }
        public string intl_order
        {
            get { return _intl_order; }
            set { _intl_order = value; }
        }
        public string intl_lic_no
        {
            get { return _intl_lic_no; }
            set { _intl_lic_no = value; }
        }
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string curr_rate_type
        {
            get { return _curr_rate_type; }
            set { _curr_rate_type = value; }
        }
        public decimal currency_rate
        {
            get { return _currency_rate; }
            set { _currency_rate = value; }
        }
        public string edi_sent
        {
            get { return _edi_sent; }
            set { _edi_sent = value; }
        }
        public string blo_exp_date
        {
            get { return _blo_exp_date; }
            set { _blo_exp_date = value; }
        }
        public string dpas_rating
        {
            get { return _dpas_rating; }
            set { _dpas_rating = value; }
        }
        public string resale_cust
        {
            get { return _resale_cust; }
            set { _resale_cust = value; }
        }
        public string resale_po
        {
            get { return _resale_po; }
            set { _resale_po = value; }
        }
        public decimal actual_frght_amt
        {
            get { return _actual_frght_amt; }
            set { _actual_frght_amt = value; }
        }
        public decimal orig_frght_amt
        {
            get { return _orig_frght_amt; }
            set { _orig_frght_amt = value; }
        }


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
            get { return "stoordre"; }
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
        //Using in Print Purchase ordre report
        public string GET_ORDER_NO
        {
            get { return "uspordernoget"; }
        }
        public string GET_ALL_ORDNO
        {
            get { return "uspordernogetall"; }
        }
        //Added by rahul Jain using in pring order acknoledgement report
        public string GET_COUNT
        {
            get { return "uspstoordrecount"; }
        }
        //Using in Print picking Documents
        public string GET_PICKING_DOC_INFO_STOORDRE
        {
            get { return "usppdocstoordreget"; }
        }
        //Added by sunil Pahwa for InvMomos
        public string GET_ADDRESS_INFO
        {
            get { return "uspimosdreget"; }
        }

        public string UPD_PICKING_DOC_INFO_STOORDRE
        {
            get { return "uspdocstoordreUpd"; }
        }

        public string GET_ORDER_ENTRY_DRE_INFO
        {
            get { return "uspordentydreget"; }
        }
        //Added By Rahul jain using in Print Open Order Summary
        public string GET_ORDER_INVOICE_TOTALS
        {
            get { return "uspc_totals"; }
        }

        public string GET_INVOICES_MOMOS
        {
            get { return "uspimosdreget"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Order Acknoledgement
        /// Created By : Rahul
        /// Created Date : 14/06/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_ORDER_ACKNOLEDGEMENT(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stoordre.rowid,stoordre.address1,stoordre.address2,stoordre.bus_name, ");
            sql.Append("stoordre.city,stoordre.country,stoordre.currency_code,  ");
            sql.Append("stoordre.cust_code,stoordre.frght_amount,stoordre.item_amount,  ");
            sql.Append("stoordre.order_date,stoordre.order_no,stoordre.pay_method,  ");
            sql.Append("stoordre.payment,stoordre.po_no,stoordre.ship_via,  ");
            sql.Append("stoordre.sls_psn_code,stoordre.state,stoordre.tax_amount, ");
            sql.Append("stoordre.terms_code,stoordre.total_amount,stoordre.trd_ds_amount,  ");
            sql.Append("stoordre.zip,stoshipd.bill_to_code,stoshipd.doc_no, ");
            sql.Append("stoshipd.item_code,stoshipd.line_no,stoshipd.net_amount,  ");
            sql.Append("stoshipd.price,stoshipd.sell_to_code,stoshipd.ship_qty, ");
            sql.Append("stoshipd.ship_to_code");
            sql.Append(" From  stoordre, stoshipd, stoordrd  ");
            sql.Append(" WHERE stoshipd.doc_no = stoordre.doc_no");
            sql.Append(" and stoshipd.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoshipd.line_no = stoordrd.line_no");
            sql.Append(" and stoshipd.stage != 'CAN' ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoordre.ack_printed  ='" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND stoordre.doc_no =" + parameters[3]);
            if (parameters[4] != null)
                if (!parameters[4].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[4].ToString() + "'");
            sql.Append(" ORDER BY stoshipd.doc_no, stoshipd.bill_to_code, stoshipd.sell_to_code, ");
            sql.Append(" stoshipd.ship_to_code, stoshipd.line_no, stoshipd.price ");
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Open Order Summary
        /// Created By : Rahul
        /// Created Date : 17/07/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_OPEN_ORDER_SUMMARY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stoordre.currency_code,stoordre.currency_rate,stoordre.cust_code, ");
            sql.Append(" stoordre.doc_no,stoordre.frght_amount,stoordre.hi_stage, ");
            sql.Append(" stoordre.item_amount,stoordre.like_type,stoordre.lo_stage, ");
            sql.Append(" stoordre.order_date,stoordre.order_no,stoordre.order_status, ");
            sql.Append(" stoordre.order_type,stoordre.sls_psn_code,stoordre.tax_amount, ");
            sql.Append(" stoordre.to_ship_date,stoordre.total_amount,stoordre.trd_ds_amount ");
            sql.Append(" From  stoordre  ");
            sql.Append(" WHERE 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != "Y")
                    sql.Append(" and stoordre.lo_stage not in ('CAN','PST') ");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[2].ToString() + "'");
             if (parameters[3] != null)
                if (!parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.to_ship_date = '" + parameters[3].ToString() + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stoordre.doc_no =" + parameters[5]);
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_type = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND stoordre.lo_stage = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            
            sql.Append(" ORDER BY stoordre.like_type, stoordre.order_no, stoordre.doc_no ");

            return sql.ToString();
        }


        /// <summary>
        /// Find Query to get the data for report :Print Salesperson Summary
        /// Created By : Sunil
        /// Created Date : 22/07/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_GET_SALESPERSON_SUMMARY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordrd.sls_psn_code, stoordre.currency_code, stoordre.currency_rate, stoordre.doc_no, ");
            sql.Append(" stoordre.like_type, stoordre.lo_stage, stoordre.order_date, stoordre.order_no, stoordre.order_status, ");
            sql.Append(" stoordre.order_type, stoshipd.net_amount, stoshipd.sell_to_code, stoshipd.stage,stoordre.cust_code ");
            sql.Append(" from  stoordre, stoordrd, stoshipd where stoordre.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoordrd.doc_no = stoshipd.doc_no and stoordrd.line_no = stoshipd.line_no ");
            sql.Append(" and stoordre.lo_stage not in ('PST','CAN') ");


            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.sls_psn_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");


            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");


            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[2].ToString() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_type = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");


            sql.Append(" ORDER BY stoordrd.sls_psn_code, stoordre.like_type, stoshipd.sell_to_code, stoordre.order_no, stoordre.doc_no ");

            return sql.ToString();
        }


        /// <summary>
        /// Find Query to get the data for report :Print Salesperson Detail
        /// Created By : Sunil
        /// Created Date : 23/07/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_GET_SALESPERSON_DETAIL(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordrd.line_no, stoordrd.sls_psn_code, stoordre.currency_code, ");
            sql.Append(" stoordre.currency_rate,stoordre.doc_no, stoordre.like_type, stoordre.lo_stage, ");
            sql.Append(" stoordre.order_date, stoordre.order_no, stoordre.order_status,stoordre.order_type, ");
            sql.Append(" stoshipd.bill_to_code, stoshipd.bko_date,stoshipd.can_date, stoshipd.inv_date, ");
            sql.Append(" stoshipd.item_code, stoshipd.net_amount, stoshipd.new_date, stoshipd.ord_date, ");
            sql.Append(" stoshipd.pic_date, stoshipd.pst_date, stoshipd.sell_to_code,stoshipd.ship_qty, ");
            sql.Append(" stoshipd.ship_to_code, stoshipd.shp_date,stoshipd.stage, stoshipd.warehouse_code ");
            sql.Append(" from stoordre, stoordrd, stoshipd where  stoordre.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoordrd.doc_no = stoshipd.doc_no and stoordrd.line_no = stoshipd.line_no ");
            sql.Append(" and stoordre.lo_stage not in ('PST','CAN')");



            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.sls_psn_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");


            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");


            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[2].ToString() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
      
            if (parameters[4] != null)
            if (parameters[4].ToString() != string.Empty)
                sql.Append(" AND stoordre.order_type = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");


            sql.Append(" ORDER BY stoordrd.sls_psn_code, stoordre.like_type, stoshipd.sell_to_code, stoordre.order_no, stoordre.doc_no, stoordrd.line_no ");

            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Open Order Summary
        /// Created By : Rahul
        /// Created Date : 17/07/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_OPEN_ORDER_DETAILS(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
           
            sql.Append("select stoordrd.ordr_qty,stoordre.create_date,stoordre.create_id, ");
            sql.Append(" stoordre.create_time,stoordre.currency_code,stoordre.currency_rate, ");
            sql.Append(" stoordre.cust_code,stoordre.doc_no,stoordre.frght_amount,  ");
            sql.Append(" stoordre.hi_stage,stoordre.item_amount,stoordre.l_mod_date, ");
            sql.Append(" stoordre.l_mod_id,stoordre.l_mod_time,stoordre.like_type, ");
            sql.Append(" stoordre.lo_stage,stoordre.order_date,stoordre.order_no,  ");
            sql.Append(" stoordre.order_status,stoordre.order_type,stoordre.pay_method, ");
            sql.Append(" stoordre.tax_amount,stoordre.total_amount,stoordre.trd_ds_amount, ");
            sql.Append(" stoshipd.bill_to_code,stoshipd.bko_date,stoshipd.can_date,  ");
            sql.Append(" stoshipd.inv_date,stoshipd.item_code,stoshipd.line_no,  ");
            sql.Append(" stoshipd.new_date,stoshipd.ord_date,stoshipd.pic_date,  ");
            sql.Append(" stoshipd.pst_date,stoshipd.sell_to_code,stoshipd.ship_no,  ");
            sql.Append(" stoshipd.ship_qty,stoshipd.ship_to_code,stoshipd.shp_date, ");
            sql.Append(" stoshipd.stage,stoshipd.warehouse_code  ");
            sql.Append(" From  stoordre, stoordrd, stoshipd  ");
            sql.Append(" WHERE stoordre.doc_no = stoordrd.doc_no "); 
	        sql.Append(" and stoordre.doc_no = stoshipd.doc_no "); 
	        sql.Append(" and stoordrd.line_no = stoshipd.line_no "); 
            if (parameters[0] != null)
                if (parameters[0].ToString() != "Y")
                    sql.Append(" and stoordre.lo_stage not in ('PST','CAN') ");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[2].ToString() + "'");
            if (parameters[3] != null)
                if (!parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.to_ship_date = '" + parameters[3].ToString() + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stoordre.doc_no =" + parameters[5]);
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_type = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND stoordre.lo_stage = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");

            sql.Append(" ORDER BY stoordre.like_type, stoordre.doc_no, stoshipd.line_no,stoshipd.ship_no ");

            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Order Status
        /// Created By : Rahul
        /// Created Date : 27/07/09
        /// </summary>
        public string FINDQUERY_ORDER_STATUS(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stoordre.bus_name,stoordre.create_date,stoordre.create_id, ");
            sql.Append(" stoordre.create_time,stoordre.cust_code,stoordre.doc_no, ");
            sql.Append(" stoordre.fob_point,stoordre.frght_amount,stoordre.item_amount, ");
            sql.Append(" stoordre.l_mod_date,stoordre.l_mod_id,stoordre.l_mod_time, ");
            sql.Append(" stoordre.lo_stage,stoordre.order_date,stoordre.order_no, ");
            sql.Append(" stoordre.order_status,stoordre.order_type,stoordre.pay_method, ");
            sql.Append(" stoordre.ship_via,stoordre.tax_amount,stoordre.total_amount, ");
            sql.Append(" stoordre.trd_ds_amount,stoshipd.bill_to_code,stoshipd.bko_date, ");
            sql.Append(" stoshipd.can_date,stoshipd.inv_date,stoshipd.inv_doc_no, ");
            sql.Append(" stoshipd.item_code,stoshipd.line_no,stoshipd.new_date, ");
            sql.Append(" stoshipd.ord_date,stoshipd.pic_date,stoshipd.price, ");
            sql.Append(" stoshipd.pst_date,stoshipd.sell_to_code,stoshipd.ship_no, ");
            sql.Append(" stoshipd.ship_qty,stoshipd.ship_to_code,stoshipd.shp_date, ");
            sql.Append(" stoshipd.stage,stoshipd.warehouse_code ");
            sql.Append(" From  stoordre, stoshipd  ");
            sql.Append(" WHERE stoordre.doc_no = stoshipd.doc_no ");
            sql.Append(" ORDER BY stoordre.doc_no, stoshipd.line_no, stoshipd.ship_no ");
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Customer Order Summary
        /// Created By : Rahul
        /// Created Date : 03/08/09
        public string FINDQUERY_GET_CUSTOMERORDER_SUMMARY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordre.currency_code,stoordre.currency_rate,stoordre.doc_no, ");
            sql.Append(" stoordre.like_type,stoordre.lo_stage,stoordre.order_date, ");
            sql.Append(" stoordre.order_no,stoordre.order_status,stoordre.order_type, ");
            sql.Append(" stoshipd.net_amount,stoshipd.sell_to_code,stoshipd.stage ");
            sql.Append(" from stoordre, stoordrd, stoshipd ");
            sql.Append(" WHERE stoordre.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoordrd.doc_no = stoshipd.doc_no ");
            sql.Append(" and stoordrd.line_no = stoshipd.line_no ");
            sql.Append(" and stoshipd.stage not in ('PST','CAN') ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoshipd.sell_to_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_type = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (!parameters[4].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[4].ToString() + "'");
            sql.Append(" ORDER BY  stoordre.like_type,stoshipd.sell_to_code, stoordre.order_no,stoordre.doc_no ");
            return sql.ToString();
        }
        /// <summary>
        /// Find Query to get the data for report :Print Customer Order Detail
        /// Created By : Rahul
        /// Created Date : 07/08/09
        public string FINDQUERY_GET_CUSTOMERORDER_DETAIL(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordrd.line_no,stoordre.currency_code,stoordre.currency_rate,stoordre.doc_no, ");
            sql.Append(" stoordre.like_type,stoordre.lo_stage,stoordre.order_date,stoordre.order_no, ");
            sql.Append(" stoordre.order_status,stoordre.order_type,stoshipd.bill_to_code,stoshipd.bko_date, ");
            sql.Append(" stoshipd.can_date,stoshipd.inv_date,stoshipd.item_code,stoshipd.net_amount, ");
            sql.Append(" stoshipd.new_date,stoshipd.ord_date,stoshipd.pic_date,stoshipd.pst_date, ");
            sql.Append(" stoshipd.sell_to_code,stoshipd.ship_qty,stoshipd.ship_to_code,stoshipd.shp_date, ");
            sql.Append(" stoshipd.stage, stoshipd.warehouse_code ");
            sql.Append(" from stoordre, stoordrd, stoshipd ");
            sql.Append(" WHERE stoordre.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoordrd.doc_no = stoshipd.doc_no ");
            sql.Append(" and stoordrd.line_no = stoshipd.line_no ");
            sql.Append(" and stoordre.lo_stage not in ('PST','CAN') ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stoshipd.sell_to_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_type = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_no = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (!parameters[4].ToString().Contains("1900"))
                    sql.Append(" AND stoordre.order_date = '" + parameters[4].ToString() + "'");
            sql.Append(" ORDER BY  stoordre.like_type,stoshipd.sell_to_code, stoordre.order_no,stoordre.doc_no,stoordrd.line_no ");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
