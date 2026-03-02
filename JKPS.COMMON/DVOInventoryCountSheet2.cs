using System;
using System.Collections.Generic;
using System.Text;
//Written By : Chandrasekhar on 26-06-2009 table used 'sticadje' for adjusting Inventory 
// against Physical Count
namespace JKPS.COMMON
{
    public class DVOInventoryCountSheethdr :DVOBase
    {
    //     doc_no serial not null ,
    //doc_date date,
    //count_desc char(30),
    //count_acct integer,
    //ok_post char(1),
    //blind char(1),
    //warehouse_code char(10),
    //l_mod_date date,
    //l_mod_time char(8),
    //l_mod_id char(8)
        private int _rowid;
        private int _doc_no;
        private DateTime _doc_date;
    	private string _count_desc;
    	private int _count_acct;
    	private string _ok_post;
    	private string _blind;
    	private string _warehouse_code;
    	private DateTime _l_mod_date;
    	private string _l_mod_time;
    	private string  _l_mod_id;
    	
        
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        
       


         #region Constructor
        public DVOInventoryCountSheethdr()
        {
            _rowid=0;
        _doc_no=0;
        _doc_date=DateTime.Now;
    	_count_desc=string.Empty;
    	_count_acct=0;;
    	_ok_post=string.Empty;
        _blind=string.Empty;
    	_warehouse_code=string.Empty;
    	_l_mod_date=DateTime.Now;
    	_l_mod_time=string.Empty;
    	_l_mod_id=string.Empty;
            
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
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }

        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }

        public string count_desc
        {
            get { return _count_desc; }
            set { _count_desc = value; }
        }
        public int count_acct
        {
            get { return _count_acct; }
            set { _count_acct = value; }
        }
        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }
        public string blind
        {
            get { return _blind; }
            set { _blind = value; }
        }

        public string warehouse_code
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }
        public DateTime l_mod_date
        {
            get { return _l_mod_date; }
            set { _l_mod_date = value; }
        }
        public string l_mod_time
        {
            get { return _l_mod_time; }
            set { _l_mod_time = value; }
        }
        public string l_mod_id
        {
            get { return _l_mod_id; }
            set { _l_mod_id = value; }
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
            get { return "uspICPChdrins"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        

        #endregion Stored-Procedures  
    
        
    }
}
