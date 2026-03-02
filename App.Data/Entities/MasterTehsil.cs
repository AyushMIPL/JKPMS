using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  [Table("MasterTehsil")]
  public class MasterTehsil : BaseEntity
  {   

    [Required(ErrorMessage = " Please Enter Tehsil ")]
    [Display(Name = "Tehsil *")]
    [StringLength(150)]
    public string Name { get; set; }


    [Display(Name = "District *")]
    [Required(ErrorMessage = "Please Select District ")]
    public int DistrictId { get; set; }
   
  
    [ForeignKey("DistrictId")]
    public virtual MasterDistrict District { get; set; }

  }
}

