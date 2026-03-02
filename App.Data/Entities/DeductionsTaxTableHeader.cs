using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class DeductionsTaxTableHeader
  {
    [Key]
    public int taxtabHID { get; set; }
    public string tax_year { get; set; }
    public string ded_code { get; set; }
    public Nullable<decimal> week_allow { get; set; }
    public Nullable<decimal> biweek_allow { get; set; }
    public Nullable<decimal> smonth_allow { get; set; }
    public Nullable<decimal> month_allow { get; set; }
    public Nullable<decimal> quarter_allow { get; set; }
    public Nullable<decimal> syear_allow { get; set; }
    public Nullable<decimal> year_allow { get; set; }
    public Nullable<decimal> misc_allow { get; set; }
    public Nullable<decimal> hrs_week_allow { get; set; }
    public Nullable<decimal> hrs_biweek_allow { get; set; }
    public Nullable<decimal> hrs_smonth_allow { get; set; }
    public Nullable<decimal> hrs_month_allow { get; set; }
    public Nullable<decimal> hrs_quarter_allow { get; set; }
    public Nullable<decimal> hrs_syear_allow { get; set; }
    public Nullable<decimal> hrs_year_allow { get; set; }
    public Nullable<decimal> hrs_misc_allow { get; set; }
    public string allow_or_limit { get; set; }
  }
}
