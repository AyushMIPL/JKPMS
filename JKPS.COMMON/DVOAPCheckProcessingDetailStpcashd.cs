using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOAPCheckProcessingDetailStpcashd : DVOBase
    {
        private int _doc_no;
        private int _inv_doc_no;
        private string _inv_no;
        private string _due_date;//date
        private int _dist_acct;
        private string _dist_department;
        private decimal _dist_amt;
        private string _dist_deb_cred;
        private string _mtax_code;
        private decimal _goods_amt;
        private int _disc_acct;
        private string _disc_department;
        private decimal _disc_amt;
        private string _disc_deb_cred;
        private int _RowId;
        private decimal _pendingBalance;
        private string _paymethod;
        private string _disc_date;//date
        private int _open_doc_no;
        private string _keyvalue;
        private string _disc_keyvalue;

        private string _ok_to_post;
        private string _ForCheckRecon;

        int _insertby;
        string _insertdate;//datetime
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;//datetime
        string _updatemachineinfo;
             

        #region Constructor

        public DVOAPCheckProcessingDetailStpcashd()
        {
            _doc_no = 0;
            _inv_doc_no = 0;
            _inv_no = string.Empty;
            _due_date = "01/01/1900";//date
            _dist_acct = 0;
            _dist_department = string.Empty;
            _dist_amt = 0;
            _dist_deb_cred = string.Empty;
            _mtax_code = string.Empty;
            _goods_amt = 0;
            _disc_acct = 0;
            _disc_department = string.Empty;
            _disc_amt = 0;
            _disc_deb_cred = string.Empty;
            _RowId = 0;
            _pendingBalance = 0;
            _paymethod = string.Empty;
            _disc_date = "01/01/1900";//date
            _open_doc_no = 0;

            _ok_to_post = string.Empty;
            _ForCheckRecon = string.Empty;
            _keyvalue = string.Empty;
            _disc_keyvalue = string.Empty;

            _insertby = 0;
            _insertdate = "01/01/1900";//datetime
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//datetime
            _updatemachineinfo = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public int inv_doc_no
        {
            get { return _inv_doc_no; }
            set { _inv_doc_no = value; }
        }
        public string inv_no
        {
            get { return _inv_no; }
            set { _inv_no = value; }
        }
        public string due_date //date
        {
            get { return _due_date; }
            set { _due_date = value; }
        }
        public int dist_acct
        {
            get { return _dist_acct; }
            set { _dist_acct = value; }
        }
        public string dist_department
        {
            get { return _dist_department; }
            set { _dist_department = value; }
        }
        public decimal dist_amt
        {
            get { return _dist_amt; }
            set { _dist_amt = value; }
        }
        public string dist_deb_cred
        {
            get { return _dist_deb_cred; }
            set { _dist_deb_cred = value; }
        }
        public string mtax_code
        {
            get { return _mtax_code; }
            set { _mtax_code = value; }
        }
        public decimal goods_amt
        {
            get { return _goods_amt; }
            set { _goods_amt = value; }
        }
        public int disc_acct
        {
            get { return _disc_acct; }
            set { _disc_acct = value; }
        }
        public string disc_department
        {
            get { return _disc_department; }
            set { _disc_department = value; }
        }
        public decimal disc_amt
        {
            get { return _disc_amt; }
            set { _disc_amt = value; }
        }
        public string disc_deb_cred
        {
            get { return _disc_deb_cred; }
            set { _disc_deb_cred = value; }
        }
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public decimal pendingBalance
        {
            get { return _pendingBalance; }
            set { _pendingBalance = value; }
        }
        public string paymethod
        {
            get { return _paymethod; }
            set { _paymethod = value; }
        }
        public string disc_date
        {
            get { return _disc_date; }
            set { _disc_date = value; }
        }
        public int open_doc_no
        {
            get { return _open_doc_no; }
            set { _open_doc_no = value; }
        }

        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }

        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }

        public string disc_keyvalue
        {
            get { return _disc_keyvalue; }
            set { _disc_keyvalue = value; }
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
        public string ForCheckRecon
        {
            get { return _ForCheckRecon; }
            set { _ForCheckRecon=value; }
        }
        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_APCheckDtlIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspApCheckDtlUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspApCheckDtlDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspApCheckDtlGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }//"uspApCheckDtlGetAl"
        }

        public override string TABLE_NAME
        {
            get { return "stpcashd"; }
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

        public string INS_CASHD_DDM
        {
            get { return "uspstpcashddins"; }
        }

        //**** Added by Bharat Dhall[04/13/2009]
        public string DELETE_ALL_DETAILS
        {
            get { return "uspapchkalldtldel"; }
        }
        //**************************************

        //*******Added nu Sunil Pahwa [20/07/2009]
        public string GET_DETAIL_FOR_CHECK_STATUS
        {
            get { return "uspprtchkstatusget"; }
        }
        //****************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            if (parameters[16].ToString().Trim() == string.Empty)
            {
                sql.Append("SELECT stpcashd.doc_no p_doc_no,inv_doc_no p_inv_doc_no,stpcashd.inv_no p_inv_no,stpcashd.due_date p_due_date,dist_acct p_dist_acct,");
                sql.Append(" dist_department v_dist_department,dist_amt p_dist_amt,dist_deb_cred p_dist_deb_cred,");
                sql.Append(" mtax_code v_mtax_code,goods_amt v_goods_amt,disc_acct p_disc_acct,stpcashd.disc_department v_disc_department,");
                sql.Append(" stpcashd.disc_amt p_disc_amt,disc_deb_cred p_disc_deb_cred,stpcashd.RowId v_RowId,stpopend.balance v_balance,");
                sql.Append(" stpinvce.pay_method v_pay_method,stpopend.disc_date v_disc_date,stpopend.doc_no v_open_doc_no");
                sql.Append(" FROM stpcashd,stpcashe,outer(stpopend, OUTER stpinvce)");
                sql.Append(" WHERE stpcashd.doc_no=stpcashe.doc_no AND inv_doc_no = stpopend.doc_no AND stpopend.doc_no=stpinvce.doc_no");

                //if (parameters[0] != null)
                //    if (parameters[0].ToString() != string.Empty)
                sql.Append(" AND stpcashe.vend_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
                //if (parameters[1] != null)
                //    if (parameters[1].ToString() != string.Empty)
                sql.Append(" AND stpcashe.pay_to_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                //if (Convert.ToInt32(parameters[2]) > 0)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stpcashe.cash_acct = " + parameters[2].ToString());
                ////if (parameters[3] != null)
                ////    if (parameters[3].ToString() != string.Empty)
                //sql.Append(" AND TRIM(stpcashe.cash_department) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

                //if (Convert.ToInt32(parameters[4]) != 0)
                if (parameters[4] != null)
                    if (parameters[4].ToString().Trim() != string.Empty)
                        sql.Append(" AND stpcashd.doc_no = " + parameters[4].ToString());
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND inv_doc_no = " + parameters[5].ToString());
                if (parameters[6] != null)
                    if (parameters[6].ToString() != string.Empty)
                        sql.Append(" AND TRIM(stpcashd.inv_no) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[7] != null)
                    if (parameters[7].ToString() != string.Empty && !parameters[7].ToString().Trim().Contains("1900"))
                        sql.Append(" AND stpcashd.due_date = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[8]) > 0)
                    sql.Append(" AND dist_acct = " + parameters[8].ToString());
                if (Convert.ToSingle(parameters[9]) > 0)
                    sql.Append(" AND dist_amt = " + parameters[9].ToString());
                if (parameters[10] != null)
                    if (parameters[10].ToString() != string.Empty)
                        sql.Append(" AND TRIM(dist_deb_cred) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[11]) > 0)
                    sql.Append(" AND disc_acct = " + parameters[11].ToString());
                if (Convert.ToSingle(parameters[12]) > 0)
                    sql.Append(" AND disc_amt = " + parameters[12].ToString());
                if (parameters[13] != null)
                    if (parameters[13].ToString() != string.Empty)
                        sql.Append(" AND TRIM(disc_deb_cred) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToSingle(parameters[14]) > 0)
                    sql.Append(" AND RowId = " + parameters[14].ToString());

                if (parameters[15] != null)
                    if (parameters[15].ToString() != string.Empty)
                        sql.Append(" AND TRIM(stpcashe.ok_to_post) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
                    else
                        sql.Append(" AND TRIM(stpcashe.ok_to_post) NOT IN ('P','C')");
            }
            else if (parameters[16].ToString().Trim() == "CheckVendorInvoice")
            {
                sql.Append("SELECT Count(*) FROM stpcashd,stpcashe");
                sql.Append(" WHERE stpcashd.doc_no=stpcashe.doc_no ");

                sql.Append(" AND stpcashe.vend_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND stpcashd.doc_no = " + parameters[4].ToString());
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND inv_doc_no = " + parameters[5].ToString());
                if (parameters[6] != null)
                    if (parameters[6].ToString() != string.Empty)
                        sql.Append(" AND TRIM(stpcashd.inv_no) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[7] != null)
                    if (parameters[7].ToString() != string.Empty && !parameters[7].ToString().Trim().Contains("1900"))
                        sql.Append(" AND stpcashd.due_date = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[8]) > 0)
                    sql.Append(" AND dist_acct = " + parameters[8].ToString());
                if (Convert.ToSingle(parameters[9]) > 0)
                    sql.Append(" AND dist_amt = " + parameters[9].ToString());
                if (parameters[10] != null)
                    if (parameters[10].ToString() != string.Empty)
                        sql.Append(" AND TRIM(dist_deb_cred) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[11]) > 0)
                    sql.Append(" AND disc_acct = " + parameters[11].ToString());
                if (Convert.ToSingle(parameters[12]) > 0)
                    sql.Append(" AND disc_amt = " + parameters[12].ToString());
                if (parameters[13] != null)
                    if (parameters[13].ToString() != string.Empty)
                        sql.Append(" AND TRIM(disc_deb_cred) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToSingle(parameters[14]) > 0)
                    sql.Append(" AND RowId = " + parameters[14].ToString());

                //if (parameters[15] != null)
                //    if (parameters[15].ToString() != string.Empty)
                //        sql.Append(" AND TRIM(stpcashe.ok_to_post) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
                //    else
                //        sql.Append(" AND TRIM(stpcashe.ok_to_post) NOT IN ('P','C')");
            }
            else if (parameters[16].ToString().Trim() == "CheckReconciliation")
            {
                sql.Append("SELECT stpcashd.doc_no p_doc_no,inv_doc_no p_inv_doc_no,stpcashd.inv_no p_inv_no,stpcashd.due_date p_due_date,dist_acct p_dist_acct,");
                sql.Append(" dist_department v_dist_department,dist_amt p_dist_amt,dist_deb_cred p_dist_deb_cred,");
                sql.Append(" mtax_code v_mtax_code,goods_amt v_goods_amt,disc_acct p_disc_acct,stpcashd.disc_department v_disc_department,");
                sql.Append(" stpcashd.disc_amt p_disc_amt,disc_deb_cred p_disc_deb_cred,stpcashd.RowId v_RowId,stpopend.balance v_balance,");
                sql.Append(" stpinvce.pay_method v_pay_method,stpopend.disc_date v_disc_date,stpopend.doc_no v_open_doc_no");
                sql.Append(" FROM stpcashd,stpcashe,outer(stpopend, OUTER stpinvce)");
                sql.Append(" WHERE stpcashd.doc_no=stpcashe.doc_no AND inv_doc_no = stpopend.doc_no AND stpopend.doc_no=stpinvce.doc_no");

                if (parameters[0] != null)
                    if (parameters[0].ToString() != string.Empty)
                sql.Append(" AND stpcashe.vend_code = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
        if (parameters[1] != null)
            if (parameters[1].ToString() != string.Empty)
                sql.Append(" AND stpcashe.pay_to_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
        if (Convert.ToInt32(parameters[2]) > 0)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stpcashe.cash_acct = " + parameters[2].ToString());
                ////if (parameters[3] != null)
                ////    if (parameters[3].ToString() != string.Empty)
                //sql.Append(" AND TRIM(stpcashe.cash_department) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

             
                if (parameters[4] != null)
                    if (parameters[4].ToString().Trim() != string.Empty)
                        sql.Append(" AND stpcashd.doc_no = " + parameters[4].ToString());
                if (Convert.ToInt32(parameters[5]) > 0)
                    sql.Append(" AND inv_doc_no = " + parameters[5].ToString());
                if (parameters[6] != null)
                    if (parameters[6].ToString() != string.Empty)
                        sql.Append(" AND TRIM(stpcashd.inv_no) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[7] != null)
                    if (parameters[7].ToString() != string.Empty && !parameters[7].ToString().Trim().Contains("1900"))
                        sql.Append(" AND stpcashd.due_date = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[8]) > 0)
                    sql.Append(" AND dist_acct = " + parameters[8].ToString());
                if (Convert.ToSingle(parameters[9]) > 0)
                    sql.Append(" AND dist_amt = " + parameters[9].ToString());
                if (parameters[10] != null)
                    if (parameters[10].ToString() != string.Empty)
                        sql.Append(" AND TRIM(dist_deb_cred) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToInt32(parameters[11]) > 0)
                    sql.Append(" AND disc_acct = " + parameters[11].ToString());
                if (Convert.ToSingle(parameters[12]) > 0)
                    sql.Append(" AND disc_amt = " + parameters[12].ToString());
                if (parameters[13] != null)
                    if (parameters[13].ToString() != string.Empty)
                        sql.Append(" AND TRIM(disc_deb_cred) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
                if (Convert.ToSingle(parameters[14]) > 0)
                    sql.Append(" AND RowId = " + parameters[14].ToString());

                if (parameters[15] != null)
                    if (parameters[15].ToString() != string.Empty)
                        sql.Append(" AND TRIM(stpcashe.ok_to_post) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
                    else
                        sql.Append(" AND TRIM(stpcashe.ok_to_post) NOT IN ('P','C')");
            }
            return sql.ToString();
        }
        #endregion store-procedures
    }

    /// <summary>
    /// Comparer to make DVOAPCheckProcessingDetailStpcashd class to comparable for dist_amt
    /// </summary>
    public class DVOAPCheckProcessingDetailStpcashd_DistAmt_Comparer : IComparer<DVOAPCheckProcessingDetailStpcashd>
    {
        #region IComparer<Student> Members

        public int Compare(DVOAPCheckProcessingDetailStpcashd obj1, DVOAPCheckProcessingDetailStpcashd obj2)
        {
            int returnValue = 1;
            if (obj1 != null && obj2 != null)
            {
                returnValue = obj2.dist_amt.CompareTo(obj1.dist_amt);
            }

            return returnValue;
        }

        #endregion
    }
}
