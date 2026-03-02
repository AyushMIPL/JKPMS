using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class MonthlyDeductionModel
  {

    [Required(ErrorMessage = "Please Select Empolyee Type")]
    [Display(Name = "Pens.Type")]
    public string EmpType { get; set; }

    [Display(Name = "Period")]
    [Required(ErrorMessage = "Please Select Month")]
    public string Months { get; set; }

    [Required(ErrorMessage = "Please Select Year")]
    public string Years { get; set; }

    //[Required(ErrorMessage = "Please Select Deduction Type")]
    [Display(Name = "Ded.Type")]
    public string DeductionType { get; set; }

  }
}
