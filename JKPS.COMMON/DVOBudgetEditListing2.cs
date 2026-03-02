using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOBudgetEditListing2 : DVOBase
    {  

        private int _rowid;
        private int _id;
        private int _account;
        private decimal _currestimate;
        private decimal _approved;
        private decimal _revised;
        private decimal _allocatedtodate;
        private decimal _allocpercent;
        private decimal _extra_funds;

        private string _keyvalue;
        private string _year;
        private string _set;


        private int _inballod_estid;
        private string _inballod_startingperiod;
        private string _inballod_endingperiod;
        private decimal _inballod_allocpercent;
        private decimal _inballod_allocamount;
        private string _inballod_used;

        public DVOBudgetEditListing2()
        {
            _rowid = 0;
            _id = 0;
            _account = 0;
            _currestimate = 0.0M;
            _approved = 0.0M;
            _extra_funds = 0.0M;
            _allocpercent = 0;
            _revised = 0.0M;
            _allocatedtodate = 0.0M;
            _keyvalue = string.Empty;
            _year = string.Empty;
            _set = string.Empty;

            inballod_estid=0;
            _inballod_startingperiod = string.Empty;
         _inballod_endingperiod=string.Empty;
         _inballod_allocpercent=0;
         _inballod_allocamount = 0; ;
         _inballod_used=string.Empty;




        }

        #region public properties
        public  int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int account
        {
            get { return _account; }
            set { _account = value; }

        }
        public decimal currestimate
        {
            get { return _currestimate; }
            set { _currestimate = value; }
        }
        public decimal approved
        { get { return _approved; }
            set { _approved = value; }
        }
        public decimal revised
        {
            get { return _revised; }
            set { _revised = value; }
        }
        public decimal  allocpercent
        {
            get { return _allocpercent; }
            set { _allocpercent = value; }
        }
        public decimal allocatedtodate
        {
             get { return _allocatedtodate; }
            set { _allocatedtodate = value; }
        }
        public decimal extra_funds
        {
            get { return _extra_funds; }
            set { _extra_funds = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string set
        {
            get { return _set; }
            set { _set = value; }
        }
        public int inballod_estid
        {
            get { return _inballod_estid; }
            set { _inballod_estid = value; }
        }
        public string inballod_startingperiod
        {
            get { return _inballod_startingperiod; }
            set { _inballod_startingperiod = value; }
        }
        public string inballod_endingperiod
        {
            get { return _inballod_endingperiod; }
            set { _inballod_endingperiod = value; }
        }
        public decimal inballod_allocpercent
        {
            get { return _inballod_allocpercent; }
            set { _inballod_allocpercent = value; }
        }
        public decimal inballod_allocamount
        {
            get { return _inballod_allocamount; }
            set { _inballod_allocamount = value; }      
        }
        public string inballod_used
        {
            get { return _inballod_used; }
            set { _inballod_used = value; }    
        }
        #endregion properties


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
            get { return ""; }
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
        public string Getdefallocper
        { 
            get { return "uspdefallocper"; } 
        }
        public string GETmtlyallocwarr
        {
            get { return "uspmtlyallocwarr"; }
        }
        public string INSINBALLOD
        { get { return "uspinballodIns"; } }

        public string GETuspupdinbstid
        {
            get { return "uspupdinbestid1"; }
        }
     
        public string GETuspupdinballod
        {
            get { return "uspupdinballod"; }
        }
        public string GEtuspupdinbwarah
        {
            get { return "uspupdinbwarah"; }
        }
        public string GETuspgetbuddef
        {
            get { return "uspgetbuddef"; }
        }
        public string GETAccountNoInfo
        {
            get { return "uspBEListng2Getall"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            return sql.ToString();
        }

        #endregion Stored-Procedures

  
    }

}
