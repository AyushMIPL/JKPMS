using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOBankCodesMasterBanks : DVOBase
    {
        private int _RowID;
        private string _bank_code;
        private string _bank_desc;
        private int _dfi_dest;
        private int _chk_digit;
        private string _dd_bank_code;
        private string _co_bank_acct_no;
        private int _cash_acct_no;
        private string _offset_debit;
        private string _mag_media;
        private string _dd_create;
        private string _dd_format;
        private string _dd_transfer;
        private string _suppliercode;
        private string _InsertMachineInfo;
        private string _InsertDate;//datetime,
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//datetime,
        private int _UpdateBy;

        #region Constructor

        public DVOBankCodesMasterBanks()
        {
            _RowID = 0;
            _bank_code = string.Empty;
            _bank_desc = string.Empty;
            _dfi_dest = 0;
            _chk_digit = 0;
            _dd_bank_code = string.Empty;
            _co_bank_acct_no = string.Empty;
            _cash_acct_no = 0;
            _offset_debit = string.Empty;
            _mag_media = string.Empty;
            _dd_create = string.Empty;
            _dd_format = string.Empty;
            _dd_transfer = string.Empty;
            _suppliercode = string.Empty;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";//datetime,
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";//datetime,
            _UpdateBy = 0;
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string bank_code
        {
            get { return _bank_code; }
            set { _bank_code = value; }
        }
        public string bank_desc
        {
            get { return _bank_desc; }
            set { _bank_desc = value; }
        }
        public int dfi_dest
        {
            get { return _dfi_dest; }
            set { _dfi_dest = value; }
        }
        public int chk_digit
        {
            get { return _chk_digit; }
            set { _chk_digit = value; }
        }
        public string dd_bank_code
        {
            get { return _dd_bank_code; }
            set { _dd_bank_code = value; }
        }
        public string co_bank_acct_no
        {
            get { return _co_bank_acct_no; }
            set { _co_bank_acct_no = value; }
        }
        public int cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no = value; }
        }
        public string offset_debit
        {
            get { return _offset_debit; }
            set { _offset_debit = value; }
        }
        public string mag_media
        {
            get { return _mag_media; }
            set { _mag_media = value; }
        }
        public string dd_create
        {
            get { return _dd_create; }
            set { _dd_create = value; }
        }
        public string dd_format
        {
            get { return _dd_format; }
            set { _dd_format = value; }
        }
        public string dd_transfer
        {
            get { return _dd_transfer; }
            set { _dd_transfer = value; }
        }
        public string suppliercode
        {
            get { return _suppliercode; }
            set { _suppliercode = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_BankCodeIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_BankCodeUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_BankCodeDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspBnkCodGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_BnkCodGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterBanks"; }
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

            sql.Append("SELECT Bank_Code_ID p_RowID,bank_code p_bank_code,bank_desc p_bank_desc,dfi_dest p_dfi_dest,chk_digit p_chk_digit,");
            sql.Append(" dd_bank_code p_dd_bank_code,co_bank_acct_no p_co_bank_acct_no,cash_acct_no p_cash_acct_no,");
            sql.Append(" offset_debit p_offset_debit,mag_media p_mag_media,dd_create p_dd_create,dd_format p_dd_format,");
            sql.Append(" dd_transfer p_dd_transfer,suppliercode p_suppliercode");
            sql.Append(" FROM MasterBanks WHERE 1=1 ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND Bank_Code_ID = " + parameters[0].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bank_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(bank_desc) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND dfi_dest = " + parameters[0].ToString().Trim());
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND chk_digit = " + parameters[0].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dd_bank_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(co_bank_acct_no) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND cash_acct_no = " + parameters[0].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(offset_debit) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mag_media) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dd_create) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dd_format) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(dd_transfer) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(suppliercode) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
