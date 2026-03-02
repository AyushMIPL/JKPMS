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
  public class MasterCityVM
  {
    public MasterCityVM()
    {
      this.Employers = new List<MasterEmployer>();
      this.ContributorPersonalDetails = new List<MasterContributor>();
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
    [Required(ErrorMessage = "Enter City First")]
    [Display(Name = "City *")]
    [StringLength(150)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Select District First")]
    [Display(Name = "District *")]
    public int DistrictId { get; set; }

    [ForeignKey("DistrictId")]
    public virtual MasterDistrict Districts { get; set; }
    public virtual ICollection<MasterEmployer> Employers { get; set; }
    public virtual ICollection<MasterContributor> ContributorPersonalDetails { get; set; }
  }

}
