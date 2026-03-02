using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  /// <summary>

  public class DVOUpdateBankCode : DVOBase
  {
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

    //Variable used to store the data for PayrollGLAccounts Table (For GL account Number)
    private string _Acct_Type;
    private string _Acct_Desc;
    private long _Acct_No;
    private decimal? _Amount;
    private string _KeyValue;

    //Variable used to store data of bus_name from MasterVendor table
    private string _bus_name;


    /// <summary>
    /// Private variable applicable only for SQL-Server
    /// </summary
    private int _RowID;
    private string _InsertMachineInfo;
    private DateTime _InsertDate;
    private int _InsertBy;

    private string _UpdateMachineInfo;
    private DateTime _UpdateDate;
    private int _UpdateBy;


    public DVOUpdateBankCode()
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

      _InsertMachineInfo = "App";
      _InsertDate = DateTime.ParseExact(DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture), DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//DateTime.Now;
      _InsertBy = -1;
      _UpdateMachineInfo = "App";
      _UpdateDate = DateTime.ParseExact(DVOApplicationUserInfo.CurrentDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture), DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);//DateTime.Now;
      _UpdateBy = -1;

      _Acct_Type = string.Empty;
      _Acct_Desc = string.Empty;
      _Acct_No = 0;
      _KeyValue = string.Empty;
      _bus_name = string.Empty;
    }

    #region StartProperties

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


    // Properties Used to get of set the data from PayrollGLAccounts table for GL account
    public string Acct_Type
    {
      get { return _Acct_Type; }
      set { _Acct_Type = value; }
    }

    public string Acct_Desc
    {
      get { return _Acct_Desc; }
      set { _Acct_Desc = value; }
    }

    public long Acct_No
    {
      get { return _Acct_No; }
      set { _Acct_No = value; }
    }

    public decimal? Amount
    {
      get { return _Amount; }
      set { _Amount = value; }
    }

    public string bus_name
    {
      get { return _bus_name; }
      set { _bus_name = value; }
    }

    public string KeyValue
    {
      get { return _KeyValue; }
      set { _KeyValue = value; }
    }


    //Properties used for only SQL Server

    public string InsertMachineInfo
    {
      get { return _InsertMachineInfo; }
      set { _InsertMachineInfo = value; }
    }
    public DateTime InsertDate
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
    public DateTime UpdateDate
    {
      get { return _UpdateDate; }
      set { _UpdateDate = value; }
    }
    public int UpdateBy
    {
      get { return _UpdateBy; }
      set { _UpdateBy = value; }
    }
    #endregion Properties



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
      get { return "uspbankcodeGet"; }
    }

    public override string ALL_SPNAME
    {
      get { return ""; }
    }

    public override string FIND_QUERY(ref Object[] parameters)
    {
      System.Text.StringBuilder sql = new StringBuilder();
      sql.Append("Select MasterBanks.Bank_Code_ID v_rowid, MasterBanks.bank_code v_bank_code,MasterBanks.bank_desc v_bank_desc,MasterBanks.dfi_dest v_dfi_dest,MasterBanks.chk_digit v_chk_digit,");
      sql.Append("MasterBanks.dd_bank_code v_dd_bank_code,MasterBanks.co_bank_acct_no v_co_bank_acct_no,MasterBanks.cash_acct_no v_cash_acct_no,");
      sql.Append("MasterBanks.offset_debit v_offset_debit,MasterBanks.mag_media v_mag_media,MasterBanks.dd_create v_dd_create,MasterBanks.dd_format v_dd_format,");
      sql.Append("MasterBanks.dd_transfer v_dd_transfer,MasterBanks.suppliercode v_suppliercode,PayrollGLAccounts.acct_type p_accttype,PayrollGLAccounts.acct_desc p_acctdesc,");
      sql.Append("PayrollGLAccounts.acct_no p_acct_no,MasterVendor.bus_name, PayrollGLAccounts.keyvalue p_keyvalue FROM MasterBanks LEFT outer JOIN  PayrollGLAccounts ON MasterBanks.cash_acct_no = PayrollGLAccounts.acct_no LEFT outer JOIN  MasterVendor ON MasterBanks.suppliercode=MasterVendor.vend_code where  1=1 ");

      if (parameters[0] != null)
        if (parameters[0].ToString() != string.Empty)
          sql.Append(" AND  bank_code=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
      if (parameters[1] != null)
        if (parameters[1].ToString() != string.Empty)
          sql.Append(" AND Rtrim(bank_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
      if (parameters[2] != null)
        if (Convert.ToInt32(parameters[2]) > 0)
          sql.Append(" AND  dfi_dest= " + parameters[2].ToString());
      if (parameters[3] != null)
        if (parameters[3].ToString() != string.Empty)
          sql.Append(" AND  co_bank_acct_no=" + "'" + parameters[3].ToString().Replace("'", "''") + "'");
      if (parameters[4] != null)
        if (Convert.ToInt32(parameters[4]) > 0)
          sql.Append(" AND  cash_acct_no= " + parameters[4].ToString());
      if (parameters[5] != null)
        if (parameters[5].ToString() != string.Empty)
          sql.Append(" AND  suppliercode=" + "'" + parameters[5].ToString().Replace("'", "''") + "'");
      if (parameters[6] != null)
        if (parameters[6].ToString() != string.Empty)
          sql.Append(" AND  MasterBanks.mag_media=" + "'" + parameters[6].ToString().Replace("'", "''") + "'");
      if (Convert.ToInt32(parameters[7]) > 0)
        sql.Append(" AND  MasterBanks.Bank_Code_ID= " + parameters[7].ToString());

      return sql.ToString();
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


    #endregion Stored-Procedures
  }

  public class DVOUpdateBankCode_BankCode_Comparer : IComparer<DVOUpdateBankCode>
  {
    public int Compare(DVOUpdateBankCode obj1, DVOUpdateBankCode obj2)
    {
      int returnvalue = 1;
      if (obj1 != null && obj2 != null)
        returnvalue = obj1.bank_code.CompareTo(obj2.bank_code);
      return returnvalue;
    }
  }
}
