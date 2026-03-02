using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class UpdateTimeCardModel
  {
    public int acct_no_id { get; set; }
    public string add_code_cr { get; set; }
    public int card_no { get; set; }
    public string department_id { get; set; }
    public string description_cr { get; set; }
    public int dflt_acct_cr { get; set; }
    public string dflt_dept_cr { get; set; }
    public decimal? dflt_hi_inc_amt_cr { get; set; }
    public decimal? dflt_hours_cr { get; set; }
    public decimal? dflt_lo_inc_amt_cr { get; set; }
    public decimal? dflt_num_cr { get; set; }
    public decimal? dflt_rate_cr { get; set; }
    public string dfltkeyvalue { get; set; }
    public string dftAccountType { get; set; }
    public string empl_code { get; set; }
    public string empl_name { get; set; }
    public int EmpTcardID { get; set; }
    public DateTime end_date { get; set; }
    public string FIND_DETAILS_BY_INCCODE { get; set; }
    public string FIND_DETAILS_FORAUTOPAY { get; set; }
    public string FIND_DETILSEARCH_BY_CARDNO { get; set; }
    public string FIND_INC_LINENO { get; set; }
    public string FIND_INC_LOAD { get; set; }
    public string FIND_INC_TIMECARD_Exist { get; set; }
    public decimal? hi_inc_amt_id { get; set; }
    public decimal? inc_Amount { get; set; }
    public string inc_code_id { get; set; }
    public decimal? inc_hours_id { get; set; }
    public decimal? inc_number_id { get; set; }
    public decimal? inc_rate_id { get; set; }
    public string inc_type_cr { get; set; }
    public string INSERT_TIMEDETAIL { get; set; }
    public int InsertBy { get; set; }
    public DateTime InsertDate { get; set; }
    public string InsertMachineInfo { get; set; }
    public int line_no_id { get; set; }
    public decimal? lo_inc_amt_id { get; set; }
    public  string NOTES_TABLE_RECORD_ID { get; set; }
    public int ret_acc_no { get; set; }
    public string ret_acc_type { get; set; }
    public string ret_keyvalue { get; set; }
    public int RowID { get; set; }
    public DateTime start_date { get; set; }
    public int timecd_acct_no { get; set; }
    public int UNIQUE_ID { get; set; }
    public string UPDATE_DETAILS { get; set; }
    public int UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
    public string UpdateMachineInfo { get; set; }
    public string used_flag { get; set; }
    public string PensionerID { get; set; }
    //public string FIND_DETAILS_FORAUTOPAY_QUERY(ref object[] parameters);
  }
}
