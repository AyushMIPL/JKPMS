using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOTreasuryBillIssueNumbersIntissur : DVOBase
    {
        private int _RowID;
        private int _doc_no;
        private string _status;
        private string _issue_date;// datetime,
        private string _redeem_date;// datetime,
        private decimal _stat_limit; 
        private decimal _dflt_amt_per_100;
        private decimal _total_details;
        private int _schId;
        private int _issue_num;
        //Added By Rajeev
        //Aim: To Set the Filter Criteria for Print TreasuryBill With Interest
        private string _tend_code;

        #region Constructor

        public DVOTreasuryBillIssueNumbersIntissur()
        {
            _RowID = 0;
            _doc_no = 0;
            _status = string.Empty;
            _issue_date = "01/01/1900";// datetime,
            _redeem_date = "01/01/1900";// datetime,
            _stat_limit = 0;
            _issue_num = 0;
            _dflt_amt_per_100 = 0;
            _total_details = 0;
            _schId = 0;
            _tend_code = string.Empty;
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
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string issue_date
        {
            get { return _issue_date; }
            set { _issue_date = value; }
        }
        public string redeem_date
        {
            get { return _redeem_date; }
            set { _redeem_date = value; }
        }
        public decimal stat_limit
        {
            get { return _stat_limit; }
            set { _stat_limit = value; }
        }
        public int issue_num
        {
            get { return _issue_num; }
            set { _issue_num = value; }
        }
        public decimal dflt_amt_per_100
        {
            get { return _dflt_amt_per_100; }
            set { _dflt_amt_per_100 = value; }
        }

        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public decimal total_details
        {
            get { return _total_details; }
            set { _total_details = value; }

        }
        public int schId
        {
            get { return _schId; }
            set { _schId = value; }
        }
        #endregion public properties

        #region Stored-Procedures

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
            get { return "uspTrsBilIsuNmbGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }//uspTrsBilIsuNmbAll
        }

        public override string TABLE_NAME
        {
            get { return "intissur"; }
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
        public string FIND_ISSUES
        {
            get { return "usptbissuerget"; }
        }
        public string UPDATE_ISSUER
        {
            get { return "usptbissuerupd"; }
        }
        public string GET_PRE_ISSUE
        {
            get { return "uspprevissue"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT RowID p_RowID,doc_no p_doc_no,status p_status,issue_date p_issue_date,redeem_date p_redeem_date,");
            sql.Append(" stat_limit p_stat_limit,issue_num p_issue_num,dflt_amt_per_100 p_dflt_amt_per_100");
            sql.Append(" FROM intissur WHERE 1=1");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append("AND RowID = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append("AND doc_no = " + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append("AND Rtrim(status) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && Convert.ToDateTime(parameters[3].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append("AND issue_date = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && Convert.ToDateTime(parameters[4].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append("AND redeem_date = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append("AND stat_limit = " + parameters[5].ToString());
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append("AND issue_num = " + parameters[6].ToString());
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append("AND dflt_amt_per_100 = " + parameters[7].ToString());

            return sql.ToString();
        }


        
        /// <summary>
        /// Added By Rajeev 
        /// Aim : To Print Treasury Bill issue With Interest
        /// Date : 31/08/2009
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_TREASURYBILL_WITH_INTEREST(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("select tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append(" tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100,");
            sql.Append(" tbissued.tend_code,tbissued.amt_applied_for,tbissued.bill_status,");
            sql.Append(" tbissued.amt_issued,tbissued.amt_per_100,tbclients.tend_name,tbclients.address1,");
            sql.Append(" tbclients.address2,tbclients.contact,tbclients.phone,tbclients.fax,tbclients.tend_class,");
            sql.Append(" tbissuer.tbschid,tbschemes.tbschname");
            sql.Append(" from tbissuer,tbissued,tbclients,tbschemes");
            sql.Append(" where tbissuer.tbschid=tbissued.tbschid");
            sql.Append(" and tbissuer.issue_num=tbissued.issue_num");
            sql.Append(" and tbclients.tend_code=tbissued.tend_code");
            sql.Append(" and tbissuer.tbschid=tbschemes.tbschid");
            sql.Append(" and tbissued.bill_status <> 'C'");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND tbissued.tbschid = " + parameters[0].ToString());
           
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND tbissued.issue_num = " + parameters[1].ToString());

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND tbissued.tend_code ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            //sql.Append("select intissur.doc_no,intissur.status,intissur.issue_date,intissur.redeem_date,intissur.stat_limit,");
            //sql.Append(" intissur.issue_num,intissur.dflt_amt_per_100,");
            //sql.Append(" inttbild.line_no,inttbild.tend_code,inttbild.amt_applied_for,inttbild.bill_status,");
            //sql.Append(" inttbild.amt_issued,inttbild.amt_per_100,");
            //sql.Append(" inttendr.tend_name,inttendr.address1,inttendr.address2,inttendr.contact,");
            //sql.Append(" inttendr.phone,inttendr.fax,inttendr.class_code");
            //sql.Append(" from intissur,inttbild,inttendr ");
            //sql.Append(" where intissur.doc_no=inttbild.doc_no and inttendr.tend_code=inttbild.tend_code");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString().Trim().Length > 0)
            //        sql.Append(" AND inttbild.tend_code ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[1] != null)
            //    if (Convert.ToInt32(parameters[1]) > 0)
            //        sql.Append(" AND intissur.issue_num = " + parameters[1].ToString());
            //if (parameters[2] != null)


            return sql.ToString();
        }
        #endregion store-procedures
    }
}
