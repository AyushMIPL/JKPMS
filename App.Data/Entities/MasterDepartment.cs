using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterDepartment : BaseEntity
  {
    public MasterDepartment()
    {
      this.JobDetails = new List<MasterContributorJobDetails>();
    }
    [Required(ErrorMessage = "Enter Department First")]
    [Display(Name = "Department *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributorJobDetails> JobDetails { get; set; }
  }
}

