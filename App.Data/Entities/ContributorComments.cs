using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace App.Data.Entities
{
  public class ContributorComments : BaseEntity
  {
    [Display(Name = "PersonID")]
    [StringLength(10)]
    public string PersonID { get; set; }
    [DataType(DataType.MultilineText)]
    public string Comments { get; set; }
  }
}
