using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class PayrollstypayddModel
  {
    public int accountTypeId { get; set; }
    public int acct_no { get; set; }
    public string acctAccoutType { get; set; }
    public string acctKeyvalue { get; set; }
    public string add_code { get; set; }
    public string ALL_SPNAME { get; set; }
    public decimal? amount { get; set; }
    public string ded_code { get; set; }
    public decimal? ded_limit { get; set; }
    public decimal? ded_rate { get; set; }
    public string ded_taxred { get; set; }
    public string ded_type { get; set; }
    public decimal? ded_ytd { get; set; }
    public bool dedflag { get; set; }
    public string DeductionsYTD { get; set; }
    public string DELETE_SPNAME { get; set; }
    public string DELETE_STYPARDD { get; set; }
    public string Department { get; set; }
    public string description_MasterIncCodes { get; set; }
    public int Doc_no { get; set; }
    public string FIND_biweek_allow { get; set; }
    public string FIND_checkpaydtl02 { get; set; }
    public string FIND_checkpaydtl2 { get; set; }
    public string FIND_checkpaydtl22 { get; set; }
    public string FIND_DEDUCTION { get; set; }
    public string FIND_misc_allow { get; set; }
    public string FIND_month_allow { get; set; }
    public string FIND_PYDEDUCTIONS { get; set; }
    public string FIND_quarter_allow { get; set; }
    public string FIND_smonth_allow { get; set; }
    public string FIND_SPNAME { get; set; }
    public string FIND_stypayddamt { get; set; }
    public string FIND_stypayddamt1 { get; set; }
    public string FIND_stypayddinc_ytd { get; set; }
    public string FIND_stypayddinc_ytd1 { get; set; }
    public string FIND_stypayddytd { get; set; }
    public string FIND_stypayddytd1 { get; set; }
    public string FIND_stypayddytd2 { get; set; }
    public string FIND_syear_allow { get; set; }
    public string FIND_TaxValueGet { get;set;  }
    public string FIND_usp_tbl_check { get;set;  }
    public string FIND_week_allow { get; set; }
    public string FIND_year_allow { get; set; }
    public string GET_BALANCEAMT { get; set; }
    public string GET_YEARROLLOVER { get;set;  }
    public decimal? hi_ded_amt { get; set; }
    public bool hi_ded_amt_null { get; set; }
    public  string INSERT_SPNAME { get; set; }
    public string INSERT_STYPARDD { get; set; }
    public int InsertBy { get; set; }
    public string InsertDate { get; set; }
    public string InsertMachineInfo { get; set; }
    public int line_no { get; set; }
    public decimal? lo_ded_amt { get; set; }
    public bool lo_ded_amt_null { get; set; }
    public int mod_flag { get; set; }
    public  string NOTES_TABLE_RECORD_ID { get; set; }
    public string pay_date { get; set; }
    public decimal? pay_limit { get; set; }
    public int RowID { get; set; }
    public string TABLE_NAME { get; set; }
    public string tax_code { get; set; }
    public int UNIQUE_ID { get; set; }
    public string UPDATE_SPNAME { get; set; }
    public int UpdateBy { get; set; }
    public string UpdateDate { get; set; }
    public string UpdateMachineInfo { get; set; }
    public string yearrollover { get; set; }

    //public override string FIND_QUERY(ref object[] parameters);
    //public string GET_ALLPAYDD(ref object[] parameters);
    //public string GetAllDeductions(ref object[] parameters);
  }
}
