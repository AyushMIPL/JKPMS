using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class FlexSegmentValueDetails:BaseEntity
  {
    public Nullable<int> segmentid { get; set; }
    public int FSVDid { get; set; }
    public string keyvalue { get; set; }
    public string desc { get; set; }
    public Nullable<int> printsafter { get; set; }
    public Nullable<int> issubto { get; set; }
    public string abbreviation { get; set; }
    public string OtherDescription { get; set; }
  }
}
