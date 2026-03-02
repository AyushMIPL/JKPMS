using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class FlexStructDetails:BaseEntity 
  {
    public int strucid { get; set; }
    public Nullable<int> flexsegid { get; set; }
    public Nullable<short> position { get; set; }
    public Nullable<short> length { get; set; }
    public string required { get; set; }
    public string subtotal { get; set; }
  }
}
