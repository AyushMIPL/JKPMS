using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class DeductionsTaxTableDetails
  {
    [Key]
    public int taxtabid { get; set; }
    public string tax_year { get; set; }
    public string ded_code { get; set; }
    public string pay_period { get; set; }
    public string marital_stat { get; set; }
    public Nullable<decimal> over_amt { get; set; }
    public Nullable<decimal> base_amt { get; set; }
    public Nullable<decimal> tax_rate { get; set; }
    public Nullable<short> order_no { get; set; }
  }
}
