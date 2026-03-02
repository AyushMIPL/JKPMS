using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ProcessPayIncome:BaseEntity
  { 
    public int DocNo { get; set; }
    public string IncCode { get; set; }
    public decimal IncRate { get; set; }
    public decimal Number { get; set; }
    public decimal Amount { get; set; }
    public int AccountNo { get; set; }
    public string Department { get; set; }
    public int ModFlag { get; set; }
    public string AddCode { get; set; }
    public decimal LoIncAmt { get; set; }
    public decimal HiIncAmt { get; set; }
  }
}
