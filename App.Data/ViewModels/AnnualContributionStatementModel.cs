using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class AnnualContributionStatementModel
  {
    [Required(ErrorMessage = "Select Employer First")]
    [Display(Name = "Employer")]
    public string EmployeeId { get; set; }

    [Display(Name = "Contributor")]
    [Required(ErrorMessage = "Select Contributor First")]
    public string ContributorId { get; set; }

    [Display(Name = "Year")]
    [Required(ErrorMessage = "Select Year First")]
    public string Year { get; set; }

  }
}
