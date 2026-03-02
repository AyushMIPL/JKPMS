using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class PensionerComments:BaseEntity
  {
    public int PensionerID { get; set; }
    public string Comments { get; set; }
  }
}
