using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLAccountBalance : DVOBase
    {
        private int _acct_no;
        private string _acct_type;
        private string _acct_desc;
        private string _acct_cat;
        private string _processing_seq;
        private string _incr_with_crdt;
        private string _subtotal_group;
        private string _keyvalue;
        private string _department;
        private string _period_month;
        private string _period_year;
        private decimal _activity = 0.0M;
        private decimal _balance = 0.0M;
        private decimal _this_month = 0.0M;
        private decimal _budget = 0.0M;



        #region Constructor

        public DVOGLAccountBalance()
        {
            _acct_no = 0;
            _acct_type = string.Empty;
            _acct_desc = string.Empty;
            _acct_cat = string.Empty;
            _processing_seq = string.Empty;
            _incr_with_crdt = string.Empty;
            _subtotal_group = string.Empty;
            _keyvalue = string.Empty;
            _department = string.Empty;
            _period_month = string.Empty;
            _period_year = string.Empty;
            _activity = 0.0M;
            _balance = 0.0M;
            _this_month = 0.0M;
            _budget = 0.0M;
        }

        #endregion Constructor


        #region Public Properties


        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }

        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value; }
        }

        public string processing_seq
        {
            get { return _processing_seq; }
            set { _processing_seq = value; }
        }
        public string incr_with_crdt
        {
            get { return _incr_with_crdt; }
            set { _incr_with_crdt = value; }
        }
        public string subtotal_group
        {
            get { return _subtotal_group; }
            set { _subtotal_group = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string period_month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public decimal activity
        {
            get { return _activity; }
            set { _activity = value; }
        }
        public decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public decimal this_month
        {
            get { return _this_month; }
            set { _this_month = value; }
        }
        public decimal budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
        #endregion Public Properties


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspGLinserttrialBal"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLupdatetrialBal"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLdeletetrialBal"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLMonthtrialBal"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspGLtrialMaster_Segment"; }
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
        public string GET_ACCOUNT_BAL
        {
            get { return "uspglaactbal"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PayrollGLAccounts.acct_no, PayrollGLAccounts.acct_type, PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_cat,");
            sql.Append("PayrollGLAccounts.processing_seq, PayrollGLAccounts.incr_with_crdt, PayrollGLAccounts.subtotal_group,");
            sql.Append("PayrollGLAccounts.keyvalue,Flex_struct_Header.id, Flex_struct_Header.desc, Flex_struct_Header.keylength,");
            sql.Append("Flex_struct_Header.printsafter, stxchrtd.department, stxchrtd.period_month,");
            sql.Append("stxchrtd.period_year, stxchrtd.activity, stxchrtd.balance,");
            sql.Append("stxchrtd.this_month,");
            sql.Append("stxchrtd.budget,PayrollGLAccounts_1.acct_desc AS SubtotalDesc");
            sql.Append(" FROM  Flex_struct_Header ,PayrollGLAccounts,stxchrtd, outer PayrollGLAccounts as PayrollGLAccounts_1");
            sql.Append(" Where");
            sql.Append(" PayrollGLAccounts.acct_no = stxchrtd.acct_no");
            sql.Append(" and Flex_struct_Header.accounttype = PayrollGLAccounts.acct_type");
            sql.Append(" and PayrollGLAccounts.subtotal_group = PayrollGLAccounts_1.keyvalue");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" and PayrollGLAccounts.acct_type='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and stxchrtd.period_month='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and stxchrtd.period_year='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3].ToString() != string.Empty)
                sql.Append(" and PayrollGLAccounts.keyvalue LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        //************************************CHANGES STARTS HERE *******************************
        //Added By Rohit , Have n't Used the Existing one There was a Self Join on Existing Query , I Don't need Subtotal Group for me 
        //Added on 08/02/2009
        public string GetAllMonthData(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PayrollGLAccounts.acct_no, PayrollGLAccounts.acct_type, PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_cat,");
            sql.Append("PayrollGLAccounts.processing_seq, PayrollGLAccounts.incr_with_crdt, PayrollGLAccounts.subtotal_group,");
            sql.Append("PayrollGLAccounts.keyvalue,Flex_struct_Header.id, Flex_struct_Header.desc, Flex_struct_Header.keylength,");
            sql.Append("Flex_struct_Header.printsafter, stxchrtd.department, stxchrtd.period_month,");
            sql.Append("stxchrtd.period_year, stxchrtd.activity, stxchrtd.balance,");
            sql.Append("stxchrtd.this_month,");
            sql.Append("stxchrtd.budget");
            sql.Append(" FROM  Flex_struct_Header ,PayrollGLAccounts,stxchrtd");
            sql.Append(" Where");
            sql.Append(" PayrollGLAccounts.acct_no = stxchrtd.acct_no");
            sql.Append(" and Flex_struct_Header.accounttype = PayrollGLAccounts.acct_type");
            // sql.Append(" and PayrollGLAccounts.subtotal_group = PayrollGLAccounts_1.keyvalue");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" and PayrollGLAccounts.acct_type='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                sql.Append(" and stxchrtd.period_month>='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                sql.Append(" and stxchrtd.period_month<='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                sql.Append(" and stxchrtd.period_year='" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
            {
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" and PayrollGLAccounts.keyvalue LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            }
            return sql.ToString();
        }
        //*************************************CHANGES ENDS HERE *******************************


        #endregion Stored-Procedures
    }
}

