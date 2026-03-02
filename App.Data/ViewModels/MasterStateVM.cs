using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class MasterStatusVM
  {
    public MasterStatusVM()
    {
      this.MasterContributor = new List<MasterContributor>();
    }
    public string Id { get; set; }

    [ScaffoldColumn(false)]
    public int CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? CreatedOn { get; set; }

    [ScaffoldColumn(false)]
    public int ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? ModifiedOn { get; set; }
    [Display(Name = "Status")]
    public bool IsActive { get; set; }
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
