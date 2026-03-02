using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOBudgetCheckStxparmd : DVOBase
    {
        private int _RowID;
        private string _language;
        private string _module;
        private string _user_id;
        private string _access_key;
        private int _line_no;
        private string _userdef;
        private string _sbd_flag;
        private string _parm_desc;
        private string _is_rule;
        private string _is_fatal;
        private int _help_num;
        private string _val_table;
        private string _val_column;
        private string _val_filter;
        private string _val_join;
        private string _val_switchbox;
        private string _val_description;
        private string _zoom_filter;
        private string _zoom_switchbox;
        private string _parm_value;

        #region Constructor

        public DVOBudgetCheckStxparmd()
        {
            _RowID = 0;
            _language = string.Empty;
            _module = string.Empty;
            _user_id = string.Empty;
            _access_key = string.Empty;
            _line_no = 0;
            _userdef = string.Empty;
            _sbd_flag = string.Empty;
            _parm_desc = string.Empty;
            _is_rule = string.Empty;
            _is_fatal = string.Empty;
            _help_num = 0;
            _val_table = string.Empty;
            _val_column = string.Empty;
            _val_filter = string.Empty;
            _val_join = string.Empty;
            _val_switchbox = string.Empty;
            _val_description = string.Empty;
            _zoom_filter = string.Empty;
            _zoom_switchbox = string.Empty;
            _parm_value = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string language
        {
            get { return _language; }
            set { _language = value; }
        }
        public string module
        {
            get { return _module; }
            set { _module = value; }
        }
        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string access_key
        {
            get { return _access_key; }
            set { _access_key = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string userdef
        {
            get { return _userdef; }
            set { _userdef = value; }
        }
        public string sbd_flag
        {
            get { return _sbd_flag; }
            set { _sbd_flag = value; }
        }
        public string parm_desc
        {
            get { return _parm_desc; }
            set { _parm_desc = value; }
        }
        public string is_rule
        {
            get { return _is_rule; }
            set { _is_rule = value; }
        }
        public string is_fatal
        {
            get { return _is_fatal; }
            set { _is_fatal = value; }
        }
        public int help_num
        {
            get { return _help_num; }
            set { _help_num = value; }
        }
        public string val_table
        {
            get { return _val_table; }
            set { _val_table = value; }
        }
        public string val_column
        {
            get { return _val_column; }
            set { _val_column = value; }
        }
        public string val_filter
        {
            get { return _val_filter; }
            set { _val_filter = value; }
        }
        public string val_join
        {
            get { return _val_join; }
            set { _val_join = value; }
        }
        public string val_switchbox
        {
            get { return _val_switchbox; }
            set { _val_switchbox = value; }
        }
        public string val_description
        {
            get { return _val_description; }
            set { _val_description = value; }
        }
        public string zoom_filter
        {
            get { return _zoom_filter; }
            set { _zoom_filter = value; }
        }
        public string zoom_switchbox
        {
            get { return _zoom_switchbox; }
            set { _zoom_switchbox = value; }
        }
        public string parm_value
        {
            get { return _parm_value; }
            set { _parm_value = value; }
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
            get { return "uspBdtChckGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspBdtChckGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "stxparmd"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT RowID p_RowID,language p_language,module p_module,user_id p_user_id,access_key p_access_key,");
			sql.Append(" line_no p_line_no,userdef p_userdef,sbd_flag p_sbd_flag,parm_desc v_parm_desc,is_rule p_is_rule,");
			sql.Append(" is_fatal p_is_fatal,help_num p_help_num,val_table v_val_table,val_column v_val_column,");
			sql.Append(" val_filter v_val_filter,val_join v_val_join,val_switchbox v_val_switchbox,");
			sql.Append(" val_description v_val_description,zoom_filter v_zoom_filter,zoom_switchbox v_zoom_switchbox,");
			sql.Append(" parm_value p_parm_value");
		    sql.Append(" FROM stxparmd WHERE 1=1 ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND RowId = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND language = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND module = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND user_id = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND access_key = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND line_no = " + parameters[5].ToString());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND userdef = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND sbd_flag = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND is_rule = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND is_fatal = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND help_num = " + parameters[10].ToString());
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND parm_value = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");

            //sql.Append("SELECT rcpt_date p_rcpt_date,strcashe.doc_no p_doc_no,cust_code v_cust_code,gross_entry p_gross_entry,def_mtaxcd p_def_mtaxcd,");
            //sql.Append(" check_no p_check_no,doc_desc p_doc_desc,cash_amt v_cash_amt,cash_acct v_cash_acct,cash_department v_cash_department,");
            //sql.Append(" cash_deb_cred v_cash_deb_cred,oa_amt v_oa_amt,oa_acct v_oa_acct,oa_department v_oa_department,");
            //sql.Append(" oa_deb_cred v_oa_deb_cred,ok_to_post p_ok_to_post,batch_id p_batch_id,min_voucher_no p_min_voucher_no,");
            //sql.Append(" tre_voucher_no p_tre_voucher_no,strcashe.RowId v_RowId,inttbcrd.RowId v_tenderRowId");
            //sql.Append(" FROM strcashe, Outer inttbcrd WHERE strcashe.doc_no=inttbcrd.doc_no");

            //if (Convert.ToInt32(parameters[0]) > 0)
            //    sql.Append(" AND doc_no = " + parameters[0].ToString().Trim());
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty && Convert.ToDateTime(parameters[1].ToString()) != Convert.ToDateTime("01/01/1900"))
            //        sql.Append(" AND rcpt_date = '" + parameters[1].ToString().Trim() + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(ok_to_post) = '" + parameters[2].ToString().Trim() + "'");
            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(check_no) = '" + parameters[3].ToString().Trim() + "'");
            //if (parameters[4] != null)
            //    if (parameters[4].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(min_voucher_no) = '" + parameters[4].ToString().Trim() + "'");
            //if (parameters[5] != null)
            //    if (parameters[5].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(tre_voucher_no) = '" + parameters[5].ToString().Trim() + "'");
            //if (parameters[6] != null)
            //    if (parameters[6].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(doc_desc) LIKE '" + parameters[6].ToString().Trim() + "%'");
            //if (parameters[7] != null)
            //    if (parameters[7].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(def_mtaxcd) =  '" + parameters[7].ToString().Trim() + "'");
            //if (parameters[8] != null)
            //    if (parameters[8].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(gross_entry) =  '" + parameters[8].ToString().Trim() + "'");
            //if (Convert.ToInt32(parameters[9]) > 0)
            //    sql.Append(" AND batch_id = " + parameters[0].ToString().Trim());
            //if (Convert.ToInt32(parameters[10]) > 0)
            //    sql.Append(" AND rowId = " + parameters[0].ToString().Trim());

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
