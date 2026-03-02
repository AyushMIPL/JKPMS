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
  public class MasterMaritalStatusVM
  {
    public MasterMaritalStatusVM()
    {
      this.MarriageDetails = new List<MasterContributorMarriageDetails>();
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
    [Required(ErrorMessage = "Enter Marital Status First")]
    [Display(Name = "Marital Status *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributorMarriageDetails> MarriageDetails { get; set; }
  }
}


