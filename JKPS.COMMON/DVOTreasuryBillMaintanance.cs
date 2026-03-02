using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 09 June 2009
    /// Description :common class for treasury bill maintanance
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOTreasuryBillMaintanance : DVOBase
    {
        private int _Rowid;
        private int _issue_no;
        private string _issue_date;
        private string _redeem_date;
        private string _withdraw_date;
        private string _withdraw_reason;
        private decimal _amt_per_100;
        private string _status;
        private int _schemeId;
        private int _doc_no;
        private decimal _stat_limit;
        private decimal _total_details;
        private decimal _dflt_amt_per_100;
        private string _bill_status;
        private decimal _amt_issued;
        private decimal _amt_tender;
        private string _tend_code;
        private string _tend_code_to;
        private string _class_code;
        private string _tend_name;
        private string _tend_class;
        private string _address1;
        private string _address2;
        private string _city;
        private string _contact;
        private string _phone;
        private string _fax;
        private string _email;
        //For Print pay out checks
        private int _bank_acct_no;
        private int _interest_acct_no;
        private int _pay_acct_no;
        private string _def_mtaxcd;
        private int _line_no;
        //****************************
        private int _Insertby;
        private DateTime _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private DateTime _Updatedate;
        private string _UpdateMachineInfo;

        private decimal _minimum_amt;
        private decimal _maximum_amt;

        #region Constructor
        public DVOTreasuryBillMaintanance()
        {
            _Rowid = 0;
            _issue_no = 0;
            _issue_date = string.Empty;
            _redeem_date = string.Empty;
            _withdraw_date = string.Empty;
            _withdraw_reason = string.Empty;
            _amt_per_100 = 0;
            _status = string.Empty;
            _schemeId = 0;
            _doc_no = 0;
            _stat_limit = 0;
            _total_details = 0;
            _dflt_amt_per_100 = 0;
            _bill_status = "N";
            _amt_issued = 0;
            _amt_tender = 0;
            _tend_code = string.Empty;
            _tend_code_to = string.Empty;
            _class_code = string.Empty;
            _tend_name = string.Empty;
            _tend_class = string.Empty;
            _address1 = string.Empty;
            _address2 = string.Empty;
            _city = string.Empty;
            _contact = string.Empty;
            _phone = string.Empty;
            _fax = string.Empty;
            _email = string.Empty;
            //For Print pay out checks
            _bank_acct_no = 0;
            _interest_acct_no = 0;
            _pay_acct_no = 0;
            _def_mtaxcd = string.Empty;
            _line_no = 0;
            //****************************
            _Insertby = 0;
            _InsertDate = Convert.ToDateTime("01/01/1900");
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _Updatedate = Convert.ToDateTime("01/01/1900");
            _UpdateMachineInfo = string.Empty;

            _minimum_amt = 0;
            _maximum_amt = 0;
        }
        #endregion Constructor
        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int issue_no
        {
            get { return _issue_no; }
            set { _issue_no = value; }
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
        public string withdraw_date
        {
            get { return _withdraw_date; }
            set { _withdraw_date = value; }
        }
        public string withdraw_reason
        {
            get { return _withdraw_reason; }
            set { _withdraw_reason = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public int schemeId
        {
            get { return _schemeId; }
            set { _schemeId = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public decimal stat_limit
        {
            get { return _stat_limit; }
            set { _stat_limit = value; }
        }
        public decimal total_details
        {
            get { return _total_details; }
            set { _total_details = value; }
        }
        public decimal dflt_amt_per_100
        {
            get { return _dflt_amt_per_100; }
            set { _dflt_amt_per_100 = value; }
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
        public decimal amt_tender
        {
            get { return _amt_tender; }
            set { _amt_tender = value; }
        }

        public string tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public string tend_code_to
        {
            get { return _tend_code_to; }
            set { _tend_code_to = value; }
        }
        public string class_code
        {
            get { return _class_code; }
            set { _class_code = value; }
        }
        public string tend_name
        {
            get { return _tend_name; }
            set { _tend_name = value; }
        }
        public string tend_class
        {
            get { return _tend_class; }
            set { _tend_class = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        //For Print pay out checks
        public int bank_acct_no
        {
            get { return _bank_acct_no; }
            set { _bank_acct_no = value; }
        }
        public int interest_acct_no
        {
            get { return _interest_acct_no; }
            set { _interest_acct_no = value; }
        }
        public int pay_acct_no
        {
            get { return _pay_acct_no; }
            set { _pay_acct_no = value; }
        }
        public string def_mtaxcd
        {
            get { return _def_mtaxcd; }
            set { _def_mtaxcd = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        //****************************
        public int Insertby
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
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
        public DateTime Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

        public decimal minimum_amt
        {
            get { return _minimum_amt; }
            set { _minimum_amt = value; }
        }
        public decimal maximum_amt
        {
            get { return _maximum_amt; }
            set { _maximum_amt = value; }
        }
        #endregion public properties
        #region Stored-Procedures
        //For check pay out  ******************
        public string GET_DATA
        {
            get { return "usp_get_data"; }
        }
        public string GET_DATA1
        {
            get { return "usp_get_data1"; }
        }
        //***********************************
        public string INSERT_TREASURY_CLIENTS
        {
            get { return "usp_ins_tre_client"; }
        }
        public string DELETE_TREASURY_CLIENTS
        {
            get { return "usp_del_tre_client"; }
        }
        public string GET_TREASURY_CLIENTS
        {
            get { return "usp_get_tre_client"; }
        }
        public string GET_TRE_CLIENTS_NPO //Not pay out
        {
            get { return "usp_get_tre_cnpo"; }
        }
        public string GET_TREA_ISSUE_STA
        {
            get { return "usp_get_tre_status"; }
        }
        public string GET_TREA_ISSUE  //To get maximun issue number
        {
            get { return "usp_get_tre_last"; }
        }
        public string GET_TREA_MAX_ISSNO
        {
            get { return "usp_get_tre_maxissno"; }
        }
        //This is for print allotment letter
        public string GET_TREA_BILL_REC
        {
            get { return "usp_get_tre_receip"; }
        }
        public string GET_LASTISSUESTATUS
        {
            get { return "usp_get_last_sta"; }
        }
        public string Get_PrevTtbild
        {
            get { return "usp_getprevttbild"; }
        }
        //This is for print Treasury Bill Receipt
        public string GET_TR_BIL_RECEIPT
        {
            get { return "usp_get_bil_receip"; }
        }
        public string UPD_TREA_BILL_CLIENT_STATUS//update Detail table(tbissued)
        {
            get { return "usp_upd_tre_clstat"; }
        }
        public string UPD_TREA_BILL_STATUS//update main table(tbissuer)
        {
            get { return "usp_upd_tre_stat"; }
        }
        public string UPD_PREV_ISSUE_STATUS//update main table(tbissuer)
        {
            get { return "usp_pre_iss_stat"; }
        }
        //This is for Report Print Tender List
        public string GET_ISSUE_NUM
        {
            get { return "usp_get_issue_tend"; }
        }
        //This is for Report Print Allotment Letter
        public string Check_TRE_TBRECD
        {
            get { return "usp_get_tyrecd"; }
        }
        public string INSERT_TYRECD
        {
            get { return "usp_tyrecd_instb"; }
        }
        //This is for treasury Bill Withdraw
        public string INSERT_WITHDRAW_INFO
        {
            get { return "usp_ins_withdraw"; }
        }
        public string UPDATE_WITHDRAW_INFO
        {
            get { return "usp_upd_withdraw"; }
        }
        public string GET_TREASURY_CLIENTS_WITHDRAWL
        {
            get { return "usp_get_cliet_with"; }
        }
        //-----------------------------------------
        //Added by Sunil Pahwa 
         public string GET_ISSUENO_FOR_TENDER
        {
            get { return "usptbtndissget"; }
        }
        //------------------------------------------
        public override string INSERT_SPNAME
        {
            get { return "usp_ins_trea_issue"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_trea_issue"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_del_trea_issue"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usp_get_trea_issue"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "tbissuer"; }
        }
        public string FIND_TBREG
        {
            get { return "usptbregget"; }
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

        public string GET_TENDER_BY_ISSUE
        {
            get { return "usp_get_tend_code"; }
        }
        public string GET_TENDER_INFO
        {
            get { return "usp_get_tend_info"; }
        }
        public string INSRET_TBPAYD
        {
            get { return "usptbpaydins"; }
        }
        public string FIND_APCDDOCNO
        {
            get { return "usptbpaydget"; }
        }
        public string FIND_PrePaidAmt
        {
            get { return "uspppaidamt"; }
        }
        public string FIND_PAYMENTS
        {
            get { return "usppaymentsget"; }
        }
        public string FIND_TBDETAILS
        {
            get { return "usptbdetailget"; }
        }
        public string INSERT_TBPAYOUT
        {
            get { return "usptbpayoutins"; }
        }
        public string FIND_TBINFO
        {
            get { return "usptbinfoget"; }
        }
        public string FIND_PAYAMT
        {
            get { return "uspgetamtpay"; }
        }
        public string INSERT_BACK_PAY
        {
            get { return "uspbppaydins"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100 ,tbschemes.minimumamount, tbschemes.maximumamount ,tbissuer.tbschid ");
            sql.Append("from tbissuer  ,tbschemes  where tbissuer.tbschid=tbschemes.tbschid ");

            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" AND tbissuer.rowid =" + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)//issue_num
                sql.Append(" AND tbissuer.issue_num =" + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)//doc_no
                sql.Append(" AND tbissuer.doc_no =" + parameters[2].ToString());
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//status
                sql.Append(" AND tbissuer.status LIKE '" + parameters[3].ToString().Replace("'", "''") + "%'");
            if (Convert.ToDecimal(parameters[4]) > 0)//stat_limit
                sql.Append(" AND tbissuer.stat_limit =" + parameters[4].ToString());

            if (Convert.ToDecimal(parameters[5]) > 0)//dflt_amt_per_100
                sql.Append(" AND tbissuer.dflt_amt_per_100 =" + parameters[5].ToString());

            if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != null)//issue_date
                sql.Append(" AND tbissuer.issue_date >= '" + parameters[6].ToString().Replace("'", "''") + "'");
            if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//redeem_date
                sql.Append(" AND tbissuer.redeem_date <= '" + parameters[7].ToString().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (Convert.ToDecimal(parameters[8]) > 0)//tbschid
                    
                    sql.Append(" AND tbissuer.tbschid =" + parameters[8].ToString());

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }
        public string FIND_QUERY2(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.issue_date,tbissuer.status,tbissuer.redeem_date,tbissuer.issue_num");
            sql.Append(",tbissued.tend_code,tbwithdraw.withdraw_date,tbwithdraw.withdraw_reason from tbissuer,tbissued, outer(tbwithdraw) where 1=1 ");
            sql.Append(" and tbissuer.issue_num=tbissued.issue_num and tbissued.tend_code=tbwithdraw.tend_code ");
            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" AND tbissuer.rowid =" + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)//issue_num
                sql.Append(" AND tbissuer.issue_num =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)//tend_code
                    sql.Append(" AND tbissued.tend_code = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)//issue_date
                    sql.Append(" AND tbissuer.issue_date >= '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)//redeem_date
                    sql.Append(" AND tbissuer.redeem_date <= '" + parameters[4].ToString().Replace("'", "''") + "'");

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }
        //public string FIND_QUERY_2(ref Object[] parameters)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select tend_code,issue_num,amt_applied_for,amt_issued,issue_date from tbissued where bill_status='N'");
        //    if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//tend_code
        //        sql.Append(" AND tend_code =" + parameters[0].ToString());
        //    if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//issue_num
        //        sql.Append(" AND tbclients.tend_class LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");

        //    return sql.ToString();
        //}
        public string FIND_QUERY_3(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.issue_num,tbissued.amt_applied_for, tbissued.tend_code,");
            sql.Append("tbclients.tend_name, strcashe.cash_amt, strcashe.rcpt_date,");
            sql.Append("tbrecd.ar_cr_doc_no tre_voucher_no, PayrollGLAccounts.acct_desc, PayrollGLAccounts.keyvalue,tbissuer.tbschid,tbschemes.tbschname,tbclients.tend_code ");
            sql.Append("from tbissuer, tbissued, tbclients, tbrecd, strcashe, tbcntrc, PayrollGLAccounts,tbschemes , outer strcashd where 1=1");
            sql.Append(" and tbissuer.tbschid = tbissued.tbschid ");
            sql.Append(" and tbissuer.issue_num = tbissued.issue_num and tbissued.tend_code = tbclients.tend_code ");
            sql.Append(" and tbissued.issue_num = tbrecd.issue_num and tbissued.tend_code = tbrecd.tend_code ");
            sql.Append(" and tbrecd.ar_cr_doc_no = strcashe.doc_no and strcashd.doc_no = strcashe.doc_no and PayrollGLAccounts.acct_no = strcashd.dist_acct and PayrollGLAccounts.acct_no = tbschemes.deposit_acct_no "); //tbcntrc.deposit_acct_no
            sql.Append(" and tbissuer.tbschid = tbschemes.tbschid");
            //Added by Rahul On 13/11/2009 to restrict canceled tenders *************
            sql.Append(" AND tbissued.bill_status <> 'C' ");
            //**********
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" and tbissued.tend_code ='" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" and tbissuer.issue_num =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)
                    sql.Append(" and tbissuer.tbschid =" + parameters[2].ToString());
            sql.Append(" order by tbclients.tend_code");

            return sql.ToString();
        }
        public string FIND_QUERY_4(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissued.issue_num,tbissued.tend_code,tbissued.amt_applied_for,");
            sql.Append("tbissued.amt_issued,tbissued.amt_per_100, tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissued.bill_status,tbclients.tend_name,tbclients.address1,tbclients.address2,tbissuer.status,tbclients.contact,");
            sql.Append("tbschemes.tbschid,tbschemes.tbschname,tbissuer.doc_no ");
            sql.Append(" from tbclients ,tbissued,tbissuer,tbschemes ");
            sql.Append(" where tbclients.tend_code=tbissued.tend_code ");
            sql.Append(" and tbissued.issue_num=tbissuer.issue_num ");
            sql.Append(" AND tbissued.tbschid = tbissuer.tbschid ");
            sql.Append(" AND tbissuer.tbschid = tbschemes.tbschid ");
            //Added by Sarvjeet On 13/11/2009 to restrict canceled tenders *************
            sql.Append(" AND tbissued.bill_status <> 'C' ");
            //**********  
            if (parameters[0] != null && parameters[1] != null)//tend_code
                if (parameters[0].ToString().Trim().Length > 0 && parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" and tbissued.tend_code between " + parameters[0].ToString().Trim() + " and " + parameters[1].ToString().Trim());

            if (parameters[0] != null)//tend_code
            {
                if (parameters[1] == null)
                {
                    if (parameters[0].ToString().Trim().Length > 0)
                        sql.Append(" and tbissued.tend_code ='" + parameters[0].ToString().Trim() + "'");
                }
                else if (parameters[1].ToString().Trim().Length == 0)
                {
                    if (parameters[0].ToString().Trim().Length > 0)
                        sql.Append(" and tbissued.tend_code ='" + parameters[0].ToString().Trim() + "'");
                }
            }
            if (parameters[1] != null)//tend_code
            {
                if (parameters[0] == null)
                {
                    if (parameters[1].ToString().Trim().Length > 0)
                        sql.Append(" and tbissued.tend_code ='" + parameters[1].ToString().Trim() + "'");
                }
                else if (parameters[0].ToString().Trim().Length == 0)
                {
                    if (parameters[1].ToString().Trim().Length > 0)
                        sql.Append(" and tbissued.tend_code ='" + parameters[1].ToString().Trim() + "'");
                }
            }

            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) > 0)//issue_num
                    sql.Append(" and tbissued.issue_num =" + parameters[2].ToString());

            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) > 0)//issue_num
                    sql.Append(" AND tbissuer.tbschid =" + parameters[3].ToString());

            sql.Append(" order by tbissued.tend_code asc");

            return sql.ToString();
        }
        public string FIND_QUERY_5(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.issue_num,tbissued.tend_code,tbissuer.issue_date,");
            sql.Append("tbissuer.redeem_date,tbissued.amt_issued,tbissued.amt_per_100,");
            sql.Append("tbwithdraw.withdraw_date,tbwithdraw.withdraw_reason from tbissuer,tbissued,tbwithdraw where tbissuer.issue_num=tbissued.issue_num ");
            sql.Append(" and tbissued.issue_num=tbwithdraw.issue_num and tbissued.tend_code=tbwithdraw.tend_code and tbwithdraw.ok_to_post<>'C' ");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//tend_code
                sql.Append(" and tbissued.tend_code ='" + parameters[0].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[1]) > 0)//issue_num
                sql.Append(" and tbissuer.issue_num =" + parameters[1].ToString());
            sql.Append(" and tbissued.bill_status='P' order by tbissuer.issue_num desc ");
            return sql.ToString();
        }
        //Get issue no which have status N or I 
        public string FIND_QUERY_6(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100 ,tbschemes.minimumamount, tbschemes.maximumamount ,tbissuer.tbschid ");
            sql.Append("from tbissuer  ,tbschemes  where tbissuer.tbschid=tbschemes.tbschid  AND tbissuer.status In ('I') ");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }
        public string FIND_QUERY_7(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100 ,tbschemes.minimumamount, tbschemes.maximumamount ,tbissuer.tbschid ");
            sql.Append("from tbissuer  ,tbschemes  where tbissuer.tbschid=tbschemes.tbschid  AND tbissuer.status In ('I','N')");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }

        public string FIND_QUERY_8(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100 ,tbschemes.minimumamount, tbschemes.maximumamount ,tbissuer.tbschid ");
            sql.Append("from tbissuer  ,tbschemes  where tbissuer.tbschid=tbschemes.tbschid  AND tbissuer.status='R'");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }
        public string FIND_QUERY_9(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100 ,tbschemes.minimumamount, tbschemes.maximumamount ,tbissuer.tbschid ");
            sql.Append("from tbissuer  ,tbschemes  where tbissuer.tbschid=tbschemes.tbschid");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }

        //This is used to bind IssueNo. Combo in TB Register and Summary Reports.
        public string FIND_QUERY_10(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbissuer.rowid,tbissuer.doc_no,tbissuer.status,tbissuer.issue_date,tbissuer.redeem_date,");
            sql.Append("tbissuer.stat_limit,tbissuer.issue_num,tbissuer.dflt_amt_per_100 ,tbschemes.minimumamount, tbschemes.maximumamount ,tbissuer.tbschid ");
            sql.Append("from tbissuer  ,tbschemes  where tbissuer.tbschid=tbschemes.tbschid  AND tbissuer.status In ('I','R')");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)//tbschid
                    sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());

            sql.Append(" order by tbissuer.issue_num desc");
            return sql.ToString();
        }


        public string FIND_TBREGISTER(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbschemes.tbschid,tbschemes.tbschname,tbschemes.tbschdesc,tbclasses.class_code, ");
            sql.Append("tbclasses.class_desc,tbissuer.doc_no,tbissuer.issue_date,tbissuer.issue_num,");
            sql.Append("tbissuer.redeem_date,tbissued.amt_applied_for,tbissued.amt_per_100,tbclients.tend_code,tbclients.tend_name");
            sql.Append(" from tbclients, tbclasses, tbissued,tbissuer,tbschemes");
            sql.Append(" where tbclasses.class_code = tbclients.tend_class");
            sql.Append(" AND tbclients.tend_code = tbissued.tend_code");
            sql.Append(" AND tbissued.tbschid = tbissuer.tbschid");
            sql.Append(" AND tbissued.issue_num = tbissuer.issue_num");
            sql.Append(" AND tbissuer.tbschid =tbschemes.tbschid ");
            //Added by Sarvjeet On 13/11/2009 to restrict canceled tenders *************
            sql.Append(" AND tbissued.bill_status <> 'C' ");
            //**********
            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbissuer.issue_num =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND tbclasses.class_code ='" + parameters[2].ToString().Trim()+"'");

            return sql.ToString();
        }

        public string FIND_RECD(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbrecd.tbschid,tbrecd.issue_num,tbrecd.tend_code,tbrecd.ar_cr_doc_no,");
            sql.Append("strcashd.dist_amt,strcashe.rcpt_date,strcashe.tre_voucher_no ");
            sql.Append(" from tbrecd, strcashe, strcashd");
            sql.Append(" where tbrecd.ar_cr_doc_no= strcashe.doc_no");
            sql.Append(" AND strcashe.doc_no =strcashd.doc_no and (tbrecd.ok_to_post <> 'C' or tbrecd.ok_to_post is null)");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbrecd.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbrecd.issue_num =" + parameters[1].ToString());

            if (parameters.Length>2)
                if(parameters[2]!=null)
                    if(parameters[2].ToString().Trim().Length>0)
                        sql.Append(" and tbrecd.tend_code ='" + parameters[2].ToString().Trim() + "'");


            return sql.ToString();

        }

        public string FIND_PAYD(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbpayd.tbschid,tbpayd.issue_num,tbpayd.tend_code,tbpayd.ap_cd_doc_no,");
            sql.Append(" stpcashe.check_no,stpcashd.dist_amt,dist_acct");
            sql.Append(" from tbpayd,stpcashe,stpcashd");
            sql.Append(" where tbpayd.ap_cd_doc_no=stpcashe.doc_no");
            sql.Append(" and stpcashe.doc_no=stpcashd.doc_no and (tbpayd.ok_to_post<> 'C' or tbpayd.ok_to_post is null)");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbpayd.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbpayd.issue_num =" + parameters[1].ToString());

            if (parameters.Length > 2)
                if (parameters[2] != null)
                    if (parameters[2].ToString().Trim().Length > 0)
                        sql.Append(" and tbpayd.tend_code ='" + parameters[2].ToString().Trim() + "'");

            return sql.ToString();

        }

        public string FIND_TBPAYD(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select rowid, issue_num,tend_code,tbschid,ok_to_post, pay_amt from TBPAYD ");
            sql.Append(" where tbpayd.ok_to_post<> 'C' ");

            if (parameters[0] != null)
                if (Convert.ToDecimal(parameters[0]) > 0)
                    sql.Append(" AND tbpayd.tbschid =" + parameters[0].ToString());
            if (parameters[1] != null)
                if (Convert.ToDecimal(parameters[1]) > 0)
                    sql.Append(" AND tbpayd.issue_num =" + parameters[1].ToString());

            if (parameters.Length > 2)
                if (parameters[2] != null)
                    if (parameters[2].ToString().Trim().Length > 0)
                        sql.Append(" and tbpayd.tend_code ='" + parameters[2].ToString().Trim() + "'");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}

