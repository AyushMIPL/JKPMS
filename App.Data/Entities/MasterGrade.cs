using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterGrade : BaseEntity
  {
    public MasterGrade()
    {
      this.JobDetails = new List<MasterContributorJobDetails>();
    }
    [Required(ErrorMessage = "Enter Grade First")]
    [Display(Name = "Grade *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributorJobDetails> JobDetails { get; set; }
  }
}
