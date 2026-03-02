using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOJournalHeaderEntry :DVOBase
    {
           
         #region Private Variables
        /// <summary>
        ///
        /// </summary>
        private long _doc_no;
        /// <summary>
        ///
        /// </summary>
        private string _doc_desc;
        /// <summary>
        ///
        /// </summary>
        private string _doc_date;
        /// <summary>
        ///
        /// </summary>
        private string _doc_src;
        /// <summary>
        ///
        /// </summary>
        private string _auto_rev;
        /// <summary>
        ///
        /// </summary>
        private string _file_type;
        /// <summary>
        ///
        /// </summary>
        private string _posted;
        /// <summary>
        ///
        /// </summary>
        private string _ok_to_post;
        /// <summary>
        ///
        /// </summary>
        private int _batch_id;
        /// <summary>
        ///
        /// </summary>
        private string _user_id;
        /// <summary>
        /// 
        /// </summary>

        private string _src_key;
        private int _check;

        private int _rowid;

        
        private List<DVOJournalDetailsEntry> _DVOJournalDetailsEntries;
        /// <summary>
        /// 
        /// </summary>
        private string _src_desc;
        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        #endregion
        #region Constructor
        public DVOJournalHeaderEntry()
        {
            doc_no=0; //integer,
            doc_date = ""; //date,
            doc_desc=string.Empty; //char(30),            
            doc_src=string.Empty; //char(6),           
            auto_rev=string.Empty; //char(1),
            file_type=string.Empty; //char(2),
            posted=string.Empty; //char(1),
            ok_to_post=string.Empty; //char(1),
            batch_id=0; //integer,
            user_id=string.Empty; //char(8)
            _src_key = string.Empty;//char(6)
            _src_desc = string.Empty;//char(30)
            _check = 0;//int
            _rowid = 0;
            _DVOJournalDetailsEntries = new List<DVOJournalDetailsEntry>();
        }
        #endregion Constructor
        #region Public Properties
        
        public long doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string doc_desc
        {
            get { return _doc_desc; }
            set { _doc_desc = value; }
        }
        public string doc_src
        {
            get { return _doc_src; }
            set { _doc_src = value; }
        }       
        public string auto_rev
        {
            get { return _auto_rev; }
            set { _auto_rev = value; }
        }
        public string file_type
        {
            get { return _file_type; }
            set { _file_type = value; }
        }
        public string posted
        {
            get { return _posted; }
            set { _posted = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }
        public string src_desc
        {
            get { return _src_desc; }
            set { _src_desc = value; }
        }
        public int check
        {
            get { return _check; }
            set { _check = value; }
        }

        public int rowid
        {
            get { return _rowid ; }
            set { _rowid  = value; }
        }
        public object DVOJournalDetailsEntries
        {
            get
            {
                object obj=_DVOJournalDetailsEntries;
                return obj;
            }
            set
            {
                _DVOJournalDetailsEntries.Add((DVOJournalDetailsEntry)value);
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

        public string Get_Ledger_List
        {
            get { return "uspJrnlInfogetall"; }
        }
        public string Get_Document_source
        {
            get { return "uspgetdocumentsrc"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspJrnlHdrins"; }//uspjrnlhdrins
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspJrnlHdrupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspJrnlHdrdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspJrnlInfoget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspJrnlInfogetall"; }
        }
        public override string TABLE_NAME
        {
            get { return "stgjoure"; }
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
        public string INSERT_STGJOURE
        {
            get { return "uspgjoureins"; }
        }
        public string INSERT_STGJOURD
        {
            get { return "uspgjourdins"; }
        }
        public string UPD_STGJOURE
        {
            get { return "uspjrnlhdrupdnew"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stgjoure.doc_no, stgjoure.doc_desc, stgjoure.doc_date, stgjoure.doc_src, stgjoure.auto_rev, stgjoure.file_type, ");
            sql.Append("stgjoure.posted, stgjoure.ok_to_post, stgjoure.batch_id, stgjoure.user_id, stgjourd.orig_journal, stgjourd.line_no,");
            sql.Append("stgjourd.acct_no, stgjourd.department, stgjourd.amount, stgjourd.debit_credit, stxinfor.src_desc, PayrollGLAccounts.acct_desc,");
            //sql.Append("PayrollGLAccounts.keyvalue,Flex_struct_Header.desc AcctType,Flex_struct_Header.id acct_type_id,stgjoure.rowid");
            //Added by rahul jain 25/02/2009
            sql.Append("PayrollGLAccounts.keyvalue,Flex_struct_Header.accounttype AcctType,Flex_struct_Header.id acct_type_id,stgjoure.rowid,stgjourd.rowid");
            sql.Append(" FROM stgjourd,stgjoure,stxinfor,PayrollGLAccounts,Flex_struct_Header where stgjourd.doc_no = stgjoure.doc_no ");
            sql.Append(" and stgjoure.doc_src = stxinfor.src_key and stgjourd.acct_no = PayrollGLAccounts.acct_no ");
            sql.Append(" and stxinfor.src_type = 'S' and stgjourd.orig_journal = 'GJ' ");//and stgjoure.posted  not in ('Y','C')
            sql.Append(" and Flex_struct_Header.accounttype=PayrollGLAccounts.acct_type");
            if (Convert.ToInt32(parameters[0]) > 0)//doc_no
                sql.Append(" AND stgjoure.doc_no = " + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString()!=null)
                sql.Append(" AND stgjoure.doc_desc like '" + parameters[1].ToString().Replace("'", "''") + " %'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)
                sql.Append(" AND stgjoure.doc_date = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)
                sql.Append(" AND stgjoure.auto_rev = '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND stgjoure.batch_id = " + parameters[4].ToString());
            if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)
                sql.Append(" AND stgjoure.user_id = '" + parameters[5].ToString().Replace("'", "''") + "'");
            if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != null)
                sql.Append(" AND stgjoure.posted = '" + parameters[6].ToString().Replace("'", "''") + "'");
            else
                sql.Append(" AND stgjoure.posted NOT IN ('Y','C')");
            //*********************Added by Sunil Pahwa for locking purpose******************************
            if (Convert.ToInt32(parameters[7]) > 0)//rowid
                sql.Append(" AND stgjoure.rowid = " + parameters[7].ToString());
            //*******************************************************************************************
            sql.Append(" ORDER BY stgjoure.doc_no desc");             
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
