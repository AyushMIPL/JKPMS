using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLWarrantDescriptionInbwarad : DVOBase
    {
        private int _DocNo;
        private int _LineNo;
        private int _Account;
        private decimal _Amount;
        private string _KeyValue;
        private string _AccountType;
        private string _Description;
        private int _AccountTypeId;

        #region Constructor

        public DVOGLWarrantDescriptionInbwarad()
        {
            _DocNo = 0;
            _LineNo = 0;
            _Account = 0;
            _Amount = 0;
            _KeyValue = string.Empty;
            _AccountType = string.Empty;
            _Description = string.Empty;
            _AccountTypeId = 0;
        }

        #endregion Constructor

        #region Properties

        public int DocNo
        {
            get { return _DocNo; }
            set { _DocNo = value; }
        }
        public int LineNo
        {
            get { return _LineNo; }
            set { _LineNo = value; }
        }
        public int Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string KeyValue
        {
            get { return _KeyValue; }
            set { _KeyValue = value; }
        }
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public int AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }

        #endregion Properties

        #region Stored-Procedur

        public override string INSERT_SPNAME
        {
            get { return "uspGLWrntDscIns"; }//uspglwrntdscins
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLWrntDscDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLWrntDscGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspGLWrntDscGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "inbwarad"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT wd.doc_no p_doc_no,lineno p_lineno,account p_account,amount p_amount,");
            sql.Append(" keyvalue p_keyvalue,acct_type p_acct_type,wd.desc p_desc,kh.id p_id");
            sql.Append(" FROM inbwarad wd,inbwarah wh,Flex_struct_Header kh ");
            sql.Append(" WHERE wd.doc_no = wh.doc_no AND Rtrim(wd.acct_type) = TRIM(kh.accounttype)");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND wd.doc_no = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND lineno = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND account = " + parameters[2].ToString());
            if (Convert.ToDouble(parameters[3]) > 0)
                sql.Append(" AND amount = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND keyvalue = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND acct_type = '" + parameters[5].ToString().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND wd.desc = '" + parameters[6].ToString().Replace("'", "''") + "'");

            return sql.ToString();
        }

        

        #endregion Stored-Procedures
    }
}
