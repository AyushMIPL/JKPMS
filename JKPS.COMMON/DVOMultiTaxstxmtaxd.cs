using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 29-07-2009 get or set property of stxmtaxd table.
    //Use in Multilevel Tax Module
    public class DVOMultiTaxstxmtaxd :DVOBase
    {
        private int _Rowid;
        private int _doc_no;
        private string _orig_journal;
        private int _inv_doc_no;
        private int _post_no;
        private string _inv_no;
        private DateTime _post_date;
        private string _mtax_code;
        private int _acct_no;
        private string _dept;
        private decimal _goods;
        private decimal _mtax_amt;
        private decimal _disc_amt;
        private string _debit_credit;

        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        private DateTime _FromDate;
        private DateTime _ToDate;

        public DVOMultiTaxstxmtaxd()
        {
            _Rowid = 0;
            _doc_no = 0;
            _orig_journal = string.Empty;
            _inv_doc_no = 0;
            _post_no = 0;
            _inv_no = string.Empty;
            _post_date = Convert.ToDateTime("01/01/1900");
            _mtax_code = string.Empty;
            _acct_no = 0;
            _dept = string.Empty;
            _goods = 0.0M;
            _mtax_amt = 0.0M;
            _disc_amt = 0.0M;
            _debit_credit = string.Empty;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
            _FromDate = Convert.ToDateTime("01/01/1900");
            _ToDate = Convert.ToDateTime("01/01/1900");
        }


        #region Public Property

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }
        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public int post_no
        {
            get { return _post_no; }
            set { _post_no = value; }
        }
        public string inv_no
        {
            get { return _inv_no; }
            set { _inv_no = value; }
        }
        public DateTime post_date
        {
            get { return _post_date; }
            set { _post_date = value; }
        }
        public string mtax_code
        {
            get { return _mtax_code; }
            set { _mtax_code = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string dept
        {
            get { return _dept; }
            set { _dept = value; }
        }
        public decimal goods
        {
            get { return _goods; }
            set { _goods = value; }
        }
        public decimal mtax_amt
        {
            get { return _mtax_amt; }
            set { _mtax_amt = value; }
        }
        public decimal disc_amt
        {
            get { return _disc_amt; }
            set { _disc_amt = value; }
        }
        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value; }
        }
        ////Properties used for only SQL Server
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

        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion

        #region Stored Procedure

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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stxmtaxd"; }
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

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }

       
        /// <summary>
        /// Find Query to get the data for report :Print Analysis Summary
        /// Created By : Rahul
        /// Created Date : 29/07/09
        /// </summary>
        public string FINDQUERY_ANALYSIS_SUMMARY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT stxmtaxd.acct_no,stxmtaxd.debit_credit,stxmtaxd.mtax_amt, ");
            sql.Append(" stxmtaxd.mtax_code,stxmtaxr.mtax_desc  ");
            sql.Append(" FROM stxmtaxd, stxmtaxr ");
            sql.Append(" WHERE stxmtaxd.mtax_code = stxmtaxr.mtax_code ");
            if (parameters[0] != null)
                if (parameters[0].ToString() == "R")
                {
                    sql.Append(" and stxmtaxd.orig_journal in ('OE','AR') ");
                }
                else if(parameters[0].ToString() =="P")
                {
                    sql.Append(" and stxmtaxd.orig_journal in ('AP','PU') ");
                }
                else
                {
                    sql.Append(" and stxmtaxd.orig_journal in ('AR','OE','AP','PU') ");
                }
            if (parameters[1] != null)
                if (!parameters[1].ToString().Contains("1900"))
                    sql.Append(" AND stxmtaxd.post_date >= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900"))
                    sql.Append(" AND stxmtaxd.post_date <= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" ORDER BY stxmtaxd.acct_no, stxmtaxd.mtax_code ");
            return sql.ToString();
        }

        /// <summary>
        /// Find Query to get the data for report :Print Analysis Summary
        /// Created By : Rahul
        /// Created Date : 29/07/09
        /// </summary>
        public string FINDQUERY_ANALYSIS_DETAIL(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT stxmtaxd.acct_no,stxmtaxd.debit_credit, ");
            sql.Append(" stxmtaxd.goods,stxmtaxd.inv_no,stxmtaxd.mtax_amt,  ");
            sql.Append(" stxmtaxd.mtax_code,stxmtaxd.post_date,stxmtaxr.mtax_desc");
            sql.Append(" FROM stxmtaxd, stxmtaxr ");
            sql.Append(" WHERE stxmtaxd.mtax_code = stxmtaxr.mtax_code ");
            if (parameters[0] != null)
                if (parameters[0].ToString() == "R")
                {
                    sql.Append(" and stxmtaxd.orig_journal in ('OE','AR') ");
                }
                else if (parameters[0].ToString() == "P")
                {
                    sql.Append(" and stxmtaxd.orig_journal in ('AP','PU') ");
                }
                else
                {
                    sql.Append(" and stxmtaxd.orig_journal in ('AR','OE','AP','PU') ");
                }
            if (parameters[1] != null)
                if (!parameters[1].ToString().Contains("1900"))
                    sql.Append(" AND stxmtaxd.post_date >= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (!parameters[2].ToString().Contains("1900")  )
                    sql.Append(" AND stxmtaxd.post_date <= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" ORDER BY stxmtaxd.acct_no, stxmtaxd.mtax_code, stxmtaxd.post_date ");
            return sql.ToString();
        }
        #endregion Stored Procedures

    }
}
