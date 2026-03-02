using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterEmployeeIncomes
  {
    [Key]
    public int EmpIncomeID { get; set; }
    public Nullable<long> EmployeeID { get; set; }
    public string Empl_Code { get; set; }
    //[Required(ErrorMessage="Income Code")]
    public string inc_code { get; set; }
    public Nullable<long> line_no { get; set; }
    //[Required(ErrorMessage = "Income Rate")]
    public Nullable<decimal> inc_rate { get; set; }
    public Nullable<decimal> inc_number { get; set; }
    public Nullable<decimal> inc_hours { get; set; }
    public Nullable<int> acct_no { get; set; }
    public string department { get; set; }
    public Nullable<decimal> inc_qtd1 { get; set; }
    public Nullable<decimal> inc_qtd2 { get; set; }
    public Nullable<decimal> inc_qtd3 { get; set; }
    public Nullable<decimal> inc_qtd4 { get; set; }
    public Nullable<decimal> inc_ytd { get; set; }
    public Nullable<decimal> lo_inc_amt { get; set; }
    //[Required(ErrorMessage = "Amount")]
    public Nullable<decimal> hi_inc_amt { get; set; }
    [Required(ErrorMessage = "Remarks is required.")]
    public string ReasonForChange { get; set; }

  }
}
