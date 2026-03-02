using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterEmployerType : BaseEntity
  {
    public MasterEmployerType()
    {
      this.Employer = new List<MasterEmployer>();
    }
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

    /*[Required(ErrorMessage = "Enter PF Rate (%) First")]
    [Display(Name = "PF Rate (%)")]
    public decimal PFRate { get; set; }*/
    public virtual List<MasterEmployer> Employer { get; set; }
  }
}
