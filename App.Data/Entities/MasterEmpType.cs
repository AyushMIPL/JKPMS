using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  [Table("MasterEmpType")]
  public class MasterEmpType
  {
    
    [Key]
    public int Emp_type_ID { get; set; }
    [Display(Name = "Type Code")]
    public string Type_Code { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Cash acct")]
    public int cash_acct { get; set; }

    [Display(Name = "Department")]
    public string Department { get; set; }

    [Display(Name = "Employee Status")]
    public string empl_status { get; set; }

    [Display(Name = "Pay Period")]
    public string pay_period { get; set; }

    [Display(Name = "Vac Code")]
    public string vac_code { get; set; }

    [Display(Name = "Vac Allowed")]
    public decimal? vac_allowed { get; set; }

    [Display(Name = "Sick Code")]
    public string sick_code { get; set; }

    [Display(Name = "Sick Allowed")]
    public decimal? sick_allowed { get; set; }

    [Display(Name = "Hold Payment")]
    public string hold_pymnt { get; set; }

    [Display(Name = "Statax Code")]
    public string statax_code { get; set; }


    [Display(Name = "Loctax Code")]
    public string loctax_code { get; set; }

    [Display(Name = "Sick Accr code")]
    public string sick_accr_code { get; set; }

    [Display(Name = "Vac Accr Code")]
    public string vac_accr_code { get; set; }
  }
}
