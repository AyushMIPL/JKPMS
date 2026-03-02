using System;
using System.Collections.Generic;
using System.Text;
namespace JKPS.COMMON
{
  
       //Written By : Chandrasekhar on 24-06-2009 table used 'stoakasr' for defining Item code Alias for a Customer 
        // against Physical Count
    public class DVOCustomerItemAlias : DVOBase
    {
         
        private int _rowid;
       	private string _cust_code;
      	private string _alias;
    	private string _item_code;
        private string _desc1;
        private string _desc2;
        private string _bus_name;
    	
        
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        
       


        #region Constructor
        public DVOCustomerItemAlias()
        {
            _rowid = 0;
            _cust_code=string.Empty;
            _alias=string.Empty;
            _item_code=string.Empty;
            _desc1 = string.Empty;
            _desc2 = string.Empty;
            _bus_name = string.Empty;
            
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
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public string alias
        {
            get { return _alias; }
            set { _alias = value; }
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
            get { return _desc1; }
            set { _desc2 = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
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
            get { return "uspOEAliasIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspOEAliasUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspOEAliasDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspOEAliasGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "stoakasr"; }
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
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select asr.rowid,asr.alias,asr.item_code,iteminfo.desc1,iteminfo.desc2,asr.cust_code,cust.bus_name");
            sql.Append(" from stoakasr asr,stiinvtr iteminfo,strcustr cust ");
            sql.Append(" where asr.item_code=iteminfo.item_code and asr.cust_code=cust.cust_code ");
            if (parameters[0].ToString() != string.Empty)
                sql.Append(" AND Rtrim(asr.alias) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1].ToString() != string.Empty)
                sql.Append(" AND Rtrim(asr.item_code) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2].ToString() != string.Empty)
                sql.Append(" AND Rtrim(asr.cust_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            return sql.ToString();
        }
        

        #endregion Stored-Procedures  
    
        
    }
}

