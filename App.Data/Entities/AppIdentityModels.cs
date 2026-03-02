using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{

    public class AppUser : IdentityUser<Int32, AppUserLogin, AppUserRole, AppUserClaim>
    {
        //public AppUser()
        //{
        //  this.ApprovalProcessAssignedUsers = new HashSet<ApprovalProcessAssignedUser>();
        //}
        //public virtual ICollection<ApprovalProcessAssignedUser> ApprovalProcessAssignedUsers { get; set; }

        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();

        [Required]
        [Index(IsUnique = true)]
        public override string UserName { get; set; }

        [StringLength(256)]
        [Index(IsUnique = true)]
        public override string Email { get; set; }

        //public override int UserId { get; set; }
        public UserProfile UserProfile
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.UserProfiles.FirstOrDefault(x => x.UserId == this.Id);
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager userManager, string authenticationType)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }

    }
    public class AppRole : IdentityRole<Int32, AppUserRole>
    {
    }

    public class AppUserClaim : IdentityUserClaim<Int32>
    {
    }

    public class AppUserLogin : IdentityUserLogin<Int32>
    {
    }

    public class AppUserRole : IdentityUserRole<Int32>
    {
    }
}
