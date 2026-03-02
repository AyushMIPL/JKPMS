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
  public class MasterDistrictVM
    {

        public MasterDistrictVM()
        {
            this.City = new List<MasterCity>();
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

        [Required(ErrorMessage = "Enter District First")]
        [Display(Name = "District *")]
        [StringLength(150)]
        public string Name { get; set; }


        [Display(Name = "Region *")]
        [Required(ErrorMessage = "Select Region First")]
        public int RegionId { get; set; }


        [ForeignKey("RegionId")]
        public virtual MasterRegion Regions { get; set; }

        public virtual ICollection<MasterCity> City { get; set; }

    }

}
