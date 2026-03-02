using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 12-05-2008 table used stiinvtr for purchasing module
    public class DVOItemCatalogstiinvtr : DVOBase
    {
        private int _rowid;
        private string _item_code;
        private string _item_type;
        private string _item_class;
        private string _price_group;
        private string _desc1;
        private string _desc2;
        private decimal _weight;
        private string _weight_unit;
        private decimal _volume;
        private int _inv_acct_no;
        private int _cog_acct_no;
        private int _sales_acct_no;
        private string _sell_unit;
        private string _bill_unit;
        private string _stock_unit;
        private string _purch_unit;
        private decimal _sell_factor;
        private decimal _bill_factor;
        private decimal _purch_factor;
        private string _serialized;
        private string _market_price;
        private string _commodity_code;


        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        private int _acct_no;
        private string _accounttype;
        private string _acct_desc;
        private string _keyvalue;

        //Added by Sarvjeet On 15/06/2009
        private string _warehouse_code;
        private string _stock_location;

        //Added By Rajeev
        private string _FromDate;
        private string _ToDate;

        #region Constructor

        public DVOItemCatalogstiinvtr()
        {
            _rowid = 0;
            _item_code = "";
            _item_type = "";
            _item_class = "";
            _price_group = "";
            _desc1 = "";
            _desc2 = "";
            _weight = 0;
            _weight_unit = "";
            _volume = 0;
            _inv_acct_no = 0;
            _cog_acct_no = 0;
            _sales_acct_no = 0;
            _sell_unit = "";
            _bill_unit = "";
            _stock_unit = "";
            _purch_unit = "";
            _sell_factor = 0;
            _bill_factor = 0;
            _purch_factor = 0;
            _serialized = "";
            _market_price = "";
            _commodity_code = "";

            _InsertMachineInfo = "App";
            _InsertDate = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
            _UpdateBy = -1;

            _acct_no = 0;
            _accounttype = string.Empty;
            _acct_desc = string.Empty;
            _keyvalue = string.Empty;
            _warehouse_code = string.Empty;
            _stock_location = string.Empty;
            _FromDate = string.Empty;
            _ToDate = string.Empty;

        }
        #endregion Constructor

        #region Property

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public string item_type
        {
            get { return _item_type; }
            set { _item_type = value; }
        }
        public string item_class
        {
            get { return _item_class; }
            set { _item_class = value; }
        }
        public string price_group
        {
            get { return _price_group; }
            set { _price_group = value; }
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
        public decimal weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public string weight_unit
        {
            get { return _weight_unit; }
            set { _weight_unit = value; }
        }
        public decimal volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        public int inv_acct_no
        {
            get { return _inv_acct_no; }
            set { _inv_acct_no = value; }
        }
        public int cog_acct_no
        {
            get { return _cog_acct_no; }
            set { _cog_acct_no = value; }
        }
        public int sales_acct_no
        {
            get { return _sales_acct_no; }
            set { _sales_acct_no = value; }
        }
        public string sell_unit
        {
            get { return _sell_unit; }
            set { _sell_unit = value; }
        }
        public string bill_unit
        {
            get { return _bill_unit; }
            set { _bill_unit = value; }
        }
        public string stock_unit
        {
            get { return _stock_unit; }
            set { _stock_unit = value; }
        }
        public string purch_unit
        {
            get { return _purch_unit; }
            set { _purch_unit = value; }
        }
        public decimal sell_factor
        {
            get { return _sell_factor; }
            set { _sell_factor = value; }
        }
        public decimal bill_factor
        {
            get { return _bill_factor; }
            set { _bill_factor = value; }
        }
        public decimal purch_factor
        {
            get { return _purch_factor; }
            set { _purch_factor = value; }
        }
        public string serialized
        {
            get { return _serialized; }
            set { _serialized = value; }
        }
        public string market_price
        {
            get { return _market_price; }
            set { _market_price = value; }
        }
        public string commodity_code
        {
            get { return _commodity_code; }
            set { _commodity_code = value; }
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

        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string accounttype
        {
            get { return _accounttype; }
            set { _accounttype = value; }
        }
        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string stock_location
        {
            get { return _stock_location; }
            set { _stock_location = value; }
        }
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion Property

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspinvitemins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvitemupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvitemdel"; }
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
            get { return "stiinvtr"; }
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
        //Using in report Print Item Catalog
        public string ITEM_CATALOG_GET
        {
            get { return "uspitemcatalogget"; }
        }
        //Added By Rahul Jain 04-06-2009 using in Post receipt reports
        public string GET_c_purch
        {
            get { return "uspc_purch"; }
        }
        public string GET_sell_factor
        {
            get { return "uspsell_factor"; }
        }
        public string GET_qty_on_hand
        {
            get { return "uspqty_on_hand"; }
        }
        public string GET_commit_qty
        {
            get { return "uspcommit_qty"; }
        }
        public string GET_trancommit_qty
        {
            get { return "usptrancommit_qty"; }
        }
        public string GET_ship_qty
        {
            get { return "uspship_qty"; }
        }
        public string GET_ordr_qty
        {
            get { return "uspordr_qty"; }
        }
        public string GET_exp_rec_qty
        {
            get { return "usexp_rec_qty"; }
        }
        //Added By Rahul jain on 22/06/09 Using in Posting Inventory Received reports
        public string GET_uspc_invt
        {
            get { return "uspc_invt"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid,item_code,item_type,item_class,price_group,desc1,desc2,weight,weight_unit,");
            sql.Append(" volume,inv_acct_no,cog_acct_no,sales_acct_no,sell_unit,bill_unit,stock_unit,purch_unit,");
            sql.Append(" sell_factor,bill_factor,purch_factor,serialized,market_price,commodity_code");
            sql.Append(" FROM  stiinvtr where 1=1");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(item_code)  ='" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(item_type) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(item_class) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(price_group) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND desc1 LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND desc2 LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToDecimal(parameters[6]) != 0)
                sql.Append(" AND weight = " + parameters[6].ToString().Trim());
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(weight_unit) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[8]) > 0)
                sql.Append(" AND volume = " + parameters[8]);
            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND inv_acct_no = " + parameters[9]);
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND cog_acct_no = " + parameters[10]);
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND sales_acct_no = " + parameters[11]);
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sell_unit) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bill_unit) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[14] != null)
                if (parameters[14].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stock_unit) = '" + parameters[14].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[15] != null)
                if (parameters[15].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(purch_unit) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[16]) > 0)
                sql.Append(" AND Rtrim(sell_factor) = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[17]) > 0)
                sql.Append(" AND Rtrim(bill_factor) = '" + parameters[17].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[18]) > 0)
                sql.Append(" AND Rtrim(purch_factor) = '" + parameters[18].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[19] != null)
                if (parameters[19].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(serialized) = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[20] != null)
                if (parameters[20].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(market_price) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[21] != null)
                if (parameters[21].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(commodity_code) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[22]) > 0)
                sql.Append(" AND rowid =" + parameters[22]);
            sql.Append(" order by item_code");
            return sql.ToString();
        }
        public string FIND_QUERY1(ref Object[] parameters)
       {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stiinvtr.item_code,stiinvtr.commodity_code, ");
            sql.Append(" stiinvtr.desc1,stiinvtr.desc2,stiinvtr.purch_unit,  ");
            sql.Append(" stiinvtr.weight,stiinvtr.weight_unit,stiinvtr.item_type, ");
            sql.Append(" stiinvtr.inv_acct_no,PayrollGLAccounts.acct_no,PayrollGLAccounts.acct_type,  ");
            sql.Append("  PayrollGLAccounts.acct_desc,PayrollGLAccounts.keyvalue,stiinvtr.rowid,stiinvtr.purch_factor from stiinvtr ,outer PayrollGLAccounts");
            sql.Append(" where stiinvtr.item_type='N' and stiinvtr.inv_acct_no=PayrollGLAccounts.acct_no");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND item_code='" + parameters[0].ToString().Trim()+"'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(desc1) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(desc2) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToDecimal(parameters[4]) != 0)
                sql.Append(" and weight=" + parameters[4].ToString().Trim());

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND weight_unit LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

         

            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND purch_unit LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");


            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND commodity_code = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");

            if (Convert.ToInt32(parameters[8]) != 0)
                sql.Append(" and stiinvtr.rowid=" + parameters[8].ToString().Trim());
            sql.Append(" order by stiinvtr.rowid,item_code");



            return sql.ToString();
        }

        //Find Query Modified By Rajeev
        //Modified Date 18:06:2009
        //Purpose : To Get the Data For Report : frmPriceCostList
        //Only i have added some selection field in last 

        public string FINDQUERY_ITMSTARPT(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.item_code, stiinvtr.stock_unit, stiinvtr.desc1,  stiinvtr.desc2, stiinvtr.sell_factor,");
            sql.Append(" stilocar.warehouse_code,stilocar.qty_on_hand,stilocar.price,stilocar.purch_unit_cost,stilocar.stock_location,");
            sql.Append(" stiinvtr.item_class,stiinvtr.item_type");
            sql.Append(" from stiinvtr, stilocar");
            sql.Append(" where stiinvtr.item_code = stilocar.item_code");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            sql.Append(" ORDER BY stiinvtr.item_code, stilocar.warehouse_code");
            return sql.ToString();
        }

        /// <summary>
        ///  Find Query to get the data for report :Reorder Advice
        /// Created By : Rajeev
        /// Created Date : 19/06/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_REORDERADVICE(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.desc1,stiinvtr.item_class,stiinvtr.item_type, stilocar.item_code,");
            sql.Append(" stilocar.qty_on_hand,stilocar.qty_reorder,stilocar.reorder_point,stilocar.stock_location,stilocar.warehouse_code");
            sql.Append(" from stilocar, stiinvtr ");
            sql.Append(" where stilocar.item_code = stiinvtr.item_code and stilocar.qty_on_hand < stilocar.reorder_point ");
            
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stilocar.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            sql.Append(" ORDER BY stilocar.item_code, stilocar.warehouse_code");
            return sql.ToString();
        }


        /// <summary>
        /// Find Query to get the data for report :Reorder Advice
        /// Created By : Rajeev
        /// Created Date : 20/06/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_SALE_PURCHASE_HISTORY(ref object[] parameters)
        {

      //     select stiactvd.cost,stiactvd.item_code,stiactvd.price,stiactvd.qty,
      //      stiactvd.warehouse_code,stitranr.doc_type,stxtranr.doc_date
      //     From stxtranr, stitranr, stiactvd,stiinvtr, stilocar
      //   Where stxtranr.orig_journal = stitranr.orig_journal and
      //stiactvd.orig_journal = stitranr.orig_journal and
      //stxtranr.doc_no = stitranr.doc_no and
      //stiactvd.doc_no = stitranr.doc_no and
      //stiinvtr.item_code = stiactvd.item_code and
      //stilocar.item_code = stiactvd.item_code and
      //stilocar.warehouse_code = stiactvd.warehouse_code and
      //stitranr.doc_type = 'SH' or stitranr.doc_type = 'PU' and

      //    stxtranr.doc_date between '20/06/2008' and '20/06/2009'
      //        order by stiactvd.item_code, stiactvd.warehouse_code

            StringBuilder sql = new StringBuilder();
            sql.Append("select stiactvd.cost,stiactvd.item_code,stiactvd.price,stiactvd.qty,");
            sql.Append(" stiactvd.warehouse_code,stitranr.doc_type,stxtranr.doc_date");
            sql.Append(" From stxtranr, stitranr, stiactvd,stiinvtr, stilocar ");
            sql.Append(" Where stxtranr.orig_journal = stitranr.orig_journal and stiactvd.orig_journal = stitranr.orig_journal and");
            sql.Append(" stxtranr.doc_no = stitranr.doc_no and stiactvd.doc_no = stitranr.doc_no and ");
            
            sql.Append(" stiinvtr.item_code = stiactvd.item_code and stilocar.item_code = stiactvd.item_code and");
            sql.Append(" stilocar.warehouse_code = stiactvd.warehouse_code and stitranr.doc_type = 'SH' or stitranr.doc_type = 'PU'");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stilocar.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null && parameters[7] != null)
                if (parameters[6].ToString() != string.Empty && parameters[7].ToString() != string.Empty)
                    sql.Append(" AND stxtranr.doc_date between '" + parameters[6].ToString() + "' and '" + parameters[7].ToString()+ "'");

            sql.Append(" ORDER BY stiactvd.item_code, stiactvd.warehouse_code");
            return sql.ToString();
        }

        public string FIND_ICJOURNAL_DETAILS
        {
            get { return "uspicjrnlget"; }
        }

        /// <summary>
        ///  Find Query to get the data for report :Print Cost Evaluation
        /// Created By : Rajeev
        /// Created Date : 25/06/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_COSTVALUATION(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.item_code,stiinvtr.serialized,stilocar.avg_unit_cost,");
            sql.Append(" stilocar.line_no,stilocar.purch_unit_cost,stilocar.qty_on_hand,stilocar.warehouse_code");
            sql.Append(" from stilocar, stiinvtr ");
            sql.Append(" where stilocar.item_code = stiinvtr.item_code");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stilocar.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            sql.Append(" ORDER BY stiinvtr.item_code, stilocar.warehouse_code");
            return sql.ToString();
        }

        public string GET_COSTMETHOD
        {
            get { return "uspcostmthdget"; }
        }

        public string GET_TOTALCOST
        {
            get { return "usptotalget"; }
        }


        /// <summary>
        ///  Find Query to get the data for report :Print LIFO/FIFO Cost
        /// Created By : Rajeev
        /// Created Date : 29/06/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_LIFOFIFOCOST(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select sticstvr.cost,sticstvr.hierarchy_no,sticstvr.item_code,sticstvr.quantity,");
            sql.Append(" sticstvr.warehouse_code ");
            sql.Append(" from sticstvr, stilocar, stiinvtr");
            sql.Append(" where sticstvr.item_code = stilocar.item_code and sticstvr.warehouse_code = stilocar.warehouse_code  and");
            sql.Append(" sticstvr.item_code = stiinvtr.item_code ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stilocar.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            sql.Append(" ORDER BY sticstvr.item_code, sticstvr.warehouse_code,sticstvr.hierarchy_no");
            return sql.ToString();
        }


        public string FINDQUERY_AVERAGECOST(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select stilocar.avg_unit_cost,stilocar.item_code,stilocar.qty_on_hand,stilocar.warehouse_code");
            sql.Append(" from stilocar, stiinvtr ");
            sql.Append(" Where stilocar.item_code = stiinvtr.item_code");
            
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stilocar.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            sql.Append(" ORDER BY stilocar.item_code, stilocar.warehouse_code");
            return sql.ToString();
        }


        /// <summary>
        ///  Find Query to get the data for report :Stock Status
        /// Created By : Rajeev
        /// Created Date : 01/07/09
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_STOCKSTATUS(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.desc1,stiinvtr.item_class,stiinvtr.item_type,stilocar.item_code,");
            sql.Append("stilocar.qty_on_hand,stilocar.stock_location,stilocar.warehouse_code ");
            sql.Append(" from stilocar, stiinvtr ");
            sql.Append(" where stilocar.item_code = stiinvtr.item_code ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stilocar.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_type LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.desc1 LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.warehouse_code LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.stock_location LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");

            sql.Append(" ORDER BY stilocar.item_code");
            return sql.ToString();
        }

        public string FINDQUERY_ITEM_CATALOG_INFO(ref object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append(" Select stiinvtr.desc1,stiinvtr.desc2,stiinvtr.item_code,stiinvtr.item_class,");
            sql.Append("stiinvtr.item_type,stpvendr.bus_name,stuctlgd.cost,stuctlgd.vend_item_code, ");
            sql.Append("stuctlgd.vendor_code,stpvendr.contact,stpvendr.phone");
            sql.Append(" From stiinvtr,outer(stuctlgd,stpvendr)");
            sql.Append(" where stiinvtr.item_code=stuctlgd.item_code ");
            sql.Append(" AND stuctlgd.vendor_code=stpvendr.vend_code ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_code  LIKE '" + parameters[0].ToString().Replace("'", "''") + "%'");
         
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stiinvtr.item_class LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");


            sql.Append(" ORDER BY stiinvtr.item_code,stuctlgd.vendor_code");
            return sql.ToString();
        }
        #endregion Stored-Procedures

    }
}
