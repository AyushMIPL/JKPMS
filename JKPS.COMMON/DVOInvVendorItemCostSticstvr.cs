using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOInvVendorItemCostSticstvr : DVOBase
    {
        int _rowid;
        int _hierarchy_no;
        string _item_code;
        string _warehouse_code;
        decimal _quantity;
        decimal _cost;
        string _vend_code;

        #region Constructor

        public DVOInvVendorItemCostSticstvr()
        {
            _rowid = 0;
            _hierarchy_no = 0;
            _item_code = string.Empty;
            _warehouse_code = string.Empty;
            _quantity = 0;
            _cost = 0;
            _vend_code = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int hierarchy_no
        {
            get { return _hierarchy_no; }
            set { _hierarchy_no = value; }
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
        public decimal quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
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
            get { return "sticstvr"; }
        }
        public override int UNIQUE_ID
        {
            get { return _hierarchy_no; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //Added By Rahul Jain on 01/07/09 Using in inventory post
        public string Getc_cstv_push
        {
            get { return "uspc_cstv_push"; }
        }
        public string Updateu_cstv
        {
            get { return "uspu_cstv"; }
        }
        public string Deleted_cstv
        {
            get { return "uspd_cstv"; }
        }
        public string Inserti_cstv
        {
            get { return "uspi_cstv"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT hierarchy_no p_hierarchy_no,item_code p_item_code,warehouse_code p_warehouse_code,");
            sql.Append(" quantity p_quantity,cost p_cost,vend_code p_vend_code");
            sql.Append(" FROM sticstvr WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND hierarchy_no = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(item_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(warehouse_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToDecimal(parameters[3]) > 0)
                sql.Append(" AND quantity = " + parameters[3].ToString());
            if (Convert.ToDecimal(parameters[4]) > 0)
                sql.Append(" AND cost = " + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(vend_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
