using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public  class PayrollStypayod
  {
    public int acct_no { get; set; }
    public string acctAccountType { get; set; }
    public int acctAccountTypeId { get; set; }
    public string acctKeyvalue { get; set; }
    public string add_code { get; set; }
    public string ALL_SPNAME { get; set; }
    public decimal? amount { get; set; }
    public int bal_acct_no { get; set; }
    public string bal_dept { get; set; }
    public string balacctAccountType { get; set; }
    public int balacctAccountTypeId { get; set; }
    public string balacctKeyvalue { get; set; }
    public  string DELETE_SPNAME { get; set; }
    public string DELETE_STYPAYOD { get; set; }
    public string Department { get; set; }
    public string description_MasterIncCodes { get; set; }
    public int Doc_no { get; set; }
    public string FIND_OBLIGATION_POST { get; set; }
    public string FIND_PYOBLICATIONS { get; set; }
    public  string FIND_SPNAME { get; set; }
    public string FIND_stypayodytd { get; set; }
    public string FIND_stypayodytd1 { get; set; }
    public string FIND_stypayodytd2 { get; set; }
    public string INSERT_SPNAME { get; set; }
    public string INSERT_STYPAYOD { get; set; }
    public int InsertBy { get; set; }
    public string InsertDate { get; set; }
    public string InsertMachineInfo { get; set; }
    public int line_no { get; set; }
    public int mod_flag { get; set; }
    public  string NOTES_TABLE_RECORD_ID { get; set; }
    public string obl_code { get; set; }
    public decimal? obl_limit { get; set; }
    public decimal? obl_rate { get; set; }
    public string obl_type { get; set; }
    public decimal? obl_ytd { get; set; }
    public string pay_date { get; set; }
    public decimal? pay_limit { get; set; }
    public int RowID { get; set; }
    public string TABLE_NAME { get; set; }
    public int UNIQUE_ID { get; set; }
    public string UPDATE_SPNAME { get; set; }
    public int UpdateBy { get; set; }
    public string UpdateDate { get; set; }
    public string UpdateMachineInfo { get; set; }

    //public override string FIND_QUERY(ref object[] parameters);
    //public string GET_ALLPAYOD(ref object[] parameters);
    //public string GetAllObligations(ref object[] parameters);
  }
}
