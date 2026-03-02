using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterDesignation : BaseEntity
  {
    public MasterDesignation()
    {
      this.JobDetails = new List<MasterContributorJobDetails>();
      this.PensionApplications = new List<PensionApplications>();
    }
    [Required(ErrorMessage = "Enter Designation First")]
    [Display(Name = "Designation *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributorJobDetails> JobDetails { get; set; }
    public ICollection<PensionApplications> PensionApplications { get; set; }
  }
}
