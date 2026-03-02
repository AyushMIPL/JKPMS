using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterBankDetails : DVOBase
    {
        int _rowid;
        string _bank_code;//char(10)
        string _media_str;//char(1000),
        int _insertby;
        string _insertdate;//datetime
        string _insertmachineinfo;//char(50)
        int _updateby;
        string _updatedate;//datetime
        string _updatemachineinfo;//char(50)

        #region Constructor

        public DVOMasterBankDetails()
        {
            _rowid = 0;
            _bank_code = string.Empty;
            _media_str = string.Empty;
            _insertby = 0;
            _insertdate = "01/01/1900";//datetime
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//datetime
            _updatemachineinfo = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string bank_code
        {
            get { return _bank_code; }
            set { _bank_code = value; }
        }
        public string media_str
        {
            get { return _media_str; }
            set { _media_str = value; }
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

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_BankDetailsIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_BankDetailsUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_BankDetailsDel"; }
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
            get { return "MasterBankDetails"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            if (parameters[2] != null && parameters[2].ToString().Trim() == "FOR_MULTIPLE_BANK")
            {
                sql.Append("SELECT Bank_Code_ID p_rowid,bank_code p_bank_code,media_str p_media_str,insertby p_insertby,");
                sql.Append(" insertdate p_insertdate,insertmachineinfo p_insertmachinfo,updateby p_updateby,");
                sql.Append(" updatedate p_updatedate,updatemachineinfo p_updatemachinfo");
                sql.Append(" FROM MasterBankDetails WHERE 1=1");
                //if (Convert.ToInt32(parameters[0]) > 0)
                //    sql.Append("AND rowid = " + parameters[0].ToString());
                //if (parameters[1] != null)
                //    if (parameters[1].ToString().Trim() != string.Empty)
                //        sql.Append("AND Rtrim(bank_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[3] != null)
                    if (parameters[3].ToString().Trim() != string.Empty)
                        sql.Append("AND bank_code IN (" + parameters[3].ToString().Trim() + ")");
            }
            else
            {
                sql.Append("SELECT Bank_Code_ID p_rowid,bank_code p_bank_code,media_str p_media_str,insertby p_insertby,");
                sql.Append(" insertdate p_insertdate,insertmachineinfo p_insertmachinfo,updateby p_updateby,");
                sql.Append(" updatedate p_updatedate,updatemachineinfo p_updatemachinfo");
                sql.Append(" FROM MasterBankDetails WHERE 1=1");
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append("AND Bank_Code_ID = " + parameters[0].ToString());
                if (parameters[1] != null)
                    if (parameters[1].ToString().Trim() != string.Empty)
                        sql.Append("AND RTRIM(bank_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            }
            return sql.ToString();
        }

        public  string getDataforExcel(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            if (parameters[2] != null && parameters[2].ToString().Trim() == "FOR_MULTIPLE_BANK")
            {
                sql.Append("SELECT Bank_Code_ID p_rowid,bank_code p_bank_code,media_str p_media_str,insertby p_insertby,");
                sql.Append(" insertdate p_insertdate,insertmachineinfo p_insertmachinfo,updateby p_updateby,");
                sql.Append(" updatedate p_updatedate,updatemachineinfo p_updatemachinfo");
                sql.Append(" FROM MasterBankDetails WHERE 1=1");
                //if (Convert.ToInt32(parameters[0]) > 0)
                //    sql.Append("AND rowid = " + parameters[0].ToString());
                //if (parameters[1] != null)
                //    if (parameters[1].ToString().Trim() != string.Empty)
                //        sql.Append("AND Rtrim(bank_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[3] != null)
                    if (parameters[3].ToString().Trim() != string.Empty)
                        sql.Append("AND bank_code IN (" + parameters[3].ToString().Trim() + ")");
            }
            else
            {
                sql.Append("SELECT Bank_Code_ID p_rowid,bank_code p_bank_code,media_str p_media_str,insertby p_insertby,");
                sql.Append(" insertdate p_insertdate,insertmachineinfo p_insertmachinfo,updateby p_updateby,");
                sql.Append(" updatedate p_updatedate,updatemachineinfo p_updatemachinfo");
                sql.Append(" FROM MasterBankDetails WHERE 1=1");
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append("AND Bank_Code_ID = " + parameters[0].ToString());
                if (parameters[1] != null)
                    if (parameters[1].ToString().Trim() != string.Empty)
                        sql.Append("AND RTRIM(bank_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            }
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
