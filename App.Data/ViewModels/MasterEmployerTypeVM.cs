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
  public class MasterEmployerTypeVM
  {
    public MasterEmployerTypeVM()
    {
      this.Employer = new List<MasterEmployer>();
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
    [Required(ErrorMessage = "Enter Employer Type First")]
    [Display(Name = "Employer Type *")]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Enter Retirement Age Before 2004")]
    [Display(Name = "Retirement Age Before 2004 *")]
    public int RetirementAge { get; set; }
    [Required(ErrorMessage = "Enter Retirement Age After 2004")]
    [Display(Name = "Retirement Age After 2004 *")]
    public int RetirementAgeAfter2004 { get; set; }
    [Required(ErrorMessage = "Enter Minimum Age Of Hiring")]
    [Display(Name = "Minimum Hiring Age *")]
    public int MinimumHiringAge { get; set; }
    [Required(ErrorMessage = "Enter Dependant Termination Age")]
    [Display(Name = "Dependant Termination Age *")]
    public int DependantTerminationAge { get; set; }
    public virtual List<MasterEmployer> Employer { get; set; }
  }

}
