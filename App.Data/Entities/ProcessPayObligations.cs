using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ProcessPayObligations:BaseEntity
  {
    public int DocNo { get; set; }
    public string ObligationCode { get; set; }
    public decimal ObligationRate { get; set; }
    public decimal Amount { get; set; }
    public int AccountNo { get; set; }
    public string Department { get; set; }
    public int BalAccountNo { get; set; }
    public string BalDepartment { get; set; }
    public int ModFlag { get; set; }
    public string AddCode { get; set; }
  }
}
