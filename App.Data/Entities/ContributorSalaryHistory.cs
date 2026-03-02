using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ContributorSalaryHistory : BaseEntity
  {
    [Display(Name = "PersonID")]
    [StringLength(10)]
    public string PersonID { get; set; }

    [DataType("decimal(18 ,4")]
    [Display(Name = "Salary Amount (Annual)")]
    public decimal SalaryAmount { get; set; }
    public bool Active { get; set; }
    public bool Default { get; set; }
    [Column(TypeName = "datetime")]
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    
    public DateTime? EffectiveDate { get; set; }

  }
}
