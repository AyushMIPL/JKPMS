using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSBhdr: DVOBase
    {
        private string _acct_cat ;
        private Int32 _acct_no;
        private string _acct_status;
        private string _last_name;
        private string _first_name;
        private string _n_join;
        private string _first_name1;
        private string _last_name1;
        private int _doc_no ;
        private DateTime _doc_date;
        private string _tran_code;
        private string _tran_no;
        private string _voucher_no;
        private decimal _deposit_amt;
        private decimal _withdrawn_amt;
        private decimal _acct_balance;
        private decimal _active_balance;
        private string _operator;
        private int _trans_id;
        private string _ok_to_post;
        private string _acct_id;
        private int _RowID;
        private int _apdoc_no;
        private string _apchk_printed; 
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        //Added By Rahul on 04-08-09 
        private int _cash_received;
        private int _batch_id;
        private string _postorcheck;

        //added by Bharat Dhall [08/19/2009]
        int _clientRowId;
     
        #region Constructor
        public DVOSBhdr()
        {
            _acct_cat = string.Empty;
            _acct_no = 0;
            _acct_status = string.Empty;
            _last_name = string.Empty;
            _first_name = string.Empty;
            _n_join = string.Empty;
            _first_name1 = string.Empty;
            _last_name1 = string.Empty;
            _doc_no = 0;
            _doc_date = Convert.ToDateTime(null);
            _tran_code = string.Empty;
            _tran_no = string.Empty;
            _deposit_amt=0.0M;
            _withdrawn_amt=0.0M;
            _acct_balance = 0.0M;
            _active_balance = 0.0M;
            _operator = string.Empty;
            _trans_id = 0;
            _voucher_no = string.Empty;
            _ok_to_post = string.Empty;
            _acct_id = string.Empty;
            _RowID = 0;
            _apdoc_no = 0;
            _apchk_printed = string.Empty;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

            _cash_received = -1;
            _batch_id = 0;
            _postorcheck = string.Empty;

            _clientRowId = 0;
        }
        #endregion Constructor

        #region public properties
        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value; }
        }
        public Int32 acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string acct_status
        {
            get { return _acct_status; }
            set { _acct_status = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string n_join
        {
            get { return _n_join; }
            set { _n_join = value; }
        }
        public string first_name1
        {
            get { return _first_name1; }
            set { _first_name1 = value; }
        }
        public string last_name1
        {
            get { return _last_name1; }
            set { _last_name1 = value; }
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
        public string tran_code
        {
            get { return _tran_code; }
            set { _tran_code = value; }
        }
        public string tran_no
        {
            get { return _tran_no; }
            set { _tran_no = value; }
        }
        public string voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }
        public  decimal deposit_amt
        {
            get { return _deposit_amt; }
            set { _deposit_amt = value; }
        }
        public decimal withdrawn_amt
        {
            get { return _withdrawn_amt; }
            set { _withdrawn_amt = value; }
        }
        public decimal acct_balance
        {
            get { return _acct_balance; }
            set { _acct_balance = value; }
        }
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        public int trans_id
        {
            get { return _trans_id; }
            set { _trans_id= value; }
        }
        public string Ok_to_Post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
         public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public int apdoc_no
        {
            get { return _apdoc_no; }
            set { _apdoc_no = value; }
        }
        public string apchk_printed
        {
            get { return _apchk_printed; }
            set { _apchk_printed = value; }
        }
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
        public string acct_id
        {
            get { return _acct_id; }
            set { _acct_id = value; }
        }
        public decimal active_balance
        {
            get { return _active_balance; }
            set { _active_balance = value; }
        }

        public int cash_received
        {
            get { return _cash_received; }
            set { _cash_received = value; }
        }
        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }
        public string postorcheck
        {
            get { return _postorcheck; }
            set { _postorcheck = value; }
        }

        /// <summary>
        /// rowid of client related to transaction
        /// </summary>
        public int clientRowId
        {
            get { return _clientRowId; }
            set { _clientRowId = value; }
        }
        
        #endregion public properties

        #region Stored-Procedures

        public string CHANGE_TRANS_POST
        {
            get { return "uspSBTranPost"; }//uspsbtranpost
        }

        public override string INSERT_SPNAME
        {
            get { return "uspsbtraninsert"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsbtranupdate"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspsbaccttrandel"; }
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
            get { return "sbtranr"; }
        }

        public override int UNIQUE_ID
        {
            get { return RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string UPD_SBTRANR_ON_VOID
        {
            get { return "uspvdsbsbtranrupd"; }
        }
        public string GET_SAVING_BANK_EDIT_LISTING
        {
            get { return "uspsblistinggetall"; }
        }
        public string GET_CHKPRINTSTS
        {
            get { return "uspchkprintsts"; }
        }
        //Added By Rahul jain using for insert data from sb_hdr to sbtranr table
        public string INSERT_TRAN_DB
        {
            get { return "uspsb_hdrins"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select s1.acct_cat v_acct_cat,s1.acct_no v_acct_no,sbclientsr.acct_status v_acct_status,s1.doc_no v_doc_no,");
            sql.Append(" s1.doc_date v_doc_date,s1.tran_type v_tran_type,s1.tran_no v_tran_no,s1.voucher_no v_voucher_no,s1.deposit_amt v_deposit_amt,");
            sql.Append(" s1.withdrawn_amt v_withdrawn_amt,s1.acct_balance v_acct_balance,s1.operator v_operator,s1.rowid v_rowid , ");
            sql.Append(" s1.acct_id v_acct_id,s1.active_balance v_active_balance,apdoc_no v_apdoc_no,stpcashe.chk_printed v_apchkprinted,");
            sql.Append(" sbclientsr.first_name v_first_name,sbclientsr.last_name v_last_name ,s1.ok_to_post,sbclientsr.RowId clientrowid ");
           // sql.Append("s2.first_name1 v_first_name1,s2.acct_status v_acct_status");
            sql.Append(" from sbtranr s1,sbclientsr,outer stpcashe where s1.acct_id=sbclientsr.acct_id and s1.apdoc_no=stpcashe.doc_no ");
            sql.Append(" AND sbclientsr.acct_status='ACTIVE' AND sbclientsr.acct_closed='N'");
            //,sb_clients s2 where s1.acct_cat=s2.acct_cat and s1.acct_no=s2.acct_no");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  s1.doc_no=" + Convert.ToInt32(parameters[0].ToString()));
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.ok_to_post) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                else
                    sql.Append(" AND Rtrim(s1.ok_to_post) NOT IN ('P','C')");
            if (Convert.ToInt32(parameters[2]) > -1)
                sql.Append(" AND s1.cash_received  = " + parameters[2].ToString());
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND s1.batch_id = " + parameters[3].ToString());

            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND s1.rowid =" + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.acct_id) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (!parameters[6].ToString().Contains("0001"))
                    sql.Append(" AND date(s1.doc_date) = date('" + parameters[6].ToString().Trim().Replace("'", "''") + "')");
            
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.acct_cat) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (Convert.ToInt32(parameters[8]) > 0)
                    sql.Append(" AND s1.acct_no =" + parameters[8].ToString());
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.acct_status) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            

            sql.Append(" order by s1.acct_cat,s1.acct_no,s1.doc_date ,s1.doc_no");
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data from :sb_hdr
        /// Created By : Rahul
        /// Created Date : 01/08/09
        /// </summary>
        public string FINDQUERY_FROM_SB_HDR(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  acct_cat,acct_no,acct_status,doc_no,doc_date, ");
            sql.Append(" tran_code,tran_no,deposit_amt,withdrawn_amt,acct_balance,operator ");
            sql.Append(" From  sb_hdr  ");
            sql.Append(" ORDER BY doc_date,acct_cat,acct_no,doc_no");
            return sql.ToString();
        }
        public string FINDQUERY_UNPOSTEDTRAN_GET(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select '' lname, '' fname,doc_no,doc_date,acct_id,acct_cat,acct_no,acct_status, ");
            sql.Append(" tran_type,tran_no,doc_date,deposit_amt,withdrawn_amt,");
            sql.Append(" acct_balance,active_balance,operator,ok_to_post,apdoc_no,cash_received,batch_id,voucher_no ");
            sql.Append(" from sbtranr");
            sql.Append(" where ok_to_post NOT IN ('P','C')");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND acct_no =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_status) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append(" Order by 11");
            return sql.ToString();
        }


        public  string FIND_DEL(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select s1.acct_cat v_acct_cat,s1.acct_no v_acct_no,sbclientsr.acct_status v_acct_status,s1.doc_no v_doc_no,");
            sql.Append(" s1.doc_date v_doc_date,s1.tran_type v_tran_type,s1.tran_no v_tran_no,s1.voucher_no v_voucher_no,s1.deposit_amt v_deposit_amt,");
            sql.Append(" s1.withdrawn_amt v_withdrawn_amt,s1.acct_balance v_acct_balance,s1.operator v_operator,s1.rowid v_rowid , ");
            sql.Append(" s1.acct_id v_acct_id,s1.active_balance v_active_balance,0 v_apdoc_no,'' v_apchkprinted,");
            sql.Append(" sbclientsr.first_name v_first_name,sbclientsr.last_name v_last_name ,s1.ok_to_post,sbclientsr.RowId clientrowid ");
            sql.Append(" from sbtranr s1,sbclientsr where s1.acct_id=sbclientsr.acct_id ");
            sql.Append(" AND sbclientsr.acct_status='ACTIVE' AND sbclientsr.acct_closed='N'");    
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  s1.doc_no=" + Convert.ToInt32(parameters[0].ToString()));
            return sql.ToString();
        }


        #endregion store-procedures
    }
}
