using System;
using System.Collections.Generic;
using System.Text;
namespace JKPS.COMMON
{
  
       //Written By : Chandrasekhar on 10-07-2009 table used 'stoltypr' for defining Line Types for an order Entry 
        // against Physical Count
    public class DVOOrderEntryLineType : DVOBase
    {
         
        private int _rowid;
       	private string _line_type;
    private string _description;
    private string _like_type;
    private string _stock_item;
    private string _desc_update;
    private string _price_update;
    private string _rnd_dollar;
    private string _stage_to_bko;

    	
        
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        
       


         #region Constructor
        public DVOOrderEntryLineType()
        {
            _rowid = 0;
            _line_type = string.Empty;
            _description = string.Empty;
            _like_type = string.Empty;
            _stock_item = string.Empty;
            _desc_update = string.Empty;
            _rnd_dollar = string.Empty;
            _stage_to_bko = string.Empty;


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
        public string line_type
        {
            get { return _line_type; }
            set { _line_type = value; }
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
        public string stock_item
        {
            get { return _stock_item; }
            set { _stock_item = value; }
        }
        public string desc_update
        {
            get { return _desc_update; }
            set { _desc_update = value; }
        }
         public string price_update
        {
            get { return _price_update; }
            set { _price_update = value; }
        }
        public string rnd_dollar
        {
            get { return _rnd_dollar; }
            set { _rnd_dollar = value; }
        }
        public string stage_to_bko
        {
            get { return _stage_to_bko; }
            set { _stage_to_bko = value; }
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

        //Added By Ragul Jain using in Print line Type Definition report
        public string GET_LINE_TYPE_DEFINITION
        {
            get { return "usplinetypedefget"; }
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

