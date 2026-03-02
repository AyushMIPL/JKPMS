using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterState : BaseEntity
  {
    public MasterState()
    {
      this.Region = new List<MasterRegion>();
    }

    [Required(ErrorMessage = "Please Enter State ")]
    [Display(Name = "State *")]
    [StringLength(50)]
    public string Name { get; set; }


    [Display(Name = "Country *")]
    [Required(ErrorMessage = "Please Select Country ")]
    public int CountryId { get; set; }
   
  
    [ForeignKey("CountryId")]
    public virtual MasterCountry Country { get; set; }

    public virtual ICollection<MasterRegion> Region { get; set; }

  }
}

