using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class RefundPaidDetails
  {
    [Key]
    public int RefundID { get; set; }
    public int RefundApplicationID { get; set; }
    public string PersonID { get; set; }
    public string FirstName { get; set; }
    public string MidName { get; set; }
    public string LastName { get; set; }
    public Nullable<decimal> RefundAmt { get; set; }
    public string Notes { get; set; }
    public int CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string CreatedmachineInfo { get; set; }
    public int ModifiedBy { get; set; }
    public System.DateTime ModifiedOn { get; set; }
    public string ModifiedMachineInfo { get; set; }
    public Nullable<bool> Isactive { get; set; }
    public string PaidStatus { get; set; }
    public string Paymentmethod { get; set; }
    public string Check_no { get; set; }
  }
}
