using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    [Table("MailTemplate")]
    public class MailTemplate : BaseEntity
    {
        [StringLength(200)]
        public string TemplateName { get; set; }

        [StringLength(200)]
        public string ProcessKey { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Body { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ToEmails { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string CcEmails { get; set; }
    }
}
