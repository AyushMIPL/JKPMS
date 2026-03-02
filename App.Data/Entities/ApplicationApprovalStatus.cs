using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class ApplicationApprovalStatus
  {
    [Key]
    public int ApplicationApprovalStatusId { get; set; }
    public int ApprovalProcessId { get; set; }
    public int ApplicationId { get; set; }
    public string Notes { get; set; }
    public string ApprovalStatus { get; set; }
    public int ApprovalLevel { get; set; }
    public DateTime? ApprovedDate { get; set; }
    //public virtual ApprovalProcessAssignedUser ApprovalProcessAssignedUser { get; set; }
    //public virtual PensionApplications PensionApplication { get; set; }
    //public virtual RefundApplications RefundApplication { get; set; }
  }
}
