using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterEmployeeDeductions
  {
    [Key]
    public int EmpDeductionID { get; set; }
    public Nullable<long> EmployeeID { get; set; }
    public string Empl_Code { get; set; }
    [Required(ErrorMessage = "Deduction Code")]
    [Display(Name = "Deduction Code *")]
    public string Ded_Code { get; set; }
    public Nullable<long> line_no { get; set; }
    [Required(ErrorMessage = "Deduction Rate")]
    [Display(Name = "Deduction Rate *")]
    public Nullable<decimal> ded_rate { get; set; }

    [Required(ErrorMessage = "Annual Limit")]
    [Display(Name = "Annual Limit *")]
    public Nullable<decimal> ded_limit { get; set; }    
    [Required(ErrorMessage = "Frequency")]
    [Display(Name = "Frequency *")]
    public string ded_apply { get; set; }
    public Nullable<int> acct_no { get; set; }
    public string department { get; set; }
    public Nullable<decimal> ded_qtd1 { get; set; }
    public Nullable<decimal> ded_qtd2 { get; set; }
    public Nullable<decimal> ded_qtd3 { get; set; }
    public Nullable<decimal> ded_qtd4 { get; set; }
    public Nullable<decimal> ded_ytd { get; set; }
    //[Required(ErrorMessage = "Applied Date")]
    [Display(Name = "Applied Date")]
    public Nullable<System.DateTime> ded_date { get; set; }
    public Nullable<decimal> lo_ded_amt { get; set; }
    public Nullable<decimal> hi_ded_amt { get; set; }
    [Required(ErrorMessage = "Pay Limit")]
    [Display(Name = "Pay Limit *")]

    public Nullable<decimal> pay_limit { get; set; }
    public Nullable<decimal> balanceamt { get; set; }


  }
}
