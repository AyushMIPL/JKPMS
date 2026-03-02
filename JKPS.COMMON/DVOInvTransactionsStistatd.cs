using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOInvTransactionsStistatd : DVOBase
    {
        int _rowid;
        string _item_code;// char(20),
        string _warehouse_code;// char(10),
        int _period;
        int _yr;
        decimal _cost;// decimal(12),
        decimal _sale;// decimal(12),
        decimal _cqty;// decimal(10),
        decimal _sqty;// decimal(10),
        decimal _usage_rate;// decimal(10),
        int _day_ostk;
        int _num_stk_outs;
        decimal _dup_sqty;// decimal(10),
        decimal _recurr_usage;// decimal(10),
        decimal _dup_recurr_usage;// decimal(10)

        #region Constructor

        public DVOInvTransactionsStistatd()
        {
            _rowid = 0;
            _item_code = string.Empty;
            _warehouse_code = string.Empty;
            _period = 0;
            _yr = 0;
            _cost = 0;
            _sale = 0;
            _cqty = 0;
            _sqty = 0;
            _usage_rate = 0;
            _day_ostk = 0;
            _num_stk_outs = 0;
            _dup_sqty = 0;
            _recurr_usage = 0;
            _dup_recurr_usage = 0;
        }

        #endregion Constructor

        #region public properties

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
        public decimal cqty
        {
            get { return _cqty; }
            set { _cqty = value; }
        }
        public decimal sqty
        {
            get { return _sqty; }
            set { _sqty = value; }
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
        public decimal dup_recurr_usage
        {
            get { return _dup_recurr_usage; }
            set { _dup_recurr_usage = value; }
        }

        #endregion public properties

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
            get { return "tistatd"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid p_rowid,item_code p_item_code,warehouse_code p_warehouse_code,");
            sql.Append(" period p_period,yr p_yr,cost p_cost,sale p_sale,cqty p_cqty,sqty p_sqty,");
            sql.Append(" usage_rate p_usage_rate,day_ostk p_day_ostk,num_stk_outs p_num_stk_outs,");
            sql.Append(" dup_sqty p_dup_sqty,recurr_usage p_recurr_usage,dup_recurr_usage p_dup_recurr_usage");
            sql.Append(" FROM stistatd WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND rowid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(item_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(warehouse_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND period = " + parameters[3].ToString());
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND yr = " + parameters[4].ToString());
            if (Convert.ToDecimal(parameters[5]) > 0)
                sql.Append(" AND cost = " + parameters[5].ToString());
            if (Convert.ToDecimal(parameters[6]) > 0)
                sql.Append(" AND sale = " + parameters[6].ToString());
            if (Convert.ToDecimal(parameters[7]) > 0)
                sql.Append(" AND cqty = " + parameters[7].ToString());
            if (Convert.ToDecimal(parameters[8]) > 0)
                sql.Append(" AND sqty = " + parameters[8].ToString());
            if (Convert.ToDecimal(parameters[9]) > 0)
                sql.Append(" AND usage_rate = " + parameters[9].ToString());
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND day_ostk = " + parameters[10].ToString());
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND num_stk_outs = " + parameters[11].ToString());
            if (Convert.ToDecimal(parameters[12]) > 0)
                sql.Append(" AND dup_sqty = " + parameters[12].ToString());
            if (Convert.ToDecimal(parameters[13]) > 0)
                sql.Append(" AND recurr_usage = " + parameters[13].ToString());
            if (Convert.ToDecimal(parameters[14]) > 0)
                sql.Append(" AND dup_recurr_usage = " + parameters[14].ToString());

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
