using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOInventoryItemsStilocar : DVOBase
    {
        int _RowId;
        string _item_code;
        string _warehouse_code;
        int _line_no;
        string _count_cycle;
        string _purchase_date;//date
        string _count_date;//date
        string _sold_date;//date
        string _obsolete;
        string _inactive_date;//date
        string _lst_act_date;//date
        string _loc_aisle;
        string _loc_row;
        string _loc_bin;
        string _stock_location;
        decimal _avg_unit_cost;
        decimal _purch_unit_cost;
        decimal _last_cost;
        string _comm_code;
        decimal _price;
        string _allow_bo;
        string _taxable;
        string _terms_disc;
        string _trade_disc;
        string _vend_code;
        string _vend_prod_no;
        string _abc_code;
        decimal _reorder_point;
        decimal _qty_reorder;
        decimal _safety_stock;
        decimal _safety_factor;
        decimal _qty_on_hand;
        decimal _last_qty;
        string _stk_out_date;//date
        string _seasonal;
        decimal _avg_ld_tm;
        int _lst_ld_tm;
        int _pri_ld_tm;
        string _freez_flag;
        string _freez_date;//date
        string _freez_expir;//date
        decimal _min_sell_qty;
        decimal _usage_rate;
        int _insertby;
        string _insertdate;//date
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;//date
        string _updatemachineinfo;

        string _item_type;
        string _item_class;
        string _desc1;
        string _desc2;
        private string _count_type;
       
        string _stock_unit;
        string _purch_unit;
        decimal _purch_factor;

        string _bill_unit;
        decimal  _bill_factor;
        string _sell_unit;
        decimal _sell_factor;
        string _bus_name;


        #region Constructor

        public DVOInventoryItemsStilocar()
        {
            _RowId = 0;
            _item_code = string.Empty;
            _warehouse_code = string.Empty;
            _line_no = 0;
            _count_cycle = string.Empty;
            _purchase_date = "01/01/1900";//date
            _count_date = "01/01/1900";//date
            _sold_date = "01/01/1900";//date
            _obsolete = string.Empty;
            _inactive_date = "01/01/1900";//date
            _lst_act_date = "01/01/1900";//date
            _loc_aisle = string.Empty;
            _loc_row = string.Empty;
            _loc_bin = string.Empty;
            _stock_location = string.Empty;
            _avg_unit_cost = 0;
            _purch_unit_cost = 0;
            _last_cost = 0;
            _comm_code = string.Empty;
            _price = 0;
            _allow_bo = string.Empty;
            _taxable = string.Empty;
            _terms_disc = string.Empty;
            _trade_disc = string.Empty;
            _vend_code = string.Empty;
            _vend_prod_no = string.Empty;
            _abc_code = string.Empty;
            _reorder_point = 0;
            _qty_reorder = 0;
            _safety_stock = 0;
            _safety_factor = 0;
            _qty_on_hand = 0;
            _last_qty = 0;
            _stk_out_date = "01/01/1900";//date
            _seasonal = string.Empty;
            _avg_ld_tm = 0;
            _lst_ld_tm = 0;
            _pri_ld_tm = 0;
            _freez_flag = string.Empty;
            _freez_date = "01/01/1900";//date
            _freez_expir = "01/01/1900";//date
            _min_sell_qty = 0;
            _usage_rate = 0;
            _item_type=string.Empty;
            _item_class=string.Empty;
            _desc1=string.Empty;
            _desc2=string.Empty;
            _count_type = string.Empty;
            _insertby = 0;
            _insertdate = "01/01/1900";//date
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//date
            _updatemachineinfo = string.Empty;

             _stock_unit=string.Empty;
             _purch_unit=string.Empty;
             _purch_factor=0;
              _bill_unit=string.Empty;
              _bill_factor=0;
              _sell_unit=string.Empty;
              _sell_factor=0;
              _bus_name = string.Empty;
           
        }

        #endregion Constructor

        #region public properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
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
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string count_cycle
        {
            get { return _count_cycle; }
            set { _count_cycle = value; }
        }
        public string purchase_date
        {
            get { return _purchase_date; }
            set { _purchase_date = value; }
        }
        public string count_date
        {
            get { return _count_date; }
            set { _count_date = value; }
        }
        public string sold_date
        { 
            get { return _sold_date; }
            set { _sold_date = value; }
        }
        public string obsolete
        {
            get { return _obsolete; }
            set { _obsolete = value; }
        }
        public string inactive_date
        {
            get { return _inactive_date; }
            set { _inactive_date = value; }
        }
        public string lst_act_date
        {
            get { return _lst_act_date; }
            set { _lst_act_date = value; }
        }
        public string loc_aisle
        {
            get { return _loc_aisle; }
            set { _loc_aisle = value; }
        }
        public string loc_row
        {
            get { return _loc_row; }
            set { _loc_row = value; }
        }
        public string loc_bin
        {
            get { return _loc_bin; }
            set { _loc_bin = value; }
        }
        public string stock_location
        {
            get { return _stock_location; }
            set { _stock_location = value; }
        }
        public decimal avg_unit_cost
        {
            get { return _avg_unit_cost; }
            set { _avg_unit_cost = value; }
        }
        public decimal purch_unit_cost
        {
            get { return _purch_unit_cost; }
            set { _purch_unit_cost = value; }
        }
        public decimal last_cost
        {
            get { return _last_cost; }
            set { _last_cost = value; }
        }
        public string comm_code
        {
            get { return _comm_code; }
            set { _comm_code = value; }
        }
        public decimal price
        {
            get { return _price; }
            set { _price = value; }
        }
        public string allow_bo
        {
            get { return _allow_bo; }
            set { _allow_bo = value; }
        }
        public string taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }
        public string terms_disc
        {
            get { return _terms_disc; }
            set { _terms_disc = value; }
        }
        public string trade_disc
        {
            get { return _trade_disc; }
            set { _trade_disc = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string vend_prod_no
        {
            get { return _vend_prod_no; }
            set { _vend_prod_no = value; }
        }
        public string abc_code
        {
            get { return _abc_code; }
            set { _abc_code = value; }
        }
        public decimal reorder_point
        { 
            get { return _reorder_point; }
            set { _reorder_point = value; }
        }
        public decimal qty_reorder
        {
            get { return _qty_reorder; }
            set { _qty_reorder = value; }
        }
        public decimal safety_stock
        {
            get { return _safety_stock; }
            set { _safety_stock = value; }
        }
        public decimal safety_factor
        {
            get { return _safety_factor; }
            set { _safety_factor = value; }
        }
        public decimal qty_on_hand
        {
            get { return _qty_on_hand; }
            set { _qty_on_hand = value; }
        }
        public decimal last_qty
        {
            get { return _last_qty; }
            set { _last_qty = value; }
        }
        public string stk_out_date
        {
            get { return _stk_out_date; }
            set { _stk_out_date = value; }
        }
        public string seasonal
        {
            get { return _seasonal; }
            set { _seasonal = value; }
        }
        public decimal avg_ld_tm
        {
            get { return _avg_ld_tm; }
            set { _avg_ld_tm = value; }
        }
        public int lst_ld_tm
        {
            get { return _lst_ld_tm; }
            set { _lst_ld_tm = value; }
        }
        public int pri_ld_tm
        {
            get { return _pri_ld_tm; }
            set { _pri_ld_tm = value; }
        }
        public string freez_flag
        {
            get { return _freez_flag; }
            set { _freez_flag = value; }
        }
        public string freez_date
        {
            get { return _freez_date; }
            set { _freez_date = value; }
        }
        public string freez_expir
        {
            get { return _freez_expir; }
            set { _freez_expir = value; }
        }
        public decimal min_sell_qty
        {
            get { return _min_sell_qty; }
            set { _min_sell_qty = value; }
        }
        public decimal usage_rate
        {
            get { return _usage_rate; }
            set { _usage_rate = value; }
        }


        public string item_type
        {
            get { return _item_type; }
            set {  _item_type= value; }
        }
        public string item_class
        {
            get { return _item_class; }
            set {  _item_class= value; }
        }
       public string desc1
        {
            get { return _desc1; }
            set {  _desc1= value; }
        }
       public string desc2
        {
            get { return _desc2; }
            set {  _desc2= value; }
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
        public string count_type
        {
            get { return _count_type; }
            set { _count_type = value; }
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
        public decimal  purch_factor
        {
            get { return _purch_factor; }
            set {  _purch_factor= value; }
        }
         public string bill_unit
        {
            get { return _bill_unit; }
            set { _bill_unit = value; }
        }
        public decimal  bill_factor
        {
            get { return _bill_factor; }
            set {  _bill_factor= value; }
        }
         public string sell_unit
        {
            get { return _sell_unit; }
            set { _sell_unit = value; }
        }
        public decimal  sell_factor
        {
            get { return _sell_factor; }
            set {  _sell_factor= value; }
        }
        public string bus_name
        {
            get { return _bus_name ; }
            set { _bus_name = value; }
        }
        
       
        #endregion public properties

        #region Stored-Procedures

        public string GET_ITEM_QUANTITY_OF_CPU
        {
            get { return "uspitemquantityget"; }
        }
        
        public override string INSERT_SPNAME
        {
            get { return "uspinvwrhsins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvwrhsupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspinvwrhsdel"; }
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
            get { return "stilocar"; }
        }
        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //Added By Rahul jain on 04-06-2009 using in POST RECEIPTS reprots
        public string GET_c_whse_chk
        {
            get { return "uspc_whse_chk"; }
        }
        public string GET_c_from
        {
            get { return "uspc_from"; }
        }
        public string Insert_i_locar1
        {
            get { return "uspi_locar1"; }
        }
        public string Update_u_locar1
        {
            get { return "uspu_locar1"; }
        }
        //Added By Rahul Jain on 22/06/2009 using in Post inventory received reports 
        public string GET_c_from_tr
        {
            get { return "uspc_from_tr"; }
        }
        public string GET_c_from_tr2
        {
            get { return "uspc_from_tr2"; }
        }
       //Added by Sarvjeet On 16/06/2009 using in Create Count Sheet.
        public string GET_whse_desc
        {
            get { return "uspwhse_desc"; }
        }
        public string GET_commit_qty1
        {
            get { return "uspcommit_qty1"; }
        }
        public string INSERT_STICADJE
        {
            get { return "uspsticadjeins"; }
        }
        public string INSERT_STICADJD
        {
            get { return "uspsticadjdins"; }
        }
        public string GET_COUNT_ACCT_NO
        {
            get { return "uspcount_acct_no"; }
        }
        public string GET_PRINTCOUNTSHEET
        {
            get { return "uspcountsheetget"; }
        }
        //Added By Rahul jain on 22/06/09 using inventory received posting
        public string GET_uspc_loca
        {
            get { return "uspc_loca"; }
        }
        public string Updateu_loca
        {
            get { return "uspu_loca"; }
        }
        //Added By Sunil Pahwa on 24/06/09 using INV COSTS/PRICES
        public string UPDATE_INV_COSTS
        {
            get { return "uspudpinvcosts"; }
        }
        public string UPDATE_INV_PRICES
        {
            get { return "uspudpinvprices"; }
        }
        //*******************************************************
       
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            
            sql.Append("SELECT item_code p_item_code,warehouse_code p_warehouse_code,line_no p_line_no,");
            sql.Append(" count_cycle p_count_cycle,purchase_date p_purchase_date,count_date p_count_date,");
            sql.Append(" sold_date p_sold_date,obsolete p_obsolete,inactive_date p_inactive_date,");
            sql.Append(" lst_act_date p_lst_act_date,loc_aisle p_loc_aisle,loc_row p_loc_row,");
            sql.Append(" loc_bin p_loc_bin,stock_location p_stock_location,avg_unit_cost p_avg_unit_cost,");
            sql.Append(" purch_unit_cost p_purch_unit_cost,last_cost p_last_cost,comm_code p_comm_code,");
            sql.Append(" price p_price,allow_bo p_allow_bo,taxable p_taxable,terms_disc p_terms_disc,");
            sql.Append(" trade_disc p_trade_disc,vend_code p_vend_code,vend_prod_no p_vend_prod_no,");
            sql.Append(" abc_code p_abc_code,reorder_point p_reorder_point,qty_reorder p_qty_reorder,");
            sql.Append(" safety_stock p_safety_stock,safety_factor p_safety_factor,qty_on_hand p_qty_on_hand,");
            sql.Append(" last_qty p_last_qty,stk_out_date p_stk_out_date,seasonal p_seasonal,");
            sql.Append(" avg_ld_tm p_avg_ld_tm,lst_ld_tm p_lst_ld_tm,pri_ld_tm p_pri_ld_tm,");
            sql.Append(" freez_flag p_freez_flag,freez_date p_freez_date,freez_expir p_freez_expir,");
            sql.Append(" min_sell_qty p_min_sell_qty,usage_rate p_usage_rate,rowid p_rowid");
            sql.Append(" from stilocar WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND rowid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(item_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(warehouse_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            
            return sql.ToString();
        }

         //if (parameters[4].ToString().Trim() != string.Empty)
         //           sql.Append(" AND Rtrim(i1.pay_to_code) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
         //   if (parameters[5] != null)
         //       if (parameters[5].ToString().Trim() != string.Empty)
         //           sql.Append(" AND Rtrim(i1.description) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");


        public  string FIND_INVENTORY_INFORMATION(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append(" select stiinvtr.item_code,stiinvtr.item_type,stilocar.line_no,");
            sql.Append(" stiinvtr.desc1,  stiinvtr.desc2,  stiinvtr.item_class,");
            sql.Append(" stilocar.price,stilocar.purch_unit_cost,stilocar.qty_on_hand, ");
            sql.Append(" stilocar.stock_location,stilocar.vend_code,stilocar.warehouse_code ");
            sql.Append(" from stiinvtr , stilocar ");
            sql.Append(" where stiinvtr.item_code = stilocar.item_code");

            
            if (parameters[0].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.item_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[1].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.item_type = '" + parameters[1].ToString().Trim()+"'");

            if (parameters[2].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.desc1 = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[3].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.desc2 = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            
            if (parameters[4].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.item_class = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[5].ToString().Trim() != string.Empty)
                sql.Append(" AND stilocar.warehouse_code = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6].ToString().Trim() != string.Empty)
                sql.Append(" AND stilocar.stock_location = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" order by stiinvtr.item_code, stilocar.warehouse_code");
        
            return sql.ToString();
        }

        public string FIND_INVENTORY_DETAIL_INFORMATION(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append(" select stiinvtr.cog_acct_no,stiinvtr.commodity_code,stiinvtr.desc1, stiinvtr.desc2, ");
            sql.Append(" stiinvtr.inv_acct_no,stiinvtr.item_class,stiinvtr.item_code,stiinvtr.market_price,  ");
            sql.Append(" stiinvtr.price_group,stiinvtr.purch_factor,stiinvtr.purch_unit,stiinvtr.sales_acct_no, ");
            sql.Append(" stiinvtr.sell_factor,stiinvtr.sell_unit,stiinvtr.serialized,stiinvtr.stock_unit, ");
            sql.Append(" stiinvtr.volume,stiinvtr.weight,stiinvtr.weight_unit,stilocar.abc_code,stilocar.allow_bo, ");
            sql.Append(" stilocar.avg_ld_tm,stilocar.avg_unit_cost,stilocar.comm_code,stilocar.count_cycle,");
            sql.Append(" stilocar.count_date,stilocar.freez_date,stilocar.freez_expir,stilocar.freez_flag,");
            sql.Append(" stilocar.last_cost,stilocar.last_qty,stilocar.loc_aisle, stilocar.loc_bin, ");
            sql.Append(" stilocar.loc_row,stilocar.lst_act_date,stilocar.lst_ld_tm,stilocar.min_sell_qty, ");
            sql.Append(" stilocar.obsolete,stilocar.pri_ld_tm,stilocar.price,stilocar.purch_unit_cost, ");
            sql.Append(" stilocar.purchase_date,stilocar.qty_on_hand,stilocar.qty_reorder,stilocar.reorder_point, ");
            sql.Append(" stilocar.safety_factor,stilocar.safety_stock,stilocar.seasonal,stilocar.sold_date,");

            sql.Append(" stilocar.stk_out_date,stilocar.taxable,stilocar.terms_disc,stilocar.trade_disc,");
            sql.Append(" stilocar.vend_code,stilocar.vend_prod_no,stilocar.warehouse_code,stistatd.cost, ");
            sql.Append(" stistatd.cqty,stistatd.period,stistatd.sale,stistatd.sqty,stistatd.usage_rate, ");
            sql.Append(" stistatd.yr , s1.keyvalue cog_acct_kv,s1.acct_desc cog_acct_desc,");
            sql.Append(" s2.keyvalue inv_acct_kv,s2.acct_desc inv_acct_desc,s3.keyvalue sales_acct_kv,s3.acct_desc sales_acct_desc ");
            sql.Append(" from stiinvtr, stilocar,stistatd,outer PayrollGLAccounts s1,outer PayrollGLAccounts s2,outer PayrollGLAccounts s3 ");
            sql.Append(" where stiinvtr.item_code = stilocar.item_code and stilocar.item_code = stistatd.item_code and  ");
            sql.Append(" stilocar.warehouse_code = stistatd.warehouse_code ");

            sql.Append(" and stiinvtr.cog_acct_no = s1.acct_no ");
            sql.Append(" and stiinvtr.inv_acct_no = s2.acct_no ");
            sql.Append(" and stiinvtr.sales_acct_no = s3.acct_no ");
           

            if (parameters[0].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.item_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[1].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.item_type = '" + parameters[1].ToString().Trim() + "'");

            if (parameters[2].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.desc1 = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[3].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.desc2 = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[4].ToString().Trim() != string.Empty)
                sql.Append(" AND stiinvtr.item_class = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[5].ToString().Trim() != string.Empty)
                sql.Append(" AND stilocar.warehouse_code = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6].ToString().Trim() != string.Empty)
                sql.Append(" AND stilocar.stock_location = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" order by stiinvtr.item_code, stilocar.warehouse_code, stistatd.yr desc, stistatd.period desc");

            return sql.ToString();
        }
        //Added by Sarvjeet Verma On 16/16/2009
        public string FIND_CREATECOUNTSHEET(ref Object[] parameters)
        {  
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select stiinvtr.desc1, stiinvtr.item_code, stiinvtr.serialized, stiinvtr.stock_unit, ");
            sql.Append("stilocar.count_cycle, stilocar.qty_on_hand, stilocar.stock_location, stilocar.warehouse_code ");
            sql.Append(" from ");
            if (parameters[1].ToString().Trim() == string.Empty)
            {
                sql.Append(" stilocar,stiinvtr where  " );
                sql.Append(" stiinvtr.item_code = stilocar.item_code");
            }
            else
            {
                sql.Append(" stilocar,stiinvtr,sticadje where ");
                sql.Append(" stiinvtr.item_code = stilocar.item_code");
                sql.Append(" and stilocar.warehouse_code=sticadje.warehouse_code");
            }
            if (parameters[0].ToString().Trim() != string.Empty)
               sql.Append(" and stilocar.warehouse_code LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1].ToString().Trim() != string.Empty)
               sql.Append(" and sticadje.count_desc LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2].ToString().Trim() != string.Empty)
               sql.Append(" and stiinvtr.item_code LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.stock_location LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.loc_aisle LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.loc_row  LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.loc_bin LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.abc_code LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.count_cycle LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");

            return sql.ToString();
        
        }

        //Added by Sunil Pahwa On 24/06/2009
        public string FIND_INVENTORY_COSTS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select stilocar.rowid,stilocar.item_code,stiinvtr.item_class,stiinvtr.desc1, ");
            sql.Append(" stiinvtr.desc2,stiinvtr.stock_unit,stiinvtr.purch_unit,stiinvtr.purch_factor,");
            sql.Append(" stilocar.warehouse_code,stilocar.vend_code,stilocar.avg_unit_cost,");
            sql.Append(" stilocar.last_cost,stilocar.last_qty,stilocar.purchase_date,"); 
            sql.Append(" stilocar.purch_unit_cost,stilocar.price,stpvendr.bus_name ");
            sql.Append(" from stiinvtr,stilocar,outer(stpvendr) ");
            sql.Append(" where stiinvtr.item_code = stilocar.item_code ");
            sql.Append(" and stilocar.vend_code=stpvendr.vend_code ");
            

            if (parameters[0].ToString().Trim() != string.Empty)
                sql.Append(" and stiinvtr.item_code LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.warehouse_code LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.vend_code LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToDecimal(parameters[3]) != 0)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stilocar.avg_unit_cost =" + parameters[3].ToString().Trim());
            if (Convert.ToDecimal(parameters[4]) != 0)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND stilocar.last_cost =" + parameters[4].ToString().Trim());
            if (Convert.ToDecimal(parameters[5]) != 0)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND stilocar.last_qty =" + parameters[5].ToString().Trim());

            if (parameters[6].ToString() != string.Empty && Convert.ToDateTime(parameters[6].ToString()) != Convert.ToDateTime("01/01/1900"))
              sql.Append(" and stilocar.purchase_date ='" + parameters[6].ToString().Trim().Replace("'", "''") + "'");


            if (Convert.ToDecimal(parameters[7]) != 0)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND  stilocar.purch_unit_cost =" + parameters[7].ToString().Trim());

            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND stilocar.rowid =" + parameters[8]);
            return sql.ToString();

        }

        public string FIND_INVENTORY_PRICES(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select stilocar.rowid,stilocar.item_code,stiinvtr.item_class,stiinvtr.desc1, ");
            sql.Append(" stiinvtr.desc2,stiinvtr.stock_unit,stiinvtr.purch_unit,stiinvtr.purch_factor,");
            sql.Append(" stilocar.warehouse_code,stilocar.vend_code,stilocar.avg_unit_cost,");
            sql.Append(" stilocar.last_cost,stilocar.last_qty,stilocar.purchase_date, ");
            sql.Append(" stilocar.purch_unit_cost,stilocar.price, ");
            sql.Append(" stiinvtr.bill_unit,stiinvtr.bill_factor, ");
            sql.Append(" stiinvtr.sell_unit,stiinvtr.sell_factor,stilocar.sold_date,stpvendr.bus_name ");
            sql.Append(" from stiinvtr,stilocar,outer(stpvendr) ");
            sql.Append(" where stiinvtr.item_code = stilocar.item_code ");
            sql.Append(" and stilocar.vend_code=stpvendr.vend_code ");

            if (parameters[0].ToString().Trim() != string.Empty)
                sql.Append(" and stiinvtr.item_code LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.warehouse_code LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2].ToString().Trim() != string.Empty)
                sql.Append(" and stilocar.vend_code LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[3].ToString() != string.Empty && Convert.ToDateTime(parameters[3].ToString()) != Convert.ToDateTime("01/01/1900"))
                sql.Append(" and stilocar.sold_date ='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            if (Convert.ToDecimal(parameters[4]) != 0)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND stilocar.price =" + parameters[4].ToString().Trim());

            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stilocar.rowid = " + parameters[5].ToString());
            return sql.ToString();

        }


        #endregion store-procedures
    }
}
