using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class FlexStructHeader:BaseEntity 
  {
    public Nullable<int> RowId { get; set; }
    public int FSHid { get; set; }
    public string accounttype { get; set; }
    public string desc { get; set; }
    public Nullable<short> keylength { get; set; }
    public Nullable<short> segmentcnt { get; set; }
    public string printsafter { get; set; }
    public string dfltacctcat { get; set; }
    public string dfltincrwcrdt { get; set; }
  }
}
