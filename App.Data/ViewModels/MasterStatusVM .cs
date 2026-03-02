using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class MasterStateVM
  {
    public MasterStateVM()
    {
      this.Region = new List<MasterRegion>();
    }

    public string Id { get; set; }

    [ScaffoldColumn(false)]
    public int CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? CreatedOn { get; set; }

    [ScaffoldColumn(false)]
    public int ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? ModifiedOn { get; set; }
    [Display(Name = "Status")]
    public bool IsActive { get; set; }
    [Required(ErrorMessage = "Enter State First")]
    [Display(Name = "State *")]
    [StringLength(50)]
    public string Name { get; set; }


    [Display(Name = "Country *")]
    [Required(ErrorMessage = "Select Country First")]
    public int CountryId { get; set; }


    [ForeignKey("CountryId")]
    public virtual MasterCountry Country { get; set; }

    public virtual ICollection<MasterRegion> Region { get; set; }
  }

}
