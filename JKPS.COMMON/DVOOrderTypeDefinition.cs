using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    
       //Written By : Chandrasekhar on 07-07-2009 table used 'stootypr' for adjusting Inventory 
        // against Physical Count
    public class DVOOrderTypeDefinition : DVOBase
    {
        private int _rowid;
        private string _order_type;
        private string _description;
        private string _like_type;
        private string _master_order;
        private string _reference_order;
        private string _print_ack;
        private string _print_pic;
        private string _print_mfs;
        private string _pay_method_req;
        private string _po_no_req;
        private string _fob_point_req;
        private string _shp_via_req;
        private string _frt_doc_no_req;
        private string _fact_ord_begin;
        private string _fact_ord_end;
        private string _fact_ord_next;
        private string _bill_only;
        private string _inv_pre_apprv;
        private string _tmp_order;
        private string _intl_order;
        private string _resale_cust;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;




        #region Constructor
        public DVOOrderTypeDefinition()
        {
            _rowid = 0;
            _order_type = string.Empty;
            _description = string.Empty;
            _like_type = string.Empty;
            _master_order = string.Empty;
            _reference_order = string.Empty;
            _print_ack = string.Empty;
            _print_pic = string.Empty;
            _print_mfs = string.Empty;
            _pay_method_req = string.Empty;
            _po_no_req = string.Empty;
            _fob_point_req = string.Empty;
            _shp_via_req = string.Empty;
            _frt_doc_no_req = string.Empty;
            _fact_ord_begin = string.Empty;
            _fact_ord_end = string.Empty;
            _fact_ord_next = string.Empty;
            _bill_only = string.Empty;
            _inv_pre_apprv = string.Empty;
            _tmp_order = string.Empty;
            _intl_order = string.Empty;
            _resale_cust = string.Empty;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }
        #endregion

        #region Property

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string order_type
        {
            get { return _order_type; }
            set { _order_type = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string like_type
        {
            get { return _like_type; }
            set { _like_type = value; }
        }
        public string master_order
        {
            get { return _master_order; }
            set { _master_order = value; }
        }
        public string reference_order
        {
            get { return _reference_order; }
            set { _reference_order = value; }
        }
        public string print_ack
        {
            get { return _print_ack; }
            set { _print_ack = value; }
        }
        public string print_pic
        {
            get { return _print_pic; }
            set { _print_pic = value; }
        }
        public string print_mfs
        {
            get { return _print_mfs; }
            set { _print_mfs = value; }
        }
        public string pay_method_req
        {
            get { return _pay_method_req; }
            set { _pay_method_req = value; }
        }
        public string po_no_req
        {
            get { return _po_no_req; }
            set { _po_no_req = value; }
        }
        public string fob_point_req
        {
            get { return _fob_point_req; }
            set { _fob_point_req = value; }
        }
        public string shp_via_req
        {
            get { return _shp_via_req; }
            set { _shp_via_req = value; }
        }
        public string frt_doc_no_req
        {
            get { return _frt_doc_no_req; }
            set { _frt_doc_no_req = value; }
        }
        public string fact_ord_begin
        {
            get { return _fact_ord_begin; }
            set { _fact_ord_begin = value; }
        }
        public string fact_ord_end
        {
            get { return _fact_ord_end; }
            set { _fact_ord_end = value; }
        }
        public string fact_ord_next
        {
            get { return _fact_ord_next; }
            set { _fact_ord_next = value; }
        }
        public string bill_only
        {
            get { return _bill_only; }
            set { _bill_only = value; }
        }
        public string inv_pre_apprv
        {
            get { return _inv_pre_apprv; }
            set { _inv_pre_apprv = value; }
        }
        public string tmp_order
        {
            get { return _tmp_order; }
            set { _tmp_order = value; }
        }
        public string intl_order
        {
            get { return _intl_order; }
            set { _intl_order = value; }
        }
        public string resale_cust
        {
            get { return _resale_cust; }
            set { _resale_cust = value; }
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

        #endregion


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspOMTypDefIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspOMTypDefUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspOMTypDefDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspOMTpDfGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return ""; }
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
        public string GET_OVER_SHORT_REPORT_INFO1
        {
            get { return ""; }
        }
        public string DELETE_COUNTS
        {
            get { return ""; }
        }
        public string UPDATE_CADJE
        {
            get { return ""; }
        }
        public string GET_INV_COUNTS
        {
            get { return ""; }
        }
        //Added By Rahul Jain using Print Open Order Summary report
        public string GET_DESCRIPTION
        {
            get { return "uspopnordsummget"; }
        }

        //Added by Sunil on 21/07/2009 for print sales person summary info
        public string GET_SALES_PERSON_DESC
        {
            get { return "uspsalpersondscget"; }
        }
        //********************************************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select  rowid,order_type,description,like_type,master_order,reference_order,print_ack,print_pic,print_mfs,");
            sql.Append("pay_method_req,po_no_req,fob_point_req,shp_via_req,frt_doc_no_req,fact_ord_begin,fact_ord_end,fact_ord_next,bill_only,inv_pre_apprv,tmp_order,intl_order,resale_cust");
            sql.Append(" from stootypr where 1=1");
            if (parameters[0].ToString() != string.Empty)
                sql.Append(" AND Rtrim(dtl.order_type) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1].ToString() != string.Empty)
                sql.Append(" AND Rtrim(dtl.description) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToString(parameters[2])!= string.Empty)
                sql.Append(" AND dtl.like_type = '" + parameters[2].ToString() + "'");
            if (Convert.ToString(parameters[3])!= string.Empty)
                sql.Append(" AND dtl.master_order = '" + parameters[3].ToString() + "'");
            if (Convert.ToString(parameters[4])!= string.Empty)
                sql.Append(" AND dtl.reference_order = '" + parameters[4].ToString() + "'");
            if (Convert.ToString(parameters[5])!= string.Empty)
                sql.Append(" AND dtl.print_ack = '" + parameters[5].ToString() + "'");
            if (Convert.ToString(parameters[6])!= string.Empty)
                sql.Append(" AND dtl.print_pic = '" + parameters[6].ToString() + "'");
            if (Convert.ToString(parameters[7])!= string.Empty)
                sql.Append(" AND dtl.print_mfs = '" + parameters[7].ToString() + "'");
            if (Convert.ToString(parameters[8])!= string.Empty)
                sql.Append(" AND dtl.pay_method_req = '" + parameters[8].ToString() + "'");
            if (Convert.ToString(parameters[9])!= string.Empty)
                sql.Append(" AND dtl.po_no_req = '" + parameters[9].ToString() + "'");
            if (Convert.ToString(parameters[10])!= string.Empty)
                sql.Append(" AND dtl.fob_point_req = '" + parameters[10].ToString() + "'");
            if (Convert.ToString(parameters[11])!= string.Empty)
                sql.Append(" AND dtl.shp_via_req = '" + parameters[11].ToString() + "'");
            if (Convert.ToString(parameters[12])!= string.Empty)
                sql.Append(" AND dtl.frt_doc_no_req = '" + parameters[12].ToString() + "'");
            //if (Convert.ToInt32(parameters[0]) > 0)
            //    sql.Append(" AND dtl.doc_no = " + parameters[0].ToString());
            //if (Convert.ToInt32(parameters[1]) > 0)
            //    sql.Append(" AND dtl.page_no = " + parameters[1].ToString());
            //if (parameters[2].ToString() != string.Empty)
            //    sql.Append(" AND Rtrim(dtl.item_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            //if (Convert.ToDecimal(parameters[3]) > 0.0M)
            //    sql.Append(" AND dtl.qty_on_hand = " + parameters[3].ToString());
            //if (Convert.ToDecimal(parameters[4]) > 0.0M)
            //    sql.Append(" AND dtl.count_qty = " + parameters[4].ToString());
            //if (Convert.ToDecimal(parameters[5]) > 0.0M)
            //    sql.Append(" AND dtl.adj_qty = " + parameters[5].ToString());
            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
