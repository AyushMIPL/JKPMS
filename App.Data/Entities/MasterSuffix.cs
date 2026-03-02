using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterSuffix : BaseEntity
  {
    public MasterSuffix()
    {
      this.ContributorPersonalDetails = new List<MasterContributor>();
    }

    [Required(ErrorMessage = "Enter Suffix First")]
    [Display(Name = "Suffix *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributor> ContributorPersonalDetails { get; set; }
  }
}
