using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ConExceptions: BaseEntity
  {
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Extra1 { get;set; }
    public string Extra2 { get; set; }
    public string Extra3 { get; set; }
  }
}
