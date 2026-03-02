using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 24/06/09 using in Inventory Posting
    //Table used :stistatd 
    public class DVOInventorystistatd : DVOBase
    {
        private int _rowid;
        private string _item_code;
        private string _warehouse_code;
        private int _period;
        private int _yr;
        private decimal _cost;
        private decimal _sale;
        private decimal _cqty;
        private decimal _sqty;
        private decimal _usage_rate;
        private int _day_ostk;
        private int _num_stk_outs;
        private decimal _dup_sqty;
        private decimal _recurr_usage;
        private decimal _dup_recurr_usage;

        #region Constructor
        public DVOInventorystistatd()
        {
            _rowid = 0;
            _item_code = "";
            _warehouse_code = "";
            _period = 0;
            _yr = 0;
            _cost = 0.0M;
            _sale = 0.0M;
            _cqty = 0.0M;
            _sqty = 0.0M;
            _usage_rate = 0.0M;
            _day_ostk = 0;
            _num_stk_outs = 0;
            _dup_recurr_usage = 0.0M;
            _dup_sqty=0.0M;
            _recurr_usage=0.0M;
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
        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public int period
        {
            get { return _period; }
            set { _period = value; }
        }
        public int yr
        {
            get { return _yr; }
            set { _yr = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public decimal sale
        {
            get { return _sale; }
            set { _sale = value; }
        }
        public decimal sqty
        {
            get { return _sqty; }
            set { _sqty = value; }
        }
        public decimal cqty
        {
            get { return _cqty; }
            set { _cqty = value; }
        }
        public decimal usage_rate
        {
            get { return _usage_rate; }
            set { _usage_rate = value; }
        }
        public int day_ostk
        {
            get { return _day_ostk; }
            set { _day_ostk = value; }
        }
        public int num_stk_outs
        {
            get { return _num_stk_outs; }
            set { _num_stk_outs = value; }
        }
        public decimal dup_recurr_usage
        {
            get { return _dup_recurr_usage; }
            set { _dup_recurr_usage = value; }
        }
        public decimal dup_sqty
        {
            get { return _dup_sqty; }
            set { _dup_sqty = value; }
        }
        public decimal recurr_usage
        {
            get { return _recurr_usage; }
            set { _recurr_usage = value; }
        }
        #endregion Property

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
        public string POSTGL
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
            get { return "stistatd"; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string Updateu_stat
        {
            get { return "uspu_stat"; }
        }
        public string Inserti_stat
        {
            get { return "uspi_stat"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
