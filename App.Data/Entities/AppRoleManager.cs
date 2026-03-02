using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class AppRoleManager : RoleManager<AppRole, int>
  {
    public AppRoleManager(IRoleStore<AppRole, int> roleStore)
        : base(roleStore)
    {
    }

    public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
    {
      return new AppRoleManager(new RoleStore<AppRole, int, AppUserRole>(context.Get<AppDbContext>()));
    }
  }
}
