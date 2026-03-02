using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using App.Data;
using System.Collections.Generic;

namespace App.Data.Entities
{
    public class AppUserManager : UserManager<AppUser, Int32>
    {
        public AppUserManager(IUserStore<AppUser, Int32> store)
            : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
            var dbContext = new AppDbContext(ConnectionStringProvider.GetConnectionString());
            var userStore = new UserStore<AppUser, AppRole, Int32, AppUserLogin, AppUserRole, AppUserClaim>(dbContext);
            var manager = new AppUserManager(userStore);

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AppUser, Int32>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {

                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            //manager.RegisterTwoFactorProvider(
            //		"PhoneCode",
            //		new PhoneNumberTokenProvider<CmsUser, Int32>
            //		{
            //			MessageFormat = "Your security code is: {0}"
            //		});

            //manager.RegisterTwoFactorProvider(
            //		"EmailCode",
            //		new EmailTokenProvider<CmsUser, Int32>
            //		{
            //			Subject = "Security Code",
            //			BodyFormat = "Your security code is: {0}"
            //		});

            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, Int32>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public static String GetUserName()
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;

            return principal.Identity.Name;
        }

        public static int GetUserId()
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Thread.CurrentPrincipal;

            int userId;
            int.TryParse(principal.Identity.GetUserId(), out userId);
            return userId;
        }

        //not used anywhere
        //public static List<UserProfile> GetDispatchers123()
        //{
        //  AppDbContext DbContext = new AppDbContext();
        //  var entity = (from ep in DbContext.Users
        //                join e in DbContext.UserProfiles on ep.Id equals e.UserId
        //                where ep.Roles.Any((r => r.RoleId == 3))
        //                select e);
        //  return entity.ToList();
        //}

        //public const string DRIVER_ROLE = "Driver";
        //public const string DISPATCHER_ROLE = "Dispatcher";

        //public static IEnumerable<UserProfile> GetDrivers()
        //{
        //  AppDbContext DbContext = new AppDbContext();
        //  var entity = from role in DbContext.Roles
        //               where role.Name == DRIVER_ROLE
        //               from userRoles in role.Users
        //               join user in DbContext.Users on userRoles.UserId equals user.Id
        //               join profile in DbContext.UserProfiles on user.Id equals profile.UserId
        //               where profile.IsActive == true
        //               select profile;

        //  return entity;
        //}
        //public static IEnumerable<UserProfile> GetDispatchers()
        //{
        //  AppDbContext DbContext = new AppDbContext();
        //  var entity = from role in DbContext.Roles
        //               where role.Name == DISPATCHER_ROLE
        //               from userRoles in role.Users
        //               join user in DbContext.Users on userRoles.UserId equals user.Id
        //               join profile in DbContext.UserProfiles on user.Id equals profile.UserId
        //               where profile.IsActive == true
        //               select profile;

        //  return entity;
        //}


    }
}