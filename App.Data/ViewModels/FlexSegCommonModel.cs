using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class FlexSegCommonModel
  {
    public string abbreviation { get; set; }
    public string AccountType { get; set; }
    public string Code { get; set; }
    public string EntityType { get; set; }
    public string FIND_KEYLEN { get; set; }
    public string FIND_SEGDESC { get; set; }
    public string FIND_SegmentName { get; set; }
    public string FIND_SegvdIDADD { get; set; }
    public string FIND_SegvdIDCheck { get; set; }
    public string FIND_SegvdIDGet { get; set; }
    public int flexsegid { get; set; }
    public string keyvalue { get; set; }
    public int length { get; set; }
    public int position { get; set; }
    public string required { get; set; }
    public string Ret_Keyvalue { get; set; }
    public int segvd_id { get; set; }
    public int strucid { get; set; }
    public int subdivides { get; set; }
    public string subtotal { get; set; }

    //public string Find_allAcctTypelength(ref object[] parameters);
    //public string Find_AllFlexDeptKeyvalue(ref object[] parameters);
    //public string Find_allFlexVal(ref object[] parameters);
    //public string GETFIRSTID();
  }
}
