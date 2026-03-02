using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterRegion : BaseEntity
  {
    public MasterRegion()
    {
      this.District = new List<MasterDistrict>();
    }

    [Required(ErrorMessage = " Please Enter Region ")]
    [Display(Name = "Region *")]
    [StringLength(150)]
    public string Name { get; set; }


    [Display(Name = "State *")]
    [Required(ErrorMessage = "Please Select State ")]
    public int StateId { get; set; }
   
  
    [ForeignKey("StateId")]
    public virtual MasterState States { get; set; }

    public virtual ICollection<MasterDistrict> District { get; set; }

  }
}

