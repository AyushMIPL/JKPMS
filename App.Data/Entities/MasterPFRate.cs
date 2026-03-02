using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterPFRate : BaseEntity
  {
    public MasterPFRate()
    {
      //this.Employers = new List<MasterEmployer>();
      this.Contributor = new List<MasterContributor>();
    }

    [Required(ErrorMessage = "Enter PF For First")]
    [Display(Name = "PF For *")]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Enter Employee PF Rate (%) First")]
    [Display(Name = "Employee PF Rate (%) *")]
    public decimal PFRate { get; set; }

    [Required(ErrorMessage = "Enter Effective Start Date First")]
    [Display(Name = "Effective Start Date *")]
    [Column(TypeName = "smalldatetime")]

    public DateTime EffectiveDate { get; set; }

    [Required(ErrorMessage = "Enter Effective End Date First")]
    [Display(Name = "Effective End Date *")]
    [Column(TypeName = "smalldatetime")]

    public DateTime EffectiveEndDate { get; set; }

    [Required(ErrorMessage = "Select Pf Type")]
    [Display(Name = "Pf Type *")]

    public string PfType { get; set; }
    //public ICollection<MasterEmployer> Employers { get; set; }
    public ICollection<MasterContributor> Contributor { get; set; }
    [NotMapped]
    [Display(Name = "Pf Rate *")]
    public int PfRateId { get; set; }
    [NotMapped]
    [Display(Name = "Pf Rate *")]
    public int PfRateIdContributor { get; set; }

  }
}
