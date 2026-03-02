using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class Payrollstypayid
  {
    public int acct_no { get; set; }
    public string acctAccountType { get; set; }
    public int acctAccountTypeId { get; set; }
    public string acctKeyvalue { get; set; }
    public string add_code { get; set; }
    public string ALL_SPNAME { get; set; }
    public decimal? amount { get; set; }
    public  string DELETE_SPNAME { get; set; }
    public string DELETE_STYPAYID { get; set; }
    public string Department { get; set; }
    public string description_MasterIncCodes { get; set; }
    public int Doc_no { get; set; }
    public string empl_code { get; set; }
    public int error { get; set; }
    public string FIND_checkpaydtl01 { get; set; }
    public string FIND_checkpaydtl1 { get; set; }
    public string FIND_checkpaydtl11 { get; set; }
    public string FIND_INCOME { get; set; }
    public string FIND_PYINCOMES { get; set; }
    public  string FIND_SPNAME { get; set; }
    public string FIND_stypayidamt { get; set; }
    public string FIND_stypayidamt1 { get; set; }
    public string FIND_stypayidinc_ytd { get; set; }
    public string FIND_stypayidinc_ytd1 { get; set; }
    public string GET_BONUS_INFO { get; set; }
    public decimal? hi_inc_amt { get; set; }
    public bool hi_inc_amt_null { get; set; }
    public decimal? hours { get; set; }
    public string inc_code { get; set; }
    public decimal? inc_rate { get; set; }
    public string inc_type { get; set; }
    public string INS_STYPAYID_INFO { get; set; }
    public  string INSERT_SPNAME { get; set; }
    public string INSERT_STYPAYID { get; set; }
    public int InsertBy { get; set; }
    public string InsertDate { get; set; }
    public string InsertMachineInfo { get; set; }
    public int line_no { get; set; }
    public decimal? lo_inc_amt { get; set; }
    public bool lo_inc_amt_null { get; set; }
    public int mod_flag { get; set; }
    public  string NOTES_TABLE_RECORD_ID { get; set; }
    public decimal? number { get; set; }
    public string pay_date { get; set; }
    public int RowID { get; set; }
    public  string TABLE_NAME { get; set; }
    public  int UNIQUE_ID { get; set; }
    public string UPDATE_SPNAME { get; set; }
    public int UpdateBy { get; set; }
    public string UpdateDate { get; set; }
    public string UpdateMachineInfo { get; set; }
    public string dfltkeyvalue { get; set; }

    //public override string FIND_QUERY(ref object[] parameters);
    //public string GET_ALLPAYID(ref object[] parameters);
    //public string GetAllIncomes(ref object[] parameters);
  }
}
