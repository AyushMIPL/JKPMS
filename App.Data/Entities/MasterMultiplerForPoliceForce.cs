using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterMultiplerForPoliceForce:BaseEntity
  {
    public int Id { get; set; }
    public int ContributorAge { get; set; }
    public decimal MultiplierFactor { get; set; }
    public int CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public int ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public bool IsActive { get; set; }
  }
}
