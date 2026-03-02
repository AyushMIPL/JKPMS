using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON 
{
   public  class DVOGLstgactvd : DVOBase
    {
     

        private string _orig_journal;
        private Int32 _doc_no;
        private Int32 _acct_no;
        private string _department;
        private Decimal _amount;
        private string _debit_credit;
        private Int32 _Rowid;
        private string _incr_with_cred;
        

        #region Constructor
        public DVOGLstgactvd()
        {
            _orig_journal = string.Empty;
            _doc_no = 0;
            _acct_no = 0;
            _department = string.Empty;
            _amount = 0.0M;
            _debit_credit = string.Empty;
            _incr_with_cred = string.Empty;
          
        }
        #endregion Constructor

        #region public properties
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }

        public Int32 doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public Int32 acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }

        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public Decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value; }
        }
        public int Rowid
        {
            get { return _Rowid ; }
            set { _Rowid  = value; }
        }
       public string incr_with_cred
        {
            get { return _incr_with_cred; }
            set { _incr_with_cred = value; }
        }


        #endregion public properties

        #region Stored-Procedures
       public string UPDATE_RECBALANCES
       {
           get { return "usp_ins_upd_rec_bl"; }
       }
       public string UPDATE_ACTIVITY
       {
           get { return "usp_upd_activity"; }
       }
       public string GET_ACTIVITY_DETAIL
       {
           get { return "usp_act_detail"; }
       }
       public string GET_REC_MONTH_YEAR
       {
           get { return "usp_rec_period"; }
       }
        public override string INSERT_SPNAME
        {
            get { return "uspstgactvdins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspupdstxckrgd"; }
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
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string uspAccGrpsDtlget
        {
            get { return ""; }
        }
       public string DELETE_GLACT
       {
           get { return "uspglactivdel"; }
       }
       public string DELETE_ARACT
       {
           get { return "usparactivdel"; }
       }
       public string UPD_CNT
       {
           get { return "uspcntupd"; }
       }
       public string DELETE_APACT
       {
           get { return "uspapactivdel"; }
       }
       public string DELETE_PUACT
       {
           get { return "usppuactivdel"; }
       }
       public string DELETE_ICACT
       {
           get { return "uspicactivdel"; }
       }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
           

            return sql.ToString();
        }

        #endregion store-procedures
    }
}
