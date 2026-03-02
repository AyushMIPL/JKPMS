using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{

  public class ProcessDirectDepositDetails : BaseEntity
  {
    public string Empl_Code { get; set; }
    public int DocNo { get; set; }
    public string PensionerID { get; set; }
    public int DfiDest { get; set; }
    public int ChkDigit { get; set; }
    public DateTime PayDate { get; set; }
    public int TransactionCode { get; set; }
    public string BankAccountNo { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public string PensionerName { get; set; }
    public decimal TraceNo { get; set; }
    public int PayDocNo { get; set; }
    public string AccountType { get; set; }
  }


  //

}
