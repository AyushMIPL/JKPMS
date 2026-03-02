using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public abstract class BaseEntity
  {


    public int Id { get; set; }

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
  }
}
