using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterCity : BaseEntity
  {
    public MasterCity()
    {
      this.Employers = new List<MasterEmployer>();
      this.ContributorPersonalDetails = new List<MasterContributor>();
    }

    
            [Display(Name = "City *")]
        [Required(ErrorMessage = "Please Enter City ")]
        [StringLength(150)]
        public string Name { get; set; }

        [Display(Name = "District *")]
        [Required(ErrorMessage = "Please Select District ")]
        public int DistrictId { get; set; }


       
        [ForeignKey("DistrictId")]
        public virtual MasterDistrict Districts { get; set; }

        //[Required]
        //[Display(Name = "District *")]
        //public int DistrictId { get; set; }

        //[ForeignKey("DistrictId")]
        //public virtual MasterDistrict Districts { get; set; }
        public virtual ICollection<MasterEmployer> Employers { get; set; }
       public virtual ICollection<MasterContributor> ContributorPersonalDetails { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "Enter District First")]
        //[Display(Name = "District *")]
        //[StringLength(150)]
        //public string DistrictName { get; set; }
    }
}
