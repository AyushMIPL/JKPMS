using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterJobTitle:BaseEntity
  {
    public MasterJobTitle()
    {
      this.JobTitle = new List<MasterJobTitle>();
    }
    [Required(ErrorMessage = "Enter Job Title First")]
    [Display(Name = "Job Title *")]
    [StringLength(50)]
    public string Name { get; set; }
    public virtual List<MasterJobTitle> JobTitle { get; set; }
  }
}
