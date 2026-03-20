using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("MasterEmails")]
    public class MasterEmail : BaseEntity
    {
        [StringLength(255)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}
