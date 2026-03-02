using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterMaritalStatus : BaseEntity
  {
    public MasterMaritalStatus()
    {
      this.MarriageDetails = new List<MasterContributorMarriageDetails>();
    }
    [Required(ErrorMessage = "Enter Marital Status First")]
    [Display(Name = "Marital Status *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterContributorMarriageDetails> MarriageDetails { get; set; }
  }
}