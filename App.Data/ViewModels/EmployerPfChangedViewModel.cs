using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
  public class EmployerPfChangedViewModel
  {
    public int Id { get; set; }
    public string PfRateName { get; set; }
    public decimal PFRate { get; set; }
    public DateTime EffiectiveDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string StringEffiectiveDate { get; set; }
    public string StringEffectiveEndDate { get; set; }
    public string StringModifiedOn { get; set; }
    public string StringCreatedOn { get; set; }
    public bool IsApplied { get; set; }
    public bool IsExpired { get; set; }
  }
}
