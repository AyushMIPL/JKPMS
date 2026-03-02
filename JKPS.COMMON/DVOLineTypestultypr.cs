using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 06-05-2009 get or set property of stultypr table.
    public class DVOLineTypestultypr : DVOBase
    {
        public int _RowId;
        public string _line_type;
        public string _line_desc;
        public int _gl_acct_no;
        public string _line_item_type;
        public string _update_description;
        public string _update_price;

        private string _account_type;
        private string _keyvalue;
        private string _acct_desc;

        public DVOLineTypestultypr()
        {
            _RowId = 0;
            _line_type = "";
            _line_desc = "";
            _gl_acct_no = 0;
            _line_item_type = "";
            _update_description = "";
            _update_price = "";
        }

        public int RowID
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string line_type
        {
            get { return _line_type; }
            set { _line_type = value; }
        }
        public string line_desc
        {
            get { return _line_desc; }
            set { _line_desc = value; }
        }
        public int gl_acct_no
        {
            get { return _gl_acct_no; }
            set { _gl_acct_no = value; }
        }
        public string line_item_type
        {
            get { return _line_item_type; }
            set { _line_item_type = value; }
        }
        public string update_description
        {
            get { return _update_description; }
            set { _update_description = value; }
        }
        public string update_price
        {
            get { return _update_price; }
            set { _update_price = value; }
        }

        public string account_type
        {
            get { return _account_type  ; }
            set { _account_type   = value; }
        }

        public string keyvalue
        {
            get { return _keyvalue ; }
            set { _keyvalue = value; }
        }

        public string acct_desc
        {
            get { return _acct_desc ; }
            set { _acct_desc = value; }
        }
        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usplitemTypIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usplitemTypUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usplitemTypdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspstultyprgetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "stultypr"; }
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


        public string GET_LINE_TYPE_DEFINITION_DTL
        {
            get { return "usplitemTypget"; }
        }
        //added By Rahul jain on 04-06-2009 using in Post Receipts reports
        public string SET_LIKE_TYPE
        {
            get { return "uspstultypr_curs"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select stultypr.line_type,stultypr.line_desc ,stultypr.gl_acct_no ,");
            sql.Append(" stultypr.line_item_type,stultypr.update_description,stultypr.update_price,");
            sql.Append(" PayrollGLAccounts.acct_type ,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,stultypr.rowid from stultypr,outer PayrollGLAccounts ");
            sql.Append(" where stultypr.gl_acct_no=PayrollGLAccounts.acct_no");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(line_type) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(line_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(line_item_type) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(update_description) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(update_price) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");



            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND stultypr.rowid = " + parameters[5].ToString());


            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
