using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
  public class MasterPensionerType : BaseEntity
  {
    public MasterPensionerType()
    {
      this.Pensioner = new List<MasterPensioner>();
    }
    [Required(ErrorMessage = " Please Enter Pansion Type ")]
    [Display(Name = "Pension Type *")]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<MasterPensioner> Pensioner { get; set; }
  }
}