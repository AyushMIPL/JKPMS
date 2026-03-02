using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterContributorPFRateDetails : BaseEntity
  {
    [Display(Name = "JKPS ID")]
    [StringLength(10)]
    public string PersonID { get; set; }
    [Index(IsUnique = true)]    

    [Required(ErrorMessage = "Enter PF For First")]
    [Display(Name = "PF For *")]
    public int PFRateID { get; set; }
    [ForeignKey("PFRateID")]
    public virtual MasterPFRate MasterPFRate { get; set; }

    [Required(ErrorMessage = "Enter PF(%) First")]
    [Display(Name = "PF(%)")]
    public decimal PFRate { get; set; }


    [Required(ErrorMessage = "Enter Effective Start Date First")]
    [Display(Name = "Effective Start Date *")]
    [Column(TypeName = "smalldatetime")]
    public DateTime EffectiveDate { get; set; }

    [Required(ErrorMessage = "Enter Effective End Date First")]
    [Display(Name = "Effective End Date *")]
    [Column(TypeName = "smalldatetime")]
    public DateTime EffectiveEndDate { get; set; }


  }
}
