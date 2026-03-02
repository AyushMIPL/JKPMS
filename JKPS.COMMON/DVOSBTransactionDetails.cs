using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSBTransactionDetails : DVOBase
    {
        //Written By Rohit for Saving Banks Client Transaction Details table :- sb_details
        //Written Date :30/01/2009
        private string _acct_cat;
        private Int32 _acct_no;
        private string _acct_status;
        private string _doc_no;
        private string _tran_code;
        private string _tran_no;
        private DateTime _doc_date;
        private string _period_month;
        private string _period_year;
        private Decimal _deposit_amt;
        private Decimal _withdrawn_amt;
        private Decimal _acct_balance;
        private string _operator;
        private string _acct_no1;
        private int _Rowid;

        private decimal _avgMonthlyBalance;
        private decimal _TotalInterest;
        private int _aparrefno;
        private string _acct_id;
        private int _post_doc_no;


        #region Constructor
        public DVOSBTransactionDetails()
        {
            _acct_cat = string.Empty;
            _acct_no = 0;
            _acct_status = string.Empty;
            _doc_no = string.Empty;
            _tran_code = string.Empty;
            _tran_no = string.Empty;
            _doc_date = Convert.ToDateTime(null);
            _period_month = string.Empty;
            _period_year = string.Empty;
            _deposit_amt = Convert.ToDecimal(null);
            _withdrawn_amt = Convert.ToDecimal(null);
            _acct_balance = Convert.ToDecimal(null);
            _operator = string.Empty;
            _acct_no1 = string.Empty;
            _Rowid = 0;

            _avgMonthlyBalance = 0.0M;
            _TotalInterest = 0.0M;
            _aparrefno = 0;

            _acct_id = string.Empty;
            _post_doc_no = 0;
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

        public string doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
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
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
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
        public Decimal deposit_amt
        {
            get { return _deposit_amt; }
            set { _deposit_amt = value; }
        }
        public Decimal withdrawn_amt
        {
            get { return _withdrawn_amt; }
            set { _withdrawn_amt = value; }
        }
        public Decimal acct_balance
        {
            get { return _acct_balance; }
            set { _acct_balance = value; }
        }
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        public string acct_no1
        {
            get { return _acct_no1; }
            set { _acct_no1 = value; }
        }
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }


        public decimal avgMonthlyBalance
        {
            get { return _avgMonthlyBalance; }
            set { _avgMonthlyBalance = value; }
        }

        public decimal TotalInterest
        {
            get { return _TotalInterest; }
            set { _TotalInterest = value; }
        }
        public int aparrefno
        {
            get { return _aparrefno; }
            set { _aparrefno = value; }
        }

        public string acct_id
        {
            get { return _acct_id; }
            set { _acct_id = value; }
        }
        public int post_doc_no
        {
            get { return _post_doc_no; }
            set { _post_doc_no = value; }
        }
        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspSBPostTranIns"; }//uspsbposttranins
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
            get { return " "; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "sbposttranr"; }
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


        //public string SAVING_BANK_ACC_GET
        //  {
        //      get { return "uspsavBankTAccGet"; }
        //  }
        public string GET_UNP_TRAN_DTL
        {
            get { return "uspsbtranrdtl"; }
        }
        public string GET_P_TRAN_DTL
        {
            get { return "usppostsbtranrdtl"; }
        }

        public string CHK_IF_ALREADY_PAID
        {
            get { return "uspchkifalpaidget "; }
        }
        public string GET_DOC_NO_FROM_SBPOSTTRANR
        {
            get { return "uspsbposttranrget"; }
        }
        public string FIND_CLT_BAL
        {
            get { return "uspacctbalget"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbposttranr.acct_cat,sbposttranr.acct_no,sbposttranr.acct_status,sbposttranr.doc_no,sbposttranr.tran_type,sbposttranr.tarn_no,sbposttranr.doc_date , ");
            sql.Append(" sbposttranr.deposit_amt,sbposttranr.withdrawn_amt,sbposttranr.acct_balance,");
            sql.Append(" sbposttranr.operator,sbposttranr.acct_no ,sbposttranr.cash_received ");
            sql.Append(" FROM sbposttranr ");
            sql.Append(" WHERE 1=1 ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND sbposttranr.acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND sbposttranr.acct_no =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty && !parameters[2].ToString().Trim().Contains("1900") && !parameters[2].ToString().Trim().Contains("0001"))
                    sql.Append(" AND  sbposttranr.doc_date>=" + "'" + parameters[2].ToString().Trim() + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Trim().Contains("1900") && !parameters[3].ToString().Trim().Contains("0001"))
                    sql.Append(" AND  sbposttranr.doc_date<=" + "'" + parameters[3].ToString().Trim() + "'");
            sql.Append(" order by sbposttranr.doc_date,sbposttranr.tarn_no ");
            return sql.ToString();
        }
        public string FIND_ALL_TRANSACTIONS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT acct_cat,acct_no,acct_status,doc_no,tran_type,tran_no,doc_date , ");
            sql.Append(" deposit_amt,withdrawn_amt,acct_balance,operator,acct_no ,cash_received ");
            sql.Append(" FROM sbtranr WHERE 1=1 and ok_to_post in ('P','Y','N')  "); //Update by Rahul including ok_to_post (PNY)on 04/12/2009  //Updated By Rajeev To Include Ok_to_post

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND sbtranr.acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND sbtranr.acct_no =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty && !parameters[2].ToString().Trim().Contains("1900") && !parameters[2].ToString().Trim().Contains("0001"))
                    sql.Append(" AND  sbtranr.doc_date>=" + "'" + parameters[2].ToString().Trim() + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Trim().Contains("1900") && !parameters[3].ToString().Trim().Contains("0001"))
                    sql.Append(" AND  sbtranr.doc_date<=" + "'" + parameters[3].ToString().Trim() + "'");
            sql.Append(" order by sbtranr.doc_no,sbtranr.tran_no ");
            return sql.ToString();
        }


        public string FIND_ACCOUNTBAL_SB_POSTTRANR(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbposttranr.postdoc_no,sbposttranr.acct_cat,sbposttranr.acct_no,sbposttranr.acct_status,sbposttranr.doc_no,sbposttranr.tran_type,sbposttranr.tarn_no,sbposttranr.doc_date , ");
            sql.Append(" sbposttranr.deposit_amt,sbposttranr.withdrawn_amt,sbposttranr.acct_balance,");
            sql.Append(" sbposttranr.operator,sbposttranr.acct_no ,sbposttranr.cash_received ,sbposttranr.aparrefno ");
            sql.Append(" FROM sbposttranr ");
            sql.Append(" WHERE 1=1  ");
            //sql.Append(" sbtranr.doc_no = sbvoidr.doc_no AND");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  sbposttranr.doc_no >" + Convert.ToInt32(parameters[0].ToString()));
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim() != string.Empty && !parameters[1].ToString().Trim().Contains("1900") && !parameters[2].ToString().Trim().Contains("0001"))
                    sql.Append(" AND  sbposttranr.doc_date>=" + "'" + parameters[1].ToString().Trim() + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND sbposttranr.acct_id=" + "'" + parameters[2].ToString().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion store-procedures
    }

    /// <summary>
    /// Comparer to make DVOSBTransactionDetails class to comparable for doc_date
    /// </summary>
    public class DVOSBTransactionDetails_doc_date_Comparer : IComparer<DVOSBTransactionDetails>
    {
        public int Compare(DVOSBTransactionDetails obj1, DVOSBTransactionDetails obj2)
        {
            int returnValue = 1;
            if (obj1 != null && obj2 != null)
            {
                returnValue = obj2.doc_date.CompareTo(obj1.doc_date);
            }
            return returnValue;
        }
    }
}
