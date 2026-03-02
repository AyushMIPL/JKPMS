using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace App.Data.Entities
{
  public class ContributonSheetHeader:BaseEntity
  {    
    public int EmployerId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public string FilePath { get; set; }
    public int Finalized { get; set; }
    public int Succeeded { get; set; }
    
  }
}
