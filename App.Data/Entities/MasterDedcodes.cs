using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterDedcodes
  {
    [Key]
    public int ded_code_id { get; set; }
    [Display(Name = "Deduction Code")]
    [Required]
    public string ded_code { get; set; }
    [Display(Name = "Description")]
    [Required]
    public string description { get; set; }
    [Display(Name = "Deduction Type")]
    [Required]
    public string ded_type { get; set; }
    [Display(Name = "Tax Status")]
    [Required]
    public string ded_taxred { get; set; }
    [Display(Name = "Rate")]
    [Required]
    public Nullable<decimal> dflt_rate { get; set; }
    public Nullable<decimal> PSPBCommission { get; set; }
    [Display(Name = "Annual Limit")]
    [Required]
    public Nullable<decimal> dflt_limit { get; set; }
    [Display(Name = "Liability Account")]
    [Required]
    public Nullable<int> dflt_acct { get; set; }
    public string dflt_dept { get; set; }
    [Display(Name = "Frequency")]
    [Required]
    public string dflt_apply { get; set; }
    [Display(Name = "High Amount")]
    public Nullable<decimal> dflt_hi_ded_amt { get; set; }
    [Display(Name = "Low Amount")]
    public Nullable<decimal> dflt_lo_ded_amt { get; set; }
    [Display(Name = "EIN Number")]
    public string state_ein { get; set; }
    [Display(Name = "Tax Jurisdiction")]
    [Required]
    public string tax_jur { get; set; }
    public string dfltaccounttype { get; set; }
    public string dfltkeyvalue { get; set; }
    [Display(Name = "Per payroll Limit")]
    [Required]
    public Nullable<decimal> dflt_pay_limit { get; set; }
    public string yearrollover { get; set; }
    public Nullable<int> insertedby { get; set; }
    public Nullable<System.DateTime> insertedon { get; set; }
    public string insertedmachineinfo { get; set; }
    public Nullable<int> updatedby { get; set; }
    public Nullable<System.DateTime> updatedon { get; set; }
    public string updatedmachineinfo { get; set; }
  }
}
