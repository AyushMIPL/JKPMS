using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPayableListingDetailStpinvcd : DVOBase
    {
        private int _doc_no;
        private int _line_no;
        private int _acct_no;
        private string _department;
        private decimal _amount;
        private string _debit_credit;
        private string _mtax_code;
        private decimal _goods_amt;
        private int _RowId;

        private string _account_type;
        private int _account_typeId;
        private string _account_description;
        private string _account_category;
        private string _keyvalue;

        #region Constructor

        public DVOPayableListingDetailStpinvcd()
        {
            _doc_no = 0;
            _line_no = 0;
            _acct_no = 0;
            _department = string.Empty;
            _amount = 0;
            _debit_credit = string.Empty;
            _mtax_code = string.Empty;
            _goods_amt = 0;
            _RowId = 0;

            _account_type = string.Empty;
            _account_typeId = 0;
            _account_description = string.Empty;
            _account_category = string.Empty;
            _keyvalue = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

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
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value; }
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
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }

        public string account_type
        {
            get { return _account_type; }
            set { _account_type = value; }
        }
        public int account_typeId
        {
            get { return _account_typeId; }
            set { _account_typeId = value; }
        }
        public string account_description
        {
            get { return _account_description; }
            set { _account_description = value; }
        }
        public string account_category
        {
            get { return _account_category; }
            set { _account_category = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usppaylistDtlIns"; }
        }

        public override string  UPDATE_SPNAME
        {
            get { return "usppaylistDtlUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppaylistDtlDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usppaylistDtlGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "usppaylistDtlGetAl"; }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT doc_no AS p_doc_no, line_no AS p_line_no, stpinvcd.acct_no AS p_acct_no, department AS p_department, amount AS p_amount,");
            sql.Append(" debit_credit AS p_debit_credit, mtax_code AS p_mtax_code, goods_amt AS p_goods_amt,PayrollGLAccounts.acct_type AS v_acct_type,");
            sql.Append(" PayrollGLAccounts.acct_desc AS v_acct_desc,PayrollGLAccounts.acct_cat AS v_acct_cat,PayrollGLAccounts.keyvalue AS v_keyvalue,Flex_struct_Header.Id v_acct_typeid,stpinvcd.RowId v_rowid");
            sql.Append(" FROM stpinvcd,PayrollGLAccounts,Flex_struct_Header");
            sql.Append(" WHERE stpinvcd.acct_no = PayrollGLAccounts.acct_no and PayrollGLAccounts.acct_type=Flex_struct_Header.accounttype");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND line_no = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND stpinvcd.acct_no = " + parameters[2].ToString());
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(department) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND amount = " + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(debit_credit) ='" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mtax_Code) ='" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND goods_amt = " + parameters[7].ToString());

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
