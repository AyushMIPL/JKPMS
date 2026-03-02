using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterBankDetails
  {
    [Key]
    public int Bank_Code_ID { get; set; }
    [Required]
    public string bank_code { get; set; }
    public string media_str { get; set; }
    public Nullable<int> insertby { get; set; }
    public Nullable<System.DateTime> insertdate { get; set; }
    public string insertmachineinfo { get; set; }
    public Nullable<int> updateby { get; set; }
    public Nullable<System.DateTime> updatedate { get; set; }
    public string updatemachineinfo { get; set; }
  }
}
