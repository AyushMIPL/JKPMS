using App.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Collections.Generic;
using App.Data.ViewModels;
using System.Web.Routing;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.API
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string Region { get; set; }

        public string deviceToken { get; set; }
        public string deviceTokenType { get; set; }
        public int UserId { get; set; }
    }


}