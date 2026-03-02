using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By :Rahul Jain on 12-05-2009 table used stuctlgd for catalog detail in purchasing module
    public class DVOCatalogDetailstuctlgd : DVOBase
    {
        private int _rowid;
        private string _vendor_code;
        private string _item_code;
        private decimal _cost;
        private string _vend_item_code;
        private string _primary_vendor;
        private string _currency_code;
        private string _line_code;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        //
        private string _bus_name;
        private string _desc1;
        private string _desc2;

        public DVOCatalogDetailstuctlgd()
        {
            _rowid = 0;
            _vendor_code = string.Empty;
            _item_code = string.Empty;
            _cost = 0;
            _vend_item_code = string.Empty;
            _primary_vendor = string.Empty;
            _currency_code = string.Empty;
            _line_code = string.Empty;


            _InsertMachineInfo = "App";
            _InsertDate = Convert.ToDateTime("01/01/1900");// DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = Convert.ToDateTime("01/01/1900"); //DateTime.Now;
            _UpdateBy = -1;

            _bus_name = string.Empty;
            _desc1 = string.Empty;
            _desc2 = string.Empty;

        }
        #region Property
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string vendor_code
        {
            get { return _vendor_code; }
            set { _vendor_code = value; }
        }
        public string item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public decimal cost
        {
            get { return _cost; }
            set { _cost = value; }
        }   
        public string vend_item_code
        {
            get { return _vend_item_code; }
            set { _vend_item_code = value; }
        }
        public string primary_vendor
        {
            get { return _primary_vendor; }
            set { _primary_vendor = value; }
        }
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string line_code
        {
            get { return _line_code; }
            set { _line_code = value; }
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

        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
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
        #endregion Property

        #region Stored-Procedures

        public override string  INSERT_SPNAME
        {
            get { return "uspicatalogIns"; }
        }

        public override string  UPDATE_SPNAME
        {
            get { return "uspicatalogupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspcatalogdtldel"; }
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
            get { return "stuctlgd"; }
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
        //Added By Rahul Jain on 15-05-2009 for getting Vendor catalog for Print Vendor catalog report
        public string VENDOR_CATALOG_GET
        {
            get { return "uspvendcatalogget"; }
        }
        //----------------------------------------------------------------------------------------------
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT s1.rowid, s1.vendor_code, s1.item_code, s1.cost ,s1.vend_item_code, s1.primary_vendor,");
            sql.Append(" s1.currency_code, s1.line_code,s2.bus_name,s3.desc1,s3.desc2 ");
            //************** stiinvtr table Added by Bharat Dhall [05/15/2009]  ******************
            sql.Append(" FROM  stuctlgd s1,outer stpvendr s2,outer stiinvtr s3 ");
            //**********************************************************************
            sql.Append(" where s1.vendor_code=s2.vend_code and s1.item_code=s3.item_code");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.item_code)  = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.vendor_code)  = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND s1.rowid  = " + parameters[2].ToString());

            sql.Append(" order by s1.vendor_code");

            return sql.ToString();
        }
        #endregion Stored-Procedures
    }

    public class DVOCatalogDetailstuctlgd_Comparer_ItemCode : IComparer<DVOCatalogDetailstuctlgd>
    {
        public int Compare(DVOCatalogDetailstuctlgd x, DVOCatalogDetailstuctlgd y)
        {
            if (x != null && y != null)
                return x.item_code.CompareTo(y.item_code);
            return 0;
        }
    }
}
