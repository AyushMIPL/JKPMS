using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMailHistory : DVOBase
    {
        private int _MailStatusID;
        private string _empl_code;
        private int _doc_no;
        private DateTime _eop_date;
        private DateTime _pay_date;
        private string _keyvalue;
        private string _bank1;
        private decimal _deposit1;
        private string _bank2;
        private decimal _deposit2;
        private string _bank3;
        private decimal _deposit3;
        private string _bank4;
        private decimal _deposit4;
        private string _bank5;
        private decimal _deposit5;
        private string _check_no;
        private int _Status;
        private string _ErrorDesc;
        private int _insertby;
        private DateTime _insertdate;
        private string _insertmachineinfo;
        private string _first_name;
        private string _last_name;
        private string _email;
        
        #region public constructor


        public DVOMailHistory()
        {
            _MailStatusID = 0;
	        _empl_code = string.Empty;
	        _doc_no = 0;
	        _eop_date  = Convert.ToDateTime("01/01/1900");
	        _pay_date  = Convert.ToDateTime("01/01/1900");
            _keyvalue = string.Empty;
	        _bank1  = string.Empty;
	        _deposit1 = 0;
	        _bank2  = string.Empty;
	        _deposit2 = 0;
	        _bank3  = string.Empty;
	        _deposit3 = 0;
	        _bank4  = string.Empty;
	        _deposit4 = 0;
	        _bank5  = string.Empty;
	        _deposit5 = 0;
	        _check_no  = string.Empty;
	        _Status  = 0;
	        _ErrorDesc = string.Empty;
	        _insertby  = 0;
	        _insertdate  = Convert.ToDateTime("01/01/1900");
            _insertmachineinfo = "";
            _first_name = string.Empty;
            _last_name = string.Empty;
            _email = string.Empty;
        }
        #endregion constructor
        #region public properties
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public int MailStatusID
        {
            get { return _MailStatusID; }
            set { _MailStatusID = value; }
        }
        public int doc_no 
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public DateTime eop_date
        {
            get { return _eop_date; }
            set { _eop_date = value; }
        }
        public DateTime pay_date
        {
            get { return _pay_date; }
            set { _pay_date = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string bank1
        {
            get { return _bank1; }
            set { _bank1 = value; }
        }
        public decimal deposit1
        {
            get { return _deposit1; }
            set { _deposit1 = value; }
        }
        public string bank2
        {
            get { return _bank2; }
            set { _bank2 = value; }
        }
        public decimal deposit2
        {
            get { return _deposit2; }
            set { _deposit2 = value; }
        }
        public string bank3
        {
            get { return _bank3; }
            set { _bank3 = value; }
        }
        public decimal deposit3
        {
            get { return _deposit3; }
            set { _deposit3 = value; }
        }
        public string bank4
        {
            get { return _bank4; }
            set { _bank4 = value; }
        }
        public decimal deposit4
        {
            get { return _deposit4; }
            set { _deposit4 = value; }
        }
        public string bank5
        {
            get { return _bank5; }
            set { _bank5 = value; }
        }
        public decimal deposit5
        {
            get { return _deposit5; }
            set { _deposit5 = value; }
        }
        public string check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string ErrorDesc
        {
            get { return _ErrorDesc; }
            set { _ErrorDesc = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public DateTime insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        
        #endregion public properties




     #region Stored-Procedures
     /// <summary>
     /// stored procedures
     /// </summary>

        public override string INSERT_SPNAME
        {
            get { return "USP_MailHistoryIns"; }
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
            get { return "USP_MailHistoryGet"; }
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
    
       /// <summary>
       /// Find the record 
       /// </summary>
       /// <param name="parameters"></param>
       /// <returns></returns>


        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
        }
        #endregion Stored-Procedures
    }
}
