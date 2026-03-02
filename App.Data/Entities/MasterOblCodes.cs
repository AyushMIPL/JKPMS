using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterOblCodes
  {
    [Key]
    public int Obl_Code_ID { get; set; }
    public string Obl_code { get; set; }
    public string description { get; set; }
    public string obl_type { get; set; }
    public Nullable<decimal> dflt_rate { get; set; }
    public Nullable<decimal> dflt_limit { get; set; }
    public Nullable<int> dflt_acct { get; set; }
    public string dflt_dept { get; set; }
    public Nullable<int> dflt_bacct { get; set; }
    public string dflt_bdept { get; set; }
    public string dfltaccounttype { get; set; }
    public string dfltbaccounttype { get; set; }
    public string dfltbkeyvalue { get; set; }
    public Nullable<decimal> dflt_pay_limit { get; set; }
  }
}
