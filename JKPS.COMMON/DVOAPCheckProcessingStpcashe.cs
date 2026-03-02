using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAPCheckProcessingStpcashe : DVOBase
    {
        private string _chk_date;//datetime
        private int _doc_no;
        private string _vend_code;
        private string _pay_to_code;
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
        private string _print_chk;
        private string _ok_to_post;
        private string _chk_printed;
        private string _ap_type;
        private int _batch_id;
        private string _min_voucher_no;
        private string _tre_voucher_no;
        private string _bus_name;
        private int _required_approval;
        private int _current_approval;
        private int _acd_id;
        private int _RowId;
        private string _keyvalue;
        private string _account_type;

        string _entry_date;//datetime
        string _collected_by;
        string _collected_date;//datetime
        int _printed_by;
        string _printed_date;//datetime
        int _insertby;
        string _insertdate;//datetime
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;//datetime
        string _updatemachineinfo;

        //Added by Sarvjeet Verma...
        private DateTime _start_date;
        private DateTime _end_date;
        private int _doc_noTo;
        private string _check_noTo;


        #region Constructor

        public DVOAPCheckProcessingStpcashe()
        {
            _chk_date = string.Empty;
            _doc_no = 0;
            _vend_code = string.Empty;
            _pay_to_code = string.Empty;
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
            _print_chk = string.Empty;
            _ok_to_post = string.Empty;
            _chk_printed = string.Empty;
            _ap_type = string.Empty;
            _batch_id = 0;
            _min_voucher_no = string.Empty;
            _tre_voucher_no = string.Empty;
            _bus_name = string.Empty;
            _required_approval = 0;
            _current_approval = 0;
            _acd_id = 0;
            _RowId = 0;
            _keyvalue = string.Empty;
            _account_type = string.Empty;

            _entry_date = "01/01/1900";//datetime
            _collected_by = string.Empty;
            _collected_date = "01/01/1900";//datetime
            _printed_by = 0;
            _printed_date = "01/01/1900";//datetime
            _insertby = 0;
            _insertdate = "01/01/1900";//datetime
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//datetime
            _updatemachineinfo = string.Empty;

            _start_date = Convert.ToDateTime(null);
            _end_date = Convert.ToDateTime(null);
            _doc_noTo = 0;
            _check_noTo = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public string chk_date
        {
            get { return _chk_date; }
            set { _chk_date = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string pay_to_code
        {
            get { return _pay_to_code; }
            set { _pay_to_code = value; }
        }
        public string gross_entry
        {
            get { return _gross_entry; }
            set { _gross_entry = value; }
        }
        public string def_mtaxcd
        {
            get { return _def_mtaxcd; }
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
        public decimal oa_amt
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
        public string print_chk
        {
            get { return _print_chk; }
            set { _print_chk = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public string chk_printed
        {
            get { return _chk_printed; }
            set { _chk_printed = value; }
        }
        public string ap_type
        {
            get { return _ap_type; }
            set { _ap_type = value; }
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
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }
        public int required_approval
        {
            get { return _required_approval; }
            set { _required_approval = value; }
        }
        public int current_approval
        {
            get { return _current_approval; }
            set { _current_approval = value; }
        }
        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }

        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string account_type
        {
            get { return _account_type; }
            set { _account_type = value; }
        }
        public string entry_date
        {
            get { return _entry_date; }
            set { _entry_date = value; }
        }
        public string collected_by
        {
            get { return _collected_by; }
            set { _collected_by = value; }
        }
        public string collected_date
        {
            get { return _collected_date; }
            set { _collected_date = value; }
        }
        public int printed_by
        {
            get { return _printed_by; }
            set { _printed_by = value; }
        }
        public string printed_date
        {
            get { return _printed_date; }
            set { _printed_date = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }



        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public DateTime end_date
        {
            get { return _end_date; }
            set { _end_date = value; }

        }
        public int doc_no_to
        {
            get { return _doc_noTo; }
            set { _doc_noTo = value; }
        }
        public string check_no_to
        {
            get { return _check_noTo; }
            set { _check_noTo = value; }
        }

        #endregion public properties

        #region Stored-Procedures
        //******************By sanjay****************************//
        public string GET_CHECK_PRINTING
        {
            get { return "uspCheckprinting"; }
        }
        public string UPD_CHECK_PRINT_STS
        {
            get { return "uspupdchkPrtSts"; }
        }
        //*********************************************************
        public string CHECK_DATE_STATUS
        {
            get { return "uspCheckDatExist"; }
        }

        public string CHECK_NUMBER_STATUS
        {
            get { return "uspCheckNmbrExist"; }
        }

        public string GET_ONACCOUNT_AMOUNT
        {
            get { return "uspVndOnActAmtGet"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspApCheckIns"; }
        }

        public string INSERT_SPNAME_NEW
        {
            get { return "USP_APCheckInsNw"; }//uspapcheckinsnw
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspApCheckUpd"; }
        }

        public string UPDATE_REQUIRED_APPROVAL
        {
            get { return "uspApChckReqAprUpd"; }
        }

        public string CHECK_RELATE_TO_DOC
        {
            get { return "uspApChckRelateDoc"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspApCheckDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspApCheckGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspApCheckGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "stpcashe"; }
        }
        public string INSERT_APCHECKRECORD
        {
            get { return "uspapchksrcdIns"; }
        }
        
        public string GET_NONAPCHECKPRINT
        {
            get { return "UspNonAPChkPrint"; }
        }
        //***Added by Sunil Pahwa For UnPrinted AP/NonAP Checks[28/07/2009]
        public string GET_UNP_NONAPCHECKPRINT
        {
            get { return "uspunpnonapchkprnt"; }
        }
        public string UPD_UNP_CHECKPRINT
        {
            get { return "uspupdunpapchksts"; }//
        }
        public string GET_UNP_APCHECKPRINT
        {
            get { return "uspunpapchkprnt"; }
        }

        public string GET_UNCLEAR_CHECKS
        {
            get { return "uspunclearchkget"; }
        }
        //*END**********************************************************

        public string GET_APCHECK_POSTDATED
        {
            get { return "UspAPChkPrintPstdt"; }
        }
        public string GET_NONAPCHK_POSTDATED
        {
            get { return "UspNonAPChkPstDt"; }
        }
        public string GET_APCHECKPRINT
        {
            get { return "UspAPChkPrint"; }
        }
        public string GET_APDUPCHECKPRINT
        {
            get { return "uspapdupchkprint"; }
        }
        public string RESET_CHECKNO
        {
            get { return "uspresetcheck"; }
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

        public string GET_DUPCHECKS
        {
            get { return "UspDupChkPrint"; }
        }
        public string INS_CASHE_DDM
        {
            get { return "uspstpcasheins"; }
        }
        public string GET_VARIFYCHECK
        {
            get { return "uspverifycheck"; }
        }
        public string GET_CHECK_JRNL
        {
            get { return "uspchkjrnlget"; }
        }
        public string GET_Bus_Name
        {
            get { return "usp_bus_name"; }
        }

        //Added by Sunil Pahwa for check status
        public string GET_INFO_FOR_CHECK_STATUS
        {
            get { return "uspchkstatusget"; }
        }
        public string UPD_INFO_FOR_CHECK_STATUS
        {
            get { return "uspchkstatusupd"; }
        }

        public string UPD_INFO_IN_STPCASHE
        {
            get { return "uspprtchkstatusupd"; }
        }

        public string GET_COUNT_FROM_STPCASHE
        {
            get { return "uspchkstpcasheget"; }
        }
        
        //*END*******************

        public string GET_CHK_PRINTED
        {
            get { return "uspchkprintedget"; }

        }
        public string GET_CHK_PRINTED_DTL
        {
            get { return "uspchkdetlget"; }
        }
        public string GET_CHK_PRINTED_ALL
        {
            get { return "uspchkprintedall"; }
        }
        public string INSERT_UPDATE_RELEASE_APCHECK_PRINT
        {
            get { return "uspapchkinsur"; }
        }

        public string SHOW_AP_CHECK_ONE_PROC
        {
            get { return "uspapchkshowds"; }
        }

        public string PRINT_AP_CHECK_ONE_PROC
        {
            get { return "uspapchkprintds"; }
        }
        //********************************ROHIT
        public string GETNONAPCheck
        {
            get { return "uspprintchk"; }
        }
        //*****************************ROHIT

        //Added by rajeev
        public string GetCheckPrintStatus
        {
            get { return "uspchkptstatus"; }
        }

        public string GET_CHECK_BY_CHECKNO
        {
            get { return "uspbychknoget"; }
        }

        public string INSERT_UPDATE_CHECKRECORD_AND_STPCASHE
        {
            get { return "uspapchkinsupd"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT chk_date p_chk_date,doc_no p_doc_no,vend_code p_vend_code,pay_to_code p_pay_to_code,");
            sql.Append(" gross_entry v_gross_entry,def_mtaxcd v_def_mtaxcd,check_no p_check_no,doc_desc p_doc_desc,");
            sql.Append(" cash_amt v_cash_amt,cash_acct v_cash_acct,cash_department v_cash_department,");
            sql.Append(" cash_deb_cred v_cash_deb_cred,oa_amt v_oa_amt,oa_acct v_oa_acct,oa_department v_oa_department,");
            sql.Append(" oa_deb_cred v_oa_deb_cred,print_chk p_print_chk,ok_to_post p_ok_to_post,chk_printed v_chk_printed,");
            sql.Append(" ap_type v_ap_type,batch_id p_batch_id,min_voucher_no v_min_voucher_no,tre_voucher_no v_tre_voucher_no,");
            sql.Append(" bus_name v_bus_name,required_approval v_required_aproval,current_approval v_current_approval,");
            sql.Append(" acd_id v_acd_id,stpcashe.RowId v_RowId,stxchrtr.keyvalue v_keyvalue");//,entry_date v_entry_date,");
            //sql.Append(" collected_by v_collected_by,collected_date v_collected_date,printed_by v_printed_by,printed_date v_printed_date");
            sql.Append(" FROM stpcashe,stxchrtr WHERE stpcashe.cash_acct=stxchrtr.acct_no");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND chk_date = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(print_chk) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(ok_to_post) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
                else
                    sql.Append(" AND TRIM(ok_to_post) NOT IN ('P','C')");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(vend_code) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(pay_to_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(check_no) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(min_voucher_no) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(tre_voucher_no) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(doc_desc) LIKE '" + parameters[9].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND batch_id = " + parameters[10].ToString());
            if (parameters[11] != null)
                if (parameters[11].ToString().Trim().Length > 0)
                    sql.Append(" AND TRIM(ap_type) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[12]) > 0)
                sql.Append(" AND stpcashe.RowId = " + parameters[12].ToString());
            if (parameters[13] != null)
                if (parameters[13].ToString().Trim().Length > 0)
                    sql.Append(" AND stpcashe. bus_name LIke'%" + parameters[13].ToString().Trim().Replace("'", "''") + "%'");
            ////if (Convert.ToInt32(parameters[13]) > 0)
            //sql.Append(" AND current_approval = " + parameters[13].ToString());
            //if (Convert.ToInt32(parameters[14]) > 0)
            //    sql.Append(" AND required_approval = " + parameters[14].ToString());
            //if (parameters[15] != null)
            //    if (parameters[15].ToString() != string.Empty)
            //        sql.Append(" AND TRIM(stxchrtr.acct_type) = '" + parameters[15].ToString().Trim() + "'");
            //     sql.Append("order by doc_no");
            return sql.ToString();
        }

        //Added By Rahul Jain 14/12/2009 
        public string FIND_DATA_BY_DOCNO(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT chk_date p_chk_date,doc_no p_doc_no,vend_code p_vend_code,pay_to_code p_pay_to_code,");
            sql.Append(" gross_entry v_gross_entry,def_mtaxcd v_def_mtaxcd,check_no p_check_no,doc_desc p_doc_desc,");
            sql.Append(" cash_amt v_cash_amt,cash_acct v_cash_acct,cash_department v_cash_department,");
            sql.Append(" cash_deb_cred v_cash_deb_cred,oa_amt v_oa_amt,oa_acct v_oa_acct,oa_department v_oa_department,");
            sql.Append(" oa_deb_cred v_oa_deb_cred,print_chk p_print_chk,ok_to_post p_ok_to_post,chk_printed v_chk_printed,");
            sql.Append(" ap_type v_ap_type,batch_id p_batch_id,min_voucher_no v_min_voucher_no,tre_voucher_no v_tre_voucher_no,");
            sql.Append(" bus_name v_bus_name,required_approval v_required_aproval,current_approval v_current_approval,");
            sql.Append(" acd_id v_acd_id,stpcashe.RowId v_RowId,stxchrtr.keyvalue v_keyvalue");//,entry_date v_entry_date,");
            //sql.Append(" collected_by v_collected_by,collected_date v_collected_date,printed_by v_printed_by,printed_date v_printed_date");
            sql.Append(" FROM stpcashe,stxchrtr WHERE stpcashe.cash_acct=stxchrtr.acct_no");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no = " + parameters[0].ToString());
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
