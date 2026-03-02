using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class GratuityDetails
  {
    [Key]
    public int GratuityID { get; set; }
    public int PensionApplicationId { get; set; }
    public string PersonID { get; set; }
    public string FirstName { get; set; }
    public string MidName { get; set; }
    public string LastName { get; set; }
    public Nullable<decimal> GratuityAmt { get; set; }
    public Nullable<decimal> DiscountedGratuity { get; set; }
    public string Notes { get; set; }
    public int CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public string CreatedmachineInfo { get; set; }
    public int ModifiedBy { get; set; }
    public System.DateTime ModifiedOn { get; set; }
    public string ModifiedMachineInfo { get; set; }
    public Nullable<bool> Isactive { get; set; }
    public string PaidStatus { get; set; }
    public Nullable<int> Paymentmethod { get; set; }
    public string Check_no { get; set; }
  }
}
