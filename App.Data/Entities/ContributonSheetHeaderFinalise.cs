using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class ContributonSheetHeaderFinalise:BaseEntity
  {
    //public int Id { get; set; }
    public int ContributonSheetHeaderId { get; set; }
    public int EmployerId { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public string FilePath { get; set; }
    [NotMapped]
    public int Succeeded { get; set; }
  }
}
