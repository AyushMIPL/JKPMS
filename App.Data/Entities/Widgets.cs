using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class Widgets:BaseEntity
  {
   
    public int WidgetId { get; set; }
    public int RoleId { get; set; }
    public bool IsDisplay { get; set; }

    public int? OldSystemId { get; set; }

    public string CreatedMachineInfo { get; set; }
   
    public string ModifiedMachineInfo { get; set; }    
    public bool IsValidate { get; set; }

  }
}
