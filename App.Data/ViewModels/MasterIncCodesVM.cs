using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class MasterIncCodesVM
  {
    public int inc_code_id { get; set; }
    [Display(Name = "Income Code")]
    public string inc_code { get; set; }
    [Display(Name = "Description")]
    public string description { get; set; }
    [Display(Name = "Default Number")]
    public Nullable<decimal> dflt_num { get; set; }
    [Display(Name = "Default Rate")]
    public Nullable<decimal> dflt_rate { get; set; }
    [Display(Name = "Default hours")]
    public Nullable<decimal> dflt_hours { get; set; }
    public Nullable<int> dflt_acct { get; set; }
    public string dflt_dept { get; set; }
    [Display(Name = "Income Type")]
    public string inc_type { get; set; }
    [Display(Name = "Low Amount")]
    public Nullable<decimal> dflt_lo_inc_amt { get; set; }
    [Display(Name = "High Amount")]
    public Nullable<decimal> dflt_hi_inc_amt { get; set; }
    [Display(Name = "Non - Qualified")]
    public string non_qual { get; set; }
    public string dfltaccounttype { get; set; }
    public Nullable<int> insertedby { get; set; }
    public Nullable<System.DateTime> insertedon { get; set; }
    public string insertedmachineinfo { get; set; }
    public Nullable<int> updatedby { get; set; }
    public Nullable<System.DateTime> updatedon { get; set; }
    public string updatedmachineinfo { get; set; }
  }

}
