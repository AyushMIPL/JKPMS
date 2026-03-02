using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace App.Data.Entities
{
  public class MasterSource:BaseEntity
  {
    [Required(ErrorMessage = "Enter Source Name")]
    [Display(Name = "Source *")]
    [StringLength(50)]
    public string Name { get; set; }
  }
}
