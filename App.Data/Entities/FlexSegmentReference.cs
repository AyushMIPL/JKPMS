using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class FlexSegmentReference:BaseEntity
  {
    public int FSR_ID { get; set; }
    public string entity_type { get; set; }
    public string code { get; set; }
    public Nullable<int> segvd_id { get; set; }
  }
}
