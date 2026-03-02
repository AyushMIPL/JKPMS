using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTreasuryBillDetailInttbild : DVOBase
    {
        private int _RowID;
        private int _doc_no;
        private int _line_no;
        private string _tend_code;
        private string _tend_name;
        private decimal _amt_applied_for;
        private string _bill_status;
        private decimal _amt_issued;
        private decimal _amt_per_100;
        private int _schId;
        private int _issue_num;
        #region Constructor

        public DVOTreasuryBillDetailInttbild()
        {
            _RowID = 0;
            _doc_no = 0;
            _line_no = 0;
            _tend_code = string.Empty;
            _tend_name = string.Empty;
            _amt_applied_for = 0;
            _bill_status = string.Empty;
            _amt_issued = 0;
            _amt_per_100 = 0;
            _schId = 0;
            _issue_num = 0;
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
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
        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public string tend_name
        {
            get { return _tend_name; }
            set { _tend_name = value; }
        }
        public decimal amt_applied_for
        {
            get { return _amt_applied_for; }
            set { _amt_applied_for = value; }
        }
        public string bill_status
        {
            get { return _bill_status; }
            set { _bill_status = value; }
        }
        public decimal amt_issued
        {
            get { return _amt_issued; }
            set { _amt_issued = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public int issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }
        public int schId
        {
            get { return _schId; }
            set { _schId = value; }
        }
        #endregion public properties

        #region Stored-Procedures
        //***************By sanjay 24/06/2009 (For PayoutChecksDetail for Treasury Bill)
        public string GET_DETAIL_FOR_PARTICULAR_TEND
        {
            get { return "usp_get_detail_ten"; }
        }
        public string GET_DETAIL_FOR_TOTAL_PAY
        {
            get { return "usp_get_total_pay"; }
        }
        public string GET_CHECK_DETAIL
        {
            get { return "usp_get_chk_detail"; }
        }
        public string GET_AMT_PAID_PREVIOUSLY_ISSUE
        {
            get { return "usp_get_amt_pissue"; }
        }
        public string GET_INTEREST_AMT
        {
            get { return "usp_get_amt_pissue"; }
        }
        public string INSERT_TYPAYD
        {
            get { return "usp_typayd_instb"; }
        }
        
        //*******************
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
            get { return "uspTrsBilDtlGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }//uspTrsBilDtlGetAl
        }

        public override string TABLE_NAME
        {
            get { return "inttbild"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string FIND_ISSUED
        {
            get { return "usptbissuedget"; }
        }
        public string UPDATE_TBISSUED
        {
            get { return "usptbissuedupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT inttbild.RowID p_RowID,doc_no p_doc_no,line_no p_line_no,inttbild.tend_code p_tend_code,amt_applied_for p_amt_applied_for,");
            sql.Append(" bill_status p_bill_status,amt_issued p_amt_issued,amt_per_100 p_amt_per_100,inttendr.tend_name v_tend_name");
            sql.Append(" FROM inttbild,inttendr WHERE  inttbild.tend_code = inttendr.tend_code");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND inttbild.RowID = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND doc_no = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND line_no = " + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(inttbild.tend_code) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND amt_applied_for = " + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bill_status) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND amt_issued = " + parameters[6].ToString());
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND amt_per_100 = " + parameters[7].ToString());

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
