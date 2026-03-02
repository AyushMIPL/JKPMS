using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
    public class GeneratePensionProcessModel
    {
        //[Required(ErrorMessage = "Enter Direct Deposits Cheques (Y/N) First")]
        //[RegularExpression("Y|N", ErrorMessage = "Enter only Y Or N")]
        [Display(Name = "Direct Deposits Cheques?")]
        public string DirectDepositsCheques { get; set; }

        //[Required(ErrorMessage = "Select Payroll Cash Account First")]
        [Display(Name = "Payroll Cash Account")]
        public string PayrollCashAccount { get; set; }

        //[Required(ErrorMessage = "Select Employee Type First")]
        [Display(Name = "Scheme Type")]
        public string EmployeeType { get; set; }


        [Display(Name = "Starting Payslip No.")]
        public string StartingChequeNo { get; set; }

        //[Required(ErrorMessage = "Select Deposit Date First")]
        [Display(Name = "Deposit Date *")]
        public DateTime? DepositDate { get; set; }

        //[Required(ErrorMessage = "Select Pay Date First")]
        [Display(Name = "Pay Date")]
        public DateTime? PayDate { get; set; }

        //[Required(ErrorMessage = "Select Pay Date First")]
        [Display(Name = "Pay Date To")]
        public DateTime? PayDateTo { get; set; }

        //[Required(ErrorMessage = "Enter Generate Cheques (Y/N) First")]
        //[RegularExpression("Y|N", ErrorMessage = "Enter only Y Or N")]
        [Display(Name = "Generate Cheques? *")]
        public string GenerateCheques { get; set; }

        //[Required(ErrorMessage = "Select Department First")]
        [Display(Name = "Department")]
        public string Department { get; set; }

        //[Required(ErrorMessage = "Select Bank Code First")]
        [Display(Name = "Bank Code *")]
        public string BanckCode { get; set; }

        [Display(Name = "Cheque Number")]
        public string ChequeNumber { get; set; }

        [Display(Name = "Prepared by")]
        public string Preparedby { get; set; }

        [Display(Name = "Checked by")]
        public string Checkedby { get; set; }

        [Display(Name = "Approved by")]
        public string Approvedby { get; set; }
        [Display(Name = "Received by")]
        public string Receivedby { get; set; }

        //[Display(Name = "Employer")]
        //public string EmployerId { get; set; }
        [Display(Name = "Pensioner")]
        public string EmployeeId { get; set; }
        public int AgeInYears { get; set; }
        [NotMapped]
        [Display(Name = "Scheme Type")]
        public string EmpType { get; set; }
        [NotMapped]
        public string RegionNames { get; set; }

    }
}
