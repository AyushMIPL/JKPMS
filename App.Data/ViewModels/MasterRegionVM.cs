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
  public class MasterRegionVM
  {

    public MasterRegionVM()
    {
      this.District = new List<MasterDistrict>();
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

        [Required(ErrorMessage = "Enter Region First")]
    [Display(Name = "Region *")]
    [StringLength(150)]
    public string Name { get; set; }


    [Display(Name = "State *")]
    [Required(ErrorMessage = "Select State First")]
    public int StateId { get; set; }


    [ForeignKey("StateId")]
    public virtual MasterState States { get; set; }

    public virtual ICollection<MasterDistrict> District { get; set; }
  }

}
