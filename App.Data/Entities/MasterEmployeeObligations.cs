using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterEmployeeObligations
  {
    [Key]
    public int EmpOblId { get; set; }
    public Nullable<long> EmployeeID { get; set; }
    public string Empl_Code { get; set; }
    public string Obl_Code { get; set; }
    public Nullable<long> line_no { get; set; }
    public Nullable<decimal> Obl_Rate { get; set; }
    public Nullable<decimal> Obl_limit { get; set; }
    public Nullable<int> acct_no { get; set; }
    public string department { get; set; }
    public Nullable<int> bal_acct_no { get; set; }
    public string bal_dept { get; set; }
    public Nullable<decimal> Obl_qtd1 { get; set; }
    public Nullable<decimal> Obl_qtd2 { get; set; }
    public Nullable<decimal> Obl_qtd3 { get; set; }
    public Nullable<decimal> Obl_qtd4 { get; set; }
    public Nullable<decimal> Obl_ytd { get; set; }
    public Nullable<decimal> pay_limit { get; set; }
      
  }
}
