using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public partial class DVOUpdateLedgerDefaults : DVOBase
    {
        private int _rowid;
        private int _retain_earnings;//as account number
        private int _genjrn_doc_no;
        private int _genjrn_post_no;
        private int _stdent_doc_no;
        private int _genled_post_no;
        private string _curr_period;
        private string _curr_year;
        private string _gl_balanced;
        private string _dir_db_cr;
        private string _setup_date;//datetimein sql
        private string _use_batch_gen;
        private string _use_approv_post;
        private string _approval_code;
        private string _budget_checking;


        private string _period;
        private string _period_year;
        private string _start_date;//in sql smalldatetime
        private string _end_date;
        private string _balanced;
        private string _period_closed;
        private int _detailRowId;


        private string _acct_type;
        private string _keyvalue;
        private string _part;

        #region Constructor
        public DVOUpdateLedgerDefaults()
        {
            _rowid = 0;
            _retain_earnings = 0;//as account number
            _genjrn_doc_no = 0;
            _genjrn_post_no = 0;
            _stdent_doc_no = 0;
            _genled_post_no = 0;
            _curr_period = string.Empty;
            _curr_year = string.Empty;
            _gl_balanced = string.Empty;
            _dir_db_cr = string.Empty;
            _setup_date = "01/01/1900";//datetimein sql
            _use_batch_gen = string.Empty;
            _use_approv_post = string.Empty;
            _approval_code = string.Empty;
            _budget_checking = string.Empty;


            _period = string.Empty;
            _period_year = string.Empty;
            _start_date = string.Empty;//in sql smalldatetime
            _end_date = string.Empty;
            _balanced = string.Empty;
            _period_closed = string.Empty;
            _detailRowId = 0;


            _acct_type = string.Empty;
            _keyvalue = string.Empty;
            _part = string.Empty;
        }
        #endregion Constructor

        #region Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int retain_earnings
        {
            get { return _retain_earnings; }
            set { _retain_earnings = value; }
        }
        public int genjrn_doc_no
        {
            get { return _genjrn_doc_no; }
            set { _genjrn_doc_no = value; }
        }
        public int genjrn_post_no
        {
            get { return _genjrn_post_no; }
            set { _genjrn_post_no = value; }
        }
        public int stdent_doc_no
        {
            get { return _stdent_doc_no; }
            set { _stdent_doc_no = value; }
        }
        public int genled_post_no
        {
            get { return _genled_post_no; }
            set { _genled_post_no = value; }
        }
        public string curr_period
        {
            get { return _curr_period; }
            set { _curr_period = value; }
        }
        public string curr_year
        {
            get { return _curr_year; }
            set { _curr_year = value; }
        }
        public string gl_balanced
        {
            get { return _gl_balanced; }
            set { _gl_balanced = value; }
        }
        public string dir_db_cr
        {
            get { return _dir_db_cr; }
            set { _dir_db_cr = value; }
        }
        public string setup_date
        {
            get { return _setup_date; }
            set { _setup_date = value; }
        }
        public string use_batch_gen
        {
            get { return _use_batch_gen; }
            set { _use_batch_gen = value; }
        }
        public string use_approv_post
        {
            get { return _use_approv_post; }
            set { _use_approv_post = value; }
        }
        public string approval_code
        {
            get { return _approval_code; }
            set { _approval_code = value; }
        }
        public string budget_checking
        {
            get { return _budget_checking; }
            set { _budget_checking = value; }
        }


        public string period
        {
            get { return _period; }
            set { _period = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public string start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public string balanced
        {
            get { return _balanced; }
            set { _balanced = value; }
        }
        public string period_closed
        {
            get { return _period_closed; }
            set { _period_closed = value; }
        }
        public int detailRowId
        {
            get { return _detailRowId; }
            set { _detailRowId = value; }
        }


        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string part
        {
            get { return _part; }
            set { _part = value; }
        }
        #endregion Properties

        #region Stored-Procedures

        //***************** Added by Bharat Dhall [15 January, 2009] ********************
        public string GET_BUDGET_CHECKING
        {
            get { return "select parm_value from stxparmd where module='gl' and access_key='budget_checking'"; }
        }

        public string UPDATE_DETAIL_SPNAME
        {
            get { return "uspupdleddefdtlupd"; }
        }
        //*******************************************************************************

        public override string INSERT_SPNAME
        {
            get { return "uspLegDefIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspLegDefUpd"; }//usplegdefupd
        }

        public override string DELETE_SPNAME
        {
            get { return "uspLegDefdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspUpdLgrDefget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspLedDefGetAll"; }//uspLedDefGetAll
        }

        public string UPDATE_uspUpdLgrCtDfUpd
        {

            get { return "uspUpdLgrCtDfUpd2"; }
        }
        public override string TABLE_NAME
        {
            get { return "stgcntrc"; }
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
        public string UPDATE_STGCNTRC
        {
            get { return "uspstgcntrcupd"; }
        }
        public string UPD_GLDOCNO
        {
            get { return "uspgldefardocnoupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            if (parameters[0] != null)
            {
                if (parameters[0].ToString().Trim() == "H")
                {
                    sql = new StringBuilder();
                    sql.Append("SELECT distinct stgcntrc.RowID,retain_earnings,genjrn_doc_no,");
                    sql.Append(" genjrn_post_no,stdent_doc_no,genled_post_no,curr_period,curr_year,");
                    sql.Append(" gl_balanced,dir_db_cr,setup_date,use_batch_gen,");
                    sql.Append(" use_approv_post,approval_code,acct_type,keyvalue");
                    sql.Append(" FROM stgcntrc,outer Master_periods,outer PayrollGLAccounts");
                    sql.Append(" where stgcntrc.curr_period = Master_periods.period and stgcntrc.retain_earnings = PayrollGLAccounts.acct_no;");

                    //sql.Append("SELECT stgcntrc.curr_period, stgcntrc.curr_year, stgcntrc.dir_db_cr, stgcntrc.setup_date, stgcntrc.gl_balanced,");
                    //sql.Append("stgcntrc.use_batch_gen, stgcntrc.use_approv_post, stgcntrc.approval_code, PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_type,stgcntrc.rowid");
                    //sql.Append(" FROM stgcntrc,Master_periods,PayrollGLAccounts");
                    //sql.Append(" where stgcntrc.curr_period = Master_periods.period and stgcntrc.retain_earnings = PayrollGLAccounts.acct_no");
                }
                else if (parameters[0].ToString().Trim() == "D")
                {
                    sql = new StringBuilder();
                    sql.Append("select Master_periods.period,Master_periods.period_year,Master_periods.start_date,Master_periods.end_date,");
                    sql.Append("Master_periods.balanced,Master_periods.period_closed,Master_periods.RowId from  OUTER(stgcntrc),Master_periods where stgcntrc.curr_period=Master_periods.period");
                    sql.Append(" and Master_periods.period !='00' order by Period_year desc, period desc");
                }
            }
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}

