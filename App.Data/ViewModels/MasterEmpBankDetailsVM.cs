using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace App.Data.ViewModels
{
    public class MasterEmpBankDetailsVM
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

        public string emp_bank_ID { get; set; }
        public string empl_code { get; set; }
        public Nullable<int> line_no { get; set; }
        [Required(ErrorMessage = "Bank Code")]
        [Display(Name = "Bank Code *")]
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

        public string CBS_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string ACCOUNT_STATUS { get; set; }
        public string AADHAAR_STATUS { get; set; }
        public string APPLICANT_BANK_IFSC_CODE { get; set; }
        public string BankName { get; set; }
        public string downloadedBy { get; set; }
        public DateTime? downloadedOn { get; set; }
        public bool IsUpload { get; set; }
        public string Remarks { get; set; }
        public string ACCT_SCHEME_TYPE { get; set; }
        public string Bank_document { get; set; }
        public HttpPostedFileBase document { get; set; }
    }
}
