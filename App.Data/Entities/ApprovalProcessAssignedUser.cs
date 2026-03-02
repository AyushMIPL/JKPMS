using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class ApprovalProcessAssignedUser
  {
    public ApprovalProcessAssignedUser()
    {
      this.ApplicationApprovalStatus = new HashSet<ApplicationApprovalStatus>();
    }
    [Key]
    public int ApprovalProcessId { get; set; }
    public int ApprovalProcessLevelId { get; set; }
    public Nullable<int> ApprovalUser { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<ApplicationApprovalStatus> ApplicationApprovalStatus { get; set; }
    //public virtual AppUser AppUser { get; set; }
  }
}
