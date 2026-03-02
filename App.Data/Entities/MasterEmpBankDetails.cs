using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Data.Entities
{
    public class MasterEmpBankDetails
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public MasterBanks bank
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.MasterBanks.Where(x => x.bank_code == this.bank_code).FirstOrDefault();
            }
        }
        [Key]
        public int emp_bank_ID { get; set; }
        public string empl_code { get; set; }
        public Nullable<int> line_no { get; set; }
        [Required(ErrorMessage = "Bank Code")]
        [Display(Name = "Bank *")]
        public string bank_code { get; set; }
        [Required(ErrorMessage = "Account Number")]
        [Display(Name = "Account Number *")]
        public string bank_acct_no { get; set; }
        [Required(ErrorMessage = "Type")]
        [Display(Name = "Type *")]
        public string type { get; set; }
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Enter only decimal value")]
        [Required(ErrorMessage = "Amount")]
        [Display(Name = "Amount *")]
        public decimal? amount { get; set; }
        [Required(ErrorMessage = "Account Type")]
        [Display(Name = "Account Type *")]
        public string typeofacct { get; set; }
        public DateTime? V_DATE { get; set; }

        [Display(Name = "CBS NAME")]
        public string CBS_NAME { get; set; }
        [Display(Name = "Branch Code")]
        public string BRANCH_CODE { get; set; }
        [Display(Name = "Account Status")]
        public string ACCOUNT_STATUS { get; set; }
        [Display(Name = "Aadhar Status")]
        public string AADHAAR_STATUS { get; set; }
        [Display(Name = "IFSC Code")]
        public string APPLICANT_BANK_IFSC_CODE { get; set; }
        public string APPLICATION_REFERENCE_NO { get; set; }
        public string BankName { get; set; }
        public string UploadedBy { get; set; }
        public string downloadedBy { get; set; }
        public DateTime? downloadedOn { get; set; }

        [DefaultValue("false")]
        public bool? IsUpload { get; set; }
        [Required(ErrorMessage = "Remarks is required.")]
        [Display(Name = "Remarks *")]
        public string Remarks { get; set; }

        [Display(Name = "Account Scheme Type")]
        public string ACCT_SCHEME_TYPE { get; set; }
        public string Bank_document { get; set; }
        [NotMapped]
        [Display(Name = "Supportive document ")]
        public string document { get; set; }


    }

    public class DownloadedMediaVM
    {
        public string empl_code { get; set; }
        public string Area { get; set; }
        public string SchemeType { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string DownloadedBy { get; set; }
        public string APPLICANT_BANK_IFSC_CODE { get; set; }
        public DateTime? DownloadedOn { get; set; }
        public string UploadedBy { get; set; }
    }
}
