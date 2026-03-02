using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class ApprovalProcessLevel
  {
    [Key]
    public int ApprovalProcessLevelId { get; set; }
    public int ModuleId { get; set; }
    public string ApprovalProcessLevelName { get; set; }
    public int ApprovalLevel { get; set; }
    public int CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public int ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public bool IsActive { get; set; }
  }
}
