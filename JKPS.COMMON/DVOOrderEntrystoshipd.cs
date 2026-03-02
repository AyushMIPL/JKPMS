
using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 08-06-2009 for Order Entry 
    public class DVOOrderEntrystoshipd : DVOBase
    {
        private int _rowid;
        private int _doc_no;
        private int _line_no;
        private int _ship_no;
        private int _kit_group;
        private string _stage;
        private decimal _ship_qty;
        private string _sell_unit;
        private decimal _ship_weight;
        private decimal _commit_qty;
        private int _pic_ticket_no;
        private string _mfs_printed;
        private int _inv_doc_no;
        private string _sell_to_code;
        private string _ship_to_code;
        private string _bill_to_code;
        private string _item_code;
        private string _warehouse_code;
        private string _stock_location;
        private string _price_group;
        private decimal _price;
        private decimal _orig_price;
        private decimal _retail_price;
        private string _price_approval;
        private decimal _tax_rate;
        private decimal _tax_amount;
        private decimal _net_amount;
        private decimal _item_cost;
        private decimal _gross_margin;
        private DateTime _new_date;
        private DateTime _bko_date;
        private DateTime _ord_date;
        private DateTime _pic_date;
        private DateTime _shp_date;
        private DateTime _inv_date;
        private DateTime _pst_date;
        private DateTime _can_date;
        private DateTime _proj_ship_date;
        private DateTime _request_date;
        private DateTime _reject_date;
        private DateTime _actual_ship_date;
        private DateTime _fact_sched_date;
        private string _ship_via_cd;
        private string _mtaxg_code;
        private int _po_doc_no;
   
        private string _cust_code;
        private string _order_no;
        private string _reprint;
        private string _hi_stage;
        private string _lo_stage;      

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;
        private string _like_type;
        private string _order_type;

        public DVOOrderEntrystoshipd()
        {
            _rowid = 0;
            _doc_no = 0;
            _line_no = 0;
            _ship_no = 0;
            _kit_group = 0;
            _stage = "";
            _ship_qty = 0.0M;
            _sell_unit = "";
            _ship_weight = 0.0M;
            _commit_qty = 0.0M;
            _pic_ticket_no = 0;
            _mfs_printed = "";
            _inv_doc_no = 0;
            _sell_to_code = "";
            _ship_to_code = "";
            _bill_to_code = "";
            _item_code = "";
            _warehouse_code = "";
            _stock_location = "";
            _price_group = "";
            _price = 0.0M;
            _orig_price = 0.0M;
            _retail_price = 0.0M;
            _price_approval = "";
            _tax_rate = 0.0M;
            _tax_amount = 0.0M;
            _net_amount = 0.0M;
            _item_cost = 0.0M;
            _gross_margin = 0.0M;
            _new_date = DateTime.Now;
            _bko_date = DateTime.Now;
            _ord_date = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
            _pic_date = DateTime.Now;
            _shp_date = Convert.ToDateTime("01/01/1900");
            _inv_date = DateTime.Now;
            _pst_date = DateTime.Now;
            _can_date = DateTime.Now;
            _proj_ship_date = DateTime.Now;
            _request_date = DateTime.Now;
            _reject_date = DateTime.Now;
            _actual_ship_date = DateTime.Now;
            _fact_sched_date = DateTime.Now;
            _ship_via_cd = "";
            _mtaxg_code = "";
            _po_doc_no = 0;
          
            _cust_code = string.Empty;
            _order_no = string.Empty;
            _reprint = string.Empty;
            _lo_stage = string.Empty;
            _hi_stage = string.Empty;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
            _like_type = string.Empty;
            _order_type = string.Empty;
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
        public int ship_no
        {
            get { return _ship_no; }
            set { _ship_no = value; }
        }
        public int kit_group
        {
            get { return _kit_group; }
            set { _kit_group = value; }
        }
        public string stage
        {
            get { return _stage; }
            set { _stage = value; }
        }
        public decimal ship_qty
        {
            get { return _ship_qty; }
            set { _ship_qty = value; }
        }
        public string sell_unit
        {
            get { return _sell_unit; }
            set { _sell_unit = value; }
        }
        public decimal ship_weight
        {
            get { return _ship_weight; }
            set { _ship_weight = value; }
        }
        public decimal commit_qty
        {
            get { return _commit_qty; }
            set { _commit_qty = value; }
        }
        public int pic_ticket_no
        {
            get { return _pic_ticket_no; }
            set { _pic_ticket_no = value; }
        }
        public string mfs_printed
        {
            get { return _mfs_printed; }
            set { _mfs_printed = value; }
        }
        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public string sell_to_code
        {
            get { return _sell_to_code; }
            set { _sell_to_code = value; }
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
        public string stock_location
        {
            get { return _stock_location; }
            set { _stock_location = value; }
        }
        public string price_group
        {
            get { return _price_group; }
            set { _price_group = value; }
        }
        public decimal price
        {
            get { return _price; }
            set { _price = value; }
        }
        public decimal orig_price
        {
            get { return _orig_price; }
            set { _orig_price = value; }
        }
        public decimal retail_price
        {
            get { return _retail_price; }
            set { _retail_price = value; }
        }
        public string price_approval
        {
            get { return _price_approval; }
            set { _price_approval = value; }
        }
        public decimal tax_rate
        {
            get { return _tax_rate; }
            set { _tax_rate = value; }
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
        public decimal item_cost
        {
            get { return _item_cost; }
            set { _item_cost = value; }
        }
        public decimal gross_margin
        {
            get { return _gross_margin; }
            set { _gross_margin = value; }
        }
        public DateTime new_date
        {
            get { return _new_date; }
            set { _new_date = value; }
        }
        public DateTime bko_date
        {
            get { return _bko_date; }
            set { _bko_date = value; }
        }
        public DateTime ord_date
        {
            get { return _ord_date; }
            set { _ord_date = value; }
        }
        public DateTime pic_date
        {
            get { return _pic_date; }
            set { _pic_date = value; }
        }
        public DateTime shp_date
        {
            get { return _shp_date; }
            set { _shp_date = value; }
        }
        public DateTime inv_date
        {
            get { return _inv_date; }
            set { _inv_date = value; }
        }
        public DateTime pst_date
        {
            get { return _pst_date; }
            set { _pst_date = value; }
        }
        public DateTime can_date
        {
            get { return _can_date; }
            set { _can_date = value; }
        }
        public DateTime proj_ship_date
        {
            get { return _proj_ship_date; }
            set { _proj_ship_date = value; }
        }
        public DateTime request_date
        {
            get { return _request_date; }
            set { _request_date = value; }
        }
        public DateTime reject_date
        {
            get { return _reject_date; }
            set { _reject_date = value; }
        }
        public DateTime actual_ship_date
        {
            get { return _actual_ship_date; }
            set { _actual_ship_date = value; }
        }
        public DateTime fact_sched_date
        {
            get { return _fact_sched_date; }
            set { _fact_sched_date = value; }
        }
        public string ship_via_cd
        {
            get { return _ship_via_cd; }
            set { _ship_via_cd = value; }
        }
        public string mtaxg_code
        {
            get { return _mtaxg_code; }
            set { _mtaxg_code = value; }
        }
        public int po_doc_no
        {
            get { return _po_doc_no; }
            set { _po_doc_no = value; }
        }
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public string order_no
        {
            get { return _order_no; }
            set { _order_no = value; }
        }
        public string reprint
        {
            get { return _reprint; }
            set { _reprint = value; }
        }

        public string lo_stage
        {
            get { return _lo_stage; }
            set { _lo_stage = value; }
        }
        public string hi_stage
        {
            get { return _hi_stage; }
            set { _hi_stage = value; }
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
        public string like_type
        {
            get { return _like_type; }
            set { _like_type = value; }
        }

        public string order_type
        {
            get { return _order_type; }
            set { _order_type = value; }
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
            get { return "uspdocstocntrcget"; }
        }

        public override string TABLE_NAME
        {
            get { return "stoshipd"; }
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

        //Added By Rahul Jain on 08-06-2009 Using in Post Receipts reports 
        public string Get_cur_hilo
        {
            get { return "uspcur_hilo"; }
        }
        public string Update_u_shipd3
        {
            get { return "uspu_shipd3"; }
        }
        //Added by Sunil Pahwa For Picking Documents
        public string GET_PICKING_DOCUMENT_STOSHIPD
        {
            get { return "usppdocstoshipdget"; }
        }
        public string UPDATE_PICKING_DOCUMENT_STOSHIPD
        {
            get { return "uspdocstoshipdUpd"; }
        }

        //*****************************************
        //Added by Sunil Pahwa for Order Entry Edit List
        public string GET_ORDER_ENTRY_EDIT_LIST_INFO
        {
            get { return "uspordrentyedtget1"; }
        }

        public string UPD_ORDER_ENTRY_EDIT_LIST_INFO
        {
            get { return "uspordentryupd"; }
        }
        public string UPD_ORDER_ENTRY_MARGIN
        {
            get { return "uspordrenstphidupd"; }
        }
        
        //*******************************************
        //Added By Rahul jain using in print packing slip report
        public string UPDATE_PACK_SLIP_STOSHIPD
        {
            get { return "usppackslpupd"; }
        }
        public string GET_COUNT
        { get { return "uspstoshipdcount"; } }

        //Added by Sunil Pahwa for getting Picking Document Info
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordrd.alias_code, stoordrd.desc1, stoordrd.desc2, ");
            sql.Append(" stoordrd.interchanged, stoordrd.item_code, stoordrd.kit_group,");
            sql.Append(" stoordrd.ordr_qty, stoordrd.sell_unit, stoordrd.serialized,");
            sql.Append(" stoordre.address1, stoordre.address2, stoordre.bus_name,");
            sql.Append(" stoordre.city, stoordre.contact, stoordre.country, stoordre.doc_no,");
            sql.Append(" stoordre.fob_point, stoordre.l_mod_time, stoordre.order_date,");
            sql.Append(" stoordre.order_no, stoordre.order_status, stoordre.pic_ticket_no,");
            sql.Append(" stoordre.po_no, stoordre.ship_via, stoordre.sls_psn_code,");
            sql.Append(" stoordre.staging_area, stoordre.state, stoordre.terms_code,");
            sql.Append(" stoordre.zip, stoshipd.line_no, stoshipd.pic_ticket_no d_pic_ticket_no,");
            sql.Append(" stoshipd.sell_to_code, stoshipd.ship_no, stoshipd.ship_qty,");
            sql.Append(" stoshipd.ship_to_code, stoshipd.stock_location, stoshipd.warehouse_code,");
            sql.Append(" stoordrd.rowid as detail_row,stoordre.cust_code,stiwhser.description from stoordre, stoordrd, stoshipd ,outer(stiwhser)");
            sql.Append(" where  stoshipd.stage = 'ORD' and stoordre.order_status not in ('CRH','HLD')");
            sql.Append(" stoordre.doc_no = stoordrd.doc_no and stoordrd.doc_no = stoshipd.doc_no and");
            sql.Append(" stoordrd.line_no = stoshipd.line_no and stoshipd.warehouse_code=stiwhser.whse_code");
         
            
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoordre.cust_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

             if (!parameters[1].ToString().Contains("1900"))
                sql.Append(" AND  stoordre.order_date=" + "'" + parameters[1].ToString().Replace("'", "''") + "'");

             if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoordre.order_no) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
           

              if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND stoordre.doc_no = " + parameters[3].ToString());
            //if (parameters[0].ToString() != string.Empty && Convert.ToDateTime(parameters[0].ToString()) != Convert.ToDateTime("01/01/1900"))
                //if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//
                //sql.Append(" and stoordre.order_date >='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

          
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoshipd.warehouse_code) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

              if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stoordre.pic_ticket_no = " + parameters[5].ToString());

            sql.Append(" order by stoshipd.warehouse_code, stoordre.doc_no, stoshipd.sell_to_code, stoshipd.ship_to_code,");
            sql.Append(" stoshipd.pic_ticket_no, stoshipd.stock_location, stoshipd.line_no, stoshipd.ship_no ");

            return sql.ToString();
        }

        //Added by Sunil Pahwa for getting OrderOpenItem Summary on [29/07/2009]
        public string FIND_ORDER_OPEN_ITEM_SUMMARY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordrd.desc1, stoordrd.desc2, stoordre.currency_code,  ");
            sql.Append(" stoordre.currency_rate, stoordre.like_type, stoshipd.bko_date, ");
            sql.Append(" stoshipd.can_date, stoshipd.inv_date, stoshipd.item_code, ");
            sql.Append(" stoshipd.net_amount, stoshipd.new_date, stoshipd.ord_date, ");
            sql.Append(" stoshipd.pic_date, stoshipd.pst_date, stoshipd.ship_qty, ");
            sql.Append(" stoshipd.shp_date, stoshipd.stage,stoshipd.warehouse_code,stoordre.order_type");
            sql.Append(" from stoshipd, stoordre, stoordrd where  stoshipd.doc_no = stoordre.doc_no ");
            sql.Append(" and stoshipd.doc_no = stoordrd.doc_no and stoshipd.line_no = stoordrd.line_no  ");
            sql.Append(" and stoshipd.stage not in ('PST','CAN') ");
         
          
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoshipd.item_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoshipd.warehouse_code) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoordre.like_type) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.lo_stage= '" + parameters[3].ToString().Trim()+"'");


     
            sql.Append(" order by  stoordre.like_type, stoshipd.item_code, stoshipd.warehouse_code, stoshipd.stage ");
          

            return sql.ToString();
        }

        //Added by Sunil Pahwa for getting OrderOpenItem DETAIL on [01/08/2009]
        public string FIND_ORDER_OPEN_ITEM_DETAIL(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stoordrd.desc1, stoordrd.desc2, stoordre.currency_code,   ");
            sql.Append(" stoordre.currency_rate, stoordre.like_type, stoordre.order_date, ");
            sql.Append(" stoordre.order_no, stoordre.order_type, stoshipd.bko_date, ");
            sql.Append(" stoshipd.can_date, stoshipd.inv_date, stoshipd.item_code, ");
            sql.Append(" stoshipd.net_amount, stoshipd.new_date, stoshipd.ord_date, ");
            sql.Append(" stoshipd.pic_date, stoshipd.pst_date, stoshipd.sell_to_code, ");
            sql.Append(" stoshipd.ship_qty, stoshipd.ship_to_code, stoshipd.shp_date,  ");
            sql.Append(" stoshipd.stage, stoshipd.warehouse_code from stoordre, stoordrd, stoshipd  ");
            sql.Append(" Where  stoshipd.doc_no = stoordre.doc_no and stoshipd.doc_no = stoordrd.doc_no ");
            sql.Append(" and stoshipd.line_no = stoordrd.line_no and stoshipd.stage not in ('PST','CAN') ");
          

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoshipd.item_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoshipd.warehouse_code) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stoordre.like_type) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.lo_stage= '" + parameters[3].ToString().Trim() + "'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stoordrd.hi_stage= '" + parameters[3].ToString().Trim() + "'");


             if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stoordre.order_type= '" + parameters[5].ToString().Trim() + "'");

            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND stoordre.cust_code= '" + parameters[6].ToString().Trim() + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND stoordre.like_type= '" + parameters[7].ToString().Trim() + "'");



               if (parameters[8].ToString() != string.Empty && Convert.ToDateTime(parameters[8].ToString()) != Convert.ToDateTime("01/01/1900"))
               // if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//
                sql.Append(" and stoordre.order_date ='" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            
            

            sql.Append(" order by stoordre.like_type, stoshipd.item_code, stoordre.order_date ");


            return sql.ToString();
        }       
       

        #endregion Stored-Procedures

    }
}

               