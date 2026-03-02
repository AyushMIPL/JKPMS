using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOARCashProcessingStrcashe : DVOBase
    {
        private int _RowId;
        private string _rcpt_date;//datetime
        private int _doc_no;
        private string _cust_code;
        private string _gross_entry;
        private string _def_mtaxcd;
        private string _check_no;
        private string _doc_desc;
        private decimal _cash_amt;
        private int _cash_acct;
        private string _cash_department;
        private string _cash_deb_cred;
        private decimal _oa_amt; 
        private int _oa_acct;
        private string _oa_department;
        private string _oa_deb_cred;
        private string _ok_to_post;
        private int _batch_id;
        private string _min_voucher_no;
        private string _tre_voucher_no;
        private int _TenderRowId;
        private int _disc_acct;
        private decimal  _disc_amt;
        private int _tend_code;
        private decimal  _dist_amt;
        private int _dist_acct;
        private decimal _oa_amtt;
        private decimal _cash_amtt;
        private string _dist_department;
        private string _disc_department;
        private string _dist_deb_cred;
        private string _disc_deb_cred;

        //Added By Rahul jain on 23-07-09
        private DateTime _dateto;
        private DateTime _dateFrom;

        String _searchOption;
        
        #region Constructor

        public DVOARCashProcessingStrcashe()
        {
            _RowId = 0;
            _rcpt_date = string.Empty;
            _doc_no = 0;
            _cust_code = string.Empty;
            _gross_entry = string.Empty;
            _def_mtaxcd = string.Empty;
            _check_no = string.Empty;
            _doc_desc = string.Empty;
            _cash_amt = 0;
            _cash_acct = 0;
            _cash_department = string.Empty;
            _cash_deb_cred = string.Empty;
            _oa_amt = 0;
            _oa_acct = 0;
            _oa_department = string.Empty;
            _oa_deb_cred = string.Empty;
            _ok_to_post = string.Empty;
            _batch_id = 0;
            _min_voucher_no = string.Empty;
            _tre_voucher_no = string.Empty;
            _TenderRowId = 0;
            _disc_acct=0;
            _disc_amt=0.0M;
            _tend_code = 0;
            _dist_amt = 0M;
            _dist_acct = 0;
            _oa_amtt = 0.0M;
            _cash_amtt = 0.0M;
            _dist_department = string.Empty;
            _disc_department = string.Empty;
            _dist_deb_cred = string.Empty;
            _disc_department = string.Empty;
           
            //Added By Rahul Jain 
            _dateFrom = Convert.ToDateTime("01/01/1900");
            _dateto = Convert.ToDateTime("01/01/1900");

            _searchOption = string.Empty;
        }

        #endregion Constructor

        #region public properties


        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int disc_acct
        {
            get { return _disc_acct; }
            set { _disc_acct = value; }
            
         
        }
        public string dist_department
        {
            get { return _dist_department; }
            set { _dist_department = value; }
          
        }
        public string disc_department
        {
            get { return _disc_department; }
            set { _disc_department = value; }

        }
        public string dist_deb_cred
        {
            get { return _dist_deb_cred; }
            set { _dist_deb_cred = value; }
        }
        public string disc_deb_cred
        {
            get { return _disc_deb_cred; }
            set { _disc_deb_cred = value; }
        }
        public decimal oa_amtt
        {
            get { return _oa_amtt; }
            set { _oa_amtt = value; }
        
        }
        public decimal cash_amtt
        {
            get { return _cash_amtt; }
            set { _cash_amtt = value; }
        }
        public int dist_acct
        {
            get { return _dist_acct; }
            set { _dist_acct = value; }


        }
        public decimal  dist_amt
        {
            get { return _dist_amt; }
            set { _dist_amt = value; }
        }
        public int tend_code
        {
            get { return _tend_code; }
            set { _tend_code = value; }
        }
        public decimal  disc_amt
        {
            get { return _disc_amt; }
            set { _disc_amt = value; }
        }
        public string rcpt_date
        {
            get { return _rcpt_date; }
            set { _rcpt_date = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public string gross_entry
        {
            get { return _gross_entry; }
            set { _gross_entry = value; }
        }
        public string def_mtaxcd
        {            get { return _def_mtaxcd; }
            set { _def_mtaxcd = value; }
        }
        public string check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }
        public string doc_desc
        {
            get { return _doc_desc; }
            set { _doc_desc = value; }
        }
        public decimal cash_amt
        {
            get { return _cash_amt; }
            set { _cash_amt = value; }
        }
        public int cash_acct
        {
            get { return _cash_acct; }
            set { _cash_acct = value; }
        }
        public string cash_department
        {
            get { return _cash_department; }
            set { _cash_department = value; }
        }
        public string cash_deb_cred
        {
            get { return _cash_deb_cred; }
            set { _cash_deb_cred = value; }
        }
        public decimal  oa_amt
        {
            get { return _oa_amt; }
            set { _oa_amt = value; }
        }
        public int oa_acct
        {
            get { return _oa_acct; }
            set { _oa_acct = value; }
        }
        public string oa_department
        {
            get { return _oa_department; }
            set { _oa_department = value; }
        }
        public string oa_deb_cred
        {
            get { return _oa_deb_cred; }
            set { _oa_deb_cred = value; }
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
        public string min_voucher_no
        {
            get { return _min_voucher_no; }
            set { _min_voucher_no = value; }
        }
        public string tre_voucher_no
        {
            get { return _tre_voucher_no; }
            set { _tre_voucher_no = value; }
        }
        public int TenderRowId
        {
            get { return _TenderRowId; }
            set { _TenderRowId = value; }
        }
        //Added By Rahul Jain on 23-07-09 using in daily cash details and daily cash summary reports
        public DateTime dateto
        {
            get { return _dateto; }
            set { _dateto = value; }
        }
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }

        public string searchOption
        {
            get { return _searchOption; }
            set { _searchOption = value; }
        }
        #endregion public properties

        #region Stored-Procedures

        public string CHECK_NUMBER_STATUS
        {
            get { return ""; }
        }
        public string INSERT_TREASURY_DOC
        {
            get { return "uspTrsrDocIns"; }
        }
        public string UPDATE_TREASURY_DOC
        {
            get { return "uspTrsrDocUpd"; }
        }
        public string DELETE_TREASURY_DOC
        {
            get { return "uspTrsrDocDel"; }
        }
        public string GET_TENDER_INFO
        {
            get { return "uspTrsrBilTndrGet"; }
        }
        public string INSERT_DOC_TENDCODE
        {
            get { return "uspDocTndCodIns"; }
        }
        public string DELETE_DOC_TENDCODE
        {
            get { return "uspDocTndCodDel"; }
        }
        public string CASH_RECEIPT_LISTING
        {
            get { return "usprptcashrcptlist"; }
        }
        public string CASH_RECEIPT_LISTING_GL
        {
            get { return "uspRptCashRcptGL"; }
        }




        public override string INSERT_SPNAME
        {
            get { return "uspArCashIns"; }
        }
        public string INSERT_SPNAME_NEW
        {
            get { return "uspArCashInsNw"; }//usparcashinsnw
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspArCashUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspArCashDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspArCashGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspArCashGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "strcashe"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string GET_REV_RECPT
        {
            get { return "usp_rev_recpt"; }
        }
        public string INS_NSSTOAR_CASHE
        {
            get { return "uspnssarcashins"; }
        }
        //***************Added By Rahul jain on 23/07/2009********************
        public string FIND_DAILY_CASH_DETAILS
        {
            get { return "uspcashdtldaily"; }
        }
        public string FIND_DAILYCASHSUM
        {
            get { return "uspdailycashsum"; }
        }
        //********************************************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rcpt_date p_rcpt_date,strcashe.doc_no p_doc_no,cust_code v_cust_code,gross_entry p_gross_entry,def_mtaxcd p_def_mtaxcd,");
            sql.Append(" check_no p_check_no,doc_desc p_doc_desc,cash_amt v_cash_amt,cash_acct v_cash_acct,cash_department v_cash_department,");
            sql.Append(" cash_deb_cred v_cash_deb_cred,oa_amt v_oa_amt,oa_acct v_oa_acct,oa_department v_oa_department,");
            sql.Append(" oa_deb_cred v_oa_deb_cred,ok_to_post p_ok_to_post,batch_id p_batch_id,min_voucher_no p_min_voucher_no,");
            sql.Append(" tre_voucher_no p_tre_voucher_no,strcashe.RowId v_RowId");//,inttbcrd.RowId v_tenderRowId
            sql.Append(" FROM strcashe WHERE 1=1");
            //sql.Append(" FROM strcashe, Outer inttbcrd WHERE strcashe.doc_no=inttbcrd.doc_no");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND strcashe.doc_no = " + parameters[0].ToString().Trim());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty && Convert.ToDateTime(parameters[1].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND rcpt_date = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null && parameters[12].ToString().Trim().Length > 0 && parameters[12].ToString().Trim().ToUpper() == "NONARCASHPROCESSINGFORM")
            {
                //if (parameters[2] != null)
                //    if (parameters[2].ToString() != string.Empty)
                //        sql.Append(" AND Rtrim(ok_to_post) NOT IN ('C')");
            }
            else
            {
                if (parameters[2] != null)
                    if (parameters[2].ToString() != string.Empty)
                        sql.Append(" AND Rtrim(ok_to_post) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                    else
                        sql.Append(" AND Rtrim(ok_to_post) NOT IN ('P','C')");
            }
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(check_no) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(min_voucher_no) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(tre_voucher_no) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(doc_desc) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(def_mtaxcd) =  '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(gross_entry) =  '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND batch_id = " + parameters[9].ToString().Trim());
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND rowId = " + parameters[10].ToString().Trim());
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND cash_acct = " + parameters[11].ToString().Trim());

            return sql.ToString();
        }

        public  string FIND_ARINFO(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rcpt_date p_rcpt_date,strcashe.doc_no p_doc_no,cust_code v_cust_code,gross_entry p_gross_entry,def_mtaxcd p_def_mtaxcd,");
            sql.Append(" check_no p_check_no,doc_desc p_doc_desc,cash_amt v_cash_amt,cash_acct v_cash_acct,cash_department v_cash_department,");
            sql.Append(" cash_deb_cred v_cash_deb_cred,oa_amt v_oa_amt,oa_acct v_oa_acct,oa_department v_oa_department,");
            sql.Append(" oa_deb_cred v_oa_deb_cred,ok_to_post p_ok_to_post,batch_id p_batch_id,min_voucher_no p_min_voucher_no,");
            sql.Append(" tre_voucher_no p_tre_voucher_no,strcashe.RowId v_RowId");
            sql.Append(" FROM strcashe WHERE 1=1 and ok_to_post<> 'C'");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND strcashe.doc_no = " + parameters[0].ToString().Trim());
            return sql.ToString();
        }
        #endregion store-procedures


    }

/// <summary>
/// Comparer to make DVOAPCheckProcessingDetailStpcashd class to comparable for dist_amt
/// </summary>
public class DVOARCashProcessingStrcashe_RowId_Comparer : IComparer<DVOARCashProcessingStrcashe>
{
    #region IComparer<DVOARCashProcessingStrcashe> Members

    public int Compare(DVOARCashProcessingStrcashe obj1, DVOARCashProcessingStrcashe obj2)
    {
        int returnValue = 1;
        if (obj1 != null && obj2 != null)
        {
            returnValue = obj1.RowId.CompareTo(obj2.RowId);
        }

        return returnValue;
    }

    #endregion
}
}
