using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ProcessPayPensioner:BaseEntity
  {
    public string PensionerID { get; set; }
    public int DocNo { get; set; }
    public DateTime DocDate { get; set; }
    public DateTime PayDate { get; set; }
    public DateTime EopDate { get; set; }
    public string PrintCheck { get; set; }
    public int CashAccountNo { get; set; }
    public string Department { get; set; }
    public decimal CashAmount { get; set; }
    public string CheckNo { get; set; }
    public decimal IncGross { get; set; }
    public decimal IncTaxable { get; set; }
    public decimal DedMediCare { get; set; }
    public decimal DedFedTax { get; set; }
    public decimal DedStaTax { get; set; }
    public decimal DedLocTax { get; set; }
    public decimal DedOther { get; set; }
    public decimal OblMediCare { get; set; }
    public decimal OblOther { get; set; }
    public decimal OblTotal { get; set; }
    public decimal IncNet { get; set; }
    public decimal IncExpense { get; set; }
    public string OkToPost { get; set; }
    public string Deposit { get; set; }
    public DateTime PayStartDate { get; set; }

  }
}
