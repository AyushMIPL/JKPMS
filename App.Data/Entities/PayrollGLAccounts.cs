using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class PayrollGLAccounts:BaseEntity
  {
    public int rowid { get; set; }
    //[Key]
    public int acct_no { get; set; }
    public string acct_type { get; set; }
    public string acct_desc { get; set; }
    public string acct_cat { get; set; }
    public string processing_seq { get; set; }
    public string incr_with_crdt { get; set; }
    public string subtotal_group { get; set; }
    public string keyvalue { get; set; }
    public Nullable<int> gobzero { get; set; }
  }
}
