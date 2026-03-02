using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ProcessPayDeductions:BaseEntity
  {
    public int DocNo { get; set; }
    public string DeductionCode { get; set; }
    public decimal DeductionRate { get; set; }
    public decimal Amount { get; set; }
    public int AccountNo { get; set; }
    public string Department { get; set; }
    public int ModFlag { get; set; }
    public string AddCode { get; set; }
    public decimal LoDedAmt { get; set; }
    public decimal HiDedAmt { get; set; }
  }
}
