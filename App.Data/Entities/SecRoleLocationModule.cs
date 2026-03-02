using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class SecRoleLocationModule : BaseEntity
  {

    public int RoleID { get; set; }

    public int RegionID { get; set; }

    public int DistrictID { get; set; }

    public int UserId { get; set; }
  }
}
