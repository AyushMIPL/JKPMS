using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{

    public class DVOJournalDetailsEntry:DVOBase
    {
        #region Private Variables

        private int _rowid;

        /// <summary>
        ///
        /// </summary>
        private string _orig_journal;
        /// <summary>
        ///
        /// </summary>
        private int _doc_no;
        /// <summary>
        ///
        /// </summary>
        private int _line_no;
        /// <summary>
        ///
        /// </summary>
        private int _acct_no;
        
        /// <summary>
        ///
        /// </summary>
        private string _department;
        /// <summary>
        ///
        /// </summary>
        private decimal _amount;
        private decimal _amountdc;
        /// <summary>
        ///
        /// </summary>
        private string _debit_credit;
        /// <summary>
        /// 
        /// </summary>
        private int _Editable;
        /// <summary>
        /// 
        /// </summary>
        private string _acct_desc;
        private string _keyvalue;
        private string _AccountType;
        private int _AccountTypeId;

        private decimal _tmpamount;
        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        #endregion
        #region Constructor
        public DVOJournalDetailsEntry()
        {
            _rowid = 0;
            _orig_journal = string.Empty;// char(2) not null ,
            _doc_no = 0;// integer not null ,
            _line_no = 0; //smallint,
            _acct_no = 0;// integer,
            _department = string.Empty;// char(3),
            _amount = 0; //decimal(12),
            _amountdc = 0; //decimal(12),
            _debit_credit = string.Empty;// char(1)
            _tmpamount = 0;
            _acct_desc = string.Empty;
            _keyvalue = string.Empty;
            _AccountType = string.Empty;
            _AccountTypeId = 0;

        }
        #endregion Constructor
        #region Public Properties

        public int Rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value.Trim(); }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value.Trim(); }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public decimal amountdc
        {
            get { return _amountdc; }
            set { _amountdc = value; }
        }
        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value.Trim(); }
        }
        public int Editable
        {
            get
            {
                return _Editable;
            }
            set
            {
                _Editable = value;
            }
        }
        public string acct_desc
        {
            get{return _acct_desc;}
            set{_acct_desc = value;}
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }
        public int AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }

        public decimal tmpamount
        {
            get
            {
                return _tmpamount;
            }
            set
            {
                _tmpamount = value;
            }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

        #endregion Public Properties
        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspJrnlDetins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspJrnlDetupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspJrnlDetdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public string UPD_STGJOURD
        {
            get { return "uspjrnldetupdnew"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            return string.Empty;
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
        #endregion Stored-Procedures
    }
}
