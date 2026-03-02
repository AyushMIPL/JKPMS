using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class MasterEmployeeDeductionsModel
  {
    public int acct_no { get; set; }
    public string acct_no_kv { get; set; }
    public string acct_no_type { get; set; }
    public int acct_no_typeid { get; set; }
    public string ALL_SPNAME { get; set; }
    public decimal? balanceamt { get; set; }
    public string ded_apply { get; set; }
    public string ded_code { get; set; }
    public string ded_date { get; set; }
    public decimal? ded_limit { get; set; }
    public decimal? ded_qtd1 { get; set; }
    public decimal? ded_qtd2 { get; set; }
    public decimal? ded_qtd3 { get; set; }
    public decimal? ded_qtd4 { get; set; }
    public decimal? ded_rate { get; set; }
    public string ded_taxred { get; set; }
    public string ded_type { get; set; }
    public decimal? ded_ytd { get; set; }
    public string DeductioncodeTaxget { get; set; }
    public string DELETE_SPNAME { get; set; }
    public string department { get; set; }
    public string description { get; set; }
    public int dflt_acct { get; set; }
    public string dflt_apply { get; set; }
    public string dflt_dept { get; set; }
    public decimal? dflt_hi_ded_amt { get; set; }
    public decimal? dflt_limit { get; set; }
    public decimal? dflt_lo_ded_amt { get; set; }
    public decimal? dflt_pay_limit { get; set; }
    public decimal? dflt_rate { get; set; }
    public string dfltaccounttype { get; set; }
    public string dfltkeyvalue { get; set; }
    public string EmpDedanddefaults { get; set; }
    public string empl_code { get; set; }
    public string empl_status { get; set; }
    public string FIND_SPNAME { get; set; }
    public string firstName { get; set; }
    public string GET_ROWID { get; set; }
    public decimal? hi_ded_amt { get; set; }
    public string INSERT_SPNAME { get; set; }
    public int InsertBy { get; set; }
    public string InsertDate { get; set; }
    public string InsertMachineInfo { get; set; }
    public string jobCode { get; set; }
    public string jobTitle { get; set; }
    public string lastName { get; set; }
    public string lastPay { get; set; }
    public int line_no { get; set; }
    public decimal? lo_ded_amt { get; set; }
    public string NOTES_TABLE_RECORD_ID { get; set; }
    public decimal? pay_limit { get; set; }
    public string pay_period { get; set; }
    public int Rowid { get; set; }
    public string SSN { get; set; }
    public string tax_code { get; set; }
    public string typeCode { get; set; }
    public int UNIQUE_ID { get; set; }
    public int UpdateBy { get; set; }
    public string UpdateDate { get; set; }
    public string UpdateMachineInfo { get; set; }
    public string yearrollover { get; set; }

    public string PensionerID { get; set; }

    public string SocialSecurityNo { get; set; }

    public decimal? amount { get; set; }
    public bool dedflag { get; set; }
  }
}
