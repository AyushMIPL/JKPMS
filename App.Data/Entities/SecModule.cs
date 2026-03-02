using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class SecModule:BaseEntity
  {
    //public SecModule()
    //{
    //  this.secModule = new HashSet<SecModule>();
    //  this.SecRoleModules = new HashSet<SecRoleModule>();
    //}    
    public string ModuleName { get; set; }
    public string ModuleDesc { get; set; }
    public int ParentId { get; set; }
    public string Url { get; set; }
    public string ActionName { get; set; }
    public string ControllerName { get; set; }
    public string ModuleClass { get; set; }
    public int? DisplayOrder { get ; set; }
    //public int ModuleType { get; set; }
    //public bool HasNew { get; set; }
    //public bool HasEdit { get; set; }
    //public bool HasDelete { get; set; }

  }
}
