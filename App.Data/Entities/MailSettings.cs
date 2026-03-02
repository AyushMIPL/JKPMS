using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MailSettings : BaseEntity
  {
    [Required(ErrorMessage = "Enter ProcessName First")]
    [Display(Name = "Process Name *")]
    [StringLength(250)]
    public string ProcessName { get; set; }

    [Required(ErrorMessage = "Enter Mail To First")]
    [Display(Name = "Mail To (Comma Separated) *")]
    public string MailTo { get; set; }

    [Display(Name = "CC (Comma Separated)")]
    public string CC { get; set; }

    [Display(Name = "BCC (Comma Separated)")]
    public string BCC { get; set; }

    [Display(Name = "Default Subject")]
    public string Subject { get; set; }


    //[Required(ErrorMessage = "Enter Contents")]
    [Display(Name = "Default Contents ")]
    //[AllowHtml]
    public string Contents { get; set; }
    [Display(Name = "Is Send Notification Alert")]
    public bool IsSendNotificationAlert { get; set; }

    //public int EmailTemplateId { get; set; }
    [Display(Name = "Process Will Start At")]
    public DateTime? ProcessWillStartAt { get; set; }
    [NotMapped]
    public string ProcessWillStartAtStr { get; set; }
    [Display(Name = "Delay Time *")]

    public decimal DelayTime { get; set; }
    [Display(Name = "Priority")]
    public int? Priority { get; set; }

    [Display(Name = "Is Sent Mail Instantly")]
    public bool IsInstantMailing { get; set; }
  }
}
