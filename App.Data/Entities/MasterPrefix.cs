using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterPrefix : BaseEntity
  {
    public MasterPrefix()
    {

      this.MasterContributor = new List<MasterContributor>();
    }
    [Required(ErrorMessage = "Enter Prefix First")]
    [Display(Name = "Prefix *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributor> MasterContributor { get; set; }
  }
}