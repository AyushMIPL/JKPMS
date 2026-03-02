using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterStatus : BaseEntity
  {
    public MasterStatus()
    {
      this.MasterContributor = new List<MasterContributor>();
    }
    [Required(ErrorMessage = "Enter Status Name First")]
    [Display(Name = "Status *")]
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(1)]
    [Column(TypeName = "char")]
    [Display(Name = "Status Code")]
    public string StatusCode { get; set; }
    public ICollection<MasterContributor> MasterContributor { get; set; }
  }
}
