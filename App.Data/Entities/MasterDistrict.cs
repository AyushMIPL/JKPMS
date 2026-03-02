using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterDistrict : BaseEntity
  {
    public MasterDistrict()
    {
      this.City = new List<MasterCity>();
    }

    [Required(ErrorMessage = "Please Enter District ")]
    [Display(Name = "District *")]
    [StringLength(150)]
    public string Name { get; set; }


    [Display(Name = "Region *")]
    [Required(ErrorMessage = "Please Select Region ")]
    public int RegionId { get; set; }
   
  
    [ForeignKey("RegionId")]
    public virtual MasterRegion Regions { get; set; }

    public virtual ICollection<MasterCity> City { get; set; }

  }
}

