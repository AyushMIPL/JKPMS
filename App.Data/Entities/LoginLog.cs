using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class LoginLog :BaseEntity
  {
    public int UserId { get; set; }
    public string UserName { get; set; }
    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }
    public string RegionName { get; set; }
    
  }
}
