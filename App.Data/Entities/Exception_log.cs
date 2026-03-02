using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class Exception_log : BaseEntity
  {
    public int rowRowID { get; set; }
    public Int64 sno { get; set; } 
    public string ErrorDescription { get; set; } 
    public string InsertMachineInfo { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertBy { get;set; }
    public string UpdateMachineInfo { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int UpdateBy { get; set; }
  }
}
