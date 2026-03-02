using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class MasterBanks
  {
    [Key]
    public int Bank_Code_ID { get; set; }
    [Display(Name = "Bank Code")]
    [Required(ErrorMessage = "Please Enter Bank Code")]
    public string bank_code { get; set; }
    [Display(Name = "Bank Name")]
    [Required(ErrorMessage = "Please Enter Bank Name")]
    public string bank_desc { get; set; }
    public Nullable<int> dfi_dest { get; set; }
    public Nullable<short> chk_digit { get; set; }
    public string dd_bank_code { get; set; }
    [Display(Name = "Bank Account Number")]
    [Required(ErrorMessage = "Please Enter Bank Account Number")]
    public string co_bank_acct_no { get; set; }
    public Nullable<int> cash_acct_no { get; set; }
    public string offset_debit { get; set; }
    [StringLength(1)]
    [Display(Name = "Sftp Media")]
    [Required(ErrorMessage = "Please Enter Sftp Media")]
    public string mag_media { get; set; }
    public string dd_create { get; set; }
    public string dd_format { get; set; }
    public string dd_transfer { get; set; }
    public string suppliercode { get; set; }
  }
}
