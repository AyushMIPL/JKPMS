using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace App.Data.Entities
{
  public class MasterBeneficiaries : BaseEntity
  {    
    public string FilePath { get; set; }
    public int Finalized { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public int? ProcessedBy { get; set; }


  }
}
