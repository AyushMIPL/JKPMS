using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterRelationshipType:BaseEntity
  {
    public MasterRelationshipType()
    {
      this.DependantDetails = new List<MasterDependantDetails>();
    }

    [Required(ErrorMessage = "Enter Relationship Type First")]
    [Display(Name = "Relationship *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterDependantDetails> DependantDetails { get; set; }
  }
}
