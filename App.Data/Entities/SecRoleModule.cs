using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class SecRoleModule:BaseEntity
  {
    public int RoleID { get; set; }
    public int ModuleID { get; set; }
    public Nullable<bool> ViewPermission { get; set; }
    public Nullable<bool> AddPermssion { get; set; }
    public Nullable<bool> EditPermission { get; set; }
    public Nullable<bool> DeletePermission { get; set; }
   
  }
}
